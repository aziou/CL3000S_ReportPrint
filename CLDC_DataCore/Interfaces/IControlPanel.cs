using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Interfaces
{
    /// <summary>
    /// 控制面板
    /// </summary>
    public interface IControlPanel:IServiceProvider
    {
        /// <summary>
        /// 获取负载控制面板的表单
        /// </summary>
        System.Windows.Forms.TabPage[] tabPages(Dictionary<string, string> tabs);

        /// <summary>
        /// 配置保存事件
        /// </summary>
        event EventHandler Save;

    }
}
