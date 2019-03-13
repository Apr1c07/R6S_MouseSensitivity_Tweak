using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace R6S_mouse_sensitivity
{
    public partial class Without_change_ADS : Form
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

        public string iniFileName;
        public double g_sens_x;
        public double g_sens_y;
        public double ads;
        public double MSMU;
        public double XFA;
        public double ads_sens;
        public double ads_new;
        public double XFA_new;
        public double XFA_temp;
        public double g_sens_x_new;
        public double g_sens_y_new;
        public bool Finish_load = false;
        public bool load_gamesetting = false;

        public Without_change_ADS()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button3.Enabled = false;
            comboBox1.Enabled = false;
            if (Properties.Settings.Default.option_Detailed_Value == false)
            {
                groupBox3.Visible = false; this.Height = 346;
            }
            else
            {
                groupBox3.Visible = true; this.Height = 589;
            }
            Finish_load = true;
        }

        private void Without_change_ADS_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.option_Default_file) { Load_Gametettings(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_gamesettings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            if (Check_Appropriate_Value())//値が正常化のチェック
            {
                Calculation();
                if (comboBox1.Items.Count != 0)
                {
                    comboBox1.SelectedIndex = 0;
                    button3.Enabled = true;
                    comboBox1.Enabled = true;
                }
                else
                {
                    button3.Enabled = false;
                    comboBox1.Enabled = false;
                    label40.Text = "変更可能な選択肢がありません。";
                }
            }
            else
            {
                MessageBox.Show("入力された数値が異常です。");
            }
        }

        private void get_gamesettings() //GameSettings読み込み
        {
            //var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Rainbow Six - Siege";
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
                load_gamesetting = true;
                button2.Enabled = true;
                iniFileName = openFileDialog1.FileName;

                StringBuilder MouseYawSensitivity = new StringBuilder(1024);　//x軸感度
                GetPrivateProfileString(
                    "INPUT",      // セクション名
                    "MouseYawSensitivity",          // キー名
                    "ありません",   // 値が取得できなかった場合に返される初期値
                    MouseYawSensitivity,             // 格納先
                    Convert.ToUInt32(MouseYawSensitivity.Capacity), // 格納先のキャパ
                    iniFileName);   // iniファイル名

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
                label35.Text = MouseSensitivityMultiplierUnit.ToString();
                textBox4.Text = XFactorAiming.ToString();
                player_ID(iniFileName);
            }
        }

        private void Load_gamesettings()
        {
            load_gamesetting = true;
            iniFileName = Properties.Settings.Default.gamesettings_file;

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
            label35.Text = MouseSensitivityMultiplierUnit.ToString();
            textBox4.Text = XFactorAiming.ToString();
        }

        private void Calculation()
        {
            g_sens_x = double.Parse(textBox1.Text);
            g_sens_y = double.Parse(textBox2.Text);
            ads = double.Parse(textBox3.Text);
            XFA = double.Parse(textBox4.Text);
            MSMU = double.Parse(label35.Text);
            ads_sens = g_sens_x * MSMU * XFA;
            double num = 1000.0;
            bool flag = false;
            bool flag2 = false;

            for (int i = 1; i <= 100; i++)
            {
                g_sens_x_new = (double)i;
                g_sens_y_new = g_sens_y * ((double)i / g_sens_x);
                num = 1000.0;
                flag = false;//とりあえず１つ見つけたか
                flag2 = false;//計算終了

                if (!Validation.IsDecimal(g_sens_y_new))//もとのXFAの値にできるだけ近い値で算出する。
                {
                    for (int j = 1; j <= 100; j++)
                    {
                        if (!flag2)
                        {
                            XFA_temp = g_sens_x * ads * XFA / (g_sens_x_new * (double)j);
                            if (XFA_temp < 10.0 && GetPrecision1((decimal)XFA_temp) <= 6)
                            {
                                if (Math.Abs(XFA - XFA_temp) < num)
                                {
                                    num = Math.Abs(XFA - XFA_temp);
                                    XFA_new = XFA_temp;
                                    ads_new = (double)j;
                                    flag = true;
                                }
                                else if (flag)
                                {
                                    if (g_sens_x_new!= g_sens_x)
                                    {
                                        comboBox1.Items.Add(g_sens_x_new);
                                        flag2 = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            g_sens_x_new = double.Parse(comboBox1.Text);
            g_sens_y_new = g_sens_y * (double.Parse(comboBox1.Text) / g_sens_x);
            double num = 1000.0;
            bool flag = false;
            bool flag2 = false;
            for (int i = 1; i <= 100; i++)
            {
                if (!flag2)
                {
                    XFA_temp = g_sens_x * ads * XFA / (g_sens_x_new * (double)i);
                    if (XFA_temp < 10.0 && GetPrecision1((decimal)XFA_temp) <= 6)
                    {
                        if (Math.Abs(XFA - XFA_temp) < num)
                        {
                            num = Math.Abs(XFA - XFA_temp);
                            XFA_new = XFA_temp;
                            ads_new = (double)i;
                            flag = true;
                        }
                        else if (flag)
                        {
                            flag2 = true;
                        }
                    }
                }
            }
            label36.Text = g_sens_x_new.ToString();
            label37.Text = g_sens_y_new.ToString();
            label38.Text = ads_new.ToString();
            label39.Text = $"{XFA_new:f6}\r\n".ToString();
            label8.Text = (double.Parse(textBox9.Text) * g_sens_x * MSMU * ads * XFA).ToString();
            label9.Text = (double.Parse(textBox9.Text) * g_sens_x_new * MSMU * ads_new * XFA_new).ToString();
            label32.Text = (double.Parse(textBox9.Text) * g_sens_y * MSMU * ads * XFA).ToString();
            label33.Text = (double.Parse(textBox9.Text) * g_sens_y_new * MSMU * ads_new * XFA_new).ToString();
            label21.Text = (double.Parse(textBox9.Text) * g_sens_x * MSMU).ToString();
            label23.Text = (double.Parse(textBox9.Text) * g_sens_x_new * MSMU).ToString();
            label22.Text = (double.Parse(textBox9.Text) * g_sens_y * MSMU).ToString();
            label24.Text = (double.Parse(textBox9.Text) * g_sens_y_new * MSMU).ToString();
            label10.Text = (double.Parse(label8.Text) - double.Parse(label9.Text)).ToString();
            label34.Text = (double.Parse(label32.Text) - double.Parse(label33.Text)).ToString();
            label25.Text = (double.Parse(label23.Text) - double.Parse(label21.Text)).ToString();
            label26.Text = (double.Parse(label24.Text) - double.Parse(label22.Text)).ToString();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            label8.Text = (double.Parse(textBox9.Text) * g_sens_x * MSMU * ads * XFA).ToString();
            label9.Text = (double.Parse(textBox9.Text) * g_sens_x_new * MSMU * ads_new * XFA_new).ToString();
            label32.Text = (double.Parse(textBox9.Text) * g_sens_y * MSMU * ads * XFA).ToString();
            label33.Text = (double.Parse(textBox9.Text) * g_sens_y_new * MSMU * ads_new * XFA_new).ToString();
            label21.Text = (double.Parse(textBox9.Text) * g_sens_x * MSMU).ToString();
            label23.Text = (double.Parse(textBox9.Text) * g_sens_x_new * MSMU).ToString();
            label22.Text = (double.Parse(textBox9.Text) * g_sens_y * MSMU).ToString();
            label24.Text = (double.Parse(textBox9.Text) * g_sens_y_new * MSMU).ToString();

            label10.Text = (double.Parse(label8.Text) - double.Parse(label9.Text)).ToString();
            label34.Text = (double.Parse(label32.Text) - double.Parse(label33.Text)).ToString();
            label25.Text = (double.Parse(label23.Text) - double.Parse(label21.Text)).ToString();
            label26.Text = (double.Parse(label24.Text) - double.Parse(label22.Text)).ToString();
        }

        private void label35_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (load_gamesetting == true && comboBox1.Text != "")
            {
                DialogResult result1 = MessageBox.Show("このデータを適応しますか？", "上書きの確認", MessageBoxButtons.YesNo);
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
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("変更したい設定を選択してから、適応を押して下さい。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            d = d + "Sens_X_" + label36.Text + "-Sens_Y_" + label37.Text + "-ADS_" + label38.Text + "-XFactor_" + label39.Text + "-";
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
                   label36.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "MousePitchSensitivity",
                    label37.Text,
                   iniFileName);

            WritePrivateProfileString(
                  "INPUT",
                  "AimDownSightsMouse",
                   label38.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "XFactorAiming",
                   label39.Text,
                   iniFileName);
        }

        public sealed class Validation
        {
            #region　IsDecimal メソッド

            /// ------------------------------------------------------------------------------
            /// <summary>
            ///     指定した値が小数を含むかどうかを返します。</summary>
            /// <param name="dValue">
            ///     検査対象となる値。</param>
            /// <returns>
            ///     小数を含む場合は true。それ以外は false。</returns>
            /// ------------------------------------------------------------------------------
            public static bool IsDecimal(double dValue)
            {
                if (dValue - System.Math.Floor(dValue) != 0)
                {
                    return true;
                }

                return false;
            }

            #endregion
        }

        private void 終了ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void tipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tips form7 = new Tips
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form7.Show();
        }

        private int GetPrecision1(decimal price)
        {
            string priceString = price.ToString().TrimEnd('0');

            int index = priceString.IndexOf('.');
            if (index == -1)
                return 0;

            return priceString.Substring(index + 1).Length;
        }

        private void re_lead_gamesettings()
        {
            StringBuilder MouseYawSensitivity = new StringBuilder(1024); //x軸感度
            GetPrivateProfileString(
                "INPUT",      // セクション名
                "MouseYawSensitivity",          // キー名
                "ありません",   // 値が取得できなかった場合に返される初期値
                MouseYawSensitivity,             // 格納先
                Convert.ToUInt32(MouseYawSensitivity.Capacity), // 格納先のキャパ
                iniFileName);   // iniファイル名

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
            label35.Text = MouseSensitivityMultiplierUnit.ToString();
            textBox4.Text = XFactorAiming.ToString();
        }

        private bool Check_Appropriate_Value()
        {
            double try_out = 0;
            bool result = true;

            if (double.TryParse(textBox1.Text, out try_out))
            {
                if (textBox1.Text != "" && 0 < double.Parse(textBox1.Text) && double.Parse(textBox1.Text) <= 100)
                {
                    textBox1.BackColor = SystemColors.Window;
                }
                else
                {
                    textBox1.BackColor = Color.Yellow;
                    result = false;
                }
            }
            else
            {
                textBox1.BackColor = Color.Yellow;
                result = false;
            }

            if (double.TryParse(textBox2.Text, out try_out))
            {
                if (textBox2.Text != "" && 0 < double.Parse(textBox2.Text) && double.Parse(textBox2.Text) <= 100)
                {
                    textBox2.BackColor = SystemColors.Window;
                }
                else
                {
                    textBox2.BackColor = Color.Yellow;
                    result = false;
                }
            }
            else
            {
                textBox2.BackColor = Color.Yellow;
                result = false;
            }

            if (double.TryParse(textBox3.Text, out try_out))
            {
                if (textBox3.Text != "" && 0 < double.Parse(textBox3.Text) && double.Parse(textBox3.Text) <= 100)
                {
                    textBox3.BackColor = SystemColors.Window;
                }
                else
                {
                    textBox3.BackColor = Color.Yellow;
                    result = false;
                }
            }
            else
            {
                textBox3.BackColor = Color.Yellow;
                result = false;
            }

            if (double.TryParse(textBox4.Text, out try_out))
            {
                if (textBox4.Text != "" && 0 < double.Parse(textBox4.Text) && GetPrecision1((decimal)double.Parse(textBox4.Text)) <= 6 && double.Parse(textBox4.Text) < 10)
                {
                    textBox4.BackColor = SystemColors.Window;
                }
                else
                {
                    textBox4.BackColor = Color.Yellow;
                    result = false;
                }
            }
            else
            {
                textBox4.BackColor = Color.Yellow;
                result = false;
            }

            return result;
        }//適切な値かどうかのチェック

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            label36.Text = "";
            label37.Text = "";
            label38.Text = "";
            label39.Text = "";

            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label32.Text = "";
            label33.Text = "";
            label34.Text = "";
            label21.Text = "";
            label23.Text = "";
            label25.Text = "";
            label22.Text = "";
            label24.Text = "";
            label26.Text = "";

            label35.Text = "";

            iniFileName = "";
            label41.Text = "読み込み中のファイルのプレイヤーID ： ";

            if (textBox1.Text == "") { textBox1.BackColor = SystemColors.Window; }
            if (textBox3.Text == "") { textBox3.BackColor = SystemColors.Window; }
            if (textBox2.Text == "") { textBox2.BackColor = SystemColors.Window; }
            if (textBox4.Text == "") { textBox4.BackColor = SystemColors.Window; }

            comboBox1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void player_ID(string x)
        {
            string[] filename;
            filename = x.Split('\\');
            Array.Reverse(filename);
            label41.Text = "読み込み中のファイルのプレイヤーID ： " + filename[1];
        }

        private void Load_Gametettings()
        {
            load_gamesetting = true;
            button2.Enabled = true;
            iniFileName = Properties.Settings.Default.gamesettings_file;

            StringBuilder MouseYawSensitivity = new StringBuilder(1024); //x軸感度
            GetPrivateProfileString(
                "INPUT",      // セクション名
                "MouseYawSensitivity",          // キー名
                "ありません",   // 値が取得できなかった場合に返される初期値
                MouseYawSensitivity,             // 格納先
                Convert.ToUInt32(MouseYawSensitivity.Capacity), // 格納先のキャパ
                iniFileName);   // iniファイル名

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
            textBox4.Text = XFactorAiming.ToString();
            label35.Text = MouseSensitivityMultiplierUnit.ToString();
            player_ID(iniFileName);
        }
    }
}