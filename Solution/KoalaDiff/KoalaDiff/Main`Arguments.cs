using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandLine;
using KoalaDiff.Library;
using KoalaDiff.Library.Command;

namespace KoalaDiff
{
    public partial class Main : Form
    {

        public Main(string[] args)
            : this()
        {

            var options = new CommandLineOptions();

            ICommandLineParser parser = new CommandLineParser(new CommandLineParserSettings() { MutuallyExclusive = true });
            if (parser.ParseArguments(args, options))
            {
                ProccesOptions(options);
            }
            else
            {
                _logger.Error("Parsing command line error");
                MessageBox.Show("Parsing argument error, please try again", "Command Line args error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            InitRender();
        }

        /// <summary>
        /// Logic of processing command line arguments
        /// </summary>
        private void ProccesOptions(CommandLineOptions options)
        {
            #region window mode
            if (options.MaximizeWindow)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            if (options.NormalSizeWindow) //beware... the order does matter, this should put after maximise window
            {
                this.WindowState = FormWindowState.Normal;
            }
            #endregion

            //ImagePath
            if (options.Paths.Count == 2)
            {
                string firstPath = options.Paths[0];
                string secondPath = options.Paths[1];

                SettingsHelper.FirstPath = firstPath;
                SettingsHelper.SecondPath = secondPath;
            }

            #region imagedisplay-mode
            if (options.FitToWindow)
            {
                this._imageDisplayControl.SetCommand(new FitToWindowImageDisplayCommand(this));
            }
            if (options.ActualSize)
            {
                this._imageDisplayControl.SetCommand(new ActualSizeImageDisplayCommand(this));
            }
            //this._imageDisplayControl.SwitchImageDisplayMode();
            #endregion

            #region Layout mode
            if (options.SideBySideLayout)
            {
                this._layoutControl.SetCommand(new SideBySideLayoutCommand(this));
            }
            if (options.OverlayLayout)
            {
                this._layoutControl.SetCommand(new OverlayLayoutCommand(this));
            }
            if (options.OverlayFlickerLayout)
            {
                this._layoutControl.SetCommand(new OverlayFlickerLayoutCommand(this));
            }
            //this._layoutControl.SwitchLayout();
            #endregion

            if (options.Highlight != int.MinValue /*minvalue equals default value*/)
            {
                this._highlightControl.SetCommand(new HighlightOnCommand(this));
                this._highlightValue = ((options.Highlight < 1 || options.Highlight > this._thresholdLookup.Length) ? this._thresholdLookup.Length : options.Highlight); //value between 0 and 100 only
            }

            //set description
            SettingsHelper.LeftDescription = options.DescriptionLeft;
            SettingsHelper.RightDescription = options.DescriptionRight;

            if (options.Help)
            {
                MessageBox.Show(options.GetHelp());
            }
        }
    }
}