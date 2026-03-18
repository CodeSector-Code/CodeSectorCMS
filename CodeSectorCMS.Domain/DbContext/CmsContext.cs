using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeSectorCMS.Domain
{
    public class CmsContext : IdentityDbContext<ApplicationUser>
    {
        public CmsContext(DbContextOptions<CmsContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<APIKey> APIKeys { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<User> Userss { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
        public DbSet<MailConfig> MailConfigs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<SubscriberCustomFieldValue> SubscriberCustomFieldValues { get; set; }
        public DbSet<SubscriberGroup> SubscriberGroups { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<SubscriberGroupSubscriberEntity> SubscriberGroupSubscriberEntitys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailConfig>()
                    .HasMany(u => u.Campaigns)
                    .WithOne(i => i.mailConfig)
                    .HasForeignKey(i => i.MailConfigID)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                    .HasMany(u => u.Campaigns)
                    .WithOne(i => i.User)
                    .HasForeignKey(i => i.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Template>()
                    .HasMany(u => u.Campaigns)
                    .WithOne(i => i.template)
                    .HasForeignKey(i => i.TemplateID)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                   .HasMany(u => u.Campaigns)
                   .WithOne(i => i.account)
                   .HasForeignKey(i => i.AccountID)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                   .HasMany(u => u.Subscribers)
                   .WithOne(i => i.User)
                   .HasForeignKey(i => i.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            //SeedData(modelBuilder);

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                    new Account {  }
                
                );
        }
    }
}
