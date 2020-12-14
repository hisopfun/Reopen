using System;
using System.Collections.Generic;
public class TXF
{
    public  class MK_data
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
        public bool Add(string nTime, string nPri, string nQty, string nTQty) {
            float Pri = float.Parse(nPri);
            int Qty = int.Parse(nQty);
            int TQty = int.Parse(nTQty);
            if (txf_1mk.Count > 0 && nTime.Substring(0, 4) == txf_1mk[txf_1mk.Count - 1].ktime) {
                txf_1mk[txf_1mk.Count - 1].ktime = nTime.Substring(0, 4);
          
                txf_1mk[txf_1mk.Count - 1].high = Math.Max(txf_1mk[txf_1mk.Count - 1].high, Pri);
                txf_1mk[txf_1mk.Count - 1].low = Math.Min(txf_1mk[txf_1mk.Count - 1].low, Pri);
                txf_1mk[txf_1mk.Count - 1].close = Pri;
                txf_1mk[txf_1mk.Count - 1].qty += Qty;
                txf_1mk[txf_1mk.Count - 1].tqty = TQty;
                return false;
            }
            if (nTime != "")
            {
                txf_1mk.Add(new MK(nTime.Substring(0, 4), Pri));
                return true;
            }
            return false;
        }
    }
}
