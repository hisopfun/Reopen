﻿namespace 覆盤
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.plotSurface2D1 = new NPlot.Windows.PlotSurface2D();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.plotSurface2D2 = new NPlot.Windows.PlotSurface2D();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(145, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "price";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(145, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "time";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 30);
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
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(960, 7);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(238, 592);
            this.listBox1.TabIndex = 9;
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
            this.comboBox1.Location = new System.Drawing.Point(145, 94);
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
            this.plotSurface2D1.Location = new System.Drawing.Point(293, 7);
            this.plotSurface2D1.Name = "plotSurface2D1";
            this.plotSurface2D1.RightMenu = null;
            this.plotSurface2D1.ShowCoordinates = true;
            this.plotSurface2D1.Size = new System.Drawing.Size(942, 484);
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
            // 
            // plotSurface2D2
            // 
            this.plotSurface2D2.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D2.AutoScaleTitle = false;
            this.plotSurface2D2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotSurface2D2.DateTimeToolTip = false;
            this.plotSurface2D2.Legend = null;
            this.plotSurface2D2.LegendZOrder = -1;
            this.plotSurface2D2.Location = new System.Drawing.Point(293, 497);
            this.plotSurface2D2.Name = "plotSurface2D2";
            this.plotSurface2D2.RightMenu = null;
            this.plotSurface2D2.ShowCoordinates = true;
            this.plotSurface2D2.Size = new System.Drawing.Size(942, 116);
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
            this.label8.Size = new System.Drawing.Size(127, 31);
            this.label8.TabIndex = 84;
            this.label8.Text = "Price";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 31);
            this.label7.TabIndex = 86;
            this.label7.Text = "time";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.Location = new System.Drawing.Point(12, 325);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 34);
            this.button2.TabIndex = 87;
            this.button2.Text = "買";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button3.Location = new System.Drawing.Point(212, 325);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 34);
            this.button3.TabIndex = 88;
            this.button3.Text = "賣";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 365);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(277, 150);
            this.dataGridView1.TabIndex = 89;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 518);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 31);
            this.label2.TabIndex = 91;
            this.label2.Text = "Qty";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(145, 518);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 31);
            this.label3.TabIndex = 90;
            this.label3.Text = "0";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 549);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 31);
            this.label6.TabIndex = 93;
            this.label6.Text = "Profit";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(145, 549);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 31);
            this.label9.TabIndex = 92;
            this.label9.Text = "0";
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 580);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 31);
            this.label10.TabIndex = 95;
            this.label10.Text = "Entries";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(145, 580);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 31);
            this.label11.TabIndex = 94;
            this.label11.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 617);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.plotSurface2D2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.plotSurface2D1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "TXF SIMU";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private NPlot.Windows.PlotSurface2D plotSurface2D1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private NPlot.Windows.PlotSurface2D plotSurface2D2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}

