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
    public partial class InitForm : Form
    {
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
    }
}