using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nsVicoClient.ctrls
{
    public partial class CurveCanves : UserControl
    {
        private List<DrawingVisual> historyVisuals = new List<DrawingVisual>();

        /// <summary>
        /// 历史曲线最大保留数
        /// </summary>
        public static int MaxHistoryVisualsCount = 10;
        /// <summary>
        /// 设置控件高度
        /// </summary>
        public double SetHeight
        {
            set
            {
                double height = value;

                cvsMain.Height = height;
                History.Height = height;
                Current.Height = height;
            }
        }
        /// <summary>
        /// 设置控件宽度
        /// </summary>
        public double SetWidth
        {
            set
            {
                double width = value;

                cvsMain.Width = width;
                History.Width = width;
                Current.Width = width;
            }
        }
        private Pen _historyBrush = new Pen(Brushes.Black, 1);
        /// <summary>
        /// 设置历史曲线颜色
        /// </summary>
        public Brush HistoryBrush
        {
            set
            {
                _historyBrush.Brush = value;

                ClearHistroyCurves();
            }
        }
        private Pen _currentBrush = new Pen(Brushes.Black, 1);
        /// <summary>
        /// 设置当前曲线颜色
        /// </summary>
        public Brush CurrentBrush
        {
            set
            {
                _currentBrush.Brush = value;
                InitCurrentCurve();
            }
        }
        /// <summary>
        /// 当前曲线图形
        /// </summary>
        private PathFigure pfCurrent = new PathFigure();

        public CurveCanves()
        {
            InitializeComponent();

            _historyBrush.DashStyle = new DashStyle(new DoubleCollection { 5, 5 }, 0);

        }

        private void InitCurrentCurve()
        {
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pfCurrent);
            DrawingVisual dv = new DrawingVisual();
            DrawGeometry(dv, pg, _currentBrush);
            Current.AddVisual(dv);
        }

        public void ClearHistroyCurves()
        {
            foreach (DrawingVisual dv in historyVisuals)
            {
                History.DeleteVisual(dv);
            }
        }

        public void NewHistroyCurve(List<Point> Points)
        {
            DrawingVisual dv = new DrawingVisual();
            DrawGeometry(dv, getGeometry(Points), _historyBrush);
            History.AddVisual(dv);

            historyVisuals.Add(dv);
            while (historyVisuals.Count > MaxHistoryVisualsCount)
            {
                History.DeleteVisual(historyVisuals[0]);
                historyVisuals.RemoveAt(0);
            }
        }

        public void RefushCurrrentCurve(List<Point> Points)
        {
            pfCurrent.Segments.Clear();

            for (int i = 0; i < Points.Count; i++)
            {
                if (i != 0)
                {
                    pfCurrent.Segments.Add(new LineSegment(getPoint(Points[i]), true));
                }
                else
                {
                    pfCurrent.StartPoint = getPoint(Points[i]);
                }
            }
        }

        private Geometry getGeometry(List<Point> points)
        {
            PathGeometry pathG = new PathGeometry();
            PathFigure pathF = new PathFigure();
            pathG.Figures.Add(pathF);

            if (points.Count > 0)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (i != 0)
                    {
                        pathF.Segments.Add(new LineSegment(getPoint(points[i]), true));
                    }
                    else
                    {
                        pathF.StartPoint = getPoint(points[i]);
                    }
                }
            }

            return pathG;
        }

        private Point getPoint(Point p)
        {
            return new Point(p.X / 10000 * cvsMain.Width, p.Y / 10000 * cvsMain.Height);
        }

        private void DrawGeometry(DrawingVisual visual,Geometry geo,Pen pen)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawGeometry(null, pen, geo);
            }
        }
    }
}
