using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class FullName
    {
        [Required]
        [DisplayName("名前")]
        public string FirstName { get; }

        [Required]
        [DisplayName("名字")]
        public string LastName { get; }

        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(FullName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(this.FirstName, other.FirstName) &&
                string.Equals(this.LastName, other.LastName);
        }

        public override int GetHashCode()
        {
            return 0; //TODO
        }

    }
}
