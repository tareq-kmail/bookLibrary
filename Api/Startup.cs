using Data;
using Data.Repository;
using Domain.Manager;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrmEarlyBound;
namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
            //Repository
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            //Manager
            services.AddScoped<IAdminManager, AdminManager>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IRentalManager, RentalManager>();
            services.AddScoped<ISalesManager, SalesManager>();
            //Mapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookModel, new_book>()
                .ForMember(dest => dest.new_price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.new_quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForMember(dest => dest.new_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.new_isbn, opt => opt.MapFrom(dest => dest.Isbn))
                .ReverseMap();
                cfg.CreateMap<AdminModel, new_admin>()
                .ForMember(dest => dest.new_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.new_adminId, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(dest => dest.new_address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.new_nationalId, opt => opt.MapFrom(dest => dest.Nationalid))
                .ForMember(dest => dest.new_username, opt => opt.MapFrom(dest => dest.Username))
                .ForMember(dest => dest.new_phone, opt => opt.MapFrom(dest => dest.Phone))
                .ForMember(dest => dest.new_password, opt => opt.MapFrom(dest => dest.Password))
                .ReverseMap();
                cfg.CreateMap<CustomerModel, new_customer>()
                .ForMember(dest => dest.new_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.new_customerId, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(dest => dest.new_city, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.new_nationalId, opt => opt.MapFrom(dest => dest.Nationalid))
                .ForMember(dest => dest.new_phone, opt => opt.MapFrom(dest => dest.Phone))
                .ReverseMap();
                cfg.CreateMap<SalesModel, new_sales>()
                .ForMember(dest => dest.new_salesId, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(dest => dest.new_price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.new_quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForMember(dest => dest.new_bookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.new_customerId, opt => opt.MapFrom(dest => dest.CustomerId))
                .ForMember(dest => dest.new_date, opt => opt.MapFrom(dest => dest.Date))
                .ReverseMap();
                cfg.CreateMap<RentalModel,new_rental>()
                .ForMember(dest => dest.new_rentalId, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(dest => dest.new_price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.new_bookingExpiryDate, opt => opt.MapFrom(dest => dest.BookingExpiryDate))
                .ForMember(dest => dest.new_bookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.new_customerId, opt => opt.MapFrom(dest => dest.CustomerId))
                .ForMember(dest => dest.new_bookingDate, opt => opt.MapFrom(dest => dest.BookingDate))
                .ReverseMap();

            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            //connection
            string connection = Configuration.GetConnectionString("SQLConnection");

            
            
            services.AddSingleton<ServiceClient>(new ServiceClient(Configuration.GetConnectionString("CRMConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
