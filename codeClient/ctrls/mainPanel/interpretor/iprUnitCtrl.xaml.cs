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
using System.IO;
using System.Xml;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for iptUnitCtrl.xaml
    /// </summary>
    public partial class iprUnitCtrl : UserControl
    {
        List<iprUnitObjCtrl> lstOldCtrls = new List<iprUnitObjCtrl>();
        List<bool> lstBehindValue = new List<bool>();
        List<bool> lstUpValue = new List<bool>();
        List<bool> lstDownValue = new List<bool>();
        Label[] lbLn = new Label[22];
        bool flagStart = false;
        bool flagEnd = false;
        int startPx = -10;
        int startPy = -10;
        int endPx = -10;
        int endPy = -10;
        string curType = "";
        bool flagLnStart = false;
        bool flagLnEnd = false;
        Canvas[] cvsGrp = new Canvas[11];
        Image[] imgGrp = new Image[11];
        iprUnitObjCtrl[,] unitCtrls = new iprUnitObjCtrl[3, 10];
        public static iprUnitObjCtrl curUnitCtrl;
        int curPartNr = 0;
        double grpWidth = 90;
        string curLbLnFocusNum = "-1";
        public double width
        {
            get
            {
                return grpWidth;
            }
            set
            {
                try
                {
                    if (value >= 0 && value < 11)
                    {
                        int grpNum = (int)value;
                        if (grpNum == 0)
                        {
                            //grpNum = 1;
                            for (int i = 9; i >= 0; i--)
                            {
                                if (unitCtrls[0, i].curUnit.sActName > 0 || unitCtrls[2, i].curUnit.sActName > 0)
                                {
                                    grpNum = i;
                                    break;
                                }
                                if (unitCtrls[1, i].curUnit.sActName > 1)
                                {
                                    grpNum = i;
                                    break;
                                }
                            }
                            if (valmoWin.flagStartUpOk)
                            {
                                if (grpNum < ((iprCtrl.curUnit.sPos >> 16) & 0xff))
                                    grpNum = ((iprCtrl.curUnit.sPos >> 16) & 0xff);
                            }

                            width = grpNum + 1;
                            return;
                        }
                        else
                        {
                            for (int i = 2; i <= 10; i++)
                            {
                                if (i <= grpNum)
                                    setGrpVisible(i, true);
                                else
                                    setGrpVisible(i, false);
                            }
                            grpWidth = (grpNum + 1) * 95;
                        }

                    }
                    else if (value > 11)
                    {
                        grpWidth = value;
                        cvsMain.Width = value;
                    }
                    lbWidthUp.Width = grpWidth - 6;
                    lbWidthDown.Width = grpWidth - 6;
                }
                catch (Exception ex)
                {
                    vm.perror("[width]" + ex.Message);

                }
            }
        }

        bool flagGrpMouseDown = false;
        private void imgGrp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flagGrpMouseDown = true;
        }
        private void imgMain7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (flagGrpMouseDown)
            {
                flagGrpMouseDown = false;
                int pos = unitCtrls[1, 0].rowNr << 16 | unitCtrls[1, 0].lineNr << 8 | partNr;
                if ((iprCtrl.curUnit.sPos & 0xff) != partNr)
                    valmoWin.dv.IprPr[17].valueNew = pos;
                if (!flagOpen) //open
                {
                    flagOpen = true;
                    width = 0;
                }
                else //close
                {
                    flagOpen = false;
                    width = 10;
                    //valmoWin.SIprCtrl.iprCtrl1.refreshCtrlWidth();
                }
                iprCtrl.reDrawCtrlsHandle();

            }
        }

        private void imgMain7_MouseLeave(object sender, MouseEventArgs e)
        {
            flagGrpMouseDown = false;
        }

        private void setGrpVisible(int num,bool flagVisible)
        {
            cvsGrp[num].Visibility = flagVisible ? Visibility.Visible : Visibility.Hidden;
            imgGrp[num].Visibility = flagVisible ? Visibility.Visible : Visibility.Hidden;
            lbLn[num].Visibility = flagVisible ? Visibility.Visible : Visibility.Hidden;
            lbLn[num + 11].Visibility = flagVisible ? Visibility.Visible : Visibility.Hidden;
        }
        
        public void refreshCtrlWidth()
        {
            width = 0;
        }

        public int partNr
        {
            get
            {
                return curPartNr;
            }
            set
            {
                curPartNr = value;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        unitCtrls[i, j].partNr = value;
                    }
                }
                if (partNr == 1)
                    lnLeft.Visibility = Visibility.Hidden;
                else if (partNr == 7)
                    lnRight.Visibility = Visibility.Hidden;
            }
        }
        
        public iprUnitCtrl()
        {
            vm.testInitTmStart("\t\t\t\t[iprUnitCtrl]");
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
            {
                vm.perror("[iprUnitCtrl] " + ex.Message);
            }
            init();
            bdFocus.Visibility = Visibility.Hidden;
            width = 10;
            vm.testInitTmEnd("\t\t\t\t[iprUnitCtrl]");
        }
        public void init()
        {
            Label lbLnFocus;
            for (int i = 0; i < 22; i++)
            {
                lbLn[i] = new Label();
                lbLnFocus = lbLn[i];
                lbLnFocus.Width = 44;
                lbLnFocus.Height = 68;
                cvsMain.Children.Add(lbLnFocus);
                Canvas.SetLeft(lbLnFocus, 118 + (i % 11) * 95);
                Canvas.SetTop(lbLnFocus, 139 + (i / 11) * 114);
                if(i != 0)
                    lbLnFocus.Visibility = Visibility.Hidden;
                lbLnFocus.Tag = i;
                lbLnFocus.MouseDown += new MouseButtonEventHandler(lnFocus_MouseDown);
                lbLnFocus.MouseUp += new MouseButtonEventHandler(lbFocus_MouseUp);
                lbLnFocus.MouseEnter += new MouseEventHandler(lnFocus_MouseEnter);
                lbLnFocus.MouseLeave += new MouseEventHandler(lbFocus_MouseLeave);
            }
            cvsGrp[1] = cvsGrp1;
            cvsGrp[2] = cvsGrp2;
            cvsGrp[3] = cvsGrp3;
            cvsGrp[4] = cvsGrp4;
            cvsGrp[5] = cvsGrp5;
            cvsGrp[6] = cvsGrp6;
            cvsGrp[7] = cvsGrp7;
            cvsGrp[8] = cvsGrp8;
            cvsGrp[9] = cvsGrp9;
            cvsGrp[10] = cvsGrp10;

            imgGrp[1] = imgBg1;
            imgGrp[2] = imgBg2;
            imgGrp[3] = imgBg3;
            imgGrp[4] = imgBg4;
            imgGrp[5] = imgBg5;
            imgGrp[6] = imgBg6;
            imgGrp[7] = imgBg7;
            imgGrp[8] = imgBg8;
            imgGrp[9] = imgBg9;
            imgGrp[10] = imgBg10;

            unitCtrls[0, 0] = iprUnitObjCtrl00;
            unitCtrls[0, 1] = iprUnitObjCtrl01;
            unitCtrls[0, 2] = iprUnitObjCtrl02;
            unitCtrls[0, 3] = iprUnitObjCtrl03;
            unitCtrls[0, 4] = iprUnitObjCtrl04;
            unitCtrls[0, 5] = iprUnitObjCtrl05;
            unitCtrls[0, 6] = iprUnitObjCtrl06;
            unitCtrls[0, 7] = iprUnitObjCtrl07;
            unitCtrls[0, 8] = iprUnitObjCtrl08;
            unitCtrls[0, 9] = iprUnitObjCtrl09;

            unitCtrls[1, 0] = iprUnitObjCtrl10;
            unitCtrls[1, 1] = iprUnitObjCtrl11;
            unitCtrls[1, 2] = iprUnitObjCtrl12;
            unitCtrls[1, 3] = iprUnitObjCtrl13;
            unitCtrls[1, 4] = iprUnitObjCtrl14;
            unitCtrls[1, 5] = iprUnitObjCtrl15;
            unitCtrls[1, 6] = iprUnitObjCtrl16;
            unitCtrls[1, 7] = iprUnitObjCtrl17;
            unitCtrls[1, 8] = iprUnitObjCtrl18;
            unitCtrls[1, 9] = iprUnitObjCtrl19;

            unitCtrls[2, 0] = iprUnitObjCtrl20;
            unitCtrls[2, 1] = iprUnitObjCtrl21;
            unitCtrls[2, 2] = iprUnitObjCtrl22;
            unitCtrls[2, 3] = iprUnitObjCtrl23;
            unitCtrls[2, 4] = iprUnitObjCtrl24;
            unitCtrls[2, 5] = iprUnitObjCtrl25;
            unitCtrls[2, 6] = iprUnitObjCtrl26;
            unitCtrls[2, 7] = iprUnitObjCtrl27;
            unitCtrls[2, 8] = iprUnitObjCtrl28;
            unitCtrls[2, 9] = iprUnitObjCtrl29;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    unitCtrls[i, j].lineNr = i;
                    unitCtrls[i, j].rowNr = j;
                    unitCtrls[i, j].Tag = i * 10 + j;
                }
            }
            for (int i = 1; i < 10; i++)
            {
                unitCtrls[0, i].objBef = unitCtrls[0, i - 1];
                unitCtrls[2, i].objBef = unitCtrls[2, i - 1];
            }
            for (int i = 0; i < 9; i++)
            {
                unitCtrls[0, i].objBeh = unitCtrls[0, i + 1];
                unitCtrls[2, i].objBeh = unitCtrls[2, i + 1];
            }
            for (int i = 1; i < 10; i++)
            {
                unitCtrls[0, i].objCen = unitCtrls[1, i - 1];
                unitCtrls[2, i].objCen = unitCtrls[1, i - 1];
            }

        }
        public void setRunState(int value)
        {
            if (valmoWin.dv.IprPr[5].valueNew == 9)
            {
                for (int i = 0; i < 30; i++)
                {
                    unitCtrls[i / 10, i % 10].setRunState(getFlagValue(value, i + 1));
                }
            }
        }
        public bool getFlagValue(int value, int num)
        {
            if (num < 0 && num > 30)
                return false;
            return ((value >> num) & 0x01) == 1;
        }
        public void save(FileStream fs)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        unitCtrls[i, j].save(fs);
            //    }
            //}
            foreach (iprUnitObjCtrl ctrl in unitCtrls)
            {
                ctrl.save(fs);
            }
        }
        public void save(XmlTextWriter writer)
        {
            foreach (iprUnitObjCtrl ctrl in unitCtrls)
            {
                ctrl.save(writer);
            }
        }
        public void save(XmlNode xn,XmlDocument xmlDoc)
        {
            foreach (iprUnitObjCtrl ctrl in unitCtrls)
            {
                ctrl.save(xn,xmlDoc);
            }
        }
        public void saveUnit(FileStream fs, int nr)
        {
            unitCtrls[nr /10,nr %10].save(fs);
        }
        public iprUnitObj getUnitObj(int nr)
        {
            return unitCtrls[nr / 10, nr % 10].getUnitObj();
        }
        public static bool isFocusMouseDown = false;
        Point mousePointFocus;
        public void showFocusMsg()
        {
            int pos = iprCtrl.curUnit.sPos;
            int linkNode = iprCtrl.curUnit.get_sLinkNode();
            curUnitCtrl = unitCtrls[(pos >> 8) & 0xff, (pos >> 16) & 0xff];

            if ((((pos >> 8) & 0xff) == 0) && (((pos >> 16) & 0xff) == 0) && (pos & 0xff) == partNr)
            {
                if (((linkNode >> 2) & 0x01) == 0x01) // (0,0)位置flagStartUp为true
                {
                    iprUnitObjCtrlStart.flagUp = true;
                }
                else
                {
                    iprUnitObjCtrlStart.flagUp = false;
                }
            }
            if ((((pos >> 8) & 0xff) == 2) && (((pos >> 16) & 0xff) == 0))
            {
                if ((linkNode & 0x01) == 0x01) // (0,0)位置flagStartUp为true
                {
                    iprUnitObjCtrlStart.flagDown = true;
                }
                else
                {
                    iprUnitObjCtrlStart.flagDown = false;
                }
            }
            lbWidthUp.Visibility = Visibility.Visible;
            lbWidthDown.Visibility = Visibility.Visible;
            curUnitCtrl.setFocus();
        }
        public void refreshLan()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    unitCtrls[i, j].refreshLan();
                }
            }
            switch (curType)
            {
                case "ipr_01_main_start":
                    {

                        //imgMain1.Opacity = 1;
                        //this.partNr = 1;
                        lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey974");// "程序启动";
                    }
                    break;
                case "ipr_02_moldclose":
                    {
                        //imgMain2.Opacity = 2;
                        //this.partNr = 2;
                        lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey975");// "合模";
                    }
                    break;
                case "ipr_03_moldClamped":
                    {
                        //imgMain3.Opacity = 3;
                        //this.partNr = 3;
                        lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey976");// "合模到底";
                    }
                    break;
                case "ipr_04_injection":
                    {
                        //imgMain4.Opacity = 4;
                        //this.partNr = 4;
                        lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey977");// "注射";
                    }
                    break;
                case "ipr_05_Cooling":
                    {
                        //imgMain5.Opacity = 5;
                        //this.partNr = 5;
                        lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey978");// "冷却";
                    }
                    break;
                case "ipr_06_moldopen":
                    {
                        //imgMain6.Opacity = 6;
                        //this.partNr = 6;
                        lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey979");// "开模";
                    }
                    break;
                case "ipr_07_moldopened":
                    {
                        //imgMain7.Opacity = 7;
                        //this.partNr = 7;
                        lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey980");// "开模完成";
                    }
                    break;
            }
        }
        public void removefocus()
        {
            if (curUnitCtrl != null)
                curUnitCtrl.removeFocus();
            lbWidthUp.Visibility = Visibility.Hidden;
            lbWidthDown.Visibility = Visibility.Hidden;
        }
        public void cleanMsg()
        {
            foreach (iprUnitObjCtrl obj in unitCtrls)
            {
                obj.cleanMsg();
            }
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        unitCtrls[i, j].cleanMsg();
            //    }
            //}
        }
        public string ctrlType
        {
            get
            {
                return curType;
            }
            set
            {
                switch (value)
                {
                    case "ipr_01_main_start":
                        {
                            
                            imgMain1.Opacity = 1;
                            this.partNr = 1;
                            lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey974");// "程序启动";
                        }
                        break;
                    case "ipr_02_moldclose":
                        {
                            imgMain2.Opacity = 2;
                            this.partNr = 2;
                            lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey975");// "合模";
                        }
                        break;
                    case "ipr_03_moldClamped":
                        {
                            imgMain3.Opacity = 3;
                            this.partNr = 3;
                            lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey976");// "合模到底";
                        }
                        break;
                    case "ipr_04_injection":
                        {
                            imgMain4.Opacity = 4;
                            this.partNr = 4;
                            lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey977");// "注射";
                        }
                        break;
                    case "ipr_05_Cooling":
                        {
                            imgMain5.Opacity = 5;
                            this.partNr = 5;
                            lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey978");// "冷却";
                        }
                        break;
                    case "ipr_06_moldopen":
                        {
                            imgMain6.Opacity = 6;
                            this.partNr = 6;
                            lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey979");// "开模";
                        }
                        break;
                    case "ipr_07_moldopened":
                        {
                            imgMain7.Opacity = 7;
                            this.partNr = 7;
                            lbGrpDis.Content = valmoWin.dv.getCurDis("lanKey980");// "开模完成";
                        }
                        break;
                }
                curType = value;
                //lbGrpDis.Content = dis;
            }
        }
        bool flagOpen = true;
        bool flagLnFocus = false;
        private void lnFocus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label lbLnFocusTmp = sender as Label;
                int num = Int32.Parse(lbLnFocusTmp.Tag.ToString());
                if(num % 11 == 0)
                {
                    if (num == 0 && iprUnitObjCtrlStart.flagUp)
                    {
                        flagLnFocus = true;
                        for (int i = 0; i < 11; i++)
                        {
                            if (unitCtrls[0, i].curUnit.sActName > 1)
                            {
                                unitCtrls[0, i].getFocus = true;
                                int pos = i << 16 | 0 << 8 | partNr;
                                iprCtrl.curUnit.set_sPos(pos);
                                
                                break;
                            }
                        }
                        flagLnStart = true;
                    }
                    else if (num == 11 && iprUnitObjCtrlStart.flagDown)
                    {
                        flagLnFocus = true;
                        for (int i = 0; i < 11; i++)
                        {
                            if (unitCtrls[2, i].curUnit.sActName > 1)
                            {
                                unitCtrls[2, i].getFocus = true;
                                int pos = i << 16 | 2 << 8 | partNr;
                                iprCtrl.curUnit.set_sPos(pos);
                                break;
                            }
                        }
                        flagLnStart = true;
                    }
                }
                else
                {
                    if (num < 11)
                    {
                        if (unitCtrls[0, num - 1].flagDown  )
                        {
                            flagLnFocus = true;
                            for (int i = num - 1; i >= 0 ; i--)
                            {
                                if (unitCtrls[0, i].curUnit.sActName > 1)
                                {
                                    unitCtrls[0, i].getFocus = true;
                                    int pos = i << 16 | 0 << 8 | partNr;
                                    iprCtrl.curUnit.set_sPos(pos);
                                    break;
                                }
                            }
                            flagLnEnd = true;
                        }
                        else if(unitCtrls[1, num - 1].flagUp)
                        {
                            flagLnFocus = true;
                            for (int i = num; i < 10; i++)
                            {
                                if (unitCtrls[0, i].curUnit.sActName > 1)
                                {
                                    unitCtrls[0, i].getFocus = true;
                                    int pos = i << 16 | 0 << 8 | partNr;
                                    iprCtrl.curUnit.set_sPos(pos);
                                    break;
                                }
                            }
                            flagLnStart = true;
                        }
                    }
                    else
                    {
                        if (unitCtrls[2, num % 11 - 1].flagUp )
                        {
                            flagLnFocus = true;
                            for (int i = num%11 - 1; i >= 0; i--)
                            {
                                if (unitCtrls[2, i].curUnit.sActName > 1)
                                {
                                    unitCtrls[2, i].getFocus = true;
                                    int pos = i << 16 | 2 << 8 | partNr;
                                    iprCtrl.curUnit.set_sPos(pos);
                                    break;
                                }
                            }
                            flagLnEnd = true;
                        }
                        else if(unitCtrls[1, num % 11 - 1].flagDown)
                        {
                            flagLnFocus = true;
                            for (int i = num%11; i < 10; i++)
                            {
                                if (unitCtrls[2, i].curUnit.sActName > 1)
                                {
                                    unitCtrls[2, i].getFocus = true;
                                    int pos = i << 16 | 2 << 8 | partNr;
                                    iprCtrl.curUnit.set_sPos(pos);
                                    break;
                                }
                            }
                            flagLnStart = true;
                        }
                    }
                }
                if (flagLnFocus)
                {
                    curLbLnFocusNum = num.ToString();
                    lnFocus.Visibility = Visibility.Visible;
                    Canvas.SetLeft(lnFocus, Canvas.GetLeft(lbLnFocusTmp) + 24);
                    Canvas.SetTop(lnFocus, Canvas.GetTop(lbLnFocusTmp) - 23);
                }
            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }
        }
        private void lnFocus_MouseEnter(object sender, MouseEventArgs e)
        {
            if (flagLnFocus)
            {
                Label lbFocus = sender as Label;
                if (lbFocus.Tag.ToString() != curLbLnFocusNum)
                {
                    int num = Int32.Parse(lbFocus.Tag.ToString());
                    if (flagLnStart)
                    {
                        linkTwoCtrls(curUnitCtrl.lineNr, curUnitCtrl.rowNr, 1, num % 11);
                        //flagLnStart = false;
                    }
                    else if(flagLnEnd)
                    {
                        linkTwoCtrls(curUnitCtrl.lineNr, curUnitCtrl.rowNr, 1, num % 11 - 1);
                        //flagLnEnd = false;
                    }
                }
            }
        }
        private void lbFocus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Label lbFocus = sender as Label;
            if (lbFocus.Tag.ToString() == curLbLnFocusNum)
                return;
            lnFocus.Visibility = Visibility.Hidden;
            
            if (flagLnFocus)
            {
                flagLnFocus = false;
                if (flagLnStart)
                {
                     
                    //startPy--;
                    if (Int32.Parse(lbFocus.Tag.ToString()) % 11 <= curUnitCtrl.rowNr)
                    {
                        if (startPy == -1)
                            startPy = 99;
                        if (startPy == 10)
                            startPy = 100;
                        int startValue = startPx | (startPy << 8);
                        //valmoWin.dv.iprPr[28].valueNew = (int)(valmoWin.dv.iprPr[28].valueNew & 0xfffffff8);
                        valmoWin.dv.IprPr[29].valueNew = startValue;
                        vm.iprPrint("★★★起始关联位置 : ({0},{1})\t", startValue & 0xff, (startValue >> 8) & 0xff);
                        
                    }
                    flagLnStart = false;
                }
                if (flagLnEnd)
                {
                    if (Int32.Parse(lbFocus.Tag.ToString()) % 11 > curUnitCtrl.rowNr)
                    {
                        if (endPy == -1)
                            endPy = 99;
                        if (endPy == 10)
                            endPy = 100;
                        int endValue = endPx | (endPy << 8);
                        //valmoWin.dv.iprPr[28].valueNew = (int)(valmoWin.dv.iprPr[28].valueNew & 0xffffff1f);
                        valmoWin.dv.IprPr[30].valueNew = endValue;
                        //Console.Write("endValue : {0:X4}\t", endValue);
                        vm.iprPrint("★★★结束关联位置 : ({0},{1})\t", endValue & 0xff, (endValue >> 8) & 0xff);
                    }
                    flagLnEnd = false;
                }
                flagStart = false;
                flagEnd = false;
                startPx = -10;
                startPy = -10;
                endPx = -10;
                endPy = -10;
                curLbLnFocusNum = "-1";
            }

        }
        private void lbFocus_MouseLeave(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lstOldCtrls.Count; i++)
            {
                lstOldCtrls[i].tmpBehind = lstBehindValue[i];
                lstOldCtrls[i].tmpUp = lstUpValue[i];
                lstOldCtrls[i].tmpDown = lstDownValue[i];
            }
            lstOldCtrls.Clear();
            lstBehindValue.Clear();
            lstUpValue.Clear();
            lstDownValue.Clear();
        }
        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            flagLnFocus = false;
            lnFocus.Visibility = Visibility.Hidden;
            curLbLnFocusNum = "-1";


            if (flagGetFocus)
            {
                if (flagStart)
                {
                    if (startPy == -1)
                        startPy = 99;
                    if (startPy == 10)
                        startPy = 100;
                    int startValue = startPx | (startPy<<8);
                    valmoWin.dv.IprPr[29].valueNew = startValue;
                    vm.iprPrint("★★★起始关联位置 : ({0},{1})\t", startValue & 0xff, (startValue >> 8) & 0xff);
                }
                if (flagEnd)
                {
                    if (endPy == -1)
                        endPy = 99;
                    if (endPy == 10)
                        endPy = 100;
                    int endValue = endPx | (endPy << 8);
                    valmoWin.dv.IprPr[30].valueNew = endValue;
                    //Console.Write("endValue : {0:X4}\t", endValue);
                    vm.iprPrint("★★★结束关联位置 : ({0},{1})\t", endValue & 0xff, (endValue >> 8) & 0xff);
                }
                flagStart = false;
                flagEnd = false;
                startPx = -10;
                startPy = -10;
                endPx = -10;
                endPy = -10;
                flagGetFocus = false;
                interpretorPage.flagStartScan = true;
                vm.printLn("");
            }
            for (int i = 0; i < lstOldCtrls.Count; i++)
            {
                lstOldCtrls[i].tmpBehind = lstBehindValue[i];
                lstOldCtrls[i].tmpUp = lstUpValue[i];
                lstOldCtrls[i].tmpDown = lstDownValue[i];
            }
            lstOldCtrls.Clear();
            lstBehindValue.Clear();
            lstUpValue.Clear();
            lstDownValue.Clear();

            isFocusMouseDown = false;
            bdFocus.Visibility = Visibility.Hidden;
            if (curUnitCtrl != null)
            {
                curUnitCtrl.getFocus = false;
            }
        }
        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isFocusMouseDown)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (bdFocus.Visibility == Visibility.Hidden)
                        bdFocus.Visibility = Visibility.Visible;
                    Point theMousePoint = e.GetPosition(this.cvsMain);
                    double tmpLeft = Canvas.GetLeft(bdFocus);
                    //Console.Write ("tmpLeft : " + tmpLeft + " theMousePoint : " + theMousePoint.X + " mousePointFocus : " + mousePointFocus.X);
                    double tmpTop = Canvas.GetTop(bdFocus);
                    double left = tmpLeft + theMousePoint.X - mousePointFocus.X;
                    double top = tmpTop + theMousePoint.Y - mousePointFocus.Y;
                    //Console.Write (" set : " + left + " , " + top);
                    //if (left > 0)
                    //    left = 0;
                    //else if (left < -1 * (cvsGrp.Width) + 1000)
                    //    left = -1 * (cvsGrp.Width) + 1000;
                    Canvas.SetLeft(bdFocus, left);
                    Canvas.SetTop(bdFocus,top);
                    mousePointFocus = theMousePoint;
                }
            }
        }
        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            isFocusMouseDown = false;
            flagGetFocus = false;
            bdFocus.Visibility = Visibility.Hidden;
            if (curUnitCtrl != null)
            {
                curUnitCtrl.getFocus = false;
            }
        }

        bool flagGetFocus = false;
        private void cvsFocus_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (curUnitCtrl != null)
                {
                    if ((sender as iprUnitObjCtrl).Tag != null && curUnitCtrl.curUnit.sActName != 0)
                    {
                        int tagNum = Int32.Parse((sender as iprUnitObjCtrl).Tag.ToString());
                        iprUnitObjCtrl ctrl = unitCtrls[tagNum / 10, tagNum % 10];
                        if (ctrl.curUnit.sActName > 1)
                        {
                            flagGetFocus = true;
                            linkTwoCtrls(curUnitCtrl.lineNr,curUnitCtrl.rowNr,ctrl.lineNr,ctrl.rowNr);
                        }
                        else if (ctrl.curUnit.sActName == 1 && ctrl.lineNr == 1)
                        {
                            flagGetFocus = true;
                            linkTwoCtrls(curUnitCtrl.lineNr, curUnitCtrl.rowNr, ctrl.lineNr, ctrl.rowNr);
                        }
                    }
                }
            }
        }
        private void cvsFocus_MouseLeave(object sender, MouseEventArgs e)
        {
            iprCtrl.isFocusMouseDown = false;
            if (flagGetFocus)
            {
                flagGetFocus = false;
            } 
            for (int i = 0; i < lstOldCtrls.Count; i++)
            {
                lstOldCtrls[i].tmpBehind = lstBehindValue[i];
                lstOldCtrls[i].tmpUp = lstUpValue[i];
                lstOldCtrls[i].tmpDown = lstDownValue[i];
            }
            lstOldCtrls.Clear();
            lstBehindValue.Clear();
            lstUpValue.Clear();
            lstDownValue.Clear();

        }

        private void removeTmpLine()
        {
                for (int i = 0; i < lstOldCtrls.Count; i++)
                {
                    lstOldCtrls[i].tmpBehind = lstBehindValue[i];
                    lstOldCtrls[i].tmpUp = lstUpValue[i];
                    lstOldCtrls[i].tmpDown = lstDownValue[i];
                }
                lstOldCtrls.Clear();
                lstBehindValue.Clear();
                lstUpValue.Clear();
                lstDownValue.Clear();
        }

        /******************************************
         * basic 表示为 触发连接的ctrl
         * linker 表示为 curUnitCtrl
         ******************************************/
        private void linkTwoCtrls(int linkerPX, int linkerPY, int basicPX, int basicPY)
        {
            try
            {
                if (basicPX == 1) //第0行和2行连接第1行
                {
                    if (linkerPX != 1)
                    {
                        if (linkerPX == 0)
                        {
                            if (linkerPY < basicPY)
                            {
                                flagEnd = true;
                                endPx = 1;
                                endPy = basicPY + 1;
                                for (int i = linkerPY; i < basicPY; i++)
                                {
                                    lstOldCtrls.Add(unitCtrls[linkerPX, i]);
                                    lstUpValue.Add(unitCtrls[linkerPX, i].tmpUp);
                                    lstDownValue.Add(unitCtrls[linkerPX, i].tmpDown);
                                    lstBehindValue.Add(unitCtrls[linkerPX, i].tmpBehind);
                                    unitCtrls[linkerPX, i].tmpBehind = true;
                                }
                                //if (linkerPY != 0)
                                //{
                                lstOldCtrls.Add(unitCtrls[linkerPX, basicPY]);
                                lstUpValue.Add(unitCtrls[linkerPX, basicPY].tmpUp);
                                lstDownValue.Add(unitCtrls[linkerPX, basicPY].tmpDown);
                                lstBehindValue.Add(unitCtrls[linkerPX, basicPY].tmpBehind);
                                unitCtrls[linkerPX, basicPY].tmpDown = true;
                                //}
                                //else
                                //{
                                //    lstOldCtrls.Add(iprUnitObjCtrlStart);
                                //    lstUpValue.Add(iprUnitObjCtrlStart.tmpUp);
                                //    lstDownValue.Add(iprUnitObjCtrlStart.tmpDown);
                                //    lstBehindValue.Add(iprUnitObjCtrlStart.tmpBehind);
                                //    iprUnitObjCtrlStart.tmpDown = true;
                                //}
                            }
                            else if (linkerPY > basicPY)
                            {
                                flagStart = true;
                                startPx = 1;
                                startPy = basicPY - 1;
                                if (basicPY != 0)
                                {
                                    lstOldCtrls.Add(unitCtrls[1, basicPY - 1]);
                                    lstUpValue.Add(unitCtrls[1, basicPY - 1].tmpUp);
                                    lstDownValue.Add(unitCtrls[1, basicPY - 1].tmpDown);
                                    lstBehindValue.Add(unitCtrls[1, basicPY - 1].tmpBehind);
                                    unitCtrls[1, basicPY - 1].tmpUp = true;
                                }
                                else
                                {
                                    lstOldCtrls.Add(iprUnitObjCtrlStart);
                                    lstUpValue.Add(iprUnitObjCtrlStart.tmpUp);
                                    lstDownValue.Add(iprUnitObjCtrlStart.tmpDown);
                                    lstBehindValue.Add(iprUnitObjCtrlStart.tmpBehind);
                                    iprUnitObjCtrlStart.tmpUp = true;
                                }

                                for (int i = basicPY; i < linkerPY; i++)
                                {
                                    lstOldCtrls.Add(unitCtrls[0, i]);
                                    lstUpValue.Add(unitCtrls[0, i].tmpUp);
                                    lstDownValue.Add(unitCtrls[0, i].tmpDown);
                                    lstBehindValue.Add(unitCtrls[0, i].tmpBehind);
                                    unitCtrls[0, i].tmpBehind = true;
                                }
                            }
                            else
                            {
                                flagStart = true;
                                flagEnd = true;
                                startPx = 1;
                                startPy = basicPY - 1;
                                endPx = 1;
                                endPy = basicPY + 1;
                                if (basicPY != 0)
                                {
                                    lstOldCtrls.Add(unitCtrls[1, basicPY - 1]);
                                    lstUpValue.Add(unitCtrls[1, basicPY - 1].tmpUp);
                                    lstDownValue.Add(unitCtrls[1, basicPY - 1].tmpDown);
                                    lstBehindValue.Add(unitCtrls[1, basicPY - 1].tmpBehind);
                                    unitCtrls[1, basicPY - 1].tmpUp = true;
                                }
                                else
                                {
                                    lstOldCtrls.Add(iprUnitObjCtrlStart);
                                    lstUpValue.Add(iprUnitObjCtrlStart.tmpUp);
                                    lstDownValue.Add(iprUnitObjCtrlStart.tmpDown);
                                    lstBehindValue.Add(iprUnitObjCtrlStart.tmpBehind);
                                    iprUnitObjCtrlStart.tmpUp = true;
                                }
                                lstOldCtrls.Add(unitCtrls[0, linkerPY]);
                                lstUpValue.Add(unitCtrls[0, linkerPY].tmpUp);
                                lstDownValue.Add(unitCtrls[0, linkerPY].tmpDown);
                                lstBehindValue.Add(unitCtrls[0, linkerPY].tmpBehind);
                                unitCtrls[0, linkerPY].tmpDown = true;
                            }
                        }
                        else if (linkerPX == 2)
                        {
                            if (linkerPY < basicPY)
                            {
                                flagEnd = true;
                                endPx = 1;
                                endPy = basicPY + 1;
                                for (int i = linkerPY; i < basicPY; i++)
                                {
                                    lstOldCtrls.Add(unitCtrls[2, i]);
                                    lstUpValue.Add(unitCtrls[2, i].tmpUp);
                                    lstDownValue.Add(unitCtrls[2, i].tmpDown);
                                    lstBehindValue.Add(unitCtrls[2, i].tmpBehind);
                                    unitCtrls[2, i].tmpBehind = true;
                                }
                                lstOldCtrls.Add(unitCtrls[2, basicPY]);
                                lstUpValue.Add(unitCtrls[2, basicPY].tmpUp);
                                lstDownValue.Add(unitCtrls[2, basicPY].tmpDown);
                                lstBehindValue.Add(unitCtrls[2, basicPY].tmpBehind);
                                unitCtrls[2, basicPY].tmpUp = true;
                            }
                            else if (linkerPY > basicPY)
                            {
                                flagStart = true;
                                startPx = 1;
                                startPy = basicPY - 1;
                                if (basicPY != 0)
                                {
                                    lstOldCtrls.Add(unitCtrls[1, basicPY - 1]);
                                    lstUpValue.Add(unitCtrls[1, basicPY - 1].tmpUp);
                                    lstDownValue.Add(unitCtrls[1, basicPY - 1].tmpDown);
                                    lstBehindValue.Add(unitCtrls[1, basicPY - 1].tmpBehind);
                                    unitCtrls[1, basicPY - 1].tmpDown = true;
                                }
                                else
                                {
                                    lstOldCtrls.Add(iprUnitObjCtrlStart);
                                    lstUpValue.Add(iprUnitObjCtrlStart.tmpUp);
                                    lstDownValue.Add(iprUnitObjCtrlStart.tmpDown);
                                    lstBehindValue.Add(iprUnitObjCtrlStart.tmpBehind);
                                    iprUnitObjCtrlStart.tmpDown = true;
                                }
                                for (int i = basicPY; i < linkerPY; i++)
                                {
                                    lstOldCtrls.Add(unitCtrls[2, i]);
                                    lstUpValue.Add(unitCtrls[2, i].tmpUp);
                                    lstDownValue.Add(unitCtrls[2, i].tmpDown);
                                    lstBehindValue.Add(unitCtrls[2, i].tmpBehind);
                                    unitCtrls[2, i].tmpBehind = true;
                                }
                            }
                            else
                            {
                                flagStart = true;
                                flagEnd = true;
                                startPx = 1;
                                startPy = basicPY - 1;
                                endPx = 1;
                                endPy = basicPY + 1;
                                if (basicPY != 0)
                                {
                                    lstOldCtrls.Add(unitCtrls[1, basicPY - 1]);
                                    lstUpValue.Add(unitCtrls[1, basicPY - 1].tmpUp);
                                    lstDownValue.Add(unitCtrls[1, basicPY - 1].tmpDown);
                                    lstBehindValue.Add(unitCtrls[1, basicPY - 1].tmpBehind);
                                    unitCtrls[1, basicPY - 1].tmpDown = true;

                                }
                                else
                                {
                                    lstOldCtrls.Add(iprUnitObjCtrlStart);
                                    lstUpValue.Add(iprUnitObjCtrlStart.tmpUp);
                                    lstDownValue.Add(iprUnitObjCtrlStart.tmpDown);
                                    lstBehindValue.Add(iprUnitObjCtrlStart.tmpBehind);
                                    iprUnitObjCtrlStart.tmpDown = true;

                                }
                                lstOldCtrls.Add(unitCtrls[2, linkerPY]);
                                lstUpValue.Add(unitCtrls[2, linkerPY].tmpUp);
                                lstDownValue.Add(unitCtrls[2, linkerPY].tmpDown);
                                lstBehindValue.Add(unitCtrls[2, linkerPY].tmpBehind);
                                unitCtrls[2, linkerPY].tmpUp = true;

                            }
                        }
                    }
                }
                else // (basicPoint.X != 1) //第0行连接第0行，第2行连接第2行
                {
                    if (basicPX == linkerPX)
                    {
                        if (linkerPY < basicPY)
                        {
                            flagEnd = true;
                            endPx = linkerPX;
                            endPy = basicPY;
                            for (int i = linkerPY; i < basicPY; i++)
                            {
                                lstOldCtrls.Add(unitCtrls[linkerPX, i]);
                                lstUpValue.Add(unitCtrls[linkerPX, i].tmpUp);
                                lstDownValue.Add(unitCtrls[linkerPX, i].tmpDown);
                                lstBehindValue.Add(unitCtrls[linkerPX, i].tmpBehind);
                                unitCtrls[linkerPX, i].tmpBehind = true;
                            }
                        }
                        else if (basicPY < linkerPY)
                        {
                            flagStart = true;
                            startPx = linkerPX;
                            startPy = basicPY;
                            for (int i = basicPY; i < linkerPY; i++)
                            {
                                lstOldCtrls.Add(unitCtrls[linkerPX, i]);
                                lstUpValue.Add(unitCtrls[linkerPX, i].tmpUp);
                                lstDownValue.Add(unitCtrls[linkerPX, i].tmpDown);
                                lstBehindValue.Add(unitCtrls[linkerPX, i].tmpBehind);
                                unitCtrls[linkerPX, i].tmpBehind = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                vm.perror(ex.ToString());
            }
        }

        public void setCloseState()
        {
            flagOpen = false;
            int tmpGrpNum = 0;
            if (partNr == 7)
            {
                lnRight.Visibility = Visibility.Visible;
            }

            for (int i = 9; i >= 0; i--)
            {
                if (unitCtrls[0, i].curUnit.sActName > 0 || unitCtrls[2, i].curUnit.sActName > 0)
                {
                    tmpGrpNum = i;
                    break;
                }
                if (((unitCtrls[1, i].curUnit.sLinkNode >> 5) & 0x01) == 1 || ((unitCtrls[1, i].curUnit.sLinkNode >> 7) & 0x01) == 1)
                {
                    tmpGrpNum = i;
                    break;
                }
            }
            width = tmpGrpNum + 1;
        }

        private void cvsFocusMouseDown(object sender, MouseButtonEventArgs e)
        {
            iprUnitObjCtrl cvsFocus = sender as iprUnitObjCtrl;
            iprCtrl.isFocusMouseDown = true;

            if (cvsFocus.Tag != null)
            {
                int tagNum = Int32.Parse(cvsFocus.Tag.ToString());
                //int pos = (tagNum % 10) << 16 | (tagNum / 10) << 8 | partNr;
                //valmoWin.dv.iprPr[17].valueNew = pos;
                setFocusPos( tagNum / 10,tagNum % 10);

                vm.printLn("[焦点位置] "+partNr +"：("+(tagNum / 10) +","+( tagNum % 10)+") ");
                Canvas.SetLeft(bdFocus, Canvas.GetLeft(cvsFocus) - 2);
                Canvas.SetTop(bdFocus, Canvas.GetTop(cvsFocus) - 2);
                mousePointFocus = e.GetPosition(cvsMain);
            }
        }

        public void setFocusPos(int lineNr, int rowNr)
        {
            int pos = rowNr << 16 | lineNr << 8 | partNr;
            valmoWin.dv.IprPr[17].valueNew = pos;
        }

        private void cvsFocsuMouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.isFocusMouseDown = false;
            vm.debug((sender as iprUnitObjCtrl).curUnit.toString());
        }

        public void show()
        {
            foreach (iprUnitObjCtrl ctrl in unitCtrls)

            {
                vm.debug(ctrl.curUnit.toString());
                
            }
        }

    }
}
