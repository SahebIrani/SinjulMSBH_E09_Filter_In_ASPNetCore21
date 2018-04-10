using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Serilog;
using SinjulMSBH_E09.Data;
using SinjulMSBH_E09.Models;
using SinjulMSBH_E09.SinjulMSBH;

namespace SinjulMSBH_E09.Controllers
{
	public class SinjulMSBHDBO
	{
		public string Id { get; set; } = Guid.NewGuid( ).ToString( );

		public string Controller { get; set; }
		public string Action { get; set; }
		public string Page { get; set; }
		public string Area { get; set; }
		public string Message { get; set; }
		public string StackTrace { get; set; }

		public string Host { get; set; }
		public string IsHTTPS { get; set; }
		public string Method { get; set; }
		public string Path { get; set; }
		public string Scheme { get; set; }
		public string Protocol { get; set; }
		public string UserAgent { get; set; }
		public string AcceptLanguage { get; set; }
	}

	public class SinjulMSBHViewModels
	{
		public string Id { get; set; } = Guid.NewGuid( ).ToString( );
		public string Controller { get; set; }
		public string Action { get; set; }
		public string Page { get; set; }
		public string Area { get; set; }
		public string Message { get; set; }
		public string StackTrace { get; set; }

		public string Host { get; set; }
		public string IsHTTPS { get; set; }
		public string Method { get; set; }
		public string Path { get; set; }
		public string Scheme { get; set; }
		public string Protocol { get; set; }
		public string UserAgent { get; set; }
		public string AcceptLanguage { get; set; }
	}

	public class ShowErrorPageTypeAttribute: TypeFilterAttribute
	{
		public ShowErrorPageTypeAttribute ( ) : base( typeof( SinjulMSBHExceptionFilterAttribute ) )
		{
		}
	}

	public class SinjulMSBHExceptionFilterAttribute: ExceptionFilterAttribute
	{
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IModelMetadataProvider _modelMetadataProvider;
		private ILogger<ApiExceptionFilter> _Logger;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly ApplicationDbContext _context;

		public SinjulMSBHExceptionFilterAttribute (
		    IHostingEnvironment hostingEnvironment ,
		    IModelMetadataProvider modelMetadataProvider , ILogger<ApiExceptionFilter> Logger , IHttpContextAccessor contextAccessor , ApplicationDbContext context )
		{
			_hostingEnvironment = hostingEnvironment;
			_modelMetadataProvider = modelMetadataProvider;
			_Logger=Logger;
			_contextAccessor=contextAccessor;
			_context=context;
		}

		public string ViewName { get; set; } = "SinjulMSBHError";
		public Type ExceptionType { get; set; } = null;

		public override void OnException ( ExceptionContext context )
		{
			if ( !_hostingEnvironment.IsDevelopment( ) )
			{
				// do nothing
				return;
			}

			//var cc= context.RouteData.Values["Controller"];
			//var aa= context.RouteData.Values["Action"];
			//var ControllerName = nameof( HomeController );
			//var ActionName = nameof( HomeController.Index );

			var RouteValues = context.ActionDescriptor.RouteValues;
			var ControllerName =  RouteValues["Controller"];
			var ActionName =  RouteValues["Action"];
			var PageName =  RouteValues["Page"];
			var AreaName =  RouteValues["Area"];

			var exception = context.Exception;
			var request = context.HttpContext.Request;
			string StackTrace = context.Exception.StackTrace;
			string message = string.Empty;
			if ( exception.InnerException != null ) message = exception.InnerException.Message;
			else message = exception.Message;
			// log exception here....
			_Logger.LogWarning( $"SinjulMSBH , Application thrown error: {message}" );

			if ( this.ExceptionType != null )
			{
				if ( context.Exception.GetType( ) == this.ExceptionType )
				{
					var result = new ViewResult { ViewName = this.ViewName };
					result.ViewData = new ViewDataDictionary( _modelMetadataProvider , context.ModelState );

					var resultMe = new SinjulMSBHViewModels
					{
						Controller = ControllerName ,
						Action = ActionName,
						Page=PageName,
						Area=AreaName,
						UserAgent= request.Headers["User-Agent"],
						AcceptLanguage = request.Headers["Accept-Language"],
						Host=request.Host.ToString(),
						IsHTTPS=request.IsHttps.ToString(),
						Method=request.Method,
						Path=request.Path,
						Protocol=request.Protocol,
						Scheme=request.Scheme,
						Message=message,
						StackTrace=StackTrace
					};

					_context.SinjulMSBHDBO.Add(
						new SinjulMSBHDBO( )
						{
							Controller = ControllerName ,
							Action = ActionName ,
							Page=PageName ,
							Area=AreaName ,
							UserAgent= request.Headers[ "User-Agent" ] ,
							AcceptLanguage = request.Headers[ "Accept-Language" ] ,
							Host=request.Host.ToString( ) ,
							IsHTTPS=request.IsHttps.ToString( ) ,
							Method=request.Method ,
							Path=request.Path ,
							Protocol=request.Protocol ,
							Scheme=request.Scheme ,
							Message=message ,
							StackTrace=StackTrace
						}
					);
					//_context.Add( resultMe );
					_context.SaveChanges( );

					result.ViewData.Add( "HandleException" , /*context.Exception*/ resultMe );
					Log.Logger.Error( resultMe.Message , resultMe.StackTrace );
					_Logger.LogError( new EventId( 0 ) , resultMe.Message , resultMe.StackTrace );

					// TODO: Pass additional detailed data via ViewData
					context.Result = result;
					context.ExceptionHandled = true;
				}
			}
			else
			{
				var result = new ViewResult {ViewName = "SinjulMSBHError"};
				result.ViewData = new ViewDataDictionary( _modelMetadataProvider , context.ModelState );

				var resultMe = new SinjulMSBHViewModels
				{
					Controller = ControllerName ,
					Action = ActionName,
					Page=PageName,
					Area=AreaName,
					UserAgent= request.Headers["User-Agent"],
					AcceptLanguage = request.Headers["Accept-Language"],
					Host=request.Host.ToString(),
					IsHTTPS=request.IsHttps.ToString(),
					Method=request.Method,
					Path=request.Path,
					Protocol=request.Protocol,
					Scheme=request.Scheme,
					Message=message,
					StackTrace=StackTrace
				};
				_context.SinjulMSBHDBO.Add(
					new SinjulMSBHDBO( )
					{
						Controller = ControllerName ,
						Action = ActionName ,
						Page=PageName ,
						Area=AreaName ,
						UserAgent= request.Headers[ "User-Agent" ] ,
						AcceptLanguage = request.Headers[ "Accept-Language" ] ,
						Host=request.Host.ToString( ) ,
						IsHTTPS=request.IsHttps.ToString( ) ,
						Method=request.Method ,
						Path=request.Path ,
						Protocol=request.Protocol ,
						Scheme=request.Scheme ,
						Message=message ,
						StackTrace=StackTrace
					}

				);
				//_context.Add( resultMe );
				_context.SaveChanges( );

				result.ViewData.Add( "HandleException" , /*context.Exception*/ resultMe );
				Log.Logger.Error( resultMe.Message , resultMe.StackTrace );
				_Logger.LogError( new EventId( 0 ) , resultMe.Message , resultMe.StackTrace );

				// TODO: Pass additional detailed data via ViewData
				context.Result = result;
				context.ExceptionHandled = true;
			}
		}
	}

	[ShowErrorPageType] // نیاز به استفاده از TypeFDilter نیست
	public class HomeController: Controller
	{
		//[TypeFilter( typeof( SinjulMSBHExceptionFilterAttribute ) /*, Order = 1*/ )]
		public IActionResult Index ( )
		{
			//Ex01
			throw new Exception( "SinjulMSBH Exception Message .. !!!!" );

			return View( );
		}

		public IActionResult SinjulMSBHError ( ) => View( );

		public IActionResult About ( )
		{
			//Ex02
			int i = Convert.ToInt32("");

			ViewData[ "Message" ] = "Your application description page.";
			return View( );
		}

		public IActionResult Contact ( )
		{
			//Ex03
			string[] arrRetValues = null;
			if ( arrRetValues.Length > 0 ) { }

			ViewData[ "Message" ] = "Your contact page.";
			return View( );
		}

		public IActionResult Privacy ( )
		{
			return View( );
		}

		[ResponseCache( Duration = 0 , Location = ResponseCacheLocation.None , NoStore = true )]
		public IActionResult Error ( )
		{
			return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
		}
	}
}