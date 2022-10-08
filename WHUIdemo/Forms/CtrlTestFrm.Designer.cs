namespace WHUIdemo.Forms
{
    partial class CtrlTestFrm
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
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.whRadioButton1 = new WHControlLib.Controls.WHRadioButton();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.Location = new System.Drawing.Point(53, 31);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(136, 93);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(53, 122);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(136, 93);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // whRadioButton1
            // 
            this.whRadioButton1.BackColor = System.Drawing.Color.Transparent;
            this.whRadioButton1.Checked = true;
            this.whRadioButton1.InShapeBl = 0.7F;
            this.whRadioButton1.InShapeColor = System.Drawing.Color.Orange;
            this.whRadioButton1.IsShowShapeBorder = true;
            this.whRadioButton1.Location = new System.Drawing.Point(424, 50);
            this.whRadioButton1.MyShape = WHControlLib.Controls.WHRadioButton.Shape.Circle;
            this.whRadioButton1.Name = "whRadioButton1";
            this.whRadioButton1.ShapeBl = 0.7F;
            this.whRadioButton1.ShapeBorderColor = System.Drawing.Color.Orange;
            this.whRadioButton1.ShapeBorderWidth = 1F;
            this.whRadioButton1.ShapeFillColor = System.Drawing.Color.White;
            this.whRadioButton1.Size = new System.Drawing.Size(112, 72);
            this.whRadioButton1.TabIndex = 6;
            this.whRadioButton1.UnEnableColor = System.Drawing.Color.Gray;
            // 
            // CtrlTestFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.whRadioButton1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Name = "CtrlTestFrm";
            this.Text = "CtrlTestFrm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private WHControlLib.Controls.WHRadioButton whRadioButton1;
    }
}