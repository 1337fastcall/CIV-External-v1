//
// CMemory
// Pure C#  
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;


class CMemory
{
    public  static Process [ ]  CSGO;

    public  static IntPtr       pHandle;

    

    public  static int          ReadInt(long Address)
    {
        int ret = 0;
        CKernel32.ReadProcessMemoryInt(pHandle, (UIntPtr)(Address), out ret, (UIntPtr)4, IntPtr.Zero);
        return ret;
    }

    public  static float        ReadFloat(long Address)
    {
        float ret = 0;

        CKernel32.ReadProcessMemoryFloat(pHandle, (UIntPtr)(Address), out ret, (UIntPtr)4, IntPtr.Zero);

        return ret;
    }

    public  static int     [ ]  ReadIntArray(long Address, int ArraySize)
    {
        int[] ret = new int[ArraySize];

        for(int i = 0; i < ArraySize; i++)
            CKernel32.ReadProcessMemoryInt(pHandle, (UIntPtr)(Address + (sizeof(int) * i)), out ret[i], (UIntPtr)4, IntPtr.Zero);

        return ret;
    }

    public  static float   [ ]  ReadFloatArray(long Address, int ArraySize)
    {
        float[] ret = new float[ArraySize];

        for (int i = 0; i < ArraySize; i++)
            CKernel32.ReadProcessMemoryFloat(pHandle, (UIntPtr)(Address + (sizeof(float) * i)), out ret[i], (UIntPtr)4, IntPtr.Zero);

        return ret;
    }

    public  static void         WriteInt(long Address, int Value)
    {
        int _zero  = 0;
        CKernel32.WriteProcessMemoryInt(pHandle, (UIntPtr)Address, ref Value, (UIntPtr)4, ref _zero);
    }

    public  static void         WriteFloat(long Address, float Value)
    {
        int _zero = 0;
        CKernel32.WriteProcessMemoryFloat(pHandle, (UIntPtr)Address, ref Value, (UIntPtr)4, ref _zero);
    }

    public  static void         WriteIntArray(long Address, int[] Values, int ArraySize)
    {
        int _zero = 0;

        for (int i = 0; i < ArraySize; i++)
            CKernel32.WriteProcessMemoryInt(pHandle, (UIntPtr)Address, ref Values[i], (UIntPtr)4, ref _zero);

    }

    public  static void         WriteFloatArray(long Address, float[] Values, int ArraySize)
    {
        int _zero = 0;

        for (int i = 0; i < ArraySize; i++)
            CKernel32.WriteProcessMemoryFloat(pHandle, (UIntPtr)(Address + (sizeof(float) * i)), ref Values[i], (UIntPtr)4, ref _zero);

    }

    private static void         GetModules()
    {
        ProcessModuleCollection modules = CSGO[0].Modules;

        foreach (ProcessModule module in modules)
        {
            if (module.ModuleName == "client.dll")
            {
                CGamePointers.Client_Dll = module.BaseAddress.ToInt32();
            }
        }

        foreach (ProcessModule module in modules)
        {
            if (module.ModuleName == "engine.dll")
            {
                CGamePointers.Engine_Dll = module.BaseAddress.ToInt32();
            }
        }
    }

    public  static void         Init()
    {
        do
        {
            CSGO = Process.GetProcessesByName("csgo");
        } while (CSGO == null);


        GetModules();

        pHandle = CKernel32.OpenProcess((uint)CKernel32.ProcessAccessFlags.All, false, CSGO[0].Id);
    }
}