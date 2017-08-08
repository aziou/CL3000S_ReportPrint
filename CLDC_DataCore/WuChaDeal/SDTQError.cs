using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
namespace CLDC_DataCore.WuChaDeal
    {
     class SDTQError:WuChaBase
        {

         public SDTQError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
             : base(wuChaDeal)
         { 
            
         }

        /// <summary>
        /// ����ʱ��Ͷ�����
        /// </summary>
        /// <param name="t1">����ʱ��</param>
        /// <param name="t2">ʵ��ʱ��</param>
        /// <returns>����</returns>
        public CLDC_DataCore.Struct.StWuChaResult getWuCha(DateTime t1, DateTime t2)
            {
           
            CLDC_DataCore.Struct.StWuChaResult stResult = new CLDC_DataCore.Struct.StWuChaResult();
            int intPastSecond = CLDC_DataCore.Function.DateTimes.DateDiff(t1, t2);
            stResult.Result =CLDC_DataCore.Function.Common.ConverResult( intPastSecond <= 300 ? true : false);
            stResult.Data = String.Format("{0}|{1}|{2}", t1.TimeOfDay.ToString(), t2.TimeOfDay.ToString(), intPastSecond.ToString());
            return stResult;
            }
        }
    }
