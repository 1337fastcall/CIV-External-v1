//
// Input(Keys)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

//User32.dll

class User32
{
    [DllImport("User32.dll")]
    private extern static bool GetAsyncKeyState(int Key);

    public static bool GetAsyncKeyStateC(int vKey)
    {
        bool state;
        state = GetAsyncKeyState(vKey);
        return state;
    }
}