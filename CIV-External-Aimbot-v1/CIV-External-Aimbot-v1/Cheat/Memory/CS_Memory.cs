//
// MemoryHandler-Class
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;


class CMemoryHandler
{
    public static Process[] csgo;

    public enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VMOperation = 0x00000008,
        VMRead = 0x00000010,
        VMWrite = 0x00000020,
        DupHandle = 0x00000040,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        Synchronize = 0x00100000
    }

    [StructLayout(LayoutKind.Explicit)]
    struct UnionArray
    {
        [FieldOffset(0)]
        public byte[] Bytes;

        [FieldOffset(0)]
        public double[] Doubles;
    }

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, byte[] dbToWrite, UIntPtr nSize, IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    public static extern Int32 CloseHandle(IntPtr hProcess);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, out float lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll",EntryPoint = "ReadProcessMemory")]
    private static extern bool ReadProcessMemoryGV(IntPtr hProcess, UIntPtr lpBaseAddress, out CGlobalVarsBase lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
    private static extern float WriteProcessMemoryFloat(int Handle, int Address, ref float Value, int Size, ref int BytesWritten); 

    public static double readDouble2(long Address)
    {
        float cast = 0;
        ReadProcessMemory(pHandle, (UIntPtr)Address, out cast, (UIntPtr)4, IntPtr.Zero);
        double ret = (double)cast;
        return ret;
    }

    public static IntPtr pHandle = new IntPtr();

    public static int readInt(long Address)
    {
        byte[] buffer = new byte[sizeof(int)];
        ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
        return BitConverter.ToInt32(buffer, 0);
    }

    public static double readDouble(long Address)
    {
        byte[] buffer = new byte[sizeof(double)];
        ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)4, IntPtr.Zero);
        return BitConverter.ToDouble(buffer, 0);
    }

    

    public static double[] readDoubleArray(long Address)
    {
        byte[] buffer = new byte[sizeof(double)];
        double[] ret = new double[3];
        ReadProcessMemory(pHandle, (UIntPtr)Address, buffer, (UIntPtr)(4), IntPtr.Zero);
        ret[0] = BitConverter.ToDouble(buffer, 0);
        ReadProcessMemory(pHandle, (UIntPtr)Address + 4, buffer, (UIntPtr)(4), IntPtr.Zero);
        ret[1] = BitConverter.ToDouble(buffer, 0);
        ReadProcessMemory(pHandle, (UIntPtr)Address + 8, buffer, (UIntPtr)(4), IntPtr.Zero);
        ret[2] = BitConverter.ToDouble(buffer, 0);
        
        return ret;
    }

    public static CGlobalVarsBase readGlobals()
    {
        CGlobalVarsBase ret = new CGlobalVarsBase();
        ReadProcessMemoryGV(pHandle, (UIntPtr)(GetEngineDll() + (int)Offsets.CGlobalVarsBase),out ret, (UIntPtr)(84), IntPtr.Zero);
        return ret;
    }

    public static void writeDouble(long Address, double ToWrite)
    {
        float cast = (float)ToWrite;
        int bytes = 0;
        WriteProcessMemoryFloat((int)pHandle, (int)Address, ref cast, (int)(4), ref bytes);
    }

    public static void ProcessOpen()
    {
        do
        {
            csgo = Process.GetProcessesByName("csgo");
        } while (csgo[0].ToString() != "System.Diagnostics.Process (csgo)");

        Console.WriteLine("CS:GO found!");

        Console.Write("Handle opened!");

        Console.WriteLine("ProcessID: " + Convert.ToString(csgo[0].Id));

    }

    public static int GetClientDll()
    {
        int temp = 0;
        ProcessModuleCollection modules = csgo[0].Modules;
        foreach (ProcessModule module in modules)
        {
            if (module.ModuleName == "client.dll")
            {
                temp = module.BaseAddress.ToInt32();
            }
        }
        return temp;
    }

    public static int GetEngineDll()
    {
        int temp = 0;
        ProcessModuleCollection modules = csgo[0].Modules;
        foreach (ProcessModule module in modules)
        {
            if (module.ModuleName == "engine.dll")
            {
                temp = module.BaseAddress.ToInt32();
            }
        }
        return temp;
    }
}

class Offsets
{
    public static ulong pLocalPlayerBase = 0xA68A14;
    public static ulong pPlayerBase = 0x4A0B0C4;
    public static ulong pBoneBase = 0xA78;
    public static ulong m_iHealth = 0xFC;
    public static ulong m_iTeamNum = 0xF0;
    public static ulong m_blifestate = 0x25B;
    public static ulong EnginePointer = 0x50D054;
    public static ulong pEnginePosition = 0x5B46A4;
    public static ulong pEngineVAngs = 0x4C88;
    public static ulong m_fFlags = 0x100;
    public static ulong m_Local = 0x136C;
    public static ulong m_vecAimPunch = 0x13DC;
    public static ulong CGlobalVarsBase = 0x50CCF0;
}