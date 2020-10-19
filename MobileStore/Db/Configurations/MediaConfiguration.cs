using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobileStore.Models;

namespace MobileStore.Db.Configurations
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.HasKey(media => media.Id);

            builder
                .HasOne<MobilePhone>()
                .WithMany(mobilePhone => mobilePhone.Mediae)
                .HasForeignKey(media => media.MobilePhoneId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
