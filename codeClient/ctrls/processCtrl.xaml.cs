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
using System.Windows.Media.Animation;
namespace nsVicoClient.ctrls
{
    /// <summary>
    /// processCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class processCtrl : UserControl
    {
        public processCtrl()
        {
            InitializeComponent();
            //Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline),new FrameworkPropertyMetadata { DefaultValue = 20 });
            stbPrs.Stop();
        }
        public void stop()
        {
            stbPrs.Stop();
        }
        public void start()
        {
            stbPrs.Begin();
        }
    }
}
