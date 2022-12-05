using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace IdentityClient
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
            services.AddControllersWithViews();
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";
            })
                    .AddCookie("Cookie")
                    .AddOpenIdConnect("oidc", config =>
                    {
						config.Authority = "https://localhost:5002/";
						//config.Authority = "http://192.168.1.11:5004/";

						config.RequireHttpsMetadata = false;
                        config.ClientId = "mvc_client";
                        config.ClientSecret = "mvc_client_secret";
                        config.SaveTokens = true; // persist tokens in the cookie
                        config.ResponseType = "code";
						//ssl

						config.BackchannelHttpHandler = new HttpClientHandler
						{
							SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13,
							ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
						};
						//end ssl

					});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
           // app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
