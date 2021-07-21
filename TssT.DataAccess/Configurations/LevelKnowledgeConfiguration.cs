using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Configurations
{
    public class LevelKnowledgeConfiguration:IEntityTypeConfiguration<LevelKnowledge>
    {
        public void Configure(EntityTypeBuilder<LevelKnowledge> builder)
        {
            builder.HasKey(obj => obj.Id);
        }
    }
}