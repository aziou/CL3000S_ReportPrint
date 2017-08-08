using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// �����������ز����鷽��
    /// ��    �ߣ�vs
    /// ��д���ڣ�2010-09-06
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    [Serializable]
    public class Plan_Carrier:Plan_Base
    {
        #region--------------˽�б���-----------------
        private List<StPlan_Carrier> m_Lst_CarrierPlan;                              //�ز������б�
        #endregion------------------------------------

        #region--------------��������-----------------
        /// <summary>
        /// �ز���Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return m_Lst_CarrierPlan.Count;
            }
        }
        #endregion------------------------------------

        #region--------------���캯��-----------------
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_int_TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="p_str_PlanName">��������</param>
        public Plan_Carrier(int p_int_TaiType, string p_str_PlanName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_ZB_FOLDERNAME, p_int_TaiType, p_str_PlanName)
        {
            this.Load();
        }

        /// <summary>
        /// ��������
        /// </summary>
        ~Plan_Carrier()
        {
            m_Lst_CarrierPlan = null;
        }
        #endregion------------------------------------

        #region--------------˽�к���-----------------
        /// <summary>
        /// �����ز��������ز������б�
        /// </summary>
        private void Load()
        {
            m_Lst_CarrierPlan = new List<StPlan_Carrier>();
            string str_Error = "";
            XmlNode xmlNode = clsXmlControl.LoadXml(_FAPath, out str_Error);
            if (str_Error != "")
                return;
            for (int _i = 0; _i < xmlNode.ChildNodes.Count; _i++)
            {
                StPlan_Carrier stCarrierPlan = new StPlan_Carrier();                
                stCarrierPlan.str_Name = xmlNode.ChildNodes[_i].Attributes["Name"].Value;
                stCarrierPlan.str_Code = xmlNode.ChildNodes[_i].Attributes["Code"].Value;
                if (xmlNode.ChildNodes[_i].Attributes["Times"] != null)
                    stCarrierPlan.int_Times = int.Parse(xmlNode.ChildNodes[_i].Attributes["Times"].Value.ToString());
                if (xmlNode.ChildNodes[_i].Attributes["Interval"] != null)
                    stCarrierPlan.int_Interval = int.Parse(xmlNode.ChildNodes[_i].Attributes["Interval"].Value.ToString());
                if (xmlNode.ChildNodes[_i].Attributes["ModuleSwaps"] != null)
                    stCarrierPlan.b_ModuleSwaps = xmlNode.ChildNodes[_i].Attributes["ModuleSwaps"].Value.ToString() == "1" ? true : false;
                m_Lst_CarrierPlan.Add(stCarrierPlan);
            }
        }
        #endregion------------------------------------

        #region--------------��������-----------------
        /// <summary>
        /// �洢�ز�����
        /// </summary>
        public void Save()
        {
            if (m_Lst_CarrierPlan.Count == 0)
                return;
            clsXmlControl xmlNode = new clsXmlControl();
            xmlNode.appendchild("", "Carrier", "Name", Name);
            for (int _i = 0; _i < m_Lst_CarrierPlan.Count; _i++)
            {
                xmlNode.appendchild(""
                                    , "R"                                    
                                    , "Name"
                                    , m_Lst_CarrierPlan[_i].str_Name
                                    , "Code"
                                    , m_Lst_CarrierPlan[_i].str_Code
                                    , "Times"
                                    ,m_Lst_CarrierPlan[_i].int_Times.ToString()
                                    , "Interval"
                                    ,m_Lst_CarrierPlan[_i].int_Interval.ToString()
                                    , "ModuleSwaps"
                                    ,m_Lst_CarrierPlan[_i].b_ModuleSwaps ? "1":"0");
            }
            xmlNode.SaveXml(_FAPath);
        }


        /// <summary>
        /// ���һ���ز���Ŀ
        /// </summary>
        /// <param name="p_int_Order"></param>
        /// <param name="p_str_Type">�ز���������</param>
        /// <param name="p_str_Name">��Ŀ����</param>
        /// <param name="p_strCode">��ʶ��</param>
        /// <param name="p_int_Times">���ʹ���</param>
        /// <param name="p_flt_Succ">�ɹ���</param>
        /// <param name="p_int_Interval">���ʱ��(��)</param>
        /// <returns></returns>
        public bool Add(int p_int_Order, string p_str_Name, string p_strCode, bool b_ModuleSwaps)
        {
            StPlan_Carrier scp_Item = new StPlan_Carrier();            
            scp_Item.str_Name = p_str_Name;
            scp_Item.str_Code = p_strCode;
            scp_Item.b_ModuleSwaps = b_ModuleSwaps;

            if (b_ModuleSwaps)
            {
                scp_Item.int_Times = 5;
                scp_Item.int_Interval = 10;
            }

            if (m_Lst_CarrierPlan.Contains(scp_Item))
                Move(p_int_Order, scp_Item);
            else
                m_Lst_CarrierPlan.Insert(p_int_Order, scp_Item);
            return true;
        }

        /// <summary>
        /// ��ȡ�ز���Ŀ
        /// </summary>
        /// <param name="p_int_index">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_Carrier GetCarrierPrj(int p_int_index)
        {
            if (p_int_index >= m_Lst_CarrierPlan.Count)
                return new StPlan_Carrier();
            return m_Lst_CarrierPlan[p_int_index];
        }

        /// <summary>
        /// �ƶ��ز���Ŀ
        /// </summary>
        /// <param name="p_int_MoveToIndex">��Ҫ�ƶ������б�λ��</param>
        /// <param name="p_scp_Item">��Ŀ�ṹ��</param>
        public void Move(int p_int_MoveToIndex, StPlan_Carrier p_scp_Item)
        {
            p_int_MoveToIndex = p_int_MoveToIndex < 0 ? 0 : p_int_MoveToIndex;
            p_int_MoveToIndex = p_int_MoveToIndex >= m_Lst_CarrierPlan.Count ? m_Lst_CarrierPlan.Count - 1 : p_int_MoveToIndex;
            this.Remove(p_scp_Item);
            m_Lst_CarrierPlan.Insert(p_int_MoveToIndex, p_scp_Item);
            return;
        }

        /// <summary>
        /// �Ƴ�ȫ����Ŀ
        /// </summary>
        public void RemoveAll()
        {
            m_Lst_CarrierPlan.Clear();
        }


        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="p_int_Index">��Ŀ������</param>
        public void RemoveAt(int p_int_Index)
        {
            if (p_int_Index < 0 || p_int_Index >= m_Lst_CarrierPlan.Count)
                return;
            m_Lst_CarrierPlan.RemoveAt(p_int_Index);
            return;
        }

        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_Carrier p_scp_Item)
        {
            if (!m_Lst_CarrierPlan.Contains(p_scp_Item))
                return;
            m_Lst_CarrierPlan.Remove(p_scp_Item);
            return;
        }
        #endregion------------------------------------
    }
}
