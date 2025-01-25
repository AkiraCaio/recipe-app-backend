using RecipeAppBackend.Application.Interface;
using RecipeAppBackend.Application.Services;
using RecipeAppBackend.Application.Mapping;
using RecipeAppBackend.Domain.Interfaces;
using RecipeAppBackend.Domain.Entities;
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

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IRoleRepository, RoleRepository>()
    .AddScoped<IUserRoleRepository, UserRoleRepository>();

var app = builder.Build();

// Verifica e cria a role "User" se não existir
using (var scope = app.Services.CreateScope())
{
    var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
    await EnsureUserRoleExists(roleRepository);
}

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

async Task EnsureUserRoleExists(IRoleRepository roleRepository)
{
    var roleExists = await roleRepository.ExistsByNameAsync("User");
    if (!roleExists)
    {
        var userRole = new Role
        {
            Name = "User",
            Description = "Default role for new users",
            CreatedAt = DateTime.UtcNow
        };
        await roleRepository.AddAsync(userRole);
    }
}