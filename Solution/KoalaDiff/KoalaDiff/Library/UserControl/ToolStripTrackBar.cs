using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;

namespace Layout.UserControl
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripTrackBar : ToolStripControlHost
    {
        public ToolStripTrackBar()
            : base(new TrackBar())
        {
            TrackBarControl.AutoSize = false;
            TrackBarControl.Maximum = 100;
            TrackBarControl.Size = new Size(200, 20);
            TrackBarControl.TickStyle = System.Windows.Forms.TickStyle.None;
        }

        public TrackBar TrackBarControl
        {
            get { return Control as TrackBar; }
        }

        public TickStyle TickStyle
        {
            get { return TrackBarControl.TickStyle; }
            set { TrackBarControl.TickStyle = value; }
        }

        public int Maximum
        {
            get { return TrackBarControl.Maximum; }
            set { TrackBarControl.Maximum = value; }
        }

        public int Minimum
        {
            get { return TrackBarControl.Minimum; }
            set { TrackBarControl.Minimum = value; }
        }
        public int Value
        {
            get { return TrackBarControl.Value; }
            set { TrackBarControl.Value = value; }
        }
        public override bool Enabled
        {
            get { return TrackBarControl.Enabled; }
            set { TrackBarControl.Enabled = value; }
        }
        public int SmallChange
        {
            get { return TrackBarControl.SmallChange; }
            set { TrackBarControl.SmallChange = value; }
        }
        public int LargeChange
        {
            get { return TrackBarControl.LargeChange; }
            set { TrackBarControl.LargeChange = value; }
        }

        public event EventHandler ValueChanged;
        public event EventHandler ScrollChanged;

        public void OnValueChanged(object sender, EventArgs e)
        {
            // not thread safe!
            if (ValueChanged != null)
            {
                ValueChanged(sender, e);
            }
        }

        public void OnScrollChanged(object sender, EventArgs e)
        {
            if (ScrollChanged != null)
            {
                ScrollChanged(sender, e);
            }
        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control); 

            (control as TrackBar).ValueChanged += OnValueChanged;
            (control as TrackBar).Scroll += OnScrollChanged;        
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control); 
            
            (control as TrackBar).ValueChanged -= OnValueChanged;
            (control as TrackBar).Scroll -= OnScrollChanged;
        }
    }
}
