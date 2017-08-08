using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;
using System.Xml;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// Ԥ�ȷ���
    /// </summary>
    [Serializable()]
    public class Plan_YuRe:Plan_Base 
    {
        private List<StPlan_YuRe> _LstYuRe;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="vFAName">��������</param>
        public Plan_YuRe(int TaiType, string FAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_YURE_FOLDERNAME , TaiType, FAName)
        {
            this.Load();
        }
        ~Plan_YuRe()
        {
            _LstYuRe = null;
        }
        /// <summary>
        /// ����Ԥ�ȷ�����Ԥ�������б�
        /// </summary>
        private void Load()
        {
            _LstYuRe = new List<StPlan_YuRe>();
            string _ErrorString="";
            XmlNode _XmlNode=clsXmlControl.LoadXml(_FAPath,out _ErrorString);
            if (_ErrorString!="")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StPlan_YuRe _Yure = new StPlan_YuRe();
                if (_XmlNode.ChildNodes[_i].Attributes["GLFX"] == null)
                    _Yure.PowerFangXiang = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;
                else
                    _Yure.PowerFangXiang=(CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_XmlNode.ChildNodes[_i].Attributes["GLFX"].Value);
                if (_XmlNode.ChildNodes[_i].Attributes["xIb"] ==null)
                    _Yure.xIb = "1";
                else
                    _Yure.xIb = _XmlNode.ChildNodes[_i].Attributes["xIb"].Value;
                if (_XmlNode.ChildNodes[_i].Attributes["Time"] == null)
                    _Yure.Times = 1;
                else
                    _Yure.Times = float.Parse(_XmlNode.ChildNodes[_i].Attributes["Time"].Value);
                _LstYuRe.Add(_Yure);
            }
        }
        /// <summary>
        /// �洢Ԥ�ȷ���
        /// </summary>
        public void Save()
        {
            if (_LstYuRe.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "YuRe", "Name", Name);
            for(int _i=0;_i<_LstYuRe.Count;_i++)
            {
                _XmlNode.appendchild(""
                                    ,"R"
                                    ,"GLFX"
                                    ,((int)_LstYuRe[_i].PowerFangXiang).ToString()
                                    ,"xIb"
                                    ,_LstYuRe[_i].xIb
                                    ,"Time"
                                    ,_LstYuRe[_i].Times.ToString());
            }
            _XmlNode.SaveXml(_FAPath);
        }
        /// <summary>
        /// ���һ��Ԥ����Ŀ
        /// </summary>
        /// <param name="Glfx">���ʷ���</param>
        /// <param name="xIb">��������Imax</param>
        /// <param name="Time">ʱ�䣨���ӣ�</param>
        /// <returns></returns>
        public bool Add(int Order,CLDC_Comm.Enum.Cus_PowerFangXiang Glfx, string xIb, float Time)
        {
            StPlan_YuRe _Item = new StPlan_YuRe();
            _Item.PowerFangXiang = Glfx;
            _Item.xIb = xIb;
            _Item.Times = Time;
            if (_LstYuRe.Contains(_Item))
                Move(Order, _Item);
            else
                _LstYuRe.Insert(Order, _Item);
            return true;
        }

        /// <summary>
        /// ��ȡԤ����Ŀ
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_YuRe getYuRePrj(int i)
        {
            if (i >= _LstYuRe.Count)
                return new StPlan_YuRe();
            return _LstYuRe[i];
        }


        /// <summary>
        /// �ƶ�Ԥ����Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i,StPlan_YuRe Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstYuRe.Count ? _LstYuRe.Count - 1 : i;
            this.Remove(Item);
            _LstYuRe.Insert(i, Item);
            return;
        }
        /// <summary>
        /// �Ƴ�ȫ����Ŀ
        /// </summary>
        public void RemoveAll()
        {
            _LstYuRe.Clear();
        }

        /// <summary>
        /// Ԥ����Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstYuRe.Count;
            }
        }
        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstYuRe.Count)
                return;
            _LstYuRe.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_YuRe Item)
        {
            if (!_LstYuRe.Contains(Item))
                return;
            _LstYuRe.Remove(Item);
            return;
        }

    }
}
