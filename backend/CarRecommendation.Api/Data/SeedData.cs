using CarRecommendation.Api.Models;
using MongoDB.Driver;

namespace CarRecommendation.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<IMongoDatabase>();
        var collection = db.GetCollection<Car>("Cars");

        var count = await collection.CountDocumentsAsync(FilterDefinition<Car>.Empty);
        if (count > 0) return;

        await collection.InsertManyAsync(GetSeedCars());
    }

    private static List<Car> GetSeedCars() =>
    [
        new() { Make = "Maruti Suzuki", Model = "Swift", Price = 650000, FuelType = "Petrol", BodyType = "Hatchback", Mileage = 22.4, SafetyRating = 3, SeatingCapacity = 5, PrimaryAttributes = ["City Commuting", "Budget Friendly", "Low Maintenance"] },
        new() { Make = "Tata", Model = "Punch", Price = 615000, FuelType = "Petrol", BodyType = "SUV", Mileage = 20.1, SafetyRating = 5, SeatingCapacity = 5, PrimaryAttributes = ["City Commuting", "High Safety", "Budget Friendly"] },
        new() { Make = "Hyundai", Model = "i20", Price = 705000, FuelType = "Petrol", BodyType = "Hatchback", Mileage = 19.7, SafetyRating = 3, SeatingCapacity = 5, PrimaryAttributes = ["City Commuting", "Feature Rich", "Premium Feel"] },
        new() { Make = "Tata", Model = "Nexon", Price = 800000, FuelType = "Petrol", BodyType = "SUV", Mileage = 17.4, SafetyRating = 5, SeatingCapacity = 5, PrimaryAttributes = ["Highway Cruising", "High Safety", "Rough Roads"] },
        new() { Make = "Maruti Suzuki", Model = "Brezza", Price = 830000, FuelType = "Petrol", BodyType = "SUV", Mileage = 19.8, SafetyRating = 4, SeatingCapacity = 5, PrimaryAttributes = ["City Commuting", "Reliable", "Family Car"] },
        new() { Make = "Mahindra", Model = "XUV3XO", Price = 749000, FuelType = "Petrol", BodyType = "SUV", Mileage = 18.2, SafetyRating = 5, SeatingCapacity = 5, PrimaryAttributes = ["Performance", "High Safety", "Feature Rich"] },
        new() { Make = "Honda", Model = "City", Price = 1180000, FuelType = "Petrol", BodyType = "Sedan", Mileage = 17.8, SafetyRating = 5, SeatingCapacity = 5, PrimaryAttributes = ["Highway Cruising", "Premium Comfort", "Family Car"] },
        new() { Make = "Hyundai", Model = "Verna", Price = 1100000, FuelType = "Petrol", BodyType = "Sedan", Mileage = 18.6, SafetyRating = 5, SeatingCapacity = 5, PrimaryAttributes = ["Performance", "Highway Cruising", "Feature Rich"] },
        new() { Make = "Maruti Suzuki", Model = "Ertiga", Price = 865000, FuelType = "CNG", BodyType = "MUV", Mileage = 26.1, SafetyRating = 3, SeatingCapacity = 7, PrimaryAttributes = ["Family Car", "Value For Money", "City Commuting"] },
        new() { Make = "Kia", Model = "Seltos", Price = 1090000, FuelType = "Diesel", BodyType = "SUV", Mileage = 20.7, SafetyRating = 3, SeatingCapacity = 5, PrimaryAttributes = ["Feature Rich", "Highway Cruising", "Premium Feel"] },
        new() { Make = "Hyundai", Model = "Creta", Price = 1100000, FuelType = "Petrol", BodyType = "SUV", Mileage = 17.4, SafetyRating = 3, SeatingCapacity = 5, PrimaryAttributes = ["Status Symbol", "Family Car", "Comfortable"] },
        new() { Make = "Mahindra", Model = "Scorpio-N", Price = 1385000, FuelType = "Diesel", BodyType = "SUV", Mileage = 14.2, SafetyRating = 5, SeatingCapacity = 7, PrimaryAttributes = ["Rough Roads", "Offroader", "Powerful"] },
        new() { Make = "Toyota", Model = "Innova Hycross", Price = 1970000, FuelType = "Hybrid", BodyType = "MUV", Mileage = 23.2, SafetyRating = 5, SeatingCapacity = 7, PrimaryAttributes = ["Premium Comfort", "Family Car", "Ultra Reliable"] },
        new() { Make = "Mahindra", Model = "Thar", Price = 1135000, FuelType = "Diesel", BodyType = "SUV", Mileage = 15.2, SafetyRating = 4, SeatingCapacity = 4, PrimaryAttributes = ["Offroader", "Adventure", "Status Symbol"] },
        new() { Make = "Tata", Model = "Tiago EV", Price = 800000, FuelType = "Electric", BodyType = "Hatchback", Mileage = 250.0, SafetyRating = 4, SeatingCapacity = 5, PrimaryAttributes = ["City Commuting", "Eco Friendly", "Low Running Cost"] }
    ];
}
