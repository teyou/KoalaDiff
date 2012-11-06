using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class LayoutControl
    {
        private LayoutCommand _command;
        public LayoutMode Status { get; protected set; }

        public LayoutControl()
        {
            this.SetCommand(new NoneLayoutCommand());
        }

        public void SetCommand(LayoutCommand command)
        {
            this._command = command;
            this.Status = this._command.GetStatus();
        }
        public void SwitchLayout()
        {            
            this._command.Execute();
        }
    }
}
