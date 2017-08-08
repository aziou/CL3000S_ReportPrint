using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Struct;
using CLDC_Comm.Enum;
using CLDC_DataCore.DataBase;
using System.Xml;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// Ǳ������
    /// </summary>
    [Serializable()]
    public class Plan_QianDong : Plan_Base
    {

        /// <summary>
        /// �ռ�ʱ����
        /// </summary>
        public string DayCheckTimesSetting;
        /// <summary>
        /// Ǳ����Ŀ�б�
        /// </summary>
        private List<StPlan_QianDong> _LstQianDong;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="vFAName">��������</param>
        public Plan_QianDong(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_QIAND_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_QianDong()
        {
            _LstQianDong = null;
        }
        /// <summary>
        /// ����Ǳ������
        /// </summary>
        private void Load()
        {
            _LstQianDong = new List<StPlan_QianDong>();
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out  _ErrorString);
            if (_XmlNode == null)
            {
                DayCheckTimesSetting = "0|1|5|60";
                return;
            }
            if (_XmlNode.ChildNodes.Count < 1)
            {
                DayCheckTimesSetting = "0|1|5|60";
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count - 1; _i++)
            {
                StPlan_QianDong _Qiandong = new StPlan_QianDong();
                _Qiandong.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_XmlNode.ChildNodes[_i].Attributes["GLFX"].Value);
                _Qiandong.FloatxU = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xU"].Value);
                _Qiandong.FloatxIb = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xQIb"].Value);
                _Qiandong.xTime = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xTime"].Value);
                _Qiandong.DefaultValue = int.Parse(_XmlNode.ChildNodes[_i].Attributes["Default"].Value);
                if (_XmlNode.ChildNodes.Count > 0)
                {
                    try
                    {
                        if (_XmlNode.ChildNodes[_XmlNode.ChildNodes.Count - 1].OuterXml.Contains("PrjParameter"))
                        {
                            _Qiandong.DayCheckTimesSetting = _XmlNode.ChildNodes[_XmlNode.ChildNodes.Count - 1].Attributes["PrjParameter"].Value;
                            DayCheckTimesSetting = _Qiandong.DayCheckTimesSetting;
                        }
                        else
                        {
                            _Qiandong.DayCheckTimesSetting = "0|1|5|60";
                            DayCheckTimesSetting = _Qiandong.DayCheckTimesSetting;

                        }

                    }
                    catch (Exception ex)
                    {
                        CLDC_DataCore.Function.ErrorLog.Write(ex);
                        _Qiandong.DayCheckTimesSetting = "0|1|5|60";
                        DayCheckTimesSetting = _Qiandong.DayCheckTimesSetting;
                    }


                    _LstQianDong.Add(_Qiandong);
                }
            }
            if (_LstQianDong.Count > 1)
            {
                DayCheckTimesSetting = _LstQianDong[_LstQianDong.Count - 1].DayCheckTimesSetting;
            }
            else
            {
                DayCheckTimesSetting = "0|1|5|60";
            }
            return;
        }
        /// <summary>
        /// �洢Ǳ������
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "QianDong", "Name", Name);
            if (_LstQianDong.Count == 0)
            {
                _XmlNode.SaveXml(_FAPath);
                return;
            }
            for (int _i = 0; _i < _LstQianDong.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    , "R"
                                    , "GLFX"
                                    , ((int)_LstQianDong[_i].PowerFangXiang).ToString()
                                    , "xU"
                                    , _LstQianDong[_i].FloatxU.ToString()
                                    , "xQIb"
                                    , _LstQianDong[_i].FloatxIb.ToString()
                                    , "xTime"
                                    , _LstQianDong[_i].xTime.ToString()
                                    , "Default"
                                    , _LstQianDong[_i].DefaultValue.ToString());
            }

            #region----Ǳ���ռ�ʱ----

            _XmlNode.appendchild(""
                                , "R"
                                , "PrjID"
                                , "002"
                                , "PrjName"
                                , "�ռ�ʱ���"
                                , "PrjOutPut"
                                , "1|1|1|0Ib|1.0"
                                , "PrjParameter"
                                , DayCheckTimesSetting
                                , "Default"
                                , "0|1|5|60");
            #endregion
            _XmlNode.SaveXml(_FAPath);
            return;
        }


        /// <summary>
        /// ����һ��Ǳ����Ŀ
        /// </summary>
        /// <param name="Glfx">���ʷ���</param>
        /// <param name="xU">��ѹ���������֣�</param> 
        /// <param name="xIb">��������(����)</param>
        /// <param name="xTime">ʱ�䱶�������ٱ���ʱ�䣩��ʱ���Ǹ��ݹ�̼���</param>
        /// <param name="Default">�Ƿ�Ĭ�Ϻϸ�0-��Ĭ�ϣ�1-Ĭ�ϣ�Ĭ�Ϻϸ�ʱ����Ŀ�򲻼춨</param>
        /// <returns></returns>
        public bool Add(Cus_PowerFangXiang Glfx, float xU, float xIb, float xTime, int DefaultValue)
        {
            StPlan_QianDong _QianDong = new StPlan_QianDong();
            _QianDong.PowerFangXiang = Glfx;
            _QianDong.FloatxU = xU;
            _QianDong.FloatxIb = xIb;
            _QianDong.xTime = xTime;
            _QianDong.DefaultValue = DefaultValue;
            if (_LstQianDong.Contains(_QianDong))
                return false;
            _LstQianDong.Add(_QianDong);
            return true;
        }

        /// <summary>
        /// �Ƴ����з�����Ŀ
        /// </summary>
        public void RemoveAll()
        {
            _LstQianDong.Clear();
        }

        /// <summary>
        /// ���ط����а�������Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstQianDong.Count;
            }
        }
        /// <summary>
        /// �����б�����ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_QianDong getQianDongPrj(int i)
        {
            if (i >= _LstQianDong.Count)
                return new StPlan_QianDong();
            return _LstQianDong[i];
        }

        /// <summary>
        /// �ƶ�Ǳ����Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, StPlan_QianDong Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstQianDong.Count ? _LstQianDong.Count - 1 : i;
            this.Remove(Item);
            _LstQianDong.Insert(i, Item);
            return;
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstQianDong.Count)
                return;
            _LstQianDong.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_QianDong Item)
        {
            if (!_LstQianDong.Contains(Item))
                return;
            _LstQianDong.Remove(Item);
            return;
        }
    }
}
