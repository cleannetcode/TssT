using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TssT.DataAccess.Configurations;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public DbSet<Answer> answers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<LevelImportance> LevelImportances { get; set; }
        public DbSet<LevelKnowledge> LevelKnowledges { get; set; }
        
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnswerConfiguration());
            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new LevelImportanceConfiguration());
            builder.ApplyConfiguration(new LevelKnowledgeConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
            base.OnModelCreating(builder);
        }
    }
}