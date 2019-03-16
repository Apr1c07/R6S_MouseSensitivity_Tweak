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
    public partial class Change_dpi_per_ingame : Form
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

        public bool load_gamesetting;
        public string iniFileName;
        public bool Finish_load = false;

        public double DPI;
        public double G_X;
        public double G_Y;
        public double MSMU;
        public double dpi_g;

        public double DPI_new;
        public double G_X_new;
        public double G_Y_new;
        public double MSMU_new;
        public double dpi_g_new;


        public Change_dpi_per_ingame()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            if (Properties.Settings.Default.option_Save_Data)
            {
                Load_Data();
                if (Check_Appropriate_Value())
                {
                    label10.Text = (double.Parse(textBox1.Text) * double.Parse(textBox4.Text)).ToString();
                }
            }
            Finish_load = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_gamesettings();
            if (Check_Appropriate_Value())
            {
                label10.Text = (double.Parse(textBox1.Text) * double.Parse(textBox4.Text)).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label10.Text = "";

            if (textBox1.Text == "") { textBox1.BackColor = SystemColors.Window; }
            if (textBox3.Text == "") { textBox3.BackColor = SystemColors.Window; }
            if (textBox2.Text == "") { textBox2.BackColor = SystemColors.Window; }
            if (textBox4.Text == "") { textBox4.BackColor = SystemColors.Window; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            if (Check_Appropriate_Value())//値が正常化のチェック
            {
                Calculation();
                if (comboBox1.Items.Count != 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
                label10.Text = (double.Parse(textBox1.Text) * double.Parse(textBox4.Text)).ToString();
            }
            else
            {
                MessageBox.Show("入力された数値が異常です。");
            }
        }

        private void get_gamesettings() //GameSettings読み込み
        {
            //var directory = Properties.Settings.Default.gamesettings_folder;
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
                load_gamesetting = true;
                button4.Enabled = true;
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

                StringBuilder MouseSensitivityMultiplierUnit = new StringBuilder(1024);
                GetPrivateProfileString(
                    "INPUT",
                    "MouseSensitivityMultiplierUnit",
                    "ありません",
                    MouseSensitivityMultiplierUnit,
                    Convert.ToUInt32(MouseSensitivityMultiplierUnit.Capacity),
                    iniFileName);

                textBox2.Text = MouseYawSensitivity.ToString();
                textBox3.Text = MousePitchSensitivity.ToString();
                textBox4.Text = MouseSensitivityMultiplierUnit.ToString();
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void Calculation()
        {
            DPI = double.Parse(textBox1.Text);
            G_X = double.Parse(textBox2.Text);
            G_Y = double.Parse(textBox3.Text);
            MSMU = double.Parse(textBox4.Text);
            dpi_g = DPI * MSMU;

            for (int i = 100; i >= 1; i--)
            {
                MSMU_new = MSMU * (G_X / i);
                G_Y_new = i * (G_Y / G_X);
                if (0 < MSMU_new && GetPrecision1((decimal)MSMU_new) <= 6 && MSMU_new < 10 && 0 < G_Y_new && G_Y_new <= 100 && GetPrecision1((decimal)G_Y_new) == 0)
                {
                    comboBox1.Items.Add(i / G_X);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Save_Data();
        }

        private bool Check_Appropriate_Value()
        {
            double try_out = 0;
            bool result = true;

            if (double.TryParse(textBox1.Text, out try_out))
            {
                if (textBox1.Text != "" && 0 < double.Parse(textBox1.Text))
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

        private int GetPrecision1(decimal price)
        {
            string priceString = price.ToString().TrimEnd('0');

            int index = priceString.IndexOf('.');
            if (index == -1)
                return 0;

            return priceString.Substring(index + 1).Length;
        }

        private void Save_Data()
        {
            if (Finish_load && Check_Appropriate_Value())
            {
                if (Properties.Settings.Default.option_Save_Data)
                {
                    //System.Diagnostics.StackFrame caller = new System.Diagnostics.StackFrame(1);
                    ////メソッド名
                    //string methodName = caller.GetMethod().Name;
                    //MessageBox.Show("call Save() : " + methodName);

                    Properties.Settings.Default.Change_dpi_per_ingame_DPI = double.Parse(textBox1.Text);
                    Properties.Settings.Default.Change_dpi_per_ingame_G_X = double.Parse(textBox2.Text);
                    Properties.Settings.Default.Change_dpi_per_ingame_G_Y = double.Parse(textBox3.Text);
                    Properties.Settings.Default.Change_dpi_per_ingame_MSMU = double.Parse(textBox4.Text);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void Load_Data()
        {
            textBox1.Text = Properties.Settings.Default.Change_dpi_per_ingame_DPI.ToString();
            textBox2.Text = Properties.Settings.Default.Change_dpi_per_ingame_G_X.ToString();
            textBox3.Text = Properties.Settings.Default.Change_dpi_per_ingame_G_Y.ToString();
            textBox4.Text = string.Format("{0:f6}\r\n", Properties.Settings.Default.Change_dpi_per_ingame_MSMU);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox5.Text = DPI.ToString();
            textBox6.Text = (G_X * double.Parse(comboBox1.Text)).ToString();
            textBox7.Text = (double.Parse(textBox6.Text) * (G_Y / G_X)).ToString();
            textBox8.Text = (MSMU / (double.Parse(comboBox1.Text))).ToString();
            label12.Text = (DPI * double.Parse(textBox8.Text)).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
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
                        re_lead_gamesettings();
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
                   textBox6.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "MousePitchSensitivity",
                    textBox7.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "MouseSensitivityMultiplierUnit",
                    textBox8.Text,
                   iniFileName);
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

            StringBuilder MouseSensitivityMultiplierUnit = new StringBuilder(1024);
            GetPrivateProfileString(
                "INPUT",
                "MouseSensitivityMultiplierUnit",
                "ありません",
                MouseSensitivityMultiplierUnit,
                Convert.ToUInt32(MouseSensitivityMultiplierUnit.Capacity),
                iniFileName);


            textBox2.Text = MouseYawSensitivity.ToString();
            textBox3.Text = MousePitchSensitivity.ToString();
            textBox4.Text = MouseSensitivityMultiplierUnit.ToString();
            label10.Text = (double.Parse(textBox1.Text) * double.Parse(textBox4.Text)).ToString();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form6 = new About { StartPosition = FormStartPosition.CenterScreen };
            form6.Show();
        }
    }
}
