using System;
using NUnit.Framework;
using ProtoTime;

namespace ProtoTime.Tests
{
	[TestFixture]
	public class DateTimeExtensionsTests
	{
		DateTime _sut;

		[SetUp]
		public void BeforeEach ()
		{
			_sut = new DateTime (1974, 11, 12);
		}

		[Test]
		public void FormatLike_WithNoParameters_ReturnsFormatWithCurrentCulture ()
		{
			string result = _sut.FormatLike ();
			
			Assert.AreEqual (_sut.ToString (), result);
		}

		[Test()]
		[Description("Right now we do not take culture into consideration")]
		public void EachMonth ()
		{
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 01, 01), "Jan");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 02, 01), "Feb");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 03, 01), "Mar");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 04, 01), "Apr");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 05, 01), "May");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 06, 01), "Jun");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 07, 01), "Jul");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 08, 01), "Aug");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 09, 01), "Sep");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 10, 01), "Oct");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 11, 01), "Nov");
			FormatLike_WithMonth_OutputsCorrectMonth (new DateTime (2000, 12, 01), "Dec");
		}

		public void FormatLike_WithMonth_OutputsCorrectMonth (DateTime sut, string expectedResult)
		{
			string result = sut.FormatLike ("Apr");
			
			Assert.AreEqual (expectedResult, result);
		}
		
		[Test]
		public void FormatLike_WithMonthAndDayInMonth()
		{
			string expectedResult = "Nov 12";
			string result = _sut.FormatLike ("Apr 01");
			
			Assert.AreEqual (expectedResult, result);
		}
	}
}

