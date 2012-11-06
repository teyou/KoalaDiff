using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class OverlayLayoutCommand : LayoutCommand
    {
        public OverlayLayoutCommand(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = LayoutMode.Overlay;
        }
        public override void Execute()
        {
            base._mainForm.DisableSideBySideLayout();
            base._mainForm.DisableOverlayFlickerLayout();
            base._mainForm.EnableOverlayLayout();
        }
    }
}
