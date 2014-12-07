//
// CUser32
// Imported C++
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;


class CUser32
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