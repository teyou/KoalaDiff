using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class FitToWindowImageDisplayCommand : ImageDisplayCommand
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public FitToWindowImageDisplayCommand(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = ImageDisplayMode.FitToWindow;
        }
        public override void Execute()
        {
            _logger.Debug("FitToWindowImageDisplayCommand.Execute");

            base._mainForm.DisableActualSizeDisplayMode();
            base._mainForm.EnableFitToWindowDisplayMode();
        }
    }
}
