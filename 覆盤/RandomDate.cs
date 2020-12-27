using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 覆盤
{
    static class RandomDate
    {

        public static DateTime RandomGetDate()
        {
            DateTime startTime = Convert.ToDateTime("2000-1-1");
            DateTime endTime = DateTime.Now.Date;
            TimeSpan ts = new TimeSpan(endTime.Ticks - startTime.Ticks);
            Random r = new Random();
            DateTime nextDT = startTime.AddDays(r.Next(ts.Days + 1));
            //MessageBox.Show(nextDT.ToString());
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
}
