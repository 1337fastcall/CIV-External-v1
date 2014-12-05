using System;
using System.IO;
using System.Text.RegularExpressions;


class COffsets
{
    static string[] allLines = new string[101];

    static void Init()
    {
        allLines = System.IO.File.ReadAllLines(@"C:\Users\__fastcall\Desktop\Offsets.txt");
    }

    public static double readDouble(int LineToRead)
    {
        string temp = "";

        temp = allLines[LineToRead];

        string _new = Regex.Replace(temp, @"([a-z])", "");
        _new = Regex.Replace(_new, @"=", "");
        _new = Regex.Replace(_new, @" ", "");

        //Console.WriteLine(_new);
 
        double dbReturn = double.Parse(_new, System.Globalization.CultureInfo.InvariantCulture);

        return dbReturn;
    }

    public static ulong readULong(int LineToRead)
    {
        string temp = "";

        temp = allLines[LineToRead];

        string _new = Regex.Replace(temp, @"([a-z])", "");
        _new = Regex.Replace(_new, @"=", "");
        _new = Regex.Replace(_new, @" ", "");

        //Console.WriteLine(_new);

        ulong dbReturn = ulong.Parse(_new, System.Globalization.CultureInfo.InvariantCulture);

        return dbReturn;
    }

    public static void GetOffsets()
    {
        for (int i = 0; i < 101; i++)
        {
            allLines[i] = "";
        }

        Init();

        for (int i = 0; i < 101; i++)
        {
            switch (allLines[i])
            {
                case "pLocalPlayerBase":
                    Offsets.pLocalPlayerBase = readULong(i);
                    break;
                case "pPlayerBase":
                    Offsets.pPlayerBase = readULong(i);
                    break;
                case "pBoneBase":
                    Offsets.pBoneBase = readULong(i);
                    break;
                case "m_iHealth":
                    Offsets.m_iHealth = readULong(i);
                    break;
                case "m_iTeamNum":
                    Offsets.m_iTeamNum = readULong(i);
                    break;
                case "m_blifestate":
                    Offsets.m_blifestate = readULong(i);
                    break;
                case "EnginePointer":
                    Offsets.EnginePointer = readULong(i);
                    break;
                case "pEngineVAngs":
                    Offsets.pEngineVAngs = readULong(i);
                    break;

                // Add more cases for other offsets

            }
        }
    }
}