using SortRacer.Sorters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortRacer
{
    public partial class Form1 : Form
    {
        private readonly SortVisualizer _sortVisualiser;
        private IList<float> _collection;

        private readonly KeyValuePair<ISorter<float>, string>[] _sortersWithNames =
        {
            new KeyValuePair<ISorter<float>, string>(new SelectSorter(), "Select sort"),
            new KeyValuePair<ISorter<float>, string>(new InsertSorter(), "Insert sort"),
            new KeyValuePair<ISorter<float>, string>(new BubbleSorter(), "Bubble sort"),
            new KeyValuePair<ISorter<float>, string>(new TopDownMergeSorter(), "Merge sort"),
            new KeyValuePair<ISorter<float>, string>(new HeapSorter(), "Heap sort"),
            new KeyValuePair<ISorter<float>, string>(new QuickSorter(), "Quicksort")
        };

        public Form1()
        {
            InitializeComponent();

            InitializeSortsComboBox();

            btn_sort.Click += Btn_sort_Click;
            btn_shuffle.Click += Btn_shuffle_Click;
            
            _sortVisualiser = new SortVisualizer(panel_draw);

            InitColumns(20);

            panel_draw.Refresh();
        }

        private void InitColumns(int count)
        {
            _collection = Enumerable.Range(1, count).Select(i => (float)i).ToArray();
            _sortVisualiser.CurrentCollection = _collection;
        }

        private void InitializeSortsComboBox()
        {
            comboBox_sorts.DataSource = _sortersWithNames;
            comboBox_sorts.DisplayMember = "Value";
            comboBox_sorts.ValueMember = "Key";
        }

        private bool _isSorting = false;
        public bool IsSorting
        {
            get => _isSorting;
            set
            {
                _isSorting = value;

                btn_shuffle.Enabled = !_isSorting;
                groupbox_columnCount.Enabled = !_isSorting;
                slider_speed.Enabled = !_isSorting;

                btn_sort.Text = _isSorting ? "Stop" : "Start";

                if (!_isSorting) { btn_sort.Enabled = true; }
            }
        }

        private float GetConfiguredDelayMultiplier(float min, float max)
        {
            float value = (float)slider_speed.Value / slider_speed.Maximum;

            return max + (min - max) * value; // čím vyšší nastavená hodnota, tím vyšší chceme rychlost => tím nižší delay
        }

        private CancellationTokenSource _sortCancelTokenSource = null;

        private async void Btn_sort_Click(object sender, EventArgs e)
        {
            if (IsSorting)
            {
                // pokud jsme na tlačítko klikli zatímco řadíme, bude fungovat jako stop
                btn_sort.Enabled = false;
                _sortCancelTokenSource.Cancel();
                return;
            }

            // jinak funguje jako start
            IsSorting = true;

            try
            {
                var result = await _sortVisualiser.SortAsync(
                    (ISorter<float>)comboBox_sorts.SelectedValue,
                    GetConfiguredDelayMultiplier(0.05f, 2f),
                    null,
                    (_sortCancelTokenSource = new CancellationTokenSource()).Token);

                MessageBox.Show($"Porovnání: {result.Comparasions}\nProhození: {result.Swaps}\nPřesouvání: {result.OutOfPlaceCopies}");
            }
            catch (OperationCanceledException) { }           

            IsSorting = false;
        }

        private void Btn_shuffle_Click(object sender, EventArgs e)
        {
            Shuffler.Shuffle(_collection);
            panel_draw.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitColumns((int)numericUpDown_columnCount.Value);
            panel_draw.Refresh();
        }

        private void slider_speed_ValueChanged(object sender, EventArgs e)
        {            
            //if (slider_speed.Value == slider_speed.Minimum)
            //{
            //    // pokud je nastavena minimální rychlost, zapauzovat
            //    if (!_sortVisualiser.IsPaused) { _sortVisualiser.Pause(); }
            //}
            //else
            //{
            //    if (_sortVisualiser.IsPaused) { _sortVisualiser.Resume(); }
            //}
        }

        private void btn_step_Click(object sender, EventArgs e)
        {
            _sortVisualiser.Step();
        }

        private void rbtn_automat_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_automat.Checked)
            {
                if (_sortVisualiser.IsPaused)
                {
                    _sortVisualiser.Resume();
                    btn_step.Enabled = false;
                }
            }
            else
            {
                if (!_sortVisualiser.IsPaused)
                {
                    _sortVisualiser.Pause();
                    btn_step.Enabled = true;
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel_draw.Refresh();
        }
    }
}
