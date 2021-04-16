using System;
using System.Collections.Generic;

namespace sort
{
    interface ISortType<T>
    {
        void Sort(T [] array);
    };

    class BubbleSort<T> : ISortType<T>
    {
        public void Sort(T [] array)
        {
            Comparer<T> defaultComparer = Comparer<T>.Default;
            bool changed = false;
            for( uint i = 1; i < array.Length; ++i )
            {
                for ( uint j = 1; j < array.Length; ++j )
                {
                    if( defaultComparer.Compare(array[j], array[j+1]) < 0 )
                    {
                        var temp = array[j];
                        array[j] = array[j-1];
                        array[j-1] = temp;
                        changed = true;
                    }
                }
                if (changed == false)
                    return;
            }
        }
    };

    class InsertionSort<T> : ISortType<T>
    {
        public void Sort(T [] array)
        {
            Comparer<T> defaultComparer = Comparer<T>.Default;
            for( uint i = 1; i < array.Length; ++i)
            {
                var temp = array[i];
                var j = i;
                while(j > 0 && defaultComparer.Compare(temp, array[j-1]) < 0 )
                {
                    array[j] = array[j-1];
                    --j;

                }
                array[j] = temp;
            }
        }
    };


    class SortedArray<T>
    {
        private T [] arr;
        private ISortType<T> currentSortType;
        public SortedArray( T [] array )
        {
            arr = array;
        }
        public void SetSorType( ISortType<T> type )
        {
            currentSortType = type;
        }
        public void Sort()
        {
            currentSortType.Sort(arr);
        }
        public void Show()
        {
            Console.Write("[");
            foreach( var currentElement in arr )
                Console.Write($"{currentElement}, ");
            Console.Write("\b\b]\n");
        }       
    };

    class Program
    {
        static void Main(string[] args)
        {
            int [] arr = new int[] {3, 5, 1, 6, 3, 7, 10, 23, 11, 5};
            SortedArray<int> testArr = new SortedArray<int>( arr);
            testArr.SetSorType( new InsertionSort<int>() );
            testArr.Show();
            testArr.Sort();
            testArr.Show();
        }
    }
}
