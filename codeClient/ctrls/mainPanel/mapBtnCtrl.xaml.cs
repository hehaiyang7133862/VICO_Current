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
    /// Interaction logic for mapBtnCtrl.xaml
    /// </summary>
    public partial class mapBtnCtrl : UserControl
    {
        objUnit curObj;
        MouseEventHandler mouseUpHandle;
        public MouseEventHandler handleMouseUp
        {
            get
            {
                return mouseUpHandle;
            }
            set
            {
                mouseUpHandle = value;
            }
        }
        public mapBtnCtrl()
        {
            InitializeComponent();
        }
        public bool isUpActive
        {
            get;
            set;
        }
        public bool isActive
        {
            get;
            set;
        }
        private void numkeyDisposeFunc()
        {
            bdBg.Opacity = 0;
            isActive = false;
        }
        public string objName
        {
            set
            {
                lbCtrl.objName = value;
                curObj = lbCtrl.obj;
            }
        }
        public bool flagLenInjType
        {
            set
            {
                if (value)
                {
                    if (curObj != null)
                    {
                        if (curObj.unitType == UnitType.Len_mm)
                        {
                            curObj.unitType = UnitType.LenInj_inch;
                        }
                        else if (curObj.unitType == UnitType.Spd_mm)
                        {
                            curObj.unitType = UnitType.SpdInj_mm;
                        }
                        else if (curObj.unitType == UnitType.Acc_mm)
                        {
                            curObj.unitType = UnitType.AccInj_mm;
                        }
                    }
                }
            }
        }
        public void setObj(string value)
        {
            lbCtrl.objName = value;
            curObj = lbCtrl.obj;
        }
        public objUnit getObj()
        {
            return curObj;
        }
        public double w
        {
            set
            {
                cvsMain.Width = value;
                bdBg.Width = value;
                lbCtrl.myWidth = value;
            }
            get
            {
                return bdBg.Height;
            }
        }
        public double h
        {
            set
            {
                cvsMain.Height = value;
                bdBg.Height = value;
                lbCtrl.myHeight = value;
            }
            get
            {
                return bdBg.Height;
            }
        }
        Point curPos;
        public Point pos
        {
            set
            {
                curPos = value;
            }
        }
        public FontFamily fFamily
        {
            set
            {
                lbCtrl.myFontFamily = value;
            }
            get
            {
                return lbCtrl.myFontFamily;
            }
        }
        public double fSize
        {
            set
            {
                lbCtrl.myFontSize = value;
            }
        }

        public string numCtrlDis
        {
            get;
            set;
        }
        private bool _bIsReadOnly = false;
        public bool readOnly
        {
            set
            {
                _bIsReadOnly = value;
                if (value)
                {
                    cvsMain.Background = new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));
                }
                else
                {
                    cvsMain.Background = Brushes.White;
                }
            }
        }
        bool isMouseDown = false;

        public HorizontalAlignment fHAlign
        {
            set
            {
                lbCtrl.myHorizontalAlignment = value;
            }
        }
        public void mouseHandleActive()
        {
            if (isMouseDown)
            {
                if (curObj != null && !_bIsReadOnly)
                {
                    if (!valmoWin.dv.checkAccesslevel(curObj.accessLevel))
                        return;
                    bdBg.Opacity = 1;
                    Thickness margin = new Thickness(curPos.X, curPos.Y, 0, 0);
                    if (numCtrlDis != null)
                    {
                        string _ctrlDis = valmoWin.dv.getCurDis(numCtrlDis);
                        valmoWin.SNumKeyPanel.init(curObj, _ctrlDis, numkeyDisposeFunc);
                    }
                    else
                        valmoWin.SNumKeyPanel.init(curObj, numkeyDisposeFunc);
                }
                else
                {
                    vm.perror("[mapBtnDown] obj is null.");
                }
            }
            isMouseDown = false;
        }
        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!isUpActive)
            {
                if (isMouseDown == true)
                {
                    if (curObj != null && !_bIsReadOnly)
                    {
                        if (!valmoWin.dv.checkAccesslevel(curObj.accessLevel))
                            return;
                        bdBg.Opacity = 1;
                        Thickness margin = new Thickness(curPos.X, curPos.Y, 0, 0);
                        if (numCtrlDis != null)
                        {
                            string _ctrlDis = valmoWin.dv.getCurDis(numCtrlDis);
                            valmoWin.SNumKeyPanel.init(curObj, _ctrlDis, numkeyDisposeFunc);
                        }
                        else
                            valmoWin.SNumKeyPanel.init(curObj, numkeyDisposeFunc);
                        isActive = true;
                    }
                    else
                    {
                        vm.perror("[mapBtnDown] obj is null.");
                    }

                    isMouseDown = false;
                }
            }
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
        }
    }
}
