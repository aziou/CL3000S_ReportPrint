using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// PCODE��Ӧ��
    /// </summary>
    [Serializable()]
    public class StPCodeDicForMis
    {
        public Dictionary<string, string> DicPCode = new Dictionary<string, string>();

        /// <summary>
        /// ��ȡcode
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
        /// ��ȡ����
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
