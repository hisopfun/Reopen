namespace 覆盤
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.plotSurface2D1 = new NPlot.Windows.PlotSurface2D();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.plotSurface2D2 = new NPlot.Windows.PlotSurface2D();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Buy = new System.Windows.Forms.Button();
            this.btn_Sell = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.plotSurface2D5 = new NPlot.Windows.PlotSurface2D();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.plotSurface2D4 = new NPlot.Windows.PlotSurface2D();
            this.plotSurface2D3 = new NPlot.Windows.PlotSurface2D();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.stopLimitControl1 = new 覆盤.StopLimitControl();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(145, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "price";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(145, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "time";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Speed";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(275, 40);
            this.button1.TabIndex = 8;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "10"});
            this.comboBox1.Location = new System.Drawing.Point(145, 95);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 30);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.Text = "10";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // plotSurface2D1
            // 
            this.plotSurface2D1.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D1.AutoScaleTitle = false;
            this.plotSurface2D1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotSurface2D1.DateTimeToolTip = false;
            this.plotSurface2D1.Legend = null;
            this.plotSurface2D1.LegendZOrder = -1;
            this.plotSurface2D1.Location = new System.Drawing.Point(6, 40);
            this.plotSurface2D1.Name = "plotSurface2D1";
            this.plotSurface2D1.RightMenu = null;
            this.plotSurface2D1.ShowCoordinates = true;
            this.plotSurface2D1.Size = new System.Drawing.Size(1084, 522);
            this.plotSurface2D1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D1.TabIndex = 79;
            this.plotSurface2D1.Text = "plotSurface2D1";
            this.plotSurface2D1.Title = "";
            this.plotSurface2D1.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D1.XAxis1 = null;
            this.plotSurface2D1.XAxis2 = null;
            this.plotSurface2D1.YAxis1 = null;
            this.plotSurface2D1.YAxis2 = null;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 58);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(275, 30);
            this.dateTimePicker1.TabIndex = 80;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // plotSurface2D2
            // 
            this.plotSurface2D2.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D2.AutoScaleTitle = false;
            this.plotSurface2D2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotSurface2D2.DateTimeToolTip = false;
            this.plotSurface2D2.Legend = null;
            this.plotSurface2D2.LegendZOrder = -1;
            this.plotSurface2D2.Location = new System.Drawing.Point(6, 568);
            this.plotSurface2D2.Name = "plotSurface2D2";
            this.plotSurface2D2.RightMenu = null;
            this.plotSurface2D2.ShowCoordinates = true;
            this.plotSurface2D2.Size = new System.Drawing.Size(1084, 179);
            this.plotSurface2D2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D2.TabIndex = 81;
            this.plotSurface2D2.Text = "plotSurface2D2";
            this.plotSurface2D2.Title = "";
            this.plotSurface2D2.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D2.XAxis1 = null;
            this.plotSurface2D2.XAxis2 = null;
            this.plotSurface2D2.YAxis1 = null;
            this.plotSurface2D2.YAxis2 = null;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 25);
            this.label8.TabIndex = 84;
            this.label8.Text = "Price";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 25);
            this.label7.TabIndex = 86;
            this.label7.Text = "time";
            // 
            // btn_Buy
            // 
            this.btn_Buy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_Buy.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Buy.Location = new System.Drawing.Point(12, 333);
            this.btn_Buy.Name = "btn_Buy";
            this.btn_Buy.Size = new System.Drawing.Size(75, 25);
            this.btn_Buy.TabIndex = 87;
            this.btn_Buy.Text = "B";
            this.btn_Buy.UseVisualStyleBackColor = false;
            this.btn_Buy.Click += new System.EventHandler(this.btn_Buy_Click);
            // 
            // btn_Sell
            // 
            this.btn_Sell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_Sell.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sell.Location = new System.Drawing.Point(209, 333);
            this.btn_Sell.Name = "btn_Sell";
            this.btn_Sell.Size = new System.Drawing.Size(75, 25);
            this.btn_Sell.TabIndex = 88;
            this.btn_Sell.Text = "S";
            this.btn_Sell.UseVisualStyleBackColor = false;
            this.btn_Sell.Click += new System.EventHandler(this.btn_Sell_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(145, 193);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 25);
            this.label12.TabIndex = 98;
            this.label12.Text = "5AVG ";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(145, 227);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 25);
            this.label13.TabIndex = 99;
            this.label13.Text = "HLDiff";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.plotSurface2D1);
            this.groupBox1.Controls.Add(this.plotSurface2D2);
            this.groupBox1.Controls.Add(this.plotSurface2D5);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(293, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1096, 924);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chart";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(70, 16);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(81, 23);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Candle";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(6, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(58, 22);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Line";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // plotSurface2D5
            // 
            this.plotSurface2D5.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D5.AutoScaleTitle = false;
            this.plotSurface2D5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotSurface2D5.DateTimeToolTip = false;
            this.plotSurface2D5.Legend = null;
            this.plotSurface2D5.LegendZOrder = -1;
            this.plotSurface2D5.Location = new System.Drawing.Point(6, 753);
            this.plotSurface2D5.Name = "plotSurface2D5";
            this.plotSurface2D5.RightMenu = null;
            this.plotSurface2D5.ShowCoordinates = true;
            this.plotSurface2D5.Size = new System.Drawing.Size(1084, 164);
            this.plotSurface2D5.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D5.TabIndex = 103;
            this.plotSurface2D5.Text = "plotSurface2D5";
            this.plotSurface2D5.Title = "";
            this.plotSurface2D5.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D5.XAxis1 = null;
            this.plotSurface2D5.XAxis2 = null;
            this.plotSurface2D5.YAxis1 = null;
            this.plotSurface2D5.YAxis2 = null;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 771);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(275, 164);
            this.textBox1.TabIndex = 101;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 193);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(127, 25);
            this.label14.TabIndex = 104;
            this.label14.Text = "5AVG";
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 228);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(127, 25);
            this.label15.TabIndex = 105;
            this.label15.Text = "H - L";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(269, 338);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "成交明細";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 22);
            this.label2.TabIndex = 97;
            this.label2.Text = "Qty";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(96, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 22);
            this.label3.TabIndex = 96;
            this.label3.Text = "0";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(96, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 22);
            this.label9.TabIndex = 98;
            this.label9.Text = "0";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 22);
            this.label6.TabIndex = 99;
            this.label6.Text = "Profit";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 22);
            this.label10.TabIndex = 101;
            this.label10.Text = "Entries";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(96, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 22);
            this.label11.TabIndex = 100;
            this.label11.Text = "0";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(266, 182);
            this.dataGridView1.TabIndex = 89;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.plotSurface2D4);
            this.tabPage1.Controls.Add(this.plotSurface2D3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(269, 338);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "日K";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // plotSurface2D4
            // 
            this.plotSurface2D4.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D4.AutoScaleTitle = false;
            this.plotSurface2D4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.plotSurface2D4.DateTimeToolTip = false;
            this.plotSurface2D4.Legend = null;
            this.plotSurface2D4.LegendZOrder = -1;
            this.plotSurface2D4.Location = new System.Drawing.Point(3, 189);
            this.plotSurface2D4.Name = "plotSurface2D4";
            this.plotSurface2D4.RightMenu = null;
            this.plotSurface2D4.ShowCoordinates = true;
            this.plotSurface2D4.Size = new System.Drawing.Size(263, 149);
            this.plotSurface2D4.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D4.TabIndex = 97;
            this.plotSurface2D4.Text = "plotSurface2D4";
            this.plotSurface2D4.Title = "";
            this.plotSurface2D4.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D4.XAxis1 = null;
            this.plotSurface2D4.XAxis2 = null;
            this.plotSurface2D4.YAxis1 = null;
            this.plotSurface2D4.YAxis2 = null;
            // 
            // plotSurface2D3
            // 
            this.plotSurface2D3.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D3.AutoScaleTitle = false;
            this.plotSurface2D3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.plotSurface2D3.DateTimeToolTip = false;
            this.plotSurface2D3.Legend = null;
            this.plotSurface2D3.LegendZOrder = -1;
            this.plotSurface2D3.Location = new System.Drawing.Point(3, 3);
            this.plotSurface2D3.Name = "plotSurface2D3";
            this.plotSurface2D3.RightMenu = null;
            this.plotSurface2D3.ShowCoordinates = true;
            this.plotSurface2D3.Size = new System.Drawing.Size(263, 177);
            this.plotSurface2D3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D3.TabIndex = 96;
            this.plotSurface2D3.Text = "plotSurface2D3";
            this.plotSurface2D3.Title = "";
            this.plotSurface2D3.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D3.XAxis1 = null;
            this.plotSurface2D3.XAxis2 = null;
            this.plotSurface2D3.YAxis1 = null;
            this.plotSurface2D3.YAxis2 = null;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 402);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(277, 364);
            this.tabControl1.TabIndex = 97;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(112, 269);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(75, 27);
            this.textBox2.TabIndex = 106;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(12, 269);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 107;
            this.button2.Text = "MIT";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(209, 269);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 25);
            this.button3.TabIndex = 108;
            this.button3.Text = "MIT";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(112, 302);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 25);
            this.button4.TabIndex = 109;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // stopLimitControl1
            // 
            this.stopLimitControl1.Location = new System.Drawing.Point(1389, 28);
            this.stopLimitControl1.Name = "stopLimitControl1";
            this.stopLimitControl1.Size = new System.Drawing.Size(246, 908);
            this.stopLimitControl1.TabIndex = 110;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1640, 937);
            this.Controls.Add(this.stopLimitControl1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btn_Sell);
            this.Controls.Add(this.btn_Buy);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Form1";
            this.Text = "未來家教 Futures Tutor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private NPlot.Windows.PlotSurface2D plotSurface2D1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private NPlot.Windows.PlotSurface2D plotSurface2D2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Buy;
        private System.Windows.Forms.Button btn_Sell;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBox1;
        private NPlot.Windows.PlotSurface2D plotSurface2D5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage1;
        private NPlot.Windows.PlotSurface2D plotSurface2D4;
        private NPlot.Windows.PlotSurface2D plotSurface2D3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private StopLimitControl stopLimitControl1;
    }
}

