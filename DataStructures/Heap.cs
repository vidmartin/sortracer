using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.DataStructures
{
    public class Heap
    {
        public static Heap<T, T> Max<T>(int initSize = 64) where T : IComparable<T>
        {
            return Heap<T, T>.Max(a => a, initSize);
        }

        public static Heap<T, T> Min<T>(int initSize = 64) where T : IComparable<T>
        {
            return Heap<T, T>.Min(a => a, initSize);
        }

        public static Heap<T, T> WrapMax<T>(T[] array) where T : IComparable<T>
        {
            return Heap<T, T>.WrapMax(array, a => a);
        }

        public static Heap<T, T> WrapMin<T>(T[] array) where T : IComparable<T>
        {
            return Heap<T, T>.WrapMin(array, a => a);
        }
    }

    public class Heap<TVal, TKey> where TKey : IComparable<TKey>
    {
        public static Heap<TVal, TKey> Max(Func<TVal, TKey> key, int initSize = 64)
        {
            return new Heap<TVal, TKey>(-1, key, initSize);
        }

        public static Heap<TVal, TKey> Min(Func<TVal, TKey> key, int initSize = 64)
        {
            return new Heap<TVal, TKey>(+1, key, initSize);
        }

        public static Heap<TVal, TKey> WrapMax(TVal[] array, Func<TVal, TKey> key)
        {
            return new Heap<TVal, TKey>(-1, key, array);
        }

        public static Heap<TVal, TKey> WrapMin(TVal[] array, Func<TVal, TKey> key)
        {
            return new Heap<TVal, TKey>(+1, key, array);
        }

        protected Heap(int wrongParentToChildCompare, Func<TVal, TKey> key, int initSize)
            : this(wrongParentToChildCompare, key, new TVal[initSize])
        {
            _len = 0;
        }

        protected Heap(int wrongParentToChildCompare, Func<TVal, TKey> key, TVal[] array)
        {
            _wrongParentToChildCompare = wrongParentToChildCompare;
            _key = key;
            _arr = array;
            _len = array.Length;
        }

        private int _wrongParentToChildCompare;
        private Func<TVal, TKey> _key;
        private TVal[] _arr;
        private int _len;

        public TVal Root => _len > 0 ? _arr[0] : throw new InvalidOperationException();
        public int Count => _len;

        private void ExpandArrayIfNeeded()
        {
            if (_len == _arr.Length)
            {
                var temp = _arr;
                _arr = new TVal[_arr.Length * 2];
                temp.CopyTo(_arr, 0);
            }
        }

        private int L(int index) => index * 2 + 1;
        private int R(int index) => index * 2 + 2;
        private int P(int index) => (index - 1) / 2;

        private bool IsOrderWrong(TVal parent, TVal child)
            => _key(parent).CompareTo(_key(child)) == _wrongParentToChildCompare;

        private void Swap(int index1, int index2)
        {
            var temp = _arr[index1];
            _arr[index1] = _arr[index2];
            _arr[index2] = temp;
        }

        public void Add(TVal val)
        {
            ExpandArrayIfNeeded();

            int i = _len;

            _arr[i] = val;

            while (FixUpStep(i, out i)) { }

            _len += 1;
        }

        private bool FixUpStep(int index, out int parent)
        {
            if (index == 0)
            {
                parent = -1;
                return false;
            }

            parent = P(index);

            if (IsOrderWrong(_arr[parent], _arr[index]))
            {
                Swap(parent, index);        
                return true; // pokračujem
            }

            return false;
        }

        public TVal Pop()
        {
            if (_len == 0) { throw new InvalidOperationException(); }

            int i = 0;

            var root = _arr[i];

            _len -= 1;
            _arr[i] = _arr[_len];

            while (FixDownStep(i, out i)) { }

            return root;
        }

        private bool FixDownStep(int index, out int child)
        {
            var l = L(index);
            var r = R(index);

            if (l >= _len)
            {
                child = -1;
                return false;
            }

            if(IsOrderWrong(_arr[index], _arr[l]) || (r < _len && IsOrderWrong(_arr[index], _arr[r])))
            {
                child = (r == _len || IsOrderWrong(_arr[r], _arr[l])) ? l : r;
                Swap(index, child);
                return true;
            }

            child = -1;
            return false;
        }

        public void Heapify()
        {
            int how_many_with_children = (int)Math.Pow(2, (int)Math.Log(_len, 2));

            for (int i = how_many_with_children - 1; i >= 0; i--)
            {
                int c = i;
                while (FixDownStep(c, out c)) { }
            }
        }

        public IEnumerable<TVal> Traverse() => _arr.Select(a => a);
    }
}
