using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moment_Application
{
    public partial class SettingForm : Form
    {
        bool processClose = true;

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (GlobalVariable.mainInformation.darkModeOn)
            {
                Color myGray = Color.FromArgb(50, 50, 50);
                Color myBlack = Color.FromArgb(30, 30, 30);
                this.BackColor = myGray;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                label7.ForeColor = Color.White;
                groupBox1.ForeColor = Color.White;
                groupBox2.ForeColor = Color.White;
                radioButton1.ForeColor = Color.White;
                radioButton2.ForeColor = Color.White;
                button1.BackColor = myBlack;
                button2.BackColor = myBlack;
                button3.BackColor = myBlack;
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
                button3.ForeColor = Color.White;
                textBox1.BackColor = myGray;
                textBox2.BackColor = myGray;
                textBox3.BackColor = myGray;
                textBox5.BackColor = myGray;
                textBox1.ForeColor = Color.White;
                textBox2.ForeColor = Color.White;
                textBox3.ForeColor = Color.White;
                textBox5.ForeColor = Color.White;

            }
            textBox1.Text = GlobalVariable.mainInformation.score.maxScore.ToString();
            textBox2.Text = GlobalVariable.mainInformation.aim.ToString();
            textBox3.Text = GlobalVariable.mainInformation.score.improve.ToString();
            textBox5.Text = GlobalVariable.mainInformation.nickname.ToString();
            label7.Text = "程序版本: " + Application.ProductVersion;
            if(GlobalVariable.mainInformation.darkModeOn)
            {
                radioButton2.Checked = true;
            } 
            else
            {
                radioButton1.Checked = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (Char)8)
                e.Handled = true;
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

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!processClose)
            {
                if (DialogResult.No == MessageBox.Show(this, "所有未保存的更改都将丢失, 确定?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    e.Cancel = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PNG 格式 (*.PNG)|*.png|JPEG 格式 (*.JPG)|*.jpg|BMP 位图格式 (*.BMP)|*.bmp";
            dialog.Title = "选择背景图片......";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dialog.FileName))
                {
                    if (MessageBox.Show(this, "是否确定写入设置? 如若按下\"确定\",则程序将会重启.", "成功", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/BackGround.png");
                        File.Copy(dialog.FileName, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/BackGround.png");
                        new Process { StartInfo = new ProcessStartInfo { FileName = Application.ExecutablePath } }.Start();
                        processClose = true;
                        Application.Exit();
                    } 
                }
                else MessageBox.Show(this, "给定文件不存在", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "是否确定清除? 如若按下\"确定\",则程序将会重启.", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { 
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/BackGround.png");
                new Process { StartInfo = new ProcessStartInfo { FileName = Application.ExecutablePath } }.Start();
                processClose = true;
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "选择成功,是否确定写入设置? 如若按下\"确定\",则程序将会重启.", "成功", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Information information = new Information
                {
                    initialization = true,
                    nickname = textBox5.Text,
                    darkModeOn = radioButton2.Checked,
                    aim = int.Parse(textBox2.Text),
                    score = new Information.Score
                    {
                        improve = int.Parse(textBox3.Text),
                        maxScore = int.Parse(textBox1.Text)
                    }
                };

                File.WriteAllText(GlobalVariable.dataPath + "Information.json", JObject.FromObject(information).ToString());

                new Process { StartInfo = new ProcessStartInfo { FileName = Application.ExecutablePath } }.Start();
                processClose = true;
                Application.Exit();
            }
        }
    }
}
