﻿namespace 覆盤
{
    partial class ChartControl
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.plotSurface2D1 = new NPlot.Windows.PlotSurface2D();
            this.plotSurface2D2 = new NPlot.Windows.PlotSurface2D();
            this.plotSurface2D5 = new NPlot.Windows.PlotSurface2D();
            this.SuspendLayout();
            // 
            // plotSurface2D1
            // 
            this.plotSurface2D1.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D1.AutoScaleTitle = false;
            this.plotSurface2D1.BackColor = System.Drawing.Color.Gray;
            this.plotSurface2D1.DateTimeToolTip = false;
            this.plotSurface2D1.Legend = null;
            this.plotSurface2D1.LegendZOrder = -1;
            this.plotSurface2D1.Location = new System.Drawing.Point(3, 3);
            this.plotSurface2D1.Name = "plotSurface2D1";
            this.plotSurface2D1.RightMenu = null;
            this.plotSurface2D1.ShowCoordinates = true;
            this.plotSurface2D1.Size = new System.Drawing.Size(1228, 522);
            this.plotSurface2D1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D1.TabIndex = 104;
            this.plotSurface2D1.Text = "plotSurface2D1";
            this.plotSurface2D1.Title = "";
            this.plotSurface2D1.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D1.XAxis1 = null;
            this.plotSurface2D1.XAxis2 = null;
            this.plotSurface2D1.YAxis1 = null;
            this.plotSurface2D1.YAxis2 = null;
            this.plotSurface2D1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.plotSurface2D1_MouseDoubleClick);
            this.plotSurface2D1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plotSurface2D1_MouseMove);
            // 
            // plotSurface2D2
            // 
            this.plotSurface2D2.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D2.AutoScaleTitle = false;
            this.plotSurface2D2.BackColor = System.Drawing.Color.Gray;
            this.plotSurface2D2.DateTimeToolTip = false;
            this.plotSurface2D2.Legend = null;
            this.plotSurface2D2.LegendZOrder = -1;
            this.plotSurface2D2.Location = new System.Drawing.Point(3, 531);
            this.plotSurface2D2.Name = "plotSurface2D2";
            this.plotSurface2D2.RightMenu = null;
            this.plotSurface2D2.ShowCoordinates = true;
            this.plotSurface2D2.Size = new System.Drawing.Size(1228, 179);
            this.plotSurface2D2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D2.TabIndex = 105;
            this.plotSurface2D2.Text = "plotSurface2D2";
            this.plotSurface2D2.Title = "";
            this.plotSurface2D2.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D2.XAxis1 = null;
            this.plotSurface2D2.XAxis2 = null;
            this.plotSurface2D2.YAxis1 = null;
            this.plotSurface2D2.YAxis2 = null;
            // 
            // plotSurface2D5
            // 
            this.plotSurface2D5.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D5.AutoScaleTitle = false;
            this.plotSurface2D5.BackColor = System.Drawing.Color.Gray;
            this.plotSurface2D5.DateTimeToolTip = false;
            this.plotSurface2D5.Legend = null;
            this.plotSurface2D5.LegendZOrder = -1;
            this.plotSurface2D5.Location = new System.Drawing.Point(3, 716);
            this.plotSurface2D5.Name = "plotSurface2D5";
            this.plotSurface2D5.RightMenu = null;
            this.plotSurface2D5.ShowCoordinates = true;
            this.plotSurface2D5.Size = new System.Drawing.Size(1228, 164);
            this.plotSurface2D5.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D5.TabIndex = 106;
            this.plotSurface2D5.Text = "plotSurface2D5";
            this.plotSurface2D5.Title = "";
            this.plotSurface2D5.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D5.XAxis1 = null;
            this.plotSurface2D5.XAxis2 = null;
            this.plotSurface2D5.YAxis1 = null;
            this.plotSurface2D5.YAxis2 = null;
            // 
            // ChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plotSurface2D1);
            this.Controls.Add(this.plotSurface2D2);
            this.Controls.Add(this.plotSurface2D5);
            this.Name = "ChartControl";
            this.Size = new System.Drawing.Size(1235, 878);
            this.MouseLeave += new System.EventHandler(this.ChartControl_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        public NPlot.Windows.PlotSurface2D plotSurface2D1;
        public NPlot.Windows.PlotSurface2D plotSurface2D2;
        public NPlot.Windows.PlotSurface2D plotSurface2D5;
    }
}
