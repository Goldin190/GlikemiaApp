using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlikemiaApp
{
    interface IMenu
    {
        void Show_Error_Message(string msg);
        bool Validate_Input(List<int> range, int exitValue);
        bool Display();
    }
}
