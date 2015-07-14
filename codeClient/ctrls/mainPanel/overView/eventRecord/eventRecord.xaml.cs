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
using System.IO;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class eventRecord : UserControl
    {
        /// <summary>
        /// 当前ergUnit[0]对应的itemsLst位
        /// </summary>
        private int curStartNr;
        /// <summary>
        /// eventRecord 默认起始位置
        /// </summary>
        private int posTopBasic = -800;
        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        private bool _bIsMouseDown = false;
        /// <summary>
        /// 鼠标当前位置
        /// </summary>
        private Point curMousePos;
        /// <summary>
        /// eventRecord 向下拖动限定值
        /// </summary>
        private int limitsDrgeDown;
        /// <summary>
        /// eventRecord 向上拖动边界
        /// </summary>
        private int limitsDragUp;
        private double lasttop = -800;

        enum sortType : byte
        {
            UserName,
            StartTime,
            EndTime,
            PlateNum
        }

        ergUnitCtrl[] ergUnitGrps = new ergUnitCtrl[68];
        List<recUnit> curItemlst = eventMgrObj.itemsLst;
        /// <summary>
        /// 经过过滤处理的list
        /// </summary>
        List<recUnit> filterLst = new List<recUnit>();

        public eventRecord()
        {
            InitializeComponent();

            valmoWin.refresh = new nullEvent(refreshEvent);

            for (int i = 0; i < curItemlst.Count; i++)
            {
                filterLst.Add(curItemlst[i]);
            }

            if (filterLst.Count > 0)
            {
                recScroll.Height = 40 * 28 * 28 / filterLst.Count;
            }
            updateErgUnitGrps();
            refreshCvsList(-20);

            valmoWin.lstLanRefresh.Add(refreshEvent);
        }

        private void updateErgUnitGrps()
        {
            cvsEventRecord.Children.Clear();
            for (int i = 0; i < ergUnitGrps.Length; i++)
            {
                ergUnitGrps[i] = new ergUnitCtrl();
                ergUnitGrps[i].MouseDown += new MouseButtonEventHandler(cvsRecord_MouseDown);
                cvsEventRecord.Children.Add(ergUnitGrps[i]);
                Canvas.SetTop(ergUnitGrps[i], i * 40 + 2);
            }
        }

        private void cvsRecord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            recScroll.Visibility = Visibility.Visible;
            recScrollBack.Visibility = Visibility.Visible;

            _bIsMouseDown = true;
            curMousePos = e.GetPosition(cvsMain);

            if (ergUnitGrps[0].erObj != null)
            {
                limitsDrgeDown = posTopBasic + (20 + 2) * 40;
            }
            else
            {
                for (int i = 19; i >= 0; i--)
                {
                    if (ergUnitGrps[i].erObj == null)
                    {
                        limitsDrgeDown = posTopBasic + (19 - i + 2) * 40;
                        break;
                    }
                }
            }

            if (ergUnitGrps[67].erObj != null)
            {
                limitsDragUp = posTopBasic - 20 * 40;
            }
            else
            {
                for (int j = 48; j < 68; j++)
                {
                    if (ergUnitGrps[j].erObj == null)
                    {
                        limitsDragUp = posTopBasic - (j - 48) * 40;
                        break;
                    }
                }
            }
        }
        private void cvsRecord_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_bIsMouseDown)
            {
                _bIsMouseDown = false;

                recScroll.Visibility = Visibility.Hidden;
                recScrollBack.Visibility = Visibility.Hidden;

                double topCur = Canvas.GetTop(cvsEventRecord);
                int startNew = curStartNr;

                if (topCur < lasttop)
                {
                    int numUp = (int)(Math.Abs(topCur - posTopBasic) / 40);

                    if (numUp > 0)
                    {
                        if (ergUnitGrps[numUp + 47].erObj != null)
                        {
                            startNew = curStartNr + numUp;
                        }
                        else
                        {
                            for (int i = 0; i < numUp; i++)
                            {
                                if (ergUnitGrps[i + 48].erObj == null)
                                {
                                    startNew = curStartNr + i;
                                    break;
                                }
                            }
                        }
                    }

                    lasttop = posTopBasic + topCur % 40;
                    Canvas.SetTop(cvsEventRecord, posTopBasic + topCur % 40);
                }
                if (topCur > lasttop)
                {
                    int j = 0;
                    int numDown;

                    if (topCur < posTopBasic)
                        numDown = 0;
                    else
                    {
                        numDown = (int)((topCur - posTopBasic) / 40) + 1;
                    }

                    if (numDown > 0)
                    {
                        if (ergUnitGrps[20 - numDown].erObj != null)
                        {
                            startNew = curStartNr - numDown;
                            j = numDown;
                        }
                        else
                        {
                            for (j = 0; j < numDown; j++)
                            {
                                if (ergUnitGrps[19 - j].erObj == null)
                                {
                                    startNew = curStartNr - j;
                                    Canvas.SetTop(recScroll, 0);
                                    break;
                                }
                            }
                        }
                    }

                    if (numDown > 0)
                    {
                        if (j == numDown)
                        {
                            lasttop = posTopBasic + topCur % 40;
                            Canvas.SetTop(cvsEventRecord, posTopBasic + topCur % 40);
                        }
                        else
                        {
                            lasttop = posTopBasic;
                            Canvas.SetTop(cvsEventRecord, posTopBasic);
                        }
                    }
                    else
                    {
                        lasttop = posTopBasic + topCur % 40;
                        Canvas.SetTop(cvsEventRecord, posTopBasic + topCur % 40);
                    }
                }

                refreshCvsList(startNew);

            }
        }
        private void cvsBgRecord_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this._bIsMouseDown)
            {
                this._bIsMouseDown = false;
                refreshCvsList(curStartNr);
                Canvas.SetTop(cvsEventRecord, posTopBasic);
            }
        }
        private void cvsRecord_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point tmp = e.GetPosition(cvsMain);

                    double topOld = Canvas.GetTop(cvsEventRecord);
                    double topNew = topOld + tmp.Y - curMousePos.Y;

                    if (topNew > limitsDrgeDown)
                        topNew = limitsDrgeDown;
                    if (topNew < limitsDragUp)
                        topNew = limitsDragUp;

                    Canvas.SetTop(cvsEventRecord, topNew);

                    double recTop = Canvas.GetTop(recScroll) + (topOld - topNew) * (40 * 28 - recScroll.Height) / 40 / (filterLst.Count - 28);
                    Canvas.SetTop(recScroll, recTop);

                    curMousePos = tmp;
                }
            }
        }

        private void alarmMsgShow()
        {
            refreshEvent();
        }
        private void paramMsgShow()
        {
            refreshEvent();
        }
        private void sysMsgShow()
        {
            refreshEvent();
        }
        private void loadMsgShow()
        {
            refreshEvent();
        }

        public void refreshEvent()
        {
            valmoWin.update();
            Canvas.SetTop(cvsEventRecord, posTopBasic);
            Canvas.SetTop(recScroll, 0);
            getFilterLst();
            refreshCvsList(-20);
        }

        /// <summary>
        /// 结果筛选
        /// </summary>
        void getFilterLst()
        {
            filterLst.Clear();

            for (int i = 0; i < curItemlst.Count; i++)
            {
                bool tmp;
                tmp = filterType(curItemlst[i]) 
                    & filterUser(curItemlst[i])
                    & filterTime(curItemlst[i]);

                if(tmp)
                {
                    filterLst.Add(curItemlst[i]);
                }
            }

            eventMgrObj.filterLst = filterLst;

            lbTotalListNum.Content = filterLst.Count;

            if (filterLst.Count < 28)
            {
                this.recScroll.Opacity = 0;
                recScrollBack.Opacity = 0;
            }
            else
            {
                this.recScroll.Opacity = 1;
                recScrollBack.Opacity = 1;
                this.recScroll.Height = 40 * 28 * 28 / filterLst.Count;
            }
            
        }

        /// <summary>
        /// 缓存历史记录
        /// 将filterLst[0]的值赋给ergUnitGrps[|startNum|]
        /// 依次类推
        /// </summary>
        /// <param name="startNum"></param>
        void refreshCvsList(int startNum)
        {
            int curErgNr = 0;
            int curItemNr ;

            for (curItemNr = startNum + curErgNr; curErgNr < 68 && curItemNr < filterLst.Count; curItemNr++)
            {
                if (curItemNr < 0)
                {
                    ergUnitGrps[curErgNr++].setValue(null, curItemNr);
                }
                else
                {
                    ergUnitGrps[curErgNr++].setValue(filterLst[curItemNr], curItemNr);
                }
            }

            curItemNr = curErgNr + startNum;
            for (int i = curErgNr; i < 68; i++)
            {
                ergUnitGrps[i].setValue(null, curItemNr++);
            }

            curStartNr = startNum;
        }

        private bool filterUser(recUnit ergObj)
        {
            return filterUserCtrl1.check(ergObj.userName);
        }
        private bool filterTime(recUnit ergObj)
        {
            return filterTimeCtrl1.check(ergObj);
        }
        private bool filterType(recUnit ergObj)
        {
            switch (ergObj.type)
            {
                case recType.alarmType:
                    if (!alarmCtrl.active)
                        return false;
                    break;
                case recType.logType:
                    if (!loadCtrl.active)
                        return false;
                    break;
                case recType.operateType:
                    if (!paramCtrl.active)
                        return false;
                    break;
                case recType.sysType:
                    if (!sysCtrl.active)
                        return false;
                    break;
                default :
                    return false;
            }
            return true;
        }
    }
}
