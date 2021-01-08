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
using System.Windows.Forms.DataVisualization.Charting;

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
                FileStream stream = File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/BackGround.png");
                this.BackgroundImage = Image.FromStream(stream);
                stream.Close();
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
                    if (File.Exists(GlobalVariable.dataPath + "DaysDatas/DayData-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day - 1).ToString() + ".json"))
                    {
                        label7.Visible = false;
                        if (File.Exists(GlobalVariable.dataPath + "DaysDatas/DayData-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day - count).ToString() + ".json"))
                        {
                            MainChart.Series[0].Points.AddXY(
                                8 - count, JObject.Parse(File.ReadAllText(GlobalVariable.dataPath + "DaysDatas/DayData-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day - count).ToString() + ".json"))
                                .ToObject<DayData>().time);
                            count++;
                        }
                        else
                        {
                            MainChart.Series[0].Points.AddXY(8 - count, 0);
                            count++;
                        }
                    }
                    else break;
                }
                else unexhausted = false;
            }

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
                radioButton1.ForeColor = Color.White;
                radioButton2.ForeColor = Color.White;
                textBox1.BackColor = myGray;
                textBox1.ForeColor = Color.White;
                button1.BackColor = myBlack;
                button2.BackColor = myBlack;
                button3.BackColor = myBlack;
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
                button3.ForeColor = Color.White;
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
           if(!string.IsNullOrEmpty(textBox1.Text))
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
                    }
                    else
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
            } else MessageBox.Show(this, "值不得为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dateToday = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG 格式 (*.PNG)|*.png|JPEG 格式 (*.JPG)|*.jpg|BMP 位图格式 (*.BMP)|*.bmp|GIF 动态图片 (*.GIF)|*.gif";
            dialog.DefaultExt = "png";
            dialog.FileName = dateToday + "_ChartImage-近一周学习时间数据";
            dialog.Title = "保存近一周学习时间数据图表";

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    ChartImageFormat format;
                    switch (dialog.FilterIndex)
                    {
                        case 1:
                            format = ChartImageFormat.Png;
                            break;
                        case 2:
                            format = ChartImageFormat.Jpeg;
                            break;
                        case 3:
                            format = ChartImageFormat.Bmp;
                            break;
                        case 4:
                            format = ChartImageFormat.Gif;
                            break;
                        default:
                            format = ChartImageFormat.Png;
                            break;
                    }
                    MainChart.SaveImage(dialog.FileName, format);
                    MessageBox.Show(this, dateToday + "的近一周学习时间数据图像导出成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show(this, dateToday + "的近一周学习时间数据图像导出失败, 可供参考的错误抛出原因是:\n(1) 传入的保存文件名不得为空字符串或传入 Null.", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            new SettingForm().Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ExamsForm().Show(this);
        }
    }
}