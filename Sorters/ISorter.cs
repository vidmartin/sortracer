using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.Sorters
{
    public interface ISorter<T>
    {
        IEnumerable<SortOperation> StepSort(IList<T> list);
    }
}
