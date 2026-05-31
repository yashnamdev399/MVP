using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarRecommendation.Api.Models;

public class Car
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Price { get; set; }
    public string FuelType { get; set; } = string.Empty;
    public string BodyType { get; set; } = string.Empty;
    public double Mileage { get; set; }
    public int SafetyRating { get; set; }
    public int SeatingCapacity { get; set; }
    public List<string> PrimaryAttributes { get; set; } = [];
}
