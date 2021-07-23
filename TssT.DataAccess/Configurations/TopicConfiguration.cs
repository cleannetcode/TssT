using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Configurations
{
    public class TopicConfiguration: IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(obj => obj.Id);
        }
    }
}