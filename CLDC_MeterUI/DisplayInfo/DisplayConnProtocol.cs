using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{
    /// <summary>
    /// 通讯协议检查试验检定数据
    /// </summary>
    public partial class DisplayConnProtocol : Base 
    {
        public DisplayConnProtocol()
        {
            InitializeComponent();
        }
        public DisplayConnProtocol(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            try
            {
                if (MeterInfo.MeterResults.Count == 0) return;
                Conn_Data.Rows.Clear();
                DataTable dtKeys = new DataTable();
                dtKeys.Columns.Add("Keys", typeof(string));
                dtKeys.Columns.Add("PrjId", typeof(string));

                foreach (string Key in MeterInfo.MeterResults.Keys)
                {
                    if (Key.IndexOf(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString("D3")) != -1)
                    {
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult meterResult = MeterInfo.MeterResults[Key];
                        if (meterResult.Mr_chrRstId != null && meterResult.Mr_chrRstName != null)
                            dtKeys.Rows.Add(Key, meterResult.Mr_chrRstId);
                    }
                }

                //只计算基本误差的数据
                DataRow[] Rows = dtKeys.Select("Keys <> '' and  PrjId Like '" + ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString("D3") + "%' ", "PrjId asc");

                for (int j = 0; j < Rows.Length; j++)
                {
                    string Key = Rows[j][0].ToString();
                    if (!MeterInfo.YaoJianYn) continue; 
                    if (!MeterInfo.MeterResults.ContainsKey(Key)) continue;
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult meterResult = MeterInfo.MeterResults[Key];
                    if (meterResult.Mr_chrRstId != null)
                    {
                        int rowIndex = Conn_Data.Rows.Add();
                        Conn_Data["表位", rowIndex].Value = MeterInfo.ToString();
                        Conn_Data["项目名称", rowIndex].Value = meterResult.Mr_chrRstName;
                        Conn_Data["项目数据", rowIndex].Value = meterResult.Mr_chrRstValue;
                    }   
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        public DisplayConnProtocol(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterGroup, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            try
            {
                if (MeterGroup.MeterGroup.Count == 0) return;
                Conn_Data.Rows.Clear();
                DataTable dtKeys = new DataTable();
                dtKeys.Columns.Add("Keys", typeof(string));
                dtKeys.Columns.Add("PrjId", typeof(string));

                foreach (string Key in MeterGroup.MeterGroup[MeterGroup.GetFirstYaoJianMeterBwh()].MeterResults.Keys)
                {
                    if (Key.IndexOf(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString("D3")) != -1)
                    {
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult meterResult = MeterGroup.MeterGroup[MeterGroup.GetFirstYaoJianMeterBwh()].MeterResults[Key];
                        if (meterResult.Mr_chrRstId != null && meterResult.Mr_chrRstName != null)
                            dtKeys.Rows.Add(Key, meterResult.Mr_chrRstId);
                    }
                }

                //只计算基本误差的数据
                DataRow[] Rows = dtKeys.Select("Keys <> '' and  PrjId Like '" + ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString("D3") + "%' ", "PrjId asc");


                for (int i = 0; i < MeterGroup._Bws; i++)
                {
                    if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;                 
                    for (int j = 0; j < Rows.Length; j++)
                    {
                        string Key = Rows[j][0].ToString();
                        if (!MeterGroup.MeterGroup[i].MeterResults.ContainsKey(Key)) continue;
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult meterResult = MeterGroup.MeterGroup[i].MeterResults[Key];
                        if (meterResult.Mr_chrRstId != null)
                        {
                            int rowIndex = Conn_Data.Rows.Add();
                            Conn_Data["表位", rowIndex].Value = MeterGroup.MeterGroup[i].ToString();
                            Conn_Data["项目名称", rowIndex].Value = meterResult.Mr_chrRstName;
                            Conn_Data["项目数据", rowIndex].Value = meterResult.Mr_chrRstValue;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
    }
}
