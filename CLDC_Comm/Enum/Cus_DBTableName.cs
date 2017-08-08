using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 对应所有被检表信息以及结论数据库表名
    /// </summary>
    public enum Cus_DBTableName
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        METER_INFO,

        /// <summary>
        /// 误差结论
        /// </summary>
        METER_ERROR,

        /// <summary>
        /// 多功能结论
        /// </summary>
        METER_COMMUNICATION,

        /// <summary>
        /// 走字结论
        /// </summary>
        METER_ENERGY_TEST_DATA,

        /// <summary>
        /// 潜动起动数据
        /// </summary>
        METER_START_NO_LOAD,

        /// <summary>
        /// 功耗数据
        /// </summary>
        METER_POWER_CONSUM_DATA,

        /// <summary>
        /// 载波数据
        /// </summary>
        METER_CARRIER_WAVE,

        /// <summary>
        /// 红外数据比对试验数据
        /// </summary>
        METER_INFRARED_DATA,

        /// <summary>
        /// 特殊检定数据
        /// </summary>
        METER_SPECIAL_DATA,

        /// <summary>
        /// 费控数据
        /// </summary>
        METER_RATES_CONTROL,

        /// <summary>
        /// 费率时段功能
        /// </summary>
        METER_FUN_RATES_TIME_CONS,

        /// <summary>
        /// 计量功能
        /// </summary>
        METER_FUN_ENERGY_MEASURE,

        /// <summary>
        /// 数据显示功能
        /// </summary>
        METER_FUN_SHOW,

        /// <summary>
        /// 需量功能
        /// </summary>
        METER_FUN_MAX_DEMAND,

        /// <summary>
        /// 事件记录
        /// </summary>
        METER_FUN_EVENT_RECORD,

        /// <summary>
        /// 规约(协议)一致性数据
        /// </summary>
        METER_STANDARD_DLT_DATA,

        /// <summary>
        /// 一致性试验数据
        /// </summary>
        METER_CONSISTENCY_DATA,

        /// <summary>
        /// 结论表
        /// </summary>
        METER_RESULTS,

        /// <summary>
        /// 冻结试验
        /// </summary>
        METER_FREEZE,

        /// <summary>
        /// 交流电压试验（兼耐压台）
        /// </summary>
        METER_HIGH_VOLTAGE,

        /// <summary>
        /// 计时功能
        /// </summary>
        METER_FUN_TIME_KEEPING,

        /// <summary>
        /// 负荷记录
        /// </summary>
        METER_FUN_LOAD_RECORD
    }
}
