using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternativeMicrosoftGenericLibrary
{
    public class Heap<TItem>: IHeap<TItem>
    {
        private int _capacity = 4;
        private int _childsCount;
        private IComparer<TItem> _comparer;
        private TItem[] _items;

        public Heap(IComparer<TItem> comparer,int childsCount = 2)
        {
            if (childsCount < 2) throw new ArgumentException("childsCount cannot be less 2");

            this._childsCount = childsCount;
            this._comparer = comparer;
            this._items = new TItem[_capacity];
        }

        public int Count { get; set; }

        public void Add(TItem newItem)
        {
            var newIndex = Count;
            _items[newIndex] = newItem;
            Count++;

            Heapify(newIndex);

            if (newIndex == _capacity - 1)
            {
                IncreaseCapacity();
            }    
        }
        private void IncreaseCapacity()
        {
            _capacity *= 2;
            var tempArr = new TItem[_capacity];
            Array.Copy(_items, tempArr, _items.Length);
            _items = tempArr;
        }

        public void Heapify(int index)
        {
            if(index < 0 || index >= Count)
            {
                throw new ArgumentException("index is outside the heap");
            }

            var isNotRoot = true;
            var current = index;
            while (isNotRoot)
            {
                if (current == 0) isNotRoot = false;
                HeapifyLoop(index);
                current = GetParrentIndex(current);
            }
        }
        private int GetParrentIndex(int childIndex)
        {
            return childIndex / _childsCount;
        }

        /// <summary>
        /// Для избавления от рекурсивных вызовов в Heapify
        /// </summary>
        /// <param name="index"></param>
        private void HeapifyLoop(int index)
        {
            var maxValueIndex = index;
            var current = _items[index];

            for (var i = 1; i <= _childsCount; i++)
            {
                var currChildIndex = index * 2 + i;
                if(currChildIndex >= Count)
                {
                    break;
                }
                var child = _items[currChildIndex];
                if (_comparer.Compare(current, child) == -1)
                {
                    maxValueIndex = currChildIndex;
                }
                if (maxValueIndex == index) return;

                Swap(index, maxValueIndex);
            }

        }

        private void Swap(int indexA,int indexB)
        {
            var temp = _items[indexA];
            _items[indexA] = _items[indexB];
            _items[indexB] = temp;
        }

        public void RemoveMax()
        {
            throw new NotImplementedException();
        }

        public void FindMax()
        {
            throw new NotImplementedException();
        }
    }
}
