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
    /// MachionActionCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class MachionActionCtrl : UserControl
    {
        public List<Image> lstMachionAction1;
        public List<Image> lstMachionAction2;

        public MachionActionCtrl()
        {
            InitializeComponent();

            lstMachionAction1 = new List<Image>();
            lstMachionAction1.Add(Action0);
            lstMachionAction1.Add(Action1);
            lstMachionAction1.Add(Action2);
            lstMachionAction1.Add(Action3);
            lstMachionAction1.Add(Action4);
            lstMachionAction1.Add(Action5);
            lstMachionAction1.Add(Action6);
            lstMachionAction1.Add(Action7);
            lstMachionAction1.Add(Action8);
            lstMachionAction1.Add(Action9);
            lstMachionAction1.Add(Action10);
            lstMachionAction1.Add(Action11);
            lstMachionAction1.Add(Action12);
            lstMachionAction1.Add(Action13);
            lstMachionAction1.Add(Action14);
            lstMachionAction1.Add(Action15);
            lstMachionAction1.Add(Action16);
            lstMachionAction1.Add(Action17);
            lstMachionAction1.Add(Action18);
            lstMachionAction1.Add(Action19);
            lstMachionAction1.Add(Action20);
            lstMachionAction1.Add(Action21);
            lstMachionAction1.Add(Action22);
            lstMachionAction1.Add(Action23);
            lstMachionAction1.Add(Action24);
            lstMachionAction1.Add(Action25);
            lstMachionAction1.Add(Action26);
            lstMachionAction1.Add(Action27);
            lstMachionAction1.Add(Action28);
            lstMachionAction1.Add(Action29);
            lstMachionAction1.Add(Action30);
            lstMachionAction1.Add(Action31);

            lstMachionAction2 = new List<Image>();
            lstMachionAction2.Add(Action32);
            lstMachionAction2.Add(Action33);
            lstMachionAction2.Add(Action34);
            lstMachionAction2.Add(Action35);
            lstMachionAction2.Add(Action36);
            lstMachionAction2.Add(Action37);
            lstMachionAction2.Add(Action38);
            lstMachionAction2.Add(Action39);
            lstMachionAction2.Add(Action40);
            lstMachionAction2.Add(Action41);
            lstMachionAction2.Add(Action42);
            lstMachionAction2.Add(Action43);
            lstMachionAction2.Add(Action44);
            lstMachionAction2.Add(Action45);
            lstMachionAction2.Add(Action46);
            lstMachionAction2.Add(Action47);
            lstMachionAction2.Add(Action48);
            lstMachionAction2.Add(Action49);
            lstMachionAction2.Add(Action50);
            lstMachionAction2.Add(Action51);
            lstMachionAction2.Add(Action52);
            lstMachionAction2.Add(Action53);
            lstMachionAction2.Add(Action54);
            lstMachionAction2.Add(Action55);
            lstMachionAction2.Add(Action56);
            lstMachionAction2.Add(Action57);
            lstMachionAction2.Add(Action58);
            lstMachionAction2.Add(Action59);
            lstMachionAction2.Add(Action60);
            lstMachionAction2.Add(Action61);
            lstMachionAction2.Add(Action62);
            lstMachionAction2.Add(Action63);

            valmoWin.dv.SysPr[106].addHandle(MachionAction1);
            valmoWin.dv.SysPr[107].addHandle(MachionAction2);
        }

        private void MachionAction1(objUnit obj)
        {
            int temp = obj.value;

            for (int i = 0; i < 32; i++)
            {
                if (((temp >> i) & 0x01) == 1)
                {
                    lstMachionAction1[i].Width = 40;
                }
                else
                {
                    lstMachionAction1[i].Width = 0;
                }
            }
        }

        private void MachionAction2(objUnit obj)
        {
            int temp = obj.value;

            for (int i = 0; i < 32; i++)
            {
                if (((temp >> i) & 0x01) == 1)
                {
                    lstMachionAction2[i].Width = 40;
                }
                else
                {
                    lstMachionAction2[i].Width = 0;
                }
            }
        }
    }
}
