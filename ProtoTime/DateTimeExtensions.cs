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
			//var MONTHNAMES_REGEXP      = string.Join("|", DateTimeFormatInfo.CurrentInfo.MonthNames);
		var real_ABBR_MONTHNAMES_REGEXP = DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames.Where(x => x != String.Empty).ToArray();
    var ABBR_MONTHNAMES_REGEXP = string.Join("|", real_ABBR_MONTHNAMES_REGEXP);
			
    //var DAYNAMES_REGEXP        = string.Join("|", DateTimeFormatInfo.CurrentInfo.DayNames);
    //var ABBR_DAYNAMES_REGEXP   = string.Join("|", DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames);
			
	//		var ONE_DIGIT_REGEXP = "\\d{1}";
			var TWO_DIGIT_REGEXP = "\\d{2}";
			var FOUR_DIGIT_REGEXP = "\\d{4}";
			
			Regex rgx1 = new Regex(ABBR_MONTHNAMES_REGEXP);
      		example = rgx1.Replace(example, "MMM");
			
			Regex rgx3 = new Regex(FOUR_DIGIT_REGEXP);
      		example = rgx3.Replace(example, "yyy");
			
			Regex rgx2 = new Regex(TWO_DIGIT_REGEXP);
      		example = rgx2.Replace(example, "dd");
			
			
		
			return dateTime.ToString (example);
			
		}
	}
}

