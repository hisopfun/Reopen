﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace 覆盤
{
    public class strategy
    {
        public string name { get; set; }
        public int bs { get; set; } = -1;
        public string signalStrong { get; set; }
        public long polarPrice { get; set; } = 0;
        public long dealPrice { get; set; } = 0;
        public long mitPrice { get; set; } = 0;
        public float benefit { get; set; } = 0;
        public float Bbenefit { get; set; } = 0;
        public float Sbenefit { get; set; } = 0;
        public int entries { get; set; } = 0;
        public long stopProfit { get; set; }
        public string dealTime { get; set; } = "";
        public string signalLevel { get; set; }
        public float signalIn { get; set; }
        public float signalOut { get; set; }
        public string exeStart { get; set; } = "";
        public string exeEnd { get; set; } = "";
        public int last_bs { get; set; } = -1;
        public string date { get; set; } = "";

        public strategy(string nName, float nSignalIn, float nSignalOut, string nExeStart, string nExeEnd, long nStopProfit)
        {
            name = nName;
            signalIn = nSignalIn;
            signalOut = nSignalOut;
            exeStart = nExeStart;
            exeEnd = nExeEnd;
            stopProfit = nStopProfit;
            initial();
        }

        public void initial()
        {

            //name;
            bs = -1;
            polarPrice = 0;
            dealPrice = 0;
            mitPrice = 0;
            dealTime = "";
            signalStrong = "0";
            signalLevel = "";
            //signalIn;
            //signalOut;
            //exeStart  = "";
            //exeEnd  = "";
            last_bs = -1;
            benefit = 0;
            Bbenefit = 0;
            Sbenefit = 0;
            entries = 0;
        }

        public void writeTo(string str) {
            using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Strategy_Log.TXT", FileMode.Append)) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    sw.WriteLine(str);
                }
            }
        }

        public string plan10M_implement(long nSignal, long nMatchPrice, string nMatchTime) //20200630
        {
            string order = "X";
            if (last_bs != -1) return order;
            if (int.Parse(nMatchTime.Substring(0, 6)) >= int.Parse(exeStart) && 
                int.Parse(nMatchTime.Substring(0, 6)) <= int.Parse(exeEnd))
            {
                signalStrong = nSignal.ToString();

                //In
                if (bs == -1)
                {
                    if (nSignal > 0)
                    {
                        bs = 0;
                        entries += 1;
                        return "B";
                    }
                    else if (nSignal < 0)
                    {
                        bs = 1;
                        entries += 1;
                        return "S";
                    }
                }

            }

            if (bs == 0 && dealPrice == 0 )
            {
                dealTime = nMatchTime;
                dealPrice = nMatchPrice;
                polarPrice = nMatchPrice;
                //if (mitPrice < nMatchPrice - 20)
                //    mitPrice = nMatchPrice - 20;
                //return "B," + nMatchPrice;
            }
            else if (bs == 1 && dealPrice == 0)
            {
                dealTime = nMatchTime;
                dealPrice = nMatchPrice;
                polarPrice = nMatchPrice;
                //if (mitPrice > nMatchPrice + 20)
                //    mitPrice = nMatchPrice + 20;
                //return "S," + nMatchPrice;
            }

            if (bs == 0 && dealPrice != 0)
            {
                
                //stop loss
                if (nMatchPrice <= mitPrice)
                {
                    bs = -1;
                    last_bs = 0;
                    dealTime = nMatchTime;
                    benefit += (mitPrice - dealPrice);
                    Bbenefit += (mitPrice - dealPrice);
                    writeTo(date + "," + benefit);
                    return "S";
                }

                //stop profit
                if (stopProfit != 0)
                {
                    if (nMatchPrice > dealPrice + stopProfit)
                    {
                        bs = -1;
                        last_bs = 0;
                        dealTime = nMatchTime;
                        benefit += stopProfit;
                        writeTo(date + "," + benefit);
                        return "S";
                    }
                }
                return "X";
            }
            else if (bs == 1 && dealPrice != 0)
            {
                
                //stop loss
                if (nMatchPrice >= mitPrice)
                {
                    bs = -1;
                    last_bs = 1;
                    dealTime = nMatchTime;
                    benefit += (dealPrice - mitPrice);
                    Sbenefit += (dealPrice - mitPrice);
                    writeTo(date + "," + benefit);
                    return "B";
                }


                //stop profit
                if (stopProfit != 0)
                {
                    if (nMatchPrice < dealPrice - stopProfit)
                    {
                        bs = -1;
                        last_bs = 1;
                        dealTime = nMatchTime;
                        benefit += stopProfit;
                        writeTo(date + "," + benefit);
                        return "B";
                    }
                }
                return "X";
            }

            return order;
        }


        public string planMACD(Technical_analysis.MACD mACD, long nMatchPrice, string nMatchTime) {
            string order = "X";
            if (last_bs != -1) return order;
            if (mACD.OSC.Count < 3) return order;

            if (int.Parse(nMatchTime.Substring(0, 6)) >= int.Parse(exeStart) &&
                int.Parse(nMatchTime.Substring(0, 6)) <= int.Parse(exeEnd))
            {
                //signalStrong = nSignal.ToString();

                //In
                    if (bs == -1)
                {
                    if (mACD.OSC[mACD.OSC.Count - 3] * mACD.OSC[mACD.OSC.Count - 2] > 0) return order;
                    if (mACD.OSC[mACD.OSC.Count-2] > 0 )
                    {
                        bs = 0;
                        entries += 1;
                        return "B";
                    }
                    else if (mACD.OSC[mACD.OSC.Count - 2] < 0)
                    {
                        bs = 1;
                        entries += 1;
                        return "S";
                    }
                }

            }

            if (bs == 0 && dealPrice == 0)
            {
                dealTime = nMatchTime;
                dealPrice = nMatchPrice;
                polarPrice = nMatchPrice;
                mitPrice = nMatchPrice - 20;
                //return "B," + nMatchPrice;
            }
            else if (bs == 1 && dealPrice == 0)
            {
                dealTime = nMatchTime;
                dealPrice = nMatchPrice;
                polarPrice = nMatchPrice;
                mitPrice = nMatchPrice + 20;
                //return "S," + nMatchPrice;
            }

            if (bs == 0 && dealPrice != 0)
            {

                //stop loss
                if (nMatchPrice <= mitPrice)
                {
                    bs = -1;
                    last_bs = 0;
                    dealTime = nMatchTime;
                    benefit += (mitPrice - dealPrice);
                    Bbenefit += (mitPrice - dealPrice);
                    return "S";
                }

                //stop profit
                if (stopProfit != 0)
                {
                    if (nMatchPrice > dealPrice + stopProfit)
                    {
                        bs = -1;
                        last_bs = 0;
                        dealTime = nMatchTime;
                        benefit += stopProfit;
                        return "S";
                    }
                }
                return "X";
            }
            else if (bs == 1 && dealPrice != 0)
            {

                //stop loss
                if (nMatchPrice >= mitPrice)
                {
                    bs = -1;
                    last_bs = 1;
                    dealTime = nMatchTime;
                    benefit += (dealPrice - mitPrice);
                    Sbenefit += (dealPrice - mitPrice);
                    return "B";
                }


                //stop profit
                if (stopProfit != 0)
                {
                    if (nMatchPrice < dealPrice - stopProfit)
                    {
                        bs = -1;
                        last_bs = 1;
                        dealTime = nMatchTime;
                        benefit += stopProfit;
                        return "B";
                    }
                }
                return "X";
            }

            return order;
        }

        public string planOSC(Technical_analysis.MACD mACD, long nMatchPrice, string nMatchTime)
        {
            string order = "X";
            if (last_bs != -1) return order;
            if (mACD.OSC.Count < 3) return order;

            if (int.Parse(nMatchTime.Substring(0, 6)) >= int.Parse(exeStart) &&
                int.Parse(nMatchTime.Substring(0, 6)) <= int.Parse(exeEnd))
            {
                //signalStrong = nSignal.ToString();

                //In
                if (bs == -1)
                {
                    if (mACD.OSC[mACD.OSC.Count - 3] * mACD.OSC[mACD.OSC.Count - 2] > 0) return order;
                    if (mACD.OSC[mACD.OSC.Count - 2] > 0)
                    {
                        bs = 0;
                        entries += 1;
                        return "B";
                    }
                    else if (mACD.OSC[mACD.OSC.Count - 2] < 0)
                    {
                        bs = 1;
                        entries += 1;
                        return "S";
                    }
                }

            }

            if (bs == 0 && dealPrice == 0)
            {
                dealTime = nMatchTime;
                dealPrice = nMatchPrice;
                polarPrice = nMatchPrice;
                mitPrice = nMatchPrice - 20;
                //return "B," + nMatchPrice;
            }
            else if (bs == 1 && dealPrice == 0)
            {
                dealTime = nMatchTime;
                dealPrice = nMatchPrice;
                polarPrice = nMatchPrice;
                mitPrice = nMatchPrice + 20;
                //return "S," + nMatchPrice;
            }

            if (bs == 0 && dealPrice != 0)
            {

                //stop 
                if (mACD.OSC[mACD.OSC.Count - 2] - mACD.OSC[mACD.OSC.Count - 3] < 0)
                {
                    bs = -1;
                    last_bs = 0;
                    dealTime = nMatchTime;
                    benefit += (mitPrice - dealPrice);
                    Bbenefit += (mitPrice - dealPrice);
                    return "S";
                }

                return "X";
            }
            else if (bs == 1 && dealPrice != 0)
            {

                //stop 
                if (mACD.OSC[mACD.OSC.Count - 2] - mACD.OSC[mACD.OSC.Count - 3] > 0)
                {
                    bs = -1;
                    last_bs = 1;
                    dealTime = nMatchTime;
                    benefit += (dealPrice - mitPrice);
                    Sbenefit += (dealPrice - mitPrice);
                    return "B";
                }


                return "X";
            }

            return order;
        }
    }
}
