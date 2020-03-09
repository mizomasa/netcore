using System;
using System.Diagnostics.CodeAnalysis;

namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class HumanId : IEquatable<HumanId>
    {
        public int Value { get; }

        public HumanId(int id)
        {
            Value = id;
        }

        public bool Equals([AllowNull] HumanId other)
        {
            return this.Value == other.Value;
        }
    }
}
