﻿using System;
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
    /// saveAsCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class saveAsCtrl : UserControl
    {
        public saveAsCtrl()
        {
            InitializeComponent();
        }

        public nullEvent fdStateChange
        {
            get;
            set;
        }
        public static DependencyProperty disProperty = DependencyProperty.Register(
            "dis",                                                    // Property name
            typeof(Object),                                           // Property type
            typeof(saveAsCtrl),                                      // Type of the dependency property provider
            new PropertyMetadata("",                                // 默认的值
            new PropertyChangedCallback(OnUriChanged)             // Callback invoked on property value has changes
            )
        );

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as saveAsCtrl).lbDis.Content = e.NewValue;
        }
        public object dis
        {
            get
            {
                return lbDis.Content;
            }
            set
            {
                lbDis.Content = value;
            }
        }


        private void cvsMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cvsMain.Background = Brushes.Silver;
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (cvsMain.Background == Brushes.Silver)
            {
                if (fdStateChange != null)
                    fdStateChange();
                cvsMain.Background = Brushes.Transparent;

            }
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            cvsMain.Background = Brushes.Transparent;
        }
    }
}
