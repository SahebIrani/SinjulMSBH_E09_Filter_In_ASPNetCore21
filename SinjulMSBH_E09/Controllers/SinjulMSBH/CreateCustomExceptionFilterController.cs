using System;
using Microsoft.AspNetCore.Mvc;
using SinjulMSBH_E09.SinjulMSBH;

namespace SinjulMSBH_E09.Controllers.SinjulMSBH
{
	public class CreateCustomExceptionFilterController: Controller
	{
		[HandleException]
		//[HandleException( ViewName = "SinjulMSBHError" , ExceptionType = typeof( DivideByZeroException ) )]
		public IActionResult Index ( )
		{
			throw new Exception( "SinjulMSBH This is some exception .. !!!!" );
			return View( );
		}

		[TypeFilter( typeof( MicrosoftExceptionFilterAttribute ) , Order = 1 )]
		public IActionResult Index1 ( )
		{
			throw new Exception( "SinjulMSBH This is some exception .. !!!!" );
			return View( "Index" );
		}

		[MyExceptionFilter]
		public IActionResult Index2 ( )
		{
			int i = Convert.ToInt32("");
			return View( "Index" );
		}

		public IActionResult SinjulMSBHError ( )
		{
			return View( );
		}
	}
}