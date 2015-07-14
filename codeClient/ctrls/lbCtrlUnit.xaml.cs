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
    /// Interaction logic for lbCtrlUnit.xaml
    /// </summary>
    public partial class lbCtrlUnit : UserControl
    {
        objUnit curObj;
        public lbCtrlUnit()
        {
            dotNum = -1;
            InitializeComponent();
        }
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(stateHandle);
                    lbMain.Content = "--.-" + curObj.unit;
                    unitType = curObj.unitType;
                }
            }
        }
        public int dotNum
        {
            get;
            set;
        }
        private void stateHandle(objUnit obj)
        {
                switch (dotNum)
                {
                    case -1:
                        {
                            if (curObj.unitType == UnitType.Tm_minRD)
                            {
                                lbMain.Content = curObj.vDblStr;
                            }
                            lbMain.Content = curObj.vDblStr + curObj.unit;

                        }
                        break;
                    case 0:
                        lbMain.Content = curObj.vDbl.ToString("0") + curObj.unit;
                        break;
                    case 1:
                        lbMain.Content = curObj.vDbl.ToString("1") + curObj.unit;
                        break;
                    case 2:
                        lbMain.Content = curObj.vDbl.ToString("2") + curObj.unit;
                        break;
                    case 3:
                        lbMain.Content = curObj.vDbl.ToString("3") + curObj.unit;
                        break;
                }
        }
        public UnitType unitType
        {
            get;
            set;
        }
        public objUnit obj
        {
            get
            {
                return curObj;
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
        public double h
        {
            set
            {
                if(value > 0)
                    lbMain.Height = value;
            }
            get
            {
                return lbMain.Height;
            }
        }
        public double w
        {
            set
            {
                lbMain.Width = value;
            }
            get
            {
                return lbMain.Width;
            }
        }
        public FontFamily fFamily
        {
            set
            {
                lbMain.FontFamily = value;
            }
            get
            {
                return lbMain.FontFamily;
            }
        }
        public FontWeight fWeight
        {
            set
            {
                lbMain.FontWeight = value;
            }
            get
            {
                return lbMain.FontWeight;
            }

        }
        public double fSize
        {
            get
            {
                return lbMain.FontSize;
            }
            set
            {
                if (value > 0)
                {
                    lbMain.FontSize = value;
                }
            }
         }
        public Brush fBackground
        {
            get
            {
                return lbMain.Background;
            }
            set
            {
                lbMain.Background = value;
            }
        }
        public Brush fForeground
        {
            get
            {
                return lbMain.Foreground;
            }
            set
            {
                lbMain.Foreground = value;
            }
        }
        public HorizontalAlignment HCA
        {
            get
            {
                return lbMain.HorizontalContentAlignment;
            }
            set
            {
                lbMain.HorizontalContentAlignment = value;
            }
        }
        public VerticalAlignment VCA
        {
            get
            {
                return lbMain.VerticalContentAlignment;
            }
            set
            {
                lbMain.VerticalContentAlignment = value;
            }
        }
        public string dis
        {
            get
            {
                return lbMain.Content.ToString();
            }
            set
            {
                lbMain.Content = value;
            }
        }
        public void setObj(string value)
        {
            curObj = valmoWin.dv.getObj(value);
            if (curObj != null)
            {
                //curObj.addLb(lbMain);
                curObj.addHandle(stateHandle);
                lbMain.Content = "--.-" + curObj.unit;
            }
        }
    }
}
