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
                Console.WriteLine("Data         : " + pomiar.Get_Date().ToString());
                Console.WriteLine("Cukier       : " + pomiar.cukier.ToString());
                Console.WriteLine("Dodatkowe JI : " + pomiar.dodatkoweJI.ToString());
                Console.WriteLine("Opis         : \n" + pomiar.opis);
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
                            pomiar.ShowPomiar();
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
            if (!dateString.Contains("-"))
            {
                return false;
            }
            else
            {

            }
            return true;
        }
        private bool SubDisplay()
        {
            Console.Clear();
            Console.WriteLine("1. Pokarz pomiary z przedziału czasowego");
            Console.WriteLine("2. Pokarz pomiary z konkretnego dnia");
            Console.WriteLine("3. Pokarz pomiar z największym cukrem");
            Console.WriteLine("4. Pokarz pomiar z największym cukrem w przedziale czasowym");
            Console.WriteLine("5. Pokarz pomiar z największym cukrem konkretnego dnia");
            Console.WriteLine("0. Wyjdź.");
            if(Validate_Input(new List<int>() {1,2,3,4,5,0 }, 0))
            {
                switch (userMenuChoice)
                {
                    case 1:
                        {
                            
                            break;
                        }
                    case 2:
                        {

                            break;
                        }
                    case 3:
                        {

                            break;
                        }
                    case 4:
                        {

                            break;
                        }
                    case 5:
                        {

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
