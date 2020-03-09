using System;
namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class PassDate
    {
        public DateTime Value { get; }

        public PassDate(DateTime value)
        {
            this.Value = value;
        }
    }
}
