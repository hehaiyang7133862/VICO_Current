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

namespace nsVicoClient.ctrls
{

    /// <summary>
    /// Interaction logic for saveFileCtrl.xaml
    /// </summary>
    public partial class saveFileCtrl : UserControl
    {
        //confirmCtrl confirmPanel = new confirmCtrl();
        //saveFileEvent saveFileHandle;
        string uPanName;
        public nullEvent disposeHandle;
        public strEvent confirmHandle
        {
            get;
            set;
        }
        public saveFileCtrl()
        {
            InitializeComponent();
            ////cvsFile.setChildSize(40,430);
            valmoWin.lstUsbCheckFunc.Add(refreshUpan);
            this.Visibility = Visibility.Hidden;            
        }
        private void disposeFunc()
        {
            
        }
        private void setEnterFunc(string value)
        {
            string fileName = value;
            if (value.Contains("/") )
            {
                string[] strTmp1 = value.Split('/');
                fileName = strTmp1[strTmp1.Length - 1];
            }
            if(fileName.Contains("\\"))
            {
                string[] strTmp2 = fileName.Split('\\');
                fileName = strTmp2[strTmp2.Length - 1];
            }
            lbFileName.Content = fileName;
        }
        public void show(string newFileName)
        {
            Console.WriteLine(this.Width + "," + this.Height);
            lbFileName.Content = newFileName;
            //refreshUpan();
            //valmoWin.lstUsbCheckFunc.Add(refreshUpan);
            btnLocal_Click(null, null);
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;

        }
        public void init()
        {
            //refreshUpan();
            btnLocal_Click(null, null);
            this.Visibility = Visibility.Visible;
        }
        public void refreshUpan()
        {
            //uPanName = null;
            //DriveInfo[] uin = DriveInfo.GetDrives();
            //foreach (DriveInfo drive in uin)
            //{
            //    if (drive.DriveType == DriveType.Removable)
            //    {
            //        uPanName = drive.Name.ToString();
            //        vm.printLn("U盘已插入，盘符为:" + drive.Name.ToString());
            //        break;
            //    }
            //}
            //if (uPanName == null)
            //{
            //    btnUPan.IsEnabled = false;
            //    //btnUPan.Background = Brushes.Silver;
            //}
            //else
            //{
            //    btnUPan.IsEnabled = true;
            //}
            btnUPan.IsEnabled = valmoWin.sUsbPath != null;
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

        }
        public void setWidth(double width)
        {
            cvsMain.Width = width;
            lbBg.Width = width;
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

            }
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
            ////cvsFile.clear();
            cvsFile.Children.Clear();
            if (Directory.Exists("Conf"))
            {
                folderCtrl fd = new folderCtrl();
                fd.lb.Content = "Conf";
                fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                cvsFile.Children.Add(fd);
                Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                ////cvsFile.add(fd);
                ////Canvas.SetTop(fd, (cvsFile.count - 1) * 40);
            }
            if (Directory.Exists("Rec"))
            {
                folderCtrl fd = new folderCtrl();
                fd.lb.Content = "Rec";
                fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                cvsFile.Children.Add(fd);
                Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                ////cvsFile.add(fd);
                ////Canvas.SetTop(fd, (cvsFile.count - 1) * 40);
            }
            if (Directory.Exists("Ipr"))
            {
                folderCtrl fd = new folderCtrl();
                fd.lb.Content = "Ipr";
                fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                cvsFile.Children.Add(fd);
                Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                ////cvsFile.add(fd);
                ////Canvas.SetTop(fd, (cvsFile.count - 1) * 40);
            }
            //lbFileName.Content = ".";
            pathName = ".";
            lbPath.Content = pathName;
        }
        private void folder_MouseDown(object sender, EventArgs e)
        {
            cvsFile.Children.Clear();
            ////cvsFile.clear();
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
                    ////cvsFile.clear();
                    btnUPan.IsEnabled = false;
                }
            }
            foreach (string d in Directory.GetFileSystemEntries(pathName))
            {
                if (File.Exists(d))
                {
                    fileSelectItemCtrl ctrlItem = new fileSelectItemCtrl();
                    cvsFile.Children.Add(ctrlItem);
                    Canvas.SetTop(ctrlItem, (cvsFile.Children.Count - 1) * 40);
                    ////cvsFile.add(ctrlItem);
                    ////Canvas.SetTop(ctrlItem, (cvsFile.count - 1) * 40);
                    ctrlItem.lb.Content = d;
                    ctrlItem.MouseDown += new MouseButtonEventHandler(fileCtrl_MouseDown);
                }
            }
            lbPath.Content = pathName;
            //if (cvsFile.Children.Count > 0)
            //{
            //    fileSelectItemCtrl item = cvsFile.Children[0] as fileSelectItemCtrl;
            //    //item.state = true;
            //    if (curPath == "L")
            //    {
            //        lbPath.Content = ".\\" + item.lb.Content.ToString();
            //    }
            //    else
            //    {
            //        lbPath.Content = item.lb.Content.ToString();
            //    }
            //}
            //else
            //{
            //    if (curPath == "L")
            //    {
            //        lbPath.Content = ".\\" + pathName;
            //    }
            //    else
            //    {
            //        if (uPanName == null)
            //        {
            //            lbFileName.Content = "";
            //        }
            //        else
            //        {
            //            lbFileName.Content = pathName;
            //        }
            //    }
            //}
        }
        private void fileCtrl_MouseDown(object sender, EventArgs e)
        {
            //fileSelectItemCtrl item = sender as fileSelectItemCtrl;
            //if (item.state)
            //{
            //    return;
            //}
            //for (int i = 0; i < cvsFile.count; i++)
            //{
            //    (cvsFile[i] as fileSelectItemCtrl).state = false;
            //}
            //item.state = true;
            //if (curPath == "L")
            //{
            //    lbFileName.Content = item.lb.Content.ToString();
            //}
            //else
            //{
            //    lbFileName.Content = item.lb.Content.ToString();
            //}

        }

        private void btnUPan_Click(object sender, RoutedEventArgs e)
        {
            curPath = "U";
            cvsFile.Children.Clear();
            ////cvsFile.clear();
            if (uPanName != null)
            {
                if (Directory.Exists(uPanName + "conf"))
                {
                    folderCtrl fd = new folderCtrl();
                    fd.lb.Content = "conf";
                    fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                    cvsFile.Children.Add(fd);
                    Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                    ////cvsFile.add(fd);
                    ////Canvas.SetTop(fd, (cvsFile.count - 1) * 40);
                }
                if (Directory.Exists(uPanName + "ini"))
                {
                    folderCtrl fd = new folderCtrl();
                    fd.lb.Content = "ini";
                    fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                    cvsFile.Children.Add(fd);
                    Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                    ////cvsFile.add(fd);
                    ////Canvas.SetTop(fd, (cvsFile.count - 1) * 40);
                }
                if (Directory.Exists(uPanName + "Ipr"))
                {
                    folderCtrl fd = new folderCtrl();
                    fd.lb.Content = "Ipr";
                    fd.MouseDown += new MouseButtonEventHandler(folder_MouseDown);
                    cvsFile.Children.Add(fd);
                    Canvas.SetTop(fd, (cvsFile.Children.Count - 1) * 40);
                    ////cvsFile.add(fd);
                    ////Canvas.SetTop(fd, (cvsFile.count - 1) * 40);
                }
                pathName = uPanName;
                //lbFileName.Content = uPanName;
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

            this.Visibility = Visibility.Hidden;
            if (disposeHandle != null)
                disposeHandle();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            bool flagRepeat = false;
            string str = lbFileName.Content.ToString();
            //Console.WriteLine(str);
            if(str.Split('\\').Length > 1)
            {

            }
            else
                str = pathName + "\\" + lbFileName.Content.ToString();
            //Console.WriteLine(str);
            foreach (string d in Directory.GetFileSystemEntries(pathName))
            {
                if (File.Exists(d))
                {
                    if (d == str || d == str + ".ipr")
                    {
                        flagRepeat = true;
                        break;
                    }
                    //Console.WriteLine(d);
                }
            }
            if (flagRepeat)
            {
                confirmPanel.init(confirmFunc);
                confirmPanel.show();
            }
            else
            {
                confirmFunc();
            }
        }
        public void confirmFunc()
        {
            if (confirmHandle != null)
            {
                string str = lbFileName.Content.ToString();
                if (str.Split('\\').Length > 1)
                    confirmHandle(str);
                else
                    confirmHandle(pathName + "\\" + lbFileName.Content.ToString());
            }
            this.Visibility = Visibility.Hidden;

        
        }
        private void lbFileName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SCharKeyPanel.dis = valmoWin.dv.getCurDis("lanKey1023");
            valmoWin.SCharKeyPanel.init(false, lbFileName.Content.ToString(), setEnterFunc, disposeFunc,new Thickness(0,800,0,0),true);
        }
    }
}
