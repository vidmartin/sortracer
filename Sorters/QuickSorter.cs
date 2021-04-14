using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.Sorters
{
    //TODO: opravit (blbne to)
    public class QuickSorter : ISorter<float>
    {
        private static readonly Random RND = new Random();

        private class IntWrap { public int number; }

        private IEnumerable<SortOperation> SplitAroundPivot(IList<float> list, float pivot, IntWrap splitIndex)
        {
            int l = 0;
            int r = list.Count - 1;

            while (l <= r)
            {
                yield return SortOperation.Compare(l, l);
                while (l < list.Count - 1 && list[l] < pivot) 
                {
                    l += 1;

                    yield return SortOperation.Compare(l, l);
                }

                yield return SortOperation.Compare(r, r);
                while (r > 0 && list[r] > pivot)
                {
                    r -= 1;

                    yield return SortOperation.Compare(r, r);
                }

                if (l < r)
                {
                    yield return SortOperation.Swap(l, r);
                    (list[l], list[r]) = (list[r], list[l]);
                    yield return SortOperation.Update();
                }

                if (l <= r)
                {
                    l += 1;
                    r -= 1;
                }
            }

            splitIndex.number = l;
        }

        public IEnumerable<SortOperation> StepSort(IList<float> list)
        {
            float[] full_array;
            int offset;
            if (list is ArraySegment<float> seg)
            {
                full_array = seg.Array;
                offset = seg.Offset;
            }
            else if (list is float[] arr)
            {
                full_array = arr;
                offset = 0;
            }
            else
            {
                throw new InvalidOperationException();
            }


            if (list.Count == 1 || list.Count == 0)
            {
                //yield return SortOperation.Done();
                yield break;
            }

            int pivotIndex = RND.Next(list.Count);

            yield return SortOperation.Copy(pivotIndex);

            float pivot = list[pivotIndex];

            yield return SortOperation.Pivot(pivot);

            IntWrap splitIndex = new IntWrap();
            foreach (var op in SplitAroundPivot(list, pivot, splitIndex))
            {
                if (op.type == SortOperation.SO_Type.Done) { continue; }

                yield return new SortOperation(op.indexA, op.indexB, op.value, op.type);
            }

            if (list.Count <= 2)
            {
                // pokud jsme podle pivotu rozdělili posloupnost s 2 prvky, musí být seřazená
                yield return SortOperation.Update();
                yield break;
            }

            IList<float> prvni_polovina = new ArraySegment<float>(full_array, offset, splitIndex.number);
            IList<float> druha_polovina = new ArraySegment<float>(full_array, offset + splitIndex.number, list.Count - splitIndex.number);
            
            foreach (var op in StepSort(prvni_polovina))
            {
                if (op.type == SortOperation.SO_Type.Done) { continue; }

                yield return new SortOperation(op.indexA, op.indexB, op.value, op.type);
            }

            foreach (var op in StepSort(druha_polovina))
            {
                if (op.type == SortOperation.SO_Type.Done) { continue; }

                yield return new SortOperation(op.indexA + splitIndex.number, op.indexB + splitIndex.number, op.value, op.type);
            }

            yield return SortOperation.Update();
            //yield return SortOperation.Done();
        }
    }
}
