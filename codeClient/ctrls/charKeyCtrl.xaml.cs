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
    public delegate void charKeyChangeEvent(string str);
    public delegate void charKeyHideEvent();

    /// <summary>
    /// Interaction logic for charKeyCtrl.xaml
    /// </summary>
    public partial class charKeyCtrl : UserControl
    {
        strEvent mouseEnterHandle;
        string curStr;
        public Label lbObj=null;
        bool flagShiftDown
        {
            get
            {
                return img_shiftLOn.Opacity == 1;

            }
            set
            {
                if(value)
                {
                    img_shiftLOff.Opacity = 0;
                    img_shiftROff.Opacity = 0;
                    img_shiftLOn.Opacity = 1;
                    img_shiftROn.Opacity = 1;

                }
                else
                {
                    img_shiftLOff.Opacity = 1;
                    img_shiftROff.Opacity = 1;
                    img_shiftLOn.Opacity = 0;
                    img_shiftROn.Opacity = 0;
                }
            }
        }
        /// <summary>
        /// 该状态表示当前用户是否在输入文件名
        /// </summary>
        public bool isSetFileName{get;set;}
        nullEvent disposeHandle = null;
        bool isSetSecretChars = false;
        public charKeyCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }
        public void setPos(double left, double top)
        {
            trySetPos(left, top);
        }
        public Point pos
        {
            set
            {
                trySetPos(value.X, value.Y);
            }
        }
        public void setHeight(double height)
        {
            cvsBackPanel.Height = height;
            lbPanelBack.Height = height;
        }
        public void setWidth(double width)
        {
            cvsBackPanel.Width = width;
            lbPanelBack.Width = width;
        }
        public void setEnterFunc(strEvent handle)
        {
            mouseEnterHandle = handle;
        }
        public void show()
        {
            img_capsOn.Opacity = 0;
            img_capsOff.Opacity = 1;
            img_shiftLOff.Opacity = 1;
            img_shiftROff.Opacity = 1;
            img_shiftLOn.Opacity = 0;
            img_shiftROn.Opacity = 0;
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }
        public void hide()
        {
            if (disposeHandle != null)
                disposeHandle();
			lbObj = null;
            this.Visibility = Visibility.Hidden;
        }
        public string dis
        {
            get
            {
                return lbDis.Content.ToString();
            }
            set
            {
                lbDis.Content = value + "：";
            }
        }
        public void init(bool isSecretChars, string oldValue, strEvent enterHandle, nullEvent disposeHandle, Thickness margin, bool flagSetFileName = false)
        {
            try
            {
                lbObj = null;
                this.isSetSecretChars = isSecretChars;
                curStr = oldValue;
                if (isSecretChars)
                {
                    string strtmp = "";
                    for (int i = 0; i < oldValue.Length; i++)
                        strtmp += "*";
                    lbValue.Content = strtmp;
                }
                else
                    lbValue.Content = oldValue;
                this.mouseEnterHandle = enterHandle;
                this.disposeHandle = disposeHandle;
                img_enter.Opacity = 0;
                isMouseDown = false;
                trySetPos(margin.Left, margin.Top);
                isSetFileName = flagSetFileName;
                show();
            }
            catch
            {

            }
        }
        public void init(bool isSecretChars, string oldValue, strEvent enterHandle, nullEvent disposeHandle, Thickness margin, Label lbobj,bool flagSetFileName = false)
        {
            try
            {
                lbObj = lbobj;
                this.isSetSecretChars = isSecretChars;
                curStr = oldValue;
                if (isSecretChars)
                {
                    string strtmp = "";
                    for (int i = 0; i < oldValue.Length; i++)
                        strtmp += "*";
                    lbValue.Content = strtmp;
                }
                else
                    lbValue.Content = oldValue;
                this.mouseEnterHandle = enterHandle;
                this.disposeHandle = disposeHandle;
                img_enter.Opacity = 0;
                isMouseDown = false;
                trySetPos(margin.Left, margin.Top);
                isSetFileName = flagSetFileName;
                show();
            }
            catch
            {

            }
        }
        void addChar(string str)
        {
            if (curStr.Length < 42)
            {
                curStr = curStr + str;
                if (isSetSecretChars)
                {
                    string tmp = "";
                    for (int i = 0; i < curStr.Length; i++)
                    {
                        tmp += "*";
                    }
                    lbValue.Content = tmp;
                    if (    lbObj!=null)
                    {
                        lbObj.Content = tmp;
                    }
                }
                else
                {
                    lbValue.Content = curStr;
                    if (lbObj != null)
                    {
                        lbObj.Content = curStr;
                    }
                }
            }
        }
        private void img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lbTip.Opacity == 1)
            {
                lbTip.Opacity = 0;
            }
            (sender as Image).Opacity = 0;
        }
        private void img_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
            {
                addChar("!");
            }
            else
            {
                addChar("1");
            }
            if (flagShiftDown)
                flagShiftDown = false;
                img_1.Opacity = 1;
        }

        private void img_2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("@");
            else
                addChar("2");
            if (flagShiftDown)
                flagShiftDown = false;
            img_2.Opacity = 1;
        }

        private void img_3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("#");
            else
                addChar("3");
            if (flagShiftDown)
                flagShiftDown = false;
            img_3.Opacity = 1;
        }

        private void img_4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("$");
            else
                addChar("4");
            if (flagShiftDown)
                flagShiftDown = false;
            img_4.Opacity = 1;
        }

        private void img_5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("%");
            else
                addChar("5");
            if (flagShiftDown)
                flagShiftDown = false;
            img_5.Opacity = 1;
        }

        private void img_6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("^");
            else
                addChar("6");
            if (flagShiftDown)
                flagShiftDown = false;
            img_6.Opacity = 1;
        }

        private void img_7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("&");
            else
                addChar("7");
            if (flagShiftDown)
                flagShiftDown = false;
            img_7.Opacity = 1;
        }

        private void img_8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
            {
                if (!isSetFileName)
                {
                    addChar("*");
                }
                else
                {
                    lbTip.Opacity = 1;
                }
            }
            else
                addChar("8");
            if (flagShiftDown)
                flagShiftDown = false;
            img_8.Opacity = 1;
        }

        private void img_9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("(");
            else
                addChar("9");
            if (flagShiftDown)
                flagShiftDown = false;
            img_9.Opacity = 1;
        }

        private void img_0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar(")");
            else
                addChar("0");
            if (flagShiftDown)
                flagShiftDown = false;
            img_0.Opacity = 1;
        }

        private void img_q_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("Q");
            else
                addChar("q");
            if (flagShiftDown)
                flagShiftDown = false;
            img_q.Opacity = 1;
        }

        private void img_w_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("W");
            else
                addChar("w");
            if (flagShiftDown)
                flagShiftDown = false;
            img_w.Opacity = 1;
        }

        private void img_e_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("E");
            else
                addChar("e");
            if (flagShiftDown)
                flagShiftDown = false;
            img_e.Opacity = 1;
        }

        private void img_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("R");
            else
                addChar("r");
            if (flagShiftDown)
                flagShiftDown = false;
            img_r.Opacity = 1;
        }

        private void img_t_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("T");
            else
                addChar("t");
            if (flagShiftDown)
                flagShiftDown = false;
            img_t.Opacity = 1;
        }

        private void img_y_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("Y");
            else
                addChar("y");
            if (flagShiftDown)
                flagShiftDown = false;
            img_y.Opacity = 1;
        }

        private void img_i_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("I");
            else
                addChar("i");
            if (flagShiftDown)
                flagShiftDown = false;
            img_i.Opacity = 1;
        }

        private void img_u_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("U");
            else
                addChar("u");
            if (flagShiftDown)
                flagShiftDown = false;
            img_u.Opacity = 1;
        }

        private void img_o_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("O");
            else
                addChar("o");
            if (flagShiftDown)
                flagShiftDown = false;
            img_o.Opacity = 1;
        }

        private void img_p_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("P");
            else
                addChar("p");
            if (flagShiftDown)
                flagShiftDown = false;
            img_p.Opacity = 1;
        }

        #region 字符 back
        private void img_back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string strTmp = "";
            for (int i = 0; i < curStr.Length - 1; i++)
            {
                strTmp += curStr[i];
            }
            curStr = strTmp;
            if (isSetSecretChars)
            {
                strTmp = "";
                for (int i = 0; i < curStr.Length; i++)
                    strTmp += "*";
            }
            vm.printLn(curStr);
            lbValue.Content = strTmp;
            if (lbObj != null)
            {
                lbObj.Content = strTmp;
            }
            img_back.Opacity = 1;
        }
        #endregion
        #region 字符 | \
        private void img_backlach_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isSetFileName)
            {
                if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                    addChar("|");
                else
                    addChar("\\");
            }
            else
            {
                lbTip.Opacity = 1;
            }
            if (flagShiftDown)
                flagShiftDown = false;
            img_backlach.Opacity = 1;
        }
        #endregion
        #region 字符 + =
        private void img_eq_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("+");
            else
                addChar("=");
            if (flagShiftDown)
                flagShiftDown = false;
            img_eq.Opacity = 1;
        }
        #endregion
        private void img_a_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("A");
            else
                addChar("a");
            if (flagShiftDown)
                flagShiftDown = false;
            img_a.Opacity = 1;
        }

        private void img_s_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("S");
            else
                addChar("s");
            if (flagShiftDown)
                flagShiftDown = false;
            img_s.Opacity = 1;
        }

        private void img_d_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("D");
            else
                addChar("d");
            if (flagShiftDown)
                flagShiftDown = false;
            img_d.Opacity = 1;
        }

        private void img_f_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("F");
            else
                addChar("f");
            if (flagShiftDown)
                flagShiftDown = false;
            img_f.Opacity = 1;
        }

        private void img_g_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("G");
            else
                addChar("g");
            if (flagShiftDown)
                flagShiftDown = false;
            img_g.Opacity = 1;
        }

        private void img_h_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("H");
            else
                addChar("h");
            if (flagShiftDown)
                flagShiftDown = false;
            img_h.Opacity = 1;
        }

        private void img_j_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("J");
            else
                addChar("j");
            if (flagShiftDown)
                flagShiftDown = false;
            img_j.Opacity = 1;
        }

        private void img_k_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("K");
            else
                addChar("k");
            if (flagShiftDown)
                flagShiftDown = false;
            img_k.Opacity = 1;
        }

        private void img_l_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("L");
            else
                addChar("l");
            if (flagShiftDown)
                flagShiftDown = false;
            img_l.Opacity = 1;
        }
        #region 字符 enter
        private void img_enter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img_enter.Opacity = 1;

            if (mouseEnterHandle != null)
            {
                mouseEnterHandle(curStr);
            }
            hide();
            this.Visibility = Visibility.Hidden;
        }
        #endregion
        private void img_z_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("Z");
            else
                addChar("z");
            if (flagShiftDown)
                flagShiftDown = false;
            img_z.Opacity = 1;
        }

        private void img_x_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("X");
            else
                addChar("x");
            if (flagShiftDown)
                flagShiftDown = false;
            img_x.Opacity = 1;
        }

        private void img_c_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("C");
            else
                addChar("c");
            if (flagShiftDown)
                flagShiftDown = false;
            img_c.Opacity = 1;
        }

        private void img_v_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("V");
            else
                addChar("v");
            if (flagShiftDown)
                flagShiftDown = false;
            img_v.Opacity = 1;
        }

        private void img_b_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("B");
            else
                addChar("b");
            if (flagShiftDown)
                flagShiftDown = false;
            img_b.Opacity = 1;
        }

        private void img_n_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("N");
            else
                addChar("n");
            if (flagShiftDown)
                flagShiftDown = false;
            img_n.Opacity = 1;
        }

        private void img_m_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("M");
            else
                addChar("m");
            if (flagShiftDown)
                flagShiftDown = false;
            img_m.Opacity = 1;
        }
        #region 字符 - _
        private void img_ln_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("_");
            else
                addChar("-");
            if (flagShiftDown)
                flagShiftDown = false;
            img_ln.Opacity = 1;
        }
        #endregion
        #region 字符 < ,
        private void img_comma_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
            {
                if (!isSetFileName)
                {
                    addChar("<");
                }
                else
                {
                    lbTip.Opacity = 1;
                }
            }
            else
                addChar(",");
            if (flagShiftDown)
                flagShiftDown = false;
            img_comma.Opacity = 1;
        }
        #endregion
        #region 字符 > .
        private void img_fs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
            {
                if (!isSetFileName)
                {
                    addChar(">");

                }
                else
                {
                    lbTip.Opacity = 1;
                }
            }
            else
                addChar(".");
            if (flagShiftDown)
                flagShiftDown = false;
            img_fs.Opacity = 1;
        }
        #endregion
        #region 字符 L shift
        private void img_shiftL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flagShiftDown = !flagShiftDown;
            img_shiftL.Opacity = 1;
        }
        #endregion
        #region 字符 R shift
        private void img_shiftR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flagShiftDown = !flagShiftDown;
            img_shiftR.Opacity = 1;
        }
        #endregion
        #region 字符 ~ `
        private void img_wave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("~");
            else
                addChar("`");
            if (flagShiftDown)
                flagShiftDown = false;
            img_wave.Opacity = 1;
        }
        #endregion
        #region 字符 ? /
        private void img_dg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isSetFileName)
            {
                if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                    addChar("?");
                else
                    addChar("/");
            }
            else
            {
                lbTip.Opacity = 1;
            }
            if (flagShiftDown)
                flagShiftDown = false;
            img_dg.Opacity = 1;
        }
        #endregion
        #region 字符 " '
        private void img_abs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
            {
                if (!isSetFileName)
                {
                    addChar("\"");
                }
                else
                {
                    lbTip.Opacity = 1;
                }
            }
            else
                addChar("'");
            if (flagShiftDown)
                flagShiftDown = false;
            img_abs.Opacity = 1;
        }
        #endregion
        #region 字符 space
        private void img_space_MouseDown(object sender, MouseButtonEventArgs e)
        {
            addChar(" ");
            img_space.Opacity = 1;
        }
        #endregion
        #region 字符 : ;
        private void img_colon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
            {
                if (!isSetFileName)
                {
                    addChar(":");
                }
                else
                {
                    lbTip.Opacity = 1;
                }
            }
            else
                addChar(";");
            if (flagShiftDown)
                flagShiftDown = false;
            img_colon.Opacity = 1;
        }
        #endregion
        #region 字符 { [
        private void img_lsb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("{");
            else
                addChar("[");
            if (flagShiftDown)
                flagShiftDown = false;
            img_lsb.Opacity = 1;
        }
        #endregion
        #region 字符 } ]
        private void img_rsb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((flagShiftDown && img_capsOn.Opacity == 0) || (!flagShiftDown && img_capsOn.Opacity == 1))
                addChar("}");
            else
                addChar("]");
            if (flagShiftDown)
                flagShiftDown = false;
            img_rsb.Opacity = 1;
        }
        #endregion
        #region 字符 capslock
        private void img_caps_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (img_capsOn.Opacity == 0)
            {
                img_capsOn.Opacity = 1;
                img_capsOff.Opacity = 0;
            }
            else
            {
                img_capsOn.Opacity = 0;
                img_capsOff.Opacity = 1;
            }
            img_caps.Opacity = 1;
        }
        #endregion
        #region 字符 ←
        private void img_left_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //img_left.Opacity = 1;
        }
        #endregion
        #region 字符 →
        private void img_right_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //img_right.Opacity = 1;
        }
        #endregion
        private void lbPanelBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hide();
        }

        bool isMouseDown = false;
        Point mousePoint;
        private void cvsChar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ////isMouseDown = true;
            ////mousePoint = e.GetPosition(cvsBackPanel);
            //vm.printLn(Canvas.GetLeft(cvsChar) + "," + Canvas.GetTop(cvsChar));
        }

        private void cvsBackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private void cvsBackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsBackPanel);
                    double tmpLeft = Canvas.GetLeft(cvsChar) + theMousePoint.X - mousePoint.X;
                    double tmpTop = Canvas.GetTop(cvsChar) + theMousePoint.Y - mousePoint.Y;
                    trySetPos(tmpLeft,tmpTop);
                    mousePoint = theMousePoint; 
                }
            }
        }
        private void trySetPos(double tmpLeft, double tmpTop)
        {
            if (tmpLeft < 0)
                tmpLeft = 0;
            else if (tmpLeft > cvsBackPanel.Width - cvsChar.Width)
                tmpLeft = cvsBackPanel.Width - cvsChar.Width;
            if (tmpTop < 0)
                tmpTop = 0;
            else if (tmpTop > cvsBackPanel.Height - cvsChar.Height)
                tmpTop = cvsBackPanel.Height - cvsChar.Height;
            Canvas.SetLeft(cvsChar, tmpLeft);
            Canvas.SetTop(cvsChar, tmpTop);
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            if (lbTip.Opacity == 1)
            {
                lbTip.Opacity = 0;
            }
            (sender as Image).Opacity = 0;
        }
    }
}
