using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class EmployeeId
    {
        public string Value { get; }

        public EmployeeId([NotNull]string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException(string.Format("Invalid args.{0}", id));
            this.Value = id;
        }
    }
}
