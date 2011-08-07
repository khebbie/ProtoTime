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
			var monthNames = RemoveEmptyElements (DateTimeFormatInfo.CurrentInfo.MonthNames);
			var monthnamesRegexp = string.Join ("|", monthNames);
			
			var abbreviatedMonthNames = RemoveEmptyElements (DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames);
			var abbrMonthnamesRegexp = string.Join ("|", abbreviatedMonthNames);

            var abbreviatedDayNames = RemoveEmptyElements(DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames);
            var abbreviatedDayNamesRegexp = string.Join("|", abbreviatedDayNames);

            var dayNames = RemoveEmptyElements(DateTimeFormatInfo.CurrentInfo.DayNames);
            var dayNamesRegexp = string.Join("|", dayNames);
			
			const string oneDigitRegexp = "\\d{1}";
			const string twoDigitRegexp = "\\d{2}";
			const string fourDigitRegexp = "\\d{4}";
		    bool monthAlreadyReplaced = false;

            const string dayReplacement = "dddd";
            exampleFormat = ReplaceWithRegex(dayNamesRegexp, exampleFormat, dayReplacement);		    
            
            const string abbrevaitedDayReplacement = "ddd";
		    exampleFormat = ReplaceWithRegex(abbreviatedDayNamesRegexp, exampleFormat, abbrevaitedDayReplacement);
		    const string monthReplacement = "yyy";
		    exampleFormat = ReplaceWithRegex(fourDigitRegexp, exampleFormat, monthReplacement);
            if (exampleFormat.Contains(monthReplacement))
                monthAlreadyReplaced = true;

		    Match match = Regex.Match(exampleFormat, twoDigitRegexp);
            if (match.Captures.Count > 0)
            {
                int twoDigits = Int32.Parse(match.Value);
                do
                {
                    if ((twoDigits > 12 && twoDigits <= 31) && !monthAlreadyReplaced)
                        exampleFormat = exampleFormat.Replace(match.Value, "dd");
                    else if (twoDigits < 13)
                        exampleFormat = exampleFormat.Replace(match.Value, "MM");
                    else if (twoDigits > 31)
                        exampleFormat = exampleFormat.Replace(match.Value, "yy");

                    match = match.NextMatch();
                    if (match.Captures.Count > 0)
                        twoDigits = Int32.Parse(match.Value);
                } while (!string.IsNullOrEmpty(match.Value));
            }
		    exampleFormat = ReplaceWithRegex (monthnamesRegexp, exampleFormat, "MMMM");
			exampleFormat = ReplaceWithRegex (abbrMonthnamesRegexp, exampleFormat, "MMM");
			
			exampleFormat = ReplaceWithRegex (twoDigitRegexp, exampleFormat, "dd");
			exampleFormat = ReplaceWithRegex (oneDigitRegexp, exampleFormat, "d");
			
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

//"d"
//The day of the month, from 1 through 31.
//More information: The "d" Custom Format Specifier.
//6/1/2009 1:45:30 PM -> 1
//6/15/2009 1:45:30 PM -> 15
//"dd"
//The day of the month, from 01 through 31.
//More information: The "dd" Custom Format Specifier.
//6/1/2009 1:45:30 PM -> 01
//6/15/2009 1:45:30 PM -> 15
//"ddd"
//The abbreviated name of the day of the week.
//More information: The "ddd" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> Mon (en-US)
//6/15/2009 1:45:30 PM -> Пн (ru-RU)
//6/15/2009 1:45:30 PM -> lun. (fr-FR)
//"dddd"
//The full name of the day of the week.
//More information: The "dddd" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> Monday (en-US)
//6/15/2009 1:45:30 PM -> понедельник (ru-RU)
//6/15/2009 1:45:30 PM -> lundi (fr-FR)

//"g", "gg"
//The period or era.
//More information: The "g" or "gg" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> A.D.

//"h"
//The hour, using a 12-hour clock from 1 to 12.
//More information: The "h" Custom Format Specifier.
//6/15/2009 1:45:30 AM -> 1
//6/15/2009 1:45:30 PM -> 1
//"hh"
//The hour, using a 12-hour clock from 01 to 12.
//More information: The "hh" Custom Format Specifier.
//6/15/2009 1:45:30 AM -> 01
//6/15/2009 1:45:30 PM -> 01
//"H"
//The hour, using a 24-hour clock from 0 to 23.
//More information: The "H" Custom Format Specifier.
//6/15/2009 1:45:30 AM -> 1
//6/15/2009 1:45:30 PM -> 13
//"HH"
//The hour, using a 24-hour clock from 00 to 23.
//More information: The "HH" Custom Format Specifier.
//6/15/2009 1:45:30 AM -> 01
//6/15/2009 1:45:30 PM -> 13

//"K"
//Time zone information.
//More information: The "K" Custom Format Specifier.
//With DateTime values:
//6/15/2009 1:45:30 PM, Kind Unspecified ->
//6/15/2009 1:45:30 PM, Kind Utc -> Z
//6/15/2009 1:45:30 PM, Kind Local -> -07:00 (depends on local computer settings)
//With DateTimeOffset values:
//6/15/2009 1:45:30 AM -07:00 --> -07:00
//6/15/2009 8:45:30 AM +00:00 --> +00:00

//"m"
//The minute, from 0 through 59.
//More information: The "m" Custom Format Specifier.
//6/15/2009 1:09:30 AM -> 9
//6/15/2009 1:09:30 PM -> 9
//"mm"
//The minute, from 00 through 59.
//More information: The "mm" Custom Format Specifier.
//6/15/2009 1:09:30 AM -> 09
//6/15/2009 1:09:30 PM -> 09

//"M"
//The month, from 1 through 12.
//More information: The "M" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> 6

//"MM"
//The month, from 01 through 12.
//More information: The "MM" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> 06
//"MMM"
//The abbreviated name of the month.
//More information: The "MMM" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> Jun (en-US)
//6/15/2009 1:45:30 PM -> juin (fr-FR)
//6/15/2009 1:45:30 PM -> Jun (zu-ZA)
//"MMMM"
//The full name of the month.
//More information: The "MMMM" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> June (en-US)
//6/15/2009 1:45:30 PM -> juni (da-DK)
//6/15/2009 1:45:30 PM -> uJuni (zu-ZA)

//"s"
//The second, from 0 through 59.
//More information: The "s" Custom Format Specifier.
//6/15/2009 1:45:09 PM -> 9
//"ss"
//The second, from 00 through 59.
//More information: The "ss" Custom Format Specifier.
//6/15/2009 1:45:09 PM -> 09

//"t"
//The first character of the AM/PM designator.
//More information: The "t" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> P (en-US)
//6/15/2009 1:45:30 PM -> 午 (ja-JP)
//6/15/2009 1:45:30 PM -> (fr-FR)
//"tt"
//The AM/PM designator.
//More information: The "tt" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> PM (en-US)
//6/15/2009 1:45:30 PM -> 午後 (ja-JP)
//6/15/2009 1:45:30 PM -> (fr-FR)

//"y"
//The year, from 0 to 99.
//More information: The "y" Custom Format Specifier.
//1/1/0001 12:00:00 AM -> 1
//1/1/0900 12:00:00 AM -> 0
//1/1/1900 12:00:00 AM -> 0
//6/15/2009 1:45:30 PM -> 9
//"yy"
//The year, from 00 to 99.
//More information: The "yy" Custom Format Specifier.
//1/1/0001 12:00:00 AM -> 01
//1/1/0900 12:00:00 AM -> 00
//1/1/1900 12:00:00 AM -> 00
//6/15/2009 1:45:30 PM -> 09
//"yyy"
//The year, with a minimum of three digits.
//More information: The "yyy" Custom Format Specifier.
//1/1/0001 12:00:00 AM -> 001
//1/1/0900 12:00:00 AM -> 900
//1/1/1900 12:00:00 AM -> 1900
//6/15/2009 1:45:30 PM -> 2009
//"yyyy"
//The year as a four-digit number.
//More information: The "yyyy" Custom Format Specifier.
//1/1/0001 12:00:00 AM -> 0001
//1/1/0900 12:00:00 AM -> 0900
//1/1/1900 12:00:00 AM -> 1900
//6/15/2009 1:45:30 PM -> 2009
//"yyyyy"
//The year as a five-digit number.
//More information: The "yyyyy" Custom Format Specifier.
//1/1/0001 12:00:00 AM -> 00001
//6/15/2009 1:45:30 PM -> 02009

//"z"
//Hours offset from UTC, with no leading zeros.
//More information: The "z" Custom Format Specifier.
//6/15/2009 1:45:30 PM -07:00 -> -7
//"zz"
//Hours offset from UTC, with a leading zero for a single-digit value.
//More information: The "zz" Custom Format Specifier.
//6/15/2009 1:45:30 PM -07:00 -> -07
//"zzz"
//Hours and minutes offset from UTC.
//More information: The "zzz" Custom Format Specifier.
//6/15/2009 1:45:30 PM -07:00 -> -07:00

//":"
//The time separator.
//More information: The ":" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> : (en-US)
//6/15/2009 1:45:30 PM -> . (it-IT)
//6/15/2009 1:45:30 PM -> : (ja-JP)

//"/"
//The date separator.
//More Information: The "/" Custom Format Specifier.
//6/15/2009 1:45:30 PM -> / (en-US)
//6/15/2009 1:45:30 PM -> - (ar-DZ)
//6/15/2009 1:45:30 PM -> . (tr-TR)

//"string"
//'string'
//Literal string delimiter.
//6/15/2009 1:45:30 PM ("arr:" h:m t) -> arr: 1:45 P
//6/15/2009 1:45:30 PM ('arr:' h:m t) -> arr: 1:45 P

//%
//Defines the following character as a custom format specifier.
//More information: Using Single Custom Format Specifiers.
//6/15/2009 1:45:30 PM (%h) -> 1

//\
//The escape character.
//6/15/2009 1:45:30 PM (h \h) -> 1 h
//Any other character
//The character is copied to the result string unchanged.
//More information: Using the Escape Character.
//6/15/2009 1:45:30 AM (arr hh:mm t) -> arr 01:45 A

