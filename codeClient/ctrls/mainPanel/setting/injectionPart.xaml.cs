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
    public partial class injectionPart : UserControl
    {
        public injectionPart()
        {
            InitializeComponent();
        }

        private bool _bIsMouseDown = false;
        private Point _curMousePos;

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            _curMousePos = e.GetPosition(cvsBack);
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point tempMousePos = e.GetPosition(cvsBack);
                    double oldTop = Canvas.GetTop(cvsMain);
                    double newTop = tempMousePos.Y - _curMousePos.Y + oldTop;

                    if (newTop <= -(1630 - (valmoWin.MainPanelHeight - 195) - 20))
                        newTop = -(1630 - (valmoWin.MainPanelHeight - 195) - 20);
                    if (newTop > 0)
                        newTop = 0;
                    Canvas.SetTop(cvsMain, newTop);
                    _curMousePos = tempMousePos;
                }
            }
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = false;
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseDown = false;
        }
    }
}
