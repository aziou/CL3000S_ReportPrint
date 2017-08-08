using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.DataBase;
using System.Xml;
using CLDC_DataCore.Struct;
namespace CLDC_DataCore.SystemModel
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 系统配置模型
        /// </summary>
        public Item.SystemConfigure SystemMode;//实验方法与依据、实验参数
        /// <summary>
        /// 用户信息集合
        /// </summary>
        public Item.csUserInfo UserGroup;
        /// <summary>
        /// 字典信息集合
        /// </summary>
        public Item.csDictionary ZiDianGroup;
        /// <summary>
        /// 功率因素字典集合
        /// </summary>
        public Item.csGlys GlysZiDian;
        /// <summary>
        /// 电流倍数字典
        /// </summary>
        public Item.csxIbDic xIbDic;
        /// <summary>
        /// 
        /// </summary>
        public Item.csDgnDic DgnDicInfo;
        /// <summary>
        /// 载波方案配置集合
        /// </summary>
        public Item.csCarrier ZaiBoInfo;
        /// <summary>
        /// 数据标识信息
        /// </summary>
        public Item.csDataFlag DataFlagInfo;
        /// <summary>
        /// 列显示信息
        /// </summary>
        public Item.csColsVisiable ColsVisiable;
        /// <summary>
        /// 实验方法与依据
        /// </summary>
        public Item.MethodAndBasis methodAndBasis;
        /// <summary>
        /// 实验参数
        /// </summary>
        public Item.TestSetting testSetting;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemInfo()
        {
            SystemMode = new Item.SystemConfigure();
            UserGroup = new Item.csUserInfo();
            ZiDianGroup = new Item.csDictionary();
            GlysZiDian = new Item.csGlys();
            xIbDic = new Item.csxIbDic();
            DgnDicInfo = new Item.csDgnDic();
            ZaiBoInfo = new Item.csCarrier();
            DataFlagInfo = new Item.csDataFlag();
            ColsVisiable = new Item.csColsVisiable();
            methodAndBasis = new Item.MethodAndBasis();
            testSetting = new Item.TestSetting();
        }
        /// <summary>
        /// 
        /// </summary>
        ~SystemInfo()
        {
            UserGroup = null;
            SystemMode = null;
            ZiDianGroup = null;
            GlysZiDian = null;
            xIbDic = null;
            DgnDicInfo = null;
            ZaiBoInfo = null;
            DataFlagInfo = null;
            ColsVisiable = null;
            methodAndBasis = null;
            testSetting = null;
        }

        /// <summary>
        /// 初始化系统模型
        /// </summary>
        public void Load()
        {
            SystemMode.Load();
            UserGroup.Load();
            ZiDianGroup.Load();
            GlysZiDian.Load();
            xIbDic.Load();
            DgnDicInfo.Load();
            ZaiBoInfo.Load();
            DataFlagInfo.Load();
            ColsVisiable.Load();
            methodAndBasis.Load();
            testSetting.Load();
        }

        /// <summary>
        /// 存储XML系统模型
        /// </summary>
        public void Save()
        {
            SystemMode.Save();
            UserGroup.Save();
            ZiDianGroup.Save();
            GlysZiDian.Save();
            xIbDic.Save();
            DgnDicInfo.Save();
            ZaiBoInfo.Save();
            DataFlagInfo.Save();
            ColsVisiable.Save();
            methodAndBasis.Save();
            testSetting.Save();
        }

        /// <summary>
        /// 系统登陆验证
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Pwd">密码</param>
        /// <param name="_UserInfo">输出用户信息结构体</param>
        /// <returns>返回验证成功或失败</returns>
        public bool CheckIn(string UserName, string Pwd, out Struct.StUserInfo _UserInfo)
        {
            return UserGroup.CheckIn(UserName, Pwd, out _UserInfo);
        }


    }
}
