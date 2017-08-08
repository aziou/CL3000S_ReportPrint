using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 计量功能
    /// </summary>
    [Serializable()]
    public class MeterJLgn : MeterErrorBase
    {
        /// <summary>
        /// 外键编号
        /// </summary>
        public string FK_LNG_METER_ID = "";
        /// <summary>
        /// 台体编号
        /// </summary>
        public string AVR_DEVICE_ID = "";
        /// <summary>
        /// 表位号
        /// </summary>
        public long LNG_BENCH_POINT_NO = 0;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string AVR_PROJECT_NAME = "";
        /// <summary>
        /// 组别	
        /// </summary>
        public string AVR_GROUP_TYPE = "";
        /// <summary>
        /// 组中的项目号
        /// </summary>
        public string AVR_LIST_NO = "";
        /// <summary>
        /// 项目结论
        /// </summary>
        public string AVR_ITEM_CONC = "";
        /// <summary>
        /// 标准有功组合状态字
        /// </summary>
        public string AVR_ACTIVE_STATE_STD = "";
        /// <summary>
        /// 表内有功组合状态字
        /// </summary>
        public string AVR_ACTIVE_STATE = "";
        /// <summary>
        /// 组合有功类电能  总|尖|峰|平|谷，以“|”分隔
        /// </summary>
        public string AVR_ACTIVE_GROUP_DATA = "";
        /// <summary>
        /// 正向有功类电能，总|尖|峰|平|谷，以“|”分隔	
        /// </summary>
        public string AVR_ACTIVE_FORWARD_DATA = "";
        /// <summary>
        /// 反向有功类电能，总|尖|峰|平|谷，以“|”分隔
        /// </summary>
        public string AVR_ACTIVE_REVERSE_DATA = "";
        /// <summary>
        /// 电能与有功组合方式状态字吻合结论Y/N
        /// </summary>
        public string AVR_ACTIVE_STATE_CONC = "";
        /// <summary>
        /// 标准无功组合方式1状态字
        /// </summary>
        public string AVR_R_STATE_1_STD = "";
        /// <summary>
        /// 表内无功组合方式1状态字
        /// </summary>
        public string AVR_R_STATE_1 = "";
        /// <summary>
        /// 标准无功组合方式2状态字
        /// </summary>
        public string AVR_R_STATE_2_STD = "";
        /// <summary>
        /// 表内无功组合方式2状态字	
        /// </summary>
        public string AVR_R_STATE_2 = "";
        /// <summary>
        /// 组合无功1类电能  总|尖|峰|平|谷，以“|”分隔
        /// </summary>
        public string AVR_R_STATE_1_DATA = "";
        /// <summary>
        /// 组合无功2类电能 总|尖|峰|平|谷，以“|”分隔
        /// </summary>
        public string AVR_R_STATE_2_DATA = "";
        /// <summary>
        /// 第一象限无功电能 总|尖|峰|平|谷，以“|”分隔	 
        /// </summary>
        public string AVR_R_QUADRANT_1_DATA = "";
        /// <summary>
        /// 第一象限无功电能 总|尖|峰|平|谷，以“|”分隔
        /// </summary>
        public string AVR_R_QUADRANT_2_DATA = "";
        /// <summary>
        /// 第一象限无功电能 总|尖|峰|平|谷，以“|”分隔
        /// </summary>
        public string AVR_R_QUADRANT_3_DATA = "";
        /// <summary>
        /// 第一象限无功电能 总|尖|峰|平|谷，以“|”分隔
        /// </summary>
        public string AVR_R_QUADRANT_4_DATA = "";
        /// <summary>
        /// 电能与无功组合方式1吻合结论Y/N
        /// </summary>
        public string AVR_R_STATE_1_CONC = "";
        /// <summary>
        /// 电能与无功组合方式2吻合结论Y/N
        /// </summary>
        public string AVR_R_STATE_2_CONC = "";
        /// <summary>
        /// A相电能  正向有功电能|反向有功电能|组合无功1电能|组合无功2电能|第一象限无功电能|第二象限无功电能|第三象限无功电能|第四象限无功电能	
        /// </summary>
        public string AVR_A_DATA = "";
        /// <summary>
        /// B相电能  正向有功电能|反向有功电能|组合无功1电能|组合无功2电能|第一象限无功电能|第二象限无功电能|第三象限无功电能|第四象限无功电能
        /// </summary>
        public string AVR_B_DATA = "";
        /// <summary>
        /// C相电能  正向有功电能|反向有功电能|组合无功1电能|组合无功2电能|第一象限无功电能|第二象限无功电能|第三象限无功电能|第四象限无功电能
        /// </summary>
        public string AVR_C_DATA = "";
        /// <summary>
        /// 分相计量结论Y/N  总电量不应为各分相电量算术加	
        /// </summary>
        public string AVR_SUM_ABC_CONC = "";
        /// <summary>
        /// 分时计量结论Y/N  总电量不应为各费率电量算术加
        /// </summary>
        public string AVR_SUM_RATES_CONC = "";
        /// <summary>
        /// 其他数据
        /// </summary>
        public string AVR_RECORD_OTHER = "";
    }
}
