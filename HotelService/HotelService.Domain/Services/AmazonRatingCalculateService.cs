using HotelService.Domain.Entities;
using HotelService.Domain.Interfaces.Services;

namespace HotelService.Domain.Services;

public class AmazonRatingCalculateService : IRatingCalculateService
{
    private const double TimeDecayFactor = 365.0;
    
    public decimal GetRealRating(ICollection<HotelRating> ratings)
    {
        if (ratings.Count == 0)
            return 0;

        double weightedSum = 0;
        double totalWeight = 0;
        var now = DateTime.UtcNow;

        foreach (var rating in ratings)
        {
            var daysOld = (now - rating.CreatedAt).TotalDays;
            if (daysOld < 0) daysOld = 0; 
            
            var weight = 1.0 / (1.0 + (daysOld / TimeDecayFactor));

            weightedSum += rating.Value * weight;
            totalWeight += weight;
        }

        if (totalWeight == 0) return 0;

        var result = weightedSum / totalWeight;

        return Math.Round((decimal)result, 2);
    }

    public decimal GetShownRating(ICollection<HotelRating> ratings)
    {
        if (ratings.Count == 0)
            return 0;

        var average = ratings.Average(r => r.Value); 

        return Math.Round((decimal)average, 1);
    }
}