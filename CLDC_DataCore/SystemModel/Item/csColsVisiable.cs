using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;
using System.Windows.Forms;
using System.Xml;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class csColsVisiable
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, StColsVisiable> _ColsVisiable;
        /// <summary>
        /// 构造函数
        /// </summary>
        public csColsVisiable()
        {
            _ColsVisiable = new Dictionary<string, StColsVisiable>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csColsVisiable()
        {
            _ColsVisiable = null;
        }
        /// <summary>
        /// 加载列配置字典
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_COLSVISIABLE, out _ErrorString);
            if (_ErrorString != "" || _XmlNode.ChildNodes.Count < 1) //_XmlNode.ChildNodes.Count < 31)             //新增加5条
            {
                #region 初始化列显示参数信息
                _XmlNode = clsXmlControl.CreateXmlNode("ColsConfig");
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "任务编号", "Name", "任务编号", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "工单号", "Name", "工单号", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "计量编号", "Name", "计量编号", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "出厂编号", "Name", "出厂编号", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "表通信地址", "Name", "表通信地址", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "出厂日期", "Name", "出厂日期", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "证书编号", "Name", "证书编号", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "表名称", "Name", "表名称", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "铅封1", "Name", "铅封1", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "铅封2", "Name", "铅封2", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "铅封3", "Name", "铅封3", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "铅封4", "Name", "铅封4", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "铅封5", "Name", "铅封5", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "软件版本号", "Name", "软件版本号", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "硬件版本号", "Name", "硬件版本号", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "到货批次号", "Name", "到货批次号", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "备用1", "Name", "备用1", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "备用2", "Name", "备用2", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "备用3", "Name", "备用3", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "备用4", "Name", "备用4", "ShowType", "0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "备用5", "Name", "备用5", "ShowType", "0"));
                
                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_DGNDICTIONARY);
                #endregion

            }
            _ColsVisiable.Clear();
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                Struct.StColsVisiable _Col = new CLDC_DataCore.Struct.StColsVisiable();
                _Col.ColName = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                _Col.ColShowName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                _Col.ColShowType = int.Parse(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                
                _ColsVisiable.Add(_Col.ColName, _Col);
            }

        }
        /// <summary>
        /// 存储列配置字典
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "ColsConfig");
            foreach (string _n in _ColsVisiable.Keys)
            {
                Struct.StColsVisiable _Col = _ColsVisiable[_n];
                _Xml.appendchild("", "R", "ID", _Col.ColName, "Name", _Col.ColShowName, "ShowType", _Col.ColShowType.ToString());
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_COLSVISIABLE);
            return;
        }
        /// <summary>
        /// 添加和修改列配置信息
        /// </summary>
        /// <param name="Col">列配置信息结构体</param>
        public void Add(Struct.StColsVisiable Col)
        {
            if (_ColsVisiable.ContainsKey(Col.ColName))
            {
                this.Remove(Col.ColName);
            }
            _ColsVisiable.Add(Col.ColName, Col);
            return;
        }
        /// <summary>
        /// 移除一个列配置项目
        /// </summary>
        /// <param name="PrjID"></param>
        public void Remove(string PrjID)
        {
            if (!_ColsVisiable.ContainsKey(PrjID))
                return;
            _ColsVisiable.Remove(PrjID);
            return;
        }
        /// <summary>
        /// 获取列项目信息列表
        /// </summary>
        /// <returns></returns>
        public List<Struct.StColsVisiable> getColPrj()
        {
            List<Struct.StColsVisiable> _Col = new List<CLDC_DataCore.Struct.StColsVisiable>();
            foreach (string _ID in _ColsVisiable.Keys)
            {
                _Col.Add(_ColsVisiable[_ID]);
            }
            return _Col;
        }
        /// <summary>
        /// 获取一个列信息
        /// </summary>
        /// <param name="PrjID"></param>
        /// <returns></returns>
        public Struct.StColsVisiable getColPrj(string PrjID)
        {
            if (!_ColsVisiable.ContainsKey(PrjID))
                return new CLDC_DataCore.Struct.StColsVisiable();
            return _ColsVisiable[PrjID];
        }


    }
}
