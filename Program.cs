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

    class MergeSort<T> : ISortType<T>
    {
        private Comparer<T> _defaultComparer = Comparer<T>.Default;
        private T[][] _subArray(T[] array)
        {
            if(array.Length <= 1)
            {
                return new T[][]{array};
            }
            int halfLength = (int)(array.Length / 2);
            T [] firstArray = new T[halfLength];
            T [] secondArray = new T[array.Length - halfLength];
            Array.Copy(array, 0, firstArray, 0, halfLength);
            Array.Copy(array, halfLength, secondArray, 0, array.Length - halfLength);

            return new T[2][]{firstArray, secondArray};
        }

        private T[] _mSort(T [] array)
        {
            T[] tempArray = new T[array.Length];
            if(array.Length == 1)
            {
                return array;
            }
            var subArrays = _subArray(array);
            subArrays[0] = _mSort(subArrays[0]);
            subArrays[1] = _mSort(subArrays[1]);

            uint i = 0, j = 0;
            uint z = 0;

            while(i < subArrays[0].Length && j < subArrays[1].Length )
            {
                tempArray[z++] = _defaultComparer.Compare(subArrays[0][i], subArrays[1][j]) < 0 ? subArrays[0][i++] : subArrays[1][j++];
            }
            while(i < subArrays[0].Length)
                tempArray[z++] = subArrays[0][i++];
            while(j < subArrays[1].Length)
                tempArray[z++] = subArrays[1][j++];

            return tempArray;
        }
        public void Sort(T [] array)
        {
            _mSort(array).CopyTo(array, 0);
        }
    }


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
            testArr.SetSorType( new MergeSort<int>() );
            testArr.Show();
            testArr.Sort();
            testArr.Show();
        }
    }
}
