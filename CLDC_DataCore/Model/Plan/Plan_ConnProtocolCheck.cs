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
    /// ͨѶЭ��������
    /// </summary>
    [Serializable()]
    public class Plan_ConnProtocolCheck:Plan_Base  
    {

        /// <summary>
        /// ͨѶЭ��������
        /// </summary>
        private List<StPlan_ConnProtocol> _LstConnProtocol;

        /// <summary>
        /// 
        /// </summary>
        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������</param>
        /// <param name="FAName">��������</param>
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
        /// ���ط���
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
                    _Item.PrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;                                                                 //��ĿID
                    _Item.ConnProtocolItem = _XmlNode.ChildNodes[_i].Attributes["ConnProtocolItem"].Value.Trim();  //����������
                    _Item.ItemCode = _XmlNode.ChildNodes[_i].Attributes["ItemCode"].Value.Trim();                                                    //��ʶ����
                    _Item.DataLen = int.Parse(_XmlNode.ChildNodes[_i].Attributes["DataLen"].Value.Trim());                                          //���ݳ���
                    _Item.PointIndex = int.Parse(_XmlNode.ChildNodes[_i].Attributes["PointIndex"].Value.Trim());                                   //С��λ����
                    _Item.StrDataType = _XmlNode.ChildNodes[_i].Attributes["StrDataType"].Value;                                                    //���ݸ�ʽ
                    _Item.OperType = (StMeterOperType)int.Parse(_XmlNode.ChildNodes[_i].Attributes["OperType"].Value.Trim());                       //��������,��/д
                    _Item.WriteContent = _XmlNode.ChildNodes[_i].Attributes["WriteContent"].Value.Trim();                                            //д������
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
        /// ���淽����XML�ĵ�
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
        /// ����һ����Ŀ
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
            _Item.PrjID = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.ͨѶЭ��������).ToString() + strProtocolNo + ((int)GetMeterOperType(opertype)).ToString();
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
                case "��":
                    data = StMeterOperType.��;
                    break;
                case "д":
                    data = StMeterOperType.д;
                    break;
                default:
                    data = StMeterOperType.д;
                    break;
            }
            return data;
        }
 
        public void RemoveAll()
        {
            _LstConnProtocol.Clear();
        }

        /// <summary>
        /// ���ط����а�������Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstConnProtocol.Count;
            }
        }
        /// <summary>
        /// �����б�����ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_ConnProtocol getConnProtocolPrj(int i)
        {
            if (i >= _LstConnProtocol.Count)
                return new StPlan_ConnProtocol();
            return _LstConnProtocol[i];
        }

        /// <summary>
        /// �ƶ���Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, StPlan_ConnProtocol Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstConnProtocol.Count ? _LstConnProtocol.Count - 1 : i;
            this.Remove(Item);
            _LstConnProtocol.Insert(i, Item);
            return;
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstConnProtocol.Count)
                return;
            _LstConnProtocol.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_ConnProtocol Item)
        {
            if (!_LstConnProtocol.Contains(Item))
                return;
            _LstConnProtocol.Remove(Item);
            return;
        }
    }
}
