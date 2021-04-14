using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer
{
    public static class Shuffler
    {
        private static readonly Random RND = new Random();

        public static void Shuffle<T>(IList<T> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int indexToSwap = RND.Next(i, list.Count);
                T swapWith = list[indexToSwap];
                list[indexToSwap] = list[i];
                list[i] = swapWith;
            }
        }
    }
}
