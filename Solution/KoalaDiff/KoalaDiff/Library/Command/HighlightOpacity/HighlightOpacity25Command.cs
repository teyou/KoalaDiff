using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightOpacity25Command : HighlightOpacityCommand
    {
        public HighlightOpacity25Command(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = HighlightOpacityMode.TwentyFive;
        }
        public override void Execute()
        {
            base._mainForm.DisableHighlight50();
            base._mainForm.DisableHighlight75();
            base._mainForm.DisableHighlight100();
            base._mainForm.EnableHighlight25();
        }
    }
}
