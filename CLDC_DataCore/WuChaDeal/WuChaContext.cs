
#region FileHeader And Descriptions
// ***************************************************************
//  WuChaContext   date: 11/06/2009 By Niaoked
//  -------------------------------------------------------------
//  Description:
//  误差计算器上下文
//  -------------------------------------------------------------
//  Copyright (C) 2009 -CdClou All Rights Reserved
// ***************************************************************
// Modify Log:
// 11/06/2009 09-17-08    Created
// ***************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// 可能出现的试验类型,不够可以在下面添加
    /// </summary>
    public enum WuChaType
    {
        错误类型=0,
        基本误差 = 1,
        标准偏差 = 2,
        走字误差之标准表法 = 3,
        走字误差之走字试验法 = 4,
        走字误差之计读脉冲法 = 5,
        走字误差之组合误差=6,
        多功能_需量误差 = 7,
        多功能_日计时误差=8,
        特殊检定 = 10,
        一致性误差 = 11,
    }

    public class WuChaContext
    {
        WuChaBase curWuCha = null;

        //构造:简单工厂模式
        public WuChaContext(WuChaType WCType, CLDC_DataCore.Struct.StWuChaDeal wuChaPara)
        {
            switch (WCType)
            {
                case WuChaType.基本误差:
                    {
                        curWuCha = new BasicError(wuChaPara);
                        break;
                    }
                case WuChaType.特殊检定:
                    {
                        curWuCha = new BasicError(wuChaPara);
                        break;
                    }
                case WuChaType.标准偏差:
                    {
                        curWuCha = new WindageError(wuChaPara);
                        break;
                    }
                case WuChaType.走字误差之标准表法:
                    {
                        curWuCha = new ZZError(wuChaPara);
                        break;
                    }
                case WuChaType.走字误差之计读脉冲法:
                    {
                        curWuCha = new ZZError(wuChaPara);
                        break;
                    }
                case WuChaType.走字误差之走字试验法:
                    {
                        curWuCha = new ZZError(wuChaPara);
                        break;
                    }
                case WuChaType.走字误差之组合误差:
                    {
                        curWuCha = new ZZError(wuChaPara); 
                        break;
                    }
                case WuChaType.多功能_需量误差:
                    {
                        curWuCha = new MaxXL(wuChaPara); 
                        break;
                    }
                case WuChaType.多功能_日计时误差:
                    {
                        curWuCha = new RJSError(wuChaPara);
                        break;
                    }
                case WuChaType.一致性误差:
                    {
                        curWuCha = new AccordError(wuChaPara);
                        break;
                    }
            }
        }

        //返回计算结果
        public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase GetResult(params float[] arrNumber)
        {
            return curWuCha.SetWuCha(arrNumber);
        }

        /// <summary>
        /// 返回其它需要处理的数据
        /// </summary>
        public string OtherData
        {
            get { return curWuCha.OtherData; }
            set { curWuCha.OtherData = value; }
        }
    }
}
