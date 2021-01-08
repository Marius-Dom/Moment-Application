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
using Newtonsoft.Json.Linq;

namespace Moment_Application
{
    public partial class InitForm : Form
    {
        public bool darkModeOn = false;
        private bool isAppClose = false;

        public InitForm()
        {
            InitializeComponent();
        }

        private void InitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isAppClose == false)
            {
                DialogResult result = MessageBox.Show(this, "软件还未完成初始化设置, 您确定要退出吗?", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void InitForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isAppClose == false)
                Application.Exit();
        }

        private void colorSelectButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "您将切换至 深色模式 , 确认?", "主题切换", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                darkModeOn = true;
                Color mymyGray = Color.FromArgb(50, 50, 50);
                Color mymyBlack = Color.FromArgb(30, 30, 30);
                this.BackColor = mymyBlack;
                backbroadBox.BackColor = mymyGray;
                mainLabel1.ForeColor = Color.White;
                mainLabel2.ForeColor = Color.White;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                textBox1.BackColor = mymyGray;
                textBox1.ForeColor = Color.White;
                textBox2.BackColor = mymyGray;
                textBox2.ForeColor = Color.White;
                textBox3.BackColor = mymyGray;
                textBox3.ForeColor = Color.White;
                textBox4.BackColor = mymyGray;
                textBox4.ForeColor = Color.White;
                textBox5.BackColor = mymyGray;
                textBox5.ForeColor = Color.White;
                confirmButton.BackColor = mymyGray;
                confirmButton.ForeColor = Color.White;
            }
        }

        private void colorSelectButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "您将切换至 浅色模式 , 确认?", "主题切换", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                darkModeOn = false;
                this.BackColor = Control.DefaultBackColor;
                backbroadBox.BackColor = Color.White;
                mainLabel1.ForeColor = Color.Black;
                mainLabel2.ForeColor = Color.Black;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                textBox1.BackColor = Color.White;
                textBox1.ForeColor = Color.Black;
                textBox2.BackColor = Color.White;
                textBox2.ForeColor = Color.Black;
                textBox3.BackColor = Color.White;
                textBox3.ForeColor = Color.Black;
                textBox4.BackColor = Color.White;
                textBox4.ForeColor = Color.Black;
                textBox5.BackColor = Color.White;
                textBox5.ForeColor = Color.Black;
                confirmButton.BackColor = Control.DefaultBackColor;
                confirmButton.ForeColor = Color.Black;
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show(this, "昵称不得为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.Parse(textBox2.Text) > int.Parse(textBox3.Text))
            {
                MessageBox.Show(this, "你的分数不得大于满分!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Information information = new Information
            {
                initialization = true,
                nickname = textBox1.Text,
                darkModeOn = darkModeOn,
                aim = int.Parse(textBox5.Text),
                score = new Information.Score
                {
                    improve = int.Parse(textBox4.Text),
                    maxScore = int.Parse(textBox3.Text)
                }
            };

            Exams exam = new Exams
            {
                score = new List<int>() { int.Parse(textBox2.Text) }
            };

            Directory.CreateDirectory(GlobalVariable.dataPath);
            File.WriteAllText(GlobalVariable.dataPath + "Information.json", JObject.FromObject(information).ToString());

            Directory.CreateDirectory(GlobalVariable.dataPath + "Exams/");
            File.WriteAllText(GlobalVariable.dataPath + "Exams/Exam.json", JObject.FromObject(exam).ToString());

            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/README.txt", 
                "# This is a warning message form the applications' anthor\n" +
                "# English\n" +
                "This Directory is a Data Storge folder for the \"Moment Application\". All of the Files were created by the application The application MAY WON'T WORK ANYMORE if the files changed incorrectly, so PLEASE DO NOT CHANGE ANYTHING in there.\n" +
                "\n" +
                "# 中文 (Chinese) \n" +
                "# Translated by Google Translate\n" +
                "该目录是\" Moment Application \"的Data Storge文件夹。 所有文件都是由应用程序创建的。如果文件更改错误，则该应用程序可能无法再使用，因此请不要在其中进行任何更改。\n" +
                "\n" +
                "# Deutsch (German)\n" +
                "# Translated by Google Translate\n" +
                "Dieses Verzeichnis ist ein Data Storge-Ordner für die \"Moment - Anwendung\". Alle Dateien wurden von der Anwendung erstellt. Die Anwendung KANN NICHT MEHR FUNKTIONIEREN, wenn sich die Dateien falsch geändert haben. BITTE ÄNDERN SIE BITTE NICHTS.\n" +
                "\n" +
                "# Русский (Russian)\n" +
                "# Translated by Google Translate\n" +
                "Этот каталог является папкой хранилища данных для  \"Moment Application\". Все файлы были созданы приложением. Приложение МОЖЕТ НЕ РАБОТАТЬ БОЛЬШЕ, если файлы были изменены некорректно, поэтому, ПОЖАЛУЙСТА, НИЧЕГО НЕ МЕНЯЙТЕ в нем.");

            isAppClose = true;
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (Char)8)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (Char)8)
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (Char)8)
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (Char)8)
                e.Handled = true;
        }
    }
}