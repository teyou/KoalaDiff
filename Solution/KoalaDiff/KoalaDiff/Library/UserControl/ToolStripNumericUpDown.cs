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
    public class ToolStripNumericUpDown : ToolStripControlHost
    {
        public ToolStripNumericUpDown()
            : base(new NumericUpDown())
        {
            NumericUpDownControl.AutoSize = false;
            NumericUpDownControl.Maximum = 100;
            NumericUpDownControl.Size = new Size(75, 20);
        }

        public NumericUpDown NumericUpDownControl
        {
            get { return Control as NumericUpDown; }
        }


        public decimal Maximum
        {
            get { return NumericUpDownControl.Maximum; }
            set { NumericUpDownControl.Maximum = value; }
        }

        public decimal Minimum
        {
            get { return NumericUpDownControl.Minimum; }
            set { NumericUpDownControl.Minimum = value; }
        }
        public decimal Value
        {
            get { return NumericUpDownControl.Value; }
            set { NumericUpDownControl.Value = value; }
        }

        public decimal Increment
        {
            get { return NumericUpDownControl.Increment; }
            set { NumericUpDownControl.Increment = value; }
        }

        public event EventHandler ValueChanged;

        public void OnValueChanged(object sender, EventArgs e)
        {
            // not thread safe!
            if (ValueChanged != null)
            {
                ValueChanged(sender, e);
            }
        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);

            (control as NumericUpDown).ValueChanged += OnValueChanged;
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);

            (control as TrackBar).ValueChanged -= OnValueChanged;
        }
    }
}
