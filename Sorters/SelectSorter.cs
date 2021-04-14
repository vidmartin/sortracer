using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.Sorters
{
    public class SelectSorter : ISorter<float>
    {
        public IEnumerable<SortOperation> StepSort(IList<float> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int min_index = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    yield return SortOperation.Compare(j, min_index);
                    if (list[j] < list[min_index])
                    {
                        min_index = j;
                    }
                }

                yield return SortOperation.Swap(i, min_index);

                float temp = list[i];
                list[i] = list[min_index];
                list[min_index] = temp;

                yield return SortOperation.Update();
            }

        }
    }
}
