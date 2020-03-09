using System;
using System.Collections.Generic;

namespace DBFirstApp.Models
{
    public partial class TEmployee
    {
        public string EmployeeId { get; set; }
        public string MailAddress { get; set; }
        public int HumanId { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
