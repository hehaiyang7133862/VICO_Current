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
using System.Windows.Threading;
using nsDataMgr;
namespace nsVicoClient.ctrls
{
    /// <summary>
    /// loadConfFileCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class loadConfFileCtrl : UserControl
    {
        DispatcherTimer dtLoad = new DispatcherTimer();
        DispatcherTimer dtShow = new DispatcherTimer();

        List<string> itemLst = new List<string>();
        int pageNr = 1;
        int fileItemNr = 0;
        List<loadFileItemCtrl> itemCtrlLst = new List<loadFileItemCtrl>();
        string fileType = "";
        public loadConfFileCtrl()
        {
            InitializeComponent();

            Label lbNum = new Label();
            lbNum.Foreground = Brushes.White;
            cvsValueLst.Children.Add(lbNum);
            for (int i = 0; i < 88; i++)
            {
                loadFileItemCtrl valueItem = new loadFileItemCtrl();
                cvsValueLst.Children.Add(valueItem);
                Canvas.SetTop(valueItem, (cvsValueLst.Children.Count - 1) * 20);
            }
            dtLoad.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dtLoad.Tick += new EventHandler(dtLoad_Tick);
            dtShow.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dtShow.Tick += new EventHandler(dtShow_Tick);
            this.Visibility = Visibility.Hidden;
        }
        void dtShow_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new nullEvent(itemAdd), null);
        }
        void dtLoad_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new nullEvent(btnLoadInvoke), null);
        }
        //int curItemNr = 1;
        void itemAdd()
        {
            if (fileItemNr < itemLst.Count)
            {
                string str = itemLst[fileItemNr];
                string[] strTmp = str.Split('\t');
                if (strTmp.Length == 2)
                {
                    loadFileItemCtrl item = new loadFileItemCtrl((fileItemNr + 1).ToString(), strTmp[0], strTmp[1]);
                    itemCtrlLst.Add(item);
                    cvsValueLst.Children.Add(item);
                    Canvas.SetTop(item, (cvsValueLst.Children.Count - 1) * 20);
                }
                else
                {
                    loadFileItemCtrl item = new loadFileItemCtrl(cvsValueLst.Children.Count.ToString(), str, "--");
                    itemCtrlLst.Add(item);
                    cvsValueLst.Children.Add(item);
                    Canvas.SetTop(item, (cvsValueLst.Children.Count - 1) * 20);
                }
                
                if (fileItemNr > 85)
                {
                    double newTop = Canvas.GetTop(cvsValueLst) - 20;
                    Canvas.SetTop(cvsValueLst, newTop);
                    lbCurPage.Content = ((int)(newTop / -85 / 20 + 1)).ToString() + "/" + pageNr;
                }
                fileItemNr++;
                progressBarShow.Value = fileItemNr * 100.0 / itemLst.Count;
                lbPerShow.Content = progressBarShow.Value.ToString("00.0") + "%";
            }
            else
            {
                dtShow.Stop();
                progressBarShow.Visibility = Visibility.Hidden;
                lbPerShow.Visibility = Visibility.Hidden;
                lbPerShow.Content = "0%";
                cvsValueLst.Height = fileItemNr * 20;
                Canvas.SetTop(cvsValueLst, 0);
                lbCurPage.Content = "1/" + pageNr;
            }
        }

        public void show(string filename)
        {
            string[] strtmp = filename.Split('.');

            if (strtmp[strtmp.Length - 1] == "mold")
                fileType = "mold";
            else if (strtmp[strtmp.Length - 1] == "inj")
                fileType = "inj";
            else if (strtmp[strtmp.Length - 1] == "sys")
                fileType = "sys";
            else
                fileType = "unKnown";
            //pageStartNr = 0;
            curFocusNr = 0;
            pageNr = 0;
            fileItemNr = 0;
            itemLst.Clear();
            progressLoad.Value = 0;
            FileStream fs = null;
            StreamReader sr = null;
            //curItemNr = 1;
            cvsValueLst.Children.Clear();
            cvsValueLst.Height = 85 * 20;
            Canvas.SetTop(cvsValueLst, 0);
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                sr = new StreamReader(fs);
                string str = sr.ReadLine();
                while (str != null)
                {
                    itemLst.Add(str);
                    str = sr.ReadLine();
                }
                if (itemLst.Count < 85)
                {
                    btnLeft.Visibility = Visibility.Hidden;
                    btnRight.Visibility = Visibility.Hidden;
                }
                else
                {
                    btnRight.IsEnabled = false;
                    btnLeft.Visibility = Visibility.Visible;
                    btnRight.Visibility = Visibility.Visible;
                }
                pageNr = itemLst.Count / 85 + 1;
                lbCurPage.Content = "1/" + pageNr;
                dtShow.Start();
                progressBarShow.Visibility = Visibility.Visible;
                lbPerShow.Visibility = Visibility.Visible;
                lbPerShow.Content = "0%";
            }
            catch(Exception ex)
            {
                vm.perror("" + ex.ToString());
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
                if (sr != null)
                    fs.Dispose();

            }
            this.Visibility = Visibility.Visible;
            this.Opacity = 1;

        }

        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            curFocusNr = 0;
            progressLoad.Value = 0;
            loadNrCount = 0;
            curStep = 0;
            dtLoad.Start();
        }

        int curStep = 0;
        private void btnLoadInvoke()
        {
            try
            {
                if (curStep == 0)
                {
                    if (fileType == "mold")
                        moldFileHeadCheck();
                    else if (fileType == "inj")
                        injFileHeadCheck();
                    else if (fileType == "sys")
                        sysFileHeadCheck();
                    curStep = 1;
                    Canvas.SetTop(cvsValueLst, 0);
                    lbCurPage.Content = "1/" + pageNr;
                }
                else if (curStep == 1)
                {
                    loadParams();
                }
                else if (curStep == 2)
                {
                    if (fileType == "mold")
                        moldFileTailCheck();
                    else if (fileType == "inj")
                        injFileTailCheck();
                    else if (fileType == "sys")
                        sysFileTailCheck();
                    
                    dtLoad.Stop();
                    MessageBox.Show("Load " + loadNrCount + " values Over!");
                    Canvas.SetTop(cvsValueLst, 0);
                    loadNrCount = 0;
                    lbCurPage.Content = "1/" + pageNr;
                }
            }
            catch (Exception ex)
            {
                vm.perror("btnLoadInvoke " + ex.ToString());
            }

        }

        int curLstNr = 0;
        int curFocusNr = 0;
        loadFileItemCtrl curItem = null;
        int loadNrCount = 0;
        

        private void loadParams()
        {
            try
            {
                int addr_int = 0;
                if (curFocusNr < itemLst.Count)
                {
                    if (curItem != null)
                        curItem.focusState = false;
                    curItem = cvsValueLst.Children[curFocusNr] as loadFileItemCtrl;

                    progressLoad.Value = (curFocusNr + 1) * 100.0 / itemLst.Count;
                    lbPer.Content = progressLoad.Value.ToString("0.0") + "%";
                    curItem.focusState = true;
                    string[] strtmp = curItem.addr.Split('.');

                    if (strtmp.Length == 2)
                    {
                        addr_int = -1;
                        if (!LinkMgr.getObjPlcAddr(curItem.addr, ref addr_int, strtmp[0]))
                        {
                            curItem.flagLoadOk = false;
                            throw new Exception("get " + curItem.addr + " addr_int error!");
                        }
                        if (addr_int == -1 || addr_int == 0)
                        {
                            curItem.flagLoadOk = false;
                        }
                        if (curItem.value != "--")
                        {
                            if (Lasal32.LslWriteToSvr(addr_int, Int32.Parse(curItem.value)))
                            {
                                int tmp = 2;
                                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                                {
                                    if (tmp == -1)
                                    {
                                        curItem.flagLoadOk = false;
                                        throw new Exception("write " + curItem.addr + " error!");
                                    }
                                    curItem.flagLoadOk = true;
                                    loadNrCount++;
                                    if (curFocusNr > 85)
                                    {
                                        double newTop = Canvas.GetTop(cvsValueLst) - 20;
                                        Canvas.SetTop(cvsValueLst, newTop);
                                        lbCurPage.Content = ((int)(newTop / -85 / 20 + 1)).ToString() + "/" + pageNr;
                                    }
                                    //vm.debug((pageStartNr + curFocusNr) + " : " + addrLst[pageStartNr + curFocusNr]);
                                }
                                else
                                {
                                    curItem.flagLoadOk = false;
                                    throw new Exception("get   " + curItem.addr + "   value error!");
                                }
                            }
                        }
                        else
                        {
                            curItem.flagLoadOk = false;
                        }
                    }
                    curLstNr++;
                    curFocusNr++;
                }
                else
                {
                    if (curItem != null)
                        curItem.focusState = false;
                    curItem = null;
                    curFocusNr = 0;
                    curStep = 2;
                }
            }
            catch (Exception ex)
            {
                if (curItem != null)
                    curItem.focusState = false;
                curLstNr = 0;
                curItem = null;
                dtLoad.Stop();

                MessageBox.Show("Load error! load " + loadNrCount + "values");
                Canvas.SetTop(cvsValueLst, 0);
                lbCurPage.Content = "1/" + pageNr;
                loadNrCount = 0;
                vm.perror("[loadConfFileCtrl.loadParams]" + ex.ToString());
            }
        }

        private void moldFileHeadCheck()
        {
            int addr_int = 0;
            int value = 1;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadMoldConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadMoldConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadMoldConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadMoldConfig  value error!");
                }
            }
        }
        private void moldFileTailCheck()
        {
            int addr_int = 0;
            int value = 0;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadMoldConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadMoldConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadMoldConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadMoldConfig  value error!");
                }
            }

            if (!LinkMgr.getObjPlcAddr("RamSourceFileMoldLoaded.Data", ref addr_int, "RamSourceFileMoldLoaded"))
            {
                throw new Exception("get RamSourceFileMoldLoaded.Data  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, 1))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == 1)
                    {
                        //bdMoldConfig.Background = Brushes.Blue;
                    }
                    else
                    {
                        throw new Exception("write MoldScrewFile1.sLoadMoldConfig 1 error! return value is " + tmp);
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadMoldConfig  value error!");
                }
                //curStep = 3;
            }
            else
            {
                throw new Exception("write MoldScrewFile1.sLoadMoldConfig 1 error!");
            }
        }

        private void injFileHeadCheck()
        {
            int addr_int = -1;
            int value = 1;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadInjeConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadInjeConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadInjeConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadInjeConfig  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadInjeConfig 1 error!");
            }
        }
        private void injFileTailCheck()
        {
            int addr_int = -1;
            int value = 0;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadInjeConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadInjeConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadInjeConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadInjeConfig  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadInjeConfig 1 error!");

            }

            if (!LinkMgr.getObjPlcAddr("RamSourceFileInjection.Data", ref addr_int, "RamSourceFileInjection"))
            {
                throw new Exception("get RamSourceFileInjection.Data  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, 1))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == 1)
                    {
                        //bdInjConfig.Background = Brushes.Blue;
                    }
                    else
                    {
                        throw new Exception("write RamSourceFileInjection.Data 1 error! return value is " + tmp);
                    }

                }
                else
                {
                    throw new Exception("get RamSourceFileInjection.Data  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "RamSourceFileInjection.Data 1 error!");

            }
        }
        private void sysFileHeadCheck()
        {
            int addr_int = -1;
            int value = 1;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadSystemConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadSystemConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadSystemConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadSystemConfig  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadSystemConfig 1 error!");

            }
        }
        private void sysFileTailCheck()
        {
            int addr_int = -1;
            int value = 0;
            if (!LinkMgr.getObjPlcAddr("MoldScrewFile1.sLoadSystemConfig", ref addr_int, "MoldScrewFile1"))
            {
                throw new Exception("get MoldScrewFile1.sLoadSystemConfig  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, value))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == -1)
                    {
                        throw new Exception("write MoldScrewFile1.sLoadSystemConfig 1 error!");
                    }
                }
                else
                {
                    throw new Exception("get MoldScrewFile1.sLoadSystemConfig  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "MoldScrewFile1.sLoadSystemConfig 1 error!");

            }

            if (!LinkMgr.getObjPlcAddr("CheckLoadRam.Data", ref addr_int, "CheckLoadRam"))
            {
                throw new Exception("get CheckLoadRam.Data  addr_int error!");
            }
            if (Lasal32.LslWriteToSvr(addr_int, 1))
            {
                int tmp = 2;
                if (Lasal32.LslReadFromSvr(addr_int, ref tmp))
                {
                    if (tmp == 1)
                    {
                        //bdSysConfig.Background = Brushes.Blue;
                    }
                    else
                    {
                        throw new Exception("write CheckLoadRam.Data 1 error! return value is " + tmp);
                    }

                }
                else
                {
                    throw new Exception("get CheckLoadRam.Data  value error!");
                }
            }
            else
            {
                throw new Exception("write " + "CheckLoadRam.Data 1 error!");

            }
        }
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            double top = Canvas.GetTop(cvsValueLst);
            Canvas.SetTop(cvsValueLst, top - 85 * 20);
            top = Canvas.GetTop(cvsValueLst);
            if (top - 85 * 20 - 1 < -cvsValueLst.Height)
                btnLeft.IsEnabled = false;
            else
                btnLeft.IsEnabled = true;
            if (top + 1 > 0)
                btnRight.IsEnabled = false;
            else
                btnRight.IsEnabled = true;
            lbCurPage.Content = ((int)(top / -85 / 20 + 1)).ToString() + "/" + pageNr;
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            double top = Canvas.GetTop(cvsValueLst);

            Canvas.SetTop(cvsValueLst, top + 85 * 20);
            top = Canvas.GetTop(cvsValueLst);
            if (top - 85 * 20 - 1 < -cvsValueLst.Height)
                btnLeft.IsEnabled = false;
            else
                btnLeft.IsEnabled = true;
            if (top + 1 > 0)
                btnRight.IsEnabled = false;
            else
                btnRight.IsEnabled = true;
            lbCurPage.Content = ((int)(top / -85 / 20 + 1)).ToString() + "/" + pageNr;
        }


    }
}
