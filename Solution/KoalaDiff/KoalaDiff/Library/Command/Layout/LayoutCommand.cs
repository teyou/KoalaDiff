using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library.Command
{
    /// <summary>
    /// using command design pattern to handle UI layout
    /// </summary>
    public abstract class LayoutCommand : Command
    {
        protected LayoutMode _status;// = LayoutMode.None;
        protected Main _mainForm;

        /// <summary>
        /// Get current LayoutMode 
        /// </summary>
        public LayoutMode GetStatus()
        {
            return this._status;
        }
    }

    public enum LayoutMode
    {
        SideBySide,
        Overlay,
        OverlayFlicker,
        None
    }
}