using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.Sorters
{
    public struct SortOperation
    {
        public enum SO_Type
        {
            Update, Compare, Swap, Done, Set, Copy, Pivot, HiddenCompare
        }

        public readonly int indexA;
        public readonly int indexB;
        public readonly SO_Type type;
        public readonly float value;

        public SortOperation(int indexA, int indexB, SO_Type type)
        {
            this.indexA = indexA;
            this.indexB = indexB;
            this.type = type;
            this.value = 0;
        }

        public SortOperation(int indexA, int indexB, float value, SO_Type type)
        {
            this.indexA = indexA;
            this.indexB = indexB;
            this.type = type;
            this.value = value;
        }

        public static SortOperation Compare(int indexA, int indexB)
            => new SortOperation(indexA, indexB, SO_Type.Compare);

        public static SortOperation HiddenCompare()
            => new SortOperation(0, 0, SO_Type.HiddenCompare);

        public static SortOperation Swap(int indexA, int indexB)
            => new SortOperation(indexA, indexB, SO_Type.Swap);

        public static SortOperation Update()
            => new SortOperation(0, 0, SO_Type.Update);

        public static SortOperation Done()
            => new SortOperation(0, 0, SO_Type.Done);

        public static SortOperation Set(int index)
            => new SortOperation(index, index, SO_Type.Set);

        public static SortOperation Copy(int index)
            => new SortOperation(index, index, SO_Type.Copy);

        public static SortOperation CopyFromTo(int from, int to)
            => new SortOperation(from, to, SO_Type.Copy);

        public static SortOperation Pivot(float value)
            => new SortOperation(0, 0, value, SO_Type.Pivot);
    }
}
