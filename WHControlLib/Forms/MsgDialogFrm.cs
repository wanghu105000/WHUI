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
    public partial class MsgDialogFrm  : BaseDialogFormcs
    {
        public MsgDialogFrm()
        {
            InitializeComponent();
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            Mydialogresult = DialogResult.Yes;
            WHMsgDialog.MyDialogResult = Mydialogresult;
            this.Close();
        }
  


        private void MsgDialogFrm_Load(object sender, EventArgs e)
        {
            MsgTextLable.Text = MessageText;
        }

        private void MsgDialogFrm_Paint(object sender, PaintEventArgs e)
        {
            float borderwidth = 0;

            if (IsDrawFormBorder)
            {
                borderwidth = FormBorderWidth;

            }

            MsgTextLable.Left = (int)this.DrawRct.X + (int)borderwidth + 5;
            MsgTextLable.Width = (int)this.DrawRct.Width - (int)borderwidth * 2 - 10;
            MsgTextLable.Height = (int)((this.DrawRct.Height - this.TitleRect.Height) / 10 * 6.5);
            MsgTextLable.Top = (int)(TitleRect.Y + TitleRect.Height + TitleBorderWidth);

            OKbutton.Height = (int)((DrawRct.Height - TitleRect.Height - MsgTextLable.Height - borderwidth) / 3 * 2);
            OKbutton.Top = (int)(TitleRect.Y + TitleRect.Height + MsgTextLable.Height + 10);
            Cannelbutton.Height = OKbutton.Height;
            Cannelbutton.Top = OKbutton.Top;


            OKbutton.Width = (int)((DrawRct.Width - borderwidth * 2) / 2-10);
            OKbutton.Left = (int)(DrawRct.X + borderwidth+5 );
            Cannelbutton.Width = OKbutton.Width;
            Cannelbutton.Left=(int)(DrawRct.X+DrawRct.Width-Cannelbutton.Width-10);


        }

        private void Cannelbutton_Click(object sender, EventArgs e)
        {
            //Mydialogresult = DialogResult.OK;
            WHMsgDialog.MyDialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
