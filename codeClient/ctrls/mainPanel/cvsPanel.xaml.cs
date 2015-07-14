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
    /// Interaction logic for cvsPanel.xaml
    /// </summary>
    public partial class cvsPanel : Canvas
    {
        double Hchild = 100;
        double Wchild = 100;
        Canvas cvsBox = new Canvas();
        public cvsPanel()
        {
            InitializeComponent();
            //cvsMain.Children.Add(cvsBox);
            //Canvas.SetTop(cvsBox, 0);
            //cvsBox.Height = cvsMain.Height;
            //cvsBox.Width = cvsMain.Width;
        }
        public double h
        {
            get
            {
                return cvsMain.Height;
            }
            set
            {
                cvsMain.Height = value;
                cvsBox.Height = value;
            }
        }
        public double w
        {
            set
            {
                cvsMain.Width = value;
                cvsBox.Width = value;
            }
            get
            {
                return cvsMain.Width;
            }
        }
        public void setWidth(double value)
        {
            cvsMain.Width = value;
            cvsBox.Width = value;
        }
        public void setChildSize(double height,double width)
        {
            Hchild = height;
            Wchild = width;
        }
        public UIElement this[int index]
        {
            get
            {
                if (index < 0 || index >= cvsBox.Children.Count)
                    return null;
                return cvsBox.Children[index];
            }
            set
            {
                cvsBox.Children[index] = value;
            }
        }
        public int count
        {
            get
            {
                return cvsBox.Children.Count;
            }
        }
        public void add(UIElement ui)
        {
            cvsBox.Children.Add(ui);
            Canvas.SetTop(ui, (cvsBox.Children.Count - 1) * Hchild);
            ui.MouseDown += new MouseButtonEventHandler(cvsChild_MouseDown);

        }
        public void clear()
        {
            cvsBox.Children.Clear();
        }
        bool isMouseDown = false;
        Point mousePoint;
        private void cvsChild_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.GetPosition(cvsMain);
        }

        private void cvsMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (cvsBox.Children.Count * Hchild > cvsBox.Height)
                    {
                        Point theMousePoint = e.GetPosition(this.cvsMain);
                        double tmpTop = Canvas.GetTop(cvsBox) + theMousePoint.Y - mousePoint.Y;
                        if (tmpTop > 0)
                            tmpTop = 0;
                        else if (tmpTop < -cvsBox.Children.Count * Hchild + cvsBox.Height)
                            tmpTop = -cvsBox.Children.Count * Hchild + cvsBox.Height;
                        Canvas.SetTop(cvsBox, tmpTop);
                        mousePoint = theMousePoint;
                    }
                }

            }
        }

        private void cvsMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private void cvsMain_MouseLeave(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

    }
}
