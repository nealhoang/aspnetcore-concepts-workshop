﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lab2
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("", context => context.Response.WriteAsync("Hello from Routing!"));
            routeBuilder.MapGet("sub", context => context.Response.WriteAsync("Hello from sub!"));
            //routeBuilder.MapGet("item/{itemName}", context => context.Response.WriteAsync($"Item: {context.GetRouteValue("itemName")}"));
            routeBuilder.MapGet("item/{id:int}", context => context.Response.WriteAsync($"Item ID: {context.GetRouteValue("id")}"));

            app.UseRouter(routeBuilder.Build());
        }
    }
}
