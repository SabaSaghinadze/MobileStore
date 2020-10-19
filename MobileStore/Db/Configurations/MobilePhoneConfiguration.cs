using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobileStore.Models;

namespace MobileStore.Db.Configurations
{
    public class MobilePhoneConfiguration : IEntityTypeConfiguration<MobilePhone>
    {
        public void Configure(EntityTypeBuilder<MobilePhone> builder)
        {
            builder.HasKey(mobilePhone => mobilePhone.Id);

            builder.Property(mobilePhone => mobilePhone.CPU).HasDefaultValue("Unknown");
            builder.Property(mobilePhone => mobilePhone.Intelligibility).HasDefaultValue("Unknown");
            builder.Property(mobilePhone => mobilePhone.Manufacturer).HasDefaultValue("Unknown");
            builder.Property(mobilePhone => mobilePhone.Memory).HasDefaultValue(0);
            builder.Property(mobilePhone => mobilePhone.OperatingSystem).HasDefaultValue("Unknown");
            builder.Property(mobilePhone => mobilePhone.ScreenSize).HasDefaultValue("Unknown");
            builder.Property(mobilePhone => mobilePhone.Size).HasDefaultValue("Unknown");
            builder.Property(mobilePhone => mobilePhone.Weight).HasDefaultValue(0);
        }
    }
}
