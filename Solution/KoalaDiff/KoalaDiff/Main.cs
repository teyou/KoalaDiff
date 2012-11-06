using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using KoalaDiff.Library;
using KoalaDiff.Library.Command;

namespace KoalaDiff
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
            HardCodeInitComponent();

            //set title
            var productInfo = Assembly.GetExecutingAssembly().GetName();
            this.Text = string.Format("{0} v.{1}", productInfo.Name, productInfo.Version.ToString());

            //Menu --> File delegates (event handlers)
            fileNewDiffToolStripMenuItem.Click += fileNewDiffToolStripMenuItem_Click;
            fileExitToolStripMenuItem.Click += fileExitToolStripMenuItem_Click;

            //Menu --> Settings delegates
            highlight25OpacityToolStripMenuItem.Click += highlight25OpacityToolStripMenuItem_Click;
            highlight50OpacityToolStripMenuItem.Click += highlight50OpacityToolStripMenuItem_Click;
            highlight75OpacityToolStripMenuItem.Click += highlight75OpacityToolStripMenuItem_Click;
            highlight100OpacityToolStripMenuItem.Click += highlight100OpacityToolStripMenuItem_Click;
            cacheEnableToolStripMenuItem.Click += cacheEnableToolStripMenuItem_Click;
            cacheDisableToolStripMenuItem.Click += cacheDisableToolStripMenuItem_Click;

            //highlight opacity delegates
            highlightOpacityToolStripComboBox.SelectedIndexChanged += highlightOpacityToolStripComboBox_SelectedIndexChanged;

            //Menu --> Help delegates
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;

            //layout delegates
            sideBySideLayoutToolStripMenuItem.Click += sideBySideLayoutToolStripMenuItem_Click;
            sideBySideLayoutToolStripButton.Click += sideBySideLayoutToolStripButton_Click;
            overlayLayoutToolStripMenuItem.Click += overlayLayoutToolStripMenuItem_Click;
            overlayLayoutToolStripButton.Click += overlayLayoutToolStripButton_Click;
            overlayFlickeringLayoutToolStripMenuItem.Click += overlayFlickeringLayoutToolStripMenuItem_Click;
            overlayFlickeringLayoutToolStripButton.Click += overlayFlickeringLayoutToolStripButton_Click;

            //image display delegates
            actualImageSizeToolStripMenuItem.Click += actualImageSizeToolStripMenuItem_Click;
            actualImageSizeToolStripButton.Click += actualImageSizeToolStripButton_Click;
            fitImageToWindowToolStripMenuItem.Click += fitImageToWindowToolStripMenuItem_Click;
            fitImageToWindowToolStripButton.Click += fitImageToWindowToolStripButton_Click;
            this.SizeChanged += Main_SizeChanged;

            //zoom delegates
            zoomInToolStripMenuItem.Click += zoomIn_Click;
            zoomInToolStripButton.Click += zoomIn_Click;
            zoomOutToolStripMenuItem.Click += zoomOut_Click;
            zoomOutToolStripButton.Click += zoomOut_Click;
            sideBySideLeftImageBox.ZoomChanged += imageBox_ZoomChanged;
            sideBySideRightImageBox.ZoomChanged += imageBox_ZoomChanged;

            sideBySideLeftImageBox.MouseWheel += ImageBox_MouseWheel;
            sideBySideRightImageBox.MouseWheel += ImageBox_MouseWheel;
            overlayImageBox.MouseWheel += ImageBox_MouseWheel;
            overlayFlickerImageBox.MouseWheel += ImageBox_MouseWheel;


            //imagebox scroll delegates
            sideBySideLeftImageBox.Scroll += sideBySideImageBox_Scroll;
            sideBySideRightImageBox.Scroll += sideBySideImageBox_Scroll;

            //highlighter delegates
            highlightToolStripButton.Click += highlightToolStripButton_Click;
            highlightToolStripTrackBar.ValueChanged += highlightToolStripTrackBar_ValueChanged;


            //overlay merge delegates            
            overlayBlendToolStripTrackBar.ValueChanged += overlayBlendToolStripTrackBar_ValueChanged;

            //overlayFlicker delegates
            overlayFlickerTimer.Tick += overlayFlickerTimer_Tick;
            overlayFlickerToolStripNumericUpDown.ValueChanged += overlayFlickerToolStripNumericUpDown_ValueChanged;
            overlayFlickerImageBox.KeyDown += overlayFlickerImageBox_KeyDown; //manual set the image to flick

            //StatusStrip delegates
            sideBySideLeftImageBox.ZoomChanged += ImageBox_ZoomChanged;
            sideBySideRightImageBox.ZoomChanged += ImageBox_ZoomChanged;
            overlayImageBox.ZoomChanged += ImageBox_ZoomChanged;
            overlayFlickerImageBox.ZoomChanged += ImageBox_ZoomChanged;

            sideBySideLeftImageBox.MouseMove += ImageBox_MouseMove;
            sideBySideRightImageBox.MouseMove += ImageBox_MouseMove;
            overlayImageBox.MouseMove += ImageBox_MouseMove;
            overlayFlickerImageBox.MouseMove += ImageBox_MouseMove; ;

        }

        /// <summary>
        /// Hard code change some UI settings
        /// </summary>
        private void HardCodeInitComponent()
        {
            _logger.Debug("HardCodeInitComponent");
            //set location
            Point mainLoc = new Point(3, 25);
            mainToolStrip.Location = mainLoc;

            var highlightLoc = new Point(mainLoc.X + mainToolStrip.Width + 25, mainLoc.Y);
            highlightToolStrip.Location = highlightLoc;

            var overlayLoc = new Point(highlightLoc.X + highlightToolStrip.Width + 25, highlightLoc.Y);
            overlayToolStrip.Location = overlayLoc;

            var overlayFLickerLoc = new Point(mainLoc.X + mainToolStrip.Width + 25, mainLoc.Y);
            overlayFlickerToolStrip.Location = overlayFLickerLoc;

            overlayBlendToolStripTrackBar.Minimum = 0;
            overlayBlendToolStripTrackBar.Maximum = 4;
            overlayBlendToolStripTrackBar.SmallChange = 1;
            overlayBlendToolStripTrackBar.LargeChange = 2;
            overlayBlendToolStripTrackBar.Value = 4 / 2; //medium value

            //set highlight range ,depends on lookup table on DisplayHighlight()
            highlightToolStripTrackBar.Minimum = 1;
            highlightToolStripTrackBar.Maximum = _thresholdLookup.Length;
            highlightToolStripTrackBar.LargeChange = 2;
            highlightToolStripTrackBar.SmallChange = 1;

            overlayFlickerToolStripNumericUpDown.NumericUpDownControl.TextAlign = HorizontalAlignment.Right;
        }

        #region Menu --> File delegates
        void fileNewDiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ChooseFileForm dialog = new ChooseFileForm())
            {
                dialog.MainForm = this;
                dialog.ShowDialog();
            }
        }

        void fileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        #endregion

        #region Menu --> Settings delegates
        void highlight25OpacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            this.SetHighlightOpacityCommand(isChecked, new HighlightOpacity25Command(this));
        }
        void highlight50OpacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            this.SetHighlightOpacityCommand(isChecked, new HighlightOpacity50Command(this));
        }
        void highlight75OpacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            this.SetHighlightOpacityCommand(isChecked, new HighlightOpacity75Command(this));
        }
        void highlight100OpacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            this.SetHighlightOpacityCommand(isChecked, new HighlightOpacity100Command(this));
        }
        void highlightOpacityToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ToolStripComboBox)sender).SelectedIndex;
            switch (index)
            {
                case 0/* 25% */: this.SetHighlightOpacityCommand(true, new HighlightOpacity25Command(this)); break;
                case 1/* 50% */: this.SetHighlightOpacityCommand(true, new HighlightOpacity50Command(this)); break;
                case 2/* 75% */: this.SetHighlightOpacityCommand(true, new HighlightOpacity75Command(this)); break;
                case 3/*100% */: this.SetHighlightOpacityCommand(true, new HighlightOpacity100Command(this)); break;
            }
        }

        private void SetHighlightOpacityCommand(bool isChecked, HighlightOpacityCommand command)
        {
            _logger.Debug("SetHighlightOpacityCommand to [{0}]", command.GetStatus());
            this._highlightOpacityControl.SetCommand(command);
            this._highlightOpacityControl.SwitchHighlightOpacity();

            if (isChecked)
            {
                DisplayImage();
            }
        }


        void cacheEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Debug("cacheEnableToolStripMenuItem_Click");
            cacheDisableToolStripMenuItem.Checked = !cacheEnableToolStripMenuItem.Checked;
            SettingsHelper.EnableCache = cacheEnableToolStripMenuItem.Checked;
        }

        void cacheDisableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Debug("cacheDisableToolStripMenuItem_Click");
            bool status = cacheEnableToolStripMenuItem.Checked = !cacheDisableToolStripMenuItem.Checked;
            SettingsHelper.EnableCache = status;
        }
        #endregion

        #region Receiver -> Action (Command Design Pattern) for highlight opacity command...
        public void EnableHighlight25()
        {
            _logger.Debug("EnableHighlight25");
            highlight25OpacityToolStripMenuItem.Checked = true;
            highlightOpacityToolStripComboBox.ComboBox.SelectedIndex = 0;

            this._highlightOpacityAlphaValue = 0.25f;
        }
        public void DisableHighlight25()
        {
            _logger.Debug("DisableHighlight25");
            highlight25OpacityToolStripMenuItem.Checked = false;
        }
        public void EnableHighlight50()
        {
            _logger.Debug("EnableHighlight50");
            highlight50OpacityToolStripMenuItem.Checked = true;
            highlightOpacityToolStripComboBox.ComboBox.SelectedIndex = 1;

            this._highlightOpacityAlphaValue = 0.5f;
        }
        public void DisableHighlight50()
        {
            _logger.Debug("DisableHighlight50");
            highlight50OpacityToolStripMenuItem.Checked = false;
        }
        public void EnableHighlight75()
        {
            _logger.Debug("EnableHighlight75");
            highlight75OpacityToolStripMenuItem.Checked = true;
            highlightOpacityToolStripComboBox.ComboBox.SelectedIndex = 2;

            this._highlightOpacityAlphaValue = 0.75f;
        }
        public void DisableHighlight75()
        {
            _logger.Debug("DisableHighlight75");
            highlight75OpacityToolStripMenuItem.Checked = false;
        }
        public void EnableHighlight100()
        {
            _logger.Debug("EnableHighlight100");
            highlight100OpacityToolStripMenuItem.Checked = true;
            highlightOpacityToolStripComboBox.ComboBox.SelectedIndex = 3;

            this._highlightOpacityAlphaValue = 1.0f;
        }
        public void DisableHighlight100()
        {
            _logger.Debug("DisableHighlight100");
            highlight100OpacityToolStripMenuItem.Checked = false;
        }
        #endregion

        #region Menu --> Help delegates
        void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();

            using (AboutBox ab = new AboutBox())
            {
                ab.ShowDialog();
            }

        }
        #endregion

        #region Receiver -> Action (Command Design Pattern) for layout command...
        public void EnableSideBySideLayout()
        {
            _logger.Debug("EnableSideBySideLayout");
            sideBySideLayoutToolStripButton.Checked = true;
            sideBySideLayoutToolStripMenuItem.Checked = true;
            highlightToolStrip.Visible = true;

            sideBySideLayoutPanel.Dock = DockStyle.Fill;
            sideBySideLayoutPanel.Visible = true;

            this._state = new SideBySideState(sideBySideLeftImageBox, sideBySideRightImageBox);
        }
        public void DisableSideBySideLayout()
        {
            _logger.Debug("DisableSideBySideLayout");
            sideBySideLayoutToolStripButton.Checked = false;
            sideBySideLayoutToolStripMenuItem.Checked = false;
            highlightToolStrip.Visible = false;
            sideBySideLayoutPanel.Visible = false;
        }

        public void EnableOverlayLayout()
        {
            _logger.Debug("EnableOverlayLayout");
            overlayLayoutToolStripButton.Checked = true;
            overlayLayoutToolStripMenuItem.Checked = true;
            highlightToolStrip.Visible = true;
            overlayToolStrip.Visible = true;

            overlayLayoutPanel.Dock = DockStyle.Fill;
            overlayLayoutPanel.Visible = true;

            DisplayBlendImage();
        }
        public void DisableOverlayLayout()
        {
            _logger.Debug("DisableOverlayLayout");
            overlayLayoutToolStripButton.Checked = false;
            overlayLayoutToolStripMenuItem.Checked = false;
            highlightToolStrip.Visible = false;
            overlayToolStrip.Visible = false;

            overlayLayoutPanel.Visible = false;
        }

        public void EnableOverlayFlickerLayout()
        {
            _logger.Debug("EnableOverlayFlickerLayout");
            overlayFlickeringLayoutToolStripButton.Checked = true;
            overlayFlickeringLayoutToolStripMenuItem.Checked = true;
            overlayFlickerToolStrip.Visible = true;

            overlayFlickerLayoutPanel.Dock = DockStyle.Fill;
            overlayFlickerLayoutPanel.Visible = true;
            overlayFlickerImageBox.Focus(); // focus in order to raise keydown event

            this._state = new OverlayFlickerState();

            overlayFlickerTimer.Interval = this._overlayFlickerTimerInterval;
            overlayFlickerTimer.Start();
        }
        public void DisableOverlayFlickerLayout()
        {
            _logger.Debug("DisableOverlayFlickerLayout");
            overlayFlickeringLayoutToolStripButton.Checked = false;
            overlayFlickeringLayoutToolStripMenuItem.Checked = false;
            overlayFlickerLayoutPanel.Visible = false;
            overlayFlickerToolStrip.Visible = false;
            overlayFlickerTimer.Stop();
        }
        #endregion

        #region layout delegates
        void sideBySideLayoutToolStripButton_Click(object sender, EventArgs e)
        {
            _logger.Debug("sideBySideLayoutToolStripButton_Click");
            bool isChecked = ((ToolStripButton)sender).Checked;
            this.SetLayoutCommand(isChecked, new SideBySideLayoutCommand(this));
        }
        void sideBySideLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Debug("sideBySideLayoutToolStripMenuItem_Click");
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            this.SetLayoutCommand(isChecked, new SideBySideLayoutCommand(this));
        }

        void overlayLayoutToolStripButton_Click(object sender, EventArgs e)
        {
            _logger.Debug("overlayLayoutToolStripButton_Click");
            bool isChecked = ((ToolStripButton)sender).Checked;
            this.SetLayoutCommand(isChecked, new OverlayLayoutCommand(this));
        }
        void overlayLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Debug("overlayLayoutToolStripMenuItem_Click");
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            this.SetLayoutCommand(isChecked, new OverlayLayoutCommand(this));
        }

        void overlayFlickeringLayoutToolStripButton_Click(object sender, EventArgs e)
        {
            _logger.Debug("overlayFlickeringLayoutToolStripButton_Click");
            bool isChecked = ((ToolStripButton)sender).Checked;
            this.SetLayoutCommand(isChecked, new OverlayFlickerLayoutCommand(this));
        }
        void overlayFlickeringLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Debug("overlayFlickeringLayoutToolStripMenuItem_Click");
            bool isChecked = ((ToolStripMenuItem)sender).Checked;
            this.SetLayoutCommand(isChecked, new OverlayFlickerLayoutCommand(this));
        }

        private void SetLayoutCommand(bool isChecked, LayoutCommand command)
        {
            _logger.Debug("SetLayoutCommand({0},{1})", isChecked, command.GetStatus());
            this._layoutControl.SetCommand(command);
            this._layoutControl.SwitchLayout();
            if (isChecked)
            {
                DisplayImage();
            }
        }
        #endregion

        #region Receiver -> Action (Command Design Pattern) for image display command...
        public void EnableFitToWindowDisplayMode()
        {
            _logger.Debug("EnableFitToWindowDisplayMode");
            fitImageToWindowToolStripButton.Checked = true;
            fitImageToWindowToolStripMenuItem.Checked = true;

            //change the behaviour of imageBox here
            foreach (var imgBox in GetAll(this, typeof(ImageBox)))
            {
                try
                {
                    //((ImageBox)imgBox).ZoomToFit();
                    ((ImageBox)imgBox).SizeToFit = true;
                }
                catch (Exception ex)
                {
                    _logger.Error("EnableFitToWindowDisplayMode.SizeToFit Error: " + ex.Message);
                }
            }
        }
        public void DisableFitToWindowDisplayMode()
        {
            _logger.Debug("DisableFitToWindowDisplayMode");
            fitImageToWindowToolStripButton.Checked = false;
            fitImageToWindowToolStripMenuItem.Checked = false;

            //change the behaviour of imageBox here
            foreach (var imgBox in GetAll(this, typeof(ImageBox)))
            {
                try
                {
                    //((ImageBox)imgBox).ZoomToFit();
                    ((ImageBox)imgBox).SizeToFit = false;
                }
                catch (Exception ex)
                {
                    _logger.Error("EnableFitToWindowDisplayMode.SizeToFit Error: " + ex.Message);
                }
            }
        }

        public void EnableActualSizeDisplayMode()
        {
            _logger.Debug("EnableActualSizeDisplayMode");
            actualImageSizeToolStripButton.Checked = true;
            actualImageSizeToolStripMenuItem.Checked = true;

            //change the behaviour of imageBox 
            foreach (var imgBox in GetAll(this, typeof(ImageBox)))
            {
                try
                {
                    ((ImageBox)imgBox).ActualSize();
                }
                catch (Exception ex)
                {
                    _logger.Error("ActualSize Error: " + ex.Message);
                }
            }
        }
        public void DisableActualSizeDisplayMode()
        {
            _logger.Debug("DisableActualSizeDisplayMode");
            actualImageSizeToolStripButton.Checked = false;
            actualImageSizeToolStripMenuItem.Checked = false;
        }
        #endregion

        #region image display delegates
        void actualImageSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Debug("actualImageSizeToolStripMenuItem_Click");
            this.SetImageDisplayCommand(new ActualSizeImageDisplayCommand(this));
        }
        void actualImageSizeToolStripButton_Click(object sender, EventArgs e)
        {
            _logger.Debug("actualImageSizeToolStripButton_Click");
            this.SetImageDisplayCommand(new ActualSizeImageDisplayCommand(this));
        }

        void fitImageToWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logger.Debug("fitImageToWindowToolStripMenuItem_Click");
            this.SetImageDisplayCommand(new FitToWindowImageDisplayCommand(this));
        }
        void fitImageToWindowToolStripButton_Click(object sender, EventArgs e)
        {
            _logger.Debug("fitImageToWindowToolStripButton_Click");            
            this.SetImageDisplayCommand(new FitToWindowImageDisplayCommand(this));
        }

        void Main_SizeChanged(object sender, EventArgs e)
        {
            _logger.Debug("Main_SizeChanged");
            DisplayImage();
        }

        private void SetImageDisplayCommand(ImageDisplayCommand command)
        {
            _logger.Debug("SetImageDisplayCommand to : {0}", command.GetStatus());
            this._imageDisplayControl.SetCommand(command);
            this._imageDisplayControl.SwitchImageDisplayMode();
        }
        #endregion

        #region zoom delegates
        void zoomOut_Click(object sender, EventArgs e)
        {
            _logger.Debug("zoomOut_Click");
            //change image display             
            this.SetImageDisplayCommand(new NoneImageDisplayCommand(this));

            switch (_layoutControl.Status)
            {
                case LayoutMode.SideBySide:
                    //sideBySideLeftImageBox.ZoomOut();
                    //sideBySideRightImageBox.ZoomOut();

                    sideBySideLeftImageBox.Zoom = sideBySideLeftImageBox.ZoomLevels.PreviousZoom(sideBySideLeftImageBox.Zoom);
                    sideBySideRightImageBox.Zoom = sideBySideRightImageBox.ZoomLevels.PreviousZoom(sideBySideRightImageBox.Zoom);
                    break;
                case LayoutMode.Overlay:
                    overlayImageBox.ZoomOut();
                    break;
                case LayoutMode.OverlayFlicker:
                    overlayFlickerImageBox.ZoomOut();
                    break;
            }
        }

        void zoomIn_Click(object sender, EventArgs e)
        {
            _logger.Debug("zoomIn_Click");
            //change image display 
            this.SetImageDisplayCommand(new NoneImageDisplayCommand(this));

            switch (_layoutControl.Status)
            {
                case LayoutMode.SideBySide:
                    sideBySideLeftImageBox.ZoomIn();
                    sideBySideRightImageBox.ZoomIn();
                    break;
                case LayoutMode.Overlay:
                    overlayImageBox.ZoomIn();
                    break;
                case LayoutMode.OverlayFlicker:
                    overlayFlickerImageBox.ZoomIn();
                    break;
            }
        }

        void imageBox_ZoomChanged(object sender, EventArgs e)
        {
            _logger.Debug("imageBox_ZoomChanged");
            string senderName = ((ImageBox)sender).Name;
            if (this._zoomImpactedImageBox != string.Empty && senderName == this._zoomImpactedImageBox)
            {
                _logger.Warn("imageBox_ZoomChanged won't change [{0}] to prevent infinite loop", senderName);
                //this._zoomImpactedImageBox = "";
                return;
            }

            ImageBox sourceImage = new ImageBox();
            ImageBox targetImage = new ImageBox();
            imageBoxSelector(sender, ref sourceImage, ref targetImage);
            this._zoomImpactedImageBox = targetImage.Name;
            _logger.Debug("imageBox_ZoomChanged-{0} with Zoom-{1}", senderName, sourceImage.Zoom);

            //set the zoom level to be the exact same value
            //targetImage.Zoom = sourceImage.Zoom;
            //targetImage.CenterAt(sourceImage.Location);
            targetImage.ZoomToRegion(sourceImage.GetSourceImageRegion());
            targetImage.Zoom = sourceImage.Zoom;
        }

        protected void imageBoxSelector(object sender, ref ImageBox sourceImage, ref ImageBox targetImage)
        {
            //_logger.Trace("imageBoxSelector-" + ((ImageBox)sender).Name);

            sourceImage = ((Cyotek.Windows.Forms.ImageBox)sender);
            targetImage = new Cyotek.Windows.Forms.ImageBox();
            switch (sourceImage.Name.ToLower())
            {
                case "sidebysideleftimagebox": targetImage = sideBySideRightImageBox; break;
                case "sidebysiderightimagebox": targetImage = sideBySideLeftImageBox; break;
            }
        }

        void ImageBox_MouseWheel(object sender, MouseEventArgs e)
        {
            _logger.Debug("ImageBox_MouseWheel-Delta [{0}]", e.Delta);
            if (e.Delta != 0)
            {
                this._zoomImpactedImageBox = "";
                //change image display 
                this.SetImageDisplayCommand(new NoneImageDisplayCommand(this));
            }
        }
        #endregion

        #region imagebox scroll delegates
        /// <summary>
        /// sync the scroll behavior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sideBySideImageBox_Scroll(object sender, ScrollEventArgs e)
        {
            _logger.Debug("sideBySideImageBox_Scroll-" + ((ImageBox)sender).Name);

            ImageBox sourceImage = new ImageBox(), targetImage = new ImageBox();

            imageBoxSelector(sender, ref sourceImage, ref targetImage);

            int verticalVal = sourceImage.VerticalScroll.Value;
            int horizontalVal = sourceImage.HorizontalScroll.Value;

            targetImage.ScrollTo(horizontalVal, verticalVal);
        }
        #endregion

        #region Receiver -> Action (Command Design Pattern) for highlight command...
        public void EnableHighlight()
        {
            _logger.Debug("EnableHighlight");
            highlightToolStripButton.CheckState = CheckState.Checked;
            highlightOpacityToolStripComboBox.Enabled = true;
            highlightToolStripTrackBar.Enabled = true;
            highlightOpacityToolStripMenuItem.Enabled = true;
            highlightThresholdToolStripStatusLabel.Visible = true;
        }
        public void DisableHightlight()
        {
            _logger.Debug("DisableHightlight");
            highlightToolStripButton.CheckState = CheckState.Unchecked;
            highlightOpacityToolStripComboBox.Enabled = false;
            highlightToolStripTrackBar.Enabled = false;
            highlightOpacityToolStripMenuItem.Enabled = false;
            highlightThresholdToolStripStatusLabel.Visible = false;
        }
        #endregion

        #region highlighter delegates
        void highlightToolStripButton_Click(object sender, EventArgs e)
        {
            _logger.Debug("highlightToolStripButton_Click");
            bool isChecked = ((ToolStripButton)sender).Checked;

            if (isChecked)
            {
                //enable highlight function
                this.SetHighlightCommand(new HighlightOnCommand(this));
            }
            else
            {
                this.SetHighlightCommand(new HighlightOffCommand(this));
            }
        }

        private void SetHighlightCommand(HighlightCommand command)
        {
            _logger.Debug("SetHighlightCommand({0})", command.GetStatus());
            this._highlightControl.SetCommand(command);
            this._highlightControl.SwitchHighlight();
            this.DisplayImage();
        }

        void highlightToolStripTrackBar_ValueChanged(object sender, EventArgs e)
        {
            _logger.Debug("highlightToolStripTrackBar_ValueChanged");
            this._highlightValue = highlightToolStripTrackBar.TrackBarControl.Value;
            toolTip.SetToolTip(highlightToolStripTrackBar.TrackBarControl, this._highlightValue.ToString());

            int threshold = this._thresholdLookup[this._thresholdLookup.Length - this._highlightValue];
            highlightThresholdToolStripStatusLabel.Text = string.Format("Highlight Threshold: {0}", threshold);

            DisplayHighlight();
        }
        #endregion

        #region overlay merge delegates
        protected delegate void UpdateOverlayImgBoxDelegate(Bitmap img);

        void overlayBlendToolStripTrackBar_ValueChanged(object sender, EventArgs e)
        {
            _logger.Debug("overlayBlendToolStripTrackBar_ValueChanged");
            this._blendValue = overlayBlendToolStripTrackBar.TrackBarControl.Value;

            DisplayBlendImage();
        }

        private void DisplayBlendImage()
        {
            const float MAX_VALUE = 255;

            var theTrackbar = overlayBlendToolStripTrackBar.TrackBarControl as TrackBar;
            //convert the trackbar value into the real threshold between 0 and MAX_VALUE;
            float tick = MAX_VALUE / theTrackbar.Maximum;

            float threshold = tick * this._blendValue;
            _logger.Debug("DisplayBlendImage with threshold [{0}]", threshold);

            DisplayBlendImageArgs args = new DisplayBlendImageArgs()
            {
                OriginalFirstImage = new Bitmap(this.OriginalFirstImage),
                OriginalSecondImage = new Bitmap(this.OriginalSecondImage),
                threshold = threshold
            };

            //use multi-thread anonymouse delegate to handle image blending
            ThreadPool.QueueUserWorkItem(
                (data) =>
                {
                    //update loading
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateLoadingProgressDelegate(UpdateLoadingProgress), true);
                    }
                    else
                    {
                        UpdateLoadingProgress(true);
                    }

                    DisplayBlendImageArgs arg = data as DisplayBlendImageArgs;
                    if (arg.threshold == 0)
                    {
                        //this is left image
                        this.OverlayBlendImage = arg.OriginalFirstImage;
                    }
                    else if (arg.threshold == 255)
                    {
                        //this is right image
                        this.OverlayBlendImage = arg.OriginalSecondImage;
                    }
                    else
                    {
                        this.OverlayBlendImage = ImageHelper.MatrixBlend(
                                                                arg.OriginalFirstImage,
                                                                arg.OriginalSecondImage,
                                                                (byte)arg.threshold);
                    }

                    this._highlightContext.setBlendImage(this.OverlayBlendImage);

                    //set the current layout state
                    this._state = new OverlayState(overlayImageBox);
                    if (this.InvokeRequired)
                    {
                        this.Invoke(
                            new UpdateOverlayImgBoxDelegate(
                                    delegate(Bitmap img)
                                    {
                                        //render highlight
                                        if (this._highlightControl.Status == HighlightMode.On)
                                        {
                                            DisplayHighlight();
                                        }
                                        else
                                        {
                                            try
                                            {
                                                this.overlayImageBox.Image = img;
                                            }
                                            catch (Exception ex)
                                            {
                                                _logger.Error("DisplayBlendImage UpdateOverlayImgBoxDelegate exception: " + ex.Message);
                                                _logger.Error("DisplayBlendImage UpdateOverlayImgBoxDelegate stack: " + ex.StackTrace);
                                            }
                                        }

                                    }),
                            new object[] { this.OverlayBlendImage });
                    }
                    else
                    {
                        //called from main UI thread
                        if (this._highlightControl.Status == HighlightMode.On)
                        {
                            DisplayHighlight();
                        }
                        else
                        {
                            overlayImageBox.Image = this.OverlayBlendImage;
                        }
                    }

                    //update loading
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateLoadingProgressDelegate(UpdateLoadingProgress), false);
                    }
                    else
                    {
                        UpdateLoadingProgress(false);
                    }
                }
            , args);

        }

        /// <summary>
        /// An class for specify the multi-thread argument for DisplayBlendImage method
        /// </summary>
        public class DisplayBlendImageArgs
        {
            public Bitmap OriginalFirstImage { get; set; }
            public Bitmap OriginalSecondImage { get; set; }
            public float threshold { get; set; }
        }
        protected delegate void UpdateLoadingProgressDelegate(bool isVisible);
        public void UpdateLoadingProgress(bool isVisible)
        {
            loadingToolStripStatusLabel.Visible = isVisible;
        }
        #endregion

        #region overlayFlicker delegates
        void overlayFlickerTimer_Tick(object sender, EventArgs e)
        {
            if (this._isFlickerFirstImage)
            {
                _logger.Debug("Flicker Second Image");
                overlayFlickerImageBox.Image = OriginalSecondImage;
            }
            else
            {
                _logger.Debug("Flicker First Image");
                overlayFlickerImageBox.Image = OriginalFirstImage;
            }
            this._isFlickerFirstImage = !this._isFlickerFirstImage;
        }

        void overlayFlickerToolStripNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            overlayFlickerTimer.Stop();
            this._overlayFlickerTimerInterval = Convert.ToInt32(overlayFlickerToolStripNumericUpDown.Value);

            _logger.Debug("overlayFlickerToolStripNumericUpDown_ValueChanged to [{0}] ms", this._overlayFlickerTimerInterval);
            overlayFlickerTimer.Interval = this._overlayFlickerTimerInterval;
            overlayFlickerTimer.Start();

            overlayFlickerImageBox.Focus();
        }

        void overlayFlickerImageBox_KeyDown(object sender, KeyEventArgs e)
        {
            _logger.Debug("overlayFlickerImageBox_KeyDown [{0}]", e.KeyCode);

            //manually set the overlay flicker image
            if (this._layoutControl.Status == LayoutMode.OverlayFlicker)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                    case Keys.Down:
                        overlayFlickerTimer.Start();
                        break;
                    case Keys.Left:
                        overlayFlickerTimer.Stop();
                        overlayFlickerImageBox.Image = OriginalFirstImage;
                        this._isFlickerFirstImage = true;
                        break;
                    case Keys.Right:
                        overlayFlickerTimer.Stop();
                        overlayFlickerImageBox.Image = OriginalSecondImage;
                        this._isFlickerFirstImage = false;
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion

        #region StatusStrip delegates
        void ImageBox_ZoomChanged(object sender, EventArgs e)
        {
            _logger.Debug("ImageBox_ZoomChanged");
            int zoomLevel = ((ImageBox)sender).Zoom;
            zoomLevelToolStripStatusLabel.Text = zoomLevel + "%";
        }

        void ImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            var theSender = ((ImageBox)sender);
            theSender.Focus();
            if (theSender.Image == null)
            {
                return;
            }

            int zoomLevel = theSender.Zoom;
            zoomLevelToolStripStatusLabel.Text = zoomLevel + "%";

            string resolution = string.Format("{0} x {1}", theSender.Image.Size.Width, theSender.Image.Size.Height);
            resolutionToolStripStatusLabel.Text = resolution;

            var cursorPoint = theSender.PointToImage(e.Location);
            if (!cursorPoint.IsEmpty)
            {
                string location = string.Format("({0},{1})", cursorPoint.X, cursorPoint.Y);
                pointerLocationToolStripStatusLabel.Text = location;

                Bitmap senderImage = theSender.Image as Bitmap;

                var currPixel = senderImage.GetPixel((cursorPoint.X - 1) & 1, (cursorPoint.Y - 1) & 1);
                string rgb = string.Format("RGBA: {0}; {1}; {2}; {3}", currPixel.R, currPixel.G, currPixel.B, currPixel.A);
                rgbToolStripStatusLabel.Text = rgb;
            }
        }
        #endregion

        /// <summary>
        /// Display / render image on the imagebox
        /// </summary>
        private void DisplayImage()
        {
            _logger.Debug("DisplayImage within [{0}] status", this._layoutControl.Status);
            if (OriginalFirstImage == null || OriginalSecondImage == null)
            {
                _logger.Warn("Image hasn't be specified");
                return;
            }

            //render highlight
            if (this._highlightControl.Status == HighlightMode.On)
            {
                DisplayHighlight();
            }
            else
            {
                switch (this._layoutControl.Status)
                {
                    case LayoutMode.SideBySide:
                        sideBySideLeftImageBox.Image = OriginalFirstImage;
                        sideBySideRightImageBox.Image = OriginalSecondImage;
                        break;
                    case LayoutMode.Overlay:
                        //Display Blend Image
                        DisplayBlendImage();
                        break;
                    case LayoutMode.OverlayFlicker:
                        overlayFlickerImageBox.Image = OriginalFirstImage;
                        break;
                }
            }
            this._imageDisplayControl.SwitchImageDisplayMode();

            GC.Collect();
        }

        /// <summary>
        /// Displaying highlight based on current state, only applies to sidebyside & overlay mode
        /// </summary>
        private void DisplayHighlight()
        {
            int threshold = this._thresholdLookup[this._thresholdLookup.Length - this._highlightValue]; //inverse direction

            _logger.Debug("DisplayHighlight with threshold: {0}", threshold);

            try
            {
                //new a instance to prevent multi-thread issue
                HighlightContextArgs args = new HighlightContextArgs()
                {
                    Alpha = this._highlightOpacityAlphaValue,
                    Threshold = threshold,
                };

                //perform highlight action
                this._highlightContext.setState(this._state, this._imageDisplayControl.Status);

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._highlightContext.Highlight), args);
            }
            catch (Exception ex)
            {
                _logger.Error("DisplayHighlight Error:{0}", ex.Message);
                _logger.Error("DisplayHighlight STACK:{0}", ex.StackTrace);
            }
        }

        /// <summary>
        /// Initialize render the images
        /// </summary>
        public void InitRender()
        {
            _logger.Debug("InitRender");

            cacheEnableToolStripMenuItem.Checked = (SettingsHelper.EnableCache == true) ? true : false;
            cacheDisableToolStripMenuItem.Checked = !cacheEnableToolStripMenuItem.Checked;
            //clear cache
            CacheHelper.RemoveAllCache();

            //check file
            if (SettingsHelper.FirstPath != string.Empty && !File.Exists(SettingsHelper.FirstPath))
            {
                string errMessage = string.Format("File doesn't existed in: {0}", SettingsHelper.FirstPath);
                _logger.Error(errMessage);
                MessageBox.Show(errMessage, "Error", MessageBoxButtons.OK);
            }
            if (SettingsHelper.SecondPath != string.Empty && !File.Exists(SettingsHelper.SecondPath))
            {
                string errMessage = string.Format("File doesn't existed in: {0}", SettingsHelper.SecondPath);
                _logger.Error(errMessage);
                MessageBox.Show(errMessage, "Error", MessageBoxButtons.OK);
            }


            //init data
            if (SettingsHelper.FirstPath != string.Empty && SettingsHelper.SecondPath != string.Empty)
            {
                try
                {
                    OriginalFirstImage = new Bitmap(AForge.Imaging.Image.FromFile(SettingsHelper.FirstPath));
                    OriginalSecondImage = new Bitmap(AForge.Imaging.Image.FromFile(SettingsHelper.SecondPath));
                    ValidateInputImages();
                }
                catch (Exception ex)
                {
                    _logger.Error("InitRender err:" + ex.Message);
                    _logger.Error("InitRender stack:" + ex.StackTrace);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this._highlightContext = new HighlightContext(this, OriginalFirstImage, OriginalSecondImage);
                this._blendValue = overlayBlendToolStripTrackBar.TrackBarControl.Value;
            }

            //init description
            sideBySideLeftTextBox.Text = string.IsNullOrEmpty(SettingsHelper.LeftDescription) ? SettingsHelper.FirstPath : SettingsHelper.LeftDescription;
            sideBySideRightTextBox.Text = string.IsNullOrEmpty(SettingsHelper.RightDescription) ? SettingsHelper.SecondPath : SettingsHelper.RightDescription;

            //init overlayflicer numeric up down
            overlayFlickerToolStripNumericUpDown.Value = this._overlayFlickerTimerInterval;

            //performCommand            
            this._layoutControl.SwitchLayout();
            this._imageDisplayControl.SwitchImageDisplayMode();
            if (this._highlightControl.Status == HighlightMode.On)
            {
                this._highlightControl.SwitchHighlight();
            }

            //bind highlight opacity value
            switch (Convert.ToInt32(this._highlightOpacityAlphaValue * 100))
            {
                case 25: this.SetHighlightOpacityCommand(false, new HighlightOpacity25Command(this)); break;
                case 50:
                default:
                    this.SetHighlightOpacityCommand(false, new HighlightOpacity50Command(this)); break;
                case 75: this.SetHighlightOpacityCommand(false, new HighlightOpacity75Command(this)); break;
                case 100: this.SetHighlightOpacityCommand(false, new HighlightOpacity100Command(this)); break;
            }

            //must put after above performCommand! to avoid highlightToolStripTrackBar.ValueChanged raised before this._state has been assigned
            highlightToolStripTrackBar.Value = this._highlightValue;

            DisplayImage();
        }

        /// <summary>
        /// Validate input image, check whether it is within the same resolution.
        /// </summary>
        private void ValidateInputImages()
        {
            var firstSize = this.OriginalFirstImage.Size;
            var secondSize = this.OriginalSecondImage.Size;
            if (firstSize != secondSize)
            {
                string msg = "Sorry, both images must be within the same resolution (width * height). \r\n Please try again";
                _logger.Warn(msg);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                Application.Exit();
                return;
            }
        }

        /// <summary>
        /// Get all the control (including sub control)
        /// </summary>        
        protected IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                            .Concat(controls)
                            .Where(c => c.GetType() == type);
        }
    }
}