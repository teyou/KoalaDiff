using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    /// <summary>
    /// using command design pattern to handle UI highlight opacity mode
    /// </summary>
    public abstract class HighlightOpacityCommand : Command
    {
        protected HighlightOpacityMode _status;
        protected Main _mainForm;

        /// <summary>
        /// Get current HighlightOpacityMode 
        /// </summary>
        public HighlightOpacityMode GetStatus()
        {
            return this._status;
        }
    }

    public enum HighlightOpacityMode
    {
        TwentyFive = 25,
        Fifty = 50,
        SeventyFive = 75,
        AHundred = 100,
        None
    }
}