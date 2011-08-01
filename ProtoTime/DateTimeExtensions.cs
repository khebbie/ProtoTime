using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

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
			//Wow so MS left an empty element at the end - can this be true or am I doing something wrong?
			var real_MonthNames = DateTimeFormatInfo.CurrentInfo.MonthNames.Where (x => x != String.Empty).ToArray ();
			var MONTHNAMES_REGEXP = string.Join ("|", real_MonthNames);
			
			var real_AbbreviatedMonthNames = DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames.Where (x => x != String.Empty).ToArray ();
			var ABBR_MONTHNAMES_REGEXP = string.Join ("|", real_AbbreviatedMonthNames);
			
			//var DAYNAMES_REGEXP        = string.Join("|", DateTimeFormatInfo.CurrentInfo.DayNames);
			//var ABBR_DAYNAMES_REGEXP   = string.Join("|", DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames);
			
			var ONE_DIGIT_REGEXP = "\\d{1}";
			var TWO_DIGIT_REGEXP = "\\d{2}";
			var FOUR_DIGIT_REGEXP = "\\d{4}";
			
			example = ReplaceWithRegex (MONTHNAMES_REGEXP, example, "MMMM");
			example = ReplaceWithRegex (ABBR_MONTHNAMES_REGEXP, example, "MMM");
			example = ReplaceWithRegex (FOUR_DIGIT_REGEXP, example, "yyy");
			example = ReplaceWithRegex (TWO_DIGIT_REGEXP, example, "dd");
			example = ReplaceWithRegex (ONE_DIGIT_REGEXP, example, "d");
			
			return dateTime.ToString (example);
			
		}

		public static string ReplaceWithRegex (string regex, string input, string replacement)
		{
			Regex rgx4 = new Regex (regex);
			var output = rgx4.Replace (input, replacement);
			return output;
		}
		
	}
}

