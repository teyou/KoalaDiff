using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace KoalaDiff.Library
{
    public class CommandLineOptions
    {
        private const string HELP_FIT_TO_WINDOW = "Let the image fits to the window";
        private const string HELP_ACTUALSIZE = "Let the image displays in Actual size";
        private const string HELP_SIDE_BY_SIDE_LAYOUT = "View the image in side by side mode";
        private const string HELP_OVERLAY_LAYOUT = "View the image in overlay mode";
        private const string HELP_OVERLAY_FLICKER_LAYOUT = "View the image in the overlay flicker mode";
        private const string HELP_HIGHLIGHT = "Please specify the value between 1 and 23";
        private const string HELP_MAXIMIZE = "Maximize the window when start the application";
        private const string HELP_NORMALSIZE = "Start the application within normal size";
        private const string HELP_DESCRIPTION_LEFT = "Description in the left side title bar";
        private const string HELP_DESCRIPTION_RIGHT = "Description in the right side title bar";

        [Option("f", "fittowindow", DefaultValue = true, MutuallyExclusiveSet = "imagedisplay-mode", HelpText = HELP_FIT_TO_WINDOW)]
        public bool FitToWindow { get; set; }

        [Option("a", "actualsize", MutuallyExclusiveSet = "imagedisplay-mode", HelpText = HELP_ACTUALSIZE)]
        public bool ActualSize { get; set; }

        [Option("s", "sidebyside", DefaultValue = true, MutuallyExclusiveSet = "layout-mode", HelpText = HELP_SIDE_BY_SIDE_LAYOUT)]
        public bool SideBySideLayout { get; set; }

        [Option("o", "overlay", MutuallyExclusiveSet = "layout-mode", HelpText = HELP_OVERLAY_LAYOUT)]
        public bool OverlayLayout { get; set; }

        [Option("v", "overlayflicker", MutuallyExclusiveSet = "layout-mode", HelpText = HELP_OVERLAY_FLICKER_LAYOUT)]
        public bool OverlayFlickerLayout { get; set; }

        [Option("h", "highlight", DefaultValue = int.MinValue, HelpText = HELP_HIGHLIGHT)]
        public int Highlight { get; set; }

        [Option("m", "max", DefaultValue = true, MutuallyExclusiveSet = "window-mode", HelpText=HELP_MAXIMIZE)]
        public bool MaximizeWindow { get; set; }

        [Option("n", "normal", MutuallyExclusiveSet = "window-mode", HelpText=HELP_NORMALSIZE)]
        public bool NormalSizeWindow { get; set; }

        [Option("l","dl", HelpText = HELP_DESCRIPTION_LEFT)]
        public string DescriptionLeft { get; set; }

        [Option("r", "dr", HelpText = HELP_DESCRIPTION_RIGHT)]
        public string DescriptionRight { get; set; }

        // leftpath & rightpath
        [ValueList(typeof(List<string>), MaximumElements = 2)]
        public IList<string> Paths { get; set; }

        [Option("?", "??")]
        public bool Help { get; set; }

        [HelpOption]
        public string GetHelp()
        {
            var help = new StringBuilder();
            help.AppendLine("Quick start for command-line arguments:".ToUpper());
            help.AppendLine(); 
            help.AppendLine("1. Please specify 2 image path to compare");
            help.AppendLine();
            help.AppendLine("2. Layout mode:");
            help.AppendLine("\t-s : " + HELP_SIDE_BY_SIDE_LAYOUT);
            help.AppendLine("\t-o : " + HELP_OVERLAY_LAYOUT);
            help.AppendLine("\t-v : " + HELP_OVERLAY_FLICKER_LAYOUT); 
            help.AppendLine();
            help.AppendLine("3. Image display mode:");
            help.AppendLine("\t-f : " + HELP_FIT_TO_WINDOW);
            help.AppendLine("\t-a : " + HELP_ACTUALSIZE);            
            help.AppendLine();
            help.AppendLine("4. Window mode:");
            help.AppendLine("\t-m : " + HELP_MAXIMIZE);
            help.AppendLine("\t-n : " + HELP_NORMALSIZE);
            help.AppendLine();
            help.AppendLine("5. Highlight:");
            help.AppendLine("\t-h : " + HELP_HIGHLIGHT);
            help.AppendLine();
            help.AppendLine("6. Description:");
            help.AppendLine("\t-l : " + HELP_DESCRIPTION_LEFT);
            help.AppendLine("\t-r : " + HELP_DESCRIPTION_RIGHT);
            return help.ToString();
        }
    }
}
