using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace nsVicoClient.ctrls
{
    public class geometryLine
    {
        Canvas rootPanel;
        bool isFirstPoint;
        PathFigure myPathFigureTest;
        PathFigure myPathFigureOld;
        PathGeometry myPathGeometry;
        PathGeometry myPathGeometryOld;
        Path myPath;
        Path myPathOld;
        //Path myPathStream;
        int curPathNum = 0;
        public List<Point> pLst = new List<Point>();
        public geometryLine(Canvas cvs)
        {
            rootPanel = cvs;
            //myPathFigure = new PathFigure();
            myPathGeometry = new PathGeometry();
            //myPathGeometry.Figures.Add(myPathFigure);

            myPath = new Path();
            myPath.Stroke = Brushes.Blue;
            myPath.StrokeThickness = 1;
            myPath.Data = myPathGeometry;
            rootPanel.Children.Add(myPath);

            myPathFigureOld = new PathFigure();
            myPathGeometryOld = new PathGeometry();
            myPathGeometryOld.Figures.Add(myPathFigureOld);

            myPathOld = new Path();
            myPathOld.Stroke = Brushes.Blue;
            myPathOld.StrokeThickness = 1;
            myPathOld.Data = myPathGeometryOld;

            rootPanel.Children.Add(myPathOld);
            myPathFigureTest = new PathFigure();
            myPathGeometry.Figures.Add(myPathFigureTest);
            
            isFirstPoint = true;
        }
        
        public void setLnColor(SolidColorBrush color)
        {
            myPath.Stroke = color;
            myPathOld.Stroke = color;
        }

        public void add(Point p)
        {
            if (isFirstPoint)
            {
                myPathFigureTest.StartPoint = p;
                myPathFigureOld.StartPoint = p;
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

        public void clear()
        {
            pLst.Clear();
            if(myPathFigureTest != null)
                myPathFigureTest.Segments.Clear();
            if(myPathFigureOld != null)
                myPathFigureOld.Segments.Clear();
            isFirstPoint = true;
        }
    }
}
