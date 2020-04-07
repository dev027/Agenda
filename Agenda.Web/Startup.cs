// <copyright file="Startup.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Crud;
using Agenda.Service;
using Agenda.Utilities.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Web
{
    /// <summary>
    /// Web application startup routines.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Design",
        "CA1052:Static holder types should be Static or NotInheritable",
        Justification = "Type used by reflection in Program.CreateHostBuilder")]
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            //// InstanceFactory.RegisterTransient<IAgendaService, AgendaService>();
            InstanceFactory.RegisterTransient<IAgendaData, AgendaData>();

            services.AddTransient<IAgendaService, AgendaService>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddRazorPages();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// IWebHostEnvironment env.
        /// </summary>
        /// <param name="app">Application pipeline.</param>
        public static void Configure(
            IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Error/");

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.Use(async (context, next) =>
            {
                Console.WriteLine(@"Before Next");

                await next().ConfigureAwait(false);

                Console.WriteLine(@"After Next");
            });

            // Add static files to the request pipeline
            app.UseStaticFiles();

            // Add the endpoint routing matcher middleware to the request pipeline
            app.UseRouting();

            // Add endpoints to the request pipeline
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller}/{action}",
                    defaults: new { action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "{controller}/{id?}");
            });
        }
    }
}