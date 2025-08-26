using DotNetEnv;
using Microsoft.EntityFrameworkCore;

using WiventoryAPI.Data;
using WiventoryAPI.Services;
using WiventoryAPI.Models;
//using WiventoryAPI.Utils;
//using WiventoryAPI.Errors;


var builder = WebApplication.CreateBuilder(args);

// ========================
// Configure Database
// ========================

// Load environment variables from .env
Env.Load();

// Build the connection string from environment variables
var server = Environment.GetEnvironmentVariable("DB_SERVER");
var database = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Check for missing environment variables
var missingVars = new[] { ("DB_SERVER", server), ("DB_NAME", database), ("DB_USER", user), ("DB_PASSWORD", password) }
    .Where(x => string.IsNullOrWhiteSpace(x.Item2))
    .Select(x => x.Item1)
    .ToArray();

if (missingVars.Any())
{
    throw new Exception($"Missing required environment variable(s): {string.Join(", ", missingVars)}");
}

string connectionString = $"server={server};database={database};user={user};password={password};";

// Register the DbContext with dependency injection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// If using SQL Server:
// builder.Services.AddDbContext<WiventoryDbContext>(options =>
//     options.UseSqlServer(connectionString));

// ========================
// Register other services
// ========================
builder.Services.AddControllers();  // registers controllers

// Register Service as implementation of it Interface
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(typeof(IStorageService<>), typeof(StorageService<>));

// Add other services like StorageService, EmailService, DbContext, etc.
// builder.Services.AddScoped(typeof(StorageService<>)); // if generic storage
// builder.Services.AddScoped<IEmailService, EmailService>();

// Build the app
var app = builder.Build();


// routing middleware to parse incoming request URLs and determine route information
app.UseRouting();               

// optional: check if the user is authorized to access the route
app.UseAuthorization();         

// controller endpoints so matched requests actually call the controller actions
app.MapControllers();           


app.Use(async (context, next) => 
{
    await next();

    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
        {
            status = 404,
            error = "The requested URL was not found on this server",
            path = context.Request.Path
         }));
    }
});

// Enable middleware like HTTPS redirection or Swagger if needed
//app.UseHttpsRedirection();

// Run the application
app.Run();
