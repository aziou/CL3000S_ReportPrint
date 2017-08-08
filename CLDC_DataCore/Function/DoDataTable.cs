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
        /// 对数据表进行排序
        /// </summary>
        /// <param name="dtSort">要排序的表</param>
        /// <param name="ColName">排序字段名[排序字段是字符串字段有效]</param>
        /// <param name="Asc">升序</param>
        public static void Sort(ref System.Data.DataTable dtSort, string ColName,bool Asc)
        {
            string IndexColName = "___Index___";
            System.Data.DataTable dtTmp = dtSort.Copy();
            dtTmp.Columns.Add(IndexColName,typeof(int));
            int MaxLen = 1;
            //取排序字段，最大数据长度并且设置行索引
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                dtTmp.Rows[i][IndexColName] = i;
                if (dtTmp.Rows[i][ColName].ToString().Length > MaxLen)
                    MaxLen = dtTmp.Rows[i][ColName].ToString().Length;
            }

            //将排序字段按右对齐，左边补0
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                dtTmp.Rows[i][ColName] = dtTmp.Rows[i][ColName].ToString().PadRight(MaxLen, '0');
            }

            //查询
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
