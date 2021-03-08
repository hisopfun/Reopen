using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 覆盤
{
    public class Line {
        public int startITime = 0;
        public string startTime = "";
        public string endTime = "";
        public float startPoint;
        public float endPoint;
        public float length;
        public float rise;
    }

    public class Point {
        public int iTime;
        public float price;
    }

    public class turn
    {
        Line line = new Line();

        public bool isTurn(List<TXF.K_data.K> mk, Line line, int i, string BS) {
            if (line.length < 5) return false;

            //start is not equal with end   or   start is equal with end but is red k
            if (BS == "B")
            {
                if (line.startTime != line.endTime ||
                    line.startTime == line.endTime && mk[i - 1].close >= mk[i - 1].open)
                {
                    if (line.startPoint < mk[i].low - 1)
                    {
                        return true;
                    }
                }
            }


            if (BS == "S") {
                //start is not equal with end   or   start is equal with end but is red k
                if (line.startTime != line.endTime ||
                    line.startTime == line.endTime && mk[i - 1].close <= mk[i - 1].open)
                {
                    if (line.startPoint > mk[i].high + 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public Point BuyTurn(List<TXF.K_data.K> mk)
        {
            float close = mk[mk.Count - 1].close;
            line.startPoint = int.MaxValue;
            line.endPoint = 0;
            for (int i = mk.Count - 2; i >= 1; i--) {

                //start is not equal with end   or   start is equal with end but is red k
                if (isTurn(mk, line, i, "B")) {

                    return new Point()
                    {
                        iTime = line.startITime,
                        price = line.startPoint
                    };
                }

                //high
                if (line.endPoint < mk[i].high)
                {
                    line.endPoint = mk[i].high;
                    line.endTime = mk[i].time;

                    if (line.startPoint < line.endPoint)
                        line.length = line.endPoint - line.startPoint;
                }

                //low
                if (line.startPoint > mk[i].low && close > mk[i].high ||
                    line.startPoint > mk[i].low && close > mk[i].low && mk[i].close >= mk[i].open ||
                    line.startPoint > mk[i].low && close > mk[i].low && mk[i].time != line.endTime )
                {
                    line.startPoint = mk[i].low;
                    line.startITime = i + 1;
                    line.startTime = mk[i].time;
                    if (line.startPoint < line.endPoint)
                        line.length = line.endPoint - line.startPoint;

                }



                //start must before than end
                if (line.startTime != "")
                {
                    if (int.Parse(line.startTime.Substring(0, 9)) > int.Parse(line.endTime.Substring(0, 9)))
                    {
                        line.startTime = "";
                        line.startITime = 0;
                        line.endPoint = 0;
                        line.startPoint = int.MaxValue;
                        line.length = 0;
                    }
                }

                close = Math.Min(close, mk[i].low);
            }

            return null;
        }


        public Point SellTurn(List<TXF.K_data.K> mk)
        {
            float close = mk[mk.Count - 1].close;
            line.endPoint = int.MaxValue;
            line.startPoint = 0;
            for (int i = mk.Count - 2; i >= 0; i--)
            {

                //start is not equal with end   or   start is equal with end but is red k
                if (isTurn(mk, line, i, "S"))
                {
                    return new Point()
                    {
                        iTime = line.startITime,
                        price = line.startPoint
                    };
                }


                //low
                if (line.endPoint > mk[i].low)
                {
                    line.endPoint = mk[i].low;
                    line.endTime = mk[i].time;

                    if (line.startPoint > line.endPoint && line.startPoint > 0 && line.endPoint > 0)
                        line.length = line.startPoint - line.endPoint;
                }

                //high
                if (line.startPoint < mk[i].high && close < mk[i].low ||
                    line.startPoint < mk[i].high && close < mk[i].high && mk[i].close <= mk[i].open ||
                    line.startPoint < mk[i].high && close < mk[i].high && mk[i].time != line.endTime)
                {
                    line.startPoint = mk[i].high;
                    line.startITime = i + 1;
                    line.startTime = mk[i].time;
                    if (line.startPoint > line.endPoint && line.startPoint > 0 && line.endPoint > 0)
                        line.length = line.startPoint - line.endPoint;
                }



                //start must before than end
                if (line.startTime != "" && line.endTime !="")
                {
                    if (int.Parse(line.startTime.Substring(0, 9)) > int.Parse(line.endTime.Substring(0, 9)))
                    {
                        line.startTime = "";
                        line.startITime = 0;
                        line.endPoint = int.MaxValue;
                        line.startPoint = 0;
                        line.length = 0;
                    }
                }

                close = Math.Max(close, mk[i].high);
            }

            return null;
        }
    }




    public class Sharp
    {


    }
}
