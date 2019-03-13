using System;
using System.Windows.Forms;

namespace R6S_mouse_sensitivity
{
    public partial class Optimize_MouseSensitivityMultiplierUnit_and_XFactorAiming_Select : Form
    {
        public Optimize_MouseSensitivityMultiplierUnit_and_XFactorAiming_Select()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                Optimize_MouseSensitivityMultiplierUnit_and_XFactorAiming form9 = new Optimize_MouseSensitivityMultiplierUnit_and_XFactorAiming { StartPosition = FormStartPosition.CenterScreen };
                form9.Show();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change_dpi_per_ingame form10 = new Change_dpi_per_ingame { StartPosition = FormStartPosition.CenterScreen };
            form10.Show();
            this.Close();
        }
    }
}