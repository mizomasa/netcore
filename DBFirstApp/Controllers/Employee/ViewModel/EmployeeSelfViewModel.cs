using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DBFirstApp.Controllers.Employee.ViewModel
{
    public class EmployeeSelfViewModel
    {
        [Required]
        [DisplayName("ユーザID")]
        public string Id { get; set; }

        [EmailAddress]
        [DisplayName("メールアドレス")]
        public string Email { get; set; }

        [Required]
        [DisplayName("名")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("姓")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("性別")]
        public int Sex { get; set; }

        [Required]
        [DisplayName("誕生日")]
        public DateTime Birthday {get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
