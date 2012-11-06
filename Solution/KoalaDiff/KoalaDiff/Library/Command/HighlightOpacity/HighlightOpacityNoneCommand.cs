using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightOpacityNoneCommand : HighlightOpacityCommand
    {
        public HighlightOpacityNoneCommand()
        {
            base._status = HighlightOpacityMode.None;
        }
        public override void Execute() {/*do nothing*/}
    }
}
