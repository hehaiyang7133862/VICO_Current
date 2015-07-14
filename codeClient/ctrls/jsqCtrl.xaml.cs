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
    /// Interaction logic for jsqCtrl.xaml
    /// </summary>
    public partial class jsqCtrl : UserControl
    {
        double[] slt = new double[30];
        string num = "0";
        string odTest1 = "null";
        string odTest2 = "null";
        bool dotUsed = false;

        public jsqCtrl()
        {
            InitializeComponent();
        }

        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                string tmp = (sender as Button).Content.ToString();
                if (num == "0")
                {
                    if (tmp == ".")
                    {
                        num = "0.";
                        dotUsed = true;
                    }
                    else
                    {
                        num = tmp;
                        slt[0] = Double.Parse(num);
                        dotUsed = false;
                    }
                }
                else
                {
                    if (tmp == ".")
                    {
                        if (!dotUsed)
                            num += ".";
                    }
                    else
                    {
                        num += tmp;
                        slt[0] = Double.Parse(num);
                    }
                }
            }
            else
            {
                if (num == "0")
                {
                    if ((sender as Button).Content.ToString() == ".")
                        num = "0.";
                    else
                        num = (sender as Button).Content.ToString();
                }
                else
                    num += (sender as Button).Content.ToString();
            }
            lbResult0.Content = num;
        }
        private void btnOd_Click(object sender, RoutedEventArgs e)
        {
            check((sender as Button).Content.ToString());
        }

        private void check(string odNew)
        {
            switch (odTest1)
            {
                case "+":
                    {
                        if (odTest2 == "null")
                        {
                            switch (odNew)
                            {
                                case "+":
                                    slt[0] = slt[0] + Double.Parse(num);
                                    lbResult0.Content = slt[0].ToString();
                                    break;
                                case "-":
                                    slt[0] = slt[0] + Double.Parse(num);
                                    odTest1 = "-";
                                    lbResult0.Content = slt[0].ToString();
                                    break;
                                case "×":
                                    slt[1] = Double.Parse(num);
                                    odTest2 = "×";
                                    lbResult0.Content = slt[1].ToString();
                                    break;
                                case "÷":
                                    slt[1] = Double.Parse(num);
                                    odTest2 = "÷";
                                    lbResult0.Content = slt[1].ToString();
                                    break;
                            }
                        }
                        else
                        {
                            switch (odTest2)
                            {
                                case "×":
                                    switch (odNew)
                                    {
                                        case "+":
                                            slt[0] = slt[0] + slt[1] * Double.Parse(num);
                                            odTest1 = "+";
                                            odTest2 = "null";
                                            lbResult0.Content = slt[0].ToString();
                                            break;
                                        case "-":
                                            slt[0] = slt[0] + slt[1] * Double.Parse(num);
                                            odTest1 = "-";
                                            odTest2 = "null";
                                            lbResult0.Content = slt[0].ToString();
                                            break;
                                        case "×":
                                            slt[1] = slt[1] * Double.Parse(num);
                                            odTest2 = "×";
                                            lbResult0.Content = slt[1].ToString();
                                            break;
                                        case "÷":
                                            slt[1] = slt[1] / Double.Parse(num);
                                            odTest2 = "÷";
                                            lbResult0.Content = slt[1].ToString();
                                            break;
                                    }
                                    break;
                                case "÷":
                                    if (num != "0")
                                    {
                                        switch (odNew)
                                        {
                                            case "+":
                                                slt[0] = slt[0] + slt[1] / Double.Parse(num);
                                                odTest1 = "+";
                                                odTest2 = "null";
                                                lbResult0.Content = slt[0].ToString();
                                                break;
                                            case "-":
                                                slt[0] = slt[0] + slt[1] / Double.Parse(num);
                                                odTest1 = "-";
                                                odTest2 = "null";
                                                lbResult0.Content = slt[0].ToString();
                                                break;
                                            case "×":
                                                slt[1] = slt[1] / Double.Parse(num);
                                                odTest2 = "×";
                                                lbResult0.Content = slt[1].ToString();
                                                break;
                                            case "÷":
                                                slt[1] = slt[1] / Double.Parse(num);
                                                odTest2 = "÷";
                                                lbResult0.Content = slt[1].ToString();
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    }
                case "-":
                    {
                        if (odTest2 == "null")
                        {
                            switch (odNew)
                            {
                                case "+":
                                    slt[0] = slt[0] - Double.Parse(num);
                                    lbResult0.Content = slt[0].ToString();
                                    break;
                                case "-":
                                    slt[0] = slt[0] - Double.Parse(num);
                                    odTest1 = "-";
                                    lbResult0.Content = slt[0].ToString();
                                    break;
                                case "×":
                                    slt[1] = Double.Parse(num);
                                    odTest2 = "×";
                                    lbResult0.Content = slt[1].ToString();
                                    break;
                                case "÷":
                                    slt[1] = Double.Parse(num);
                                    odTest2 = "÷";
                                    lbResult0.Content = slt[1].ToString();
                                    break;
                            }
                        }
                        else
                        {
                            switch (odTest2)
                            {
                                case "×":
                                    switch (odNew)
                                    {
                                        case "+":
                                            slt[0] = slt[0] - slt[1] * Double.Parse(num);
                                            odTest1 = "+";
                                            odTest2 = "null";
                                            lbResult0.Content = slt[0].ToString();
                                            break;
                                        case "-":
                                            slt[0] = slt[0] - slt[1] * Double.Parse(num);
                                            odTest1 = "-";
                                            odTest2 = "null";
                                            lbResult0.Content = slt[0].ToString();
                                            break;
                                        case "×":
                                            slt[1] = slt[1] * Double.Parse(num);
                                            odTest2 = "×";
                                            lbResult0.Content = slt[1].ToString();
                                            break;
                                        case "÷":
                                            slt[1] = slt[1] * Double.Parse(num);
                                            odTest2 = "÷";
                                            lbResult0.Content = slt[1].ToString();
                                            break;
                                    }
                                    break;
                                case "÷":
                                    if (num != "0")
                                    {
                                        switch (odNew)
                                        {
                                            case "+":
                                                slt[0] = slt[0] + slt[1] / Double.Parse(num);
                                                odTest1 = "+";
                                                odTest2 = "null";
                                                lbResult0.Content = slt[0].ToString();
                                                break;
                                            case "-":
                                                slt[0] = slt[0] + slt[1] / Double.Parse(num);
                                                odTest1 = "-";
                                                odTest2 = "null";
                                                lbResult0.Content = slt[0].ToString();
                                                break;
                                            case "×":
                                                if (num != "0")
                                                {
                                                    slt[1] = slt[1] / Double.Parse(num);
                                                    odTest2 = "×";
                                                    lbResult0.Content = slt[1].ToString();
                                                }
                                                break;
                                            case "÷":
                                                if (num != "0")
                                                {
                                                    slt[1] = slt[1] / Double.Parse(num);
                                                    odTest2 = "÷";
                                                    lbResult0.Content = slt[1].ToString();
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    }
                case "×":
                    switch (odNew)
                    {
                        case "+":
                            slt[0] = slt[0] * Double.Parse(num);
                            odTest1 = "+";
                            lbResult0.Content = slt[0].ToString();
                            break;
                        case "-":
                            slt[0] = slt[0] * Double.Parse(num);
                            odTest1 = "-";
                            lbResult0.Content = slt[0].ToString();
                            break;
                        case "×":
                            slt[0] = slt[0] * Double.Parse(num);
                            odTest1 = "×";
                            lbResult0.Content = slt[0].ToString();
                            break;
                        case "÷":
                            slt[0] = slt[0] * Double.Parse(num);
                            odTest1 = "÷";
                            lbResult0.Content = slt[0].ToString();
                            break;
                    }
                    break;
                case "÷":
                    if (num != "0")
                    {
                        switch (odNew)
                        {
                            case "+":
                                slt[0] = slt[0] * Double.Parse(num);
                                odTest1 = "+";
                                lbResult0.Content = slt[0].ToString();
                                break;
                            case "-":
                                slt[0] = slt[0] * Double.Parse(num);
                                odTest1 = "-";
                                lbResult0.Content = slt[0].ToString();
                                break;
                            case "×":
                                slt[0] = slt[0] * Double.Parse(num);
                                odTest1 = "×";
                                lbResult0.Content = slt[0].ToString();
                                break;
                            case "÷":
                                slt[0] = slt[0] * Double.Parse(num);
                                odTest1 = "÷";
                                lbResult0.Content = slt[0].ToString();
                                break;
                        }
                    }
                    break;
                case "null":
                    slt[0] = Double.Parse(num);
                    odTest1 = odNew;
                    break;
                case "=":
                    if (odNew != "=")
                    {
                        odTest1 = odNew;
                        //check(odNew);
                    }
                    break;


            }
            num = "0";

        }
        private void btnOdAM_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                slt[0] *= -1;
                lbResult0.Content = slt[0].ToString();
            }
            else
            {
                num = (-1 * Double.Parse(num)).ToString();
                lbResult0.Content = num;
            }
        }
        private void button11_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                slt[0] = Math.Sqrt(slt[0]);
                lbResult0.Content = slt[0].ToString();
            }
            else
            {
                num = (Math.Sqrt(Double.Parse(num))).ToString();
                lbResult0.Content = num;
            }
        }
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                if (slt[0] >= 0.0001 || slt[0] < -0.0001)
                {
                    slt[0] = 1 / slt[0];
                    lbResult0.Content = slt[0].ToString();
                }
            }
            else
            {
                num = (1 / Double.Parse(num)).ToString();
                lbResult0.Content = num;
            }
        }
        private void button14_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                string tmp = slt[0].ToString();
                string tmpOut = "";
                for (int i = 0; i < tmp.Length - 1; i++)
                {
                    tmpOut += tmp[i];
                }
                if (tmpOut.Length == 0)
                    tmpOut = "0";
                slt[0] = Double.Parse(tmpOut);
                lbResult0.Content = slt[0].ToString();

            }
            else
            {
                string tmp = num.ToString();
                string tmpOut = "";
                for (int i = 0; i < tmp.Length - 1; i++)
                {
                    tmpOut += tmp[i];
                }
                if (tmpOut.Length == 0)
                    tmpOut = "0";
                num = tmpOut;
                lbResult0.Content = num;

            }
        }
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                slt[0] *= slt[0];
                lbResult0.Content = slt[0].ToString();
            }
            else
            {
                num = (Double.Parse(num) * Double.Parse(num)).ToString();
                lbResult0.Content = num;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                string tmp = slt[0].ToString();
                string tmpOut = "";
                for (int i = 0; i < tmp.Length - 1; i++)
                {
                    tmpOut += tmp[i];
                }
                if (tmpOut.Length == 0)
                    tmpOut = "0";
                slt[0] = Double.Parse(tmpOut);
                lbResult0.Content = slt[0].ToString();

            }
            else
            {
                string tmp = num.ToString();
                string tmpOut = "";
                for (int i = 0; i < tmp.Length - 1; i++)
                {
                    tmpOut += tmp[i];
                }
                if (tmpOut.Length == 0)
                    tmpOut = "0";
                num = tmpOut;
                lbResult0.Content = num;

            }
        }

        private void btnClr_Click(object sender, RoutedEventArgs e)
        {
            lbResult0.Content = "0";
            lbResult.Content = "0";
            num = "0";
            odTest1 = "null";
            odTest2 = "null";
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                slt[0] = Math.Sqrt(slt[0]);
                lbResult0.Content = slt[0].ToString();
            }
            else
            {
                num = (Math.Sqrt(Double.Parse(num))).ToString();
                lbResult0.Content = num;
            }
        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            switch (odTest1)
            {
                case "+":
                    switch (odTest2)
                    {
                        case "×":
                            slt[0] = slt[0] + slt[1] * Double.Parse(num);
                            odTest1 = "null";
                            odTest2 = "null";
                            break;
                        case "÷":
                            if (num != "0")
                            {
                                slt[0] = slt[0] + slt[1] * Double.Parse(num);
                                odTest1 = "null";
                                odTest2 = "null";
                            }
                            break;
                        case "null":
                            slt[0] = slt[0] + Double.Parse(num);
                            odTest1 = "null";
                            break;
                    }
                    break;
                case "-":
                    switch (odTest2)
                    {
                        case "×":
                            slt[0] = slt[0] - slt[1] * Double.Parse(num);
                            odTest1 = "null";
                            odTest2 = "null";
                            break;
                        case "÷":
                            if (num != "0")
                            {
                                slt[0] = slt[0] - slt[1] * Double.Parse(num);
                                odTest1 = "null";
                                odTest2 = "null";
                            }
                            break;
                        case "null":
                            slt[0] = slt[0] - Double.Parse(num);
                            odTest1 = "null";
                            break;
                    }
                    break;
                case "×":
                    slt[0] = slt[0] * Double.Parse(num);
                    odTest1 = "null";
                    break;
                case "÷":
                    if (num != "0")
                    {
                        slt[0] = slt[0] / Double.Parse(num);
                        odTest1 = "null";
                    }
                    break;
            }
            odTest1 = "=";
            num = "0";
            lbResult0.Content = slt[0].ToString();
        }

        private void btnSquare_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                slt[0] *= slt[0];
                lbResult0.Content = slt[0].ToString();
            }
            else
            {
                num = (Double.Parse(num) * Double.Parse(num)).ToString();
                lbResult0.Content = num;
            }
        }

        private void btnCountDown_Click(object sender, RoutedEventArgs e)
        {
            if (odTest1 == "=")
            {
                if (slt[0] >= 0.0001 || slt[0] < -0.0001)
                {
                    slt[0] = 1 / slt[0];
                    lbResult0.Content = slt[0].ToString();
                }
            }
            else
            {
                num = (1 / Double.Parse(num)).ToString();
                lbResult0.Content = num;
            }
        }

    }
}
