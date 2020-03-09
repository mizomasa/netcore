using System;
using System.Collections;
using System.Collections.Generic;
using DBFirstApp.Domain.Employees.ValueObject;

namespace DBFirstApp.Domain.Employees
{
    public class QualificationsHeld : IAggregate<Qualification, QualificationCode>
    {
        private readonly List<Qualification> _Qualification = new List<Qualification>();

        public int Length => _Qualification.Count;

        public void Append(Qualification domainObj)
        {
            this._Qualification.Add(domainObj);
        }

        public Qualification Get(int index)
        {
            return this._Qualification[index];
        }

        public Qualification GetById(QualificationCode id)
        {
            return this._Qualification.Find(e => e.QualificationCode.Value.Equals(id));
        }


        public void Remove(Qualification domainObj)
        {
             this._Qualification.Remove(domainObj);
        }

        public IEnumerator<Qualification> GetEnumerator()
        {
            return new Iterator<QualificationsHeld, Qualification, QualificationCode>(this);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
