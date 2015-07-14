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
using System.Xml;
using System.Collections.Specialized;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// iprFileInfo.xaml 的交互逻辑
    /// </summary>
    public partial class iprFileInfo : UserControl
    {
        FileStream curFile = null;
        public fileItemCtrl curCtrl = null;
        //FileInfo curPrgFile = null;
        public static strEvent renameHandle;
        public DirectoryInfo prgDir ;
        public nullEvent refreshFolderHandle;
        public iprFileInfo()
        {
            InitializeComponent();
            prgDir = interpretorPage.prgDir;
        }
        public string prgName
        {
            get;
            set;
        }
        public FileStream prgStream
        {
            get
            {
                return null;// interpretorPage.prgStream;
            }
        }
        public string filename
        {
            get
            {
                return lbFilename.Content.ToString();
            }
        }
        public string user
        {
            get
            {
                return lbCreater.Content.ToString();
            }
        }
        public string dis
        {
            get
            {
                return tbDis.Text;
            }
        }

        public void init(fileItemCtrl fileCtrl,bool flagLoadFile = false)
        {
            try
            {
                FileInfo curFileInfo;
                if ((prgDir.FullName == fileCtrl.fileInfo.DirectoryName) && (valmoWin.SIprCtrl.prgFileName == fileCtrl.fileInfo.Name))
                {
                    if (flagLoadFile)
                        flagReadonly = true;
                    else
                        flagReadonly = false;
                    //curFileInfo = new FileInfo(prgDir + valmoWin.SIprCtrl.prgFile);
                }
                else
                {

                    flagReadonly = true;
                    
                    //curFile = new FileStream(fileCtrl.fileInfo.FullName, FileMode.Open);
                }
                curFileInfo = fileCtrl.fileInfo;
                if (curFileInfo != null)
                {
                    //XmlDocument xmlDoc = new XmlDocument();
                    //xmlDoc.Load(curFileInfo.FullName);
                    //XmlNodeList root = xmlDoc.SelectSingleNode("ipr").ChildNodes;

                    ////读取param 节点
                    //XmlNode xn = root.Item(0) as XmlNode;
                    //XmlElement xe=(XmlElement)xn;
                    //string userName = xe.GetAttribute("user");
                    //string strCreateTime = xe.GetAttribute("createTime");
                    //string strModifyTime = xe.GetAttribute("modifyTime");

                    lbFilename.Content = curFileInfo.Name;
                    lbCreateTm.Content = fileCtrl.fileInfo.CreationTime;
                    //lbCreater.Content = userName;
                    lbModifyTm.Content = fileCtrl.fileInfo.LastWriteTime;
                }
                curCtrl = fileCtrl;
            }
            catch (Exception ex)
            {
                if (curFile != null)
                    curFile.Close();
                curFile = null;
                vm.perror("[iprFileInfo.init] " + ex.ToString());
            }
        }

        public string init()
        {
            try
            {
                StringCollection iprMsg = valmoWin.SIprCtrl.prgMsg;
                if (iprMsg.Count != 5)
                {
                    iprMsg.Clear();
                    iprMsg.Add(valmoWin.SIprCtrl.prgFileName);
                    iprMsg.Add("");
                    iprMsg.Add("");
                    iprMsg.Add("");
                    iprMsg.Add("");
                    valmoWin.SIprCtrl.prgMsg = iprMsg;
                }
                lbFilename.Content = iprMsg[0];
                if (iprMsg[1] == "")
                {
                    lbCreateTm.Content = iprMsg[1];
                    lbCreater.Content = iprMsg[2];
                    lbModifyTm.Content = iprMsg[3];
                    tbDis.Text = iprMsg[4];
                }
                else
                {
                    lbCreateTm.Content = iprMsg[1];
                    lbCreater.Content = iprMsg[2];
                    lbModifyTm.Content = iprMsg[3];
                    tbDis.Text = iprMsg[4];
                }

            }
            catch(Exception ex)
            {
                vm.perror("[iprFileInfo.init] " + ex.ToString());
            }
            return null;
        }
        public bool flagReadonly
        {
            get
            {
                return lbFilename.Background == Brushes.Silver;
            }
            set
            {
                if (value)
                {
                    lbFilenameDis.Background = Brushes.Silver;
                    lbFilename.Background = Brushes.Silver;
                    lbCreateTmDis.Background = Brushes.Silver;
                    lbCreateTm.Background = Brushes.Silver;
                    lbCreaterDis.Background = Brushes.Silver;
                    lbCreater.Background = Brushes.Silver;
                    lbModifyTmDis.Background = Brushes.Silver;
                    lbModifyTm.Background = Brushes.Silver;
                    tbDis.Background = Brushes.Silver;
                }
                else
                {
                    lbFilenameDis.Background = Brushes.Transparent;
                    lbFilename.Background = Brushes.Transparent;
                    lbCreateTmDis.Background = Brushes.Silver;
                    lbCreateTm.Background = Brushes.Silver;
                    lbCreaterDis.Background = Brushes.Silver;
                    lbCreater.Background = Brushes.Silver;
                    lbModifyTmDis.Background = Brushes.Silver;
                    lbModifyTm.Background = Brushes.Silver;
                    tbDis.Background = Brushes.Transparent;
                }
            }
         }

        public bool saved
        {
            get
            {
                return curCtrl != null;
            }
        }
        public void save()
        {
            try
            {
                valmoWin.SIprCtrl.saveIprFileFunc();
                //if (curCtrl == null)
                //{
                //    curFile = interpretorPage.prgStream;
                //}
                //else
                //{
                //    curFile = new FileStream(curCtrl.fileInfo.FullName, FileMode.Open);
                //}
                //curFile.Seek(100, SeekOrigin.Begin);
                //interpretorPage.saveIprHandle(curFile);
                
                //curFile.Flush();
                //curFile.Close();
                //curFile = null;
            }
            catch (Exception ex)
            {
                vm.perror("[iprFileInfo.save] " + ex.ToString());
            }
        }
        private void saveDis()
        {
            //FileInfo prgFileInfo = valmoWin.SIprCtrl.prgFileinfo;
            //if (prgFileInfo.Exists)
            //{
            //    valmoWin.SIprCtrl.saveCurPrgToFile(valmoWin.SIprCtrl.prgFileName);
            //}
            StringCollection iprMsg = valmoWin.SIprCtrl.prgMsg;
            StringCollection prgMsg = new StringCollection();
            prgMsg.Add(iprMsg[0]);
            prgMsg.Add(iprMsg[1]);
            prgMsg.Add(valmoWin.SCurUser.name);
            prgMsg.Add(lbModifyTm.Content.ToString());
            prgMsg.Add(tbDis.Text.ToString());
            valmoWin.SIprCtrl.prgMsg = prgMsg;
        }
        bool mouseDown = false;
        private void lbFilename_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;
        }

        private void lbFilename_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void charEnterFunc(string str)
        {
            try
            {
                //lbFilename.Content = str;
                string filename = str;
                string[] tmp = str.Split('.');
                if (tmp[tmp.Length - 1] != "ipr")
                {
                    filename = str + ".ipr";
                }
                lbFilename.Content = filename;
                save();
                if (curCtrl != null)
                {
                    curFile.Close();
                    curFile = null;
                    string fullname = curCtrl.fileInfo.FullName;
                    int pos = fullname.LastIndexOf('\\');
                    string newName = fullname.Substring(0, pos);
                    File.Move(curCtrl.fileInfo.FullName, newName + "\\" + filename);
                    curCtrl.init(new FileInfo(newName + "\\" + filename));
                    this.init(curCtrl);
                }
                else
                {
                    this.init();
                }
            }
            catch
            {
                valmoWin.SCharKeyPanel.hide();
            }
        }

        private void lbFilename_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mouseDown)
            {
                if (lbFilename.Background != Brushes.Silver)
                {
                    mouseDown = false;
                    string filename = lbFilename.Content.ToString();
                    string[] str = filename.Split('.');
                    if (str[str.Length - 1] == "ipr")
                    {
                        filename = filename.Substring(0, filename.Length - 4);
                    }
                    valmoWin.SCharKeyPanel.init(false, filename, renameCurIpr, null, new Thickness(0, 635, 0, 0));
                }
            }
        }

        string newFileName = "";
        public void renameCurIpr(string newfilename)
        {
            if (newfilename.Length > 4)
            {
                if (newfilename.Substring(newfilename.Length - 4, 4) != ".ipr")
                {
                    newfilename += ".ipr";
                }
            }
            else
            {
                newfilename += ".ipr";
            }
            newFileName = newfilename;
            if (File.Exists(prgDir + "\\" + newfilename))
            {
                valmoWin.SFileCoveredAlarmPanel.init(saveCoverFile, new Point(390, 720));
            }
            else
            {
                saveCoverFile();
            }
        }

        private void saveCoverFile()
        {
            if (File.Exists(prgDir + "\\" + valmoWin.SIprCtrl.prgFileName))
            {
                if( File.Exists(prgDir + "\\" + newFileName))
                {
                    File.Delete(prgDir + "\\" + newFileName);
                }
                File.Move(prgDir + "\\" + valmoWin.SIprCtrl.prgFileName, prgDir + "\\" + newFileName);
            }
            else
            {
                valmoWin.SIprCtrl.saveCurPrgToFile(prgDir + "\\" + newFileName);
            }

            valmoWin.SIprCtrl.prgFileName = newFileName;
            StringCollection prdMsgOld = Properties.Settings.Default.curIprMsg;
            StringCollection prdMsg = new StringCollection();
            prdMsg.Add(newFileName);
            prdMsg.Add(prdMsgOld[1]);
            prdMsg.Add(valmoWin.SCurUser.name);
            prdMsg.Add(DateTime.Now.ToString());
            prdMsg.Add(prdMsgOld[4]);
            Properties.Settings.Default.curIprMsg = prdMsg;

            refreshFolderHandle();
        }

        bool isDisMousedown = false;
        private void tbDis_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDisMousedown = true;
        }

        private void tbDis_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDisMousedown)
            {
                if (tbDis.Background != Brushes.Silver)
                {
                    isDisMousedown = false;
                    valmoWin.SCharKeyPanel.init(false, tbDis.Text, setDisFunc, null, new Thickness(0, 1241, 0, 0));
                }
            }
        }

        private void setDisFunc(string str)
        {
            tbDis.Text = str;
            DateTime now = DateTime.Now;
            lbModifyTm.Content = now.ToString("yyyy.MM.dd hh:mm:ss");
            //save();
            saveDis();
        }

        private void tbDis_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isDisMousedown)
            {
                isDisMousedown = false;
            }
        }
    }
}
