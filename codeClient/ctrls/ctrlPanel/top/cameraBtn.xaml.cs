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
    /// cameraBtn.xaml 的交互逻辑
    /// </summary>
    public partial class cameraBtn : UserControl
    {
        public cameraBtn()
        {
            InitializeComponent();
        }

        bool isMousedown = false;
        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousedown = true;
            btnDown.Visibility = Visibility.Visible;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMousedown)
            {
                isMousedown = false;
                btnDown.Visibility = Visibility.Hidden;
                takeAPhoto();
            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMousedown)
            {
                btnDown.Visibility = Visibility.Hidden;
                isMousedown = false;
            }
        }

        private string pathInitialization()
        {
            for (int i = 0; i < 100; i++)
            {
                string path = Environment.CurrentDirectory + @"\jpeg\" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "_" + i.ToString() + ".jpeg";
                if (File.Exists(path))
                {
                    continue;
                }
                else
                {
                    return path;
                }
            }

            return "Error";
        }

        private void takeAPhoto()
        {
            System.Windows.Forms.Screen scr = System.Windows.Forms.Screen.PrimaryScreen;
            System.Drawing.Rectangle rc = scr.Bounds;
            System.Drawing.Image ScreenShoot = new System.Drawing.Bitmap(1080, 1920);
            //从一个继承自Image类的对象中创建Graphics对象     
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ScreenShoot);
            //抓屏并拷贝到ScreenShoot里   
            g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(1080, 1920));

            string SavePath = pathInitialization();
            if (SavePath != "Error")
            {
                ScreenShoot.Save(SavePath);
            }
            else
            {
                MessageBox.Show("截图数量超过系统限制！");
            }

            PictureViewCtrl.addNewPic(SavePath);
        }
    }
}
