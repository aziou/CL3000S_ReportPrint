using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// <summary>
    /// ActiveId���ı�
    /// </summary>
    [Serializable ]
    public class UpdateActiveId_Ask:Command_Ask 
    {
        /// <summary>
        /// �µġ��ı��Ժ��ActiveId��ֵ
        /// </summary>
        public int ActiveId;

        public UpdateActiveId_Ask()
        {
            AskMessage = "����ű��";
        }
    }
}
