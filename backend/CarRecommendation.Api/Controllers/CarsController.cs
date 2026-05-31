using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CarRecommendation.Api.Models;

namespace CarRecommendation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController(IMongoDatabase db) : ControllerBase
{
    private readonly IMongoCollection<Car> _cars = db.GetCollection<Car>("Cars");

    [HttpGet]
    public async Task<List<Car>> GetAll() =>
        await _cars.Find(_ => true).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetById(string id)
    {
        var car = await _cars.Find(c => c.Id == id).FirstOrDefaultAsync();
        return car is null ? NotFound() : car;
    }

    [HttpPost]
    public async Task<ActionResult<Car>> Create(Car car)
    {
        await _cars.InsertOneAsync(car);
        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }
}
