using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using NPlot;

namespace 覆盤
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        object Lock = new object();
        Thread T_Quote, T_GUI;
        TXF.K_data MKdata = new TXF.K_data();
        TXF.K_data DKdata = new TXF.K_data();
        Kline k1, k2;
        Technical_analysis.MACD mACD = new Technical_analysis.MACD();
        Simulation simu = new Simulation();
        TIMES times = new TIMES(1);


        private void Form1_Load(object sender, EventArgs e)
        {
            k1 = new Kline(plotSurface2D1, plotSurface2D2, 1, 300);
            k2 = new Kline(plotSurface2D3, plotSurface2D4, 1, 40);
            k2.KP = new candlep(k2);
            radioButton1.Checked = true;

            dateTimePicker1.Value = RandomDate.RandomSelectDate();
            load_dayK();

            Init();
            //dataGridView1.DataSource = simu.MatList;
        }

        private void Init() {
            if (radioButton1.Checked)
                k1.KP = new linep(k1);
            if (radioButton2.Checked)
                k1.KP = new candlep(k1);

            plotSurface2D5.Clear();
            plotSurface2D5.Add(mACD.LP_DIF);
            plotSurface2D5.Add(mACD.LP_DEM);
            plotSurface2D5.Add(mACD.horizontalLine);
            //plotSurface2D1.Add(ema3.LP_EMA);
            plotSurface2D1.Add(mACD.EMA1.LP_EMA);
            plotSurface2D1.Add(mACD.EMA2.LP_EMA);

            k1.KP.refreshK(MKdata.kdata);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        


        public void quote() {
            if (!RandomDate.CheckDate(dateTimePicker1.Value)) {

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
                MessageBox.Show("No Data");

                return;
            }
            using (StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\TXF\\" + dateTimePicker1.Value.ToString("MM-dd-yyyy") +"TXF.TXT"))
            {
                string[] wordss = sr.ReadToEnd().Split('\n');
                string time = "";
                bool istart = false;

                //matlistBox
                //listBox1.InvokeIfRequired(() => {
                //    ExtensionMethods.DoubleBuffered(listBox1, true);
                //});
                string date = "";
                dateTimePicker1.InvokeIfRequired(() => {
                    date = dateTimePicker1.Value.ToString("yyyy/M/d");
                });

                foreach (string words in wordss) {
                    if (words == "") break;
                    string[] word = words.Split(',');

                    //search start
                    if (word[1].Length < 6) return;
                    if (word[1].Substring(0, 6) == "084500") istart = true;
                    if (int.Parse(word[1].Substring(0, 4)) > 1344) break;
                    if (!istart) continue;


                    //MK
                    if (MKdata.Add(time, word[4], word[5], word[6])){}
                    DKdata.Add(date , word[4], word[5], word[6]);

                    mACD.macd(MKdata.kdata);
                    //plotSurface2D5.InvokeIfRequired(() =>
                    //{
                    //    plotSurface2D5.XAxis1 = mACD.LP_DIF.SuggestXAxis();
                    //    plotSurface2D5.YAxis1 = mACD.LP_DIF.SuggestYAxis();

                    //    plotSurface2D5.YAxis1.TickTextNextToAxis = false;
                    //    plotSurface2D5.Refresh();
                    //});
                    

                    //run
                    if (word[1].Substring(0, 9) != time)
                    {
                        if (time != "")
                        {
                            int s = ms.s_diff(word[1], time);
                            //label2.InvokeIfRequired(() => {
                            //    listBox1.BeginUpdate();
                            //    label2.Text = s.ToString();
                            //    listBox1.EndUpdate();
                            //});

                            //thread sleep
                            //comboBox1.InvokeIfRequired(() =>
                            //{
                            //    if (Convert.ToInt32(s) - int.Parse(comboBox1.Text) > 0) 
                            //        Thread.Sleep(Convert.ToInt32(s) / int.Parse(comboBox1.Text));
                            //});
                            int ss = times.tDiff(word[1]);
                            if (ss > 0)
                                Thread.Sleep(ss);


                            //time(s)
                            //label4.InvokeIfRequired(() =>
                            //{
                            //    label4.Text = word[1].Substring(0, 6).ToString();
                            //});

                        }

                        //high - low 
                        //label13.InvokeIfRequired(() =>
                        //{
                        //    label13.Text = (DKdata.kdata[DKdata.kdata.Count - 1].high - DKdata.kdata[DKdata.kdata.Count - 1].low).ToString();
                        //});

                        //price
                        //label1.InvokeIfRequired(() => {
                        //    label1.Text = word[4].ToString();
                        //});

                        //Qty
                        //label3.InvokeIfRequired(() =>
                        //{
                        //    label3.Text = simu.Qty("", "").ToString();
                        //});


                        //Profit
                        //label9.InvokeIfRequired(() =>
                        //{
                        //    label9.Text = simu.Profit(word[4].ToString());
                        //});

                        //Entries
                        //label11.InvokeIfRequired(() =>
                        //{
                        //    label11.Text = simu.Entries().ToString();
                        //});

                        time = word[1].Substring(0, 9);
                    }
                    //else
                    //{
                        
                    //    label1.InvokeIfRequired(() => {
                    //        label1.Text = word[4];
                    //    });
                    //}
                    //k1.KP.refreshK(MKdata.kdata);
                    //k2.KP.refreshK(DKdata.kdata);
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
        public void gui() {
            while (true)
            {
                Thread.Sleep(1);

                lock (Lock)
                {
                    if (MKdata.kdata.Count > 0)
                    {
                        ////time
                        if (MKdata.kdata[MKdata.kdata.Count - 1].time != null)
                            label4.InvokeIfRequired(() =>
                            {
                                label4.Text = MKdata.kdata[MKdata.kdata.Count - 1].time.Substring(0, 6).ToString();
                            });


                        //close
                        if (MKdata.kdata[MKdata.kdata.Count - 1].close != null)
                            label1.InvokeIfRequired(() =>
                            {
                                label1.Text = MKdata.kdata[MKdata.kdata.Count - 1].close.ToString();
                            });

                        //high - low 
                        label13.InvokeIfRequired(() =>
                        {
                            label13.Text = (DKdata.kdata[DKdata.kdata.Count - 1].high - DKdata.kdata[DKdata.kdata.Count - 1].low).ToString();
                        });

                        //Qty
                        label3.InvokeIfRequired(() =>
                        {
                            label3.Text = simu.Qty("", "").ToString();
                        });

                        //Profit
                        label9.InvokeIfRequired(() =>
                        {
                            label9.Text = simu.Profit(MKdata.kdata[MKdata.kdata.Count - 1].close.ToString());
                        });

                        //Entries
                        label11.InvokeIfRequired(() =>
                        {
                            label11.Text = simu.Entries().ToString();
                        });

                        //try
                        //{
                        plotSurface2D5.InvokeIfRequired(() =>
                        {
                            //plotSurface2D5.XAxis1 = mACD.LP_DIF.SuggestXAxis();
                            //plotSurface2D5.YAxis1 = mACD.LP_DIF.SuggestYAxis();
                            plotSurface2D5.XAxis1.WorldMax = 300;
                            plotSurface2D5.XAxis1.WorldMin = 0;
                            plotSurface2D5.YAxis1.WorldMax = mACD.highest;
                            plotSurface2D5.YAxis1.WorldMin = mACD.lowest;

                            plotSurface2D5.YAxis1.TickTextNextToAxis = false;
                            plotSurface2D5.Refresh();
                        });
                        //}
                        //catch (Exception ex) {

                        //}

                        k1.KP.refreshK(MKdata.kdata);
                        k2.KP.refreshK(DKdata.kdata);
                        ////thread sleep
                        //comboBox1.InvokeIfRequired(() =>
                        //{
                        //    if (Convert.ToInt32(s) > int.Parse(comboBox1.Text))
                        //        Thread.Sleep(Convert.ToInt32(s) / int.Parse(comboBox1.Text));
                        //});
                    }
                }

                string time = "";
                label4.InvokeIfRequired(() =>
                {
                    time = label4.Text;
                });

                if (time == "") 
                    return;
                else if (time.Substring(0, 4) == "1100") {
                    textBox1.InvokeIfRequired(() =>
                    {
                        textBox1.Text = "After 11:00, it can rise, without large volume.";
                    });
                    
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;
            dateTimePicker1.Enabled = false;
            Lock = new object();

            lock (Lock)
            {
                MKdata = new TXF.K_data();
                times = new TIMES(int.Parse(comboBox1.Text));
                simu = new Simulation();
                mACD = new Technical_analysis.MACD();
                Init();
            }
            

            if (T_Quote != null)
                T_Quote.Abort();
            if (T_GUI != null)
                T_GUI.Abort();

            T_Quote = new Thread(quote);
            T_Quote.Start();

            T_GUI = new Thread(gui);
            T_GUI.Start();
        }


        //buy
        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text == "price") return;
            if (int.Parse(label4.Text) <= 91500) {
                textBox1.Text = "Warning : Order Failed!!\n Please place your order after 9:15";
                return;
            }
            simu.MatList.Add(new Simulation.match(label4.Text, "TXF", "B", "1", label1.Text, ""));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = simu.MatList;
            dataGridView1.Refresh();
        }

        //sell
        private void button3_Click(object sender, EventArgs e)
        {
            if (label1.Text == "price") return;
            if (int.Parse(label4.Text) <= 91500)
            {
                textBox1.Text = "Warning : Order Failed!!\n Please place your order after 9:15";
                return;
            }
            simu.MatList.Add(new Simulation.match(label4.Text, "TXF", "S", "1", label1.Text, ""));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = simu.MatList;
            dataGridView1.Refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            load_dayK();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Init();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (T_Quote != null)
                T_Quote.Abort();
            if (T_GUI != null)
                T_GUI.Abort();
        }

        private void load_dayK() {
            DKdata.kdata = new List<TXF.K_data.K>();
            using (StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\日K.TXT")) {
                string words = sr.ReadLine();
                while ((words = sr.ReadLine()) != null) {
                    string[] word = words.Split(',');
                    if (Convert.ToDateTime(word[0]).Date >= dateTimePicker1.Value.Date)
                    {
                        if (DKdata.kdata.Count < 1) return;
                        float avgDay5 = 0;
                        int i;
                        for (i = 1; i < 6; i++)
                        {
                            avgDay5 += DKdata.kdata[DKdata.kdata.Count - i].high - DKdata.kdata[DKdata.kdata.Count - i].low;
                        }
                        avgDay5 /= 5;
                        label12.Text = avgDay5.ToString();
                        break;
                    }
                    DKdata.kdata.Add(new TXF.K_data.K("", 0));
                    DKdata.kdata[DKdata.kdata.Count - 1].ktime = word[0];
                    DKdata.kdata[DKdata.kdata.Count - 1].open = float.Parse(word[1]);
                    DKdata.kdata[DKdata.kdata.Count - 1].high = float.Parse(word[2]);
                    DKdata.kdata[DKdata.kdata.Count - 1].low = float.Parse(word[3]);
                    DKdata.kdata[DKdata.kdata.Count - 1].close = float.Parse(word[4]);
                    DKdata.kdata[DKdata.kdata.Count - 1].qty = int.Parse(word[12]);
                }
            }
            k2.KP.refreshK(DKdata.kdata);
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
