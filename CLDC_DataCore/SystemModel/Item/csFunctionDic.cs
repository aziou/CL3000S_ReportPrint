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
    public class csFunctionDic
    {
        /// <summary>
        /// 智能表功能配置字典
        /// </summary>
        private Dictionary<string, Struct.StFunctionConfig> _FunctionConfig;
        /// <summary>
        /// 构造函数
        /// </summary>
        public csFunctionDic()
        {
            _FunctionConfig = new Dictionary<string, CLDC_DataCore.Struct.StFunctionConfig>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csFunctionDic()
        {
            _FunctionConfig = null;
        }
        /// <summary>
        /// 加载智能表功能配置字典
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_FUNCTIONDICTIONARY, out _ErrorString);
            if (_ErrorString != "" || _XmlNode.ChildNodes.Count < 6)             //新增加5条
            {
                #region 初始化智能表功能参数信息
                _XmlNode = clsXmlControl.CreateXmlNode("FunctionConfig");
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "001", "Name", "计量功能", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "002", "Name", "计时功能", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "003", "Name", "显示功能", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "004", "Name", "费率时段功能", "OutPramerter", "1|1|1|Imax|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "005", "Name", "脉冲输出功能", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "006", "Name", "最大需量功能", "OutPramerter", "1|1|1|0Ib|1.0"));



                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_FUNCTIONDICTIONARY);
                #endregion

            }
            _FunctionConfig.Clear();
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                Struct.StFunctionConfig _Function = new CLDC_DataCore.Struct.StFunctionConfig();
                _Function.FunctionPrjID = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                _Function.FunctionPrjName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                _Function.OutPramerter = new CLDC_DataCore.Struct.StPowerPramerter();
                _Function.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                _FunctionConfig.Add(_Function.FunctionPrjID, _Function);
            }

        }
        /// <summary>
        /// 存储智能表功能配置字典
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "FunctionConfig");
            foreach (string _n in _FunctionConfig.Keys)
            {
                Struct.StFunctionConfig _Function = _FunctionConfig[_n];
                _Xml.appendchild("", "R", "ID", _Function.FunctionPrjID, "Name", _Function.FunctionPrjName, "OutPramerter", _Function.OutPramerter.Jion());
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_FUNCTIONDICTIONARY);
            return;
        }
        /// <summary>
        /// 添加和修改多功能配置信息
        /// </summary>
        /// <param name="FunctionInfo">多功能配置信息结构体</param>
        public void Add(Struct.StFunctionConfig FunctionInfo)
        {
            if (_FunctionConfig.ContainsKey(FunctionInfo.FunctionPrjID))
            {
                this.Remove(FunctionInfo.FunctionPrjID);
            }
            _FunctionConfig.Add(FunctionInfo.FunctionPrjID, FunctionInfo);
            return;
        }
        /// <summary>
        /// 移除一个多功能配置项目
        /// </summary>
        /// <param name="PrjID"></param>
        public void Remove(string PrjID)
        {
            if (!_FunctionConfig.ContainsKey(PrjID))
                return;
            _FunctionConfig.Remove(PrjID);
            return;
        }
        /// <summary>
        /// 获取多功能项目信息列表
        /// </summary>
        /// <returns></returns>
        public List<Struct.StFunctionConfig> getFunctionPrj()
        {
            List<Struct.StFunctionConfig> _Function = new List<CLDC_DataCore.Struct.StFunctionConfig>();
            foreach (string _ID in _FunctionConfig.Keys)
            {
                _Function.Add(_FunctionConfig[_ID]);
            }
            return _Function;
        }
        /// <summary>
        /// 获取一个项目信息
        /// </summary>
        /// <param name="PrjID"></param>
        /// <returns></returns>
        public Struct.StFunctionConfig getFunctionPrj(string PrjID)
        {
            if (!_FunctionConfig.ContainsKey(PrjID))
                return new CLDC_DataCore.Struct.StFunctionConfig();
            return _FunctionConfig[PrjID];
        }

    }
}
