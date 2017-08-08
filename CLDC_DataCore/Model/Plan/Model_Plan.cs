using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_Comm.Enum;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// �춨����
    /// </summary>
    [Serializable()]
    public class Model_Plan :Plan_Base 
    {

        /// <summary>
        /// �춨��Ŀ�б�
        /// </summary>
        public List<object> CheckPlan;
        
        private List<StFAGroup> _Plan;
        /// <summary>
        /// ��Ƿ����Ƿ�����޸�,��ӦGroup�����IsCanModofy����
        /// </summary>
        public bool isCanModify = true;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������</param>
        /// <param name="vFAName">��������</param>
        public Model_Plan(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, TaiType, vFAName)
        {
            if (CLDC_DataCore.Const.GlobalUnit.Plan_FromMDB == true)
            {
                this.LoadFromMDB();
            }
            else
            {
                this.LoadFromXml();
            }
        }

        ///// <summary>
        ///// �����ܷ�����Ϣ,��XML�ļ�
        ///// </summary>
        //private void LoadFromXml()
        //{
        //    _Plan = new List<StFAGroup>();
        //    string _ErrorString = "";
        //    XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
        //    XmlNode canBeModifyNode = null;
        //    if (_ErrorString != "")
        //        return;
        //    ///�ڷ���Group���ڵ�����isCanModofy���Խڵ�(����Ϊ1ʱ���Ա༭),��������ڸ�����,Ĭ��Ϊ1,���ݿ⣺����-1����ģ�壬��ֹ�༭
        //    canBeModifyNode = _XmlNode.Attributes.GetNamedItem("isCanModify");
        //    if (canBeModifyNode != null)
        //    {
        //        isCanModify = canBeModifyNode.Value.ToString() == "1";
        //        //System.Windows.Forms.MessageBox.Show((isCanModify) ? "��ʾ" : "����ʾ");
        //    }
        //    for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
        //    {
        //        StFAGroup _FA = new StFAGroup();
        //        _FA.FAType = (Cus_FAGroup)int.Parse(_XmlNode.ChildNodes[_i].Attributes["Name"].Value);
        //        _FA.FAName = _XmlNode.ChildNodes[_i].ChildNodes[0].Value;

        //        _Plan.Add(_FA);
        //    }
        //    return;
        //}
        /// <summary>
        /// �����ܷ�����Ϣ,�����ݿ��ļ�
        /// </summary>
        private void LoadFromMDB()
        {
            _Plan = new List<StFAGroup>();
            string _ErrorString = "";
            Plan_Scheme_Check dbCheck=new Plan_Scheme_Check(0,"");
            List<PlanModel.Scheme_Check> _XmlNode = dbCheck.GetList("", out _ErrorString);
            
            if (_ErrorString != "")
                return;
            int lstCount = _XmlNode.Count;
            for (int i = 0; i < lstCount; i++)
            {
                StFAGroup _FA = new StFAGroup();
                _FA.FAType = (Cus_FAGroup)(_XmlNode[i].schemeID);
                _FA.FAName = _XmlNode[i].chrPlanName;

                _Plan.Add(_FA);
            }
                ///�ڷ����ܱ��У�chrSchemeStatus������,Ĭ��Ϊ0,���Ա༭������1����ֹ�༭������-1����ģ�壬��ֹ�༭
                //canBeModifyNode = _XmlNode.Attributes.GetNamedItem("isCanModify");
                //if (canBeModifyNode != null)
                //{
                //    isCanModify = canBeModifyNode.Value.ToString() == "1";
                //    //System.Windows.Forms.MessageBox.Show((isCanModify) ? "��ʾ" : "����ʾ");
                //}
                //for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
                //{
                //    StFAGroup _FA = new StFAGroup();
                //    _FA.FAType = (Cus_FAGroup)int.Parse(_XmlNode.ChildNodes[_i].Attributes["Name"].Value);
                //    _FA.FAName = _XmlNode.ChildNodes[_i].ChildNodes[0].Value;

                //    _Plan.Add(_FA);
                //}
                return;
        }/// <summary>
        /// �����ܷ�����Ϣ
        /// </summary>
        private void LoadFromXml()
        {
            _Plan = new List<StFAGroup>();
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
            XmlNode canBeModifyNode = null;
            if (_ErrorString != "")
                return;
            ///�ڷ���Group���ڵ�����isCanModofy���Խڵ�(����Ϊ1ʱ���Ա༭),��������ڸ�����,Ĭ��Ϊ1
            canBeModifyNode = _XmlNode.Attributes.GetNamedItem("isCanModify");
            if (canBeModifyNode != null)
            {
                isCanModify = canBeModifyNode.Value.ToString() == "1";
                //System.Windows.Forms.MessageBox.Show((isCanModify) ? "��ʾ" : "����ʾ");
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StFAGroup _FA = new StFAGroup();
                _FA.FAType = (Cus_FAGroup)int.Parse(_XmlNode.ChildNodes[_i].Attributes["Name"].Value);
                _FA.FAName = _XmlNode.ChildNodes[_i].ChildNodes[0].Value;
                if (_XmlNode.ChildNodes[_i].Attributes["Index"] != null)
                {
                    _FA.index = int.Parse(_XmlNode.ChildNodes[_i].Attributes["Index"].Value);
                }
                else
                {
                    _FA.index = -1;
                }
                _Plan.Add(_FA);
            }
            return;
        }
        /// <summary>
        /// �洢�ܷ���XML�ĵ�
        /// </summary>
        public void Save()
        {
            if (_Plan.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "FAGroup", "Name", Name);
            for (int _i = 0; _i < _Plan.Count; _i++)
            {
                _XmlNode.appendchild("", "R", "Name", ((int)_Plan[_i].FAType).ToString(), "Index", _Plan[_i].index.ToString()
                                    , _Plan[_i].FAName);
            }

            _XmlNode.SaveXml(_FAPath);
        }
        ///// <summary>
        ///// �洢�ܷ���XML�ĵ�
        ///// </summary>
        //public void Save()
        //{
        //    if (_Plan.Count == 0)
        //        return;
        //    clsXmlControl _XmlNode = new clsXmlControl();
        //    _XmlNode.appendchild("", "FAGroup", "Name", Name);

        //    for (int _i = 0; _i < _Plan.Count; _i++)
        //    {
        //        _XmlNode.appendchild("", "R", "Name", ((int)_Plan[_i].FAType).ToString()
        //                            , _Plan[_i].FAName);
        //    }

        //    _XmlNode.SaveXml(_FAPath);
        //}
        /// <summary>
        /// ���һ����������
        /// </summary>
        /// <param name="FAType">��������</param>
        /// <param name="vFAName">��������</param>
        /// <param name="Order">�춨˳��</param>
        public void Add(Cus_FAGroup FAType, string vFAName, int Order)
        {
            StFAGroup _Item = new StFAGroup();
            _Item.FAType = FAType;
            _Item.FAName = vFAName;
            //2011/6/8,���λ�����
            _Item.index = Order;
            if (_Plan.Contains(_Item))
                _Plan.Remove(_Item);
            if (_Plan.Count == 0)
                _Plan.Add(_Item);
            else
                Move(Order, _Item);
            return;
        }
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _Plan.Count;
            }
        }
        /// <summary>
        /// �����б�����ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StFAGroup getFAPrj(int i)
        {
            if (i >= _Plan.Count)
                return new StFAGroup();
            return _Plan[i];
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _Plan.Count)
                return;
            _Plan.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StFAGroup Item)
        {
            if (!_Plan.Contains(Item))
                return;
            _Plan.Remove(Item);
            return;
        }
        /// <summary>
        /// �������б�
        /// </summary>
        public void Clear()
        {
            _Plan.Clear();
        }
        /// <summary>
        /// �ƶ���Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, StFAGroup Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _Plan.Count ? _Plan.Count  : i;
            this.Remove(Item);
            _Plan.Insert(i, Item);
            return;
        }

        /// <summary>
        /// ��������������������������Ǵ���������Ŀ�б���09-7-2���Ժ�ʹ��
        /// </summary>
        /// <returns></returns>
        public List<object> CreateFA(Model.DnbModel.DnbInfo.MeterBasicInfo mb_Info,ref string Ib,ref int Qs,ref string WcLimit)
        {
            List<object> CheckFaItem = new List<object>();

            if (_Plan.Count == 0) return CheckFaItem;

            for (int i = 0; i < _Plan.Count; i++)
            {
                StFAGroup _Item = _Plan[i];
                switch (_Item.FAType)
                {
                    case Cus_FAGroup.Ԥ�ȵ���:              //Ԥ�ȷ�������
                        Plan_PrepareTest _Pre = new Plan_PrepareTest(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Pre.Count; j++)
                            CheckFaItem.Add(_Pre.getDgnPrj(j));
                        _Pre = null;
                        break;
                    case Cus_FAGroup.Ԥ������:              //Ԥ�ȷ�������
                        Plan_YuRe _YuRe = new Plan_YuRe(_TaiType, _Item.FAName);
                        for (int j = 0; j < _YuRe.Count; j++)
                            CheckFaItem.Add(_YuRe.getYuRePrj(j));
                        _YuRe = null;
                        break;
                    case Cus_FAGroup.��ۼ������:              //��ۼ�����鷽������
                        Plan_WGJC _WGJC = new Plan_WGJC(_TaiType, _Item.FAName);
                        for (int j = 0; j < _WGJC.Count; j++)
                            CheckFaItem.Add(_WGJC.getWGJCPrj(j));
                        _WGJC = null;
                        break;
                    case Cus_FAGroup.������:              //����������Ŀ����
                        Plan_QiDong _QiDong = new Plan_QiDong(_TaiType, _Item.FAName);
                        for (int j = 0; j < _QiDong.Count; j++)
                        {
                            StPlan_QiDong stQiD = _QiDong.getQiDongPrj(j);
                            stQiD.CheckTimeAndIb(mb_Info.GuiChengName, CLDC_DataCore.Const.GlobalUnit.Clfs,
                                              CLDC_DataCore.Const.GlobalUnit.U,
                                              mb_Info.Mb_chrIb,
                                              mb_Info.Mb_chrBdj,
                                              mb_Info.Mb_chrBcs,
                                              mb_Info.Mb_BlnZnq,
                                              mb_Info.Mb_BlnHgq);
                            CheckFaItem.Add(stQiD);
                        }
                        _QiDong = null;
                        break;
                    case Cus_FAGroup.Ǳ������:              //Ǳ����Ŀ��������
                        Plan_QianDong _QianDong = new Plan_QianDong(_TaiType, _Item.FAName);
                        for (int j = 0; j < _QianDong.Count; j++)
                        {
                            StPlan_QianDong stQianD = _QianDong.getQianDongPrj(j);
                            stQianD.CheckTimeAndIb(mb_Info.GuiChengName,
                                                   CLDC_DataCore.Const.GlobalUnit.Clfs,
                                                   CLDC_DataCore.Const.GlobalUnit.U,
                                                   mb_Info.Mb_chrIb,
                                                   mb_Info.Mb_chrBdj,
                                                   mb_Info.Mb_chrBcs,
                                                   mb_Info.Mb_BlnZnq,
                                                   mb_Info.Mb_BlnHgq);
                            CheckFaItem.Add(stQianD);
                        }
                        _QianDong = null;
                        break;
                    case Cus_FAGroup.�����������:          //����������鷽������
                        Plan_WcPoint _Wc = new Plan_WcPoint(_TaiType, _Item.FAName);

                        Ib = _Wc.Qscz;

                        Qs = _Wc.Czqs;

                        WcLimit = _Wc.CzWcLimit;

                        for (int j = 0; j < _Wc.Count; j++)
                            CheckFaItem.Add(_Wc.getCheckPoint(j));
                        _Wc = null;
                        break;
                    case Cus_FAGroup.��������:          //�������鷽����Ŀ����
                        Plan_ZouZi _Zouzi = new Plan_ZouZi(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Zouzi.Count; j++)
                        {
                            StPlan_ZouZi _ZouPrjPlan = _Zouzi.getZouZiPrj(j);
                            for (int z = 0; z < _ZouPrjPlan.ZouZiPrj.Count; z++)
                            {
                                CheckFaItem.Add(_ZouPrjPlan.getNewPlan(z));
                            }
                        }
                        _Zouzi = null;
                        break;
                    case Cus_FAGroup.�๦������:        //�๦�����鷽����Ŀ����
                        Plan_Dgn _Dgn = new Plan_Dgn(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Dgn.Count; j++)
                            CheckFaItem.Add(_Dgn.getDgnPrj(j));
                        _Dgn = null;
                        break;
                    case Cus_FAGroup.ͨѶЭ��������:        //ͨѶЭ��������
                        Plan_ConnProtocolCheck _Cpc = new Plan_ConnProtocolCheck(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Cpc.Count; j++)
                            CheckFaItem.Add(_Cpc.getConnProtocolPrj(j));
                        _Dgn = null;
                        break;
                    case Cus_FAGroup.Ӱ��������:
                        Plan_Specal _Specal = new Plan_Specal(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Specal.Count; j++)
                            CheckFaItem.Add(_Specal.getSpecalPrj(j));
                        _Specal = null;
                        break;
                    case Cus_FAGroup.�ز�����:
                        Plan_Carrier _ZaiBo = new Plan_Carrier(_TaiType, _Item.FAName);
                        for (int j = 0; j < _ZaiBo.Count; j++)
                            CheckFaItem.Add(_ZaiBo.GetCarrierPrj(j));
                        _ZaiBo = null;
                        break;
                    case Cus_FAGroup.���һ����:
                        Plan_ErrAccord _ErrAccord = new Plan_ErrAccord(_TaiType, _Item.FAName);
                        for (int j = 0; j < _ErrAccord.Count; j++)
                            CheckFaItem.Add(_ErrAccord.getErrAccordPrj(j));
                        _ErrAccord = null;
                        break;
                    case Cus_FAGroup.��������:
                        {
                            Plan_PowerConsume _PowerConsume = new Plan_PowerConsume(_TaiType, _Item.FAName);
                            for (int j = 0; j < _PowerConsume.Count; j++)
                                CheckFaItem.Add(_PowerConsume.getPowerConsumePrj(j));
                            _PowerConsume = null;
                        }
                        break;
                    case Cus_FAGroup.���Ṧ������:
                        Plan_Freeze _Freeze = new Plan_Freeze(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Freeze.Count; j++)
                            CheckFaItem.Add(_Freeze.getFreezePrj(j));
                        _Freeze = null;
                        break;
                    case Cus_FAGroup.�ѿع�������:
                        Plan_CostControl _CostControl = new Plan_CostControl(_TaiType, _Item.FAName);
                        for (int j = 0; j < _CostControl.Count; j++)
                            CheckFaItem.Add(_CostControl.getCostControlPrj(j));
                        _CostControl = null;
                        break;
                    case Cus_FAGroup.���ܱ�������:
                        Plan_Function _Function = new Plan_Function(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Function.Count; j++)
                            CheckFaItem.Add(_Function.getFunctionPrj(j));
                        _Function = null;
                        break;
                    case Cus_FAGroup.�¼���¼����:
                        Plan_EventLog _Eventlog = new Plan_EventLog(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Eventlog.Count; j++)
                            CheckFaItem.Add(_Eventlog.getEventLogPrj(j));
                        _Eventlog = null;
                        break;

                    case Cus_FAGroup.����ת������:
                        Plan_DataSendForRelay _DataSend = new Plan_DataSendForRelay(_TaiType, _Item.FAName);
                        for (int j = 0; j < _DataSend.Count; j++)
                            CheckFaItem.Add(_DataSend.getDataSendForRelay(j));
                        _DataSend = null;
                        break;
                    case Cus_FAGroup.��Ƶ��ѹ����:
                        Plan_Insulation planInsulation = new Plan_Insulation(_TaiType, _Item.FAName);
                        for (int j = 0; j < planInsulation.Count; j++)
                            CheckFaItem.Add(planInsulation.GetPlan(j));
                        planInsulation = null;
                        break;
                    case Cus_FAGroup.�������ݱȶ�����:
                        Plan_Infrared planInfrared = new Plan_Infrared(_TaiType, _Item.FAName);
                        for (int j = 0; j < planInfrared.Count; j++)
                            CheckFaItem.Add(planInfrared.GetCarrierPrj(j));
                        planInfrared = null;
                        break;
                    case Cus_FAGroup.���ɼ�¼����:
                        Plan_LoadRecord planA = new Plan_LoadRecord(_TaiType, _Item.FAName);
                        for (int j = 0; j < planA.Count; j++)
                            CheckFaItem.Add(planA.GetCurrentPrj(j));
                        planA = null;
                        break;
                }
            }
            return CheckFaItem;
        }


        #region ��Ӱ���ȡXml��� 2011/6/2 by netteans@163.com
        public int[] GetIndexs(out string[] names)
        {
            StFAGroup[] sts = this._Plan.ToArray();
            int[] rarr = new int[sts.Length];
            names = new string[sts.Length];
            for (int i = 0; i < sts.Length; i++)
            {
                rarr[i] = sts[i].index;
                names[i] = sts[i].FAType.ToString();
            }
            return rarr;
        }
        #endregion

        #region �Ѿ����õķ���,�޵���
        /// <summary>
        /// ����������09-7-2�������
        /// </summary>
        /// <param name="Current">��������1.5(6)</param>
        /// <param name="MeConst">��ǰ�ñ��� �й����޹���</param>
        /// <param name="MinConst">��ǰ���б�����С����(���飬�±�0=�й���1=�޹�)</param>
        /// <param name="WcLimit">������б����</param>
        /// <param name="GuiChengName">������ƣ�JJG596-1999��</param>
        /// <param name="DjString">�ȼ��ַ���1.0(2.0)</param>
        /// <param name="Hgq">�Ƿ񾭻�����1-����0-����</param>
        //public void CreateFA(string Current, string MeConst, int[] MinConst, string GuiChengName, string DjString, int Hgq,CLDC_Comm.Enum.Cus_Clfs clfs,float U,bool znq)
        //{
        //    CheckPlan = new List<object>();
        //    Error_CheckPoint _Wc=null;                 //�������������Ϊ�˼ӿ��ٶ�


        //    for (int i = 0; i < _Plan.Count; i++)
        //    {
        //        StFAGroup _Item = _Plan[i];
   
        //        switch (_Item.FAType)
        //        { 
        //            case Cus_FAGroup.Ԥ��: 
        //                {

        //                    Error_YuRe _YuRe = new Error_YuRe(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _YuRe.Count; j++)
        //                    {
        //                        CheckPlan.Add(_YuRe.getYuRePrj(j));
        //                    }
        //                    _YuRe = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.����:
        //                {

        //                    Error_QiDong _QiDong = new Error_QiDong(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _QiDong.Count; j++)
        //                    {
        //                        StQiDong _QiDongItem = _QiDong.getQiDongPrj(j);
        //                        _QiDongItem.CheckTimeAndIb(GuiChengName, clfs, U, Current, DjString, MeConst, znq, Hgq==0?false:true);
        //                        CheckPlan.Add(_QiDongItem);
        //                    }
        //                    _QiDong = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.Ǳ��:
        //                {

        //                    Error_QianDong _QianDong = new Error_QianDong(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _QianDong.Count; j++)
        //                    {
        //                        StQianDong _QianDongItem = _QianDong.getQianDongPrj(j);
        //                        _QianDongItem.CheckTimeAndIb(GuiChengName, clfs, U, Current, DjString, MeConst, znq, Hgq==0?false:true);
        //                        CheckPlan.Add(_QianDongItem);
        //                    }
        //                    _QianDong = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.�����������:
        //                {

        //                    if (_Wc == null)
        //                    { 
        //                        _Wc= new Error_CheckPoint(_TaiType, _Item.FAName);
        //                    }

        //                    _Wc.SetQs(Current, MeConst, MinConst);
        //                    _Wc.SetWcx(GuiChengName, DjString, Hgq);
        //                    for (int j = 0; j < _Wc.Count; j++)
        //                    {
        //                        CheckPlan.Add(_Wc.getCheckPoint(j));
        //                    }
        //                    //_Wc = null;

        //                    break;
        //                }
        //            case Cus_FAGroup.��������:
        //                {

        //                    Plan_ZouZi _Zouzi = new Plan_ZouZi(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _Zouzi.Count; j++)
        //                    {
        //                        StZouZiPlan _ZouPrjPlan = _Zouzi.getZouZiPrj(j);
        //                        for (int z = 0; z < _ZouPrjPlan.ZouZiPrj.Count;z++ )
        //                        {
        //                            CheckPlan.Add(_ZouPrjPlan.getNewPlan(z));
        //                        }
        //                    }
        //                    _Zouzi = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.�๦������:
        //                {

        //                    Plan_Dgn _Dgn = new Plan_Dgn(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _Dgn.Count; j++)
        //                    {
        //                        CheckPlan.Add(_Dgn.getDgnPrj(j));
        //                    }
        //                    _Dgn = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.ͨѶЭ��������:
        //                {

        //                    Plan_ConnProtocolCheck cpc = new Plan_ConnProtocolCheck(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < cpc.Count; j++)
        //                    {
        //                        CheckPlan.Add(cpc.getConnProtocolPrj(j));
        //                    }
        //                    cpc = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.Ӱ��������:
        //                {

        //                    Plan_Specal _Specal = new Plan_Specal(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _Specal.Count; j++)
        //                    {
        //                        CheckPlan.Add(_Specal.getSpecalPrj(j));
        //                    }
        //                    _Specal = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.�ز�����:
        //                {
        //                    Plan_Carrier _Carrier = new Plan_Carrier(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _Carrier.Count; j++)
        //                    {
        //                        CheckPlan.Add(_Carrier.GetCarrierPrj(j));
        //                    }
        //                    _Carrier = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.���һ����:
        //                {
        //                    Plan_ErrAccord _ErrAccord = new Plan_ErrAccord(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _ErrAccord.Count; j++)
        //                    {
        //                        CheckPlan.Add(_ErrAccord.getErrAccordPrj(j));
        //                    }
        //                    _ErrAccord = null;
        //                    break;
        //                }
        //        }
            
        //    }

        //    _Wc = null;
        //}
        #endregion

    }
}
