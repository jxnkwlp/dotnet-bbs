using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleBBS.Core;

namespace SimpleBBS.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, long>
    {
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Reply> Reply { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasOne(t => t.UserInfo).WithOne(t => t.User);
            builder.Entity<UserInfo>().HasKey(t => t.UserId);

            builder.Entity<Topic>().HasOne(t => t.Tags).WithMany(t => t.Topics).HasForeignKey(t => t.TagsId);
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            if (entity is BaseEntity @base)
            {
                @base.CreationTime = DateTime.Now;
            }
            return base.Add(entity);
        }

        public override Task<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is BaseEntity @base)
            {
                @base.CreationTime = DateTime.Now;
            }
            return base.AddAsync(entity, cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
