using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBFirstApp.Controllers.Employee.ViewModel
{
    public class EmployeeDetailAllViewModel
    {
        public EmployeeSelfViewModel Self { get; set; }

        public EmployeeFamilyViewModel Family { get; set; }

        public EmployeeDetailAllViewModel()
        {
            Self = new EmployeeSelfViewModel();
            Family = new EmployeeFamilyViewModel();
            Family.Families = new System.Collections.Generic.List<Family>();
        }
    }
}
