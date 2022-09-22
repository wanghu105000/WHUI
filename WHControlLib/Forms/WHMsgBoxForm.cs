using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHControlLib.Forms
{
    public partial class WhMsgBoxForm : BaseDialogFormcs
    {
        public WhMsgBoxForm()
        {
            InitializeComponent();

          
        }

        private void whButtonPro1_Load(object sender, EventArgs e)
        {
          

        }



        private void whButtonPro1_Click(object sender, EventArgs e)
        {
            whButtonPro1.Text = "你好";
            MessageBox.Show(whButtonPro1.Text);
        }
    }
}
