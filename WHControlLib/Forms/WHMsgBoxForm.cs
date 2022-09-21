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

        private void whButtonPro1_MouseClick(object sender, MouseEventArgs e)
        {
            whTextBox1.MyText = "你好";

        }
    }
}
