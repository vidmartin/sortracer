using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortRacer
{
    public class DblBuffPanel : Panel
    {
        public DblBuffPanel() : base()
        {
            this.DoubleBuffered = true;
        }
    }
}
