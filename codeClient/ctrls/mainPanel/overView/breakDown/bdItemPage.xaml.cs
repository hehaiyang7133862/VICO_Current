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
using System.Collections.Specialized;

namespace nsVicoClient.ctrls
{
    public partial class bdItemPage : UserControl
    {
        public static Dictionary<typeName, barType> AnaBlocks = new Dictionary<typeName, barType>()
        {
            {typeName.noactive       	,	barType.noactive       	},
            {typeName.undefined      	,	barType.undefined      	},
            {typeName.moldclose      	,	barType.moldclose      	},
            {typeName.moldopen       	,	barType.moldopen       	},
            {typeName.moldopened     	,	barType.moldopened     	},
            {typeName.carriageFor    	,	barType.carriageFor    	},
            {typeName.carriageBack   	,	barType.carriageBack   	},
            {typeName.nozzleOpen     	,	barType.nozzleOpen     	},
            {typeName.injection      	,	barType.injection      	},
            {typeName.holding        	,	barType.holding        	},
            {typeName.presRelease    	,	barType.pressRelease    },
            {typeName.cooling        	,	barType.cooling        	},
            {typeName.preSuckback    	,	barType.preSuckback    	},
            {typeName.plasticaz      	,	barType.plasticaz      	},
            {typeName.suckback       	,	barType.suckback       	},
            {typeName.nozzleClose    	,	barType.nozzleClose    	},
            {typeName.ejectorFor     	,	barType.ejectorFor     	},
            {typeName.ejectorShake   	,	barType.ejectorShake   	},
            {typeName.ejectorBack    	,	barType.ejectorBack    	},
            {typeName.coreAin        	,	barType.coreIn        	},
            {typeName.coreAout       	,	barType.coreOut       	},
            {typeName.coreBin        	,	barType.coreIn        	},
            {typeName.coreBout       	,	barType.coreOut       	},
            {typeName.coreCin        	,	barType.coreIn        	},
            {typeName.coreCout       	,	barType.coreOut       	},
            {typeName.coreDin        	,	barType.coreIn        	},
            {typeName.coreDout       	,	barType.coreOut       	},
            {typeName.coreEin        	,	barType.coreIn        	},
            {typeName.coreEout       	,	barType.coreOut       	},
            {typeName.coreFin        	,	barType.coreIn        	},
            {typeName.coreFout       	,	barType.coreOut       	},
            {typeName.airBlow1       	,	barType.airBlow       	},
            {typeName.airBlow2       	,	barType.airBlow       	},
            {typeName.airBlow3       	,	barType.airBlow       	},
            {typeName.airBlow4       	,	barType.airBlow       	},
            {typeName.airBlow5       	,	barType.airBlow       	},
            {typeName.airBlow6       	,	barType.airBlow       	},
            {typeName.airBlow7       	,	barType.airBlow       	},
            {typeName.airBlow8       	,	barType.airBlow       	},
            {typeName.airBlow9       	,	barType.airBlow       	},
            {typeName.airBlow10      	,	barType.airBlow      	},
            {typeName.airBlow11      	,	barType.airBlow      	},
            {typeName.airBlow12      	,	barType.airBlow      	},
            {typeName.valveGate1     	,	barType.valveGate     	},
            {typeName.valveGate2     	,	barType.valveGate     	},
            {typeName.valveGate3     	,	barType.valveGate     	},
            {typeName.valveGate4     	,	barType.valveGate     	},
            {typeName.takeout        	,	barType.takeout        	},
            {typeName.tableIn        	,	barType.tableIn        	},
            {typeName.tableOut       	,	barType.tableOut      	}
        };

        string[] machionState1 = new string[32]{
            "lanKey063",//"合模" bit0
            "lanKey064",//"开模" bit1
            "lanKey065",//"等待机械手信号" bit2
            "",//周期间隔 bit3
            "lanKey067",//"调模前进" bit4
            "lanKey068",//"调模后退" bit5
            "lanKey070",//"顶针进" bit6
            "lanKey069",//"顶针退" bit7
            "lanKey071",//"等待产品掉落" bit8
            "lanKey072",//"转盘正转" bit9
             "lanKey073",//"转盘反转" bit10
             "",//"吹气1" bit11
             "",//"吹气2" bit12
             "",//"吹气3" bit13
             "",//"吹气4" bit14
             "lanKey078",//   "座台进",//bit15
             "lanKey079",//   "座台退",//bit16
             "lanKey080",//   "注射",//bit17
             "lanKey081",//   "塑化",//bit18
             "lanKey082",//   "松退",//bit19
             "lanKey083",//  "冷却",//bit20
             "",//   "吹气7",//bit21
             "",//   "吹气8",//bit22
             "lanKey086",//  "自动调模",//bit23
             "lanKey087",//   "自动清料",//bit24
             "",//   "	",//bit25
             "lanKey089",//   "中子A退",//bit26
             "lanKey090",//   "中子A进",//bit27
             "lanKey091",//   "中子B退",//bit28
             "lanKey092",//   "中子B进",//bit29
             "lanKey093",//   "中子C退",//bit30
             "lanKey094",//   "中子C进"//bit31
        };
        string[] machionState2 = new string[32]{
            "lanKey095",//     "丝杆润滑",//bit0
            "lanKey096",//     "吹气9",//bit1
            "lanKey097",//     "吹气10",//bit2
            "lanKey098",//     "吹气11",//bit3
            "lanKey099",//     "吹气12",//bit4
            "lanKey100",//     "中子D退",//bit5
            "lanKey101",//     "中子D进",//bit6
            "lanKey102",//     "中子E退",//bit7
            "lanKey103",//     "中子E进",//bit8
            "lanKey104",//     "中子F退",//bit9
            "lanKey105",//     "中子F进",//bit10
            "lanKey106",//     "机绞润滑",//bit11
            "lanKey107",//     "吹气1",//bit12
            "lanKey108",//     "吹气2",//bit13
            "lanKey109",//     "吹气3",//bit14
            "lanKey110",//     "吹气4",//bit15
            "lanKey111",//    "吹气5",//bit16
            "lanKey112",//    "吹气6",//bit17
            "lanKey113",//    "吹气7",//bit18
            "lanKey114",//    "吹气8",//bit19
            "",//    "",//bit20
            "",//    "",//bit21
            "",//    "",//bit22
            "",//    "",//bit23
            "",//    "",//bit24
            "",//    "",//bit25
            "",//   "",//bit26
            "",//   "",//bit27
            "",//   "",//bit28
            "",//   "",//bit29
            "",//   "",//bit30
            "Sys108",//   "模具开启时间",//bit31
        };


        private int time_MoldClose = 0;
        private int time_Injection = 0;
        private int time_Holding = 0;
        private int time_Cooling = 0;
        private int time_MoldOpen_Start = 0;
        private int time_MoldClose_Start = 0;
        private bool _bIsBaseOn = false;
        public static double sRate = 0;
        private double _staticCycleTm = 0;
        public double staticCycleTime
        {
            set
            {
                _staticCycleTm = value;

                if ((_staticCycleTm > baseCycleTime) && (_staticCycleTm > 0))
                {
                    sRate = 600 / _staticCycleTm;
                }
            }
            get
            {
                return _staticCycleTm;
            }
        }
        private double _baseCycleTime = 0;
        public double baseCycleTime
        {
            set
            {
                _baseCycleTime = value;

                if ((_baseCycleTime > staticCycleTime) && (_baseCycleTime > 0))
                {
                    sRate = 600 / _baseCycleTime;
                }
            }
            get
            {
                return _baseCycleTime;
            }
        }

        private List<bdItemBarCtrl> lstBarCtrl = new List<bdItemBarCtrl>();
        /// <summary>
        /// 当前数据地址
        /// </summary>
        private uint curDataAddr;
        /// <summary>
        /// 基准数据地址
        /// </summary>
        private uint baseDataAddr;
        /// <summary>
        /// 静态数据地址
        /// </summary>
        private uint staticDataAddr;

        objUnit objOpAction1;
        objUnit objOpAction2;

        public bdItemPage()
        {
            try
            {            
                InitializeComponent();

                for (int i = 0; i < 50; i++)
                {
                    bdItemBarCtrl bar = new bdItemBarCtrl();
                    lstBarCtrl.Add(bar);
                    cvsBars.Children.Add(bar);
                }

                valmoWin.dv.PrdPr[269].addHandle(updateRate);
                valmoWin.dv.PrdPr[270].addHandle(handleSetBase, plcLstSpd.mapType);
                valmoWin.dv.PrdPr[271].addHandle(handleRefreshBlock, plcLstSpd.mapType);
                valmoWin.dv.PrdPr[272].addHandle(handlePauseState, plcLstSpd.mapType);
                valmoWin.dv.PrdPr[267].addMap();
                valmoWin.dv.PrdPr[104].addHandle(TaskTimer);

                objOpAction1 = valmoWin.dv.SysPr[106];
                objOpAction1.addHandle(handleSysPr_106);
                objOpAction2 = valmoWin.dv.SysPr[107];
                objOpAction2.addHandle(handleSysPr_106);

                valmoWin.lstStartUpInit.Add(startUpInit);
            }
            catch (Exception ex)
            {
                vm.perror("[bdItemPage_init]" + ex.ToString());
            }
        }

        private void TaskTimer(objUnit obj)
        {
            double temp = obj.vDbl;

            string time = string.Empty;

            time += ((int)(temp / 3600)).ToString() + ":";
            time += ((int)(temp % 3600 / 60)).ToString().PadLeft(2, '0') + ":";
            time += ((int)(temp % 60)).ToString().PadLeft(2, '0');
            lbTime.Content = time;
        }

        private string strMachionState1 = string.Empty;
        private void updateMachineAction1(objUnit obj)
        {
            strMachionState1 = string.Empty;

            int tmp = obj.value;

            for (int i = 0; i < 32; i++)
            {
                if (((tmp >> i) & 0x01) == 1)
                {
                    if (machionState1[i] != "")
                    {
                        if (strMachionState1 == "")
                            strMachionState1 += valmoWin.dv.getCurDis(machionState1[i]);
                        else
                            strMachionState1 += "+" + valmoWin.dv.getCurDis(machionState1[i]);
                    }
                }
            }

            lbCurFocus.Content = strMachionState1 + "+" + strMachionState2;
        }
        private string strMachionState2 = string.Empty;
        private void updateMachineAction2(objUnit obj)
        {
            strMachionState2 = string.Empty;

            int tmp = obj.value;

            for (int i = 0; i < 32; i++)
            {
                if (((tmp >> i) & 0x01) == 1)
                {
                    if (machionState2[i] != "")
                    {
                        if (strMachionState2 == "")
                            strMachionState2 += valmoWin.dv.getCurDis(machionState1[i]);
                        else
                            strMachionState2 += "+" + valmoWin.dv.getCurDis(machionState1[i]);
                    }
                }
            }

            lbCurFocus.Content = strMachionState1 + "+" + strMachionState2;
        }
       
        private void handleSysPr_106(objUnit obj)
        {
            strMachionState1 = "";
            for (int i = 0; i < 32; i++)
            {
                int nr = (objOpAction1.value >> i) & 0x01;
                if (nr == 1)
                {
                    if (machionState1[i] != "")
                    {
                        if (strMachionState1 == "")
                            strMachionState1 += valmoWin.dv.getCurDis(machionState1[i]);
                        else
                            strMachionState1 += "+" + valmoWin.dv.getCurDis(machionState1[i]);
                    }
                }
                
            }
            strMachionState2 = "";
            for (int i = 0; i < 32; i++)
            {
                if (((objOpAction2.value >> i) & 0x01) == 1)
                {
                    if (machionState2[i] != "")
                    {
                        if (strMachionState2 == "")
                            strMachionState2 += valmoWin.dv.getCurDis(machionState2[i]);
                        else
                            strMachionState2 += "+" + valmoWin.dv.getCurDis(machionState2[i]);
                    }
                }
            }
            if (strMachionState1 == "")
                lbCurFocus.Content = strMachionState2;
            else
            {
                if (strMachionState2 == "")
                    lbCurFocus.Content = strMachionState1;
                else
                    lbCurFocus.Content = strMachionState1 + "+" + strMachionState2;
            }
        }
        int flagParseState = 0;

        /// <summary>
        /// 暂停状态
        /// </summary>
        /// <param name="obj">Prd272</param>
        private void handlePauseState(objUnit obj)
        {
            if (obj.value == 2)
            {
                flagParseState = 2;
                imgParse.Visibility = Visibility.Visible;
                imgRestart.Visibility = Visibility.Hidden;
                imgRefresh.Visibility = Visibility.Visible;
                
            }
            else if(obj.value == 0)
            {
                flagParseState = 0;
                imgParse.Visibility = Visibility.Hidden;
                imgRestart.Visibility = Visibility.Visible;
                imgRefresh.Visibility = Visibility.Hidden;
            }
        }

        private void handleSetBase(objUnit obj)
        {
            if (obj.value == 0)
            {
                btnSetReference.Background = Brushes.Transparent;

                if (obj.valueOld == 1)
                {
                    _bIsBaseOn = true;

                    btnSetReference.SetResourceReference(Label.ContentProperty, "OI_TurnOffReference");
                }
            }
            else if (obj.value == 1)
            {
                btnSetReference.Background = Brushes.LightBlue;
            }
        }

        private void lbSetRefrence_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_bIsBaseOn == false)
            {
                valmoWin.dv.PrdPr[270].valueNew = 1;
            }
            else
            {
                _bIsBaseOn = false;
                btnSetReference.SetResourceReference(Label.ContentProperty, "OI_SetReference");
            }
        }

        private int TotalMoldNumber = 0;

        private void updateRate(objUnit obj)
        {
            staticCycleTime = obj.vDbl;
        }

        private void handleRefreshBlock(objUnit obj)
        {
            if (obj.value == 1)
            {
                obj.valueNew = 0;

                if (sRate > 0)
                {
                    cvsCursor.Visibility = Visibility.Visible;
                    lbCursor.Content = (Canvas.GetLeft(cvsCursor) / sRate).ToString("0.00");

                    if (_bIsBaseOn == true)
                    {
                        uint[] blocksCur = new uint[valmoWin.dv.PrdPr[267].value];

                        if (flagParseState == 2)
                        {
                            Lasal32.GetData(blocksCur, staticDataAddr, blocksCur.Length);
                        }
                        else
                        {
                            Lasal32.GetData(blocksCur, curDataAddr, blocksCur.Length);
                        }

                        uint[] blocksBase = new uint[valmoWin.dv.PrdPr[267].value];
                        Lasal32.GetData(blocksBase, baseDataAddr, blocksBase.Length);

                        int j = 0;
                        for (int i = 0; i < blocksCur.Length / 32; i++)
                        {
                            baseCycleTime = valmoWin.dv.PrdPr[269].getDblValue(Convert.ToInt32(blocksBase[0]));
                            uint ActionName = blocksCur[i * 8 + 3];

                            switch (ActionName)
                            {
                                case 3:
                                    time_MoldOpen_Start = Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                case 2:
                                    time_MoldClose_Start = Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    time_MoldClose = Convert.ToInt32(blocksCur[i * 8 + 5]) - Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                case 8:
                                    time_Injection = Convert.ToInt32(blocksCur[i * 8 + 5]) - Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                case 9:
                                    time_Holding = Convert.ToInt32(blocksCur[i * 8 + 5]) - Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                case 11:
                                    time_Cooling = Convert.ToInt32(blocksCur[i * 8 + 5]) - Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                default:
                                    break;
                            }

                            Anablocks sblockCur = new Anablocks();
                            sblockCur.Active = blocksCur[i * 8 + 1];
                            sblockCur.Move = blocksCur[i * 8 + 2];
                            sblockCur.Name = (typeName)ActionName;
                            sblockCur.tStart = Math.Round(valmoWin.dv.PrdPr[269].getDblValue(Convert.ToInt32(blocksCur[i * 8 + 4])), 3);
                            sblockCur.tEnd = Math.Round(valmoWin.dv.PrdPr[269].getDblValue(Convert.ToInt32(blocksCur[i * 8 + 5])), 3);
                            sblockCur.seqStart = blocksCur[i * 8 + 6];
                            sblockCur.seqEnd = blocksCur[i * 8 + 7];

                            Anablocks sblockBase = new Anablocks();
                            sblockBase.Active = blocksBase[i * 8 + 1];
                            sblockBase.Move = blocksBase[i * 8 + 2];
                            sblockBase.Name = (typeName)blocksBase[i * 8 + 3];
                            sblockBase.tStart = Math.Round(valmoWin.dv.PrdPr[269].getDblValue(Convert.ToInt32(blocksBase[i * 8 + 4])), 3);
                            sblockBase.tEnd = Math.Round(valmoWin.dv.PrdPr[269].getDblValue(Convert.ToInt32(blocksBase[i * 8 + 5])), 3);
                            sblockBase.seqStart = blocksBase[i * 8 + 6];
                            sblockBase.seqEnd = blocksBase[i * 8 + 7];

                            if (sblockBase.Name == sblockCur.Name)
                            {
                                if (typeName.IsDefined(typeof(typeName), sblockCur.Name) && (sblockCur.Name != typeName.noactive))
                                {
                                    lstBarCtrl[j].visiable = Visibility.Visible;
                                    lstBarCtrl[j].type = sblockCur.Name;
                                    lstBarCtrl[j].setCurAction(sblockCur);
                                    lstBarCtrl[j].setBaseAction(sblockBase);
                                    j++;
                                }
                            }
                            else
                            {
                                if (typeName.IsDefined(typeof(typeName), sblockCur.Name) && (sblockCur.Name != typeName.noactive))
                                {
                                    lstBarCtrl[j].visiable = Visibility.Visible;
                                    lstBarCtrl[j].type = sblockCur.Name;
                                    lstBarCtrl[j].setCurAction(sblockCur);
                                    j++;
                                }

                                if (typeName.IsDefined(typeof(typeName), sblockCur.Name) && (sblockCur.Name != typeName.noactive))
                                {
                                    lstBarCtrl[j].visiable = Visibility.Visible;
                                    lstBarCtrl[j].type = sblockBase.Name;
                                    lstBarCtrl[j].setBaseAction(sblockBase);
                                    j++;
                                }
                            }
                        }

                        for (int i = j; i < 50; i++)
                        {
                            lstBarCtrl[i].visiable = Visibility.Hidden;
                        }

                        if (valmoWin.dv.PrdPr[96].value > TotalMoldNumber)
                        {
                            double tmep = valmoWin.dv.PrdPr[269].value;

                            if (tmep <= 0)
                            {
                                return;
                            }

                            lb1.Content = "0.0%";
                            if ((time_MoldClose < tmep) && (time_MoldClose > 0))
                            {
                                lb1.Content = ((time_MoldClose) * 100 / tmep).ToString("0.0") + "%";
                            }

                            lb2.Content = "0.0%";
                            if (time_Injection != 0)
                            {
                                lb2.Content = ((time_Injection + time_Holding + time_Cooling) * 100 / tmep).ToString("0.0") + "%";
                            }

                            lb3.Content = "0.0%";
                            if ((time_MoldOpen_Start < tmep) && (time_MoldClose_Start > 0))
                            {
                                lb3.Content = ((tmep - time_MoldOpen_Start) * 100 / tmep).ToString("0.0") + "%";
                            }

                            TotalMoldNumber = valmoWin.dv.PrdPr[96].value;
                        }
                    }
                    else
                    {
                        uint[] blocksCur = new uint[valmoWin.dv.PrdPr[267].value];

                        if (flagParseState == 2)
                        {
                            Lasal32.GetData(blocksCur, staticDataAddr, blocksCur.Length);
                        }
                        else
                        {
                            Lasal32.GetData(blocksCur, curDataAddr, blocksCur.Length);
                        }

                        int j = 0;

                        for (int i = 0; i < blocksCur.Length / 32; i++)
                        {
                            uint ActionName = blocksCur[i * 8 + 3];

                            switch (ActionName)
                            {
                                case 3:
                                    time_MoldOpen_Start = Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                case 2:
                                    time_MoldClose_Start = Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    time_MoldClose = Convert.ToInt32(blocksCur[i * 8 + 5]) - Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                case 8:
                                    time_Injection = Convert.ToInt32(blocksCur[i * 8 + 5]) - Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                case 9:
                                    time_Holding = Convert.ToInt32(blocksCur[i * 8 + 5]) - Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                case 11:
                                    time_Cooling = Convert.ToInt32(blocksCur[i * 8 + 5]) - Convert.ToInt32(blocksCur[i * 8 + 4]);
                                    break;
                                default:
                                    break;
                            }

                            Anablocks sblockCur = new Anablocks();
                            sblockCur.Active = blocksCur[i * 8 + 1];
                            sblockCur.Move = blocksCur[i * 8 + 2];
                            sblockCur.Name = (typeName)ActionName;
                            sblockCur.tStart = Math.Round(valmoWin.dv.PrdPr[269].getDblValue(Convert.ToInt32(blocksCur[i * 8 + 4])), 3);
                            sblockCur.tEnd = Math.Round(valmoWin.dv.PrdPr[269].getDblValue(Convert.ToInt32(blocksCur[i * 8 + 5])), 3);
                            sblockCur.seqStart = blocksCur[i * 8 + 6];
                            sblockCur.seqEnd = blocksCur[i * 8 + 7];
                            Anablocks sblockBase = new Anablocks();
                            sblockBase.Active = blocksCur[i * 8 + 1];
                            sblockBase.Move = blocksCur[i * 8 + 1];
                            sblockBase.Name = (typeName)ActionName;
                            sblockBase.tStart = 0;
                            sblockBase.tEnd = 0;
                            sblockBase.seqStart = blocksCur[i * 8 + 6];
                            sblockBase.seqEnd = blocksCur[i * 8 + 7];

                            if (typeName.IsDefined(typeof(typeName), sblockCur.Name) && (sblockCur.Name != typeName.noactive))
                            {
                                lstBarCtrl[j].visiable = Visibility.Visible;
                                lstBarCtrl[j].type = sblockCur.Name;
                                lstBarCtrl[j].setCurAction(sblockCur);
                                lstBarCtrl[j].setBaseAction(sblockBase);
                                j++;
                            }
                        }

                        for (int i = j; i < 50; i++)
                        {
                            lstBarCtrl[i].visiable = Visibility.Hidden;
                        }

                        if (valmoWin.dv.PrdPr[96].value > TotalMoldNumber)
                        {
                            double tmep = valmoWin.dv.PrdPr[269].value;

                            if (tmep <= 0)
                            {
                                return;
                            }

                            lb1.Content = "0.0%";
                            if ((time_MoldClose < tmep) && (time_MoldClose > 0))
                            {
                                lb1.Content = ((time_MoldClose) * 100 / tmep).ToString("0.0") + "%";
                            }

                            lb2.Content = "0.0%";
                            if (time_Injection != 0)
                            {
                                lb2.Content = ((time_Injection + time_Holding + time_Cooling) * 100 / tmep).ToString("0.0") + "%";
                            }

                            lb3.Content = "0.0%";
                            if ((time_MoldOpen_Start < tmep) && (time_MoldClose_Start > 0))
                            {
                                lb3.Content = ((tmep - time_MoldOpen_Start) * 100 / tmep).ToString("0.0") + "%";
                            }

                            TotalMoldNumber = valmoWin.dv.PrdPr[96].value;
                        }
                    }
                }
                else
                {
                    cvsCursor.Visibility = Visibility.Hidden;
                }
            }
        }

        public static int lastCycleTime = 0;

        Point mousePoint;
        private bool isMouseDown = false;
        private void cvsTmAll_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(cvsMain);
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private void startUpInit()
        {
            curDataAddr = (uint)valmoWin.dv.PrdPr[265].valueNew;
            baseDataAddr = (uint)valmoWin.dv.PrdPr[266].valueNew;
            staticDataAddr = (uint)valmoWin.dv.PrdPr[273].valueNew;
        }

        private bool bIsMouseDown = false;
        private Point PointMouseDown;
        private Point PointMousePosCur;
        private void cvsCurValue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sRate > 0)
            {
                bIsMouseDown = true;
                PointMouseDown = e.GetPosition(cvsCurValue);
                cvsCursor.Visibility = Visibility.Visible;
                double left = PointMouseDown.X;
                if (left < 0)
                    left = 0;
                if (left > 600)
                    left = 600;
                lbCursor.Content = (left / sRate).ToString("0.00");
                Canvas.SetLeft(cvsCursor, left);
            }
        }

        private void cvsCurValue_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    PointMousePosCur = e.GetPosition(cvsCurValue);

                    double left = PointMousePosCur.X;
                    if (left < 0)
                        left = 0;
                    if (left > 600)
                        left = 600;
                    lbCursor.Content = (left / sRate).ToString("0.00");
                    Canvas.SetLeft(cvsCursor, left);
                }
            }
        }

        private void cvsCurValue_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bIsMouseDown = false;
        }

        private void cvsCurValue_MouseLeave(object sender, MouseEventArgs e)
        {
            bIsMouseDown = false;
        }
        bool isParseDown = false;
        private void imgParse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isParseDown = true;
            recParse.Fill = Brushes.LightBlue;
        }

        private void imgParse_MouseLeave(object sender, MouseEventArgs e)
        {
            imgParse_MouseUp(null, null);
        }

        private void imgParse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isParseDown)
            {
                isParseDown = false;
                recParse.Fill = Brushes.Transparent;
                valmoWin.dv.PrdPr[272].valueNew = 0;
            }
        }

        bool isRestartDown = false;

        private void imgRestart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isRestartDown = true;
            recParse.Fill = Brushes.LightBlue;
        }

        private void imgRestart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isRestartDown)
            {
                isRestartDown = false;
                recParse.Fill = Brushes.Transparent;
                valmoWin.dv.PrdPr[272].valueNew = 1;
            }
        }

        private void imgRestart_MouseLeave(object sender, MouseEventArgs e)
        {
            imgRestart_MouseUp(null, null);
        }

        bool isRefreshDown = false;

        private void recRefresh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isRefreshDown = true;
            recRefresh.Fill = Brushes.LightBlue;
        }

        private void recRefresh_MouseLeave(object sender, MouseEventArgs e)
        {
            recRefresh_MouseUp(null, null);
        }

        private void recRefresh_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isRefreshDown)
            {
                isRefreshDown = false;
                recRefresh.Fill = Brushes.Transparent;
                valmoWin.dv.PrdPr[271].valueNew = 3;
            }
        }
    }
}
