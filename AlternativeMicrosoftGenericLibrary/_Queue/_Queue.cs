using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternativeMicrosoftGenericLibrary
{
    public class _Queue<T>
    {
        private T[] _items { get; set; }

        private readonly int _expansionIndex = 2;
        public int _capacity = 4;

        private int _count = 0;
        public int Count
        {
            get { return _count; }
        }

        private void IncreaseIndex(ref int index)
        {
            index = (index+1)%_capacity;
        }

        private int tail;
        private int header;

        public _Queue()
        {
            _items = new T[_capacity];
        }
        void EnlargeSize(int newSize)
        {
            

            var oldItems = _items;
            _items = new T[newSize];

            Array.Copy(oldItems, header, _items, 0, (_count - header));
            Array.Copy(oldItems, 0, _items, _count - header, _count - tail);

            _capacity = newSize;
            header = 0;
            tail = _count;
        }

        public void Enqueue(T newItem)
        {
            if(_count == _capacity)
            {
                EnlargeSize(_capacity * _expansionIndex);
            }

            _items[tail] = newItem;
            IncreaseIndex(ref tail);
            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0) throw new _InvalidOperationException("Очередь Queue<T> является пустой.");
            var result = _items[header];
            IncreaseIndex(ref header);
            _count--;
            return result;
        }

        public T Peek()
        {
            if (_count != 0) 
                return _items[header];
            else 
                throw new _InvalidOperationException("Очередь Queue<T> является пустой.");
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < _count; i++)
            {
                var index = (header + i) % _capacity;
                if (item.Equals(_items[index])) return true;
            }
            return false;
        }
    }
}
