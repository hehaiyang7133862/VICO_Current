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
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class myComboBox : UserControl
    {
        private int _selectedIndex = 0;
        private StackPanel sPanelItems;
        private Popup pop;

        public int SelectedIndex
        {
            set
            {
                _selectedIndex = value;

                int i = 0;
                foreach (object obj in sPanelItems.Children)
                {
                    if (obj.GetType() == typeof(Border))
                    {
                        if (_selectedIndex == i)
                        {
                            (obj as Border).Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
                        }
                        else
                        {
                            (obj as Border).Background = Brushes.White;
                        }

                        i++;
                    }
                }
            }
            get
            {
                return _selectedIndex;
            }
        }
        private objUnit _curObj;
        public string objname
        {
            set
            {
                tabControl.objname = value;

                objUnit obj = valmoWin.dv.getObj(value);
                if (obj != null)
                {
                    _curObj = obj;
                }
            }
        }

        public double CtrlWidth
        {
            set
            {
                bdMain.Width = value;
            }
        }
        public double CtrlHeight
        {
            set
            {
                bdMain.Height = value;
            }
        }

        private List<string> _items;

        private int _itemsCount = 0;

        [TypeConverter(typeof(stringToItemsTypeConverter))]
        public List<string> Items
        {
            set
            {
                _items = value;

                tabControl.Items = value;

                foreach (string str_key in _items)
                {
                    addItem(str_key);
                }
            }
        }

        public myComboBox()
        {
            InitializeComponent();

            sPanelItems = new StackPanel();

            pop = new Popup();
            pop.AllowsTransparency = true;
            pop.Child = sPanelItems;
            pop.PlacementTarget = bdMain;
            pop.Placement = PlacementMode.Relative;
            pop.VerticalOffset = 35;
            pop.HorizontalOffset = 0;
            pop.StaysOpen = false;
            pop.Closed += new EventHandler(pop_Closed);
        }

        private bool bClosed = true;
        void pop_Closed(object sender, EventArgs e)
        {
            bClosed = true;
            pop.IsOpen = false;
            bdMain.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x90, 0x90, 0x90));

            up.Visibility = Visibility.Hidden;
            down.Visibility = Visibility.Visible;
        }

        private void updateSelectedIndex(objUnit obj)
        {
            SelectedIndex = obj.value;
        }

        public void addItem(string str_key)
        {
            string str_Item = string.Empty;

            object obj_Item = TryFindResource(str_key);

            Label lb = new Label();
            lb.Height = 35;
            if (obj_Item != null)
            {
                lb.SetResourceReference(Label.ContentProperty, str_key);
            }
            else
            {
                lb.Content = str_key + "_undefined";
            }
            lb.FontSize = 14;
            lb.Foreground = Brushes.Black;
            lb.VerticalContentAlignment = VerticalAlignment.Center;
            lb.Margin = new Thickness(10, 0, 0, 0);

            Border bd = new Border();
            bd.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90));
            bd.Background = Brushes.White;
            bd.BorderThickness = new Thickness(1, 0, 1, 1);
            bd.Height = 35;
            bd.Width = bdMain.Width;
            bd.Child = lb;
            bd.Tag = _itemsCount;
            bd.MouseUp += new MouseButtonEventHandler(bd_MouseUp);

            sPanelItems.Children.Add(bd);

            _itemsCount++;
        }

        void bd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            object tag = (sender as Border).Tag;

            if (tag != null)
            {
                _curObj.valueNew = Convert.ToInt32(tag);

                pop.IsOpen = false;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (bClosed == true)
            {
                pop.IsOpen = true;

                bdMain.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xB4, 0xE1));
                up.Visibility = Visibility.Visible;
                down.Visibility = Visibility.Hidden;

                SelectedIndex = _curObj.value;

                bClosed = false;
            }
        }
    }

    public class stringToItemsTypeConverter : UriTypeConverter
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
}
