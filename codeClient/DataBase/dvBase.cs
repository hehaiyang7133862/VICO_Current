using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;

namespace nsDataMgr
{
    public class dvBase
    {
        public delegate void lstFeedbackEvent(List<Label> lbGrp, List<callBackObjEvent> handleGrp, objUnit obj);
        public static setValueEvent setValueHandle = Lasal32.LslWriteToSvr;
        public static getValueEvent getValueHandle = Lasal32.LslReadFromSvr;
        //nullEvent checkLinkHandle ;
        public static callBackObjEvent lstFeedbackHandle;

        public  dvClass AlmPr = new dvClass();
        public  dvClass KeyPr = new dvClass();
        public  dvClass IprPr = new dvClass();
        public  dvClass SysPr = new dvClass();
        public  dvClass MldPr = new dvClass();
        public  dvClass InjPr = new dvClass();
        public  dvClass TmpPr = new dvClass();
        public  dvClass PrdPr = new dvClass();
        public  dvClass IOSPr = new dvClass();
        public  dvClass IOFPr = new dvClass();
        public  dvClass IBTPr = new dvClass();
        public objUnit objHeart; 

        dbBase dataBase = new dbBase();
        public dvBase()
        {
            try
            {
                objHeart = dataBase[0];

                objUnit.getObjHandle = getObj;
                for (int i = 0; i < dataBase.length; i++)
                {
                    if (dataBase[i] != null)
                    {
                        int num = Int32.Parse(dataBase[i].serialNum.Substring(3, dataBase[i].serialNum.Length - 3));
                        string ser = dataBase[i].serialNum.Substring(0, 3);
                        switch (ser)
                        {
                            case "Abt":
                                AlmPr[num] = dataBase[i];
                                break;
                            case "Key":
                                KeyPr[num] = dataBase[i];
                                break;
                            case "Ipr":
                                IprPr[num] = dataBase[i];
                                break;
                            case "Sys":
                                SysPr[num] = dataBase[i];
                                break;
                            case "Mld":
                                MldPr[num] = dataBase[i];
                                break;
                            case "Inj":
                                InjPr[num] = dataBase[i];
                                break;
                            case "Tmp":
                                TmpPr[num] = dataBase[i];
                                break;
                            case "Prd":
                                PrdPr[num] = dataBase[i];
                                break;
                            case "IOS":
                                IOSPr[num] = dataBase[i];
                                break;
                            case "IOF":
                                IOFPr[num] = dataBase[i];
                                break;
                            case "IBT":
                                IBTPr[num] = dataBase[i];
                                break;
                        }
                        //vm.debug(dataBase[i].serialNum);
                    }
                }
                objUnit.dv_base = this;
                almInit();
            }
            catch (Exception ex)
            {
                vm.perror("[dvBase] " + ex.ToString());
            }
        }
        private void almInit()
        {
            AlmPr[1].addBitSer( 0, "Alm002");
            AlmPr[1].addBitSer(	1, "Alm029");
            AlmPr[1].addBitSer(	2, "Alm032");
            AlmPr[1].addBitSer(	3, "###103");
            AlmPr[1].addBitSer(	4, "Alm112");
            AlmPr[1].addBitSer(	5, "Alm114");
            AlmPr[1].addBitSer(	6, "Alm034");
            AlmPr[1].addBitSer(	7, "Alm035");
            AlmPr[1].addBitSer(	8, "Alm001");
            AlmPr[1].addBitSer(	9, "Alm030");
            AlmPr[1].addBitSer(	10, "Alm028");
            AlmPr[1].addBitSer(	11, "Alm031");
            AlmPr[1].addBitSer(	12, "Alm033");
            AlmPr[1].addBitSer(	13, "###113");
            AlmPr[1].addBitSer(	14, "###114");
            AlmPr[1].addBitSer(	15, "Alm116");
            AlmPr[1].addBitSer(	16, "Alm117");
            AlmPr[1].addBitSer(	17, "Alm175");
            AlmPr[1].addBitSer(	18, "Alm044");
            AlmPr[1].addBitSer(	19, "Alm045");
            AlmPr[1].addBitSer(	20, "Alm091");
            AlmPr[1].addBitSer(	21, "Alm036");
            AlmPr[1].addBitSer(	22, "Alm182");
            AlmPr[1].addBitSer(	23, "Alm181");
            AlmPr[1].addBitSer(	24, "Alm022");
            AlmPr[1].addBitSer(	25, "Alm040");
            AlmPr[1].addBitSer(	26, "Alm041");
            AlmPr[1].addBitSer(	27, "Alm039");
            AlmPr[1].addBitSer( 28, "Alm207");
            AlmPr[1].addBitSer( 29, "Alm208");
            AlmPr[1].addBitSer( 30, "Alm209");
            AlmPr[1].addBitSer( 31, "Alm229");

            AlmPr[2].addBitSer(	0, "Alm013");
            AlmPr[2].addBitSer(	1, "Alm085");
            AlmPr[2].addBitSer(	2, "Alm086");
            AlmPr[2].addBitSer(	3, "Alm176");
            AlmPr[2].addBitSer(	4, "Alm188");
            AlmPr[2].addBitSer(	5, "Alm189");
            AlmPr[2].addBitSer(	6, "Alm037");
            AlmPr[2].addBitSer(	7, "Alm038");
            AlmPr[2].addBitSer(	8, "Alm026");
            AlmPr[2].addBitSer(	9, "Alm027");
            AlmPr[2].addBitSer(	10, "Alm179");
            AlmPr[2].addBitSer(	11, "Alm180");
            AlmPr[2].addBitSer(12, "Alm271");
            AlmPr[2].addBitSer(13, "Alm272");
            AlmPr[2].addBitSer(	14, "###214");
            AlmPr[2].addBitSer(	15, "###215");
            AlmPr[2].addBitSer(	16, "###216");
            AlmPr[2].addBitSer(	17, "###217");
            AlmPr[2].addBitSer(	18, "###218");
            AlmPr[2].addBitSer(19, "Alm266");
            AlmPr[2].addBitSer(20, "###000");
            AlmPr[2].addBitSer(21, "Alm268");
            AlmPr[2].addBitSer(22, "Alm269");
            AlmPr[2].addBitSer(23, "Alm267");
            AlmPr[2].addBitSer(24, "Alm273");
            AlmPr[2].addBitSer(25, "Alm274");
            AlmPr[2].addBitSer(	26, "###226");
            AlmPr[2].addBitSer(	27, "###227");
            AlmPr[2].addBitSer(	28, "Alm087");
            AlmPr[2].addBitSer(	29, "Alm096");
            AlmPr[2].addBitSer(	30, "Alm060");
            AlmPr[2].addBitSer(	31, "Alm059");

            AlmPr[3].addBitSer(	0, "Alm003");
            AlmPr[3].addBitSer(	1, "Alm004");
            AlmPr[3].addBitSer(	2, "Alm005");
            AlmPr[3].addBitSer(	3, "Alm006");
            AlmPr[3].addBitSer(	4, "Alm007");
            AlmPr[3].addBitSer(	5, "Alm008");
            AlmPr[3].addBitSer(	6, "Alm009");
            AlmPr[3].addBitSer(	7, "Alm010");
            AlmPr[3].addBitSer(	8, "Alm011");
            AlmPr[3].addBitSer(	9, "Alm012");
            AlmPr[3].addBitSer(	10, "Alm097");
            AlmPr[3].addBitSer(	11, "Alm172");
            AlmPr[3].addBitSer(12, "Alm173");
            AlmPr[3].addBitSer(	13, "Alm236");
            AlmPr[3].addBitSer(	14, "Alm237");
            AlmPr[3].addBitSer(	15, "Alm238");
            AlmPr[3].addBitSer(16, "Alm261");
            AlmPr[3].addBitSer(	17, "###317");
            AlmPr[3].addBitSer(	18, "###318");
            AlmPr[3].addBitSer(	19, "###319");
            AlmPr[3].addBitSer(	20, "Alm066");
            AlmPr[3].addBitSer(	21, "Alm072");
            AlmPr[3].addBitSer(	22, "Alm067");
            AlmPr[3].addBitSer(	23, "Alm073");
            AlmPr[3].addBitSer(	24, "Alm068");
            AlmPr[3].addBitSer(	25, "Alm074");
            AlmPr[3].addBitSer(	26, "Alm069");
            AlmPr[3].addBitSer(	27, "Alm075");
            AlmPr[3].addBitSer(	28, "Alm070");
            AlmPr[3].addBitSer(	29, "Alm076");
            AlmPr[3].addBitSer(	30, "Alm071");
            AlmPr[3].addBitSer(	31, "Alm077");

            AlmPr[4].addBitSer(	0, "Alm014");
            AlmPr[4].addBitSer(	1, "Alm174");
            AlmPr[4].addBitSer(	2, "Alm015");
            AlmPr[4].addBitSer(	3, "Alm016");
            AlmPr[4].addBitSer(	4, "Alm053");
            AlmPr[4].addBitSer(	5, "Alm062");
            AlmPr[4].addBitSer(	6, "Alm108");
            AlmPr[4].addBitSer(	7, "Alm023");
            AlmPr[4].addBitSer(	8, "Alm115");
            AlmPr[4].addBitSer(	9, "Alm226");
            AlmPr[4].addBitSer(	10, "Alm051");
            AlmPr[4].addBitSer(	11, "Alm043");
            AlmPr[4].addBitSer(	12, "Alm227");
            AlmPr[4].addBitSer(	13, "Alm104");
            AlmPr[4].addBitSer(	14, "Alm105");
            AlmPr[4].addBitSer(	15, "Alm099");
            AlmPr[4].addBitSer(	16, "Alm228");
            AlmPr[4].addBitSer(	17, "Alm024");
            AlmPr[4].addBitSer(	18, "Alm220");
            AlmPr[4].addBitSer(	19, "Alm221");
            AlmPr[4].addBitSer(	20, "Alm222");
            AlmPr[4].addBitSer(	21, "Alm017");
            AlmPr[4].addBitSer(	22, "Alm042");
            AlmPr[4].addBitSer(	23, "Alm054");
            AlmPr[4].addBitSer(	24, "Alm055");
            AlmPr[4].addBitSer(	25, "Alm065");
            AlmPr[4].addBitSer(	26, "Alm064");
            AlmPr[4].addBitSer(	27, "Alm063");
            AlmPr[4].addBitSer(	28, "Alm223");
            AlmPr[4].addBitSer(	29, "Alm110");
            AlmPr[4].addBitSer(	30, "Alm224");
            AlmPr[4].addBitSer(	31, "Alm225");

            AlmPr[5].addBitSer(	0, "Alm018");
            AlmPr[5].addBitSer(	1, "Alm185");
            AlmPr[5].addBitSer(	2, "Alm184");
            AlmPr[5].addBitSer(	3, "Alm186");
            AlmPr[5].addBitSer(	4, "Alm187");
            AlmPr[5].addBitSer(	5, "Alm205");
            AlmPr[5].addBitSer(	6, "Alm191");
            AlmPr[5].addBitSer(	7, "Alm192");
            AlmPr[5].addBitSer(	8, "###508");
            AlmPr[5].addBitSer(	9, "Alm019");
            AlmPr[5].addBitSer(	10, "Alm113");
            AlmPr[5].addBitSer(	11, "Alm095");
            AlmPr[5].addBitSer(	12, "Alm025");
            AlmPr[5].addBitSer(	13, "Alm101");
            AlmPr[5].addBitSer(	14, "Alm190");
            AlmPr[5].addBitSer(	15, "Alm230");
            AlmPr[5].addBitSer(	16, "Alm231");
            AlmPr[5].addBitSer(	17, "Alm057");
            AlmPr[5].addBitSer(	18, "Alm056");
            AlmPr[5].addBitSer(	19, "Alm058");
            AlmPr[5].addBitSer(	20, "Alm098");
            AlmPr[5].addBitSer(	21, "Alm020");
            AlmPr[5].addBitSer(	22, "Alm021");
            AlmPr[5].addBitSer(23, "Alm233");
            AlmPr[5].addBitSer(24, "Alm234");
            AlmPr[5].addBitSer(25, "Alm235");
            AlmPr[5].addBitSer(	26, "Alm242");
            AlmPr[5].addBitSer(	27, "###527");
            AlmPr[5].addBitSer(	28, "###528");
            AlmPr[5].addBitSer(	29, "###529");
            AlmPr[5].addBitSer(	30, "###530");
            AlmPr[5].addBitSer(	31, "Alm243");

            AlmPr[6].addBitSer(	0, "Alm195");
            AlmPr[6].addBitSer(	1, "Alm196");
            AlmPr[6].addBitSer(	2, "Alm197");
            AlmPr[6].addBitSer(	3, "Alm198");
            AlmPr[6].addBitSer(	4, "Alm199");
            AlmPr[6].addBitSer(	5, "Alm119");
            AlmPr[6].addBitSer(	6, "Alm120");
            AlmPr[6].addBitSer(	7, "Alm121");
            AlmPr[6].addBitSer(	8, "Alm118");
            AlmPr[6].addBitSer(	9, "Alm206");
            AlmPr[6].addBitSer(	10, "Alm183");
            AlmPr[6].addBitSer(	11, "Alm122");
            AlmPr[6].addBitSer(	12, "Alm123");
            AlmPr[6].addBitSer(	13, "Alm124");
            AlmPr[6].addBitSer(	14, "Alm125");
            AlmPr[6].addBitSer(	15, "Alm200");
            AlmPr[6].addBitSer(	16, "Alm201");
            AlmPr[6].addBitSer(	17, "Alm202");
            AlmPr[6].addBitSer(	18, "Alm203");
            AlmPr[6].addBitSer(	19, "Alm204");
            AlmPr[6].addBitSer(	20, "Alm240");
            AlmPr[6].addBitSer(	21, "Alm241");
            AlmPr[6].addBitSer(22, "Alm244");
            AlmPr[6].addBitSer(23, "Alm245");
            AlmPr[6].addBitSer(24, "Alm246");
            AlmPr[6].addBitSer(25, "Alm247");
            AlmPr[6].addBitSer(26, "Alm248");
            AlmPr[6].addBitSer(27, "Alm249");
            AlmPr[6].addBitSer(	28, "###628");
            AlmPr[6].addBitSer(	29, "###629");
            AlmPr[6].addBitSer(	30, "Alm126");
            AlmPr[6].addBitSer(	31, "Alm127");

            AlmPr[7].addBitSer(	0, "Als128");
            AlmPr[7].addBitSer(	1, "Als129");
            AlmPr[7].addBitSer(	2, "Als130");
            AlmPr[7].addBitSer(	3, "Als131");
            AlmPr[7].addBitSer(	4, "Als132");
            AlmPr[7].addBitSer(	5, "Als133");
            AlmPr[7].addBitSer(	6, "Als134");
            AlmPr[7].addBitSer(	7, "Als135");
            AlmPr[7].addBitSer(	8, "Als136");
            AlmPr[7].addBitSer(	9, "Als210");
            AlmPr[7].addBitSer(	10, "Als211");
            AlmPr[7].addBitSer(	11, "Als212");
            AlmPr[7].addBitSer(	12, "Als213");
            AlmPr[7].addBitSer(	13, "Als214");
            AlmPr[7].addBitSer(	14, "Als215");
            AlmPr[7].addBitSer(	15, "Als137");
            AlmPr[7].addBitSer(	16, "Als138");
            AlmPr[7].addBitSer(	17, "Als139");
            AlmPr[7].addBitSer(	18, "Als140");
            AlmPr[7].addBitSer(	19, "Als141");
            AlmPr[7].addBitSer(	20, "Als142");
            AlmPr[7].addBitSer(	21, "Als143");
            AlmPr[7].addBitSer(	22, "Als159");
            AlmPr[7].addBitSer(	23, "Als160");
            AlmPr[7].addBitSer(	24, "Als161");
            AlmPr[7].addBitSer(	25, "Als153");
            AlmPr[7].addBitSer(	26, "Als154");
            AlmPr[7].addBitSer(	27, "Als155");
            AlmPr[7].addBitSer(	28, "Als156");
            AlmPr[7].addBitSer(	29, "Als157");
            AlmPr[7].addBitSer(	30, "Als158");
            AlmPr[7].addBitSer(	31, "Als162");

            AlmPr[8].addBitSer(	0, "Als163");
            AlmPr[8].addBitSer(	1, "Als164");
            AlmPr[8].addBitSer(	2, "Als165");
            AlmPr[8].addBitSer(	3, "Als166");
            AlmPr[8].addBitSer(	4, "Als167");
            AlmPr[8].addBitSer(	5, "Als168");
            AlmPr[8].addBitSer(	6, "Als152");
            AlmPr[8].addBitSer(	7, "Als144");
            AlmPr[8].addBitSer(	8, "Als145");
            AlmPr[8].addBitSer(	9, "Als146");
            AlmPr[8].addBitSer(	10, "Als147");
            AlmPr[8].addBitSer(	11, "Als148");
            AlmPr[8].addBitSer(	12, "Als149");
            AlmPr[8].addBitSer(	13, "Als150");
            AlmPr[8].addBitSer(	14, "Als169");
            AlmPr[8].addBitSer(	15, "Als151");
            AlmPr[8].addBitSer(	16, "Als170");
            AlmPr[8].addBitSer(	17, "Als171");
            AlmPr[8].addBitSer(	18, "Als193");
            AlmPr[8].addBitSer(	19, "Als194");
            AlmPr[8].addBitSer( 20, "Als232");
            AlmPr[8].addBitSer(	21, "Alm239");
            AlmPr[8].addBitSer(	22, "###822");
            AlmPr[8].addBitSer(	23, "###823");
            AlmPr[8].addBitSer(	24, "###824");
            AlmPr[8].addBitSer(	25, "###825");
            AlmPr[8].addBitSer(	26, "###826");
            AlmPr[8].addBitSer(	27, "###827");
            AlmPr[8].addBitSer(	28, "###828");
            AlmPr[8].addBitSer(	29, "###829");
            AlmPr[8].addBitSer(	30, "###830");
            AlmPr[8].addBitSer(31, "###831");

            AlmPr[9].addBitSer(0, "Alm250");
            AlmPr[9].addBitSer(1, "Alm251");
            AlmPr[9].addBitSer(2, "Alm252");
            AlmPr[9].addBitSer(3, "###903");
            AlmPr[9].addBitSer(4, "Alm253");
            AlmPr[9].addBitSer(5, "Alm254");
            AlmPr[9].addBitSer(6, "Alm255");
            AlmPr[9].addBitSer(7, "Alm256");
            AlmPr[9].addBitSer(8, "Alm257");
            AlmPr[9].addBitSer(9, "###909");
            AlmPr[9].addBitSer(10, "Alm258");
            AlmPr[9].addBitSer(11, "###911");
            AlmPr[9].addBitSer(12, "###912");
            AlmPr[9].addBitSer(13, "###913");
            AlmPr[9].addBitSer(14, "###914");
            AlmPr[9].addBitSer(15, "Alm259");
            AlmPr[9].addBitSer(16, "##917");
            AlmPr[9].addBitSer(18, "Alm260");
            AlmPr[9].addBitSer(19, "##919");
            AlmPr[9].addBitSer(20, "Alm262");
            AlmPr[9].addBitSer(21, "Alm263");
            AlmPr[9].addBitSer(22, "Alm264");
            AlmPr[9].addBitSer(23, "Alm265");
            AlmPr[9].addBitSer(24, "###924");
            AlmPr[9].addBitSer(25, "###925");
            AlmPr[9].addBitSer(26, "###926");
            AlmPr[9].addBitSer(27, "###927");
            AlmPr[9].addBitSer(28, "###928");
            AlmPr[9].addBitSer(29, "###929");
            AlmPr[9].addBitSer(30, "###930");
            AlmPr[9].addBitSer(31, "###931");
        }
        public callBackObjEvent feedbackHandle
        {
            set
            {
                lstFeedbackHandle = value;
            }
            get
            {
                return lstFeedbackHandle;
            }
        }
        public void init(setValueEvent setHandle,nullEvent checkHandle)
        {

        }
        public objUnit this[int index]
        {
            get
            {
                //if (index < 0 || index >= dataBase.length)
                //    return null;
                return dataBase[index];
            }
            set
            {
                dataBase[index] = value;
            }
        }
        public int length
        {
            get
            {
                return dataBase.length;
            }
        }
        public objUnit getObj(string serialNum)
        {
            try
            {
                objUnit retObj = null;
                int serNr = Int32.Parse(serialNum.Substring(3, serialNum.Length - 3));
                switch (serialNum.Substring(0, 3))
                {
                    //case "Hrt":
                    //    curObj = valmoWin.dv.objHeart;
                    //    break;
                    case "Ipr":
                        retObj = IprPr[serNr];
                        break;
                    case "Sys":
                        retObj = SysPr[serNr];
                        break;
                    case "Mld":
                        retObj = MldPr[serNr];
                        break;
                    case "Inj":
                        retObj = InjPr[serNr];
                        break;
                    case "Tmp":
                        retObj = TmpPr[serNr];
                        break;
                    case "Prd":
                        retObj = PrdPr[serNr];
                        break;
                    case "IOS":
                        retObj = IOSPr[serNr];
                        break;
                    case "IOF":
                        retObj = IOFPr[serNr];
                        break;
                    case "IBT":
                        retObj = IBTPr[serNr];
                        break;
                    case "Alm":
                        retObj = AlmPr[serNr];
                        break;
                    case "Key":
                        retObj = KeyPr[serNr];
                        break;
                }
                return retObj;
            }
            catch (Exception ex)
            {
                vm.perror("[dvBase.getObj] " + ex.ToString());
                return null;
            }
        }

    }
}
