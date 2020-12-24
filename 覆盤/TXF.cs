﻿using System;
using System.Collections.Generic;
public class TXF
{
    public  class K_data
    {
        public class K
        {
            public string time;
            public string ktime;
            public float open;
            public float high;
            public float low;
            public float close;
            public int qty;
            public int tqty;
            public K(string nKtime, float nPri) {
                ktime = nKtime;
                open = nPri;
                high = nPri;
                low = nPri;
                close = nPri;
            }
        }
        public List<K> kdata = new List<K>();
        public bool Add(string nTime, string nPri, string nQty, string nTQty) {
            if (nQty == "") return false;
            float Pri = float.Parse(nPri);
            int Qty = int.Parse(nQty);
            int TQty = int.Parse(nTQty);
            if (kdata.Count > 0 && nTime.Substring(0, 4) == kdata[kdata.Count - 1].ktime) {
                kdata[kdata.Count - 1].ktime = nTime.Substring(0, 4);
                kdata[kdata.Count - 1].time = nTime;
                kdata[kdata.Count - 1].high = Math.Max(kdata[kdata.Count - 1].high, Pri);
                kdata[kdata.Count - 1].low = Math.Min(kdata[kdata.Count - 1].low, Pri);
                kdata[kdata.Count - 1].close = Pri;
                kdata[kdata.Count - 1].qty += Qty;
                kdata[kdata.Count - 1].tqty = TQty;
                return false;
            }
            if (nTime != "")
            {
                kdata.Add(new K(nTime.Substring(0, 4), Pri));
                return true;
            }
            return false;
        }
    }
}
