using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 覆盤
{
    public static class RandomDate
    {

        public static DateTime RandomGetDate()
        {
            DateTime startTime = Convert.ToDateTime("2000-1-1");
            DateTime endTime = DateTime.Now.Date;
            TimeSpan ts = new TimeSpan(endTime.Ticks - startTime.Ticks);
            Random r = new Random();
            DateTime nextDT = startTime.AddDays(r.Next(ts.Days + 1));
            return nextDT;
        }

        public static bool CheckDate(DateTime date)
        {

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\TXF\\" + date.ToString("MM-dd-yyyy") + "TXF.TXT"))
            {
                using (StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\TXF\\" + date.ToString("MM-dd-yyyy") + "TXF.TXT"))
                {
                    string words = "";
                    while ((words = sr.ReadLine()) != null)
                    {
                        if (words == "") break;
                        string[] word = words.Split(',');

                        //search start
                        if (word[1].Length < 6)
                            return false;
                        if (word[1].Substring(0, 6) == "084500")
                            return true;
                    }
                }
            }
            return false;
        }

        public static DateTime RandomSelectDate()
        {
            DateTime DT = RandomGetDate();
            while (!CheckDate(DT))
            {
                DT = RandomGetDate();
            }
            return DT;
        }
    }

    public static class ms {
        public static DateTime convertToMillisecond(string dt)
        {
            int hh = int.Parse(dt.Substring(0, 2));
            int mm = int.Parse(dt.Substring(2, 2));
            int ss = int.Parse(dt.Substring(4, 2));
            int fff = int.Parse(dt.Substring(6, 3));

            return new DateTime(2020, 1, 1, hh, mm, ss, fff);
        }

        public static int s_diff(string t1, string t2)
        {
            TimeSpan d = convertToMillisecond(t1) - convertToMillisecond(t2);
            return Convert.ToInt32(d.TotalMilliseconds);
        }

        public static int s_diff(DateTime t1, DateTime t2)
        {
            TimeSpan d = t1 - t2;

            return Convert.ToInt32(d.TotalMilliseconds);
        }
    }

    public class TIMES {
        public DateTime sysTime;
        public DateTime lastTime;
        public int speed = 1;

        public TIMES(int nSpeed) {
            speed = nSpeed;
        }

        public int tDiff(string dt) {
            int sys_diff = 0, diff = 0;
            if (lastTime.Date == new DateTime(0001,1,1)) {
                lastTime = ms.convertToMillisecond(dt);
                sysTime = DateTime.Now;
            }
            //diff = ms.s_diff(ms.convertToMillisecond(dt), lastTime);
            //sys_diff = ms.s_diff(DateTime.Now, sysTime) ;
            diff = ms.s_diff(ms.convertToMillisecond(dt), lastTime);

            if (lastTime != ms.convertToMillisecond(dt))
            {
                lastTime = ms.convertToMillisecond(dt);
                sysTime = DateTime.Now;
            }

            return Math.Max(0, diff / speed);
        }
    }

}
