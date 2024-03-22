using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class RatingService : IRatingService
{
    private readonly IRepository repo;

    public RatingService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<double> GetAvgRatingAsync(int whiskyId)
    {
        var ratings = await repo
            .AllReadOnly<Rating>()
            .Where(r => r.WhiskyId == whiskyId)
            .ToListAsync();

        if (ratings.Any())
        {
            return ratings.Average(r => r.AveragePoints);
        }

        return -1;
    }
}
