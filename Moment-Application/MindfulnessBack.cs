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
    public partial class MindfulnessBack : Form
    {
        public static MindfulnessBack mindfulnessBack;

        public MindfulnessBack()
        {
            InitializeComponent();
        }

        private void MindfulnessBack_Load(object sender, EventArgs e)
        {
            mindfulnessBack = this;

            this.Width = SystemInformation.VirtualScreen.Width;
            this.Height = SystemInformation.VirtualScreen.Height;
            this.Location = new Point(0, 0);
            this.TopLevel = true;

            new MindfulnessFront().Show(this);
        }
    }
}
