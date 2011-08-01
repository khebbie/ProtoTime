using System;
using System.Collections.Generic;

namespace ProtoTime
{
	public static class DateTimeExtensions
	{
		public static string FormatLike (this DateTime dateTime)
		{
			return dateTime.ToString();
		}
		
		public static string FormatLike (this DateTime dateTime, string example)
		{
			var monthIntToMonthName = new Dictionary<int, string>();
			
			return dateTime.ToString("MMM");
			   
		}
	}
}

