using Asseti_Fi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Asseti_Fi.Dto;

namespace Asseti_Fi.Aessiti_Fi_DBContext;

public class AesstsDBContext : DbContext
{
    public AesstsDBContext(DbContextOptions<AesstsDBContext> options) : base(options)
    {
        
    }
    
    // public DbSet<User>? Users { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetAllocation> AssetAllocations { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }
    public DbSet<AuditRequest> AuditRequests { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AssetAllocation>()
            .HasOne(a => a.Asset)
            .WithMany(a => a.Allocations)
            .HasForeignKey(a => a.AssetId);

        modelBuilder.Entity<AssetAllocation>()
            .HasOne(a => a.Asset)
            .WithMany(a => a.Allocations)
            .HasForeignKey(a => a.AssetId);

        modelBuilder.Entity<AuditRequest>()
            .HasOne(a => a.Admin)
            .WithMany()  // Assuming Admin does not need a collection of AuditRequests
            .HasForeignKey(a => a.AdminId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent Admin deletion if linked to AuditRequest

        modelBuilder.Entity<AuditRequest>()
            .HasOne(a => a.User)
            .WithMany(u => u.AuditRequests)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<AuditRequest>()
            .HasOne(a => a.Admin)
            .WithMany() // Assuming Admin does not have a collection of audit requests
            .HasForeignKey(a => a.AdminId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of Admin if related AuditRequests exist

        modelBuilder.Entity<AuditRequest>()
            .HasOne(a => a.User)
            .WithMany(u => u.AuditRequests) // User can have multiple audit requests
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<AssetAllocation>()
            .HasOne(a => a.User)
            .WithMany(u => u.Allocations)  // Assuming 'Allocations' is a navigation property in 'User'
            .HasForeignKey(a => a.UserId)
            .IsRequired();

        // Configure precision for AssetValue
        modelBuilder.Entity<Asset>()
            .Property(a => a.AssetValue)
            .HasPrecision(18, 2); // Precision: 18 total digits, 2 after the decimal point

        // One-to-Many: AssetAllocation -> Asset
        modelBuilder.Entity<AssetAllocation>()
            .HasOne(a => a.Asset)
            .WithMany(b => b.Allocations)  // Asset can have multiple allocations
            .HasForeignKey(a => a.AssetId);

        // One-to-Many: AssetAllocation -> User
        modelBuilder.Entity<AssetAllocation>()
            .HasOne(a => a.User)
            .WithMany(b => b.Allocations)  // User can have multiple allocations
            .HasForeignKey(a => a.UserId);

        // One-to-Many: ServiceRequest -> Asset
        modelBuilder.Entity<ServiceRequest>()
            .HasOne(s => s.Asset)
            .WithMany(a => a.ServiceRequests)  // Asset can have multiple service requests
            .HasForeignKey(s => s.AssetId);

        // One-to-Many: ServiceRequest -> User
        modelBuilder.Entity<ServiceRequest>()
            .HasOne(s => s.User)
            .WithMany(u => u.ServiceRequests)  // User can have multiple service requests
            .HasForeignKey(s => s.UserId);

        // One-to-Many: AuditRequest -> Admin (User)
        modelBuilder.Entity<AuditRequest>()  // Corrected from AssetsRequests to AuditRequest
            .HasOne(a => a.Admin)  // Assuming Admin is a User
            .WithMany()  // Admin can have many audit requests
            .HasForeignKey(a => a.AdminId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of Admin if there are related AuditRequests

        // One-to-Many: AuditRequest -> User
        modelBuilder.Entity<AuditRequest>()
            .HasOne(a => a.User)
            .WithMany(u => u.AuditRequests)  // User can have multiple audit requests
            .HasForeignKey(a => a.UserId);
    }


}