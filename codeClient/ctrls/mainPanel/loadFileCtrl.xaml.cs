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
using System.Windows.Interop;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    //public delegate void confirmEvent2(string str);
    public delegate void cancleEvent();
    /// <summary>
    /// Interaction logic for loadFileCtrl.xaml
    /// </summary>
    public partial class loadFileCtrl : UserControl
    {
        public strEvent confirmHandle
        {
            get;
            set;
        }
        public nullEvent disposeHandle
        {
            set;
            get;
        }
        string winDis = "";
        string uPanName;
        charKeyCtrl keyPanel = new charKeyCtrl();
        string strFilter = "";
        public loadFileCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
            cvsMain.Children.Add(keyPanel);
            keyPanel.setEnterFunc(confirmFunc);
        }
        string fileName = "";
        public void confirmFunc(string value)
        {
            fileName = value;
            lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + fileName;
        }
        public void show()
        {
            fileName = "";
            lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + fileName;
            refreshUpan();
            btnLocal_Click(null, null);
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }
        public void hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        public void init()
        {
            refreshUpan();
            btnLocal_Click(null, null);
            this.Visibility = Visibility.Visible;
        }
        public void init(string dis, strEvent confirmHandle, nullEvent disposeHandle)
        {
            this.confirmHandle = confirmHandle;
            this.disposeHandle = disposeHandle;
            winDis = dis;
            lbWinDis.Content = valmoWin.dv.getCurDis("lanKey1022") + " " + dis;
            refreshUpan();
            btnLocal_Click(null, null);
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }
        public void init(string dis, string filterStr, strEvent confirmHandle, nullEvent disposeHandle)
        {
            this.confirmHandle = confirmHandle;
            this.disposeHandle = disposeHandle;
            winDis = dis;
            strFilter = filterStr;
            lbWinDis.Content = valmoWin.dv.getCurDis("lanKey1022") + " " + dis;
            refreshUpan();
            btnLocal_Click(null, null);
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
        }
        public void refreshUpan()
        {
            uPanName = null;
            DriveInfo[] uin = DriveInfo.GetDrives();
            foreach (DriveInfo drive in uin)
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    uPanName = drive.Name.ToString();
                    vm.printLn("U盘已插入，盘符为:" + drive.Name.ToString());
                    break;
                }
            }
            if (uPanName == null)
            {
                btnUPan.IsEnabled = false;
            }
            else
            {
                btnUPan.IsEnabled = true;
            }
        }
        public void setPos(double left, double top)
        {
            Canvas.SetLeft(cvsGrp, left);
            Canvas.SetTop(cvsGrp, top);
        }
        public void setHeight(double height)
        {
            cvsMain.Height = height;
            lbBg.Height = height;
            keyPanel.setHeight(height);
        }
        public double h
        {
            get
            {
                return cvsMain.Height;
            }
            set
            {
                cvsMain.Height = value;
                lbBg.Height = value;
                keyPanel.setHeight(value);
            }
        }
        public void setWidth(double width)
        {
            cvsMain.Width = width;
            lbBg.Width = width;
            keyPanel.setWidth(width);
        }
        public double w
        {
            get
            {
                return cvsMain.Width;
            }
            set
            {
                cvsMain.Width = value;
                lbBg.Width = value;
                keyPanel.setWidth(value);
            }
        }
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            refreshUpan();
        }
        string curPath = "";
        string pathName = ".";

        private void btnLocal_Click(object sender, RoutedEventArgs e)
        {
            curPath = "L";
            cvsFile.Children.Clear();
            if (Directory.Exists("Conf"))
            {
                folderCtrl fd = new folderCtrl();
                fd.lb.Content = "Conf";
                fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                cvsFile.Children.Add(fd);
                Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
            }
            if (Directory.Exists("Rec"))
            {
                folderCtrl fd = new folderCtrl();
                fd.lb.Content = "Rec";
                fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                cvsFile.Children.Add(fd);
                Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
            }
            if (Directory.Exists("Ipr"))
            {
                folderCtrl fd = new folderCtrl();
                fd.lb.Content = "Ipr";
                fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                cvsFile.Children.Add(fd);
                Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
            }
            lbPath.Content = ".";
            fileName = "";
            lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + fileName;
            pathName = ".";
            btnSelect.IsEnabled = false;
        }
        private void folder_MouseDown(object sender, EventArgs e)
        {
            cvsFile.Children.Clear();
            //string pathName = "";
            if (curPath == "L")
            {
                pathName = ".\\" + (sender as folderCtrl).lb.Content.ToString();
            }
            else
            {
                if (uPanName != null)
                {
                    pathName = uPanName + (sender as folderCtrl).lb.Content.ToString();
                }
                else
                {
                    cvsFile.Children.Clear();
                    btnUPan.IsEnabled = false;
                }
            }
            foreach (string d in Directory.GetFileSystemEntries(pathName))
            {
                if (File.Exists(d))
                {
                    if (strFilter != "")
                    {
                        if (d.Length > strFilter.Length)
                        {
                            if (d.Substring(d.Length - strFilter.Length, strFilter.Length) != strFilter)
                                continue;
                        }
                        else
                            continue;
                    }
                    fileSelectItemCtrl ctrlItem = new fileSelectItemCtrl();
                    cvsFile.Children.Add(ctrlItem);
                    Canvas.SetTop(ctrlItem, (cvsFile.Children.Count - 1) * 40);
                    ctrlItem.lb.Content = d;
                    ctrlItem.MouseDown += new MouseButtonEventHandler(fileCtrl_MouseDown);
                }
            }
            if (cvsFile.Children.Count > 0)
            {
                fileSelectItemCtrl item = cvsFile.Children[0] as fileSelectItemCtrl;
                item.state = true;
                fileName = item.lb.Content.ToString();
                lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + fileName;
                btnSelect.IsEnabled = true;
            }
            else
            {
                if (curPath == "L")
                {
                    lbPath.Content = ".\\" + pathName;
                }
                else
                {
                    if (uPanName == null)
                    {
                        fileName = "";
                        lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + "";
                    }
                    else
                    {
                        fileName = "";
                        lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + "";
                    }
                }
                btnSelect.IsEnabled = false;
            }
            lbPath.Content = pathName;
        }
        private void fileCtrl_MouseDown(object sender, EventArgs e)
        {
            fileSelectItemCtrl item = sender as fileSelectItemCtrl;
            if (item.state)
            {
                return;
            }
            for (int i = 0; i < cvsFile.Children.Count; i++)
            {
                (cvsFile.Children[i] as fileSelectItemCtrl).state = false;
            }
            item.state = true;
            if (curPath == "L")
            {
                fileName = item.lb.Content.ToString();
                lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + fileName;
            }
            else
            {
                fileName = item.lb.Content.ToString();
                lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + fileName;
            }
        }

        private void btnUPan_Click(object sender, RoutedEventArgs e)
        {
            curPath = "U";
            cvsFile.Children.Clear();
            if (uPanName != null)
            {
                if (Directory.Exists(uPanName + "conf"))
                {
                    folderCtrl fd = new folderCtrl();
                    fd.lb.Content = "conf";
                    fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                    cvsFile.Children.Add(fd);
                    Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                }
                if (Directory.Exists(uPanName + "upgrade"))
                {
                    folderCtrl fd = new folderCtrl();
                    fd.lb.Content = "upgrade";
                    fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                    cvsFile.Children.Add(fd);
                    Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                }
                if (Directory.Exists(uPanName + "Ipr"))
                {
                    folderCtrl fd = new folderCtrl();
                    fd.lb.Content = "Ipr";
                    fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                    cvsFile.Children.Add(fd);
                    Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                }
                pathName = uPanName;
                lbPath.Content = uPanName;
                fileName = "";
                lbFileName.Content = valmoWin.dv.getCurDis("lanKey1023") + fileName;
                btnSelect.IsEnabled = false;

            }


        }
        bool isMouseDown = false;
        Point mousePoint;
        private void bdMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(this.cvsMain);
        }

        private void cvsMain1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private void cvsMain1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point theMousePoint = e.GetPosition(this.cvsMain);
                    double tmpLeft = Canvas.GetLeft(cvsGrp) + theMousePoint.X - mousePoint.X;
                    double tmpTop = Canvas.GetTop(cvsGrp) + theMousePoint.Y - mousePoint.Y;
                    if (tmpLeft < 0)
                        tmpLeft = 0;
                    else if (tmpLeft > cvsMain.Width - cvsGrp.Width)
                        tmpLeft = cvsMain.Width - cvsGrp.Width;
                    if (tmpTop < 0)
                        tmpTop = 0;
                    else if (tmpTop > cvsMain.Height - cvsGrp.Height)
                        tmpTop = cvsMain.Height - cvsGrp.Height;
                    Canvas.SetLeft(cvsGrp, tmpLeft);
                    Canvas.SetTop(cvsGrp, tmpTop);
                    mousePoint = theMousePoint;
                }

            }
        }

        private void lbBg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (disposeHandle != null)
            {
                disposeHandle();
            }
            this.Visibility = Visibility.Hidden;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (confirmHandle != null)
                {
                    confirmHandle(fileName);
                }
                this.Visibility = Visibility.Hidden;
            }
            catch
            {

            }
        }

        private void lbFileName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            keyPanel.Visibility = Visibility.Visible;
            keyPanel.setPos(0, Canvas.GetTop(cvsGrp) + 520);
        }
    }
}
