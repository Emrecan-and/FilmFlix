using Microsoft.EntityFrameworkCore;

namespace projects.Data{
    public class DataContext:DbContext{
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<Film> Films =>Set<Film>();

        public DbSet<User> Users =>Set<User>();

        public DbSet<List> Lists =>Set<List>();

    }
}