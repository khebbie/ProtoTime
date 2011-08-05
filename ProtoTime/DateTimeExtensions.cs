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

		public static string FormatLike (this DateTime dateTime, string exampleFormat)
		{
			//Wow so MS left an empty element at the end - can this be true or am I doing something wrong?
			var MonthNames = RemoveEmptyElements (DateTimeFormatInfo.CurrentInfo.MonthNames);
			var MONTHNAMES_REGEXP = string.Join ("|", MonthNames);
			
			var AbbreviatedMonthNames = RemoveEmptyElements (DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames);
			var ABBR_MONTHNAMES_REGEXP = string.Join ("|", AbbreviatedMonthNames);
			
			//var DAYNAMES_REGEXP        = string.Join("|", DateTimeFormatInfo.CurrentInfo.DayNames);
			//var ABBR_DAYNAMES_REGEXP   = string.Join("|", DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames);
			
			var ONE_DIGIT_REGEXP = "\\d{1}";
			var TWO_DIGIT_REGEXP = "\\d{2}";
			var FOUR_DIGIT_REGEXP = "\\d{4}";

            exampleFormat = ReplaceWithRegex(FOUR_DIGIT_REGEXP, exampleFormat, "yyy");

		    Match match = Regex.Match(exampleFormat, TWO_DIGIT_REGEXP);
		    int twoDigits = Int32.Parse(match.Value);
		    do
		    {
                if (twoDigits > 31)
                    exampleFormat = exampleFormat.Replace(match.Value, "yy");
                else if (twoDigits < 13)
                    exampleFormat = exampleFormat.Replace(match.Value, "MM");
                else if (twoDigits > 12 && twoDigits <= 31)
                    exampleFormat = exampleFormat.Replace(match.Value, "dd");
		        
		        match = match.NextMatch();
                if(match.Captures.Count > 0)
                    twoDigits = Int32.Parse(match.Value);
		    } while (!string.IsNullOrEmpty(match.Value));
            
		    exampleFormat = ReplaceWithRegex (MONTHNAMES_REGEXP, exampleFormat, "MMMM");
			exampleFormat = ReplaceWithRegex (ABBR_MONTHNAMES_REGEXP, exampleFormat, "MMM");
			
			exampleFormat = ReplaceWithRegex (TWO_DIGIT_REGEXP, exampleFormat, "dd");
			exampleFormat = ReplaceWithRegex (ONE_DIGIT_REGEXP, exampleFormat, "d");
			
			return dateTime.ToString (exampleFormat);
			
		}

		public static string[] RemoveEmptyElements (string[] input)
		{
			return input.Where (x => x != String.Empty).ToArray ();
		}

		public static string ReplaceWithRegex (string regex, string input, string replacement)
		{
			Regex rgx4 = new Regex (regex);
			var output = rgx4.Replace (input, replacement);
			return output;
		}
		
	}
}

