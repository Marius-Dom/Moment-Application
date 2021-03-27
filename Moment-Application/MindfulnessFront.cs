using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moment_Application
{
    public partial class MindfulnessFront : Form
    {
        public int mindfulnessMin = 0;
        public int mindfulnessSec = 0;
        public int prepareTime = 10;
        public bool finish = false;

        public MindfulnessFront()
        {
            InitializeComponent();
        }

        private void MindfulnessFront_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.mindfulnessMin = GlobalVariable.MindfulnessTime;
            button1.Text = "退出（" + prepareTime.ToString() + ")";
            label1.Text = "距离正念结束：" + mindfulnessMin.ToString() + "分钟 " + mindfulnessSec.ToString() + "秒";
            progressBar1.Maximum = mindfulnessMin * 60;

            if (GlobalVariable.mainInformation.darkModeOn)
            {
                Color myGray = Color.FromArgb(50, 50, 50);
                Color myBlack = Color.FromArgb(30, 30, 30);
                this.BackColor = myGray;
                label1.ForeColor = Color.White;
                button1.BackColor = myBlack;
                button1.ForeColor = Color.White;
            }

            if(GlobalVariable.mainInformation.unCompulsoryMindfulness)
            {
                timer2.Stop();
                button1.Text = "退出";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mindfulnessSec == 0)
            {
                if (mindfulnessMin == 0)
                {
                    label1.Text = "正念已完成！";
                    button1.Enabled = true;
                    finish = true;
                    timer1.Stop();
                    return;
                }

                mindfulnessSec = 60;
                mindfulnessMin--;
            }

            mindfulnessSec--;
            progressBar1.Value = progressBar1.Maximum - (mindfulnessMin * 60 + mindfulnessSec);
            label1.Text = "距离正念结束：" + mindfulnessMin.ToString() + "分钟 " + mindfulnessSec.ToString() + "秒";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (prepareTime == 0)
            {
                button1.Text = "退出";
                button1.Enabled = false;
                timer2.Stop();
            } else
            {
                button1.Text = "退出（" + prepareTime.ToString() + ")";
                prepareTime--;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (finish)
            {
                string dateToday = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                string todayDataPath = GlobalVariable.dataPath + "DaysDatas/DayData-" + dateToday + ".json";

                File.WriteAllText(todayDataPath, JObject.FromObject(new DayData
                {
                    time = JObject.Parse(File.ReadAllText(todayDataPath)).ToObject<DayData>().time + progressBar1.Maximum / 60
                }).ToString());

                MessageBox.Show(this, "已成功登记" + progressBar1.Maximum / 60 + "分钟", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            if (!GlobalVariable.mainInformation.unCompulsoryMindfulness)
            {
                MindfulnessBack.mindfulnessBack.Close();
            }
            MainForm.mainForm.flash();
            MainForm.mainForm.Show();
            this.Close();
        }
    }
}
