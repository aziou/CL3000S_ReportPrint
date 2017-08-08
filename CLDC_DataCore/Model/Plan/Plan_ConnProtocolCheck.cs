using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using System.Xml;
using CLDC_Comm.Enum;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 通讯协议检查试验
    /// </summary>
    [Serializable()]
    public class Plan_ConnProtocolCheck:Plan_Base  
    {

        /// <summary>
        /// 通讯协议检查试验
        /// </summary>
        private List<StPlan_ConnProtocol> _LstConnProtocol;

        /// <summary>
        /// 
        /// </summary>
        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型</param>
        /// <param name="FAName">方案名称</param>
        public Plan_ConnProtocolCheck(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_CONNPROTOCOL_FOLDERNAME, TaiType, vFAName)
        {
            _csDataFlag = new CLDC_DataCore.SystemModel.Item.csDataFlag();
            _csDataFlag.Load();

            this.Load();
        }
        ~Plan_ConnProtocolCheck()
        {
            _LstConnProtocol = null;
        }
        /// <summary>
        /// 加载方案
        /// </summary>
        private bool  Load()
        {
            _LstConnProtocol = new List<StPlan_ConnProtocol>();
            string _ErrorString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
            if (_ErrorString != "")
                return false;
            try
            {
                for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
                {
                    StPlan_ConnProtocol _Item = new StPlan_ConnProtocol();
                    _Item.PrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;                                                                 //项目ID
                    _Item.ConnProtocolItem = _XmlNode.ChildNodes[_i].Attributes["ConnProtocolItem"].Value.Trim();  //数据项名称
                    _Item.ItemCode = _XmlNode.ChildNodes[_i].Attributes["ItemCode"].Value.Trim();                                                    //标识编码
                    _Item.DataLen = int.Parse(_XmlNode.ChildNodes[_i].Attributes["DataLen"].Value.Trim());                                          //数据长度
                    _Item.PointIndex = int.Parse(_XmlNode.ChildNodes[_i].Attributes["PointIndex"].Value.Trim());                                   //小数位索引
                    _Item.StrDataType = _XmlNode.ChildNodes[_i].Attributes["StrDataType"].Value;                                                    //数据格式
                    _Item.OperType = (StMeterOperType)int.Parse(_XmlNode.ChildNodes[_i].Attributes["OperType"].Value.Trim());                       //操作类型,读/写
                    _Item.WriteContent = _XmlNode.ChildNodes[_i].Attributes["WriteContent"].Value.Trim();                                            //写入内容
                    _LstConnProtocol.Add(_Item);
                    
                }
                return true ;
            }
            catch(Exception ex)
            {
                CLDC_DataCore.Function.ErrorLog.Write(ex);
                return false;
            }
        }

        /// <summary>
        /// 保存方案到XML文档
        /// </summary>
        public void Save()
        {
            if (System.IO.File.Exists(_FAPath))
            {
                System.IO.File.Delete(_FAPath);
            }
            if (_LstConnProtocol.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();

            _XmlNode.appendchild("", "ConnProtocol", "Name", Name);
            for (int _i = 0; _i < _LstConnProtocol.Count; _i++)
            {
                StPlan_ConnProtocol _Item = _LstConnProtocol[_i];
                XmlNode _ChildNode=_XmlNode.appendchild(true
                                                    ,"R"
                                                    ,"PrjID"
                                                    , _Item.PrjID
                                                    , "ConnProtocolItem"
                                                    , _Item.ConnProtocolItem
                                                    , "ItemCode"
                                                    , _Item.ItemCode
                                                    , "DataLen"
                                                    , _Item.DataLen.ToString()
                                                    , "PointIndex"
                                                    , _Item.PointIndex.ToString()
                                                    , "StrDataType"
                                                    , _Item.StrDataType
                                                    , "OperType"
                                                    , ((int)_Item.OperType).ToString()
                                                    , "WriteContent"
                                                    , _Item.WriteContent);
            }
            _XmlNode.SaveXml(_FAPath);
            return;
        }

        /// <summary>
        /// 增加一个项目
        /// </summary>
        /// <param name="connProtocolItem"></param>
        /// <param name="itemcode"></param>
        /// <param name="datalen"></param>
        /// <param name="pointindex"></param>
        /// <param name="datatype"></param>
        /// <param name="opertype"></param>
        /// <param name="writecontent"></param>
        /// <returns></returns>
        public bool Add(string connProtocolItem
                        ,string itemcode
                        ,string datalen
                        ,string pointindex
                        ,string datatype
                        , string opertype
                        ,string writecontent)
        {
            StPlan_ConnProtocol _Item = new StPlan_ConnProtocol();
            string strProtocolNo = itemcode;//_csDataFlag.GetDataFlagNo(connProtocolItem).ToString().PadLeft(2, '0');
            _Item.PrjID = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString() + strProtocolNo + ((int)GetMeterOperType(opertype)).ToString();
            _Item.ConnProtocolItem = connProtocolItem;
            _Item.ItemCode = itemcode;
            _Item.DataLen = int.Parse(datalen);
            _Item.PointIndex = int.Parse(pointindex);
            _Item.StrDataType = datatype;
            _Item.OperType = GetMeterOperType(opertype);
            _Item.WriteContent = writecontent;
            if (_LstConnProtocol.Contains(_Item))
                return false;
            _LstConnProtocol.Add(_Item);
            return true;
        }

        private StMeterOperType GetMeterOperType(string opertype)
        {
            StMeterOperType data;
            switch (opertype)
            {
                case "读":
                    data = StMeterOperType.读;
                    break;
                case "写":
                    data = StMeterOperType.写;
                    break;
                default:
                    data = StMeterOperType.写;
                    break;
            }
            return data;
        }
 
        public void RemoveAll()
        {
            _LstConnProtocol.Clear();
        }

        /// <summary>
        /// 返回方案中包含的项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstConnProtocol.Count;
            }
        }
        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StPlan_ConnProtocol getConnProtocolPrj(int i)
        {
            if (i >= _LstConnProtocol.Count)
                return new StPlan_ConnProtocol();
            return _LstConnProtocol[i];
        }

        /// <summary>
        /// 移动项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StPlan_ConnProtocol Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstConnProtocol.Count ? _LstConnProtocol.Count - 1 : i;
            this.Remove(Item);
            _LstConnProtocol.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstConnProtocol.Count)
                return;
            _LstConnProtocol.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StPlan_ConnProtocol Item)
        {
            if (!_LstConnProtocol.Contains(Item))
                return;
            _LstConnProtocol.Remove(Item);
            return;
        }
    }
}
