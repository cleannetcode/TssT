using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Configurations
{
    public class LevelImportanceConfiguration:IEntityTypeConfiguration<LevelImportance>
    {
        public void Configure(EntityTypeBuilder<LevelImportance> builder)
        {
            builder.HasKey(obj => obj.Id);
        }
    }
}