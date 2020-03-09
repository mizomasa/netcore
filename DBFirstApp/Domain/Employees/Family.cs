using System;
using System.Collections;
using System.Collections.Generic;
using DBFirstApp.Domain.Employees.ValueObject;

namespace DBFirstApp.Domain.Employees
{
    public class Family : IAggregate<Human,HumanId>
    {
        private readonly List<Human> _Humans = new List<Human>();

        public int Length => _Humans.Count;

        //public IIterator<Human> Iterator => new Iterator<Family, Human, HumanId>(this);

        public void Append(Human domainObj)
        {
            _Humans.Add(domainObj);
        }

        public Human Get(int index)
        {
            return _Humans[index];
        }

        public Human GetById(HumanId id)
        {
            return _Humans.Find(e => e.HumanId.Equals(id));
        }

        public void Remove(Human domainObj)
        {
            _Humans.Remove(domainObj);
        }

        private Iterator<Family, Human, HumanId> iterator;

        public IEnumerator<Human> GetEnumerator()
        {
            if(iterator==null)
                iterator = new Iterator<Family, Human, HumanId>(this);
            return iterator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
