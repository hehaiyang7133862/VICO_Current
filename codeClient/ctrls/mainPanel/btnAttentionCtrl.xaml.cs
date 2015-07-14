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

namespace nsVicoClient.ctrls
{
    public partial class btnAttentionCtrl : UserControl
    {
        private objUnit _CurPos;
        private bool _bIsMouseDown = false;

        public btnAttentionCtrl()
        {
            InitializeComponent();
        }

        public void Show(objUnit obj,string dis)
        {
            _CurPos = obj;
            if(_CurPos != null)
            {
                if (_CurPos.valueNew == 0)
                {
                    lb1.Content = valmoWin.dv.getCurDis("lanKey1107");
                }
                else
                {
                    lb1.Content = valmoWin.dv.getCurDis("lanKey1106");
                }

                lb2.Content = valmoWin.dv.getCurDis(_CurPos.serialNum) + "?";

                this.Visibility = Visibility.Visible;
            }
        }

        private void lbConfirm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;

            lbConfirm.Background = new SolidColorBrush(Color.FromArgb(255, 234, 234, 234));
        }
        private void lbConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;
                lbConfirm.Background = new SolidColorBrush(Colors.Transparent);

                if (_CurPos != null)
                    _CurPos.setValue(_CurPos.value == 1 ? 0 : 1);
                this.Visibility = Visibility.Hidden;
            }
        }

        private void lbCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;

            lbCancel.Background = new SolidColorBrush(Color.FromArgb(255, 234, 234, 234));
        }
        private void lbCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                _bIsMouseDown = false;
                lbCancel.Background = new SolidColorBrush(Colors.Transparent);

                this.Visibility = Visibility.Hidden;
            }
        }

        private void cvsBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
