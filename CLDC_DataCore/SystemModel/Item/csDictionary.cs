using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.DataBase;
using System.Windows.Forms;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 系统字典
    /// </summary>
    public class csDictionary
    {
        /// <summary>
        /// 字典集合
        /// </summary>
        private Dictionary<string, List<string>> _ZiDian;
        /// <summary>
        /// 构造函数，初始化字典集合
        /// </summary>
        public csDictionary()
        {
            _ZiDian = new Dictionary<string, List<string>>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csDictionary()
        {
            _ZiDian = null;
        }
        /// <summary>
        /// 加载字典集合
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            _ZiDian.Clear();     //清除字典信息集合
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_DICTIONARY, out _ErrorString);
            if (_ErrorString != "")
            {
                #region 初始化字典表
                _XmlNode = clsXmlControl.CreateXmlNode("ZiDianGroup");

                //XmlNode _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "录入类型");
                //_XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "扫描条码"));
                //_XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "手工录入"));
                //_XmlNode.AppendChild(_XmlChildNode);

                XmlNode _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "电压");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "57.7"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "100"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "220"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "380"));
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "电流");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "1"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "1.5"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "4"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "5"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "6"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "10"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "15"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "20"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "40"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "60"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "80"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "100"));
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "常数");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "1200"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "1200(1200)"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "2400(2400)"));
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "等级");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "1.0"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "0.5S(2)"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "0.2S(1)"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "1(2)"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "2.0"));
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "制造厂家");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "深圳科陆"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "江苏林洋"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "杭州华立"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "湖南威盛"));
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "表类型");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "电子式"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "机电式"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "感应式"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "机械式"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "多功能"));
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "表型号");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "DDS668"));
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "送检单位");
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "检定类型");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "首检"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "周检"));
                _XmlNode.AppendChild(_XmlChildNode);

                _XmlChildNode = clsXmlControl.CreateXmlNode("R", "Name", "波特率");
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "9600,e,8,1"));
                _XmlChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "9600,n,8,1"));
                _XmlNode.AppendChild(_XmlChildNode);

                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_DICTIONARY);
                #endregion

            }
            if (_XmlNode == null)
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                List<string> _Values = new List<string>();
                for (int _j = 0; _j < _XmlNode.ChildNodes[_i].ChildNodes.Count; _j++)
                {
                    _Values.Add(_XmlNode.ChildNodes[_i].ChildNodes[_j].InnerText);          //获取字典值
                }
                _ZiDian.Add(_XmlNode.ChildNodes[_i].Attributes[0].Value, _Values);       //获取字典属性值
            }
            return;

        }
        /// <summary>
        /// 存储字典集合
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "ZiDianGroup");
            foreach (string _Name in _ZiDian.Keys)
            {
                _Xml.appendchild("", "R", "Name", _Name);           //<ZiDianGroup><R Name="_Name"/></ZiDianGroup>
                for (int _i = 0; _i < _ZiDian[_Name].Count; _i++)
                {
                    _Xml.appendchild(clsXmlControl.XPath("R,Name," + _Name), "C", _ZiDian[_Name][_i]);  //<ZiDianGroup><R Name="_Name"><C><![CDATA[_ZiDian[_name][_i]]]></R></ZiDianGroup>
                }
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_DICTIONARY);

        }
        /// <summary>
        /// 增加一个字典属性值
        /// </summary>
        /// <param name="ZiDianName">字典名称</param>
        /// <param name="sValue">属性值</param>
        public void Add(string ZiDianName, string sValue)
        {
            if (!_ZiDian.ContainsKey(ZiDianName))
                return;
            if (_ZiDian[ZiDianName].Contains(sValue))
                return;
            _ZiDian[ZiDianName].Add(sValue);

        }
        /// <summary>
        /// 增加一个字典属性值，并且选择是否及时存储
        /// </summary>
        /// <param name="ZiDianName">字典</param>
        /// <param name="sValue">属性值</param>
        /// <param name="SaveNow">是否马上存档</param>
        public void Add(string ZiDianName, string sValue, bool SaveNow)
        {
            this.Add(ZiDianName, sValue);
            if (SaveNow)
                this.Save();
        }
        /// <summary>
        /// 移除一个属性值
        /// </summary>
        /// <param name="ZiDianName">字典名称</param>
        /// <param name="RemoveValue">需要移除的属性值</param>
        public void Remove(string ZiDianName, string RemoveValue)
        {
            if (!_ZiDian.ContainsKey(ZiDianName) || !_ZiDian[ZiDianName].Contains(RemoveValue))
                return;
            _ZiDian[ZiDianName].Remove(RemoveValue);
        }
        /// <summary>
        /// 移除一个属性值,是否同时存档
        /// </summary>
        /// <param name="ZiDianName">字典名称</param>
        /// <param name="RemoveValue">字典名称</param>
        /// <param name="SaveNow">是否存档</param>
        public void Remove(string ZiDianName, string RemoveValue, bool SaveNow)
        {
            this.Remove(ZiDianName, RemoveValue);
            if (SaveNow)
                this.Save();
            return;
        }

        /// <summary>
        /// 移除一个字典的所有属性值
        /// </summary>
        /// <param name="ZiDianName">字典名称</param>
        public void Remove(string ZiDianName)
        {
            if (!_ZiDian.ContainsKey(ZiDianName))
                return;
            _ZiDian[ZiDianName].Clear();
            return;
        }
        /// <summary>
        /// 移除一个字典的所有属性值
        /// </summary>
        /// <param name="ZiDianName">字典名称</param>
        /// <param name="SaveNow">是否存档</param>
        public void Remove(string ZiDianName, bool SaveNow)
        {
            this.Remove(ZiDianName);
            if (SaveNow)
                this.Save();
            return;
        }


        /// <summary>
        /// 获取字典名称
        /// </summary>
        /// <returns>返回字典名称集合</returns>
        public List<string> getZiDianName()
        {
            List<string> _Name = new List<string>();

            foreach (string _k in _ZiDian.Keys)
            {
                _Name.Add(_k);
            }
            return _Name;
        }

        /// <summary>
        /// 获取字典对应值集合
        /// </summary>
        /// <param name="ZiDianName">字典名称</param>
        /// <returns></returns>
        public List<string> getValues(string ZiDianName)
        {
            if (!_ZiDian.ContainsKey(ZiDianName))
                return new List<string>();
            return _ZiDian[ZiDianName];
        }
    }
}
