using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;

namespace CLDC_Comm.Command
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class CheckNow : CTNPCommand
    {
        /// <summary>
        /// �����������ֶ�
        /// </summary>
        public int ItemID = 0;
        /// <summary>
        /// ���캯��
        /// </summary>
        public CheckNow()
        {
        }
    }

}
