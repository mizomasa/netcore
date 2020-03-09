using System;
using System.Collections;
using System.Collections.Generic;

namespace DBFirstApp.Domain.Employees
{

    public class Iterator<T, E, E_OF_Key>
        : IEnumerator<E> where T : IAggregate<E, E_OF_Key>
    {

        private readonly T _Object;

        private int index = 0;

        public Iterator(T obj)
        {
            _Object = obj;
        }

        public E Current => this._Object.Get(index);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            index++;
            return index < this._Object.Length;
        }

        public void Reset()
        {
            index = 0;
        }
    }
}
