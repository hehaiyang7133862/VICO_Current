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
    /// 数字键盘
    /// </summary>
    public partial class numKeyCtrl : UserControl
    {
        /// <summary>
        /// 当前对象
        /// </summary>
        private objUnit _curObj;
        /// <summary>
        /// 对象当前值
        /// </summary>
        private string numInput;
        /// <summary>
        /// 对象最大值
        /// </summary>
        private double _max;
        /// <summary>
        /// 对象最小值
        /// </summary>
        private double _min;
        /// <summary>
        /// 单位
        /// </summary>
        private string _objUnit = string.Empty;
        /// <summary>
        /// 对象描述
        /// </summary>
        private string _objSer = "内容提示";
        bool flagFirstInputValue = true;
        /// <summary>
        /// dispose函数
        /// </summary>
        private nullEvent dispose;
        bool isMouseDown = false;
        Point mousePoint;
        object mouseCtrl = null;
        public Point curPos;
        private Thickness margin;

        bool isKeyMouseDown = false;


        public numKeyCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 显示
        /// </summary>
        public void show()
        {
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;

            curPos = mainPanelCtrl.PointMouseDown;
            margin = new Thickness(curPos.X, curPos.Y, 0, 0);
            trySetCtrlPos(margin);
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        public void checkAccessLevel()
        {
            if (valmoWin.dv.users.curUser.accessLevel >= 3)
            {
                lbSer.Opacity = 1;
                lbPos.Opacity = 1;
               
            }
            else
            {
                lbSer.Opacity = 0;
                lbPos.Opacity = 0;

            }
            checkBoxItl.bIsChecked = false;
            if (valmoWin.dv.users.curUser.accessLevel >= 4)
            {
                checkBoxItl.Visibility = Visibility.Visible;
            }
            else
            {
                checkBoxItl.Visibility = Visibility.Hidden;
            }
        }
        public void hide()
        {
            this.Visibility = Visibility.Hidden;
            if (this.dispose != null)
                this.dispose();
        }
        public void trySetCtrlPos(Thickness margin)
        {
            if (margin.Left < 540)
            {
                margin.Left += 60;
            }
            else
            {
                margin.Left -= 410;
            }

            if (margin.Top < 255)
            {
                margin.Top = 0;
            }
            else if (margin.Top > valmoWin.MainPanelHeight - 255)
            {
                margin.Top = valmoWin.MainPanelHeight - 510;
            }
            else
            {
                margin.Top -= 255;
            }
           
            Thickness newMargin = new Thickness(margin.Left, margin.Top, margin.Right, margin.Bottom);
            cvsMain.Margin = newMargin;
        }
        public void setWidth(double width)
        {
            cvsBackPanel.Width = width;
            lbPanelBack.Width = width;
        }
        public void setHeight(double height)
        {
            cvsBackPanel.Height = height;
        }

        public double ctrlWidth
        {
            get
            {
                return cvsBackPanel.Width;
            }
            set
            {
                cvsBackPanel.Width = value;
                lbPanelBack.Width = value;
            }
        }
        public double ctrlHeight
        {
            get
            {
                return cvsBackPanel.Height;
            }
            set
            {
                cvsBackPanel.Height = value;
            }
        }
        
        public void init(objUnit obj, nullEvent disposeHandle)
        {
            _curObj = obj;
            _max = obj.vMaxDbl;
            _min = obj.vMinDbl;
            numInput = obj.vDblStr;
            lbSer.Content = obj.serialNum;
            _objUnit = obj.unit;

            lbMax.Content = obj.vMaxStr;
            lbMin_numKey.Content = obj.vMinStr;
            lbCur_numKey.Content = numInput;
            lbInput_numKey.Content = numInput;
            string description = valmoWin.dv.getCurDis(_curObj.serialNum);
            if (description == null)
            {
                lbDis.Content = "对象" + _curObj.serialNum + "未找到描述";
            }
            else
            {
                lbDis.Content = description;
            }
            flagFirstInputValue = true;
            if (obj.unit == "" || obj.unit == null)
                lbUnit_numKey.Content = "";
            else
                lbUnit_numKey.Content = "[" + obj.unit + "]";

            lbMax.Foreground = Brushes.Black;
            lbMin_numKey.Foreground = Brushes.Black;
            dispose = disposeHandle;

            checkAccessLevel();
            show();
        }
        
        public void init(objUnit obj, string dis, nullEvent disposeHandle)
        {
            lbInput_numKey.FontSize = 40;
            _curObj = obj;
            lbSer.Content = obj.serialNum;
            lbMax.Content = obj.vMaxStr;
            this._max = obj.vMaxDbl;
            lbMin_numKey.Content = obj.vMinStr;
            this._min = obj.vMinDbl;
            numInput = obj.vDblStr;

            lbCur_numKey.Content = numInput;
            lbInput_numKey.Content = numInput;
            lbDis.Content = dis;
            flagFirstInputValue = true;
            if (obj.unit == "")
                lbUnit_numKey.Content = "";
            else
                lbUnit_numKey.Content = "[" + obj.unit + "]";

            lbMax.Foreground = Brushes.Black;
            lbMin_numKey.Foreground = Brushes.Black;
            this.dispose = disposeHandle; 
  
            checkAccessLevel();
            show();
        }

        public void init(objUnit obj, string dis, nullEvent disposeHandle,double min,double max)
        {
            lbInput_numKey.FontSize = 40;
            _curObj = obj;
            lbSer.Content = obj.serialNum;
            this._max = max;
            lbMax.Content = _max.ToString();
            this._min = min;
            lbMin_numKey.Content = _min.ToString();
            numInput = obj.vDblStr;

            lbCur_numKey.Content = numInput;
            lbInput_numKey.Content = numInput;
            lbDis.Content = dis;
            flagFirstInputValue = true;
            if (obj.unit == "")
                lbUnit_numKey.Content = "";
            else
                lbUnit_numKey.Content = "[" + obj.unit + "]";

            lbMax.Foreground = Brushes.Black;
            lbMin_numKey.Foreground = Brushes.Black;
            this.dispose = disposeHandle;

            checkAccessLevel();
            show();
        }
        
        public bool mouseEnterFunc(double value, objUnit obj)
        {
            if (obj == null)
            {
                if (dispose != null)
                    dispose();
                return true;
            }
            if (checkBoxItl.bIsChecked)
            {
                obj.vDbl = value;
                
                this.hide();
                obj.refresh();
                return true;
            }
            else
            {
                if (obj.flagInerval)
                {
                    obj.vDblNew = value;
                    obj.refresh();
                    return true;
                }
                else
                {
                    int oldValue = obj.value;
                    if (obj.setValue(value))
                    {
                        if (obj.objType != objectType.ItlPr)
                        {
                            valmoWin.eventMgr.addParamMsg(obj.serialNum, DateTime.Now, oldValue, obj.valueNew);
                            valmoWin.refresh();
                        }
                        this.hide();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        
        private void lbNum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isKeyMouseDown = true;
            (sender as Image).Opacity = 1;
        }
        
        private void lbNum_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image curImg = sender as Image;
            switch ((sender as Image).Name)
            {
                case "img1":
                case "img2":
                case "img3":
                case "img4":
                case "img5":
                case "img6":
                case "img7":
                case "img8":
                case "img9":
                case "img0":
                    if (curImg.Opacity == 1)
                    {
                        addStr(curImg.Tag.ToString());
                    }
                    break;
                case "imgDot":
                    if (imgDot.Opacity == 1)
                    {
                        if (!flagFirstInputValue)
                        {
                            for (int i = 0; i < numInput.Length; i++)
                            {
                                if (numInput[i] == '.')
                                {
                                    imgDot.Opacity = 0;
                                    return;
                                }
                            }
                        }
                        addStr(".");
                    }
                    break;
                case "imgBack":
                    if (imgBack.Opacity == 1)
                    {
                        if (flagFirstInputValue)
                        {
                            flagFirstInputValue = false;
                        }
                        if (numInput.Length == 1)
                        {
                            numInput = "0";
                        }
                        else if (numInput.Length == 2 && numInput[0] == '-')
                        {
                            numInput = "0";
                        }
                        else
                        {
                            //string strTmp = null;
                            //for (int i = 0; i < numInput.Length - 1; i++)
                            //{
                            //    strTmp += numInput[i];
                            //}
                            numInput = numInput.Substring(0,numInput.Length - 1);
                        }
                        lbInput_numKey.Content = numInput;
                    }
                    break;
                case "imgSub":
                    if (imgSub.Opacity == 1)
                    {
                        if (flagFirstInputValue)
                        {
                            flagFirstInputValue = false;
                        }
                        if (numInput != null)
                        {
                            if (numInput[0] != '-')
                            {
                                numInput = "-" + numInput;
                            }
                            else
                            {
                                string strTmp = null;
                                for (int i = 1; i < numInput.Length; i++)
                                {
                                    strTmp += numInput[i];
                                }
                                numInput = strTmp;
                            }
                        }
                        else
                        {
                            numInput = "-0";
                        }
                        lbInput_numKey.Content = numInput;
                    }
                    break;
                case "imgOk":
                    if (imgOk.Opacity == 1)
                    {
                        if (flagFirstInputValue)
                        {
                            flagFirstInputValue = false;
                            //break;
                        }

                        if (checkValue(_curObj))
                        {
                            if (mouseEnterFunc(Double.Parse(numInput), _curObj))
                            {
                                this.hide();
                            }

                        }
                    }
                    break;
            }
            curImg.Opacity = 0;
            isKeyMouseDown = false;
        }
        
        private void addStr(string str)
        {
            if (flagFirstInputValue)
            {
                if (str == ".")
                    numInput = "0.";
                else
                    numInput = str;
                flagFirstInputValue = false;
            }
            else
            {
                if (numInput == "0")
                {
                    if (str == ".")
                        numInput = "0.";
                    else
                        numInput = str;
                }
                else if (numInput == "-0")
                {
                    if(str == ".")
                        numInput = "-0.";
                    else
                        numInput = "-" + str;
                }
                else
                {
                    if (numInput.Length > 7)
                    {
                        lbInput_numKey.FontSize = 30;
                    }
                    else
                        lbInput_numKey.FontSize = 40;
                    if(numInput.Length < 9)
                        numInput += str;

                }
            }
            lbInput_numKey.Content = numInput;
        }

        private void lbNum_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isKeyMouseDown)
                return;
            (sender as Image).Opacity = 0;
        }

        private bool checkValue(objUnit obj)
        {
            try
            {
                double curValue = Double.Parse(numInput);
                if (curValue > _max)
                {
                    lbMax.Foreground = Brushes.Red;
                }
                else if (curValue < _min)
                {
                    lbMin_numKey.Foreground = Brushes.Red;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(this.cvsBackPanel);
            mouseCtrl = sender;
            try
            {
                double a = cvsMain.Margin.Left;
                double b = cvsMain.Margin.Top;
                //double ad = Canvas.GetLeft(cvsMain);
                //double b = Canvas.GetTop(cvsMain);
                lbPos.Content = a.ToString() + "," + b.ToString();
                //Console.WriteLine(a + " " + b);
                //lbL.Content = a;
                //lbR.Content = b;
            }
            catch 
            {
                //Console.WriteLine(ex.ToString());
            }

           
        }

        private void lbPanelBack_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBackPanel);
                    //Console.WriteLine("{0},{1}", theMousePoint.X, theMousePoint.Y);
                    double tmpLeft = cvsMain.Margin.Left +theMousePoint.X - mousePoint.X;
                    double tmpTop = cvsMain.Margin.Top +theMousePoint.Y - mousePoint.Y;
                    //if (tmpLeft < 0)
                    //    tmpLeft = 0;
                    //else if (tmpLeft > 730)
                    //    tmpLeft = 730;
                    //if (tmpTop < 0)
                    //    tmpTop = 0;
                    //else if (tmpTop > 1410)
                    //    tmpTop = 1410;
                    if (tmpLeft < 0)
                        tmpLeft = 0;
                    else if (tmpLeft > ctrlWidth - cvsMain.Width)
                        tmpLeft = ctrlWidth - cvsMain.Width;
                    if (tmpTop < 0)
                        tmpTop = 0;
                    else if (tmpTop > ctrlHeight - cvsMain.Height)
                        tmpTop = ctrlHeight - cvsMain.Height;
                    cvsMain.Margin = new Thickness(tmpLeft, tmpTop, 0, 0);
                    lbPos.Content = tmpLeft + "," + tmpTop;
                    //lbL.Content = tmpLeft;
                    //lbR.Content = tmpTop;
                    mousePoint = theMousePoint;
                }

            }

        }

        private void lbPanelBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMouseDown)
                isMouseDown = false;
        }

        private void lbPanelBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hide();
        }
    }
}
