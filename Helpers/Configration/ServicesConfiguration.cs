using Data;
using Data.Entity;
using Data.Repository;
using Domain.Manager;
using Domain.Model;
using Helpers.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Configration
{
    public static class ServicesConfiguration
    {

        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
           
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddSingleton<IAdminRepository, AdminRepository>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            
        

            return services;
        }



        public static IServiceCollection ConfigureManager(this IServiceCollection services)
        {
            services.AddScoped<IAdminManager, AdminManager>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IRentalManager, RentalManager>();
            services.AddScoped<ISalesManager, SalesManager>();

            services.AddTransient<LibraryHelper>();
            return services;
        }

        public static IServiceCollection ConfigureHelper(this IServiceCollection services)
        {
            services.AddScoped<ILibraryHelper,LibraryHelper>();
            return services;
        }

        public static IServiceCollection ConfigureMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookModel, BookEntity>().ReverseMap();
                cfg.CreateMap<AdminModel, AdminEntity>().ReverseMap();
                cfg.CreateMap<CustomerModel, CustomerEntity>().ReverseMap();
                cfg.CreateMap<SalesModel, SalesEntity>().ReverseMap();
                cfg.CreateMap<RentalModel, RentalEntity>().ReverseMap();

            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        public static IServiceCollection AddConnection(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("SQLConnection");
            
            //var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            //optionsBuilder.EnableSensitiveDataLogging(true);
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            services.AddDbContext<LibraryContext>(options =>
                                                     options.UseSqlServer(connection, b => b.MigrationsAssembly("Data")),ServiceLifetime.Transient);

            return services;
        }
    }
}
