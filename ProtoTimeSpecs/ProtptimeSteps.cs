using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ProtoTime;
using TechTalk.SpecFlow;

namespace ProtoTimeSpecs
{
    [Binding]
    public class ProtptimeSteps
    {
        [Given(@"the date June 1, 1926")]
        public void GivenTheDateJune11926()
        {
            ScenarioContext.Current.Pending();
        }
        [When(@"I stamp the example ""Marilyn Monroe was born on January 1, 1999\.""")]
        public void WhenIStampTheExampleMarilynMonroeWasBornOnJanuary11999_()
        {
            ScenarioContext.Current.Pending();
        }
        [Then(@"I produce ""Marilyn Monroe was born on June  1, 1926\.""")]
        public void ThenIProduceMarilynMonroeWasBornOnJune11926_()
        {
            ScenarioContext.Current.Pending();
        }
        [Given(@"the date September 8, 2011")]
        public void GivenTheDateSeptember82011()
        {
            ScenarioContext.Current.Set(new DateTime(2011, 9, 8));
        }

        [When(@"I stamp the example ""(.*)""")]
        public void WhenIStampTheExample0101(string exampleFormat)
        {
            var dateTime = ScenarioContext.Current.Get<DateTime>();
            string output = dateTime.FormatLike(exampleFormat);
            ScenarioContext.Current.Set(output);
        }

        [Then(@"I produce ""(.*)""")]
        public void ThenIProduce0908(string expectedOutput)
        {
            var result = ScenarioContext.Current.Get<string>();

            Assert.AreEqual(expectedOutput, result);
        }
    }
}
