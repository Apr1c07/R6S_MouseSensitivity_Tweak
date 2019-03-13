using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace R6S_mouse_sensitivity
{
    public partial class Optimize_MouseSensitivityMultiplierUnit_and_XFactorAiming_DPI : Form
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

        // [DllImport("KERNEL32.DLL")]
        // private static extern uint GetPrivateProfileInt(
        //string lpAppName,
        //string lpKeyName,
        //int nDefault,
        //string lpFileName);
        [DllImport("KERNEL32.DLL")]
        private static extern uint WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        public double g_sens_x;
        public double g_sens_y;
        public double ads;
        public double MSMU;
        public double XFA;

        public double g_sens_x_opt;
        public double g_sens_y_opt;
        public double ads_opt;
        public double MSMU_opt;
        public double XFA_opt;
        public bool load_gamesetting = false;
        public string iniFileName;

        public Optimize_MouseSensitivityMultiplierUnit_and_XFactorAiming_DPI()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Load_Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Load_settings();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Save_Data();
            Store_the_data();
            Initialize_variable();
            textBox6.Text = (double.Parse(textBox8.Text) * double.Parse(textBox4.Text)).ToString();
            textBox7.Text = (double.Parse(textBox9.Text) * double.Parse(textBox4.Text)).ToString();

            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                optimisation_MSMU();
                optimisation_XFA();
            }
            else
            {
                if (checkBox1.Checked == true && checkBox2.Checked == true)
                {
                    optimisation_MSMU();
                    optimisation_XFA();
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        optimisation_MSMU();
                    }

                    if (checkBox2.Checked == true)
                    {
                        optimisation_XFA();
                    }
                }
            }

            label11.Text = g_sens_x_opt.ToString();
            label12.Text = g_sens_y_opt.ToString();
            label13.Text = ads_opt.ToString();
            label14.Text = string.Format("{0:f6}\r\n", MSMU_opt);
            label15.Text = string.Format("{0:f6}\r\n", XFA_opt);
        }

        private void optimisation_MSMU()
        {
            MSMU_opt = (g_sens_x * MSMU) / 50; // MSMU最適化
            g_sens_x_opt = 50;
            g_sens_y_opt = Math.Round(g_sens_y * MSMU / MSMU_opt);
        }

        private void optimisation_XFA()
        {
            XFA_opt = (ads * XFA) / 50; // MSMU最適化
            ads_opt = 50;
        }

        private void Save_Data()
        {
            Properties.Settings.Default.G_X_5 = double.Parse(textBox1.Text);
            Properties.Settings.Default.G_Y_5 = double.Parse(textBox2.Text);
            Properties.Settings.Default.ADS_5 = double.Parse(textBox3.Text);
            Properties.Settings.Default.MSMU_5 = double.Parse(textBox4.Text);
            Properties.Settings.Default.XFA_5 = double.Parse(textBox5.Text);
            Properties.Settings.Default.Save();

            Properties.Settings.Default.DPI_X = double.Parse(textBox8.Text);
            if (checkBox3.Checked == true)
            {
                Properties.Settings.Default.DPI_Y = double.Parse(textBox9.Text);
            }
        }

        private void Load_Data()
        {
            textBox1.Text = Properties.Settings.Default.G_X_5.ToString();
            textBox2.Text = Properties.Settings.Default.G_Y_5.ToString();
            textBox3.Text = Properties.Settings.Default.ADS_5.ToString();
            textBox4.Text = string.Format("{0:f6}\r\n", Properties.Settings.Default.MSMU_5);
            textBox5.Text = string.Format("{0:f6}\r\n", Properties.Settings.Default.XFA_5);
        }

        private void Load_settings()
        {
            load_gamesetting = true;
            var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Rainbow Six - Siege";
            string[] directoryCount = Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly);

            if (directoryCount.Length == 1)
            {
                openFileDialog1.InitialDirectory = directoryCount[0];
            }
            else
            {
                openFileDialog1.InitialDirectory = directory;
            }

            openFileDialog1.FileName = "GameSettings";
            openFileDialog1.Filter = "INI ファイル (.ini)|*.ini|all|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                iniFileName = openFileDialog1.FileName;

                StringBuilder MouseYawSensitivity = new StringBuilder(1024); //x軸感度
                GetPrivateProfileString(
                    "INPUT",      // セクション名
                    "MouseYawSensitivity",          // キー名
                    "ありません",   // 値が取得できなかった場合に返される初期値
                    MouseYawSensitivity,             // 格納先
                    Convert.ToUInt32(MouseYawSensitivity.Capacity), // 格納先のキャパ
                    iniFileName);   // iniファイル名

                StringBuilder MousePitchSensitivity = new StringBuilder(1024); //y軸感度
                GetPrivateProfileString(
                    "INPUT",
                    "MousePitchSensitivity",
                    "ありません",
                    MousePitchSensitivity,
                    Convert.ToUInt32(MousePitchSensitivity.Capacity),
                    iniFileName);

                StringBuilder AimDownSightsMouse = new StringBuilder(1024); //y軸感度
                GetPrivateProfileString(
                    "INPUT",
                    "AimDownSightsMouse",
                    "ありません",
                    AimDownSightsMouse,
                    Convert.ToUInt32(AimDownSightsMouse.Capacity),
                    iniFileName);

                StringBuilder MouseSensitivityMultiplierUnit = new StringBuilder(1024); //y軸感度
                GetPrivateProfileString(
                    "INPUT",
                    "MouseSensitivityMultiplierUnit",
                    "ありません",
                    MouseSensitivityMultiplierUnit,
                    Convert.ToUInt32(MouseSensitivityMultiplierUnit.Capacity),
                    iniFileName);

                StringBuilder XFactorAiming = new StringBuilder(1024); //y軸感度
                GetPrivateProfileString(
                    "INPUT",
                    "XFactorAiming",
                    "ありません",
                    XFactorAiming,
                    Convert.ToUInt32(XFactorAiming.Capacity),
                    iniFileName);

                textBox1.Text = MouseYawSensitivity.ToString();
                textBox2.Text = MousePitchSensitivity.ToString();
                textBox3.Text = AimDownSightsMouse.ToString();
                textBox4.Text = MouseSensitivityMultiplierUnit.ToString();
                textBox5.Text = XFactorAiming.ToString();
                Save_Data();
            }
        }

        private void Store_the_data()
        {
            g_sens_x = double.Parse(textBox1.Text);
            g_sens_y = double.Parse(textBox2.Text);
            ads = double.Parse(textBox3.Text);
            MSMU = double.Parse(textBox4.Text);
            XFA = double.Parse(textBox5.Text);
        }

        private void Initialize_variable()
        {
            g_sens_x_opt = double.Parse(textBox1.Text);
            g_sens_y_opt = double.Parse(textBox2.Text);
            ads_opt = double.Parse(textBox3.Text);
            MSMU_opt = double.Parse(textBox4.Text);
            XFA_opt = double.Parse(textBox5.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (load_gamesetting == true)
            {
                DialogResult result1 = MessageBox.Show("このデータを適応しますか？", "上書きの確認", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    if (Properties.Settings.Default.option_Back_Up == true)
                    {
                        back_up();
                        write_gamesettings();
                    }
                    else
                    {
                        DialogResult result2 = MessageBox.Show("バックアップの設定がされていません。本当によろしいですか？", "上書きの確認", MessageBoxButtons.OKCancel);
                        if (result2 == DialogResult.OK)
                        {
                            write_gamesettings();
                        }
                    }
                }
            }
        }

        private void back_up()
        {
            //バックアップしたいファイル名を取得
            var filename = System.IO.Path.GetFileName(iniFileName);
            //日付を取得
            var d = DateTime.Now.ToString("yyyyMMdd_HHmm_");
            //日付を先頭にしてバックアップファイルのパスを作る
            var buckupfilepath = string.Format(Properties.Settings.Default.gamesettings_bkup + @"\{0}{1}", d, filename);
            //ファイルをコピーしてバックアップ
            System.IO.File.Copy(iniFileName, buckupfilepath);
        }

        private void write_gamesettings()
        {
            WritePrivateProfileString(
                   "INPUT",
                   "MouseYawSensitivity",
                   label11.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "MousePitchSensitivity",
                    label12.Text,
                    iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "AimDownSightsMouse",
                   label13.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "MouseSensitivityMultiplierUnit",
                    label14.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "XFactorAiming",
                   label15.Text,
                   iniFileName);
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingMenu form3 = new SettingMenu();
            form3.Show();
        }

        private void 終了ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form6 = new About { StartPosition = FormStartPosition.CenterScreen };
            form6.Show();
        }

        private void tipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tips form7 = new Tips { StartPosition = FormStartPosition.CenterScreen };
            form7.Show();
        }
    }
}