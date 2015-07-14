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
using System.Collections.Specialized;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// iprSaveCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class iprSaveCtrl : UserControl
    {
        FileStream fsCurIprPrg = null;
        fileItemCtrl curItem = null;
        DirectoryInfo PrgDir = interpretorPage.prgDir;
        DirectoryInfo curDir = null;
        confirmCtrl confirmPanel = new confirmCtrl();
        /// <summary>
        /// 需要保存的文件的信息
        /// </summary>
        FileInfo SavedFileInfo; 
        //static DirectoryInfo dirIpr = new DirectoryInfo("ipr");
        public strEvent setCurPrgHandle
        {
            get;
            set;
        }
        public iprSaveCtrl()
        {
            InitializeComponent();
            //iprFileInfo.renameHandle = renameIprFile;
            cvsMain.Children.Add(confirmPanel);
            iprFileInfo1.refreshFolderHandle = refreshCurFolder;
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
                SavedFileInfo = curItem.fileInfo;
            }
        }

        private void item_MouseLeave(object sender, MouseEventArgs e)
        {
            isItemMousedown = false;
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

        private void refreshCurFolder()
        {
            curItem = null;
            SavedFileInfo = valmoWin.SIprCtrl.prgFileinfo;
            cvsFolder.Children.Clear();
            Canvas.SetTop(cvsFolder, 0);
            FileInfo[] iprFiles = curDir.GetFiles();
            foreach (FileInfo file in iprFiles)
            {
                if (file.Extension == ".ipr")
                {
                    fileItemCtrl item = new fileItemCtrl(file);
                    if (file.Name == prgName && curDir == PrgDir)
                    {
                        curItem = item;
                        SavedFileInfo = curItem.fileInfo;
                        curItem.focusState = true;
                    }
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
            iprFileInfo1.init();

        }

        private void refreshFolders()
        {
            curDir = PrgDir;
            curItem = null;
            cvsFolder.Children.Clear();
            Canvas.SetTop(cvsFolder, 0);
            //bool prgFileSaved = false;
            FileInfo[] iprFiles = PrgDir.GetFiles();
            foreach (FileInfo file in iprFiles)
            {
                if(file.Extension == ".ipr")
                {
                    fileItemCtrl item = new fileItemCtrl(file);
                    if (file.Name == prgName && curDir == PrgDir)
                    {
                        curItem = item;
                        //prgFileSaved = true;
                        curItem.focusState = true;
                    }
                    cvsFolder.Children.Add(item);
                    Canvas.SetTop(item, (cvsFolder.Children.Count - 1) * 43);
                    item.MouseDown += item_MouseDown;
                    item.MouseUp += item_MouseUp;
                    //item.MouseMove += item_MouseMove;
                    item.MouseLeave += item_MouseLeave;
                }
            }
            cvsFolder.Height = cvsFolder.Children.Count * 43;
            if (cvsFolder.Height < cvsFolderPanel.Height)
                cvsFolder.Height = cvsFolderPanel.Height;

            iprFileInfo1.init();
            //if (prgFileSaved)
            //{
            //    //iprFileInfo1.init(curItem);
            //    //cvsIprSave.Visibility = Visibility.Visible;
            //    //btnDel.Visibility = Visibility.Hidden;
            //    curItem.focusState = true;
            //}
            //else
            //{
            //    if (cvsFolder.Children.Count != 0)
            //    {
            //        curItem = cvsFolder.Children[0] as fileItemCtrl;
            //        iprFileInfo1.init(curItem);
            //        cvsIprSave.Visibility = Visibility.Hidden;
            //        btnDel.Visibility = Visibility.Visible;
            //        curItem.focusState = true;
            //    }
            //}
        }
        DirectoryInfo usbDir;
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
                                usbDir = dir;
                                foreach (FileInfo file in dir.GetFiles())
                                {
                                    if (file.Extension == ".ipr")
                                    {
                                        fileItemCtrl item = new fileItemCtrl(file);
                                        cvsFolder.Children.Add(item);
                                        Canvas.SetTop(item, (cvsFolder.Children.Count - 1) * 43);
                                        item.MouseDown += item_MouseDown;
                                        item.MouseUp += item_MouseUp;
                                        //item.MouseMove += item_MouseMove;
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
                //refreshCurIprFile();
            }
            catch
            {

            }
        }

        private void lbLocal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbFolders.SelectedIndex = 0;
            curDir = PrgDir;
            refreshFolders();
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
                            interpretorPage.prgDir = curDir;
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

        bool flagSaveAsDown = false;

        private void saveAsFunc()
        {
            try
            {
                flagSaveAsDown = false;
                valmoWin.SCharKeyPanel.dis = "文件名";
                string filename = valmoWin.SIprCtrl.prgFileName;
                valmoWin.SCharKeyPanel.init(false, filename, saveNewPrgFile, null, new Thickness(0, 1250, 0, 0));
            }
            catch (Exception ex)
            {
                vm.perror("[imgSaveAs_MouseUp] " + ex.ToString());
            }
        }
        string savedFilename = "";
        public void saveNewPrgFile(string newFilename)
        {
            SavedFileInfo = new FileInfo(curDir + "\\" + newFilename);
            if (SavedFileInfo.Exists)
            {
                valmoWin.SFileCoveredAlarmPanel.init(saveCoverFile, new Point(390, 720));
            }
            else
            {
                saveCoverFile();
            }
        }
        /// <summary>
        /// 直接保存savedFileInfo对应的文件
        /// </summary>
        private void saveCoverFile()
        {
            try
            {
                StringCollection prgMsg = new StringCollection();
                savedFilename = valmoWin.SIprCtrl.saveCurPrgToFile(SavedFileInfo.Name);
                prgMsg.Add(savedFilename);
                prgMsg.Add(DateTime.Now.ToString());
                prgMsg.Add(valmoWin.SCurUser.name);
                prgMsg.Add(DateTime.Now.ToString());
                prgMsg.Add("");
                
                valmoWin.SIprCtrl.prgFileName = SavedFileInfo.Name;
                valmoWin.SIprCtrl.prgMsg = prgMsg;
                refreshCurFolder();
                
            }
            catch(Exception ex)
            {
                vm.perror("[saveCoverFile] (" + tbFolders.SelectedIndex + ") " + ex.ToString());
            }
        }
        bool imgIprMousedown = false;
        private void saveFileFunc()
        {
            try
            {
                saveCoverFile();
            }
            catch (Exception ex)
            {
                vm.perror("[imgIprSave_MouseUp] " + ex.ToString());
            }
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

    }
}
