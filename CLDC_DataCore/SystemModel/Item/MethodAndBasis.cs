using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.DataBase;
using System.Xml;
using System.Windows.Forms;
using System.Net;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 功能描述：实验方法与依据
    /// 作    者：lsx 
    /// 编写日期：2014-02-12
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    public class MethodAndBasis
    {
        /// <summary>
        /// 实验方法与依据
        /// </summary>
        private Dictionary<string, CLDC_DataCore.Struct.StSystemInfo> _MethodAndBasis;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MethodAndBasis()
        {
            _MethodAndBasis = new Dictionary<string, CLDC_DataCore.Struct.StSystemInfo>();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~MethodAndBasis()
        {
            _MethodAndBasis = null;
        }

        /// <summary>
        /// 初始化实验方法与依据信息
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            _MethodAndBasis.Clear();            //清空系统配置集合
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_MethodBasis, out _ErrorString);
            if (_ErrorString != "")
            {
                _XmlNode = clsXmlControl.CreateXmlNode("MethodAndBasisInfo");
                #region 实验方法与依据默认值
                this.CreateDefaultData(ref _XmlNode);
                #endregion

                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_MethodBasis);
            }

            if (_XmlNode.ChildNodes.Count > 0)
            {
                if (_XmlNode.ChildNodes[0].Attributes.Count < 6)
                {
                    CLDC_DataCore.Function.File.RemoveFile(Application.StartupPath + Const.Variable.CONST_MethodBasis);   //如果发现旧的系统配置文件就要删除掉，再重新创建
                    this.Load();
                    return;
                }
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = new CLDC_DataCore.Struct.StSystemInfo();

                _Item.Value = _XmlNode.ChildNodes[_i].Attributes[1].Value;       //项目值
                _Item.Name = _XmlNode.ChildNodes[_i].Attributes[2].Value;       //项目中文名称
                _Item.Description = _XmlNode.ChildNodes[_i].Attributes[3].Value;      //项目描述
                _Item.ClassName = _XmlNode.ChildNodes[_i].Attributes[4].Value;  //项目分类名称
                _Item.DataSource = _XmlNode.ChildNodes[_i].Attributes[5].Value; //数据源
                if (_MethodAndBasis.ContainsKey(_XmlNode.ChildNodes[_i].Attributes[0].Value))
                    _MethodAndBasis.Remove(_XmlNode.ChildNodes[_i].Attributes[0].Value);
                _MethodAndBasis.Add(_XmlNode.ChildNodes[_i].Attributes[0].Value, _Item);
            }
        }

        /// <summary>
        /// 存储实验方法与依据XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "MethodAndBasisInfo");
            foreach (string _Key in _MethodAndBasis.Keys)
            {
                _Xml.appendchild(""
                                , "R"
                                , "Item", _Key
                                , "Value", _MethodAndBasis[_Key].Value
                                , "Name", _MethodAndBasis[_Key].Name
                                , "Description", _MethodAndBasis[_Key].Description
                                , "ClassName", _MethodAndBasis[_Key].ClassName
                                , "DataSource", _MethodAndBasis[_Key].DataSource);
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_MethodBasis);
        }

        /// <summary>
        /// 创建系统默认的实验方法与依据 配置文件
        /// </summary>
        /// <param name="xml"></param>
        private void CreateDefaultData(ref XmlNode xml)
        {
            #region -------起动和潜动-------
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TSB_STARTTIME 
        , "Value", "1.2"
        , "Name", "起动时间"
        , "Description", "起动时间值，默认值为1.2即1.2倍。"
        , "ClassName", "起动和潜动"
        , "DataSource", ""));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TSB_METERTYPES
        , "Value", "南网表"
        , "Name", "表类型"
        , "Description", "表类型分为南网表和国网表。"
        , "ClassName", "起动和潜动"
        , "DataSource", ""));
            #endregion

            #region ----------实验方法---------
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_TM_TIMECUT
                   , "Value", "按判断电量的改变为投切实验方法"
                   , "Name", "时段投切实验方法"
                   , "Description", "时段投切实验方法，包括按判断电量的改变为投切实验方法，按在标准时间内的电量改变量实验方法和按表位顺序检定。"
                   , "ClassName", "实验方法"
                   , "DataSource", "按判断电量的改变为投切实验方法|按在标准时间内的电量改变量实验方法|按表位顺序检定|脉冲法"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_TM_QPOWERM
                    , "Value", "读取各象限存储器电量进行判断"
                    , "Name", "无功电量存储器实验方法"
                    , "Description", "无功电量存储器实验方法，包含读取各象限存储器电量进行判断和用各象限进行实验时产生的电量差值进行判断。"
                    , "ClassName", "实验方法"
                    , "DataSource", "读取各象限存储器电量进行判断|用各象限进行实验时产生的电量差值进行判断"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_TM_PULLGATE 
                    , "Value", "02（03）级指令拉合闸（按键合闸）"
                    , "Name", "拉合闸实验方法"
                    , "Description", "实验方法请查阅标准表相关说明。"
                    , "ClassName", "实验方法"
                    , "DataSource", "02（03）级指令拉合闸（按键合闸）|02（03）级指令拉合闸（直接合闸）|按实际拉闸状态跟运行状态字判断（继电器外置表）|按实际拉闸状态跟运行状态字判断（继电器内置表）|按读电表运行状态字判断（内置表、外置表）"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TM_GPSGETT
        , "Value", "CL2032GPS"
        , "Name", "GPS取时方式"
        , "Description", "包括CL2032GPS,取GPS时间服务器和取电脑时间。"
        , "ClassName", "实验方法"
        , "DataSource", "CL2032GPS|取GPS时间服务器|取电脑时间"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            , "Item", CLDC_DataCore.Const.Variable.CTC_TM_LOADSWITCH
            , "Value", "继电器内置（状态字）"
            , "Name", "负荷开关实验方法"
            , "Description", "负荷开关实验方法，包括继电器内置（状态字）和继电器外置（表位信号+状态字）。"
            , "ClassName", "实验方法"
            , "DataSource", "继电器内置（状态字）|继电器外置（表位信号+状态字）"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            , "Item", CLDC_DataCore.Const.Variable.CTC_TM_ISSUPPLYVOICE 
            , "Value", "是"
            , "Name", "提供语音提示功能接口"
            , "Description", "提供语音提示功能接口。"
            , "ClassName", "实验方法"
            , "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TM_TIMEFUNCTION
        , "Value", "仅禁止零点广播校时"
        , "Name", "计时功能实验方法"
        , "Description", "计时功能实验方法，包括仅禁止零点广播校时和禁止在零点前后五分钟内广播校时。"
        , "ClassName", "实验方法"
        , "DataSource", "仅禁止零点广播校时|禁止在零点前后五分钟内广播校时"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TM_ISTEST05BC
        , "Value", "是"
        , "Name", "零点5分广播校时时是否做实验"
        , "Description", "零点5分广播校时时是否做实验。"
        , "ClassName", "实验方法"
        , "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TM_ISFILTERMACROV 
        , "Value", "是"
        , "Name", "是否过滤大误差"
        , "Description", "是否过滤大误差。"
        , "ClassName", "实验方法"
        , "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TM_ISJUDGECOUNT
        , "Value", "是"
        , "Name", "误差是否判断次数"
        , "Description", "误差是否判断次数。"
        , "ClassName", "实验方法"
        , "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TM_ISTESTCLOSEC 
        , "Value", "是"
        , "Name", "拉合闸实验后加电流是否测试已合闸"
        , "Description", "拉合闸实验后加电流是否测试已合闸。"
        , "ClassName", "实验方法"
        , "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
         , "Item", CLDC_DataCore.Const.Variable.CTC_TM_ISCLEAR0CP 
         , "Value", "是"
         , "Name", "拉合闸实验加电流后是否电量清零"
         , "Description", "拉合闸实验加电流后是否电量清零。"
         , "ClassName", "实验方法"
         , "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
          , "Item", CLDC_DataCore.Const.Variable.CTC_TM_ISLESSPULSEC 
          , "Value", "9999"
          , "Name", "做潜动实验时采集的脉冲数不大于多少"
          , "Description", "做潜动实验时采集的脉冲数不大于多少。"
          , "ClassName", "实验方法"
          , "DataSource", ""));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
          , "Item", CLDC_DataCore.Const.Variable.CTC_TM_ISMOREPULSEC 
          , "Value", "0"
          , "Name", "做起动实验时采集的脉冲数不小于多少"
          , "Description", "做起动实验时采集的脉冲数不小于多少。"
          , "ClassName", "实验方法"
          , "DataSource", ""));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
          , "Item", CLDC_DataCore.Const.Variable.CTC_TM_CL2036CONTROLCM 
          , "Value", "关闭"
          , "Name", "CL2036控制器控制方式"
          , "Description", "CL2036控制器控制方式，包括关闭和开放。"
          , "ClassName", "实验方法"
          , "DataSource", "关闭|开放"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
          , "Item", CLDC_DataCore.Const.Variable.CTC_TM_LEFTRIGHTPRICE
          , "Value", "02800020,4,4"
          , "Name", "当前电价标识"
          , "Description", "用于剩余电量递减准确度等需要读取电价的试验。"
          , "ClassName", "实验方法"
          , "DataSource", "02800020,4,4|028000FE,3,4"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
          , "Item", CLDC_DataCore.Const.Variable.CTC_TM_WRITEWRINGPRICE
          , "Value", "正常"
          , "Name", "报警金额写权限"
          , "Description", "用于控制功能试验写入报警金额。"
          , "ClassName", "实验方法"
          , "DataSource", "正常|内蒙"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
             , "Item", CLDC_DataCore.Const.Variable.CTC_TM_IS_GB_Address
             , "Value", "是"
             , "Name", "冻结指令是否采用广播地址方式下发"
             , "Description", "冻结指令是否采用广播地址方式下发。"
             , "ClassName", "实验方法"
             , "DataSource", "是|否"));
            #endregion
     
            #region ----------实验依据---------
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TB_EMETER
        , "Value", "JJG307-2006"
        , "Name", "机械表实验检定规程依据"
        , "Description", "机械表实验检定规程依据，包括JJG307-2006和JJG307-88。"
        , "ClassName", "实验依据"
        , "DataSource", "JJG307-2006|JJG307-88"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_TB_POWERUSETESTJ 
                , "Value", "DL/T614-200x"
                , "Name", "功耗测试判断规程依据"
                , "Description", "功耗测试判断规程依据，包括DL/T614-200x和DL/T614-1997。"
                , "ClassName", "实验依据"
                , "DataSource", "DL/T614-200x|DL/T614-1997|DL_T614-2007"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_TB_NEEDVALUEIF
                , "Value", "DL/T596-1999"
                , "Name", "需量示值误差判定依据"
                , "Description", "需量示值误差判定依据，包括DL/T596-1999中的1.9点和DLT/614-1997中的5.4.5点。"
                , "ClassName", "实验依据"
                , "DataSource", "DL/T596-1999|DLT/614-1997"));
            #endregion

            #region ----------实验方法与依据其它配置----------
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_TO_CHECKREGISTER
        , "Value", "读电量法"
        , "Name", "寄存器检查"
        , "Description", "寄存器检查"
        , "ClassName", "实验方法与依据其它配置"
        , "DataSource", "读电量法|差值法"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
         , "Item", CLDC_DataCore.Const.Variable.CTC_TO_CWREPLACE485
         , "Value", "否"
         , "Name", "用载波法代替485进行试验"
         , "Description", "用载波法代替485进行试验"
         , "ClassName", "实验方法与依据其它配置"
         , "DataSource", "是|否"));
            #endregion

        }

        /// <summary>
        /// 获取关键字列表
        /// </summary>
        /// <returns></returns>
        public List<string> getKeyNames()
        {
            List<string> _Keys = new List<string>();
            foreach (string _name in _MethodAndBasis.Keys)
            {
                _Keys.Add(_name);
            }
            return _Keys;
        }

        /// <summary>
        /// 清空列表
        /// </summary>
        public void Clear()
        {
            _MethodAndBasis.Clear();
        }

        /// <summary>
        /// 获取实验方法与依据的结构体
        /// </summary>
        /// <param name="Tkey">系统项目ID</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StSystemInfo getItem(string Tkey)
        {
            if (_MethodAndBasis.Count == 0)
                return new CLDC_DataCore.Struct.StSystemInfo();
            if (_MethodAndBasis.ContainsKey(Tkey))
                return _MethodAndBasis[Tkey];
            else
                return new CLDC_DataCore.Struct.StSystemInfo();
        }

        /// <summary>
        /// 实验方法与依据的个数
        /// </summary>
        public int Count
        {
            get
            {
                return _MethodAndBasis.Count;
            }
        }

        /// <summary>
        /// 添加实验方法与依据的项目
        /// </summary>
        /// <param name="Tkey">实验方法与依据 项目名称</param>
        /// <param name="Item">实验方法与依据 配置值</param>
        public void Add(string Tkey, CLDC_DataCore.Struct.StSystemInfo Item)
        {
            if (_MethodAndBasis.ContainsKey(Tkey))
            {
                _MethodAndBasis.Remove(Tkey);
                _MethodAndBasis.Add(Tkey, Item);
            }
            else
                _MethodAndBasis.Add(Tkey, Item);
            return;
        }
        /// <summary>
        /// 读取系统配置项目值
        /// </summary>
        /// <param name="Tkey">系统项目ID</param>
        /// <returns>系统项目配置值</returns>
        public string getValue(string Tkey)
        {
            if (_MethodAndBasis.Count == 0)
                return "";
            if (_MethodAndBasis.ContainsKey(Tkey))
                return _MethodAndBasis[Tkey].Value;
            else
                return "";
        }
    }
}
