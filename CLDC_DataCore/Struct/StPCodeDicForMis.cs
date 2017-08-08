using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// PCODE对应表
    /// </summary>
    [Serializable()]
    public class StPCodeDicForMis
    {
        public Dictionary<string, string> DicPCode = new Dictionary<string, string>();

        /// <summary>
        /// 获取code
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public string GetPCode(string strName)
        {
            string strCode = "";
            if(DicPCode.Count>0)
            {
                foreach (string Key in DicPCode.Keys)
                {
                    if (DicPCode[Key] == strName)
                        strCode = Key;
                }
            }
            return strCode;
        }
        /// <summary>
        /// 获取名字
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public string GetPName(string strCode)
        {
            string strName = "";
            if (DicPCode.Count > 0)
            {
                foreach (string Key in DicPCode.Keys)
                {
                    if (Key == strCode)
                        strName = DicPCode[Key];
                }
            }
            return strName;
        }
    }
}
