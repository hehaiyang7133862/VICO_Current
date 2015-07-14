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
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    public partial class myTabControl : UserControl
    {
        private objUnit _curobj;

        public string objname
        {
            set
            {
                objUnit obj = valmoWin.dv.getObj(value);

                if (obj != null)
                {
                    _curobj = obj;
                    _curobj.addHandle(ChangeTabSelected);
                }
                else
                {
                    MessageBox.Show(value + " _undefined");
                }
            }
        }

        private List<string> _items;

        [TypeConverter(typeof(stringToItemsTypeConverter))]
        public List<string> Items
        {
            set
            {
                _items = value;

                foreach (string str_key in _items)
                {
                    addItem(str_key);
                }
            }
        }

        public myTabControl()
        {
            InitializeComponent();
        }

        private void ChangeTabSelected(objUnit obj)
        {
            if ((obj.value > tbSelected.Items.Count - 1) || (obj.value < 0))
            {
                MessageBox.Show(obj.serialNum + "Error");
            }
            else
            {
                tbSelected.SelectedIndex = obj.value;
            }
        }

        private void addItem(string str_key)
        {
            TabItem item = new TabItem();
            item.Margin = new Thickness(0);
            item.Padding = new Thickness(0);
            item.Height = 0;

            string str_Item = string.Empty;

            object obj_Item = TryFindResource(str_key);

            Label lb_Item = new Label();
            lb_Item.FontSize = 14;
            if (obj_Item != null)
            {
                lb_Item.SetResourceReference(Label.ContentProperty, str_key);
            }
            else
            {
                lb_Item.Content = str_key + "_undefined";
            }
            lb_Item.Height = 35;
            lb_Item.VerticalContentAlignment = VerticalAlignment.Center;
            lb_Item.Margin = new Thickness(10, 0, 0, 0);

            item.Content = lb_Item;

            tbSelected.Items.Add(item);
        }
    }
}
