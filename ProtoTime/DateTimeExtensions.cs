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
			
			if (Regex.Matches(example, regexForMonth).Count > 0) {
				return dateTime.ToString ("MMM");
			}
			return dateTime.ToString("MMM dd");
			
		}
	}
}

