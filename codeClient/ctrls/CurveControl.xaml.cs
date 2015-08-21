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
    public partial class CurveControl : UserControl
    {
        private Brush _curveBrush;
        /// <summary>
        /// 设置曲线颜色
        /// </summary>
        public Brush CurveColor
        {
            set 
            {
                _curveBrush = value;
                InitPen(_curveBrush);

                cvsMain.Children.Clear();
                visualHost = new MyVisualHost(pg, _curvePen);
                cvsMain.Children.Add(visualHost);
            }
        }
        private double _staffHeight = 300;
        /// <summary>
        /// 设置控件高度
        /// </summary>
        public double CtrlHeight
        {
            set
            {
                _staffHeight = value;
                cvsMain.Height = _staffHeight;
                this.Height = _staffHeight;
            }
        }
        private double _staffWidth = 300;
        /// <summary>
        /// 设置控件宽度
        /// </summary>
        public double CtrlWidth
        {
            set
            {
                _staffWidth = value;
                cvsMain.Width = _staffWidth;
                this.Width = _staffWidth;
            }
        }
        private Pen _curvePen;
        private bool _bIsCurveReverseX = false;
        /// <summary>
        /// 设置曲线沿X轴方向
        /// </summary>
        public bool IsCurveReverseX
        {
            set
            {
                _bIsCurveReverseX = value;
            }
        }
        private bool _bIsCurverReverseY = false;
        /// <summary>
        /// 设置曲线沿Y轴方向
        /// </summary>
        public bool IsCurverReverseY
        {
            set
            {
                _bIsCurverReverseY = value;
            }
        }
        private PathFigure pf = new PathFigure();
        private PathGeometry pg = new PathGeometry();

        private MyVisualHost visualHost;

        public CurveControl()
        {
            InitializeComponent();

            InitPen();
            pg.Figures.Add(pf);

            visualHost = new MyVisualHost(pg,_curvePen);
            cvsMain.Children.Add(visualHost);
        }

        /// <summary>
        /// 刷新曲线
        /// </summary>
        /// <param name="p">曲线点集合</param>
        public void refushCurve(List<Point> points)
        {
            pf.Segments.Clear();

            for (int i = 0; i < points.Count; i++)
            {
                if (i != 0)
                {
                    pf.Segments.Add(new LineSegment(getPos(points[i]), true));
                }
                else
                {
                    pf.StartPoint = getPos(points[i]);
                }
            }
        }

        public void refushCurve(Point[] points)
        {
            pf.Segments.Clear();

            for (int i = 0; i < points.Length; i++)
            {
                if (i != 0)
                {
                    pf.Segments.Add(new LineSegment(getPos(points[i]), true));
                }
                else
                {
                    pf.StartPoint = getPos(points[i]);
                }
            }
        }

        /// <summary>
        /// 清理曲线
        /// </summary>
        public void ClearCurve()
        {
        }

        private Point getPos(Point p)
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
            if (p.Y < -10000)
            {
                p.Y = -10000;
            }

            return new Point(getPosX(p.X), getPosY(p.Y));
        }
        private double getPosX(double x)
        {
            if (!_bIsCurveReverseX)
            {
                return x / 10000 * _staffWidth;
            }
            else
            {
                return _staffWidth - x / 10000 * _staffWidth;
            }
        }
        private double getPosY(double y)
        {
            if (!_bIsCurverReverseY)
            {
                return y / 10000 * _staffHeight;
            }
            else
            {
                return _staffHeight - y / 10000 * _staffHeight;
            }
        }

        private void InitPen()
        {
            _curveBrush = Brushes.Black;
            _curvePen = new Pen(_curveBrush, 1);
        }
        private void InitPen(Brush brush)
        {
            _curvePen = new Pen(brush, 1);
        }
    }

    public class MyVisualHost : FrameworkElement
    {
        private PathGeometry geo;
        private Pen p;

        private VisualCollection _children;

        public MyVisualHost(PathGeometry pathGeometry, Pen pen)
        {
            geo = pathGeometry;
            p = pen;

            _children = new VisualCollection(this);
            _children.Add(CreateDrawingVisualGeometry());
        }

        private DrawingVisual CreateDrawingVisualGeometry()
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            DrawingContext drawingContext = drawingVisual.RenderOpen();

            drawingContext.DrawGeometry(null, p, geo);

            drawingContext.Close();

            return drawingVisual;

        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }

        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }
    }
}
