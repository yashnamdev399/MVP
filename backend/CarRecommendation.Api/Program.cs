using MongoDB.Driver;
using CarRecommendation.Api.Data;
using CarRecommendation.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- LOGGING SETUP ---
Console.WriteLine("--> [INIT] Starting Car Recommendation API...");

// MongoDB Configuration Logging
var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");
var connectionString = mongoSettings["ConnectionString"]!;
var databaseName = mongoSettings["DatabaseName"]!;

Console.WriteLine($"--> [CONFIG] Database Name: '{databaseName}'");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("--> [ERROR] MongoDB Connection String is MISSING or NULL!");
}
else
{
    Console.WriteLine("--> [CONFIG] MongoDB Connection String successfully loaded from environment.");
}

// MongoDB Services Registration
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(databaseName));

// Custom Application Services
builder.Services.AddSingleton<RecommendationService>();

builder.Services.AddControllers();

// CORS Settings
Console.WriteLine("--> [CONFIG] Setting up CORS Policy for 'https://mvpproj.netlify.app'...");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlify", policy =>
    {
        policy.WithOrigins("https://mvpproj.netlify.app") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
    
var app = builder.Build();

// --- DATA SEEDING WITH LOGGING & TRY-CATCH ---
try
{
    Console.WriteLine("--> [DATABASE] Attempting to initialize database seed data...");
    await SeedData.InitializeAsync(app.Services);
    Console.WriteLine("--> [DATABASE] Seed data initialized successfully or records already exist.");
}
catch (Exception ex)
{
    Console.WriteLine($"--> [CRITICAL ERROR] Database initialization failed: {ex.Message}");
    Console.WriteLine($"--> [STACK TRACE] {ex.StackTrace}");
    // App ko crash nahi hone dega, logs mein error print karke chalta rahega
}

// Middleware Configuration Routing
app.UseCors("AllowNetlify");
app.MapControllers();

Console.WriteLine("--> [READY] Kestrel Web Server is fully configured. Running application...");
app.Run();