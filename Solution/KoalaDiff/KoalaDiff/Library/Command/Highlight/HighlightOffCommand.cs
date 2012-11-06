using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightOffCommand : HighlightCommand
    {
        public HighlightOffCommand(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = HighlightMode.Off;
        }
        public override void Execute()
        {
            base._mainForm.DisableHightlight();
        }
    }
}
