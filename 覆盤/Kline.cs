using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPlot;
using System.Drawing;


namespace 覆盤
{
    public interface klineplot
    {
        void InitKLinePS();
        void refreshK(List<TXF.K_data.K> mk);
    }

    public class candlep : klineplot
    {
        Kline KL;
        public candlep(Kline kl) {
            KL = kl;
            InitKLinePS();
        }

        public void InitKLinePS()
        {
            KL.InitPS(KL.PS);
            KL.InitPS(KL.PS_volumn);

            KL.InitCandle();
            KL.InitHp();
            KL.InitLPP();

            KL.PS.Add(KL.CP);
            KL.PS_volumn.Add(KL.hp);
            KL.PS.Refresh();
        }

        void klineplot.refreshK(List<TXF.K_data.K> mk)
        {
            if (KL.autoRefresh == false)
                return;

            List<int[]> TX = new List<int[]>();

            int i, Highest = 0, Lowest = int.MaxValue, Highest_Qty = 0;
            for (i = 0; i < 6; i++)
                TX.Add(new int[KL.KLine_num]);

            for (i = 0; i < KL.KLine_num; i++)
            {
                if (mk.Count >= i + 1)
                {
                    int istart = mk.Count - KL.KLine_num;
                    istart = Math.Max(0, istart);
                    Highest_Qty = Math.Max(Highest_Qty, Convert.ToInt32(mk[i + istart].qty));
                    Highest = Math.Max(Highest, Convert.ToInt32(mk[i + istart].high));
                    Lowest = Math.Min(Lowest, Convert.ToInt32(mk[i + istart].low));
                    TX[0][i] = Convert.ToInt32(mk[i + istart].open);
                    TX[1][i] = Convert.ToInt32(mk[i + istart].high);
                    TX[2][i] = Convert.ToInt32(mk[i + istart].low);
                    TX[3][i] = Convert.ToInt32(mk[i + istart].close);
                    TX[4][i] = Convert.ToInt32(mk[i + istart].qty);
                }
                TX[5][i] = i + 1;
            }

            KL.PS.InvokeIfRequired(() =>
            {
                KL.CP.OpenData = TX[0];// opens;
                KL.CP.HighData = TX[1];//highs;
                KL.CP.LowData = TX[2];//lows;
                KL.CP.CloseData = TX[3];//closes;
                KL.CP.AbscissaData = TX[5];// times;

                //KL.lpp.AbscissaData = new int[] { mk.Count };
                //KL.lpp.DataSource = new float[] { mk[mk.Count-1].close };
                //KL.lpp.TextData = new string[] { "⮜" };//⬅⮜←⟵

                //KL.PS.XAxis1.WorldMin = 0;
                //KL.PS.XAxis1.WorldMax = KL.KLine_num + 1;
                //KL.PS.YAxis1.WorldMin = Lowest;
                //KL.PS.YAxis1.WorldMax = Highest;
                //KL.PS.YAxis1.TickTextNextToAxis = false;
                KL.AdjustChart(mk, Highest + 10, Lowest - 10);
                KL.PS.Refresh();
            });

            KL.PS_volumn.InvokeIfRequired(() =>
            {
                KL.hp.AbscissaData = TX[5];
                KL.hp.DataSource = TX[4];
                KL.PS_volumn.XAxis1.WorldMin = 0;
                KL.PS_volumn.XAxis1.WorldMax = KL.KLine_num + 1;
                KL.PS_volumn.YAxis1.WorldMin = 1;
                KL.PS_volumn.YAxis1.WorldMax = Convert.ToInt32(Highest_Qty * 1.1);
                //PS_volumn.Location = new System.Drawing.Point(PS.Location.X, PS_volumn.Location.Y);
                KL.PS_volumn.YAxis1.TickTextNextToAxis = false;
                KL.PS_volumn.Refresh();
            });
        }
    }

    public class linep : klineplot
    {
        Kline KL;
        public void InitKLinePS()
        {
            KL.InitPS(KL.PS);
            KL.InitPS(KL.PS_volumn);

            KL.InitLp();
            KL.InitHp();
            KL.InitLPP();

            KL.PS.Add(KL.linePlot);
            KL.PS_volumn.Add(KL.hp);
            KL.PS.Refresh();
        }
        public linep(Kline kl)
        {
            KL = kl;
            InitKLinePS();
        }
        void klineplot.refreshK(List<TXF.K_data.K> mk)
        {
            if (KL.autoRefresh == false)
                return;

            List<int[]> TX = new List<int[]>();

            int i, Highest = 0, Lowest = int.MaxValue, Highest_Qty = 0;
            for (i = 0; i < 6; i++)
                TX.Add(new int[KL.KLine_num]);
            List<float> close = new List<float>();

            for (i = 0; i < KL.KLine_num; i++)
            {
                if (mk.Count >= i + 1)
                {
                    if (mk.Count > close.Count) 
                        close.Add(new float());
                    int istart = mk.Count - KL.KLine_num;
                    istart = Math.Max(0, istart);
                    Highest_Qty = Math.Max(Highest_Qty, Convert.ToInt32(mk[i + istart].qty));
                    Highest = Math.Max(Highest, Convert.ToInt32(mk[i + istart].high));
                    Lowest = Math.Min(Lowest, Convert.ToInt32(mk[i + istart].low));
                    //TX[0][i] = Convert.ToInt32(mk[i + istart].open);
                    //TX[1][i] = Convert.ToInt32(mk[i + istart].high);
                    //TX[2][i] = Convert.ToInt32(mk[i + istart].low);
                    //TX[3][i] = Convert.ToInt32(mk[i + istart].close);
                    close[i] = Convert.ToInt32(mk[i + istart].close);
                    TX[4][i] = Convert.ToInt32(mk[i + istart].qty);
                }
                else {
                    if (mk.Count > 0)
                        TX[3][i] = TX[3][i-1];
                }
                TX[5][i] = i + 1;
            }

            KL.PS.InvokeIfRequired(() =>
            {

                KL.linePlot.DataSource = close;//closes;
                KL.linePlot.AbscissaData = TX[5];// times;

                KL.AdjustChart(mk, Highest + 10, Lowest - 10);
                KL.PS.Refresh();
            });

            KL.PS_volumn.InvokeIfRequired(() =>
            {
                KL.hp.AbscissaData = TX[5];
                KL.hp.DataSource = TX[4];
                KL.PS_volumn.XAxis1.WorldMin = 0;
                KL.PS_volumn.XAxis1.WorldMax = KL.KLine_num + 1;
                KL.PS_volumn.YAxis1.WorldMin = 1;
                KL.PS_volumn.YAxis1.WorldMax = Convert.ToInt32(Highest_Qty * 1.1);
                //PS_volumn.Location = new System.Drawing.Point(PS.Location.X, PS_volumn.Location.Y);
                KL.PS_volumn.YAxis1.TickTextNextToAxis = false;
                KL.PS_volumn.Refresh();
            });
        }
    }

    public class Kline
    {

        public List<int[]> TX_1mk = null;
        public NPlot.Windows.PlotSurface2D PS = null;
        public NPlot.Windows.PlotSurface2D PS_volumn = null;
        public NPlot.CandlePlot CP = new CandlePlot();
        public NPlot.HistogramPlot hp = new HistogramPlot();
        public NPlot.LabelPointPlot lpp = new LabelPointPlot();

        public int MK = 0;
        public int KLine_num = 0;
        //public TXF.MK_data TXF_1MK;
        public VerticalLine lineCrossX = null;// = new VerticalLine(10);
        public HorizontalLine lineCrossY = null;// = new HorizontalLine(10);

        public NPlot.PointPlot pointPlot = new NPlot.PointPlot();
        public NPlot.LinePlot linePlot = new NPlot.LinePlot();
        public bool autoRefresh = true;
        public object Lock = new object();

        public klineplot KP;

        //public ref TXF_MK refTXF();
        public Kline(NPlot.Windows.PlotSurface2D nPS, NPlot.Windows.PlotSurface2D nPS2, int nMK, int nKLine_num)
        {
            PS = nPS;
            PS_volumn = nPS2;
            MK = nMK;
            KLine_num = nKLine_num;
            KP = new linep(this);
            //InitKLinePS();
            KP.InitKLinePS();
        }

        public void InitPS(NPlot.Windows.PlotSurface2D PlotSurface2D) {
            PlotSurface2D.AutoScaleAutoGeneratedAxes = true;
            PlotSurface2D.AutoScaleTitle = false;
            PlotSurface2D.DateTimeToolTip = true;
            PlotSurface2D.Legend = null;
            PlotSurface2D.LegendZOrder = -1;
            //PS.Location = new System.Drawing.Point(0, 0);
            PlotSurface2D.Name = "costPS";
            PlotSurface2D.RightMenu = null;
            PlotSurface2D.Padding = 1;

            //滑鼠tooltips 時間+價格
            PlotSurface2D.ShowCoordinates = true;
            PlotSurface2D.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            PlotSurface2D.TabIndex = 2;
            PlotSurface2D.Title = "123";
            PlotSurface2D.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            PlotSurface2D.Clear();

            Grid mygrid = new Grid() {
                HorizontalGridType = Grid.GridType.Fine,
                VerticalGridType = Grid.GridType.Fine
            };
            PlotSurface2D.Add(mygrid);
        }

        public void InitLPP() {
            int[] times = { 500 };
            lpp.DataSource = new int[] { 100 };
            lpp.AbscissaData = times;
            lpp.Font = new Font("Arial", 25);
            lpp.Marker.Type = Marker.MarkerType.None;
            lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Right;
            lpp.Marker.Size = 5;
            PS.Add(lpp);

            //NPlot.PointPlot pp = new PointPlot();
            //pp.Marker.Pen = Pens.Red;
            //pp.Marker.Type = Marker.MarkerType.Triangle;
            //pp.Marker.FillBrush = Brushes.DarkRed;
            ////pp.Marker.Filled = true;
            //pp.Marker.Size = 50;
            //pp.DataSource = new int[] { 500 };
            //pp.AbscissaData = new int[] { 200 };
            //PS.Add(pp);

        }

        public void InitLp() {
            int[] times = { 100, 200, 300, 400, 500, 600, 700 };
            linePlot.AbscissaData = times;
            linePlot.DataSource = times;
        }

        public void InitHp() {
            int[] times = { 100, 200, 300, 400, 500, 600, 700 };
            hp.AbscissaData = times;
            hp.DataSource = times;
            hp.Color = Color.Blue;
            hp.Filled = true;
            hp.RectangleBrush = RectangleBrushes.Horizontal.FaintGreenFade;
            hp.Center = false;
        }
        public void InitCandle()
        {
            /////////蠟燭圖///////////
            CP.BullishColor = Color.Red;
            CP.BearishColor = Color.Green;
            CP.Style = CandlePlot.Styles.Filled;

            int[] opens = { 1, 2, 1, 2, 1, 3, 50 };
            int[] closes = { 2, 2, 2, 1, 2, 1, 99 };
            int[] lows = { 1, 1, 1, 1, 1, 1, 40 };
            int[] highs = { 3, 2, 3, 3, 3, 4, 110 };
            int[] times = { 100, 200, 300, 400, 500, 600, 700 };

            CP.CloseData = closes;
            CP.OpenData = opens;
            CP.LowData = lows;
            CP.HighData = highs;
            CP.AbscissaData = times;
            CP.Color = Color.Gray;
            CP.Centered = false;
            //PS.Add(linePlot);
            //PS.Add(pointPlot);
            PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
        }

        public void DrawLpp(string BS, int Price, int iTime)
        {

            NPlot.LabelPointPlot lpp = new LabelPointPlot()
            {
                DataSource = new int[] { Price },
                AbscissaData = new int[] { iTime },

            };


            lpp.Marker.Size = 10;
            if (BS == "B")
            {
                lpp.Marker.Color = System.Drawing.Color.Red;
                lpp.Marker.Type = Marker.MarkerType.TriangleUp;
                lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Below;
                lpp.TextData = new string[] { "\n" + Price.ToString() };
            }
            else
            {
                lpp.Marker.Color = System.Drawing.Color.Green;
                lpp.Marker.Type = Marker.MarkerType.TriangleDown;
                lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Above;
                lpp.TextData = new string[] { Price.ToString() + "\n\n" };
            }
            lpp.Marker.Filled = true;
            PS.Add(lpp);
        }

        public void DrawAllLpp(Simulation simu) {

            //Mat
            foreach (Simulation.match mat in simu.MatList) {
                DrawLpp(mat.BS, int.Parse(mat.Price), mat.iTIME);
            }


        }
        public void DrawAllHL(Simulation simu)
        {
            //MIT
            foreach (Simulation.match mat in simu.MITList)
            {
                mat.horizontalLine = new HorizontalLine(int.Parse(mat.Price), (mat.BS == "B") ? System.Drawing.Color.Red : System.Drawing.Color.Green);
                PS.Add(mat.horizontalLine);
            }
        } 

        public void AdjustChart(List<TXF.K_data.K> mk, float Highest, float Lowest) {
            lpp.AbscissaData = new int[] { mk.Count };
            lpp.DataSource = new float[] { mk[mk.Count - 1].close };
            lpp.TextData = new string[] { "⟵" };//⬅⮜←⟵

            PS.XAxis1.WorldMin = 0;
            PS.XAxis1.WorldMax = KLine_num + 1;
            PS.YAxis1.WorldMin = Lowest;
            PS.YAxis1.WorldMax = Highest;
            PS.YAxis1.TickTextNextToAxis = false;
        }

        //public void refreshK(List<TXF.K_data.K> mk)
        //{
        //    if (autoRefresh == false)
        //        return;

        //    List<int[]> TX = new List<int[]>();

        //    int i, Highest = 0, Lowest = int.MaxValue, Highest_Qty = 0;
        //    for (i = 0; i < 6; i++)
        //        TX.Add(new int[KLine_num]);

        //    for (i = 0; i < KLine_num; i++)
        //    {
        //        if (mk.Count >= i + 1)
        //        {
        //            int istart = mk.Count - KLine_num;
        //            istart = Math.Max(0, istart);
        //            Highest_Qty = Math.Max(Highest_Qty, Convert.ToInt32(mk[i + istart].qty));
        //            Highest = Math.Max(Highest, Convert.ToInt32(mk[i + istart].high));
        //            Lowest = Math.Min(Lowest, Convert.ToInt32(mk[i + istart].low));
        //            TX[0][i] = Convert.ToInt32(mk[i + istart].open);
        //            TX[1][i] = Convert.ToInt32(mk[i + istart].high);
        //            TX[2][i] = Convert.ToInt32(mk[i + istart].low);
        //            TX[3][i] = Convert.ToInt32(mk[i + istart].close);
        //            TX[4][i] = Convert.ToInt32(mk[i + istart].qty);
        //        }
        //        TX[5][i] = i + 1;
        //    }

        //    PS.InvokeIfRequired(() =>
        //    {
        //        CP.OpenData = TX[0];// opens;
        //        CP.HighData = TX[1];//highs;
        //        CP.LowData = TX[2];//lows;
        //        CP.CloseData = TX[3];//closes;
        //        CP.AbscissaData = TX[5];// times;
        //                                //PS.Add(CP);
        //        PS.XAxis1.WorldMin = 0;
        //        PS.XAxis1.WorldMax = KLine_num + 1;
        //        PS.YAxis1.WorldMin = Lowest;
        //        PS.YAxis1.WorldMax = Highest;
        //        PS.YAxis1.TickTextNextToAxis = false;
        //        PS.Refresh();
        //    });

        //    PS_volumn.InvokeIfRequired(() =>
        //    {
        //        hp.AbscissaData = TX[5];
        //        hp.DataSource = TX[4];
        //        PS_volumn.XAxis1.WorldMin = 0;
        //        PS_volumn.XAxis1.WorldMax = KLine_num;
        //        PS_volumn.YAxis1.WorldMin = 1;
        //        PS_volumn.YAxis1.WorldMax = Convert.ToInt32(Highest_Qty * 1.1);
        //        //PS_volumn.Location = new System.Drawing.Point(PS.Location.X, PS_volumn.Location.Y);
        //        PS_volumn.YAxis1.TickTextNextToAxis = false;
        //        PS_volumn.Refresh();
        //    });
        //}
    }
}
