using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MobileStore.Db.Configurations;
using MobileStore.Models;

namespace MobileStore.Db
{
    public class MobileStoreDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<MobilePhone> MobilePhones { get; set; }
        public DbSet<Media> Mediae { get; set; }

        public MobileStoreDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MobileStoreDb"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new MobilePhoneConfiguration());
            builder.ApplyConfiguration(new MediaConfiguration());
        }
    }
}
