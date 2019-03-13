using System;
using System.IO;
using System.Windows.Forms;

namespace R6S_mouse_sensitivity
{
    public partial class SettingMenu : Form
    {
        public string load_dr;
        public string load_dr_bk;

        public SettingMenu()
        {
            InitializeComponent();
            Load_Data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Rainbow Six - Siege";
            string[] directoryCount = Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly);

            openFileDialog1.FileName = "GameSettings";
            openFileDialog1.Filter = "INI ファイル (.ini)|*.ini|all|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Rainbow Six - Siege";
            string[] directoryCount = Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly);

            if (directoryCount.Length == 1)
            {
                folderBrowserDialog1.SelectedPath = directoryCount[0];
            }
            else
            {
                folderBrowserDialog1.SelectedPath = directory;
            }

            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                load_dr_bk = folderBrowserDialog1.SelectedPath;
                textBox3.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Save_Data();
            Application.Restart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked == true)
            {
                label2.Enabled = true;
                textBox2.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                label2.Enabled = false;
                textBox2.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3.Checked = checkBox3.Checked;

            if (checkBox3.Checked == true)
            {
                label3.Enabled = true;
                textBox3.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                label3.Enabled = false;
                textBox3.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void Load_Data()
        {
            if (Properties.Settings.Default.option_Default_file == false)
            {
                label2.Enabled = false;
                textBox2.Enabled = false;
                button2.Enabled = false;
            }
            if (Properties.Settings.Default.option_Back_Up == false)
            {
                label3.Enabled = false;
                textBox3.Enabled = false;
                button3.Enabled = false;
            }

            textBox2.Text = Properties.Settings.Default.gamesettings_file;
            textBox3.Text = Properties.Settings.Default.gamesettings_bkup;
            checkBox1.Checked = Properties.Settings.Default.option_Save_Data;
            checkBox2.Checked = Properties.Settings.Default.option_Default_file;
            checkBox3.Checked = Properties.Settings.Default.option_Back_Up;
            checkBox4.Checked = Properties.Settings.Default.option_Detailed_Value;
            load_dr = Properties.Settings.Default.gamesettings_folder;
            load_dr_bk = Properties.Settings.Default.gamesettings_bkup;
        }

        private void Save_Data()
        {
            Properties.Settings.Default.option_Save_Data = checkBox1.Checked;
            Properties.Settings.Default.option_Default_file = checkBox2.Checked;
            Properties.Settings.Default.option_Back_Up = checkBox3.Checked;
            Properties.Settings.Default.option_Detailed_Value = checkBox4.Checked;
            Properties.Settings.Default.gamesettings_folder = load_dr;
            Properties.Settings.Default.gamesettings_bkup = load_dr_bk;
            Properties.Settings.Default.gamesettings_file = textBox2.Text;
            Properties.Settings.Default.Save();
        }


    }
}