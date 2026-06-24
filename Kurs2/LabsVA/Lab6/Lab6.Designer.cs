namespace VA6
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbOtkl = new System.Windows.Forms.TextBox();
            this.tbPolinomValue = new System.Windows.Forms.TextBox();
            this.tbFunctionValue = new System.Windows.Forms.TextBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.btnDrawLagrange = new System.Windows.Forms.Button();
            this.chartLagrange = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.nudPartititons = new System.Windows.Forms.NumericUpDown();
            this.nudPartitions = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvLagrange = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDraw = new System.Windows.Forms.Button();
            this.chartCubicSpline = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnCreateEmpty = new System.Windows.Forms.Button();
            this.nudCubicPartitions = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvCubicSpline = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLagrange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPartititons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPartitions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLagrange)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCubicSpline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCubicPartitions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCubicSpline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1085, 574);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.tbOtkl);
            this.tabPage1.Controls.Add(this.tbPolinomValue);
            this.tabPage1.Controls.Add(this.tbFunctionValue);
            this.tabPage1.Controls.Add(this.buttonCalculate);
            this.tabPage1.Controls.Add(this.btnDrawLagrange);
            this.tabPage1.Controls.Add(this.chartLagrange);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.nudPartititons);
            this.tabPage1.Controls.Add(this.nudPartitions);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dgvLagrange);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1077, 541);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(837, 418);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 25);
            this.label9.TabIndex = 15;
            this.label9.Text = "Отклонение";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(799, 354);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(219, 25);
            this.label8.TabIndex = 14;
            this.label8.Text = "Значение многочлена";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(809, 283);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "Значение функции";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(786, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Точка из интервала [2,5]";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(791, 245);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(227, 26);
            this.textBox1.TabIndex = 11;
            // 
            // tbOtkl
            // 
            this.tbOtkl.Location = new System.Drawing.Point(791, 446);
            this.tbOtkl.Name = "tbOtkl";
            this.tbOtkl.Size = new System.Drawing.Size(227, 26);
            this.tbOtkl.TabIndex = 10;
            // 
            // tbPolinomValue
            // 
            this.tbPolinomValue.Location = new System.Drawing.Point(791, 382);
            this.tbPolinomValue.Name = "tbPolinomValue";
            this.tbPolinomValue.Size = new System.Drawing.Size(227, 26);
            this.tbPolinomValue.TabIndex = 9;
            // 
            // tbFunctionValue
            // 
            this.tbFunctionValue.Location = new System.Drawing.Point(791, 311);
            this.tbFunctionValue.Name = "tbFunctionValue";
            this.tbFunctionValue.Size = new System.Drawing.Size(227, 26);
            this.tbFunctionValue.TabIndex = 8;
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(795, 500);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(223, 35);
            this.buttonCalculate.TabIndex = 7;
            this.buttonCalculate.Text = "Вычислить";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // btnDrawLagrange
            // 
            this.btnDrawLagrange.Location = new System.Drawing.Point(281, 500);
            this.btnDrawLagrange.Name = "btnDrawLagrange";
            this.btnDrawLagrange.Size = new System.Drawing.Size(223, 35);
            this.btnDrawLagrange.TabIndex = 6;
            this.btnDrawLagrange.Text = "Построить";
            this.btnDrawLagrange.UseVisualStyleBackColor = true;
            this.btnDrawLagrange.Click += new System.EventHandler(this.btnDrawLagrange_Click);
            // 
            // chartLagrange
            // 
            chartArea5.Name = "ChartArea1";
            this.chartLagrange.ChartAreas.Add(chartArea5);
            legend3.Name = "Legend1";
            this.chartLagrange.Legends.Add(legend3);
            this.chartLagrange.Location = new System.Drawing.Point(21, 210);
            this.chartLagrange.Name = "chartLagrange";
            this.chartLagrange.Size = new System.Drawing.Size(717, 279);
            this.chartLagrange.TabIndex = 5;
            this.chartLagrange.Text = "chart1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(16, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "Число разбиений";
            // 
            // nudPartititons
            // 
            this.nudPartititons.Location = new System.Drawing.Point(268, 167);
            this.nudPartititons.Name = "nudPartititons";
            this.nudPartititons.Size = new System.Drawing.Size(201, 26);
            this.nudPartititons.TabIndex = 3;
            this.nudPartititons.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nudPartitions
            // 
            this.nudPartitions.Location = new System.Drawing.Point(268, 130);
            this.nudPartitions.Name = "nudPartitions";
            this.nudPartitions.Size = new System.Drawing.Size(201, 26);
            this.nudPartitions.TabIndex = 2;
            this.nudPartitions.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(16, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 29);
            this.label4.TabIndex = 1;
            this.label4.Text = "Число точек";
            // 
            // dgvLagrange
            // 
            this.dgvLagrange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLagrange.Location = new System.Drawing.Point(16, 6);
            this.dgvLagrange.Name = "dgvLagrange";
            this.dgvLagrange.RowHeadersWidth = 62;
            this.dgvLagrange.RowTemplate.Height = 28;
            this.dgvLagrange.Size = new System.Drawing.Size(1055, 118);
            this.dgvLagrange.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.nudCount);
            this.tabPage2.Controls.Add(this.btnDraw);
            this.tabPage2.Controls.Add(this.chartCubicSpline);
            this.tabPage2.Controls.Add(this.btnCreateEmpty);
            this.tabPage2.Controls.Add(this.nudCubicPartitions);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.dgvCubicSpline);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1077, 541);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(435, 499);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(249, 36);
            this.btnDraw.TabIndex = 11;
            this.btnDraw.Text = "Построить";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // chartCubicSpline
            // 
            chartArea6.Name = "ChartArea1";
            this.chartCubicSpline.ChartAreas.Add(chartArea6);
            this.chartCubicSpline.Location = new System.Drawing.Point(82, 225);
            this.chartCubicSpline.Name = "chartCubicSpline";
            this.chartCubicSpline.Size = new System.Drawing.Size(924, 268);
            this.chartCubicSpline.TabIndex = 10;
            this.chartCubicSpline.Text = "chart1";
            // 
            // btnCreateEmpty
            // 
            this.btnCreateEmpty.Location = new System.Drawing.Point(421, 183);
            this.btnCreateEmpty.Name = "btnCreateEmpty";
            this.btnCreateEmpty.Size = new System.Drawing.Size(241, 36);
            this.btnCreateEmpty.TabIndex = 9;
            this.btnCreateEmpty.Text = "Создать пустую таблицу";
            this.btnCreateEmpty.UseVisualStyleBackColor = true;
            this.btnCreateEmpty.Click += new System.EventHandler(this.btnCreateEmpty_Click);
            // 
            // nudCubicPartitions
            // 
            this.nudCubicPartitions.Location = new System.Drawing.Point(852, 151);
            this.nudCubicPartitions.Name = "nudCubicPartitions";
            this.nudCubicPartitions.Size = new System.Drawing.Size(201, 26);
            this.nudCubicPartitions.TabIndex = 7;
            this.nudCubicPartitions.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(597, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(235, 29);
            this.label10.TabIndex = 8;
            this.label10.Text = "Число разбиений";
            // 
            // dgvCubicSpline
            // 
            this.dgvCubicSpline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCubicSpline.Location = new System.Drawing.Point(17, 6);
            this.dgvCubicSpline.Name = "dgvCubicSpline";
            this.dgvCubicSpline.RowHeadersWidth = 62;
            this.dgvCubicSpline.RowTemplate.Height = 28;
            this.dgvCubicSpline.Size = new System.Drawing.Size(1036, 137);
            this.dgvCubicSpline.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(959, 659);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Выйти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 659);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(138, 35);
            this.button2.TabIndex = 2;
            this.button2.Text = "Назад";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(474, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "y(x)=e";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(587, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "-(x+sinx)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(728, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 37);
            this.label3.TabIndex = 4;
            this.label3.Text = "[2,5]";
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(213, 149);
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(201, 26);
            this.nudCount.TabIndex = 12;
            this.nudCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(20, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 29);
            this.label11.TabIndex = 13;
            this.label11.Text = "Число точек";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 725);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Lab6";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLagrange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPartititons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPartitions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLagrange)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCubicSpline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCubicPartitions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCubicSpline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvLagrange;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudPartititons;
        private System.Windows.Forms.NumericUpDown nudPartitions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLagrange;
        private System.Windows.Forms.Button btnDrawLagrange;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbOtkl;
        private System.Windows.Forms.TextBox tbPolinomValue;
        private System.Windows.Forms.TextBox tbFunctionValue;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvCubicSpline;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudCubicPartitions;
        private System.Windows.Forms.Button btnCreateEmpty;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCubicSpline;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Label label11;
    }
}