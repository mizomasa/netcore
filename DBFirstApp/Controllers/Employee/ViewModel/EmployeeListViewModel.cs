using System;
using System.ComponentModel;

namespace DBFirstApp.Controllers.Employee.ViewModel
{
    public class EmployeeListViewModel
    {

        [DisplayName("ユーザID")]
        public string EmployeeId { get; set; }

        public string HumanId { get; set; }

        [DisplayName("名")]
        public string FirstName { get; set; }

        [DisplayName("姓")]
        public string LastName { get; set; }

        [DisplayName("年齢")]
        public string Age { get; set; }

    }
}