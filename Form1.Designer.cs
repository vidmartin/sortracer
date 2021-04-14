namespace SortRacer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_sort = new System.Windows.Forms.Button();
            this.btn_shuffle = new System.Windows.Forms.Button();
            this.comboBox_sorts = new System.Windows.Forms.ComboBox();
            this.slider_speed = new System.Windows.Forms.TrackBar();
            this.numericUpDown_columnCount = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.groupbox_columnCount = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_draw = new SortRacer.DblBuffPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtn_manual = new System.Windows.Forms.RadioButton();
            this.rbtn_automat = new System.Windows.Forms.RadioButton();
            this.btn_step = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.slider_speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_columnCount)).BeginInit();
            this.groupbox_columnCount.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_sort
            // 
            this.btn_sort.Location = new System.Drawing.Point(3, 33);
            this.btn_sort.Name = "btn_sort";
            this.btn_sort.Size = new System.Drawing.Size(238, 30);
            this.btn_sort.TabIndex = 0;
            this.btn_sort.Text = "Start";
            this.btn_sort.UseVisualStyleBackColor = true;
            // 
            // btn_shuffle
            // 
            this.btn_shuffle.Location = new System.Drawing.Point(3, 69);
            this.btn_shuffle.Name = "btn_shuffle";
            this.btn_shuffle.Size = new System.Drawing.Size(238, 30);
            this.btn_shuffle.TabIndex = 1;
            this.btn_shuffle.Text = "Zamíchat";
            this.btn_shuffle.UseVisualStyleBackColor = true;
            // 
            // comboBox_sorts
            // 
            this.comboBox_sorts.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.comboBox_sorts.DisplayMember = "Item2";
            this.comboBox_sorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sorts.FormattingEnabled = true;
            this.comboBox_sorts.Location = new System.Drawing.Point(3, 3);
            this.comboBox_sorts.Name = "comboBox_sorts";
            this.comboBox_sorts.Size = new System.Drawing.Size(238, 24);
            this.comboBox_sorts.TabIndex = 4;
            // 
            // slider_speed
            // 
            this.slider_speed.Location = new System.Drawing.Point(7, 22);
            this.slider_speed.Name = "slider_speed";
            this.slider_speed.Size = new System.Drawing.Size(225, 56);
            this.slider_speed.TabIndex = 5;
            this.slider_speed.Value = 5;
            this.slider_speed.ValueChanged += new System.EventHandler(this.slider_speed_ValueChanged);
            // 
            // numericUpDown_columnCount
            // 
            this.numericUpDown_columnCount.Location = new System.Drawing.Point(6, 25);
            this.numericUpDown_columnCount.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_columnCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown_columnCount.Name = "numericUpDown_columnCount";
            this.numericUpDown_columnCount.Size = new System.Drawing.Size(101, 22);
            this.numericUpDown_columnCount.TabIndex = 6;
            this.numericUpDown_columnCount.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 25);
            this.button1.TabIndex = 7;
            this.button1.Text = "Nastavit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupbox_columnCount
            // 
            this.groupbox_columnCount.Controls.Add(this.button1);
            this.groupbox_columnCount.Controls.Add(this.numericUpDown_columnCount);
            this.groupbox_columnCount.Location = new System.Drawing.Point(3, 14);
            this.groupbox_columnCount.Name = "groupbox_columnCount";
            this.groupbox_columnCount.Size = new System.Drawing.Size(238, 64);
            this.groupbox_columnCount.TabIndex = 8;
            this.groupbox_columnCount.TabStop = false;
            this.groupbox_columnCount.Text = "Počet sloupců";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.slider_speed);
            this.groupBox1.Location = new System.Drawing.Point(3, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 89);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rychlost";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.panel_draw, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(882, 453);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel_draw
            // 
            this.panel_draw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel_draw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_draw.Location = new System.Drawing.Point(3, 3);
            this.panel_draw.Name = "panel_draw";
            this.panel_draw.Size = new System.Drawing.Size(626, 447);
            this.panel_draw.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(635, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 447);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.groupbox_columnCount);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 366);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(244, 81);
            this.flowLayoutPanel2.TabIndex = 12;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.comboBox_sorts);
            this.flowLayoutPanel1.Controls.Add(this.btn_sort);
            this.flowLayoutPanel1.Controls.Add(this.btn_shuffle);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(244, 299);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtn_manual);
            this.groupBox2.Controls.Add(this.rbtn_automat);
            this.groupBox2.Controls.Add(this.btn_step);
            this.groupBox2.Location = new System.Drawing.Point(3, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 87);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ovládání";
            // 
            // rbtn_manual
            // 
            this.rbtn_manual.AutoSize = true;
            this.rbtn_manual.Location = new System.Drawing.Point(136, 21);
            this.rbtn_manual.Name = "rbtn_manual";
            this.rbtn_manual.Size = new System.Drawing.Size(75, 21);
            this.rbtn_manual.TabIndex = 15;
            this.rbtn_manual.Text = "Manuál";
            this.rbtn_manual.UseVisualStyleBackColor = true;
            // 
            // rbtn_automat
            // 
            this.rbtn_automat.AutoSize = true;
            this.rbtn_automat.Checked = true;
            this.rbtn_automat.Location = new System.Drawing.Point(7, 21);
            this.rbtn_automat.Name = "rbtn_automat";
            this.rbtn_automat.Size = new System.Drawing.Size(81, 21);
            this.rbtn_automat.TabIndex = 14;
            this.rbtn_automat.TabStop = true;
            this.rbtn_automat.Text = "Automat";
            this.rbtn_automat.UseVisualStyleBackColor = true;
            this.rbtn_automat.CheckedChanged += new System.EventHandler(this.rbtn_automat_CheckedChanged);
            // 
            // btn_step
            // 
            this.btn_step.Enabled = false;
            this.btn_step.Location = new System.Drawing.Point(7, 48);
            this.btn_step.Name = "btn_step";
            this.btn_step.Size = new System.Drawing.Size(225, 30);
            this.btn_step.TabIndex = 13;
            this.btn_step.Text = "Krok";
            this.btn_step.UseVisualStyleBackColor = true;
            this.btn_step.Click += new System.EventHandler(this.btn_step_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 453);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "SortRacer";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.slider_speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_columnCount)).EndInit();
            this.groupbox_columnCount.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_sort;
        private System.Windows.Forms.Button btn_shuffle;
        private DblBuffPanel panel_draw;
        private System.Windows.Forms.ComboBox comboBox_sorts;
        private System.Windows.Forms.TrackBar slider_speed;
        private System.Windows.Forms.NumericUpDown numericUpDown_columnCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupbox_columnCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btn_step;
        private System.Windows.Forms.RadioButton rbtn_automat;
        private System.Windows.Forms.RadioButton rbtn_manual;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

