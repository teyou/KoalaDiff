using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightOpacityControl
    {
        private HighlightOpacityCommand _command;
        public HighlightOpacityMode Status { get; protected set; }

        public HighlightOpacityControl()
        {
            this.SetCommand(new HighlightOpacityNoneCommand());//default none
        }

        public void SetCommand(HighlightOpacityCommand command)
        {
            this._command = command;
            this.Status = this._command.GetStatus();
        }

        public void SwitchHighlightOpacity()
        {
            this._command.Execute();
        }
    }
}
