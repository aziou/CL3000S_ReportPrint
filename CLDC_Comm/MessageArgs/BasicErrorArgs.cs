using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.MessageArgs
{
    /// <summary>
    /// ���������Ϣ
    /// </summary>
    public class BasicErrorArgs:EventArgs
    {
        //���һ���������
        private string[] _LastverifyData;
        /// <summary>
        /// ������
        /// </summary>
        public bool[] Result;
        /// <summary>
        /// ������
        /// </summary>
        public int[] WcCount;
        //�����������
        private List<string[]> _VerifyData = new List<string[]>();
        //��ǰ�춨��ĿID
        /// <summary>
        /// ���һ���������
        /// </summary>
        public string[] LastVerifyData
        {
            set { _LastverifyData = value; }
            get { return _LastverifyData; }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public List<string[]> VerifyData
        {
            get { return _VerifyData; }
            //set { _VerifyData.Add(value); }
        }

    }
}
