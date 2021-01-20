﻿using System;
using System.Collections.Generic;
public class TXF
{
    public  class K_data
    {
        public class K
        {
            public string time = "000000";
            public string ktime;
            public float open;
            public float high;
            public float low;
            public float close;
            public uint qty;
            public uint tqty;
            public K(string nKtime, float nPri) {
                ktime = nKtime;
                open = nPri;
                high = nPri;
                low = nPri;
                close = nPri;
            }
        }
        public List<K> klist = new List<K>();
        public bool Run(string nTime, string nPri, string nQty) {
            if (nTime == "") return false;
            if (nQty == "") return false;
            float Pri = float.Parse(nPri);
            uint Qty = uint.Parse(nQty);
            if (klist.Count > 0 && nTime.Substring(0, 4) == klist[klist.Count - 1].ktime) {
                klist[klist.Count - 1].ktime = nTime.Substring(0, 4);
                klist[klist.Count - 1].time = nTime;
                klist[klist.Count - 1].high = Math.Max(klist[klist.Count - 1].high, Pri);
                klist[klist.Count - 1].low = Math.Min(klist[klist.Count - 1].low, Pri);
                klist[klist.Count - 1].close = Pri;
                klist[klist.Count - 1].qty += Qty;
                if (klist.Count >= 2)
                    klist[klist.Count - 1].tqty = klist[klist.Count - 2].tqty + klist[klist.Count - 1].qty;
                else
                    klist[klist.Count - 1].tqty = klist[klist.Count - 1].qty;
                return false;
            }
            if (nTime != "")
            {
                klist.Add(new K(nTime.Substring(0, 4), Pri));
                klist[klist.Count - 1].ktime = nTime.Substring(0, 4);
                klist[klist.Count - 1].time = nTime;
                klist[klist.Count - 1].high = Math.Max(klist[klist.Count - 1].high, Pri);
                klist[klist.Count - 1].low = Math.Min(klist[klist.Count - 1].low, Pri);
                klist[klist.Count - 1].close = Pri;
                klist[klist.Count - 1].qty += Qty;
                if (klist.Count >= 2)
                    klist[klist.Count - 1].tqty = klist[klist.Count - 2].tqty + klist[klist.Count - 1].qty;
                else
                    klist[klist.Count - 1].tqty = klist[klist.Count - 1].qty;
                return true;
            }
            return false;
        }
    }
}
