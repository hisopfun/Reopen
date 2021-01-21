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
    public partial class StopLimitControl : UserControl
    {
        public StopLimitControl()
        {
            InitializeComponent();
        }
        public DataTable dt;
        private int Ref, upPri, dnPri, lastPri;
        public string lastTime;
        public object Lock;
        public Simulation simu;
        public bool start = false;

        public void Init() {
            InitVar();
            InitStopLimitDGV();
        }
        public void InitVar()
        {
            Ref = 0;
            upPri = 0;
            dnPri = 0;
            lastPri = 0;
            lastTime = "";
            start = false;
            Lock = new object();
        }
        public void InitStopLimitDGV()
        {
            DGV_StopLimit.DataSource = null;
            ExtensionMethods.DoubleBuffered(DGV_StopLimit, true);


            //Add DataColumn
            this.dt = new DataTable("QuoteTable");
            DataColumn Col1 = new DataColumn("nPrice", System.Type.GetType("System.String"));
            DataColumn Col6 = new DataColumn("SBid", System.Type.GetType("System.String"));
            DataColumn Col4 = new DataColumn("LBid", System.Type.GetType("System.String"));
            DataColumn Col2 = new DataColumn("Bid", System.Type.GetType("System.String"));
            DataColumn Col0 = new DataColumn("Price", System.Type.GetType("System.String"));
            DataColumn Col3 = new DataColumn("Ask", System.Type.GetType("System.String"));
            DataColumn Col5 = new DataColumn("LAsk", System.Type.GetType("System.String"));
            DataColumn Col7 = new DataColumn("SAsk", System.Type.GetType("System.String"));
            this.dt.Columns.Add(Col1);
            this.dt.Columns.Add(Col6);
            this.dt.Columns.Add(Col4);
            this.dt.Columns.Add(Col2);
            this.dt.Columns.Add(Col0);
            this.dt.Columns.Add(Col3);
            this.dt.Columns.Add(Col5);
            this.dt.Columns.Add(Col7);
            this.dt.PrimaryKey = new DataColumn[] { this.dt.Columns["nPrice"] };
            DGV_StopLimit.DataSource = this.dt;

            //visible
            DGV_StopLimit.Columns[0].Visible = false;
            DGV_StopLimit.Columns[3].Visible = false;
            DGV_StopLimit.Columns[5].Visible = false;

            //Not Allow Sort
            DGV_StopLimit.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            //Buy Limit
            DGV_StopLimit.Columns[1].HeaderCell.Style.BackColor = Color.FromArgb(255, 192, 192);
            DGV_StopLimit.Columns[2].HeaderCell.Style.BackColor = Color.FromArgb(255, 192, 192);

            //Sell Limit
            DGV_StopLimit.Columns[6].HeaderCell.Style.BackColor = Color.FromArgb(192, 255, 192);
            DGV_StopLimit.Columns[7].HeaderCell.Style.BackColor = Color.FromArgb(192, 255, 192);

            //Pri
            DGV_StopLimit.Columns[4].HeaderCell.Style.BackColor = Color.FromArgb(255, 255, 192);

            //Adjust width
            DGV_StopLimit.Columns[1].Width = 35;
            DGV_StopLimit.Columns[2].Width = 35;
            DGV_StopLimit.Columns[3].Width = 35;
            DGV_StopLimit.Columns[4].Width = 80;
            DGV_StopLimit.Columns[5].Width = 35;
            DGV_StopLimit.Columns[6].Width = 35;
            DGV_StopLimit.Columns[7].Width = 35;
        }

        public void AddPrice(int nRef)
        {

            //Add Price DataRow

            DGV_StopLimit.InvokeIfRequired(() =>
            {
                if (start == false)
                {
                    DataRow DR;
                    upPri = Convert.ToInt32(Math.Floor(nRef * 1.1));
                    dnPri = Convert.ToInt32(Math.Ceiling(nRef * 0.9));
                    int i;
                    for (i = upPri; i >= dnPri; i--)
                    {
                        DR = this.dt.NewRow();
                        lock (Lock)
                        {
                            DR["nPrice"] = i.ToString();
                            DR["Price"] = i.ToString();
                        }
                        this.dt.Rows.Add(DR);
                    }
                }
            });


            start = true;
        }

        private void DGV_StopLimit_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string Pri = "";
            Pri = DGV_StopLimit[0, e.RowIndex].Value.ToString();

            

            if (e.Button == MouseButtons.Left)
            {
                if (e.ColumnIndex != 4)
                {
                    //if (DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                    //    DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value =
                    //        (float.Parse(DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value.ToString()) + 1).ToString();
                    //else
                    //    DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value = "1";

                    //Stop Buy
                    if (e.ColumnIndex == 1)
                    {
                        simu.MIT(lastTime, "TXF", "B", "1", Pri, lastPri.ToString());
                    }

                    //Stop Sell
                    if (e.ColumnIndex == 7)
                    {
                        simu.MIT(lastTime, "TXF", "S", "1", Pri, lastPri.ToString());
                    }

                    //Limit Buy 
                    if (e.ColumnIndex == 2) {
                        simu.Order(lastTime, "TXF", "B", "1", Pri);
                    }

                    //Limit Buy 
                    if (e.ColumnIndex == 6)
                    {
                        simu.Order(lastTime, "TXF", "S", "1", Pri);
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex == 4)
                {
                    if (lastPri != 0)
                    {
                        ScrollingTo(DGV_StopLimit, upPri - lastPri);
                        return;
                    }
                }

                //Clear Cell Centent
                DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value = "";

                //Delete Limit Buy
                if (e.ColumnIndex == 2) {
                    simu.DeleteNotMat(simu.OrdList, "B", Pri);
                }

                //Delete Limit Sell
                if (e.ColumnIndex == 6)
                {
                    simu.DeleteNotMat(simu.OrdList, "S", Pri);
                }

                //Delete Stop Buy
                if (e.ColumnIndex == 1)
                {
                    simu.DeleteNotMat(simu.MITList, "B", Pri);
                }

                //Delete Stop Sell
                if (e.ColumnIndex == 7)
                {
                    simu.DeleteNotMat(simu.MITList, "S", Pri);
                }
            }
        }

        private void DGV_StopLimit_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string Pri = "";
            Pri = DGV_StopLimit[0, e.RowIndex].Value.ToString();



            if (e.Button == MouseButtons.Left)
            {
                if (e.ColumnIndex != 4)
                {
                    //if (DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                    //    DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value =
                    //        (float.Parse(DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value.ToString()) + 1).ToString();
                    //else
                    //    DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value = "1";

                    //Stop Buy
                    if (e.ColumnIndex == 1)
                    {
                        simu.MIT(lastTime, "TXF", "B", "1", Pri, lastPri.ToString());
                    }

                    //Stop Sell
                    if (e.ColumnIndex == 7)
                    {
                        simu.MIT(lastTime, "TXF", "S", "1", Pri, lastPri.ToString());
                    }

                    //Limit Buy 
                    if (e.ColumnIndex == 2)
                    {
                        simu.Order(lastTime, "TXF", "B", "1", Pri);
                    }

                    //Limit Buy 
                    if (e.ColumnIndex == 6)
                    {
                        simu.Order(lastTime, "TXF", "S", "1", Pri);
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex == 4)
                {
                    if (lastPri != 0)
                    {
                        ScrollingTo(DGV_StopLimit, upPri - lastPri);
                        return;
                    }
                }

                //Clear Cell Centent
                DGV_StopLimit[e.ColumnIndex, e.RowIndex].Value = "";

                //Delete Limit Buy
                if (e.ColumnIndex == 2)
                {
                    simu.DeleteNotMat(simu.OrdList, "B", Pri);
                }

                //Delete Limit Sell
                if (e.ColumnIndex == 6)
                {
                    simu.DeleteNotMat(simu.OrdList, "S", Pri);
                }

                //Delete Stop Buy
                if (e.ColumnIndex == 1)
                {
                    simu.DeleteNotMat(simu.MITList, "B", Pri);
                }

                //Delete Stop Sell
                if (e.ColumnIndex == 7)
                {
                    simu.DeleteNotMat(simu.MITList, "S", Pri);
                }
            }
        }

        public void gui(List<TXF.K_data.K> mk) {

            //Display MatPri
            //DGV_StopLimit.InvokeIfRequired(() =>
            //{
                if (upPri != 0 && dnPri != 0)
                {
                    if (lastPri != 0)
                        DGV_StopLimit[4, upPri - lastPri].Style.BackColor = Color.White;
                    lastPri = (int)mk[mk.Count - 1].close;
                    lastTime = mk[mk.Count - 1].time;

                    DGV_StopLimit[4, upPri - lastPri].Style.BackColor = Color.Pink;
                }

            //});

            //Run MIT and Order
            RUN_MIT_Limit();
        }

        public void DeleteMIT(List<string> Orders) {
            foreach (string ord in Orders) {
                string price = ord.Split(',')[2];
                string BS = (ord.Split(',')[1] == "B") ? "SBid" : "SAsk";
                DataRow DR = this.dt.Rows.Find(price);
                lock (Lock)
                    DR[BS] = "";
            }
        }

        public void DeleteOrder(List<string> Deals)
        {
            foreach (string mat in Deals)
            {
                string Pri = mat.Split(',')[2];
                if (Pri == "M") continue;
                string price = mat.Split(',')[2];
                string BS = (mat.Split(',')[1] == "B") ? "LBid" : "LAsk";
                DataRow DR = this.dt.Rows.Find(price);
                lock(Lock)
                    DR[BS] = "";
            }
        }

        public void RUN_MIT_Limit() {
            DataRow DR;
            List<string> seen = new List<string>();
            foreach (Simulation.match ord in simu.OrdList) {
                string Pri = ord.Price;
                if (Pri == "M") continue;
                string BS = (ord.BS == "B") ? "LBid" : "LAsk";
                DR = this.dt.Rows.Find(ord.Price);

                //check price whether seen or not
                string str = seen.Find(x => x.Contains(ord.Price));
                if (str == ord.Price)
                {
                    lock(Lock)
                        DR[BS] = (int.Parse(DR[BS].ToString()) + 1).ToString();
                }
                else
                {
                    lock (Lock)
                        DR[BS] = "1";
                    seen.Add(ord.Price);
                }
            }

            seen = new List<string>();
            foreach (Simulation.match mit in simu.MITList)
            {
                string BS = (mit.BS == "B") ? "SBid" : "SAsk";
                DR = this.dt.Rows.Find(mit.Price);
                
                //check price whether seen or not
                string str = seen.Find(x => x.Contains(mit.Price));
                if (str == mit.Price)
                {
                    lock (Lock)
                        DR[BS] = (int.Parse(DR[BS].ToString()) + 1).ToString();
                }
                else
                {
                    lock (Lock)
                        DR[BS] = "1";
                    seen.Add(mit.Price);
                }
            }
        }

        public void StopLimitDGV(string nMatPri, string nBid, string nAsk, string nQty)
        {
            DataRow DR = this.dt.Rows.Find(nMatPri);
            DGV_StopLimit.Enabled = true;
            if (DR != null)
            {
                lock (Lock)
                    DR["Price"] = $"{nMatPri} ({nQty})";

                DR.EndEdit();
                DR.AcceptChanges();
            }
        }

        private static void ScrollingTo(DataGridView view, int rowToShow)
        {
            if (rowToShow >= 0 && rowToShow < view.RowCount)
            {
                var countVisible = view.DisplayedRowCount(false);
                var firstVisible = view.FirstDisplayedScrollingRowIndex;
                int mid = Math.Max(0, rowToShow - countVisible / 2);
                view.FirstDisplayedScrollingRowIndex = mid;
            }
        }
    }
}
