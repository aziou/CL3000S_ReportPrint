using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Interfaces
{
    /// <summary>
    /// 报表接口相关操作
    /// </summary>
    public interface IReportInterface
    {

        /// <summary>
        /// 显示面板
        /// </summary>
        void ShowPanel(IControlPanel panel);

        void PrintRpt(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, int ReportTao, int ReportType, string Jdyj, string zzbz);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="taoxing"></param>
        /// <returns></returns>
        string[] ReportType(int taoxing);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> ReportTaoXing();
    }
}
