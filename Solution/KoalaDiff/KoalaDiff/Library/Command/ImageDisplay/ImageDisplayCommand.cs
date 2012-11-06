using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    public abstract class ImageDisplayCommand : Command
    {
        protected ImageDisplayMode _status;
        protected Main _mainForm;

        /// <summary>
        /// Get current ImageMode
        /// </summary>
        /// <returns></returns>
        public ImageDisplayMode GetStatus()
        {
            return this._status;
        }
    }

    public enum ImageDisplayMode
    {
        FitToWindow,
        ActualSize,
        None
    }  
}
