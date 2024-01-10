using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TestProject1
{
    public class AppDbContextTests
    {
        [Fact]
        public void AppDbContext_DbPath_ReturnsProperValue()
        {

            var dbPath = "test_db.db";
            var dbContext = new AppDbContext(dbPath);
            var result = dbContext.DbPath;
            Assert.Equal(dbPath, result);
        }

        [Fact]
        public void AppDbContext_Intervals_ReturnsDbSet()
        {

            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "test_db").Options;
            var mockContext = new Mock<AppDbContext>(options);
            var intervalData = new List<Interval>
        {
            new Interval { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddHours(1) },
            new Interval { Id = 2, Start = DateTime.Now.AddHours(2), End = DateTime.Now.AddHours(3) }};
            var mockDbSet = new Mock<DbSet<Interval>>();
            mockDbSet.As<IQueryable<Interval>>().Setup(m => m.Provider).Returns(intervalData.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Interval>>().Setup(m => m.Expression).Returns(intervalData.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Interval>>().Setup(m => m.ElementType).Returns(intervalData.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Interval>>().Setup(m => m.GetEnumerator()).Returns(intervalData.AsQueryable().GetEnumerator());
            mockContext.Setup(m => m.Intervals).Returns(mockDbSet.Object);

            var result = mockContext.Object.Intervals;

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}