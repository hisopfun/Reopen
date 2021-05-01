using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPlot;
using System.Drawing;
using System.Windows.Forms;

namespace 覆盤
{
    public enum Kind { 
        Line, Candle, Both
    }


    public interface klineplot
    {
        void InitKLinePS();
        void refreshK(List<TXF.K_data.K> mk);
    }



    public class CandleP : klineplot
    {
        Kline KL;
        public CandleP(Kline kl) {
            KL = kl;
            InitKLinePS();
        }

        public void InitKLinePS()
        {
            KL.InitPS(KL.PS);
            KL.InitPS(KL.PS_volumn);
            KL.InitPS(KL.PS_MACD);

            KL.InitCandle();
            KL.InitQtyPlot();
            KL.InitNowPrice();
            KL.InitMACDChart(ref KL.mACD);
            KL.InitDayHalf();
            KL.InitLineCross();
            //TradingDateTimeAxis tdt = new TradingDateTimeAxis(KL.PS.XAxis1);
            //tdt.StartTradingTime = new TimeSpan(8, 45,0);
            //tdt.EndTradingTime = new TimeSpan(13,45,0);
            //tdt.

            //KL.PS.XAxis1 = tdt;
            //KL.PS.XAxis1.Label = "Date";
            //KL.PS.XAxis1.NumberFormat = "yyyy-MM-dd";
            KL.PS.InvokeIfRequired(() =>
            {
                KL.PS.Refresh();
            });
        }

        void klineplot.refreshK(List<TXF.K_data.K> mk)
        {
            if (KL.autoRefresh == false)                return;
            if (KL.KLine_num < mk.Count) return;

            int i, Highest = 0, Lowest = int.MaxValue, Highest_Qty = 0;

            List<int[]> TX = new List<int[]>();
            for (i = 0; i < 6; i++)
                TX.Add(new int[KL.KLine_num / KL.MK]);

            //Dayhalf List
            int[] Dayhalf = new int[KL.KLine_num / KL.MK];

            for (i = 0; i < KL.KLine_num; i++)
            {
                if (mk.Count >= i + 1)
                {
                    int istart = mk.Count - KL.KLine_num;
                    istart = Math.Max(0, istart);

                    if (TX[0][i / KL.MK] == 0)
                        TX[0][i / KL.MK] = Convert.ToInt32(mk[i + istart].open);
                    TX[1][i / KL.MK] = Math.Max( Convert.ToInt32(mk[i + istart].high), TX[1][i / KL.MK]);
                    if (TX[2][i / KL.MK] == 0) TX[2][i / KL.MK] = Convert.ToInt32(mk[i + istart].low);
                    TX[2][i / KL.MK] = Math.Min( Convert.ToInt32(mk[i + istart].low), TX[2][i / KL.MK]);
                    TX[3][i / KL.MK] = Convert.ToInt32(mk[i + istart].close);
                    TX[4][i / KL.MK] += Convert.ToInt32(mk[i + istart].qty);


                    Highest_Qty = Math.Max(Highest_Qty, TX[4][i / KL.MK]);
                    Highest = Math.Max(Highest, Convert.ToInt32(mk[i + istart].high));
                    Lowest = Math.Min(Lowest, Convert.ToInt32(mk[i + istart].low));
                    //half
                    //Dayhalf[i] = (Highest + Lowest) / 2;
                }
                TX[5][i / KL.MK] = i / KL.MK + 1;
            }

            //Day Half
            KL.RunDayHalf(Dayhalf.Take(mk.Count).ToArray(), TX[5].Take(mk.Count).ToArray());

            KL.PS.InvokeIfRequired(() =>
            {
                KL.CP.OpenData = TX[0];// opens;
                KL.CP.HighData = TX[1];//highs;
                KL.CP.LowData = TX[2];//lows;
                KL.CP.CloseData = TX[3];//closes;
                KL.CP.AbscissaData = TX[5];// times;

                KL.AdjustChart(mk, Highest + 10, Lowest - 10);
                KL.PS.Refresh();
            });

            //adjust Volume chart
            KL.AdjustQty(TX[4], TX[5], Highest_Qty);

            //MACD
            KL.Adjust_MACD(KL.mACD);
        }
    }

    public class LineP : klineplot
    {
        Kline KL;
        public void InitKLinePS()
        {
            KL.InitPS(KL.PS);
            KL.InitPS(KL.PS_volumn);
            KL.InitPS(KL.PS_MACD);

            KL.InitCloseLinePlot();
            KL.InitQtyPlot();
            KL.InitNowPrice();
            KL.InitMACDChart(ref KL.mACD);
            KL.InitDayHalf();
            KL.InitLineCross();
      

            KL.PS.InvokeIfRequired(() =>
            {
                KL.PS.Refresh();
            });
        }
        public LineP(Kline kl)
        {
            KL = kl;
            InitKLinePS();
        }
        void klineplot.refreshK(List<TXF.K_data.K> mk)
        {
            
            if (KL.autoRefresh == false)                return;
            if (KL.KLine_num < mk.Count) return;

            int i, Highest = 0, Lowest = int.MaxValue, Highest_Qty = 0;

            List<int[]> TX = new List<int[]>();
            for (i = 0; i < 6; i++)
                TX.Add(new int[KL.KLine_num / KL.MK]);

            //Dayhalf List
            int[] Dayhalf = new int[KL.KLine_num / KL.MK];

            for (i = 0; i < KL.KLine_num; i++)
            {
                if (mk.Count >= i + 1)
                {
                    int istart = mk.Count - KL.KLine_num;
                    istart = Math.Max(0, istart);

                    //TX[0][i] = Convert.ToInt32(mk[i + istart].open);
                    //TX[1][i] = Convert.ToInt32(mk[i + istart].high);
                    //TX[2][i] = Convert.ToInt32(mk[i + istart].low);
                    TX[3][i / KL.MK] = Convert.ToInt32(mk[i + istart].close);
                    //close[i] = Convert.ToInt32(mk[i + istart].close);
                    TX[4][i / KL.MK] += Convert.ToInt32(mk[i + istart].qty);

                    //half
                    //Dayhalf[i] = (Highest + Lowest) / 2;

                    Highest_Qty = Math.Max(Highest_Qty, TX[4][i / KL.MK]);
                    Highest = Math.Max(Highest, Convert.ToInt32(mk[i + istart].high));
                    Lowest = Math.Min(Lowest, Convert.ToInt32(mk[i + istart].low));
                }
                TX[5][i / KL.MK] = i / KL.MK + 1;
            }


            //Day Half
            KL.RunDayHalf(Dayhalf.Take(mk.Count).ToArray(), TX[5].Take(mk.Count).ToArray());


            KL.PS.InvokeIfRequired(() =>
            {
                KL.linePlot.DataSource = TX[3].Take((mk.Count - 1) / KL.MK + 1).ToArray();//.GetRange(0, mk.Count);//closes;   //mk.GroupBy(e => e.close).SelectMany(g => g.Select(p => Convert.ToInt32(p.close))).ToArray();
                KL.linePlot.AbscissaData = TX[5];// times;
                KL.AdjustChart(mk, Highest + 10, Lowest - 10);
                KL.PS.Refresh();
            });

            //adjust Volume chart
            KL.AdjustQty(TX[4], TX[5], Highest_Qty);

            //MACD
            KL.Adjust_MACD(KL.mACD);
        }
    }

    public class CandleLineP : klineplot
    {
        Kline KL;
        public CandleLineP(Kline kl)
        {
            KL = kl;
            InitKLinePS();
        }

        public void InitKLinePS()
        {
            KL.InitPS(KL.PS);
            KL.InitPS(KL.PS_volumn);
            KL.InitPS(KL.PS_MACD);

            KL.InitCandle();
            KL.InitCloseLinePlot();
            KL.InitQtyPlot();
            KL.InitNowPrice();
            KL.InitMACDChart(ref KL.mACD);
            KL.InitDayHalf();
            KL.InitLineCross();

            KL.CP.BullishColor = Color.FromArgb(255,192,192);
            KL.CP.BearishColor = Color.FromArgb(192, 255, 192);

            KL.PS.InvokeIfRequired(() =>
            {
                KL.PS.Refresh();
            });
        }

        void klineplot.refreshK(List<TXF.K_data.K> mk)
        {
            if (KL.autoRefresh == false)                return;
            if (KL.KLine_num < mk.Count) return;

            List<int[]> TX = new List<int[]>();

            int i, Highest = 0, Lowest = int.MaxValue, Highest_Qty = 0;
            for (i = 0; i < 6; i++)
                TX.Add(new int[KL.KLine_num / KL.MK]);

            //Dayhalf List
            int[] Dayhalf = new int[KL.KLine_num / KL.MK];

            for (i = 0; i < KL.KLine_num; i++)
            {
                if (mk.Count >= i + 1)
                {
                    int istart = mk.Count - KL.KLine_num;
                    istart = Math.Max(0, istart);

                    if (TX[0][i / KL.MK] == 0)
                        TX[0][i / KL.MK] = Convert.ToInt32(mk[i + istart].open);
                    TX[1][i / KL.MK] = Math.Max(Convert.ToInt32(mk[i + istart].high), TX[1][i / KL.MK]);
                    if (TX[2][i / KL.MK] == 0) TX[2][i / KL.MK] = Convert.ToInt32(mk[i + istart].low);
                    TX[2][i / KL.MK] = Math.Min(Convert.ToInt32(mk[i + istart].low), TX[2][i / KL.MK]);
                    TX[3][i / KL.MK] = Convert.ToInt32(mk[i + istart].close);
                    TX[4][i / KL.MK] += Convert.ToInt32(mk[i + istart].qty);

                    //half
                    //Dayhalf[i / KL.MK] = (Highest + Lowest) / 2;


                    Highest_Qty = Math.Max(Highest_Qty, TX[4][i / KL.MK]);
                    Highest = Math.Max(Highest, Convert.ToInt32(mk[i + istart].high));
                    Lowest = Math.Min(Lowest, Convert.ToInt32(mk[i + istart].low));

                }
                TX[5][i / KL.MK] = i / KL.MK + 1;
                
            }


            //Day Half
            //KL.RunDayHalf(Dayhalf.Take(mk.Count).ToArray(), TX[5].Take(mk.Count).ToArray());

            //adjust Volume chart
            KL.AdjustQty(TX[4], TX[5], Highest_Qty);

            //MACD
            KL.Adjust_MACD(KL.mACD);
        }
    }



    public class Kline
    {

        public List<int[]> TX_1mk = null;
        public NPlot.Windows.PlotSurface2D PS = null;
        public NPlot.Windows.PlotSurface2D PS_volumn = null;
        public NPlot.Windows.PlotSurface2D PS_MACD = null;
        public Technical_analysis.MACD mACD;
        public NPlot.CandlePlot CP = new CandlePlot();
        public NPlot.HistogramPlot hp = new HistogramPlot();
        public NPlot.LabelPointPlot lpp = new LabelPointPlot();

        public int MK = 0;
        public int KLine_num = 0;
        //public TXF.MK_data TXF_1MK;
        public VerticalLine lineCrossX = null;// = new VerticalLine(10);
        public HorizontalLine lineCrossY = null;// = new HorizontalLine(10);
        public VerticalLine Volume_lineCrossX = null;
        public VerticalLine MACD_lineCrossX = null;
        public ToolTip tooltip = null;

        public List<NPlot.IDrawable> dw = null; //plot List
        public LinePlot HLDayHalf = null; //Day Half
        public TextItem LPPDayHalf = null; //Day Half
        public NPlot.PointPlot pointPlot = new NPlot.PointPlot();
        public NPlot.LinePlot linePlot = new NPlot.LinePlot();
        System.Drawing.Point? prevPosition = null;
        

        public bool autoRefresh = true;
        public object Lock = new object();

        public klineplot KP;

        //public ref TXF_MK refTXF();
        public Kline(NPlot.Windows.PlotSurface2D nPS, NPlot.Windows.PlotSurface2D nPS2, NPlot.Windows.PlotSurface2D nPS3, int nMK, int nKLine_num)
        {
            this.PS = nPS;
            this.PS_volumn = nPS2;
            this.PS_MACD = nPS3;
            this.MK = nMK;
            this.KLine_num = nKLine_num;
            this.mACD = new Technical_analysis.MACD(nKLine_num);
            this.KP = new LineP(this);
            this.KP.InitKLinePS();

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
            PlotSurface2D.DateTimeToolTip = false;
            PlotSurface2D.ShowCoordinates = false;
            PlotSurface2D.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            PlotSurface2D.TabIndex = 2;
            PlotSurface2D.Title = "123";
            PlotSurface2D.TitleFont = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            PlotSurface2D.Clear();

            Grid mygrid = new Grid()
            {
                HorizontalGridType = Grid.GridType.Coarse,
                VerticalGridType = Grid.GridType.Coarse
            };
            PlotSurface2D.Add(mygrid);
        }

        public void InitLineCross() {
            this.lineCrossX = null;
            this.lineCrossY = null;
            this.MACD_lineCrossX = null;
            this.Volume_lineCrossX = null;
            if (this.tooltip != null)
                this.tooltip.RemoveAll();
            this.tooltip = null;
        }
        public void InitDayHalf() {
            this.LPPDayHalf = new NPlot.TextItem(new PointD(KLine_num * 0.95 ,200), "");
            this.PS.Add(this.LPPDayHalf);

            this.HLDayHalf = new LinePlot() { 
                AbscissaData = new int[]{ 100, 200, 300, 400, 500, 600, 700 },
                DataSource = new int[] { 800, 800, 800, 800, 800, 800, 800 }
            };
            this.HLDayHalf.Pen.Color = System.Drawing.Color.Gold;
            this.PS.Add(this.HLDayHalf);
        }

        public void RunDayHalf(int[] nDayHalf, int[] times) {
            if (nDayHalf.Length <= 0) return;
            this.HLDayHalf.DataSource = nDayHalf;
            this.HLDayHalf.AbscissaData = times;

            this.LPPDayHalf.Start = new PointD(KLine_num * 0.95, nDayHalf[nDayHalf.Length - 1]);
            this.LPPDayHalf.Text = nDayHalf[nDayHalf.Length - 1].ToString();
        }

        public void InitMACDChart(ref Technical_analysis.MACD mACD)
        {
            this.PS_MACD.DateTimeToolTip = false;
            //mACD = new Technical_analysis.MACD();
            this.PS_MACD.Clear();
            //this.PS_MACD.Add(new NPlot.Grid()
            //{
            //    HorizontalGridType = NPlot.Grid.GridType.Coarse,
            //    VerticalGridType = NPlot.Grid.GridType.Coarse
            //});
            
            this.PS_MACD.Add(this.mACD.LP_DIF);
            this.PS_MACD.Add(this.mACD.LP_DEM);
            this.PS_MACD.Add(this.mACD.Bar_OSC);
            this.PS_MACD.Add(this.mACD.horizontalLine);
            this.PS_MACD.YAxis1.TickTextNextToAxis = false;
            this.PS_MACD.XAxis1.WorldMin = 50;
            this.PS_MACD.XAxis1.WorldMax = 750;
            
        }

        public void InitNowPrice() {
            int[] times = { 500 };
            lpp.DataSource = new int[] { 100 };
            lpp.AbscissaData = times;
            lpp.TextData = new string[] { "" };
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

        public void InitCloseLinePlot() {
            int[] times = { 100, 200, 300, 400, 500, 600, 700 };
            //DateTime[] times = new DateTime[7];
            //for (int i = 0; i < 7; i++)
            //{
            //    times[i] = DateTime.Now.Date.AddMinutes(i * 15);
            //}

            linePlot.AbscissaData = times;
            linePlot.DataSource = new int[]{ 200, 300, 500, 250, 850, 1000, 1200 }; ;
            //linePlot.Shadow = true;
            //linePlot.ShadowColor = Color.Black;
            linePlot.Pen.Width = 2;

            PS.Add(linePlot);
            PS.YAxis1.TickTextNextToAxis = false;
            this.PS.XAxis1.WorldMin = 50;
            this.PS.XAxis1.WorldMax = 750;
        }

        public void InitQtyPlot() {
            int[] times = { 100, 200, 300, 400, 500, 600, 700 };
            hp.AbscissaData = times;
            hp.DataSource = times;
            hp.Color = Color.Blue;
            hp.Filled = true;
            hp.RectangleBrush = RectangleBrushes.Horizontal.FaintGreenFade;
            hp.Center = true;
            PS_volumn.Add(hp);
            PS_volumn.YAxis1.TickTextNextToAxis = false;
        }
        public void InitCandle()
        {
            /////////蠟燭圖///////////
            CP.BullishColor = Color.Red;
            CP.BearishColor = Color.Green;
            CP.Style = CandlePlot.Styles.Filled;
            
            int[] opens =  { 100, 200,300, 500, 750, 850, 1000 };
            int[] closes = { 200, 300, 500, 250, 850, 1000, 1200 };
            int[] lows =   { 50, 200, 250, 250, 750, 750, 950 };
            int[] highs =  { 250, 350, 600, 600, 900, 1050, 1250 };
            int[] times =  { 100, 200, 300, 400, 500, 600, 700 };
            //DateTime[] times = new DateTime[7];
            //for (int i = 0; i < 7; i++)
            //{
            //    times[i] = DateTime.Now.Date.AddDays(i);
            //}
            CP.CloseData = closes;
            CP.OpenData = opens;
            CP.LowData = lows;
            CP.HighData = highs;
            CP.AbscissaData = times;
            CP.Color = Color.Gray;
            CP.Centered = false;
            PS.Add(CP);
            PS.YAxis1.TickTextNextToAxis = false;
            this.PS.XAxis1.WorldMin = 50;
            this.PS.XAxis1.WorldMax = 750;
            
            //PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            //PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            //PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
        }

        public void DrawLpp(MatGraph graph, int Price, int iTime)
        {

            NPlot.LabelPointPlot lpp = new LabelPointPlot()
            {
                DataSource = new int[] { Price },
                AbscissaData = new int[] { (iTime - 1) / MK + 1},
            };

            lpp.Marker.Size = 10;
            if (graph == MatGraph.redCircle)
            {
                lpp.Marker.Color = System.Drawing.Color.Red;
                lpp.Marker.Type = Marker.MarkerType.Circle;
                lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Below;
                lpp.TextData = new string[] { "\n" + Price.ToString() };
            }
            else if (graph == MatGraph.redSquare)
            {
                lpp.Marker.Color = System.Drawing.Color.Red;
                lpp.Marker.Type = Marker.MarkerType.Square;
                lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Above;
                lpp.TextData = new string[] { Price.ToString() + "\n\n" };
            }
            else if (graph == MatGraph.greenCircle)
            {
                lpp.Marker.Color = System.Drawing.Color.Green;
                lpp.Marker.Type = Marker.MarkerType.Circle;
                lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Above;
                lpp.TextData = new string[] { Price.ToString() + "\n\n" };
            }
            else if (graph == MatGraph.greenSquare)
            {
                lpp.Marker.Color = System.Drawing.Color.Green;
                lpp.Marker.Type = Marker.MarkerType.Square;
                lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Below;
                lpp.TextData = new string[] { "\n" + Price.ToString() };
            }

            if (graph == MatGraph.buy)
            {
                lpp.Marker.Color = System.Drawing.Color.Red;
                lpp.Marker.Type = Marker.MarkerType.TriangleUp;
                lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Below;
                lpp.TextData = new string[] { "\n" + Price.ToString() };
            }

            if (graph == MatGraph.sell)
            {
                lpp.Marker.Color = System.Drawing.Color.Green;
                lpp.Marker.Type = Marker.MarkerType.TriangleDown;
                lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Below;
                lpp.TextData = new string[] { "\n" + Price.ToString() };
            }

            lpp.Marker.Filled = true;
            PS.Add(lpp);
        }

        public enum MatGraph { 
            redCircle, redSquare, greenCircle, greenSquare, buy, sell
        }

        public void DrawAllLpp(Simulation simu) {//label point plot

            //Mat
            for(int i = 0; i < simu.MatList.Count; i++) {
                if (i == 0 || simu.Qty(simu.MatList.GetRange(0, i)) == 0)
                {
                    //Buy in
                    if (simu.MatList[i].BS == "B")
                    {
                        //DrawLpp(MatGraph.redCircle, int.Parse(simu.MatList[i].Price), simu.MatList[i].iTIME);
                        DrawLpp(MatGraph.buy, int.Parse(simu.MatList[i].Price), simu.MatList[i].iTIME);
                    }

                    //Sell in
                    else 
                    {
                        //DrawLpp(MatGraph.greenCircle, int.Parse(simu.MatList[i].Price), simu.MatList[i].iTIME);
                        DrawLpp(MatGraph.sell, int.Parse(simu.MatList[i].Price), simu.MatList[i].iTIME);
                    }
                }

                //Buy out
                else if (simu.Qty(simu.MatList.GetRange(0, i)) == 1 && simu.MatList[i].BS == "S") {
                    //DrawLpp(MatGraph.redSquare, int.Parse(simu.MatList[i].Price), simu.MatList[i].iTIME);
                    DrawLpp(MatGraph.sell, int.Parse(simu.MatList[i].Price), simu.MatList[i].iTIME);
                }

                //Sell out
                else if (simu.Qty(simu.MatList.GetRange(0, i)) == -1 && simu.MatList[i].BS == "B")
                {
                    //DrawLpp(MatGraph.greenSquare, int.Parse(simu.MatList[i].Price), simu.MatList[i].iTIME);
                    DrawLpp(MatGraph.buy, int.Parse(simu.MatList[i].Price), simu.MatList[i].iTIME);
                }

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

        public void Adjust_MACD(Technical_analysis.MACD mACD)
        {
            if (mACD.DEM.Chart_EMA.Count > mACD.times_DEM.Length) return;
            try
            {
                //chart
                PS_MACD.InvokeIfRequired(() =>
                {
                    PS_MACD.XAxis1.WorldMax = KLine_num + 1;
                    PS_MACD.XAxis1.WorldMin = 0;
                    PS_MACD.YAxis1.WorldMax = mACD.highest;
                    PS_MACD.YAxis1.WorldMin = mACD.lowest;

                    PS_MACD.YAxis1.TickTextNextToAxis = false;
                    PS_MACD.Refresh();
                });
            }
            catch (NPlot.NPlotException NE) { 
                
            }
        }

        public void AdjustChart(List<TXF.K_data.K> mk, float Highest, float Lowest) {
            if (mk.Count > 0)
            {
                lpp.AbscissaData = new int[] { (mk.Count - 1) / MK + 1 };
                lpp.DataSource = new float[] { mk[mk.Count - 1].close };
                lpp.TextData = new string[] { "⟵" };//⬅⮜←⟵

                PS.XAxis1.WorldMin = 0;
                PS.XAxis1.WorldMax = KLine_num / MK + 1;
                PS.YAxis1.WorldMin = Lowest;
                PS.YAxis1.WorldMax = Highest;
                PS.YAxis1.TickTextNextToAxis = false;
            }
        }

        public void AdjustQty( int[] data, int[] abscissa, int Highest_Qty) {
            PS_volumn.InvokeIfRequired(() =>
            {
                hp.AbscissaData = abscissa;
                hp.DataSource = data;
                PS_volumn.XAxis1.WorldMin = 0;
                PS_volumn.XAxis1.WorldMax = KLine_num / MK + 1;
                PS_volumn.YAxis1.WorldMin = 1;
                PS_volumn.YAxis1.WorldMax = Convert.ToInt32(Highest_Qty * 1.1);
                //PS_volumn.Location = new System.Drawing.Point(PS.Location.X, PS_volumn.Location.Y);
                PS_volumn.YAxis1.TickTextNextToAxis = false;
                PS_volumn.Refresh();
            });
        }

        public void showTooltip(MouseEventArgs e)
        {
            if (this.PS.PhysicalXAxis1Cache == null || this.PS.PhysicalYAxis1Cache == null) return;
            if (this.tooltip == null) {                 this.tooltip = new ToolTip();            }
            if (!this.tooltip.Active) this.tooltip.Active = true;
            //if (this.tooltip.)

            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            //var results = chart1.HitTest(pos.X, pos.Y, false,
            //                                ChartElementType.DataPoint);
            //foreach (var result in results)
            //{
            //    if (result.ChartElementType == ChartElementType.DataPoint)
            //    {
            //        var prop = result.Object as DataPoint;
            //        if (prop != null)
            //        {
            //            var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
            //            var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

            //            check if the cursor is really close to the point(2 pixels around the point)
            //            if (Math.Abs(pos.X - pointXPixel) < 2 &&
            //                Math.Abs(pos.Y - pointYPixel) < 2)
            //            {
            System.Drawing.Point here = new System.Drawing.Point(e.X, e.Y);
        
            double x = this.PS.PhysicalXAxis1Cache.PhysicalToWorld(here, true);
            double y = this.PS.PhysicalYAxis1Cache.PhysicalToWorld(here, true);
            if (x != this.PS.PhysicalXAxis1Cache.PhysicalToWorld(here, false)) return;
            if (y != this.PS.PhysicalYAxis1Cache.PhysicalToWorld(here, false)) return;
            tooltip.Show("X=" + (int)x + ", Y=" + (int)y, this.PS, pos.X + 10, pos.Y - 15);
            //            }
            //        }
            //    }
            //}
        }

        public void AddLineCrossXY(int xx, int yy) {
            if (this.lineCrossX != null)
            {
                this.PS.Remove(lineCrossX, false);
                this.PS.Remove(lineCrossY, false);
                this.PS_volumn.Remove(Volume_lineCrossX, false);
                this.PS_MACD.Remove(MACD_lineCrossX, false);

                this.lineCrossX = null;
                this.lineCrossY = null;
                this.Volume_lineCrossX = null;
                this.MACD_lineCrossX = null;
                return;
            }

            System.Drawing.Point here = new System.Drawing.Point(xx , yy);
            //螢幕座標轉化為業務座標
            double x = this.PS.PhysicalXAxis1Cache.PhysicalToWorld(here, true);
            double y = this.PS.PhysicalYAxis1Cache.PhysicalToWorld(here, true);
            DateTime dateTime = new DateTime((long)x);
            //水平線建立
            this.lineCrossY = new NPlot.HorizontalLine(y);
            this.lineCrossY.LengthScale = 1;
            this.lineCrossY.OrdinateValue = y;
            this.lineCrossY.Pen = Pens.Blue;
            //line.OrdinateValue = 2;
            this.PS.Add(lineCrossY);
            ////  ///////垂直線///////////
            this.lineCrossX = new NPlot.VerticalLine(x);
            this.lineCrossX.LengthScale = 1;
            this.lineCrossX.Pen = Pens.Blue;
            this.lineCrossX.AbscissaValue = x;

            ////  ///////垂直線///////////
            this.Volume_lineCrossX = new NPlot.VerticalLine(x);
            this.Volume_lineCrossX.LengthScale = 1;
            this.Volume_lineCrossX.Pen = Pens.Blue;
            this.Volume_lineCrossX.AbscissaValue = x;

            ////  ///////垂直線///////////
            this.MACD_lineCrossX = new NPlot.VerticalLine(x);
            this.MACD_lineCrossX.LengthScale = 1;
            this.MACD_lineCrossX.Pen = Pens.Blue;
            this.MACD_lineCrossX.AbscissaValue = x;

            this.PS.Add(lineCrossX);
            this.PS.Refresh();

            this.PS_volumn.Add(Volume_lineCrossX);
            this.PS_volumn.Refresh();

            this.PS_MACD.Add(MACD_lineCrossX);
            this.PS_MACD.Refresh();
        }

        public void lineCrossMove(int xx, int yy) {
            if (this.PS.PhysicalXAxis1Cache == null || this.PS.PhysicalYAxis1Cache == null)
                return;
            System.Drawing.Point here = new System.Drawing.Point(xx, yy);
            int x = Convert.ToInt32(this.PS.PhysicalXAxis1Cache.PhysicalToWorld(here, true));
            int y = Convert.ToInt32(this.PS.PhysicalYAxis1Cache.PhysicalToWorld(here, true));

            if (this.lineCrossY != null && this.lineCrossX != null)
            {
                this.lineCrossY.OrdinateValue = y;
                this.lineCrossX.AbscissaValue = x;
                this.Volume_lineCrossX.AbscissaValue = x;
                this.MACD_lineCrossX.AbscissaValue = x;
            }
            
            
            this.PS.Refresh();
            this.PS_volumn.Refresh();
            this.PS_MACD.Refresh();
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
