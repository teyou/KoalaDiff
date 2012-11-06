using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightOpacity100Command : HighlightOpacityCommand
    {
        public HighlightOpacity100Command(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = HighlightOpacityMode.AHundred;
        }
        public override void Execute()
        {
            base._mainForm.DisableHighlight25();
            base._mainForm.DisableHighlight50();
            base._mainForm.DisableHighlight75();
            base._mainForm.EnableHighlight100();
        }
    }
}