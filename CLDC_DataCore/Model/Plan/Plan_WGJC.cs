using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;
using System.Xml;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// ��ۼ�����鷽��
    /// </summary>
    [Serializable()]
    public class Plan_WGJC:Plan_Base 
    {
        private List<StPlan_WGJC> _LstWGJC;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="vFAName">��������</param>
        public Plan_WGJC(int TaiType, string FAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_WGJC_FOLDERNAME , TaiType, FAName)
        {
            this.Load();
        }
        ~Plan_WGJC()
        {
            _LstWGJC = null;
        }
        /// <summary>
        /// ������ۼ�鷽����Ԥ�������б�
        /// </summary>
        private void Load()
        {
            _LstWGJC = new List<StPlan_WGJC>();
            string _ErrorString="";
            XmlNode _XmlNode=clsXmlControl.LoadXml(_FAPath,out _ErrorString);
            if (_ErrorString!="")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StPlan_WGJC _WGJC = new StPlan_WGJC();
                _WGJC.WGJCPrjID = (_i + 1).ToString("D3");
                _LstWGJC.Add(_WGJC);
            }
        }
        /// <summary>
        /// �洢��ۼ�鷽��
        /// </summary>
        public void Save()
        {
            if (_LstWGJC.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "WGJC", "Name", Name);
            for (int _i = 0; _i < _LstWGJC.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    ,"R"
                                    , "GLFX"
                                    , _LstWGJC[_i].WGJCPrjID
                                    );
            }
            _XmlNode.SaveXml(_FAPath);
        }
        /// <summary>
        /// ���һ����ۼ����Ŀ
        /// </summary>        
        /// <returns></returns>
        public bool Add(int Order)
        {
            StPlan_WGJC _Item = new StPlan_WGJC();

            _Item.WGJCPrjID = (Order + 1).ToString("D3");
            if (_LstWGJC.Contains(_Item))
                Move(Order, _Item);
            else
                _LstWGJC.Insert(Order, _Item);
            return true;
        }

        /// <summary>
        /// ��ȡ��ۼ����Ŀ
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_WGJC getWGJCPrj(int i)
        {
            if (i >= _LstWGJC.Count)
                return new StPlan_WGJC();
            return _LstWGJC[i];
        }


        /// <summary>
        /// �ƶ���ۼ����Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, StPlan_WGJC Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstWGJC.Count ? _LstWGJC.Count - 1 : i;
            this.Remove(Item);
            _LstWGJC.Insert(i, Item);
            return;
        }
        /// <summary>
        /// �Ƴ�ȫ����Ŀ
        /// </summary>
        public void RemoveAll()
        {
            _LstWGJC.Clear();
        }

        /// <summary>
        /// ��ۼ����Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstWGJC.Count;
            }
        }
        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstWGJC.Count)
                return;
            _LstWGJC.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_WGJC Item)
        {
            if (!_LstWGJC.Contains(Item))
                return;
            _LstWGJC.Remove(Item);
            return;
        }

    }
}
