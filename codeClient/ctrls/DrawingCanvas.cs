using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace nsVicoClient.ctrls
{
    public class DrawingCanvas : Panel
    {
        private List<Visual> visuals = new List<Visual>();

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }

        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);

            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }

        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);

            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }
    }
}
