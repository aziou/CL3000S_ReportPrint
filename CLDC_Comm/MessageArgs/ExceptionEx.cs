
#region FileHeader And Descriptions
// ***************************************************************
//  ExceptionEx   date: 10/26/2009 By Niaoked
//  -------------------------------------------------------------
//  Description:
//  检定器异常类。用于检定器内部抛出错误
//  -------------------------------------------------------------
//  Copyright (C) 2009 -CdClou All Rights Reserved
// ***************************************************************
// Modify Log:
// 10/26/2009 09-42-57    Created
// ***************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;
namespace CLDC_Comm.MessageArgs
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionEx : Exception
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public int ErrorID = 0;

        /// <summary>
        /// 错误描述，通过错误ID
        /// </summary>
        private static Dictionary<int, string> m_lstErrors = new Dictionary<int, string>();
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ErrID">错误ID，与系统错误对照表对应。</param>
        public ExceptionEx(int ErrID)
            : base()
        {
            ErrorID = ErrID;
            InitDefaultLanguage();  //初始化语言包
        }
        /// <summary>
        /// 获取错误描述
        /// </summary>
        public override string Message
        {
            get
            {
                if (m_lstErrors.ContainsKey(ErrorID))
                {
                    return m_lstErrors[ErrorID];
                }
                else
                {
                    return String.Format("暂无错误描述：错误代码：{0}", ErrorID);
                }
            }
        }
        /// <summary>
        /// 初始化错误描述内容
        /// </summary>
        private void InitDefaultLanguage()
        {
            
            if (m_lstErrors.Count == 0)
            {
                #region 逻辑错误描述,错误代码:1000---1200
                m_lstErrors.Add((int)Cus_ErrorID.ERR_LOGIC_PLANCOUNTOUTOFRANGE, "方案下标越界!");     
                #endregion
                #region 检定参数(方案)错误描述，错误代码:1201----1500
                #endregion
                #region 设备控制错误描述，错误代码：2000---2200
                #endregion
                #region 485控制错误描述，错误代码 2201-2500
#endregion
                #region 检定控制错误描述，错误代码3000---3500
#endregion

            }
        }

    }
}
