using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.Sorters
{
    public class TopDownMergeSorter : ISorter<float>
    {
        private IEnumerable<SortOperation> Merge(IList<float> one, IList<float> two, IList<float> output)
        {
            if (output.Count != one.Count + two.Count)
            {
                throw new InvalidOperationException();
            }

            int one_i = 0;
            int two_i = 0;

            while (one_i < one.Count && two_i < two.Count)
            {
                yield return SortOperation.HiddenCompare();

                yield return SortOperation.Set(one_i + two_i);

                if (one[one_i] <= two[two_i])
                {
                    output[one_i + two_i] = one[one_i];
                    one_i += 1;
                }
                else
                {
                    output[one_i + two_i] = two[two_i];
                    two_i += 1;
                }

                yield return SortOperation.Update();
            }

            for (; one_i < one.Count; one_i++)
            {
                yield return SortOperation.Set(one_i + two_i);

                output[one_i + two_i] = one[one_i];

                yield return SortOperation.Update();
            }

            for (; two_i < two.Count; two_i++)
            {
                yield return SortOperation.Set(one_i + two_i);

                output[one_i + two_i] = two[two_i];

                yield return SortOperation.Update();
            }
        }

        public IEnumerable<SortOperation> StepSort(IList<float> list)
        {
            if (list.Count == 1)
            {
                yield break;
            }

            int split_count = list.Count / 2;

            ArraySegment<float> prvni_polovina;
            ArraySegment<float> druha_polovina;

            if (list is float[] arr)
            {
                prvni_polovina = new ArraySegment<float>(arr, 0, split_count);
                druha_polovina = new ArraySegment<float>(arr, split_count, list.Count - split_count);
            }
            else if (list is ArraySegment<float> seg)
            {
                prvni_polovina = new ArraySegment<float>(seg.Array, seg.Offset, split_count);
                druha_polovina = new ArraySegment<float>(seg.Array, seg.Offset + split_count, list.Count - split_count);
            }
            else
            {
                throw new NotImplementedException();
            }

            // rekurzivní volání mergeSortu
            foreach (var op in StepSort(prvni_polovina))
            {
                yield return new SortOperation(op.indexA, op.indexB, op.type);
            }

            foreach (var op in StepSort(druha_polovina))
            {
                yield return new SortOperation(op.indexA + split_count, op.indexB + split_count, op.type);
            }

            // budeme kopírovat
            for (int i = 0; i < list.Count; i++)
            {
                yield return SortOperation.Copy(i);
            }

            // Array.ToArray() znamená kopie
            foreach (var op in Merge(prvni_polovina.ToArray(), druha_polovina.ToArray(), list))
            {
                if (op.type == SortOperation.SO_Type.Done) { continue; }

                yield return new SortOperation(op.indexA, op.indexB, op.type);
            }
        }
    }
}
