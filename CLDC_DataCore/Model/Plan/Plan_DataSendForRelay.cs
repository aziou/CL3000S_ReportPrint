
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using CLDC_Comm.Enum;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Struct;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 数据转发
    /// </summary>
    [Serializable()]
    public class Plan_DataSendForRelay : Plan_Base  
    {
        private List<StDataSendForRelay> _LstDataSendForRelay;



        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag;




          /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型</param>
        /// <param name="FAName">方案名称</param>
        public Plan_DataSendForRelay(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_DATASEND_FOLDERNAME, TaiType, vFAName)
        {
            _csDataFlag = new CLDC_DataCore.SystemModel.Item.csDataFlag();
            _csDataFlag.Load();

            this.Load();
        }
        ~Plan_DataSendForRelay()
        {
            _LstDataSendForRelay = null;
        }



        /// <summary>
        /// 加载方案
        /// </summary>
        private bool Load()
        {
            _LstDataSendForRelay = new List<StDataSendForRelay>();
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
            if (_ErrorString != "")
                return false;
            try
            {
                for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
                {
                    StDataSendForRelay _Item = new StDataSendForRelay();
                    _Item.PrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;                                                                 //项目ID
                    _Item.ConnProtocolItem = _XmlNode.ChildNodes[_i].Attributes["ConnProtocolItem"].Value.Trim();  //数据项名称
                    _Item.ItemCode = _XmlNode.ChildNodes[_i].Attributes["ItemCode"].Value.Trim();                                                    //标识编码
                    _Item.BarCode =_XmlNode.ChildNodes[_i].Attributes["BarCode"].Value.Trim();                                          //条形码
                    _Item.MeterPosition = _XmlNode.ChildNodes[_i].Attributes["MeterPosition"].Value.Trim();                                   //表位号
                    _Item.PARAMS_LIST = _XmlNode.ChildNodes[_i].Attributes["PARAMS_LIST"].Value;                                                    //参数值
                    _Item.WriteContent = _XmlNode.ChildNodes[_i].Attributes["WriteContent"].Value.Trim();                                            //写入内容
                    _Item.PROTOCOL = _XmlNode.ChildNodes[_i].Attributes["PROTOCOL"].Value.Trim();                                            //通讯规约
                    _LstDataSendForRelay.Add(_Item);

                }
                return true;
            }
            catch (Exception ex)
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
            if (_LstDataSendForRelay.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();

            _XmlNode.appendchild("", "ConnProtocol", "Name", Name);
            for (int _i = 0; _i < _LstDataSendForRelay.Count; _i++)
            {
                StDataSendForRelay _Item = _LstDataSendForRelay[_i];
                XmlNode _ChildNode = _XmlNode.appendchild(true
                                                    , "R"
                                                    , "PrjID"
                                                    , _Item.PrjID
                                                    , "ConnProtocolItem"
                                                    , _Item.ConnProtocolItem
                                                    , "ItemCode"
                                                    , _Item.ItemCode
                                                    , "BarCode"
                                                    , _Item.BarCode
                                                    , "MeterPosition"
                                                    , _Item.MeterPosition
                                                    , "PARAMS_LIST"
                                                    , _Item.PARAMS_LIST
                                                    , "PROTOCOL"
                                                    , _Item.PROTOCOL
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
                        , string itemcode
                        , string barCode
                        , string meterPosition
                        , string pARAMS_LIST
                        , string writecontent
                        , string pROTOCOL)
        {
            StDataSendForRelay _Item = new StDataSendForRelay();
            string strProtocolNo = _csDataFlag.GetDataFlagNo(connProtocolItem).ToString().PadLeft(2, '0');
            _Item.PrjID = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.数据转发试验).ToString() + strProtocolNo;
            _Item.ConnProtocolItem = connProtocolItem;
            _Item.ItemCode = itemcode;
            _Item.BarCode = barCode;
            _Item.MeterPosition = meterPosition;
            _Item.PARAMS_LIST = pARAMS_LIST;
            _Item.PROTOCOL = pROTOCOL;
            _Item.WriteContent = writecontent;
            if (_LstDataSendForRelay.Contains(_Item))
                return false;
            _LstDataSendForRelay.Add(_Item);
            return true;
        }



        public void RemoveAll()
        {
            _LstDataSendForRelay.Clear();
        }

        /// <summary>
        /// 返回方案中包含的项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstDataSendForRelay.Count;
            }
        }
        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StDataSendForRelay getDataSendForRelay(int i)
        {
            if (i >= _LstDataSendForRelay.Count)
                return new StDataSendForRelay();
            return _LstDataSendForRelay[i];
        }

        /// <summary>
        /// 移动项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StDataSendForRelay Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstDataSendForRelay.Count ? _LstDataSendForRelay.Count - 1 : i;
            this.Remove(Item);
            _LstDataSendForRelay.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstDataSendForRelay.Count)
                return;
            _LstDataSendForRelay.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StDataSendForRelay Item)
        {
            if (!_LstDataSendForRelay.Contains(Item))
                return;
            _LstDataSendForRelay.Remove(Item);
            return;
        }
    }
}



