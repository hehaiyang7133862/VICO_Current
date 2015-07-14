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
    /// Interaction logic for tbMenuCtrl.xaml
    /// </summary>
    public partial class tbMenuCtrl : UserControl
    {
        public tbMenuCtrl()
        {
            InitializeComponent();
        }
        public void setFocus()
        {
            switch (iprCtrl.curUnit.sActName)
            {
                case (int)menuType.ipr_op05_ejectorback:
                    {
                        tbMenu.SelectedItem = menu_ejectorback;
                        switch (iprCtrl.curUnit.sOperateType)
                        {
                            case 0:
                                lbejectorback2.Content = valmoWin.dv.getCurDis("lanKey981");//"顶针到底"
                                break;
                            case 1:
                                {
                                    iprCtrl.curUnit.get_sValueA();
                                    lbejectorback2.Content = iprCtrl.curUnit.getStrValueA();
                                }
                                break;
                        }

                    }
                    break;
                case (int)menuType.ipr_op04_ejectorfor:
                    {
                        tbMenu.SelectedItem = menu_ejectorfor;
                        switch (iprCtrl.curUnit.sOperateType)
                        {
                            case 0:
                                lbejectorfor2.Content = valmoWin.dv.getCurDis("lanKey981");//"顶针到底";
                                break;
                            case 1:
                                {
                                    iprCtrl.curUnit.get_sValueA();
                                    lbejectorfor2.Content = iprCtrl.curUnit.getStrValueA();
                                }
                                break;
                        }
                    }
                    break;
                case (int)menuType.ipr_op06_safetydoorClose:
                    {
                        tbMenu.SelectedItem = menu_safetydoorClose;
                        lbsafetydoorClose2.Visibility = Visibility.Hidden;
                    }
                    break;
                case (int)menuType.ipr_op07_safetydoorOpen:
                    {
                        tbMenu.SelectedItem = menu_safetydoorOpen;
                    }
                    break;
                case (int)menuType.ipr_op08_CarrriageFWD:
                    {
                        tbMenu.SelectedItem = menu_CarrriageFWD;
                        iprCtrl.curUnit.get_sValueA();
                    }
                    break;
                case (int)menuType.ipr_op09_CarrriageBWD:
                    {
                        tbMenu.SelectedItem = menu_CarrriageBWD;
                    }
                    break;
                case (int)menuType.ipr_op10_ShackMold:
                    {
                        tbMenu.SelectedItem = menu_MoldCompress;
                    }
                    break;
                case (int)menuType.ipr_op14_Charge:
                    {
                        tbMenu.SelectedItem = menu_Charge;
                    }
                    break;
                case (int)menuType.ipr_op17_Air:
                    {
                        tbMenu.SelectedItem = menu_Air;
                        iprCtrl.curUnit.get_sFuncSelect();
                        iprCtrl.curUnit.get_sValueB();
                        lbAir1.Content = valmoWin.dv.getCurDis("lanKey959") + (iprCtrl.curUnit.sFuncSelect + 1);
                        lbAir2.Content = valmoWin.dv.tmTypeObj.getStrValue(iprCtrl.curUnit.sValueB) + objUnit.unitBase[UnitType.Tm_s];
                    }
                    break;
                case (int)menuType.ipr_op16_Cores:
                    {
                        tbMenu.SelectedItem = menu_Core;
                        iprCtrl.curUnit.get_sFuncSelect();
                        iprCtrl.curUnit.get_sOperateType();
                        string coreName = "";
                        switch (iprCtrl.curUnit.sFuncSelect)
                        {
                            case 0: coreName = "A"; break;
                            case 1: coreName = "B"; break;
                            case 2: coreName = "C"; break;
                            case 3: coreName = "D"; break;
                            case 4: coreName = "E"; break;
                            case 5: coreName = "F"; break;
                        }
                        lbCore1.Content = valmoWin.dv.getCurDis("lanKey960") + coreName;
                        if (iprCtrl.curUnit.sOperateType == 0)
                            lbCore2.Content = valmoWin.dv.getCurDis("lanKey984");
                        else
                            lbCore2.Content = valmoWin.dv.getCurDis("lanKey985");
                    }
                    break;
                case (int)menuType.ipr_op21_Rottable:
                    {
                        tbMenu.SelectedItem = menu_Rotate;
                    }
                    break;
                case (int)menuType.ipr_op19_Output:
                    {
                        tbMenu.SelectedItem = menu_Output;
                        switch (iprCtrl.curUnit.sFuncSelect)
                        {
                            case 0:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey982");//"数字输出1";
                                break;
                            case 1:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey983");//"数字输出2";
                                break;
                            case 2:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey986");//"数字输出3";
                                break;
                            case 3:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey987");//"数字输出4";
                                break;
                            case 4:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey988");//"报警红灯";
                                break;
                            case 5:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey989");//"报警黄灯";
                                break;
                        }
                        lbOutput3.Content = (iprCtrl.curUnit.sValueA == 0) ? "OFF" : "ON";
                    }
                    break;
                case (int)menuType.ipr_op22_Nozzle:
                    {
                        tbMenu.SelectedItem = menu_Nozzle;
                        iprCtrl.curUnit.get_sValueA();
                        iprCtrl.curUnit.get_sValueB();
                    }
                    break;
                case (int)menuType.ipr_op23_Valvegate:
                    {
                        tbMenu.SelectedItem = menu_Valvegate;
                        iprCtrl.curUnit.get_sFuncSelect();
                        iprCtrl.curUnit.get_sOperateType();
                        lbValvegate1.Content = valmoWin.dv.getCurDis("lanKey990") + iprCtrl.curUnit.sFuncSelect;
                        if (iprCtrl.curUnit.sOperateType == 1)
                            lbValvegate2.Content = valmoWin.dv.getCurDis("lanKey298");//"开";
                        else
                            lbValvegate2.Content = valmoWin.dv.getCurDis("lanKey299");//"关";
                    }
                    break;
                case (int)menuType.ipr_op03_delay:
                    {
                        tbMenu.SelectedItem = menu_delay;
                        iprCtrl.curUnit.get_sValueA();
                        lbdelay2.Content = iprCtrl.curUnit.getStrValueA();
                    }
                    break;
                case (int)menuType.ipr_op18_Variable:
                    {
                        tbMenu.SelectedItem = menu_Calculater;
                        iprCtrl.curUnit.get_sValueA();
                        iprCtrl.curUnit.get_sFuncSelect();
                        switch (iprCtrl.curUnit.sFuncSelect)
                        {
                            case 0:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey991");//"变量A";
                                break;
                            case 1:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey992");//"变量B";
                                break;
                            case 2:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey993");//"变量C";
                                break;
                            case 3:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey994");//"变量D";
                                break;
                            case 4:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey995");//"标记模数";
                                break;
                            case 5:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey996");//"不良品数";
                                break;
                            case 6:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey997");//"生产模数";
                                break;
                        }

                        switch (iprCtrl.curUnit.sOperateType)
                        {
                            case 0:
                                lbCalculater3.Content = "= " + iprCtrl.curUnit.sValueA;
                                break;
                            case 1:
                                lbCalculater3.Content = "+ " + iprCtrl.curUnit.sValueA;
                                break;
                            case 2:
                                lbCalculater3.Content = "- " + iprCtrl.curUnit.sValueA;
                                break;
                        }

                    }
                    break;
                case (int)menuType.ipr_op02_wait:
                    #region 等待
                    {
                        tbMenu.SelectedItem = menu_wait;
                        //imgGrp[19].Opacity = 1;
                        lbwait1.Content = valmoWin.dv.getCurDis("lanKey968");// "等待";
                        //lbwait2.Visibility = Visibility.Visible;
                        //lbwait3.Visibility = Visibility.Visible;
                        switch (iprCtrl.curUnit.sFuncSelect)
                        {
                            case 0://数字输入
                                {
                                    switch (iprCtrl.curUnit.sValueE)
                                    {
                                        case 0:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey998");//"数字输入1";
                                            break;
                                        case 1:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey999");//"数字输入2";
                                            break;
                                        case 2:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1000");//"数字输入3";
                                            break;
                                        case 3:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1001");//"数字输入4";
                                            break;
                                    }
                                    if (iprCtrl.curUnit.sValueA == 0)
                                    {
                                        lbwait3.Content = "OFF";
                                    }
                                    else
                                    {
                                        lbwait3.Content = "ON";
                                    }
                                }
                                break;
                            case 1://位置
                                {
                                    switch (iprCtrl.curUnit.sValueE)
                                    {
                                        case 0:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1007");//"模板位置";
                                            break;
                                        case 1:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1008");//"顶针位置";
                                            break;
                                        case 2:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1002");//"注射位置";
                                            break;
                                        case 3:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey737");//"射台位置";
                                            break;
                                    }
                                    switch (iprCtrl.curUnit.sOperateType)
                                    {
                                        case 0:
                                            lbwait3.Content = "= " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                        case 1:
                                            lbwait3.Content = "> " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                        case 2:
                                            lbwait3.Content = "< " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                    }
                                }
                                break;
                            case 2://喷嘴接触
                                {
                                    lbwait2.Content = valmoWin.dv.getCurDis("lanKey1009");//"喷嘴接触";
                                    if (iprCtrl.curUnit.sValueA == 0)
                                    {
                                        lbwait3.Content = "OFF";
                                    }
                                    else
                                    {
                                        lbwait3.Content = "ON";
                                    }
                                }
                                break;
                            case 3://锁模力
                                {
                                    lbwait2.Content = valmoWin.dv.getCurDis("lanKey1018");//"锁模力";
                                    switch (iprCtrl.curUnit.sOperateType)
                                    {
                                        case 0:
                                            lbwait3.Content = "= " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                        case 1:
                                            lbwait3.Content = "> " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                        case 2:
                                            lbwait3.Content = "< " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                    }
                                }
                                break;
                            case 5://计数器
                                {
                                    switch (iprCtrl.curUnit.sValueE)
                                    {
                                        case 0:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1003");//"计数器1";
                                            break;
                                        case 1:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1004");//"计数器2";
                                            break;
                                        case 2:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1005");//"计数器3";
                                            break;
                                        case 3:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1006");//"计数器4";
                                            break;
                                        case 4:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey995");//"标记模数";
                                            break;
                                        case 5:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey996");//"不良品数";
                                            break;
                                        case 6:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey997");//"生产模数";
                                            break;
                                    }
                                    switch (iprCtrl.curUnit.sOperateType)
                                    {
                                        case 0:
                                            lbwait3.Content = "= " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                        case 1:
                                            lbwait3.Content = "> " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                        case 2:
                                            lbwait3.Content = "< " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                    }
                                }
                                break;
                            case 6://欧规信号
                                {
                                    switch (iprCtrl.curUnit.sValueE)
                                    {
                                        case 0:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1010");//"合模允许";
                                            break;
                                        case 1:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1011");//"模区安全";
                                            break;
                                        case 2:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1012");//"顶退允许";
                                            break;
                                        case 3:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1013");//"顶进允许";
                                            break;
                                        case 4:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1014");//"中子退允许";
                                            break;
                                        case 5:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1015");//"中子进允许";
                                            break;
                                    }
                                    lbwait3.Content = (iprCtrl.curUnit.sValueA == 0) ? "OFF" : "ON";

                                }
                                break;
                            case 7://注射压力
                                {
                                    lbwait2.Content = valmoWin.dv.getCurDis("lanKey285");//"注射压力";
                                    switch (iprCtrl.curUnit.sOperateType)
                                    {
                                        case 0:
                                            lbwait3.Content = "= " + iprCtrl.curUnit.getStrValueA();

                                            break;
                                        case 1:
                                            lbwait3.Content = "> " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                        case 2:
                                            lbwait3.Content = "< " + iprCtrl.curUnit.getStrValueA();
                                            break;
                                    }
                                }
                                break;

                        }

                    }
                    #endregion
                    break;
                case (int)menuType.ipr_op26_jump:
                    {
                        tbMenu.SelectedItem = menu_jump;
                        lbjump2.Content = valmoWin.dv.getCurDis("lanKey1016") + iprCtrl.curUnit.sValueC;
                    }
                    break;
                case (int)menuType.ipr_op12_Moldopen:
                    {
                        tbMenu.SelectedItem = menu_Moldopen;
                        if (iprCtrl.curUnit.sOperateType == 0)
                        {
                            lbMoldopen2.Content = valmoWin.dv.getCurDis("lanKey1017");// "到底";
                        }
                        else
                        {
                            lbMoldopen2.Content = iprCtrl.curUnit.getStrValueA();
                        }
                    }
                    break;
                case (int)menuType.ipr_op11_Moldclose:
                    {
                        tbMenu.SelectedItem = menu_Moldclose;
                        if (iprCtrl.curUnit.sOperateType == 0)
                        {
                            lbMoldclose2.Content = valmoWin.dv.getCurDis("lanKey1017");//"到底";
                        }
                        else
                        {
                            lbMoldclose2.Content = iprCtrl.curUnit.getStrValueA();
                        }
                    }
                    break;
                case (int)menuType.ipr_op13_Injection:
                    {
                        tbMenu.SelectedItem = menu_Injection;
                    }
                    break;
                case (int)menuType.ipr_op15_cooling:
                    {
                        tbMenu.SelectedItem = menu_cooling;
                    }
                    break;
                case (int)menuType.ipr_op30_waitstartsignal:
                    {
                        tbMenu.SelectedItem = menu_waitsignal;

                        switch (iprCtrl.curUnit.sFuncSelect)
                        {
                            case 0:
                                lbwaitstartsignal2.Content = App.Current.TryFindResource("lanKey154");
                                break;
                            case 1:
                                lbwaitstartsignal2.Content = App.Current.TryFindResource("lanKey149");
                                break;
                            case 2:
                                {
                                    lbwaitstartsignal2.Content = App.Current.TryFindResource("lanKey951");
                                    lbwaitstartsignal3.Content = valmoWin.dv.getCurDis("ipr_waitStartSignal_TouchCount") + iprCtrl.curUnit.getStrValueC();
                                }
                                break;
                            case 3:
                                {
                                    lbwaitstartsignal2.Content = App.Current.TryFindResource("lanKey952");
                                    lbwaitstartsignal3.Content = valmoWin.dv.getCurDis("ipr_waitStartSignal_TouchCount") + iprCtrl.curUnit.getStrValueC();
                                }
                                break;
                        }
                    }
                    break;
                case (int)menuType.ipr_op24_seteuromapsignal:
                    {
                        tbMenu.SelectedItem = menu_setEMSignal;
                        {
                            switch (iprCtrl.curUnit.sFuncSelect)
                            {
                                case 0:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_MoldClose");//合模允许
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    lbwaitstartsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                    break;
                                case 1:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_MoldSafety");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 2:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_EjectorBWD");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 3:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_EjectorFWD");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 4:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_CoreBWD");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 5:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_CoreFWD");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 6:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_MoldOpen");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case (int)menuType.ipr_op29_waiteuromapsignal:
                    {
                        tbMenu.SelectedItem = menu_waitEMsignal;
                        switch (iprCtrl.curUnit.sFuncSelect)
                        {
                            case 0:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_MoldClose");//合模允许
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 1:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_MoldOpen");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 2:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_EjectorBWD");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 3:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_EjectorFWD");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 4:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_CoreFWD");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 5:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_CoreBWD");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                        }
                    }
                    break;
            }
        }

        public void refreshLan(iprUnitObj curUnit)
        {
            switch (curUnit.sActName)
            {
                case (int)menuType.ipr_op05_ejectorback:
                    {
                        tbMenu.SelectedItem = menu_ejectorback;
                        switch (curUnit.sOperateType)
                        {
                            case 0:
                                lbejectorback2.Content = valmoWin.dv.getCurDis("lanKey981");//"顶针到底"
                                break;
                            case 1:
                                {
                                    //curUnit.get_sValueA();
                                    lbejectorback2.Content = curUnit.getStrValueA();
                                }
                                break;
                        }

                    }
                    break;
                case (int)menuType.ipr_op04_ejectorfor:
                    {
                        tbMenu.SelectedItem = menu_ejectorfor;
                        switch (iprCtrl.curUnit.sOperateType)
                        {
                            case 0:
                                lbejectorfor2.Content = valmoWin.dv.getCurDis("lanKey981");//"顶针到底";
                                break;
                            case 1:
                                {
                                    iprCtrl.curUnit.get_sValueA();
                                    lbejectorfor2.Content = iprCtrl.curUnit.getStrValueA();
                                }
                                break;
                        }
                    }
                    break;
                case (int)menuType.ipr_op06_safetydoorClose:
                    {
                        //tbMenu.SelectedItem = menu_safetydoorClose;
                        lbsafetydoorClose2.Visibility = Visibility.Hidden;
                    }
                    break;
                case (int)menuType.ipr_op07_safetydoorOpen:
                    {
                        //tbMenu.SelectedItem = menu_safetydoorOpen;
                    }
                    break;
                case (int)menuType.ipr_op08_CarrriageFWD:
                    {
                        //tbMenu.SelectedItem = menu_CarrriageFWD;
                        //iprCtrl.curUnit.get_sValueA();
                    }
                    break;
                case (int)menuType.ipr_op09_CarrriageBWD:
                    {
                        //tbMenu.SelectedItem = menu_CarrriageBWD;
                    }
                    break;
                case (int)menuType.ipr_op10_ShackMold:
                    {
                        //tbMenu.SelectedItem = menu_MoldCompress;
                    }
                    break;
                case (int)menuType.ipr_op14_Charge:
                    {
                        //tbMenu.SelectedItem = menu_Charge;
                    }
                    break;
                case (int)menuType.ipr_op17_Air:
                    {
                        //tbMenu.SelectedItem = menu_Air;
                        //iprCtrl.curUnit.get_sValueA();
                        //iprCtrl.curUnit.get_sValueB();
                        lbAir1.Content = valmoWin.dv.getCurDis("lanKey959") + (curUnit.sValueA + 1);
                        lbAir2.Content = valmoWin.dv.tmTypeObj.getStrValue(curUnit.sValueB) + objUnit.unitBase[UnitType.Tm_s];
                    }
                    break;
                case (int)menuType.ipr_op16_Cores:
                    {
                        //tbMenu.SelectedItem = menu_Core;
                        //iprCtrl.curUnit.get_sValueA();
                        //iprCtrl.curUnit.get_sValueB();
                        string coreName = "";
                        switch (curUnit.sValueA)
                        {
                            case 0: coreName = "A"; break;
                            case 1: coreName = "B"; break;
                            case 2: coreName = "C"; break;
                            case 3: coreName = "D"; break;
                            case 4: coreName = "E"; break;
                            case 5: coreName = "F"; break;
                        }
                        lbCore1.Content = valmoWin.dv.getCurDis("lanKey960") + coreName;
                        if (curUnit.sValueB == 0)
                            lbCore2.Content = valmoWin.dv.getCurDis("lanKey985");
                        else
                            lbCore2.Content = valmoWin.dv.getCurDis("lanKey984");
                    }
                    break;
                case (int)menuType.ipr_op21_Rottable:
                    {
                        //tbMenu.SelectedItem = menu_Rotate;
                    }
                    break;
                case (int)menuType.ipr_op19_Output:
                    {
                        //tbMenu.SelectedItem = menu_Output;
                        switch (curUnit.sFuncSelect)
                        {
                            case 0:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey982");//"数字输出1";
                                break;
                            case 1:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey983");//"数字输出2";
                                break;
                            case 2:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey986");//"数字输出3";
                                break;
                            case 3:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey987");//"数字输出4";
                                break;
                            case 4:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey988");//"报警红灯";
                                break;
                            case 5:
                                lbOutput2.Content = valmoWin.dv.getCurDis("lanKey989");//"报警黄灯";
                                break;
                        }
                        lbOutput3.Content = (curUnit.sValueA == 0) ? "OFF" : "ON";
                    }
                    break;
                case (int)menuType.ipr_op22_Nozzle:
                    {
                        //tbMenu.SelectedItem = menu_Nozzle;
                        //iprCtrl.curUnit.get_sValueA();
                        //iprCtrl.curUnit.get_sValueB();
                    }
                    break;
                case (int)menuType.ipr_op23_Valvegate:
                    {
                        //tbMenu.SelectedItem = menu_Valvegate;
                        //iprCtrl.curUnit.get_sValueA();
                        //iprCtrl.curUnit.get_sValueB();
                        lbValvegate1.Content = valmoWin.dv.getCurDis("lanKey990") + curUnit.sValueA;
                        if (curUnit.sValueB == 1)
                            lbValvegate2.Content = valmoWin.dv.getCurDis("lanKey298");//"开";
                        else
                            lbValvegate2.Content = valmoWin.dv.getCurDis("lanKey299");//"关";
                    }
                    break;
                case (int)menuType.ipr_op03_delay:
                    {
                        //tbMenu.SelectedItem = menu_delay;
                        //iprCtrl.curUnit.get_sValueA();
                        lbdelay2.Content = curUnit.getStrValueA();
                    }
                    break;
                case (int)menuType.ipr_op18_Variable:
                    {
                        //tbMenu.SelectedItem = menu_Calculater;
                        //iprCtrl.curUnit.get_sValueA();
                        //iprCtrl.curUnit.get_sFuncSelect();
                        switch (curUnit.sFuncSelect)
                        {
                            case 0:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey991");//"变量A";
                                break;
                            case 1:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey992");//"变量B";
                                break;
                            case 2:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey993");//"变量C";
                                break;
                            case 3:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey994");//"变量D";
                                break;
                            case 4:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey995");//"标记模数";
                                break;
                            case 5:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey996");//"不良品数";
                                break;
                            case 6:
                                lbCalculater2.Content = valmoWin.dv.getCurDis("lanKey997");//"生产模数";
                                break;
                        }

                        switch (curUnit.sOperateType)
                        {
                            case 0:
                                lbCalculater3.Content = "= " + curUnit.sValueA;
                                break;
                            case 1:
                                lbCalculater3.Content = "+ " + curUnit.sValueA;
                                break;
                            case 2:
                                lbCalculater3.Content = "- " + curUnit.sValueA;
                                break;
                        }

                    }
                    break;
                case (int)menuType.ipr_op02_wait:
                    #region 等待
                    {
                        //tbMenu.SelectedItem = menu_wait;
                        //imgGrp[19].Opacity = 1;
                        lbwait1.Content = valmoWin.dv.getCurDis("lanKey968");// "等待";
                        //lbwait2.Visibility = Visibility.Visible;
                        //lbwait3.Visibility = Visibility.Visible;
                        switch (curUnit.sFuncSelect)
                        {
                            case 0://数字输入
                                {
                                    switch (curUnit.sValueE)
                                    {
                                        case 0:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey998");//"数字输入1";
                                            break;
                                        case 1:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey999");//"数字输入2";
                                            break;
                                        case 2:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1000");//"数字输入3";
                                            break;
                                        case 3:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1001");//"数字输入4";
                                            break;
                                    }
                                    if (curUnit.sValueA == 0)
                                    {
                                        lbwait3.Content = "OFF";
                                    }
                                    else
                                    {
                                        lbwait3.Content = "ON";
                                    }
                                }
                                break;
                            case 1://位置
                                {
                                    switch (curUnit.sValueE)
                                    {
                                        case 0:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1007");//"模板位置";
                                            break;
                                        case 1:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1008");//"顶针位置";
                                            break;
                                        case 2:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1002");//"注射位置";
                                            break;
                                        case 3:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey737");//"射台位置";
                                            break;
                                    }
                                    switch (curUnit.sOperateType)
                                    {
                                        case 0:
                                            lbwait3.Content = "= " + curUnit.getStrValueA();
                                            break;
                                        case 1:
                                            lbwait3.Content = "> " + curUnit.getStrValueA();
                                            break;
                                        case 2:
                                            lbwait3.Content = "< " + curUnit.getStrValueA();
                                            break;
                                    }
                                }
                                break;
                            case 2://喷嘴接触
                                {
                                    lbwait2.Content = valmoWin.dv.getCurDis("lanKey1009");//"喷嘴接触";
                                    if (curUnit.sValueA == 0)
                                    {
                                        lbwait3.Content = "OFF";
                                    }
                                    else
                                    {
                                        lbwait3.Content = "ON";
                                    }
                                }
                                break;
                            case 3://锁模力
                                {
                                    lbwait2.Content = valmoWin.dv.getCurDis("lanKey1018");//"锁模力";
                                    switch (curUnit.sOperateType)
                                    {
                                        case 0:
                                            lbwait3.Content = "= " + curUnit.getStrValueA();
                                            break;
                                        case 1:
                                            lbwait3.Content = "> " + curUnit.getStrValueA();
                                            break;
                                        case 2:
                                            lbwait3.Content = "< " + curUnit.getStrValueA();
                                            break;
                                    }
                                }
                                break;
                            case 5://计数器
                                {
                                    switch (curUnit.sValueE)
                                    {
                                        case 0:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1003");//"计数器1";
                                            break;
                                        case 1:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1004");//"计数器2";
                                            break;
                                        case 2:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1005");//"计数器3";
                                            break;
                                        case 3:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1006");//"计数器4";
                                            break;
                                        case 4:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey995");//"标记模数";
                                            break;
                                        case 5:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey996");//"不良品数";
                                            break;
                                        case 6:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey997");//"生产模数";
                                            break;
                                    }
                                    switch (curUnit.sOperateType)
                                    {
                                        case 0:
                                            lbwait3.Content = "= " + curUnit.getStrValueA();
                                            break;
                                        case 1:
                                            lbwait3.Content = "> " + curUnit.getStrValueA();
                                            break;
                                        case 2:
                                            lbwait3.Content = "< " + curUnit.getStrValueA();
                                            break;
                                    }
                                }
                                break;
                            case 6://欧规信号
                                {
                                    switch (curUnit.sValueE)
                                    {
                                        case 0:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1010");//"合模允许";
                                            break;
                                        case 1:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1011");//"模区安全";
                                            break;
                                        case 2:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1012");//"顶退允许";
                                            break;
                                        case 3:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1013");//"顶进允许";
                                            break;
                                        case 4:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1014");//"中子退允许";
                                            break;
                                        case 5:
                                            lbwait2.Content = valmoWin.dv.getCurDis("lanKey1015");//"中子进允许";
                                            break;
                                    }
                                    lbwait3.Content = (curUnit.sValueA == 0) ? "OFF" : "ON";

                                }
                                break;
                            case 7://注射压力
                                {
                                    lbwait2.Content = valmoWin.dv.getCurDis("lanKey285");//"注射压力";
                                    switch (curUnit.sOperateType)
                                    {
                                        case 0:
                                            lbwait3.Content = "= " + curUnit.getStrValueA();

                                            break;
                                        case 1:
                                            lbwait3.Content = "> " + curUnit.getStrValueA();
                                            break;
                                        case 2:
                                            lbwait3.Content = "< " + curUnit.getStrValueA();
                                            break;
                                    }
                                }
                                break;

                        }

                    }
                    #endregion
                    break;
                case (int)menuType.ipr_op26_jump:
                    {
                        //tbMenu.SelectedItem = menu_jump;
                        lbjump2.Content = valmoWin.dv.getCurDis("lanKey1016") + curUnit.sValueC;
                    }
                    break;
                case (int)menuType.ipr_op12_Moldopen:
                    {
                        //tbMenu.SelectedItem = menu_Moldopen;
                        if (curUnit.sOperateType == 0)
                        {
                            lbMoldopen2.Content = valmoWin.dv.getCurDis("lanKey1017");// "到底";
                        }
                        else
                        {
                            lbMoldopen2.Content = curUnit.getStrValueA();
                        }
                    }
                    break;
                case (int)menuType.ipr_op11_Moldclose:
                    {
                        //tbMenu.SelectedItem = menu_Moldclose;
                        if (curUnit.sOperateType == 0)
                        {
                            lbMoldclose2.Content = valmoWin.dv.getCurDis("lanKey1017");//"到底";
                        }
                        else
                        {
                            lbMoldclose2.Content = curUnit.getStrValueA();
                        }
                    }
                    break;
                case (int)menuType.ipr_op13_Injection:
                    {
                        //tbMenu.SelectedItem = menu_Injection;
                    }
                    break;
                case (int)menuType.ipr_op15_cooling:
                    {
                        //tbMenu.SelectedItem = menu_cooling;
                    }
                    break;
                case (int)menuType.ipr_op30_waitstartsignal:
                    {
                        tbMenu.SelectedItem = menu_waitsignal;

                        switch (iprCtrl.curUnit.sFuncSelect)
                        {
                            case 0:
                                lbwaitstartsignal2.Content = App.Current.TryFindResource("lanKey154");
                                break;
                            case 1:
                                lbwaitstartsignal2.Content = App.Current.TryFindResource("lanKey149");
                                break;
                            case 2:
                                {
                                    lbwaitstartsignal2.Content = App.Current.TryFindResource("lanKey951");
                                    lbwaitstartsignal3.Content = valmoWin.dv.getCurDis("ipr_waitStartSignal_TouchCount") + iprCtrl.curUnit.getStrValueC();
                                }
                                break;
                            case 3:
                                {
                                    lbwaitstartsignal2.Content = App.Current.TryFindResource("lanKey952");
                                    lbwaitstartsignal3.Content = valmoWin.dv.getCurDis("ipr_waitStartSignal_TouchCount") + iprCtrl.curUnit.getStrValueC();
                                }
                                break;
                        }
                    }
                    break;
                case (int)menuType.ipr_op24_seteuromapsignal:
                    {
                        tbMenu.SelectedItem = menu_setEMSignal;
                        {
                            switch (iprCtrl.curUnit.sFuncSelect)
                            {
                                case 0:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_MoldClose");//合模允许
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    lbwaitstartsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                    break;
                                case 1:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_MoldSafety");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 2:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_EjectorBWD");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 3:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_EjectorFWD");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 4:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_CoreBWD");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 5:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_CoreFWD");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                                case 6:
                                    lbseteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_setEmSignal_MoldOpen");//模区安全
                                    if (iprCtrl.curUnit.get_sValueA() == 1)
                                    {
                                        lbseteuromapsignal3.Content = "ON";
                                    }
                                    else
                                    {
                                        lbseteuromapsignal3.Content = "OFF";
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case (int)menuType.ipr_op29_waiteuromapsignal:
                    {
                        tbMenu.SelectedItem = menu_waitEMsignal;
                        switch (iprCtrl.curUnit.sFuncSelect)
                        {
                            case 0:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_MoldClose");//合模允许
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 1:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_MoldOpen");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 2:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_EjectorBWD");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 3:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_EjectorFWD");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 4:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_CoreFWD");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                            case 5:
                                lbwaiteuromapsignal2.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal_CoreBWD");//模区安全
                                lbwaiteuromapsignal3.Content = iprCtrl.curUnit.getStrValueA();
                                break;
                        }
                    }
                    break;
            }
        }

    }
}
