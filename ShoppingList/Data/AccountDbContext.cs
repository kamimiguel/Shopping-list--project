using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;

namespace ShoppingList.Data
{
    public class AccountDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LH8TJ1R\SQLEXPRESS;Initial Catalog=Shopping_List;Integrated Security=True;Pooling=False;Trusted_Connection=True;TrustServerCertificate=True");
        }
      
        public DbSet<UserAccount> userAccount{ get; set; }
    }
}
