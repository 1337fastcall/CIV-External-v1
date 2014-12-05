using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace CIV_External_Aimbot_v1
{
    class Program
    {
        
        static void Main(string[] args)
        {

            CMemoryHandler.ProcessOpen();
            CMemoryHandler.pHandle = CMemoryHandler.OpenProcess((uint)0x1F0FF, false, CMemoryHandler.csgo[0].Id);
            
            while (true)
            {
                //CMisc.DisplayLocalPlayerInfo();

                CAimbot.doAim();

                Thread.Sleep(1);
                Console.Clear();
            }        
        }
    }
}