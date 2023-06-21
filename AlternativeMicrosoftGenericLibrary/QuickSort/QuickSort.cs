using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternativeMicrosoftGenericLibrary.QuickSort
{
    public static class QuickSortExtention<T>
        where T:IComparable<T>
    {
        public static void QuickSort(T[] arr,int l = -1,int r = -1,int statementIndex = 0)
        {
            //инициализация            
            if (l == -1) l = 0;
            if (r == -1) r = arr.Length - 1;
            if (r - l + 1 < 2) return;

            var initL = l;
            var initR = r;

            var statementElement = arr[statementIndex];

            while(l <= r)
            {
                while(l <= r && arr[l].CompareTo(statementElement) < 0)
                {
                    l++;
                }
                while (l <= r && arr[r].CompareTo(statementElement) > 0)
                {
                    r--;
                }
                
                //меняем элементы местами
                if(l <= r)
                {
                    var temp = arr[l];
                    arr[l] = arr[r];
                    arr[r] = temp;
                    l++;
                    r--;
                }
            }
            if (r > initL)QuickSort(arr, l: initL, r: r);
            if (l < initR) QuickSort(arr, l: l, r: initR);
        }
    }
}
