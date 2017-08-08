using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Interfaces
{
    public delegate Model.DnbModel.DnbInfo.MeterBasicInfo GetMeterInfo(string barcode);
    public interface IMeterInfoUpdateDownEnablecs
    {

        /// <summary>
        /// 从营销下载电能表信息
        /// </summary>
         event GetMeterInfo OnGetMeterInfo;
    }
}
