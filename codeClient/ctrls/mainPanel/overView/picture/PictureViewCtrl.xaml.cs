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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using nsDataMgr;
using nsVicoClient.ctrls;

namespace nsVicoClient.ctrls
{
    public partial class PictureViewCtrl : UserControl
    {
        /// <summary>
        /// 鼠标是否移动
        /// </summary>
        private bool _bIsMouseMove = false;
        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        private bool _bIsMouseDown = false;
        /// <summary>
        /// 鼠标按下位置
        /// </summary>
        private System.Windows.Point pMouseDown;
        /// <summary>
        /// 鼠标当前位置
        /// </summary>
        private System.Windows.Point curMousePos;

        public static int count = 0;
        public static strEvent addNewPic;

        private ExportProgressCtrl ExportProgress = new ExportProgressCtrl();
        private DeletePictureProgressCtrl DeleteProgress = new DeletePictureProgressCtrl();

        public PictureViewCtrl()
        {
            InitializeComponent();

            cvsMain.Children.Add(ExportProgress);
            Canvas.SetLeft(ExportProgress, 284);
            Canvas.SetTop(ExportProgress, 200);

            DeleteProgress.ExitEvent = pictrueInitialize;
            cvsMain.Children.Add(DeleteProgress);
            Canvas.SetLeft(DeleteProgress, 284);
            Canvas.SetTop(DeleteProgress, 200);

            addNewPic = new strEvent(this.Add);

            pictrueInitialize();
        }

        public void Add(string path)
        {
            FileInfo f = new FileInfo(path);
            PictureItemCtrl p = new PictureItemCtrl();
            p.ImageSource = f.FullName;
            p.MouseUp += new MouseButtonEventHandler(ItemMouseUp);
            SetImageSource(p.image, f.FullName);
            cvsPictureList.Children.Add(p);

            Canvas.SetLeft(p, 32 + count % 8 * 128);
            Canvas.SetTop(p, 20 + count / 8 * 212);

            count++;
            cvsPictureList.Height = (count / 8 + 1) * 212 + 20;
        }

        private void pictrueInitialize()
        {
            cvsPictureList.Children.Clear();
            count = 0;

            DirectoryInfo d = new DirectoryInfo(Environment.CurrentDirectory + @"\jpeg\");

            foreach (FileInfo f in d.GetFiles("*.jpeg"))
            {
                PictureItemCtrl p = new PictureItemCtrl();
                p.ImageSource = f.FullName;
                p.MouseUp += new MouseButtonEventHandler(ItemMouseUp);
                SetImageSource(p.image, f.FullName);
                cvsPictureList.Children.Add(p);

                Canvas.SetLeft(p, 32 + count % 8 * 128);
                Canvas.SetTop(p, 20 + count / 8 * 212);

                count++;

                cvsPictureList.Height = (count / 8 + 1) * 212 + 20;
            }
        }

        private void ItemMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseMove != true)
            {
                if (bIsSelect == true)
                {
                    (sender as PictureItemCtrl).Selected = !(sender as PictureItemCtrl).Selected;
                }
                else
                {
                    lbPictureName.Content = new FileInfo((sender as PictureItemCtrl).ImageSource).Name;
                    cvsPictureView.Visibility = Visibility.Visible;
                    imgCur.Source = BitmapFrame.Create(new Uri((sender as PictureItemCtrl).ImageSource));
                }
            }
        }

        private void SetImageSource(System.Windows.Controls.Image image, string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(fileName);
                Bitmap sourceBmp = new Bitmap(sourceImage, 108, 192);
                IntPtr hBitmap = sourceBmp.GetHbitmap();
                BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty,
                       BitmapSizeOptions.FromEmptyOptions());
                bitmapSource.Freeze();
                WriteableBitmap writeableBmp = new WriteableBitmap(bitmapSource);
                sourceImage.Dispose();
                sourceBmp.Dispose();
                image.Source = writeableBmp;
            }
        }

        private bool _bIsSelect = false;
        public bool bIsSelect
        {
            set
            {
                _bIsSelect = value;

                if (_bIsSelect == true)
                {
                    lbSelect.Content = App.Current.TryFindResource("lanKey2010");
                    spEdit.Visibility = Visibility.Visible;
                }
                else
                {
                    lbSelect.Content = App.Current.TryFindResource("lanKey2187");
                    spEdit.Visibility = Visibility.Hidden;

                    bIsSelectAll = false;
                }
            }
            get
            {
                return _bIsSelect;
            }
        }

        private bool _bIsSelectAll = false;
        public bool bIsSelectAll
        {
            set
            {
                _bIsSelectAll = value;

                if (_bIsSelectAll == true)
                {
                    lbSelectAll.Content = App.Current.TryFindResource("lanKey2186");

                    foreach (object obj in cvsPictureList.Children)
                    {
                        if (obj.GetType() == typeof(PictureItemCtrl))
                        {
                            (obj as PictureItemCtrl).Selected = true;
                        }
                    }
                }
                else
                {
                    lbSelectAll.Content = App.Current.TryFindResource("lanKey2185");

                    foreach (object obj in cvsPictureList.Children)
                    {
                        if (obj.GetType() == typeof(PictureItemCtrl))
                        {
                            (obj as PictureItemCtrl).Selected = false;
                        }
                    }
                }
            }
            get
            {
                return _bIsSelectAll;
            }
        }

        private void lbSelect_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bIsSelect = !bIsSelect;
        }

        private void lbSelectAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bIsSelectAll = !bIsSelectAll;
        }

        private void cvsPictureView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            cvsPictureView.Visibility = Visibility.Hidden;
            imgCur.Source = null;
            lbPictureName.Content = string.Empty;
        }

        private void cvsPictureList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseDown = true;
            pMouseDown = e.GetPosition(cvsMain);
            curMousePos = pMouseDown;
        }

        private void cvsPictureList_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    System.Windows.Point tempMousePos = e.GetPosition(cvsMain);
                    if ((Math.Abs(tempMousePos.Y - pMouseDown.Y) > 5) || (Math.Abs(tempMousePos.X - pMouseDown.X) > 5))
                    {
                        _bIsMouseMove = true;
                    }

                    double oldTop = Canvas.GetTop(cvsPictureList);
                    double newTop = tempMousePos.Y - curMousePos.Y + oldTop;

                    if (newTop <= -(cvsPictureList.Height - (valmoWin.MainPanelHeight - 195)) - 20)
                        newTop = -(cvsPictureList.Height - (valmoWin.MainPanelHeight - 195)) - 20;
                    if (newTop > 0)
                        newTop = 0;
                    Canvas.SetTop(cvsPictureList, newTop);
                    curMousePos = tempMousePos;
                }
            }
        }

        private void cvsPictureList_MouseLeave(object sender, MouseEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }

        private void cvsPictureList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _bIsMouseMove = false;
            _bIsMouseDown = false;
        }


        private void btnDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnDelete.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
        }
        private void btnDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnDelete.Background = System.Windows.Media.Brushes.Transparent;

            List<string> LstDelete = new List<string>();

            foreach (object obj in cvsPictureList.Children)
            {
                if (obj.GetType() == typeof(PictureItemCtrl))
                {
                    if (((obj as PictureItemCtrl).Selected == true) && File.Exists((obj as PictureItemCtrl).ImageSource))
                    {
                        LstDelete.Add((obj as PictureItemCtrl).ImageSource);
                    }
                }
            }

            if (LstDelete.Count == 0)
            {
                return;
            }

            DeleteProgress.Initialize(LstDelete);
        }

        private void btnExport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnExport.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xEA, 0xEA, 0xEA));
        }

        private void btnExport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnExport.Background = System.Windows.Media.Brushes.Transparent;

            string uDisk = string.Empty;
            DriveInfo[] uin = DriveInfo.GetDrives();
            foreach (DriveInfo drive in uin)
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    uDisk = drive.Name + @"Valmo\ScrenShoot\";

                    DirectoryInfo di = new DirectoryInfo(uDisk);
                    if (di.Exists == false)
                    {
                        di.Create();
                    }
                }
            }

            if (uDisk.Length == 0)
            {
                MessageBox.Show(App.Current.TryFindResource("lanKey2106").ToString());
            }
            else
            {
                List<string> LstExport = new List<string>();

                LstExport.Clear();

                foreach (object obj in cvsPictureList.Children)
                {
                    if (obj.GetType() == typeof(PictureItemCtrl))
                    {
                        if (((obj as PictureItemCtrl).Selected == true) && File.Exists((obj as PictureItemCtrl).ImageSource))
                        {
                            LstExport.Add((obj as PictureItemCtrl).ImageSource);
                        }
                    }
                }

                if (LstExport.Count == 0)
                {
                    return;
                }

                ExportProgress.Initialize(LstExport, uDisk);
            }
        }
    }
}
