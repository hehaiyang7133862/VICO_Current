using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using nsDataMgr;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// GraphicsCurveCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class GraphicsCurveCtrl : System.Windows.Controls.UserControl
    {
        bool isFirstPoint;
        int curPathNum = 0;
        public List<Point> pLst = new List<Point>();

        System.Drawing.Pen curvePen = new System.Drawing.Pen(System.Drawing.Brushes.Blue, 2);
        Bitmap bitmap = new Bitmap(300, 250);
        Graphics objGraphics;
        public DblDblEvent getLnVHandle
        {
            get;
            set;
        }
        public DblDblEvent getLnHHandle
        {
            get;
            set;
        }
        public GraphicsCurveCtrl()
        {
            InitializeComponent();
            vOpt = true;
            objGraphics = Graphics.FromImage(bitmap);
            //w = 200;
            //h = 200;
        }

        bool _hOpt = false;
        public bool hOpt
        {
            get
            {
                return _hOpt;
            }
            set
            {
                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.ScaleY = vOpt ? -1 : 1;
                scaleTransform.ScaleX = value ? -1 : 1;
                ctrlMain.RenderTransform = scaleTransform;
                ctrlMain.Margin = new System.Windows.Thickness(value ? -ctrlMain.Width : ctrlMain.Margin.Left,ctrlMain.Margin.Top, ctrlMain.Margin.Right, ctrlMain.Margin.Bottom);
                _hOpt = value;
            }
        }

        private bool _vOpt = false;
        public bool vOpt
        {
            get
            {
                return _vOpt;
            }
            set
            {
                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.ScaleX = hOpt ? -1 : 1;
                scaleTransform.ScaleY = value ? -1 : 1;
                ctrlMain.RenderTransform = scaleTransform;
                ctrlMain.Margin = new System.Windows.Thickness(ctrlMain.Margin.Left,value ? - ctrlMain.Height : ctrlMain.Margin.Top,ctrlMain.Margin.Right,ctrlMain.Margin.Bottom);
                _vOpt = value;
            }
        }

        public double w
        {
            get
            {
                return ctrlMain.Width;
            }
            set
            {
                ctrlMain.Width = value;
                bitmap.SetResolution((float)w, (float)h);
            }
        }

        public double h
        {
            get
            {
                return ctrlMain.Height;
            }
            set
            {
                ctrlMain.Height = value;
                bitmap.SetResolution((float)w, (float)h);
            }
        }

        public System.Drawing.Brush mForeground
        {
            get
            {
                return curvePen.Brush;
            }
            set
            {
                curvePen.Brush = value;
            }
        }

        System.Windows.Media.PointCollection _points = new System.Windows.Media.PointCollection();
        public System.Windows.Media.PointCollection points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                clear();
                objGraphics.Dispose();
                Bitmap bitmap = new Bitmap((int)w, (int)h);
                objGraphics = Graphics.FromImage(bitmap);
                clear();
                Point[] tmpPoint = new Point[_points.Count];
                for (int i = 0; i < _points.Count; i++)
                {
                    tmpPoint[i] = new Point((int)_points[i].X, (int)_points[i].Y);
                }

                objGraphics.DrawLines(curvePen, tmpPoint);
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Bmp);
                byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以，至于区别么，下面有解释
                ms.Close(); 

                BitmapImage bitmapImg = new BitmapImage();
                bitmapImg.BeginInit();
                bitmapImg.StreamSource = new MemoryStream(bytes);
                bitmapImg.EndInit();
                ctrlMain.Source = bitmapImg;
            }
        }
 

        public void clear()
        {
            if(objGraphics != null)
                objGraphics.Clear(System.Drawing.Color.White);
        }

    

    }
}
