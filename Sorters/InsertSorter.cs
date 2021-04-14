using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.Sorters
{
    public class InsertSorter : ISorter<float>
    {
        public IEnumerable<SortOperation> StepSort(IList<float> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int target_index = i;
                while (target_index > 0)
                {
                    yield return SortOperation.Compare(target_index - 1, i);
                    if (list[target_index - 1] < list[i]) { break; }
                    target_index -= 1;
                }

                yield return SortOperation.Copy(i);
                var temp = list[i];

                for (int j = i; j > target_index; j--)
                {
                    yield return SortOperation.CopyFromTo(j - 1, j);
                    list[j] = list[j - 1];
                    yield return SortOperation.Update();
                }

                yield return SortOperation.Set(target_index);
                list[target_index] = temp;
                yield return SortOperation.Update();

            }
        }
    }
}
