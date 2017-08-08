using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm
{

    /// <summary>
    /// 保存登陆以前设置的数据信息以及登录者身份信息
    /// </summary>
    [Serializable()]
    public class LoginSettingData : CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// 公共的、静态的数据
        /// 在登陆时设值、 创建方案时取值
        /// </summary>
        public static LoginSettingData LoginSetting = null ;

        /// <summary>
        /// 过压保护值
        /// </summary>
        public  float MaxDianLiu = 0F;

        /// <summary>
        /// 过压保护值字符串
        /// </summary>
        public string  StrMaxDianLiu = "";

        /// <summary>
        /// 过流保护值
        /// </summary>
        public  float MaxDianYa = 0F;

        /// <summary>
        ///  过流保护值字符串
        /// </summary>
        public string  StrMaxDianYa = "";

        /// <summary>
        /// 检定员
        /// </summary>
        public  string JianDY_Name = string.Empty;

        /// <summary>
        /// 核定员
        /// </summary>
        public  string HeDY_Name = string.Empty;

        /// <summary>
        /// 检定员密码
        /// </summary>
        public  string JianDY_Pass = string.Empty;

        /// <summary>
        /// 核定员密码
        /// </summary>
        public  string HeDY_Pass = string.Empty;

        /// <summary>
        /// 是否使用演示模式，true演示模式、false正常模式
        /// </summary>
        public  bool IsUseYanShiMode = false;



    }



}
