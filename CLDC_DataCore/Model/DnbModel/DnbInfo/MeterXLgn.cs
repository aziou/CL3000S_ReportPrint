using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 需量功能
    /// </summary>
    [Serializable()]
    public class MeterXLgn : MeterErrorBase
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
        public string AVR_GRP_TYPE = "";
        /// <summary>
        /// 项目号
        /// </summary>
        public string AVR_LIST_NO = "";
        /// <summary>
        /// 小项目号
        /// </summary>
        public string AVR_ITEM_TYPE = "";
        /// <summary>
        /// 分项结论		
        /// </summary>
        public string AVR_ITEM_CONC = "";
        /// <summary>
        /// 方向和角度
        /// </summary>
        public string AVR_GK = "";
        /// <summary>
        /// 清零方法
        /// </summary>
        public string AVR_CLEAR_DEMAND_TYPE = "";
        /// <summary>
        /// 编程键		
        /// </summary>
        public string AVR_PRAGRAMMING_FLAG = "";
        /// <summary>
        /// 不到一个需量周期是否有数据
        /// </summary>
        public string AVR_LESS_PERIOD_DATA = "";
        /// <summary>
        /// 打开编程键影响结果
        /// </summary>
        public string AVR_PRAGRAMMING_INFLUENCE = "";
        /// <summary>
        /// 转存后数据是否正确		
        /// </summary>
        public string AVR_FREEZE_DATA = "";
        /// <summary>
        /// 清零后事件记录是否正确结论,冻结时间,记录发送时间
        /// </summary>
        public string AVR_CLEAR_RECORD = "";
        /// <summary>
        /// 当前需量记录是否正确（有数据，07规约有时标）
        /// </summary>
        public string AVR_DEMAND_CONC = "";
        /// <summary>
        /// 上一次结算日P+:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_LAST_ACTIVE_FORWARD = "";
        /// <summary>
        /// 上一次结算日P-:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_LAST_ACTIVE_REVERSE = "";
        /// <summary>
        /// 上一次结算日组合无功1:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_LAST_R_GROUP_1 = "";
        /// <summary>
        /// 上一次结算日组合无功2:总，需量时标，1,2,3,4,A,B,C		
        /// </summary>
        public string AVR_LAST_R_GROUP_2 = "";
        /// <summary>
        /// 上一次结算日第一象限无功需量:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_LAST_R_QUADRANT_1 = "";
        /// <summary>
        /// 上一次结算日第二象限无功需量:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_LAST_R_QUADRANT_2 = "";

        /// <summary>
        /// 上一次结算日第三象限无功需量:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_LAST_R_QUADRANT_3 = "";
        /// <summary>
        /// 上一次结算日第四象限无功需量:总，需量时标，1,2,3,4,A,B,C		
        /// </summary>
        public string AVR_LAST_R_QUADRANT_4 = "";
        /// <summary>
        /// P+:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_ACTIVE_FORWARD = "";
        /// <summary>
        /// P-:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_ACTIVE_REVERSE = "";
        /// <summary>
        /// 组合无功1:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_R_GROUP_1 = "";
        /// <summary>
        /// 组合无功2:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_R_GROUP_2 = "";
        /// <summary>
        /// 第一象限无功需量:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_R_QUADRANT_1 = "";
        /// <summary>
        /// 第二象限无功需量:总，需量时标，1,2,3,4,A,B,C		
        /// </summary>
        public string AVR_R_QUADRANT_2 = "";
        /// <summary>
        /// 第三象限无功需量:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_R_QUADRANT_3 = "";
        /// <summary>
        /// 第四象限无功需量:总，需量时标，1,2,3,4,A,B,C
        /// </summary>
        public string AVR_R_QUADRANT_4 = "";
        /// <summary>
        /// 备份1
        /// </summary>
        public string AVR_OTHER_1 = "";
        /// <summary>
        /// 备份2
        /// </summary>
        public string AVR_OTHER_2 = "";
        /// <summary>
        /// 备份3
        /// </summary>
        public string AVR_OTHER_3 = "";
        /// <summary>
        /// 备份4
        /// </summary>
        public string AVR_OTHER_4 = "";
        /// <summary>
        /// 备份5
        /// </summary>
        public string AVR_OTHER_5 = "";
        /// <summary>
        /// 其他数据
        /// </summary>
        public string AVR_RECORD_OTHER = "";
    }
}
