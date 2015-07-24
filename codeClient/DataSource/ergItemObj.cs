using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using nsVicoClient.ctrls;

namespace nsVicoClient
{
    public enum recType : byte
    {
        alarmType = 0,
        operateType = 1,
        sysType = 2,
        logType = 3
    }

    public class recUnit
    {
        public string serialNum { get; set; }
        public int listNum;
        public string userName { get; set; }
        public string discription { get; set; }
        public DateTime dtStart { get; set; }
        public DateTime dtEnd;
        public int plateNums;
        public recType type;
        public double oldValue;
        public double newValue;

        public recUnit()
        {
            this.serialNum = "";
            this.userName = "";
            this.discription = "";
            this.dtStart = DateTime.Now;
            this.dtEnd = valmoWin.tmNull;
            this.plateNums = 0;
            this.oldValue = 0;
            this.newValue = 0;
            this.type = recType.alarmType;
        }
        public recUnit(string serialNum, string userName, string dis, DateTime dtStart, DateTime dtEnd, int plateNum, double oldValue, double newValue ,recType type)
        {
            this.serialNum = serialNum;
            this.userName = userName;
            this.discription = dis;
            this.dtStart = dtStart;
            this.dtEnd = dtEnd;
            this.plateNums = plateNum;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.type = type;
        }
        public recUnit(string serialNum, string userName, DateTime dtStart, DateTime dtEnd, int plateNum, double oldValue, double newValue, recType type)
        {
            this.serialNum = serialNum;
            this.userName = userName;
            string objDis = valmoWin.dv.getCurDis(serialNum); //Application.Current.Resources.MergedDictionaries[0][serialNum];
            if (objDis != null && objDis != "null")
                this.discription = objDis;
            else
                this.discription = " ";
            this.dtStart = dtStart;
            this.dtEnd = dtEnd;
            this.plateNums = plateNum;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.type = type;
        }
        public recUnit(string serialNum, string userName, DateTime dtStart, DateTime dtEnd, int plateNum)
        {
            this.serialNum = serialNum;
            this.userName = userName;
            string objDis = valmoWin.dv.getCurDis(serialNum); //Application.Current.Resources.MergedDictionaries[0][serialNum];
            if (objDis != null && objDis != "null")
                this.discription = objDis;
            else
                this.discription = " ";
            this.dtStart = dtStart;
            this.dtEnd = dtEnd;
            this.plateNums = plateNum;
            this.oldValue = 0;
            this.newValue = 0;
            this.type = recType.alarmType;
        }
        public recUnit(string serialNum, DateTime dtStart, recType type)
        {
            this.serialNum = serialNum;
            this.userName = valmoWin.dv.users.curUser.name;
            string objDis = valmoWin.dv.getCurDis(serialNum); //Application.Current.Resources.MergedDictionaries[0][serialNum];
            if (objDis != null && objDis != "null")
                this.discription = objDis;
            else
                this.discription = " ";
            this.dtStart = dtStart;
            //this.dtEnd = dtEnd;
            this.plateNums = valmoWin.dv.getCurPlateNr();
            this.oldValue = 0;
            this.newValue = 0;
            this.type = type;
        }
        public recUnit(string serialNum,string userName, DateTime dtStart, recType type)
        {
            this.serialNum = serialNum;
            this.userName = userName;
            string objDis = valmoWin.dv.getCurDis(serialNum); //Application.Current.Resources.MergedDictionaries[0][serialNum];
            if (objDis != null && objDis != "null")
                this.discription = objDis;
            else
                this.discription = " ";
            this.dtStart = dtStart;
            //this.dtEnd = dtEnd;
            this.plateNums = valmoWin.dv.getCurPlateNr();
            this.oldValue = 0;
            this.newValue = 0;
            this.type = type;
        }
        public recUnit(DateTime dtStart, DateTime dtEnd)
        {
            //this.serialNum = serialNum;
            this.userName = valmoWin.dv.users.curUser.name;
            string objDis = valmoWin.dv.getCurDis(serialNum); //Application.Current.Resources.MergedDictionaries[0][serialNum];
            if (objDis != null && objDis != "null")
                this.discription = objDis;
            else
                this.discription = " ";
            this.dtStart = dtStart;
            this.dtEnd = dtEnd;
            this.plateNums = valmoWin.dv.getCurPlateNr();
            this.oldValue = 0;
            this.newValue = 0;
            this.type = recType.logType;
        }


        public recUnit(string str)
        {
            try
            {
                string[] strTmp = str.Split('\t');
                if (strTmp.Length < 4)
                {
                    this.serialNum = "error";
                    return;
                }
                this.serialNum = strTmp[0];
                this.userName = strTmp[1];
                this.dtStart = DateTime.Parse(strTmp[2]);
                this.plateNums = Int32.Parse(strTmp[3]);

                switch (serialNum.Substring(0,3))
                {
                    case "Alm":
                        {
                            this.type = recType.alarmType;

                            //if (strTmp[4] == "--")
                            //{

                            //}
                            //else
                            //{
                            //    this.dtEnd = DateTime.Parse(strTmp[4]);
                            //}
                        }
                        break;
                    case "Log":
                        {
                            this.type = recType.logType;
                        }
                        break;
                    case "Als":
                        {
                            this.type = recType.sysType;
                        }
                        break;
                    default:
                        {
                            this.oldValue = double.Parse(strTmp[4]);
                            this.newValue = double.Parse(strTmp[5]);
                            this.type = recType.operateType;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.serialNum = "error";
                vm.printLn("[ergItemObj] " + ex.ToString());
            }

        }
        public static recUnit getObjFromStr(string str)
        {
            recUnit obj = null;
            try
            {
                string[] strTmp = str.Split(',');
                if (strTmp.Length > 4)
                {
                    obj.serialNum = strTmp[0];
                    obj.userName = strTmp[1];
                    obj.dtStart = DateTime.Parse(strTmp[2]);
                    obj.plateNums = Int32.Parse(strTmp[3]);
                    obj.discription = valmoWin.dv.getCurDis(strTmp[0]);
                    switch (strTmp[0][0])
                    {
                        case 'A':
                            {
                                obj.type = recType.alarmType;
                                if (strTmp.Length != 5)
                                    break;
                                obj.dtEnd = DateTime.Parse(strTmp[4]);
                            }
                            break;
                        case 'M':
                            {
                                obj.type = recType.operateType;
                                if (strTmp.Length != 6)
                                    break;
                                obj.oldValue = double.Parse(strTmp[4]);
                                obj.newValue = double.Parse(strTmp[5]);

                            }
                            break;
                        case 'S':
                            {
                                obj.type = recType.sysType;
                            }
                            break;
                        case 'L':
                            {
                                obj.type = recType.logType;
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                vm.printLn("[ergItemObj] " + ex.ToString());
            }
            return obj;
        }

        public string toSaveString(string str)
        {
            string strReturn = null;

            strReturn = serialNum + str + userName + str + dtStart.ToString("yyyy/MM/dd hh:mm:ss") + str + plateNums + str;

            if ((serialNum[0] == 'A') || (serialNum[0] == 'L'))
            {
                if (dtEnd.Year != 1)
                {
                    strReturn += dtEnd.ToString("yyyy.MM.dd hh:mm:ss") + str;
                }
                else
                {
                    strReturn += "--" + str;
                }
            }
            else
            {
                strReturn += oldValue + str + newValue + str;
            }
            return strReturn;
        }
        public static string dis
        {
            get
            {
                return valmoWin.dv.getCurDis("lanKey034") + "\t" 
                     + valmoWin.dv.getCurDis("lanKey035") + "\t" 
                     + valmoWin.dv.getCurDis("lanKey036") + "\t" 
                     + valmoWin.dv.getCurDis("lanKey037") + "\t"
                     + valmoWin.dv.getCurDis("lanKey039") + "\t"
                     + valmoWin.dv.getCurDis("lanKey038") + "\t"
                     + valmoWin.dv.getCurDis("lanKey040") + "\t"
                     + valmoWin.dv.getCurDis("lanKey041") ;
                //return "编号\t用户名\t说明\t触发时间\t结束时间\t模数\t旧值\t新值";

            }
        }
        public string toExportString(string str)
        {
            string strReturn = serialNum + str + userName + str + valmoWin.dv.getCurDis(serialNum) + str + dtStart.ToString("yyyy/MM/dd hh:mm:ss") + str + plateNums + str;
            if(dtEnd.Year != 1)
                strReturn += dtEnd.ToString("yyyy/MM/dd hh:mm:ss") + str;
            else
                strReturn += " " + str;
            if (oldValue < 0.0001 && oldValue > -0.0001 && newValue < 0.0001 && newValue > -0.0001)
            {
                strReturn += " " + str;
                strReturn += " " + str;
            }
            else
            {
                strReturn += oldValue + str;
                strReturn += newValue + str;
                vm.printLn("len:" + strReturn.Length);
            }
            
            return strReturn;
        }
    }
}
