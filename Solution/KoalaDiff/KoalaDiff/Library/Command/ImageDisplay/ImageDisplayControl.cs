using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class ImageDisplayControl
    {
        private ImageDisplayCommand _command;
        public ImageDisplayMode Status { get; protected set; }

        public void SetCommand(ImageDisplayCommand command)
        {
            this._command = command;
            this.Status = this._command.GetStatus();
        }

        public void SwitchImageDisplayMode()
        {            
            this._command.Execute();
        }
    }
}
