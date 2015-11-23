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
using System.Collections.Specialized;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class keysPanel : UserControl
    {
        public static nullEvent refreshCtrlsHandle;

        public keysPanel()
        {
            InitializeComponent();

            init();

            refreshCtrlsHandle += init;
        }

        private void init()
        {
            cvsCnt.Children.Clear();
            StringCollection strLst = Properties.Settings.Default.ctrlLst;

            if (strLst != null)
            {
                int i = 0;
                foreach (string str in strLst)
                {
                    cntBtn cnt = new cntBtn();
                    cnt.Type = (ctnBtnType)Convert.ToInt32(str);
                    cvsCnt.Children.Add(cnt);
                    Canvas.SetLeft(cnt, 25 + i % 7 * 148);
                    Canvas.SetTop(cnt, 8 + i / 7 * 113);
                    i++;
                }
            }
        }

        /// <summary>
        /// 将控件设置为合适大小
        /// </summary>
        private void setCtrlMiddle()
        {
            cvsMain.Height = 593;
            cvsCnt.Height = 345;
            cvsMenuBtn.Height = 65;
            lmenu.Opacity = 1;

            valmoWin.MainPanelHeight = 1170;
        }

        /// <summary>
        /// 将控件设置为最小
        /// </summary>
        private void setCtrlMin()
        {
            cvsMain.Height = 185;
            cvsCnt.Height = 0;
            cvsMenuBtn.Height = 0;
            lmenu.Opacity = 0;

            valmoWin.MainPanelHeight = 1580;
        }

        private bool bIsFold = false;
        private void btnFold_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (bIsFold == false)
            {
                bIsFold = true;

                setCtrlMin();

                imgDown.Visibility = Visibility.Hidden;
                imgUp.Visibility = Visibility.Visible;
            }
            else
            {
                bIsFold = false;

                setCtrlMiddle();

                imgDown.Visibility = Visibility.Visible;
                imgUp.Visibility = Visibility.Hidden;
            }
        }
    }
}
