using System;
using System.Collections;
using System.Collections.Generic;

namespace DBFirstApp.Domain.Employees
{
    public interface IAggregate<E,E_OF_Key>: IEnumerable<E>
    {
        //IIterator<E> Iterator { get; }
        public int Length { get; }
        public void Append(E domainObj);
        public E Get(int index);
        public E GetById(E_OF_Key id);
        public void Remove(E domainObj);
    }

}
