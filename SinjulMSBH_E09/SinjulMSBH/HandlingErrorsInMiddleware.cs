using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

namespace SinjulMSBH_E09.SinjulMSBH
{
	//public virtual ActionResult Error ( int? statusCode = null )
	//{
	//	if ( statusCode.HasValue )
	//	{
	//		if ( statusCode == 404 || statusCode == 500 )
	//		{
	//			var viewName = statusCode.ToString();
	//			return View( viewName );
	//		}
	//	}
	//	return View( );
	//}

	//public class ExceptionFilterMe: ExceptionFilterAttribute
	//{
	//	public override void OnException ( ExceptionContext context )
	//	{
	//		Exception ex = context.Exception;
	//		var exception = _exceptionHandler.HandleException(ex, context.HttpContext.Request);
	//		int status;
	//		if ( exception != null )
	//		{
	//			status = exception.StatusCode ?? StatusCodes.Status500InternalServerError;
	//			if ( exception.ClientData != null )
	//			{
	//				context.Result = new JsonResult( exception.ClientData ) { StatusCode = status };
	//			}
	//			else
	//			{
	//				context.Result = new ObjectResult( exception ) { StatusCode = status };
	//			}
	//		}
	//	}
	//}

	//public class ExceptionHandlingMiddleware
	//{
	//	private readonly RequestDelegate _next;

	//	public ExceptionHandlingMiddleware ( RequestDelegate next )
	//	{
	//		_next = next;
	//	}

	//	public async Task Invoke ( HttpContext context )
	//	{
	//		try
	//		{
	//			await _next.Invoke( context );
	//			// SKIPPED. The same as before
	//		}
	//		catch ( Exception ex )
	//		{
	//			var resultException = _handler.HandleException(ex, context.Request);
	//			if ( context.Request.IsApiCall( ) )
	//			{
	//				IActionResult result;
	//				if ( resultException?.ClientData != null )
	//				{
	//					result = new JsonResult( resultException.ClientData ) { StatusCode = context.Response.StatusCode };
	//				}
	//				else
	//				{
	//					result = new ObjectResult( resultException ) { StatusCode = context.Response.StatusCode };
	//				}
	//				// now we have a IActionResult, let's return it
	//				RouteData routeData = context.GetRouteData();
	//				ActionDescriptor actionDescriptor = new ActionDescriptor();
	//				ActionContext actionContext = new ActionContext(context, routeData, actionDescriptor);
	//				await result.ExecuteResultAsync( actionContext );
	//				return;
	//			}
	//			throw;
	//		}
	//	}
	//}

	//public static class ExtMe
	//{
	//	public static bool IsApiCall ( this HttpRequest request )
	//	{
	//		bool isJson = request.GetTypedHeaders().Accept.Contains(new MediaTypeHeaderValue("application/json"));
	//		if ( isJson )
	//			return true;
	//		if ( request.Path.Value.StartsWith( "/api/" ) )
	//			return true;
	//		return false;
	//	}

	//	public static IApplicationBuilder UseExceptionHandling ( this IApplicationBuilder app )
	//	{
	//		if ( app == null )
	//			throw new ArgumentNullException( nameof( app ) );
	//		return app.UseMiddleware<ExceptionHandlingMiddleware>( );
	//	}
	//}

	//public class ExceptionHandlingMiddleware
	//{
	//	public async Task Invoke ( HttpContext context )
	//	{
	//		try
	//		{
	//			await _next.Invoke( context );
	//			if ( context.Response.StatusCode == StatusCodes.Status404NotFound )
	//			{
	//				var statusCodeFeature = context.Features.Get<IStatusCodePagesFeature>();
	//				if ( statusCodeFeature == null || !statusCodeFeature.Enabled )
	//				{
	//					// there's no StatusCodePagesMiddleware in app
	//					if ( !context.Response.HasStarted )
	//					{
	//						//var view = new ErrorPage(new ErrorPageModel());
	//						//await view.ExecuteAsync( context );
	//					}
	//				}
	//			}
	//		}
	//		catch ( Exception ex )
	//		{
	//			// TODO: stay tuned
	//			throw;
	//		}
	//	}

	//	public static IApplicationBuilder UseExceptionHandling ( this IApplicationBuilder app )
	//	{
	//		if ( app == null )
	//			throw new ArgumentNullException( nameof( app ) );
	//		return app.UseMiddleware<ExceptionHandlingMiddleware>( );
	//	}
	//}
}