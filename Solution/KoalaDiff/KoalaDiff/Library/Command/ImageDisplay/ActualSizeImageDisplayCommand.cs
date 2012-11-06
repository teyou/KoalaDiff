using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public class ActualSizeImageDisplayCommand : ImageDisplayCommand
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ActualSizeImageDisplayCommand(Main mainForm)
        {
            base._mainForm = mainForm;
            base._status = ImageDisplayMode.ActualSize;
        }
        public override void Execute()
        {
            _logger.Debug("ActualSizeImageDisplayCommand.Execute");

            base._mainForm.DisableFitToWindowDisplayMode();
            base._mainForm.EnableActualSizeDisplayMode();
        }
    }
}
