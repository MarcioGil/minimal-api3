
using MINIMAL_API3.Dominio.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", (LoginDTO loginDTO) =>
{
    if (loginDTO.Email == "admin@test.com" && loginDTO.Senha == "12345678")
    {
        return Results.Ok("Login successful");
    }
    return Results.Unauthorized();
});

app.Run();
