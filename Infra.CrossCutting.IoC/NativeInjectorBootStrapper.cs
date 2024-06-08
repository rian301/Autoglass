using AutoMapper;
using Autoglass.Application;
using Autoglass.Application.Implement;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Interfaces;
using Autoglass.Infra.CrossCutting.Identity;
using Autoglass.Infra.CrossCutting.Identity.Extensions;
using Autoglass.Infra.CrossCutting.Identity.Models;
using Autoglass.Repository;
using Autoglass.Repository.Implement;
using Autoglass.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Autoglass.Infra.CrossCutting.Identity.Services;
using Autoglass.Infra.Data.UoW;
using Autoglass.Service.Implement;
using Autoglass.Service;
using Autoglass.Service.Implement;
using Autoglass.Repository;
using Repository.Implement;
using Repository;

namespace Autoglass.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IPasswordHasher<User>, IdentityPasswordHasher<User>>();

            //Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Application
            services.AddScoped<IProdutoApp, ProdutoApp>();
            services.AddScoped<ILogErroApp, LogErroApp>();
            //services.AddScoped<IUserApp, UserApp>();

            //Services            
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ILogErroService, LogErroService>();
            //services.AddScoped<IUserService, UserService>();

            //Repository            
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ILogErroRepository, LogErroRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();

            //Others
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUser, WebUserContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
