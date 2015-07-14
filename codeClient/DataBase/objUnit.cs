using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Controls;
using System.IO;

namespace nsDataMgr
{
    public enum objectType
    {
        nullType = 0,
        IprPr = 1,
        SysPr = 2,
        MldPr = 3,
        InjPr = 4,
        TmpPr = 5,
        PrdPr = 6,
        IOTPr = 7,
        AlmPr = 8,
        KeyPr = 9,
        AioPr = 20,
        Key = 100,
        Alarm = 101,
        AlmSys,
        IOTState,
        IOTForce,
        ItlPr = 1741
    }
    public enum UnitType : byte
    {
        /// <summary>  
        /// value=0 rate=1 unit=""
        /// </summary>
        DgtType,
        /// <summary> 
        /// value=0.00 rate=10000 unit="mm"
        /// </summary>
        Len_mm,
        /// <summary> 
        /// value=0.00 rate=10000 * 25.4 unit="inch"
        /// </summary>
        Len_inch,
        /// <summary> 
        /// value=0.00 rate=10000 unit="Mpa"
        /// </summary>
        Prs_Mpa,
        /// <summary> 
        /// value=0.0 rate=1000 unit="Bar"
        /// </summary>
        Prs_Bar,
        /// <summary> 
        /// value=0 rate=10000 / 145 unit="PSI"
        /// </summary>
        Prs_PSI,
        /// <summary> 
        /// value=0.00 rate=1000 unit="s"
        /// </summary>
        Tm_s,
        /// <summary>
        /// value=0.000 rate=1000 unit="s"
        /// </summary>
        Tm_s3,
        /// <summary> 
        /// value=0 rate= 1000 * 60 unit="min"
        /// </summary>
        Tm_min,
        /// <summary>
        /// value=0 rate= 1 unit="min"
        /// </summary>
        Tm_minLarge,
        /// <summary> 
        /// value=00:00 rate= 1000 unit="min"
        /// </summary>
        Tm_minRD,
        /// <summary> 
        /// value=00:00 rate= 10 unit="min"
        /// </summary>
        Tm_minRDLarge,
        /// <summary> 
        /// value=0 rate= 1 unit="min"
        /// </summary>
        Tm_minDg,
        /// <summary> 
        /// value=0 rate=1 unit="ms"
        /// </summary>
        Tm_ms,
        /// <summary> 
        /// value=0 rate=1 unit="year"
        /// </summary>
        Tm_year,
        /// <summary> 
        /// value=0 rate=1 unit="month"
        /// </summary>
        Tm_month,
        /// <summary> 
        /// value=0 rate=1 unit="day"
        /// </summary>
        Tm_day,
        /// <summary> 
        /// value=0.0 rate=3600 unit="hour"
        /// </summary>
        Tm_hour,
        /// <summary> 
        /// value=0.0 rate=10 uni="%"
        /// </summary>
        Per,
        /// <summary> 
        /// value=0.0 rate=100 unit="ton"
        /// </summary>
        Force_ton,
        /// <summary> 
        /// value=0.0 rate= 10 unit="KN"
        /// </summary>
        Force_KN,
        /// <summary> 
        /// value=0.0 rate= 100.0/9.07 unit="US-ton"
        /// </summary>
        Force_USton,
        /// <summary> 
        /// value=0.0 rate=100 unit="ton"
        /// </summary>
        ForceTon,
        /// <summary> 
        /// value=0.0 rate= 10 unit="KN"
        /// </summary>
        ForceKN,
        /// <summary> 
        /// value=0.00 rate=100 unit="Nm"
        /// </summary>
        Torque,
        /// <summary> 
        /// value=0.00 rate=100 unit="kg"
        /// </summary>
        Weight_kg,
        /// <summary> 
        /// value=0.00 rate=100 unit="g"
        /// </summary>
        Weight_g,
        /// <summary>
        /// value=0 rate=1 unit="mm³"
        /// </summary>
        Volume_mm3,
        /// <summary> 
        /// value=0.00 rate=1000 unit="cm³"
        /// </summary>
        Volume_cm3,
        /// <summary> 
        /// value=0.00 rate=10000 unit"mm/s"
        /// </summary>
        Spd_mm,
        /// <summary>
        /// value=0.00 rate=10000 unit"mm/s"
        /// </summary>
        Spd_mm3,
        /// <summary> 
        /// value=0.000 rate= 10000 * 25.4 unit="inch/s"
        /// </summary>
        Spd_inch,
        /// <summary> 
        /// value=0.00 rate=10000 unit="mm/s²"
        /// </summary>
        Acc_mm,
        /// <summary> 
        /// value=0.000 rate= 10000 * 25.4 unit="inch/s²"
        /// </summary>
        Acc_inch,
        /// <summary> 
        /// value=0.0 rate=10 unit="%"
        /// </summary>
        Temp_C,
        /// <summary> 
        /// value=0.0  rate= 10.0 * 9 / 5  value = value / rate + 32 
        /// </summary>
        Temp_F,
        /// <summary> 
        /// value=0 rate=1 unit="rpm"
        /// </summary>
        RSpeed,
        /// <summary> 
        /// value=0 rate=1000 unit="°/s"
        /// </summary>
        DAcceleration,
        /// <summary> 
        /// value=0 rate=1000 unit="°"
        /// </summary>
        Degree,
        /// <summary> 
        /// value=0 rate=1000 unit="°/s"
        /// </summary>
        DegreeSpd,
        /// <summary> 
        /// value=0.00 rate=1000 unit="V"
        /// </summary>
        voltage,
        /// <summary> 
        /// value=0.000 rate=10000 unit="mm"
        /// </summary>
        LenInj_mm,
        /// <summary> 
        /// value=0.000 rate=10000 * 25.4 unit="inch"
        /// </summary>
        LenInj_inch,
        /// <summary> 
        /// value=0.000 rate=10000 unit="mm/s"
        /// </summary>
        SpdInj_mm,
        /// <summary> 
        /// value=0.000 rate=10000 * 25.4 unit="inch/s"
        /// </summary>
        SpdInj_inch,
        /// <summary> 
        /// value=0.000 rate=10000 unit="mm/s²"
        /// </summary>
        AccInj_mm,
        /// <summary> 
        /// value=0.000 rate=10000 * 25.4 unit="inch/s²"
        /// </summary>
        AccInj_inch,
        /// <summary> 
        /// value=0.0 rate=10 unit="I"
        /// </summary>
        Current,
        /// <summary> 
        /// value=0  rate=1000 * 60 unit="min"
        /// </summary>
        TmMinWr,
        /// <summary> 
        /// value= 0.00 rate=100 unit="w"
        /// </summary>
        Power,
        /// <summary> 
        /// value=0 rate=1 unit="wh"
        /// </summary>
        PowerHour,
        /// <summary> 
        /// value=0.0 rate=10 unit="kwh"
        /// </summary>
        PowerKHour
    }
    public enum plcLstSpd : byte
    {
        /// <summary>
        /// 10ms
        /// </summary>
        highSpdType = 10, 
        /// <summary>
        /// 20ms
        /// </summary>
        mapType = 20, 
        /// <summary>
        /// 100ms
        /// </summary>
        normalType = 100,
        /// <summary>
        /// 250ms
        /// </summary>
        lowSpdType = 250 
    }

    public class objUnit
    {
        /// <summary> 
        /// 999999999
        /// </summary>
        public const int VMAX = Int32.MaxValue;
        /// <summary> 
        /// -999999999
        /// </summary>
        public const int VMIN = Int32.MinValue;
        public const string unit_s = "s";
        public const string unit_min = "min";
        public const string unit_ms = "ms";
        public const string unit_year = "year";
        public const string unit_month = "month";
        public const string unit_day = "day";
        public const string unit_hour = "hour";
        public const string unit_mm = "mm";
        public const string unit_mm_s = "mm/s";
        public const string unit_mm_s2 = "mm/s²";
        public const string unit_inch = "inch";
        public const string unit_inch_s = "inch/s";
        public const string unit_inch_s2 = "inch/s²";
        public const string unit_C = "℃";
        public const string unit_F = "℉";
        public const string unit_Mpa = "Mpa";
        public const string unit_Bar = "Bar";
        public const string unit_PSI = "PSI";
        public const string unit_ton = "ton";
        public const string unit_KN = "KN";
        public const string unit_Us_ton = "US-ton";
        public const string unit_w = "w";
        public const string unit_Per = "%";
        public const string unit_kg = "kg";
        public const string unit_g = "g";
        public const string unit_mm3 = "mm³";
        public const string unit_cm3 = "cm³";
        public const string unit_rpm = "RPM";
        public const string unit_Degree_S = "°/s";
        public const string unit_Degree = "°";
        public const string unit_V = "V";
        public const string unit_I = "I";
        public const string unit_wh = "wh";
        public const string unit_kwh = "kwh";
        public const string unit_Nm = "Nm";

        public static Dictionary<UnitType, string> unitBase = new Dictionary<UnitType, string>()
        {
            {UnitType.DgtType,         "" },
            {UnitType.Len_mm,          unit_mm },
            {UnitType.Len_inch,        unit_inch },
            {UnitType.Prs_Mpa,         unit_Mpa },
            {UnitType.Prs_Bar,         unit_Bar },
            {UnitType.Prs_PSI,         unit_PSI },
            {UnitType.Tm_s,            unit_s },
            {UnitType.Tm_s3,           unit_s },
            {UnitType.Tm_min,          unit_min },
            {UnitType.Tm_minLarge,     unit_min },
            {UnitType.Tm_minRD,        "" },
            {UnitType.Tm_minRDLarge,   "" },
            {UnitType.Tm_minDg,        unit_min },
            {UnitType.Tm_ms,           unit_ms },
            {UnitType.Tm_year,         unit_year },
            {UnitType.Tm_month,        unit_month },
            {UnitType.Tm_day,          unit_day },
            {UnitType.Tm_hour,         unit_hour },
            {UnitType.Per,             unit_Per },
            {UnitType.Force_ton,       unit_ton },
            {UnitType.Force_KN,        unit_KN },
            {UnitType.Force_USton,     unit_Us_ton },
            {UnitType.ForceTon,        unit_ton },
            {UnitType.ForceKN,         unit_KN },
            {UnitType.Torque,          unit_Nm},
            {UnitType.Weight_kg,       unit_kg },
            {UnitType.Weight_g,        unit_g },
            {UnitType.Volume_mm3,      unit_mm3 },
            {UnitType.Volume_cm3,      unit_cm3 },
            {UnitType.Spd_mm,          unit_mm_s },
            {UnitType.Spd_mm3,         unit_mm_s },
            {UnitType.Spd_inch,        unit_inch_s },
            {UnitType.Acc_mm,          unit_mm_s2 },
            {UnitType.Acc_inch,        unit_inch_s2 },
            {UnitType.Temp_C,          unit_C },
            {UnitType.Temp_F,          unit_F },
            {UnitType.RSpeed,          unit_rpm },
            {UnitType.DAcceleration,   unit_Degree_S },
            {UnitType.Degree,          unit_Degree },
            {UnitType.DegreeSpd,       unit_Degree_S },
            {UnitType.voltage,         unit_V },
            {UnitType.LenInj_mm,       unit_mm },
            {UnitType.LenInj_inch,     unit_inch },
            {UnitType.SpdInj_mm,       unit_mm_s },
            {UnitType.SpdInj_inch,     unit_inch_s },
            {UnitType.AccInj_mm,       unit_mm_s2 },
            {UnitType.AccInj_inch,     unit_inch_s2 },
            {UnitType.Current,         unit_I },
            {UnitType.TmMinWr,         unit_min },
            {UnitType.Power,           unit_w },
            {UnitType.PowerKHour,      unit_kwh },
            {UnitType.PowerHour,       unit_wh }
        };
        public static IDictionary<UnitType, double> rate = new Dictionary<UnitType, double>()
        {
#region rate
            {UnitType.DgtType,                 1 },
            {UnitType.Len_mm,                   10000 },
            {UnitType.Len_inch,                 10000 * InchToMm },
            {UnitType.Spd_mm,                   10000 },
            {UnitType.Spd_mm3,                   10000 },
            {UnitType.Spd_inch,                 10000 * InchToMm },
            {UnitType.Acc_mm,                   10000 },
            {UnitType.Acc_inch,                 10000 * InchToMm },
            {UnitType.Prs_Mpa,                  10000 },
            {UnitType.Prs_Bar,                  1000 },
            {UnitType.Prs_PSI,                  10000 /145.0 },
            {UnitType.Tm_s,                     1000 },
            {UnitType.Tm_s3,                    1000 },
            {UnitType.Tm_min,                   1000 * 60 },
            {UnitType.Tm_minLarge,                   1 },
            {UnitType.Tm_minRD,                   1000  },
            {UnitType.Tm_minRDLarge,            10  },
            {UnitType.Tm_minDg,                   1  },
            {UnitType.Tm_ms,                    1 },
            {UnitType.Tm_year,                  1 },
            {UnitType.Tm_month,                 1 },
            {UnitType.Tm_day,                   1 },
            {UnitType.Tm_hour,                  3600 },
            {UnitType.Per,                      10 },
            {UnitType.Force_ton,                100 },
            {UnitType.Force_KN,                 10 },
            {UnitType.Force_USton,              100 / 9.07 },
            {UnitType.ForceTon,                 100 },
            {UnitType.ForceKN,                 10 },
            {UnitType.Torque,                   100},
            {UnitType.Weight_kg,                   100 },
            {UnitType.Weight_g,                   100 },
            {UnitType.Volume_mm3,                   1 },
            {UnitType.Volume_cm3,               1000 },
            {UnitType.Temp_C,                   10 },
            {UnitType.Temp_F,                   10.0 * 9 / 5 },
            {UnitType.RSpeed,                   1 },
            {UnitType.DAcceleration,            1000 },
            {UnitType.Degree,            1000 },
            {UnitType.DegreeSpd,            1000 },

            {UnitType.voltage,                  1000 },
            {UnitType.LenInj_mm,                10000 },
            {UnitType.LenInj_inch,              10000 * InchToMm },
            {UnitType.SpdInj_mm,                10000 },
            {UnitType.SpdInj_inch,              10000 * InchToMm },
            {UnitType.AccInj_mm,       10000 },
            {UnitType.AccInj_inch,     10000 * InchToMm},
            {UnitType.Current,                  1 },
            {UnitType.TmMinWr,                  1000 * 60 },
            {UnitType.Power,                    100 },
            {UnitType.PowerHour,                1 },
            {UnitType.PowerKHour,               10 }
#endregion
        };
        public static IDictionary<UnitType, string> strFm = new Dictionary<UnitType, string>()
        {
#region strFm
            {UnitType.DgtType,                 "0" },//1
            {UnitType.Len_mm,                   "0.00" },//0.1
            {UnitType.Len_inch,                 "0.000" },//0.1
            {UnitType.Prs_Mpa,                  "0.00" },//1
            {UnitType.Prs_Bar,                  "0.0" },//10
            {UnitType.Prs_PSI,                  "0" },//10
            {UnitType.Tm_s,                     "0.00" },//0.01
            {UnitType.Tm_s3,                     "0.000" },//0.01
            {UnitType.Tm_min,                   "0.0" },//1
            {UnitType.Tm_minLarge,              "0" },//1
            {UnitType.Tm_minRD,                 "00:00" },//1
            {UnitType.Tm_minRDLarge,            "00:00" },//1
            {UnitType.Tm_minDg,                 "0" },//1
            {UnitType.Tm_ms,                    "0" },//1
            {UnitType.Tm_year,                  "0" },//1
            {UnitType.Tm_month,                 "0" },//1
            {UnitType.Tm_day,                   "0" },//1
            {UnitType.Tm_hour,                  "0.0" },//1
            {UnitType.Per,                      "0.0" },//0.1
            {UnitType.Force_ton,                "0.0" },//0.1
            {UnitType.Force_KN,                 "0.0" },//1
            {UnitType.Force_USton,              "0.0" },//1/
            {UnitType.ForceTon,                 "0.0" },//0.1
            {UnitType.ForceKN,                  "0.0" },//1
            {UnitType.Torque,                   "0.00"},//0.1
            {UnitType.Weight_kg,                "0.00" },//0.1
            {UnitType.Weight_g,                 "0.00" },//1
            {UnitType.Volume_mm3,               "0" },//1
            {UnitType.Volume_cm3,               "0.00" },//0.01
            {UnitType.Spd_mm,                 "0.00" },//1
            {UnitType.Spd_mm3,                 "0.000" },//1
            {UnitType.Spd_inch,               "0.000" },//0.1
            {UnitType.Acc_mm,       "0.00" },//1
            {UnitType.Acc_inch,     "0.000"},//1
            {UnitType.Temp_C,                   "0.0" },//1
            {UnitType.Temp_F,                   "0" },//1
            {UnitType.RSpeed,                   "0" },//1
            {UnitType.DAcceleration,            "0" },//1
            {UnitType.Degree,            "0.00" },//0.01
            {UnitType.DegreeSpd,            "0.00" },//1
            {UnitType.voltage,                  "0.00" },//1
            {UnitType.LenInj_mm,                "0.000" },//0.01
            {UnitType.LenInj_inch,              "0.000" },//0.01
            {UnitType.SpdInj_mm,                "0.000" },//1
            {UnitType.SpdInj_inch,              "0.000" },//1
            {UnitType.AccInj_mm,       "0.000" },//1
            {UnitType.AccInj_inch,     "0.000"},//1
            {UnitType.Current,                  "0.0" },//1
            {UnitType.TmMinWr,                  "0" },//1
            {UnitType.Power,                    "0.00" },//1
            {UnitType.PowerHour,                "0" },//1
            {UnitType.PowerKHour,               "0.0" }//1
#endregion
        };

        /// <summary>
        /// dvBase 数据操作入口
        /// </summary>
        private static dvBase dvBase;
        public static dvBase dv_base
        {
            set
            {
                dvBase = value;
            }
        }
        public static int lstCountNr = 0;
        public int countNr;
        public const double InchToMm = 25.4;

        public static UnitType lenUnitBasic = UnitType.Len_mm;
        public static UnitType spdUnitBasic = UnitType.Spd_mm;
        public static UnitType accUnitBasic = UnitType.Acc_mm;
        public static UnitType lenInjUnitBasic = UnitType.LenInj_mm;
        public static UnitType spdInjUnitBasic = UnitType.SpdInj_mm;
        public static UnitType accInjUnitBasic = UnitType.AccInj_mm;
        public static UnitType tempUnitBasic = UnitType.Temp_C;
        public static UnitType forceUnitBasic = UnitType.Force_ton;
        public static UnitType prsUnitBasic = UnitType.Prs_Mpa;

        public objectType objType = objectType.nullType;
        public Lasal32.SRLVarInfo var;
        int value_vm;
        private string addrLocal_vm;
        public string addrLocal { get { return addrLocal_vm; } set { addrLocal_vm = value; } }
        public int addrPlc;
        public string className;
        public static getObjEvent getObjHandle;
        /// <summary>
        /// set时，不进行权限检查
        /// </summary>
        public int valueNew
        {
            get
            {
                getPlcValue();
                return value_vm;
            }
            set
            {
                setValueToPlc(value);
            }
        }
        public int value
        {
            get
            {
                return value_vm;
            }
            set
            {
                value_vm = value;
            }

        }
        public int lstValue;
        public int valueOld;

        string vMax_vm;
        string vMin_vm;
        public byte accessLevel;
        public string tag { get; set; }
        public string note;
        public string listObj;
        public string description;
        public string serialNum;
        public uint plcCBNum;
        /// <summary>
        /// 标记当前对象是否已经添加到回调list中
        /// </summary>
        public bool flagListAdded = false;
        //int curLstAddCode = 0;
        public plcLstSpd lstSpdType = plcLstSpd.normalType;
        public string[] bitSers = new string[32];

        public objUnit()
        {
            this.serialNum = "Itl000";
            objType = objectType.ItlPr;
            this.vMax_vm = "+";
            this.vMin_vm = "_";
            this.unitType = UnitType.DgtType;
        }
        public objUnit(string serialNum)
        {
            this.serialNum = serialNum;
            objType = objectType.ItlPr;
            this.vMax_vm = "+";
            this.vMin_vm = "_";
            this.unitType = UnitType.DgtType;
        }
        int bit = -1;

        public objUnit(string serialNum, UnitType type, string min, string max, string state, string tag, string addr)
        {
            try
            {
                this.serialNum = serialNum;

                switch (serialNum.Substring(0, 3))
                {
                    case "Ipr":
                        objType = objectType.IprPr;
                        break;
                    case "Sys":
                        {
                            objType = objectType.SysPr;
                        }
                        break;
                    case "Mld":
                        {
                            objType = objectType.MldPr;
                        }
                        break;
                    case "Inj":
                        {
                            objType = objectType.InjPr;
                        }
                        break;
                    case "Tmp":
                        {
                            objType = objectType.TmpPr;
                        }
                        break;
                    case "Prd":
                        {
                            objType = objectType.PrdPr;
                        }

                        break;
                    case "DTS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DTF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DIS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DIF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DOS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DOF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "AIO":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    default:
                        {
                            //vm.perror("[objUnit] new Type : " + serialNum);
                        }
                        break;

                }

                this.unitType = type;
                this.vMax_vm = max;
                this.vMin_vm = min;
                this.tag = tag;

                string[] strTmp = addr.Split('.');

            }
            catch (Exception ex)
            {
                vm.perror("[objUnit_init] : " + serialNum + " " + ex.ToString());
            }
        }
        public objUnit(string serialNum, UnitType type, string min, string max, string addr)
        {
            try
            {
                this.serialNum = serialNum;

                switch (serialNum.Substring(0, 3))
                {
                    case "Ipr":
                        objType = objectType.IprPr;
                        break;
                    case "Sys":
                        {
                            objType = objectType.SysPr;
                        }
                        break;
                    case "Mld":
                        {
                            objType = objectType.MldPr;
                        }
                        break;
                    case "Inj":
                        {
                            objType = objectType.InjPr;
                        }
                        break;
                    case "Tmp":
                        {
                            objType = objectType.TmpPr;
                        }
                        break;
                    case "Prd":
                        {
                            objType = objectType.PrdPr;
                        }

                        break;
                    case "DTS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DTF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DIS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DIF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DOS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DOF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "AIO":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    default:
                        {
                            //vm.perror("[objUnit] new Type : " + serialNum);
                        }
                        break;
                }

                this.unitType = type;
                this.vMax_vm = max;
                this.vMin_vm = min;
                this.tag = tag;
                this.addrLocal = addr;
                string[] strTmp = addr.Split('.');
                this.className = strTmp[0];

            }
            catch (Exception ex)
            {
                vm.perror("[objUnit_init] : " + serialNum + " " + ex.ToString());
            }
        }
        public objUnit(string serialNum, UnitType type, string min, string max, string addr, byte accessLevel)
        {
            try
            {
                this.serialNum = serialNum;

                switch (serialNum.Substring(0, 3))
                {
                    case "Ipr":
                        objType = objectType.IprPr;
                        break;
                    case "Sys":
                        {
                            objType = objectType.SysPr;
                        }
                        break;
                    case "Mld":
                        {
                            objType = objectType.MldPr;
                        }
                        break;
                    case "Inj":
                        {
                            objType = objectType.InjPr;
                        }
                        break;
                    case "Tmp":
                        {
                            objType = objectType.TmpPr;
                        }
                        break;
                    case "Prd":
                        {
                            objType = objectType.PrdPr;
                        }

                        break;
                    case "DTS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DTF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DIS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DIF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DOS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DOF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "AIO":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    default:
                        {
                            //vm.perror("[objUnit] new Type : " + serialNum);
                        }
                        break;
                }

                this.unitType = type;
                this.vMax_vm = max;
                this.vMin_vm = min;
                this.tag = tag;
                this.addrLocal = addr;
                string[] strTmp = addr.Split('.');
                this.className = strTmp[0];
                this.accessLevel = accessLevel;

            }
            catch (Exception ex)
            {
                vm.perror("[objUnit_init] : " + serialNum + " " + ex.ToString());
            }
        }

        public objUnit(string serialNum, UnitType type)
        {
            try
            {
                this.serialNum = serialNum;
                switch (serialNum.Substring(0, 3))
                {
                    case "Ipr":
                        objType = objectType.IprPr;
                        break;
                    case "Sys":
                        {
                            objType = objectType.SysPr;
                        }
                        break;
                    case "Mld":
                        {
                            objType = objectType.MldPr;
                        }
                        break;
                    case "Inj":
                        {
                            objType = objectType.InjPr;
                        }
                        break;
                    case "Tmp":
                        {
                            objType = objectType.TmpPr;
                        }
                        break;
                    case "Prd":
                        {
                            objType = objectType.PrdPr;
                        }
                        break;
                    case "DTS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DTF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DIS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DIF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DOS":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "DOF":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "AIO":
                        {
                            objType = objectType.IOTPr;
                        }
                        break;
                    case "Itl":
                        {
                            objType = objectType.ItlPr;
                        }
                        break;
                    default:
                        {
                            //vm.perror("[objUnit] new Type : " + serialNum);
                        }
                        break;
                }
                this.unitType = type;
                this.vMax_vm = "+";
                this.vMin_vm = "_";
                this.tag = tag;
            }
            catch (Exception ex)
            {
                vm.perror("[objUnit_init] : " + serialNum + " " + ex.ToString());
            }
        }
        public objUnit(string serialNum, string addr)
        {
            try
            {
                this.serialNum = serialNum;
                string[] strBit = addr.Split('[');
                if (strBit.Length == 2)
                {
                    string[] strBit1 = strBit[1].Split(']');
                    if (strBit1.Length == 2)
                    {
                        bit = Int32.Parse(strBit1[0]);
                        this.addrLocal_vm = strBit[0];
                    }
                }
                else
                    this.addrLocal_vm = addr;
                string[] strTmp = addr.Split('.');
                this.className = strTmp[0];
                switch (serialNum.Substring(0, 3))
                {
                    case "Abt":
                        {
                            objType = objectType.AlmPr;
                        }
                        break;
                    case "Key":
                        {
                            objType = objectType.KeyPr;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                vm.perror("[objUnit_init] : " + serialNum + " " + ex.ToString());
            }
        }
        public objUnit(string serialNum, UnitType type, string addr)
        {
            try
            {
                this.serialNum = serialNum;
                this.unitType = type;
                this.addrLocal_vm = addr;
                string[] strTmp = addr.Split('.');
                this.className = strTmp[0];
            }
            catch (Exception ex)
            {
                vm.perror("[objUnit_init] : " + serialNum + " " + ex.ToString());
            }
        }
        public objUnit(string serialNum, objectType type, string addr)
        {
            try
            {
                this.serialNum = serialNum;
                this.objType = type;
                this.addrLocal_vm = addr;
                string[] strTmp = addr.Split('.');
                this.className = strTmp[0];
            }
            catch (Exception ex)
            {
                vm.perror("[objUnit_init] : " + serialNum + " " + ex.ToString());
            }
        }
        public objUnit(string serialNum, objectType type, UnitType unitType, string addr)
        {
            try
            {
                this.serialNum = serialNum;
                objType = type;
                this.unitType = unitType;
                this.addrLocal_vm = addr;
                string[] strTmp = addr.Split('.');
                //this.className = strTmp[0];
            }
            catch (Exception ex)
            {
                vm.perror("[objUnit_init] : " + serialNum + " " + ex.ToString());
            }
        }
        public objUnit(string serialNum, string varAddr, string localAddr) //heart
        {
            this.serialNum = serialNum;
            //this.varAddr = varAddr;
            this.addrLocal = localAddr;
            string[] strTmp = varAddr.Split('.');
        }

        public void addBitSer(int num, string ser)
        {
            if(num >= 0 && num<32)
            {
                bitSers[num] = ser;
            }
        }
        /// <summary>
        /// 保留到小数点后0位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double getFmDot0RoundNum(double value)
        {
            if (value >= 0)
                return (int)(value * 10 + 5) / 10;
            else
                return (int)(value * 10 - 5) / 10;
        }
        /// <summary>
        /// 保留到小数点后1位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double getFmDot1RoundNum(double value)
        {
            if (value >= 0)
                return ((int)(value * 100 + 5) / 10) / 10.0;
            else
                return ((int)(value * 100 - 5) / 10) / 10.0;
        }
        /// <summary>
        /// 保留到小数点后2位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double getFmDot2RoundNum(double value)
        {
            if (value >= 0)
                return ((int)(value * 1000 + 5) / 10) / 100.0;
            else
                return ((int)(value * 1000 - 5) / 10) / 100.0;
        }
        /// <summary>
        /// 保留到小数点后3位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double getFmDot3RoundNum(double value)
        {
            if (value >= 0)
                return ((int)(value * 10000 + 5) / 10) / 1000.0;
            else
                return ((int)(value * 10000 - 5) / 10) / 1000.0;
        }

        public bool setValue(int value)
        {
            //if (!valmoWin.dv.checkAccesslevel(accessLevel))
            //    return false;
            return setValueToPlc(value);
        }
        public bool setValue(double value)
        {
            //if (!valmoWin.dv.checkAccesslevel(accessLevel))
                //return false;
            return setValueToPlc(value);
        }
        public bool setValueToPlc(int value)
        {
            bool b = false;
            if (objType == objectType.ItlPr)
            {
                if (value >= VMAX)
                    value = VMAX;
                else if (value <= VMIN)
                    value = VMIN;
                
                this.value_vm = value;
                b = true;
            }
            else// if (link.checkLinkPlc())
            {

                b = dvBase.setValueHandle(addrPlc, value);
                //Lasal32.LslWriteToSvr(addrPlc, value);
                //b = true;
            }
            if (objType == objectType.KeyPr)
            {
                if (value == 1)
                {
                    vm.print(serialNum + " ↓ : \t" + (b ? "√\t" : "×\t"));
                }
                else if (value == 0)
                {
                    vm.printLn(serialNum + "\t ↑ : " + (b ? "√\t" : "×\t"));
                }
            }

            if (objType == objectType.IOTState || objType == objectType.IOTForce)
            {
                vm.printLn(serialNum + " : " + addrLocal + " ↓ : " + value + "\t" + (b ? "√" : "×"));
            }
            return b;
        }
        public bool setValueToPlc(double value)
        {
            double plcValue = 0;
            switch (strFm[unitType])
            {
                case "0":
                    plcValue = rate[unitType] * getFmDot0RoundNum(value);
                    break;
                case "0.0":
                    plcValue = rate[unitType] * getFmDot1RoundNum(value);
                    break;
                case "0.00":
                    plcValue = rate[unitType] * getFmDot2RoundNum(value);
                    break;
                case "0.000":
                    plcValue = rate[unitType] * getFmDot3RoundNum(value);
                    break;
            }
            return setValueToPlc((int)plcValue);
        }
        private bool setValueToLocal(double value)
        {
            double plcValue = 0;
            switch (strFm[unitType])
            {
                case "0":
                    plcValue = rate[unitType] * getFmDot0RoundNum(value);
                    break;
                case "0.0":
                    plcValue = rate[unitType] * getFmDot1RoundNum(value);
                    break;
                case "0.00":
                    plcValue = rate[unitType] * getFmDot2RoundNum(value);
                    break;
                case "0.000":
                    plcValue = rate[unitType] * getFmDot3RoundNum(value);
                    break;
            }
            if (plcValue >= VMAX)
                plcValue = VMAX;
            else if (plcValue <= VMIN)
                plcValue = VMIN;
            this.value_vm = (int)plcValue;
            return true;
        }

        public string getStrValue(int value)
        {
            return getStrValue(value, unitType);
        }
        public string getStrValue(double retDbl)
        {
            return retDbl.ToString(strFm[unitType]);
        }
        public string getStrValue(UnitType type)
        {
            return getStrValue(value_vm, type);
        }

        public double getPrecision()
        {
            switch (unitType)
            {
                case UnitType.Len_mm:
                case UnitType.Len_inch:
                case UnitType.Per:
                case UnitType.Force_ton:
                case UnitType.ForceTon:
                case UnitType.Torque:
                case UnitType.Weight_kg:
                case UnitType.Spd_inch:
                    return 0.1;
                case UnitType.Tm_s:
                case UnitType.Tm_s3:
                case UnitType.Volume_cm3:
                case UnitType.Degree:
                case UnitType.LenInj_mm:
                case UnitType.LenInj_inch:
                    return 0.01;
                case UnitType.Prs_Bar:
                case UnitType.Prs_PSI:
                    return 10;
                default:
                    return 1;
            }
        }

        public static string getStrValue(UnitType type, int plcValue)
        {
            try
            {
                double value = getDblValue(type, plcValue);
                if (type == UnitType.Tm_minRD || type== UnitType.Tm_minRDLarge)
                {
                    if (value > 3600)
                        return ((value % 86400) / 3600).ToString("00") + ":" + ((value % 3600) / 60).ToString("00") + ":" + (value % 60).ToString("00");
                    else
                        return ((int)value / 60).ToString("00") + ":" + (value % 60).ToString("00");
                }
                else
                    return value.ToString(strFm[type]);
            }
            catch (System.Exception ex)
            {
                vm.perror("[objUnit.getStrValue]" + ex.ToString());
                return "null";
            }
        }
        public static string getStrValue(int value, UnitType type)
        {
            return getStrValue(type, value);
        }

        public bool getPlcValue()
        {
            return dvBase.getValueHandle(this.addrPlc, ref value_vm);
        }
        public double getDblValue(int value)
        {
            return getDblValue(unitType, value);
        }
        public double getDblValue(UnitType type)
        {
            return getDblValue(type, value_vm);
        }

        public static double getDblValue(UnitType type, int value)
        {
            //return vIntPlcToDblType(value, type);
            try
            {
                double retDbl = 0;
                if (type == UnitType.Temp_F)
                {
                    retDbl = value / rate[type] + 32;
                }
                else
                {
                    retDbl = value / rate[type];
                }
                return retDbl;
            }
            catch (System.Exception ex)
            {
                vm.perror("[objUnit.getDblValue]" + ex.ToString());
                return -1;
            }
        }
        public static double getDblValue(int value, UnitType type)
        {
            return getDblValue(type, value);
        }

        public bool flagInerval
        {
            get
            {
                return objType == objectType.ItlPr;
            }
        }
        private bool setMinDblType(double value, UnitType type)
        {
            double plcValue = 0;
            switch (strFm[type])
            {
                case "0":
                    plcValue = rate[type] * getFmDot0RoundNum(value);
                    break;
                case "0.0":
                    plcValue = rate[type] * getFmDot1RoundNum(value);
                    break;
                case "0.00":
                    plcValue = rate[type] * getFmDot2RoundNum(value);
                    break;
                case "0.000":
                    plcValue = rate[type] * getFmDot3RoundNum(value);
                    break;
            }
            if (plcValue <= VMIN)
                plcValue = VMIN;
            else if (plcValue >= VMAX)
                plcValue = VMAX;
            this.vMin_vm = plcValue.ToString();
            return true;
        }
        private bool setMaxDblType(double value, UnitType type)
        {
            double plcValue = 0;
            switch (strFm[type])
            {
                case "0":
                    plcValue = rate[type] * getFmDot0RoundNum(value);
                    break;
                case "0.0":
                    plcValue = rate[type] * getFmDot1RoundNum(value);
                    break;
                case "0.00":
                    plcValue = rate[type] * getFmDot2RoundNum(value);
                    break;
                case "0.000":
                    plcValue = rate[type] * getFmDot3RoundNum(value);
                    break;
            }
            this.vMax_vm = plcValue.ToString();
            return true;
        }

        public double vMinDbl
        {
            get
            {
                double retValue = 0;
                if (this.vMin_vm != null)
                {

                    if (vMin_vm[0] >= '0' && vMin_vm[0] < '9' || vMin_vm[0] == '-')
                    {
                        int valueMin = Int32.Parse(vMin_vm);
                        return getDblValue(unitType, valueMin);
                    }
                    else
                    {
                        if (vMin_vm == "_" || vMin_vm == "noMin")
                            retValue = getDblValue(unitType, VMIN);
                        else if (vMin_vm.Length == 6)
                        {
                            string strTmpName = vMin_vm.Substring(0, 3);
                            string strTmpValue = vMin_vm.Substring(3, 3);
                            bool isNum = true;
                            for (int i = 3; i < 6; i++)
                            {
                                if (vMin_vm[i] < '0' || vMin_vm[i] > '9')
                                    isNum = false;
                            }
                            if (isNum)
                            {
                                if (getObjHandle != null)
                                {
                                    objUnit obj = getObjHandle(serialNum);
                                    retValue = obj.vDblNew;
                                }
                                switch (strTmpName)
                                {
                                    case "Mld":
                                        retValue = dvBase.MldPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                    case "Inj":
                                        retValue = dvBase.InjPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                    case "Tmp":
                                        retValue = dvBase.TmpPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                    case "Ipr":
                                        retValue = dvBase.IprPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                    case "Prd":
                                        retValue = dvBase.PrdPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;

                                }
                            }
                        }
                    }
                }
                else
                    retValue = VMIN;
                return retValue;
            }

        }
        public double vMinDblNew
        {
            get
            {
                return vMinDbl;
            }
            set
            {
                setMinDblType(value, unitType);
            }
        }
        public double vMaxDblNew
        {
            get
            {
                return vMaxDbl;
            }
            set
            {
                if (value >= VMAX)
                    value = VMAX;
                setMaxDblType(value, unitType);
            }
        }
        public string vMinStr
        {
            get
            {
                string retValue = "0";
                if (this.vMin_vm != null)
                {

                    if (vMin_vm[0] >= '0' && vMin_vm[0] < '9' || vMin_vm[0] == '-')
                    {
                        int valueMin = Int32.Parse(vMin_vm);
                        return getStrValue(valueMin, unitType);
                    }
                    else
                    {
                        if (vMin_vm == "_" || vMin_vm == "noMin")
                            retValue = getStrValue(VMIN, unitType);
                        else
                            if (vMin_vm.Length == 6)
                            {
                                string strTmpName = vMin_vm.Substring(0, 3);
                                string strTmpValue = vMin_vm.Substring(3, 3);
                                bool isNum = true;
                                for (int i = 3; i < 6; i++)
                                {
                                    if (vMin_vm[i] < '0' || vMin_vm[i] > '9')
                                        isNum = false;
                                }
                                if (isNum)
                                {
                                    if (getObjHandle != null)
                                    {
                                        objUnit obj = getObjHandle(serialNum);
                                        retValue = obj.vDblStrNew;
                                    }
                                    switch (strTmpName)
                                    {
                                        case "Mld":
                                            retValue = dvBase.MldPr[Int32.Parse(strTmpValue)].vDblStrNew;
                                            break;
                                        case "Inj":
                                            retValue = dvBase.InjPr[Int32.Parse(strTmpValue)].vDblStrNew;
                                            break;
                                        case "Tmp":
                                            retValue = dvBase.TmpPr[Int32.Parse(strTmpValue)].vDblStrNew;
                                            break;
                                        case "Ipr":
                                            retValue = dvBase.IprPr[Int32.Parse(strTmpValue)].vDblStrNew;
                                            break;
                                    }
                                }
                            }
                    }
                }
                else
                    retValue = VMIN.ToString();
                return retValue;
            }

        }
        public string vMinStrNew
        {
            set
            {
                if (objType == objectType.ItlPr)
                {
                    if (value == "_" || value == "noMin")
                    {
                        this.vMin_vm = value;
                    }
                    else
                    {
                        throw (new Exception("set vMinStrNew value error!"));
                    }
                }
            }
        }
        public double vMaxDbl
        {
            get
            {
                double retValue = 0;
                if (this.vMax_vm != null)
                {

                    if (vMax_vm[0] >= '0' && vMax_vm[0] <= '9' || vMax_vm[0] == '-')
                    {
                        int valueMax = Int32.Parse(vMax_vm);
                        return getDblValue(valueMax, unitType);
                    }
                    else
                    {
                        if (vMax_vm == "+" || vMax_vm == "noMax")
                            retValue = getDblValue(VMAX, unitType);
                        if (vMax_vm.Length == 6)
                        {
                            string strTmpName = vMax_vm.Substring(0, 3);
                            string strTmpValue = vMax_vm.Substring(3, 3);
                            bool isNum = true;
                            for (int i = 3; i < 6; i++)
                            {
                                if (vMax_vm[i] < '0' || vMax_vm[i] > '9')
                                    isNum = false;
                            }
                            if (isNum)
                            {
                                if (getObjHandle != null)
                                {
                                    objUnit obj = getObjHandle(serialNum);
                                    retValue = obj.vDblNew;
                                }
                                switch (strTmpName)
                                {
                                    case "Mld":
                                        retValue = dvBase.MldPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                    case "Inj":
                                        retValue = dvBase.InjPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                    case "Tmp":
                                        retValue = dvBase.TmpPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                    case "Ipr":
                                        retValue = dvBase.IprPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                    case "Prd":
                                        retValue = dvBase.PrdPr[Int32.Parse(strTmpValue)].vDblNew;
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                    retValue = VMAX;
                return retValue;
            }
            //switch(vMax_vm[0])
        }
        public string vMaxStr
        {
            get
            {
                string retValue = "0";
                if (this.vMax_vm != null)
                {
                    if (vMax_vm[0] >= '0' && vMax_vm[0] <= '9' || vMax_vm[0] == '-')
                    {
                        int valueMax = Int32.Parse(vMax_vm);
                        return getStrValue(valueMax, unitType);
                    }
                    else
                    {

                        if (vMax_vm == "+" || vMax_vm == "noMax")
                            retValue = getStrValue(VMAX, unitType);
                        else
                            if (vMax_vm.Length == 6)
                            {
                                string strTmpName = vMax_vm.Substring(0, 3);
                                string strTmpValue = vMax_vm.Substring(3, 3);
                                bool isNum = true;
                                for (int i = 3; i < 6; i++)
                                {
                                    if (vMax_vm[i] < '0' || vMax_vm[i] > '9')
                                        isNum = false;
                                }
                                if (isNum)
                                {
                                    if (getObjHandle != null)
                                    {
                                        objUnit obj = getObjHandle(serialNum);
                                        retValue = obj.vDblStrNew;
                                    }
                                    switch (strTmpName)
                                    {
                                        case "Mld":
                                            retValue = dvBase.MldPr[Int32.Parse(strTmpValue)].vDblStrNew;
                                            break;
                                        case "Inj":
                                            retValue = dvBase.InjPr[Int32.Parse(strTmpValue)].vDblStrNew;
                                            break;
                                        case "Tmp":
                                            retValue = dvBase.TmpPr[Int32.Parse(strTmpValue)].vDblStrNew;
                                            break;
                                        case "Ipr":
                                            retValue = dvBase.IprPr[Int32.Parse(strTmpValue)].vDblStrNew;
                                            break;
                                    }
                                }
                            }
                    }
                }
                else
                    retValue = VMAX.ToString();
                return retValue;
            }
        }
        public string vMaxStrNew
        {
            set
            {
                if (objType == objectType.ItlPr)
                {
                    if (value == "+" || value == "noMax")
                    {
                        this.vMin_vm = value;
                    }
                    else
                    {
                        throw (new Exception("set vMaxStrNew value error!"));
                    }
                }
            }
        }
        public double vDblNew
        {
            get
            {
                if (objType != objectType.ItlPr)
                {
                    if (!dvBase.getValueHandle(this.addrPlc, ref value_vm))
                    {
                        //link.checkLinkPlc();
                        //if (!Lasal32.LslReadFromSvr(this.addrPlc, ref value_vm))
                            return 0;
                    }
                }
                return vDbl;
            }
            set
            {
                setValueToPlc(value);
            }
        }

        public double vDbl
        {
            get
            {
                return getDblValue(value_vm, unitType);
            }
            set
            {
                setValueToLocal(value);
            }
        }
        public string vDblStrNew
        {
            get
            {
                if (objType == objectType.ItlPr)
                    return vDblStr;
                if (!dvBase.getValueHandle(this.addrPlc, ref value_vm))
                {
                    //if (!link.isOnLine())
                    //{
                    //    link.checkLinkPlc();
                    //    if (!Lasal32.LslReadFromSvr(this.addrPlc, ref value_vm))
                    //        return null;
                    //}
                }
                return vDblStr;
            }
        }
        public string vDblStr
        {
            get
            {
                return getStrValue(value_vm, unitType);
            }
        }

        public int num
        {
            get;
            set;
        }
        private UnitType _unitType = UnitType.DgtType;
        public UnitType unitType
        {
            get
            {
                if (_unitType == UnitType.Len_mm || _unitType == UnitType.Len_inch)
                    return lenUnitBasic;
                else if (_unitType == UnitType.Spd_mm || _unitType == UnitType.Spd_inch)
                    return spdUnitBasic;
                else if (_unitType == UnitType.Acc_mm || _unitType == UnitType.Acc_inch)
                    return accUnitBasic;
                else if (_unitType == UnitType.LenInj_mm || _unitType == UnitType.LenInj_inch)
                    return lenInjUnitBasic;
                else if (_unitType == UnitType.SpdInj_mm || _unitType == UnitType.SpdInj_inch)
                    return spdInjUnitBasic;
                else if (_unitType == UnitType.AccInj_mm || _unitType == UnitType.AccInj_inch)
                    return accInjUnitBasic;
                else if (_unitType == UnitType.Temp_C || _unitType == UnitType.Temp_F)
                    return tempUnitBasic;
                else if (_unitType == UnitType.Prs_Mpa || _unitType == UnitType.Prs_Bar || _unitType == UnitType.Prs_PSI)
                    return prsUnitBasic;
                else if (_unitType == UnitType.Force_ton || _unitType == UnitType.Force_KN || _unitType == UnitType.Force_USton)
                    return forceUnitBasic;
                else
                    return _unitType;

            }
            set
            {
                _unitType = value;
            }
        }
        public string unit
        {
            get
            {
                return unitBase[unitType];
            }
        }

        //int nullObjNum = 0;
        public static int count = 0;
        public static int errNum = 0;
        public static int errMap = 0;
        public static int countNormal = 0;
        public static int countMap = 0;
        public void copyFrom(objUnit obj)
        {
            this.serialNum = obj.serialNum;
            this.vMax_vm = obj.vMax_vm;
            this.vMin_vm = obj.vMin_vm;
            this.value = obj.value;
        }
        public void setLstSpd(plcLstSpd spd)
        {
            lstSpdType = spd;
        }
        /// <summary>
        /// 将当前对象添加到回调list中
        /// </summary>
        /// <param name="lstSpdType"></param>
        public void add(plcLstSpd lstSpdType = plcLstSpd.normalType)
        {
            if (lstSpdType < plcLstSpd.normalType)
                this.lstSpdType = lstSpdType;
            flagListAdded = true;
        }
        public void addMap()
        {
            this.lstSpdType = plcLstSpd.mapType;
            flagListAdded = true;
        }
        public List<Label> lbGrp = new List<Label>();
        public List<callBackObjEvent> handleGrp = new List<callBackObjEvent>();
        public void addLb(Label lb, plcLstSpd lstSpdType = plcLstSpd.normalType)
        {
            if (lstSpdType < plcLstSpd.normalType)
                this.lstSpdType = lstSpdType;
            lbGrp.Add(lb);
            flagListAdded = true;
        }
        public void addHandle(callBackObjEvent handle, plcLstSpd lstSpdType = plcLstSpd.normalType)
        {
            if (lstSpdType < plcLstSpd.normalType)
                this.lstSpdType = lstSpdType;
            handleGrp.Add(handle);
            flagListAdded = true;
        }
        public void addLstHandle(callBackObjEvent handle, plcLstSpd lstSpdType = plcLstSpd.normalType)
        {
            if (lstSpdType < plcLstSpd.normalType)
                this.lstSpdType = lstSpdType;
            handleGrp.Add(handle);
            flagListAdded = true;
        }
         
        public void refresh(int value)
        {
            countNr = lstCountNr++;
            valueOld = value_vm;
            value_vm = value;
            lstValue = value;
            dvBase.lstFeedbackHandle(this);
        }
        public void refresh()
        {
            //if (lbGrp.Count != 0 || handleGrp.Count != 0)
                dvBase.lstFeedbackHandle(this);
        }
        /// <summary>
        /// handleGrp.Clear();
        /// lbGrp.Clear();
        /// </summary>
        public void clear()
        {
            handleGrp.Clear();
            lbGrp.Clear();
        }
        public static string getUnit(UnitType unitType)
        {
            return unitBase[unitType];
        }

        public void copyTo(ref objUnit destObj)
        {
            destObj.serialNum = this.serialNum;
            destObj.unitType = this.unitType;
            destObj.value = this.value;
            destObj.vMax_vm = this.vMax_vm;
            destObj.vMin_vm = this.vMin_vm;
        }
        public void copyAllTo(ref objUnit destObj)
        {
            destObj.serialNum = this.serialNum;
            destObj.unitType = this.unitType;
            destObj.value = this.value;
            destObj.vMax_vm = this.vMax_vm;
            destObj.vMin_vm = this.vMin_vm;
        }
        public static int setUnitTo(string newUnitName)
        {
            switch (newUnitName)
            {
                case "mm":
                    {
                        lenUnitBasic = UnitType.Len_mm;
                        spdUnitBasic = UnitType.Spd_mm;
                        accUnitBasic = UnitType.Acc_mm;
                        lenInjUnitBasic = UnitType.LenInj_mm;
                        spdInjUnitBasic = UnitType.SpdInj_mm;
                        accInjUnitBasic = UnitType.AccInj_mm;
                    }
                    break;
                case "inch":
                    {
                        lenUnitBasic = UnitType.Len_inch;
                        spdUnitBasic = UnitType.Spd_inch;
                        accUnitBasic = UnitType.Acc_inch;
                        lenInjUnitBasic = UnitType.LenInj_inch;
                        spdInjUnitBasic = UnitType.SpdInj_inch;
                        accInjUnitBasic = UnitType.AccInj_inch;
                    }
                    break;
                case unit_C:
                    {
                        tempUnitBasic = UnitType.Temp_C;
                    }
                    break;
                case unit_F:
                    {
                        tempUnitBasic = UnitType.Temp_F;
                    }
                    break;
                case unit_Mpa:
                    {
                        prsUnitBasic = UnitType.Prs_Mpa;
                    }
                    break;
                case "Bar":
                    {
                        prsUnitBasic = UnitType.Prs_Bar;
                    }
                    break;
                case "PSI":
                    {
                        prsUnitBasic = UnitType.Prs_PSI;
                    }
                    break;
                case "ton":
                    {
                        forceUnitBasic = UnitType.Force_ton;
                    }
                    break;
                case "KN":
                    {
                        forceUnitBasic = UnitType.Force_KN;
                    }
                    break;
                case "US-ton":
                    {
                        forceUnitBasic = UnitType.Force_USton;
                    }
                    break;

            }
            int unitValue = 0;
            if (lenUnitBasic == UnitType.Len_mm)
            {
                unitValue |= 0x00;
            }
            else
            {
                unitValue |= 0x01;
            }
            if (tempUnitBasic == UnitType.Temp_C)
            {
                unitValue |= 0x00;
            }
            else
            {
                unitValue |= 0x10;
            }
            if (prsUnitBasic == UnitType.Prs_Mpa)
            {
                unitValue |= 0x0000;
            }
            else if (prsUnitBasic == UnitType.Prs_Bar)
            {
                unitValue |= 0x0100;
            }
            else if (prsUnitBasic == UnitType.Prs_PSI)
            {
                unitValue |= 0x0200;
            }
            if (forceUnitBasic == UnitType.Force_ton)
            {
                unitValue |= 0x0000;
            }
            else if (forceUnitBasic == UnitType.Force_KN)
            {
                unitValue |= 0x1000;
            }
            else if (forceUnitBasic == UnitType.Force_USton)
            {
                unitValue |= 0x2000;
            }
            //Properties.Settings.Default.units = unitValue;
            return unitValue;

        }
        public static void unitInit(int units)
        {

            int unitValue = units;// Properties.Settings.Default.units;
            if ((unitValue & 0x0f) == 0)
            {
                lenUnitBasic = UnitType.Len_mm;
                spdUnitBasic = UnitType.Spd_mm;
                accUnitBasic = UnitType.Acc_mm;
                lenInjUnitBasic = UnitType.LenInj_mm;
                spdInjUnitBasic = UnitType.SpdInj_mm;
                accInjUnitBasic = UnitType.AccInj_mm;
            }
            else
            {
                lenUnitBasic = UnitType.Len_inch;
                spdUnitBasic = UnitType.Spd_inch;
                accUnitBasic = UnitType.Acc_inch;
                lenInjUnitBasic = UnitType.LenInj_inch;
                spdInjUnitBasic = UnitType.SpdInj_inch;
                accInjUnitBasic = UnitType.AccInj_inch;
            }

            if (((unitValue >> 4) & 0x0f) == 0)
            {
                tempUnitBasic = UnitType.Temp_C;
            }
            else if (((unitValue >> 4) & 0x0f) == 1)
            {
                tempUnitBasic = UnitType.Temp_F;
            }

            if (((unitValue >> 8) & 0x0f) == 0)
            {
                prsUnitBasic = UnitType.Prs_Mpa;
            }
            else if (((unitValue >> 8) & 0x0f) == 1)
            {
                prsUnitBasic = UnitType.Prs_Bar;
            }
            else if (((unitValue >> 8) & 0x0f) == 2)
            {
                prsUnitBasic = UnitType.Prs_PSI;
            }

            if (((unitValue >> 12) & 0x0f) == 0)
            {
                forceUnitBasic = UnitType.Force_ton;
            }
            else if (((unitValue >> 12) & 0x0f) == 1)
            {
                forceUnitBasic = UnitType.Force_KN;
            }
            else if (((unitValue >> 12) & 0x0f) == 2)
            {
                forceUnitBasic = UnitType.Force_USton;
            }
        }

    }

    public class callbackGrp
    {
        public callBackDebugEvent handle;
        public Label lb;

        public callbackGrp(callBackDebugEvent handle, Label lb)
        {
            this.handle = handle;
            this.lb = lb;
        }
    }
}
