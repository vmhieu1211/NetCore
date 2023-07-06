global using Microsoft.EntityFrameworkCore;


namespace WebApiApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Post> Post => Set<Post>();
        public DbSet<User> User => Set<User>();
    }
}
