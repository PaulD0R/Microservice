using System.Linq.Expressions;
using HotelService.Application.Helpers.FilterModels;
using HotelService.Application.Helpers.SortModels;
using HotelService.Domain.Entities;

namespace HotelService.Application.Helpers.HelperExtensions;

public static class HotelHelperExtension
{
    extension(IQueryable<Hotel> hotels)
    {
        public IQueryable<Hotel> Filter(HotelFilterModel hotelFilterModel)
        {
            if (hotelFilterModel.Stars != null)
                hotels = hotels.Where(h => h.Stars == hotelFilterModel.Stars);
        
            if (!string.IsNullOrEmpty(hotelFilterModel.Country))
                hotels = hotels.Where(h => h.Country == hotelFilterModel.Country);
        
            if (!string.IsNullOrEmpty(hotelFilterModel.City))
                hotels = hotels.Where(h => h.City == hotelFilterModel.City);
        
            if (hotelFilterModel.MinRating != null)
                hotels = hotels.Where(h => h.RealRating >= hotelFilterModel.MinRating);
        
            if (hotelFilterModel.MinPrice != null)
                hotels = hotels.Where(h => h.MinPrice >= hotelFilterModel.MinPrice);
            
            if (hotelFilterModel.MaxPrice != null)
                hotels = hotels.Where(h => h.MinPrice <= hotelFilterModel.MaxPrice);
        
            return hotels;
        }

        public IQueryable<Hotel> Order(HotelSortModel hotelSortModel)
        {
            return hotelSortModel.Ascending ?
                hotels.OrderBy(GetFilterPredicate(hotelSortModel.OrderBy)) : 
                hotels.OrderByDescending(GetFilterPredicate(hotelSortModel.OrderBy));
        }
    }

    private static Expression<Func<Hotel, object>> GetFilterPredicate(string orderBy)
    {
        if (string.IsNullOrEmpty(orderBy))
            return h => h.Id;

        return orderBy switch
        {
            nameof(Hotel.RealRating) => h => h.RealRating,
            nameof(Hotel.Stars) => h => h.Stars,
            _ => h => h.Id
        };
    }
}