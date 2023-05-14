using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternativeMicrosoftGenericLibrary
{
    public class Heap<TItem>: IHeap<TItem>
        where TItem: IComparable<TItem>
    {
        private int _capacity = 4;
        private int _childsCount;
        private IComparer<TItem> _comparer;
        private TItem[] _items;

        public Heap(IComparer<TItem> comparer = null,int childsCount = 2)
        {
            if (comparer == null) comparer = Comparer<TItem>.Default;

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

            HeapifyTop(newIndex);

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

        private void HeapifyTop(int index)
        {
            var isNotRoot = true;
            var parent = GetParrentIndex(index);
            while (isNotRoot)
            {
                if (parent == 0) isNotRoot = false;
                HeapifyLoop(parent);
                parent = GetParrentIndex(parent);
            }
        }

        private void HeapifyDown(int parrentIndex)
        {
            var queue = new Queue<int>();
            queue.Enqueue(parrentIndex);
            while(queue.Count > 0)
            {
                var currentIndex = queue.Dequeue();
                HeapifyLoop(currentIndex);
                var childs = GetChildIndexes(currentIndex);
                childs.ForEach(child => queue.Enqueue(child));
            }
        }

        private List<int> GetChildIndexes(int parentIndex)
        {
            var result = new List<int>();
            var startIndex = parentIndex * _childsCount;
            
            for(var i = 1; i <= _childsCount; i++)
            {
                var current = startIndex + i;
                if (current >= Count)
                {
                    break;
                }
                result.Add(current);
            }
            return result;
        }
        private int GetParrentIndex(int childIndex)
        {
            return (childIndex - 1) / _childsCount;
        }

        /// <summary>
        /// Для избавления от рекурсивных вызовов в Heapify
        /// </summary>
        /// <param name="index"></param>
        /// <returns>индекс ,с которым свапнул index</returns>
        private int HeapifyLoop(int index)
        {
            var minValueIndex = index;
            var current = _items[index];

            foreach(var childIndex in GetChildIndexes(index))
            {
                if(childIndex >= Count)
                {
                    break;
                }
                var childItem = _items[childIndex];
                if (_comparer.Compare(current, childItem) == 1)
                {
                    current = childItem;
                    minValueIndex = childIndex;
                }
            }
            if (minValueIndex == index) return -1;
            Swap(index, minValueIndex);
            return minValueIndex;
        }

        private void Swap(int indexA,int indexB)
        {
            var temp = _items[indexA];
            _items[indexA] = _items[indexB];
            _items[indexB] = temp;
        }

        public void RemoveMax()
        {
            if (Count == 0) throw new ArgumentException("Heap is Empty!");
            Count--;
            var temp = _items;
            _items = new TItem[_capacity];
            Array.Copy(temp, 1, _items, 0, Count);

            this.HeapifyDown(0);
        }

        public TItem FindMax()
        {
            return _items[0];
        }

        public IEnumerable<TItem> GetHeapItems()
        {
            return _items;
        }
        public static bool IsHeap(IEnumerable<TItem> heap,int childCount)
        {
            throw new ArgumentException("");
        }

        public TItem[] Sort()
        {
            var heap = this;

            var tempItems = new TItem[heap.Count];
            Array.Copy(_items, tempItems, heap.Count);

            var sortedArray = new TItem[heap.Count];
            var count = heap.Count;

            while (heap.Count > 0)
            {
                var max = heap.FindMax();
                heap.Count--;
                sortedArray[heap.Count] = max;

                heap.Swap(0, heap.Count);
                heap.HeapifyDown(0);
            }

            //тк минимум относительно компоратора будет в конце
            sortedArray = sortedArray.Reverse().ToArray();

            //потому что испортили для пирамидальной сортировки
            heap.Count = count;
            _items = tempItems;

            return sortedArray;
        }

        /// <summary>
        /// Обьединяет несколько отсортированных массивов в один отсортированный массив
        /// </summary>
        public static List<TItem> SortSeveralSortedList(List<TItem[]> arrays,IComparer<TItem> comparer = null)
        {
            var result = new List<TItem>();

            if (comparer == null) comparer = Comparer<TItem>.Default;

            var resourses = new(TItem[] arr,int nextIndex)[arrays.Count];

            //создаём кучу со своим компаратором
            var heap = new Heap<(TItem value,int arrIndex)>(
                comparer:Comparer<(TItem value, int arrIndex)>
                .Create(
                    (x,y)=> { return comparer.Compare(x.value,y.value); }
                    )
                );

            //предподготовка
            for(var i = 0; i < arrays.Count; i++)
            {
                var arr = arrays[i];
                if (arr.Length != 0)
                {
                    heap.Add((value: arr[0],i));
                    resourses[i] = (arr: arr, nextIndex: 1);
                }
            }

            //основной цикл
            while(heap.Count > 0)
            {
                var current = heap.FindMax();
                heap.RemoveMax();

                result.Add(current.value);
                var resourse = resourses[current.arrIndex];
                if(resourse.nextIndex < resourse.arr.Length)
                {
                    var newItem = resourse.arr[resourse.nextIndex];
                    resourses[current.arrIndex].nextIndex++;
                    heap.Add((newItem, current.arrIndex));
                }
            }
            return result;
        }
    }
}
