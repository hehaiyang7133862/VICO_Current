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
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// motorWarningCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class motorWarningCtrl : UserControl
    {
        private objUnit _curObj;
        private bool _state = false;

        /// <summary>
        /// 设置对象
        /// </summary>
        public string objName
        {
            set
            {
                _curObj = valmoWin.dv.getObj(value);
                if (_curObj != null)
                {
                    _curObj.addHandle(refushState);
                }
            }
        }

        private DriveErr de;

        [TypeConverter(typeof(Str2DriveErrTypeConverter))]
        public List<string> DriveErr
        {
            set
            {
                List<string> lst = value;

                if (lst.Count != 7)
                {
                    App.log.Error("驱动错误信息初始化失败!");
                }
                else
                {
                    de = new DriveErr(lst[0], lst[1], lst[2], lst[3], lst[4], lst[5], lst[6]);
                }
            }
        }

        public class Str2DriveErrTypeConverter : UriTypeConverter
        {
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                string strTemp = value.ToString();
                List<string> items = new List<string>();

                while (strTemp.Length > 0)
                {
                    if (strTemp.IndexOf(",") != -1)
                    {
                        items.Add(strTemp.Substring(0, strTemp.IndexOf(",")));
                        strTemp = strTemp.Substring(strTemp.IndexOf(",") + 1, strTemp.Length - strTemp.IndexOf(",") - 1);
                    }
                    else
                    {
                        items.Add(strTemp);
                        strTemp = string.Empty;
                    }
                }

                return items;
            }
        }

        public motorWarningCtrl()
        {
            InitializeComponent();
        }

        private void refushState(objUnit obj)
        {
            if ((((obj.value >> 3) & 0x01) == 1) ||(((obj.value >> 7) & 0x01) == 1))
            {
                _state = true;
            }
            else
            {
                _state = false;
            }

            tbState.SelectedIndex = (_state == false) ? 1 : 0;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<string> lst = de.GetErrList();

            if (lst.Count == 0)
            {
                return;
            }

            Popup pop = new Popup();
            pop.AllowsTransparency = true;

            StackPanel SPanel = new StackPanel();
            pop.Child = SPanel;
            pop.PlacementTarget = cvsMain;
            pop.Placement = PlacementMode.Relative;
            pop.VerticalOffset = 0;
            pop.HorizontalOffset = 130;
            pop.StaysOpen = false;

            Line l = new Line();
            l.Stroke = new SolidColorBrush(Color.FromRgb(0xC3, 0x14, 0x00));
            l.StrokeThickness = 2;
            l.ClipToBounds = true;
            l.SnapsToDevicePixels = true;
            l.X2 = 220;
            SPanel.Children.Add(l);

            foreach (string str in lst)
            {
                Label lb = new Label();
                lb.Content = str;
                lb.FontSize = 14;
                lb.Foreground = new SolidColorBrush(Color.FromRgb(0xC3, 0x14, 0x00));
                lb.VerticalContentAlignment = VerticalAlignment.Center;
                lb.Margin = new Thickness(10, 0, 0, 0);

                Border bd = new Border();
                bd.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC3, 0x14, 0x00));
                bd.Background = Brushes.White;
                bd.BorderThickness = new Thickness(1, 0, 1, 1);
                bd.Height = 35;
                bd.Width = 220;
                bd.Child = lb;

                SPanel.Children.Add(bd);
            }

            pop.IsOpen = true;
        }
    }
}
