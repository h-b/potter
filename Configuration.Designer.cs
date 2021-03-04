namespace potter
{
    partial class Configuration
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration));
            this.comboBoxExecuteCommand = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxDefaultTimeInverval = new System.Windows.Forms.NumericUpDown();
            this.textBoxOptionalTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.textBoxRoundTimes = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxStartOnLogin = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDefaultTimeInverval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxOptionalTimeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxRoundTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxExecuteCommand
            // 
            this.comboBoxExecuteCommand.FormattingEnabled = true;
            this.comboBoxExecuteCommand.Items.AddRange(new object[] {
            "cmd /C echo ^| $FROM_DATE ^| $FROM_TIME ^| $TO_DATE ^| $TO_TIME ^| $CATEGORY ^| $" +
                "ACTIVITY ^| >>timesheet.md"});
            this.comboBoxExecuteCommand.Location = new System.Drawing.Point(18, 45);
            this.comboBoxExecuteCommand.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxExecuteCommand.Name = "comboBoxExecuteCommand";
            this.comboBoxExecuteCommand.Size = new System.Drawing.Size(988, 39);
            this.comboBoxExecuteCommand.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(688, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command to be executed on every change of activity:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(910, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Default time interval after which the current activity should be asked for:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 147);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "minutes";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxInfo.Location = new System.Drawing.Point(18, 501);
            this.textBoxInfo.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.Size = new System.Drawing.Size(1020, 244);
            this.textBoxInfo.TabIndex = 5;
            this.textBoxInfo.TabStop = false;
            this.textBoxInfo.Text = resources.GetString("textBoxInfo.Text");
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(844, 750);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(196, 50);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 254);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "minutes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 211);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(372, 32);
            this.label5.TabIndex = 7;
            this.label5.Text = "Optional (long) time interval:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(642, 750);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(196, 50);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // textBoxDefaultTimeInverval
            // 
            this.textBoxDefaultTimeInverval.Location = new System.Drawing.Point(18, 141);
            this.textBoxDefaultTimeInverval.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDefaultTimeInverval.Name = "textBoxDefaultTimeInverval";
            this.textBoxDefaultTimeInverval.Size = new System.Drawing.Size(142, 38);
            this.textBoxDefaultTimeInverval.TabIndex = 1;
            // 
            // textBoxOptionalTimeInterval
            // 
            this.textBoxOptionalTimeInterval.Location = new System.Drawing.Point(18, 254);
            this.textBoxOptionalTimeInterval.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxOptionalTimeInterval.Name = "textBoxOptionalTimeInterval";
            this.textBoxOptionalTimeInterval.Size = new System.Drawing.Size(142, 38);
            this.textBoxOptionalTimeInterval.TabIndex = 2;
            // 
            // textBoxRoundTimes
            // 
            this.textBoxRoundTimes.Location = new System.Drawing.Point(18, 355);
            this.textBoxRoundTimes.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxRoundTimes.Name = "textBoxRoundTimes";
            this.textBoxRoundTimes.Size = new System.Drawing.Size(120, 38);
            this.textBoxRoundTimes.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 320);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 32);
            this.label6.TabIndex = 10;
            this.label6.Text = "Round times to:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(144, 356);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 32);
            this.label7.TabIndex = 11;
            this.label7.Text = "minutes";
            // 
            // checkBoxStartOnLogin
            // 
            this.checkBoxStartOnLogin.AutoSize = true;
            this.checkBoxStartOnLogin.Location = new System.Drawing.Point(18, 432);
            this.checkBoxStartOnLogin.Margin = new System.Windows.Forms.Padding(6);
            this.checkBoxStartOnLogin.Name = "checkBoxStartOnLogin";
            this.checkBoxStartOnLogin.Size = new System.Drawing.Size(34, 33);
            this.checkBoxStartOnLogin.TabIndex = 12;
            this.checkBoxStartOnLogin.UseVisualStyleBackColor = true;
            this.checkBoxStartOnLogin.CheckedChanged += new System.EventHandler(this.checkBoxStartOnLogin_CheckedChanged);
            // 
            // Configuration
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1050, 812);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxStartOnLogin);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxRoundTimes);
            this.Controls.Add(this.textBoxOptionalTimeInterval);
            this.Controls.Add(this.textBoxDefaultTimeInverval);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxExecuteCommand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Configuration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Potter configuration";
            this.Load += new System.EventHandler(this.Configuration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDefaultTimeInverval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxOptionalTimeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxRoundTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxExecuteCommand;
        private System.Windows.Forms.NumericUpDown textBoxDefaultTimeInverval;
        private System.Windows.Forms.NumericUpDown textBoxOptionalTimeInterval;
        private System.Windows.Forms.NumericUpDown textBoxRoundTimes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxStartOnLogin;
    }
}

