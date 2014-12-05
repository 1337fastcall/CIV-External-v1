//
// Aimbot-Class
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

class CAimbot
{
    public static void doAim()
    {
        Entity pLocal = new Entity();
        double[] dbAimAngles = new double[3];

        //pLocal.vOldPunch = new double[3];
        
        int smooth_value = 1;
        
        pLocal.pBase = CMemoryHandler.readInt(CMemoryHandler.GetClientDll() + (int)Offsets.pLocalPlayerBase); // LocalBasePointer
        pLocal.IHealth = CMemoryHandler.readInt(pLocal.pBase + (int)Offsets.m_iHealth); // LocalHealth
        pLocal.ITeam = CMemoryHandler.readInt(pLocal.pBase + (int)Offsets.m_iTeamNum); // Team of LocalPlayer
        int n_tick = CMemoryHandler.readInt(pLocal.pBase + 0x17C0); // Ticks of LocalClient
        pLocal.IFlags = CMemoryHandler.readInt(pLocal.pBase + (int)Offsets.m_fFlags); // Flags of LocalPlayer

        // Engine Pointer
        pLocal.pEnginePointer = CMemoryHandler.readInt(CMemoryHandler.GetEngineDll() + (int)Offsets.EnginePointer);

        // Position of LocalPlayer in engine.dll
        pLocal.EnginePos = new double[3];
        pLocal.EnginePos[0] = CMemoryHandler.readDouble2(CMemoryHandler.GetEngineDll() + (int)Offsets.pEnginePosition);
        pLocal.EnginePos[1] = CMemoryHandler.readDouble2(CMemoryHandler.GetEngineDll() + (int)Offsets.pEnginePosition + 4);
        pLocal.EnginePos[2] = CMemoryHandler.readDouble2(CMemoryHandler.GetEngineDll() + (int)Offsets.pEnginePosition + 8);

        // View Angles of LocalPlayer in engine.dll
        pLocal.vAngs = new double[3];
        pLocal.vAngs[0] = CMemoryHandler.readDouble2(pLocal.pEnginePointer + 0x4C90);
        pLocal.vAngs[1] = CMemoryHandler.readDouble2(pLocal.pEnginePointer + 0x4C90 + 4);
        pLocal.vAngs[2] = CMemoryHandler.readDouble2(pLocal.pEnginePointer + 0x4C90 + 8);

        int weapon_handle = CMemoryHandler.readInt(pLocal.pBase + 0x12C0);
        int index = weapon_handle & 0xFFF;
        int weaponbase = CMemoryHandler.readInt(CMemoryHandler.GetClientDll() + 0x4A0B0C4 + ((index - 1) * 0x10));
        pLocal.IWeaponID = CMemoryHandler.readInt(weaponbase + 0x1684);

        pLocal.vPunch = new double[3];
        pLocal.vPunch[0] = CMemoryHandler.readDouble2(pLocal.pBase + (int)Offsets.m_vecAimPunch);
        pLocal.vPunch[1] = CMemoryHandler.readDouble2(pLocal.pBase + (int)Offsets.m_vecAimPunch + 4);
        pLocal.vPunch[2] = CMemoryHandler.readDouble2(pLocal.pBase + (int)Offsets.m_vecAimPunch + 8);


        for (int _n = 0; _n < 3; _n++)
        {
            if (pLocal.vPunch[_n] == 0)
            {
                pLocal.vOldPunch[_n] = 0;
            }
        }
        

        for (int i = 0; i < 64; i++)
        {
            Entity pEntity = new Entity();
            
            int ent = 0;
            if(i != 0)
                ent = i - 1;
            int baseOffset = CMemoryHandler.GetClientDll() + (int)Offsets.pPlayerBase + 0x10 * ent; 
            pEntity.pBase = CMemoryHandler.readInt(baseOffset); // EntityBasePointer
            pEntity.IHealth = CMemoryHandler.readInt(pEntity.pBase + 0xFC); // health of the entity
            pEntity.ITeam = CMemoryHandler.readInt(pEntity.pBase + 0xF0); // team of the entity



            
            

            if (pEntity.pBase == 0) // if nullpointer, next playerid
                break;

            // Read HeadbonePosition
            pEntity.vecHead = new double[3];
            pEntity.pBoneBase = CMemoryHandler.readInt(pEntity.pBase + (int)Offsets.pBoneBase);
            pEntity.vecHead[0] = CMemoryHandler.readDouble2(pEntity.pBoneBase + 0x30 * 10 + 0x0C);
            pEntity.vecHead[1] = CMemoryHandler.readDouble2(pEntity.pBoneBase + 0x30 * 10 + 0x1C);
            pEntity.vecHead[2] = CMemoryHandler.readDouble2(pEntity.pBoneBase + 0x30 * 10 + 0x2C);

            // Get the fov
            double fov_check = CAimbotMath.Get_FOV(pLocal.vAngs, pLocal.EnginePos, pEntity.vecHead);

            if (User32.GetAsyncKeyStateC(0x01))
            {
                

                if (fov_check < 1.8 && pEntity.IHealth > 1 && pEntity.ITeam != pLocal.ITeam) // Check if in fov,check if in enemy team, and if not knife or bomb
                {
                    if ((pLocal.IWeaponID == 42) || (pLocal.IWeaponID == 49))
                        break;

                    
                    
                    CAimbotMath.CalcAimAngles(pLocal.EnginePos, pEntity.vecHead, dbAimAngles); // Calc the angles

                      

                    // <Smooth> // still testing
                    double[] dbSmoothed = new double[3];
                    CAimbotMath.Smooth(dbAimAngles, dbSmoothed, pLocal.vAngs, smooth_value);
                    // </Smooth>

                    // <NoRecoil> // still testing
                    if (pLocal.IWeaponID != 9)
                    {
                        dbSmoothed[0] = dbSmoothed[0] - (pLocal.vPunch[0] * 2) + (pLocal.vOldPunch[0] * 2);
                        dbSmoothed[1] = dbSmoothed[1] - (pLocal.vPunch[1] * 2) + (pLocal.vOldPunch[1] * 2);
                        dbSmoothed[2] = 0;
                        pLocal.vOldPunch = pLocal.vPunch;
                    }
                    // </NoRecoil>

                    CAimbotMath.NormalizeAngles(dbSmoothed); // Normalize them (sdk func)
                    CMemoryHandler.writeDouble((int)(pLocal.pEnginePointer + 0x4C90), dbSmoothed[0]); // write pitch
                    CMemoryHandler.writeDouble((int)(pLocal.pEnginePointer + 0x4C90 + 4), dbSmoothed[1]); // write yaw
                    // roll is always 0, you don't have to write it
                }
            }
        }
    }
    public bool bKeyPress()
    {
        return true;
    }
}