using CarRecommendation.Api.DTOs;
using CarRecommendation.Api.Models;
using MongoDB.Driver;

namespace CarRecommendation.Api.Services;

public class RecommendationService(IMongoDatabase db)
{
    private readonly IMongoCollection<Car> _cars = db.GetCollection<Car>("Cars");

    public async Task<List<RecommendationResultDto>> RecommendAsync(UserPreferenceDto prefs)
    {
        // Hard filters: budget range and minimum seats
        var filter = Builders<Car>.Filter.Lte(c => c.Price, prefs.MaxBudget)
                   & Builders<Car>.Filter.Gte(c => c.Price, prefs.MinBudget)
                   & Builders<Car>.Filter.Gte(c => c.SeatingCapacity, prefs.MinSeats);

        var candidates = await _cars.Find(filter).ToListAsync();

        var scored = candidates.Select(car => Score(car, prefs))
                               .OrderByDescending(r => r.MatchScore)
                               .Take(3)
                               .ToList();

        return scored;
    }

    private static RecommendationResultDto Score(Car car, UserPreferenceDto prefs)
    {
        int score = 50;
        var reasons = new List<string>();

        // Preference matching: PrimaryUse vs PrimaryAttributes
        if (!string.IsNullOrWhiteSpace(prefs.PrimaryUse) &&
            car.PrimaryAttributes.Any(a => a.Equals(prefs.PrimaryUse, StringComparison.OrdinalIgnoreCase)))
        {
            score += 20;
            reasons.Add($"matches your \"{prefs.PrimaryUse}\" lifestyle");
        }

        // TopPriority scoring
        switch (prefs.TopPriority?.ToLowerInvariant())
        {
            case "safety":
                if (car.SafetyRating >= 4)
                {
                    score += 20;
                    reasons.Add($"{car.SafetyRating}-star safety rating");
                }
                break;

            case "fueleconomy" or "fuel economy":
                var isEcoFuel = car.FuelType.Equals("Electric", StringComparison.OrdinalIgnoreCase)
                             || car.FuelType.Equals("Hybrid", StringComparison.OrdinalIgnoreCase);
                if (car.Mileage > 19 || isEcoFuel)
                {
                    score += 20;
                    reasons.Add(isEcoFuel
                        ? $"{car.FuelType} powertrain for low running cost"
                        : $"{car.Mileage} km/l fuel efficiency");
                }
                break;
        }

        // Budget fit bonus: if price is in the lower half of their range, reward it
        var midpoint = (prefs.MinBudget + prefs.MaxBudget) / 2;
        if (car.Price <= midpoint)
        {
            score += 10;
            reasons.Add("fits well within your budget");
        }

        // Cap at 100
        score = Math.Min(score, 100);

        var matchReason = reasons.Count > 0
            ? string.Join(", ", reasons)
            : "solid all-rounder in your budget";

        return new RecommendationResultDto
        {
            Car = car,
            MatchScore = score,
            MatchReason = char.ToUpper(matchReason[0]) + matchReason[1..]
        };
    }
}
