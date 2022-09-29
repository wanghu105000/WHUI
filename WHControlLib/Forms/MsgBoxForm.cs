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
    public partial class MsgBoxForm : BaseDialogFormcs
    {
        public MsgBoxForm()
        {
            InitializeComponent();
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
       
        //}
        private void MsgBoxForm_Load(object sender, EventArgs e)
        {
         


            MsgTextLable.Text = MessageText;
        }

        private void MsgBoxForm_Paint(object sender, PaintEventArgs e)
        {

          
            if (IsDrawFormBorder)
            {
                MsgTextLable.Left = (int)this.DrawRct.X + (int)FormBorderWidth + 5;
                MsgTextLable.Width = (int)this.DrawRct.Width - (int)FormBorderWidth*2 - 10;
         MsgTextLable.Height = (int)((this.DrawRct.Height - this.TitleRect.Height)/10*6.5) ;

            OKbutton.Height = (int)(DrawRct.Height-TitleRect.Height-MsgTextLable.Height-FormBorderWidth-15);
          OKbutton.Top= (int)(TitleRect.X+TitleRect.Height+MsgTextLable.Height+5);
                
                   OKbutton.Width = (int)((DrawRct.Width -FormBorderWidth*2)/ 3);
                OKbutton.Left= (int)(DrawRct.X+FormBorderWidth+DrawRct.Width/2-OKbutton.Width/2);
     
            }
            else
            {
                MsgTextLable.Left = (int)this.DrawRct.X  + 5;
                MsgTextLable.Width = (int)this.DrawRct.Width - 10;


            }

         //MsgTextLable.Top = this.TitleRect.Y + TitleRect.Height ;
         
         




        }
    }
}
