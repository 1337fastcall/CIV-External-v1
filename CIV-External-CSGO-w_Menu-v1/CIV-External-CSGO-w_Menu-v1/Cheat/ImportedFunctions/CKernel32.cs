//
// CKernel32
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


class CKernel32
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern Int32 CloseHandle(IntPtr hProcess);

    [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
    public static extern bool ReadProcessMemoryInt(IntPtr hProcess, UIntPtr lpBaseAddress, out int lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
    public static extern bool ReadProcessMemoryFloat(IntPtr hProcess, UIntPtr lpBaseAddress, out float lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
    public static extern bool WriteProcessMemoryInt(IntPtr hProcess, UIntPtr lpBaseAddress, ref int lpBuffer, UIntPtr nSize, ref int lpNumberOfBytesRead); 

    [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
    public static extern bool WriteProcessMemoryFloat(IntPtr hProcess, UIntPtr lpBaseAddress, ref float lpBuffer, UIntPtr nSize, ref int lpNumberOfBytesRead);

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
}