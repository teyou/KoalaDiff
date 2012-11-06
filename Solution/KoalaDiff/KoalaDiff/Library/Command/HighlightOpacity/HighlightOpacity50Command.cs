using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightOpacity50Command : HighlightOpacityCommand
    {
        public HighlightOpacity50Command(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = HighlightOpacityMode.Fifty;
        }
        public override void Execute()
        {
            base._mainForm.DisableHighlight25();
            base._mainForm.DisableHighlight75();
            base._mainForm.DisableHighlight100();
            base._mainForm.EnableHighlight50();
        }
    }
}
