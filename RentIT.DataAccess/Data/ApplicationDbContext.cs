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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		//modelBuilder.Entity<Order>()
		//		.HasOne(o => o.Item)
		//		.WithMany()
		//		.HasForeignKey(o => o.ItemId)
		//		.OnDelete(DeleteBehavior.NoAction);
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
		//// Configure the foreign key relationship for Borrower -> Order to have no action on delete
		//modelBuilder.Entity<Order>()
		//	.HasOne(o => o.Borrower)
		//	.WithMany()
		//	.HasForeignKey(o => o.BorrowerId)
		//	.OnDelete(DeleteBehavior.NoAction);

	}
}
