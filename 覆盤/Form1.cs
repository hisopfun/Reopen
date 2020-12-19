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
        NPlot.PlotSurface2D ps = new NPlot.PlotSurface2D();
        private void Form1_Load(object sender, EventArgs e)
        {
            kl = new Kline(plotSurface2D1, plotSurface2D2, 1, 300);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        Thread T_Quote, T_GUI;
        TXF.MK_data MKdata = new TXF.MK_data();
        Kline kl;
        public void quote() {
            using (StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\TXF\\" + dateTimePicker1.Value.ToString("MM-dd-yyyy") +"TXF.TXT"))
            {
                string[] wordss = sr.ReadToEnd().Split('\n');
                string time = "";
                bool istart = false;
                listBox1.InvokeIfRequired(() => {
                    ExtensionMethods.DoubleBuffered(listBox1, true);
                });

                foreach (string words in wordss) {
                    if (words == "") break;
                    string[] word = words.Split(',');
                    
                    //search start
                    if (word[1].Substring(0,6) == "084500") istart = true;
                    if (!istart) continue;
                    
                    //listbox matinfo
                    listBox1.InvokeIfRequired(() => {
                        listBox1.Items.Insert(0, $"{word[1].Substring(0,2) + ":" + word[1].Substring(2, 2)+":"+ word[1].Substring(4, 2), -10}" +
                            $"{ word[2], -8}{word[3], -8}{word[4], -8}{word[5], -8}");
                    });

                    //MK
                    if (MKdata.Add(time, word[4], word[5], word[6]))
                        listBox2.InvokeIfRequired(() =>
                        {
                            List<TXF.MK_data.MK> mk = MKdata.txf_1mk;
                            if (mk.Count > 1)
                                listBox2.Items.Insert(0, mk[mk.Count - 2].ktime + " " + mk[mk.Count - 2].open + " " + mk[mk.Count - 2].high + " "
                                    + mk[mk.Count - 2].low + " " + mk[mk.Count - 2].close);
                        });

                    //run
                    if (word[1].Substring(0, 9) != time)
                    {
                        if (time != "")
                        {
                            int s = s_diff(word[1], time);
                            label2.InvokeIfRequired(() => {
                                listBox1.BeginUpdate();
                                label2.Text = s.ToString();
                                listBox1.EndUpdate();
                            });

                            //thread sleep
                            comboBox1.InvokeIfRequired(() =>
                            {
                                if (Convert.ToInt32(s) > int.Parse(comboBox1.Text))
                                    Thread.Sleep(Convert.ToInt32(s) / int.Parse(comboBox1.Text));
                            });

                            //time(ms)
                            label4.InvokeIfRequired(() =>
                            {
                                label4.Text = word[1];
                            });

                            //time(s)
                            label3.InvokeIfRequired(() =>
                            {
                                label3.Text = word[1].Substring(0, 6).ToString();
                            });
                        }
                        label1.InvokeIfRequired(() => {
                            label1.Text = word[4].ToString();
                        });
                        time = word[1].Substring(0, 9);
                        //  Application.DoEvents();
                    }
                    else
                    {
                        
                        label1.InvokeIfRequired(() => {
                            label1.Text = word[4];
                        });
                    }
                    kl.refreshK(MKdata.txf_1mk);
                }
            }
            comboBox1.InvokeIfRequired(() => {
                comboBox1.Enabled = true;
            });

            T_Quote.Abort();
        }
        public void gui() {
            while (true)
            {
                Thread.Sleep(100);
                kl.refreshK(MKdata.txf_1mk);

                if (MKdata.txf_1mk.Count > 0)
                {
                    ////time(ms)
                    //if (MKdata.txf_1mk[MKdata.txf_1mk.Count - 1].time != null)
                    //    label4.InvokeIfRequired(() =>
                    //    {
                    //        label4.Text = MKdata.txf_1mk[MKdata.txf_1mk.Count - 1].time.Substring(0, 6).ToString();
                    //    });

                    ////time(s)
                    //if (MKdata.txf_1mk[MKdata.txf_1mk.Count - 1].time != null)
                    //    label3.InvokeIfRequired(() =>
                    //    {
                    //        label3.Text = MKdata.txf_1mk[MKdata.txf_1mk.Count - 1].time;
                    //    });

                    ////close
                    //if (MKdata.txf_1mk[MKdata.txf_1mk.Count - 1].close != null)
                    //    label1.InvokeIfRequired(() => {
                    //        label1.Text = MKdata.txf_1mk[MKdata.txf_1mk.Count - 1].close.ToString();
                    //    });

                    ////thread sleep
                    //comboBox1.InvokeIfRequired(() =>
                    //{
                    //    if (Convert.ToInt32(s) > int.Parse(comboBox1.Text))
                    //        Thread.Sleep(Convert.ToInt32(s) / int.Parse(comboBox1.Text));
                    //});
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            //if (T_Quote!= null && T_Quote.IsAlive == true) T_Quote.Abort();
            button1.Enabled = false;
            dateTimePicker1.Enabled = false;

            T_Quote = new Thread(quote);
            T_Quote.Start();

            //T_GUI = new Thread(gui);
            //T_GUI.Start();
        }
        public DateTime convertToDate(string dt)
        {
            int hh = int.Parse(dt.Substring(0, 2));
            int mm = int.Parse(dt.Substring(2, 2));
            int ss = int.Parse(dt.Substring(4, 2));
            int fff = int.Parse(dt.Substring(6, 3));

            return new DateTime(2020, 1, 1, hh, mm, ss, fff);
        }

        public int s_diff(string t1, string t2) {
            TimeSpan d = convertToDate(t1) - convertToDate(t2);
            
            return Convert.ToInt32(d.TotalMilliseconds);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (T_Quote != null)
                T_Quote.Abort();
            if (T_GUI != null)
                T_GUI.Abort();
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
