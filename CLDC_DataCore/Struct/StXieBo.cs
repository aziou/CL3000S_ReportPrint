using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// г�������ṹ��
    /// </summary>
    [Serializable()]
    public class StXieBo
    {
        /// <summary>
        /// Ԫ����A��B,C�������ں�Ԫ��ȡֵ��ΧӦ����2��3��4
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerYuanJian YuanJian;

        /// <summary>
        /// �Ƿ��ǵ�ѹ����Ϊ��ѹ����Ϊ������
        /// </summary>
        public bool IsUb;

        /// <summary>
        /// г������
        /// </summary>
        public int Num;

        /// <summary>
        /// ���ȣ�������40%��
        /// </summary>
        public float Extent;

        /// <summary>
        /// г���Ƕ�
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
