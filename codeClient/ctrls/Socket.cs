using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using nsDataMgr;

namespace nsVicoClient.ctrls
{
    class SocketConnect
    {
        private static SocketConnect instance;

        private Socket socketClient = null;
        public bool IsSocketInit = false;

        private SocketConnect()
        {
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("192.168.1.104");
            IPEndPoint endPoint = new IPEndPoint(ip, 13000);

            try
            {
                socketClient.Connect(endPoint);

                IsSocketInit = true;
            }
            catch (SocketException se)
            {
                App.log.Error("Socket Init Error!" + se.Message);

                IsSocketInit = false;
            }
        }

        public void UpLoad(string fileName)
        {
            if (IsSocketInit == true)
            {
                string socketMsg = "";

                //时间
                socketMsg += DateTime.Now.ToString("yyyyMMddhhmmss");

                //教导文件名
                socketMsg += fileName.PadRight(20, '#');

                //机器状态
                socketMsg += queryParm("Sys105");

                //生产计划

                //已生产产品数（良品数）
                socketMsg += queryParm("Prd001");
                //不良品数
                socketMsg += queryParm("Prd002");
                //生产计时
                socketMsg += queryParm("Prd003");
                //计划产品数
                socketMsg += queryParm("Prd008");
                //平均周期时间
                socketMsg += queryParm("Prd024");

                //生产监控

                //注射保压时间
                socketMsg += queryParm("Prd026");
                socketMsg += queryParm("Prd027");
                //储料时间
                socketMsg += queryParm("Prd033");
                socketMsg += queryParm("Prd034");
                //循环时间
                socketMsg += queryParm("Prd040");
                socketMsg += queryParm("Prd041");
                //VP切换位置
                socketMsg += queryParm("Prd047");
                socketMsg += queryParm("Prd048");
                //VP切换压力
                socketMsg += queryParm("Prd054");
                socketMsg += queryParm("Prd055");
                //VP切换时间
                socketMsg += queryParm("Prd061");
                socketMsg += queryParm("Prd062");
                //注射开始位置
                socketMsg += queryParm("Prd068");
                socketMsg += queryParm("Prd069");
                //注射最前位置
                socketMsg += queryParm("Prd075");
                socketMsg += queryParm("Prd076");
                //储料前位置
                socketMsg += queryParm("Prd082");
                socketMsg += queryParm("Prd083");
                //注射峰值压力
                socketMsg += queryParm("Prd089");
                socketMsg += queryParm("Prd090");

                //电能统计
                
                //机器加热每周期能耗
                socketMsg += queryParm("Prd745");
                //模具加热每周期能耗
                socketMsg += queryParm("Prd746");
                //驱动器每周期能耗
                socketMsg += queryParm("Prd747");
                //每周期总能耗
                socketMsg += queryParm("Prd748");
                //机器加热每小时能耗
                socketMsg += queryParm("Prd749");
                //模具加热每小时能耗
                socketMsg += queryParm("Prd750");
                //驱动器每小时能耗
                socketMsg += queryParm("Prd751");
                //每小时总能耗
                socketMsg += queryParm("Prd752");
                //机器加热功率
                socketMsg += queryParm("Prd753");
                //模具加热功率
                socketMsg += queryParm("Prd754");
                //驱动器功率
                socketMsg += queryParm("Prd755");
                //机器总功率
                socketMsg += queryParm("Prd756");
                //每KG能耗
                socketMsg += queryParm("Prd757");
                //项目总能耗
                socketMsg += queryParm("Prd758");

                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(socketMsg);

                socketClient.Send(arrMsg);
            }
        }

        private string queryParm(string objName)
        {
            string result = "";

            objUnit obj = objUnit.getObjHandle(objName);

            if (obj != null)
            {
                result = objName + obj.value.ToString().PadRight(10, '#');
            }
            else
            {
                result = "###0000000000000";
            }

            return result;
        }

        public static SocketConnect getInstance()
        {
            if (instance == null)
            {
                instance = new SocketConnect();
            }

            return instance;
        }
    }
}
