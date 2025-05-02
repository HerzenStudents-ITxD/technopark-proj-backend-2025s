using Microsoft.EntityFrameworkCore;
using TechnoparkProj.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnoparkProj.DataAccess.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.HasKey(x => x.Id);
            // внешние ключи

            builder.Property(b => b.Description).IsRequired();
        }
    }
}
