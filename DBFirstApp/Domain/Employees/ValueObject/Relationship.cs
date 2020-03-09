using System;
namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class Relationship
    {
        public string Value { get; }
        public string Name { get; }

        public Relationship(string value)
        {
            this.Value = value;
            this.Name = value == "0" ? "本人" : "本人以外"; //TODO
        }
    }
}
