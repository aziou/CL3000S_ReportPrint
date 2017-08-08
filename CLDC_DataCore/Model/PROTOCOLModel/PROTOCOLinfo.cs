using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.PROTOCOLModel
{
    /// <summary>
    /// 协议基本信息
    /// </summary>
   public class PROTOCOLinfo
    {
       /// <summary>
       /// 编号，唯一索引
       /// </summary>
       public int PK_LNG_DLT_ID { get; set; }
       /// <summary>
       ///国标协议代号0：DLT645-2007，1：DLT645-1997 
       /// </summary>
       public string AVR_PROTOCOL_NO { get; set; }
       /// <summary>
       /// 规约名称
       /// </summary>
       public string AVR_PROTOCOL_NAME { get; set; }
       /// <summary>
       /// 数据标识编码类型,1=电能量、2=最大需量、3=变量、4=事件记录、5=参变量、6=冻结、7=负荷记录、8=安全认证读、9=安全认证写
       /// </summary>
       public string AVR_IDENT_TYPE { get; set; }
       /// <summary>
       /// 项名称
       /// </summary>
       public string AVR_ITEM_NAME { get; set; }
       /// <summary>
       /// 数据标识
       /// </summary>
       public string AVR_ID { get; set; }
       /// <summary>
       /// 权限，防止特殊命令使用特殊权限，例如：一般禁止写通讯地址。0：禁止写，1：普通（遵循协议的读写），2：特殊（表厂自定的超级权限等）
       /// </summary>
       public string CHR_CLASS { get; set; }
       /// <summary>
       /// 长度
       /// </summary>
       public string LNG_LENGTH { get; set; }
       /// <summary>
       /// 小数位
       /// </summary>
       public string LNG_DOT { get; set; }
       /// <summary>
       /// 操作方式：0=只读，1=只写，2=读写。当可读写时，根据自定方案决定读或写。
       /// </summary>
       public string CHR_TYPE { get; set; }
       /// <summary>
       /// 格式串
       /// </summary>
       public string AVR_FORMAT { get; set; }
       /// <summary>
       /// 当写时可提供的默认值，实际写以自定方案为准
       /// </summary>
       public string AVR_DEF_VALUE { get; set; }

       
    }
}
