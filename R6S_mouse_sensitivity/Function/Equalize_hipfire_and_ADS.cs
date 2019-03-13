using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace R6S_mouse_sensitivity
{
    public partial class Equalize_hipfire_and_ADS : Form
    {
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

        public bool load_gamesetting=false;
        public string iniFileName;
        public double g_sens_x;
        public double g_sens_y;
        public decimal ads;
        public double MSMU;
        public decimal XFA;

        public double g_sens_x_new;
        public double g_sens_y_new;
        public double ads_new;
        public double MSMU_new;
        public double XFA_new;

        public Equalize_hipfire_and_ADS()
        {
            InitializeComponent();
        }

        private void Equalize_hipfire_and_ADS_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.option_Default_file) { get_gamesettings(0); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear_data();
            get_gamesettings(1);
        }

        private void get_gamesettings(int x)
        {
            if (x == 0)//規定の読み込みファイルを読み込むとき
            {
                iniFileName = Properties.Settings.Default.gamesettings_file;
            }
            else if (x == 1)//手動で読み込むとき
            {
                var directory = Properties.Settings.Default.gamesettings_folder;
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
                }
            }
            else if (x==2)//リロード
            {
            }

            if (iniFileName != "")
            {
                load_gamesetting = true;
                button2.Enabled = true;
                button3.Enabled = true;

                player_ID(iniFileName);

                StringBuilder MouseYawSensitivity = new StringBuilder(1024);
                GetPrivateProfileString(
                    "INPUT",
                    "MouseYawSensitivity",
                    "ありません",
                    MouseYawSensitivity,
                    Convert.ToUInt32(MouseYawSensitivity.Capacity),
                    iniFileName);

                StringBuilder MousePitchSensitivity = new StringBuilder(1024);
                GetPrivateProfileString(
                    "INPUT",
                    "MousePitchSensitivity",
                    "ありません",
                    MousePitchSensitivity,
                    Convert.ToUInt32(MousePitchSensitivity.Capacity),
                    iniFileName);

                StringBuilder AimDownSightsMouse = new StringBuilder(1024);
                GetPrivateProfileString(
                    "INPUT",
                    "AimDownSightsMouse",
                    "ありません",
                    AimDownSightsMouse,
                    Convert.ToUInt32(AimDownSightsMouse.Capacity),
                    iniFileName);

                StringBuilder MouseSensitivityMultiplierUnit = new StringBuilder(1024);
                GetPrivateProfileString(
                    "INPUT",
                    "MouseSensitivityMultiplierUnit",
                    "ありません",
                    MouseSensitivityMultiplierUnit,
                    Convert.ToUInt32(MouseSensitivityMultiplierUnit.Capacity),
                    iniFileName);

                StringBuilder XFactorAiming = new StringBuilder(1024);
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
            }
        }

        private void player_ID(string x)
        {
            string[] filename;
            filename = x.Split('\\');
            Array.Reverse(filename);
            label41.Text = "読み込み中のファイルのプレイヤーID ： " + filename[1];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            //label38.Text = "47";
            //label6.Text = "0.035461";

            decimal x;
            decimal ads_x=0;
            decimal X=10000;
            decimal XFA_x = 10000;

            
            for (decimal ads = 1; ads <= 100; ads++)
            {
                XFA = 1 / ((decimal)0.6 * ads);               
                XFA= Math.Round(XFA, 6, MidpointRounding.AwayFromZero);
                x = ads * XFA*(decimal)0.6;
                
                if (Math.Abs(1-x)<=Math.Abs(1-X))
                {   
                    X = x;
                    ads_x = ads;
                    XFA_x = XFA;
                }
            }
            label36.Text = textBox1.Text;
            label37.Text = textBox2.Text;
            label39.Text = textBox4.Text;
            label38.Text = ads_x.ToString();
            label6.Text = XFA_x.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            //label38.Text = "3";
            //label6.Text = "0.952381";

            decimal x;
            decimal ads_x = 0;
            decimal X = 10000;
            decimal XFA_x = 10000;

            for (decimal ads = 1; ads <= 100; ads++)
            {
                XFA = 1 / ((decimal)0.35 * ads);
                XFA = Math.Round(XFA, 6, MidpointRounding.AwayFromZero);
                x = ads * XFA * (decimal)0.35;

                if (Math.Abs(1 - x) <= Math.Abs(1 - X))
                {
                    X = x;
                    ads_x = ads;
                    XFA_x = XFA;
                }
            }
            label36.Text = textBox1.Text;
            label37.Text = textBox2.Text;
            label39.Text = textBox4.Text;
            label38.Text = ads_x.ToString();
            label6.Text = XFA_x.ToString();
        }

        private void clear_data()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label36.Text = "";
            label37.Text = "";
            label38.Text = "";
            label39.Text = "";
            label6.Text = "";

            iniFileName = "";
            label41.Text = "読み込み中のファイルのプレイヤーID ： ";

            button2.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear_data();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (load_gamesetting == true)
            {
                string[] filename;
                filename = iniFileName.Split('\\');
                Array.Reverse(filename);

                DialogResult result1 = MessageBox.Show("このデータを " + filename[1] + " の" + "\r\n" + "GameSettings.ini に上書きしますか？", "上書きの確認", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    if (Properties.Settings.Default.option_Back_Up == true)
                    {
                        back_up();
                        write_gamesettings();
                        re_lead_gamesettings();//同じファイル名をもう一度読み込んで、テキストボックス等を更新
                    }
                    else
                    {
                        DialogResult result2 = MessageBox.Show("バックアップの設定がされていません。本当によろしいですか？", "上書きの確認", MessageBoxButtons.OKCancel);
                        if (result2 == DialogResult.OK)
                        {
                            write_gamesettings();
                            re_lead_gamesettings();
                        }
                    }
                    MessageBox.Show("上書きしました。");
                }
            }
            else if (load_gamesetting == false)
            {
                MessageBox.Show("Gamesettings.iniが読み込まれていないため適応できません。\n手動で書き換えを行ってください。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void back_up()
        {
            //バックアップしたいファイル名を取得
            var filename = System.IO.Path.GetFileName(iniFileName);
            //日付を取得
            var d = DateTime.Now.ToString("yyyyMMdd_HHmm-");
            d = d + "ADS_" +label38.Text + "-XFactor_" + label6.Text + "-";
            //日付を先頭にしてバックアップファイルのパスを作る
            var buckupfilepath = string.Format(Properties.Settings.Default.gamesettings_bkup + @"\{0}{1}", d, filename);
            //ファイルをコピーしてバックアップ
            System.IO.File.Copy(iniFileName, buckupfilepath);
        }

        private void write_gamesettings()
        {
            WritePrivateProfileString(
                  "INPUT",
                  "AimDownSightsMouse",
                   label38.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "XFactorAiming",
                   label6.Text,
                   iniFileName);
        }

        private void re_lead_gamesettings()
        {
            get_gamesettings(2);
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