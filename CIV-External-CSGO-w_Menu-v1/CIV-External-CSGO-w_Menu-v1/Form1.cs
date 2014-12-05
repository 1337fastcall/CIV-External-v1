using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIV_External_CSGO_w_Menu_v1
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();

            string Timestamp = DateTime.Now.ToString("dd-MM-yyyy");

            string key = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION\\";
            string valueName = Application.ProductName + ".exe";

           
            Microsoft.Win32.Registry.SetValue(key, valueName, (uint)(0x0), Microsoft.Win32.RegistryValueKind.DWord);

            MenuBrowser.Navigate("file:///H:/GitHub/CIV-External-v1/CSGOHACK/Csgohack.html");
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {

        }
    }
}
