
using System.ComponentModel;

namespace _4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters());


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));

            AppDbContext context = new AppDbContext("db.sqlite");

            CalendarService service = new CalendarService(context);

            app.MapGet("/check", (HttpContext httpContext, int year) =>
            {
                return service.IsLeapYear(year);
            })
            .WithName("CheckDate")
            .WithOpenApi();

            app.MapGet("/calc", (HttpContext httpContext, DateOnly from, DateOnly to) =>
            {
                return service.CalcIntervalLength(from, to);
            })
            .WithName("CalcLength")
            .WithOpenApi();

            app.MapGet("/day", (HttpContext httpContext, DateOnly date) =>
            {
                return service.GetDayOfWeek(date);
            })
            .WithName("GetWeekDay")
            .WithOpenApi();

            app.MapGet("/show", (HttpContext httpContext) =>
            {
                return service.Dates;
            })
            .WithName("ShowAllDates")
            .WithOpenApi();

            app.Run();
        }
    }
}
