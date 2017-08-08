using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;
using CLDC_DataCore.Const;
namespace CLDC_DataCore.Command.Login2
{

    /// <summary>
    /// 请求登陆
    /// </summary>
    [Serializable()]
    public class Login_Ask:Command_Ask 
    {
        //下面是数据字段
        /// <summary>
        /// 台子编号
        /// </summary>
        public byte DeskNo = 0;		    
        /// <summary>
        /// 表位数
        /// </summary>
        public int	PosCount = 0;
        /// <summary>
        /// 台子类型（0-三相台，1-单相台）
        /// </summary>
        public byte	DeskType  =0;		
        /// <summary>
        /// 是否是被控
        /// </summary>
        public bool BeControl = false; 
        /// <summary>
        /// 台子名称
        /// </summary>
        public string DeskName = "未命名";
        /// <summary>
        /// 
        /// </summary>
        public Login_Ask()
        {
            AskMessage = "请求登陆";
        }
    }
}
