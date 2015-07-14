using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using nsDataMgr;
namespace nsVicoClient
{
    public class ItlPr
    {
        //objectType objType = objectType.ItlPr;
        objUnit[] items = new objUnit[100];
        public ItlPr()
        {
            items[0] = new objUnit("Itl000", UnitType.DgtType);
            items[1] = new objUnit("Itl001", UnitType.Tm_s);//prdPr171 min
            items[2] = new objUnit("Itl002", UnitType.Force_ton);
            items[3] = new objUnit("Itl003", UnitType.Prs_Mpa);//PrdPr189
            items[4] = new objUnit("Itl004", UnitType.Len_mm);//PrdPr189
            items[5] = new objUnit("Itl005", UnitType.Len_mm);//PrdPr190
            items[6] = new objUnit("Itl006", UnitType.Len_mm);//PrdPr190
            items[7] = new objUnit("Itl007", UnitType.Tm_s);//PrdPr191
            items[8] = new objUnit("Itl008", UnitType.Prs_Mpa);//PrdPr191
            items[9] = new objUnit("Itl009", UnitType.Len_mm);//PrdPr192
            items[10] = new objUnit("Itl010", UnitType.Prs_Mpa);//PrdPr192
            items[11] = new objUnit("Itl011", UnitType.Tm_s);//PrdPr193
            items[12] = new objUnit("Itl012", UnitType.Len_mm);//PrdPr193
            items[13] = new objUnit("Itl013", UnitType.Tm_s);//PrdPr194
            items[14] = new objUnit("Itl014", UnitType.Tm_s);//PrdPr194
            items[15] = new objUnit("Itl015", UnitType.Prs_Mpa);//PrdPr195
            items[16] = new objUnit("Itl016", UnitType.Prs_Mpa);//PrdPr195
            items[17] = new objUnit("Itl017", UnitType.Len_mm);//PrdPr196
            items[18] = new objUnit("Itl018", UnitType.Len_mm);//PrdPr196
            items[19] = new objUnit("Itl019", UnitType.Prs_Mpa);//PrdPr197
            items[20] = new objUnit("Itl020", UnitType.Prs_Mpa);//PrdPr197
            items[21] = new objUnit("Itl021", UnitType.Tm_s);//PrdPr198
            items[22] = new objUnit("Itl022", UnitType.Tm_s);//PrdPr198
            items[23] = new objUnit("Itl023", UnitType.DgtType);//时间设定 年份
            items[24] = new objUnit("Itl024", UnitType.DgtType);//时间设定 月份
            items[25] = new objUnit("Itl025", UnitType.DgtType);//时间设定 日期
            items[26] = new objUnit("Itl026", UnitType.DgtType);//时间设定 小时
            items[27] = new objUnit("Itl027", UnitType.DgtType);//时间设定 分钟
            items[28] = new objUnit("Itl028", UnitType.DgtType);//时间设定 秒
            items[29] = new objUnit("Itl029", UnitType.DgtType); //对应系统ip地址设置
            items[30] = new objUnit("Itl030", UnitType.PowerHour); //对应数据分析页面耗电图中的最大耗电线
            items[31] = new objUnit("Itl031", UnitType.Len_mm);//偏差
            items[32] = new objUnit("Itl032", UnitType.Tm_s);
            items[33] = new objUnit("Itl033", UnitType.Tm_s);
            items[34] = new objUnit("Itl034", UnitType.Tm_s);
            items[35] = new objUnit("Itl035", UnitType.Tm_s);
            items[36] = new objUnit("Itl036", UnitType.Tm_s);
            items[37] = new objUnit("Itl037", UnitType.Tm_s);
            items[38] = new objUnit("Itl038", UnitType.Tm_s);
            items[39] = new objUnit("Itl039", UnitType.Tm_s);
            
        }
        public objUnit this[int index]
        {
            get
            {
                if (index < 0 || index >= items.Length)
                    return null;
                return items[index];
            }
            set
            {
                items[index] = value;
            }

        }
        //private void read()
        //{
        //    string str = Properties.Settings.Default.ItlPr;
        //    string[] strTmp = str.Split('#');
        //    int num = Int32.Parse(strTmp[0]);
        //    for (int i = 0; i < num; i++)
        //    {

        //    }
        //}
        //public void save()
        //{
        //    string str = items.Length.ToString() + "#";
        //    for (int i = 1; i < items.Length - 1; i++)
        //    {
        //        string strTmp = items[i].value + "," + items[i].vMaxDbl + "," + items[i].vMinDbl + "#";
        //        str += strTmp;
        //    }
        //    Properties.Settings.Default.ItlPr = str;

        //}
    }
}
