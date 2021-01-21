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
        public NPlot.Windows.PlotSurface2D PS = null;
        public class match
        {
            public string Time { get; }
            public string FutNo { get;  }
            public string BS { get; }
            public string Qty { get;  }
            public string Price { get;  }
            public string MITLabel;
            public int iTIME = 0;

            //public NPlot.LinePlot LineChart = new NPlot.LinePlot();
            public NPlot.HorizontalLine horizontalLine;
 
            public match(string nMatTime, string nFutNo, string nBSCode, string nQty, string nPrice, string nMITLabel)
            {
                Time = nMatTime;
                FutNo = nFutNo;
                BS = nBSCode;
                Qty = nQty;
                Price = nPrice;
                MITLabel = nMITLabel;
                
            }
        }

        public Simulation(NPlot.Windows.PlotSurface2D nPS) {
            PS = nPS;
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
            MITList[MITList.Count - 1].horizontalLine = new HorizontalLine(int.Parse(nMITPri), (nBSCode == "B") ? System.Drawing.Color.Red : System.Drawing.Color.Green);
            PS.Add(MITList[MITList.Count - 1].horizontalLine);
            return true;
        }
        public List<string> MITToOrder(string nMatTime, string nBid, string nAsk, string nMatPri)
        {
            List<string> Order = new List<string>();
            //MIT -> order
            int i;
            for (i = 0; i < MITList.Count; i++)
            {
                if (int.Parse(nMatPri) >= int.Parse(MITList[i].Price) && MITList[i].MITLabel.Equals(">=") ||
                    int.Parse(nMatPri) <= int.Parse(MITList[i].Price) && MITList[i].MITLabel.Equals("<="))
                {

                    OrdList.Add(new match(nMatTime, MITList[i].FutNo, MITList[i].BS, MITList[i].Qty, "M", ""));
                    Order.Add(nMatPri + "," + MITList[i].BS + "," + MITList[i].Price);
                    DeleteNotMat(MITList, MITList[i].BS, MITList[i].Price);
                    //MITList.Remove(MITList[i]);
                    i--;
                }
            }
            return Order;
        }

        public List<string> DealInfo(string nMatTime, string nBid, string nAsk, string nMatPri)
        {
            List<string> deal = new List<string>();

            int i;
            //order -> deal
            for (i = 0; i < OrdList.Count; i++)
            {
                if (OrdList[i].BS.Equals("B") && OrdList[i].Price.Equals("M") ||
                    OrdList[i].BS.Equals("B") && int.Parse(nAsk) <= int.Parse(OrdList[i].Price))
                {
                    MatList.Add(new match(nMatTime, OrdList[i].FutNo, OrdList[i].BS, OrdList[i].Qty, nAsk, ""));
                    deal.Add(nAsk + ",B," + OrdList[i].Price);
                    DeleteNotMat(OrdList, OrdList[i].BS, OrdList[i].Price);
                    //OrdList.Remove(OrdList[i]);
                }
                else if (OrdList[i].BS.Equals("S") && OrdList[i].Price.Equals("M") ||
                         OrdList[i].BS.Equals("S") && int.Parse(nBid) >= int.Parse(OrdList[i].Price))
                {
                    MatList.Add(new match(nMatTime, OrdList[i].FutNo, OrdList[i].BS, OrdList[i].Qty, nBid, ""));
                    deal.Add(nBid + ",S," + OrdList[i].Price);
                    DeleteNotMat(OrdList, OrdList[i].BS, OrdList[i].Price);
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
                int dif = (int.Parse(nMatPri) - int.Parse(mat.Price)) * (mat.BS.Equals("S") ? -1 : 1);
                profit += (dif * int.Parse(mat.Qty));
            }
            return profit.ToString();
        }

        public int Entries()
        {

            int B_Qty = 0, S_Qty = 0;

            foreach (match mat in MatList)
            {
                B_Qty += (int.Parse(mat.Qty) * (mat.BS.Equals("B") ? 1 : 0));
                S_Qty += (int.Parse(mat.Qty) * (mat.BS.Equals("S") ? 1 : 0));
            }
            return Math.Max(B_Qty, S_Qty);
        }

        public int Qty(string nBS, string nPrice)
        {

            int Qty = 0;

            //All OI Qty
            if (nBS == "" && nPrice == "")
            {
                foreach (match mat in MatList)
                {
                    Qty += (int.Parse(mat.Qty) * (mat.BS.Equals("B") ? 1 : -1));
                }
                return Qty;
            }

            //Price OI Qty
            List<match> OIList = OI();
            foreach (match mat in OIList)
            {
                if (mat.BS.Equals(nBS) && mat.Price.Equals(nPrice))
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
                if (!MatL[0].BS.Equals(MatL[index].BS))
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
                if (mat.BS.Equals(nBS) && mat.Price.Equals(nPrice))
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

        public bool DeleteNotMat(List<match> NotMatList, string nBSCode, string nPrice)
        {
            bool change = false;
            int i;
            for (i = 0; i < NotMatList.Count; i++)
            {
                if (NotMatList[i].BS.Equals(nBSCode) && NotMatList[i].Price.Equals(nPrice))
                {
                    PS.Remove(NotMatList[i].horizontalLine, false);

                    //PS.Remove(NotMatList[i].LineChart, false);
                    NotMatList.Remove(NotMatList[i]);
                    i--;
                    change = true;
                    break;
                }
            }
            return change;
        }


        public void DeleteAllMIT()
        {
            while(MITList.Count > 0)
            {
                DeleteNotMat(MITList, MITList[0].BS, MITList[0].Price);
            }
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
}
