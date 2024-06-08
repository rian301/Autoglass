using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoglass.Infra.Data.Map
{
    public class UserProfileMap : EntityTypeConfiguration<UserProfile>
    {
        public override void Map(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfile");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);
        }
    }
}
