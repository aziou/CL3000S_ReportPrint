using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.DataBase;
using System.Windows.Forms;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class csCostControlDic
    {
        /// <summary>
        /// 费控功能配置字典
        /// </summary>
        private Dictionary<string, Struct.StCostControlConfig> _CostControlConfig;
        /// <summary>
        /// 构造函数
        /// </summary>
        public csCostControlDic()
        {
            _CostControlConfig = new Dictionary<string, CLDC_DataCore.Struct.StCostControlConfig>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csCostControlDic()
        {
            _CostControlConfig = null;
        }
        /// <summary>
        /// 加载智能表功能配置字典
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_COSTCONTROLDICTIONARY, out _ErrorString);
            if (_ErrorString != "" || _XmlNode.ChildNodes.Count < 21)             //新增加5条
            {
                #region 初始化费控功能参数信息
                _XmlNode = clsXmlControl.CreateXmlNode("CostControlConfig");
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "001", "Name", "身份认证", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "002", "Name", "远程控制", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "003", "Name", "报警功能", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "004", "Name", "保电功能", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "005", "Name", "保电解除", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "006", "Name", "远程保电", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "007", "Name", "数据回抄", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "008", "Name", "参数设置", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "009", "Name", "剩余电量递减准确度", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "010", "Name", "电价切换", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "011", "Name", "负荷开关", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "012", "Name", "98级电量清零", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "013", "Name", "密钥更新", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "014", "Name", "密钥恢复", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "015", "Name", "控制功能", "OutPramerter", "1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "016", "Name", "阶梯电价检测", "OutPramerter", "1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "017", "Name", "费率电价检测", "OutPramerter", "1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "018", "Name", "远程控制直接合闸", "OutPramerter", "1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "019", "Name", "钱包初始化", "OutPramerter", "1|1|0Ib|1.0"));
                #region @C_B
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "021", "Name", "预置内容检查", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "022", "Name", "预置内容设置", "OutPramerter", "1|1|1|0Ib|1.0"));
                #endregion

                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "023", "Name", "本地模式切换远程模式", "OutPramerter", "1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "024", "Name", "远程模式切换本地模式", "OutPramerter", "1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "025", "Name", "用户卡开户", "OutPramerter", "1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "026", "Name", "透支功能", "OutPramerter", "1|1|0Ib|1.0"));
                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_COSTCONTROLDICTIONARY);
                #endregion

            }
            _CostControlConfig.Clear();
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                Struct.StCostControlConfig _CostControl = new CLDC_DataCore.Struct.StCostControlConfig();
                _CostControl.CostControlPrjID = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                _CostControl.CostControlPrjName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                _CostControl.OutPramerter = new CLDC_DataCore.Struct.StPowerPramerter();
                _CostControl.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                _CostControlConfig.Add(_CostControl.CostControlPrjID, _CostControl);
            }

        }
        /// <summary>
        /// 存储费控功能功能配置字典
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "CostControlConfig");
            foreach (string _n in _CostControlConfig.Keys)
            {
                Struct.StCostControlConfig _CostControl = _CostControlConfig[_n];
                _Xml.appendchild("", "R", "ID", _CostControl.CostControlPrjID, "Name", _CostControl.CostControlPrjName, "OutPramerter", _CostControl.OutPramerter.Jion());
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_FUNCTIONDICTIONARY);
            return;
        }
        /// <summary>
        /// 添加和修改费控功能配置信息
        /// </summary>
        /// <param name="FunctionInfo">多功能配置信息结构体</param>
        public void Add(Struct.StCostControlConfig FunctionInfo)
        {
            if (_CostControlConfig.ContainsKey(FunctionInfo.CostControlPrjID))
            {
                this.Remove(FunctionInfo.CostControlPrjID);
            }
            _CostControlConfig.Add(FunctionInfo.CostControlPrjID, FunctionInfo);
            return;
        }
        /// <summary>
        /// 移除一个费控功能配置项目
        /// </summary>
        /// <param name="PrjID"></param>
        public void Remove(string PrjID)
        {
            if (!_CostControlConfig.ContainsKey(PrjID))
                return;
            _CostControlConfig.Remove(PrjID);
            return;
        }
        /// <summary>
        /// 获取费控功能项目信息列表
        /// </summary>
        /// <returns></returns>
        public List<Struct.StCostControlConfig> getCostControlPrj()
        {
            List<Struct.StCostControlConfig> _CostControl = new List<CLDC_DataCore.Struct.StCostControlConfig>();
            foreach (string _ID in _CostControlConfig.Keys)
            {
                _CostControl.Add(_CostControlConfig[_ID]);
            }
            return _CostControl;
        }
        /// <summary>
        /// 获取一个项目信息
        /// </summary>
        /// <param name="PrjID"></param>
        /// <returns></returns>
        public Struct.StCostControlConfig getCostControlPrj(string PrjID)
        {
            if (!_CostControlConfig.ContainsKey(PrjID))
                return new CLDC_DataCore.Struct.StCostControlConfig();
            return _CostControlConfig[PrjID];
        }

    }
}
