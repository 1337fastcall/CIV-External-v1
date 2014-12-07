//
// CAimbot
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


class CAimbot
{
    public static void doAim()
    {
        CBaseEntity.CEntity pLocal = new CBaseEntity.CEntity();
        CBaseEntity.CEntity pEntity = new CBaseEntity.CEntity();

        float[] fAngles = new float[3];
        float[] fSmooth = new float[3];

        CBaseEntity.CEntityRead.ReadLocalPlayer(pLocal);

        pEntity.iBone = 10;

        for (int i = 0; i < 64; i++)
        {
            pEntity.Player = i;
            CBaseEntity.CEntityRead.ReadPlayer(pEntity);

            float fov_temp = CMath.Aimbot.Get_FOV(pLocal.vAngs, pLocal.vPositionInEngine, pEntity.vBone);

            if (CUser32.GetAsyncKeyStateC(0x01))
            {

                if (fov_temp < 1.8 && pEntity.iHealth > 1 && pEntity.iTeam != pLocal.iTeam)
                {
                    CMath.Aimbot.CalcAimAngles(pLocal.vPositionInEngine, pEntity.vBone, fAngles);

                    CMemory.WriteFloatArray(pLocal.pEnginePointer + 0x4C90, fAngles, 3);
                }
            }
        }
    }
}