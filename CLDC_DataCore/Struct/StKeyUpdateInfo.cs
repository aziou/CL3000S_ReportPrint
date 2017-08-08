using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 密钥信息
    /// </summary>
    [Serializable()]
    public struct StKeyUpdateInfo
    {
        /// <summary>
        /// 本地表
        /// </summary>
        public bool bLocalMeter;
        /// <summary>
        /// 是否获取密文成功
        /// </summary>
        public bool bGetKeyInfoSucc;
        /// <summary>
        /// 是否公钥状态下更新
        /// </summary>
        public bool bUpdateKeyPublic;
        /// <summary>
        /// 分散因子
        /// </summary>
        public string MeterDiv;

        /// <summary>
        /// 随机数
        /// </summary>
        public string MeterRand;

        /// <summary>
        /// ESAM序列号
        /// </summary>
        public string MeterEsamNo;

        /// <summary>
        ///       
        /// </summary>
        public string 主控密钥明文;

        /// <summary>
        /// 
        /// </summary>
        public string 远程密钥明文;

        /// <summary>
        /// 
        /// </summary>
        public string 参数密钥明文;

        /// <summary>
        /// 
        /// </summary>
        public string 身份密钥明文;

        /// <summary>
        /// 
        /// </summary>
        public string 主控密钥密文;

        /// <summary>
        /// 
        /// </summary>
        public string 主控密钥信息;

        /// <summary>
        /// 
        /// </summary>
        public string 远程密钥密文;

        /// <summary>
        /// 
        /// </summary>
        public string 远程密钥信息;

        /// <summary>
        /// 
        /// </summary>
        public string 参数密钥密文;

        /// <summary>
        /// 
        /// </summary>
        public string 参数密钥信息;

        /// <summary>
        /// 
        /// </summary>
        public string 身份密钥密文;

        /// <summary>
        /// 
        /// </summary>
        public string 身份密钥信息;
    }
}
