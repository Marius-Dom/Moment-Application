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
    public partial class MainForm : Form
    {
        private DayData dayData = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string dateToday = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();

            if (!Directory.Exists(GlobalVariable.dataPath + "DaysDatas"))
            {
                Directory.CreateDirectory(GlobalVariable.dataPath + "DaysDatas/");
            }

            string todayDataPath = GlobalVariable.dataPath + "DaysDatas/DayData-" + dateToday + ".json";

            if (!File.Exists(todayDataPath))
            {
                File.WriteAllText(todayDataPath, JObject.FromObject(new DayData {
                    time = 0
                }).ToString());
                dayData = JObject.Parse(File.ReadAllText(todayDataPath)).ToObject<DayData>();
            } 
            else
            {
                dayData = JObject.Parse(File.ReadAllText(todayDataPath)).ToObject<DayData>();
            }

            GlobalVariable.mainInformation = JObject.Parse(File.ReadAllText(GlobalVariable.dataPath + "Information.json")).ToObject<Information>();


            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/BackGround.png"))
            {
                this.BackgroundImage = Image.FromFile(Environment.SpecialFolder.ApplicationData + "/Moment/BackGround.png");
            }

            label1.Text = "你好," + GlobalVariable.mainInformation.nickname + ".   今日总共已学习:  " + dayData.time.ToString() + " 分钟.";
            
            if (GlobalVariable.mainInformation.aim > dayData.time)
            {
                label4.Text = "还有  " + (GlobalVariable.mainInformation.aim - dayData.time).ToString() + "分钟  完成今日的学习任务.";
            }
            else if (GlobalVariable.mainInformation.aim == dayData.time)
            {
                label4.Text = "今日学习任务已完成!";
            } 
            else
            {
                label4.Text = "今日学习任务已完成,并且比目标多学" + (dayData.time - GlobalVariable.mainInformation.aim).ToString() + "分钟!";
            }

            label3.Text = "学习时间管理软件  " + dateToday;

            bool unexhausted = true;
            int count = 1;

            while (unexhausted)
            {
                if (count <= 7)
                {
                    if (File.Exists(GlobalVariable.dataPath + "DaysDatas/DayData-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day - count).ToString() + ".json"))
                    {
                        MainChart.Series[0].Points.AddXY(
                            count, JObject.Parse(File.ReadAllText(GlobalVariable.dataPath + "DaysDatas/DayData-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day - count).ToString() + ".json"))
                            .ToObject<DayData>().time);
                        count++;
                    }
                    else break;
                }
                else unexhausted = false;
            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox3.BorderStyle = BorderStyle.None;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox3.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BorderStyle = BorderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dateToday = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            string todayDataPath = GlobalVariable.dataPath + "DaysDatas/DayData-" + dateToday + ".json";

            if (radioButton1.Checked == true)
            {
                File.WriteAllText(todayDataPath, JObject.FromObject(new DayData
                {
                    time = JObject.Parse(File.ReadAllText(todayDataPath)).ToObject<DayData>().time + int.Parse(textBox1.Text)
                }).ToString());

                MessageBox.Show(this, "已成功登记" + textBox1.Text + "分钟", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
            } 
            else
            {
                if (JObject.Parse(File.ReadAllText(todayDataPath)).ToObject<DayData>().time > int.Parse(textBox1.Text))
                {
                    File.WriteAllText(todayDataPath, JObject.FromObject(new DayData
                    {
                        time = JObject.Parse(File.ReadAllText(todayDataPath)).ToObject<DayData>().time - int.Parse(textBox1.Text)
                    }).ToString());

                    MessageBox.Show(this, "已成功注销" + textBox1.Text + "分钟", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show(this, "你不能注销比你已登记的时间还长的时间", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBox1.Text = "";
            }

            dayData = JObject.Parse(File.ReadAllText(todayDataPath)).ToObject<DayData>();

            label1.Text = "你好," + GlobalVariable.mainInformation.nickname + ".   今日总共已学习:  " + dayData.time.ToString() + " 分钟.";

            if (GlobalVariable.mainInformation.aim > dayData.time)
            {
                label4.Text = "还有  " + (GlobalVariable.mainInformation.aim - dayData.time).ToString() + "分钟  完成今日的学习任务.";
            }
            else if (GlobalVariable.mainInformation.aim == dayData.time)
            {
                label4.Text = "今日学习任务已完成!";
            }
            else
            {
                label4.Text = "今日学习任务已完成,并且比目标多学" + (dayData.time - GlobalVariable.mainInformation.aim).ToString() + "分钟!";
            }
        }
    }
}
