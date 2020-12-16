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

        private Point mousePoint = new Point();

        private void InitForm_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint.X = e.X;
            mousePoint.Y = e.Y;
        }

        private void InitForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point Posittion = MousePosition;
                Posittion.Offset(-mousePoint.X, -mousePoint.Y);
                Location = Posittion;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "软件还未完成初始化设置, 您确定要退出吗?", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }
    }
}
