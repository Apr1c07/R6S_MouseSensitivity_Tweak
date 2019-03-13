using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace R6S_mouse_sensitivity
{
    public partial class Tips : Form
    {
        // Win32APIの GetPrivateProfileString を使う宣言
        [DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            uint nSize,
            string lpFileName);

        [DllImport("KERNEL32.DLL")]
        private static extern uint WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        public Tips()
        {
            InitializeComponent();
            initializ();
        }

        private void initializ()
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            R6S_Sensitivity_Setteings form8 = new R6S_Sensitivity_Setteings { StartPosition = FormStartPosition.CenterScreen };
            form8.Show();
        }
    }
}