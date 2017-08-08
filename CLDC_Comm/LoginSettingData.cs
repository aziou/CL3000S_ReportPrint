using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm
{

    /// <summary>
    /// �����½��ǰ���õ�������Ϣ�Լ���¼�������Ϣ
    /// </summary>
    [Serializable()]
    public class LoginSettingData : CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// �����ġ���̬������
        /// �ڵ�½ʱ��ֵ�� ��������ʱȡֵ
        /// </summary>
        public static LoginSettingData LoginSetting = null ;

        /// <summary>
        /// ��ѹ����ֵ
        /// </summary>
        public  float MaxDianLiu = 0F;

        /// <summary>
        /// ��ѹ����ֵ�ַ���
        /// </summary>
        public string  StrMaxDianLiu = "";

        /// <summary>
        /// ��������ֵ
        /// </summary>
        public  float MaxDianYa = 0F;

        /// <summary>
        ///  ��������ֵ�ַ���
        /// </summary>
        public string  StrMaxDianYa = "";

        /// <summary>
        /// �춨Ա
        /// </summary>
        public  string JianDY_Name = string.Empty;

        /// <summary>
        /// �˶�Ա
        /// </summary>
        public  string HeDY_Name = string.Empty;

        /// <summary>
        /// �춨Ա����
        /// </summary>
        public  string JianDY_Pass = string.Empty;

        /// <summary>
        /// �˶�Ա����
        /// </summary>
        public  string HeDY_Pass = string.Empty;

        /// <summary>
        /// �Ƿ�ʹ����ʾģʽ��true��ʾģʽ��false����ģʽ
        /// </summary>
        public  bool IsUseYanShiMode = false;



    }



}
