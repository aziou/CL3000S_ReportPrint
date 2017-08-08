using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 功耗试验方案
    /// </summary>
    [Serializable]
    public class Plan_PowerConsume:Plan_Base
    {
        private List<CLDC_DataCore.Struct.StPowerConsume> _LstPowerConsume;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单向台</param>
        /// <param name="vFAName">方案名称</param>
        public Plan_PowerConsume(int TaiType, string FAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_GONGHAO_FOLDERNAME, TaiType, FAName)
        {
            this.Load();
        }
        ~Plan_PowerConsume()
        {
            _LstPowerConsume = null;
        }
        /// <summary>
        /// 加载功耗方案到功耗数据列表
        /// </summary>
        private void Load()
        {
            _LstPowerConsume = new List<CLDC_DataCore.Struct.StPowerConsume>();
            string _ErrorString = "";
            XmlNode _XmlNode = CLDC_DataCore.DataBase.clsXmlControl.LoadXml(_FAPath, out _ErrorString);
            if (_ErrorString != "")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                CLDC_DataCore.Struct.StPowerConsume _PowerConsume = new CLDC_DataCore.Struct.StPowerConsume();
                _PowerConsume.PowerConsumePrjID = _XmlNode.ChildNodes[_i].Attributes["bChecked"].Value;
                _PowerConsume.PowerConsumePrjName = _XmlNode.ChildNodes[_i].Attributes["ItemName"].Value;
                _PowerConsume.PrjParm = _XmlNode.ChildNodes[_i].Attributes["Para"].Value;

                _LstPowerConsume.Add(_PowerConsume);
            }
        }
        /// <summary>
        /// 存储功耗方案
        /// </summary>
        public void Save()
        {
            if (_LstPowerConsume.Count == 0)
                return;
            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl();
            _XmlNode.appendchild("", "PowerConsume", "Name", Name);
            for (int _i = 0; _i < _LstPowerConsume.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    , "R"
                                    , "bChecked"
                                    , _LstPowerConsume[_i].PowerConsumePrjID
                                    , "ItemName"
                                    , _LstPowerConsume[_i].PowerConsumePrjName
                                    , "Para"
                                    , _LstPowerConsume[_i].PrjParm);
            }
            _XmlNode.SaveXml(_FAPath);
        }

        /// <summary>
        /// 添加一个功耗项目
        /// </summary>
        /// /// <param name="sYn">是否要检</param>
        /// <param name="sItemName">功耗项目名称</param>
        /// <param name="sPara">参数</param>
        /// <returns></returns>
        public bool Add(int Order, string sYn, string sItemName, string sPara)
        {
            CLDC_DataCore.Struct.StPowerConsume _Item = new CLDC_DataCore.Struct.StPowerConsume();
            _Item.PowerConsumePrjID = sYn;
            _Item.PowerConsumePrjName = sItemName;
            _Item.PrjParm = sPara;
            if (_LstPowerConsume.Contains(_Item))
                Move(Order, _Item);
            else
                _LstPowerConsume.Insert(Order, _Item);
            return true;
        }

        /// <summary>
        /// 获取功耗项目
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StPowerConsume getPowerConsumePrj(int i)
        {
            if (i >= _LstPowerConsume.Count)
                return new CLDC_DataCore.Struct.StPowerConsume();
            return _LstPowerConsume[i];
        }

        /// <summary>
        /// 移动功耗项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i,CLDC_DataCore.Struct.StPowerConsume Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstPowerConsume.Count ? _LstPowerConsume.Count - 1 : i;
            this.Remove(Item);
            _LstPowerConsume.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 移除全部项目
        /// </summary>
        public void RemoveAll()
        {
            _LstPowerConsume.Clear();
        }

        /// <summary>
        /// 功耗项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstPowerConsume.Count;
            }
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstPowerConsume.Count)
                return;
            _LstPowerConsume.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(CLDC_DataCore.Struct.StPowerConsume Item)
        {
            if (!_LstPowerConsume.Contains(Item))
                return;
            _LstPowerConsume.Remove(Item);
            return;
        }
    }
}
