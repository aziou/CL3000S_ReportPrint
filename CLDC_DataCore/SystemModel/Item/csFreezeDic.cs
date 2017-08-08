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
    public class csFreezeDic
    {
        /// <summary>
        /// 冻结配置字典
        /// </summary>
        private Dictionary<string, Struct.StFreezeConfig> _FreezeConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        public csFreezeDic()
        {
            _FreezeConfig = new Dictionary<string, CLDC_DataCore.Struct.StFreezeConfig>();
        }

        /// <summary>
        /// 析构函数 
        /// </summary>
        ~csFreezeDic()
        {
            _FreezeConfig = null;
        }

        /// <summary>
        /// 加载冻结配置字典
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_FREEZEDICTIONARY, out _ErrorString);
            if (_ErrorString != "" || _XmlNode.ChildNodes.Count < 5)             
            {
                #region 初始化冻结参数信息
                _XmlNode = clsXmlControl.CreateXmlNode("FreezeConfig");
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "001", "Name", "定时冻结", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "002", "Name", "瞬时冻结", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "003", "Name", "日冻结", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "004", "Name", "约定冻结", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "005", "Name", "整点冻结", "OutPramerter", "1|1|1|0Ib|1.0"));
                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_FREEZEDICTIONARY);
                #endregion
            }
            _FreezeConfig.Clear();
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                Struct.StFreezeConfig _Freeze = new CLDC_DataCore.Struct.StFreezeConfig();
                _Freeze.FreezePrjID = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                _Freeze.FreezePrjName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                _Freeze.OutPramerter = new CLDC_DataCore.Struct.StPowerPramerter();
                _Freeze.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                _FreezeConfig.Add(_Freeze.FreezePrjID, _Freeze);
            }
        }

        /// <summary>
        /// 存储冻结配置字典
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "FreezeConfig");
            foreach (string _n in _FreezeConfig.Keys)
            {
                Struct.StFreezeConfig _Freeze = _FreezeConfig[_n];
                _Xml.appendchild("", "R", "ID", _Freeze.FreezePrjID, "Name", _Freeze.FreezePrjName, "OutPramerter", _Freeze.OutPramerter.Jion());
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_FREEZEDICTIONARY);
            return;
        }

        /// <summary>
        /// 添加和修改冻结配置信息
        /// </summary>
        /// <param name="FreezeInfo">冻结配置信息结构体</param>
        public void Add(Struct.StFreezeConfig FreezeInfo)
        {
            if (_FreezeConfig.ContainsKey(FreezeInfo.FreezePrjID))
            {
                this.Remove(FreezeInfo.FreezePrjID);
            }
            _FreezeConfig.Add(FreezeInfo.FreezePrjID, FreezeInfo);
            return;
        }

        /// <summary>
        /// 移除一个冻结配置项目
        /// </summary>
        /// <param name="PrjID"></param>
        public void Remove(string PrjID)
        {
            if (!_FreezeConfig.ContainsKey(PrjID))
                return;
            _FreezeConfig.Remove(PrjID);
            return;
        }

        /// <summary>
        /// 获取冻结项目信息列表
        /// </summary>
        /// <returns></returns>
        public List<Struct.StFreezeConfig> getFreezePrj()
        {
            List<Struct.StFreezeConfig> _Freeze = new List<CLDC_DataCore.Struct.StFreezeConfig>();
            foreach (string _ID in _FreezeConfig.Keys)
            {
                _Freeze.Add(_FreezeConfig[_ID]);
            }
            return _Freeze;
        }

        /// <summary>
        /// 获取一个项目信息
        /// </summary>
        /// <param name="PrjID"></param>
        /// <returns></returns>
        public Struct.StFreezeConfig getFreezePrj(string PrjID)
        {
            if (!_FreezeConfig.ContainsKey(PrjID))
                return new CLDC_DataCore.Struct.StFreezeConfig();
            return _FreezeConfig[PrjID];
        }
    }
}
