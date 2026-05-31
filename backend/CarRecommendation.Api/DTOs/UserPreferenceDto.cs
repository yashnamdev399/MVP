namespace CarRecommendation.Api.DTOs;

public class UserPreferenceDto
{
    public int MaxBudget { get; set; }
    public int MinBudget { get; set; }
    public int MinSeats { get; set; }
    public string PrimaryUse { get; set; } = string.Empty;
    public string TopPriority { get; set; } = string.Empty;
}
