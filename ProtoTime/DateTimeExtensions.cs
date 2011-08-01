using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProtoTime
{
	public static class DateTimeExtensions
	{
		public static string FormatLike (this DateTime dateTime)
		{
			return dateTime.ToString ();
		}

		public static string FormatLike (this DateTime dateTime, string example)
		{
			var regexForMonth = "^[A-Za-z]{3}$";
			var regexForMonthAndDayInMonth =  "^[A-Za-z]{3} \\d{2}$";
			
			if (Regex.Matches(example, regexForMonth).Count > 0) {
				return dateTime.ToString ("MMM");
			}
			
			if (Regex.Matches(example, regexForMonthAndDayInMonth).Count > 0) {
				return dateTime.ToString ("MMM dd");
			}
			
			return dateTime.ToString("MMM dd, yyy");
			
		}
	}
}

