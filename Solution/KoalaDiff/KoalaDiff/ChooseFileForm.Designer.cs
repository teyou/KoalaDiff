namespace KoalaDiff
{
    partial class ChooseFileForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.firstPathTextBox = new System.Windows.Forms.TextBox();
            this.secondPathTextBox = new System.Windows.Forms.TextBox();
            this.firstPathBrowseButton = new System.Windows.Forms.Button();
            this.secondPathBrowseButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "1st:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "2nd:";
            // 
            // firstPathTextBox
            // 
            this.firstPathTextBox.Location = new System.Drawing.Point(43, 37);
            this.firstPathTextBox.Name = "firstPathTextBox";
            this.firstPathTextBox.Size = new System.Drawing.Size(332, 22);
            this.firstPathTextBox.TabIndex = 3;
            // 
            // secondPathTextBox
            // 
            this.secondPathTextBox.Location = new System.Drawing.Point(43, 73);
            this.secondPathTextBox.Name = "secondPathTextBox";
            this.secondPathTextBox.Size = new System.Drawing.Size(332, 22);
            this.secondPathTextBox.TabIndex = 4;
            // 
            // firstPathBrowseButton
            // 
            this.firstPathBrowseButton.Location = new System.Drawing.Point(381, 36);
            this.firstPathBrowseButton.Name = "firstPathBrowseButton";
            this.firstPathBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.firstPathBrowseButton.TabIndex = 5;
            this.firstPathBrowseButton.Text = "&Browse...";
            this.firstPathBrowseButton.UseVisualStyleBackColor = true;
            // 
            // secondPathBrowseButton
            // 
            this.secondPathBrowseButton.Location = new System.Drawing.Point(381, 72);
            this.secondPathBrowseButton.Name = "secondPathBrowseButton";
            this.secondPathBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.secondPathBrowseButton.TabIndex = 6;
            this.secondPathBrowseButton.Text = "Bro&wse...";
            this.secondPathBrowseButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(327, 146);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 27);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(414, 146);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 27);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.firstPathTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.secondPathBrowseButton);
            this.groupBox1.Controls.Add(this.secondPathTextBox);
            this.groupBox1.Controls.Add(this.firstPathBrowseButton);
            this.groupBox1.Location = new System.Drawing.Point(17, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 117);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose files to diff:";
            // 
            // ChooseFileForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(505, 186);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ChooseFileForm";
            this.Text = "Choose Files";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstPathTextBox;
        private System.Windows.Forms.TextBox secondPathTextBox;
        private System.Windows.Forms.Button firstPathBrowseButton;
        private System.Windows.Forms.Button secondPathBrowseButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}