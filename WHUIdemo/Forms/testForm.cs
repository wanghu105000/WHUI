using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHControlLib.Forms;

namespace WHUIdemo.Forms
{
    public partial class testForm : WHForm
    {
        public testForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OK");
        }

        private void testForm_Load(object sender, EventArgs e)
        {

        }
    }
}
