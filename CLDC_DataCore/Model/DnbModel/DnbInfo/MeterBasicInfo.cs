using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CLDC_Comm.Enum;
using CLDC_CTNProtocol;
using CLDC_DataCore.Const;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    [Serializable()]
    public class MeterBasicInfo : MeterErrorBase
    {
        #region 构造
        public MeterBasicInfo(int Bwh)
            : this()
        {
            _Mb_intBno = Bwh;
            _intBno = Bwh;
        }

        public MeterBasicInfo()
        {
            this.AddEquipmentInfo();
        }
        /// <summary>
        /// 表位号		在表架上所挂位置(只读)
        /// </summary>
        public int Mb_intBno
        {
            get
            {
                return _Mb_intBno;
            }
        }

        /// <summary>
        /// 设置表位号
        /// </summary>
        /// <param name="Bwh"></param>
        public void SetBno(int Bwh)
        {
            _Mb_intBno = Bwh;
            _intBno = Bwh;
        }


        /// <summary>
        /// 挂新表使用
        /// </summary>
        /// <returns></returns>
        public void GetNewMeter()
        {
            this.ClearData();                  //清理已经产生了的检定数据
            this.YaoJianYn = true;
            this.Mb_ChrJlbh = "";
            this.Mb_ChrCcbh = "";
            this.Mb_ChrTxm = "";
            this.Mb_chrAddr = "";
            this.Mb_chrBcs = "";
            this.Mb_chrBdj = "";
            this.Mb_Bxh = "";
            this.Mb_chrCcrq = "";
            this.Mb_ChrBmc = "";
            this.Mb_chrZsbh = "";
            this.AVR_WORK_NO = "";
            this.AVR_TASK_NO = "";
            this.Mb_chrQianFeng1 = "";
            this.Mb_chrQianFeng2 = "";
            this.Mb_chrQianFeng3 = "";
            this.AVR_SEAL_4 = "";
            this.AVR_SEAL_5 = "";
            this.Mb_chrSoftVer = "";
            this.Mb_chrHardVer = "";
            this.Mb_chrArriveBatchNo = "";
            this.Mb_chrOther1 = "";
            this.Mb_chrOther2 = "";
            this.Mb_chrOther3 = "";
            this.Mb_chrOther4 = "";
            this.Mb_chrOther5 = "";
            this.MeterExtend.Clear();
            AddEquipmentInfo();
        }
        /// <summary>
        /// 获取对应台体的装置信息 lees 20161028
        /// </summary>
        /// <param name="KeyType">标签名</param>
        /// <param name="EquimentNo">装置标识</param>
        /// <returns></returns>
        private string BackTheEquimentInfo(string KeyType,string EquimentNo)
        {

            try
            {
                return "";
            }
            catch
            {
                return null;
            }
        }
            
        /// <summary>
        /// 添加台体信息到扩展数据集合中
        /// </summary>
        /// <remarks>
        /// 2012-03-05 增加，每块表初始化时自带标准表信息和台体信息，方便服务器端输出报表以及MIS。
        /// </remarks>
        private void AddEquipmentInfo()
        {
            if (MeterExtend == null) MeterExtend = new Dictionary<string, string>();
            //第一步：插入标准表信息,共八项，排列方式：标准表名称|型号|证书编号|测量范围起|测量范围止|不确定度|证书有效期|设备编号
            string stdMeterInfo = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}"
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_STDMETER_NAME, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_STDMETER_SIZE, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_STDMETER_ASSNO, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_STDMETERRANGE_START, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_STDMETERRANGE_END, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_STDMETER_ERROR, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_STDMETER_EXPERDATE, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_STDMETER_NO, "")
                                    );

            MeterExtend["StdMeterInfo"] = stdMeterInfo;
            //台体信息,共八项，排列方式：装置名称|型号|证书编号|测量范围起|测量范围止|不确定度|证书有效期|设备编号
            stdMeterInfo = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}"
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_NAME, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_SIZE, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_ASSNO, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_EQUIPMENTRANGE_START, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_EQUIPMENTRANGE_END, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_ERROR, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_EXPERDATE, "")
                                    , CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_NO, "")
                                    );
            MeterExtend["StdSetInfo"] = stdMeterInfo;
        }
        #endregion

        #region 一块表基本信息
        
        /// <summary>
        /// 表位号		在表架上所挂位置
        /// </summary>
        private int _Mb_intBno = 0;
        /// <summary>
        /// 计量编号
        /// </summary>
        public string Mb_ChrJlbh = "";
        /// <summary>
        /// 出厂编号
        /// </summary>
        public string Mb_ChrCcbh = "";
        /// <summary>
        /// 条形码	
        /// </summary>
        public string Mb_ChrTxm = "";
        /// <summary>
        /// 表通信地址
        /// </summary>
        public string Mb_chrAddr = "";
        /// <summary>
        /// 制造厂家
        /// </summary>
        public string Mb_chrzzcj = "";
        /// <summary>
        /// 表型号
        /// </summary>
        public string Mb_Bxh = "";
        /// <summary>
        /// 表号，用于加密参数
        /// </summary>
        public string _Mb_MeterNo = "";
        /// <summary>
        /// 表常数		有功（无功）
        /// </summary>
        public string Mb_chrBcs = "";
        /// <summary>
        /// 表类型
        /// </summary>
        public string Mb_chrBlx = "";
        /// <summary>
        /// 表等级		有功（无功）
        /// </summary>
        public string Mb_chrBdj = "";

        /// <summary>
        /// 共阴 共阳类型
        /// </summary>
        public Cus_GyGyType Mb_gygy = CLDC_Comm.Enum.Cus_GyGyType.共阴;
        /// <summary>
        /// 出厂日期		YYYY.MM.DD
        /// </summary>
        public string Mb_chrCcrq = "";
        /// <summary>
        /// 送检单位
        /// </summary>
        public string Mb_chrSjdwNo = "";
        /// <summary>
        /// 送检单位
        /// </summary>
        public string Mb_chrSjdw = "";
        /// <summary>
        /// 证书编号
        /// </summary>
        public string Mb_chrZsbh = "";
        /// <summary>
        /// 表名称	
        /// </summary>
        public string Mb_ChrBmc = "";
        /// <summary>
        /// 测量方式	
        /// </summary>
        public int Mb_intClfs = 0;
        /// <summary>
        /// 电压		XXX（不带单位）
        /// </summary>
        public string Mb_chrUb = "";
        /// <summary>
        /// 电流		Ib(Imax)（不带单位）
        /// </summary>
        public string Mb_chrIb = "";
        /// <summary>
        /// 频率		XX（不带单位）
        /// </summary>
        public string Mb_chrHz = "";
        /// <summary>
        /// 止逆器		1-有，0-无
        /// </summary>
        public bool Mb_BlnZnq = false;
        /// <summary>
        /// 互感器		1-经互感器
        /// </summary>
        public bool Mb_BlnHgq = false;
        /// <summary>
        /// 检测类型
        /// </summary>
        public string Mb_chrTestType = "";
        /// <summary>
        /// 检定日期		YYYY-MM-DD HH:NN:SS
        /// </summary>
        public string Mb_DatJdrq = "";
        /// <summary>
        /// 计检日期		YYYY-MM-DD HH:NN:SS
        /// </summary>
        public string Mb_Datjjrq = "";
        /// <summary>
        /// 温度		XX（不带单位）
        /// </summary>
        public string Mb_chrWd = "";
        /// <summary>
        /// 湿度		XX（不带单位）
        /// </summary>
        public string Mb_chrSd = "";
        /// <summary>
        /// 总结论		合格/不合格
        /// </summary>
        public string Mb_chrResult = "";
        /// <summary>
        /// 检验员
        /// </summary>
        public string Mb_ChrJyy = "";
        /// <summary>
        /// 核验员	
        /// </summary>
        public string Mb_ChrHyy = "";
        /// <summary>
        /// 主管
        /// </summary>
        public string Mb_chrZhuGuan = "";
        /// <summary>
        /// 检验员编号
        /// </summary>
        public string Mb_ChrJyyNo = "";
        /// <summary>
        /// 核验员编号
        /// </summary>
        public string Mb_ChrHyyNo = "";
        /// <summary>
        /// 主管人员编号
        /// </summary>
        public string Mb_chrZhuGuanNo = "";
        /// <summary>
        /// 是否上传到服务器
        /// </summary>
        public bool Mb_BlnToServer = false;
        /// <summary>
        /// 是否上传到MIS		在集控下无效
        /// </summary>
        public bool Mb_BlnToMis = false;
        /// <summary>
        /// 铅封1
        /// </summary>
        public string Mb_chrQianFeng1 = "";
        /// <summary>
        /// 铅封2
        /// </summary>
        public string Mb_chrQianFeng2 = "";
        /// <summary>
        /// 铅封3
        /// </summary>
        public string Mb_chrQianFeng3 = "";

        /// <summary>
        /// 37铅封号4
        /// </summary>
        public string AVR_SEAL_4 = "";
        /// <summary>
        /// 38铅封号5
        /// </summary>
        public string AVR_SEAL_5 = "";
        /// <summary>
        /// 软件版本号
        /// </summary>
        public string Mb_chrSoftVer = "";
        /// <summary>
        /// 硬件版本号
        /// </summary>
        public string Mb_chrHardVer = "";
        /// <summary>
        /// 到货批次号
        /// </summary>
        public string Mb_chrArriveBatchNo = "";
        /// <summary>
        /// 方案唯一编号
        /// </summary>
        public int Mb_intSchemeID = 0;
         /// <summary>
        /// 协议唯一编号
        /// </summary>
        public int Mb_intProtocolID = 0;
        /// <summary>
        /// 通讯协议名称
        /// </summary>
        public string AVR_PROTOCOL_NAME="";
        /// <summary>
        /// 载波协议名称
        /// </summary>
        public string AVR_CARR_PROTC_NAME = "";
        /// <summary>
        /// 费控类型,1本地，0远程
        /// </summary>
        public int Mb_intFKType = 0;
        /// <summary>
        /// 45任务编号
        /// </summary>
        public string AVR_TASK_NO = "";

        /// <summary>
        /// 46工单号
        /// </summary>
        public string AVR_WORK_NO = "";
        /// <summary>
        /// 备用1	
        /// </summary>
        public string Mb_chrOther1 = "";
        /// <summary>
        /// 备用2
        /// </summary>
        public string Mb_chrOther2 = "";
        /// <summary>
        /// 备用3
        /// </summary>
        public string Mb_chrOther3 = "";
        /// <summary>
        /// 备用4
        /// </summary>
        public string Mb_chrOther4 = "";
        /// <summary>
        /// 备用5
        /// </summary>
        public string Mb_chrOther5 = "";
        /// <summary>
        /// 是否要检,用于检定
        /// </summary>
        public bool YaoJianYn = false;
        /// <summary>
        /// 是否要检，只能在参数录入赋值。用于参数录入、数据保存。
        /// </summary>
        public bool YaoJianYnSave = false;

        /// <summary>
        /// 使用的规程名称
        /// </summary>
        public string GuiChengName = string.Empty;

        /// <summary>
        /// 选定的电子式表使用的规程
        /// </summary>
        public string GuiChengName_DianZi = string.Empty;

        /// <summary>
        /// 选定的感应式表使用规程
        /// </summary>
        public string GuiChengName_GanYing = string.Empty;

        /// <summary>
        /// 总结论
        /// </summary>
        public string Mb_Result
        {
            get
            {
                #region 获取总结论
                Mb_chrResult = Variable.CTG_HeGe;
                if (MeterErrors != null)
                {
                    if (MeterErrors.Count > 0)
                    {
                        string[] Keys=new string[MeterErrors.Count];
                        MeterErrors.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterErrors[sKey].Me_chrWcJl == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }
                if (MeterResults != null)
                {
                    if (MeterResults.Count > 0)
                    {
                        string[] Keys = new string[MeterResults.Count];
                        MeterResults.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterResults[sKey].Mr_chrRstValue == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterDgns != null)
                {
                    if (MeterDgns.Count > 0)
                    {
                        string[] Keys = new string[MeterDgns.Count];
                        MeterDgns.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterDgns[sKey].Md_chrValue == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterSjJLgns != null)
                {
                    if (MeterSjJLgns.Count > 0)
                    {
                        string[] Keys = new string[MeterSjJLgns.Count];
                        MeterSjJLgns.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterSjJLgns[sKey].ItemConc == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterSpecialErrs != null)
                {
                    if (MeterSpecialErrs.Count > 0)
                    {
                        string[] Keys = new string[MeterSpecialErrs.Count];
                        MeterSpecialErrs.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterSpecialErrs[sKey].Me_chrWcJl == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterErrAccords != null)
                {
                    if (MeterErrAccords.Count > 0)
                    {
                        string[] Keys = new string[MeterErrAccords.Count];
                        MeterErrAccords.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterErrAccords[sKey].Mea_Result == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterPowers != null)
                {
                    if (MeterPowers.Count > 0)
                    {
                        string[] Keys = new string[MeterPowers.Count];
                        MeterPowers.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterPowers[sKey].Md_chrValue == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterShows != null)
                {
                    if (MeterShows.Count > 0)
                    {
                        string[] Keys = new string[MeterShows.Count];
                        MeterShows.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterShows[sKey].Msh_chrJL == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterQdQids != null)
                {
                    if (MeterQdQids.Count > 0)
                    {
                        string[] Keys = new string[MeterQdQids.Count];
                        MeterQdQids.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterQdQids[sKey].Mqd_chrJL == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterJLgns != null)
                {
                    if (MeterJLgns.Count > 0)
                    {
                        string[] Keys = new string[MeterJLgns.Count];
                        MeterJLgns.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterJLgns[sKey].AVR_ITEM_CONC == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterCostControls != null)
                {
                    if (MeterCostControls.Count > 0)
                    {
                        string[] Keys = new string[MeterCostControls.Count];
                        MeterCostControls.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterCostControls[sKey].Mfk_chrJL == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterFLSDgns != null)
                {
                    if (MeterFLSDgns.Count > 0)
                    {
                        string[] Keys = new string[MeterFLSDgns.Count];
                        MeterFLSDgns.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterFLSDgns[sKey].Mfl_chrItemJL == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterXLgns != null)
                {
                    if (MeterXLgns.Count > 0)
                    {
                        string[] Keys = new string[MeterXLgns.Count];
                        MeterXLgns.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterXLgns[sKey].AVR_ITEM_CONC == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterZZErrors != null)
                {
                    if (MeterZZErrors.Count > 0)
                    {
                        string[] Keys = new string[MeterZZErrors.Count];
                        MeterZZErrors.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterZZErrors[sKey].Mz_chrJL == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterConsistencys != null)
                {
                    if (MeterConsistencys.Count > 0)
                    {
                        string[] Keys = new string[MeterConsistencys.Count];
                        MeterConsistencys.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterConsistencys[sKey].Mc_chrJL == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterDLTDatas != null)
                {
                    if (MeterDLTDatas.Count > 0)
                    {
                        string[] Keys = new string[MeterDLTDatas.Count];
                        MeterDLTDatas.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterDLTDatas[sKey].Mdlt_chrValue == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterInsulations != null)
                {
                    if (MeterInsulations.Count > 0)
                    {
                        string[] Keys = new string[MeterInsulations.Count];
                        MeterInsulations.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterInsulations[sKey].Result == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterFreezes != null)
                {
                    if (MeterFreezes.Count > 0)
                    {
                        string[] Keys = new string[MeterFreezes.Count];
                        MeterFreezes.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterFreezes[sKey].Md_chrValue == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterEventLogs != null)
                {
                    if (MeterEventLogs.Count > 0)
                    {
                        string[] Keys = new string[MeterEventLogs.Count];
                        MeterEventLogs.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterEventLogs[sKey].Mel_Result == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterFunctions != null)
                {
                    if (MeterFunctions.Count > 0)
                    {
                        string[] Keys = new string[MeterFunctions.Count];
                        MeterFunctions.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterFunctions[sKey].Mf_Result == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                if (MeterCarrierDatas != null)
                {
                    if (MeterCarrierDatas.Count > 0)
                    {
                        string[] Keys = new string[MeterCarrierDatas.Count];
                        MeterCarrierDatas.Keys.CopyTo(Keys, 0);
                        foreach (string sKey in Keys)
                        {
                            if (MeterCarrierDatas[sKey].Mce_ItemResult == Variable.CTG_BuHeGe)
                            {
                                Mb_chrResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                return Mb_chrResult;
                            }
                        }
                    }
                }

                return Mb_chrResult;
                #endregion
            }
        }
        /// <summary>
        /// 费控类型，本地、远程
        /// </summary>
        public string FKType
        {
            get
            {
                if (Mb_intFKType == 1)
                {
                   return "本地费控";
                }
                if (Mb_intFKType == 2)
                {
                    return "不带费控";
                }
                else
                {
                    return "远程费控";
                }
            }
            set
            {
                if (value == "本地费控")
                {
                    Mb_intFKType = 1;
                }
                else if (value == "不带费控")
                {
                    Mb_intFKType = 2;
                }
                else
                {
                    Mb_intFKType = 0;
                }
            }
        }
        /// <summary>
        /// 表类型：电子式 Or 感应式
        /// </summary>
        public CLDC_Comm.Enum.Cus_MeterType_DianziOrGanYing MeterType_DzOrGy
        {
            get
            {
                if (Mb_chrBlx.IndexOf("机电") != -1
                    || Mb_chrBlx.IndexOf("感应") != -1
                    || Mb_chrBlx.IndexOf("机械") != -1)
                {
                    return CLDC_Comm.Enum.Cus_MeterType_DianziOrGanYing.GanYingShi;
                }
                else
                {
                    return CLDC_Comm.Enum.Cus_MeterType_DianziOrGanYing.DianZiShi;
                }
            }
        }
        #endregion

        #region 一块表检定数据模型
        /// <summary>
        /// 电能表误差集合Key值为项目Prj_ID值，由于特殊检定部分被T出去单独建结构所以不会出现关键字重复的情况 
        /// </summary>
        public Dictionary<string, MeterError> MeterErrors = new Dictionary<string, MeterError>();
        /// <summary>
        /// 电能表结论集；Key值为检定项目ID编号格式化字符串。格式为[检定项目ID号]参照数据库结构设计文档中附2
        /// </summary>
        public Dictionary<string, MeterResult> MeterResults = new Dictionary<string, MeterResult>();
        /// <summary>
        /// 电能表多功能数据集； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterDgn> MeterDgns = new Dictionary<string, MeterDgn>();
        /// <summary>
        /// 事件记录数据集； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterSjJLgn> MeterSjJLgns = new Dictionary<string, MeterSjJLgn>();
        /// <summary>
        /// 电能表走字数据误差集；Key值为Prj_ID
        /// </summary>
        public Dictionary<string, MeterZZError> MeterZZErrors = new Dictionary<string, MeterZZError>();

        /// <summary>
        /// 电能表载波检定数据集； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterCarrierData> MeterCarrierDatas = new Dictionary<string, MeterCarrierData>();
        /// <summary>
        /// 电能表数据比对检定数据集； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterInfraredData> MeterInfraredDatas = new Dictionary<string, MeterInfraredData>();

        /// <summary>
        /// 电能表特殊检定数据误差集；Key值为P_[下标序号]由于无法确定关键字，故只能使用下标序号来表示
        /// </summary>
        /// 
        public Dictionary<string, MeterSpecialErr> MeterSpecialErrs = new Dictionary<string, MeterSpecialErr>();

        /// <summary>
        /// 电能表误差一致性集；Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterErrAccord> MeterErrAccords = new Dictionary<string, MeterErrAccord>();
        /// <summary>
        /// 电能表功耗测试数据集；key值为项目Md_PrjID值
        /// </summary>
        public Dictionary<string, MeterPower> MeterPowers = new Dictionary<string, MeterPower>();
        /// <summary>
        /// 电能表扩展数据集，Key为标志ID,不能超过10个字节，标志值不能超过50个字节
        /// </summary>
        public Dictionary<string, string> MeterExtend = new Dictionary<string, string>();
        /// <summary>
        /// 数据显示功能；Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterShow> MeterShows = new Dictionary<string, MeterShow>();
        /// <summary>
        /// 潜动启动数据；Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterQdQid> MeterQdQids = new Dictionary<string, MeterQdQid>();
        /// <summary>
        /// 计量功能；Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterJLgn> MeterJLgns = new Dictionary<string, MeterJLgn>();
        /// <summary>
        /// 费控数据；Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterFK> MeterCostControls = new Dictionary<string, MeterFK>();
        /// <summary>
        /// 费率时段功能； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterFLSDgn> MeterFLSDgns = new Dictionary<string, MeterFLSDgn>();
        /// <summary>
        /// 需量功能；Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterXLgn> MeterXLgns = new Dictionary<string, MeterXLgn>();
        /// <summary>
        /// 一致性试验数据
        /// </summary>
        public Dictionary<string, MeterConsistency> MeterConsistencys = new Dictionary<string, MeterConsistency>();
        /// <summary>
        /// 规约一致性数据
        /// </summary>
        public Dictionary<string, MeterDLTData> MeterDLTDatas = new Dictionary<string, MeterDLTData>();
        /// <summary>
        /// 耐压数据集
        /// </summary>
        public Dictionary<string, MeterInsulation> MeterInsulations = new Dictionary<string, MeterInsulation>();

        /// <summary>
        /// 电能表冻结数据集； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterFreeze> MeterFreezes = new Dictionary<string, MeterFreeze>();
        /// <summary>
        /// 电能表事件记录数据集； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterEventLog> MeterEventLogs = new Dictionary<string, MeterEventLog>();
        /// <summary>
        /// 智能表功能数据集； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterFunction> MeterFunctions = new Dictionary<string, MeterFunction>();
        /// <summary>
        /// 预先调试数据集； Key值为项目Prj_ID值
        /// </summary>
        public Dictionary<string, MeterPrepareTest> MeterPrepareTest = new Dictionary<string, MeterPrepareTest>();
        /// <summary>
        /// 负荷记录数据集；Key值为项目prj_ID值
        /// </summary>
        public Dictionary<string, MeterLoadRecord> MeterLoadRecords = new Dictionary<string, MeterLoadRecord>();
        /// <summary>
        /// 南网费控软件数据
        /// </summary>
        public Dictionary<string, MeterOtherSoftData> MeterOtherSoftData = new Dictionary<string, MeterOtherSoftData>();
        #endregion

        /// <summary>
        /// 电能表多功能通信配置协议
        /// </summary>
        public DgnProtocol.DgnProtocolInfo DgnProtocol;

        #region 公用
        /// <summary>
        /// 获取表常数 
        /// </summary>
        /// <returns>[有功，无功]</returns>
        public int[] GetBcs()
        {
            Mb_chrBcs = Mb_chrBcs.Replace("（", "(").Replace("）", ")");

            if (Mb_chrBcs.Trim().Length < 1)
            {
                //System.Windows.Forms.MessageBox.Show("没有录入常数");
                return new int[] { 1, 1 };
            }

            string[] arTmp = Mb_chrBcs.Trim().Replace(")", "").Split('(');

            if (arTmp.Length == 1)
            {
                if (CLDC_DataCore.Function.Number.IsNumeric(arTmp[0]))
                    return new int[] { int.Parse(arTmp[0]), int.Parse(arTmp[0]) };
                else
                    return new int[] { 1, 1 };
            }
            else
            {
                if (CLDC_DataCore.Function.Number.IsNumeric(arTmp[0]) && CLDC_DataCore.Function.Number.IsNumeric(arTmp[1]))
                    return new int[] { int.Parse(arTmp[0]), int.Parse(arTmp[1]) };
                else
                    return new int[] { 1, 1 };
            }
        }

        /// <summary>
        /// 获取电流
        /// </summary>
        /// <returns>[最小电流,最大电流]</returns>
        public float[] GetIb()
        {
            Mb_chrIb = Mb_chrIb.Replace("（", "(").Replace("）", ")");

            if (Mb_chrIb.Trim().Length < 1)
            {
                return new float[] { 1, 1 };
            }

            string[] arTmp = Mb_chrIb.Trim().Replace(")", "").Split('(');

            if (arTmp.Length == 1)
            {
                if (CLDC_DataCore.Function.Number.IsNumeric(arTmp[0]))
                    return new float[] { float.Parse(arTmp[0]), float.Parse(arTmp[0]) };
                else
                    return new float[] { 1, 1 };
            }
            else
            {
                if (CLDC_DataCore.Function.Number.IsNumeric(arTmp[0]) && CLDC_DataCore.Function.Number.IsNumeric(arTmp[1]))
                    return new float[] { float.Parse(arTmp[0]), float.Parse(arTmp[1]) };
                else
                    return new float[] { 1, 1 };
            }
        }

        /// <summary>
        /// 返回表位号格式化字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0:d2}表位", Mb_intBno);
        }
        /// <summary>
        /// 清理所有检定数据
        /// </summary>
        public void ClearData()
        {
            MeterDgns.Clear();
            MeterErrors.Clear();
            MeterResults.Clear();
            MeterZZErrors.Clear();
            MeterQdQids.Clear();
            if (MeterSpecialErrs == null)
            {
                MeterSpecialErrs = new Dictionary<string, MeterSpecialErr>();
            }
            MeterSpecialErrs.Clear();
            if (MeterErrAccords == null)
            {
                MeterErrAccords = new Dictionary<string, MeterErrAccord>();
            }
            MeterErrAccords.Clear();
            if (MeterCarrierDatas == null)
            {
                MeterCarrierDatas = new Dictionary<string, MeterCarrierData>();
            }
            MeterCarrierDatas.Clear();
            if (MeterPowers == null)
            {
                MeterPowers = new Dictionary<string, MeterPower>();
            }
            MeterPowers.Clear();
            if (MeterInsulations == null)
            {
                MeterInsulations = new Dictionary<string, MeterInsulation>();
            }
            MeterInsulations.Clear();
            if (MeterCostControls == null)
            {
                MeterCostControls = new Dictionary<string, MeterFK>();
            }
            MeterCostControls.Clear();
            if (MeterFunctions == null)
            {
                MeterFunctions = new Dictionary<string, MeterFunction>();
            }
            MeterFunctions.Clear();
            if (MeterPrepareTest == null)
            {
                MeterPrepareTest = new Dictionary<string, MeterPrepareTest>();
            }
            MeterPrepareTest.Clear();
            if (MeterEventLogs == null)
            {
                MeterEventLogs = new Dictionary<string, MeterEventLog>();
            }
            MeterEventLogs.Clear();
            if (MeterFreezes == null)
            {
                MeterFreezes = new Dictionary<string, MeterFreeze>();
            }
            MeterFreezes.Clear();
            if (MeterInfraredDatas == null)
            {
                MeterInfraredDatas = new Dictionary<string, MeterInfraredData>();
            }
            MeterInfraredDatas.Clear();
            if (MeterSjJLgns == null)
            {
                MeterSjJLgns = new Dictionary<string, MeterSjJLgn>();
            }
            MeterSjJLgns.Clear();
            
            if (MeterDLTDatas == null)
            {
                MeterDLTDatas = new Dictionary<string, MeterDLTData>();
            }
            MeterDLTDatas.Clear();
            if (MeterConsistencys == null)
            {
                MeterConsistencys = new Dictionary<string, MeterConsistency>();
            }
            MeterConsistencys.Clear();
            if (MeterFLSDgns == null)
            {
                MeterFLSDgns = new Dictionary<string, MeterFLSDgn>();
            }
            MeterFLSDgns.Clear();
            if (MeterJLgns == null)
            {
                MeterJLgns = new Dictionary<string, MeterJLgn>();
            }
            MeterJLgns.Clear();
            if (MeterShows == null)
            {
                MeterShows = new Dictionary<string, MeterShow>();
            }
            MeterShows.Clear();
            if (MeterXLgns == null)
            {
                MeterXLgns = new Dictionary<string, MeterXLgn>();
            }
            MeterXLgns.Clear();

            if (MeterLoadRecords == null)
            {
                MeterLoadRecords = new Dictionary<string, MeterLoadRecord>();
            }
            MeterLoadRecords.Clear();
        }

        /// <summary>
        /// 数据清理，可根据项目清理
        /// </summary>
        /// <param name="DataType"></param>
        public void ClearData(ClearDataType DataType)
        {
            switch (DataType)
            {
                case ClearDataType.多功能数据:
                    MeterDgns.Clear();

                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.多功能试验).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.多功能试验).ToString());
                    }

                    break;
                case ClearDataType.误差数据:       //清理误差数据的时候需要同时清理结论表中的偏差结论，偏差值，特殊检定结论
                    MeterErrors.Clear();

                    #region-----------清理基本误差结论-------------------
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString());
                    }

                    for (int i = 1; i < 5; i++) //分功率方向结论
                    {
                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString() + i.ToString()))
                        {
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString() + i.ToString());
                        }
                    }
                    #endregion

                    #region -------------清理最大偏差，及偏差结论---------------------
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString()))
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString());

                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString()))
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString());
                    for (int _i = 1; _i < 5; _i++)          //分功率方向结论
                    {
                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString() + _i.ToString()))
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString() + _i.ToString());
                    }
                    #endregion

                    break;
                case ClearDataType.走字数据:            //清理走字数据的时候需要同时清理结论表中的组合误差结论及组合误差值
                    MeterZZErrors.Clear();

                    #region-----------清理走字误差结论-------------------
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString());
                    }

                    for (int i = 1; i < 5; i++) //分功率方向结论
                    {
                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString() + i.ToString()))
                        {
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString() + i.ToString());
                        }
                    }
                    #endregion

                    #region -------------------清理组合误差结论、值-----------------
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString()))
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString());
                    for (int _i = 1; _i < 5; _i++)      //分功率方向结论，及组合误差值
                    {
                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString() + _i.ToString()))
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString() + _i.ToString());

                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString() + _i.ToString()))
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString() + _i.ToString());
                    }

                    #endregion

                    break;
                case ClearDataType.特殊检定数据:
                    MeterSpecialErrs.Clear();
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.特殊检定).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.特殊检定).ToString());
                    }
                    break;
                case ClearDataType.启动数据:
                    MeterQdQids.Clear();
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString());
                    }
                    for (int i = 1; i < 5; i++)
                    {
                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString() + i.ToString()))
                        {
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString() + i.ToString());
                        }
                    }
                    break;
                case ClearDataType.潜动数据:
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString());
                    }
                    for (int i = 1; i < 5; i++)
                    {
                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString() + i.ToString()))
                        {
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString() + i.ToString());
                        }
                    }
                    break;
                case ClearDataType.通讯协议检定数据:
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString());
                    }
                    for (int i = 1; i < 5; i++)
                    {
                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString() + i.ToString()))
                        {
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString() + i.ToString());
                        }
                    }
                    break;
                case ClearDataType.载波数据:
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.载波试验).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.载波试验).ToString());
                    }
                    for (int i = 1; i < 5; i++)
                    {
                        if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.载波试验).ToString() + i.ToString()))
                        {
                            MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.载波试验).ToString() + i.ToString());
                        }
                    }
                    break;
                case ClearDataType.误差一致性数据:
                    MeterErrAccords.Clear();

                    #region-----------清理误差一致性结论-------------------
                    if (MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.误差一致性).ToString()))
                    {
                        MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.误差一致性).ToString());
                    }
                    #endregion

                    break;
                default:
                    break;
            }


        }


        public enum ClearDataType
        {
            多功能数据 = 1,

            误差数据 = 2,

            启动数据 = 3,

            潜动数据 = 4,

            走字数据 = 5,

            特殊检定数据 = 6,

            通讯协议检定数据 = 7,

            载波数据=8,

            误差一致性数据 = 9
        }
        #endregion
    }
}
