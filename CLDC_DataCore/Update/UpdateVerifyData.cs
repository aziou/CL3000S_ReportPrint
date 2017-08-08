using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Update
{
    /// <summary>
    /// 局部数据更新处理类：服务器与客户端共用
    /// </summary>
    public class UpdateVerifyData
    {
        public string ErrorMsg = string.Empty;
        //电能表模型
        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo m_DnbGroup = null;
        /// <summary>
        /// 数据更新处理
        /// </summary>
        /// <param name="Cmd_Data"></param>
        /// <returns></returns>
        public bool UpdateData(CLDC_DataCore.Command.Update.UpdateData_Ask Cmd_Data)
        {
            if (m_DnbGroup == null)
            {
                ErrorMsg = "要更新的电能表模型为空";
                return false;
            }

            if (Cmd_Data == null)
            {
                ErrorMsg = "网络数据包为null";
                return false;
            }

            int DataLen = Cmd_Data.strKey.Length;
            if (DataLen != Cmd_Data.objData.Length)
            {
                ErrorMsg = "要更新的数据键名与数据值不一一对应";
                return false;
            }

            int startBW;
            int endBW;
            object objDst;

            //统一单块表与全部表
            if (Cmd_Data.BW == 999)
            {
                startBW = 0;
                endBW = DnbGroup._Bws;
            }
            else
            {
                startBW = Cmd_Data.BW;
                endBW = Cmd_Data.BW + 1;
            }

            string strItemKey = string.Empty;
            object objItemValue = null;

            //开始赋值
            for (int BW = startBW; BW < endBW; BW++)
            {

                if (Cmd_Data.BW == 999 && DataLen > 1)
                {
                    strItemKey = Cmd_Data.strKey[BW];
                    objItemValue = Cmd_Data.objData[BW];
                }
                else
                {
                    strItemKey = Cmd_Data.strKey[0];
                    objItemValue = Cmd_Data.objData[0];
                }

                switch (Cmd_Data.DataType)
                {
                    #region----------基本信息----------
                    case CLDC_Comm.Enum.Cus_MeterDataType.基本信息:
                        {

                            objDst = this.DnbGroup.MeterGroup[BW];

                            if (Cmd_Data.BW == 999)
                            {
                                CLDC_DataCore.Function.Common.SetObjValue(objDst
                                    , strItemKey
                                    , objItemValue);
                            }
                            else
                            {
                                //基本信息一块表可以同时更新多条数据
                                for (int k = 0; k < DataLen; k++)
                                {
                                    CLDC_DataCore.Function.Common.SetObjValue(objDst
                                        , Cmd_Data.strKey[k]
                                        , Cmd_Data.objData[k]);
                                }
                            }

                            break;
                        }
                    #endregion

                    #region----------检定方案----------
                    case CLDC_Comm.Enum.Cus_MeterDataType.检定方案:
                        {

                            //if (!Comm.Function.Number.IsNumeric(strItemKey))
                            //    continue;
                            //int curPlanIndex = int.Parse(strItemKey);
                            //this.DnbGroup.MeterGroup[BW].MeterPlan.CheckPlan.RemoveAt(curPlanIndex);
                            //this.DnbGroup.MeterGroup[BW].MeterPlan.CheckPlan.Insert(curPlanIndex, objItemValue);

                            break;
                        }
                    #endregion

                    #region----------检定结论----------
                    case CLDC_Comm.Enum.Cus_MeterDataType.检定结论:
                        {
                            if (strItemKey != null)
                            {
                                if (DnbGroup.MeterGroup[BW].MeterResults.ContainsKey(strItemKey))
                                {
                                    DnbGroup.MeterGroup[BW].MeterResults.Remove(strItemKey);
                                }
                                if (!Cmd_Data.isDelete)
                                {
                                    DnbGroup.MeterGroup[BW].MeterResults.Add(strItemKey, (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult)objItemValue);
                                }
                            }

                            break;
                        }
                    #endregion

                    #region----------误差数据----------
                    case CLDC_Comm.Enum.Cus_MeterDataType.误差数据:
                        {
                            if (strItemKey != null)
                            {
                                if (DnbGroup.MeterGroup[BW].MeterErrors.ContainsKey(strItemKey))
                                {
                                    DnbGroup.MeterGroup[BW].MeterErrors.Remove(strItemKey);
                                }
                                if (!Cmd_Data.isDelete && objItemValue != null)
                                {
                                    DnbGroup.MeterGroup[BW].MeterErrors.Add(strItemKey, (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError)objItemValue);
                                }
                            }
                            break;
                        }
                    #endregion

                    #region----------走字数据----------
                    case CLDC_Comm.Enum.Cus_MeterDataType.走字数据:
                        {
                            if (strItemKey != null)
                            {
                                if (DnbGroup.MeterGroup[BW].MeterZZErrors.ContainsKey(strItemKey))
                                {
                                    DnbGroup.MeterGroup[BW].MeterZZErrors.Remove(strItemKey);
                                }
                                if (!Cmd_Data.isDelete)
                                {
                                    DnbGroup.MeterGroup[BW].MeterZZErrors.Add(strItemKey, (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError)objItemValue);
                                }
                            }
                            break;
                        }
                    #endregion

                    #region----------多功能数据----------
                    case CLDC_Comm.Enum.Cus_MeterDataType.多功能数据:
                        {
                            if (strItemKey != null)
                            {
                                if (DnbGroup.MeterGroup[BW].MeterDgns.ContainsKey(strItemKey))
                                {
                                    DnbGroup.MeterGroup[BW].MeterDgns.Remove(strItemKey);
                                }
                                if (!Cmd_Data.isDelete && objItemValue != null)
                                {
                                    DnbGroup.MeterGroup[BW].MeterDgns.Add(strItemKey, (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn)objItemValue);
                                }
                            }

                            break;
                        }
                    #endregion

                    #region----------载波数据-------------
                    case CLDC_Comm.Enum.Cus_MeterDataType.载波数据:
                        {
                            if (strItemKey != null)
                            {
                                if (DnbGroup.MeterGroup[BW].MeterCarrierDatas.ContainsKey(strItemKey))
                                {
                                    DnbGroup.MeterGroup[BW].MeterCarrierDatas.Remove(strItemKey);
                                }
                                if (!Cmd_Data.isDelete && objItemValue != null)
                                {
                                    DnbGroup.MeterGroup[BW].MeterCarrierDatas.Add(strItemKey, (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterCarrierData)objItemValue);
                                }
                            }
                        }
                        break;
                    #endregion

                    #region----------误差一致性数据  add by Jeson Wong ----------
                    case CLDC_Comm.Enum.Cus_MeterDataType.误差一致性数据:
                        {
                            if (strItemKey != null)
                            {
                                if (DnbGroup.MeterGroup[BW].MeterErrAccords.ContainsKey(strItemKey))
                                {
                                    DnbGroup.MeterGroup[BW].MeterErrAccords.Remove(strItemKey);
                                }
                                if (!Cmd_Data.isDelete && objItemValue != null)
                                {
                                    DnbGroup.MeterGroup[BW].MeterErrAccords.Add(strItemKey, (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccord)objItemValue);
                                }
                            }
                            break;
                        }
                    #endregion

                    default:
                        {
                            ErrorMsg = "不明更新数据类型" + Cmd_Data.DataType.ToString();
                            return false;
                            //break;
                        }
                }
            }
            return true;
        }
        /// <summary>
        /// 设置电能表模型
        /// </summary>
        public CLDC_DataCore.Model.DnbModel.DnbGroupInfo DnbGroup
        {
            set { m_DnbGroup = value; }
            get { return m_DnbGroup; }
        }
    }
}
