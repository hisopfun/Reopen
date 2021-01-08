using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 覆盤
{
    class Technical_analysis
    {
        public class MACD {
            public float[] times;
            public EMA EMA1 = new EMA(12, System.Drawing.Color.Red);
            public EMA EMA2 = new EMA(26, System.Drawing.Color.Blue);
            private List<float> DIF { get; } = new List<float>();
            private EMA DEM { get; } = new EMA(9, System.Drawing.Color.Yellow);
            public NPlot.LinePlot LP_DIF = new NPlot.LinePlot();
            public NPlot.LinePlot LP_DEM = new NPlot.LinePlot();
            public NPlot.HorizontalLine horizontalLine = new NPlot.HorizontalLine(0);
            public float highest = int.MinValue, lowest = int.MaxValue;
            public MACD() {
                InitLp();
                times = new float[300];
                int i;
                for (i = 0; i < times.Length; i++) {
                    times[i] = i;
                }
            }
            public void InitLp()
            {
                LP_DIF.Color = System.Drawing.Color.Red;
                int[] times = { 100, 200, 300, 400, 500, 600, 700 };
                LP_DIF.AbscissaData = times;
                LP_DIF.DataSource = times;
                LP_DEM.AbscissaData = new int[]{ 400, 500, 600, 700 }; 
                LP_DEM.DataSource = new int[] { 700, 600, 500, 400 };
            }
            public void macd(List<TXF.K_data.K> mk) {
                EMA1.ema(mk);
                EMA2.ema(mk);
                dif(mk);
                DEM.ema(DIF);

                

                LP_DIF.DataSource = DIF;
                LP_DIF.AbscissaData = times;
                LP_DEM.DataSource = DEM.Chart_EMA;
                LP_DEM.AbscissaData = times;

        
            }

            public void dif(List<TXF.K_data.K> mk) {
                while (mk.Count > DIF.Count)
                {
                    DIF.Add(new float());
                    DEM.Chart_EMA.Add(new float());
                    if (DIF.Count < EMA2.ema_count + DEM.ema_count + 2)
                    {
                        if (DEM.Chart_EMA.Count > 2)
                        {
                            DIF[DIF.Count - 3] = 0;
                            DEM.Chart_EMA[DEM.Chart_EMA.Count - 3] = 0;
                        }
                    }
                }
                //int i;
                //for (i = 25; i < DIF.Count; i++) {
                //    DIF[i] = EMA1.SMA_[i] - EMA2.SMA_[i];
                //}
                if (EMA1.Chart_EMA.Count > 0 && EMA2.Chart_EMA.Count > 0)
                {
                    DIF[DIF.Count - 1] = EMA1.Chart_EMA[EMA1.Chart_EMA.Count - 1] - EMA2.Chart_EMA[EMA2.Chart_EMA.Count - 1];
                    highest = Math.Max(highest, DIF[DIF.Count - 1]);
                    lowest = Math.Min(lowest, DIF[DIF.Count - 1]);
                }

                //if (mk.Count == EMA2.ema_count + DEM.ema_count + 2) {
                //    int n = 34;
                //    while(n-- > 0) {
                //        DIF.Remove(DIF[0]);
                //        DEM.Chart_EMA.Remove(DEM.Chart_EMA[0]);
                //        times = new float[times.Length - 1];

                //        int i;
                //        for (i = 0; i < times.Length; i++)
                //            times[i] = i + (300 - times.Length) + 1;
                //    }
                //}
            }
        }

        public class EMA
        {
            public float[] times;
            //total ema
            public List<float> EMA_ = new List<float>();
            public int ema_count { get; set; } = 0;

            //chart
            public List<float> Chart_EMA = new List<float>();
            public NPlot.LinePlot LP_EMA = new NPlot.LinePlot();

            public EMA(int nMACount, System.Drawing.Color color)
            {
                ema_count = nMACount;
                times = new float[300 - nMACount + 1];
                int i;
                for (i = 0; i < times.Length; i++)
                {
                    times[i] = i + nMACount;
                }

                LP_EMA.Color = color;
            }

            public float ema(List<TXF.K_data.K> mk)
            {

                LP_EMA.DataSource = Chart_EMA;
                LP_EMA.AbscissaData = times;
                //if (mk.Count >= ma_count + EMA_.Count)
                //{
                while (mk.Count > EMA_.Count )
                    EMA_.Add(new float());

                while (mk.Count > ema_count + Chart_EMA.Count - 1)
                    Chart_EMA.Add(new float());

                //ema = sum / ma_count

                //nEMA=(前一日nEMA*(n-1)＋今日收盤價×2)/(n+1)
                if (EMA_.Count > 1)
                {
                    if (EMA_[EMA_.Count - 2] == 0)
                        EMA_[EMA_.Count - 2] = mk[mk.Count - 2].close;
                    EMA_[EMA_.Count - 1] = (EMA_[EMA_.Count - 2] * (ema_count - 1) + mk[mk.Count - 1].close * 2) / (ema_count + 1);
                    if (EMA_.Count >= ema_count) 
                        Chart_EMA[Chart_EMA.Count - 1] = EMA_[EMA_.Count - 1];
                    return EMA_[EMA_.Count - 1];
                }

                return 0;
            }


            public float ema(List<float> mk)
            {

                LP_EMA.DataSource = Chart_EMA;
                LP_EMA.AbscissaData = times;
                //if (mk.Count >= ma_count + EMA_.Count)
                //{
                while (mk.Count > EMA_.Count)
                    EMA_.Add(new float());

                while (mk.Count > ema_count + Chart_EMA.Count - 1)
                    Chart_EMA.Add(new float());

                //ema = sum / ma_count

                //nEMA=(前一日nEMA*(n-1)＋今日收盤價×2)/(n+1)
                if (EMA_.Count > 1)
                {
                    if (EMA_[EMA_.Count - 2] == 0)
                        EMA_[EMA_.Count - 2] = mk[mk.Count - 2];
                    EMA_[EMA_.Count - 1] = (EMA_[EMA_.Count - 2] * (ema_count - 1) + mk[mk.Count - 1] * 2) / (ema_count + 1);
                    if (EMA_.Count >= ema_count)
                        Chart_EMA[Chart_EMA.Count - 1] = EMA_[EMA_.Count - 1];
                    return EMA_[EMA_.Count - 1];
                }

                return 0;
            }
        }




        public class SMA {
            public float[] times;
            public List<float> SMA_ = new List<float>();
            private int sma_count { get; set; }= 0;
            private List<float> SUM = new List<float>();
            public NPlot.LinePlot LP_SMA = new NPlot.LinePlot();

            public SMA(int nMACount, System.Drawing.Color color) {
                sma_count = nMACount;
                times = new float[300 - nMACount + 1];
                int i;
                for (i = 0; i < times.Length; i++)
                {
                    times[i] = i+ nMACount;
                }

                LP_SMA.Color = color;
            }

            public float ema(List<TXF.K_data.K> mk) {

                //int n = mk.Count - SUM.Count;
                //while (n-- > 0)
                //{
                //    SUM.Add(new float());
                //    //EMA_.Add(new float());
                //}

                LP_SMA.DataSource = SMA_;
                LP_SMA.AbscissaData = times;
                //if (mk.Count >= ma_count + EMA_.Count)
                //{
                    while (mk.Count > sma_count + SMA_.Count - 1)
                        SMA_.Add(new float());

                //ema = sum / ma_count
                if (SMA_.Count > 0)
                {
                    SMA_[SMA_.Count - 1] = Sum(mk, mk.Count - sma_count + 1, mk.Count) / sma_count;
                    //LP_Ema.DataSource = EMA_;
                    //LP_Ema.AbscissaData = times;

                    return SMA_[SMA_.Count - 1];
                }
                //}

                return 0;
            }

            private float Sum(List<TXF.K_data.K> mk, int start, int end) {
                while (mk.Count > SUM.Count)
                {
                    SUM.Add(new float());
                    //EMA_.Add(new float());
                }

                if (mk.Count < end) return 0;
                if (start < 0) return 0;
                int count = end - start + 1;
                if (count <= 1) return mk[start - 1].close;

                if (end >= count){

                    //SUM[end - 2] = Sum(mk, start, end - 1);
                    if (SUM[end - 2] == 0) 
                        SUM[end - 2] = Sum(mk, 1, end - 1);

                    if (start == 1) //dont substract
                        SUM[end - 1] = SUM[end - 2] + mk[end - 1].close;
                    else            //need substract
                        SUM[end - 1] = SUM[end - 2]  - mk[start - 2].close + mk[end - 1].close;
                }

                return SUM[end - 1];
            }

            public float ema(List<float> mk)
            {

                //int n = mk.Count - SUM.Count;
                //while (n-- > 0)
                //{
                //    SUM.Add(new float());
                //    //EMA_.Add(new float());
                //}

                LP_SMA.DataSource = SMA_;
                LP_SMA.AbscissaData = times;
                //if (mk.Count >= ma_count + EMA_.Count)
                //{
                while (mk.Count > sma_count + SMA_.Count - 1)
                    SMA_.Add(new float());

                //ema = sum / ma_count
                if (SMA_.Count > 0)
                {
                    SMA_[SMA_.Count - 1] = Sum(mk, mk.Count - sma_count + 1, mk.Count) / sma_count;
                    //LP_Ema.DataSource = EMA_;
                    //LP_Ema.AbscissaData = times;

                    return SMA_[SMA_.Count - 1];
                }
                //}

                return 0;
            }

            private float Sum(List<float> mk, int start, int end)
            {
                while (mk.Count > SUM.Count)
                {
                    SUM.Add(new float());
                    //EMA_.Add(new float());
                }

                if (mk.Count < end) return 0;
                if (start < 0) return 0;
                int count = end - start + 1;
                if (count <= 1) return mk[start - 1];

                if (end >= count)
                {

                    //SUM[end - 2] = Sum(mk, start, end - 1);
                    if (SUM[end - 2] == 0)
                        SUM[end - 2] = Sum(mk, 1, end - 1);

                    if (start == 1) //dont substract
                        SUM[end - 1] = SUM[end - 2] + mk[end - 1];
                    else            //need substract
                        SUM[end - 1] = SUM[end - 2] - mk[start - 2] + mk[end - 1];
                }

                return SUM[end - 1];
            }
        }
    }
}
