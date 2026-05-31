using MongoDB.Driver;
using CarRecommendation.Api.Data;
using CarRecommendation.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// MongoDB
var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");
var connectionString = mongoSettings["ConnectionString"]!;
var databaseName = mongoSettings["DatabaseName"]!;

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(databaseName));

// Services
builder.Services.AddSingleton<RecommendationService>();

builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Seed data if collection is empty
await SeedData.InitializeAsync(app.Services);

app.UseCors();
app.MapControllers();

app.Run();
