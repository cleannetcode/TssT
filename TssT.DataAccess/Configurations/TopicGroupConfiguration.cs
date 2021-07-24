using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Configurations
{
    public class TopicGroupConfiguration:IEntityTypeConfiguration<TopicGroup>
    {
        public void Configure(EntityTypeBuilder<TopicGroup> builder)
        {
            builder.HasKey(obj => obj.Id);
        }
    }
}