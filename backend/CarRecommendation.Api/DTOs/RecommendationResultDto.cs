using CarRecommendation.Api.Models;

namespace CarRecommendation.Api.DTOs;

public class RecommendationResultDto
{
    public Car Car { get; set; } = null!;
    public int MatchScore { get; set; }
    public string MatchReason { get; set; } = string.Empty;
}
