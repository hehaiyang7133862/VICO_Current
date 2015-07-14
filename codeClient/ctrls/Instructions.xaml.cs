using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Instructions.xaml 的交互逻辑
    /// </summary>
    public partial class Instructions : Window
    {
        private List<FileInfo> lstPagePaths;
        private List<System.Windows.Controls.Image> lstImg;

        private int _curpageNum;
        private int curPageNum
        {
            set
            {
                _curpageNum = value;

                if (_curpageNum >= 1 && _curpageNum <= lstPagePaths.Count)
                {
                    load(_curpageNum);
                    lbPageNum.Content = _curpageNum + "/" + lstPagePaths.Count;
                }
            }
            get
            {
                return _curpageNum;
            }
        }

        public Instructions()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            lstPagePaths = new List<FileInfo>();
            lstImg = new List<System.Windows.Controls.Image>();

            DirectoryInfo d = new DirectoryInfo(Environment.CurrentDirectory + @"\instructions\");

            foreach (FileInfo f in d.GetFiles("*.png"))
            {
                lstPagePaths.Add(f);

                System.Windows.Controls.Image imgCtrl = new System.Windows.Controls.Image();
                imgCtrl.Height = 600;
                imgCtrl.Width = 600;
                imgCtrl.Stretch = Stretch.Fill;
                imgCtrl.Margin = new Thickness(0, 0, 50, 0);

                lstImg.Add(imgCtrl);
                spView.Children.Add(imgCtrl);
            }

            spView.Width = 650 * lstPagePaths.Count;

            curPageNum = 1;
        }

        private void load(int loadPage)
        {
            if (lstImg[loadPage - 1].Source != null)
            {
                return;
            }

            System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(lstPagePaths[loadPage - 1].FullName);
            Bitmap sourceBmp = new Bitmap(sourceImage);
            IntPtr hBitmap = sourceBmp.GetHbitmap();
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty,
                   BitmapSizeOptions.FromEmptyOptions());
            bitmapSource.Freeze();
            WriteableBitmap writeableBmp = new WriteableBitmap(bitmapSource);
            sourceImage.Dispose();
            sourceBmp.Dispose();

            lstImg[loadPage - 1].Source = writeableBmp;
        }

        private bool bIsMouseDown = false;
        System.Windows.Point pMouseDownPos;
        DateTime dtMouseDown;

        private void spView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = true;
            pMouseDownPos = pMouseLastPos = e.GetPosition(cvsMain);
            dtMouseDown = DateTime.Now;
        }

        System.Windows.Point pMouseLastPos;
        private void spView_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    System.Windows.Point pMouseCurPos = e.GetPosition(cvsMain);

                    double left = Canvas.GetLeft(spView);
                    double leftNew = pMouseCurPos.X - pMouseLastPos.X + left;

                    if (leftNew > 100)
                        leftNew = 100;
                    if (leftNew < 500 - spView.Width)
                        leftNew = 500 - spView.Width;

                    Canvas.SetLeft(spView, leftNew);
                    pMouseLastPos = pMouseCurPos;
                }
            }
        }

        private void spView_MouseLeave(object sender, MouseEventArgs e)
        {
            curPage(false);
        }

        private void spView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                bIsMouseDown = false;

                if ((DateTime.Now - dtMouseDown) < TimeSpan.FromMilliseconds(600))
                {
                    if (pMouseDownPos.X - pMouseLastPos.X > 3)
                    {
                        nextPage(true);
                    }
                    else if (pMouseDownPos.X - pMouseLastPos.X < -3)
                    {
                        prePage(true);
                    }
                }
                else if ((pMouseLastPos.X - pMouseDownPos.X) > 300)
                {
                    prePage(false);
                }
                else if ((pMouseLastPos.X - pMouseDownPos.X) < -300)
                {
                    nextPage(false);
                }
                else
                {
                    curPage(false);
                }
            }
        }

        private void nextPage(bool bIsQuick)
        {
            if (curPageNum != lstPagePaths.Count)
            {
                curPageNum++;
            }

            if (bIsQuick == true)
            {
                setPageAnimationQuick();
            }
            else
            {
                setPageAnimationSlow();
            }
        }

        private void prePage(bool bIsQuick)
        {
            if (curPageNum != 1)
            {
                curPageNum--;
            }

            if (bIsQuick == true)
            {
                setPageAnimationQuick();
            }
            else
            {
                setPageAnimationSlow();
            }
        }

        private void curPage(bool bIsQuick)
        {
            if (bIsQuick == true)
            {
                setPageAnimationQuick();
            }
            else
            {
                setPageAnimationSlow();
            }
        }

        private void setPageAnimationQuick()
        {
            DoubleAnimation leftAnimation = new DoubleAnimation();
            leftAnimation.To = -650 * (curPageNum - 1);
            leftAnimation.Duration = TimeSpan.FromMilliseconds(100);
            leftAnimation.Completed += new EventHandler(leftAnimation_Completed);
            spView.BeginAnimation((Canvas.LeftProperty), leftAnimation);
        }

        private void setPageAnimationSlow()
        {
            DoubleAnimation leftAnimation = new DoubleAnimation();
            leftAnimation.To = -650 * (curPageNum - 1);
            leftAnimation.Duration = TimeSpan.FromMilliseconds(200);
            leftAnimation.Completed += new EventHandler(leftAnimation_Completed);
            spView.BeginAnimation((Canvas.LeftProperty), leftAnimation);
        }

        void leftAnimation_Completed(object sender, EventArgs e)
        {
            spView.BeginAnimation((Canvas.LeftProperty), null);
            Canvas.SetLeft(spView, -650 * (curPageNum - 1));
        }
    }
}
