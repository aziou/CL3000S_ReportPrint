using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.MessageArgs
{
    /// <summary>
    /// 误差数据消息
    /// </summary>
    public class BasicErrorArgs:EventArgs
    {
        //最后一次误差数据
        private string[] _LastverifyData;
        /// <summary>
        /// 误差结论
        /// </summary>
        public bool[] Result;
        /// <summary>
        /// 误差次数
        /// </summary>
        public int[] WcCount;
        //所有误差数据
        private List<string[]> _VerifyData = new List<string[]>();
        //当前检定项目ID
        /// <summary>
        /// 最后一次误差数据
        /// </summary>
        public string[] LastVerifyData
        {
            set { _LastverifyData = value; }
            get { return _LastverifyData; }
        }
        /// <summary>
        /// 返回所有误差
        /// </summary>
        public List<string[]> VerifyData
        {
            get { return _VerifyData; }
            //set { _VerifyData.Add(value); }
        }

    }
}
