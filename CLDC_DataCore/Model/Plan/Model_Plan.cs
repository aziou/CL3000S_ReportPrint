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
    /// 检定方案
    /// </summary>
    [Serializable()]
    public class Model_Plan :Plan_Base 
    {

        /// <summary>
        /// 检定项目列表
        /// </summary>
        public List<object> CheckPlan;
        
        private List<StFAGroup> _Plan;
        /// <summary>
        /// 标记方案是否可以修改,对应Group下面的IsCanModofy属性
        /// </summary>
        public bool isCanModify = true;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型</param>
        /// <param name="vFAName">方案名称</param>
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
        ///// 加载总方案信息,从XML文件
        ///// </summary>
        //private void LoadFromXml()
        //{
        //    _Plan = new List<StFAGroup>();
        //    string _ErrorString = "";
        //    XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
        //    XmlNode canBeModifyNode = null;
        //    if (_ErrorString != "")
        //        return;
        //    ///在方案Group根节点增加isCanModofy属性节点(属性为1时可以编辑),如果不存在该属性,默认为1,数据库：等于-1，是模板，禁止编辑
        //    canBeModifyNode = _XmlNode.Attributes.GetNamedItem("isCanModify");
        //    if (canBeModifyNode != null)
        //    {
        //        isCanModify = canBeModifyNode.Value.ToString() == "1";
        //        //System.Windows.Forms.MessageBox.Show((isCanModify) ? "显示" : "不显示");
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
        /// 加载总方案信息,从数据库文件
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
                ///在方案总表中，chrSchemeStatus该属性,默认为0,可以编辑；等于1，禁止编辑；等于-1，是模板，禁止编辑
                //canBeModifyNode = _XmlNode.Attributes.GetNamedItem("isCanModify");
                //if (canBeModifyNode != null)
                //{
                //    isCanModify = canBeModifyNode.Value.ToString() == "1";
                //    //System.Windows.Forms.MessageBox.Show((isCanModify) ? "显示" : "不显示");
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
        /// 加载总方案信息
        /// </summary>
        private void LoadFromXml()
        {
            _Plan = new List<StFAGroup>();
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
            XmlNode canBeModifyNode = null;
            if (_ErrorString != "")
                return;
            ///在方案Group根节点增加isCanModofy属性节点(属性为1时可以编辑),如果不存在该属性,默认为1
            canBeModifyNode = _XmlNode.Attributes.GetNamedItem("isCanModify");
            if (canBeModifyNode != null)
            {
                isCanModify = canBeModifyNode.Value.ToString() == "1";
                //System.Windows.Forms.MessageBox.Show((isCanModify) ? "显示" : "不显示");
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
        /// 存储总方案XML文档
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
        ///// 存储总方案XML文档
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
        /// 添加一个方案内容
        /// </summary>
        /// <param name="FAType">方案类型</param>
        /// <param name="vFAName">方案名称</param>
        /// <param name="Order">检定顺序</param>
        public void Add(Cus_FAGroup FAType, string vFAName, int Order)
        {
            StFAGroup _Item = new StFAGroup();
            _Item.FAType = FAType;
            _Item.FAName = vFAName;
            //2011/6/8,添加位置序号
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
        /// 返回项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _Plan.Count;
            }
        }
        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StFAGroup getFAPrj(int i)
        {
            if (i >= _Plan.Count)
                return new StFAGroup();
            return _Plan[i];
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _Plan.Count)
                return;
            _Plan.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StFAGroup Item)
        {
            if (!_Plan.Contains(Item))
                return;
            _Plan.Remove(Item);
            return;
        }
        /// <summary>
        /// 清理方案列表
        /// </summary>
        public void Clear()
        {
            _Plan.Clear();
        }
        /// <summary>
        /// 移动项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StFAGroup Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _Plan.Count ? _Plan.Count  : i;
            this.Remove(Item);
            _Plan.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 创建方案，这个创建方案仅仅是创建方案项目列表，在09-7-2日以后使用
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
                    case Cus_FAGroup.预先调试:              //预热方案加载
                        Plan_PrepareTest _Pre = new Plan_PrepareTest(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Pre.Count; j++)
                            CheckFaItem.Add(_Pre.getDgnPrj(j));
                        _Pre = null;
                        break;
                    case Cus_FAGroup.预热试验:              //预热方案加载
                        Plan_YuRe _YuRe = new Plan_YuRe(_TaiType, _Item.FAName);
                        for (int j = 0; j < _YuRe.Count; j++)
                            CheckFaItem.Add(_YuRe.getYuRePrj(j));
                        _YuRe = null;
                        break;
                    case Cus_FAGroup.外观检查试验:              //外观检查试验方案加载
                        Plan_WGJC _WGJC = new Plan_WGJC(_TaiType, _Item.FAName);
                        for (int j = 0; j < _WGJC.Count; j++)
                            CheckFaItem.Add(_WGJC.getWGJCPrj(j));
                        _WGJC = null;
                        break;
                    case Cus_FAGroup.起动试验:              //启动方案项目加载
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
                    case Cus_FAGroup.潜动试验:              //潜动项目方案加载
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
                    case Cus_FAGroup.基本误差试验:          //基本误差试验方案加载
                        Plan_WcPoint _Wc = new Plan_WcPoint(_TaiType, _Item.FAName);

                        Ib = _Wc.Qscz;

                        Qs = _Wc.Czqs;

                        WcLimit = _Wc.CzWcLimit;

                        for (int j = 0; j < _Wc.Count; j++)
                            CheckFaItem.Add(_Wc.getCheckPoint(j));
                        _Wc = null;
                        break;
                    case Cus_FAGroup.走字试验:          //走字试验方案项目加载
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
                    case Cus_FAGroup.多功能试验:        //多功能试验方案项目加载
                        Plan_Dgn _Dgn = new Plan_Dgn(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Dgn.Count; j++)
                            CheckFaItem.Add(_Dgn.getDgnPrj(j));
                        _Dgn = null;
                        break;
                    case Cus_FAGroup.通讯协议检查试验:        //通讯协议检查试验
                        Plan_ConnProtocolCheck _Cpc = new Plan_ConnProtocolCheck(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Cpc.Count; j++)
                            CheckFaItem.Add(_Cpc.getConnProtocolPrj(j));
                        _Dgn = null;
                        break;
                    case Cus_FAGroup.影响量试验:
                        Plan_Specal _Specal = new Plan_Specal(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Specal.Count; j++)
                            CheckFaItem.Add(_Specal.getSpecalPrj(j));
                        _Specal = null;
                        break;
                    case Cus_FAGroup.载波试验:
                        Plan_Carrier _ZaiBo = new Plan_Carrier(_TaiType, _Item.FAName);
                        for (int j = 0; j < _ZaiBo.Count; j++)
                            CheckFaItem.Add(_ZaiBo.GetCarrierPrj(j));
                        _ZaiBo = null;
                        break;
                    case Cus_FAGroup.误差一致性:
                        Plan_ErrAccord _ErrAccord = new Plan_ErrAccord(_TaiType, _Item.FAName);
                        for (int j = 0; j < _ErrAccord.Count; j++)
                            CheckFaItem.Add(_ErrAccord.getErrAccordPrj(j));
                        _ErrAccord = null;
                        break;
                    case Cus_FAGroup.功耗试验:
                        {
                            Plan_PowerConsume _PowerConsume = new Plan_PowerConsume(_TaiType, _Item.FAName);
                            for (int j = 0; j < _PowerConsume.Count; j++)
                                CheckFaItem.Add(_PowerConsume.getPowerConsumePrj(j));
                            _PowerConsume = null;
                        }
                        break;
                    case Cus_FAGroup.冻结功能试验:
                        Plan_Freeze _Freeze = new Plan_Freeze(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Freeze.Count; j++)
                            CheckFaItem.Add(_Freeze.getFreezePrj(j));
                        _Freeze = null;
                        break;
                    case Cus_FAGroup.费控功能试验:
                        Plan_CostControl _CostControl = new Plan_CostControl(_TaiType, _Item.FAName);
                        for (int j = 0; j < _CostControl.Count; j++)
                            CheckFaItem.Add(_CostControl.getCostControlPrj(j));
                        _CostControl = null;
                        break;
                    case Cus_FAGroup.智能表功能试验:
                        Plan_Function _Function = new Plan_Function(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Function.Count; j++)
                            CheckFaItem.Add(_Function.getFunctionPrj(j));
                        _Function = null;
                        break;
                    case Cus_FAGroup.事件记录试验:
                        Plan_EventLog _Eventlog = new Plan_EventLog(_TaiType, _Item.FAName);
                        for (int j = 0; j < _Eventlog.Count; j++)
                            CheckFaItem.Add(_Eventlog.getEventLogPrj(j));
                        _Eventlog = null;
                        break;

                    case Cus_FAGroup.数据转发试验:
                        Plan_DataSendForRelay _DataSend = new Plan_DataSendForRelay(_TaiType, _Item.FAName);
                        for (int j = 0; j < _DataSend.Count; j++)
                            CheckFaItem.Add(_DataSend.getDataSendForRelay(j));
                        _DataSend = null;
                        break;
                    case Cus_FAGroup.工频耐压试验:
                        Plan_Insulation planInsulation = new Plan_Insulation(_TaiType, _Item.FAName);
                        for (int j = 0; j < planInsulation.Count; j++)
                            CheckFaItem.Add(planInsulation.GetPlan(j));
                        planInsulation = null;
                        break;
                    case Cus_FAGroup.红外数据比对试验:
                        Plan_Infrared planInfrared = new Plan_Infrared(_TaiType, _Item.FAName);
                        for (int j = 0; j < planInfrared.Count; j++)
                            CheckFaItem.Add(planInfrared.GetCarrierPrj(j));
                        planInfrared = null;
                        break;
                    case Cus_FAGroup.负荷记录试验:
                        Plan_LoadRecord planA = new Plan_LoadRecord(_TaiType, _Item.FAName);
                        for (int j = 0; j < planA.Count; j++)
                            CheckFaItem.Add(planA.GetCurrentPrj(j));
                        planA = null;
                        break;
                }
            }
            return CheckFaItem;
        }


        #region 添加按获取Xml序号 2011/6/2 by netteans@163.com
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

        #region 已经弃用的方法,无调用
        /// <summary>
        /// 创建方案（09-7-2后废弃）
        /// </summary>
        /// <param name="Current">电流参数1.5(6)</param>
        /// <param name="MeConst">当前该表常数 有功（无功）</param>
        /// <param name="MinConst">当前所有表中最小常数(数组，下标0=有功，1=无功)</param>
        /// <param name="WcLimit">误差限列表对象</param>
        /// <param name="GuiChengName">规程名称（JJG596-1999）</param>
        /// <param name="DjString">等级字符串1.0(2.0)</param>
        /// <param name="Hgq">是否经互感器1-经，0-不经</param>
        //public void CreateFA(string Current, string MeConst, int[] MinConst, string GuiChengName, string DjString, int Hgq,CLDC_Comm.Enum.Cus_Clfs clfs,float U,bool znq)
        //{
        //    CheckPlan = new List<object>();
        //    Error_CheckPoint _Wc=null;                 //这个放在这上面为了加快速度


        //    for (int i = 0; i < _Plan.Count; i++)
        //    {
        //        StFAGroup _Item = _Plan[i];
   
        //        switch (_Item.FAType)
        //        { 
        //            case Cus_FAGroup.预热: 
        //                {

        //                    Error_YuRe _YuRe = new Error_YuRe(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _YuRe.Count; j++)
        //                    {
        //                        CheckPlan.Add(_YuRe.getYuRePrj(j));
        //                    }
        //                    _YuRe = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.启动:
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
        //            case Cus_FAGroup.潜动:
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
        //            case Cus_FAGroup.基本误差试验:
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
        //            case Cus_FAGroup.走字试验:
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
        //            case Cus_FAGroup.多功能试验:
        //                {

        //                    Plan_Dgn _Dgn = new Plan_Dgn(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _Dgn.Count; j++)
        //                    {
        //                        CheckPlan.Add(_Dgn.getDgnPrj(j));
        //                    }
        //                    _Dgn = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.通讯协议检查试验:
        //                {

        //                    Plan_ConnProtocolCheck cpc = new Plan_ConnProtocolCheck(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < cpc.Count; j++)
        //                    {
        //                        CheckPlan.Add(cpc.getConnProtocolPrj(j));
        //                    }
        //                    cpc = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.影响量试验:
        //                {

        //                    Plan_Specal _Specal = new Plan_Specal(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _Specal.Count; j++)
        //                    {
        //                        CheckPlan.Add(_Specal.getSpecalPrj(j));
        //                    }
        //                    _Specal = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.载波试验:
        //                {
        //                    Plan_Carrier _Carrier = new Plan_Carrier(_TaiType, _Item.FAName);
        //                    for (int j = 0; j < _Carrier.Count; j++)
        //                    {
        //                        CheckPlan.Add(_Carrier.GetCarrierPrj(j));
        //                    }
        //                    _Carrier = null;
        //                    break;
        //                }
        //            case Cus_FAGroup.误差一致性:
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
