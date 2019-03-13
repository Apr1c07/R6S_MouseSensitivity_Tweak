using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace R6S_mouse_sensitivity
{
    public partial class Change_mouse_sensitivity : Form
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

        public double dpi_x;
        public double dpi_y;
        public double g_sens_x;
        public double g_sens_y;
        public double dpi_max;
        public int os_sens_num;
        public double os_sens;
        public double MSMU;
        public double os_sens_temp;

        public double dpi_x_new;
        public double dpi_y_new;
        public double g_sens_x_new;
        public double g_sens_y_new;
        public int os_sens_num_new;
        public double os_sens_new;
        public double MSMU_new;
        public string iniFileName;
        public bool load_gamesetting = false;
        public int check_load_form = 0;
        public bool Finish_load = false;

        public Change_mouse_sensitivity()
        {
            InitializeComponent();
            button2.Enabled = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            if (Properties.Settings.Default.option_Detailed_Value == false)
            {
                groupBox3.Visible = false; this.Height = 440;
            }
            else
            {
                groupBox3.Visible = true; this.Height = 553;
            }

            if (checkBox1.Checked == false)
            {
                label3.Enabled = false;
                textBox2.Enabled = false;
            }

            if (checkBox4.Checked == false)
            {
                textBox8.Enabled = false;
            }
            else
            {
                textBox8.Enabled = true;
            }
            comboBox1.Enabled = false;
            Finish_load = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.change_mouse_sensitivity_read_Y;
            checkBox2.Checked = Properties.Settings.Default.change_mouse_sensitivity_os_Accuracy;
            checkBox3.Checked = Properties.Settings.Default.change_mouse_sensitivity_MSMU_Change;
            checkBox4.Checked = Properties.Settings.Default.change_mouse_sensitivity_os_UnAccuracy;
            if (Properties.Settings.Default.option_Save_Data) { Load_Data(); }//Load Data
            if (Properties.Settings.Default.option_Default_file) { Load_Gametettings(); }
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Properties.Settings.Default.change_mouse_sensitivity_read_Y = true;
                label3.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                Properties.Settings.Default.change_mouse_sensitivity_read_Y = false;
                label3.Enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                Properties.Settings.Default.change_mouse_sensitivity_os_Accuracy = false;
                checkBox4.Checked = false;
                checkBox4.Enabled = true;
            }
            else
            {
                Properties.Settings.Default.change_mouse_sensitivity_os_Accuracy = true;
                checkBox4.Checked = false;
                checkBox4.Enabled = false;
            }
            Properties.Settings.Default.Save();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                Properties.Settings.Default.change_mouse_sensitivity_os_UnAccuracy = true;
                textBox8.Enabled = true;
            }
            else
            {
                Properties.Settings.Default.change_mouse_sensitivity_os_UnAccuracy = false;
                textBox8.Enabled = false;
            }
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
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
                load_gamesetting = true;
                button3.Enabled = true;
                iniFileName = openFileDialog1.FileName;

                StringBuilder MouseYawSensitivity = new StringBuilder(1024);　//x軸感度
                GetPrivateProfileString(
                    "INPUT",      // セクション名
                    "MouseYawSensitivity",          // キー名
                    "ありません",   // 値が取得できなかった場合に返される初期値
                    MouseYawSensitivity,             // 格納先
                    Convert.ToUInt32(MouseYawSensitivity.Capacity), // 格納先のキャパ
                    iniFileName);   // iniファイル名

                StringBuilder MousePitchSensitivity = new StringBuilder(1024);　//y軸感度
                GetPrivateProfileString(
                    "INPUT",
                    "MousePitchSensitivity",
                    "ありません",
                    MousePitchSensitivity,
                    Convert.ToUInt32(MousePitchSensitivity.Capacity),
                    iniFileName);

                StringBuilder MouseSensitivityMultiplierUnit = new StringBuilder(1024);　//y軸感度
                GetPrivateProfileString(
                    "INPUT",
                    "MouseSensitivityMultiplierUnit",
                    "ありません",
                    MouseSensitivityMultiplierUnit,
                    Convert.ToUInt32(MouseSensitivityMultiplierUnit.Capacity),
                    iniFileName);

                textBox3.Text = MouseYawSensitivity.ToString();
                textBox4.Text = MousePitchSensitivity.ToString();
                textBox5.Text = MouseSpeed.Get().ToString();
                textBox7.Text = MouseSensitivityMultiplierUnit.ToString();
                player_ID(iniFileName);
            }
            if (textBox1.Text == "") { textBox1.BackColor = Color.Yellow; }
            if (textBox3.Text == "") { textBox3.BackColor = Color.Yellow; }
            if (textBox4.Text == "") { textBox4.BackColor = Color.Yellow; }
            if (textBox5.Text == "") { textBox5.BackColor = Color.Yellow; }
            if (textBox6.Text == "") { textBox6.BackColor = Color.Yellow; }
            if (textBox7.Text == "") { textBox7.BackColor = Color.Yellow; }

            if (checkBox1.Checked && textBox2.Text == "") { textBox2.BackColor = Color.Yellow; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (load_gamesetting == true && comboBox1.Text != "")
            {
                string[] filename;
                filename = iniFileName.Split('\\');
                Array.Reverse(filename);

                DialogResult result1 = MessageBox.Show("このデータを " + filename[1]+ " の"+ "\r\n"+"GameSettings.ini に上書きしますか？", "上書きの確認", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    if (Properties.Settings.Default.option_Back_Up == true)
                    {
                        back_up();
                        write_gamesettings();
                        MouseSpeed.Set(int.Parse(label19.Text));
                    }
                    else
                    {
                        DialogResult result2 = MessageBox.Show("バックアップの設定がされていません。本当によろしいですか？", "上書きの確認", MessageBoxButtons.OKCancel);
                        if (result2 == DialogResult.OK)
                        {
                            write_gamesettings();
                            MouseSpeed.Set(int.Parse(label19.Text));
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

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            Clear_left_data_sheet();
            if (Check_Appropriate_Value())
            {
                dpi_x = double.Parse(textBox1.Text);
                if (checkBox1.Checked == true)
                {
                    dpi_y = double.Parse(textBox2.Text);
                }
                else
                {
                    dpi_y = double.Parse(textBox1.Text);
                }
                g_sens_x = double.Parse(textBox3.Text);
                g_sens_y = double.Parse(textBox4.Text);
                os_sens_num = int.Parse(textBox5.Text);
                dpi_max = double.Parse(textBox6.Text);
                MSMU = double.Parse(textBox7.Text);

                os_sens = num_to_sens(os_sens_num);//os_sens_numからos_sensを取得

                double dpi_x_temp;//dpi_x案
                double dpi_y_temp;//dpi_y案
                double g_sens_x_temp;//ゲーム内x軸感度案
                double g_sens_y_temp;//ゲーム内y軸感度案
                double MSMU_temp_x;//MSMU_x案
                double MSMU_temp_y;//MSMU_x案

                if (checkBox1.Checked == true)//y軸も考慮する
                {
                    if (checkBox2.Checked == true)//OSのマウス感度を保持
                    {
                        for (int i = 1; i < 12; i++)
                        {
                            os_sens_temp = num_to_sens(i);//iからos_sens_tempに変換
                            dpi_x_temp = dpi_x * (os_sens / os_sens_temp);
                            dpi_y_temp = dpi_y * (os_sens / os_sens_temp);

                            if (dpi_x_temp > 0 && dpi_x_temp <= dpi_max && //osのマウス感度を維持可能な一覧の中で0以上、maxdpi以下をフィルター
                                dpi_y_temp > 0 && dpi_y_temp <= dpi_max &&
                                Validation.IsDecimal(dpi_x_temp) == false &&
                                Validation.IsDecimal(dpi_y_temp) == false)//dpiが整数値かどうか
                            {
                                if (checkBox3.Checked == true)
                                {
                                    MSMU_temp_x = MSMU * (dpi_x / dpi_x_temp);
                                    MSMU_temp_y = MSMU * (dpi_y / dpi_y_temp);
                                    if (MSMU_temp_x == MSMU_temp_y &&
                                        GetPrecision1((decimal)MSMU_temp_x) <= 6 &&
                                        GetPrecision1((decimal)MSMU_temp_y) <= 6
                                        )
                                    {
                                        if (dpi_x != dpi_x_temp)
                                        {
                                            comboBox1.Items.Add(dpi_x_temp);
                                        }
                                    }
                                }
                                else
                                {
                                    g_sens_x_temp = g_sens_x * (dpi_x / dpi_x_temp);
                                    g_sens_y_temp = g_sens_y * (dpi_y / dpi_y_temp);

                                    if (g_sens_x_temp >= 0 && g_sens_x_temp <= 100 &&
                                        g_sens_y_temp >= 0 && g_sens_y_temp <= 100 &&
                                        Validation.IsDecimal(g_sens_x_temp) == false &&
                                        Validation.IsDecimal(g_sens_y_temp) == false
                                        )
                                    {
                                        if (dpi_x != dpi_x_temp)
                                        {
                                            comboBox1.Items.Add(dpi_x_temp);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else//OSのマウス感度を保持しない。
                    {
                        if (checkBox4.Checked == true)//一応OSのマウス感度が調整可能なレベルのフィルターを掛ける
                        {
                            double os_sens_choice;
                            var list = new[] { 0.025, 0.05, 0.25, 0.5, 0.75, 1, 1.5, 2, 2.5, 3, 3.5 };

                            for (int i = (int)dpi_max; i >= 5; i = i - 5)
                            {
                                os_sens_choice = (dpi_x * os_sens) / i;//選択したdpiでos感度を維持するとos_sensはいくらであればいいか
                                os_sens_new = list.Nearest(os_sens_choice);
                                dpi_x_temp = i;
                                dpi_y_temp = i * (dpi_y / dpi_x);
                                if (Validation.IsDecimal(dpi_y_temp) == false)
                                {
                                    if (textBox8.Text == "")
                                    {
                                        textBox8.Text = "0";
                                    }
                                    if (System.Math.Abs(dpi_x * os_sens - dpi_x_temp * os_sens_new) <= double.Parse(textBox8.Text) &&
                                        System.Math.Abs(dpi_y * os_sens - dpi_y_temp * os_sens_new) <= double.Parse(textBox8.Text)
                                        )//変更後のOSのマウス感度を誤差500dpi以内に収める
                                    {
                                        if (checkBox3.Checked == true)//かなり少ない選択肢しか出ない
                                        {
                                            MSMU_temp_x = MSMU * (dpi_x / dpi_x_temp);
                                            MSMU_temp_y = MSMU * (dpi_y / dpi_y_temp);
                                            if (GetPrecision1((decimal)MSMU_temp_x) <= 6 &&
                                                GetPrecision1((decimal)MSMU_temp_y) <= 6
                                                )
                                            {
                                                comboBox1.Items.Add(dpi_x_temp.ToString());
                                            }
                                        }
                                        else//フィルターにかけないのでバカみたいな値も出る。プロファイルスイッチが必要。
                                        {
                                            g_sens_x_temp = g_sens_x * (dpi_x / dpi_x_temp);
                                            g_sens_y_temp = g_sens_y * (dpi_y / dpi_y_temp);

                                            if (g_sens_x_temp >= 0 && g_sens_x_temp <= 100 &&
                                                g_sens_y_temp >= 0 && g_sens_y_temp <= 100 &&
                                                Validation.IsDecimal(g_sens_x_temp) == false &&
                                                Validation.IsDecimal(g_sens_y_temp) == false
                                                )
                                            {
                                                comboBox1.Items.Add(dpi_x_temp);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = (int)dpi_max; i >= 5; i = i - 5)
                            {
                                dpi_x_temp = i;
                                dpi_y_temp = (i / dpi_x) * dpi_y;
                                if (Validation.IsDecimal(dpi_y_temp) == false)
                                {
                                    if (checkBox3.Checked == true)//かなり少ない選択肢しか出ない
                                    {
                                        MSMU_temp_x = MSMU * (dpi_x / dpi_x_temp);
                                        MSMU_temp_y = MSMU * (dpi_y / dpi_y_temp);
                                        if (GetPrecision1((decimal)MSMU_temp_x) <= 6 &&
                                            GetPrecision1((decimal)MSMU_temp_y) <= 6
                                            )
                                        {
                                            comboBox1.Items.Add(dpi_x_temp.ToString());
                                        }
                                    }
                                    else//フィルターにかけないのでバカみたいな値も出る。プロファイルスイッチが必要。
                                    {
                                        g_sens_x_temp = g_sens_x * (dpi_x / dpi_x_temp);
                                        g_sens_y_temp = (g_sens_y / dpi_y_temp) * dpi_y;

                                        if (g_sens_x_temp >= 0 && g_sens_x_temp <= 100 &&
                                            g_sens_y_temp >= 0 && g_sens_y_temp <= 100 &&
                                            Validation.IsDecimal(g_sens_x_temp) == false &&
                                            Validation.IsDecimal(g_sens_y_temp) == false
                                            )
                                        {
                                            comboBox1.Items.Add(dpi_x_temp);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else//xのみ
                {
                    if (checkBox2.Checked == true)//OSのマウス感度を保持
                    {
                        for (int i = 1; i < 12; i++)
                        {
                            os_sens_temp = num_to_sens(i);//iからos_sens_tempに変換
                            dpi_x_temp = dpi_x * (os_sens / os_sens_temp);

                            if (dpi_x_temp > 0 && dpi_x_temp <= dpi_max && //osのマウス感度を維持可能な一覧の中で0以上、maxdpi以下をフィルター
                                Validation.IsDecimal(dpi_x_temp) == false)//dpiが整数値かどうか
                            {
                                if (checkBox3.Checked == true)
                                {
                                    MSMU_temp_x = MSMU * (dpi_x / dpi_x_temp);
                                    if (GetPrecision1((decimal)MSMU_temp_x) <= 6)
                                    {
                                        if (dpi_x != dpi_x_temp)
                                        {
                                            comboBox1.Items.Add(dpi_x_temp);
                                        }
                                    }
                                }
                                else
                                {
                                    g_sens_x_temp = g_sens_x * (dpi_x / dpi_x_temp);
                                    g_sens_y_temp = (g_sens_y / dpi_x_temp) * dpi_x;

                                    if (g_sens_x_temp >= 0 && g_sens_x_temp <= 100 &&
                                        g_sens_y_temp >= 0 && g_sens_y_temp <= 100 &&
                                        Validation.IsDecimal(g_sens_x_temp) == false &&
                                        Validation.IsDecimal(g_sens_y_temp) == false
                                        )
                                    {
                                        if (dpi_x!= dpi_x_temp)
                                        {
                                            comboBox1.Items.Add(dpi_x_temp);
                                        }                        
                                    }
                                }
                            }
                        }
                    }
                    else//OSのマウス感度を保持しない。
                    {
                        if (checkBox4.Checked == true)//一応OSのマウス感度が調整可能なレベルのフィルターを掛ける
                        {
                            double os_sens_choice;
                            var list = new[] { 0.025, 0.05, 0.25, 0.5, 0.75, 1, 1.5, 2, 2.5, 3, 3.5 };

                            for (int i = (int)dpi_max; i >= 5; i = i - 5)
                            {
                                os_sens_choice = (dpi_x * os_sens) / i;//選択したdpiでos感度を維持するとos_sensはいくらであればいいか
                                os_sens_new = list.Nearest(os_sens_choice);
                                dpi_x_temp = i;
                                if (textBox8.Text == "")
                                {
                                    textBox8.Text = "0";
                                }
                                if (System.Math.Abs(dpi_x * os_sens - dpi_x_temp * os_sens_new) <= double.Parse(textBox8.Text))//変更後のOSのマウス感度を誤差500dpi以内に収める
                                {
                                    if (checkBox3.Checked == true)//かなり少ない選択肢しか出ない
                                    {
                                        MSMU_temp_x = MSMU * (dpi_x / dpi_x_temp);
                                        if (GetPrecision1((decimal)MSMU_temp_x) <= 6)
                                        {
                                            comboBox1.Items.Add(dpi_x_temp.ToString());
                                        }
                                    }
                                    else//フィルターにかけないのでバカみたいな値も出る。プロファイルスイッチが必要。
                                    {
                                        g_sens_x_temp = g_sens_x * (dpi_x / dpi_x_temp);
                                        g_sens_y_temp = g_sens_y / dpi_x_temp * dpi_x;

                                        if (g_sens_x_temp >= 0 && g_sens_x_temp <= 100 &&
                                            g_sens_y_temp >= 0 && g_sens_y_temp <= 100 &&
                                            Validation.IsDecimal(g_sens_x_temp) == false &&
                                            Validation.IsDecimal(g_sens_y_temp) == false
                                            )
                                        {
                                            comboBox1.Items.Add(dpi_x_temp);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = (int)dpi_max; i >= 5; i = i - 5)
                            {
                                if (checkBox3.Checked == true)//かなり少ない選択肢しか出ない
                                {
                                    MSMU_temp_x = MSMU * (dpi_x / i);
                                    if (GetPrecision1((decimal)MSMU_temp_x) <= 6)
                                    {
                                        comboBox1.Items.Add(i.ToString());
                                    }
                                }
                                else//フィルターにかけないのでバカみたいな値も出る。プロファイルスイッチが必要。
                                {
                                    g_sens_x_temp = g_sens_x * (dpi_x / i);
                                    g_sens_y_temp = (g_sens_y / i) * dpi_x;

                                    if (g_sens_x_temp >= 0 && g_sens_x_temp <= 100 &&
                                        g_sens_y_temp >= 0 && g_sens_y_temp <= 100 &&
                                        Validation.IsDecimal(g_sens_x_temp) == false &&
                                        Validation.IsDecimal(g_sens_y_temp) == false
                                        )
                                    {
                                        comboBox1.Items.Add(i);
                                    }
                                }
                            }
                        }
                    }
                }

                if (comboBox1.Items.Count != 0)//変更可能な選択肢があるか
                {
                    comboBox1.Enabled = true;
                    comboBox1.SelectedIndex = 0;
                    label40.Text = "";
                }
                else
                {
                    comboBox1.Enabled = false;
                    label40.Text="変更可能な選択肢がありません。";
                }
            }
            else
            {
                MessageBox.Show("適切なデータを入力してください。");
            }

            if (comboBox1.Items.Count != 0)//適応ボタンを表示・非表示
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";

            label27.Text = "";
            label28.Text = "";
            label29.Text = "";
            label30.Text = "";
            label31.Text = "";
            label32.Text = "";
            label36.Text = "";
            label37.Text = "";
            label38.Text = "";

            iniFileName = "";
            label41.Text = "読み込み中のファイルのプレイヤーID ： ";

            if (textBox1.Text == "") { textBox1.BackColor = SystemColors.Window; }
            if (textBox3.Text == "") { textBox3.BackColor = SystemColors.Window; }
            if (textBox2.Text == "") { textBox2.BackColor = SystemColors.Window; }
            if (textBox4.Text == "") { textBox4.BackColor = SystemColors.Window; }
            if (textBox5.Text == "") { textBox5.BackColor = SystemColors.Window; }
            if (textBox6.Text == "") { textBox6.BackColor = SystemColors.Window; }
            if (textBox7.Text == "") { textBox7.BackColor = SystemColors.Window; }

            comboBox1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private int GetPrecision1(decimal price)
        {
            string priceString = price.ToString().TrimEnd('0');

            int index = priceString.IndexOf('.');
            if (index == -1)
                return 0;

            return priceString.Substring(index + 1).Length;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dpi_x_new = double.Parse(comboBox1.Text);
            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
            if (checkBox1.Checked == true)//y軸も考慮する。
            {
                if (checkBox2.Checked == true)
                {
                    os_sens_new = ((dpi_x * os_sens) / dpi_x_new);
                    os_sens_num_new = (int)sens_to_num(os_sens_new);//選択したdpiからosの設定値を算出
                    if (checkBox3.Checked == true)
                    {
                        MSMU_new = MSMU * (dpi_x / dpi_x_new);
                        g_sens_x_new = g_sens_x;
                        g_sens_y_new = g_sens_y;
                    }
                    else
                    {
                        MSMU_new = MSMU;
                        g_sens_x_new = g_sens_x * (dpi_x / dpi_x_new);
                        g_sens_y_new = (g_sens_y / dpi_y_new) * dpi_y;
                    }
                }
                else
                {
                    if (checkBox4.Checked == true)
                    {
                        double os_sens_choice;
                        var list = new[] { 0.025, 0.05, 0.25, 0.5, 0.75, 1, 1.5, 2, 2.5, 3, 3.5 };
                        if (checkBox3.Checked == true)
                        {
                            MSMU_new = MSMU * (dpi_x / dpi_x_new);
                            dpi_x_new = double.Parse(comboBox1.Text);
                            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
                            os_sens_choice = (dpi_x * os_sens) / double.Parse(comboBox1.Text);
                            os_sens_new = list.Nearest(os_sens_choice);
                            os_sens_num_new = (int)sens_to_num(os_sens_new);
                            g_sens_x_new = g_sens_x;
                            g_sens_y_new = g_sens_y;
                        }
                        else
                        {
                            MSMU_new = MSMU;
                            dpi_x_new = double.Parse(comboBox1.Text);
                            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
                            os_sens_choice = (dpi_x * os_sens) / double.Parse(comboBox1.Text);
                            os_sens_new = list.Nearest(os_sens_choice);
                            os_sens_num_new = (int)sens_to_num(os_sens_new);
                            g_sens_x_new = g_sens_x * (dpi_x / dpi_x_new);
                            g_sens_y_new = (g_sens_y / dpi_y_new) * dpi_y;
                        }
                    }
                    else
                    {
                        if (checkBox3.Checked == true)
                        {
                            MSMU_new = MSMU * (dpi_x / dpi_x_new);
                            dpi_x_new = double.Parse(comboBox1.Text);
                            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
                            os_sens_new = os_sens;
                            os_sens_num_new = os_sens_num;
                            g_sens_x_new = g_sens_x;
                            g_sens_y_new = g_sens_y;
                        }
                        else
                        {
                            MSMU_new = MSMU;
                            dpi_x_new = double.Parse(comboBox1.Text);
                            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
                            os_sens_new = os_sens;
                            os_sens_num_new = os_sens_num;
                            g_sens_x_new = g_sens_x * (dpi_x / dpi_x_new);
                            g_sens_y_new = (g_sens_y / dpi_y_new) * dpi_y;
                        }
                    }
                }
            }
            else//xのみ
            {
                if (checkBox2.Checked == true)
                {
                    os_sens_new = ((dpi_x * os_sens) / dpi_x_new);
                    os_sens_num_new = (int)sens_to_num(os_sens_new);//選択したdpiからosの設定値を算出
                    if (checkBox3.Checked == true)
                    {
                        MSMU_new = MSMU * (dpi_x / dpi_x_new);
                        g_sens_x_new = g_sens_x;
                        g_sens_y_new = g_sens_y;
                    }
                    else
                    {
                        MSMU_new = MSMU;
                        g_sens_x_new = g_sens_x * (dpi_x / dpi_x_new);
                        g_sens_y_new = (g_sens_y / dpi_y_new) * dpi_y;
                    }
                }
                else
                {
                    if (checkBox4.Checked == true)
                    {
                        double os_sens_choice;
                        var list = new[] { 0.025, 0.05, 0.25, 0.5, 0.75, 1, 1.5, 2, 2.5, 3, 3.5 };
                        if (checkBox3.Checked == true)
                        {
                            MSMU_new = MSMU * (dpi_x / dpi_x_new);
                            dpi_x_new = double.Parse(comboBox1.Text);
                            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
                            os_sens_choice = (dpi_x * os_sens) / double.Parse(comboBox1.Text);
                            os_sens_new = list.Nearest(os_sens_choice);
                            os_sens_num_new = (int)sens_to_num(os_sens_new);
                            g_sens_x_new = g_sens_x;
                            g_sens_y_new = g_sens_y;
                        }
                        else
                        {
                            MSMU_new = MSMU;
                            dpi_x_new = double.Parse(comboBox1.Text);
                            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
                            os_sens_choice = (dpi_x * os_sens) / double.Parse(comboBox1.Text);
                            os_sens_new = list.Nearest(os_sens_choice);
                            os_sens_num_new = (int)sens_to_num(os_sens_new);
                            g_sens_x_new = g_sens_x * (dpi_x / dpi_x_new);
                            g_sens_y_new = (g_sens_y / dpi_y_new) * dpi_y;
                        }
                    }
                    else
                    {
                        if (checkBox3.Checked == true)
                        {
                            MSMU_new = MSMU * (dpi_x / dpi_x_new);
                            dpi_x_new = double.Parse(comboBox1.Text);
                            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
                            os_sens_new = os_sens;
                            os_sens_num_new = os_sens_num;
                            g_sens_x_new = g_sens_x;
                            g_sens_y_new = g_sens_y;
                        }
                        else
                        {
                            MSMU_new = MSMU;
                            dpi_x_new = double.Parse(comboBox1.Text);
                            dpi_y_new = double.Parse(comboBox1.Text) * (dpi_y / dpi_x);
                            os_sens_new = os_sens;
                            os_sens_num_new = os_sens_num;
                            g_sens_x_new = g_sens_x * (dpi_x / dpi_x_new);
                            g_sens_y_new = (g_sens_y / dpi_y_new) * dpi_y;
                        }
                    }
                }
            }

            label15.Text = dpi_x_new.ToString();
            label16.Text = dpi_y_new.ToString();
            label17.Text = g_sens_x_new.ToString();
            label18.Text = g_sens_y_new.ToString();
            label19.Text = os_sens_num_new.ToString();
            label20.Text = string.Format("{0:f6}\r\n", MSMU_new);

            label27.Text = (dpi_x * os_sens).ToString();
            label28.Text = (dpi_x * g_sens_x * MSMU).ToString();
            label29.Text = (dpi_y * g_sens_y * MSMU).ToString();
            label30.Text = (dpi_x_new * os_sens_new).ToString();
            label31.Text = (dpi_x_new * g_sens_x_new * MSMU_new).ToString();
            label32.Text = (dpi_y_new * g_sens_y_new * MSMU_new).ToString();

            label36.Text = (double.Parse(label27.Text) - double.Parse(label30.Text)).ToString();
            label37.Text = (double.Parse(label28.Text) - double.Parse(label31.Text)).ToString();
            label38.Text = (double.Parse(label29.Text) - double.Parse(label32.Text)).ToString();
        }//comboBox1の値が選択されたら

        private void MSMU_Calculation()
        {
            MSMU_new = Math.Round(MSMU / (50 / g_sens_x_new), 6);
            g_sens_x_new = 50;
            g_sens_y_new = Math.Round((g_sens_y_new * MSMU) / MSMU_new);
        }//MSMUの最適化

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

            if (checkBox1.Checked)
            {
                if (double.TryParse(textBox2.Text, out try_out))
                {
                    if (textBox2.Text != "" && 0 < double.Parse(textBox2.Text))
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
                if (textBox4.Text != "" && 0 < double.Parse(textBox4.Text) && double.Parse(textBox4.Text) <= 100)
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

            if (double.TryParse(textBox5.Text, out try_out))
            {
                if (textBox5.Text != "" && 1 <= double.Parse(textBox5.Text) && double.Parse(textBox5.Text) <= 11 && GetPrecision1((decimal)double.Parse(textBox5.Text)) == 0)
                {
                    textBox5.BackColor = SystemColors.Window;
                }
                else
                {
                    textBox5.BackColor = Color.Yellow;
                    result = false;
                }
            }
            else
            {
                textBox5.BackColor = Color.Yellow;
                result = false;
            }

            if (double.TryParse(textBox6.Text, out try_out))
            {
                if (textBox6.Text != "" && 0 < double.Parse(textBox6.Text))
                {
                    textBox6.BackColor = SystemColors.Window;
                }
                else
                {
                    textBox6.BackColor = Color.Yellow;
                    result = false;
                }
            }
            else
            {
                textBox6.BackColor = Color.Yellow;
                result = false;
            }

            if (double.TryParse(textBox7.Text, out try_out))
            {
                if (textBox7.Text != "" && 0 < double.Parse(textBox7.Text) && GetPrecision1((decimal)double.Parse(textBox7.Text)) <= 6 && double.Parse(textBox7.Text) < 10)
                {
                    textBox7.BackColor = SystemColors.Window;
                }
                else
                {
                    textBox7.BackColor = Color.Yellow;
                    result = false;
                }
            }
            else
            {
                textBox7.BackColor = Color.Yellow;
                result = false;
            }

            return result;
        }//入力された値が数字かどうか

        private void Save_Data()
        {
            if (Finish_load && Check_Appropriate_Value())
            {
                if (Properties.Settings.Default.option_Save_Data)
                {
                    Properties.Settings.Default.DPI_X = double.Parse(textBox1.Text);
                    if (checkBox1.Checked == true)
                    {
                        Properties.Settings.Default.DPI_Y = double.Parse(textBox2.Text);
                    }
                    //Properties.Settings.Default.G＿X = double.Parse(textBox3.Text);
                    //Properties.Settings.Default.G＿Y = double.Parse(textBox4.Text);
                    //Properties.Settings.Default.OS = double.Parse(textBox5.Text);
                    Properties.Settings.Default.DPI_MAX = double.Parse(textBox6.Text);
                    //Properties.Settings.Default.MSMU = double.Parse(textBox7.Text);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void Load_Data()
        {
            textBox1.Text = Properties.Settings.Default.DPI_X.ToString();
            if (checkBox1.Checked == true)
            {
                textBox2.Text = Properties.Settings.Default.DPI_Y.ToString();
            }
            //textBox3.Text = Properties.Settings.Default.G＿X.ToString();
            //textBox4.Text = Properties.Settings.Default.G＿Y.ToString();
            //textBox5.Text = Properties.Settings.Default.OS.ToString();
            textBox6.Text = Properties.Settings.Default.DPI_MAX.ToString();
            //textBox7.Text = string.Format("{0:f6}\r\n", Properties.Settings.Default.MSMU);
        }

        private void Load_Gametettings()
        {
            load_gamesetting = true;
            button3.Enabled = true;
            iniFileName = Properties.Settings.Default.gamesettings_file;

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

            StringBuilder MouseSensitivityMultiplierUnit = new StringBuilder(1024); //y軸感度
            GetPrivateProfileString(
                "INPUT",
                "MouseSensitivityMultiplierUnit",
                "ありません",
                MouseSensitivityMultiplierUnit,
                Convert.ToUInt32(MouseSensitivityMultiplierUnit.Capacity),
                iniFileName);

            textBox3.Text = MouseYawSensitivity.ToString();
            textBox4.Text = MousePitchSensitivity.ToString();
            textBox5.Text = MouseSpeed.Get().ToString();
            textBox7.Text = MouseSensitivityMultiplierUnit.ToString();

            player_ID(iniFileName);
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

        private void バージョン情報ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("【R6S Mouse Sensitivity Tweak Ver 1.0.0.0】" + Environment.NewLine +
                            "--------------------------------------------------" + Environment.NewLine +
                            "Twitter : https://twitter.com/Annin_game_", "バージョン情報");
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
        }//引数が整数かどうか（整数ならfalse）

        public static class MouseSpeed
        {
            [DllImport("user32", SetLastError = true)]
            private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvVoid, uint fWinIni);

            private const uint SPI_GETMOUSESPEED = 0x70;
            private const uint SPI_SETMOUSESPEED = 0x71;

            public static void Set(int speed)
            {
                switch (speed)
                {
                    case 1:
                        speed = 1;
                        break;

                    case 2:
                        speed = 2;
                        break;

                    case 3:
                        speed = 4;
                        break;

                    case 4:
                        speed = 6;
                        break;

                    case 5:
                        speed = 8;
                        break;

                    case 6:
                        speed = 10;
                        break;

                    case 7:
                        speed = 12;
                        break;

                    case 8:
                        speed = 14;
                        break;

                    case 9:
                        speed = 16;
                        break;

                    case 10:
                        speed = 18;
                        break;

                    case 11:
                        speed = 20;
                        break;

                    default:
                        MessageBox.Show("osのマウス感度の設定に失敗しました。");
                        break;
                }//取得したos感度を適正値に変更
                if (!SystemParametersInfo(SPI_SETMOUSESPEED, 0, new IntPtr(speed), 0))
                {
                    throw new Exception("マウスカーソルの速度を設定できませんでした。");
                }
            }

            unsafe public static int Get()
            {
                int result;

                if (!SystemParametersInfo(SPI_GETMOUSESPEED, 0, new IntPtr((void*)&result), 0))
                {
                    throw new Exception("マウスカーソルの速度を取得できませんでした。");
                }
                switch (result)
                {
                    case 1:
                        result = 1;
                        break;

                    case 2:
                        result = 2;
                        break;

                    case 4:
                        result = 3;
                        break;

                    case 6:
                        result = 4;
                        break;

                    case 8:
                        result = 5;
                        break;

                    case 10:
                        result = 6;
                        break;

                    case 12:
                        result = 7;
                        break;

                    case 14:
                        result = 8;
                        break;

                    case 16:
                        result = 9;
                        break;

                    case 18:
                        result = 10;
                        break;

                    case 20:
                        result = 11;
                        break;

                    default:
                        MessageBox.Show("osのマウス感度を取得できませんでした。");
                        break;
                }//取得したos感度を適正値に変更

                return result;
            }
        }//OSのマウス感度の取得・変更

        private void back_up()
        {
            //バックアップしたいファイル名を取得
            var filename = System.IO.Path.GetFileName(iniFileName);
            //日付を取得
            var d = DateTime.Now.ToString("yyyyMMdd_HHmm-");
            d = d + "DPI_X_" +label15.Text + "-DPI_Y_" + label16.Text + "-OS_" + label19.Text+"-";
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
                   label17.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "MousePitchSensitivity",
                    label18.Text,
                   iniFileName);

            WritePrivateProfileString(
                   "INPUT",
                   "MouseSensitivityMultiplierUnit",
                    label20.Text,
                   iniFileName);
        }

        private double num_to_sens(double a)
        {
            switch (a)
            {
                case 1:
                    a = 0.025;
                    break;

                case 2:
                    a = 0.05;
                    break;

                case 3:
                    a = 0.25;
                    break;

                case 4:
                    a = 0.5;
                    break;

                case 5:
                    a = 0.75;
                    break;

                case 6:
                    a = 1;
                    break;

                case 7:
                    a = 1.5;
                    break;

                case 8:
                    a = 2;
                    break;

                case 9:
                    a = 2.5;
                    break;

                case 10:
                    a = 3;
                    break;

                case 11:
                    a = 3.5;
                    break;

                default:
                    MessageBox.Show("Errer : 入力されたOSのマウス感度が異常です。");
                    break;
            }//os_sens_numからos_sensを取得
            return a;
        }

        private double sens_to_num(double a)
        {
            switch (a)//新しいos感度の取得 チェック２が外れたとき
            {
                case 0.025:
                    a = 1;
                    break;

                case 0.05:
                    a = 2;
                    break;

                case 0.25:
                    a = 3;
                    break;

                case 0.5:
                    a = 4;
                    break;

                case 0.75:
                    a = 5;
                    break;

                case 1:
                    a = 6;
                    break;

                case 1.5:
                    a = 7;
                    break;

                case 2:
                    a = 8;
                    break;

                case 2.5:
                    a = 9;
                    break;

                case 3:
                    a = 10;
                    break;

                case 3.5:
                    a = 11;
                    break;

                default:
                    MessageBox.Show("入力されたOSのマウス感度倍率が異常です。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            return a;
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

        private void button5_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Clear_left_data_sheet()
        {
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
        }

        private void player_ID(string x)
        {
            string[] filename;
            filename = x.Split('\\');
            Array.Reverse(filename);
            label41.Text = "読み込み中のファイルのプレイヤーID ： " + filename[1];
        }
    }

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 目的の値に最も近い値を返します
        /// </summary>
        public static double Nearest(
            this IEnumerable<double> self,
            double target
        )
        {
            var min = self.Min(c => Math.Abs(c - target));
            return self.First(c => Math.Abs(c - target) == min);
        }
    }//もっとも近い値を返す。
}