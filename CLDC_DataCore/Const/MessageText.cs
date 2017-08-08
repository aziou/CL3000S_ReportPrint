using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Const
{
    /// <summary>
    /// 提示文本类语言包。默认为中文。如果需要添加其它语言可以在另行添加
    /// </summary>
    public class MessageText
    {

        /// <summary>
        /// 初始化检定参数提示
        /// </summary>
        public const string CTX_INIT = "正在初始化检定参数......";

        /// <summary>
        /// 失败提示文字
        /// </summary>
        public  const string CTX_FAIL = "失败";
        /// <summary>
        /// 成功提示文字
        /// </summary>
        public  const string CTX_SUCCESS = "成功";

        /// <summary>
        /// 工作提示
        /// </summary>
        public  const string CTX_WORKING = "正在检定...";

        /// <summary>
        /// 检定停止提示
        /// </summary>
        public  const string CTX_STOP = "检定停止";
        /// <summary>
        /// 设置误差计算器提示
        /// </summary>
        public  const string CTX_INIT188G = "设置误差计算器参数";
        /// <summary>
        /// 设置脉冲通道
        /// </summary>
        public  const  string CTX_INITPULSE="设置脉冲通道";
         /// <summary>
         /// 设置检定参数
         /// </summary>
        public  const string CTX_INITPRARA = "设置检定参数";
        /// <summary>
        /// 设置检定点
        /// </summary>
        public  const string CTX_SETTESTPOINT = "设置检定点";
    }
}
