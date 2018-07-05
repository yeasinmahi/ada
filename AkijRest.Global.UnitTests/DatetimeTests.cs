using System;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AkijRest.Global.UnitTests
{
    [TestFixture]
    public class DatetimeTests
    {
        [TestCase(Datetime.Format.dd_MM_yyyy, "dd/MM/yyyy")]
        [TestCase(Datetime.Format.MM_dd_yyyy, "MM/dd/yyyy")]
        public void GetStringFromDateFormat_InputEnumFormat_ReturndStringFormat(Datetime.Format format, string expected)
        {
            // Arrange
            //string format = "dd/MM/yyyy";
            //Act
            var result = Datetime.GetStringFromDateFormat(format);
            //Assert
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void ToDateTime_StringToDateTime_ReturnDateTime()
        {
            //Arrange
            string date = "31/12/2018"; //default format in dd/MM/yyyy
            //Act
            var result = Datetime.ToDateTime(date);
            //Assert
            Assert.AreEqual(new DateTime(2018,12,31), result);
        }
        [Test]
        public void ToDateTime_StringToDateTimeWithFormatddMMyyyy_ReturnDateTime()
        {
            //Arrange
            string date = "30/11/2018";
            //Act
            var result = Datetime.ToDateTime(date, Datetime.Format.dd_MM_yyyy);
            //Assert
            Assert.AreEqual(new DateTime(2018, 11, 30), result);
        }
        [Test]
        public void ToDateTime_StringToDateTimeWithFormatMMddyyyy_ReturnDateTime()
        {
            //Arrange
            string date = "11/30/2018";
            //Act
            var result = Datetime.ToDateTime(date, Datetime.Format.MM_dd_yyyy);
            //Assert
            Assert.AreEqual(new DateTime(2018, 11, 30), result);
        }
        [Test]
        public void ToString_DateTimeToString_ReturnString()
        {
            //Arrange
            DateTime date = new DateTime(2018,12,31);
            //Act
            var result = Datetime.ToString(date);
            //Assert
            Assert.AreEqual("31/12/2018", result);
        }
        [Test]
        public void ToString_DateTimeToStringWithFormatddMMyyyy_ReturnString()
        {
            //Arrange
            DateTime date = new DateTime(2018, 12, 31);
            //Act
            var result = Datetime.ToString(date, Datetime.Format.dd_MM_yyyy);
            //Assert
            Assert.AreEqual("31/12/2018", result);
        }
        [Test]
        public void ToString_DateTimeToStringWithFormatMMddyyyy_ReturnString()
        {
            //Arrange
            DateTime date = new DateTime(2018, 12, 31);
            //Act
            var result = Datetime.ToString(date, Datetime.Format.MM_dd_yyyy);
            //Assert
            Assert.AreEqual("12/31/2018", result);
        }
    }
}
