using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moment_Application
{
    public partial class Mindfulness : Form
    {
        bool buttonClose = false;
        public Mindfulness()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (Char)8)
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(int.Parse(textBox1.Text) >= 1))
            {
                MessageBox.Show(this, "为了确保您正确的进入正念状态，您的正念时间必须设置为至少10分钟!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GlobalVariable.MindfulnessTime = int.Parse(textBox1.Text);
            if (GlobalVariable.mainInformation.unCompulsoryMindfulness)
            {
                new MindfulnessFront().Show();
                buttonClose = true;
            } else
            {
                new MindfulnessBack().Show();
                buttonClose = true;
            }
            this.Close();
        }

        private void Mindfulness_Load(object sender, EventArgs e)
        {
            if (GlobalVariable.mainInformation.darkModeOn)
            {
                Color myGray = Color.FromArgb(50, 50, 50);
                Color myBlack = Color.FromArgb(30, 30, 30);
                this.BackColor = myGray;
                groupBox1.BackColor = myGray;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                groupBox1.ForeColor = Color.White;
                textBox1.BackColor = myGray;
                textBox1.ForeColor = Color.White;
                button1.BackColor = myBlack;
                button1.ForeColor = Color.White;
            }
        }

        private void Mindfulness_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!buttonClose)
            {
                MainForm.mainForm.Show();
            }
        }
    }
}
