using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class OverlayFlickerLayoutCommand : LayoutCommand
    {
        public OverlayFlickerLayoutCommand(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = LayoutMode.OverlayFlicker;
        }
        public override void Execute()
        {
            base._mainForm.DisableSideBySideLayout();
            base._mainForm.DisableOverlayLayout();
            base._mainForm.EnableOverlayFlickerLayout();
        }
    }
}
