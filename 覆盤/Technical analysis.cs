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
            public MACD() {
                InitLp();
                times = new float[300 - 26];
                int i;
                for (i = 1; i <= times.Length; i++) {
                    times[i-1] = i+12;
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
                dif();
                DEM.ema(DIF);

                

                LP_DIF.DataSource = DIF;
                LP_DIF.AbscissaData = times;
                LP_DEM.DataSource = DEM.EMA_;
                LP_DEM.AbscissaData = times;

        
            }

            public void dif() {
                int n = EMA1.EMA_.Count - DIF.Count;
                while (n-- > 0)
                    DIF.Add(new float());

                int i;
                for (i = 25; i < DIF.Count; i++) {
                    DIF[i] = EMA1.EMA_[i] - EMA2.EMA_[i];
                }
            }
        }


        public class EMA {
            public float[] times;
            public List<float> EMA_ = new List<float>();
            private int ma_count { get; set; }= 0;
            private List<float> SUM = new List<float>();
            public NPlot.LinePlot LP_Ema = new NPlot.LinePlot();

            public EMA(int nMACount, System.Drawing.Color color) {
                ma_count = nMACount;
                times = new float[300 - nMACount];
                int i;
                for (i = 1; i <= times.Length; i++)
                {
                    times[i - 1] = i+12;
                }

                LP_Ema.Color = color;
            }

            public float ema(List<TXF.K_data.K> mk) {

                //int n = mk.Count - EMA_.Count;
                //while (n-- > 0)
                //{
                //    SUM.Add(new float());
                //    //EMA_.Add(new float());
                //}


                if (mk.Count >= ma_count)
                {

                    //ema = sum / ma_count
                    EMA_[mk.Count - 1] = Sum(mk, mk.Count - ma_count + 1, mk.Count) / ma_count;
                    LP_Ema.DataSource = EMA_;
                    LP_Ema.AbscissaData = times;

                    return EMA_[mk.Count - 1];
                }

                return 0;
            }

            private float Sum(List<TXF.K_data.K> mk, int start, int end) {

                int n = mk.Count - EMA_.Count;
                while (n-- > 0)
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
                if (mk.Count > EMA_.Count)
                {
                    int n = mk.Count - EMA_.Count;
                    while (n-- > 0)
                    {
                        SUM.Add(new float());
                        EMA_.Add(new float());
                    }
                }

                if (mk.Count >= ma_count)
                {

                    //ema = sum / ma_count
                    EMA_[mk.Count - 1] = Sum(mk, mk.Count - ma_count + 1, mk.Count) / ma_count;
                    return EMA_[mk.Count - 1];
                }

                return 0;
            }

            private float Sum(List<float> mk, int start, int end)
            {
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
