using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountServer.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedDB;

namespace AccountServer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		//string _connectionString = @"Server=base2.jdj.kr;Database=GameDB;User Id=sa;Password=mandlJa**18;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		string _accountConnectionString = @"Server=127.0.0.1;Database=account;Uid=root;Pwd=;";
		string _shareddbConnectionString = @"Server=127.0.0.1;Database=shareddb;Uid=root;Pwd=;";
		
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.PropertyNamingPolicy = null;
				options.JsonSerializerOptions.DictionaryKeyPolicy = null;
			});

			services.AddDbContext<AppDbContext>(options =>
				//options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			    options.UseMySql(_accountConnectionString, ServerVersion.AutoDetect(_accountConnectionString)));


			services.AddDbContext<SharedDbContext>(options =>
				//options.UseSqlServer(Configuration.GetConnectionString("SharedConnection")));
			    options.UseMySql(_shareddbConnectionString, ServerVersion.AutoDetect(_shareddbConnectionString)));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
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
