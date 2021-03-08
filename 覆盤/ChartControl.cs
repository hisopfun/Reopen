using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;


namespace 覆盤
{
    public partial class ChartControl : UserControl
    {
        public Kline KL_1MK;
        public ChartControl()
        {
            InitializeComponent();
            KL_1MK = new Kline(plotSurface2D1, plotSurface2D2, plotSurface2D5, 1, 300);
        }
        public void InitChart(Kind kd)
        {
            if (kd == Kind.Line)
                KL_1MK.KP = new LineP(KL_1MK);
            if (kd == Kind.Candle)
                KL_1MK.KP = new CandleP(KL_1MK);
            if (kd == Kind.Both)
                KL_1MK.KP = new CandleLineP(KL_1MK);
        }



        private void plotSurface2D1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KL_1MK.AddLineCrossXY(e.X, e.Y);
        }

        private void plotSurface2D1_MouseMove(object sender, MouseEventArgs e)
        {
            KL_1MK.lineCrossMove(e.X, e.Y);
            KL_1MK.showTooltip(e);
            //using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//test.TXT", FileMode.Append))
            //{
            //    using (StreamWriter sw = new StreamWriter(fs))
            //    {
            //        sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "   " + e.X.ToString() + "   " + e.Y.ToString());
            //    }
            //}
        }

        private void ChartControl_MouseLeave(object sender, EventArgs e)
        {
            if (KL_1MK.tooltip != null)
            {
                KL_1MK.tooltip.Active = false;
            }
            
        }
    }
}
