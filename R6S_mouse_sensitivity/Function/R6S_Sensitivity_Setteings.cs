using System;
using System.Windows.Forms;

namespace R6S_mouse_sensitivity
{
    public partial class R6S_Sensitivity_Setteings : Form
    {
        public R6S_Sensitivity_Setteings()
        {
            InitializeComponent();
            this.webBrowser1.Navigate(@"C:\Users\annin\source\repos\R6S_mouse_sensitivity\R6S_mouse_sensitivity\R6S_Sensitiviry_Setting.html");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}