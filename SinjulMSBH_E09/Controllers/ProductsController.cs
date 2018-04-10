using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SinjulMSBH_E09.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace SinjulMSBH_E09.Controllers
{
	#region SinjulMSBHNewVersionAPI

	//[ValidateModel]
	//[ValidateModelState]
	//[ValidateModelState2]
	//[ValidateModelState3]

	[Produces( "application/json" )]
	[Route( "api/[controller]" )]
	[ApiController] // Automatically responding with a 400 when validation errors occur
	public class ProductsNewVersionController: ControllerBase
	{
		private readonly ProductsRepository _repository;

		public ProductsNewVersionController ( ProductsRepository repository )
		{
			_repository = repository;
		}

		[HttpPost, AutoValidateAntiforgeryToken]
		public async Task<IActionResult> SaveMe ( Product model )
		{
			//if ( !ModelState.IsValid )
			//	return view( model );

			// invoke some business logic magic that makes money
			try
			{
				//await _dbContext.SaveChanges( );
			}
			catch ( Exception e )
			{
				//_logger.LogError( "message" , e );
				ModelState.AddModelError( "exception" , "Houston we have a problem:" + e.Message );
				return Ok( model );
			}
			return RedirectToAction( "Read" , new { id = model.Id } );
		}

		[HttpGet]
		public IEnumerable<Product> Get ( )
		{
			return _repository.GetProducts( );
		}

		[HttpPost]
		[ProducesResponseType( 201 )]
		public ActionResult<Product> Post ( Product product )
		{
			_repository.AddProduct( product );
			return CreatedAtAction( nameof( Get ) , new { id = product.Id } , product );
		}
	}

	#endregion SinjulMSBHNewVersionAPI

	#region SinjulMSBHOldVersionAPI

	[Route( "api/[controller]" )]
	public class ProductsOLDVersionController: ControllerBase
	{
		private readonly ProductsRepository _repository;

		public ProductsOLDVersionController ( ProductsRepository repository )
		{
			_repository = repository;
		}

		// GET api/values
		[HttpGet]
		public IEnumerable<Product> Get ( )
		{
			return _repository.GetProducts( );
		}

		//Go To region SinjulMSBH03

		[HttpPost]
		public IActionResult Create ( [FromBody]Product value )
		{
			return Ok( "OK" );
		}

		// POST api/values
		[HttpPost]
		[ProducesResponseType( typeof( Product ) , 201 )]
		[ValidateModelState2]
		public IActionResult Post ( [FromBody] Product product )
		{
			//if ( !ModelState.IsValid )
			//{
			//	return BadRequest( ModelState );
			//}
			product = _repository.AddProduct( product );
			return CreatedAtAction( nameof( Get ) , new { id = product.Id } , product );
		}
	}

	#endregion SinjulMSBHOldVersionAPI

	#region SinjulMSBH00

	public class ProductsRepository
	{
		internal Product AddProduct ( Product product )
		{
			throw new NotImplementedException( );
		}

		internal IEnumerable<Product> GetProducts ( )
		{
			throw new NotImplementedException( );
		}
	}

	public class Product
	{
		public object Id { get; internal set; }
	}

	#endregion SinjulMSBH00

	#region SinjulMSBH01

	public sealed class ValidateModelState2Attribute: ActionFilterAttribute
	{
		public override void OnActionExecuting ( ActionExecutingContext context )
		{
			if ( !context.ModelState.IsValid )
			{
				context.Result = new BadRequestObjectResult( context.ModelState );
			}

			//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

			if ( !context.ModelState.IsValid && context.HttpContext.Request.Path.StartsWithSegments( "/Api" , StringComparison.OrdinalIgnoreCase ) )
			{
				context.Result = new BadRequestObjectResult( context.ModelState );
			}

			//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘
			var modelState = context.ModelState;

			if ( !modelState.IsValid )
				context.Result = new ContentResult( )
				{
					Content = "SinjulMSBH Modelstate not valid .." ,
					StatusCode = int.Parse( HttpStatusCode.BadRequest.ToString( ) ) //400
				};

			//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘
			if ( !context.ModelState.IsValid )
			{
				List<string> list = (from modelState1 in context.ModelState.Values from error in modelState1.Errors select error.ErrorMessage).ToList();
				context.Result = new BadRequestObjectResult( list );
			}

			//◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘

			if ( !context.ModelState.IsValid )
			{
				List<string> list = (from modelState22 in context.ModelState.Values from error in modelState22.Errors select error.ErrorMessage).ToList();

				//Also add exceptions.
				list.AddRange( from modelState22 in context.ModelState.Values from error in modelState22.Errors select error.Exception.ToString( ) );

				context.Result = new BadRequestObjectResult( list );
			}

			base.OnActionExecuting( context );
		}
	}

	#endregion SinjulMSBH01

	#region SinjulMSBH02

	//	{
	//  "message": "Validation Failed",
	//  "errors": [
	//    {
	//      "message": "This is a model-wide error"
	//    },
	//    {
	//      "field": "Url",
	//      "message": "'Url' should not be empty."
	//    }
	//  ]
	//}

	public class ValidationError
	{
		[JsonProperty( NullValueHandling = NullValueHandling.Ignore )]
		public string Field { get; }

		public string Message { get; }

		public ValidationError ( string field , string message )
		{
			Field = field != string.Empty ? field : null;
			Message = message;
		}
	}

	public class ValidationResultModel
	{
		public string Message { get; }

		public List<ValidationError> Errors { get; }

		public ValidationResultModel ( ModelStateDictionary modelState )
		{
			Message = "SinjulMSBH Validation Failed ..";
			Errors = modelState.Keys
				  .SelectMany( key => modelState[ key ].Errors.Select( x => new ValidationError( key , x.ErrorMessage ) ) )
				  .ToList( );
		}
	}

	public class ValidationFailedResult: ObjectResult
	{
		public ValidationFailedResult ( ModelStateDictionary modelState )
		    : base( new ValidationResultModel( modelState ) )
		{
			StatusCode = StatusCodes.Status422UnprocessableEntity;
		}
	}

	public class ValidateModelAttribute: ActionFilterAttribute
	{
		public override void OnActionExecuting ( ActionExecutingContext context )
		{
			if ( !context.ModelState.IsValid )
			{
				context.Result = new ValidationFailedResult( context.ModelState );
			}
		}
	}

	#endregion SinjulMSBH02

	#region SinjulMSBH03

	[AttributeUsage( AttributeTargets.Method , AllowMultiple = false , Inherited = false )]
	public class ValidateModelState3Attribute: ActionFilterAttribute
	{
		public ValidateModelState3Attribute ( )
		{
		}

		public override void OnActionExecuting ( ActionExecutingContext context )
		{
			if ( !context.ModelState.IsValid )
			{
				// When ModelState is not valid, we throw exception
				throw new ValidationException( context.ModelState.GetErrors( ) );
			}
		}
	}

	public class ValidationException: Exception
	{
		public List<string> Errors { get; set; }

		public ValidationException ( )
		{
			Errors = new List<string>( );
		}

		public ValidationException ( List<string> errorDictionary )
		{
			this.Errors = errorDictionary;
		}
	}

	public static class ModelStateExtensions
	{
		public static List<string> GetErrors ( this ModelStateDictionary modelState )
		{
			var validationErrors = new List<string>();

			foreach ( var state in modelState )
			{
				validationErrors.AddRange( state.Value.Errors
				    .Select( error => error.ErrorMessage )
				    .ToList( ) );
			}

			return validationErrors;
		}
	}

	public class AjaxResponse
	{
		public bool IsSuccess { get; set; }

		public string Message { get; set; }

		public object Data { get; set; }

		public bool IsAuthenticationError { get; set; }

		public AjaxResponse ( )
		{
		}
	}

	public class HttpGlobalExceptionFilter: IExceptionFilter
	{
		public HttpGlobalExceptionFilter ( )
		{
		}

		public void OnException ( ExceptionContext context )
		{
			var exception = context.Exception;
			var code = HttpStatusCode.InternalServerError;
			var ajaxResponse = new AjaxResponse
			{
				IsSuccess = false
			};

			if ( exception is ValidationException )
			{
				code = HttpStatusCode.BadRequest;
				ajaxResponse.Message = "Bad request";
				// We can serialize exception message here instead of throwing Bad request message
			}
			else
			{
				ajaxResponse.Message = "Internal Server Error";
			}

			context.Result = new JsonResult( ajaxResponse );
			context.HttpContext.Response.StatusCode = ( int ) code;
			context.ExceptionHandled = true;
		}
	}

	#endregion SinjulMSBH03

	#region SinjulMSBH04

	public class ValidateModel2Attribute: ActionFilterAttribute
	{
		public override void OnActionExecuting ( ActionExecutingContext context )
		{
			var result = new ViewResult();
			if ( !context.ModelState.IsValid )
			{
				// Usually POST action looks like: IActionResult Save(MyMode model)
				// if validation failed we need to forward that model back to view
				// Model is in ActionArgument[0] in this case, but you may tweak
				// this to your specific needs
				if ( context.ActionArguments.Count > 0 )
				{
					SetViewData( context , result );
				}
				// This is same as
				// return View(mnodel);
				context.Result = result;
			}
			else
			{
				try
				{
					// This was all happening BEFORE actione executed.
					// Lets exec the action now and catch possible exceptions!
					//await next.Invoke( );
					context.Result = result;
				}
				// You can catch here app specific exception!
				// catch(MySpecialException e)
				catch ( Exception e )
				{
					// Uh, something went wrong:(

					// Pull logger for IOC container.
					// Note: I don't really like this approach, it would be much nicer to inject
					// interfaces into ctor, but then we wouldn't be able to use this filter like:
					// [ValidateModel] <- ctor requires interface, doesn't compile
					// but like this:
					// [ServiceFilter(typeof(ValidateModelAttribute))]
					// so pick whatever approach you like!
					var loggerFactory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();

					var message = e.Unwrap().Message; // Dig for inner exception message

					var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
					var logger = loggerFactory.CreateLogger(descriptor.ControllerName);

					logger.LogError( $"Error while executing {descriptor.ControllerName}/{descriptor.ActionName}: {message}" );

					// Sets view model, and adds error to ModelState list
					if ( context.ActionArguments.Count > 0 )
					{
						SetViewData( context , result );
						result.ViewData.ModelState.AddModelError( "" , message );
					}
					// Again, return View(model);
					context.Result = result;
				}
			}
		}

		/// <summary>
		/// Sets view model from action executing context
		/// </summary>
		private static void SetViewData ( ActionExecutingContext context , ViewResult result )
		{
			result.ViewData = new ViewDataDictionary(
			    context.HttpContext.RequestServices.GetService<IModelMetadataProvider>( ) ,
			    context.ModelState )
			{
				Model = context.ActionArguments.First( ).Value
			};
		}
	}

	#endregion SinjulMSBH04
}