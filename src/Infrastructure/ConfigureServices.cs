﻿using LogOT.Application.Common.Interfaces;
using LogOT.Infrastructure.Files;
using LogOT.Infrastructure.Identity;
using LogOT.Infrastructure.Persistence;
using LogOT.Infrastructure.Persistence.Interceptors;
using LogOT.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("LogOTDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
<<<<<<< HEAD
        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
=======

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

>>>>>>> 6daade1845861cefb9da2c3966be2b3b18f4595a
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        services.AddAuthentication(o =>
        {
            o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ReturnUrlParameter = "redirectUrl";
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.SameSite = SameSiteMode.Lax;
            })
            ;

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                //.AddAuthenticationSchemes(
                //    CookieAuthenticationDefaults.AuthenticationScheme‌​,
                //    GoogleDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy("admin", policy => policy
                .Combine(options.DefaultPolicy)
                .RequireRole("Administrator")
                .Build());
            options.AddPolicy("member", policy => policy
                .Combine(options.DefaultPolicy)
                .RequireRole("Member")
                .Build());

        });

        return services;
    }
}