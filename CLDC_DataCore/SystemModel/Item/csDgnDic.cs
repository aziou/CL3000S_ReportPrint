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
    public class csDgnDic
    {
        /// <summary>
        /// 多功能配置字典
        /// </summary>
        private Dictionary<string, Struct.StDgnConfig> _DgnConfig;
        /// <summary>
        /// 构造函数
        /// </summary>
        public csDgnDic()
        {
            _DgnConfig = new Dictionary<string, CLDC_DataCore.Struct.StDgnConfig>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csDgnDic()
        {
            _DgnConfig = null;
        }
        /// <summary>
        /// 加载多功能配置字典
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_DGNDICTIONARY, out _ErrorString);
            if (_ErrorString != "" || _XmlNode.ChildNodes.Count < 28)             //新增加5条
            {
                #region 初始化多功能参数信息
                _XmlNode = clsXmlControl.CreateXmlNode("DgnConfig");
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "001", "Name", "通信测试", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "002", "Name", "日计时误差", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "003", "Name", "费率时段检查", "OutPramerter", "1|1|1|Imax|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "004", "Name", "时段投切", "OutPramerter", "1|1|1|Imax|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "005", "Name", "计度器示值组合误差", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "006", "Name", "费率时段示值误差", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "007", "Name", "GPS对时", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "008", "Name", "闰年判断功能", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "009", "Name", "事件记录检查", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "010", "Name", "需量清空", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "011", "Name", "电压逐渐变化", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "012", "Name", "电压跌落", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "013", "Name", "时间误差", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "014", "Name", "最大需量0.1Ib", "OutPramerter", "1|1|1|0.1Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "015", "Name", "最大需量1.0Ib", "OutPramerter", "1|1|1|1.0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "016", "Name", "最大需量Imax", "OutPramerter", "1|1|1|Imax|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "017", "Name", "读取电量", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "018", "Name", "电量清零", "OutPramerter", "1|1|1|0Ib|1.0"));                
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "019", "Name", "电量寄存器检查", "OutPramerter", "1|1|1|1.0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "020", "Name", "需量寄存器检查", "OutPramerter", "1|1|1|1.0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "021", "Name", "瞬时寄存器检查", "OutPramerter", "1|1|1|1.0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "022", "Name", "状态寄存器检查", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "023", "Name", "失压寄存器检查", "OutPramerter", "1|1|0.6|1.0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "024", "Name", "校对电量", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "025", "Name", "校对需量", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "026", "Name", "检查电表运行状态", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "027", "Name", "预付费检测", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "037", "Name", "电压短时中断", "OutPramerter", "1|1|1|0Ib|1.0"));
                

                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_DGNDICTIONARY);
                #endregion

            }
            _DgnConfig.Clear();
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                Struct.StDgnConfig _Dgn = new CLDC_DataCore.Struct.StDgnConfig();
                _Dgn.DgnPrjID = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                _Dgn.DgnPrjName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                _Dgn.OutPramerter = new CLDC_DataCore.Struct.StPowerPramerter();
                _Dgn.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                _DgnConfig.Add(_Dgn.DgnPrjID, _Dgn);
            }

        }
        /// <summary>
        /// 存储多功能配置字典
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "DgnConfig");
            foreach (string _n in _DgnConfig.Keys)
            {
                Struct.StDgnConfig _Dgn = _DgnConfig[_n];
                _Xml.appendchild("", "R", "ID", _Dgn.DgnPrjID, "Name", _Dgn.DgnPrjName, "OutPramerter", _Dgn.OutPramerter.Jion());
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_DGNDICTIONARY);
            return;
        }
        /// <summary>
        /// 添加和修改多功能配置信息
        /// </summary>
        /// <param name="DgnInfo">多功能配置信息结构体</param>
        public void Add(Struct.StDgnConfig DgnInfo)
        {
            if (_DgnConfig.ContainsKey(DgnInfo.DgnPrjID))
            {
                this.Remove(DgnInfo.DgnPrjID);
            }
            _DgnConfig.Add(DgnInfo.DgnPrjID, DgnInfo);
            return;
        }
        /// <summary>
        /// 移除一个多功能配置项目
        /// </summary>
        /// <param name="PrjID"></param>
        public void Remove(string PrjID)
        {
            if (!_DgnConfig.ContainsKey(PrjID))
                return;
            _DgnConfig.Remove(PrjID);
            return;
        }
        /// <summary>
        /// 获取多功能项目信息列表
        /// </summary>
        /// <returns></returns>
        public List<Struct.StDgnConfig> getDgnPrj()
        {
            List<Struct.StDgnConfig> _Dgn = new List<CLDC_DataCore.Struct.StDgnConfig>();
            foreach (string _ID in _DgnConfig.Keys)
            {
                _Dgn.Add(_DgnConfig[_ID]);
            }
            return _Dgn;
        }
        /// <summary>
        /// 获取一个项目信息
        /// </summary>
        /// <param name="PrjID"></param>
        /// <returns></returns>
        public Struct.StDgnConfig getDgnPrj(string PrjID)
        {
            if (!_DgnConfig.ContainsKey(PrjID))
                return new CLDC_DataCore.Struct.StDgnConfig();
            return _DgnConfig[PrjID];
        }

    }
}
