using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using System.Xml;
using CLDC_DataCore.DataBase;
using System.Windows.Forms;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class csUserInfo
    {
        private Dictionary<string, StUserInfo> _UserInfo;
        /// <summary>
        /// 构造函数
        /// </summary>
        public csUserInfo()
        { 
            _UserInfo=new Dictionary<string,StUserInfo>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csUserInfo()
        {
            _UserInfo = null;
        }
        /// <summary>
        /// 读取初始化用户信息模型
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            _UserInfo.Clear();           //清空用户信息集合
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_USERSPATH, out _ErrorString);
            if (_ErrorString != "")
            {
                _XmlNode = clsXmlControl.CreateXmlNode("UserInfo");
                XmlNode _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "Admin", "Pwd", "", "Level", "0");
                _XmlNode.AppendChild(_XmlChildNode);
                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_USERSPATH);
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StUserInfo _User = new StUserInfo();

                _User.UserName = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                _User.Pwd = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                _User.Level = int.Parse(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                _UserInfo.Add(_User.UserName, _User);
            }
            return;
        }
        /// <summary>
        /// 存储用户模型数据到XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("","UserInfo");
            foreach(StUserInfo _u in _UserInfo.Values)
            {
                _Xml.appendchild("","R","Name",_u.UserName,"Pwd",_u.Pwd,"Level",_u.Level.ToString());
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_USERSPATH);
        }
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="User">用户信息结构体</param>
        public void Add(StUserInfo User)
        {
            if (User.UserName == "")
            {
                return;
            }
            if (_UserInfo.ContainsKey(User.UserName))
            {
                _UserInfo[User.UserName] = User;

            }
            else
            {
                _UserInfo.Add(User.UserName, User);
            }
            this.Save();        //新增完毕保存XML文档
                               
        }
        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public bool FindUser(string UserName)
        {
            return _UserInfo.ContainsKey(UserName);
                
        }

        /// <summary>
        /// 移除一个用户
        /// </summary>
        /// <param name="Tkey">用户名</param>
        public void Remove(string Tkey)
        {
            if (!_UserInfo.ContainsKey(Tkey))
                return;
            _UserInfo.Remove(Tkey);
            this.Save();
        }
        /// <summary>
        /// 系统登陆验证
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Pwd">密码</param>
        /// <param name="OutUserInfo">用户信息结构体</param>
        /// <returns>返回登陆成功或失败</returns>
        public bool CheckIn(string UserName, string Pwd, out StUserInfo OutUserInfo)
        {
            if (!_UserInfo.ContainsKey(UserName) || Pwd != _UserInfo[UserName].Pwd)
            {
                OutUserInfo = new StUserInfo();
                return false;
            }
            else
            {
                OutUserInfo = _UserInfo[UserName];
            }
            return true;

        }
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>返回List</returns>
        public List<StUserInfo> getUsers()
        {
            List<StUserInfo> _Users=new List<StUserInfo>();
            foreach (string _name in _UserInfo.Keys)
            {

                StUserInfo _TmpUser = _UserInfo[_name];
                _Users.Add(_TmpUser);
                
            }
            return _Users;
        }
        /// <summary>
        /// 获得用户权限
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public int GetUserLevel(string strUserName)
        {
            int int_Level = 1;
            foreach (string _name in _UserInfo.Keys)
            {

                StUserInfo _TmpUser = _UserInfo[_name];
                if (_TmpUser.UserName == strUserName)
                    int_Level = _TmpUser.Level;
            }
            return int_Level;
        }

    }
}
