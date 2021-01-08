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
    public partial class ExamsForm : Form
    {
        int count = 1;
        public ExamsForm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (Char)8)
                e.Handled = true;
        }

        private void ExamsForm_Load(object sender, EventArgs e)
        {
            Exams exams = JObject.Parse(File.ReadAllText(GlobalVariable.dataPath + "Exams/Exam.json")).ToObject<Exams>();
            foreach (int var in exams.score)
            {
                chart1.Series[0].Points.AddXY(count, var);
                count++;
            }

            //ArgumentOutOfRangeException
            try
            {
                if ((exams.score[count - 2] - exams.score[count - 3]) > GlobalVariable.mainInformation.score.improve)
                {
                    label2.Text = "恭喜! 您本次考试比上一次考试进步的分数超过了您设定的目标!,超过了 " + (exams.score[count - 2] - exams.score[count - 3] - GlobalVariable.mainInformation.score.improve).ToString() + " 分!";
                }
                else if ((exams.score[count - 2] - exams.score[count - 3]) == GlobalVariable.mainInformation.score.improve)
                {
                    label2.Text = "恭喜! 您本次考试比上一次考试进步的分数达到了您设定的目标!";
                }
                else if ((exams.score[count - 2] - exams.score[count - 3]) > 0)
                {
                    label2.Text = "您本次考试相比上次考试虽然进步了 " + (exams.score[count - 2] - exams.score[count - 3]).ToString() + " 分,但未超过目标,再接再励!";
                }
                else if ((exams.score[count - 2] - exams.score[count - 3]) < 0)
                {
                    label2.Text = "较上次有退步，加油。";
                }
                else if ((exams.score[count - 2] - exams.score[count - 3]) == 0)
                {
                    label2.Text = "较上次分数一样，加油。";
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                label2.Text = "数据不足以分析，请添加考试。";
            }

            if (GlobalVariable.mainInformation.darkModeOn)
            {
                Color myGray = Color.FromArgb(50, 50, 50);
                Color myBlack = Color.FromArgb(30, 30, 30);
                this.BackColor = myGray;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                textBox1.BackColor = myGray;
                textBox1.ForeColor = Color.White;
                button1.BackColor = myBlack;
                button2.BackColor = myBlack;
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) <= GlobalVariable.mainInformation.score.maxScore)
            {
                Exams exams = JObject.Parse(File.ReadAllText(GlobalVariable.dataPath + "Exams/Exam.json")).ToObject<Exams>();
                exams.score.Add(int.Parse(textBox1.Text));
                File.WriteAllText(GlobalVariable.dataPath + "Exams/Exam.json", JObject.FromObject(exams).ToString());
                count++;
                chart1.Series[0].Points.AddXY(count, int.Parse(textBox1.Text));
                textBox1.Text = "";
                if ((exams.score[count - 2] - exams.score[count - 3]) > GlobalVariable.mainInformation.score.improve)
                {
                    label2.Text = "恭喜! 您本次考试比上一次考试进步的分数超过了您设定的目标!,超过了 " + (exams.score[count - 2] - exams.score[count - 3] - GlobalVariable.mainInformation.score.improve).ToString() + " 分!";
                }
                else if ((exams.score[count - 2] - exams.score[count - 3]) == GlobalVariable.mainInformation.score.improve)
                {
                    label2.Text = "恭喜! 您本次考试比上一次考试进步的分数达到了您设定的目标!";
                }
                else if ((exams.score[count - 2] - exams.score[count - 3]) > 0)
                {
                    label2.Text = "您本次考试相比上次考试虽然进步了 " + (exams.score[count - 2] - exams.score[count - 3]).ToString() + " 分,但未超过目标,再接再励!";
                }
                else if ((exams.score[count - 2] - exams.score[count - 3]) < 0)
                {
                    label2.Text = "较上次有退步，加油。";
                }
                else if ((exams.score[count - 2] - exams.score[count - 3]) == 0)
                {
                    label2.Text = "较上次分数一样，加油。";
                }
            }
            else MessageBox.Show(this, "错误, 您不能添加一个超过满分的成绩!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dateToday = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG 格式 (*.PNG)|*.png|JPEG 格式 (*.JPG)|*.jpg|BMP 位图格式 (*.BMP)|*.bmp|GIF 动态图片 (*.GIF)|*.gif";
            dialog.DefaultExt = "png";
            dialog.FileName = dateToday + "_ChartImage-考试数据图表";
            dialog.Title = "保存考试数据图表";

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
                    chart1.SaveImage(dialog.FileName, format);
                    MessageBox.Show(this, dateToday + "的考试数据图表导出成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, dateToday + "的考试数据图表导出失败, 可供参考的错误抛出原因是:\n(1) 传入的保存文件名不得为空字符串或传入 Null.", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
    }
}
