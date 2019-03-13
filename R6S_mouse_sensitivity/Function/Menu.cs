using System;
using System.IO;
using System.Windows.Forms;

//数値型から数値型　　：　(double)x, (int)x
//数値型から文字列型　：　x.ToStoring()
//文字列型空数値型　　：　double.Parse(x)
namespace R6S_mouse_sensitivity
{
    public partial class Menu : Form
    {
        private string iniFileName = Properties.Settings.Default.gamesettings_folder;

        public Menu()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //if (!Properties.Settings.Default.initialize)
            //{
            //    var path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Rainbow Six - Siege";
            //    if (Directory.Exists(path))
            //    {
            //        var path_backup = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Rainbow Six - Siege\\Backup";
            //        if (!Directory.Exists(path_backup))
            //        {
            //            Directory.CreateDirectory(path_backup);
            //            Properties.Settings.Default.initialize = true;
            //            Properties.Settings.Default.Save();
            //        }
            //        Properties.Settings.Default.gamesettings_bkup = path_backup;
            //        Properties.Settings.Default.gamesettings_folder = path;
            //        Properties.Settings.Default.Save();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Gamesetting.iniファイルの場所を見つけられませんでした。\nメニューの設定から手動で選択してください。",
            //        "エラー",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);
            //    }
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Change_mouse_sensitivity form2 = new Change_mouse_sensitivity { StartPosition = FormStartPosition.CenterScreen };
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Without_change_ADS form4 = new Without_change_ADS { StartPosition = FormStartPosition.CenterScreen };
            form4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Optimize_MouseSensitivityMultiplierUnit_and_XFactorAiming_Select form5 = new Optimize_MouseSensitivityMultiplierUnit_and_XFactorAiming_Select { StartPosition = FormStartPosition.CenterScreen };
            form5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Equalize_hipfire_and_ADS form8 = new Equalize_hipfire_and_ADS { StartPosition = FormStartPosition.CenterScreen };
            form8.Show();
        }


        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingMenu form3 = new SettingMenu();
            form3.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form6 = new About { StartPosition = FormStartPosition.CenterScreen };
            form6.Show();
        }

        private void 終了ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tips form7 = new Tips { StartPosition = FormStartPosition.CenterScreen };
            form7.Show();
        }
    }
}