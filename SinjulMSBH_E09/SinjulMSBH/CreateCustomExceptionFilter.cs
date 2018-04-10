using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SinjulMSBH_E09.SinjulMSBH
{
	public class HandleExceptionAttribute: ExceptionFilterAttribute
	{
		public string ViewName { get; set; } = "SinjulMSBHError";
		public Type ExceptionType { get; set; } = null;

		public override void OnException ( ExceptionContext context )
		{
			if ( this.ExceptionType != null )
			{
				if ( context.Exception.GetType( ) == this.ExceptionType )
				{
					var result = new ViewResult { ViewName = this.ViewName };
					var modelMetadata = new EmptyModelMetadataProvider();
					result.ViewData = new ViewDataDictionary( modelMetadata , context.ModelState );
					result.ViewData.Add( "HandleException" , context.Exception );
					context.Result = result;
					context.ExceptionHandled = true;
				}
			}
			else
			{
				var result = new ViewResult { ViewName = "SinjulMSBHError" };
				var modelMetadata = new EmptyModelMetadataProvider();
				result.ViewData = new ViewDataDictionary( modelMetadata , context.ModelState );
				result.ViewData.Add( "HandleException" , context.Exception );
				context.Result = result;
				context.ExceptionHandled = true;
			}
		}
	}
}