using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    [Serializable()]
    public class MeterError:MeterErrorBase 
    {
        public MeterError()
        {

        }

        /// <summary>
        /// 误差项目ID
        /// </summary>
        public string Me_chrProjectNo = "";
        /// <summary>
        /// 项目名称描述
        /// </summary>
        //public string MePrjName = "";
        /// <summary>
        /// 误差类别
        /// </summary>
        public int Me_intWcType = 0;
        /// <summary>
        /// 元件
        /// </summary>
        public int Me_intYj = 0;
        /// <summary>
        /// 项目结论		合格/不合格
        /// </summary>
        public string Me_chrWcJl = CLDC_DataCore.Const.Variable.CTG_DEFAULTRESULT;
        /// <summary>
        /// 功率因素		1.0，0.5L等
        /// </summary>
        public string Me_chrGlys = "";
        /// <summary>
        /// 检定方向
        /// </summary>
        public string Me_Glfx = "";
        /// <summary>
        /// 额定电流的倍数		
        /// </summary>
        public string Me_dblxIb = "";
        /// <summary>
        /// 9.额定电流IB的倍数的字符串（IB、IMAX）
        /// </summary>
        public string AVR_IB_MULTIPLE_STRING { get; set; }
        
        /// <summary>
        /// 误差值		误差一|误差二|...|误差平均|误差化整
        /// </summary>	
        public string MeWc = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Me_chrMemo = "";

        /// <summary>
        /// 误差化整值
        /// </summary>
        public string Me_chrWcHz = "";
        /// <summary>
        /// 误差值
        /// </summary>
        public string Me_chrWc = "";
        /// <summary>
        /// 13.偏差值
        /// </summary>
        public string AVR_STANDARD_ERROR { get; set; }
        /// <summary>
        /// 14.更多次误差  更多次数误差，格式：误差10#误差11#...#...，以“#”分隔
        /// </summary>
        public string Me_chrWcMore = "";
        /// <summary>
        /// 15.误差限		上线|下线
        /// </summary>
        public string Me_WcLimit = "";

        /// <summary>
        /// 16.不平衡负载与平衡负载时的上限
        /// </summary>
        public string AVR_UPPER_LIMIT { get; set; }

        /// <summary>
        /// 17.不平衡负载与平衡负载时的下限
        /// </summary>
        public string AVR_LOWER_LIMIT { get; set; }

        /// <summary>
        /// 18.圈数
        /// </summary>
        public string AVR_CIRCLE_COUNT { get; set; }

        /// <summary>
        /// 19.不平衡负载与平衡负载时误差之差试验。0：没有，1：有
        /// </summary>
        public string CHR_DIF_ERR_FLAG { get; set; }
        ///// 误差4
        /// <summary>
        /// 20.平衡负载时误差原始值，以#分隔
        /// </summary>
        public string AVR_DIF_H_ERRORS { get; set; }

        /// <summary>
        /// 21.平衡负载时误差平均值
        /// </summary>
        public string AVR_DIF_H_ERR_AVG { get; set; }

        /// <summary>
        /// 22.平衡负载时误差平均值化整
        /// </summary>
        public string AVR_DIF_H_ERR_ROUND { get; set; }

        /// <summary>
        /// 23.不平衡负载与平衡负载时误差之差（平均值的差）
        /// </summary>
        public string AVR_DIF_ERR_AVG { get; set; }

        /// <summary>
        /// 24.不平衡负载与平衡负载时误差之差化整
        /// </summary>
        public string AVR_DIF_ERR_ROUND { get; set; }

        ///// </summary>
        //public string Me_WcLimit = "";
    }
}
