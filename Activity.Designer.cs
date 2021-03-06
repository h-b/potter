﻿namespace potter
{
    partial class Activity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Activity));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxActivity = new System.Windows.Forms.ComboBox();
            this.buttonAskDefault = new System.Windows.Forms.Button();
            this.buttonAskOptional = new System.Windows.Forms.Button();
            this.buttonConfiguration = new System.Windows.Forms.Button();
            this.buttonRemoveActivity = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.buttonRemoveCategory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your current activity:";
            // 
            // comboBoxActivity
            // 
            this.comboBoxActivity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxActivity.FormattingEnabled = true;
            this.comboBoxActivity.Location = new System.Drawing.Point(292, 46);
            this.comboBoxActivity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxActivity.Name = "comboBoxActivity";
            this.comboBoxActivity.Size = new System.Drawing.Size(600, 39);
            this.comboBoxActivity.TabIndex = 0;
            this.comboBoxActivity.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxActivity_Validating);
            // 
            // buttonAskDefault
            // 
            this.buttonAskDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAskDefault.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAskDefault.Location = new System.Drawing.Point(578, 368);
            this.buttonAskDefault.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAskDefault.Name = "buttonAskDefault";
            this.buttonAskDefault.Size = new System.Drawing.Size(540, 56);
            this.buttonAskDefault.TabIndex = 6;
            this.buttonAskDefault.UseVisualStyleBackColor = true;
            // 
            // buttonAskOptional
            // 
            this.buttonAskOptional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAskOptional.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAskOptional.Location = new System.Drawing.Point(18, 368);
            this.buttonAskOptional.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAskOptional.Name = "buttonAskOptional";
            this.buttonAskOptional.Size = new System.Drawing.Size(540, 56);
            this.buttonAskOptional.TabIndex = 5;
            this.buttonAskOptional.UseVisualStyleBackColor = true;
            // 
            // buttonConfiguration
            // 
            this.buttonConfiguration.Location = new System.Drawing.Point(12, 213);
            this.buttonConfiguration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonConfiguration.Name = "buttonConfiguration";
            this.buttonConfiguration.Size = new System.Drawing.Size(288, 52);
            this.buttonConfiguration.TabIndex = 4;
            this.buttonConfiguration.Text = "Settings...";
            this.buttonConfiguration.UseVisualStyleBackColor = true;
            this.buttonConfiguration.Click += new System.EventHandler(this.ButtonConfiguration_Click);
            // 
            // buttonRemoveActivity
            // 
            this.buttonRemoveActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveActivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemoveActivity.Location = new System.Drawing.Point(900, 43);
            this.buttonRemoveActivity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRemoveActivity.Name = "buttonRemoveActivity";
            this.buttonRemoveActivity.Size = new System.Drawing.Size(220, 50);
            this.buttonRemoveActivity.TabIndex = 1;
            this.buttonRemoveActivity.Text = "Remove";
            this.buttonRemoveActivity.UseVisualStyleBackColor = true;
            this.buttonRemoveActivity.Click += new System.EventHandler(this.buttonRemoveActivity_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Category:";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(292, 108);
            this.comboBoxCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(600, 39);
            this.comboBoxCategory.TabIndex = 2;
            this.comboBoxCategory.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxCategory_Validating);
            // 
            // buttonRemoveCategory
            // 
            this.buttonRemoveCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemoveCategory.Location = new System.Drawing.Point(900, 103);
            this.buttonRemoveCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRemoveCategory.Name = "buttonRemoveCategory";
            this.buttonRemoveCategory.Size = new System.Drawing.Size(220, 50);
            this.buttonRemoveCategory.TabIndex = 3;
            this.buttonRemoveCategory.Text = "Remove";
            this.buttonRemoveCategory.UseVisualStyleBackColor = true;
            this.buttonRemoveCategory.Click += new System.EventHandler(this.buttonRemoveCategory_Click);
            // 
            // Activity
            // 
            this.AcceptButton = this.buttonAskDefault;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonAskDefault;
            this.ClientSize = new System.Drawing.Size(1130, 457);
            this.ControlBox = false;
            this.Controls.Add(this.buttonRemoveCategory);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonRemoveActivity);
            this.Controls.Add(this.buttonConfiguration);
            this.Controls.Add(this.buttonAskOptional);
            this.Controls.Add(this.buttonAskDefault);
            this.Controls.Add(this.comboBoxActivity);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Activity";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time Tracker";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Activity_FormClosing);
            this.Load += new System.EventHandler(this.Activity_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxActivity;
        private System.Windows.Forms.Button buttonAskDefault;
        private System.Windows.Forms.Button buttonAskOptional;
        private System.Windows.Forms.Button buttonConfiguration;
        private System.Windows.Forms.Button buttonRemoveActivity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Button buttonRemoveCategory;
    }
}