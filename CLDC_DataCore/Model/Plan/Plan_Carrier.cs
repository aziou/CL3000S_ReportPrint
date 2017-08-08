using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 功能描述：载波试验方案
    /// 作    者：vs
    /// 编写日期：2010-09-06
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    [Serializable]
    public class Plan_Carrier:Plan_Base
    {
        #region--------------私有变量-----------------
        private List<StPlan_Carrier> m_Lst_CarrierPlan;                              //载波方案列表
        #endregion------------------------------------

        #region--------------公共属性-----------------
        /// <summary>
        /// 载波项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return m_Lst_CarrierPlan.Count;
            }
        }
        #endregion------------------------------------

        #region--------------构造函数-----------------
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_int_TaiType">台体类型0-三相台，1-单向台</param>
        /// <param name="p_str_PlanName">方案名称</param>
        public Plan_Carrier(int p_int_TaiType, string p_str_PlanName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_ZB_FOLDERNAME, p_int_TaiType, p_str_PlanName)
        {
            this.Load();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~Plan_Carrier()
        {
            m_Lst_CarrierPlan = null;
        }
        #endregion------------------------------------

        #region--------------私有函数-----------------
        /// <summary>
        /// 加载载波方案到载波数据列表
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

        #region--------------公共函数-----------------
        /// <summary>
        /// 存储载波方案
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
        /// 添加一个载波项目
        /// </summary>
        /// <param name="p_int_Order"></param>
        /// <param name="p_str_Type">载波试验类型</param>
        /// <param name="p_str_Name">项目名称</param>
        /// <param name="p_strCode">标识符</param>
        /// <param name="p_int_Times">发送次数</param>
        /// <param name="p_flt_Succ">成功率</param>
        /// <param name="p_int_Interval">间隔时间(秒)</param>
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
        /// 获取载波项目
        /// </summary>
        /// <param name="p_int_index">项目列表索引</param>
        /// <returns></returns>
        public StPlan_Carrier GetCarrierPrj(int p_int_index)
        {
            if (p_int_index >= m_Lst_CarrierPlan.Count)
                return new StPlan_Carrier();
            return m_Lst_CarrierPlan[p_int_index];
        }

        /// <summary>
        /// 移动载波项目
        /// </summary>
        /// <param name="p_int_MoveToIndex">需要移动到的列表位置</param>
        /// <param name="p_scp_Item">项目结构体</param>
        public void Move(int p_int_MoveToIndex, StPlan_Carrier p_scp_Item)
        {
            p_int_MoveToIndex = p_int_MoveToIndex < 0 ? 0 : p_int_MoveToIndex;
            p_int_MoveToIndex = p_int_MoveToIndex >= m_Lst_CarrierPlan.Count ? m_Lst_CarrierPlan.Count - 1 : p_int_MoveToIndex;
            this.Remove(p_scp_Item);
            m_Lst_CarrierPlan.Insert(p_int_MoveToIndex, p_scp_Item);
            return;
        }

        /// <summary>
        /// 移除全部项目
        /// </summary>
        public void RemoveAll()
        {
            m_Lst_CarrierPlan.Clear();
        }


        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="p_int_Index">项目索引号</param>
        public void RemoveAt(int p_int_Index)
        {
            if (p_int_Index < 0 || p_int_Index >= m_Lst_CarrierPlan.Count)
                return;
            m_Lst_CarrierPlan.RemoveAt(p_int_Index);
            return;
        }

        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
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
