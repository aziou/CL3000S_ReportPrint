using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 
    /// </summary>
    public class BindCombox
    {
        /// <summary>
        /// ��LstData�е����ݰ󶨵�������
        /// </summary>
        /// <param name="LstData"></param>
        /// <param name="CmbBox"></param>
        public static void BindComboxItem(ComboBox CmbBox, List<string> LstData)
        {
            //CmbBox.Items.Clear();
            DataTable dtTmp = new DataTable();
            dtTmp.Columns.Add("����", typeof(string));
            dtTmp.Columns.Add("ֵ", typeof(string));

            for (int i = 0; i < LstData.Count; i++)
            {
                dtTmp.Rows.Add(new object[] {LstData[i],LstData[i] });
            }
            CmbBox.DisplayMember = "����";
            CmbBox.ValueMember = "ֵ";
            CmbBox.DataSource = dtTmp;
            
        }

        /// <summary>
        /// ��LstData�е����ݰ󶨵�������
        /// </summary>
        /// <param name="CmbBox"></param>
        /// <param name="LstData"></param>
        /// <param name="Sort">�Ƿ�����</param>
        public static void BindComboxItem(ComboBox CmbBox, List<string> LstData,bool Sort)
        {
            if (Sort)
            {
                SortList(ref LstData,true );
            }
            BindComboxItem(CmbBox, LstData);
        }

        private static void SortList(ref List<string> LstData,bool Asc)
        {
            if (LstData.Count < 1) return;
            //LstData.Sort();

            DataTable dtTmp = new DataTable();
            dtTmp.Columns.Add("Value",typeof(string));
            dtTmp.Columns.Add("OrderBy", typeof(string));

            //��ȡ��ַ�������
            int MaxLen = 1;
            for (int i = 0; i < LstData.Count; i++)
            {
                if (LstData[i].Length > MaxLen)
                    MaxLen = LstData[i].Length;
            }

            //�� List �е����ݵ��뵽���ݱ���
            for (int i = 0; i < LstData.Count; i++)
            {
                dtTmp.Rows.Add(new object[] {LstData[i],LstData[i].PadLeft(MaxLen,'0') });
            }
            DataRow[] Rows;
            if (Asc)
                Rows = dtTmp.Select("", " OrderBy Asc");
            else
                Rows = dtTmp.Select("", " OrderBy Desc");

            LstData.Clear();
            for (int i = 0; i < Rows.Length; i++)
                LstData.Add(Rows[i]["Value"].ToString());

            dtTmp.Dispose();
        }

        /// <summary>
        /// ��LstData�е����ݰ󶨵�������
        /// </summary>
        /// <param name="LstData"></param>
        /// <param name="CmbBox"></param>
        /// <param name="AddNew">��Ҫ������</param>
        /// <param name="ZiDianName">�ֵ���</param>
        /// <param name="RegFilter">������ʽ������ƥ�������ֵ��ʽ�Ƿ�����Ҫ���������ִ�Сд</param>
        /// <param name="hZiDian">�����Ժ󱣴浽���ֵ����= null ��ʹ��Comm.GlobalUnit.g_SystemConfig.ZiDianGroup</param>
        public static void BindComboxItem(ComboBox CmbBox, List<string> LstData,bool AddNew,string ZiDianName,string RegFilter,CLDC_DataCore.SystemModel.Item.csDictionary hZiDian)
        {
            BindComboxItem(CmbBox,  LstData);
            if (AddNew)
            {
                ((DataTable)CmbBox.DataSource).Rows.Add(new object[] { "===����===", RegFilter });
            }
            if (LstData.Count < 1 )
                CmbBox.SelectedIndex = -1;

            CmbBox.DropDownClosed -= delegate(object sender, EventArgs e) { };
            CmbBox.DropDownClosed += delegate(object sender, EventArgs e)
            {
                int index = ((ComboBox)sender).SelectedIndex;
                if (index == -1) return;
                if (((DataTable)((ComboBox)sender).DataSource).Rows[index]["����"].ToString() != "===����===") return;
                Form FrmBindCmbBox;
                if (hZiDian == null)
                {
                    FrmBindCmbBox = new BindCombox_NewValue((ComboBox)sender, ZiDianName, ((ComboBox)CmbBox).SelectedValue.ToString());
                }
                else
                {
                    FrmBindCmbBox = new BindCombox_NewValue((ComboBox)sender, ZiDianName, ((ComboBox)CmbBox).SelectedValue.ToString(),hZiDian);
                }
                if (FrmBindCmbBox.ShowDialog() != DialogResult.OK)
                {
                    ((ComboBox)sender).SelectedIndex = -1;
                }
                FrmBindCmbBox.Dispose();
            };
        }

        /// <summary>
        /// ��LstData�е����ݰ󶨵�������
        /// </summary>
        /// <param name="CmbBox"></param>
        /// <param name="LstData"></param>
        /// <param name="AddNew">�Ƿ��������</param>
        /// <param name="ZiDianName">�ֵ���</param>
        /// <param name="RegFilter">������ʽ������ƥ�������ֵ��ʽ�Ƿ�����Ҫ���������ִ�Сд</param>
        /// <param name="Sort">�Ƿ���Ҫ����</param>
        /// <param name="hZiDian">�����Ժ󱣴浽���ֵ����= null ��ʹ��Comm.GlobalUnit.g_SystemConfig.ZiDianGroup</param>
        public static void BindComboxItem(ComboBox CmbBox, List<string> LstData, bool AddNew, string ZiDianName, string RegFilter, bool Sort, CLDC_DataCore.SystemModel.Item.csDictionary hZiDian)
        {
            if (Sort)
            {
                SortList(ref LstData,true);
            }
            BindComboxItem( CmbBox,  LstData,  AddNew,  ZiDianName,  RegFilter,hZiDian);
        }

        /// <summary>
        /// ��ȡ������ֵ
        /// </summary>
        /// <param name="CmbBox"></param>
        /// <returns></returns>
        public static string GetSelectItemValue(ComboBox CmbBox)
        {
            if (CmbBox.SelectedIndex == -1)
                return "";
            if (CmbBox.DataSource == null)
                return CmbBox.Items[CmbBox.SelectedIndex].ToString();
            else
                return CmbBox.SelectedValue.ToString();
        }
           
        /// <summary>
        /// ��ȡ��������ʾ����
        /// </summary>
        /// <param name="CmbBox"></param>
        /// <returns></returns>
        public static string GetSelectItemText(ComboBox CmbBox)
        {
            if (CmbBox.SelectedIndex == -1)
                return "";
            if (CmbBox.DataSource == null)
                return CmbBox.Items[CmbBox.SelectedIndex].ToString();
            else
                return CmbBox.SelectedText.ToString().Length > 0 ? CmbBox.SelectedText.ToString() : CmbBox.Text;
        }

        /// <summary>
        /// ����ѡ����
        /// </summary>
        /// <param name="CmbBox"></param>
        /// <param name="strValue"></param>
        public static void BindSelectItem(ComboBox CmbBox, string strValue)
        {
            try
            {
                for (int i = 0; i < CmbBox.Items.Count; i++)
                {
                    if (CmbBox.Items[i].ToString() == strValue)
                    {
                        CmbBox.SelectedIndex = i;
                        return;
                    }
                }
            }
            catch { }

            try
            {
                for (int i = 0; i < ((DataTable)CmbBox.DataSource).Rows.Count; i++)
                {
                    if (((DataTable)CmbBox.DataSource).Rows[i][1].ToString() == strValue)
                    {
                        try
                        {
                            CmbBox.SelectedIndex = i;
                        }
                        catch {
                            CmbBox.SelectedValue = strValue;
                        }
                        return;
                    }
                        
                }
            }
            catch { }
            CmbBox.SelectedIndex = -1;
        }
    }
}
