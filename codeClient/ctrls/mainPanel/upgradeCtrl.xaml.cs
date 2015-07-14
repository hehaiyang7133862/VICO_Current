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
using System.Diagnostics;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// upgradeCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class upgradeCtrl : UserControl
    {
        public upgradeCtrl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }

        public void show()
        {
            if (valmoWin.sUsbPath != null)
            {
                if (File.Exists("d:\\Program\\upgrade.exe"))
                {
                    FileVersionInfo curVer = FileVersionInfo.GetVersionInfo("d:\\Program\\upgrade.exe");
                    lbCurVerUpgrade.Content = curVer.FileVersion;
                }
                else
                {
                    lbCurVerUpgrade.Content = "";
                }
                if (File.Exists(valmoWin.sUsbPath + "upgrade\\upgrade.exe"))
                {
                    checkBoxCtrl1.Visibility = Visibility.Visible;
                    checkBoxCtrl1.bIsChecked = true;
                    lbFileNameUpgrade.Background = Brushes.Transparent;
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(valmoWin.sUsbPath + "upgrade\\upgrade.exe");
                    lbNewVerUpgrade.Content = myFileVersionInfo.FileVersion;
                }
                else
                {
                    checkBoxCtrl1.Visibility = Visibility.Hidden;
                    lbFileNameUpgrade.Background = Brushes.Silver;
                    lbNewVerUpgrade.Content = "";

                }

                if (File.Exists("d:\\Program\\Program II.exe"))
                {
                    FileVersionInfo curVer = FileVersionInfo.GetVersionInfo("d:\\Program\\Program II.exe");
                    lbCurVerProgramII.Content = curVer.FileVersion;
                }
                else
                {
                    lbCurVerProgramII.Content = "";
                }
                if (File.Exists(valmoWin.sUsbPath + "upgrade\\Program II.exe"))
                {
                    checkBoxCtrl2.Visibility = Visibility.Visible;
                    checkBoxCtrl2.bIsChecked = true;
                    lbFileNameProgramII.Background = Brushes.Transparent;
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(valmoWin.sUsbPath + "upgrade\\Program II.exe");
                    lbNewVerProgramII.Content = myFileVersionInfo.FileVersion;
                }
                else
                {
                    checkBoxCtrl2.Visibility = Visibility.Hidden;
                    lbFileNameProgramII.Background = Brushes.Silver;
                }

                if (File.Exists(valmoWin.sUsbPath + "upgrade\\lanCN.xaml"))
                {
                    checkBoxCtrl3.Visibility = Visibility.Visible;
                    checkBoxCtrl3.bIsChecked = true;
                    lbFileNameLanCN.Background = Brushes.Transparent;
                }
                else
                {
                    checkBoxCtrl3.readOnly = true;
                    checkBoxCtrl3.bIsChecked = false;
                    lbFileNameLanCN.Background = Brushes.Silver;
                }

                if (File.Exists(valmoWin.sUsbPath + "upgrade\\lanEN.xaml"))
                {
                    checkBoxCtrl4.Visibility = Visibility.Visible;
                    checkBoxCtrl4.bIsChecked = true;
                    lbFileNameLanEN.Background = Brushes.Transparent;
                }
                else
                {
                    checkBoxCtrl4.readOnly = true;
                    checkBoxCtrl4.bIsChecked = false;
                    lbFileNameLanEN.Background = Brushes.Silver;
                }
            }
            else
            {
                checkBoxCtrl1.Visibility = Visibility.Hidden;
                checkBoxCtrl2.Visibility = Visibility.Hidden;
                checkBoxCtrl3.Visibility = Visibility.Hidden;
                checkBoxCtrl4.Visibility = Visibility.Hidden;
                lbDisUpgrade.Content = "共检测到0个可升级文件";
                
            }
            this.Visibility = Visibility.Visible;
        }

        private void lbBg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
    public class upgradeItemCtrl
    {
        Canvas cvs = new Canvas();
        checkBoxCtrl checkBtn = new checkBoxCtrl();
        Label lbFileOld = new Label();
        Label lbFileVersionOld = new Label();
    }

}
