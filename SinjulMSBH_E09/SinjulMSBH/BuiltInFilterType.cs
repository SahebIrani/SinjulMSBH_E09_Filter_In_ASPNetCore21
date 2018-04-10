using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SinjulMSBH_E09.SinjulMSBH
{
	public class MyExceptionFilterAttribute: Attribute, IExceptionFilter
	{
		public void OnException ( ExceptionContext context )
		{
			var exception = context.Exception;
			string message = string.Empty;
			if ( exception.InnerException != null )
			{
				message = exception.InnerException.Message;
			}
			else
			{
				message = exception.Message;
			}
			// log exception here....
			var result = new ViewResult { ViewName = "SinjulMSBHError" };
			var modelMetadata = new EmptyModelMetadataProvider();
			result.ViewData = new ViewDataDictionary( modelMetadata , context.ModelState );
			result.ViewData.Add( "Exception" , message );
			context.Result = result;
			context.ExceptionHandled = true;
		}
	}

	public class CustomExceptionFilterAttribute: ExceptionFilterAttribute
	{
		public override async Task OnExceptionAsync ( ExceptionContext context )
		{
			var exception = context.Exception;
			string message = string.Empty;
			if ( exception.InnerException != null )
			{
				message = exception.InnerException.Message;
			}
			else
			{
				message = exception.Message;
			}
			// log exception here....
			var result = new ViewResult { ViewName = "SinjulMSBHError" };
			var modelMetadata = new EmptyModelMetadataProvider();
			result.ViewData = new ViewDataDictionary( modelMetadata , context.ModelState );
			result.ViewData.Add( "Exception" , message );
			context.Result = result;
			context.ExceptionHandled = true;
		}
	}
}