using BlazorTable;
using Discord.OAuth2;
using Lost.DataAccess.Entities;
using Lost.SharedLib;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using System;
using System.Linq;
using System.Net.Http;

namespace Lost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = DiscordDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddDiscord(x =>
            {
                x.AppId = Configuration["Discord:AppId"];
                x.AppSecret = Configuration["Discord:AppSecret"];

                x.SaveTokens = true;
            });

            services.AddBlazorTable();

            services.AddScoped<NotifierTitleService>();

            services.AddTransient<IGroupeService, GroupeService>();
            services.AddSingleton<GroupeService>();

            services.AddTransient<IUtilisateurService, UtilisateurService>();
            services.AddSingleton<UtilisateurService>();

            services.AddTransient<IPersonneService, PersonneService>();
            services.AddSingleton<PersonneService>();

            services.AddTransient<IAchatVenteService, AchatVenteService>();
            services.AddSingleton<AchatVenteService>();
            
            services.AddTransient<IBilletService, BilletService>();
            services.AddSingleton<BilletService>();

            services.AddTransient<ISacService, SacService>();
            services.AddSingleton<SacService>();

            services.AddTransient<IVoitureService, VoitureService>();
            services.AddSingleton<VoitureService>();

            services.AddTransient<IVoiturePersonneService, VoiturePersonneService>();
            services.AddSingleton<VoiturePersonneService>();

            services.AddTransient<ISemaineService, SemaineService>();
            services.AddSingleton<SemaineService>();

            services.AddTransient<IStatistiqueGroupeService, StatistiqueGroupeService>();
            services.AddSingleton<StatistiqueGroupeService>();

            services.AddTransient<IStatistiquePersonneService, StatistiquePersonneService>();
            services.AddSingleton<StatistiquePersonneService>();

            services.AddTransient<ICompteClubTransactionService, CompteClubTransactionService>();
            services.AddSingleton<CompteClubTransactionService>();

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();

            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped<HttpClient>(s =>
                {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var uriHelper = s.GetRequiredService<NavigationManager>();
                    return new HttpClient
                    {
                        BaseAddress = new Uri(uriHelper.BaseUri)
                    };
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
