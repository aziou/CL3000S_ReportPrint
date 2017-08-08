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
    /// 功能描述：实验参数
    /// 作    者：lsx 
    /// 编写日期：2014-02-12
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    public class TestSetting
    {
        /// <summary>
        /// 实验参数
        /// </summary>
        private Dictionary<string, CLDC_DataCore.Struct.StSystemInfo> _TestSetting;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TestSetting()
        {
            _TestSetting = new Dictionary<string, CLDC_DataCore.Struct.StSystemInfo>();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~TestSetting()
        {
            _TestSetting = null;
        }

        /// <summary>
        /// 初始化实验参数信息
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            _TestSetting.Clear();            //清空系统配置集合
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_TestSet, out _ErrorString);
            if (_ErrorString != "")
            {
                _XmlNode = clsXmlControl.CreateXmlNode("TestSettingInfo");
                #region 实验参数默认值
                this.CreateDefaultData(ref _XmlNode);
                #endregion

                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_TestSet);
            }

            if (_XmlNode.ChildNodes.Count > 0)
            {
                if (_XmlNode.ChildNodes[0].Attributes.Count < 6)
                {
                    CLDC_DataCore.Function.File.RemoveFile(Application.StartupPath + Const.Variable.CONST_TestSet);   //如果发现旧的系统配置文件就要删除掉，再重新创建
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
                if (_TestSetting.ContainsKey(_XmlNode.ChildNodes[_i].Attributes[0].Value))
                    _TestSetting.Remove(_XmlNode.ChildNodes[_i].Attributes[0].Value);
                _TestSetting.Add(_XmlNode.ChildNodes[_i].Attributes[0].Value, _Item);
            }
        }

        /// <summary>
        /// 存储实验参数XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "TestSettingInfo");
            foreach (string _Key in _TestSetting.Keys)
            {
                _Xml.appendchild(""
                                , "R"
                                , "Item", _Key
                                , "Value", _TestSetting[_Key].Value
                                , "Name", _TestSetting[_Key].Name
                                , "Description", _TestSetting[_Key].Description
                                , "ClassName", _TestSetting[_Key].ClassName
                                , "DataSource", _TestSetting[_Key].DataSource);
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_TestSet);
        }

        /// <summary>
        /// 创建系统默认配置文件
        /// </summary>
        /// <param name="xml"></param>
        private void CreateDefaultData(ref XmlNode xml)
        {
            #region ----------实验参数---------

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_DRIVERF
                   , "Value", "1"
                   , "Name", "参数1"
                   , "Description", "实验参数请查阅标准表相关说明。"
                   , "ClassName", "实验参数"
                   , "DataSource", ""));
            #endregion
        }

        /// <summary>
        /// 获取关键字列表
        /// </summary>
        /// <returns></returns>
        public List<string> getKeyNames()
        {
            List<string> _Keys = new List<string>();
            foreach (string _name in _TestSetting.Keys)
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
            _TestSetting.Clear();
        }

        /// <summary>
        /// 获取实验参数的结构体
        /// </summary>
        /// <param name="Tkey">系统项目ID</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StSystemInfo getItem(string Tkey)
        {
            if (_TestSetting.Count == 0)
                return new CLDC_DataCore.Struct.StSystemInfo();
            if (_TestSetting.ContainsKey(Tkey))
                return _TestSetting[Tkey];
            else
                return new CLDC_DataCore.Struct.StSystemInfo();
        }

        /// <summary>
        /// 实验参数的个数
        /// </summary>
        public int Count
        {
            get
            {
                return _TestSetting.Count;
            }
        }

        /// <summary>
        /// 添加实验参数的项目
        /// </summary>
        /// <param name="Tkey">实验参数 项目名称</param>
        /// <param name="Item">实验参数 配置值</param>
        public void Add(string Tkey, CLDC_DataCore.Struct.StSystemInfo Item)
        {
            if (_TestSetting.ContainsKey(Tkey))
            {
                _TestSetting.Remove(Tkey);
                _TestSetting.Add(Tkey, Item);
            }
            else
                _TestSetting.Add(Tkey, Item);
            return;
        }
    }
}
