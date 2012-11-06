using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KoalaDiff.Library;
using KoalaDiff.Library.Command;

namespace KoalaDiff
{
    public partial class Main
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// or Left image in Side by Side mode
        /// </summary>
        public Bitmap OriginalFirstImage { get; set; }

        /// <summary>
        /// or Right image in Side by Side mode
        /// </summary>
        public Bitmap OriginalSecondImage { get; set; }

        /// <summary>
        /// The image when using on overlay mode
        /// </summary>
        public Bitmap OverlayBlendImage { get; set; }

        /// <summary>
        /// Blend trackbar's value
        /// </summary>
        private int _blendValue;

        /// <summary>
        /// Highlight trackbar's value
        /// </summary>
        private int _highlightValue = 1;

        /// <summary>
        /// Lookup table for the threshold value within image highlighting
        /// </summary>
        private int[] _thresholdLookup = new int[] { 1, 3, 5, 10, 15, 30, 50, 75, 100, 125, 150, 175, 200, 225, 250 };
        //1,3,5,10,15,30,50,75,100,125,150,175,200,225,250
        //1, 3, 5, 10, 20, 40, 80, 120, 160, 200, 225, 250, 275, 300, 320, 340, 360, 380, 400 
        /// <summary>
        /// Recording the target imagebox for performing zoom action
        /// usage: prevent infinite zoom loop
        /// </summary>
        private string _zoomImpactedImageBox;

        /// <summary>
        /// presenting LayoutState
        /// </summary>
        private State _state;
        private HighlightContext _highlightContext;

        /// <summary>
        /// highlight opacity value, range between 0.25~1 (i.e. 25% to 100%)
        /// </summary>
        private float _highlightOpacityAlphaValue = 1f;
        /// <summary>
        /// Controller for Highlight Opacity mode
        /// </summary>
        private HighlightOpacityControl _highlightOpacityControl = new HighlightOpacityControl();

        /// <summary>
        /// Controller for Highlight mode
        /// </summary>
        private HighlightControl _highlightControl = new HighlightControl();

        /// <summary>
        /// Controller for Layout Mode , can get the layout status through this class
        /// </summary>
        private LayoutControl _layoutControl = new LayoutControl();

        /// <summary>
        /// Controller for Image display Mode , can get the display status through this class
        /// </summary>        
        private ImageDisplayControl _imageDisplayControl = new ImageDisplayControl();

        /// <summary>
        /// Tick interval for the timer
        /// </summary>
        private int _overlayFlickerTimerInterval = 500;

        private bool _isFlickerFirstImage = false;

        //control
        private ToolTip toolTip = new ToolTip() { ShowAlways = true, AutoPopDelay = 5000, InitialDelay = 1000, ReshowDelay = 500 };

        private Timer overlayFlickerTimer = new Timer();
    }



}
