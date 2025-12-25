using HotelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Core;

public class ApplicationDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<HotelComment>  HotelComments { get; set; }
    public DbSet<HotelPhoto> HotelPhotos { get; set; }
    public DbSet<HotelRating> HotelRatings { get; set; }
    public DbSet<HotelRoom> HotelRooms { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<RoomPhoto> RoomPhotos { get; set; }
    public DbSet<RoomState> RoomStates { get; set; }
    
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :  base(options)
    {
        
    }
}