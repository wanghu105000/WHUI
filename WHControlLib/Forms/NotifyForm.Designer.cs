namespace WHControlLib.Forms
{
    partial class NotifyForm
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.MsgTxtLable = new WHControlLib.Controls.WHLableEx();
            this.timeLable = new System.Windows.Forms.Label();
            this.nowtime = new System.Windows.Forms.Timer(this.components);
            this.titlepicturebox = new System.Windows.Forms.PictureBox();
            this.whButton1 = new WHControlLib.Controls.WHButton();
            ((System.ComponentModel.ISupportInitialize)(this.titlepicturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
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
            this.MsgTxtLable.Location = new System.Drawing.Point(31, 56);
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
            this.MsgTxtLable.TabIndex = 0;
            this.MsgTxtLable.Text = "whLableEx1";
            this.MsgTxtLable.UnEnableColor = System.Drawing.Color.Gray;
            // 
            // timeLable
            // 
            this.timeLable.BackColor = System.Drawing.Color.Transparent;
            this.timeLable.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.timeLable.Location = new System.Drawing.Point(48, 96);
            this.timeLable.Name = "timeLable";
            this.timeLable.Size = new System.Drawing.Size(220, 24);
            this.timeLable.TabIndex = 1;
            this.timeLable.Text = "label1";
            this.timeLable.Click += new System.EventHandler(this.timeLable_Click);
            // 
            // nowtime
            // 
            this.nowtime.Interval = 1000;
            this.nowtime.Tick += new System.EventHandler(this.nowtime_Tick);
            // 
            // titlepicturebox
            // 
            this.titlepicturebox.BackColor = System.Drawing.Color.Transparent;
            this.titlepicturebox.Location = new System.Drawing.Point(198, 112);
            this.titlepicturebox.Name = "titlepicturebox";
            this.titlepicturebox.Size = new System.Drawing.Size(152, 24);
            this.titlepicturebox.TabIndex = 3;
            this.titlepicturebox.TabStop = false;
            this.titlepicturebox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlepicturebox_MouseDown);
            this.titlepicturebox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlepicturebox_MouseMove);
            // 
            // whButton1
            // 
            this.whButton1.BackColor = System.Drawing.Color.Transparent;
            this.whButton1.BorderWidth = 2;
            this.whButton1.BornColor = System.Drawing.Color.Blue;
            this.whButton1.ColorChangeint = 50;
            this.whButton1.FirstFillcolor = System.Drawing.Color.Orange;
            this.whButton1.FontColor = System.Drawing.Color.Black;
            this.whButton1.ImageOffSet = new System.Drawing.Size(0, 0);
            this.whButton1.ImageTranparentColor = System.Drawing.Color.White;
            this.whButton1.IsAutoSize = false;
            this.whButton1.IsDrawBoin = false;
            this.whButton1.IsOpenimageTranparentColor = false;
            this.whButton1.IsShowFouceLine = true;
            this.whButton1.IsShowMark = false;
            this.whButton1.IsShowMarkBorder = false;
            this.whButton1.IsShowMyImage = false;
            this.whButton1.IsShowText = true;
            this.whButton1.IsUseTwoColor = false;
            this.whButton1.Location = new System.Drawing.Point(310, 47);
            this.whButton1.MarkBackColor = System.Drawing.Color.Red;
            this.whButton1.MarkBorderColor = System.Drawing.Color.White;
            this.whButton1.MarkBorderWidth = 1;
            this.whButton1.MarkerTextColor = System.Drawing.Color.White;
            this.whButton1.MarkerTextSzie = 10;
            this.whButton1.MarkHeight = 2F;
            this.whButton1.MarkText = "";
            this.whButton1.MarkTextColor = System.Drawing.Color.White;
            this.whButton1.MyFillColorDec = WHControlLib.baseStaticCtrl.FillColorDec.Vertical;
            this.whButton1.MyFont = new System.Drawing.Font("微软雅黑", 12F);
            this.whButton1.MyImage = null;
            this.whButton1.MyImageDec = WHControlLib.baseStaticCtrl.ImageDec.left;
            this.whButton1.MyimageHeight = 0.5F;
            this.whButton1.MyImageOnMouse = null;
            this.whButton1.MyImageUnEnable = null;
            this.whButton1.MyShape = WHControlLib.baseStaticCtrl.Shape.RoundRectange;
            this.whButton1.MyTextAlign = WHControlLib.baseStaticCtrl.TextAlign.CenterMiddle;
            this.whButton1.Name = "whButton1";
            this.whButton1.OnMouseColor = System.Drawing.Color.BurlyWood;
            this.whButton1.Radius = 5F;
            this.whButton1.SecondFillcolor = System.Drawing.Color.Orange;
            this.whButton1.Size = new System.Drawing.Size(44, 24);
            this.whButton1.TabIndex = 4;
            this.whButton1.Text = "whButton1";
            this.whButton1.UnEnableColor = System.Drawing.Color.Gray;
            this.whButton1.Click += new System.EventHandler(this.whButton1_Click);
            // 
            // NotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BoxJG = 12;
            this.ClientSize = new System.Drawing.Size(384, 171);
            this.CloseBoxSize = 1F;
            this.Controls.Add(this.whButton1);
            this.Controls.Add(this.titlepicturebox);
            this.Controls.Add(this.timeLable);
            this.Controls.Add(this.MsgTxtLable);
            this.firstColor = System.Drawing.Color.DodgerBlue;
            this.IsShowCloseBoxBorder = false;
            this.IsShowTitleBorder = false;
            this.IsUseTwoColor = true;
            this.MyCloseBoxShape = WHControlLib.Forms.BaseDialogFormcs.CloseBoxShape.Circle;
            this.MyDialogHeightBl = 6F;
            this.MyDialogWidthBl = 5F;
            this.Name = "NotifyForm";
            this.Radius = 12F;
            this.Text = "NotifyForm";
            this.TitleBackColor = System.Drawing.Color.Transparent;
            this.TitleBorderColor = System.Drawing.Color.LightGray;
            this.TitleHeight = 6;
            this.TitleTextColor = System.Drawing.Color.White;
            this.TitleTextFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Load += new System.EventHandler(this.NotifyForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NotifyForm_MouseDown);
            this.MouseLeave += new System.EventHandler(this.NotifyForm_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.titlepicturebox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer nowtime;
        public Controls.WHLableEx MsgTxtLable;
        public System.Windows.Forms.Label timeLable;
        private System.Windows.Forms.PictureBox titlepicturebox;
        private Controls.WHButton whButton1;
    }
}