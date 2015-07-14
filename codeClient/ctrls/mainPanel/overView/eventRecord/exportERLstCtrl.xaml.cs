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
using System.IO;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for exportERLstCtrl.xaml
    /// </summary>
    public partial class exportERLstCtrl : UserControl
    {
        public saveFileCtrl saveFilePanel = new saveFileCtrl();
        List<recUnit> filterLst = eventMgrObj.filterLst;
        List<recUnit> filterLst2 = eventMgrObj.filterLst;
        public exportERLstCtrl()
        {
            InitializeComponent();
            cvsMain.Children.Add(saveFilePanel);
            Canvas.SetLeft(saveFilePanel, 0);
            Canvas.SetTop(saveFilePanel, 0);
            //saveFilePanel.confirmHandle = saveFileFunc;
            saveFilePanel.disposeHandle = new nullEvent(disposeFunc);
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgBg.Opacity = 1;

        }

        FileInfo exportFileInfo = null;
        FileStream exportFile = null;
        StreamWriter exportWr = null;
        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //imgBg.Opacity = 0;
            exportFile = null;
            exportWr = null;
            curItemNr = 0;
            filterLst = eventMgrObj.filterLst;
            try
            {

                DriveInfo[] uin = DriveInfo.GetDrives();
                foreach (DriveInfo drive in uin)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        string filePath = drive.Name + "Valmo EventHistory " + DateTime.Now.ToString("yyyy.M.dd hh_mm_ss") + ".xls";
                        exportFileInfo = new FileInfo(filePath);
                        exportFile = new FileStream(exportFileInfo.FullName, FileMode.OpenOrCreate);
                        exportWr = new StreamWriter(exportFile);
                        break;
                    }
                }

                if (exportWr != null)
                {
                    exportWr.WriteLine(recUnit.dis);
                    valmoWin.SExportEventHistoryPanel.show(saveEventItem, disposeExportFunc, exportFileInfo.FullName,filterLst.Count);
                }
                else
                {
                    MessageBox.Show(valmoWin.dv.getCurDis("lanKey2106"));
                    imgBg.Opacity = 0;
                }
                //if (saveFileFunc(file.FullName))
                //{
                //    MessageBox.Show("");
                //}
                //else
                //{
                    
                //}
            }
            catch (System.Exception ex)
            {

                vm.perror("[cvsMain_MouseUp] " + ex.ToString());
            }
            
        }
        int curItemNr = 0;
        public double saveEventItem(ref int saveNum)
        {
            saveNum = 0;
            for (int i = 0; i < 10;i++ )
            {
                if (curItemNr < filterLst.Count)
                {
                    string tmp = string.Empty;
                    tmp = filterLst[curItemNr].toSaveString("\t");
                    exportWr.WriteLine(tmp);
                    curItemNr++;
                    saveNum++;
                }
                else
                {
                    return 100;
                }
            }
            return 100.0 * curItemNr / filterLst.Count;

        }

        private void disposeExportFunc()
        {
            if (exportWr != null)
                exportWr.Close();
            if (exportFile != null)
                exportFile.Close();
            imgBg.Opacity = 0;
            //MessageBox.Show("导出完成");
        }
        private bool saveFileFunc(string fileName)
        {
            if (!fileName.Contains(".xls"))
                fileName += ".xls";
            imgBg.Opacity = 0;
            return eventMgrObj.saveToFile(fileName);
            
        }
        private void disposeFunc()
        {
            imgBg.Opacity = 0;
        }
    }
}
