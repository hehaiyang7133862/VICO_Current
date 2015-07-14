using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// AlarmHelpCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmHelpCtrl : UserControl
    {
        public AlarmHelpCtrl()
        {
            InitializeComponent();
        }

        public void init(string Cause, string Effect, string Rest)
        {
            object objCause = TryFindResource(Cause);
            if (objCause != null)
            {
                tbCause.SetResourceReference(TextBlock.TextProperty, Cause);
            }

            object objEffect = TryFindResource(Effect);
            if (objEffect != null)
            {
                tbEffect.SetResourceReference(TextBlock.TextProperty, Effect);
            }

            object objRest = TryFindResource(Rest);
            if (objRest != null)
            {
                tbRest.SetResourceReference(TextBlock.TextProperty, Rest);
            }
        }
    }
}
