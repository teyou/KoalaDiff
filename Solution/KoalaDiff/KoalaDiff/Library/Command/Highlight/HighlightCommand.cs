using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    /// <summary>
    /// using command design pattern to handle UI layout
    /// </summary>
    public abstract class HighlightCommand : Command
    {
        protected HighlightMode _status;// = LayoutMode.None;
        protected Main _mainForm;

        /// <summary>
        /// Get current HighlightMode 
        /// </summary>
        public HighlightMode GetStatus()
        {
            return this._status;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }

    public enum HighlightMode
    {
        On,
        Off,
        None   
    }
}