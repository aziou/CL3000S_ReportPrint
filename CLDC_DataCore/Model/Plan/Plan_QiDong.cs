using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_Comm;
using CLDC_DataCore.DataBase;
using CLDC_Comm.Enum;
using CLDC_DataCore.Struct;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// �𶯷���
    /// </summary>
    [Serializable()]
    public class Plan_QiDong:Plan_Base
    {

        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        private List<StPlan_QiDong> _LstQiDong;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������</param>
        /// <param name="FAName">��������</param>
        public Plan_QiDong(int TaiType, string FAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_QID_FOLDERNAME , TaiType, FAName)
        {
            this.Load();
        }
        ~Plan_QiDong()
        {
            _LstQiDong = null;
        }
        /// <summary>
        /// ������������
        /// </summary>
        private void Load()
        {
            _LstQiDong = new List<StPlan_QiDong>();
            string _ErrorString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath,out  _ErrorString);
            if(_XmlNode==null)
                return;
            for(int _i=0;_i<_XmlNode.ChildNodes.Count;_i++)
            {
                StPlan_QiDong _Qidong = new StPlan_QiDong();
                _Qidong.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_XmlNode.ChildNodes[_i].Attributes["GLFX"].Value);
                _Qidong.FloatxIb = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xIb"].Value);
                _Qidong.xTime = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xTime"].Value);
                _Qidong.DefaultValue = int.Parse(_XmlNode.ChildNodes[_i].Attributes["Default"].Value);
                _LstQiDong.Add(_Qidong);
            }
            return;
        }
        /// <summary>
        /// �洢��������
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "QiDong", "Name", Name);
            if (_LstQiDong.Count == 0)
            {
                _XmlNode.SaveXml(_FAPath);
                return;
            }
            for (int _i = 0; _i < _LstQiDong.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    , "R"
                                    , "GLFX"
                                    , ((int)_LstQiDong[_i].PowerFangXiang).ToString()
                                    , "xIb"
                                    , _LstQiDong[_i].FloatxIb.ToString()
                                    , "xTime"
                                    , _LstQiDong[_i].xTime.ToString()
                                    , "Default"
                                    , _LstQiDong[_i].DefaultValue.ToString());
            }
            _XmlNode.SaveXml(_FAPath);
            return;
        }
        /// <summary>
        /// ����һ��������Ŀ
        /// </summary>
        /// <param name="Glfx">���ʷ���</param>
        /// <param name="xIb">��������(����)</param>
        /// <param name="xTime">ʱ�䱶�������ٱ���ʱ�䣩��ʱ���Ǹ��ݹ�̼���</param>
        /// <param name="DefaultValue">�Ƿ�Ĭ�Ϻϸ�0-��Ĭ�ϣ�1-Ĭ�ϣ�Ĭ�Ϻϸ�ʱ����Ŀ�򲻼춨</param>
        /// <returns></returns>
        public bool Add(Cus_PowerFangXiang Glfx, float xIb, float xTime, int DefaultValue)
        {
            StPlan_QiDong _QiDong = new StPlan_QiDong();
            _QiDong.PowerFangXiang = Glfx;
            _QiDong.FloatxIb = xIb;
            _QiDong.xTime = xTime;
            _QiDong.DefaultValue=DefaultValue;
            if (_LstQiDong.Contains(_QiDong))
                return false;
            _LstQiDong.Add(_QiDong);
            return true;
        }
        /// <summary>
        /// �Ƴ�������Ŀ
        /// </summary>
        public void RemoveAll()
        {
            _LstQiDong.Clear();
        }


        /// <summary>
        /// ���ط����а�������Ŀ����
        /// </summary>
        public int Count
        {
            get 
            {
                return _LstQiDong.Count;
            }
        }
        /// <summary>
        /// �����б�����ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_QiDong getQiDongPrj(int i)
        {
            if (i >= _LstQiDong.Count)
                return new StPlan_QiDong();
            return _LstQiDong[i];
        }

        /// <summary>
        /// �ƶ�������Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, StPlan_QiDong Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstQiDong.Count ? _LstQiDong.Count - 1 : i;
            this.Remove(Item);
            _LstQiDong.Insert(i, Item);
            return;
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstQiDong.Count)
                return;
            _LstQiDong.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_QiDong Item)
        {
            if (!_LstQiDong.Contains(Item))
                return;
            _LstQiDong.Remove(Item);
            return;
        }

    }
}
