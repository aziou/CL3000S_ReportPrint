using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// �����������������ݱȶ����鷽��
    /// ��    �ߣ�vs
    /// ��д���ڣ�2014-05-07
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    [Serializable]
    public class Plan_Infrared:Plan_Base
    {
        #region--------------˽�б���-----------------
        private List<StPlan_Infrared> m_Lst_InfraredPlan;                              //�������ݱȶԷ����б�
        #endregion------------------------------------

        #region--------------��������-----------------
        /// <summary>
        /// �������ݱȶ���Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return m_Lst_InfraredPlan.Count;
            }
        }
        #endregion------------------------------------

        #region--------------���캯��-----------------
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_int_TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="p_str_PlanName">��������</param>
        public Plan_Infrared(int p_int_TaiType, string p_str_PlanName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_HW_FOLDERNAME, p_int_TaiType, p_str_PlanName)
        {
            this.Load();
        }

        /// <summary>
        /// ��������
        /// </summary>
        ~Plan_Infrared()
        {
            m_Lst_InfraredPlan = null;
        }
        #endregion------------------------------------

        #region--------------˽�к���-----------------
        /// <summary>
        /// ���غ������ݱȶԷ������������ݱȶ������б�
        /// </summary>
        private void Load()
        {
            m_Lst_InfraredPlan = new List<StPlan_Infrared>();
            string str_Error = "";
            XmlNode xmlNode = clsXmlControl.LoadXml(_FAPath, out str_Error);
            if (str_Error != "")
                return;
            for (int _i = 0; _i < xmlNode.ChildNodes.Count; _i++)
            {
                StPlan_Infrared stCarrierPlan = new StPlan_Infrared();                
                stCarrierPlan.str_Name = xmlNode.ChildNodes[_i].Attributes["Name"].Value;
                stCarrierPlan.str_Code = xmlNode.ChildNodes[_i].Attributes["Code"].Value;
                
                m_Lst_InfraredPlan.Add(stCarrierPlan);
            }
        }
        #endregion------------------------------------

        #region--------------��������-----------------
        /// <summary>
        /// �洢�������ݱȶԷ���
        /// </summary>
        public void Save()
        {
            if (m_Lst_InfraredPlan.Count == 0)
                return;
            clsXmlControl xmlNode = new clsXmlControl();
            xmlNode.appendchild("", "Infrared", "Name", Name);
            for (int _i = 0; _i < m_Lst_InfraredPlan.Count; _i++)
            {
                xmlNode.appendchild(""
                                    , "R"                                    
                                    , "Name"
                                    , m_Lst_InfraredPlan[_i].str_Name
                                    , "Code"
                                    , m_Lst_InfraredPlan[_i].str_Code
                                    , "Times");
            }
            xmlNode.SaveXml(_FAPath);
        }


        /// <summary>
        /// ���һ���������ݱȶ���Ŀ
        /// </summary>
        /// <param name="p_int_Order"></param>
        /// <param name="p_str_Type">�������ݱȶ���������</param>
        /// <param name="p_str_Name">��Ŀ����</param>
        /// <param name="p_strCode">��ʶ��</param>
        /// <returns></returns>
        public bool Add(int p_int_Order,  string p_str_Name, string p_strCode)
        {
            StPlan_Infrared scp_Item = new StPlan_Infrared();            
            scp_Item.str_Name = p_str_Name;
            scp_Item.str_Code = p_strCode;
            

            if (m_Lst_InfraredPlan.Contains(scp_Item))
                Move(p_int_Order, scp_Item);
            else
                m_Lst_InfraredPlan.Insert(p_int_Order, scp_Item);
            return true;
        }

        /// <summary>
        /// ��ȡ�������ݱȶ���Ŀ
        /// </summary>
        /// <param name="p_int_index">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_Infrared GetCarrierPrj(int p_int_index)
        {
            if (p_int_index >= m_Lst_InfraredPlan.Count)
                return new StPlan_Infrared();
            return m_Lst_InfraredPlan[p_int_index];
        }

        /// <summary>
        /// �ƶ��������ݱȶ���Ŀ
        /// </summary>
        /// <param name="p_int_MoveToIndex">��Ҫ�ƶ������б�λ��</param>
        /// <param name="p_scp_Item">��Ŀ�ṹ��</param>
        public void Move(int p_int_MoveToIndex, StPlan_Infrared p_scp_Item)
        {
            p_int_MoveToIndex = p_int_MoveToIndex < 0 ? 0 : p_int_MoveToIndex;
            p_int_MoveToIndex = p_int_MoveToIndex >= m_Lst_InfraredPlan.Count ? m_Lst_InfraredPlan.Count - 1 : p_int_MoveToIndex;
            this.Remove(p_scp_Item);
            m_Lst_InfraredPlan.Insert(p_int_MoveToIndex, p_scp_Item);
            return;
        }

        /// <summary>
        /// �Ƴ�ȫ����Ŀ
        /// </summary>
        public void RemoveAll()
        {
            m_Lst_InfraredPlan.Clear();
        }


        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="p_int_Index">��Ŀ������</param>
        public void RemoveAt(int p_int_Index)
        {
            if (p_int_Index < 0 || p_int_Index >= m_Lst_InfraredPlan.Count)
                return;
            m_Lst_InfraredPlan.RemoveAt(p_int_Index);
            return;
        }

        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_Infrared p_scp_Item)
        {
            if (!m_Lst_InfraredPlan.Contains(p_scp_Item))
                return;
            m_Lst_InfraredPlan.Remove(p_scp_Item);
            return;
        }
        #endregion------------------------------------
    }
}
