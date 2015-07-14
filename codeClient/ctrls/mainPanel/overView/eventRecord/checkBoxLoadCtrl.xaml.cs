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
using nsDataMgr;
//using interfaceMgr;
namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for checkBoxLoadCtrl.xaml
    /// </summary>
    public partial class checkBoxLoadCtrl : UserControl
    {
        public checkBoxLoadCtrl()
        {
            InitializeComponent();
        }
        public bool active
        {
            get
            {
                return imgAct.Opacity == 1;
            }
            set
            {
                imgAct.Opacity = value ? 1 : 0;
                imgUnAct.Opacity = value ? 0 : 1;
            }
        }
        public nullEvent fdStateChange
        {
            get;
            set;
        }
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgBg.Opacity = 1;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (imgBg.Opacity == 1)
            {
                active = !active;
                if (fdStateChange != null)
                {
                    fdStateChange();
                }
                imgBg.Opacity = 0;
            }
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imgBg.Opacity == 1)
            {
                imgBg.Opacity = 0;
            }
        }
    }
}
