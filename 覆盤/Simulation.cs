using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPlot;
using System.Drawing;


namespace 覆盤
{
    public class Simulation
    {
        public List<match> MatList { get; set; } = new List<match>();
        public List<match> OrdList { get; set; } = new List<match>();
        public List<match> MITList { get; set; } = new List<match>();

        public class match
        {
            public string MatTime { get; }
            public string FutNo { get; }
            public string BSCode { get; }
            public string Qty { get; }
            public string Price { get; }
            public string MITLabel { get; }

            public NPlot.LinePlot LineChart = new NPlot.LinePlot();

            public match(string nMatTime, string nFutNo, string nBSCode, string nQty, string nPrice, string nMITLabel)
            {
                MatTime = nMatTime;
                FutNo = nFutNo;
                BSCode = nBSCode;
                Qty = nQty;
                Price = nPrice;
                MITLabel = nMITLabel;
            }
        }

        public bool Order(string nMatTime, string nFutNo, string nBSCode, string nQty, string nOrdPri)
        {
            OrdList.Add(new match(nMatTime, nFutNo, nBSCode, nQty, nOrdPri, ""));
            return true;
        }

        public bool MIT(string nMatTime, string nFutNo, string nBSCode, string nQty, string nMITPri, string nMatPri)
        {
            string Label = "";
            if (int.Parse(nMITPri) < int.Parse(nMatPri))
            {
                Label = "<=";
            }
            else if (int.Parse(nMITPri) >= int.Parse(nMatPri))
            {
                Label = ">=";
            }
            MITList.Add(new match(nMatTime, nFutNo, nBSCode, nQty, nMITPri, Label));
            return true;
        }
        public List<string> MITToOrder(string nMatTime, string nBid, string nAsk, string nMatPri, NPlot.Windows.PlotSurface2D PS)
        {
            List<string> Order = new List<string>();
            //MIT -> order
            int i;
            for (i = 0; i < MITList.Count; i++)
            {
                if (int.Parse(nMatPri) >= int.Parse(MITList[i].Price) && MITList[i].MITLabel.Equals(">=") ||
                    int.Parse(nMatPri) <= int.Parse(MITList[i].Price) && MITList[i].MITLabel.Equals("<="))
                {

                    OrdList.Add(new match(nMatTime, MITList[i].FutNo, MITList[i].BSCode, MITList[i].Qty, "M", ""));
                    Order.Add(nMatPri + "," + MITList[i].BSCode + "," + MITList[i].Price);
                    DeleteNotMat(MITList, MITList[i].BSCode, MITList[i].Price, PS);
                    //MITList.Remove(MITList[i]);
                    i--;
                }
            }
            return Order;
        }

        public List<string> DealInfo(string nMatTime, string nBid, string nAsk, string nMatPri, NPlot.Windows.PlotSurface2D PS)
        {
            List<string> deal = new List<string>();

            int i;
            //order -> deal
            for (i = 0; i < OrdList.Count; i++)
            {
                if (OrdList[i].BSCode.Equals("B") && OrdList[i].Price.Equals("M") ||
                    OrdList[i].BSCode.Equals("B") && int.Parse(nAsk) <= int.Parse(OrdList[i].Price))
                {
                    MatList.Add(new match(nMatTime, OrdList[i].FutNo, OrdList[i].BSCode, OrdList[i].Qty, nAsk, ""));
                    deal.Add(nAsk + ",B," + OrdList[i].Price);
                    DeleteNotMat(OrdList, OrdList[i].BSCode, OrdList[i].Price, PS);
                    //OrdList.Remove(OrdList[i]);
                }
                else if (OrdList[i].BSCode.Equals("S") && OrdList[i].Price.Equals("M") ||
                         OrdList[i].BSCode.Equals("S") && int.Parse(nBid) >= int.Parse(OrdList[i].Price))
                {
                    MatList.Add(new match(nMatTime, OrdList[i].FutNo, OrdList[i].BSCode, OrdList[i].Qty, nBid, ""));
                    deal.Add(nBid + ",S," + OrdList[i].Price);
                    DeleteNotMat(OrdList, OrdList[i].BSCode, OrdList[i].Price, PS);
                    //OrdList.Remove(OrdList[i]);
                }
            }
            return deal;
        }


        public string Profit(string nMatPri)
        {
            int profit = 0;
            foreach (match mat in MatList)
            {
                int dif = (int.Parse(nMatPri) - int.Parse(mat.Price)) * (mat.BSCode.Equals("S") ? -1 : 1);
                profit += (dif * int.Parse(mat.Qty));
            }
            return profit.ToString();
        }

        public int Qty(string nBS, string nPrice)
        {

            int Qty = 0;

            //All OI Qty
            if (nBS == "" && nPrice == "")
            {
                foreach (match mat in MatList)
                {
                    Qty += (int.Parse(mat.Qty) * (mat.BSCode.Equals("B") ? 1 : -1));
                }
                return Qty;
            }

            //Price OI Qty
            List<match> OIList = OI();
            foreach (match mat in OIList)
            {
                if (mat.BSCode.Equals(nBS) && mat.Price.Equals(nPrice))
                {
                    Qty += int.Parse(mat.Qty);
                }
            }
            return Qty;

        }
        public List<match> OI()
        {
            int index = 1;
            List<match> MatL = new List<match>();
            foreach (match mat in MatList)
            {
                MatL.Add(mat);
            }

            while (index < MatL.Count)
            {
                if (!MatL[0].BSCode.Equals(MatL[index].BSCode))
                {
                    MatL.Remove(MatL[index]);
                    MatL.Remove(MatL[0]);
                    index--;
                    continue;
                }
                index++;
            }
            return MatL;
        }

        public int OrderQty(string nBS, string nPrice)
        {

            int Qty = 0;
            //Price OI Qty
            foreach (match mat in OrdList)
            {
                if (mat.BSCode.Equals(nBS) && mat.Price.Equals(nPrice))
                {
                    Qty += int.Parse(mat.Qty);
                }
            }
            return Qty;
        }
        public string Price()
        {
            return MatList[MatList.Count - 1].Price;
        }

        public bool DeleteNotMat(List<match> NotMatList, string nBSCode, string nPrice, NPlot.Windows.PlotSurface2D PS)
        {
            bool change = false;
            int i;
            for (i = 0; i < NotMatList.Count; i++)
            {
                if (NotMatList[i].BSCode.Equals(nBSCode) && NotMatList[i].Price.Equals(nPrice))
                {
                    PS.Remove(NotMatList[i].LineChart, false);
                    NotMatList.Remove(NotMatList[i]);
                    i--;
                    change = true;
                }
            }
            return change;
        }

        /*
        public bool DeleteMIT(string nBSCode, string nPrice, NPlot.Windows.PlotSurface2D PS)
        {
            bool change = false;
            int i;
            for (i = 0; i < MITList.Count; i++)
            {
                if (MITList[i].BSCode.Equals(nBSCode) && MITList[i].Price.Equals(nPrice))
                {
                    PS.Remove(MITList[i].LineChart, false);
                    MITList.Remove(MITList[i]);
                    i--;
                    change = true;
                }
            }
            return change;
        }
        */
    }

    public class Kline { 
        public List<int[]> TX_1mk = null;
        public NPlot.Windows.PlotSurface2D PS = null;
        public NPlot.CandlePlot CP = new CandlePlot();
        public int MK = 0;
        public int KLine_num = 0;
        public TXF_MK TXF_1MK;
        public VerticalLine lineCrossX = null;// = new VerticalLine(10);
        public HorizontalLine lineCrossY = null;// = new HorizontalLine(10);

        public NPlot.PointPlot pointPlot = new NPlot.PointPlot();
        public NPlot.LinePlot linePlot = new NPlot.LinePlot();
        public bool autoRefresh = true;
        public object Lock = new object();

        //public ref TXF_MK refTXF();
        public Kline(NPlot.Windows.PlotSurface2D nPS, int nMK, int nKLine_num, TXF_MK nTXF_1MK)
        {
            PS = nPS;
            MK = nMK;
            KLine_num = nKLine_num;
            TXF_1MK = nTXF_1MK;
            InitKLinePS();
        }

        public void InitKLinePS()
        {

            PS.AutoScaleAutoGeneratedAxes = true;
            PS.AutoScaleTitle = false;
            PS.DateTimeToolTip = true;
            PS.Legend = null;
            PS.LegendZOrder = -1;
            //PS.Location = new System.Drawing.Point(0, 0);
            PS.Name = "costPS";
            PS.RightMenu = null;
            PS.Padding = 1;

            //滑鼠tooltips 時間+價格
            PS.ShowCoordinates = true;
            //PS.Size = new System.Drawing.Size(969, 595);
            //PS.Width = 1300;
            PS.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            PS.TabIndex = 2;
            PS.Title = "123";
            PS.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

            //////////////////////
            PS.Clear();
            ////////網格//////////
            //KLinePS = new NPlot.Windows.PlotSurface2D();
            Grid mygrid = new Grid();
            mygrid.HorizontalGridType = Grid.GridType.Fine;
            mygrid.VerticalGridType = Grid.GridType.Fine;
            PS.Add(mygrid);


            /////////水平線//////////
            //HorizontalLine line = new HorizontalLine(10);
            //line.LengthScale = 2.89f;
            ////line.OrdinateValue = 2;
            //PS.Add(line, -10);
            /////////垂直線///////////
            //VerticalLine line2 = new VerticalLine(10);
            //line2.LengthScale = 0.89f;
            //PS.Add(line2);

            InitCandle();

            //LineChart.Label = "MIT";
            //LineChart.Color = System.Drawing.Color.Blue;
            //PS.Add(LineChart, NPlot.PlotSurface2D.XAxisPosition.Bottom,
            //      NPlot.PlotSurface2D.YAxisPosition.Left);
            
            PS.Add(CP);
            PS.Refresh();
        }
        public void InitCandle()
        {
            /////////蠟燭圖///////////
            ///

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
           
            PS.Add(linePlot);
            PS.Add(pointPlot);


            PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            PS.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
        }

        public void refreshK() {
            if (autoRefresh == false)
                return;
            TXF_MK p = TXF_1MK;
            List<int[]> TX = new List<int[]>();

            int i, Highest = 0, Lowest = int.MaxValue;
            for (i = 0; i < 5; i++)
                TX.Add(new int[KLine_num]);
            int now = KLine_num - 1;

            while(now >= 0)
            {
                p = p.next;
                if (p == null)
                {
                    while(now-1 >= 0) {
                        now--;
                        TX[0][now] = (Lowest + Highest) / 2;
                        TX[1][now] = (Lowest + Highest) / 2;
                        TX[2][now] = (Lowest + Highest) / 2;
                        TX[3][now] = (Lowest + Highest) / 2;
                        TX[4][now] = now;
                    }
                    break;
                }
                if (Convert.ToInt32(p.low) == 0 || Convert.ToInt32(p.high) == 0)
                    continue;

                Highest = Math.Max(Convert.ToInt32(p.high), Highest);
                Lowest = Math.Min(Convert.ToInt32(p.low), Lowest);
                Lowest = Lowest == 0 ? Convert.ToInt32(p.low) : Lowest;
                int kTime = int.Parse(p.kTime.Substring(2, 2));
                if (kTime % MK == MK - 1)
                {
                    now--;
                    if (now == -1)
                        break;
                }

                //if (kTime % MK == 0)
                TX[0][now] = Convert.ToInt32(p.open);
                TX[1][now] = Math.Max(Convert.ToInt32(p.high), TX[1][now]); 
                TX[2][now] = Math.Min(Convert.ToInt32(p.low), TX[2][now]);
                TX[2][now] = TX[2][now] == 0 ? Convert.ToInt32(p.low) : TX[2][now];
                if (TX[3][now] == 0)
                    TX[3][now] = Convert.ToInt32(p.close);
                TX[4][now] = now;

                
            }
            
            float[] one_po_three = new float[KLine_num];
            int iHigh = 0, iLow = 0, iStart = -1;
            string status = "";
            for (i = 0; i < KLine_num; i++) {
                one_po_three[i] = 100000;
                if (TX[0][i] == 0)
                    continue;
                if (status == "") {
                    if (TX[1][i] > TX[1][iHigh]) { 
                        iHigh = i;
                    }
                    if (TX[2][i] < TX[2][iLow])
                    {
                        iLow = i;
                        iHigh = i;
                    }

                    //high - low >= 5
                    if (TX[1][iHigh] - TX[2][iLow] >= 10 * Math.Sqrt(MK) && iHigh > iLow) {
                        status = "start";
                        iStart = iLow;
                        one_po_three[iStart] = TX[2][iStart];
                        //one_po_three[iHigh] = TX[1][iHigh];
                    }
                    continue;
                }

                //回檔
                if (status == "start" && TX[2][i] < TX[2][iStart] + (TX[1][iHigh] - TX[2][iStart]) * 0.7)
                {
                    status = "back";
                    one_po_three[iHigh] = TX[1][iHigh];
                    int k;
                    for (k = iLow + 1; k < iHigh; k++)
                    {
                        one_po_three[k] = TX[2][iLow] + (float)(TX[1][iHigh] - TX[2][iLow]) * (k - iLow) / (iHigh - iLow);
                    }
                    iLow = i;
                    one_po_three[i] = TX[2][iLow];
                    continue;
                }

                //回檔下探
                if (status == "back" && TX[2][i] < TX[2][iLow])
                {
                    //one_po_three[iLow] = 0;
                    iLow = i;
                    one_po_three[i] = TX[2][iLow];
                    int k;
                    for (k = iHigh + 1; k < iLow; k++)
                    {
                        one_po_three[k] = TX[1][iHigh] - (float)(TX[1][iHigh] - TX[2][iLow]) * (k - iHigh) / (iLow - iHigh);
                    }
                    continue;
                }

                //未回檔破高
                if (status == "start" && TX[1][i] > TX[1][iHigh]) {
                    //one_po_three[iHigh] = 0;
                    iHigh = i;
                    one_po_three[i] = TX[1][iHigh];

                    int k;
                    for (k = iLow + 1; k < iHigh; k++)
                    {
                        one_po_three[k] = TX[2][iLow] + (float)(TX[1][iHigh] - TX[2][iLow]) * (k - iLow) / (iHigh - iLow);
                    }
                    continue;
                }

                //reset
                if (status == "back" && TX[2][i] < TX[2][iStart]) {
                    int k;
                    for (k = iHigh +1; k <= i; k++)
                    {
                        one_po_three[k] = 100000;
                    }
                    status = "";
                    iHigh = i; iLow = i; iStart = -1;
                }

                //折確立
                if (status == "back" && TX[1][i] > TX[1][iHigh]){
                    status = "start";
                    iStart = iLow;
                    one_po_three[iLow] = TX[2][iLow];
                    int k;
                    for (k = iHigh + 1; k < iLow; k++)
                    {
                        one_po_three[k] = TX[1][iHigh] - (float)(TX[1][iHigh] - TX[2][iLow]) * (k - iHigh) / (iLow - iHigh);
                    }
                    iHigh = i;
                    one_po_three[i] = TX[1][iHigh];
                }
            }

            pointPlot.AbscissaData = TX[4];
            pointPlot.DataSource = one_po_three;
            //pointPlot.Color = Color.Lime;
            pointPlot.Marker.Filled = true;
            pointPlot.Marker.Color = Color.Aqua;
            pointPlot.Marker.Size = 5;
            linePlot.AbscissaData = TX[4];
            linePlot.DataSource = one_po_three;
            linePlot.Color = Color.Red;

            CP.OpenData = TX[0];// opens;
            CP.HighData = TX[1];//highs;
            CP.LowData = TX[2];//lows;
            CP.CloseData = TX[3];//closes;
            CP.AbscissaData = TX[4];// times;

            PS.XAxis1.WorldMin = 1;
            PS.XAxis1.WorldMax = KLine_num; //(nKLineList[0].Length - 1) / MK >= KLine_num ? KLine_num : (nKLineList[0].Length - 1) / MK + 2;
            PS.YAxis1.WorldMin = Lowest;
            PS.YAxis1.WorldMax = Highest;

            //PS.YAxis1 = CP.SuggestYAxis();
            //PS.XAxis1 = CP.SuggestXAxis();


            PS.Refresh();
        }
    }
}
