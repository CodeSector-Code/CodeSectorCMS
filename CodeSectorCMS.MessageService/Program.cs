using Azure.Messaging.ServiceBus;
using CodeSectorCMS.Domain;
using CodeSectorCMS.Domain.Managers.Implementations;
using CodeSectorCMS.Domain.Managers.Interfaces;
using CodeSectorCMS.Domain.Repositories;
using CodeSectorCMS.MessageService.Services;
using Microsoft.EntityFrameworkCore;
using System;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace CodeSectorCMS.MessageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContextPool<CmsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CMSContext")));

            //Services
            builder.Services.AddScoped<UnityOfWork>();
            builder.Services.AddScoped<IMailConfigManager, MailConfigManager>();
            builder.Services.AddScoped<IMessageManager, MessageManager>();

            // Mail Service
            builder.Services.AddScoped<IMailService, MailService>();

            // Messaging
            builder.Services.AddSingleton<ServiceBusClient>(_ =>
            {
                var connectionString = builder.Configuration.GetConnectionString("AzureServiceBus");
                return new ServiceBusClient(connectionString);
            });
            builder.Services.AddHostedService<ProcessMailMessages>();

            // Add Controllers
            builder.Services.AddControllers();
            // Add services to the container.
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
