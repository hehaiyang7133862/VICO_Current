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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Drawing;
using nsDataMgr;
using nsVicoClient.ctrls;

namespace nsVicoClient.ctrls
{
    public partial class SPCCtrl : UserControl
    {
        public SPCCtrl()
        {
            InitializeComponent();

            Initialize();

            valmoWin.dv.PrdPr[215].addHandle(getNewSPCData);
            valmoWin.ds.CycleNumberChanged += UpdateCycle;

            Canvas.SetRight(tbMain, -820);
        }

        private void getNewSPCData(objUnit obj)
        {
            valmoWin.ds.ReadNewData();
        }

        private void UpdateCycle()
        {
            lbCounter.Content = valmoWin.ds.CycleNumber;
        }

        private void Initialize()
        {
            sPanel.Children.Clear();

            for (int i = 0; i < valmoWin.ds.lstSPCVariable.Count; i++)
            {
                if (valmoWin.ds.lstSPCVariable[i].Switch == true)
                {
                    SPCSummaryUnitCtrl spcUnit = new SPCSummaryUnitCtrl(valmoWin.ds.lstSPCVariable[i]);
                    spcUnit.SerialNum = i;
                    spcUnit.MouseUp += new MouseButtonEventHandler(spcUnit_MouseUp);
                    sPanel.Children.Add(spcUnit);

                    SPCDetailCtrl spcDetail = new SPCDetailCtrl();
                    spcDetail.Initialize(valmoWin.ds.lstSPCVariable[i]);

                    TabItem t = new TabItem();
                    t.Height = 0;
                    t.Width = 0;
                    t.Margin = new Thickness(0);
                    t.Content = spcDetail;

                    tbMain.Items.Add(t);
                }
            }
        }

        private void DetialCtrlHide()
        {
            foreach (object obj in sPanel.Children)
            {
                if (obj.GetType() == typeof(SPCSummaryUnitCtrl))
                {
                    (obj as SPCSummaryUnitCtrl).Selected = false;
                }
            }
        }

        private void spcUnit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (sender.GetType() != typeof(SPCSummaryUnitCtrl))
            {
                return;
            }

            foreach (object obj in sPanel.Children)
            {
                (obj as SPCSummaryUnitCtrl).Selected = false;
            }

            (sender as SPCSummaryUnitCtrl).Selected = true;

            tbMain.SelectedIndex = (sender as SPCSummaryUnitCtrl).SerialNum;

            showTabControlAnimation();

            //Bitmap ScreenShoot = new Bitmap(1080, 1920);
            ////从一个继承自Image类的对象中创建Graphics对象     
            //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(ScreenShoot);
            ////抓屏并拷贝到ScreenShoot里   
            //g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(1080, 1920));
            //Rectangle rec = new Rectangle(0, 0, 1080, 1920);
            //ScreenShoot.GaussianBlur(ref rec, 10, true);
            //var imgsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ScreenShoot.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        private bool bIsMouseDown = false;
        System.Windows.Point pMouseDownPos;
        DateTime dtMouseDown;

        private void tbMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = true;
            pMouseDownPos = pMouseLastPos = e.GetPosition(cvsMain);
            dtMouseDown = DateTime.Now;
        }

        System.Windows.Point pMouseLastPos;

        private void tbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    System.Windows.Point pMouseCurPos = e.GetPosition(cvsMain);

                    double right = Canvas.GetRight(tbMain);
                    double rightNew = -(pMouseCurPos.X - pMouseLastPos.X) + right;

                    if (rightNew > 0)
                        rightNew = 0;
                    if (rightNew < -820)
                        rightNew = -820;
                    Canvas.SetRight(tbMain, rightNew);
                    pMouseLastPos = pMouseCurPos;
                }
            }
        }

        private void tbMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                bIsMouseDown = false;

                if (pMouseDownPos.X - pMouseLastPos.X < -300)
                {
                    hideTabControlAnimation();
                }
                else
                {
                    showTabControlAnimation();
                }
            }
        }
        private void tbMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                bIsMouseDown = false;

                if (pMouseDownPos.X - pMouseLastPos.X < -300)
                {
                    hideTabControlAnimation();
                }
                else
                {
                    if ((pMouseDownPos.X - pMouseLastPos.X < -3) && (DateTime.Now - dtMouseDown) < TimeSpan.FromMilliseconds(500))
                    {
                        hideTabControlAnimation();
                    }
                    else
                    {
                        showTabControlAnimation();
                    }
                }
            }
        }

        private void showTabControlAnimation()
        {
            DoubleAnimation RightAnimation = new DoubleAnimation();
            RightAnimation.To = 0;
            RightAnimation.Duration = TimeSpan.FromMilliseconds(300);
            RightAnimation.Completed += new EventHandler(showAnimation_Completed);
            tbMain.BeginAnimation((Canvas.RightProperty), RightAnimation);
        }

        void showAnimation_Completed(object sender, EventArgs e)
        {
            tbMain.BeginAnimation((Canvas.RightProperty), null);
            Canvas.SetRight(tbMain, 0);
        }

        private void hideTabControlAnimation()
        {
            DoubleAnimation RightAnimation = new DoubleAnimation();
            RightAnimation.To = -820;
            RightAnimation.Duration = TimeSpan.FromMilliseconds(300);
            RightAnimation.Completed += new EventHandler(hideAnimation_Completed);
            tbMain.BeginAnimation((Canvas.RightProperty), RightAnimation);
        }

        void hideAnimation_Completed(object sender, EventArgs e)
        {
            tbMain.BeginAnimation((Canvas.RightProperty), null);
            Canvas.SetRight(tbMain, -820);

            foreach (object obj in sPanel.Children)
            {
                (obj as SPCSummaryUnitCtrl).Selected = false;
            }
        }
    }
}
