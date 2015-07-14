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
        private List<DrawingVisual> historyDv = new List<DrawingVisual>();
        private DrawingVisual currentDv = new DrawingVisual();

        private int _historyCount = 10;
        public int HistoryCount
        {
            set
            {
                _historyCount = value;
            }
        }
        private double _height = 300;
        public double SetHeight
        {
            set
            {
                _height = value;
                drawingSurface.Height = value;
                this.Height = value;
            }
        }
        private double _width = 300;
        public double SetWidth
        {
            set
            {
                _width = value;
                drawingSurface.Width = value;
                this.Width = value;
            }
        }

        private Pen _historyBrush = new Pen(Brushes.Black, 1);
        public Brush HistoryBrush
        {
            set
            {
                _historyBrush.Brush = value;
            }
        }
        private Pen _currentBrush = new Pen(Brushes.Black, 1);
        public Brush CurrentBrush
        {
            set
            {
                _currentBrush.Brush = value;
            }
        }

        public CurveCanves()
        {
            InitializeComponent();

            _historyBrush.DashCap = PenLineCap.Triangle;
            DashStyle hDs = new DashStyle();
            hDs.Dashes = new DoubleCollection { 5, 5 };
            _historyBrush.DashStyle = hDs;
        }

        public void Clear()
        {
            foreach (DrawingVisual dv in historyDv)
            {
                drawingSurface.DeleteVisual(dv);
            }

            if (currentDv != null)
            {
                drawingSurface.DeleteVisual(currentDv);
            }
        }

        public void SaveCurve(List<Point> Points)
        {
            DrawingVisual dv = new DrawingVisual();

            DrawHistoryGeometry(dv, getGeometry(Points));
            drawingSurface.AddVisual(dv);
            historyDv.Add(dv);

            if (historyDv.Count > _historyCount)
            {
                drawingSurface.DeleteVisual(historyDv[0]);
                historyDv.RemoveAt(0);
            }
        }

        public void UpdateCurve(List<Point> Points)
        {
            if (currentDv != null)
            {
                drawingSurface.DeleteVisual(currentDv);
            }

            DrawingVisual dv = new DrawingVisual();

            DrawCurrentGeometry(dv, getGeometry(Points));
            drawingSurface.AddVisual(dv);
            currentDv = dv;
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
            if (p.X > 10000)
            {
                p.X = 10000;
            }
            if (p.X < 0)
            {
                p.X = 0;
            }

            if (p.Y > 10000)
            {
                p.Y = 10000;
            }
            if (p.Y < -1000)
            {
                p.Y = -1000;
            }

            return new Point(p.X / 10000 * _width, p.Y / 10000 * _height);
        }

        private void DrawHistoryGeometry(DrawingVisual visual,Geometry geo)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawGeometry(null, _historyBrush, geo);
            }
        }
        private void DrawCurrentGeometry(DrawingVisual visual, Geometry geo)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawGeometry(null, _currentBrush, geo);
            }
        }
    }
}
