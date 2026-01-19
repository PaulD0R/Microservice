using System.Linq.Expressions;
using HotelService.Application.Helpers.FilterModels;
using HotelService.Application.Helpers.SortModels;
using HotelService.Domain.Entities;

namespace HotelService.Application.Helpers.HelperExtensions;

public static class HotelRoomHelperExtension
{
    extension(IQueryable<HotelRoom> hotelRooms)
    {
        public IQueryable<HotelRoom> Filter(HotelRoomFilter hotelRoomFilter)
        {
            if (hotelRoomFilter.MinPrice != null)
                hotelRooms = hotelRooms.Where(r => r.Price >= hotelRoomFilter.MinPrice);
        
            if (hotelRoomFilter.MaxPrice != null)
                hotelRooms = hotelRooms.Where(r => r.Price >= hotelRoomFilter.MaxPrice);

            if (hotelRoomFilter.NumberOfResidents != null)
                hotelRooms = hotelRooms.Where(r => r.NumberOfResidents == hotelRoomFilter.NumberOfResidents);
            
            if (hotelRoomFilter is { StartDate: not null, EndDate: not null }) 
                hotelRooms = hotelRooms.Where(r => !r.States.Any(s => 
                    s.StartDate <= hotelRoomFilter.EndDate && s.EndDate >= hotelRoomFilter.StartDate));
            
            return hotelRooms;
        }

        public IQueryable<HotelRoom> Order(HotelRoomSortModel hotelRoomSortModel)
        {
            return hotelRoomSortModel.Ascending ? 
                hotelRooms.OrderBy(GetOrderPredicate(hotelRoomSortModel.OrderBy)) :
                hotelRooms.OrderByDescending(GetOrderPredicate(hotelRoomSortModel.OrderBy));
        }
    }

    private static Expression<Func<HotelRoom, object>> GetOrderPredicate(string orderBy)
    {
        if (string.IsNullOrEmpty(orderBy))
            return r => r.Id;

        return orderBy switch
        {
            nameof(HotelRoom.NumberOfResidents) => r => r.NumberOfResidents,
            nameof(HotelRoom.Price) => r => r.Price,
            _ => r => r.Id
        };
    }
}