using System;
namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class Birthday
    {
        public DateTime Value { get; }
        public int Age { get; }
        public Birthday(DateTime value)
        {
            Value = value;
            this.Age = 0;//TODO
        }
    }
}
