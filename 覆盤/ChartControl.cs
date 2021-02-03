using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace 覆盤
{
    public partial class ChartControl : UserControl
    {
        public Kline KL_1MK;
        public ChartControl()
        {
            InitializeComponent();
            KL_1MK = new Kline(plotSurface2D1, plotSurface2D2, 1, 300);
        }
        public void InitChart(Kind kd)
        {
            if (kd == Kind.Line)
                KL_1MK.KP = new LineP(KL_1MK);
            if (kd == Kind.Candle)
                KL_1MK.KP = new CandleP(KL_1MK);
            if (kd == Kind.All)
                KL_1MK.KP = new CandleLineP(KL_1MK);
        }
        public void InitMACDChart(Technical_analysis.MACD mACD)
        {
            //mACD = new Technical_analysis.MACD();
            plotSurface2D5.Clear();
            plotSurface2D5.Add(new NPlot.Grid()
            {
                HorizontalGridType = NPlot.Grid.GridType.Fine,
                VerticalGridType = NPlot.Grid.GridType.Fine
            });

            plotSurface2D5.Add(mACD.LP_DIF);
            plotSurface2D5.Add(mACD.LP_DEM);
            plotSurface2D5.Add(mACD.horizontalLine);
            plotSurface2D5.YAxis1.TickTextNextToAxis = false;
        }

        public void Adjust_MACD(Technical_analysis.MACD mACD) {

            //chart
            plotSurface2D5.InvokeIfRequired(() =>
            {
                plotSurface2D5.XAxis1.WorldMax = 300;
                plotSurface2D5.XAxis1.WorldMin = 0;
                plotSurface2D5.YAxis1.WorldMax = mACD.highest;
                plotSurface2D5.YAxis1.WorldMin = mACD.lowest;

                plotSurface2D5.YAxis1.TickTextNextToAxis = false;
                plotSurface2D5.Refresh();
            });
        }

        private void plotSurface2D1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KL_1MK.AddLineCrossXY(e.X, e.Y);
        }

        private void plotSurface2D1_MouseMove(object sender, MouseEventArgs e)
        {
            KL_1MK.lineCrossMove(e.X, e.Y);
        }
    }
}
