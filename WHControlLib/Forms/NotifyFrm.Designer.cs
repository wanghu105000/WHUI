namespace WHControlLib.Forms
{
    partial class NotifyFrm
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
            this.components = new System.ComponentModel.Container();
            this.timeLable = new System.Windows.Forms.Label();
            this.MsgTxtLable = new WHControlLib.Controls.WHLableEx();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.nowtime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timeLable
            // 
            this.timeLable.BackColor = System.Drawing.Color.Transparent;
            this.timeLable.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.timeLable.Location = new System.Drawing.Point(72, 132);
            this.timeLable.Name = "timeLable";
            this.timeLable.Size = new System.Drawing.Size(220, 24);
            this.timeLable.TabIndex = 3;
            this.timeLable.Text = "label1";
            // 
            // MsgTxtLable
            // 
            this.MsgTxtLable.BackColor = System.Drawing.Color.Transparent;
            this.MsgTxtLable.BorderWidth = 2;
            this.MsgTxtLable.BornColor = System.Drawing.Color.Blue;
            this.MsgTxtLable.ColorChangeint = 50;
            this.MsgTxtLable.FirstFillcolor = System.Drawing.Color.Transparent;
            this.MsgTxtLable.FontColor = System.Drawing.Color.WhiteSmoke;
            this.MsgTxtLable.ImageOffSet = new System.Drawing.Size(0, 0);
            this.MsgTxtLable.ImageTranparentColor = System.Drawing.Color.White;
            this.MsgTxtLable.IsAutoSize = false;
            this.MsgTxtLable.IsDrawBoin = false;
            this.MsgTxtLable.IsOpenimageTranparentColor = false;
            this.MsgTxtLable.IsShowFouceLine = false;
            this.MsgTxtLable.IsShowMark = false;
            this.MsgTxtLable.IsShowMarkBorder = false;
            this.MsgTxtLable.IsShowMyImage = false;
            this.MsgTxtLable.IsShowText = true;
            this.MsgTxtLable.IsUseTwoColor = false;
            this.MsgTxtLable.Location = new System.Drawing.Point(65, 92);
            this.MsgTxtLable.MarkBackColor = System.Drawing.Color.Red;
            this.MsgTxtLable.MarkBorderColor = System.Drawing.Color.White;
            this.MsgTxtLable.MarkBorderWidth = 1;
            this.MsgTxtLable.MarkerTextColor = System.Drawing.Color.White;
            this.MsgTxtLable.MarkerTextSzie = 10;
            this.MsgTxtLable.MarkHeight = 2F;
            this.MsgTxtLable.MarkText = "";
            this.MsgTxtLable.MarkTextColor = System.Drawing.Color.White;
            this.MsgTxtLable.MyFillColorDec = WHControlLib.baseStaticCtrl.FillColorDec.Vertical;
            this.MsgTxtLable.MyFont = new System.Drawing.Font("微软雅黑", 12F);
            this.MsgTxtLable.MyImage = null;
            this.MsgTxtLable.MyImageDec = WHControlLib.baseStaticCtrl.ImageDec.left;
            this.MsgTxtLable.MyimageHeight = 0.5F;
            this.MsgTxtLable.MyImageOnMouse = null;
            this.MsgTxtLable.MyImageUnEnable = null;
            this.MsgTxtLable.MyShape = WHControlLib.baseStaticCtrl.Shape.RoundRectange;
            this.MsgTxtLable.MyTextAlign = WHControlLib.baseStaticCtrl.TextAlign.CenterMiddle;
            this.MsgTxtLable.Name = "MsgTxtLable";
            this.MsgTxtLable.OnMouseColor = System.Drawing.Color.BurlyWood;
            this.MsgTxtLable.Radius = 5F;
            this.MsgTxtLable.SecondFillcolor = System.Drawing.Color.Orange;
            this.MsgTxtLable.Size = new System.Drawing.Size(165, 37);
            this.MsgTxtLable.TabIndex = 2;
            this.MsgTxtLable.Text = "whLableEx1";
            this.MsgTxtLable.UnEnableColor = System.Drawing.Color.Gray;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // nowtime
            // 
            this.nowtime.Interval = 1000;
            // 
            // NotifyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 228);
            this.Controls.Add(this.timeLable);
            this.Controls.Add(this.MsgTxtLable);
            this.Name = "NotifyFrm";
            this.Text = "NotifyFrm";
            this.Load += new System.EventHandler(this.NotifyFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label timeLable;
        public Controls.WHLableEx MsgTxtLable;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer nowtime;
    }
}