using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightOpacity75Command : HighlightOpacityCommand
    {
        public HighlightOpacity75Command(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = HighlightOpacityMode.SeventyFive;
        }
        public override void Execute()
        {
            base._mainForm.DisableHighlight25();
            base._mainForm.DisableHighlight50();
            base._mainForm.DisableHighlight100();
            base._mainForm.EnableHighlight75();
        }
    }
}
