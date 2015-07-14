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
using nsDataMgr;
using System.Windows.Threading;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// iprLoadCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class iprLoadCtrl : UserControl
    {
        FileStream fsCurIprPrg = null;
        fileItemCtrl curItem = null;
        DirectoryInfo PrgDir = interpretorPage.prgDir;
        DirectoryInfo curDir = null;
        FileInfo loadedFileInfo; 
        public strEvent setCurPrgHandle
        {
            get;
            set;
        }

        public iprLoadCtrl()
        {
            InitializeComponent();
            PrgDir = interpretorPage.prgDir;
        }

        public void init()
        {
            tbFolders.SelectedIndex = 0;
            curDir = PrgDir;
            refreshCurFolder();
        }

        public string prgName
        {
            get
            {
                return valmoWin.SIprCtrl.prgFileName;
            }
            set
            {
                valmoWin.SIprCtrl.prgFileName = value;
            }
        }

        public bool usbEnable
        {
            set
            {
                if (value)
                {
                    imgRemoveNull.Opacity = 0;
                    imgRemove.Opacity = 1;
                }
                else
                {
                    imgRemoveNull.Opacity = 1;
                    imgRemove.Opacity = 0;
                    if (tbFolders.SelectedIndex == 1)
                    {
                        tbFolders.SelectedIndex = 0;
                        refreshFolders();
                    }
                }
            }
        }

        bool isItemMousedown = false;
        private void item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isItemMousedown = true;
        }

        private void item_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isItemMousedown)
            {
                isItemMousedown = false;
                if (curItem != null)
                    curItem.focusState = false;
                curItem = sender as fileItemCtrl;

                curItem.focusState = true;
                loadedFileInfo = curItem.fileInfo;
                if (curItem.fileInfo.Name == prgName && tbFolders.SelectedIndex == 0)
                {
                    cvsIprSave.Visibility = Visibility.Hidden;
                }
                else
                {
                    cvsIprSave.Visibility = Visibility.Visible;
                }
                iprFileInfo1.init(curItem,true);
            }
        }

        private void item_MouseLeave(object sender, MouseEventArgs e)
        {
            isItemMousedown = false;
        }

        private void item_MouseMove(object sender, MouseEventArgs e)
        {


        }
        bool ismMousedown = false;
        Point mousePoint;
        private void cvsFolder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ismMousedown = true;
            mousePoint = e.GetPosition(cvsFolderPanel);
        }

        private void cvsFolder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ismMousedown = false;
        }

        private void cvsFolder_MouseLeave(object sender, MouseEventArgs e)
        {
            ismMousedown = false;
        }

        private void cvsFolder_MouseMove(object sender, MouseEventArgs e)
        {
            if (ismMousedown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    isItemMousedown = false;
                    Point theMousePoint = e.GetPosition(this.cvsFolderPanel);
                    double curTop = Canvas.GetTop(cvsFolder);
                    double tmpTop = curTop + theMousePoint.Y - mousePoint.Y;
                    if (cvsFolderPanel.Height - curTop > cvsFolder.Height)
                        tmpTop = -cvsFolder.Height + cvsFolderPanel.Height;
                    if (tmpTop > 0)
                        tmpTop = 0;
                    Canvas.SetTop(cvsFolder, tmpTop);
                    mousePoint = theMousePoint;
                }
            }
        }

        private void lbLocal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbFolders.SelectedIndex = 0;
            curDir = interpretorPage.prgDir;
            refreshCurFolder();
        }

        private void refreshFolders()
        {
            cvsFolder.Children.Clear();
            Canvas.SetTop(cvsFolder, 0);
            bool prgFileSaved = false;
            FileInfo[] fileLst = PrgDir.GetFiles();
            foreach (FileInfo file in fileLst)
            {
                if (file.Extension == ".ipr")
                {
                    fileItemCtrl item = new fileItemCtrl(file);
                    if (file.Name == prgName)
                    {
                        curItem = item;
                        prgFileSaved = true;
                    }
                    cvsFolder.Children.Add(item);
                    Canvas.SetTop(item, (cvsFolder.Children.Count - 1) * 43);
                    item.MouseDown += item_MouseDown;
                    item.MouseUp += item_MouseUp;
                    item.MouseMove += item_MouseMove;
                    item.MouseLeave += item_MouseLeave;
                }
            }
            cvsFolder.Height = cvsFolder.Children.Count * 43;
            if (cvsFolder.Height < cvsFolderPanel.Height)
                cvsFolder.Height = cvsFolderPanel.Height;
            if (prgFileSaved)
            {
                iprFileInfo1.init(curItem,true);
                cvsIprSave.Visibility = Visibility.Hidden;
                curItem.focusState = true;
            }
            else
            {
                if (cvsFolder.Children.Count != 0)
                {
                    curItem = cvsFolder.Children[0] as fileItemCtrl;
                    iprFileInfo1.init(curItem, true);
                    cvsIprSave.Visibility = Visibility.Visible;
                    curItem.focusState = true;
                }
            }
        }

        private void refreshUsbFolders()
        {
            try
            {
                DriveInfo[] uin = DriveInfo.GetDrives();
                foreach (DriveInfo drive in uin)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        cvsFolder.Children.Clear();
                        bool flagGetUsbIprDir = false;
                        DirectoryInfo dirDriver = new DirectoryInfo(drive.Name);
                        foreach (DirectoryInfo dir in dirDriver.GetDirectories())
                        {
                            if (dir.Name == "ipr")
                            {
                                flagGetUsbIprDir = true;
                                foreach (FileInfo file in dir.GetFiles())
                                {
                                    if (file.Extension == ".ipr")
                                    {
                                        fileItemCtrl item = new fileItemCtrl(file);
                                        cvsFolder.Children.Add(item);
                                        Canvas.SetTop(item, (cvsFolder.Children.Count - 1) * 43);
                                        item.MouseDown += item_MouseDown;
                                        item.MouseUp += item_MouseUp;
                                        item.MouseMove += item_MouseMove;
                                        item.MouseLeave += item_MouseLeave;
                                    }
                                }
                                cvsFolder.Height = cvsFolder.Children.Count * 43;
                                if (cvsFolder.Height < cvsFolderPanel.Height)
                                    cvsFolder.Height = cvsFolderPanel.Height;
                                break;
                            }
                        }
                        if (!flagGetUsbIprDir)
                        {
                            DirectoryInfo dirNew = new DirectoryInfo(drive.Name + "ipr");
                            dirNew.Create();
                        }
                        break;
                    }
                }
                refreshCurIprFile();
            }
            catch
            {

            }
        }

        private void lbUsb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (imgRemove.Opacity == 1)
            {
                tbFolders.SelectedIndex = 1;
                curDir = null;
                try
                {
                    DriveInfo[] uin = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in uin)
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            if (!Directory.Exists(drive.Name + "\\ipr"))
                            {
                                Directory.CreateDirectory(drive.Name + "\\ipr");
                            }
                            curDir = new DirectoryInfo(drive.Name + "\\ipr");
                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    tbFolders.SelectedIndex = 0;
                    curDir = PrgDir;
                    vm.perror("[lbUsb_MouseDown]" + ex.ToString());
                }
                refreshCurFolder();
            }
        }

        private void refreshCurFolder()
        {
            cvsIprSave.Visibility = Visibility.Hidden;
            curItem = null;
            loadedFileInfo = null;
            cvsFolder.Children.Clear();
            Canvas.SetTop(cvsFolder, 0);
            FileInfo[] iprFiles = curDir.GetFiles();
            foreach (FileInfo file in iprFiles)
            {
                if (file.Extension == ".ipr")
                {
                    fileItemCtrl item = new fileItemCtrl(file);
                    //if (file.Name == prgName && curDir == PrgDir)
                    //{
                    //    curItem = item;
                    //    //SavedFileInfo = curItem.fileInfo;
                    //    //curItem.focusState = true;
                    //}
                    cvsFolder.Children.Add(item);
                    Canvas.SetTop(item, (cvsFolder.Children.Count - 1) * 43);
                    item.MouseDown += item_MouseDown;
                    item.MouseUp += item_MouseUp;
                    item.MouseLeave += item_MouseLeave;
                    item.mouseUp = btnDel_MouseUp;
                }
            }
            cvsFolder.Height = cvsFolder.Children.Count * 43;
            if (cvsFolder.Height < cvsFolderPanel.Height)
                cvsFolder.Height = cvsFolderPanel.Height;
            foreach (fileItemCtrl ctrl in cvsFolder.Children)
            {
                if (ctrl.fileInfo.FullName != valmoWin.SIprCtrl.prgFileinfo.FullName)
                {
                    curItem = ctrl;
                    curItem.focusState = true;
                    loadedFileInfo = ctrl.fileInfo;
                    cvsIprSave.Visibility = Visibility.Visible;
                    break;
                        
                }
            }
            //iprFileInfo1.init();

        }
        private void btnDel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SIprCtrl.confirmPanel.show(confirmDelFun, "确认删除文件" + curItem.fileInfo.Name + "?");
        }
        private void confirmDelFun()
        {
            File.Delete(curItem.fileInfo.FullName);
            refreshCurFolder();
        }

        private void refreshCurIprFile()
        {
            if (fsCurIprPrg != null)
            {
                iprFileInfo1.init();
                lbIprFile.Content = valmoWin.SIprCtrl.prgFileName;
                cvsIprSave.Visibility = Visibility.Visible;

            }
        }

        bool imgIprMousedown = false;
        private void loadIprFileFunc()
        {
            try
            {
                valmoWin.SIprCtrl.loadIprFileFunc(loadedFileInfo);
            }
            catch(Exception ex)
            {
                vm.perror("<loadIprFileFunc> " + ex.ToString());
            }
        }


    }
}
