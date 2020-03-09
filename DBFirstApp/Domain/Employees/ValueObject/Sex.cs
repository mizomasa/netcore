using System;
namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class Sex
    {
        public int Value { get; }
        public string Display { get; }
        public Sex(int value)
        {
            Value = value;
            Display = value == 0 ? "男" : "女";
        }
    }
}
