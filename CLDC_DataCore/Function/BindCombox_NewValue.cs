using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BindCombox_NewValue : Form
    {
        private ComboBox CmbBox_Option = null;
        private string RegFilter_Option = string.Empty;
        private string ZiDianName_Option = string.Empty;

        private CLDC_DataCore.SystemModel.Item.csDictionary hZiDianGroup = null;

        /// <summary>
        /// 
        /// </summary>
        public BindCombox_NewValue()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CmbBox"></param>
        /// <param name="ZiDianName"></param>
        /// <param name="RegFilter"></param>
        public BindCombox_NewValue(ComboBox CmbBox, string ZiDianName, string RegFilter)
        {
            InitializeComponent();
            CmbBox_Option = CmbBox;
            RegFilter_Option = RegFilter;
            ZiDianName_Option = ZiDianName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CmbBox"></param>
        /// <param name="ZiDianName"></param>
        /// <param name="RegFilter">用于匹配格式的正则字符串</param>
        /// <param name="hZiDian">新增以后保存到的字典对象</param>
        public BindCombox_NewValue(ComboBox CmbBox, string ZiDianName, string RegFilter,CLDC_DataCore.SystemModel.Item.csDictionary hZiDian)
            :this( CmbBox,  ZiDianName,  RegFilter)
        {
            hZiDianGroup = hZiDian;
        }


        private void BindCombox_NewValue_Load(object sender, EventArgs e)
        {
            Point PosOld = new Point(this.Location.X, this.Location.Y);
            PosOld.Y -= PosOld.Y / 3;
            this.Location = PosOld;
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            //
            string strNewValue = Txt_Value.Text.Trim();
            if (strNewValue.Length < 1)
            {
                MessageBox.Show("请输入值!");
                Txt_Value.Focus();
                return;
            }
            Regex RegFilter = new Regex(RegFilter_Option, RegexOptions.IgnoreCase);
            if (RegFilter.Replace(strNewValue, "").Length > 0)
            {
                MessageBox.Show("你输入的数据的格式不符合要求");
                Txt_Value.Focus();
                return;
            }
            foreach (DataRow Row in ((DataTable)CmbBox_Option.DataSource).Rows)
            {
                if (Row["值"].ToString() == strNewValue)
                {
                    MessageBox.Show("这个数据已经存在!");
                    Txt_Value.Focus();
                    return;
                }
            }

            if (hZiDianGroup != null)
            {
                hZiDianGroup.Add(ZiDianName_Option, strNewValue);
                hZiDianGroup.Save();
            }
            else
            {
                CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.ZiDianGroup.Add(ZiDianName_Option, strNewValue);
                CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.ZiDianGroup.Save();
            }
            {
                //((DataTable)CmbBox_Option.DataSource).Rows.Add(new object[] {strNewValue,strNewValue });
                DataRow Row = ((DataTable)CmbBox_Option.DataSource).NewRow();
                Row.ItemArray = new object[] { strNewValue, strNewValue };
                ((DataTable)CmbBox_Option.DataSource).Rows.InsertAt(Row, ((DataTable)CmbBox_Option.DataSource).Rows.Count - 1);
                CLDC_DataCore.Function.BindCombox.BindSelectItem(CmbBox_Option, strNewValue);
            }
            this.Tag = strNewValue;
            this.DialogResult = DialogResult.OK;       
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Txt_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                Btn_Ok_Click(Btn_Ok, e);
            }
            else if ((int)e.KeyCode == 27)
            {
                Btn_Cancel_Click(sender, e);
            }
        }




    }
}