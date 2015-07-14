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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// loadFromUsbCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class loadFromUsbCtrl : UserControl
    {
        public loadFromUsbCtrl()
        {
            InitializeComponent();
            valmoWin.lstUsbCheckFunc.Add(checkUsb);
            this.Visibility = Visibility.Hidden;
        }

        public void checkUsb()
        {
            if (valmoWin.sUsbPath == null)
            {
                if(this.Visibility == Visibility.Visible)
                    this.Visibility = Visibility.Hidden;
            }
        }

        public void show()
        {

            if (valmoWin.sUsbPath != null)
            {
                btnCurUser.dis = valmoWin.dv.users.curUser.name;
                btnUsb.dis = valmoWin.sUsbPath.Name;
                this.Visibility = Visibility.Visible;
            }
        }

        private void recBg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cvsUsers.Visibility == Visibility.Visible)
                cvsUsers.Visibility = Visibility.Hidden;
            else if (cvsUsb.Visibility == Visibility.Visible)
                cvsUsb.Visibility = Visibility.Hidden;
            else
                this.Visibility = Visibility.Hidden;
        }

        private void btnCurUser_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (cvsUsers.Visibility == Visibility.Visible)
                cvsUsers.Visibility = Visibility.Hidden;
            else
            {
                cvsUsers.Visibility = Visibility.Visible;
                cvsUsers.Children.Clear();

                Label lbUserMgr = new Label();
                lbUserMgr.Height = 40;
                lbUserMgr.Width = 180;
                lbUserMgr.Background = Brushes.White;
                lbUserMgr.BorderThickness = new Thickness(1);
                lbUserMgr.BorderBrush = Brushes.Transparent;
                lbUserMgr.Content = valmoWin.dv.users.curUser.name;
                cvsUsers.Children.Add(lbUserMgr);
                Canvas.SetTop(lbUserMgr, 40 * (cvsUsers.Children.Count - 1));
                lbUserMgr.Background = Brushes.LightBlue;
                lbUserMgr.MouseDown += new MouseButtonEventHandler(lbUserMgr_MouseDown);
                lbUserMgr.MouseUp += new MouseButtonEventHandler(lbUserMgr_MouseUp);
                lbUserMgr.MouseLeave += new MouseEventHandler(lbUserMgr_MouseLeave);
                if (valmoWin.dv.users.curUser.accessLevel == 5)
                {
                    Label lbUser = new Label();
                    lbUser.Height = 40;
                    lbUser.Width = 180;
                    lbUser.Background = Brushes.White;
                    lbUser.BorderThickness = new Thickness(1);
                    lbUser.BorderBrush = Brushes.Transparent;
                    lbUser.Content = valmoWin.dv.users.service.name;
                    lbUser.MouseDown += new MouseButtonEventHandler(lbUserMgr_MouseDown);
                    lbUser.MouseUp += new MouseButtonEventHandler(lbUserMgr_MouseUp);
                    lbUser.MouseLeave += new MouseEventHandler(lbUserMgr_MouseLeave);
                    cvsUsers.Children.Add(lbUser);
                    Canvas.SetTop(lbUser, 40 * (cvsUsers.Children.Count - 1));
                }

                if (valmoWin.dv.users.curUser.accessLevel >= 4)
                {
                    Label lbUser = new Label();
                    lbUser.Height = 40;
                    lbUser.Width = 180;
                    lbUser.Background = Brushes.White;
                    lbUser.BorderThickness = new Thickness(1);
                    lbUser.BorderBrush = Brushes.Transparent;
                    lbUser.Content = valmoWin.dv.users.mgr.name;
                    lbUser.MouseDown += new MouseButtonEventHandler(lbUserMgr_MouseDown);
                    lbUser.MouseUp += new MouseButtonEventHandler(lbUserMgr_MouseUp);
                    lbUser.MouseLeave += new MouseEventHandler(lbUserMgr_MouseLeave);
                    Canvas.SetTop(lbUser, 40 * (cvsUsers.Children.Count - 1));
                }
                if (valmoWin.dv.users.curUser.accessLevel >= 3)
                {
                    Label lbUserMt = new Label();
                    lbUserMt.Height = 40;
                    lbUserMt.Width = 180;
                    lbUserMt.Background = Brushes.White;
                    lbUserMt.BorderThickness = new Thickness(1);
                    lbUserMt.BorderBrush = Brushes.Transparent;
                    lbUserMt.Content = valmoWin.dv.users.mt.name;
                    cvsUsers.Children.Add(lbUserMt);
                    lbUserMt.MouseDown += new MouseButtonEventHandler(lbUserMgr_MouseDown);
                    lbUserMt.MouseUp += new MouseButtonEventHandler(lbUserMgr_MouseUp);
                    lbUserMt.MouseLeave += new MouseEventHandler(lbUserMgr_MouseLeave);
                    Canvas.SetTop(lbUserMt, 40 * (cvsUsers.Children.Count - 1));
                    foreach (userClass user in valmoWin.dv.users.lstMt)
                    {
                        Label lbUser = new Label();
                        lbUser.Height = 40;
                        lbUser.Width = 180;
                        lbUser.Background = Brushes.White;
                        lbUser.BorderThickness = new Thickness(1);
                        lbUser.BorderBrush = Brushes.Transparent;
                        lbUser.Content = user.name;
                        cvsUsers.Children.Add(lbUser);
                        lbUser.MouseDown += new MouseButtonEventHandler(lbUserMgr_MouseDown);
                        lbUser.MouseUp += new MouseButtonEventHandler(lbUserMgr_MouseUp);
                        lbUser.MouseLeave += new MouseEventHandler(lbUserMgr_MouseLeave);
                        Canvas.SetTop(lbUser, 40 * (cvsUsers.Children.Count - 1));
                    }
                }
                if (valmoWin.dv.users.curUser.accessLevel >= 2)
                {

                    Label lbUserOp = new Label();
                    lbUserOp.Height = 40;
                    lbUserOp.Width = 180;
                    lbUserOp.Background = Brushes.White;
                    lbUserOp.BorderThickness = new Thickness(1);
                    lbUserOp.BorderBrush = Brushes.Transparent;
                    lbUserOp.Content = valmoWin.dv.users.op.name;
                    cvsUsers.Children.Add(lbUserOp);
                    lbUserOp.MouseDown += new MouseButtonEventHandler(lbUserMgr_MouseDown);
                    lbUserOp.MouseUp += new MouseButtonEventHandler(lbUserMgr_MouseUp);
                    lbUserOp.MouseLeave += new MouseEventHandler(lbUserMgr_MouseLeave);
                    Canvas.SetTop(lbUserOp, 40 * (cvsUsers.Children.Count - 1));
                    foreach (userClass user in valmoWin.dv.users.lstOp)
                    {
                        Label lbUser = new Label();
                        lbUser.Height = 40;
                        lbUser.Width = 180;
                        lbUser.Background = Brushes.White;
                        lbUser.BorderThickness = new Thickness(1);
                        lbUser.BorderBrush = Brushes.Transparent;
                        lbUser.Content = user.name;
                        cvsUsers.Children.Add(lbUser);
                        lbUser.MouseDown += new MouseButtonEventHandler(lbUserMgr_MouseDown);
                        lbUser.MouseUp += new MouseButtonEventHandler(lbUserMgr_MouseUp);
                        lbUser.MouseLeave += new MouseEventHandler(lbUserMgr_MouseLeave);
                        Canvas.SetTop(lbUser, 40 * (cvsUsers.Children.Count - 1));
                    }
                }
                cvsUsers.Height = cvsUsers.Children.Count * 40;
            }

        }

        bool isMousedown = false;
        private void lbUserMgr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            Label lb = sender as Label;
            lb.BorderBrush = Brushes.Blue;
            
        }

        private void lbUserMgr_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnCurUser.dis = (sender as Label).Content.ToString();
                cvsUsers.Visibility = Visibility.Hidden;
            }
        }

        private void lbUserMgr_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
            }
        }

        
        private void btnUsb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (cvsUsb.Visibility == Visibility.Visible)
                cvsUsb.Visibility = Visibility.Hidden;
            else
            {
                cvsUsb.Visibility = Visibility.Visible;
                cvsUsb.Children.Clear();
                DriveInfo[] uin = DriveInfo.GetDrives();
                foreach (DriveInfo drive in uin)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        Label lb = new Label();
                        lb.Height = 40;
                        lb.Content = drive.Name;
                        lb.Background = Brushes.White;
                        lb.BorderThickness = new Thickness(1);
                        lb.BorderBrush = Brushes.Transparent;
                        cvsUsb.Children.Add(lb);
                        lb.MouseDown += new MouseButtonEventHandler(lbUsb_MouseDown);
                        lb.MouseUp += new MouseButtonEventHandler(lbUsb_MouseUp);
                        lb.MouseLeave += new MouseEventHandler(lbUsb_MouseLeave);
                        Canvas.SetTop(lb, 180 * (cvsUsb.Children.Count - 1));
                    }
                }

                cvsUsb.Height = cvsUsb.Children.Count * 40;
            }
        }

        bool isUsbMousedown = false;
        private void lbUsb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isUsbMousedown = true;
            Label lb = sender as Label;
            lb.BorderBrush = Brushes.Blue;

        }

        private void lbUsb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isUsbMousedown)
            {
                isUsbMousedown = false;
                btnUsb.dis = (sender as Label).Content.ToString();

                cvsUsb.Visibility = Visibility.Hidden;
            }
        }

        private void lbUsb_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isUsbMousedown)
            {
                isUsbMousedown = false;
            }
        }

        private void btnCtrl1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                userClass user = valmoWin.dv.users.getUserObjByName(btnCurUser.dis);
                if (user != null)
                {
                    DriveInfo curDrive = null;
                    DriveInfo[] uin = DriveInfo.GetDrives();
                    List<DriveInfo> usbDriveLst = new List<DriveInfo>();
                    foreach (DriveInfo drive in uin)
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            usbDriveLst.Add(drive);
                            if (drive.Name == btnUsb.dis)
                            {
                                curDrive = drive;
                                break;
                            }
                        }
                    }
                    if (curDrive != null)
                    {
                        if (File.Exists(curDrive.Name + "/Acc.vm"))
                        {
                            File.Delete(curDrive.Name + "/Acc.vm");
                        }
                        fs = new FileStream(curDrive.Name + "/Acc.vm", FileMode.Create);
                        List<string> serLst = hardware.GetUsbSerial();
                        if (usbDriveLst.Count != 0)
                        {
                            for(int i = 0; i<  usbDriveLst.Count;i++)
                            {
                                if (usbDriveLst[i].Name == btnUsb.dis)
                                {
                                    if (i < serLst.Count)
                                    {
                                        sw = new StreamWriter(fs);
                                        string str = valmoWin.dv.EncryptDES(serLst[i] + "#" + user.name + "#" + user.password + "#" + user.accessLevel + "#", "Valmo076");
                                        sw.WriteLine(str);
                                        sw.Close();
                                        fs.Close();
                                        MessageBox.Show("make OK!");
                                    }
                                    else
                                    {
                                        MessageBox.Show("make error!");
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("make error!");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Drive error!");
                    }
                }
                else
                {
                    MessageBox.Show("User error!");
                }
            }
            catch(Exception ex)
            {
                if(sw != null)
                sw.Close();
                if(fs != null)
                fs.Close();
                MessageBox.Show("Make error ! \n" + ex.Message);
            }
        }
    }
}
