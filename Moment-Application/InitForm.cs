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

        public InitForm()
        {
            InitializeComponent();
        }

        private void InitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "软件还未完成初始化设置, 您确定要退出吗?", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void InitForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                Application.Exit();
        }

        private void colorSelectButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "您将切换至 深色模式 , 确认?", "主题切换", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                darkModeOn = true;
                Color myGray = Color.FromArgb(50, 50, 50);
                Color myBlack = Color.FromArgb(30, 30, 30);
                this.BackColor = myBlack;
                backbroadBox.BackColor = myGray;
                mainLabel1.ForeColor = Color.White;
                mainLabel2.ForeColor = Color.White;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                textBox1.BackColor = myGray;
                textBox1.ForeColor = Color.White;
                numericUpDown1.BackColor = myGray;
                numericUpDown1.ForeColor = Color.White;
                numericUpDown2.BackColor = myGray;
                numericUpDown2.ForeColor = Color.White;
                numericUpDown3.BackColor = myGray;
                numericUpDown3.ForeColor = Color.White;
                numericUpDown4.BackColor = myGray;
                numericUpDown4.ForeColor = Color.White;
                confirmButton.BackColor = myGray;
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
                numericUpDown1.BackColor = Color.White;
                numericUpDown1.ForeColor = Color.Black;
                numericUpDown2.BackColor = Color.White;
                numericUpDown2.ForeColor = Color.Black;
                numericUpDown3.BackColor = Color.White;
                numericUpDown3.ForeColor = Color.Black;
                numericUpDown4.BackColor = Color.White;
                numericUpDown4.ForeColor = Color.Black;
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

            //if (numericUpDown1.Value > numericUpDown2.Value)
            //{
                //MessageBox.Show(this, "你的分数不得大于满分!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            //}

            Information information = new Information { 
                initialization = true,
                nickname = textBox1.Text,
                darkModeOn = darkModeOn,
                score = new Information.Score
                {
                    improve = Decimal.ToInt32(numericUpDown3.Value),
                    maxScore = Decimal.ToInt32(numericUpDown2.Value)
                }
            };

            Exams exam = new Exams { 
                time = new DateTime(),
                name = "InitializationExam",
                score = Decimal.ToInt32(numericUpDown1.Value)
            };

            File.WriteAllText(GlobalVariable.dataPath + "Information.json", JObject.FromObject(information).ToString());

            Directory.CreateDirectory(GlobalVariable.dataPath + "Exams/");
            File.WriteAllText(GlobalVariable.dataPath + "Exams/Exam-Initialization.json", JObject.FromObject(exam).ToString());
        }
    }
}