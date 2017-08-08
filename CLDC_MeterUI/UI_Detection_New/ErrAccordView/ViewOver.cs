#region FileHeader And Descriptions
// ***************************************************************
//  ViewOver   date: 2010-04-09 By Gqs
//  -------------------------------------------------------------
//  Description:
//   ��ʾ������������
//   �����ݶ�Ϊ�̶�:��λ|У���|���1|���2|ƽ��ֵ|����ֵ|����
//  -------------------------------------------------------------
// ***************************************************************

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using CLDC_DataCore.Struct;

namespace CLDC_MeterUI.UI_Detection_New.ErrAccordView
{
    public partial class ViewOver : UserControl
    {
        public ViewOver()
        {
            InitializeComponent();
        }

        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup, CLDC_DataCore.Struct.StErrAccord CurItem)
        {
            if (Data_View.Columns.Count == 0 || MeterGroup.Count == 0) return;

            int BwRows = CurItem.lstErrPoint.Count;      //ÿ����λ�����ļ춨����

            #region ------��ʼ����������ݱ�����
            //�����ǰ���ݱ�����С�ڵ��ܱ��λ��������Ҫ���´�����Ӧ���ݱ�
            if (Data_View.Rows.Count != MeterGroup.Count * BwRows)
            {
                Data_View.Rows.Clear();
                int RowIndex = 0;
                bool bColor = false;

                for (int i = 0; i < MeterGroup.Count; i++)
                {
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

                        if ((i * BwRows + j) % BwRows == 0)
                        {
                            Data_View[0, i * BwRows + j].Value = MeterGroup[i].ToString();
                        }

                        Data_View[1, i * BwRows + j].Value = CurItem.lstErrPoint[j].TestPointName;
                    }
                    bColor = !bColor;
                }
            }
            #endregion

            #region ------��������------
            for (int i = 0; i < MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup[i];

                if (!_MeterInfo.YaoJianYn)        //������죬����Ϊ��
                {
                    for (int j = 0; j < BwRows; j++)
                    {
                        DataGridViewRow Row = Data_View.Rows[i * BwRows + j];

                        for (int k = 2; k < Row.Cells.Count; k++)
                        {
                            Row.Cells[k].Value = string.Empty;
                        }
                    }
                    continue;
                }

                string _ItemKey = CurItem.ErrAccordType.ToString();

                if (_MeterInfo.MeterErrAccords.ContainsKey(_ItemKey))          //�������ģ�����Ѿ����ڸõ������
                {
                    try
                    {
                        for (int j = 0; j < _MeterInfo.MeterErrAccords[_ItemKey].lstTestPoint.Count; j++)
                        {
                            string _subKey = CurItem.lstErrPoint[j].PrjID;
                            if (_MeterInfo.MeterErrAccords[_ItemKey].lstTestPoint.ContainsKey(_subKey))
                            {
                                DataGridViewRow Row = Data_View.Rows[i * BwRows + j];

                                //���ü춨����ɫ
                                if (_MeterInfo.MeterErrAccords[_ItemKey].lstTestPoint[_subKey].Mea_ItemResult == CLDC_DataCore.Const.Variable.CTG_BuHeGe)            //������ϸ��޸ĵ�ǰ�б�����ɫΪ��ɫ
                                {
                                    Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                                }
                                else
                                {
                                    Row.DefaultCellStyle.ForeColor = Color.Black;
                                }

                                //�ֽ����
                                //��λ|У���|���1|���2|ƽ��ֵ|����ֵ|����
                                string[] Arr_Err = _MeterInfo.MeterErrAccords[_ItemKey].lstTestPoint[_subKey].Mea_Wc1.Split('|');           //�ֽ����
                                for (int k = 0; k < Arr_Err.Length; k++)
                                {
                                    if (k > 4) break;
                                    Row.Cells[2 + k].Value = Arr_Err[k];
                                }

                                //��д��ǰ�춨��Ľ���
                                Row.Cells[6].Value = _MeterInfo.MeterErrAccords[_ItemKey].lstTestPoint[_subKey].Mea_ItemResult;
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    for (int j = 0; j < BwRows; j++)
                    {
                        DataGridViewRow Row = Data_View.Rows[i * BwRows + j];

                        Row.DefaultCellStyle.ForeColor = Color.Black;
                        for (int k = 2; k < Data_View.Columns.Count; k++)
                        {
                            Row.Cells[k].Value = "";
                        }
                    }
                }
            }
            #endregion
        }
    }
}
