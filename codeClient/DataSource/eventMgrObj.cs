using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using nsVicoClient.ctrls;
using nsDataMgr;
using System.Xml;

namespace nsVicoClient
{
    public delegate void addNewItemsEvent();
	public class eventMgrObj
	{
        string intervalChart = "\t";
        public delegate void callbackWhenNewDataComeEvent();
        
        public static List<recUnit> itemsLst = new List<recUnit>();
        public static List<recUnit> filterLst = new List<recUnit>();

        public eventMgrObj()
        {
            if (!Directory.Exists("conf"))
            {
                Directory.CreateDirectory("conf");
            }
            loadMessageFromFile();
        }

        public void addParamMsg(objUnit obj, double oldValue, recType type)
        {
            string ergSerialNum = "--";
            if (obj.serialNum != null)
                ergSerialNum = obj.serialNum;
            string userName = valmoWin.dv.users.curUser.name;
            if (userName == "null")
                userName = "--";
            DateTime dtStart = DateTime.Now;
            int plateNum = valmoWin.dv.getCurPlateNr();
            double newValue = obj.valueNew;
            recUnit erObj = new recUnit(ergSerialNum, userName, dtStart, valmoWin.sNullTime, plateNum, oldValue, newValue, type);
            msgSave(erObj);
        }

        public void addParamMsg(string serialNum, DateTime dtStart, double oldValue, double newValue, recType type = recType.operateType)
        {
            string ergSerialNum = "--";
            if (serialNum != null)
                ergSerialNum = serialNum;
            string userName = valmoWin.dv.users.curUser.name;
            int plateNum = valmoWin.dv.getCurPlateNr();
            recUnit erObj = new recUnit(serialNum, userName, dtStart, valmoWin.sNullTime, plateNum, oldValue, newValue, type);
            msgSave(erObj);

            valmoWin.refresh();
        }

        /// <summary>
        /// 产生警是记录
        /// </summary>
        /// <param name="erObj"></param>
        public void msgSave(recUnit erObj)
        {
            itemsLst.Insert(0, erObj);

            valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwMsg));

            if (itemsLst.Count - countFs > 50)
            {
                saveToFile();
            }
        }
        List<XmlElement> emLst = new List<XmlElement>();

        /// <summary>
        /// 自动保存
        /// </summary>
        public void saveToFile()
        {
            FileStream fs = new FileStream(@"conf\msgRecord.vm", FileMode.OpenOrCreate);
            fs.Seek(0, SeekOrigin.End);
            for (int i = itemsLst.Count - countFs - 1; i >= 0; i--)
            {
                string str = itemsLst[i].toSaveString(intervalChart);
                byte[] buffer = System.Text.Encoding.Default.GetBytes(str);
                fs.Write(buffer, 0, buffer.Length);
                for (int j = buffer.Length; j < 120; j++)
                {
                    fs.WriteByte(0);
                }
                countFs++;
            }
            fs.Flush();
            fs.Close();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>操作结果</returns>
        public static bool saveToFile(string fileName)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(fileName, FileMode.OpenOrCreate);
                sw = new StreamWriter(fs);
                string tmp = "编号\t用户\t对象\t触发时间\t结束时间\t模数\t旧值\t新值";
                sw.WriteLine(tmp);

                for (int i = 0; i < filterLst.Count; i++)
                {
                    tmp = string.Empty;
                    tmp += filterLst[i].serialNum.ToString() + "\t";
                    tmp += filterLst[i].userName.ToString() + "\t";
                    tmp += valmoWin.dv.getCurDis(filterLst[i].serialNum) + "\t";
                    tmp += filterLst[i].dtStart.ToString() + "\t";
                    if (filterLst[i].dtEnd.Year != 1)
                    {
                        tmp += filterLst[i].dtEnd.ToString() + "\t";
                    }
                    else
                    {
                        tmp += " " + "\t";
                    }
                    tmp += filterLst[i].plateNums.ToString() + "\t";
                    if (filterLst[i].oldValue != 0)
                    {
                        tmp += filterLst[i].oldValue.ToString() + "\t";
                    }
                    else
                    {
                        tmp += " " + "\t";
                    }
                    if (filterLst[i].newValue != 0)
                    {
                        tmp += filterLst[i].newValue.ToString();
                    }
                    else
                    {
                        tmp += " " + "\t";
                    }

                    sw.WriteLine(tmp);
                }

                sw.Close();
                fs.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                if(sw != null)
                    sw.Close();
                if(fs != null)
                    fs.Close();
                return false;

            }

        }
        int count = 0;
        int countFs = 0;

        /// <summary>
        /// 读取事件记录
        /// </summary>
        public void loadMessageFromFile()
        {
            FileStream fs = new FileStream(@"conf\msgRecord.vm", FileMode.OpenOrCreate);
            byte[] buffer = new byte[120];
            while (fs.Read(buffer, 0, 120) > 0)
            {
                string str = System.Text.Encoding.Default.GetString(buffer);
                recUnit itemUnit = new recUnit(str);
                if (itemUnit.serialNum != "error")
                {
                    itemsLst.Insert(0,itemUnit);
                    countFs++;
                }
            }
            fs.Close();
        }
	}
}
