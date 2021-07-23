using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Configurations
{
<<<<<<< HEAD:TssT.DataAccess/Configurations/TopicGroupConfiguration.cs
    public class TopicGroupConfiguration:IEntityTypeConfiguration<TopicGroup>
    {
        public void Configure(EntityTypeBuilder<TopicGroup> builder)
=======
    public class TopicConfiguration: IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
>>>>>>> 6117b47 (feature: task-6 / добавил интерфейс и класс для доступа к данным о топиках):TssT.DataAccess/Configurations/TopicConfiguration.cs
        {
            builder.HasKey(obj => obj.Id);
        }
    }
}