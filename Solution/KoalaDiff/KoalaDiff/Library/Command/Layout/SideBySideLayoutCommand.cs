using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class SideBySideLayoutCommand : LayoutCommand
    {
        public SideBySideLayoutCommand(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = LayoutMode.SideBySide;
        }
        public override void Execute()
        {
            base._mainForm.DisableOverlayLayout();
            base._mainForm.DisableOverlayFlickerLayout();
            base._mainForm.EnableSideBySideLayout();
        }
    }
}
