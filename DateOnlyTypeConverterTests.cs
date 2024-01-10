using NUnit.Framework;
using System;


namespace TestProject1
{

    public class DateOnlyTypeConverterTests
    {
        [Fact]
        public void CanConvertFrom_String_ReturnsTrue()
        {

            var converter = new DateOnlyTypeConverter();
            bool result = converter.CanConvertFrom(null, typeof(string));

            Assert.True(result);
        }

        [Fact]
        public void ConvertFrom_ValidString_ReturnsDateOnly()
        {

            var converter = new DateOnlyTypeConverter();
            string input = "2022-01-01";


            var result = converter.ConvertFrom(null, null, input);


            Assert.InstanceOf<DateOnly>(result);
            Assert.Equal(new DateOnly(2022, 1, 1), result);
        }

        [Fact]
        public void CanConvertTo_String_ReturnsTrue()
        {

            var converter = new DateOnlyTypeConverter();


            bool result = converter.CanConvertTo(null, typeof(string));


            Assert.True(result);
        }

        [Fact]
        public void ConvertTo_DateOnly_ReturnsValidString()
        {

            var converter = new DateOnlyTypeConverter();
            DateOnly date = new DateOnly(2022, 1, 1);


            var result = converter.ConvertTo(null, null, date, typeof(string));


            Assert.IsInstanceOf<string>(result);
            Assert.Equal("2022-01-01", result);
        }
    }
    }
