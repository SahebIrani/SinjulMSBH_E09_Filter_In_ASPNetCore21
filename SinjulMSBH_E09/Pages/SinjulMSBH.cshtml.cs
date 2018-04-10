using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinjulMSBH_E09.Controllers;

namespace SinjulMSBH_E09.Pages
{
	public class SinjulMSBHModel: PageModel
	{
		public void OnGet ( )
		{
		}

		[ValidateModelState]
		public IActionResult OnPost ( [FromBody] Product model )
		{
			return Page( );
		}

		public override void OnPageHandlerExecuting ( PageHandlerExecutingContext context )
		{
			if ( !context.ModelState.IsValid )
			{
				context.Result = new BadRequestObjectResult( context.ModelState );
			}
		}
	}

	public class ValidateModelStateAttribute: ActionFilterAttribute
	{
		public override void OnActionExecuting ( ActionExecutingContext context )
		{
			if ( !context.ModelState.IsValid )
			{
				context.Result = new BadRequestObjectResult( context.ModelState );
			}
		}
	}
}