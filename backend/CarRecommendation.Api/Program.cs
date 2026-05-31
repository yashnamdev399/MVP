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


await SeedData.InitializeAsync(app.Services);

app.UseCors("AllowNetlify");
app.MapControllers();

app.Run();
