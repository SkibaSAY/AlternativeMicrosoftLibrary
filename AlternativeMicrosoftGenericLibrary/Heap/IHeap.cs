using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternativeMicrosoftGenericLibrary
{
    public interface IHeap<TItem>
    {
        void Add(TItem newItem);
        void Heapify(int index);

        void RemoveMax();
        TItem FindMax();
        int Count { get; }

    }
}
