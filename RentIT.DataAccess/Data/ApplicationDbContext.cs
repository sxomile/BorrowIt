using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentIT.Models;

namespace RentIT.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Order> Orders { get; set; }
	public DbSet<Country> Countries { get; set; }
	public DbSet<City> Cities { get; set; }
	public DbSet<Report> Reports { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Order>()
			.HasOne(o => o.Lender)
			.WithMany()
			.HasForeignKey(o => o.LenderId)
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<Order>()
			.HasOne(o => o.Borrower)
			.WithMany()
			.HasForeignKey(o => o.BorrowerId)
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<City>()
			.HasOne(c => c.Country)
			.WithMany(co => co.Cities)
			.HasForeignKey(c => c.CountryId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<ApplicationUser>()
			.HasOne(au => au.CityFromID)
			.WithMany(c => c.CityFromUsers)  
			.HasForeignKey(au => au.CityFromIDId)
			.OnDelete(DeleteBehavior.Restrict);  

		modelBuilder.Entity<ApplicationUser>()
			.HasOne(au => au.CityOfResidence)
			.WithMany(c => c.CityOfResidenceUsers)  
			.HasForeignKey(au => au.CityOfResidenceId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Report>()
			.HasOne(r => r.Admin)
			.WithMany(a => a.CreatedReports)
			.HasForeignKey(r => r.AdminId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Report>()
			.HasOne(r => r.User)
			.WithMany(u => u.ReceivedReports) 
			.HasForeignKey(r => r.UserId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
