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


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var hotelIds = ChangeTracker.Entries<HotelRoom>()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        (e.State == EntityState.Modified && e.Property(r => r.Price).IsModified))
            .Select(e => e.Entity.HotelId).Distinct().ToList();
        
        if (!hotelIds.Any())
            return await base.SaveChangesAsync(cancellationToken);
        
        var result = await base.SaveChangesAsync(cancellationToken);

        await UpdateHotelsMinPrice(hotelIds, cancellationToken);

        return result;
    }

    private async Task UpdateHotelsMinPrice(IEnumerable<Guid> hotelIds, CancellationToken ct)
    {
        foreach (var id in hotelIds)
        {
            var minPrice = await HotelRooms
                .Where(r => r.HotelId == id)
                .MinAsync(r => (decimal?)r.Price, ct) ?? 0;

            await Hotels
                .Where(h => h.Id == id)
                .ExecuteUpdateAsync(s => 
                    s.SetProperty(h => h.MinPrice, minPrice), ct);
        }
    }
}