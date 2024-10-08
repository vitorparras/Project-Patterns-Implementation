﻿using API.Middlewares;
using Application.Services;
using Application.Services.Interfaces;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.Repository.Interface;

namespace API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
        {
            builder.Services
                  .AddEndpointsApiExplorer()
                  .AddSwaggerGen();

            builder.ConfigureContext();
            builder.ConfigureRepositories();
            builder.ConfigureServices();

            return builder;
        }

        public static void ConfigureMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger()
                   .UseSwaggerUI();
            }
            app.UseMiddleware<JwtTokenValidationMiddleware>();
            app.UseHttpsRedirection();
        }


        public static WebApplicationBuilder ConfigureRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      
            return builder;
        }

        public static WebApplicationBuilder ConfigureContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<MinimalContext>();

            return builder;
        }
        
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();

            return builder;
        }
    }
}
