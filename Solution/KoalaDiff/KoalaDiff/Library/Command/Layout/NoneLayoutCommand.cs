using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class NoneLayoutCommand : LayoutCommand
    {
        public NoneLayoutCommand()
        {
            base._status = LayoutMode.None;
        }
        public override void Execute() {/*do nothing*/}
    }
}
