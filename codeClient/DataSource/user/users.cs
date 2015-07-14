using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nsVicoClient.ctrls;
using System.IO;

namespace nsVicoClient
{

 /* //用户总共分为5个等级，级别越高权限越大
    // 5级为root用户，可以执行任何操作
    // 4级为设备厂商调试人员级别，暂时只创建一个用户
    // 3级为设备使用厂家的最高级别，一般可创建三个用户，而且该用户可以设置后面1、2级别用户的信息
    // 2级为设备使用厂家工艺操作员级别，可以执行基本操作及工艺流程的参数设置，数量限制为20个
    // 1级为设备使用厂家基本操作员级别，只能执行基本的操作，数量限制为20个
  * 
  * 针对每个客户提供一个客户编号，以后每个客户的每台机器都会在plc上记录当前的客户编号
  * 管理员用户（3级）可以为每个工艺员（2级）和操作员（1级）生成登录U盘。
  * 在登录U盘中会将用户编号记录到U盘中作为登录比较的其中一项依据
  * 
  * 级别高的用户可以创建、删除、修改级别低的用户的信息
  * 
  * */
    public class Users
    {
        public userClass root;
        public userClass service;
        public userClass mgr;
        public userClass mt;
        public userClass op;
        public List<userClass> lstMt;
        public List<userClass> lstOp;
        public userClass curUser;
        public userClass nullUser;
        userBase userDb = new userBase();
        public static string[] userTypeName = new string[] { "...", "userOp", "userMt", "userMgr", "userSer", "userRoot" };
        public Users()
        {
            try
            {
                this.root = userDb.userRoot;
                this.service = userDb.userSer;
                this.mgr = userDb.userMgr;
                this.lstMt = userDb.lstMt;
                this.lstOp = userDb.lstOp;
                this.nullUser = userDb.userNull;
                this.curUser = nullUser;
                this.mt = userDb.userMt;
                this.op = userDb.userOp;
            }
            catch
            {


            }
        }
        public List<userClass> getUserLst()
        {
            return getUserLst(curUser.accessLevel);
        }
        public List<userClass> getUserLst(int accLevel)
        {
            List<userClass> userLst = new List<userClass>();
            if (accLevel == 5)
                userLst.Add(root);
            if (accLevel > 3)
            {
                userLst.Add(service);
            }
            if (accLevel > 2)
            {
                userLst.Add(mgr);
                userLst.Add(mt);
                for (int i = 0; i < lstMt.Count; i++)
                    userLst.Add(lstMt[i]);
            }
            if (accLevel > 1)
            {
                userLst.Add(op);
                for (int i = 0; i < lstOp.Count; i++)
                    userLst.Add(lstOp[i]);
            }
            if (accLevel == 2 || accLevel == 1)
            {
                userLst.Insert(0, curUser);
            }
            return userLst;
        }
        bool checkUserNameAndPassword(string name, string password)
        {
            return userDb.checkUserNameAndPassword(name, password);
        }
        public userClass getUserObjByName(string name)
        {
            return userDb.getUserByName(name);
        }
        public userClass getUserObjByUserId(int id)
        {
            return userDb.getUserObjByUserId(id);
        }
        public bool setCurUser(string name, string password)
        {
            if (checkUserNameAndPassword(name, password))
            {
                this.curUser = userDb.getUserByName(name);

                valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwLogInOK));
                return true;
            }
            return false ;
        }
        public void setCurUser(int nr)
        {
            if (nr == 5)
                this.curUser = userDb.userRoot;
            else if (nr == 4)
                this.curUser = userDb.userSer;
            else if (nr == 3)
                this.curUser = userDb.userMgr;
            else if (nr == 643)
                this.curUser = userDb.userLiuzs;
            else if (nr == 1)
                this.curUser = userDb.userOp;
            else if (nr == 2)
                this.curUser = userDb.userMt;

            valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwLogInOK));
        }
        public void liuzsLoad()
        {
            this.curUser = userDb.userLiuzs;
            //WinMsg msg = new WinMsg();
            //msg.type = WinMsgType.mwLogInOK;
            //valmoWin.sendMsgToWinHandle(msg);
            valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwLogInOK));
        }
        public void unload()
        {
            if (curUser == nullUser)
            {
                return;
            }
            else
            {
                string lastUserName = curUser.name;
                curUser = nullUser;
                valmoWin.execHandle(opeOrderType.winMsg, new WinMsg(WinMsgType.mwLogOut, lastUserName));
            }
        }
        public bool addUser(userClass user)
        {
            if (curUser.accessLevel <= user.accessLevel)
                return false;
            switch (user.accessLevel)
            {
                case 1:
                    
                    break;
                case 2:

                    break;
                case 3:

                    break;
            }
            return true;
        }
        public userClass addUser()
        {
            if (lstOp.Count < 3)
            {
                return makeNewOpUser(1);
            }
            if (curUser.accessLevel > 1 && lstMt.Count < 3)
            {
                return makeNewMtUser(1);
            }
            return null;
        }
        public userClass makeNewOpUser(int nr)
        {
            if (nr > 9)
                return null;
            if (getUserObjByName("op" + nr) != null)
            {
                return makeNewOpUser(nr + 1);
            }
            else
            {
                userClass newUser = new userClass("op" + nr, "111", Properties.Settings.Default.mtID++, DateTime.Now, 1);
                lstOp.Add(newUser);
                userDb.saveToFile(1);
                return newUser;
            }
        }
        public userClass makeNewMtUser(int nr)
        {
            if (nr > 9)
                return null;
            if (getUserObjByName("mt" + nr) != null)
            {
                return makeNewMtUser( nr + 1);
            }
            else
            {
                userClass newUser = new userClass("mt" + nr, "222", Properties.Settings.Default.mtID++, DateTime.Now, 2);
                lstMt.Add(newUser);
                userDb.saveToFile(2);
                return newUser;
            }
        }
        public bool delUser(userClass user)
        {
            userDb.deleteUser(user.name);
            return true;
        }
        public bool mdyUser(string nameNew, string name, string password, int level, string lan)
        {
            try
            {
                userDb.modifyUserMessage(nameNew, name, password, level, lan);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool checkFull()
        {
            if (curUser.accessLevel > 2)
            {
                if (lstMt.Count < 3 || lstOp.Count < 3)
                    return false;
            }
            else if (curUser.accessLevel > 1)
            {
                if (lstOp.Count < 3)
                    return false;
            }
            return true;
        }
    }
    public enum UserHandleType : byte
    {
        NAME,         
        PASSWORD,
        ACCESSLEVEL,
        DEL
    }
}
