using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ShoppingList.Models;
using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Data
{
    public class ItemlistDbContext: DbContext
    {
        public ItemlistDbContext()
        {

        }
        public ItemlistDbContext(DbContextOptions<ItemlistDbContext>options):base(options)
        {
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.UserAccount)
                .WithMany(u => u.Items)
                .HasForeignKey(i => i.UserId);

            modelBuilder.Entity<Item>()
                .HasIndex(i => new { i.UserId, i.Name })
                .IsUnique();

        }

        public DbSet<Item> Items { get; set; }
    }
}

  