using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KoalaDiff.Library;

namespace KoalaDiff
{
    public partial class ChooseFileForm : Form
    {
        public Main MainForm { get; set; }
        private string _firstPath;
        private string _secondPath;
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ChooseFileForm()
        {
            InitializeComponent();

            Init();

            //delegates
            firstPathBrowseButton.Click += firstPathBrowseButton_Click;
            secondPathBrowseButton.Click += secondPathBrowseButton_Click;
            okButton.Click += okButton_Click;
            cancelButton.Click += cancelButton_Click;
        }

        private void Init()
        {
            if (SettingsHelper.FirstPath != String.Empty)
                firstPathTextBox.Text = _firstPath = SettingsHelper.FirstPath;

            if (SettingsHelper.SecondPath != String.Empty)
                secondPathTextBox.Text = _secondPath = SettingsHelper.SecondPath;
        }

        private void firstPathBrowseButton_Click(object sender, EventArgs e)
        {
            this.BrowsePath(1);
        }

        private void secondPathBrowseButton_Click(object sender, EventArgs e)
        {
            this.BrowsePath(2);
        }

        /// <summary>
        /// Browsing Path
        /// </summary>
        /// <param name="index">1:first  or 2: second</param>
        private void BrowsePath(int index)
        {
            using (FileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "All Supported Images (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
                dialog.DefaultExt = "jpg";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string filePath = dialog.FileName;
                        _logger.Trace("[{0}] filepath:{1}", index, filePath);
                        switch (index)
                        {
                            case 1:
                                this._firstPath = firstPathTextBox.Text = filePath;
                                break;
                            case 2:
                            default:
                                this._secondPath = secondPathTextBox.Text = filePath;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        void okButton_Click(object sender, EventArgs e)
        {
            //check if the file existed
            if (!File.Exists(this._firstPath))
            {
                MessageBox.Show("the file on the first path doesn't exist, please choose another image file", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(this._secondPath))
            {
                MessageBox.Show("the file on the second path doesn't exist, please choose another image file", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //save the parameter to settings
            SettingsHelper.FirstPath = this._firstPath;
            SettingsHelper.SecondPath = this._secondPath;
            SettingsHelper.LeftDescription = "";
            SettingsHelper.RightDescription = "";

            //notify main form to render
            MainForm.InitRender();

            //close the form
            cancelButton.PerformClick();
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
