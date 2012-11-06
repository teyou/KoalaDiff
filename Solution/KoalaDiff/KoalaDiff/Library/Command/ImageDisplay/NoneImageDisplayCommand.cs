using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class NoneImageDisplayCommand : ImageDisplayCommand
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public NoneImageDisplayCommand(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = ImageDisplayMode.None;
        }
        public override void Execute()
        {
            _logger.Debug("NoneImageDisplayCommand.Execute");

            base._mainForm.DisableFitToWindowDisplayMode();
            base._mainForm.DisableActualSizeDisplayMode();
        }
    }
}
