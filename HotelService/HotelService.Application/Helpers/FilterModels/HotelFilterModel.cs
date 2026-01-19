namespace HotelService.Application.Helpers.FilterModels;

public record HotelFilterModel(
    byte? Stars,
    string Country,
    string City,
    decimal? MinRating,
    decimal? MinPrice,
    decimal? MaxPrice);