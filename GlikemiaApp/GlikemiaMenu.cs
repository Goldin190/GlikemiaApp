using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlikemiaApp
{
    class GlikemiaMenu : IMenu
    {
        XmlGlikemiaHandler xmlHandler;
        private int userMenuChoice;

        public void Hold_Execution()
        {
            Console.WriteLine("Kilknij Enter aby kontynuować");
            Console.ReadLine();
        }

        private void DebugStop()
        {
            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }
        public GlikemiaMenu()
        {
            xmlHandler = new XmlGlikemiaHandler();
        }

        public void Show_Error_Message(string msg)
        {
            Console.WriteLine("");
            Console.WriteLine(msg + "\nNaciśij Enter");
            Console.ReadLine();
        }
        public bool Validate_Input(List<int> range, int exitValue)
        {
            userMenuChoice = new int();
            string errMsg = "";
            if (int.TryParse(Console.ReadLine(), out userMenuChoice))
            {
                if (range.Contains(userMenuChoice))
                {
                    return true;
                }
                else
                {
                    errMsg += "Nie poprawna opcja \n";
                }
            }
            else
            {
                errMsg += "Wprowadź tylko liczby.\n";
            }
            Console.Clear();
            this.Show_Error_Message(errMsg);
            return false;
        }
        private PomiaryGlikemi Add_Pomiar()
        {
            PomiaryGlikemi pomiar = new PomiaryGlikemi();

            do {
                Console.Clear();
                int input;
                bool isWrong = false;
                
                pomiar.id = xmlHandler.DeserializeLastObject().id+1;
                pomiar.Set_Date();

                do
                {
                    Console.WriteLine("Podaj cukier");
                    if (!int.TryParse(Console.ReadLine(), out input))
                    {
                        isWrong = true;
                        Console.Clear();
                    }
                    else if (input < 0)
                    {
                        isWrong = true;
                        Console.Clear();
                    }
                    else
                    {
                        isWrong = false;
                    }
                } while (isWrong);
                pomiar.cukier = input;
                do
                {
                    Console.WriteLine("Podaj ilość dodatkowych jednostek insuliny");
                    if (!int.TryParse(Console.ReadLine(), out input))
                    {
                        isWrong = true;
                        Console.Clear();
                    }
                    else if (input < 0)
                    {
                        isWrong = true;
                        Console.Clear();
                    }
                    else
                    {
                        isWrong = false;
                    }
                } while (isWrong);
                pomiar.dodatkoweJI = input;
                Console.WriteLine("Opis pomiaru:");
                pomiar.opis = Console.ReadLine();

                Console.WriteLine("Pomiar :");
                pomiar.Show_Pomiar();
                Console.WriteLine("Zatwierdzić ?" +
                    "\n1.Tak" +
                    "\n2.Nie");

                while (!Validate_Input(new List<int>() { 1,2},1)) ;
                Console.WriteLine(userMenuChoice.ToString());
                this.DebugStop();
            } while (userMenuChoice != 1);
            return pomiar;
        }
        public bool Display()
        {
            Console.Clear();
            Console.WriteLine("1. Dodaj pomiar");
            Console.WriteLine("2. Wyświetl pomiary");
            Console.WriteLine("0. Exit");
            if (Validate_Input(new List<int>() { 1, 2, 0 }, 0))
            {
                switch (userMenuChoice)
                {
                    case 1:
                        {
                            //add to XML
                            PomiaryGlikemi pomiar = Add_Pomiar();
                            Console.WriteLine("Pomiar data i cukier :\n" + pomiar.Get_Date().ToString() + "\n" + pomiar.cukier.ToString());
                            this.DebugStop();
                            break;
                        }
                    case 2:
                        {
                            while (SubDisplay()) ;
                            XmlGlikemiaHandler xml = new XmlGlikemiaHandler();
                            PomiaryGlikemi pomiar = (PomiaryGlikemi)xml.DeserializeObject(2);
                            pomiar.Show_Pomiar();
                            Console.ReadLine();
                            break;
                        }
                    case 0:
                        {
                            return false;
                        }
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        private bool Validate_Date(string dateString)
        {
            List<string> SplitString = dateString.Split('-').ToList();
            if (SplitString.Count == 3)
            {
                foreach (string part in SplitString)
                {
                    if (!int.TryParse(part, out int dumpVar))
                    {
                        return false;
                    }
                    ////    comment or uncomment for debug
                    //else
                    //{
                    //    Console.WriteLine(dumpVar);
                    //    Console.ReadLine();
                    //}
                }
            }
            else
            {
                return false;
            }
            if (DateTime.TryParse(dateString, out var dump))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void PrintResult(int pos,List<PomiaryGlikemi> pomiary) 
        {
            Console.Clear();
            for (int i = pos; i < pomiary.Count && i < (pos+5); i++)
            {
                pomiary[i].Show_Pomiar();
            }
        }
        private void PrintResult(List<PomiaryGlikemi> pomiary,string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            foreach(PomiaryGlikemi pomiar in pomiary)
            {
                pomiar.Show_Pomiar();
            }
            
        }
        private void ShowResults(List<PomiaryGlikemi> pomiary)
        {
            int currentPosition = 0;
            bool exit = false;
            Console.WriteLine(pomiary.Count);
            PrintResult(currentPosition, pomiary);
            Console.WriteLine("1. Następne 5    2. Poprzednie 5     0. Wyjdź");
            do
            {
                
                if (Validate_Input(new List<int>() { 1, 2, 0 }, 0))
                {
                    switch (userMenuChoice)
                    {
                        case 1:
                            {
                                if (pomiary.Count >= currentPosition + 5)
                                {
                                    currentPosition += 5;
                                    PrintResult(currentPosition, pomiary);
                                }
                                exit = false;
                                break;
                            }
                        case 2:
                            {
                                if (currentPosition - 5 >= 0)
                                {
                                    currentPosition -= 5;
                                    PrintResult(currentPosition, pomiary);
                                }
                                exit = false;
                                break;
                            }
                        case 0:
                            {
                                exit = true;
                                break;
                            }
                    }   
                }
                Console.WriteLine("1. Następne 5    2. Poprzednie 5     0. Wyjdź");
            } while (!exit);
            Console.Clear();
        }
        private DateTime GetDateDialouge(string dateType)
        {
            string dateString;
            do
            {
                Console.Clear();
                Console.WriteLine("Podaj " + dateType);
                dateString = Console.ReadLine();
            } while (!Validate_Date(dateString));
            return DateTime.Parse(dateString);
        }
        private bool SubDisplay()
        {
            Console.Clear();
            Console.WriteLine("1. Pokarz pomiary z przedziału czasowego");
            Console.WriteLine("2. Pokarz pomiary z konkretnego dnia");            
            Console.WriteLine("3. Pokarz pomiar z największym i najniższym cukrem w przedziale czasowym");
            Console.WriteLine("4. Pokarz pomiar z największym i najniższym cukrem konkretnego dnia");
            Console.WriteLine("0. Wyjdź.");
            if(Validate_Input(new List<int>() {1,2,3,4,0 }, 0))
            {
                switch (userMenuChoice)
                {
                    case 1:
                        {
                            DateTime startDate, endDate;
                            do
                            {
                                startDate = GetDateDialouge("datę początkową");

                                endDate = GetDateDialouge("datę końcową");
                                if (startDate > endDate)
                                {
                                    Console.WriteLine("Data początkowa musi być wcześniejsza niż końcowa");
                                    Hold_Execution();
                                }
                            } while (startDate > endDate);
                            // wyszukanie za pomocą LINQ z wszystkich pomiarów wybieramy te w przedziale czasowym zdefiniowanym przez użytkownika.
                            List<PomiaryGlikemi> pomiaryGlikemi = (from pomiar in xmlHandler.DeserializeObjectsAll()
                                                                   where pomiar.Get_Date().Date >= startDate.Date
                                                                   && pomiar.Get_Date().Date <= endDate.Date
                                                                   select pomiar).ToList();    
                            if(pomiaryGlikemi.Count > 0)
                            {
                                ShowResults(pomiaryGlikemi);
                            }
                            else
                            {
                                Console.WriteLine("Brak pomiarów w okresie {0} - {1} ", startDate.Date.ToString(),endDate.Date.ToString());
                                Hold_Execution();
                            }
                            
                            break;
                        }
                    case 2:
                        {
                            DateTime startDate;
                            startDate = GetDateDialouge("datę");
                            // wyszukanie za pomocą LINQ z wszystkich pomiarów wybieramy te w przedziale czasowym zdefiniowanym przez użytkownika.
                            List<PomiaryGlikemi> pomiaryGlikemi = (from pomiar in xmlHandler.DeserializeObjectsAll()
                                                                   where pomiar.Get_Date().Date == startDate.Date
                                                                   select pomiar).ToList();
                            if (pomiaryGlikemi.Count > 0)
                            {
                                ShowResults(pomiaryGlikemi);
                            }
                            else
                            {
                                Console.WriteLine("Brak pomiarów w dniu {0} ", startDate.Date.ToString());
                                Hold_Execution();
                            }
                            
                            break;
                        }
                    case 3:
                        {
                            DateTime startDate, endDate;
                            do
                            {
                                startDate = GetDateDialouge("datę początkową");

                                endDate = GetDateDialouge("datę końcową");
                                if(startDate > endDate)
                                {
                                    Console.WriteLine("Data początkowa musi być wcześniejsza niż końcowa");
                                    Hold_Execution();
                                }
                            } while (startDate > endDate);
                            // wyszukanie za pomocą LINQ z wszystkich pomiarów wybieramy te w przedziale czasowym zdefiniowanym przez użytkownika.
                            List <PomiaryGlikemi> pomiaryGlikemi = (from pomiar in xmlHandler.DeserializeObjectsAll()
                                                                   where pomiar.Get_Date().Date >= startDate.Date
                                                                   && pomiar.Get_Date().Date <= endDate.Date
                                                                   select pomiar).OrderByDescending(p=>p.cukier).ToList();
                            if (pomiaryGlikemi.Count > 0)
                            {
                                pomiaryGlikemi = new List<PomiaryGlikemi>() { pomiaryGlikemi.First(), pomiaryGlikemi.Last() };
                                PrintResult(pomiaryGlikemi, "pomiary z największym i najniższym cukrem\nw danym okresie :");
                                Hold_Execution();
                            }
                            else
                            {
                                Console.WriteLine("Brak pomiarów w okresie {0} - {1} ", startDate.Date.ToString(), endDate.Date.ToString());
                                Hold_Execution();
                            }
                            
                            break;
                        }
                    case 4:
                        {
                            DateTime startDate;
                            startDate = GetDateDialouge("datę");
                            // wyszukanie za pomocą LINQ z wszystkich pomiarów wybieramy te w przedziale czasowym zdefiniowanym przez użytkownika.
                            List<PomiaryGlikemi> pomiaryGlikemi = (from pomiar in xmlHandler.DeserializeObjectsAll()
                                                                   where pomiar.Get_Date().Date == startDate.Date
                                                                   select pomiar).OrderByDescending(p => p.cukier).ToList();
                            if (pomiaryGlikemi.Count > 0)
                            {
                                pomiaryGlikemi = new List<PomiaryGlikemi>() { pomiaryGlikemi.First(), pomiaryGlikemi.Last() };
                                PrintResult(pomiaryGlikemi, "pomiary z największym i najniższym cukrem\nw dniu :");
                                Hold_Execution();
                            }
                            else
                            {
                                Console.WriteLine("Brak pomiarów w dniu {0} ", startDate.Date.ToString());
                                Hold_Execution();
                            }
                            
                            break;
                        }
                    case 0:
                        {
                            return false;
                        }
                }
                return true;
            }
            return true;
        }

    }
}
