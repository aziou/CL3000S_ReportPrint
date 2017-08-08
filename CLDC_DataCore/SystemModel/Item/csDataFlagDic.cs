using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using System.Xml;
using CLDC_DataCore.DataBase;
using System.Windows.Forms;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// �������������ݱ�ʶģ��
    /// ��    �ߣ�zzg soinlove@126.com
    /// ��д���ڣ�2013-03-06
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    public class csDataFlag
    {
        #region--------------˽�б���-----------------
        private Dictionary<string, StDataFlagInfo> m_Dic_DataFlagInfo;
        #endregion------------------------------------

        #region--------------���캯��-----------------
        /// <summary>
        /// ���캯��
        /// </summary>
        public csDataFlag()
        {
            m_Dic_DataFlagInfo = new Dictionary<string, StDataFlagInfo>();
        }

        /// <summary>
        /// �������� 
        /// </summary>
        ~csDataFlag()
        {
            m_Dic_DataFlagInfo = null;
        }

        #endregion------------------------------------

        #region--------------��������-----------------
        /// <summary>
        /// ��ȡ��ʼ�����ݱ�ʶģ��
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            m_Dic_DataFlagInfo.Clear();           //������ݱ�ʶ����
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_DATAFLAG_DICT, out _ErrorString);
            if (_ErrorString != "")
            {
                _XmlNode = clsXmlControl.CreateXmlNode("DataFlagInfo");
                XmlNode _XmlChildNode = clsXmlControl.CreateXmlNode("R",
                                                                    "DataFlagName", "���",
                                                                    "DataFlag", "04000402",
                                                                    "DataLength", "6",
                                                                    "DataSamllNumber", "0",                    
                                                                    "DataFormat", "NNNNNNNNNNNN");

                _XmlNode.AppendChild(_XmlChildNode);
                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_DATAFLAG_DICT);
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StDataFlagInfo sci_CarrierInfo = new StDataFlagInfo();

                sci_CarrierInfo.DataFlagName = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                sci_CarrierInfo.DataFlag = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                sci_CarrierInfo.DataLength = _XmlNode.ChildNodes[_i].Attributes[2].Value;
                sci_CarrierInfo.DataSmallNumber = _XmlNode.ChildNodes[_i].Attributes[3].Value;                
                sci_CarrierInfo.DataFormat = _XmlNode.ChildNodes[_i].Attributes[4].Value;

                m_Dic_DataFlagInfo.Add(sci_CarrierInfo.DataFlagName, sci_CarrierInfo);
            }
            return;
        }

        /// <summary>
        /// �洢�ز�����ģ�����ݵ�XML�ĵ�
        /// </summary>
        public void Save()
        {
            clsXmlControl xmlControl = new clsXmlControl();
            xmlControl.appendchild("", "DataFlagInfo");
            foreach (StDataFlagInfo _ci in m_Dic_DataFlagInfo.Values)
            {
                xmlControl.appendchild("", "R",
                                    "DataFlagName",
                                    _ci.DataFlagName,
                                    "DataFlag",
                                    _ci.DataFlag,
                                    "DataLength",
                                    _ci.DataLength,
                                    "DataSmallNumber",
                                    _ci.DataSmallNumber,
                                    "DataFormat",
                                    _ci.DataFormat);
            }
            xmlControl.SaveXml(Application.StartupPath + Const.Variable.CONST_DATAFLAG_DICT);
        }

        /// <summary>
        /// ����һ�����ݱ�ʶ
        /// </summary>
        /// <param name="p_sci_DataFlagInfo">���ݱ�ʶ�ṹ��</param>
        public void Add(StDataFlagInfo p_sci_DataFlagInfo)
        {
            if (p_sci_DataFlagInfo.DataFlagName == "")
            {
                return;
            }
            if (m_Dic_DataFlagInfo.ContainsKey(p_sci_DataFlagInfo.DataFlagName))
            {
                m_Dic_DataFlagInfo[p_sci_DataFlagInfo.DataFlagName] = p_sci_DataFlagInfo;

            }
            else
            {
                m_Dic_DataFlagInfo.Add(p_sci_DataFlagInfo.DataFlagName, p_sci_DataFlagInfo);
            }
            this.Save();        //������ϱ���XML�ĵ�

        }

        /// <summary>
        /// ������ݱ�ʶ�Ƿ����
        /// </summary>
        /// <param name="p_str_DataFlagName">���ݱ�ʶ��</param>
        /// <returns></returns>
        public bool FindDataFlagInfo(string p_str_DataFlagName)
        {
            return m_Dic_DataFlagInfo.ContainsKey(p_str_DataFlagName);
        }

        /// <summary>
        /// �Ƴ�һ�����ݱ�ʶ
        /// </summary>
        /// <param name="p_str_DataFlagName">���ݱ�ʶ��</param>
        public void Remove(string p_str_DataFlagName)
        {
            if (!m_Dic_DataFlagInfo.ContainsKey(p_str_DataFlagName))
                return;
            m_Dic_DataFlagInfo.Remove(p_str_DataFlagName);
            this.Save();
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="p_str_DataFlagName">����������</param>
        /// <returns></returns>
        public int GetDataFlagNo(string p_str_DataFlagName)
        {
            StDataFlagInfo DataFlagInfo = new StDataFlagInfo();
            int iNo = 0;
            foreach (string _name in m_Dic_DataFlagInfo.Keys)
            {
                
                DataFlagInfo = m_Dic_DataFlagInfo[_name];
                if (DataFlagInfo.DataFlagName == p_str_DataFlagName)
                {
                    
                    break;
                }
                iNo++;
            }
            return iNo;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="p_str_DataFlagName">����������</param>
        /// <returns></returns>
        public StDataFlagInfo GetDataFlagInfo(string p_str_DataFlagName)
        {
            StDataFlagInfo DataFlagInfo = new StDataFlagInfo();
            foreach (string _name in m_Dic_DataFlagInfo.Keys)
            {
                DataFlagInfo = m_Dic_DataFlagInfo[_name];
                if (DataFlagInfo.DataFlagName == p_str_DataFlagName)
                    break;
            }
            return DataFlagInfo;
        }
        /// <summary>
        /// ��ȡ�������ݱ�ʶ�б�
        /// </summary>
        /// <returns>����List</returns>
        public List<StDataFlagInfo> GetDataFlagList()
        {
            List<StDataFlagInfo> lst_stDataFlagInfo = new List<StDataFlagInfo>();
            foreach (string _name in m_Dic_DataFlagInfo.Keys)
            {
                StDataFlagInfo stc_tmp = m_Dic_DataFlagInfo[_name];
                lst_stDataFlagInfo.Add(stc_tmp);
            }
            return lst_stDataFlagInfo;
        }
        /// <summary>
        /// ��ȡ���ݱ�ʶ�����б�
        /// </summary>
        /// <returns></returns>
        public List<string> GetDataFlagNameList()
        {
            List<string> lst_stDataFlagInfo = new List<string>();
            foreach (string _name in m_Dic_DataFlagInfo.Keys)
            {
                StDataFlagInfo stc_tmp = m_Dic_DataFlagInfo[_name];
                lst_stDataFlagInfo.Add(stc_tmp.DataFlagName);
            }
            return lst_stDataFlagInfo;
        }
        #endregion------------------------------------
    }
}
