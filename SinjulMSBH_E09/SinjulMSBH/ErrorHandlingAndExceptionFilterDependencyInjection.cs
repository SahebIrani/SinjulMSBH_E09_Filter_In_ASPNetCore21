using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Serilog;
using Westwind.Utilities;

namespace SinjulMSBH_E09.SinjulMSBH
{
	public class ApiExceptionFilter: ExceptionFilterAttribute
	{
		private ILogger<ApiExceptionFilter> _Logger;

		public ApiExceptionFilter ( ILogger<ApiExceptionFilter> logger )
		{
			_Logger = logger;
		}

		public override void OnException ( ExceptionContext context )
		{
			ApiError apiError = null;
			if ( context.Exception is ApiException )
			{
				// handle explicit 'known' API errors
				var ex = context.Exception as ApiException;
				context.Exception = null;
				apiError = new ApiError( ex.Message );
				apiError.errors = ex.Errors;

				context.HttpContext.Response.StatusCode = ex.StatusCode;

				_Logger.LogWarning( $"SinjulMSBH , Application thrown error: {ex.Message}" , ex );
			}
			else if ( context.Exception is UnauthorizedAccessException )
			{
				apiError = new ApiError( "Unauthorized Access" );
				context.HttpContext.Response.StatusCode = 401;

				// handle logging here
				Log.Logger.Error( context.Exception , "SinjulMSBH , An unhandled error occurred. Error has been logged .." );
			}
			else
			{
				// Unhandled errors
#if !DEBUG
		    var msg = "An unhandled error occurred.";
		    string stack = null;
#else
				var msg = context.Exception.GetBaseException().Message;
				string stack = context.Exception.StackTrace;
#endif

				apiError = new ApiError( msg );
				apiError.detail = stack;

				context.HttpContext.Response.StatusCode = 500;

				// handle logging here

				_Logger.LogError( new EventId( 0 ) , context.Exception , msg );
			}

			// always return a JSON result
			context.Result = new JsonResult( apiError );
		}
	}

	public class ApiException: Exception
	{
		public int StatusCode { get; set; }

		public ValidationErrorCollection Errors { get; set; }

		public ApiException ( string message ,
					  int statusCode = 500 ,
					  ValidationErrorCollection errors = null ) : base( message )
		{
			StatusCode = statusCode;
			Errors = errors;
		}

		public ApiException ( Exception ex , int statusCode = 500 ) : base( ex.Message )
		{
			StatusCode = statusCode;
		}
	}

	public class ApiError
	{
		public string message { get; set; }
		public bool isError { get; set; }
		public string detail { get; set; }
		public ValidationErrorCollection errors { get; set; }

		public ApiError ( string message )
		{
			this.message = message;
			isError = true;
		}

		public ApiError ( ModelStateDictionary modelState )
		{
			this.isError = true;
			if ( modelState != null && modelState.Any( m => m.Value.Errors.Count > 0 ) )
			{
				message = "SinjulMSBH , Please correct the specified errors and try again ..";
				//errors = modelState.SelectMany( m => m.Value.Errors ).ToDictionary( m => m.Key , m => m.ErrorMessage );
				//errors = modelState.SelectMany( m => m.Value.Errors.Select( me => new KeyValuePair<string , string>( m.Key , me.ErrorMessage ) ) );
				//errors = modelState.SelectMany( m => m.Value.Errors.Select( me => new ModelError { FieldName = m.Key , ErrorMessage = me.ErrorMessage } ) );
			}
		}
	}
}