using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestApp.Middleware;

namespace TestApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // The IConfiguration object is now injected by default by the framework
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // add framework services
            services.AddMvc();

            // add app services

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /* **********************************************************
             * We can configure the http pipeline using Use, Run and Map.
             * Use - can short-circuit the pipeline if it does not call a "next" request delegate.
             * Run - terminates the pipeline
             * Map - branches the pipeline
             * MapWhen - branches the pipeline based on the result of the given predicate
             ***********************************************************/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // todo
                app.UseExceptionHandler("/Error/Error");
            }

            //app.UseAuthentication(); // todo: determine if we need this

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            // custom middleware
            app.UseRequestCulture();

            // the first app.Run delegate terminates the request pipeline
            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}
