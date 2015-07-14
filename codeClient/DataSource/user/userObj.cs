using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsVicoClient
{
    //用户总共分为5个等级，级别越高权限越大
    // 5级为root用户，可以执行任何操作
    // 4级为设备厂商调试人员级别，暂时只创建一个用户
    // 3级为设备使用厂家的最高级别，一般可创建三个用户，而且该用户可以设置后面1、2级别用户的信息
    // 2级为设备使用厂家工艺操作员级别，可以执行基本操作及工艺流程的参数设置，数量限制为20个
    // 1级为设备使用厂家基本操作员级别，只能执行基本的操作，数量限制为20个
    public class userClass
    {
        private string user_name;
        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public string name
        {
            get
            {
                return user_name;
            }
            set
            {
                user_name = value;
            }
        } 
        private string user_password;
        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string password
        {
            set
            {
                user_password = value;
            }
            get
            {
                return user_password;
            }
        } 
        private int user_userId;
        /// <summary>
        /// 获取用户ID(未启用)
        /// </summary>
        public int userId
        {
            get
            {
                return user_userId;
            }
        }
        private DateTime user_createTime;
        /// <summary>
        /// 获取用户创建时间(未启用)
        /// </summary>
        public DateTime createTime
        {
            get
            {
                return user_createTime;
            }

        } 
        private DateTime user_lastLoadTime;
        /// <summary>
        /// 获取或设置用户最后登录时间
        /// </summary>
        public DateTime lastLoadTime
        {
            get
            {
                return user_lastLoadTime;
            }
            set
            {
                user_lastLoadTime = value;
            }
        }  
        private int user_accessLevel;
        /// <summary>
        /// 获取或设置用户权限等级
        /// </summary>
        public int accessLevel
        {
            get
            {
                return user_accessLevel;
            }
            set
            {
                user_accessLevel = value;
            }
        } 
        private string user_language;
        /// <summary>
        /// 获取或设置用户语言
        /// </summary>
        public string language
        {
            get
            {
                return user_language;
            }
            set
            {
                user_language = value;
            }
        }
        private List<int> user_children;
        /// <summary>
        /// 获取或设置子用户(以ID表示）
        /// </summary>
        public List<int> children
        {
            get
            {
                return user_children;
            }
            set
            {
                user_children = value;
            }
        }

      
        public userClass()
        {
            user_name = "null";
            user_password = "";
            user_userId = -1;
            user_createTime = DateTime.Now;
            user_accessLevel = 0;
            user_language = "lanCN";
            user_children = new List<int>();
        }
        public userClass(string name, string password, int userid, DateTime createtime, byte accesslevel)
        {
            user_name = name;
            user_password = password;
            user_userId = userid;
            user_createTime = createtime;
            user_accessLevel = accesslevel;
            user_language = "lanCN";
            user_children = new List<int>();
        }

        /// <summary>
        /// 复制用户
        /// </summary>
        /// <param name="user_scr">源用户</param>
        public void user_copy(userClass user_scr)
        {
            if (this == user_scr)
                return;
            name = user_scr.name;
            user_password = user_scr.user_password;
            user_userId = user_scr.user_userId;
            user_createTime = user_scr.user_createTime;
            user_accessLevel = user_scr.user_accessLevel;
            user_language = user_scr.user_language;
        }

        public userClass getCurUser()
        {
            return null;
        }

        /// <summary>
        /// 打印用户信息
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return user_name + "\t" + user_password + "\t" + user_userId + "\t" + user_createTime + "\t" + user_accessLevel + "\t";
        }

    }
}
