using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using KoalaDiff.Library.Command;
using AForge.Imaging;

namespace KoalaDiff.Library
{

    public class HighlightContext
    {
        /// <summary>
        /// Logger
        /// </summary>
        public static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public Main originalForm;
        private State _state;
        public UnmanagedImage _unOriginalLeftImage { get; protected set; }
        public UnmanagedImage _unOriginalRightImage { get; protected set; }
        public UnmanagedImage _unBlendImage { get; protected set; }
        public ImageDisplayMode imageDisplayMode;

        public HighlightContext(Form form, Bitmap originalLeftImage, Bitmap originalRightImage)
        {
            this.originalForm = (Main)form;

            this._unOriginalLeftImage = UnmanagedImage.FromManagedImage(originalLeftImage);
            this._unOriginalRightImage = UnmanagedImage.FromManagedImage(originalRightImage);
            this._unBlendImage = UnmanagedImage.FromManagedImage(originalLeftImage);
        }

        public void setState(State state, ImageDisplayMode imageDisplayMode)
        {
            this._state = state;
            this.imageDisplayMode = imageDisplayMode;
        }

        /// <summary>
        /// Set blend image
        /// </summary>
        /// <param name="blendImage">instance of blendimage</param>
        public void setBlendImage(Bitmap blendImage)
        {
            this._unBlendImage = UnmanagedImage.FromManagedImage(blendImage);
        }

        /// <summary>
        /// Perform highlight
        /// </summary>
        /// <param name="arg"></param>
        public void Highlight(object arg)
        {
            try
            {
                if (originalForm.InvokeRequired)
                {
                    originalForm.Invoke(new UpdateLoadingProgressDelegate(originalForm.UpdateLoadingProgress), true);
                }
                var args = arg as HighlightContextArgs;

                _state.Handle(this, args.Threshold, args.Alpha);

                if (originalForm.InvokeRequired)
                {
                    originalForm.Invoke(new UpdateLoadingProgressDelegate(originalForm.UpdateLoadingProgress), false);
                }
            }
            catch (NullReferenceException nullex)
            {
                _logger.Error("Highlight Null Error:{0}" + nullex.Message);
                _logger.Error("Highlight Null Stack:{0}" + nullex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.Error("Highlight Error:{0}" + ex.Message);
                _logger.Error("Highlight Stack:{0}" + ex.StackTrace);
            }
            finally
            {                
                GC.Collect();                
            }
        }

        protected delegate void UpdateLoadingProgressDelegate(bool isVisible);
    }

    /// <summary>
    /// An class for specify the multi-thread argument for HighlightContext.Highlight method
    /// </summary>
    public class HighlightContextArgs
    {
        public int Threshold { get; set; }
        public float Alpha { get; set; }
    }

    public abstract class State
    {
        protected delegate void UpdateImageBoxDelegate(System.Drawing.Image img, ImageDisplayMode imgDisplayMode);
        /// <summary>
        /// Handle the highlight logic
        /// </summary>
        /// <param name="context"></param>
        /// <param name="threshold">threshold for diff</param>
        /// <param name="alpha">alpha value for hightlight color</param>
        public abstract void Handle(HighlightContext context, int threshold, float alpha);
    }

    public class OverlayFlickerState : State
    {
        public override void Handle(HighlightContext context, int threshold, float alpha)
        {
            //do nothing...
        }
    }

    public class OverlayState : State
    {
        /// <summary>
        /// Logger
        /// </summary>
        public static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private ImageBox _overlayImgBox;

        public OverlayState(ImageBox overlayImageBox)
        {
            this._overlayImgBox = overlayImageBox;
        }

        public override void Handle(HighlightContext context, int threshold, float alpha)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                //calculate the highlighted portion stored into image object
                var highlightedImage = Library.ImageHelper.HightlightDifferences(context._unOriginalLeftImage, context._unOriginalRightImage, threshold, alpha);

                //use blend image as the source instead!!
                var highlightOverlayImage = Library.ImageHelper.MatrixBlend(context._unBlendImage, highlightedImage, (byte)128);

                context.originalForm.Invoke(
                    new UpdateImageBoxDelegate(UpdateOverlayImgBox),
                    new object[] { highlightOverlayImage, context.imageDisplayMode });

                //release memory from images
                highlightedImage.Dispose();
            }
            catch (Exception ex)
            {
                _logger.Error("Handle exception: " + ex.Message);
                _logger.Error("Handle err Stack: " + ex.StackTrace);
            }
            finally
            {
                sw.Stop();
                _logger.Info("OverlayState.Handle takes {0}", sw.Elapsed.ToString());
            }
        }

        private void UpdateOverlayImgBox(System.Drawing.Image img, ImageDisplayMode imgDisplayMode)
        {
            _logger.Debug("UpdateOverlayImgBox with [{0}] mode", imgDisplayMode);

            this._overlayImgBox.Image = img;
            switch (imgDisplayMode)
            {
                case ImageDisplayMode.FitToWindow:
                    this._overlayImgBox.ZoomToFit();
                    break;
                case ImageDisplayMode.ActualSize:
                    this._overlayImgBox.ActualSize();
                    break;
            }
        }
    }

    public class SideBySideState : State
    {
        /// <summary>
        /// Logger
        /// </summary>
        public static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        protected ImageBox _leftImgBox;
        protected ImageBox _rightImgBox;

        public SideBySideState(ImageBox leftImageBox, ImageBox rightImageBox)
        {
            this._leftImgBox = leftImageBox;
            this._rightImgBox = rightImageBox;
        }

        public override void Handle(HighlightContext context, int threshold, float alpha)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                //calculate the highlighted portion stored into image object
                var highlightedImage = Library.ImageHelper.HightlightDifferences(context._unOriginalLeftImage, context._unOriginalRightImage, threshold, alpha);

                //blend the image with the original one
                var highlightLeftImage = Library.ImageHelper.MatrixBlend(context._unOriginalLeftImage, highlightedImage, (byte)128);
                var highlightRightImage = Library.ImageHelper.MatrixBlend(context._unOriginalRightImage, highlightedImage, (byte)128);

                //draw image
                context.originalForm.Invoke(
                    new UpdateImageBoxDelegate(UpdateLeftImgBox),
                    new object[] { highlightLeftImage, context.imageDisplayMode });

                context.originalForm.Invoke(
                    new UpdateImageBoxDelegate(UpdateRightImgBox),
                    new object[] { highlightRightImage, context.imageDisplayMode });

                //release memory from images
                highlightedImage.Dispose();
            }
            catch (Exception ex)
            {
                _logger.Error("Handle exception: " + ex.Message);
                _logger.Error("Handle err Stack: " + ex.StackTrace);
            }
            finally
            {
                sw.Stop();
                _logger.Info("SideBySideState.Handle takes {0}", sw.Elapsed.ToString());
            }
        }

        private void UpdateLeftImgBox(System.Drawing.Image img, ImageDisplayMode imgDisplayMode)
        {
            _logger.Debug("UpdateLeftImgBox with [{0}] mode", imgDisplayMode);
            this._leftImgBox.Image = img;
            switch (imgDisplayMode)
            {
                case ImageDisplayMode.FitToWindow:
                    this._leftImgBox.ZoomToFit();
                    break;
                case ImageDisplayMode.ActualSize:
                    this._leftImgBox.ActualSize();
                    break;
            }
        }

        private void UpdateRightImgBox(System.Drawing.Image img, ImageDisplayMode imgDisplayMode)
        {
            _logger.Debug("UpdateRightImgBox with [{0}] mode", imgDisplayMode);

            this._rightImgBox.Image = img;
            switch (imgDisplayMode)
            {
                case ImageDisplayMode.FitToWindow:
                    this._rightImgBox.ZoomToFit();
                    break;
                case ImageDisplayMode.ActualSize:
                    this._rightImgBox.ActualSize();
                    break;
            }
        }
    }
}
