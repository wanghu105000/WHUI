namespace WHControlLib.Forms
{
    partial class MsgBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OKbutton = new WHControlLib.Controls.WHButton();
            this.MsgTextLable = new WHControlLib.Controls.WHLableEx();
            this.SuspendLayout();
            // 
            // OKbutton
            // 
            this.OKbutton.BackColor = System.Drawing.Color.Transparent;
            this.OKbutton.BorderWidth = 2;
            this.OKbutton.BornColor = System.Drawing.Color.Blue;
            this.OKbutton.ColorChangeint = 50;
            this.OKbutton.FirstFillcolor = System.Drawing.Color.Orange;
            this.OKbutton.FontColor = System.Drawing.Color.Black;
            this.OKbutton.ImageOffSet = new System.Drawing.Size(30, 0);
            this.OKbutton.ImageTranparentColor = System.Drawing.Color.White;
            this.OKbutton.IsAutoSize = false;
            this.OKbutton.IsDrawBoin = false;
            this.OKbutton.IsOpenimageTranparentColor = false;
            this.OKbutton.IsShowFouceLine = true;
            this.OKbutton.IsShowMark = false;
            this.OKbutton.IsShowMarkBorder = false;
            this.OKbutton.IsShowMyImage = false;
            this.OKbutton.IsShowText = true;
            this.OKbutton.IsUseTwoColor = false;
            this.OKbutton.Location = new System.Drawing.Point(125, 199);
            this.OKbutton.MarkBackColor = System.Drawing.Color.Red;
            this.OKbutton.MarkBorderColor = System.Drawing.Color.White;
            this.OKbutton.MarkBorderWidth = 1;
            this.OKbutton.MarkerTextColor = System.Drawing.Color.White;
            this.OKbutton.MarkerTextSzie = 10;
            this.OKbutton.MarkHeight = 2F;
            this.OKbutton.MarkText = "";
            this.OKbutton.MarkTextColor = System.Drawing.Color.White;
            this.OKbutton.MyFillColorDec = WHControlLib.baseStaticCtrl.FillColorDec.Vertical;
            this.OKbutton.MyFont = new System.Drawing.Font("微软雅黑", 12F);
            this.OKbutton.MyImage = null;
            this.OKbutton.MyImageDec = WHControlLib.baseStaticCtrl.ImageDec.left;
            this.OKbutton.MyimageHeight = 0.5F;
            this.OKbutton.MyImageOnMouse = null;
            this.OKbutton.MyImageUnEnable = null;
            this.OKbutton.MyShape = WHControlLib.baseStaticCtrl.Shape.RoundRectange;
            this.OKbutton.MyTextAlign = WHControlLib.baseStaticCtrl.TextAlign.CenterMiddle;
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.OnMouseColor = System.Drawing.Color.BurlyWood;
            this.OKbutton.Radius = 5F;
            this.OKbutton.SecondFillcolor = System.Drawing.Color.Orange;
            this.OKbutton.Size = new System.Drawing.Size(147, 46);
            this.OKbutton.TabIndex = 0;
            this.OKbutton.Text = "确定";
            this.OKbutton.UnEnableColor = System.Drawing.Color.Gray;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // MsgTextLable
            // 
            this.MsgTextLable.BackColor = System.Drawing.Color.Transparent;
            this.MsgTextLable.BorderWidth = 2;
            this.MsgTextLable.BornColor = System.Drawing.Color.Beige;
            this.MsgTextLable.ColorChangeint = 50;
            this.MsgTextLable.FirstFillcolor = System.Drawing.Color.Transparent;
            this.MsgTextLable.FontColor = System.Drawing.Color.Black;
            this.MsgTextLable.ImageOffSet = new System.Drawing.Size(0, 0);
            this.MsgTextLable.ImageTranparentColor = System.Drawing.Color.White;
            this.MsgTextLable.IsAutoSize = false;
            this.MsgTextLable.IsDrawBoin = true;
            this.MsgTextLable.IsOpenimageTranparentColor = false;
            this.MsgTextLable.IsShowFouceLine = false;
            this.MsgTextLable.IsShowMark = false;
            this.MsgTextLable.IsShowMarkBorder = false;
            this.MsgTextLable.IsShowMyImage = false;
            this.MsgTextLable.IsShowText = true;
            this.MsgTextLable.IsUseTwoColor = false;
            this.MsgTextLable.Location = new System.Drawing.Point(45, 76);
            this.MsgTextLable.MarkBackColor = System.Drawing.Color.Red;
            this.MsgTextLable.MarkBorderColor = System.Drawing.Color.White;
            this.MsgTextLable.MarkBorderWidth = 1;
            this.MsgTextLable.MarkerTextColor = System.Drawing.Color.White;
            this.MsgTextLable.MarkerTextSzie = 10;
            this.MsgTextLable.MarkHeight = 2F;
            this.MsgTextLable.MarkText = "";
            this.MsgTextLable.MarkTextColor = System.Drawing.Color.White;
            this.MsgTextLable.MyFillColorDec = WHControlLib.baseStaticCtrl.FillColorDec.Vertical;
            this.MsgTextLable.MyFont = new System.Drawing.Font("微软雅黑", 12F);
            this.MsgTextLable.MyImage = null;
            this.MsgTextLable.MyImageDec = WHControlLib.baseStaticCtrl.ImageDec.left;
            this.MsgTextLable.MyimageHeight = 0.5F;
            this.MsgTextLable.MyImageOnMouse = null;
            this.MsgTextLable.MyImageUnEnable = null;
            this.MsgTextLable.MyShape = WHControlLib.baseStaticCtrl.Shape.RoundRectange;
            this.MsgTextLable.MyTextAlign = WHControlLib.baseStaticCtrl.TextAlign.CenterMiddle;
            this.MsgTextLable.Name = "MsgTextLable";
            this.MsgTextLable.OnMouseColor = System.Drawing.Color.BurlyWood;
            this.MsgTextLable.Radius = 5F;
            this.MsgTextLable.SecondFillcolor = System.Drawing.Color.Orange;
            this.MsgTextLable.Size = new System.Drawing.Size(327, 79);
            this.MsgTextLable.TabIndex = 1;
            this.MsgTextLable.Text = null;
            this.MsgTextLable.UnEnableColor = System.Drawing.Color.Gray;
            // 
            // MsgBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.BoxJG = 8;
            this.ClientSize = new System.Drawing.Size(426, 257);
            this.CloseBoxBorderColor = System.Drawing.Color.White;
            this.CloseBoxShapeColor = System.Drawing.Color.FloralWhite;
            this.Controls.Add(this.MsgTextLable);
            this.Controls.Add(this.OKbutton);
            this.FormBorderColor = System.Drawing.Color.LightGray;
            this.FormBorderWidth = 4F;
            this.IsUseTwoColor = true;
            this.MyCloseBoxShape = WHControlLib.Forms.BaseDialogFormcs.CloseBoxShape.Circle;
            this.MyDialogHeightBl = 4F;
            this.Name = "MsgBoxForm";
            this.Radius = 9F;
            this.Text = "MsgBoxForm";
            this.TitleBackColor = System.Drawing.Color.Transparent;
            this.TitleHeight = 6;
            this.TitleTextFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MsgBoxForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MsgBoxForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WHButton OKbutton;
        private Controls.WHLableEx MsgTextLable;
    }
}