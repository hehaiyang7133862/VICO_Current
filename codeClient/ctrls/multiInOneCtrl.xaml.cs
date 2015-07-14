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
    /// multiInOneCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class multiInOneCtrl : Canvas
    {
        objUnit curObj;
        List<Label> ctrls = new List<Label>();
        List<Thickness> lstThickness = new List<Thickness>();
        public multiInOneCtrl()
        {
            InitializeComponent();
        }
        public string objName
        {
            set
            {
                curObj = valmoWin.dv.getObj(value);
                if (curObj != null)
                {
                    curObj.addHandle(handleRefresh);
                }
            }
        }
        private void handleRefresh(objUnit obj)
        {
            foreach (Label lb in ctrls)
            {
                lb.BorderBrush = Brushes.Silver;
                lb.BorderThickness = new Thickness();
            }


            if (obj.value < ctrls.Count)
            {
                ctrls[obj.value].BorderBrush = Brushes.Red;
                ctrls[obj.value].BorderThickness = new Thickness(2);
            }
            //switch (obj.value)
            //{
            //    case 0:
            //        ctrls[0].BorderBrush = Brushes.Red;
            //        ctrls[0].BorderThickness = new Thickness(2);
            //        break;
            //    case 1:

            //        break;
            //    case 2:

            //        break;
            //    case 3:

            //        break;
            //}
        }
        //public int num
        //{
        //    set
        //    {
        //        switch (value)
        //        {
        //            case 3:
        //                {
        //                    lb3.Visibility = Visibility.Hidden;
        //                    Canvas.SetLeft(lb4, 150);
        //                    cvsMain.Width = 225;
        //                }
        //                break;
        //            case 4:

        //                break;
        //        }
        //    }
        //}
        protected override Size MeasureOverride(Size availableSize)
        {
            Size panelDesiredSize = new Size();
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                panelDesiredSize = child.DesiredSize;
            }
            return panelDesiredSize;
        }   
        protected override Size ArrangeOverride(Size finalSize)
        {
            Console.WriteLine(this.Children.Count);
            int ctrlNum = 0;
            ctrls.Clear();
            lstThickness.Clear();
            foreach (UIElement ctrl in this.Children)
            {
                
                System.Type type = ctrl.GetType();
                if (type.FullName == "System.Windows.Controls.Label")
                {
                    Label lb = ctrl as Label;
                    Canvas.SetLeft(lb, 75 * ctrlNum++);
                    lb.Width = 75;
                    lb.BorderBrush = Brushes.Silver;
                    ctrls.Add(lb);
                    switch (ctrlNum)
                    {
                        case 1:
                            {
                                (ctrl as Label).BorderThickness = new Thickness(1, 1, 0, 1);
                            }
                            break;
                        case 2:
                            {
                                (ctrl as Label).BorderThickness = new Thickness(1, 1, 0, 1);
                            }
                            break;
                        case 3:
                            {
                                if (this.Children.Count == 3)
                                {
                                    lb.BorderThickness = new Thickness(1, 1, 1, 1);
                                }
                                else if(this.Children.Count == 4)
                                {
                                    lb.BorderThickness = new Thickness(1, 1, 0, 1);
                                }
                            }
                            break;
                        case 4:
                            {
                                if (this.Children.Count == 4)
                                {
                                    lb.BorderThickness = new Thickness(1, 1, 1, 1);
                                }
                            }
                            break;
                    }
                    lstThickness.Add(lb.BorderThickness);

                    ctrl.Arrange(new Rect(new Point(Canvas.GetLeft(ctrl), Canvas.GetTop(ctrl)), ctrl.DesiredSize));
                }
                else
                {
                    throw (new Exception("★★★" + this.Name + "控件中的第" + ctrlNum + "个子控件不是label类型。需要将其删除，并重新加载。" ) );

                    ctrl.Visibility = Visibility.Hidden;
                }
                //Console.WriteLine(type);
            }
            return base.ArrangeOverride(finalSize);

        }

    }
}
