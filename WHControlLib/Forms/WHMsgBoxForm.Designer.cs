namespace WHControlLib.Forms
{
    partial class WhMsgBoxForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.whTextBox1 = new WHControlLib.WHTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkOrange;
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(-1, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(388, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "信息提示";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // whTextBox1
            // 
            this.whTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.whTextBox1.BorderColor = System.Drawing.Color.White;
            this.whTextBox1.FillBackgroundColor = System.Drawing.Color.RoyalBlue;
            this.whTextBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.whTextBox1.IsAllowMultieLine = true;
            this.whTextBox1.IsDisplayWaterMark = false;
            this.whTextBox1.Location = new System.Drawing.Point(27, 66);
            this.whTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.whTextBox1.MyFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.whTextBox1.MyFontColor = System.Drawing.SystemColors.Window;
            this.whTextBox1.MyShape = WHControlLib.WHTextBox.Shapes.RoundRectange;
            this.whTextBox1.MyText = "";
            this.whTextBox1.Name = "whTextBox1";
            this.whTextBox1.Radius = 3F;
            this.whTextBox1.Size = new System.Drawing.Size(344, 116);
            this.whTextBox1.TabIndex = 4;
            this.whTextBox1.WaterMarkColor = System.Drawing.Color.LightGray;
            this.whTextBox1.WatermarkDirection = WHControlLib.WHTextBox.WaterMarkDirection.left;
            this.whTextBox1.WaterMarkText = "";
            // 
            // WhMsgBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(384, 228);
            this.Controls.Add(this.whTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.firstColor = System.Drawing.Color.Indigo;
            this.IsUseTwoColor = true;
            this.Name = "WhMsgBoxForm";
            this.Radius = 9F;
            this.SecondColor = System.Drawing.Color.RoyalBlue;
            this.Text = "WhMsgBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private WHTextBox whTextBox1;
    }
}