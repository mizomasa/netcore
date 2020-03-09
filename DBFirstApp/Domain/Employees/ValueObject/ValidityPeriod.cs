using System;
namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class ValidityPeriod
    {
        public int Year { get; }
        public int Month { get; }
        public ValidityPeriod(int year,int month)
        {
            this.Year = year;
            this.Month = month;
        }
    }
}
