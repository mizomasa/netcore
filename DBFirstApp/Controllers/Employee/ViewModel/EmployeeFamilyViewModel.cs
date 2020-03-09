using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DBFirstApp.Domain.Employees;
using DBFirstApp.Domain.Employees.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace DBFirstApp.Controllers.Employee.ViewModel
{

    public class EmployeeFamilyViewModel
    {
        
        public EmployeeFamilyViewModel() { }
        public EmployeeFamilyViewModel(string employeeId, Family family, List<Family> families)
        {
            EmployeeId = employeeId;
            Family = family;
            Families = families;
        }

        public string EmployeeId { get; set; }
        public Family Family { get; set; }
        public List<Family> Families { get; set; }
    }

    public class Family
    {
        public Family() { }
        public Family(string humanId, string lastName, string firstName, string relationShip, int sex, DateTime birthday)
        {
            HumanId = humanId;
            LastName = lastName;
            FirstName = firstName;
            RelationShip = relationShip;
            Sex = sex;
            Birthday = birthday;
        }

        public string HumanId { get; set; }
        [DisplayName("名字")]
        [Required]
        public string LastName { get; set; }

        [DisplayName("名前")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("続柄")]
        [Required]
        public string RelationShip { get; set; }

        [DisplayName("性別")]
        [Required]
        public int Sex { get; set; }

        [DisplayName("誕生日")]
        [Required]
        public DateTime Birthday { get; set; }
    }
}
