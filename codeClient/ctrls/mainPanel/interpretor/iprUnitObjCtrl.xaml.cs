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
    /// Interaction logic for iptUnitObjCtrl.xaml
    /// </summary>
    public partial class iprUnitObjCtrl : UserControl
    {
        public iprUnitObj curUnit = new iprUnitObj();
        public int partNr;
        public int lineNr { get; set; }
        public int rowNr;
        public iprUnitObjCtrl objBef;
        public iprUnitObjCtrl objBeh;
        public iprUnitObjCtrl objCen;
        public tbMenuCtrl tbMenu;
        private startBehindCtrl startBehind;
        private startUpCtrl startUp;
        private startDownCtrl startDown;
        private endUpCtrl endUp;
        private endDownCtrl endDown;
        private startBehindTmpCtrl startBehindTmp;
        private startUpTmpCtrl startUpTmp;
        private startDownTmpCtrl startDownTmp;
        private endUpTmpCtrl endUpTmp;
        private endDownTmpCtrl endDownTmp;


        public bool getFocus = false;
        public bool getMouseEnterFocus = false;
        public bool flagStartUp;
        public bool flagStartMld;
        public bool flagStartDown;
        public bool flagBehind
        {
            get
            {
                return startBehind != null;
            }
            set
            {
                try
                {
                    if (value)
                    {
                        if (startBehind == null)
                        {
                            startBehind = new startBehindCtrl();
                            cvsLn.Children.Add(startBehind);
                        }
                    }
                    else
                    {
                        if (startBehind != null)
                        {
                            cvsLn.Children.Remove(startBehind);
                            startBehind = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    vm.perror("[flagBehind]" + ex.Message);
                }
            }
        }
        public bool flagUp
        {
            get
            {
                if (this.lineNr == 1)
                {
                    return startUp != null;
                    //if (cvsStart0.Visibility == Visibility.Visible)
                    //    return true;
                    //else
                    //    return false;
                }
                else if (this.lineNr == 2)
                {
                    return endUp != null;
                    //if (cvs2End.Visibility == Visibility.Visible)
                    //    return true;
                    //else
                    //    return false;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    if (this.lineNr == 1)
                    {
                        if (startUp == null)
                        {
                            startUp = new startUpCtrl();
                            cvsLn.Children.Add(startUp);
                        }
                        //cvsStart0.Visibility = Visibility.Visible;

                    }
                    else if (this.lineNr == 2)
                    {
                        if (endUp == null)
                        {
                            endUp = new endUpCtrl();
                            cvsLn.Children.Add(endUp);
                        }
                        //cvs2End.Visibility = Visibility.Visible;
                    }
                    //////if (this.curUnit.sActName == 1)
                    //////{
                    //////    imgNullUsed.Visibility = Visibility.Visible;
                    //////    lnNull.Visibility = Visibility.Visible;
                    //////}
                }
                else
                {
                    if (this.lineNr == 1)
                    {
                        if (startUp != null)
                        {
                            cvsLn.Children.Remove(startUp);
                            startUp = null;
                        }
                        //cvsStart0.Visibility = Visibility.Hidden;
                    }
                    else if (this.lineNr == 2)
                    {
                        if (endUp != null)
                        {
                            cvsLn.Children.Remove(endUp);
                            endUp = null;
                        }
                        //cvs2End.Visibility = Visibility.Hidden;

                    }
                    ////if (this.curUnit.sActName != 1)
                    ////{
                    ////    imgNullUsed.Visibility = Visibility.Hidden;
                    ////    lnNull.Visibility = Visibility.Hidden;
                    ////}
                }
            }
        }
        public bool flagDown
        {
            get
            {
                if (this.lineNr == 0)
                {
                    return endDown != null;
                    //if (cvs0End.Visibility == Visibility.Visible)
                    //    return true;
                    //else
                    //    return false;
                }
                else if (this.lineNr == 1)
                {
                    return startDown != null;
                    //if (cvsStart2.Visibility == Visibility.Visible)
                    //    return true;
                    //else
                    //    return false;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    if (this.lineNr == 0)
                    {
                        if (endDown == null)
                        {
                            endDown = new endDownCtrl();
                            cvsLn.Children.Add(endDown);
                        }
                        //cvs0End.Visibility = Visibility.Visible;
                    }
                    else if (this.lineNr == 1)
                    {
                        if (startDown == null)
                        {
                            startDown = new startDownCtrl();
                            cvsLn.Children.Add(startDown);
                        }
                        //cvsStart2.Visibility = Visibility.Visible;
                    }
                    //if (this.curUnit.sActName == 1)
                    //{
                    //    imgNullUsed.Visibility = Visibility.Visible;
                    //    lnNull.Visibility = Visibility.Visible;
                    //}
                }
                else
                {
                    if (this.lineNr == 0)
                    {
                        if (endDown != null)
                        {
                            cvsLn.Children.Remove(endDown);
                            endDown = null;
                        }
                        //cvs0End.Visibility = Visibility.Hidden;

                    }
                    else if (this.lineNr == 1)
                    {
                        if (startDown != null)
                        {
                            cvsLn.Children.Remove(startDown);
                            startDown = null;
                        }
                        //cvsStart2.Visibility = Visibility.Hidden;

                    }
                    ////if (this.curUnit.sActName != 1)
                    ////{
                    ////    imgNullUsed.Visibility = Visibility.Hidden;
                    ////    lnNull.Visibility = Visibility.Hidden;
                    ////}
                }
            }
        }
        public bool tmpBehind
        {
            get
            {
                return startBehindTmp != null;
                //if (cvsStartBehindTmp.Visibility == Visibility.Visible)
                //    return true;
                //else
                //    return false;
            }
            set
            {
                if (value)
                {
                    if (startBehindTmp == null)
                    {
                        startBehindTmp = new startBehindTmpCtrl();
                        cvsLn.Children.Add(startBehindTmp);
                    }
                    //cvsStartBehindTmp.Visibility = Visibility.Visible;
                    ////if (this.curUnit.sActName == 0)
                    ////{
                    ////    imgNullUsedTmp.Visibility = Visibility.Visible;
                    ////    lnNullTmp.Visibility = Visibility.Visible;
                    ////}
                }
                else
                {
                    if (startBehindTmp != null)
                    {
                        cvsLn.Children.Remove(startBehindTmp);
                        startBehindTmp = null;
                    }
                    //cvsStartBehindTmp.Visibility = Visibility.Hidden;
                    ////imgNullUsedTmp.Visibility = Visibility.Hidden;
                    ////lnNullTmp.Visibility = Visibility.Hidden;
                }
            }
        }
        public bool tmpUp
        {
            get
            {
                if (this.lineNr == 1)
                {
                    return startUpTmp != null;
                    //if (cvsStart0Tmp.Visibility == Visibility.Visible)
                    //    return true;
                    //else
                    //    return false;
                }
                else if (this.lineNr == 2)
                {
                    return endUpTmp != null;
                    //if (cvs2EndTmp.Visibility == Visibility.Visible)
                    //    return true;
                    //else
                    //    return false;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    if (this.lineNr == 1)
                    {
                        if (startUpTmp == null)
                        {
                            startUpTmp = new startUpTmpCtrl();
                            cvsLn.Children.Add(startUpTmp);
                        }
                        //cvsStart0Tmp.Visibility = Visibility.Visible;

                    }
                    else if (this.lineNr == 2)
                    {
                        if (endUpTmp == null)
                        {
                            endUpTmp = new endUpTmpCtrl();
                            cvsLn.Children.Add(endUpTmp);
                        }
                        //cvs2EndTmp.Visibility = Visibility.Visible;
                    }
                    ////if (iprCtrl.curUnit.sActName == 0)
                    ////{
                    ////    imgNullUsedTmp.Visibility = Visibility.Visible;
                    ////    lnNullTmp.Visibility = Visibility.Visible;
                    ////}
                }
                else
                {
                    if (this.lineNr == 1)
                    {
                        if (startUpTmp != null)
                        {
                            cvsLn.Children.Remove(startUpTmp);
                            startUpTmp = null;
                        }
                        //cvsStart0Tmp.Visibility = Visibility.Hidden;
                    }
                    else if (this.lineNr == 2)
                    {
                        if (endUpTmp != null)
                        {
                            cvsLn.Children.Remove(endUpTmp);
                            endUpTmp = null;
                        }
                        //cvs2EndTmp.Visibility = Visibility.Hidden;

                    }
                    ////imgNullUsedTmp.Visibility = Visibility.Hidden;
                    ////lnNullTmp.Visibility = Visibility.Hidden;
                }
            }
        }
        public bool tmpDown
        {
            get
            {
                if (this.lineNr == 0)
                {
                    return endDownTmp != null;
                    //if (cvs0EndTmp.Visibility == Visibility.Visible)
                    //    return true;
                    //else
                    //    return false;
                }
                else if (this.lineNr == 1)
                {
                    return startDownTmp != null;
                    //if (cvsStart2Tmp.Visibility == Visibility.Visible)
                    //    return true;
                    //else
                    //    return false;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    if (this.lineNr == 0)
                    {
                        if (endDownTmp == null)
                        {
                            endDownTmp = new endDownTmpCtrl();
                            cvsLn.Children.Add(endDownTmp);
                        }
                        //cvs0EndTmp.Visibility = Visibility.Visible;
                    }
                    else if (this.lineNr == 1)
                    {
                        if (startDownTmp == null)
                        {
                            startDownTmp = new startDownTmpCtrl();
                            cvsLn.Children.Add(startDownTmp);
                        }
                        //cvsStart2Tmp.Visibility = Visibility.Visible;
                    }
                    ////if (this.curUnit.sActName == 0)
                    ////{
                    ////    imgNullUsedTmp.Visibility = Visibility.Visible;
                    ////    lnNullTmp.Visibility = Visibility.Visible;
                    ////}
                }
                else
                {
                    if (this.lineNr == 0)
                    {
                        if (endDownTmp != null)
                        {
                            
                            cvsLn.Children.Remove(endDownTmp);
                            endDownTmp = null;
                        }
                    }
                        //cvs0EndTmp.Visibility = Visibility.Hidden;
                    else if (this.lineNr == 1)
                    {
                        cvsLn.Children.Remove(startDownTmp);
                        startDownTmp = null;
                    }
                        //cvsStart2Tmp.Visibility = Visibility.Hidden;
                    ////imgNullUsedTmp.Visibility = Visibility.Hidden;
                    ////lnNullTmp.Visibility = Visibility.Hidden;
                }
            }
        }

        public iprUnitObjCtrl()
        {
            InitializeComponent();
        }

        public void setRunState(bool flag)
        {
            imgPlay.Opacity = flag?1:0;
        }
        public void save(FileStream fs)
        {
            curUnit.sPos = rowNr << 16 | lineNr << 8 | partNr;
            curUnit.save(fs);
        }
        public void save(XmlTextWriter writer)
        {
            curUnit.sPos = rowNr << 16 | lineNr << 8 | partNr;
            curUnit.save(writer);
        }
        public void save(XmlNode xn,XmlDocument xmlDoc)
        {
            curUnit.sPos = rowNr << 16 | lineNr << 8 | partNr;
            curUnit.save(xn,xmlDoc);
        }
        public iprUnitObj getUnitObj()
        {
            return curUnit;
        }
        public void setMainCtr(string name,string dis)
        {

        }
        public void setFocus()
        {
            imgNull.Opacity = 1;
            iprCtrl.curUnit.get_sNotReady();
            if (iprCtrl.curUnit.sNotReady == 0)
            {
                imgErr.Opacity = 0;
            }
            else
            {
                imgErr.Opacity = 1;
            }
            iprCtrl.curUnit.get_sActName();
            switch (iprCtrl.curUnit.sActName)
            {
                case 0:
                    {
                        //imgNullUsed.Visibility = Visibility.Hidden;
                        //lnNull.Visibility = Visibility.Hidden;
                        if (tbMenu != null)
                        {
                            cvsMenu.Children.Remove(tbMenu);
                            tbMenu = null;
                        }
                    }
                    break;
                case (int)menuType.ipr_op01_blank:
                    {
                        if (tbMenu != null)
                        {
                            cvsMenu.Children.Remove(tbMenu);
                            tbMenu = null;
                        }
                    }
                    break;
                default:
                    {
                        if (tbMenu == null)
                        {
                            tbMenu = new tbMenuCtrl();
                            cvsMenu.Children.Add(tbMenu);
                        }
                        tbMenu.setFocus();
                    }
                    break;
            }
            int tmpPr_28 = iprCtrl.curUnit.sLinkNode;//valmoWin.dv.iprPr[28].valueNew;

            int tmpStartUp = tmpPr_28 & 0x01;
            int tmpStartMld = (tmpPr_28 >> 1) & 0x01;
            int tmpStartDown = (tmpPr_28 >> 2) & 0x01;

            flagStartMld = tmpStartMld == 1 ? true : false;
            flagStartUp = tmpStartUp == 1 ? true : false;
            flagStartDown = tmpStartDown == 1 ? true : false;

            int tmpUp = (tmpPr_28>>5)&0x01;
            int tmpMld = (tmpPr_28>>6)&0x01;
            int tmpDown = (tmpPr_28>>7)&0x01;
            flagBehind = tmpMld == 1 ? true : false;
            flagUp = tmpUp == 1 ? true : false;
            flagDown = tmpDown == 1 ? true : false;
            this.curUnit.getValue(iprCtrl.curUnit);
        }
        public void refreshLan()
        {
            if (tbMenu != null)
            {
                tbMenu.refreshLan(curUnit);
            }
        }
        public void showLn()
        {
            int tmpPr_28 = valmoWin.dv.IprPr[28].valueNew;

            int tmpStartUp = tmpPr_28 & 0x01;
            int tmpStartMld = (tmpPr_28 >> 1) & 0x01;
            int tmpStartDown = (tmpPr_28 >> 2) & 0x01;

            bool flagStartUp = tmpStartUp == 1 ? true : false;
            bool flagStartMld = tmpStartMld == 1 ? true : false;
            bool flagStartDown = tmpStartDown == 1 ? true : false;

            int tmpUp = (tmpPr_28 >> 5) & 0x01;
            int tmpMld = (tmpPr_28 >> 6) & 0x01;
            int tmpDown = (tmpPr_28 >> 7) & 0x01;
            flagUp = tmpUp == 1 ? true : false;
            flagBehind = tmpMld == 1 ? true : false;
            flagDown = tmpDown == 1 ? true : false;

            //Console.WriteLine("flagStartUp:\t{0}\nflagStartMld:\t{1}\nflagStartDown:{2}\n", flagStartUp, flagStartMld, flagStartDown);
            //Console.WriteLine("flagUp:\t{0}\nflagMld:\t{1}\nflagDown:{2}\n", flagUp, flagBehind, flagDown);

        }
        public void removeFocus()
        {
            imgNull.Opacity = 0;
        }
        public void cleanMsg()
        {
            imgErr.Opacity = 0;
            flagBehind = false;
            flagUp = false;
            flagDown = false;
            tmpBehind = false;
            tmpDown = false;
            tmpUp = false;
            imgNull.Opacity = 0;

            //imgNullUsed.Visibility = Visibility.Hidden;
            //lnNull.Visibility = Visibility.Hidden;
            cvsMenu.Children.Clear();
            tbMenu = null;
            curUnit.init();
        }

    }
}
