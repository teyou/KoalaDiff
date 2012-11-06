using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightControl
    {
        private HighlightCommand _command;
        public HighlightMode Status { get; protected set; }

        public HighlightControl()
        {
            this.SetCommand(new HighlightNoneCommand());//default none
        }

        public void SetCommand(HighlightCommand command)
        {
            this._command = command;
            this.Status = this._command.GetStatus();
        }

        public void SwitchHighlight()
        {
            this._command.Execute();
        }
    }
}
