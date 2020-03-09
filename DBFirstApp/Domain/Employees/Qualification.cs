using System;
using DBFirstApp.Domain.Employees.ValueObject;

namespace DBFirstApp.Domain.Employees
{

    public class Qualification
    {
        public QualificationCode QualificationCode { get;set;}
        public String Name { get; set; }
        public PassDate PassDate { get; set; }
        public ValidityPeriod ValidityPeriod { get; set; }

        public bool IsExpired()
        {
            return PassDate.Value >= ExpirationDate();
        }

        public DateTime ExpirationDate()
        {
            return DateTime.Now;//TODO;
            //SystemDate - 取得日
        }
    }

}
