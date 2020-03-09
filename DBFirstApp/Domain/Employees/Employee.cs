using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DBFirstApp.Domain.Employees.ValueObject;

namespace DBFirstApp.Domain.Employees
{

    public class Employee
    {
        public EmployeeId EmployeeId { get; set; }
        public Email Email { get; set; }
        public Human EmployeeAttr { get; set; }
        private Family Family { get; set; }
        private QualificationsHeld QualificationsHeld { get; set; }

        private Employee() { }
        public Employee(EmployeeId employeeId, Email email, Human employeeAttr)
        {
            EmployeeId = employeeId;
            Email = email;
            EmployeeAttr = employeeAttr;
            Family = new Family();
            QualificationsHeld = new QualificationsHeld();
        }

        public Employee ChangeName(FullName fullName)
        {
            this.EmployeeAttr.FullName = fullName;
            return this;
        }

        public Employee ChangeSex(Sex sex)
        {
            this.EmployeeAttr.Sex = sex;
            return this;
        }

        public Employee ChangeEmail(Email email)
        {
            this.Email = email;

            return this;
        }

        public int HumanId => this.EmployeeAttr.HumanId.Value;
        public string FirstName => this.EmployeeAttr.FullName.FirstName;
        public string LastName => this.EmployeeAttr.FullName.LastName;
        public int Sex => this.EmployeeAttr.Sex.Value;
        public DateTime Birthday => this.EmployeeAttr.Birthday.Value;
        public int Age => this.EmployeeAttr.Birthday.Age;

        public IEnumerator<Human> FamilyIterator => this.Family.GetEnumerator();

        public Human Get(int index) => Family.Get(index);

        public Human GetById(HumanId id) => this.Family.GetById(id);

        public void Remove(Human t)
        {
            this.Family.Remove(t);
        }

        public void Append(Human t)
        {
            this.Family.Remove(t);
            this.Family.Append(t);
        }


        public IEnumerator<Qualification> QualificationIterator() => this.QualificationsHeld.GetEnumerator();

        public Qualification GetQualification(int index) => this.QualificationsHeld.Get(index);

        public Qualification GetById(QualificationCode id) => this.QualificationsHeld.GetById(id);

        public void Remove(Qualification qualification)
        {
            this.QualificationsHeld.Remove(qualification);
        }

        public void Append(Qualification qualification)
        {
            this.QualificationsHeld.Append(qualification);
        }
    }
}