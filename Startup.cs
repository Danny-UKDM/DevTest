﻿using AutoMapper;
using DevTest.Models;
using DevTest.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace DevTest
{
	public class Startup
	{
		private readonly IConfigurationRoot _config;

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json");

			_config = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging();

			services.AddSingleton(_config);

			services.AddDbContext<MemberContext>();
			services.AddScoped<IMemberRepository, MemberRepository>();

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				factory.AddDebug(LogLevel.Information);
			}
			else
			{
				factory.AddDebug(LogLevel.Error);
			}

			Mapper.Initialize(config =>
			{
				config.CreateMap<MemberViewModel, Member>().ReverseMap();
			});

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
