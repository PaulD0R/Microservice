using HotelService.Application.Models.HotelRooms;
using HotelService.Application.Models.RoomPhotos;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class HotelRoomMapper
{
    public static HotelRoom ToHotelRoom(this AddHotelRoomRequest addHotelRoomRequest)
    {
        return new HotelRoom
        {
            NumberOfResidents =  addHotelRoomRequest.NumberOfResidents,
            Description = addHotelRoomRequest.Description
        };
    }

    extension(HotelRoom hotelRoom)
    {
        public HotelRoomDto ToHotelRoomDto(RoomPhotoDto roomPhoto)
        {
            return new HotelRoomDto(
                hotelRoom.Id,
                hotelRoom.NumberOfResidents,
                roomPhoto);
        }

        public HotelRoomFullDto ToHotelRoomFullDto(IEnumerable<RoomPhotoDto> roomPhotos)
        {
            return new HotelRoomFullDto(
                hotelRoom.Id,
                hotelRoom.NumberOfResidents,
                hotelRoom.Description,
                roomPhotos);
        }
        
        public void ToUpdateHotelRoom(PatchHotelRoomRequest patchHotelRequest)
        {
            var hotelRoomType = typeof(HotelRoom);
            var patchHotelRoomType = typeof(PatchHotelRoomRequest);
            
            var properties = patchHotelRoomType.GetProperties();
            foreach (var patchProp in properties)
            {
                var hotelRoomProp = hotelRoomType.GetProperty(patchProp.Name);
                if (hotelRoomProp == null) continue;

                var patchValue = patchProp.GetValue(patchHotelRequest);

                if (patchValue == null)
                    continue;

                var targetType = hotelRoomProp.PropertyType;

                var underlying = Nullable.GetUnderlyingType(patchProp.PropertyType);
                if (underlying != null && targetType == underlying)
                {
                    patchValue = Convert.ChangeType(patchValue, targetType);
                }

                hotelRoomProp.SetValue(hotelRoom, patchValue);
            }
        }
    }
}