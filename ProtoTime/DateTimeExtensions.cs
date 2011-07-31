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
			
			monthIntToMonthName.Add(1, "Jan");
			monthIntToMonthName.Add(2, "Feb");
			monthIntToMonthName.Add(3, "Mar");
			monthIntToMonthName.Add(4, "Apr");
			monthIntToMonthName.Add(5, "May");
			monthIntToMonthName.Add(6, "Jun");
			monthIntToMonthName.Add(7, "Jul");
			monthIntToMonthName.Add(8, "Aug");
			monthIntToMonthName.Add(9, "Sep");
			monthIntToMonthName.Add(10, "Okt");
			monthIntToMonthName.Add(11, "Nov");
			monthIntToMonthName.Add(12, "Dec");
			
			if(monthIntToMonthName.ContainsKey(dateTime.Month))
			   return monthIntToMonthName[dateTime.Month];
			   
			   return string.Empty;
		}
	}
}

