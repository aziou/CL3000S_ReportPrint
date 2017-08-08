using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// �������鷽��
    /// </summary>
    [Serializable]
    public class Plan_PowerConsume:Plan_Base
    {
        private List<CLDC_DataCore.Struct.StPowerConsume> _LstPowerConsume;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="vFAName">��������</param>
        public Plan_PowerConsume(int TaiType, string FAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_GONGHAO_FOLDERNAME, TaiType, FAName)
        {
            this.Load();
        }
        ~Plan_PowerConsume()
        {
            _LstPowerConsume = null;
        }
        /// <summary>
        /// ���ع��ķ��������������б�
        /// </summary>
        private void Load()
        {
            _LstPowerConsume = new List<CLDC_DataCore.Struct.StPowerConsume>();
            string _ErrorString = "";
            XmlNode _XmlNode = CLDC_DataCore.DataBase.clsXmlControl.LoadXml(_FAPath, out _ErrorString);
            if (_ErrorString != "")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                CLDC_DataCore.Struct.StPowerConsume _PowerConsume = new CLDC_DataCore.Struct.StPowerConsume();
                _PowerConsume.PowerConsumePrjID = _XmlNode.ChildNodes[_i].Attributes["bChecked"].Value;
                _PowerConsume.PowerConsumePrjName = _XmlNode.ChildNodes[_i].Attributes["ItemName"].Value;
                _PowerConsume.PrjParm = _XmlNode.ChildNodes[_i].Attributes["Para"].Value;

                _LstPowerConsume.Add(_PowerConsume);
            }
        }
        /// <summary>
        /// �洢���ķ���
        /// </summary>
        public void Save()
        {
            if (_LstPowerConsume.Count == 0)
                return;
            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl();
            _XmlNode.appendchild("", "PowerConsume", "Name", Name);
            for (int _i = 0; _i < _LstPowerConsume.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    , "R"
                                    , "bChecked"
                                    , _LstPowerConsume[_i].PowerConsumePrjID
                                    , "ItemName"
                                    , _LstPowerConsume[_i].PowerConsumePrjName
                                    , "Para"
                                    , _LstPowerConsume[_i].PrjParm);
            }
            _XmlNode.SaveXml(_FAPath);
        }

        /// <summary>
        /// ���һ��������Ŀ
        /// </summary>
        /// /// <param name="sYn">�Ƿ�Ҫ��</param>
        /// <param name="sItemName">������Ŀ����</param>
        /// <param name="sPara">����</param>
        /// <returns></returns>
        public bool Add(int Order, string sYn, string sItemName, string sPara)
        {
            CLDC_DataCore.Struct.StPowerConsume _Item = new CLDC_DataCore.Struct.StPowerConsume();
            _Item.PowerConsumePrjID = sYn;
            _Item.PowerConsumePrjName = sItemName;
            _Item.PrjParm = sPara;
            if (_LstPowerConsume.Contains(_Item))
                Move(Order, _Item);
            else
                _LstPowerConsume.Insert(Order, _Item);
            return true;
        }

        /// <summary>
        /// ��ȡ������Ŀ
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StPowerConsume getPowerConsumePrj(int i)
        {
            if (i >= _LstPowerConsume.Count)
                return new CLDC_DataCore.Struct.StPowerConsume();
            return _LstPowerConsume[i];
        }

        /// <summary>
        /// �ƶ�������Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i,CLDC_DataCore.Struct.StPowerConsume Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstPowerConsume.Count ? _LstPowerConsume.Count - 1 : i;
            this.Remove(Item);
            _LstPowerConsume.Insert(i, Item);
            return;
        }

        /// <summary>
        /// �Ƴ�ȫ����Ŀ
        /// </summary>
        public void RemoveAll()
        {
            _LstPowerConsume.Clear();
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstPowerConsume.Count;
            }
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstPowerConsume.Count)
                return;
            _LstPowerConsume.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(CLDC_DataCore.Struct.StPowerConsume Item)
        {
            if (!_LstPowerConsume.Contains(Item))
                return;
            _LstPowerConsume.Remove(Item);
            return;
        }
    }
}
