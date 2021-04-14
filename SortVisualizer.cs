using SortRacer.Sorters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortRacer
{
    public class SortVisualizer
    {
        public Control Target { get; }

        public IList<float> CurrentCollection { get; set; } = null;
        public IList<float> CurrentPivots { get; } = new List<float>();
                

        public SortOperation LastOperation { get; private set; } = SortOperation.Update();
        private DateTime _lastUpdate;
        public double MsSinceLastUpdate => (DateTime.Now - _lastUpdate).TotalMilliseconds;

        //public Color BgColor { get; set; } = Color.DarkGray;
        public Color ColumnColor { get; set; } = Color.White;
        public Color CompareColor { get; set; } = Color.Cyan;
        public Color SwapColor { get; set; } = Color.Blue;
        public Color SetColor { get; set; } = Color.Magenta;
        public Color DoneColor { get; set; } = Color.Lime;
        public Color CopyColor { get; set; } = Color.Orange;
        public Pen PivotPen { get; set; } = new Pen(new SolidBrush(Color.FromArgb(128, Color.Black)), 1);
        public float ColumnWidthPercent { get; set; } = 0.9f;
        public float Padding { get; set; } = 10;
        public bool Sorting { get; private set; } = false;
        //public int FPS { get; set; } = 10;

        private Color _doneColor;

        public SortVisualizer(Control target)
        {
            Target = target;
            Target.Paint += Target_Paint;
        }

        private void Target_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentCollection == null)
                return;

            Dictionary<int, Color> columnRecolors = new Dictionary<int, Color>();
            switch (LastOperation.type)
            {
                case SortOperation.SO_Type.Compare:
                    columnRecolors[LastOperation.indexA] = CompareColor;
                    columnRecolors[LastOperation.indexB] = CompareColor;
                    break;
                case SortOperation.SO_Type.Swap:
                    columnRecolors[LastOperation.indexA] = SwapColor;
                    columnRecolors[LastOperation.indexB] = SwapColor;
                    break;
                case SortOperation.SO_Type.Set:
                    columnRecolors[LastOperation.indexA] = SetColor;
                    break;
                case SortOperation.SO_Type.Copy:
                    columnRecolors[LastOperation.indexB] = SetColor;
                    columnRecolors[LastOperation.indexA] = CopyColor;
                    break;
                case SortOperation.SO_Type.Done:
                    for (int ii = 0; ii < CurrentCollection.Count; ii++)
                    {
                        columnRecolors[ii] = _doneColor;
                    }
                    break;
            }

            float availableWidth = (float)e.ClipRectangle.Width - Padding * 2;
            float availableHeight = (float)e.ClipRectangle.Height - Padding * 2;

            float colSpace = availableWidth / CurrentCollection.Count;
            float colWidth = colSpace * ColumnWidthPercent;
            float colOffset = (colSpace - colWidth) / 2;

            float max = CurrentCollection.Max();

            int i = 0; 
            foreach (float d in CurrentCollection)
            {
                Color color = columnRecolors.ContainsKey(i) ? columnRecolors[i] : ColumnColor;
                Brush brush = new SolidBrush(color);

                float h = d / max * availableHeight;
                float w = colWidth;
                float x = Padding + i * colSpace + colOffset;
                float y = availableHeight - h + Padding;

                e.Graphics.FillRectangle(brush, x, y, w, h);

                i++;
            }

            foreach (float v in CurrentPivots)
            {
                float y = availableHeight * (1 - v / max) + Padding;

                e.Graphics.DrawLine(PivotPen, 0, y, e.ClipRectangle.Width, y);
            }
        }

        private float GetOperationDelay(SortOperation.SO_Type type)
        {
            switch (type)
            {
                case SortOperation.SO_Type.Compare:
                    return 150;
                case SortOperation.SO_Type.Swap:
                    return 200;
                case SortOperation.SO_Type.Set:
                    return 200;
                case SortOperation.SO_Type.Copy:
                    return 200;
                case SortOperation.SO_Type.Update:
                    return 50;
                case SortOperation.SO_Type.Pivot:
                    return 0;
                case SortOperation.SO_Type.HiddenCompare:
                    return -1;
                case SortOperation.SO_Type.Done:
                default:                
                    throw new NotImplementedException();
            }
        }

        private async Task AnimateDone()
        {
            for (int i = 0; i < 6; i++)
            {
                _doneColor = i % 2 == 0 ? DoneColor : ColumnColor;
                Target.Invoke((Action)Target.Refresh);
                await Task.Delay(150);
            }
        }

        public bool IsPaused { get; private set; } = false;

        public void Pause()
        {
            if (IsPaused) { throw new InvalidOperationException(); }

            IsPaused = true;
        }

        public void Step(Exception ex = null)
        {
            if (_stepCompletionSource == null || _stepCompletionSource.Task.IsCompleted)
            {
                return;
            }

            _stepCompletionSource.Task.ContinueWith(task =>
            {
                _stepCompletionSource = 
                    new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
            });

            if (ex == null)
            {
                _stepCompletionSource.SetResult(null);
            }
            else
            {
                _stepCompletionSource.SetException(ex);
            }
        }

        private TaskCompletionSource<object> _stepCompletionSource
            = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

        public void Resume()
        {
            if (!IsPaused) { throw new InvalidOperationException(); }

            IsPaused = false;
            Step();            
        }

        private async Task AwaitStep(CancellationToken token)
        {
            await _stepCompletionSource.Task;      
        }

        private async Task AnimateAsync(SortOperation operation, CancellationToken cancellationToken, float stepDelayMultiplier, bool dontWait = false)
        {
            cancellationToken.Register(() => Step(new TaskCanceledException()));

            if (operation.type == SortOperation.SO_Type.Done)
            {
                LastOperation = operation;
                await AnimateDone();
                return;
            }

            if (operation.type == SortOperation.SO_Type.Pivot)
            {
                CurrentPivots.Add(operation.value);
            }

            LastOperation = operation;
            
            Target.Refresh();

            if (dontWait || GetOperationDelay(operation.type) < 0) { return; }

            if (IsPaused)
            {
                // jsme-li zapauzovaní, umožnit uživateli manuálně krokovat
                await AwaitStep(cancellationToken);
            }
            else
            {
                float delay = GetOperationDelay(operation.type) * stepDelayMultiplier;
                await TaskHelper.Delay(delay, cancellationToken);
            }
        }

        public async Task<SortResult> SortAsync(ISorter<float> sorter, float stepDelayMultiplier, Action doneCallback, CancellationToken cancellationToken = default)
        {
            if (Sorting)
                throw new InvalidOperationException("Už sortuju kámo");

            Sorting = true;

            var result = new SortResult();

            try
            {
                await SortAsyncInner(result, sorter, stepDelayMultiplier, doneCallback, cancellationToken);
            }
            catch (OperationCanceledException)
            {
            }

            await AnimateAsync(
                SortOperation.Update(),
                default,
                stepDelayMultiplier,
                true); // finální update, bez možnosti kancelace

            Sorting = false;

            return result;
        }

        private async Task SortAsyncInner(SortResult result, ISorter<float> sorter, float stepDelayMultiplier, Action doneCallback, CancellationToken cancellationToken = default)
        {
            CurrentPivots.Clear();            

            await AnimateAsync(SortOperation.Update(), cancellationToken, stepDelayMultiplier);
            
            foreach (var op in sorter.StepSort(CurrentCollection))
            {
                switch (op.type)
                {
                    case SortOperation.SO_Type.Compare:
                    case SortOperation.SO_Type.HiddenCompare:
                        result.Comparasions += 1;
                        break;

                    case SortOperation.SO_Type.Copy:
                    case SortOperation.SO_Type.Set:
                        result.OutOfPlaceCopies += 1;
                        break;

                    case SortOperation.SO_Type.Swap:
                        result.Swaps += 1;
                        break;                        
                }

                await AnimateAsync(op, cancellationToken, stepDelayMultiplier);
            }

            doneCallback?.Invoke();

            CurrentPivots.Clear();
            await AnimateAsync(SortOperation.Done(), cancellationToken, stepDelayMultiplier);            
        }
    }
}
