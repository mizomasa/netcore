using System;
namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class QualificationCode
    {
        public string Value { get; }

        public QualificationCode(string value)
        {
            this.Value = value;
        }
    }
}
