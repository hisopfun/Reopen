using System;
using System.Collections.Generic;
public class TXF
{
    public class MK_data
    {
        public class MK
        {
            public string ktime;
            public float open;
            public float high;
            public float low;
            public float close;
            public int qty;
            public int tqty;
            public MK(string nKtime, float nPri) {
                ktime = nKtime;
                open = nPri;
                high = nPri;
                low = nPri;
                close = nPri;
            }
        }
        public List<MK> txf_1mk = new List<MK>();
        public MK_data(string nTime, string nPri, string nQty, string nTQty) {
            float Pri = float.Parse(nPri);
            if (txf_1mk.Count > 0) { 
                
            }
            
            txf_1mk.Add(new MK(nTime.Substring(0,4), Pri);
        }
    }
}
