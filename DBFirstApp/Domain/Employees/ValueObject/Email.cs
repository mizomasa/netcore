using System;
using System.Diagnostics.CodeAnalysis;

namespace DBFirstApp.Domain.Employees.ValueObject
{
    public class Email
    {
        public string Value { get; }

        public Email([NotNull]string mailAddress)
        {
            if (string.IsNullOrEmpty(mailAddress)) throw new ArgumentException(string.Format("Invalid args.{0}", mailAddress));
            this.Value = mailAddress;
        }

    }
}
