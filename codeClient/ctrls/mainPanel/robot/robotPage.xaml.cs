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
using nsVicoClient;
using System.Threading;
using nsDataMgr;
using System.Xml;
using System.Runtime.InteropServices;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for robotPage.xaml
    /// </summary>
    public partial class robotPage : UserControl
    {

        public robotPage()
        {
            InitializeComponent();
        }
        int count1 = 0;
        int countNormal = 0;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            count1 = 0;
            countNormal = 0;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("fileConf.xml");
            XmlNode root = xmlDoc.SelectSingleNode("FileItems");//查找<bookstore>

            XmlTextWriter writer = new XmlTextWriter("fileExport.xml", System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("FileItems");
            writer.WriteEndElement();//end of Config
            writer.Close();
            XmlDocument xmlExport = new XmlDocument();
            xmlExport.Load("fileExport.xml");
            XmlNode rootExport = xmlExport.SelectSingleNode("FileItems");
            foreach (XmlNode xn in root.ChildNodes)
            {
                try
                {
                    vm.printLn(xn.Name);
                    int AddrPlc = 0;
                    int valuePlc = 0;
                    switch (xn.Name)
                    {
                        case "SysPr67":
                            XmlNode itemSysPr67 = xmlExport.CreateElement("SysPr67");
                            foreach (XmlElement xe in xn.ChildNodes)
                            {
                                filterValue(xe.InnerText, itemSysPr67,xmlExport);
                                //vm.printLn(count1++ + xe.InnerText);
            
                            }
                            rootExport.AppendChild(itemSysPr67);
                            break;
                        case "Normal":
                            XmlNode itemNormal = xmlExport.CreateElement("Normal");
                            foreach (XmlElement xe in xn.ChildNodes)
                            {
                                filterValue(xe.InnerText, itemNormal, xmlExport);
                            }
                            rootExport.AppendChild(itemNormal);
                            break;
                    }
                    vm.printLn("Write over");
                }
                catch (System.Exception ex)
                {

                    vm.perror("error!!!!!!!!!!!!!!");
                }
            }
            xmlExport.Save("fileExport.xml");
             
        }
        private void filterValue(string str,XmlNode xn,XmlDocument xDoc)
        {
            try
            {
                int AddrPlc = 0;
                int valuePlc = 0;
                string addr = str;
                string[] tmpStr = addr.Split('.');
                if (tmpStr.Length == 2)
                {
                    LinkMgr.getObjPlcAddr(addr, ref AddrPlc, tmpStr[0]);
                    Lasal32.LslReadFromSvr(AddrPlc, ref valuePlc);
                    //XmlElement xe = new XmlElement();
                    XmlElement item = xDoc.CreateElement("item");
                    item.SetAttribute("value", valuePlc.ToString());
                    item.InnerText = str;
                    //xmlDoc.AppendChild(item);
                    xn.AppendChild(item);
                    //vm.printLn(count1++ + "\t" + str + "\t" + valuePlc);
                }


            }
            catch (System.Exception ex)
            {
                vm.perror("[filterValue]\t" + str + " Error");
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SystemParametersInfo(uint uAction, uint uParam, StringBuilder lpvParam, uint init);
        //static extern bool SystemParametersInfo(uint uAction, uint uParam, uint lpvParam, uint init);
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // 将本程序设置为屏保
            //const uint SPI_SETSCREENSAVERRUNNING = 0x0061;
            // 屏保状态
            //const uint SPI_SETSCREENSAVEACTIVE = 0x0011;
            //// 获取屏幕壁纸地址
            //const uint SPI_GETDESKWALLPAPER = 0x0073;
            //const uint SPI_SETPOWEROFFTIMEOUT = 0x0052;
            //bool ok;
            //StringBuilder str = new StringBuilder(100);
            //ok = SystemParametersInfo(SPI_SETSCREENSAVEACTIVE, 1, str, 0);
            //vm.printLn(ok.ToString());
            //vm.printLn(SystemParametersInfo(SPI_SETPOWEROFFTIMEOUT, 5, new StringBuilder(true.ToString()), 0).ToString());
            //SystemParametersInfo(15, 5, new StringBuilder(true.ToString()), 1);
            //SPI_SETPOWEROFFTIMEOUT
            /* 
             * 设置壁纸
             * private const int SPI_SETDESKWALLPAPER = 20
             * SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename, 1);
             */

        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            //PDFReader pr = new PDFReader(@"D:\hhy\Working\Valmo\资料\文档\说明书\Engel说明书.pdf");
            //wfh.Child = pr;
        }
    }
}
