using System;
using DBFirstApp.Domain.Employees.ValueObject;

namespace DBFirstApp.Domain.Employees
{
    public class Human
    {
        public Human(HumanId humanId, FullName fullName, Sex sex, Birthday birthday, Relationship relationship)
        {
            HumanId = humanId;
            FullName = fullName;
            Sex = sex;
            Birthday = birthday;
            Relationship = relationship;
        }

        public HumanId HumanId { get; set; }
        public FullName FullName { get; set; }
        public Sex Sex { get; set; }
        public Birthday Birthday { get; set; }
        public Relationship Relationship { get; set; }
    }

}
