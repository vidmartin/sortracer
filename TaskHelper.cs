using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortRacer
{
    public static class TaskHelper
    {
        private static readonly Stopwatch _stopwatch = new Stopwatch();

        static TaskHelper()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// tato metoda by měla umožňovat přesnější delay nežli Task.Delay
        /// </summary>
        public static async Task Delay(float ms, CancellationToken token = default)
        {
            var calltime = _stopwatch.ElapsedTicks;

            await Task.Run(() =>
            {
                while ((float)(_stopwatch.ElapsedTicks - calltime) / Stopwatch.Frequency * 1000 < ms)
                {
                    token.ThrowIfCancellationRequested();
                }
            });
        }
    }
}
