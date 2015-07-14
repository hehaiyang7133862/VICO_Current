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
    /// <summary>
    /// Interaction logic for menuCtrl.xaml
    /// </summary>
    public partial class menuCtrl : UserControl
    {
        menuType curType;
        public menuCtrl()
        {
            InitializeComponent();
        }
        public menuType type
        {
            set
            {
                switch (value)
                {
                    case menuType.ipr_op05_ejectorback:
                        img1.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey951");//"顶针回退";
                        break;
                    case menuType.ipr_op04_ejectorfor:
                        img2.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey952");//"顶针前进";
                        break;
                    case menuType.ipr_op06_safetydoorClose:
                        img3.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey953");//"关闭安全门";
                        break;
                    case menuType.ipr_op07_safetydoorOpen:
                        img4.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey954");//"打开安全门";
                        break;
                    case menuType.ipr_op08_CarrriageFWD:
                        img5.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey955");//"射台前进";
                        break;
                    case menuType.ipr_op09_CarrriageBWD:
                        img6.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey956");//"射台后退";
                        break;
                    case menuType.ipr_op10_ShackMold:
                        img7.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey957");//"排气动作";
                        break;
                    case menuType.ipr_op14_Charge:
                        img8.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("btnKey_19");//"储料";
                        break;
                    case menuType.ipr_op17_Air:
                        img9.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey959");//"吹气";
                        break;
                    case menuType.ipr_op16_Cores:
                        img10.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey960");//"中子";
                        break;
                    case menuType.ipr_op21_Rottable:
                        img11.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey961");//"转盘";
                        break;
                    case menuType.ipr_op19_Output:
                        img12.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey962");//"可编程输出";
                        break;
                    case menuType.ipr_op22_Nozzle:
                        img13.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey963");//"机器喷嘴";
                        break;
                    case menuType.ipr_op23_Valvegate:
                        img14.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey964");//"热流道阀针";
                        break;
                    case menuType.ipr_op03_delay:
                        img17.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey966");//"延时";
                        break;
                    case menuType.ipr_op18_Variable:
                        img18.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey967");//"变量操作";
                        break;
                    case menuType.ipr_op02_wait:
                        img19.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey968");//"等待";
                        break;
                    case menuType.ipr_op26_jump:
                        img20.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey969");//"跳转";
                        break;
                    case menuType.ipr_op12_Moldopen:
                        img21.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey970");//"开模";
                        break;
                    case menuType.ipr_op11_Moldclose:
                        img22.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey971");//"合模";
                        break;
                    case menuType.ipr_op13_Injection:
                        {
                            img23.Opacity = 1;
                            lbDis.Content = valmoWin.dv.getCurDis("lanKey972");//"注射";
                        }
                        break;
                    case menuType.ipr_op15_cooling:
                        {
                            img24.Opacity = 1;
                            lbDis.Content = valmoWin.dv.getCurDis("lanKey973");//"冷却";
                        }
                        break;
                    case menuType.ipr_op24_seteuromapsignal:
                        {
                            img29.Opacity = 1;
                            lbDis.Content = valmoWin.dv.getCurDis("ipr_setEmSignal");//等待开始信号
                        }
                        break;
                    case menuType.ipr_op30_waitstartsignal:
                        {
                            img30.Opacity = 1;
                            lbDis.Content = valmoWin.dv.getCurDis("ipr_waitStartSignal"); //设置机械手信号
                        }
                        break;
                    case menuType.ipr_op29_waiteuromapsignal:
                        {
                            img31.Opacity = 1;
                            lbDis.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal"); //等待机械手信号
                        }
                        break;
                    default:
                        break;

                }
                curType = value;
            }
        }

        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int type = (int)curType;
            valmoWin.dv.IprPr[36].setValue((int)curType);
            vm.printLn("set curUnitObjCtrl to " + valmoWin.dv.IprPr[36].valueNew);
        }
        public void refreshLan()
        {
            switch (curType)
            {
                case menuType.ipr_op05_ejectorback:
                    img1.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey951");//"顶针回退";
                    break;
                case menuType.ipr_op04_ejectorfor:
                    img2.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey952");//"顶针前进";
                    break;
                case menuType.ipr_op06_safetydoorClose:
                    img3.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey953");//"关闭安全门";
                    break;
                case menuType.ipr_op07_safetydoorOpen:
                    img4.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey954");//"打开安全门";
                    break;
                case menuType.ipr_op08_CarrriageFWD:
                    img5.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey955");//"射台前进";
                    break;
                case menuType.ipr_op09_CarrriageBWD:
                    img6.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey956");//"射台后退";
                    break;
                case menuType.ipr_op10_ShackMold:
                    img7.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey957");//"排气动作";
                    break;
                case menuType.ipr_op14_Charge:
                    img8.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("btnKey_19");//"储料";
                    break;
                case menuType.ipr_op17_Air:
                    img9.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey959");//"吹气";
                    break;
                case menuType.ipr_op16_Cores:
                    img10.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey960");//"中子";
                    break;
                case menuType.ipr_op21_Rottable:
                    img11.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey961");//"转盘";
                    break;
                case menuType.ipr_op19_Output:
                    img12.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey962");//"可编程输出";
                    break;
                case menuType.ipr_op22_Nozzle:
                    img13.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey963");//"机器喷嘴";
                    break;
                case menuType.ipr_op23_Valvegate:
                    img14.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey964");//"热流道阀针";
                    break;
                case menuType.ipr_op03_delay:
                    img17.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey966");//"延时";
                    break;
                case menuType.ipr_op18_Variable:
                    img18.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey967");//"变量操作";
                    break;
                case menuType.ipr_op02_wait:
                    img19.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey968");//"等待";
                    break;
                case menuType.ipr_op26_jump:
                    img20.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey969");//"跳转";
                    break;
                case menuType.ipr_op12_Moldopen:
                    img21.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey970");//"开模";
                    break;
                case menuType.ipr_op11_Moldclose:
                    img22.Opacity = 1;
                    lbDis.Content = valmoWin.dv.getCurDis("lanKey971");//"合模";
                    break;
                case menuType.ipr_op13_Injection:
                    {
                        img23.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey972");//"注射";
                    }
                    break;
                case menuType.ipr_op15_cooling:
                    {
                        img24.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("lanKey973");//"冷却";
                    }
                    break;
                case menuType.ipr_op24_seteuromapsignal:
                    {
                        img29.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("ipr_setEmSignal");//等待开始信号
                    }
                    break;
                case menuType.ipr_op30_waitstartsignal:
                    {
                        img30.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("ipr_waitStartSignal"); //设置机械手信号
                    }
                    break;
                case menuType.ipr_op29_waiteuromapsignal:
                    {
                        img31.Opacity = 1;
                        lbDis.Content = valmoWin.dv.getCurDis("ipr_waitEmSignal"); //等待机械手信号
                    }
                    break;
            }
        }
    }
    public enum menuType : byte
    {
        ipr_op00_nothing = 0,
        ipr_op01_blank = 1,
        ipr_op02_wait = 2,
        ipr_op03_delay = 3,
        //EjectorOUT
        ipr_op04_ejectorfor = 4,
        //EjectorIn
        ipr_op05_ejectorback = 5,
        ipr_op06_safetydoorClose = 6,
        ipr_op07_safetydoorOpen = 7,
        ipr_op08_CarrriageFWD = 8,
        ipr_op09_CarrriageBWD = 9,
        ipr_op10_ShackMold = 10,
        ipr_op11_Moldclose = 11,
        ipr_op12_Moldopen = 12,
        ipr_op13_Injection = 13,
        ipr_op14_Charge = 14,
        ipr_op15_cooling = 15,
        ipr_op16_Cores = 16,
        ipr_op17_Air = 17,
        ipr_op18_Variable = 18,
        ipr_op19_Output = 19,
        ipr_op20_palleprogram = 20,
        ipr_op21_Rottable = 21,
        ipr_op22_Nozzle = 22,
        ipr_op23_Valvegate = 23,
        ipr_op24_seteuromapsignal = 24,
        ipr_op25_others = 25,
        ipr_op26_jump = 26,
        ipr_op27_active_palleprogram = 27,
        ipr_op28_inactive_palleprogram = 28,
        ipr_op29_waiteuromapsignal = 29,
        ipr_op30_waitstartsignal = 30
    }
}
