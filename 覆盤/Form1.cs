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
        Kline KL_1DK;
        string date;
        
        TIMES times;
        SOCKET SK;
        //TickEncoder tickGUI;

        strategy plan10M, planMACD, planOSC, planNightReverse;

        public void write(string path, string content) {
            using (FileStream fs = new FileStream(path, FileMode.Append)) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    sw.Write(content);
                }
            }
        }

        private void DrawMat(strategy plan,  string[] word, string BS) {
            if (BS != "X")
            {
                if (plan.dealPrice == 0)
                    plan.dealPrice = int.Parse(word[4]);
                else
                {
                    if (plan.last_bs == 0)
                        plan.dealPrice = plan.dealPrice + (int)plan.benefit;
                    else
                        plan.dealPrice = plan.dealPrice - (int)plan.benefit;
                }
                //if (plan10M.benefit != 0)
                //    stopLimitControl1.simu.MatList.Add(new Simulation.match(word[1], "TXF", BS, "1", (int.Parse(word[4]) + plan10M.benefit).ToString(), ""));
                //else
                stopLimitControl1.simu.MatList.Add(new Simulation.match(word[1], "TXF", BS, "1", plan.dealPrice.ToString(), ""));
                stopLimitControl1.simu.MatList[stopLimitControl1.simu.MatList.Count - 1].iTIME = MKdata.klist.Count;
                chartControl1.KL_1MK.DrawAllLpp(stopLimitControl1.simu);
            }
            if (plan.bs != -1 && BS != "X")
            {

                turn turn = new turn();
                Point x = new Point();
                if (BS == "B")
                    x = turn.BuyTurn(MKdata.klist);
                else if (BS == "S")
                    x = turn.SellTurn(MKdata.klist);

                plan.mitPrice = (int)x.price + ((plan.bs == 0) ? -1 : 1) * 3;

                if (x != null)
                {
                    NPlot.LabelPointPlot lpp = new NPlot.LabelPointPlot();
                    lpp.DataSource = new float[] { x.price };
                    lpp.AbscissaData = new int[] { x.iTime };
                    lpp.TextData = new string[] { "".ToString() };
                    lpp.LabelTextPosition = LabelPointPlot.LabelPositions.Below;
                    lpp.Marker.Size = 10;
                    lpp.Marker.Filled = true;
                    lpp.Marker.Color = System.Drawing.Color.Blue;
                    lpp.Marker.Type = Marker.MarkerType.Cross1;

                    chartControl1.KL_1MK.PS.Add(lpp);
                }
            }
        }

        public void OnTickEncoded(object sender, TicksEventArgs e)
        {
            string[] word = e.tick.Split(',');

            //Avoid PreOpen
            if (int.Parse(word[1].Substring(0, 4)) >= 0830 &&
                int.Parse(word[1].Substring(0, 4)) < 0845 ||
                int.Parse(word[1].Substring(0, 4)) >= 1450 &&
                int.Parse(word[1].Substring(0, 4)) < 1500)
                return;

            //StopLimit
            stopLimitControl1.StopLimitDGV(word[4], word[2], word[3], word[5]);

            //MK
            MKdata.Run(word[1], word[4], word[5]);

            //DK
            DKdata.Run("000000", word[4], word[5]);

            //MACD
            chartControl1.KL_1MK.mACD.macd(MKdata.klist);
            KL_1DK.mACD.macd(DKdata.klist);


            ////plan10M
            //string BS = plan10M.plan10M_implement(int.Parse(word[4]) - Convert.ToInt32(MKdata.klist[0].close), int.Parse(word[4]), word[1]);
            //DrawMat(plan10M, word, BS);

            ////planNightReverse
            //string BS = planNightReverse.planNight_Reverse_implement(MKdata.klist, int.Parse(word[4]), word[1]);
            
            //stopLimitControl1.simu.Limit(word[1], "TXF", BS, "1", "M");

            ////planMACD
            //string BS = planMACD.planMACD(chartControl1.KL_1MK.mACD, int.Parse(word[4]), word[1]);
            //stopLimitControl1.simu.Limit(word[1], "TXF", BS, "1", "M");

            //planOSC
            //string BS = planMACD.planOSC(chartControl1.KL_1MK.mACD, int.Parse(word[4]), word[1]);
            //stopLimitControl1.simu.Limit(word[1], "TXF", BS, "1", "M");

            //draw
            MITandLIT(word);

        }

        public void MITandLIT(string[] word) { 
            //Order
            List<string> Limits = stopLimitControl1.simu.MITToLimit(word[1], word[2], word[3], word[4]);
            List<string> Deals = stopLimitControl1.simu.DealInfo(word[1], word[2], word[3], word[4]);
            if (Deals != null && Deals.Count > 0)
            {
                //Draw MIT 
                stopLimitControl1.simu.MatList[stopLimitControl1.simu.MatList.Count - 1].iTIME = MKdata.klist.Count;
                chartControl1.KL_1MK.DrawAllLpp(stopLimitControl1.simu);
                chartControl1.KL_1MK.DrawAllHL(stopLimitControl1.simu);

                //Delete MIT DR
                stopLimitControl1.DeleteMIT(Limits);

                //Delete Lim DR
                stopLimitControl1.DeleteLimit(Deals);

                ////MatList
                dataGridView1.InvokeIfRequired(() =>
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = stopLimitControl1.simu.MatList;
                });
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            KL_1DK = new Kline(plotSurface2D3, plotSurface2D4, plotSurface2D1, 1, 40);
            KL_1DK.KP = new CandleP(KL_1DK);

            load_dayK();
            radioButton1.Checked = true;
            chartControl1.InitChart(Kind.Line);
            tabControl1.TabPages.Remove(tabPage3);
            textBox1.Text = "";
        }


        public void start(string date) {
            //comboBox1.Enabled = false;
            //button1.Enabled = false;
            //dateTimePicker1.Enabled = false;
            Lock = new object();
            dataGridView1.InvokeIfRequired(() =>
            {
                dataGridView1.DataSource = null;
            });

            stopLimitControl1.Init();

            lock (Lock)
            {
                MKdata = new TXF.K_data();
                DKdata = new TXF.K_data();
                comboBox1.InvokeIfRequired(() => { 
                    times = new TIMES(int.Parse(comboBox1.Text));
                });
                stopLimitControl1.simu = new Simulation(chartControl1.plotSurface2D1);
                chartControl1.KL_1MK.mACD = new Technical_analysis.MACD(chartControl1.KL_1MK.KLine_num);
                Kind kind = Kind.Both;
                radioButton1.InvokeIfRequired(() =>
                {
                    if (radioButton1.Checked)
                        kind = Kind.Line;
                });
                radioButton2.InvokeIfRequired(() =>
                {
                    if (radioButton2.Checked)
                        kind = Kind.Candle;
                });
                radioButton3.InvokeIfRequired(() =>
                {
                    if (radioButton3.Checked)
                        kind = Kind.Both;
                });
                chartControl1.InitChart(kind);
                //chartControl1.KL_1MK.InitMACDChart(chartControl1.KL_1MK.mACD = new Technical_analysis.MACD());

                plan10M = new strategy("plan10M", 0, 0, "100000", "134459", 20);
                plan10M.date = date;
                planMACD = new strategy("planMACD", 0, 0, "093000", "134459", 20);
                planOSC = new strategy("planOSC", 0, 0, "093000", "134459", 0);
                planNightReverse = new strategy("planNightReverse", 0, 0, "000000", "235959", 10);
                planNightReverse.date = date;
            }

            checkBox1.InvokeIfRequired(() =>
            {
                if (checkBox1.Checked)
                    SK = new SOCKET(date, "127.0.0.1", 12002, checkBox1.Checked);
                else
                    SK = new SOCKET(date, "122.99.4.117", 12002, checkBox1.Checked);
            });

            SK.TE.TickEncoded += new TickEncoder.TickEncoderEventHandler( OnTickEncoded);


            if (T_Quote != null)
                T_Quote.Abort();
            if (T_GUI != null)
                T_GUI.Abort();

            if (!checkBox1.Checked)
            {
                T_Quote = new Thread(quote);
                T_Quote.Start();
            }

            T_GUI = new Thread(gui);
            T_GUI.Start();
        }

        public void manyDay()
        {
            //int i = 2;
            //while (i > 0)
            //{

            //    if (T_Quote == null || !T_Quote.IsAlive)
            //    {
            //        dateTimePicker1.InvokeIfRequired(() =>
            //        {
            //            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
            //        });
            start(dateTimePicker1.Value.ToString("MM-dd-yyyy"));
            date = dateTimePicker1.Value.ToString("MM-dd-yyyy");
            //    }
            //    else
            //    {
            //        Thread.Sleep(100);
            //        continue;
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(manyDay);
            th.Start();
        }


        public void WaitReopenData() {
            string contents = "";

            //socket
            textBox1.InvokeIfRequired(() =>
            {
                textBox1.Text = "請稍後 約5秒";
            });

            //wait
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
                textBox1.InvokeIfRequired(() =>
                {
                    textBox1.Text = "NO DATA";
                });
                
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
            
            bool istart = false;
           
            while (SK.t1.IsAlive || SK.ticks.Count > 0) {
                if (SK.ticks.Count > 0)
                {
                    string words = "";
                    lock (SK.Lock)
                        words = SK.ticks.Dequeue();
                    if (words == null) continue;
                    if (words == "") break;
                    string[] word = words.Split(',');

                    if (word.Length == 1)   continue;
                    if (word[1].Length < 6) return;


                    //AM
                    if (int.Parse(word[1].Substring(0, 4)) < 0845 ||
                        int.Parse(word[1].Substring(0, 4)) > 1345)
                        continue;

                    ////Night
                    //if (int.Parse(word[1].Substring(0, 4)) >= 0830 &&
                    //    int.Parse(word[1].Substring(0, 4)) <= 1345)
                    //    continue;

                    if (word[1].Substring(0, 4) == "1500")
                        continue;
                    //Avoid PreOpen
                    if (int.Parse(word[1].Substring(0, 4)) >= 0830 &&
                        int.Parse(word[1].Substring(0, 4)) < 0845 ||
                        int.Parse(word[1].Substring(0, 4)) >= 1450 &&
                        int.Parse(word[1].Substring(0, 4)) < 1500)
                        continue;

                    if (word[1].Substring(0, 4) == "2230") 
                        continue;

                    //Run All Ticks
                    SK.TE.Encode(words);

                    //time interval
                    int ss = times.tDiff(word[1]);
                    if (ss > 0)
                        Thread.Sleep(ss);

                    if (SK.ticks.Count <= 0)
                        continue;
                }
            }

            //Screenshot.CaptureMyScreen();

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

        //大量K畫線

     

        public void gui() {
            bool close = false;
            while (true)
            {
                Thread.Sleep(50);
                
                lock (Lock)
                {


                    if (MKdata.klist.Count > 0 && DKdata.klist.Count >0)
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
                        if (DKdata.klist != null && DKdata.klist.Count > 0)
                        {
                            label13.InvokeIfRequired(() =>
                            {
                                label13.Text = (DKdata.klist[DKdata.klist.Count - 1].high - DKdata.klist[DKdata.klist.Count - 1].low).ToString();
                            });
                        }


                        //Qty
                        label3.InvokeIfRequired(() =>
                        {
                            label3.Text = stopLimitControl1.simu.Qty().ToString();
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

                        //MACD
                        chartControl1.KL_1MK.Adjust_MACD(chartControl1.KL_1MK.mACD);

                        ////MatList
                        //dataGridView1.InvokeIfRequired(() =>
                        //{
                        //    dataGridView1.DataSource = null;
                        //    dataGridView1.DataSource = stopLimitControl1.simu.MatList;
                        //});


                        chartControl1.KL_1MK.KP.refreshK(MKdata.klist);
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
                if (T_Quote!= null && !T_Quote.IsAlive) close = true;

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
                chartControl1.InitChart(Kind.Line);
                if (stopLimitControl1.simu != null)
                {
                    chartControl1.KL_1MK.DrawAllLpp(stopLimitControl1.simu);
                    chartControl1.KL_1MK.DrawAllHL(stopLimitControl1.simu);
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
                chartControl1.InitChart(Kind.Candle);
                if (stopLimitControl1.simu != null)
                {
                    chartControl1.KL_1MK.DrawAllLpp(stopLimitControl1.simu);
                    chartControl1.KL_1MK.DrawAllHL(stopLimitControl1.simu);
                }
            }
            if (T_GUI != null && !T_GUI.IsAlive)
            {
                T_GUI = new Thread(gui);
                T_GUI.Start();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Lock = new object();
            lock (Lock)
            {
                chartControl1.InitChart(Kind.Both);
                if (stopLimitControl1.simu != null)
                {
                    chartControl1.KL_1MK.DrawAllLpp(stopLimitControl1.simu);
                    chartControl1.KL_1MK.DrawAllHL(stopLimitControl1.simu);
                }
            }
            if (T_GUI != null && !T_GUI.IsAlive)
            {
                T_GUI = new Thread(gui);
                T_GUI.Start();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
                tabControl1.Visible = !tabControl1.Visible;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            times.speed = int.Parse(comboBox1.Text);
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void contextMenuStrip4_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

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
