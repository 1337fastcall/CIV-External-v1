//
// Entity-Class definiert
//

using System;
using System.IO;
using System.Text.RegularExpressions;


class Entity
{
    // Vars
    public int pBase, pBoneBase, pEnginePointer;
    public int IHealth, ITeam, IFlags, IWeaponID;
    public double[] vecHead, vecOrigin, EnginePos, vAngs, vPunch, vOldPunch = new double[3];
    bool bIsAlive;
    public CGlobalVarsBase pGlobals;
}



struct CGlobalVarsBase
{
    public  float realtime;
    public  int framecount;
    public  float absolute_frametime;
    public  float absolute_framestarttimestddev;
    public  float curtime;
    public  float frameTime;
    public  int maxClients;
    public  int tickcount;
    public  float interval_per_tick;
    public  float interpolation_amount;
    public  int simThicksThisFrame;
    public  int network_protocol;
}