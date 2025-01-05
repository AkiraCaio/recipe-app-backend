using RecipeAppBackend.Application.Interface;
using RecipeAppBackend.Application.Services;
using RecipeAppBackend.Application.Mapping;
using RecipeAppBackend.Domain.Interfaces;
using RecipeAppBackend.Extensions;
using RecipeAppBackend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Chama as extensões em cadeia
builder.Services
    .AddDbContexts(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddOpenApi()
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddCors(options =>
    {
        // TODO: DESENVOLVER CORS
        options.AddPolicy("MyPolicy", builder => 
        {
            builder.WithOrigins("https://meusite.com")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    })
    .AddAutoMapper(typeof(MappingProfile))
    .AddControllers(options => { 
        options.SuppressAsyncSuffixInActionNames = false;
    });

// Aqui é onde você injeta serviços e repositórios do Application/Infrastructure
// e.g., builder.Services.AddScoped<IRecipesRepository, RecipesRepository>();
//       builder.Services.AddScoped<IRecipesService, RecipesService>();
builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IAuthService, AuthService>();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();