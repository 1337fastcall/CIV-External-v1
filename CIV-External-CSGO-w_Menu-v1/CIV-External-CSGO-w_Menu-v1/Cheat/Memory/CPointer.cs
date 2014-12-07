//
// CPointer
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

using System.Text.RegularExpressions;
using System.IO;


class CGamePointers
{
    public static int Client_Dll;
    public static int Engine_Dll;

    public static int pLocalPlayerBase = 0xA68A14;
    public static int pPlayerBase = 0x4A0B0C4;
    public static int pBoneBase = 0xA78;
    public static int m_iHealth = 0xFC;
    public static int m_iTeamNum = 0xF0;
    public static int m_blifestate = 0x25B;
    public static int EnginePointer = 0x50D054;
    public static int pEnginePosition = 0x5B46A4;
    public static int pEngineVAngs = 0x4C88;
    public static int m_fFlags = 0x100;
    public static int m_Local = 0x136C;
    public static int m_vecAimPunch = 0x13DC;
    public static int CGlobalVarsBase = 0x50CCF0;
    public static int m_hActiveWeapon = 0x12C0;
    public static int WeaponID = 0x1684;
}

class CReadPointersFromFile
{
    static string[] AllLines = new string[101];

    static void InitAllLines()
    {
        AllLines = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\" + "Offsets.txt");
    }

    static int ReadValue(int LineToRead)
    {
        string temp = "";

        temp = AllLines[LineToRead];

        string _new = Regex.Replace(temp, @"([a-z])", "");
        _new = Regex.Replace(_new, @"=", "");
        _new = Regex.Replace(_new, @" ", "");

        int iRet = int.Parse(_new, System.Globalization.CultureInfo.InvariantCulture);

        return iRet;
    }

    public static void ReadFile()
    {
        for (int i = 0; i < 101; i++)
        {
            AllLines[i] = "";
        }

        InitAllLines();

        for (int i = 0; i < 101; i++)
        {
            switch (AllLines[i])
            {
                case "pLocalPlayerBase":
                    CGamePointers.pLocalPlayerBase = ReadValue(i);
                    break;
                case "pPlayerBase":
                    CGamePointers.pPlayerBase = ReadValue(i);
                    break;
                case "pBoneBase":
                    CGamePointers.pBoneBase = ReadValue(i);
                    break;
                case "m_iHealth":
                    CGamePointers.m_iHealth = ReadValue(i);
                    break;
                case "m_iTeamNum":
                    CGamePointers.m_iTeamNum = ReadValue(i);
                    break;
                case "m_blifestate":
                    CGamePointers.m_blifestate = ReadValue(i);
                    break;
                case "EnginePointer":
                    CGamePointers.EnginePointer = ReadValue(i);
                    break;
                case "pEngineVAngs":
                    CGamePointers.pEngineVAngs = ReadValue(i);
                    break;

                // Add more cases for other offsets

            }
        }
    }
}