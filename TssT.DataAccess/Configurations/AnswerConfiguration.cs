using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Configurations
{
    public class AnswerConfiguration:IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(obj => obj.Id);
        }
    }
}