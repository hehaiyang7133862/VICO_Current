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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for catchScreen.xaml
    /// </summary>
    public partial class catchScreen : UserControl
    {
        public catchScreen()
        {
            InitializeComponent();
        }

        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.Screen scr = System.Windows.Forms.Screen.PrimaryScreen;
            System.Drawing.Rectangle rc = scr.Bounds;
            int iWidth = 1100;
            int iHeight = 1180;
            //创建一个和屏幕一样大的Bitmap     
            System.Drawing.Image myImage = new System.Drawing.Bitmap(iWidth, iHeight);
            //从一个继承自Image类的对象中创建Graphics对象     
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(myImage);
            //抓屏并拷贝到myimage里   
            //System.Drawing.Point p1 = new System.Drawing.Point(20, 100);
            //System.Drawing.Point p2 = new System.Drawing.Point(0, 140);
            //System.Drawing.Size s = new System.Drawing.Size(iWidth, iHeight);
            //g.CopyFromScreen(p1, p2, s);
            g.CopyFromScreen(0, 190, 0, 0, new System.Drawing.Size(iWidth, iHeight));
            //保存为文件  
            DateTime dt = DateTime.Now;
            string strtmp = dt.ToString("yyyy-MM-dd hh.mm.ss");  //G: 2008/06/15 21:15:07
            //string[] strs = strtmp.Split('T');
            //string[] strd = strs[1].Split(':');
            string tmp = strtmp.Replace('T', ' ');
            string tmp2 = tmp.Replace(':', '-');
            string imgName = tmp2 + ".jpeg";
            myImage.Save(imgName); 
        }
    }
}
