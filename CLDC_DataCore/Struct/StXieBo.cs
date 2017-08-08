using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 谐波参数结构体
    /// </summary>
    [Serializable()]
    public class StXieBo
    {
        /// <summary>
        /// 元件（A，B,C）不存在合元，取值范围应该在2，3，4
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerYuanJian YuanJian;

        /// <summary>
        /// 是否是电压（真为电压，假为电流）
        /// </summary>
        public bool IsUb;

        /// <summary>
        /// 谐波次数
        /// </summary>
        public int Num;

        /// <summary>
        /// 幅度（不超过40%）
        /// </summary>
        public float Extent;

        /// <summary>
        /// 谐波角度
        /// </summary>
        public float Xw;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StXieBo Clone()
        {
            StXieBo Item = new StXieBo();
            Item.YuanJian = this.YuanJian;
            Item.IsUb = this.IsUb;
            Item.Num = this.Num;
            Item.Extent = this.Extent;
            Item.Xw = this.Xw;
            return Item;
        }

    }
}
