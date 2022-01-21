using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlikemiaApp
{
    class MainMenu : IMenu
    {
        private int userMenuChoice;

        public void Hold_Execution()
        {
            Console.WriteLine("Kilknij Enter aby kontynuować");
            Console.ReadLine();
        }
        public void Show_Error_Message(string msg)
        {
            Console.WriteLine("Wrong input:");
            Console.WriteLine(msg);
        }
        public bool Validate_Input(List<int> range, int exitValue )
        {
            string errMsg = "";
            if(int.TryParse(Console.ReadLine(),out userMenuChoice))
            {
                if (range.Contains(userMenuChoice))
                {
                    return true;
                }
                else
                {
                    errMsg += "Invalid Option \n";
                }
            }
            else
            {
                errMsg += "Enter number only.\n";
            }
            Console.Clear();
            this.Show_Error_Message(errMsg);
            return false;
        }
        public bool Display()
        {
            Console.Clear();
            Console.WriteLine("1. Dieta");
            Console.WriteLine("2. Glikemia");
            Console.WriteLine("3. Potrawy");
            Console.WriteLine("0. Exit");
            if (Validate_Input(new List<int>() { 1, 2, 3, 0 }, 0))
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
