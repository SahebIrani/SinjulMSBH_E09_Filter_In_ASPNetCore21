using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SinjulMSBH_E09.Controllers;

namespace SinjulMSBH_E09.Data
{
	public class ApplicationDbContext: IdentityDbContext
	{
		public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options )
		    : base( options )
		{
		}

		public DbSet<SinjulMSBHDBO> SinjulMSBHDBO { get; set; }
	}
}