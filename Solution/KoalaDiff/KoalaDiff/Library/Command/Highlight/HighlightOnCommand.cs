using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightOnCommand : HighlightCommand
    {
        public HighlightOnCommand(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = HighlightMode.On;
        }
        public override void Execute()
        {
            base._mainForm.EnableHighlight();
        }
    }
}
