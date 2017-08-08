using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New
{
    public partial class AuditingSave : Base
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditingSave()
        {
            InitializeComponent();
            Btn_DoComplated.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        public AuditingSave(
            Main parent
            , CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup
            , int taiType
            , int taiId)
            : base(parent, meterGroup, taiType, taiId)
        {
            InitializeComponent();

            if (CLDC_DataCore.Function.Common.IsVSDevenv())
            {
                return;
            }

            Btn_DoComplated.Visible = false;

            try
            {
                this.Dgv_Result.Cursor = base.CurHand;
            }
            catch
            {
                this.Dgv_Result.Cursor = Cursors.Default;
            }

        }
        /// <summary>
        /// 窗体加载刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuditingSave_Load(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Function.Common.IsVSDevenv())
            {
                return;
            }

            List<CLDC_DataCore.Struct.StUserInfo> Lst_User = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.UserGroup.getUsers();
            for (int i = 0; i < Lst_User.Count; i++)
            {
                Cmb_Hyy.Items.Add(Lst_User[i].UserName);
                Cmb_Jyy.Items.Add(Lst_User[i].UserName);
                Cmb_Master.Items.Add(Lst_User[i].UserName);
            }
            Cmb_Jyy.Text = CLDC_DataCore.Const.GlobalUnit.User_Jyy.UserName;
            Cmb_Hyy.Text = CLDC_DataCore.Const.GlobalUnit.User_Hyy.UserName;
            LoadData();
            try
            {
                Txt_Month.Text = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getItem("DTM_VALID_MONTHS").Value;
            }
            catch { }
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        public override void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            base.RefreshData(meterGroup, taiType, taiId);
            LoadData();
        }

        private void LoadData()
        {
            int ColIndex = 0;
            //清除所有表格内容
            Dgv_Result.Columns.Clear();
            Dgv_Result.Rows.Clear();

            if (MeterGroup.MeterGroup.Count == 0)
                return;

            int FirstYJMeter = Main.GetFirstYaoJianMeterIndex(MeterGroup);

            #region   -----------------这个地方补充结论数据-----------------
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {

                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                if (!_MeterInfo.YaoJianYn) continue;

                #region -------------------增加结论项目----------------------

                if (!_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通电检查).ToString()))            //检查是否存在通电检查
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpMeterResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                    _TmpMeterResult.Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;
                    _TmpMeterResult._intMyId = MeterGroup.MeterGroup[i]._intMyId;
                    _TmpMeterResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通电检查).ToString();
                    _TmpMeterResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.通电检查.ToString();
                    _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通电检查).ToString(), _TmpMeterResult);
                }

                if (!_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.外观检查试验).ToString()))            //检查是否存在外观检查试验
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpMeterResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                    _TmpMeterResult.Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;
                    _TmpMeterResult._intMyId = MeterGroup.MeterGroup[i]._intMyId;
                    _TmpMeterResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.外观检查试验).ToString();
                    _TmpMeterResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.外观检查试验.ToString();
                    _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.外观检查试验).ToString(), _TmpMeterResult);
                }

                if (!_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.工频耐压试验).ToString()))            //检查是否存在工频耐压试验
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpMeterResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                    _TmpMeterResult.Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;
                    _TmpMeterResult._intMyId = MeterGroup.MeterGroup[i]._intMyId;
                    _TmpMeterResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.工频耐压试验).ToString();
                    _TmpMeterResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.工频耐压试验.ToString();
                    _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.工频耐压试验).ToString(), _TmpMeterResult);
                }

                #endregion

                List<string> _CheckPrjYn = new List<string>();

                string[] Arr_ID = new string[_MeterInfo.MeterResults.Keys.Count];

                _MeterInfo.MeterResults.Keys.CopyTo(Arr_ID, 0);

                for (int j = 0; j < Arr_ID.Length; j++)
                {
                    string _ID = Arr_ID[j];

                    if (_ID.Length == 3)       //如果ID长度是3表示是大项目，则跳过
                        continue;


                    #region  -----------------------------在结论集合中加入起动试验总结论-----------------------------------------
                    if (_ID.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString())
                    {
                        if (_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString()))         //如果存在大ID，并默认合格
                        {
                            if (!_CheckPrjYn.Contains(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString()))           //如果在验证列表里面没有起动试验这个ID才进行结论初始化，否则跳过
                            {
                                _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;

                                _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString());         //在验证列表中加入该ID
                            }
                        }
                        else     //如果不存在，则模拟一个大ID
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                            _TmpResult._intMyId = _MeterInfo.MeterResults[_ID]._intMyId;
                            _TmpResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString();
                            _TmpResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验.ToString();
                            _TmpResult.Mr_chrRstValue = _MeterInfo.MeterResults[_ID].Mr_chrRstValue;
                            _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString(), _TmpResult);

                            _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString());
                        }

                        if (_MeterInfo.MeterResults[_ID].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //如果不合格
                            _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_BuHeGe;

                    }

                    #endregion

                    #region  -----------------------------在结论集合中加入潜动总结论-----------------------------------------

                    if (_ID.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString())
                    {
                        if (_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString()))
                        {
                            if (!_CheckPrjYn.Contains(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString()))           //如果在验证列表里面没有起动试验这个ID才进行结论初始化，否则跳过
                            {
                                _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;

                                _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString());         //在验证列表中加入该ID
                            }
                        }
                        else   //如果不存在，则模拟一个大ID
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                            _TmpResult._intMyId = _MeterInfo.MeterResults[_ID]._intMyId;
                            _TmpResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString();
                            _TmpResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验.ToString();
                            _TmpResult.Mr_chrRstValue = _MeterInfo.MeterResults[_ID].Mr_chrRstValue;
                            _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString(), _TmpResult);

                            _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString());         //在验证列表中加入该ID
                        }

                        if (_MeterInfo.MeterResults[_ID].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //如果不合格
                            _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_BuHeGe;

                    }
                    #endregion

                    #region  -----------------------------在结论集合中加入基本误差总结论-----------------------------------------
                    if (_ID.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString())
                    {
                        if (_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString()))
                        {
                            if (!_CheckPrjYn.Contains(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString()))           //如果在验证列表里面没有起动试验这个ID才进行结论初始化，否则跳过
                            {
                                _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;

                                _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString());         //在验证列表中加入该ID
                            }
                        }
                        else   //如果不存在，则模拟一个大ID
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                            _TmpResult._intMyId = _MeterInfo.MeterResults[_ID]._intMyId;
                            _TmpResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString();
                            _TmpResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验.ToString();
                            _TmpResult.Mr_chrRstValue = _MeterInfo.MeterResults[_ID].Mr_chrRstValue;
                            _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString(), _TmpResult);

                            _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString());         //在验证列表中加入该ID
                        }

                        if (_MeterInfo.MeterResults[_ID].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //如果不合格
                            _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_BuHeGe;

                    }
                    #endregion

                    #region  -----------------------------在结论集合中加入标准偏差总结论-----------------------------------------
                    if (_ID.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString())
                    {

                        if (_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString()))
                        {
                            if (!_CheckPrjYn.Contains(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString()))           //如果在验证列表里面没有起动试验这个ID才进行结论初始化，否则跳过
                            {
                                _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;

                                _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString());         //在验证列表中加入该ID
                            }
                        }
                        else   //如果不存在，则模拟一个大ID
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                            _TmpResult._intMyId = _MeterInfo.MeterResults[_ID]._intMyId;
                            _TmpResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString();
                            _TmpResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差.ToString();
                            _TmpResult.Mr_chrRstValue = _MeterInfo.MeterResults[_ID].Mr_chrRstValue;
                            _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString(), _TmpResult);

                            _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString());         //在验证列表中加入该ID
                        }

                        if (_MeterInfo.MeterResults[_ID].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //如果不合格
                            _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_BuHeGe;

                    }

                    #endregion

                    #region  -----------------------------在结论集合中加入最大偏差值-----------------------------------------
                    if (_ID.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString())
                    {

                        if (_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString()))
                        {
                            if (!_CheckPrjYn.Contains(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString()))           //如果在验证列表里面没有起动试验这个ID才进行结论初始化，否则跳过
                            {
                                _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;

                                _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString());         //在验证列表中加入该ID
                            }
                        }
                        else   //如果不存在，则模拟一个大ID
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                            _TmpResult._intMyId = _MeterInfo.MeterResults[_ID]._intMyId;
                            _TmpResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString();
                            _TmpResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差.ToString();
                            _TmpResult.Mr_chrRstValue = _MeterInfo.MeterResults[_ID].Mr_chrRstValue;
                            _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString(), _TmpResult);

                            _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString());         //在验证列表中加入该ID
                        }

                        if (_MeterInfo.MeterResults[_ID].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //如果不合格
                            _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_BuHeGe;

                    }

                    #endregion

                    #region  -----------------------------在结论集合中加入走字试验总结论-----------------------------------------
                    if (_ID.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString())
                    {
                        if (_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString()))
                        {
                            if (!_CheckPrjYn.Contains(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString()))           //如果在验证列表里面没有起动试验这个ID才进行结论初始化，否则跳过
                            {
                                _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;

                                _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString());         //在验证列表中加入该ID
                            }
                        }
                        else   //如果不存在，则模拟一个大ID
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                            _TmpResult._intMyId = _MeterInfo.MeterResults[_ID]._intMyId;
                            _TmpResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString();
                            _TmpResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验.ToString();
                            _TmpResult.Mr_chrRstValue = _MeterInfo.MeterResults[_ID].Mr_chrRstValue;
                            _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString(), _TmpResult);

                            _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString());         //在验证列表中加入该ID
                        }

                        if (_MeterInfo.MeterResults[_ID].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //如果不合格
                            _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                    }
                    #endregion

                    #region  -----------------------------在结论集合中加入走字组合误差总结论-----------------------------------------
                    if (_ID.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString())
                    {
                        if (_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString()))
                        {
                            if (!_CheckPrjYn.Contains(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString()))            //如果在验证列表里没有组合误差这个ID菜进行结论初始化，否则跳过
                            {
                                _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;

                                _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString());             //在验证列表中加入该ID
                            }
                        }

                        else
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                            _TmpResult._intMyId = _MeterInfo.MeterResults[_ID]._intMyId;
                            _TmpResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString();
                            _TmpResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验.ToString();
                            _TmpResult.Mr_chrRstValue = _MeterInfo.MeterResults[_ID].Mr_chrRstValue;
                            _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString(), _TmpResult);
                        }

                        if (_MeterInfo.MeterResults[_ID].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //如果不合格，则改写总结论
                        {
                            _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                        }

                    }
                    #endregion

                    #region-----------------------------在结论集合中加入走字组合误差总误差值（最大误差值）-----------------------------------------
                    if (_ID.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString())
                    {
                        if (_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString()))
                        {
                            if (!_CheckPrjYn.Contains(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString()))            //如果在验证列表里没有组合误差这个ID菜进行结论初始化，否则跳过
                            {
                                _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;

                                _CheckPrjYn.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString());             //在验证列表中加入该ID
                            }
                        }

                        else
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                            _TmpResult._intMyId = _MeterInfo.MeterResults[_ID]._intMyId;
                            _TmpResult.Mr_chrRstId = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString();
                            _TmpResult.Mr_chrRstName = CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值.ToString();
                            _TmpResult.Mr_chrRstValue = _MeterInfo.MeterResults[_ID].Mr_chrRstValue;
                            _MeterInfo.MeterResults.Add(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString(), _TmpResult);
                        }

                        if (_MeterInfo.MeterResults[_ID].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //如果不合格，则改写总结论
                        {
                            _MeterInfo.MeterResults[((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString()].Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                        }
                    }
                    #endregion
                }
                #region ---------------------挂多功能总结论-------------------
                string strDgnKey = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.多功能试验).ToString("000");
                if (_MeterInfo.MeterDgns.Count == 0)
                {
                    if (_MeterInfo.MeterResults.ContainsKey(strDgnKey))
                        _MeterInfo.MeterResults.Remove(strDgnKey);
                }
                else
                {
                    //查看所有多功能项目
                    bool Result = true;
                    foreach (string strKey in _MeterInfo.MeterDgns.Keys)
                    {
                        if (_MeterInfo.MeterDgns[strKey].Md_chrValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                        {
                            Result = false;
                            break;
                        }
                    }
                    //增加总结论
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult dgnResult = null;
                    if (_MeterInfo.MeterResults.ContainsKey(strDgnKey))
                        dgnResult = _MeterInfo.MeterResults[strDgnKey];
                    else
                    {
                        dgnResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                        _MeterInfo.MeterResults.Add(strDgnKey, dgnResult);
                        dgnResult.Mr_chrRstId = strDgnKey;
                        dgnResult.Mr_chrRstName = "多功能试验";

                    }
                    dgnResult.Mr_chrRstValue = CLDC_DataCore.Function.Common.ConverResult(Result);
                }
                #endregion
                _CheckPrjYn = null;

            }


            #endregion

            bool _ChangeData = (CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getItem(CLDC_DataCore.Const.Variable.CTC_CHANGEDATA).Value == "是" ? false : true);   //是否允许修改检定数据

            foreach (string _ResultID in MeterGroup.MeterGroup[FirstYJMeter].MeterResults.Keys)    //遍历结论ID
            {
                if (_ResultID.Length == 3)          //如果ID长度=3则表示是需要显示的大项目结论
                {
                    ColIndex = Dgv_Result.Columns.Add("Key_" + _ResultID, ((CLDC_Comm.Enum.Cus_MeterResultPrjID)int.Parse(_ResultID)).ToString());
                    Dgv_Result.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    Dgv_Result.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    Dgv_Result.Columns[ColIndex].ReadOnly = _ChangeData;

                    Dgv_Result.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_Result.Columns[ColIndex].Tag = _ResultID;             //将检定ID保存到TAG中
                }
            }
            //total rst
            ColIndex = Dgv_Result.Columns.Add("Key_total", "总结论");
            Dgv_Result.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Dgv_Result.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            Dgv_Result.Columns[ColIndex].ReadOnly = _ChangeData;

            Dgv_Result.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Result.Columns[ColIndex].Tag = "Key_total";

            if (Dgv_Result.Columns.Count == 0)
                return;
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;

                int RowIndex = Dgv_Result.Rows.Add();

                Dgv_Result.Rows[RowIndex].Tag = i;          //保存表位号

                if ((RowIndex + 1) % 2 == 0)
                {
                    Dgv_Result.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                }
                else
                {
                    Dgv_Result.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                }
                Dgv_Result.Rows[RowIndex].HeaderCell.Value = MeterGroup.MeterGroup[i].ToString();           //表位号

                for (int j = 0; j < Dgv_Result.Columns.Count; j++)
                {
                    string Key = Dgv_Result.Columns[j].Tag.ToString();            //取出检定ID
                    if (MeterGroup.MeterGroup[i].MeterResults.ContainsKey(Key))
                    {

                        Dgv_Result.Rows[RowIndex].Cells[j].Value = MeterGroup.MeterGroup[i].MeterResults[Key].Mr_chrRstValue;         //如果存在则填写结论
                        if (MeterGroup.MeterGroup[i].MeterResults[Key].Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                        {
                            Dgv_Result.Rows[RowIndex].Cells[j].Style.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                        }
                        else
                        {
                            Dgv_Result.Rows[RowIndex].Cells[j].Style.ForeColor = Color.Black;
                        }
                    }
                }
                Dgv_Result.Rows[RowIndex].Cells[Dgv_Result.Columns.Count - 1].Value = MeterGroup.MeterGroup[i].Mb_chrResult;
                if (MeterGroup.MeterGroup[i].Mb_chrResult == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    Dgv_Result.Rows[RowIndex].Cells[Dgv_Result.Columns.Count - 1].Style.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                }
                else
                {
                    Dgv_Result.Rows[RowIndex].Cells[Dgv_Result.Columns.Count - 1].Style.ForeColor = Color.Black;
                }
            }

            this.InvokeRefreshTab();
        }



        private delegate void Dgt_InvokeRefreshTab();

        /// <summary>
        /// 显示具体检定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Result_SelectionChanged(object sender, EventArgs e)
        {

            if (Dgv_Result.SelectedRows.Count == 0)
                return;

            //Comm.Function.TopWaiting.ShowWaiting("正在切换...");

            this.Invoke(new Dgt_InvokeRefreshTab(this.InvokeRefreshTab));


            //Comm.Function.TopWaiting.HideWaiting();
        }

        private void InvokeRefreshTab()
        {
            if (Dgv_Result.SelectedRows[0].Tag == null || Dgv_Result.SelectedRows[0].Tag.ToString() == string.Empty) return;

            int _intBwh = (int)Dgv_Result.SelectedRows[0].Tag;

            bool _ChangeData = (CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getItem(CLDC_DataCore.Const.Variable.CTC_CHANGEDATA).Value == "是" ? true : false);   //是否允许修改检定数据

            // 可以通过开关这条语句确定是否每次都要重新建立
            int LastSelectTabIndex = Tab_Data.SelectedIndex;

            Tab_Data.TabPages.Clear();

            Tab_Data.Visible = false;
            //添加基本信息
            {
                if (!Tab_Data.TabPages.ContainsKey("基本参数"))
                {
                    Tab_Data.TabPages.Add("基本参数", "基本参数");
                    DisplayInfo.MeterBasicInfo WC_QiQian = new CLDC_MeterUI.DisplayInfo.MeterBasicInfo(MeterGroup.MeterGroup[_intBwh], _ChangeData);

                    WC_QiQian.ValueChanged += delegate(string PropertyName, object Value, int Bwh)
                                            {
                                                if (ParentMain.Evt_DataInfoChanged != null)
                                                {
                                                    ParentMain.Evt_DataInfoChanged(PropertyName, Value, Bwh, base.TaiType, base.TaiId);
                                                }
                                            };


                    Tab_Data.TabPages["基本参数"].Controls.Add(WC_QiQian);
                    WC_QiQian.Margin = new System.Windows.Forms.Padding(0);
                    WC_QiQian.Dock = DockStyle.Fill;
                    WC_QiQian.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    ((CLDC_MeterUI.DisplayInfo.Base)Tab_Data.TabPages["基本参数"].Controls[0]).SetData(MeterGroup.MeterGroup[_intBwh], _ChangeData);
                }
            }
            //添加检定数据
            {
                if (!Tab_Data.TabPages.ContainsKey("详细数据"))
                {
                    Tab_Data.TabPages.Add("详细数据", "详细数据");
                    DisplayInfo.DisplayMeterDetailInfo MeterDetailInfo = new DisplayInfo.DisplayMeterDetailInfo(MeterGroup, _ChangeData);

                    Tab_Data.TabPages["详细数据"].Controls.Add(MeterDetailInfo);
                    MeterDetailInfo.Margin = new System.Windows.Forms.Padding(1);
                    MeterDetailInfo.Dock = DockStyle.Fill;
                    MeterDetailInfo.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    ((CLDC_MeterUI.DisplayInfo.Base)Tab_Data.TabPages["详细数据"].Controls[1]).SetData(MeterGroup, _ChangeData);
                }
            }

            if (Tab_Data.TabPages.Count > LastSelectTabIndex && LastSelectTabIndex != -1)
            {
                Tab_Data.SelectedIndex = LastSelectTabIndex;
            }
            Tab_Data.Visible = true;
        }
        /// <summary>
        /// 存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Save_Click(object sender, EventArgs e)
        {
            
        }

        private bool[] GetRepeatPressMeters(string[] states)
        {
            bool[] rp = new bool[MeterGroup.MeterGroup.Count];
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                if (states[i].IndexOf("011") < 0 && states[i].IndexOf("010") < 0)//
                {
                    rp[i] = true;
                }
            }
            return rp;
        }
    }
}
