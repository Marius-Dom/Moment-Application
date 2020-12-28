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

            //Debug Command
            //Console.WriteLine(todayDataPath);

            if (!File.Exists(todayDataPath))
            {
                File.WriteAllText(todayDataPath, JObject.FromObject(new DayData {
                    time = 0
                }).ToString());
            } 
            else
            {
                dayData = JObject.Parse(File.ReadAllText(todayDataPath)).ToObject<DayData>();
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Moment/BackGround.png"))
            {
                this.BackgroundImage = Image.FromFile(Environment.SpecialFolder.ApplicationData + "/Moment/BackGround.png");
            }

            label1.Text = "今日总共已学习:  " + dayData.time.ToString() + " 分钟.";
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
    }
}
