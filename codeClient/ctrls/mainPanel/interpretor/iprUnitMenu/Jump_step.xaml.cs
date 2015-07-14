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

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// Interaction logic for Jump_step.xaml
    /// </summary>
    public partial class Jump_step : UserControl
    {
        string curOpeName = "";
        public Jump_step()
        {
            InitializeComponent();
        }
        private void numkeyDisposeFunc()
        {
            btnPA.focusState = false;
            btnCA1.focusState = false;
            btnVA1.focusState = false;
            btnIA1.focusState = false;
            btnVC.focusState = false;
            btnFunc11.focusState = false;
            btnFunc12.focusState = false;
            btnFunc13.focusState = false;
            btnFunc14.focusState = false;
            btnFunc15.focusState = false;
            btnFunc16.focusState = false;
        }
        public void setValue()
        {
            iprCtrl.curUnit.get_sNotReady();
            if (iprCtrl.curUnit.sNotReady != 0)
            {
                if (iprCtrl.curUnit.sErrLink)
                {
                    activeErr1Ctrl1.Visibility = Visibility.Visible;
                    activeErr1Ctrl1.dis = "触发异常";
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
                    activeErr1Ctrl1.Visibility = Visibility.Hidden;
                }
                if (iprCtrl.curUnit.sErrUndefined)
                {
                    activeErr1Ctrl1.Visibility = Visibility.Visible;
                    activeErr1Ctrl1.dis = "功能未定义";
                }
                else
                {
                    activeErr1Ctrl1.Visibility = Visibility.Hidden;
                }
            }

            iprCtrl.curUnit.get_sFuncSelect();
            btn1.focusState = false;
            btn2.focusState = false;
            btn3.focusState = false;
            btn4.focusState = false;
            btn5.focusState = false;
            btn6.focusState = false;
            btn7.focusState = false;
            switch (iprCtrl.curUnit.sFuncSelect)
            {
                case 0:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn1.setErrValue();
                        }
                        else
                        {
                            btn1.clearErrValue();
                        }
                        
                        curOpeName = "数字输入";
                        btn1.focusState = true;
                        tbValue.SelectedItem = menu_digIn;
                        iprCtrl.curUnit.get_sValueE();
                        btnDE1.focusState = false;
                        btnDE2.focusState = false;
                        btnDE3.focusState = false;
                        btnDE4.focusState = false;
                        switch (iprCtrl.curUnit.sValueE)
                        {
                            case 0:
                                {
                                    btnDE1.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnDE1.setErrValue();
                                    }
                                    else
                                    {
                                        btnDE1.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnDE2.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnDE2.setErrValue();
                                    }
                                    else
                                    {
                                        btnDE2.clearErrValue();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    btnDE3.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnDE3.setErrValue();
                                    }
                                    else
                                    {
                                        btnDE3.clearErrValue();
                                    }
                                }
                                break;
                            case 3:
                                {
                                    btnDE4.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnDE4.setErrValue();
                                    }
                                    else
                                    {
                                        btnDE4.clearErrValue();
                                    }
                                }
                                break;
                        }
                        iprCtrl.curUnit.get_sValueA();
                        btnDA0.focusState = false;
                        btnDA1.focusState = false;
                        switch (iprCtrl.curUnit.sValueA)
                        {
                            case 0:
                                {
                                    btnDA0.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueA)
                                    {
                                        btnDA0.setErrValue();
                                    }
                                    else
                                    {
                                        btnDA0.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnDA1.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueA)
                                    {
                                        btnDA1.setErrValue();
                                    }
                                    else
                                    {
                                        btnDA1.clearErrValue();
                                    }
                                }
                                break;
                        }
                        btnVC.dis = iprCtrl.curUnit.getStrValueC();
                        if (iprCtrl.curUnit.sErrValueC)
                        {
                            btnVC.setErrValue();
                        }
                        else
                        {
                            btnVC.clearErrValue();
                        }
                    }
                    break;
                case 1:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn2.setErrValue();
                        }
                        else
                        {
                            btn2.clearErrValue();
                        }
                        curOpeName = "位置";
                        btn2.focusState = true;
                        tbValue.SelectedItem = menu_pos;
                        btnPE1.focusState = false;
                        btnPE2.focusState = false;
                        btnPE3.focusState = false;
                        btnPE4.focusState = false;
                        btnPE5.focusState = false;
                        bool flagVisiblePE4 = ((valmoWin.dv.IprPr[18].valueNew >> 11) & 0x01) == 1;
                        if (flagVisiblePE4)
                        {
                            btnPE4.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            btnPE4.Visibility = Visibility.Hidden;
                        }
                        iprCtrl.curUnit.get_sValueE();
                        switch (iprCtrl.curUnit.sValueE)
                        {
                            case 0:
                                {
                                    btnPE1.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnPE1.setErrValue();
                                    }
                                    else
                                    {
                                        btnPE1.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnPE2.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnPE2.setErrValue();
                                    }
                                    else
                                    {
                                        btnPE2.clearErrValue();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    btnPE3.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnPE3.setErrValue();
                                    }
                                    else
                                    {
                                        btnPE3.clearErrValue();
                                    }
                                }
                                break;
                            case 3:
                                {
                                    btnPE4.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnPE4.setErrValue();
                                    }
                                    else
                                    {
                                        btnPE4.clearErrValue();
                                    }
                                }
                                break;
                            case 4:
                                {
                                    btnPE5.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnPE5.setErrValue();
                                    }
                                    else
                                    {
                                        btnPE5.clearErrValue();
                                    }
                                }
                                break;
                        }
                        btnP0.focusState = false;
                        btnP1.focusState = false;
                        btnP2.focusState = false;
                        iprCtrl.curUnit.get_sOperateType();
                        switch (iprCtrl.curUnit.sOperateType)
                        {
                            case 0:
                                {
                                    btnP0.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnP0.setErrValue();
                                    }
                                    else
                                    {
                                        btnP0.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnP1.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnP1.setErrValue();
                                    }
                                    else
                                    {
                                        btnP1.clearErrValue();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    btnP2.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnP2.setErrValue();
                                    }
                                    else
                                    {
                                        btnP2.clearErrValue();
                                    }
                                }
                                break;

                        }
                        btnPA.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnPA.setErrValue();
                        }
                        else
                        {
                            btnPA.clearErrValue();
                        }
                        btnFunc11.dis = iprCtrl.curUnit.getStrValueC();
                    }
                    break;
                case 2:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn3.setErrValue();
                        }
                        else
                        {
                            btn3.clearErrValue();
                        }
                        curOpeName = "喷嘴接触";
                        btn3.focusState = true;
                        tbValue.SelectedItem = menu_NozzleTouch;
                        btnNA0.focusState = false;
                        btnNA1.focusState = false;
                        iprCtrl.curUnit.get_sValueA();
                        switch (iprCtrl.curUnit.sValueA)
                        {
                            case 0:
                                {
                                    btnNA0.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueA)
                                    {
                                        btnNA0.setErrValue();
                                    }
                                    else
                                    {
                                        btnNA0.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnNA1.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueA)
                                    {
                                        btnNA1.setErrValue();
                                    }
                                    else
                                    {
                                        btnNA1.clearErrValue();
                                    }
                                }
                                break;
                        }
                        btnFunc12.dis = iprCtrl.curUnit.getStrValueC();
                        if (iprCtrl.curUnit.sErrValueC)
                        {
                            btnFunc12.setErrValue();
                        }
                        else
                        {
                            btnFunc12.clearErrValue();
                        }
                    }
                    break;
                case 3:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn4.setErrValue();
                        }
                        else
                        {
                            btn4.clearErrValue();
                        }
                        curOpeName = "锁模力";
                        btn4.focusState = true;
                        tbValue.SelectedItem = menu_ClampForce;
                        btnC0.focusState = false;
                        btnC1.focusState = false;
                        btnC2.focusState = false;
                        iprCtrl.curUnit.get_sOperateType();
                        switch (iprCtrl.curUnit.sOperateType)
                        {
                            case 0:
                                {
                                    btnC0.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnC0.setErrValue();
                                    }
                                    else
                                    {
                                        btnC0.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnC1.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnC1.setErrValue();
                                    }
                                    else
                                    {
                                        btnC1.clearErrValue();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    btnC2.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnC2.setErrValue();
                                    }
                                    else
                                    {
                                        btnC2.clearErrValue();
                                    }
                                }
                                break;
                        }
                        btnCA1.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnCA1.setErrValue();
                        }
                        else
                        {
                            btnCA1.clearErrValue();
                        }
                        btnFunc13.dis = iprCtrl.curUnit.getStrValueC();
                        if (iprCtrl.curUnit.sErrValueC)
                        {
                            btnFunc13.setErrValue();
                        }
                        else
                        {
                            btnFunc13.clearErrValue();
                        }
                    }
                    break;
                case 5:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn5.setErrValue();
                        }
                        else
                        {
                            btn5.clearErrValue();
                        }
                        curOpeName = "计数器";
                        btn5.focusState = true;
                        tbValue.SelectedItem = menu_Variables;
                        btnVE1.focusState = false;
                        btnVE2.focusState = false;
                        btnVE3.focusState = false;
                        btnVE4.focusState = false;
                        btnVE5.focusState = false;
                        btnVE6.focusState = false;
                        btnVE7.focusState = false;
                        iprCtrl.curUnit.get_sValueE();
                        switch (iprCtrl.curUnit.sValueE)
                        {
                            case 0:
                                {
                                    btnVE1.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnVE1.setErrValue();
                                    }
                                    else
                                    {
                                        btnVE1.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnVE2.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnVE2.setErrValue();
                                    }
                                    else
                                    {
                                        btnVE2.clearErrValue();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    btnVE3.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnVE3.setErrValue();
                                    }
                                    else
                                    {
                                        btnVE3.clearErrValue();
                                    }
                                }
                                break;
                            case 3:
                                {
                                    btnVE4.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnVE4.setErrValue();
                                    }
                                    else
                                    {
                                        btnVE4.clearErrValue();
                                    }
                                }
                                break;
                            case 4:
                                {
                                    btnVE5.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnVE5.setErrValue();
                                    }
                                    else
                                    {
                                        btnVE5.clearErrValue();
                                    }
                                }
                                break;
                            case 5:
                                {
                                    btnVE6.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnVE6.setErrValue();
                                    }
                                    else
                                    {
                                        btnVE6.clearErrValue();
                                    }
                                }
                                break;
                            case 6:
                                {
                                    btnVE7.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnVE7.setErrValue();
                                    }
                                    else
                                    {
                                        btnVE7.clearErrValue();
                                    }
                                }
                                break;
                        }
                        btnV0.focusState = false;
                        btnV1.focusState = false;
                        btnV2.focusState = false;
                        iprCtrl.curUnit.get_sOperateType();
                        switch (iprCtrl.curUnit.sOperateType)
                        {
                            case 0:
                                {
                                    btnV0.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnV0.setErrValue();
                                    }
                                    else
                                    {
                                        btnV0.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnV1.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnV1.setErrValue();
                                    }
                                    else
                                    {
                                        btnV1.clearErrValue();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    btnV2.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnV2.setErrValue();
                                    }
                                    else
                                    {
                                        btnV2.clearErrValue();
                                    }
                                }
                                break;
                        }
                        btnVA1.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnVA1.setErrValue();
                        }
                        else
                        {
                            btnVA1.clearErrValue();
                        }
                        btnFunc14.dis = iprCtrl.curUnit.getStrValueC();
                        if (iprCtrl.curUnit.sErrValueC)
                        {
                            btnFunc14.setErrValue();
                        }
                        else
                        {
                            btnFunc14.clearErrValue();
                        }
                    }
                    break;
                case 6:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn6.setErrValue();
                        }
                        else
                        {
                            btn6.clearErrValue();
                        }
                        curOpeName = "欧规信号";
                        btn6.focusState = true;
                        tbValue.SelectedItem = menu_EuromapIn;
                        btnEE1.focusState = false;
                        btnEE2.focusState = false;
                        btnEE3.focusState = false;
                        btnEE4.focusState = false;
                        btnEE5.focusState = false;
                        btnEE6.focusState = false;
                        iprCtrl.curUnit.get_sValueE();
                        switch (iprCtrl.curUnit.sValueE)
                        {
                            case 0:
                                {
                                    btnEE1.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnEE1.setErrValue();
                                    }
                                    else
                                    {
                                        btnEE1.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnEE2.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnEE2.setErrValue();
                                    }
                                    else
                                    {
                                        btnEE2.clearErrValue();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    btnEE3.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnEE3.setErrValue();
                                    }
                                    else
                                    {
                                        btnEE3.clearErrValue();
                                    }
                                }
                                break;
                            case 3:
                                {
                                    btnEE4.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnEE4.setErrValue();
                                    }
                                    else
                                    {
                                        btnEE4.clearErrValue();
                                    }
                                }
                                break;
                            case 4:
                                {
                                    btnEE5.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnEE5.setErrValue();
                                    }
                                    else
                                    {
                                        btnEE5.clearErrValue();
                                    }
                                }
                                break;
                            case 5:
                                {
                                    btnEE6.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueE)
                                    {
                                        btnEE6.setErrValue();
                                    }
                                    else
                                    {
                                        btnEE6.clearErrValue();
                                    }
                                }
                                break;
                        }
                        iprCtrl.curUnit.get_sValueA();
                        btnEA1.focusState = false;
                        btnEA0.focusState = false;
                        switch (iprCtrl.curUnit.sValueA)
                        {
                            case 1:
                                {
                                    btnEA1.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueA)
                                    {
                                        btnEA1.setErrValue();
                                    }
                                    else
                                    {
                                        btnEA1.clearErrValue();
                                    }
                                }
                                break;
                            case 0:
                                {
                                    btnEA0.focusState = true;
                                    if (iprCtrl.curUnit.sErrValueA)
                                    {
                                        btnEA0.setErrValue();
                                    }
                                    else
                                    {
                                        btnEA0.clearErrValue();
                                    }
                                }
                                break;
                        }
                        btnFunc15.dis = iprCtrl.curUnit.getStrValueC();
                        if (iprCtrl.curUnit.sErrValueC)
                        {
                            btnFunc15.setErrValue();
                        }
                        else
                        {
                            btnFunc15.clearErrValue();
                        }

                    }
                    break;
                case 7:
                    {
                        if (iprCtrl.curUnit.sErrFuncSelect)
                        {
                            btn7.setErrValue();
                        }
                        else
                        {
                            btn7.clearErrValue();
                        }
                        curOpeName = "注射压力";
                        btn7.focusState = true;
                        tbValue.SelectedItem = menu_InjectionPressure;
                        btnI0.focusState = false;
                        btnI1.focusState = false;
                        btnI2.focusState = false;
                        iprCtrl.curUnit.get_sOperateType();
                        switch (iprCtrl.curUnit.sOperateType)
                        {
                            case 0:
                                {
                                    btnI0.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnI0.setErrValue();
                                    }
                                    else
                                    {
                                        btnI0.clearErrValue();
                                    }
                                }
                                break;
                            case 1:
                                {
                                    btnI1.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnI1.setErrValue();
                                    }
                                    else
                                    {
                                        btnI1.clearErrValue();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    btnI2.focusState = true;
                                    if (iprCtrl.curUnit.sErrOpSelect)
                                    {
                                        btnI2.setErrValue();
                                    }
                                    else
                                    {
                                        btnI2.clearErrValue();
                                    }
                                }
                                break;
                        }
                        btnIA1.dis = iprCtrl.curUnit.getStrValueA();
                        if (iprCtrl.curUnit.sErrValueA)
                        {
                            btnIA1.setErrValue();
                        }
                        else
                        {
                            btnIA1.clearErrValue();
                        }
                        btnFunc16.dis = iprCtrl.curUnit.getStrValueC();
                        if (iprCtrl.curUnit.sErrValueC)
                        {
                            btnFunc16.setErrValue();
                        }
                        else
                        {
                            btnFunc16.clearErrValue();
                        }
                    }
                    break;

            }
            //btnVC.dis = iprCtrl.curUnit.getStrValueC();
        }

        private void btn1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btn2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void btn3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btn4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void btn5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btn6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void btn7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btn1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(0);
        }

        private void btn2_MouseUp(object sender, MouseButtonEventArgs e)
        {
           iprCtrl.curUnit.set_sFuncSelect(1);
        }

        private void btn3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(2);
        }

        private void btn4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(3);
        }

        private void btn5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(5);
        }

        private void btn6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(6);
        }

        private void btn7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sFuncSelect(7);
        }

        private void btnDE1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnDE2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnDE3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnDE4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnDE1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(0);
        }

        private void btnDE2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(1);
        }

        private void btnDE3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(2);
        }

        private void btnDE4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(3);
        }

        private void btnDA1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnDA0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnDA1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueA(1);
        }

        private void btnDA0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueA(0);
        }

        private void btnPE1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnPE2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnPE3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnPE4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void btnPE5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void btnPE1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(0);
        }

        private void btnPE2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(1);
        }

        private void btnPE3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(2);
        }

        private void btnPE4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(3);
        }
        private void btnPE5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(4);
        }
        private void btnP1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnP0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnP2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnP1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }

        private void btnP0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(0);
        }

        private void btnP2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(2);
        }

        private void btnPA_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnPA_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueAObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, curOpeName, numkeyDisposeFunc);
            btnPA.focusState = false;
        }

        private void btnNA1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnNA0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnNA1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueA(1);
        }

        private void btnNA0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueA(0);
        }

        private void btnC1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnC0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnC2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnC1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }

        private void btnC0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(0);
        }

        private void btnC2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(2);
        }

        private void btnCA1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnCA1_MouseUp(object sender, MouseButtonEventArgs e)
        { 
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueAObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, curOpeName, numkeyDisposeFunc);
            btnCA1.focusState = false;
        }

        private void btnVE1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnVE2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnVE3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnVE4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnVE5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnVE6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnVE7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnVE1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(0);
        }

        private void btnVE2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(1);
        }

        private void btnVE3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(2);
        }

        private void btnVE4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(3);
        }

        private void btnVE5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(4);
        }

        private void btnVE6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(5);
        }

        private void btnVE7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(6);
        }

        private void btnV1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnV0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnV2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnV1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }

        private void btnV0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(0);
        }

        private void btnV2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(2);
        }

        private void btnVA1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnVA1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueAObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, curOpeName, numkeyDisposeFunc);
            btnVA1.focusState = true;    
        }

        private void btnEE1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnEE2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnEE3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnEE4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnEE5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnEE6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnEE1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(0);
        }

        private void btnEE2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(1);
        }

        private void btnEE3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(2);
        }

        private void btnEE4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(3);
        }

        private void btnEE5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(4);
        }

        private void btnEE6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueE(5);
        }

        private void btnEA1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnEA2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnEA1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueA(1);
        }

        private void btnEA2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sValueA(0);
        }

        private void btnI1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnI0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnI2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnI1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(1);
        }

        private void btnI0_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(0);
        }

        private void btnI2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iprCtrl.curUnit.set_sOperateType(2);
        }

        private void btnIA1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnIA1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueAObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueA, curOpeName, numkeyDisposeFunc);
            btnIA1.focusState = true;
        }

        private void btnVC_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnVC_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnVC.focusState = true;
            Thickness margin = new Thickness(200, 200, 0, 0);
            iprCtrl.curUnit.get_sValueCObj();
            valmoWin.SNumKeyPanel.init(iprCtrl.curUnit.objValueC, "跳转至", numkeyDisposeFunc);
        }
    }
}
