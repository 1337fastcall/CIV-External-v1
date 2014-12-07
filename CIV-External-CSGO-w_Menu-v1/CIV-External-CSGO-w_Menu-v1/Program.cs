using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

class CAimbotThread
{
    public void StartAimbotThread()
    {
        CMemory.Init();

        while (true)
        {
            CAimbot.doAim();
            
        }
    }
}

namespace CIV_External_CSGO_w_Menu_v1
{
    static class Program
    {

        static bool bThreadsAreCreated = false;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!bThreadsAreCreated)
            {
                // Create Threads for Cheat here
                CAimbotThread aimbot_thread = new CAimbotThread();
                Thread aim_thread = new Thread(aimbot_thread.StartAimbotThread);
                aim_thread.Start();
                bThreadsAreCreated = true;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuForm());
        }
    }
}
