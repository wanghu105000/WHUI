﻿using System;
using System.Windows.Forms;

namespace WHControlLib.Forms
{
    public partial class WhMsgBoxForm : BaseDialogFormcs
    {
        public WhMsgBoxForm()
        {
            InitializeComponent();


        }

        private void WhMsgBoxForm_Load(object sender, EventArgs e)
        {
            whButton1.Focus();
        }

        private void whButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(whButton1.Text);
        }
    }
}
