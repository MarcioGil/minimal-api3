using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DbContexto>(options =>
{
  var conn = builder.Configuration.GetConnectionString("mysql");
  if (string.IsNullOrEmpty(conn))
  {
    throw new InvalidOperationException("Connection string 'mysql' not found in configuration.");
  }
  options.UseMySql(conn, ServerVersion.AutoDetect(conn));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Endpoint de login
app.MapPost("/login", (LoginDTO loginDTO) =>
{
  if (loginDTO.Email == "admin@test.com" && loginDTO.Senha == "12345678")
  {
    return Results.Ok("Login successful");
  }
  return Results.Unauthorized();
});

// Endpoint para listar administradores
app.MapGet("/administradores", async (DbContexto db) =>
{
  return await db.Administradores.ToListAsync();
});

// Endpoint para adicionar administrador
app.MapPost("/administradores", async (DbContexto db, Administrador admin) =>
{
  db.Administradores.Add(admin);
  await db.SaveChangesAsync();
  return Results.Created($"/administradores/{admin.Id}", admin);
});

app.Run();
