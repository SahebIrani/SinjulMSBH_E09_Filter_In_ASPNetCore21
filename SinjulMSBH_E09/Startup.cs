using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;
using SinjulMSBH_E09.Data;
using SinjulMSBH_E09.SinjulMSBH;

namespace SinjulMSBH_E09
{
	public class Startup
	{
		private object loggerFactory;

		public Startup ( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices ( IServiceCollection services )
		{
			services.AddDbContext<ApplicationDbContext>( _ => _.UseInMemoryDatabase( Guid.NewGuid( ).ToString( ) ) );

			//services.AddDbContext<ApplicationDbContext>(options =>
			//    options.UseSqlServer(
			//    options.UseSqlServer(
			//        Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<IdentityUser , IdentityRole>( options => options.Stores.MaxLengthForKeys = 128 )
			    .AddEntityFrameworkStores<ApplicationDbContext>( )
			    .AddDefaultUI( )
			    .AddDefaultTokenProviders( );

			services.Configure<CookiePolicyOptions>( options =>
			 {
				 // This lambda determines whether user consent for non-essential cookies is needed for a given request.
				 options.CheckConsentNeeded = context => true;
				 options.MinimumSameSitePolicy = SameSiteMode.None;
			 } );

			services.AddSingleton<IHttpContextAccessor , HttpContextAccessor>( );

			services.AddScoped<ApiExceptionFilter>( );

			services.AddMvc( options =>
			{
				options.Filters.Add( typeof( CustomExceptionFilter ) );
				options.Filters.Add( typeof( ApiExceptionFilter ) );

				//options.Filters.Add( typeof( ValidateModelStateAttribute ) );
				//options.Filters.Add( new ValidateModelState2Attribute( ) );
				//options.Filters.Add( new ValidateModelState3Attribute( ) );
				//options.Filters.Add( typeof( HttpGlobalExceptionFilter ) );
			} ).SetCompatibilityVersion( CompatibilityVersion.Version_2_1 )
			.AddJsonOptions( options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver( );
			} ); ;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure ( IApplicationBuilder app , IHostingEnvironment env , ILoggerFactory loggerFactory )
		{
			if ( env.IsDevelopment( ) )
			{
				app.UseBrowserLink( );
				app.UseDeveloperExceptionPage( );
				app.UseDatabaseErrorPage( );

				//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

				// ASP.NET Log Config
				loggerFactory.WithFilter( new FilterLoggerSettings
				 {
					{"Trace",LogLevel.Trace },
					{"Default", LogLevel.Trace},
					{"Microsoft", LogLevel.Warning},
					{"System", LogLevel.Warning}
				 } )
				    .AddConsole( )
				    .AddSerilog( );
			}
			else
			{
				//app.UseExceptionHandler( "/Home/Error" );
				app.UseHsts( );

				//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

				loggerFactory.WithFilter( new FilterLoggerSettings
				  {
					{"Trace",LogLevel.Trace },
					{"Default", LogLevel.Trace},
					{"Microsoft", LogLevel.Warning},
					{"System", LogLevel.Warning}
				  } )
				     .AddSerilog( );

				//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

				//app.UseStatusCodePagesWithReExecute(@“/error”, “?statusCode={ 0 }”);

				app.UseExceptionHandler( options =>
				{
					options.Run(
					async context =>
					{
						context.Response.StatusCode = ( int ) HttpStatusCode.InternalServerError;
						context.Response.ContentType = "text/html";
						var ex = context.Features.Get<IExceptionHandlerFeature>();
						if ( ex != null )
						{
							var err = $"<h1>SinjulMSBH Error: {ex.Error.Message}</h1>{ex.Error.StackTrace } ..";
							await context.Response.WriteAsync( err ).ConfigureAwait( false );
						}
					} );
				}
				);

				//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

				app.UseStatusCodePages( async context =>
				{
					var statusCodePagesFeature = context.HttpContext.Features.Get<IStatusCodePagesFeature>();

					if ( statusCodePagesFeature != null )
					{
						statusCodePagesFeature.Enabled = false;
					}

					context.HttpContext.Response.ContentType = "text/plain";
					await context.HttpContext.Response.WriteAsync( "SinjulMSBH Status code page , status code: " + context.HttpContext.Response.StatusCode );
				} );

				//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

				//app.UseExceptionHandling( );
			}

			// Serilog configuration
			Log.Logger = new LoggerConfiguration( )
				  .WriteTo.RollingFile( pathFormat: "logs\\log-{Date}.log" )
				  .CreateLogger( );

			//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

			app.UseHttpsRedirection( );
			app.UseStaticFiles( );
			app.UseCookiePolicy( );

			app.UseAuthentication( );

			app.UseMvc( routes =>
			 {
				 routes.MapRoute(
			    name: "default" ,
			    template: "{controller=Home}/{action=Index}/{id?}" );
			 } );
		}
	}
}