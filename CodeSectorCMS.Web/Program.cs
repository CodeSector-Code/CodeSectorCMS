using System;
using CodeSectorCMS.Domain;
using Azure.Messaging.ServiceBus;
using CodeSectorCMS.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.Domain.Managers.Interfaces;
using CodeSectorCMS.Domain.Managers.Implementations;

namespace CodeSectorCMS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContextPool<CmsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CMSContext")));

            // Asp.Net Core Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<CmsContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DI
            builder.Services.AddScoped<UnityOfWork>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IAPIKeyManager, APIKeyManager>();
            builder.Services.AddScoped<IMailConfigManager, MailConfigManager>();
            builder.Services.AddScoped<ICampaignManager, CampaignManager>();
            builder.Services.AddScoped<ICustomFieldsManager, CustomFieldManager>();
            builder.Services.AddScoped<ISubscriberManager, SubscriberManager>();
            builder.Services.AddScoped<ISubscriberGroupManager,  SubscriberGroupManager>();
            builder.Services.AddScoped<ITemplateManager, TemplateManager>();
            builder.Services.AddScoped<ISubscriberCustomFieldValueManager, SubscriberCustomFieldValueManager>();

            // Messaging
            builder.Services.AddScoped<IMessagePublisher, AzureMessagePublisher>();
            builder.Services.AddSingleton<ServiceBusClient>(_ => 
            {
                var connectionString = builder.Configuration.GetConnectionString("AzureServiceBus");
                return new ServiceBusClient(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
