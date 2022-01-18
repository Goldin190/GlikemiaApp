using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlikemiaApp
{
    class PomiaryGlikemi
    {
        public int id;
        public int cukier;
        private DateTime data;
        public String opis;
        public int dodatkoweJI;

        public PomiaryGlikemi() { }
        public PomiaryGlikemi(int p_id, int p_cukier, DateTime p_date, string p_opis, int p_dodatkoweJI)
        {
            this.id = p_id;
            this.cukier = p_cukier;
            this.data = p_date;
            this.opis = p_opis;
            this.dodatkoweJI = p_dodatkoweJI;
        }

        private bool Validate_Input_Date(string date)
        {
            List<string> SplitString = date.Split('-').ToList();
            if (SplitString.Count == 3)
            {
                foreach (string part in SplitString)
                {
                    if (!int.TryParse(part, out int dumpVar))
                    {
                        return false;
                    }
                    else
                    {                        
                        //comment or uncomment for debug
                        //Console.WriteLine(dumpVar);
                        //Console.ReadLine();
                    }

                }
            }
            else
            {
                return false;
            }
            try
            {
                this.data = new DateTime(int.Parse(SplitString[2]), int.Parse(SplitString[1]), int.Parse(SplitString[0]));
            }
            catch (Exception e )
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Podaj poprawną datę.");
                return false;
            }
            
            return true;
        }
        private bool Validate_Input_Time(string time)
        {
            List<string> SplitString = time.Split(':').ToList();
            if (SplitString.Count == 2)
            {
                foreach (string part in SplitString)
                {
                    if (!int.TryParse(part, out int dumpVar))
                    {
                        return false;
                    }
                    else
                    {
                        //comment or uncomment for debug
                        //Console.WriteLine(dumpVar);
                        //Console.ReadLine();
                    }
                }
            }
            else
            {
                return false;
            }
            int hour = int.Parse(SplitString[0]);
            int minute = int.Parse(SplitString[1]);
            if (hour < 0 || hour > 24 || minute < 0 || minute > 60)
            {
                Console.WriteLine("Podaj poprawną godzinę.");
                return false;
            }
            try
            {
                TimeSpan ts = new TimeSpan(hour, int.Parse(SplitString[1]), 0);
                this.data = this.data.Add(ts);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Podaj poprawną godzinę.");
                return false;
            }
           
            return true;
        }
        public void Set_Date()
        {
            Console.WriteLine("Podaj datę pomiaru(dd-mm-yyyy):");
            while (!Validate_Input_Date(Console.ReadLine())) ;
            Console.WriteLine("Podaj godzinę pomiaru(hh:mm)");
            while (!Validate_Input_Time(Console.ReadLine())) ;
        }
        public void Set_Date(string dateString)
        {
            this.data = DateTime.Parse(dateString);
        }

        public DateTime Get_Date()
        {
            return data;
        }

        public void ShowPomiar()
        {
            //"id          :" + id          + "\n" + Add for debug
            Console.WriteLine("data        :" + Get_Date()  + "\n" +
                              "cukier      :" + cukier      + "\n" +
                              "opis        :" + opis        + "\n" +
                              "DodatkoweJI :" + dodatkoweJI + "\n");

        }
    }
}
