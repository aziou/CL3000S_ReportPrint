using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;

namespace CLDC_DataCore.Function
{

    /// <summary>
    /// 
    /// </summary>
    public class DoDataTable
    {
 
        /// <summary>
        /// �����ݱ��������
        /// </summary>
        /// <param name="dtSort">Ҫ����ı�</param>
        /// <param name="ColName">�����ֶ���[�����ֶ����ַ����ֶ���Ч]</param>
        /// <param name="Asc">����</param>
        public static void Sort(ref System.Data.DataTable dtSort, string ColName,bool Asc)
        {
            string IndexColName = "___Index___";
            System.Data.DataTable dtTmp = dtSort.Copy();
            dtTmp.Columns.Add(IndexColName,typeof(int));
            int MaxLen = 1;
            //ȡ�����ֶΣ�������ݳ��Ȳ�������������
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                dtTmp.Rows[i][IndexColName] = i;
                if (dtTmp.Rows[i][ColName].ToString().Length > MaxLen)
                    MaxLen = dtTmp.Rows[i][ColName].ToString().Length;
            }

            //�������ֶΰ��Ҷ��룬��߲�0
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                dtTmp.Rows[i][ColName] = dtTmp.Rows[i][ColName].ToString().PadRight(MaxLen, '0');
            }

            //��ѯ
            DataRow[] Rows;
            if (Asc)
                Rows =  dtTmp.Select("", string.Format("{0} Asc",ColName));
            else
                Rows = dtTmp.Select("", string.Format("{0} Desc",ColName));

            System.Data.DataTable dtTmp2 = dtSort.Copy();
            dtSort.Rows.Clear();
            for (int i = 0; i < Rows.Length; i++)
            {
                dtSort.Rows.Add(dtTmp2.Rows[Convert.ToInt32(Rows[i][IndexColName])].ItemArray);
            }
        }


    }
}
