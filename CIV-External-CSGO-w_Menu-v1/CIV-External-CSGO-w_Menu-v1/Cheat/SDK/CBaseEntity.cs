//
// CBaseEntity
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


namespace CBaseEntity
{
    public partial class CEntity
    {
        public  int Player;

        public  int pBase;
        public  int pEnginePointer;
        public  int pBoneBase;

        public  int iHealth;
        public  int iTeam;
        public  int iFlags;
        public  int iWeaponID;
        public  int iBone;

        public  float[] vOrigin;
        public  float[] vBone;
        public  float[] vAngs;
        public  float[] vPunch;
        public  float[] vOldPunch;
        public  float[] vPositionInEngine;

        public bool bIsAlive;
    }

    public partial class CEntityRead
    {
        public static void ReadLocalPlayer(CEntity pLocal)
        {
            pLocal.pBase = CMemory.ReadInt(CGamePointers.Client_Dll + CGamePointers.pLocalPlayerBase);
            pLocal.iHealth = CMemory.ReadInt(pLocal.pBase + CGamePointers.m_iHealth);
            pLocal.iTeam = CMemory.ReadInt(pLocal.pBase + CGamePointers.m_iTeamNum);
            pLocal.iFlags = CMemory.ReadInt(pLocal.pBase + CGamePointers.m_fFlags);
            pLocal.bIsAlive = Convert.ToBoolean(CMemory.ReadInt(pLocal.pBase + CGamePointers.m_blifestate));
            
            pLocal.pEnginePointer = CMemory.ReadInt(CGamePointers.Engine_Dll + CGamePointers.EnginePointer);
            pLocal.vPositionInEngine = CMemory.ReadFloatArray(CGamePointers.Engine_Dll + CGamePointers.pEnginePosition, 3);
            pLocal.vAngs = CMemory.ReadFloatArray(pLocal.pEnginePointer + CGamePointers.pEngineVAngs, 3);

            ReadWeapon(pLocal);

            pLocal.vPunch = CMemory.ReadFloatArray(pLocal.pBase + CGamePointers.m_vecAimPunch, 3);

            pLocal.vOldPunch = new float[3];
            pLocal.vPunch = new float[3];

            for (int _n = 0; _n < 3; _n++)
            {
                if (pLocal.vPunch[_n] == 0)
                {
                    pLocal.vOldPunch[_n] = 0;
                }
            }
        }

        public static void ReadPlayer(CEntity pEntity)
        {
            ReadEntity(pEntity);

            pEntity.vBone = new float[3];
            pEntity.pBoneBase = CMemory.ReadInt(pEntity.pBase + CGamePointers.pBoneBase);
            pEntity.vBone[0] = CMemory.ReadFloat(pEntity.pBoneBase + 0x30 * pEntity.iBone + 0x0C);
            pEntity.vBone[1] = CMemory.ReadFloat(pEntity.pBoneBase + 0x30 * pEntity.iBone + 0x1C);
            pEntity.vBone[2] = CMemory.ReadFloat(pEntity.pBoneBase + 0x30 * pEntity.iBone + 0x2C);
        }

        public static void ReadEntity(CEntity pEntity)
        {
            pEntity.pBase = CMemory.ReadInt(CGamePointers.Client_Dll + CGamePointers.pPlayerBase + 0x10 * pEntity.Player);
            pEntity.iHealth = CMemory.ReadInt(pEntity.pBase + CGamePointers.m_iHealth);
            pEntity.iTeam = CMemory.ReadInt(pEntity.pBase + CGamePointers.m_iTeamNum);
            pEntity.iFlags = CMemory.ReadInt(pEntity.pBase + CGamePointers.m_fFlags);
            pEntity.bIsAlive = Convert.ToBoolean(CMemory.ReadInt(pEntity.pBase + CGamePointers.m_blifestate));
        }

        private static void ReadWeapon(CEntity pEntity)
        {
            int WeaponHandle = CMemory.ReadInt(pEntity.pBase + CGamePointers.m_hActiveWeapon);
            int index = WeaponHandle & 0xFFF;
            int WeaponBase = CMemory.ReadInt(CGamePointers.Client_Dll + CGamePointers.pPlayerBase + ((index - 1) * 0x10));
            pEntity.iWeaponID = CMemory.ReadInt(WeaponBase + CGamePointers.WeaponID);
        }
    } 
}