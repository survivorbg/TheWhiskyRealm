namespace TheWhiskyRealm.Core.Contracts;

public interface IRatingService
{
    Task<double> GetAvgRatingAsync(int whiskyId);
}
