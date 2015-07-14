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
    /// Interaction logic for mapBtnCtrl2.xaml
    /// </summary>
    public partial class mapBtnCtrl2 : UserControl
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

        public mapBtnCtrl2()
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

        /// <summary>
        /// 设置对象
        /// </summary>
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
            get
            {
                if (curObj != null)
                    return curObj.unitType == UnitType.LenInj_inch || curObj.unitType == UnitType.SpdInj_mm || curObj.unitType == UnitType.AccInj_mm;
                else
                    return false;
            }
            set
            {
                try
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
                catch (Exception ex)
                {
                    vm.perror(ex.ToString());
                }
            }
        }
        public void setObj(string value)
        {
            lbCtrl.objName = value;
            curObj = lbCtrl.obj;
        }

        /// <summary>
        /// 设置控件宽度
        /// </summary>
        public double w
        {
            set
            {
                cvsMain.Width = value;
                bdBg.Width = value ;
				lbCtrl.myWidth = value;
            }
            get
            {
                return cvsMain.Width ;
            }
        }

        /// <summary>
        /// 设置控件高度
        /// </summary>
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
                return cvsMain.Height;
            }
        }

        Point curPos;
        public Point pos
        {
            set
            {
                curPos = value;
            }
            get
            {
                return curPos;
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
        private bool isMouseDown = false;
        private bool _bisMouseMove = false;
        public bool bIsMouseMove
        {
            set
            {
                _bisMouseMove = value;
            }
        }
        
        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMouseDown && !_bisMouseMove)
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
                    vm.printLn("[mapBtnDown] obj is null.");
                }
                isMouseDown = false;
            }
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bisMouseMove = false;
            isMouseDown = true;
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
    }
}
