using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SinjulMSBH_E09.SinjulMSBH;

namespace SinjulMSBH_E09.Controllers
{
	[Produces( "application/json" )]
	[Route( "api/[controller]" )]
	public class GlobalExceptionHandlingWebApiController: Controller
	{
		//تنظیمات پیش فرض UseExceptionHandler فایل Startup Configure
		// GET api/values
		[HttpGet]
		//https://localhost:44393/api/GlobalExceptionHandlingWebApi
		public IEnumerable<string> Get ( )
		{
			string[] arrRetValues = null;
			if ( arrRetValues.Length > 0 )
			{ }
			return arrRetValues;
		}

		//سفارشی سازی در فایل GlobalExceptionHandlingWebApi.cs
		// GET api/values/5
		//https://localhost:44393/api/GlobalExceptionHandlingWebApi/13
		[HttpGet( "{id}" )]
		public string Get ( int id )
		{
			string sMessage = "SinjulMSBH test ..";
			if ( sMessage.Length > 0 )
			{
				throw new MyAppException( "SinjulMSBH , My Custom Exception .." );
			}
			return sMessage;
		}
	}

	[ServiceFilter( typeof( ApiExceptionFilter ) , Order = 1 )]
	//[ApiExceptionFilter]
	[Produces( "application/json" )]
	[Route( "api/[controller]" )]
	public class ServiceExceptionHandlingWebApiController: Controller
	{
		[HttpGet]
		public object Get ( )
		{
			throw new InvalidOperationException( "SinjulMSBH , This is an unhandled exception .." );
		}
	}
}