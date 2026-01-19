namespace HotelService.Application.Helpers.FilterModels;

public record HotelRoomFilter(
    decimal? MinPrice,
    decimal? MaxPrice,
    byte? NumberOfResidents,
    DateOnly? StartDate,
    DateOnly? EndDate);