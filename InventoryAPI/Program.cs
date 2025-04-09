using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using InventoryAPI.Models;
using InventoryManagement.Repositories;
using InventoryTracker.Data;
using InventoryAPI.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Register JwtService as a singleton
builder.Services.AddSingleton<JwtService>();


// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretKey = builder.Configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured.");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });
builder.Services.AddAuthorization();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  // Add this line to register AutoMapper

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Add JWT Bearer security definition for Swagger UI
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer", // This is important as it tells Swagger it's using Bearer tokens
        BearerFormat = "JWT"
    });

    // Apply the security definition to all API operations
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();


// Configure database connection for SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlite(connectionString));

// Add CORS policy (if required)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Allow Angular app
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
        c.RoutePrefix = string.Empty; // Swagger UI at the root of the application
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization(); // Add authorization middleware

// Serve Angular static files from wwwroot folder
app.UseDefaultFiles();  // Looks for default files like index.html
app.UseStaticFiles();   // Enables serving static files (JS, CSS, etc.) from wwwroot



app.MapControllers(); // Enable API controller routing
app.MapFallbackToFile("index.html");  // If no API or static file matches the request, serve Angular's index.html (for client-side routing support)

app.Run();
