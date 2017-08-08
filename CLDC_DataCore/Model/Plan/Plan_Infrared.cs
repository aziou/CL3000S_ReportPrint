using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 功能描述：红外数据比对试验方案
    /// 作    者：vs
    /// 编写日期：2014-05-07
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    [Serializable]
    public class Plan_Infrared:Plan_Base
    {
        #region--------------私有变量-----------------
        private List<StPlan_Infrared> m_Lst_InfraredPlan;                              //红外数据比对方案列表
        #endregion------------------------------------

        #region--------------公共属性-----------------
        /// <summary>
        /// 红外数据比对项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return m_Lst_InfraredPlan.Count;
            }
        }
        #endregion------------------------------------

        #region--------------构造函数-----------------
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_int_TaiType">台体类型0-三相台，1-单向台</param>
        /// <param name="p_str_PlanName">方案名称</param>
        public Plan_Infrared(int p_int_TaiType, string p_str_PlanName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_HW_FOLDERNAME, p_int_TaiType, p_str_PlanName)
        {
            this.Load();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~Plan_Infrared()
        {
            m_Lst_InfraredPlan = null;
        }
        #endregion------------------------------------

        #region--------------私有函数-----------------
        /// <summary>
        /// 加载红外数据比对方案到红外数据比对数据列表
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

        #region--------------公共函数-----------------
        /// <summary>
        /// 存储红外数据比对方案
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
        /// 添加一个红外数据比对项目
        /// </summary>
        /// <param name="p_int_Order"></param>
        /// <param name="p_str_Type">红外数据比对试验类型</param>
        /// <param name="p_str_Name">项目名称</param>
        /// <param name="p_strCode">标识符</param>
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
        /// 获取红外数据比对项目
        /// </summary>
        /// <param name="p_int_index">项目列表索引</param>
        /// <returns></returns>
        public StPlan_Infrared GetCarrierPrj(int p_int_index)
        {
            if (p_int_index >= m_Lst_InfraredPlan.Count)
                return new StPlan_Infrared();
            return m_Lst_InfraredPlan[p_int_index];
        }

        /// <summary>
        /// 移动红外数据比对项目
        /// </summary>
        /// <param name="p_int_MoveToIndex">需要移动到的列表位置</param>
        /// <param name="p_scp_Item">项目结构体</param>
        public void Move(int p_int_MoveToIndex, StPlan_Infrared p_scp_Item)
        {
            p_int_MoveToIndex = p_int_MoveToIndex < 0 ? 0 : p_int_MoveToIndex;
            p_int_MoveToIndex = p_int_MoveToIndex >= m_Lst_InfraredPlan.Count ? m_Lst_InfraredPlan.Count - 1 : p_int_MoveToIndex;
            this.Remove(p_scp_Item);
            m_Lst_InfraredPlan.Insert(p_int_MoveToIndex, p_scp_Item);
            return;
        }

        /// <summary>
        /// 移除全部项目
        /// </summary>
        public void RemoveAll()
        {
            m_Lst_InfraredPlan.Clear();
        }


        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="p_int_Index">项目索引号</param>
        public void RemoveAt(int p_int_Index)
        {
            if (p_int_Index < 0 || p_int_Index >= m_Lst_InfraredPlan.Count)
                return;
            m_Lst_InfraredPlan.RemoveAt(p_int_Index);
            return;
        }

        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
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
