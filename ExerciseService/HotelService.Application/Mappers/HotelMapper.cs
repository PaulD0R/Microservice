using HotelService.Application.Models.HotelPhotos;
using HotelService.Application.Models.Hotels;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class HotelMapper
{
    public static Hotel ToHotel(this AddHotelRequest addHotelRequest)
    {
        return new Hotel
        {
            Name = addHotelRequest.Name,
            Country = addHotelRequest.Country,
            City = addHotelRequest.City,
            Stars = addHotelRequest.Stars,
            Description = addHotelRequest.Description
        };
    }

    extension(Hotel hotel)
    {
        public HotelDto ToHotelDto(HotelPhotoDto photo)
        {
            return new HotelDto(
                hotel.Id,
                hotel.Name,
                hotel.Country,
                hotel.City,
                hotel.Stars,
                photo);
        }

        public HotelFullDto ToHotelFullDto(IEnumerable<HotelPhotoDto> photos)
        {
            return new HotelFullDto(
                hotel.Id,
                hotel.Name,
                hotel.Country,
                hotel.City,
                hotel.Stars,
                hotel.Description,
                hotel.ShownRating,
                photos);
        }

        public void ToUpdateHotel(PatchHotelRequest patchHotelRequest)
        {
            var hotelType = typeof(Hotel);
            var patchHotelType = typeof(PatchHotelRequest);
            
            var properties = patchHotelType.GetProperties();
            foreach (var patchProp in properties)
            {
                var hotelProp = hotelType.GetProperty(patchProp.Name);
                if (hotelProp == null) continue;

                var patchValue = patchProp.GetValue(patchHotelRequest);

                if (patchValue == null)
                    continue;

                var targetType = hotelProp.PropertyType;

                var underlying = Nullable.GetUnderlyingType(patchProp.PropertyType);
                if (underlying != null && targetType == underlying)
                {
                    patchValue = Convert.ChangeType(patchValue, targetType);
                }

                hotelProp.SetValue(hotel, patchValue);
            }
        }
    }
}