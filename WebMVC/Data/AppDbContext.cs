﻿using System;
using Microsoft.EntityFrameworkCore;

namespace WebMVC.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
	}
}

