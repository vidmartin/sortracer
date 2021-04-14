using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortRacer.Sorters
{
    public class HeapSorter : ISorter<float>
    {
        private int L(int i) => i * 2 + 1; //levé děcko
        private int R(int i) => i * 2 + 2; //pravé děcko
        private int P(int i) => (i - 1) / 2; //rodič

        /// <summary>
        /// Spravit subtree začínající na indexu index
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private IEnumerable<SortOperation> Heapify(IList<float> list, int index)
        {
            int r = R(index);
            int l = L(index);

            if (l >= list.Count) { yield break; } // pokud nemáme děcka, konec

            yield return SortOperation.Compare(l, index);
            if (r != list.Count) { yield return SortOperation.Compare(l, r); }

            // pokud je levé děcko větší než rodič a (pravé děcko buďto není nebo je menší nebo rovno levému děcku)
            if (list[l] > list[index] && (r == list.Count || list[l] >= list[r])) // max heap - swap - levé děcko
            {
                yield return SortOperation.Swap(l, index);

                // prohodit levé děcko a rodiče (aby se udržela heap property)
                (list[l], list[index]) = (list[index], list[l]);

                yield return SortOperation.Update();
                
                // rekurzivně zavolat toto levém děcku
                foreach (var op in Heapify(list, l)) { yield return op; }

                yield break;
            }

            yield return SortOperation.Compare(r, index);

            // je-li pravé děcko a je-li větší než rodič
            if (r < list.Count && list[r] > list[index]) // max heap - swap - pravé děcko
            {
                yield return SortOperation.Swap(l, index);

                // prohodit pravě děcko a rodiče
                (list[r], list[index]) = (list[index], list[r]);

                yield return SortOperation.Update();

                // rekurzivně zavolat toto na pravém děcku
                foreach (var op in Heapify(list, r)) { yield return op; }
            }
        }

        public IEnumerable<SortOperation> StepSort(IList<float> list)
        {
            if (!(list is float[] arr)) { throw new ArgumentException(); }

            // až z pole uděláme haldu, kolik prvků bude mít děti?
            //  => počet prvků ve všech úrovních kromě poslední
            //      ( v každé úrovni je dvakrát tolik prvků kolik v předchozí )
            int how_many_elements_with_children
                = Enumerable.Repeat(2, (int)Math.Log(arr.Length, 2)).Aggregate((a, b) => a * b);

            // zpřeházet pole tak, aby z něj byla binární halda
            for (int i = how_many_elements_with_children - 1; i >= 0; i--)
            {
                foreach (var op in Heapify(arr, i))
                {
                    yield return op;
                }
            }

            // postupně z haldy vytahovat největší prvky a přesouvat je na konec pole
            for (int i = list.Count - 1; i >= 0; i--)
            {
                // prohodit aktuální prvek s prvním prvkem (první prvek je určitě maximum)
                yield return SortOperation.Swap(i, 0);
                (list[i], list[0]) = (list[0], list[i]); 
                yield return SortOperation.Update();

                // zařídit, aby se zachovala heap property
                foreach (var op in Heapify(new ArraySegment<float>(arr, 0, i), 0))
                {
                    yield return op;
                }
            }
        }
    }
}
