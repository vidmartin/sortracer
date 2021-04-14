using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer
{
    public class Sortable
    {
        private List<float> _list;

        public Sortable(IEnumerable<float> initializeWith)
        {
            _list = new List<float>(initializeWith);
        }

        public int CompareAB(int indexA, int indexB)
        {
            return Math.Sign(_list[indexA] - _list[indexB]);
        }

        public void SwapAB(int indexA, int indexB)
        {
            float remember = _list[indexA];
            _list[indexA] = _list[indexB];
            _list[indexB] = remember;
        }
    }
}
