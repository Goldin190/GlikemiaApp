using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlikemiaApp
{
    class PotrawyMenu : IMenu
    {
        int userMenuChoice;
        public void Hold_Execution()
        {
            Console.WriteLine("Kilknij Enter aby kontynuować");
            Console.ReadLine();
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
        public bool Display()
        {
            Console.Clear();
            Console.WriteLine("1. Posiłki");
            Console.WriteLine("2. Potrawy");
            Console.WriteLine("3. Gotowe Potrawy");
            Console.WriteLine("4. Zapisane Potrawy");
            Console.WriteLine("0. Exit");
            if (Validate_Input(new List<int>() { 1, 2, 3,4, 0 }, 0))
            {
                switch (userMenuChoice)
                {
                    case 1:
                        {
                            //DeitaObject
                            break;
                        }
                    case 2:
                        {
                            GlikemiaMenu glikemiaMenu = new GlikemiaMenu();
                            while (glikemiaMenu.Display()) ;
                            break;
                        }
                    case 3:
                        {
                            //PotrawyObject
                            break;
                        }
                    case 4:
                        {
                            //PotrawyObject
                            break;
                        }
                    case 0:
                        {
                            Console.WriteLine("Exitiing");
                            System.Threading.Thread.Sleep(2000);
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
    }
}
