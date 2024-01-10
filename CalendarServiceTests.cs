using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TestProject1
{
    public class CalendarServiceTests
    {
        [Fact]
        public void IsLeapYear_ReturnsTrueForLeapYear()
        {

            var mockContext = new Mock<AppDbContext>();
            var calendarService = new CalendarService(mockContext.Object);
            int year = 2024;
            bool result = calendarService.IsLeapYear(year);
            Assert.True(result);
        }


        [Fact]
        public void CalcIntervalLength_ReturnsCorrectIntervalLength()
        {
            var mockDbContext = new Mock<AppDbContext>();
            var calendarService = new CalendarService(mockDbContext.Object);
            var from = new DateOnly(2022, 1, 1);
            var to = new DateOnly(2022, 1, 10);
            var result = calendarService.CalcIntervalLength(from, to);

            Assert.Equal(9, result);
        }

        [Fact]
        public void GetDayOfWeek_ReturnsCorrectDayOfWeek()
        {
            var mockDbContext = new Mock<AppDbContext>();
            var calendarService = new CalendarService(mockDbContext.Object);
            var date = new DateOnly(2022, 1, 1);

            var result = calendarService.GetDayOfWeek(date);

            Assert.Equal("Saturday", result);
        }

        [Fact]
        public void Dates_ReturnsCorrectDates()
        {

            var mockDbContext = new Mock<AppDbContext>();
            var calendarService = new CalendarService(mockDbContext.Object);
            mockDbContext.Setup(db => db.Dates).Returns(new List<DateOnly> { new DateOnly(2022, 1, 1), new DateOnly(2022, 1, 10) });

            var result = calendarService.Dates;

            Assert.Contains(new DateOnly(2022, 1, 1), result);
            Assert.Contains(new DateOnly(2022, 1, 10), result);
        }
    }
}

