using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHUIdemo.Forms
{
    public partial class CtrlTestFrm : Form
    {
        public CtrlTestFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(whRadioButton1.Checked.ToString() + ",2" + whRadioButton2.Checked.ToString());
        }
    }
}
