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
using System.Windows.Threading;
using nsDataMgr;
using System.Xml;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// exportIprFileCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class exportIprFileCtrl : UserControl
    {
        SolidColorBrush downBrushe = new SolidColorBrush(Color.FromRgb(194, 194, 194));
        SolidColorBrush upBrushe = Brushes.Transparent;
        SolidColorBrush readOnlyBrushe = new SolidColorBrush(Color.FromRgb(200, 200, 200));

        DispatcherTimer dtLoad = new DispatcherTimer();
        DblRefIntEvent dealHandle;
        nullEvent disposeHandle;
        public exportIprFileCtrl()
        {
            InitializeComponent();
            dtLoad.Interval = new TimeSpan(0, 0, 0, 0, 2);
            dtLoad.Tick += new EventHandler(dtLoad_Tick);
            this.Visibility = Visibility.Hidden;
        }
        string UpanPath = null;
        int total = 0;
        List<iprUnitObj> lstIprCtr = new List<iprUnitObj>();
        XmlNodeList xnlSysPr67 = null;
        XmlNodeList xnlNormal = null;
        int exportCount = 0;
        int curLstNr = 0;
        int curStep = 0;
        public void show(DblRefIntEvent dealFunc = null, nullEvent disposeFunc = null, string path = null, int totalNum = 0)
        {
            UpanPath = path;
            total = totalNum;
            //lbDisOk.Visibility = Visibility.Hidden;
            btnConfirm.readOnly = true;
            readOnly = true;
            dealHandle = dealFunc;
            disposeHandle = disposeFunc;
            //pBar.Value = 0;
            //lbValue.Content = "0.0%";
            count = 0;
            //lstIprCtr.Clear();
            //for (int i = 0; i < 300; i++)
            //{
            //   lstIprCtr.Add( valmoWin.SIprCtrl.getUnitObj(i));
            //}
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load("fileConf.xml");
            //XmlNode root = xmlDoc.SelectSingleNode("FileItems");//查找<bookstore>
            //foreach (XmlNode xn in root.ChildNodes)
            //{
            //    switch (xn.Name)
            //    {
            //        case "SysPr67":
            //            xnlSysPr67 = xn.ChildNodes;
            //            break;
            //        case "Normal":
            //            xnlNormal = xn.ChildNodes;
            //            break;
            //    }
            //}
            //exportCount = lstIprCtr.Count + xnlSysPr67.Count + xnlNormal.Count;
            //curStep = 0;

            //XmlTextWriter writer = new XmlTextWriter(prgDir + "\\" + fileName, System.Text.Encoding.UTF8);
            //writer.Formatting = Formatting.Indented;
            //writer.WriteStartDocument();
            //writer.WriteStartElement("ipr");
            //writer.WriteStartElement("param");
            //writer.WriteAttributeString("user", valmoWin.dv.users.curUser.name);
            //writer.WriteEndElement();//end of param
            ////saveIprFileFunc(writer);
            //writer.WriteEndElement();//end of ipr
            //writer.Close();
            tbDis.Text = "";
            dtLoad.Start();
            this.Visibility = Visibility.Visible;
        }


        public void setInterval(int millSecond, int second = 0, int minutes = 0)
        {
            dtLoad.Interval = new TimeSpan(0, 0, minutes, second, millSecond);
        }
        int count = 0;
        void dtLoad_Tick(object sender, EventArgs e)
        {
            double curValue = 0;
            int savedNum = 0;
            if (dealHandle != null)
            {
                curValue = dealHandle(ref savedNum);
                count += savedNum;
            }
            //else
            //    curValue = pBar.Value + 1;

            if (curValue > 99.99)
            {
                pBar.Value = curValue;
                rate = curValue;
                dtLoad.Stop();
                if (disposeHandle != null)
                {
                    disposeHandle();
                }
                if (UpanPath != null)
                {
                    tbDis.Text = "File export successful !";
                    btnConfirm.readOnly = false;
                    readOnly = false;
                    //lbDisOk.Content = "已成功导出文件到U盘" + UpanPath + "目录";
                }
                else
                {
                    this.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                rate = curValue;
                pBar.Value = curValue;
                //tbDis.Text = "正在导出:" + count + "/" + total;
                //dis = curValue.ToString("0.0") + "%";
            }
        }

        private void btnConfirm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btnCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dtLoad.Stop();
            if (disposeHandle != null)
                disposeHandle();
            this.Visibility = Visibility.Hidden;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (bdConfirm.Background == upBrushe)
            {
                bdConfirm.Background = downBrushe;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (bdConfirm.Background == downBrushe)
            {
                bdConfirm.Background = upBrushe;
            }

        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bdConfirm.Background == downBrushe)
            {
                bdConfirm.Background = upBrushe;
                this.Visibility = Visibility.Hidden;
            }
        }

        private void bdCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bdCancel.Background = downBrushe;
        }

        private void bdCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bdCancel.Background == downBrushe)
            {
                bdCancel.Background = upBrushe;
                //dtLoad.Stop();
                //if (disposeHandle != null)
                //    disposeHandle();
                //this.Visibility = Visibility.Hidden;
            }
        }

        private void bdCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (bdCancel.Background == downBrushe)
            {
                bdCancel.Background = upBrushe;
            }
        }

        public bool readOnly
        {
            get
            {
                return bdConfirm.Background == readOnlyBrushe;
            }
            set
            {
                bdConfirm.Background = value ? readOnlyBrushe : upBrushe;
            }
        }
        private double rate
        {
            get
            {
                return 100.0 * LnRate.X1 / lnAll.X1;
            }
            set
            {
                LnRate.X1 = value / 100 * lnAll.X1;
                lbRate.Content = value.ToString("0.0") + "%";
            }
        }
    }
}
