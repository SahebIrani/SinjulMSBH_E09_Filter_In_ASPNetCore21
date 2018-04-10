using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SinjulMSBH_E09.SinjulMSBH
{
	public class MicrosoftExceptionFilterAttribute: ExceptionFilterAttribute
	{
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IModelMetadataProvider _modelMetadataProvider;

		public MicrosoftExceptionFilterAttribute (
		    IHostingEnvironment hostingEnvironment ,
		    IModelMetadataProvider modelMetadataProvider )
		{
			_hostingEnvironment = hostingEnvironment;
			_modelMetadataProvider = modelMetadataProvider;
		}

		public override void OnException ( ExceptionContext context )
		{
			if ( !_hostingEnvironment.IsDevelopment( ) )
			{
				// do nothing
				return;
			}
			var result = new ViewResult {ViewName = "SinjulMSBHError"};
			result.ViewData = new ViewDataDictionary( _modelMetadataProvider , context.ModelState );
			result.ViewData.Add( "HandleException" , context.Exception );
			// TODO: Pass additional detailed data via ViewData
			context.Result = result;
			context.ExceptionHandled = true;
		}
	}
}