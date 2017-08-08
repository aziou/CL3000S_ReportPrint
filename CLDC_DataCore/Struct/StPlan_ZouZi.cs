using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// ���ַ�����Ŀ
    /// </summary>
    [Serializable()]
    public struct StPlan_ZouZi
    {

        private string _PrjID;
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        public string PrjID
        {
            get
            {
                return _PrjID;
            }
            set
            {
                _PrjID = value;
                this.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_PrjID.Substring(0, 1));
                this.PowerYj = (CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(_PrjID.Substring(1, 1));
                //this.FeiLv = (Comm.Enum.Cus_FeiLv)int.Parse(_PrjID.Substring(6, 1));
            }
        }

        /// <summary>
        /// ���ʷ���
        /// </summary>
        public Cus_PowerFangXiang PowerFangXiang;

        /// <summary>
        /// Ԫ��
        /// </summary>
        public Cus_PowerYuanJian PowerYj;
        /// <summary>
        /// �춨����
        /// </summary>
        public Cus_ZouZiMethod ZouZiMethod;

        /// <summary>
        /// ��������(Imax)
        /// </summary>
        public string xIb;
        /// <summary>
        /// ��ȡת����ĵ�������
        /// </summary>
        public string GetxIb
        {
            get
            {
                string xib = "00";
                switch (xIb.ToUpper())
                {
                    case "IMAX":
                        xib = "01";
                        break;
                    case "0.5IMAX":
                        xib = "02";
                        break;
                    case "3.0IB":
                        xib = "03";
                        break;
                    case "2.0IB":
                        xib = "04";
                        break;
                    case "1.5IB":
                        xib = "05";
                        break;
                    case "0.5(IMAX-IB)":
                        xib = "06";
                        break;
                    case "1.0IB":
                        xib = "07";
                        break;
                    case "0.8IB":
                        xib = "08";
                        break;
                    case "0.5IB":
                        xib = "09";
                        break;
                    case "0.2IB":
                        xib = "10";
                        break;
                    case "0.1IB":
                        xib = "11";
                        break;
                    case "0.05IB":
                        xib = "12";
                        break;
                    case "0.02IB":
                        xib = "13";
                        break;
                    case "0.01IB":
                        xib = "14";
                        break;
                    default:
                        break;

                }
                return xib;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string Glys;
        /// <summary>
        /// ��ȡת����Ĺ�������
        /// </summary>
        public string GetGlys
        {
            get
            {
                string glys = "00";
                switch (Glys.TrimStart('-').ToUpper())
                {
                    case "1.0":
                        glys = "01";
                        break;
                    case "0.5L":
                        glys = "02";
                        break;
                    case "0.8C":
                        glys = "03";
                        break;
                    case "0.5C":
                        glys = "04";
                        break;
                    case "0.8L":
                        glys = "05";
                        break;
                    case "0.25L":
                        glys = "06";
                        break;
                    case "0.25C":
                        glys = "07";
                        break;
                    default:
                        break;

                }
                return glys;
            }
        }
        /// <summary>
        /// ��Ŀ���ʣ�ֻ����CreateFA�����Ч
        /// </summary>
        public CLDC_Comm.Enum.Cus_FeiLv FeiLv;
        /// <summary>
        /// ��ȡitemkeyֵ
        /// </summary>
        public string itemKey
        {
            get 
            {
                return string.Format("{0}{1}{2}{3}{4}", (int)PowerFangXiang, (int)PowerYj, GetGlys, GetxIb, (int)FeiLv);
            }
        }
        /// <summary>
        /// ��ʼʱ�䣬ֻ����CreateFA�����Ч
        /// </summary>
        public string StartTime;

        /// <summary>
        /// ����ʱ�䣬ֻ����CreateFA�����Ч
        /// </summary>
        public float UseMinutes;

        /// <summary>
        /// ����
        /// </summary>
        public string FeiLvString;
        /// <summary>
        /// �Ƿ������������Ϊ0����Ϊ1
        /// </summary>
        public string ZuHeWc;
        /// <summary>
        /// ������Ŀ
        /// </summary>
        public List<StPrjFellv> ZouZiPrj;
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return string.Format("{0}����:����{1}", PowerFangXiang.ToString(), FeiLv.ToString());
            }
            catch
            {
                return string.Format("{0}����", PowerFangXiang.ToString());
            }
        }

        /// <summary>
        /// ���¸��ݷ�������ʵ�ʵ����鷽��
        /// </summary>
        /// <param name="i">���ָ���Ŀ�е�ZouZiPrj�б��Ԫ������</param>
        /// <returns></returns>
        public StPlan_ZouZi getNewPlan(int i)
        {
            StPlan_ZouZi _Tmp = new StPlan_ZouZi();
            _Tmp.PrjID = PrjID + ((int)ZouZiPrj[i].FeiLv).ToString();
            _Tmp.PowerFangXiang = PowerFangXiang;
            _Tmp.PowerYj = PowerYj;
            _Tmp.Glys = Glys;
            _Tmp.xIb = xIb;
            _Tmp.ZuHeWc = ZuHeWc;
            _Tmp.ZouZiMethod = ZouZiMethod;
            _Tmp.FeiLv = ZouZiPrj[i].FeiLv;
            _Tmp.StartTime = ZouZiPrj[i].StartTime;
            _Tmp.UseMinutes = float.Parse(ZouZiPrj[i].ZouZiTime);
            return _Tmp;
        }


        /// <summary>
        /// ���ַ���
        /// </summary>
        public struct StPrjFellv
        {
            /// <summary>
            /// ���ַ���
            /// </summary>
            public CLDC_Comm.Enum.Cus_FeiLv FeiLv;
            /// <summary>
            /// ������ʼʱ��
            /// </summary>
            public string StartTime;
            /// <summary>
            /// ����ʱ��
            /// </summary>
            public string ZouZiTime;
        
        }

    }
}
