using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DB
{
	public class AppDbContext : DbContext
	{
		public DbSet<AccountDb> Accounts { get; set; }
		public DbSet<PlayerDb> Players { get; set; }
		public DbSet<ItemDb> Items { get; set; }

		static readonly ILoggerFactory _logger = LoggerFactory.Create(builder => { builder.AddConsole(); });

		//string _connectionString = @"Server=base2.jdj.kr;Database=GameDB;User Id=sa;Password=mandlJa**18;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		string _connectionString = @"Server=127.0.0.1;Database=gamedb;Uid=root;Pwd=;";

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options
				.UseLoggerFactory(_logger)
				//.UseSqlServer(ConfigManager.Config == null ? _connectionString : ConfigManager.Config.connectionString);
				.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<AccountDb>()
				.HasIndex(a => a.AccountName)
				.IsUnique();

			builder.Entity<PlayerDb>()
				.HasIndex(p => p.PlayerName)
				.IsUnique();
		}
	}
}
