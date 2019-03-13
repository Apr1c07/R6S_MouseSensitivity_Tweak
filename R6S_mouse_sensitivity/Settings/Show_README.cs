using System;
using System.Windows.Forms;

namespace R6S_mouse_sensitivity
{
    public partial class Show_README : Form
    {
        public Show_README()
        {
            InitializeComponent();
            string url = "res://" +
             System.Reflection.Assembly.GetExecutingAssembly().Location + "/README.HTML";
            // this.webBrowser1.Navigate(@"C:\Users\annin\source\repos\R6S_mouse_sensitivity\R6S_mouse_sensitivity\README.html");
            object refmissing = System.Reflection.Missing.Value;
            this.webBrowser1.Navigate(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}