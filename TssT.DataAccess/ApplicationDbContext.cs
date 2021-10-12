using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using TssT.DataAccess.Configurations;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        //public DbSet<Answer> Answers { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Test> Tests { get; set; }
        //public DbSet<LevelImportance> LevelImportances { get; set; }
        //public DbSet<LevelKnowledge> LevelKnowledges { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}