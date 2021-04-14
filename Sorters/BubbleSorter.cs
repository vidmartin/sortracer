using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.Sorters
{
    public class BubbleSorter : ISorter<float>
    {
        public IEnumerable<SortOperation> StepSort(IList<float> list)
        {
            bool swappedAny = true;
            while (swappedAny)
            {
                swappedAny = false;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    yield return SortOperation.Compare(i, i + 1);
                    float a = list[i];
                    float b = list[i + 1];
                    if (a > b)
                    {
                        yield return SortOperation.Swap(i, i + 1);
                        list[i] = b;
                        list[i + 1] = a;
                        swappedAny = true;
                        yield return SortOperation.Update();
                    }
                }
            }
        }
    }
}
