using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4
{
    internal class CalendarService
    {
        private readonly AppDbContext context;

        public CalendarService(AppDbContext context)
        {
            this.context = context;
        }

        public bool IsLeapYear(int year)
        {
            DateOnly yearObj = new DateOnly(year, 1, 1);
            SaveDate(yearObj);
            return DateTime.IsLeapYear(year);
        }

        public int CalcIntervalLength(DateOnly from, DateOnly to)
        {
            SaveDate(from);
            SaveDate(to);
            return (to.ToDateTime(new TimeOnly(0, 0)) - from.ToDateTime(new TimeOnly(0, 0))).Days;
        }

        public string GetDayOfWeek(DateOnly date)
        {
            SaveDate(date);
            return date.ToString("dddd");
        }

        public IEnumerable<DateOnly> Dates
        {
            get
            {
                context.Database.EnsureCreated();
                return context.Dates.Select((e) => e.Value);
            }
        }

        private void SaveDate(DateOnly date)
        {
            context.Database.EnsureCreated();
            var value = new DateOnlyBox() { Value = date };
            if (!context.Dates.Contains(value)) {
                context.Dates.Add(value);
            }
            context.SaveChanges();
        }
    }
}
