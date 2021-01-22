using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.IO;
using System.Threading;
using System.Reflection;
using NPlot;
using System.Net.Sockets;
using System.Data;
using System.Drawing;


namespace 覆盤
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        object Lock;
        Thread T_Quote, T_GUI;
        TXF.K_data MKdata;
        TXF.K_data DKdata;
        Kline KL_1MK, KL_1DK;
        Technical_analysis.MACD mACD;
        
        TIMES times;
        SOCKET SK;

        private void Form1_Load(object sender, EventArgs e)
        {
         

            KL_1MK = new Kline(plotSurface2D1, plotSurface2D2, 1, 300);
            KL_1DK = new Kline(plotSurface2D3, plotSurface2D4, 1, 40);
            KL_1DK.KP = new candlep(KL_1DK);
            radioButton1.Checked = true;

            load_dayK();
            InitChart();

            //閃電下單
            //stopLimitControl1.InitStopLimitDGV(12500);
        }

      

        private void InitMACDChart() { 
            plotSurface2D5.Clear();
            plotSurface2D5.Add(new Grid()
            {
                HorizontalGridType = Grid.GridType.Fine,
                VerticalGridType = Grid.GridType.Fine
            });

            plotSurface2D5.Add(mACD.LP_DIF);
            plotSurface2D5.Add(mACD.LP_DEM);
            plotSurface2D5.Add(mACD.horizontalLine);
        }

        private void InitChart() {
            if (radioButton1.Checked)
                KL_1MK.KP = new linep(KL_1MK);
            if (radioButton2.Checked)
                KL_1MK.KP = new candlep(KL_1MK);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;
            dateTimePicker1.Enabled = false;
            Lock = new object();
            dataGridView1.DataSource = null;
            stopLimitControl1.Init();

            lock (Lock)
            {
                MKdata = new TXF.K_data();
                DKdata = new TXF.K_data();
                times = new TIMES(int.Parse(comboBox1.Text));
                stopLimitControl1.simu = new Simulation(plotSurface2D1);
                //stopLimitControl1.AddPrice(15000);
                mACD = new Technical_analysis.MACD();
                InitChart();
                InitMACDChart();
            }

            SK = new SOCKET(dateTimePicker1.Value.ToString("MM-dd-yyyy"), "122.99.4.117", 12002, false);
            SK._Load();

            if (T_Quote != null)
                T_Quote.Abort();
            if (T_GUI != null)
                T_GUI.Abort();

            T_Quote = new Thread(quote);
            T_Quote.Start();

            T_GUI = new Thread(gui);
            T_GUI.Start();
        }


        public void WaitReopenData() {
            string contents = "";

            //socket
            textBox1.InvokeIfRequired(() =>
            {
                textBox1.Text = "請稍後 約5秒";
            });

            while (SK.ticks.Count <= 0 && SK.t1.IsAlive)
            {
                textBox1.InvokeIfRequired(() =>
                {
                    textBox1.Text += ".";
                });

                Thread.Sleep(1000);
            }

            textBox1.InvokeIfRequired(() =>
            {
                if (SK.firstMsg != "")
                    textBox1.Text = SK.firstMsg;
            });

            contents = SK.datas.Replace("DONE", "");
            if (contents.Contains("NO DATA") || contents.Contains("無法連線，因為目標電腦拒絕連線"))
            {
                MessageBox.Show("NO DATA");
                comboBox1.InvokeIfRequired(() => {
                    comboBox1.Enabled = true;
                });

                button1.InvokeIfRequired(() =>
                {
                    button1.Enabled = true;
                });

                dataGridView1.InvokeIfRequired(() =>
                {
                    dateTimePicker1.Enabled = true;
                });
                return;
            }
        }

        public void quote() {

            //wait data
            WaitReopenData();

            string date = "";
            dateTimePicker1.InvokeIfRequired(() => {
                date = dateTimePicker1.Value.ToString("yyyy/M/d");
            });

            bool istart = false;
            //foreach (string words in wordss) {
            while (SK.t1.IsAlive || SK.ticks.Count > 0) {
                if (SK.ticks.Count > 0)
                {
                    string words = "";
                    lock (SK.Lock)
                        words = SK.ticks.Dequeue();
                    if (words == null) continue;
                    if (words == "") break;
                    string[] word = words.Split(',');
                    if (word.Length == 1) 
                        continue;
                    if (word[1].Length < 6) return;
                    if (word[1].Substring(0, 6) == "084500") 
                        istart = true;
                    if (int.Parse(word[1].Substring(0, 6)) > 134459 && istart) break;
                    if (!istart) continue;

                    //Run All Ticks
                    RunTicks(word, istart, date);

                    //run
                    int ss = times.tDiff(word[1]);
                    if (ss > 0)
                        Thread.Sleep(ss);
                }
            }
           
            comboBox1.InvokeIfRequired(() => {
                comboBox1.Enabled = true;
            });

            button1.InvokeIfRequired(() =>
            {
                button1.Enabled = true;
            });

            dataGridView1.InvokeIfRequired(() =>
            {
                dateTimePicker1.Enabled = true;
            });
            T_Quote.Abort();
        }

        private void RunTicks(string[] word, bool istart, string date) {

            //start
            if (!istart) return;

            //StopLimit
            stopLimitControl1.StopLimitDGV(word[4], word[2], word[3], word[5]);

            //MK
            MKdata.Run(word[1], word[4], word[5]);
            DKdata.Run(date, word[4], word[5]);
            mACD.macd(MKdata.klist);

            //MIT
            List<string> Orders = stopLimitControl1.simu.MITToOrder(word[1], word[2], word[3], word[4]);
            List<string> Deals = stopLimitControl1.simu.DealInfo(word[1], word[2], word[3], word[4]);
            if (Deals != null && Deals.Count > 0)
            {
                //Draw MIT 
                stopLimitControl1.simu.MatList[stopLimitControl1.simu.MatList.Count - 1].iTIME = MKdata.klist.Count;
                KL_1MK.DrawAllLpp(stopLimitControl1.simu);
                KL_1MK.DrawAllHL(stopLimitControl1.simu);

                //MatList
                dataGridView1.InvokeIfRequired(() =>
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = stopLimitControl1.simu.MatList;
                });

                //Delete MIT DR
                stopLimitControl1.DeleteMIT(Orders);

                //Delete Ord DR
                stopLimitControl1.DeleteOrder(Deals);
            }

        }



        public void gui() {
            bool close = false;
            while (true)
            {
                Thread.Sleep(1);
                
                lock (Lock)
                {


                    if (MKdata.klist.Count > 0)
                    {

                        //DGV
                        stopLimitControl1.AddPrice((int)MKdata.klist[MKdata.klist.Count - 1].close);

                        //StopLimit
                        stopLimitControl1.gui(MKdata.klist);

                        //time
                        if (MKdata.klist != null)
                        {
                            label4.InvokeIfRequired(() =>
                            {
                                label4.Text = MKdata.klist[MKdata.klist.Count - 1].time.Substring(0, 2).ToString() + ":" +
                                MKdata.klist[MKdata.klist.Count - 1].time.Substring(2, 2).ToString() + ":" +
                                MKdata.klist[MKdata.klist.Count - 1].time.Substring(4, 2).ToString();
                            });
                        }

                        //high - low 
                        label13.InvokeIfRequired(() =>
                        {
                            label13.Text = (DKdata.klist[DKdata.klist.Count - 1].high - DKdata.klist[DKdata.klist.Count - 1].low).ToString();
                        });

                        //Qty
                        label3.InvokeIfRequired(() =>
                        {
                            label3.Text = stopLimitControl1.simu.Qty("", "").ToString();
                        });

                        //Profit
                        label9.InvokeIfRequired(() =>
                        {
                            label9.Text = stopLimitControl1.simu.Profit(MKdata.klist[MKdata.klist.Count - 1].close.ToString());
                        });

                        //Entries
                        label11.InvokeIfRequired(() =>
                        {
                            label11.Text = stopLimitControl1.simu.Entries().ToString();
                        });

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
                        KL_1MK.KP.refreshK(MKdata.klist);
                        KL_1DK.KP.refreshK(DKdata.klist);
                    }
                }

                string time = "";
                label4.InvokeIfRequired(() =>
                {
                    time = label4.Text.Replace(":", string.Empty);
                });

                if (time == "") 
                    return;
                else if (time.Substring(0, 4) == "1100") {
                    textBox1.InvokeIfRequired(() =>
                    {
                        textBox1.Text = "After 11:00, it can rise without large volume.";
                    });
                    
                }

                if (close) T_GUI.Abort();
                if (!T_Quote.IsAlive) close = true;

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label12.Text = "5AVG";
            load_dayK();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Lock = new object();
            lock (Lock)
            {
                InitChart();
                if (stopLimitControl1.simu != null)
                {
                    KL_1MK.DrawAllLpp(stopLimitControl1.simu);
                    KL_1MK.DrawAllHL(stopLimitControl1.simu);
                }
            }
            if (T_GUI != null && !T_GUI.IsAlive)
            {
                T_GUI = new Thread(gui);
                T_GUI.Start();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Lock = new object();
            lock (Lock)
            {
                InitChart();
                if (stopLimitControl1.simu != null)
                {
                    KL_1MK.DrawAllLpp(stopLimitControl1.simu);
                    KL_1MK.DrawAllHL(stopLimitControl1.simu);
                }
            }
            if (T_GUI != null && !T_GUI.IsAlive)
            {
                T_GUI = new Thread(gui);
                T_GUI.Start();
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            stopLimitControl1.simu.DeleteAllMIT();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                tabControl1.Visible = !tabControl1.Visible;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (T_Quote != null)
                T_Quote.Abort();
            if (T_GUI != null)
                T_GUI.Abort();

            if (SK != null)
                SK.t1.Abort();
        }

        private void load_dayK() {
            return;
            DKdata.klist = new List<TXF.K_data.K>();
            using (StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\日K.TXT")) {
                string words = sr.ReadLine();
                while ((words = sr.ReadLine()) != null) {
                    string[] word = words.Split(',');
                    if (Convert.ToDateTime(word[0]).Date >= dateTimePicker1.Value.Date)
                    {
                        if (DKdata.klist.Count < 1) return;
                        float avgDay5 = 0;
                        int i;
                        for (i = 1; i < 6; i++)
                        {
                            avgDay5 += DKdata.klist[DKdata.klist.Count - i].high - DKdata.klist[DKdata.klist.Count - i].low;
                        }
                        avgDay5 /= 5;
                        label12.Text = avgDay5.ToString();
                        break;
                    }
                    DKdata.klist.Add(new TXF.K_data.K("", 0));
                    DKdata.klist[DKdata.klist.Count - 1].ktime = word[0];
                    DKdata.klist[DKdata.klist.Count - 1].open = float.Parse(word[1]);
                    DKdata.klist[DKdata.klist.Count - 1].high = float.Parse(word[2]);
                    DKdata.klist[DKdata.klist.Count - 1].low = float.Parse(word[3]);
                    DKdata.klist[DKdata.klist.Count - 1].close = float.Parse(word[4]);
                    DKdata.klist[DKdata.klist.Count - 1].qty = uint.Parse(word[12]);
                }
            }
            KL_1DK.KP.refreshK(DKdata.klist);
        }
    }
    //擴充方法
    public static class Extension
    {
        //非同步委派更新UI
        public static void InvokeIfRequired(
            this Control control, MethodInvoker action)
        {
            try
            {
                if (control.InvokeRequired)//在非當前執行緒內 使用委派
                {
                    control.Invoke(action);
                }
                else
                {
                    action();
                }
            }
            catch (Exception ex) { }
        }
    }
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this object dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
