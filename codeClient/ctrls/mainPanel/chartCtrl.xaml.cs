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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// chartCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class chartCtrl : UserControl
    {
        //public delegate Double DblDblEvent(double value);

        bool isFirstPoint;
        int curPathNum = 0;
        PathFigure myPathFigureTest = new PathFigure();
        PathFigure myPathFigureOld = new PathFigure();
        PathGeometry myPathGeometry = new PathGeometry();
        PathGeometry myPathGeometryOld = new PathGeometry();
        Path myPath = new Path();
        Path myPathOld = new Path();

        public List<Point> pLst = new List<Point>();

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
        public chartCtrl()
        {
            InitializeComponent();
            myPathGeometry.Figures.Add(myPathFigureTest);
            myPath.Stroke = Brushes.Blue;
            myPath.StrokeThickness = 1;
            myPath.Data = myPathGeometry;
            ctrlMain.Children.Add(myPath);
            myPath.SnapsToDevicePixels = true;
            //myPath.ClipToBounds = true;

            myPathGeometryOld.Figures.Add(myPathFigureOld);
            myPathOld.Stroke = Brushes.Blue;
            myPathOld.StrokeThickness = 1;
            myPathOld.Data = myPathGeometryOld;
            ctrlMain.Children.Add(myPathOld);
            myPathOld.SnapsToDevicePixels = true;
            //myPathOld.ClipToBounds = true;

            isFirstPoint = true;

            getLnVHandle = getCurLnV;
            getLnHHandle = getCurLnH;
            mForeground = Brushes.Blue;
            vOpt = true;
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
                double left = value ? ctrlMain.Width : 0;
                Canvas.SetLeft(myPath, left);
                Canvas.SetLeft(myPathOld, left);
                myPath.RenderTransform = scaleTransform;
                myPathOld.RenderTransform = scaleTransform;
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
                double top = value ? ctrlMain.Height : 0;
                Canvas.SetTop(myPath, top);
                Canvas.SetTop(myPathOld, top);
                //ctrlMain.RenderTransform = scaleTransform;
                myPath.RenderTransform = scaleTransform;
                myPathOld.RenderTransform = scaleTransform;
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
            }
        }

        public Brush mForeground
        {
            get
            {
                return myPath.Stroke;
            }
            set
            {
                myPath.Stroke = value;
                myPathOld.Stroke = value;
            }
        }
        public Brush mBackground
        {
            get
            {
                return myPath.Fill;
            }
            set
            {
                myPath.Fill = value;
                myPathOld.Fill = value;
            }
        }

        objUnit[] objHs = new objUnit[30];
        public string objFirst
        {
            get
            {
                if (objHs[0] != null)
                    return objHs[0].serialNum;
                else
                    return "null";
            }
            set
            {
                for (int i = 0; i < objHs.Length;i++ )
                {
                    if (i == 0)
                        objHs[i] = valmoWin.dv.getObj(value);
                    else
                        objHs[i] = valmoWin.dv.getObjNext(objHs[i - 1].serialNum);
                    objHs[i].addMap();
                }
            }
        }


        PointCollection _points = new PointCollection();
        public PointCollection points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                clear();
                foreach (Point p in value)
                {
                    add(p);
                }
            }
        }
        private double getCurLnH(double pos)
        {
             return pos * ctrlMain.Width / 10000;
        }

        private double getCurLnV(double spd)
        {
             return spd * ctrlMain.Height / 10000;
        }
        public void add(Point p)
        {
            if (isFirstPoint)
            {
                //if(myPathFigureTest == null)
                //    myPathFigureTest = new PathFigure();
                myPathFigureTest.StartPoint = p;
                myPathFigureOld.StartPoint = p;
                //myPathGeometry.Figures.Add(myPathFigureTest);
                isFirstPoint = false;
                pLst.Add(p);
            }
            else
            {
                if (pLst.Count >= 1000 * (curPathNum + 1))
                {

                    for (int i = pLst.Count - 1000; i < pLst.Count; i++)
                    {
                        myPathFigureOld.Segments.Add(new LineSegment(pLst[i], true));
                    }

                    curPathNum++;
                    //myPathFigure[curPathNum] = new PathFigure();
                    myPathFigureTest.Segments.Clear();
                    myPathFigureTest.StartPoint = p;
                    myPathFigureTest.Segments.Add(new LineSegment(p, true /* IsStroked */ ));
                    pLst.Add(p);
                }
                else
                {
                    pLst.Add(p);
                    //myPathFigure[curPathNum].Segments.Add(new LineSegment(p, true /* IsStroked */ ));
                    myPathFigureTest.Segments.Add(new LineSegment(p, true /* IsStroked */ ));

                }
            }
        }
        public void add(int valueNum)
        {
            double pHTmp;
            double pVTmp;
            int pH, pV, tmpValue;
            for (int i = 0; i < valueNum && i < 30; i++)
            {
                tmpValue = objHs[i].value;
                pH = (tmpValue >> 16) & 0xffff;
                pV = (short)(tmpValue & 0xffff);

                pHTmp = pH * ctrlMain.Width / 10000;
                pVTmp = pV* ctrlMain.Height / 10000;
                add(new Point(pHTmp, pVTmp));
            }
        }

        public void clear()
        {
            pLst.Clear();
            if (myPathFigureTest != null)
                myPathFigureTest.Segments.Clear();
            if (myPathFigureOld != null)
                myPathFigureOld.Segments.Clear();
            isFirstPoint = true;
        }


    }
}
