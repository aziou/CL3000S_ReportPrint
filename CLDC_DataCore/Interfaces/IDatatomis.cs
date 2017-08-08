using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Interfaces
{
    public interface IDatatomis
    {
        /// <summary>
        /// 显示面板
        /// </summary>
        void ShowPanel(CLDC_DataCore.Interfaces.IControlPanel panel);

        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="Item">集合对象</param>
        /// <returns></returns>
        bool UpdateData(Model.DnbModel.DnbInfo.MeterBasicInfo Item, string FileName);

        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="Items">数据对象集合</param>
        /// <returns></returns>
        bool UpdateData(List<Model.DnbModel.DnbInfo.MeterBasicInfo> Items, string FileName);

        /// <summary>
        /// 数据下载
        /// </summary>
        /// <param name="barCode">条码号</param>
        /// <param name="Item">被检表对象</param>
        /// <returns></returns>
        bool DownData(string barCode, ref Model.DnbModel.DnbInfo.MeterBasicInfo Item);

        /// <summary>
        /// 数据下载,从服务器
        /// </summary>
        /// <param name="barCode">条码号</param>
        /// <param name="Item">被检表对象</param>
        /// <returns></returns>
        bool DownDataFromSqlServer(string barCode, ref Model.DnbModel.DnbInfo.MeterBasicInfo Item);
    }
}
