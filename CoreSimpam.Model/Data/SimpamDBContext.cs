using Microsoft.EntityFrameworkCore;
using CoreSimpam.Model;

namespace CoreSimpam.Model.Data
{
    public class SimpamDBContext: DbContext
    {
        public SimpamDBContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel
                {
                    RoleID = 1,
                    RoleName = "root",
                    IsEnabled = true
                }
            );
        }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ScreenModel> Screen { get; set; }
        public DbSet<RoleScreenModel> RoleScreen { get; set; }
    }
}
