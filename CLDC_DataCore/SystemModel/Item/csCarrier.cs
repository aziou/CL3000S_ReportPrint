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
    /// �����������ز�����ģ��
    /// ��    �ߣ�zzg soinlove@126.com
    /// ��д���ڣ�2013-03-06
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    public class csCarrier
    {
        #region--------------˽�б���-----------------
        private Dictionary<string, StCarrierInfo> m_Dic_CarrierInfo;
        #endregion------------------------------------

        #region--------------���캯��-----------------
        /// <summary>
        /// ���캯��
        /// </summary>
        public csCarrier()
        {
            m_Dic_CarrierInfo = new Dictionary<string, StCarrierInfo>();
        }

        /// <summary>
        /// �������� 
        /// </summary>
        ~csCarrier()
        {
            m_Dic_CarrierInfo = null;
        }

        #endregion------------------------------------

        #region--------------��������-----------------
        /// <summary>
        /// ��ȡ��ʼ���ز�����ģ��
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            m_Dic_CarrierInfo.Clear();           //����ز���������
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_CARRIER, out _ErrorString);
            if (_ErrorString != "")
            {
                _XmlNode = clsXmlControl.CreateXmlNode("CarrierInfo");
                XmlNode _XmlChildNode = clsXmlControl.CreateXmlNode("R",
                                                                    "CarrierName", "��׼�ز�����",
                                                                    "CarrierType", "2041",
                                                                    "RdType", "����",
                                                                    "CommuType", "COM",
                                                                    "BaudRate", "9600,n,8,1",
                                                                    "Comm", "COM1",
                                                                    "CmdTime", "10",
                                                                    "ByteTime", "10",
                                                                    "RouterID", "0");

                _XmlNode.AppendChild(_XmlChildNode);
                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_CARRIER);
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StCarrierInfo sci_CarrierInfo = new StCarrierInfo();

                sci_CarrierInfo.CarrierName = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                sci_CarrierInfo.CarrierType = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                sci_CarrierInfo.RdType = _XmlNode.ChildNodes[_i].Attributes[2].Value;
                sci_CarrierInfo.CommuType = _XmlNode.ChildNodes[_i].Attributes[3].Value;                
                sci_CarrierInfo.BaudRate = _XmlNode.ChildNodes[_i].Attributes[4].Value;
                sci_CarrierInfo.Comm = _XmlNode.ChildNodes[_i].Attributes[5].Value;
                sci_CarrierInfo.CmdTime = _XmlNode.ChildNodes[_i].Attributes[6].Value;
                sci_CarrierInfo.ByteTime = _XmlNode.ChildNodes[_i].Attributes[7].Value;
                if (_XmlNode.ChildNodes[_i].Attributes.Count > 8)
                {
                    sci_CarrierInfo.RouterID = byte.Parse(_XmlNode.ChildNodes[_i].Attributes[8].Value);
                }
                m_Dic_CarrierInfo.Add(sci_CarrierInfo.CarrierName, sci_CarrierInfo);
            }
            return;
        }

        /// <summary>
        /// �洢�ز�����ģ�����ݵ�XML�ĵ�
        /// </summary>
        public void Save()
        {
            clsXmlControl xmlControl = new clsXmlControl();
            xmlControl.appendchild("", "CarrierInfo");
            foreach (StCarrierInfo _ci in m_Dic_CarrierInfo.Values)
            {
                xmlControl.appendchild("", "R",
                                    "CarrierName",
                                    _ci.CarrierName,
                                    "CarrierType",
                                    _ci.CarrierType,
                                    "RdType",
                                    _ci.RdType,
                                    "CommuType",
                                    _ci.CommuType,
                                    "BaudRate",
                                    _ci.BaudRate,
                                    "Comm",
                                    _ci.Comm,
                                    "CmdTime",
                                    _ci.CmdTime,
                                    "ByteTime",
                                    _ci.ByteTime,
                                    "RouterID",
                                    _ci.RouterID.ToString("D"));
            }
            xmlControl.SaveXml(Application.StartupPath + Const.Variable.CONST_CARRIER);
        }

        /// <summary>
        /// ����һ���ز�����
        /// </summary>
        /// <param name="p_sci_CarrierInfo">�ز������ṹ��</param>
        public void Add(StCarrierInfo p_sci_CarrierInfo)
        {
            if (p_sci_CarrierInfo.CarrierName == "")
            {
                return;
            }
            if (m_Dic_CarrierInfo.ContainsKey(p_sci_CarrierInfo.CarrierName))
            {
                m_Dic_CarrierInfo[p_sci_CarrierInfo.CarrierName] = p_sci_CarrierInfo;

            }
            else
            {
                m_Dic_CarrierInfo.Add(p_sci_CarrierInfo.CarrierName, p_sci_CarrierInfo);
            }
            this.Save();        //������ϱ���XML�ĵ�

        }

        /// <summary>
        /// ����ز��Ƿ����
        /// </summary>
        /// <param name="p_str_CarrierName">�ز�������</param>
        /// <returns></returns>
        public bool FindCarrierInfo(string p_str_CarrierName)
        {
            return m_Dic_CarrierInfo.ContainsKey(p_str_CarrierName);
        }

        /// <summary>
        /// �Ƴ�һ���ز�����
        /// </summary>
        /// <param name="p_str_CarrierName">�ز�������</param>
        public void Remove(string p_str_CarrierName)
        {
            if (!m_Dic_CarrierInfo.ContainsKey(p_str_CarrierName))
                return;
            m_Dic_CarrierInfo.Remove(p_str_CarrierName);
            this.Save();
        }

        /// <summary>
        /// ��ȡ�����ز������б�
        /// </summary>
        /// <returns>����List</returns>
        public List<StCarrierInfo> GetCarrierList()
        {
            List<StCarrierInfo> lst_stCarrierInfo = new List<StCarrierInfo>();
            foreach (string _name in m_Dic_CarrierInfo.Keys)
            {
                StCarrierInfo stc_tmp = m_Dic_CarrierInfo[_name];
                lst_stCarrierInfo.Add(stc_tmp);
            }
            return lst_stCarrierInfo;
        }
        #endregion------------------------------------
    }
}
