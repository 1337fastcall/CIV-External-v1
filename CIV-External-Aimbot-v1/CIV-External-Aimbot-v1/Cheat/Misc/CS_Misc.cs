//
// Misc-Class definiert
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

class CMisc
{
    static Entity pLocalPlayer = new Entity();
    public static void Bunnyhop()
    {
        


    }

    public static void DisplayLocalPlayerInfo()
    {
        

        pLocalPlayer.pBase = CMemoryHandler.readInt(CMemoryHandler.GetClientDll() + 0xA68A14);
        pLocalPlayer.IHealth = CMemoryHandler.readInt(pLocalPlayer.pBase + 0xFC);
        pLocalPlayer.ITeam = CMemoryHandler.readInt(pLocalPlayer.pBase + 0xF0);
        int n_tick = CMemoryHandler.readInt(pLocalPlayer.pBase + 0x17C0);
        pLocalPlayer.IFlags = CMemoryHandler.readInt(pLocalPlayer.pBase + 0x100);


        int weapon_handle = CMemoryHandler.readInt(pLocalPlayer.pBase + 0x12C0);
        int index = weapon_handle & 0xFFF;
        int weaponbase = CMemoryHandler.readInt(CMemoryHandler.GetClientDll() + 0x4A0B0C4 + ((index - 1) * 0x10));
        pLocalPlayer.IWeaponID = CMemoryHandler.readInt(weaponbase + 0x1684);

        pLocalPlayer.vecOrigin = new double[3];
        pLocalPlayer.vecOrigin[0] = CMemoryHandler.readDouble2((pLocalPlayer.pBase + 0x134));
        pLocalPlayer.vecOrigin[1] = CMemoryHandler.readDouble2((pLocalPlayer.pBase + 0x134 + 4));
        pLocalPlayer.vecOrigin[2] = CMemoryHandler.readDouble2((pLocalPlayer.pBase + 0x134 + 8));


        pLocalPlayer.pEnginePointer = CMemoryHandler.readInt(CMemoryHandler.GetEngineDll() + (int)Offsets.EnginePointer);

        pLocalPlayer.EnginePos = new double[3];
        pLocalPlayer.EnginePos[0] = CMemoryHandler.readDouble2(CMemoryHandler.GetEngineDll() + (int)Offsets.pEnginePosition);
        pLocalPlayer.EnginePos[1] = CMemoryHandler.readDouble2(CMemoryHandler.GetEngineDll() + (int)Offsets.pEnginePosition + 4);
        pLocalPlayer.EnginePos[2] = CMemoryHandler.readDouble2(CMemoryHandler.GetEngineDll() + (int)Offsets.pEnginePosition + 8);

        pLocalPlayer.vAngs = new double[3];
        pLocalPlayer.vAngs[0] = CMemoryHandler.readDouble2(pLocalPlayer.pEnginePointer + 0x4C90);
        pLocalPlayer.vAngs[1] = CMemoryHandler.readDouble2(pLocalPlayer.pEnginePointer + 0x4C90 + 4);
        pLocalPlayer.vAngs[2] = CMemoryHandler.readDouble2(pLocalPlayer.pEnginePointer + 0x4C90 + 8);


        // Global Vars
        


        pLocalPlayer.vPunch = new double[3];
        pLocalPlayer.vPunch[0] = CMemoryHandler.readDouble2(pLocalPlayer.pBase + (int)Offsets.m_vecAimPunch);
        pLocalPlayer.vPunch[1] = CMemoryHandler.readDouble2(pLocalPlayer.pBase + (int)Offsets.m_vecAimPunch + 4);
        pLocalPlayer.vPunch[2] = CMemoryHandler.readDouble2(pLocalPlayer.pBase + (int)Offsets.m_vecAimPunch + 8);

        Console.WriteLine("LocalPlayer Information:\n");
        Console.WriteLine(" -> Health: " + pLocalPlayer.IHealth);
        Console.WriteLine(" -> Team: " + pLocalPlayer.ITeam);
        Console.WriteLine(" -> m_nTickbase: " + n_tick);
        Console.WriteLine(" -> WeaponID: " + pLocalPlayer.IWeaponID);


        if (Convert.ToBoolean(pLocalPlayer.IFlags & (1 << 0)))
        {
            Console.WriteLine(" -> Flags: On Ground");
        }
        else
        {
            Console.WriteLine(" -> Flags: In Air");
        }

        Console.Write("\n");
        Console.WriteLine(" -> Position: ");
        Console.WriteLine("       X:    " + pLocalPlayer.EnginePos[0]);
        Console.WriteLine("       Y:    " + pLocalPlayer.EnginePos[1]);
        Console.WriteLine("       Z:    " + pLocalPlayer.EnginePos[2]);

        Console.Write("\n");
        Console.WriteLine(" -> ViewAngles: ");
        Console.WriteLine("       X:    " + pLocalPlayer.vAngs[0]);
        Console.WriteLine("       Y:    " + pLocalPlayer.vAngs[1]);
        Console.WriteLine("       Z:    " + pLocalPlayer.vAngs[2]);

        Console.Write("\n");
        Console.WriteLine(" -> PunchAngles: ");
        Console.WriteLine("       X:    " + pLocalPlayer.vPunch[0]);
        Console.WriteLine("       Y:    " + pLocalPlayer.vPunch[1]);
        Console.WriteLine("       Z:    " + pLocalPlayer.vPunch[2]);

        // <testing>
        //Console.WriteLine("interval_per_tick: " + pLocalPlayer.pGlobals.interval_per_tick);
        // </testing>
        
        for (int _n = 0; _n < 3; _n++)
        {
            if (pLocalPlayer.vPunch[_n] == 0)
            {
                pLocalPlayer.vOldPunch[_n] = 0;
            }
        }

        if (User32.GetAsyncKeyStateC(0x01))
        {
            // <NoRecoil>

            // <NoRecoil> // still testing
            pLocalPlayer.vAngs[0] = pLocalPlayer.vAngs[0] - (pLocalPlayer.vPunch[0] * 2) + (pLocalPlayer.vOldPunch[0] * 2);
            pLocalPlayer.vAngs[1] = pLocalPlayer.vAngs[1] - (pLocalPlayer.vPunch[1] * 2) + (pLocalPlayer.vOldPunch[1] * 2);
            pLocalPlayer.vAngs[2] = 0;
            pLocalPlayer.vOldPunch = pLocalPlayer.vPunch;
            // </NoRecoil>  

          //  CAimbotMath.NormalizeAngles(pLocalPlayer.vAngs); // Normalize them (sdk func)
            CMemoryHandler.writeDouble((int)(pLocalPlayer.pEnginePointer + 0x4C90), pLocalPlayer.vAngs[0]); // write pitch
            CMemoryHandler.writeDouble((int)(pLocalPlayer.pEnginePointer + 0x4C90 + 4), pLocalPlayer.vAngs[1]); // write yaw
            // roll is always 0, you don't have to write it
        }
         
    }
}