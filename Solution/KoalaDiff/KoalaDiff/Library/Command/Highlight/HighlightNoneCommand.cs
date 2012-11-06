using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class HighlightNoneCommand : HighlightCommand
    {
        public HighlightNoneCommand()
        {
            base._status = HighlightMode.None;
        }
        public override void Execute() {/*do nothing*/}
    }
}
