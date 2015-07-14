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
    /// <summary>
    /// manualBtn.xaml 的交互逻辑
    /// </summary>
    public partial class manualBtn : UserControl
    {
        objUnit objManual;
        objUnit objSemiAuto;
        objUnit objAuto;

        public manualBtn()
        {
            InitializeComponent();
            objManual = valmoWin.dv.KeyPr[2];
            objSemiAuto = valmoWin.dv.KeyPr[3];
            objAuto = valmoWin.dv.KeyPr[4];

            objManual.addHandle(handleManual);
            objSemiAuto.addHandle(handleSemiAuto);
            objAuto.addHandle(handleAuto);
        }

        private void handleManual(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    {
                        btnManual.Opacity = 1;
                        btnManualActive.Visibility = Visibility.Hidden;
                    }
                    break;
                case 1:
                    {
                        btnManual.Opacity = 0;
                        btnManualActive.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void handleSemiAuto(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    {
                        btnSemiAuto.Opacity = 1;
                        btnSemiAutoActive.Visibility = Visibility.Hidden;
                    }
                    break;
                case 1:
                    {
                        btnSemiAuto.Opacity = 0;
                        btnSemiAutoActive.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void handleAuto(objUnit obj)
        {
            switch (obj.value)
            {
                case 0:
                    {
                        btnAuto.Opacity = 1;
                        btnAutoActive.Visibility = Visibility.Hidden;
                    }
                    break;
                case 1:
                    {
                        btnAuto.Opacity = 0;
                        btnAutoActive.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        bool isManualDown = false;
        private void btnManual_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isManualDown = true;
            btnManualPress.Visibility = Visibility.Visible;
            objManual.valueNew = 1;
        }

        private void btnManual_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isManualDown)
            {
                isManualDown = false;
                btnManualPress.Visibility = Visibility.Hidden;
                objManual.valueNew = 0;
            }
        }

        private void btnManual_MouseLeave(object sender, MouseEventArgs e)
        {
            btnManual_MouseUp(null, null);
        }

        bool isSemiAutoDown = false;
        private void btnSemiAuto_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isSemiAutoDown = true;
            btnSemiAutoPress.Visibility = Visibility.Visible;
            objSemiAuto.valueNew = 1;
        }

        private void btnSemiAuto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isSemiAutoDown)
            {
                isSemiAutoDown = false;
                btnSemiAutoPress.Visibility = Visibility.Hidden;
                objSemiAuto.valueNew = 0;
            }
        }

        private void btnSemiAuto_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSemiAuto_MouseUp(null, null);
        }

        bool isAutoDown = false;
        private void btnAuto_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isAutoDown = true;
            btnAutoPress.Visibility = Visibility.Visible;
            objAuto.valueNew = 1;
        }

        private void btnAuto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isAutoDown)
            {
                isAutoDown = false;
                btnAutoPress.Visibility = Visibility.Hidden;
                objAuto.valueNew = 0;
            }
        }

        private void btnAuto_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAuto_MouseUp(null, null);
        }
    }
}
