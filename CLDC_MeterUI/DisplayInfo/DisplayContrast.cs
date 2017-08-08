#region FileHeader And Descriptions
// ***************************************************************
//  ViewContrast   date: 2010-04-09 By Gqs
//  -------------------------------------------------------------
//  Description:
//   ��ʾ���������
//   �����ݶ�Ϊ�̶�:��λ|У���|���1|���2|ƽ��ֵ|����ֵ|���1|���2|ƽ��ֵ|����ֵ|��ֵ|����
//  -------------------------------------------------------------
// ***************************************************************

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CLDC_DataCore.Struct;

namespace CLDC_MeterUI.DisplayInfo
{
    public partial class DisplayContrast : Base
    {
        /// <summary>
        /// ���һ������ĿID
        /// </summary>
        private const string Key = "2";

        public DisplayContrast()
        {
            InitializeComponent();
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            if (Data_View.Columns.Count == 0 || MeterInfo.MeterErrAccords.Count == 0) return;
            Data_View.Rows.Clear();

            
            //ÿ����λ�����ļ춨����
            int BwRows = 0;
            if (MeterInfo.MeterErrAccords.ContainsKey(Key))
                BwRows = MeterInfo.MeterErrAccords[Key].lstTestPoint.Count;
            else
                return;
            #region ------��ʼ����������ݱ�����
            //�����ǰ���ݱ�����С�ڵ��ܱ��λ��������Ҫ���´�����Ӧ���ݱ�


            int RowIndex = 0;
            bool bColor = false;
            string[] strSubKey = new string[MeterInfo.MeterErrAccords[Key].lstTestPoint.Keys.Count];
            int t = 0;
            foreach (string strKey in MeterInfo.MeterErrAccords[Key].lstTestPoint.Keys)
            {
                strSubKey[t++] = strKey;
            }

            //�ع�ÿ��λ��Ӧ������
            for (int j = 0; j < BwRows; j++)
            {
                RowIndex = Data_View.Rows.Add();
                if (bColor)
                {
                    Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                }
                else
                {
                    Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                }

                if (j % BwRows == 0)
                {
                    Data_View[0, j].Value = MeterInfo.ToString();
                }

                Data_View[1, j].Value = MeterInfo.MeterErrAccords[Key].lstTestPoint[strSubKey[j]].Mea_PrjName;
            }
            bColor = !bColor;

            #endregion

            #region ------��������------


            if (MeterInfo.MeterErrAccords.ContainsKey(Key))          //�������ģ�����Ѿ����ڸõ������
            {
                try
                {
                    for (int j = 0; j < MeterInfo.MeterErrAccords[Key].lstTestPoint.Count; j++)
                    {
                        string _subKey = strSubKey[j];
                        if (MeterInfo.MeterErrAccords[Key].lstTestPoint.ContainsKey(_subKey))
                        {
                            DataGridViewRow Row = Data_View.Rows[j];

                            //���ü춨����ɫ
                            if (MeterInfo.MeterErrAccords[Key].lstTestPoint[_subKey].Mea_ItemResult == CLDC_DataCore.Const.Variable.CTG_BuHeGe)            //������ϸ��޸ĵ�ǰ�б�����ɫΪ��ɫ
                            {
                                Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                            }
                            else
                            {
                                Row.DefaultCellStyle.ForeColor = Color.Black;
                            }

                            //�ֽ����
                            //��λ|У���|���1|���2|ƽ��ֵ|����ֵ|���1|���2|ƽ��ֵ|����ֵ|��ֵ|����
                            string[] Arr_Err = MeterInfo.MeterErrAccords[Key].lstTestPoint[_subKey].Mea_Wc1.Split('|');           //�ֽ����
                            for (int k = 0; k < Arr_Err.Length; k++)
                            {
                                if (k > 4) break;
                                Row.Cells[2 + k].Value = Arr_Err[k];
                            }

                            Arr_Err = MeterInfo.MeterErrAccords[Key].lstTestPoint[_subKey].Mea_Wc2.Split('|');
                            for (int k = 0; k < Arr_Err.Length; k++)
                            {
                                if (k > 4) break;
                                Row.Cells[6 + k].Value = Arr_Err[k];
                            }

                            //��ֵ
                            Row.Cells[10].Value = MeterInfo.MeterErrAccords[Key].lstTestPoint[_subKey].Mea_Wc;
                            //��д��ǰ�춨��Ľ���
                            Row.Cells[11].Value = MeterInfo.MeterErrAccords[Key].lstTestPoint[_subKey].Mea_ItemResult;
                        }
                    }
                }
                catch { }
            }
            else
            {
                for (int j = 0; j < BwRows; j++)
                {
                    DataGridViewRow Row = Data_View.Rows[j];

                    Row.DefaultCellStyle.ForeColor = Color.Black;
                    for (int k = 2; k < Data_View.Columns.Count; k++)
                    {
                        Row.Cells[k].Value = "";
                    }
                }
            }
            #endregion
            
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            if (Data_View.Columns.Count == 0 || MeterGroup.MeterGroup.Count == 0) return;
            Data_View.Rows.Clear();

            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
                //ÿ����λ�����ļ춨����
                int BwRows = 0;
                if (MeterGroup.MeterGroup[i].MeterErrAccords.ContainsKey(Key))
                    BwRows = MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint.Count;
                else
                    continue ;
                #region ------��ʼ����������ݱ�����
                //�����ǰ���ݱ�����С�ڵ��ܱ��λ��������Ҫ���´�����Ӧ���ݱ�


                int RowIndex = 0;
                bool bColor = false;
                string[] strSubKey = new string[MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint.Keys.Count];
                int t = 0;
                foreach (string strKey in MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint.Keys)
                {
                    strSubKey[t++] = strKey;
                }

                //�ع�ÿ��λ��Ӧ������
                for (int j = 0; j < BwRows; j++)
                {
                    int intCol = j + i * MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint.Count;
                    RowIndex = Data_View.Rows.Add();
                    if (bColor)
                    {
                        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }

                    Data_View[0, intCol].Value = MeterGroup.MeterGroup[i].ToString();

                    Data_View[1, intCol].Value = MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint[strSubKey[j]].Mea_PrjName;
                }
                bColor = !bColor;

                #endregion

                #region ------��������------


                if (MeterGroup.MeterGroup[i].MeterErrAccords.ContainsKey(Key))          //�������ģ�����Ѿ����ڸõ������
                {
                    try
                    {
                        for (int j = 0; j < MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint.Count; j++)
                        {
                            string _subKey = strSubKey[j];
                            if (MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint.ContainsKey(_subKey))
                            {
                                DataGridViewRow Row = Data_View.Rows[j + i * MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint.Count];

                                //���ü춨����ɫ
                                if (MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint[_subKey].Mea_ItemResult == CLDC_DataCore.Const.Variable.CTG_BuHeGe)            //������ϸ��޸ĵ�ǰ�б�����ɫΪ��ɫ
                                {
                                    Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                                }
                                else
                                {
                                    Row.DefaultCellStyle.ForeColor = Color.Black;
                                }

                                //�ֽ����
                                //��λ|У���|���1|���2|ƽ��ֵ|����ֵ|���1|���2|ƽ��ֵ|����ֵ|��ֵ|����
                                string[] Arr_Err = MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint[_subKey].Mea_Wc1.Split('|');           //�ֽ����
                                for (int k = 0; k < Arr_Err.Length; k++)
                                {
                                    if (k > 4) break;
                                    Row.Cells[2 + k].Value = Arr_Err[k];
                                }

                                Arr_Err = MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint[_subKey].Mea_Wc2.Split('|');
                                for (int k = 0; k < Arr_Err.Length; k++)
                                {
                                    if (k > 4) break;
                                    Row.Cells[6 + k].Value = Arr_Err[k];
                                }

                                //��ֵ
                                Row.Cells[10].Value = MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint[_subKey].Mea_Wc;
                                //��д��ǰ�춨��Ľ���
                                Row.Cells[11].Value = MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint[_subKey].Mea_ItemResult;
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    for (int j = 0; j < BwRows; j++)
                    {
                        DataGridViewRow Row = Data_View.Rows[j + i * MeterGroup.MeterGroup[i].MeterErrAccords[Key].lstTestPoint.Count];

                        Row.DefaultCellStyle.ForeColor = Color.Black;
                        for (int k = 2; k < Data_View.Columns.Count; k++)
                        {
                            Row.Cells[k].Value = "";
                        }
                    }
                }
                #endregion
            }
            SpanRow(0, Data_View.Rows.Count, 0);
        }

        private void SpanRow(int startRowIndex, int rowCount, int col)
        {
            if (rowCount == 0) return;

            int rowSpan = 1;

            int intstart = startRowIndex;

            for (int i = startRowIndex + 1; i < rowCount + intstart; i++)
            {
                if (Data_View[col, i].Value.ToString() == Data_View[col, startRowIndex].Value.ToString())
                {
                    rowSpan++;
                }
                else
                {
                    ((CLZDataGridView.DataGridViewTextBoxCellEx)Data_View[col, startRowIndex]).RowSpan = rowSpan;
                    if (col < 4)
                    {
                        SpanRow(startRowIndex, rowSpan, col + 1);
                    }
                    startRowIndex = i;
                    rowSpan = 1;
                }

            }

            ((CLZDataGridView.DataGridViewTextBoxCellEx)Data_View[col, startRowIndex]).RowSpan = rowSpan;

            if (col < 2)
            {
                SpanRow(startRowIndex, rowSpan, col + 1);
            }
        }
    }
}
