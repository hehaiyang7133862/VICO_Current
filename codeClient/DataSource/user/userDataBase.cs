using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsVicoClient
{
    public enum modify_type : int
    {
        MODIFY_USER_NAME = 0,
        MODIFY_USER_PASSWORD,
        MODIFY_USER_ACCESSLEVEL
    };
    public enum userType: byte
    {
        RootUser = 5,
        DebugUser = 4,

    }
    public class userBase
    {
        public userClass userRoot;
        public userClass userSer;
        public userClass userMgr;
        public userClass userMt;
        public userClass userOp;
        public userClass userLiuzs;
        public List<userClass> lstMt = new List<userClass>();
        public List<userClass> lstOp = new List<userClass>();
        public userClass userNull;
        userClass[] vm_userObjs = new userClass[100];
        /************************************************
         *    方法
         * 0、构造函数
         * 1、匹配用户名、密码
         * 2、添加新用户
         * 3、删除一个用户
         * 4、修改用户信息
         ************************************************/

        //0、构造函数
        //0、construct function
        public userBase()
        {
            try
            {
                string processorID = hardware.getProcessorId();
                if (processorID == Properties.Settings.Default.processorId)
                {
                    string str = Properties.Settings.Default.root;
                   
                    string[] strTmp = str.Split('\t');
                    //if (strTmp[0] == "root")
                    //{
                    userRoot = new userClass(strTmp[0], strTmp[1], Int32.Parse(strTmp[2]), new DateTime(2012, 3, 25, 0, 0, 0), 5);
                    //}

                    str = Properties.Settings.Default.ser;
                    strTmp = str.Split('\t');
                    //if (strTmp[0] == "ser")
                    //{
                    userSer = new userClass(strTmp[0], strTmp[1], Int32.Parse(strTmp[2]), new DateTime(2012, 3, 25, 0, 0, 0), 4);
                    //}
                    str = Properties.Settings.Default.mgr;
                    strTmp = str.Split('\t');
                    //if (strTmp[0] == "mgr")
                    //{
                    userMgr = new userClass(strTmp[0], strTmp[1], Int32.Parse(strTmp[2]), new DateTime(2012, 3, 25, 0, 0, 0), 3);
                    //}
                    str = Properties.Settings.Default.mt;
                    strTmp = str.Split('\t');
                    //if (strTmp[0] == "mt")
                    //{
                    userMt = new userClass(strTmp[0], strTmp[1], Int32.Parse(strTmp[2]), new DateTime(2012, 3, 25, 0, 0, 0), 2);
                    //}
                    str = Properties.Settings.Default.op;
                    strTmp = str.Split('\t');
                    //if (strTmp[0] == "op")
                    //{
                    userOp = new userClass(strTmp[0], strTmp[1], Int32.Parse(strTmp[2]), new DateTime(2012, 3, 25, 0, 0, 0), 1);
                    //}

                    str = Properties.Settings.Default.lstMt;
                    strTmp = str.Split('#');

                    int num = Int32.Parse(strTmp[0]);
                    if (strTmp.Length == num + 2)
                    {
                        for (int i = 1; i < num + 1; i++)
                        {
                            string[] strVar = strTmp[i].Split('\t');
                            lstMt.Add(new userClass(strVar[0], strVar[1], Int32.Parse(strVar[2]), DateTime.Parse(strVar[3]), Byte.Parse(strVar[4])));
                        }
                    }

                    str = Properties.Settings.Default.lstOp;
                    strTmp = str.Split('#');
                    num = Int32.Parse(strTmp[0]);
                    if (strTmp.Length == num + 2)
                    {
                        for (int i = 1; i < num + 1; i++)
                        {
                            string[] strVar = strTmp[i].Split('\t');
                            lstOp.Add(new userClass(strVar[0], strVar[1], Int32.Parse(strVar[2]), DateTime.Parse(strVar[3]), Byte.Parse(strVar[4])));
                        }
                    }

                }
                else
                {
                    userRoot = new userClass("root", "Yehill", 5, new DateTime(2012, 3, 25, 0, 0, 0), 5);
                    userSer = new userClass("ser", "Action", 4, new DateTime(2012, 3, 25, 0, 0, 0), 4);
                    userMgr = new userClass("mgr", "2013", 3, new DateTime(2012, 3, 25, 0, 0, 0), 3);
                    userMt = new userClass("mt", "222", 2, new DateTime(2012, 3, 25, 0, 0, 0), 2);
                    userOp = new userClass("op", "111", 1, new DateTime(2012, 3, 25, 0, 0, 0), 1);

                    Properties.Settings.Default.root = userRoot.toString();
                    Properties.Settings.Default.ser = userSer.toString();
                    Properties.Settings.Default.mgr = userMgr.toString();
                    Properties.Settings.Default.mt = userMt.toString();
                    Properties.Settings.Default.op = userOp.toString();
                    Properties.Settings.Default.lstMt = "0#";
                    Properties.Settings.Default.lstOp = "0#";
                    Properties.Settings.Default.processorId = processorID; 
                    Properties.Settings.Default.Save();
                }
            }
            catch
            {

            }
            userLiuzs = new userClass("liuzs","643",6,new DateTime(2012, 3, 25, 0, 0, 0), 3);
             userNull = new userClass("guest", "null", 0, new DateTime(2012, 3, 25, 0, 0, 0), 0);
        }
        //1、匹配用户名、密码
        //1. match the username and password
        public bool checkUserNameAndPassword(string name, string password)
        {
            if (name == userRoot.name && password == userRoot.password)
                return true;
            else if (name == userSer.name && password == userSer.password)
                return true;
            else if (name == userMgr.name && password == userMgr.password)
                return true;
            else if (name == userMt.name && password == userMt.password)
                return true;
            else if (name == userOp.name && password == userOp.password)
                return true;
            for (int i = 0; i < lstMt.Count; i++)
            {
                if (lstMt[i].name == name && lstMt[i].password == password)
                    return true;
            }
            for (int i = 0; i < lstOp.Count; i++)
            {
                if (lstOp[i].name == name && lstOp[i].password == password)
                    return true;
            }
            return false;
        }
        //2、添加新用户
        //2、add new user
        //先检查用户名是否已经存在
        //如果不存在就保存到结构中，并写到文件中；
        public int addUser(string name, string password, DateTime dt, byte level,string lan)
        {
            try
            {
                if (name == null || name == "")
                    return -2;//用户名错误
                userClass userNew = getUserByName(name);
                if (userNew != null)  //该用户已经存在
                    return -1;

                int userid = 76;
                switch (level)
                {
                    case 1:
                        {
                            userid = Properties.Settings.Default.opID;
                            userNew = new userClass(name, password, userid, dt, level);
                            Properties.Settings.Default.opID = userid + 1;
                            lstOp.Add(userNew);
                            saveToFile(1);
                            break;
                        }
                    case 2:
                        {
                            userid = Properties.Settings.Default.mtID;
                            userNew = new userClass(name, password, userid, dt, level);
                            userid = Properties.Settings.Default.mtID = userid + 1;
                            lstMt.Add(userNew);
                            saveToFile(2);
                            break;
                        }
                    case 3:
                        break;
                }
                vm.printLn("add new user ok！");
                return 1;

                //vm.printLn("name : {0} password : {1} userid : {2} createtime : {3} accessleve : {4} ", name, password, userid, dt, level);
            }
            catch (Exception ex)
            {
                vm.printLn(ex.ToString());
            }
            return 0;
        }
        public int deleteUser(string name)
        {
            userClass user = getUserByName(name);
            
            if (user == null)
                return -1; //不存在该用户
            if (user.accessLevel > 2)
                return -2;
            switch (user.accessLevel)
            {
                case 1:
                    {
                        lstOp.Remove(user);
                        saveToFile(1);
                        break;
                    }
                case 2:
                    {
                        lstMt.Remove(user);
                        saveToFile(2);
                        break;
                    }
            }
            return 0;
        }
        public userClass getUserByName(string name)
        {
            if (name == userRoot.name)
                return userRoot;
            if (name == userSer.name)
                return userSer;
            if (name == userMgr.name)
                return userMgr;
            if (name == userMt.name)
                return userMt;
            if (name == userOp.name)
                return userOp;
                     
            for (int i = 0; i < lstMt.Count; i++)
            {
                if (lstMt[i].name == name)
                    return lstMt[i];
            }
            for (int i = 0; i < lstOp.Count; i++)
            {
                if (lstOp[i].name == name)
                    return lstOp[i];
            }
            return null;
        }
        public userClass getUserObjByUserId(int id)
        {
            if (id == 5)
                return userRoot;
            else if (id == 4)
                return userSer;
            else if (id == 3)
                return userMgr;
            else if (id == 2)
                return userMt;
            else if (id == 1)
                return userOp;
            else
            {
                for (int i = 0; i < lstMt.Count; i++)
                {
                    if (lstMt[i].userId == id)
                        return lstMt[i];

                }
                for (int i = 0; i < lstOp.Count; i++)
                {
                    if (lstOp[i].userId == id)
                        return lstOp[i];
                }
            }
            return null;
        }
        //4、修改用户信息
         public int modifyUserMessage(string nameNew, string name, string password, int level, string lan)
        {
            userClass user = getUserByName(name);
             
            if (user == null)
                return -1;//该用户不存在
            if (getUserByName(nameNew) != null)
                return -2;//该用户已经存在
            if (user.accessLevel != level)
            {
                if (level == 1)
                {
                    if(lstOp.Count < 3)
                    {
                        lstOp.Add(new userClass(user.name,user.password,user.userId,user.createTime,1));
                        lstMt.Remove(user);
                        saveToFile(2);
                    }
                }
                else if (level == 2)
                {
                    if (lstMt.Count < 3)
                    {
                        lstMt.Add(new userClass(user.name, user.password, user.userId, user.createTime, 2));
                        lstOp.Remove(user);
                        saveToFile(1);
                    }
                }
            }
            else
            {
                if (nameNew != null && nameNew != "")
                    user.name = nameNew;
                user.password = password;
                user.language = lan;
                user.accessLevel = level;
            }
            saveToFile(level);
            return 1;
        }
        public int modifyUserMessage(string name, string type, string value)
        {
            userClass user = getUserByName(name);
            if (user == null)
                return -1;//该用户不存在
            switch (type)
            {
                case "name": //userName
                    {
                        if (value == null || value == "")
                            return -3;//用户名错误
                        userClass newUser = getUserByName(value);
                        if (getUserByName(value) != null)
                            return -2;//该用户已经存在
                            user.name = value;
                    }
                    break;
                case "password": //password
                    user.password = value;
                    break;
                case "accessLevel"://level
                    byte acclevel = byte.Parse(value);
                    int oldLevel = user.accessLevel;
                    user.accessLevel = acclevel;
                    saveToFile(oldLevel);
                    break;
            }
            saveToFile(user.accessLevel);
            return 1;
        }
        public void saveToFile(int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        {
                            Properties.Settings.Default.op = userOp.toString();

                            string str = lstOp.Count + "#";
                            for (int i = 0; i < lstOp.Count; i++)
                            {
                                str += lstOp[i].toString() + "#";
                            }
                            Properties.Settings.Default.lstOp = str;
                            break;
                        }
                    case 2:
                        {
                            Properties.Settings.Default.mt = userMt.toString();

                            string str = lstMt.Count + "#";
                            for (int i = 0; i < lstMt.Count; i++)
                            {
                                str += lstMt[i].toString() + "#";
                            }
                            Properties.Settings.Default.lstMt = str;
                            break;
                        }
                    case 3:
                        {
                            string str = userMgr.toString();
                            Properties.Settings.Default.mgr = str;
                            break;
                        }
                    case 4:
                        {
                            string str = userSer.toString();
                            Properties.Settings.Default.ser = str;
                        }
                        break;
                    case 5:
                        {
                            string str = userRoot.toString();
                            Properties.Settings.Default.root = str;
                        }
                        break;
                }
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                vm.printLn("[save User Message to file]" + ex.ToString());
            }
        }

    }
}
