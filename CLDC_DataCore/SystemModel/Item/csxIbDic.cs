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
    public class csxIbDic
    {
        /// <summary>
        /// 电流倍数字典集合
        /// </summary>
        private Dictionary<string, string> _xIbDic;
        /// <summary>
        /// 构造函数
        /// </summary>
        public csxIbDic()
        {
            _xIbDic = new Dictionary<string, string>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csxIbDic()
        {
            _xIbDic = null;
        }
        /// <summary>
        /// 加载电流负载点信息
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            _xIbDic.Clear();           //清空用户信息集合
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_XIBDICTIONARY, out _ErrorString);
            if (_ErrorString != "")
            {
                #region 初始化电流倍数字典

                _XmlNode = clsXmlControl.CreateXmlNode("xIbGroup");
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "Imax", "ID", "01"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.5Imax", "ID", "02"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "3.0Ib", "ID", "03"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "2.0Ib", "ID", "04"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "1.5Ib", "ID", "05"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.5(Imax-Ib)", "ID", "06"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "1.0Ib", "ID", "07"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.8Ib", "ID", "08"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.5Ib", "ID", "09"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.2Ib", "ID", "10"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.1Ib", "ID", "11"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.05Ib", "ID", "12"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.02Ib", "ID", "13"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "Name", "0.01Ib", "ID", "14"));

                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_XIBDICTIONARY);

                #endregion
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                _xIbDic.Add(_XmlNode.ChildNodes[_i].Attributes[0].Value, _XmlNode.ChildNodes[_i].Attributes[1].Value);
            }
            return;
        }
        /// <summary>
        /// 保存电流倍数字典XML文档
        /// </summary>
        public void Save()
        {
            if (_xIbDic.Count == 0)
                return;
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "xIbGroup");
            foreach (string _xIb in _xIbDic.Keys)
            {
                _Xml.appendchild("", "R", "Name", _xIb, "ID", _xIbDic[_xIb]);
            }

            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_XIBDICTIONARY); 
        }
        /// <summary>
        /// 添加新的负载点
        /// <param name="xIbName">负载点名称</param>
        /// </summary>
        public bool Add(string xIbName)
        {
            if (_xIbDic.ContainsKey(xIbName))
                return false;
            string _ID = "";
            for (int _I = 1; _I <= 99; _I++)            //总共99个预留点，
            { 
                _ID=_I.ToString("d2");
                if (!_xIbDic.ContainsValue(_ID))
                    break;
            }
            _xIbDic.Add(xIbName, _ID);
            return true;
        }
        /// <summary>
        /// 添加新的负载点
        /// </summary>
        /// <param name="xIbName">负载点名称</param>
        /// <param name="SaveNow">是否立即存档</param>
        public bool Add(string xIbName, bool SaveNow)
        {
            bool _OK=this.Add(xIbName);
            if(_OK && SaveNow)
                this.Save();
            return _OK;
        }
        /// <summary>
        /// 移除一个电流负载点
        /// </summary>
        /// <param name="xIbName">电流倍数名称Iamx,1.0Ib,3.0Ib</param>
        public void Remove(string xIbName)
        {
            if (!_xIbDic.ContainsKey(xIbName) || int.Parse(_xIbDic[xIbName])<15)
                return;
            _xIbDic.Remove(xIbName);
            return;
        }
        /// <summary>
        /// 移除一个电流负载点
        /// </summary>
        /// <param name="xIbName">电流倍数名称</param>
        /// <param name="SaveNow">是否立即存档</param>
        public void Remove(string xIbName, bool SaveNow)
        {
            this.Remove(xIbName);
            if (SaveNow)
                this.Save();
        }
        /// <summary>
        /// 获取电流倍数ID值
        /// </summary>
        /// <param name="xIbName">电流倍数</param>
        /// <returns></returns>
        public string getxIbID(string xIbName)
        {
            foreach (string Key in _xIbDic.Keys)
            {
                if (Key.ToLower() == xIbName.ToLower())
                {
                    return _xIbDic[Key];
                }
            }

            return "";
        }
        /// <summary>
        /// 根据ID值获取电流倍数名称
        /// </summary>
        /// <param name="ID">ID值</param>
        /// <returns></returns>
        public string getxIb(string ID)
        {
            if (!_xIbDic.ContainsValue(ID))
                return "";
            foreach (string _Name in _xIbDic.Keys)
            {
                if (_xIbDic[_Name] == ID)
                    return _Name;
            }
            return "";
        }
        /// <summary>
        /// 获取电流倍数名称列表
        /// </summary>
        /// <returns></returns>
        public List<string> getxIb()
        {
            List<string> _xIbs = new List<string>();
            foreach (string _Name in _xIbDic.Keys)
            {
                _xIbs.Add(_Name);
            }
            return _xIbs;
        }

    }
}
