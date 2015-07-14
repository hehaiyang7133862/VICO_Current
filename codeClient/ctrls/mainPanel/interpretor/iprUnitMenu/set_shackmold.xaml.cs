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
    public partial class set_shackmold : UserControl
    {
        public set_shackmold()
        {
            InitializeComponent();
        }

        private void numkeyDisposeFunc()
        {
            Value1.dis = valmoWin.dv.MldPr[601].vDblStr + "[" + valmoWin.dv.MldPr[601].unit + "]";
            Value2.dis = valmoWin.dv.MldPr[602].vDblStr + "[" + valmoWin.dv.MldPr[602].unit + "]";
            Value3.dis = valmoWin.dv.MldPr[603].vDblStr + "[" + valmoWin.dv.MldPr[603].unit + "]";
            Value4.dis = valmoWin.dv.MldPr[606].vDblStr + "[" + valmoWin.dv.MldPr[606].unit + "]";
            Value5.dis = valmoWin.dv.MldPr[605].vDblStr + "[" + valmoWin.dv.MldPr[605].unit + "]";
        }

        public void setValue()
        {
            Value1.dis = valmoWin.dv.MldPr[601].vDblStr + "[" + valmoWin.dv.MldPr[601].unit + "]";
            Value2.dis = valmoWin.dv.MldPr[602].vDblStr + "[" + valmoWin.dv.MldPr[602].unit + "]";
            Value3.dis = valmoWin.dv.MldPr[603].vDblStr + "[" + valmoWin.dv.MldPr[603].unit + "]";
            Value4.dis = valmoWin.dv.MldPr[606].vDblStr + "[" + valmoWin.dv.MldPr[606].unit + "]";
            Value5.dis = valmoWin.dv.MldPr[605].vDblStr + "[" + valmoWin.dv.MldPr[605].unit + "]";

            iprCtrl.curUnit.get_sNotReady();
            if (iprCtrl.curUnit.sErrLink)
            {
                activeErr1Ctrl1.Visibility = Visibility.Visible;
                activeErr1Ctrl1.dis = "触发错误";
            }
            else
            {
                activeErr1Ctrl1.Visibility = Visibility.Hidden;
            }
            if (iprCtrl.curUnit.sErrActName)
            {
                activeErr1Ctrl1.Visibility = Visibility.Visible;
                activeErr1Ctrl1.dis = "该功能无法在此位置执行";
            }
            else
            {
                if (!iprCtrl.curUnit.sErrLink)
                    activeErr1Ctrl1.Visibility = Visibility.Hidden;
            }
            if (iprCtrl.curUnit.sErrUndefined)
            {
                activeErr1Ctrl1.Visibility = Visibility.Visible;
                activeErr1Ctrl1.dis = "功能未定义";
            }
            else
            {
                if (!iprCtrl.curUnit.sErrLink && !iprCtrl.curUnit.sErrActName)
                    activeErr1Ctrl1.Visibility = Visibility.Hidden;
            }
        }

        private void btn1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SNumKeyPanel.init(valmoWin.dv.MldPr[601], App.Current.TryFindResource("Mld601").ToString(), numkeyDisposeFunc);
        }

        private void btn2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SNumKeyPanel.init(valmoWin.dv.MldPr[602], App.Current.TryFindResource("Mld602").ToString(), numkeyDisposeFunc);
        }

        private void btn3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SNumKeyPanel.init(valmoWin.dv.MldPr[603], App.Current.TryFindResource("Mld603").ToString(), numkeyDisposeFunc);
        }
        private void btn4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SNumKeyPanel.init(valmoWin.dv.MldPr[606], App.Current.TryFindResource("Mld606").ToString(), numkeyDisposeFunc);
        }
        private void btn5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            valmoWin.SNumKeyPanel.init(valmoWin.dv.MldPr[605], App.Current.TryFindResource("Mld605").ToString(), numkeyDisposeFunc);
        }
    }
}
