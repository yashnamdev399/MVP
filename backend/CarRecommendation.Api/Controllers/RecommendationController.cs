using Microsoft.AspNetCore.Mvc;
using CarRecommendation.Api.DTOs;
using CarRecommendation.Api.Services;

namespace CarRecommendation.Api.Controllers;

[ApiController]
[Route("api/cars")]
public class RecommendationController(RecommendationService service) : ControllerBase
{
    [HttpPost("recommend")]
    public async Task<ActionResult<List<RecommendationResultDto>>> Recommend(UserPreferenceDto prefs)
    {
        if (prefs.MaxBudget <= 0)
            return BadRequest("MaxBudget is required.");

        var results = await service.RecommendAsync(prefs);

        if (results.Count == 0)
            return NotFound("No cars match your criteria. Try adjusting your budget or seat requirement.");

        return Ok(results);
    }
}
