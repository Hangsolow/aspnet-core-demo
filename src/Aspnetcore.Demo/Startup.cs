﻿using Aspnetcore.Demo.Options;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspnetcore.Demo
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
            services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
            });
            services.AddOptions();
            services.Configure<ApiOptions>(Configuration);
            services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://www.almbrand.dk"), "ping abdk readiness", tags: new[] { "readiness" })
                .AddUrlGroup(new Uri("https://www.almbrand.dk"), "ping abdk liveness", tags: new[] { "liveness" });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            ConfigureHealthChecks();
            //app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseMvc();

            void ConfigureHealthChecks()
            {
                app.UseHealthChecks("/health");
                app.UseHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = hc => hc.Tags.Any(t => t.Equals("liveness", StringComparison.OrdinalIgnoreCase)),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                app.UseHealthChecks("/readiness", new HealthCheckOptions
                {
                    Predicate = hc => hc.Tags.Any(t => t.Equals("readiness", StringComparison.OrdinalIgnoreCase)),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            }
        }
    }
}
