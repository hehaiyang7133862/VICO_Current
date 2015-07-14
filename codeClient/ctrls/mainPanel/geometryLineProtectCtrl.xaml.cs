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
    /// geometryLineProtectCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class geometryLineProtectCtrl : UserControl
    {
        public strEvent callBackTmHandle
        {
            get;
            set;
        }
        bool isFirstPoint;
        PathFigure myPathFigureTest = new PathFigure();
        PathFigure myPathFigureOld = new PathFigure();
        PathGeometry myPathGeometry = new PathGeometry();
        PathGeometry myPathGeometryOld = new PathGeometry();
        Path myPath = new Path();
        Path myPathOld = new Path();
        int curPathNum = 0;
        public List<Point> pLst = new List<Point>();
        public geometryLineProtectCtrl()
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
        }
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
        objUnit[] objHs = new objUnit[50];
        public string objFirst
        {
            set
            {
                ////if (value == "Prd503")
                ////    firstValue = 2;
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
                objHs[11] = valmoWin.dv.getObjNext(objHs[10].serialNum);
                objHs[12] = valmoWin.dv.getObjNext(objHs[11].serialNum);
                objHs[13] = valmoWin.dv.getObjNext(objHs[12].serialNum);
                objHs[14] = valmoWin.dv.getObjNext(objHs[13].serialNum);
                objHs[15] = valmoWin.dv.getObjNext(objHs[14].serialNum);
                objHs[16] = valmoWin.dv.getObjNext(objHs[15].serialNum);
                objHs[17] = valmoWin.dv.getObjNext(objHs[16].serialNum);
                objHs[18] = valmoWin.dv.getObjNext(objHs[17].serialNum);
                objHs[19] = valmoWin.dv.getObjNext(objHs[18].serialNum);
                objHs[20] = valmoWin.dv.getObjNext(objHs[19].serialNum);
                objHs[21] = valmoWin.dv.getObjNext(objHs[20].serialNum);
                objHs[22] = valmoWin.dv.getObjNext(objHs[21].serialNum);
                objHs[23] = valmoWin.dv.getObjNext(objHs[22].serialNum);
                objHs[24] = valmoWin.dv.getObjNext(objHs[23].serialNum);
                objHs[25] = valmoWin.dv.getObjNext(objHs[24].serialNum);
                objHs[26] = valmoWin.dv.getObjNext(objHs[25].serialNum);
                objHs[27] = valmoWin.dv.getObjNext(objHs[26].serialNum);
                objHs[28] = valmoWin.dv.getObjNext(objHs[27].serialNum);
                objHs[29] = valmoWin.dv.getObjNext(objHs[28].serialNum);
                objHs[30] = valmoWin.dv.getObjNext(objHs[29].serialNum);
                objHs[31] = valmoWin.dv.getObjNext(objHs[30].serialNum);
                objHs[32] = valmoWin.dv.getObjNext(objHs[31].serialNum);
                objHs[33] = valmoWin.dv.getObjNext(objHs[32].serialNum);
                objHs[34] = valmoWin.dv.getObjNext(objHs[33].serialNum);
                objHs[35] = valmoWin.dv.getObjNext(objHs[34].serialNum);
                objHs[36] = valmoWin.dv.getObjNext(objHs[35].serialNum);
                objHs[37] = valmoWin.dv.getObjNext(objHs[36].serialNum);
                objHs[38] = valmoWin.dv.getObjNext(objHs[37].serialNum);
                objHs[39] = valmoWin.dv.getObjNext(objHs[38].serialNum);
                objHs[40] = valmoWin.dv.getObjNext(objHs[39].serialNum);
                objHs[41] = valmoWin.dv.getObjNext(objHs[40].serialNum);
                objHs[42] = valmoWin.dv.getObjNext(objHs[41].serialNum);
                objHs[43] = valmoWin.dv.getObjNext(objHs[42].serialNum);
                objHs[44] = valmoWin.dv.getObjNext(objHs[43].serialNum);
                objHs[45] = valmoWin.dv.getObjNext(objHs[44].serialNum);
                objHs[46] = valmoWin.dv.getObjNext(objHs[45].serialNum);
                objHs[47] = valmoWin.dv.getObjNext(objHs[46].serialNum);
                objHs[48] = valmoWin.dv.getObjNext(objHs[47].serialNum);
                objHs[49] = valmoWin.dv.getObjNext(objHs[48].serialNum);

                foreach (objUnit obj in objHs)
                {
                    obj.addMap();
                }

            }
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
        public void clear()
        {
            //if (needRecord)
            //{
            //    //vm.fprintLn("start:");
            //    //for (int i = 0; i < lstV.Count; i++)
            //    //{
            //    //    vm.fprintLn(i + "\t" + lstTm[i] + "\t" + lstV[i]);
            //    //}
            //    //vm.fprintLn("end:");
            //    //vm.fflush();
            //}
            lstTm.Clear();
            lstV.Clear();
            pLst.Clear();
            if (myPathFigureTest != null)
                myPathFigureTest.Segments.Clear();
            if (myPathFigureOld != null)
                myPathFigureOld.Segments.Clear();

            isFirstPoint = true;
        }

        private double getCurLnH(double pos)
        {
            int count = lstTm.Count;
            if (count != 0)
            {

                if (lstTm[count - 1] != 0)
                {
                    if (opt)
                        return pos * cvsMain.Width / lstTm[count - 1];
                    else
                        return cvsMain.Width - pos * cvsMain.Width / lstTm[count - 1];
                }
                else
                    return 0;
          
            }
            return 0;
        }
        private double getCurLnV(double spd)
        {
            if (hOpt)
                return spd * cvsMain.Height / 10000;
            else
                return cvsMain.Height - spd * cvsMain.Height / 10000;
        }

        public bool needRecord
        {
            get;
            set;
        }
        List<double> lstTm = new List<double>();
        List<double> lstV = new List<double>();
        public void add(int valueNum)
        {
            double pHTmp;
            double pVTmp;
            int pH, pV, tmpValue;
            for (int i = 0; i < valueNum && i < 50; i++)
            {
                tmpValue = objHs[i].value;
                pH = (tmpValue >> 16) & 0xffff;
                pV = (short)(tmpValue & 0xffff);

                //pHTmp = getCurLnH(pH);

                //pVTmp = getCurLnV(-pV);


                lstTm.Add(pH);
                lstV.Add(pV);
                //add(new Point(pHTmp, pVTmp));

            }
            myPathFigureTest.Segments.Clear();
            myPathFigureOld.Segments.Clear();
            isFirstPoint = true;
            for (int i = 0; i < lstTm.Count; i++)
            {
                pHTmp = getCurLnH(lstTm[i]);
                pVTmp = getCurLnV(lstV[i]);
                add(new Point(pHTmp, pVTmp));
            }
            if (callBackTmHandle != null)
                callBackTmHandle(valmoWin.dv.tempTypeObj.getStrValue(lstTm[lstTm.Count - 1]));
           
        }
        public double getLastTm()
        {
            if (lstTm.Count > 0)
                return lstTm[lstTm.Count - 1];
            else
                return 0;
        }
    }
}
