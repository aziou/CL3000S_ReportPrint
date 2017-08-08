using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 数据显示功能
    /// </summary>
    [Serializable()]
    public class MeterShow : MeterErrorBase
    {
        
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Msh_chrProjectName = "";
        /// <summary>
        /// 组别	
        /// </summary>
        public string Msh_chrGrpType = "";
        /// <summary>
        /// 项目类型
        /// </summary>
        public string Msh_intItemType = "";
        /// <summary>
        /// 结论	
        /// </summary>
        public string Msh_chrJL = "";
        /// <summary>
        /// 实验项目类型主键
        /// </summary>
        public int Msh_intType = 0;
        /// <summary>
        /// 实验项目类型名称
        /// </summary>
        public string Msh_chrType = "";
        /// <summary>
        /// 分项实验项目主键
        /// </summary>
        public int Msh_intSubItem = 0;
        /// <summary>
        /// 分项实验项目名称	
        /// </summary>
        public string Msh_chrSubItem = "";
        /// <summary>
        /// 标示符
        /// </summary>
        public string Msh_chrID = "";
        /// <summary>
        /// 数据长度	
        /// </summary>
        public int Msh_intLength = 0;
        /// <summary>
        /// 小数位
        /// </summary>
        public int Msh_intDot = 0;
        /// <summary>
        /// 数据格式	
        /// </summary>
        public string Msh_chrFormat = "";
        /// <summary>
        /// 读写标志
        /// </summary>
        public int Msh_intReadWrite = 0;
        /// <summary>
        /// 对比内容	
        /// </summary>
        public string Msh_chrContent = "";
        /// <summary>
        /// 读取数据
        /// </summary>
        public string Msh_chrData = "";
        /// <summary>
        /// 备用1	
        /// </summary>
        public string Msh_c_Other1 = "";
        /// <summary>
        /// 备用2
        /// </summary>
        public string Msh_c_Other2 = "";
        /// <summary>
        /// 备用3	
        /// </summary>
        public string Msh_c_Other3 = "";
        /// <summary>
        /// 备用4
        /// </summary>
        public string Msh_c_Other4 = "";
        /// <summary>
        /// 备用5	
        /// </summary>
        public string Msh_c_Other5 = "";
        /// <summary>
        /// 备用6
        /// </summary>
        public string Msh_c_Other6 = "";
        /// <summary>
        /// 备用7	
        /// </summary>
        public string Msh_c_Other7 = "";
        /// <summary>
        /// 备用8
        /// </summary>
        public string Msh_c_Other8 = "";
        /// <summary>
        /// 备用9	
        /// </summary>
        public string Msh_c_Other9 = "";
        /// <summary>
        /// 备用10
        /// </summary>
        public string Msh_c_Other10 = "";
    }
}
