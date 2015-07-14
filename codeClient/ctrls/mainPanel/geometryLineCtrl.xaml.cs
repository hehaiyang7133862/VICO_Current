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
    /// 曲线
    /// </summary>
    public partial class geometryLineCtrl : Canvas
    {
        bool isFirstPoint;

        Path myPath = new Path();
        PathFigure myPathFigureTest = new PathFigure();
        PathGeometry myPathGeometry = new PathGeometry();
        Path myPathOld = new Path();
        PathFigure myPathFigureOld = new PathFigure();
        PathGeometry myPathGeometryOld = new PathGeometry();

        int curPathNum = 0;
        public List<Point> pLst = new List<Point>();

        public geometryLineCtrl()
        {
            InitializeComponent();

            myPathGeometry.Figures.Add(myPathFigureTest);
            myPath.Stroke = Brushes.Blue;
            myPath.StrokeThickness = 1;
            myPath.Data = myPathGeometry;
            cvsMain.Children.Add(myPath);

            myPathGeometryOld.Figures.Add(myPathFigureOld);
            myPathOld.Stroke = Brushes.Blue;
            myPathOld.StrokeThickness = 1;
            myPathOld.Data = myPathGeometryOld;
            cvsMain.Children.Add(myPathOld);

            isFirstPoint = true;
            valmoWin.lstStartUpInit.Add(startUpInit);
        }

        int[] pH = new int[12];
        int[] pV = new int[12];
        public double w
        {
            set
            {
                cvsMain.Width = value;
            }
            get
            {
                return cvsMain.Width;
            }
        }
        public double h
        {
            set
            {
                cvsMain.Height = value;
            }
            get
            {
                return cvsMain.Height;
            }
        }

        public SolidColorBrush color
        {
            set
            {
                myPath.Stroke = value;
                myPathOld.Stroke = value;
            }
        }
        public bool opt
        {
            get;
            set;
        }
        public bool hOpt
        {
            get;
            set;
        }
        public objUnit startObj;
        objUnit[] objHs = new objUnit[12];
        objUnit[] objVs = new objUnit[12];
        public string objStart
        {
            set
            {
                startObj = valmoWin.dv.getObj(value);
                //startObj.addHandle(handleStart);
            }
        }
        public objUnit countObj;
        public string objCount
        {
            set
            {
                countObj = valmoWin.dv.getObj(value);
                //countObj.addHandle(handleCount);
            }
        }
        public string objFirst
        {
            set
            {
                objHs[0] = valmoWin.dv.getObj(value);
                objHs[1] = valmoWin.dv.getObjNext(objHs[0].serialNum);
                objHs[2] = valmoWin.dv.getObjNext(objHs[1].serialNum);
                objHs[3] = valmoWin.dv.getObjNext(objHs[2].serialNum);
                objHs[4] = valmoWin.dv.getObjNext(objHs[3].serialNum);
                objHs[5] = valmoWin.dv.getObjNext(objHs[4].serialNum);
                objHs[6] = valmoWin.dv.getObjNext(objHs[5].serialNum);
                objHs[7] = valmoWin.dv.getObjNext(objHs[6].serialNum);
                objHs[8] = valmoWin.dv.getObjNext(objHs[7].serialNum);
                objHs[9] = valmoWin.dv.getObjNext(objHs[8].serialNum);
                objHs[10] = valmoWin.dv.getObjNext(objHs[9].serialNum);
                objHs[11] = valmoWin.dv.getObjNext(objHs[10].serialNum);

                objHs[0].add(plcLstSpd.mapType);
                objHs[1].add(plcLstSpd.mapType);
                objHs[2].add(plcLstSpd.mapType);
                objHs[3].add(plcLstSpd.mapType);
                objHs[4].add(plcLstSpd.mapType);
                objHs[5].add(plcLstSpd.mapType);
                objHs[6].add(plcLstSpd.mapType);
                objHs[7].add(plcLstSpd.mapType);
                objHs[8].add(plcLstSpd.mapType);
                objHs[9].add(plcLstSpd.mapType);
                objHs[10].add(plcLstSpd.mapType);
                objHs[11].add(plcLstSpd.mapType);

            }
        }

        /// <summary>
        /// 曲线数据地址
        /// </summary>
        private uint _dataAddress;
        /// <summary>
        /// 设置曲线数据的PLC地址
        /// </summary>
        objUnit objAddr;
        public string objAddrName
        {
            set
            {
                objAddr = valmoWin.dv.getObj(value);
            }
        }

        private void startUpInit()
        {
            if (objAddr != null)
                _dataAddress = (uint)objAddr.valueNew;
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
                    myPathFigureTest.Segments.Clear();
                    myPathFigureTest.StartPoint = p;
                    myPathFigureTest.Segments.Add(new LineSegment(p, true /* IsStroked */ ));
                    pLst.Add(p);
                }
                else
                {
                    pLst.Add(p);
                    myPathFigureTest.Segments.Add(new LineSegment(p, true /* IsStroked */ ));

                }
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
        private void handleStart(int value)
        {
            switch (value)
            {
                case 0:
                    {
                        //vm.fprintLn2("====> " + " \t\t " + posClose[0] + "\t" + posClose[1] + "\t" + posClose[2] + "\t" + posClose[3] + "\t" + posClose[4] + "\t" + posClose[5] + "\t" + posClose[6] + "\t" + posClose[7] + "\t" + posClose[8] + "\t" + posClose[9] + "\t" + posClose[10] + "\t" + posClose[11]
                        //                      + "\t,\t" + spdClose[0] + "\t" + spdClose[1] + "\t" + spdClose[2] + "\t" + spdClose[3] + "\t" + spdClose[4] + "\t" + spdClose[5] + "\t" + spdClose[6] + "\t" + spdClose[7] + "\t" + spdClose[8] + "\t" + spdClose[9] + "\t" + spdClose[10] + "\t" + spdClose[11]);
                    }
                    break;
                case 1:
                    {
                        clear();
                    }
                    break;

            }
        }
        private void handleCount(int value)
        {
            if (value == 0)
                return;
            double[] pHTmp = new double[12];
            double[] pVTmp = new double[12];
            int valueNum = (value >> 16) & 0xffff;
            if (value == 1)
                valueNum = 6;
            for (int i = 12 - valueNum; i < 12; i++)
            {
                pHTmp[i] = getCurLnH(pH[i]);
                pVTmp[i] = getCurLnV(pV[i]);
            }
            for (int i = 12 - valueNum; i < 12; i++)
            {
                add(new Point(pHTmp[i], pVTmp[i]));
            }

        }

        private double getCurLnH(double pos)
        {
            if (opt)
                return pos * cvsMain.Width / 10000;
            else
                return cvsMain.Width - pos * cvsMain.Width / 10000;
        }
        private double getCurLnV(double spd)
        {
            if (hOpt)
                return spd * cvsMain.Height / 10000;
            else
                return cvsMain.Height - Math.Abs( spd) * cvsMain.Height / 10000;
        }
        public void add(int valueNum)
        {
            int[] blocks = new int[valueNum];
            Lasal32.GetData(blocks, _dataAddress, (int)blocks.Length * 4);

            double[] pHTmp = new double[valueNum];
            double[] pVTmp = new double[valueNum];
            for (int i = 0; i < valueNum; i++)
            {
                pHTmp[i] = getCurLnH((blocks[i] >> 16) & 0xffff);
                pVTmp[i] = getCurLnV((short)( blocks[i] & 0xffff));

            }

            for (int i = 0 ; i < valueNum; i++)
            {
                add(new Point(pHTmp[i], pVTmp[i]));
            }
        }
    }
}
