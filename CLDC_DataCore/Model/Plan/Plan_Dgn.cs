using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_Comm.Enum;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Struct;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// �๦�ܷ���
    /// </summary>
    [Serializable()]
    public class Plan_Dgn:Plan_Base 
    {
        /// <summary>
        /// �๦����Ŀ�б�
        /// </summary>
        private List<CLDC_DataCore.Struct.StPlan_Dgn> _LstDgn;

        public Plan_Dgn(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_DGN_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_Dgn()
        {
            _LstDgn = null;
        }
        /// <summary>
        /// ���ض๦�ܷ���
        /// </summary>
        private void Load()
        { 
            _LstDgn=new List<StPlan_Dgn>();
            string _ErrString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrString);
            if (_ErrString != "")
            {
                return;
            }
            int intPrjID = 0;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StPlan_Dgn _Item = new StPlan_Dgn();
                _Item.DgnPrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;
                _Item.DgnPrjName = _XmlNode.ChildNodes[_i].Attributes["PrjName"].Value;
                _Item.OutPramerter = new StPowerPramerter(); 
                _Item.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes["PrjOutPut"].Value);
                _Item.PrjParm=_XmlNode.ChildNodes[_i].Attributes["PrjParameter"].Value;

                _LstDgn.Add(_Item);

                #region //�ر���ʱ��Ͷ�У��ĸ�����Ϊ�˸Ķ��ٵ�ҪչʾΪ2���ڵ�
                intPrjID = int.Parse(_Item.DgnPrjID);
                if (intPrjID == (int)Cus_DgnItem.ʱ��Ͷ��)
                {
                    string[] _PrjParm = _Item.PrjParm.Split('|');
                    
                    if (_PrjParm != null && _PrjParm.Length > 1)
                    {
                        int _PrjParmLength = _PrjParm.Length;
                        if (_PrjParm[_PrjParmLength - 1].Length == 4)
                        {
                            if (_PrjParm[_PrjParmLength - 1][1] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����й�ʱ��Ͷ��).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����й�ʱ��Ͷ��.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][2] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����޹�ʱ��Ͷ��).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����޹�ʱ��Ͷ��.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][3] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����޹�ʱ��Ͷ��).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����޹�ʱ��Ͷ��.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                        }
                    }
                }
                else if (intPrjID == (int)Cus_DgnItem.�ƶ���ʾֵ������)
                {
                    string[] _PrjParm = _Item.PrjParm.Split('|');

                    if (_PrjParm != null && _PrjParm.Length > 1)
                    {
                        int _PrjParmLength = _PrjParm.Length;
                        if (_PrjParm[_PrjParmLength - 1].Length == 4)
                        {
                            if (_PrjParm[_PrjParmLength - 1][1] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����й��ƶ���ʾֵ������).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����й��ƶ���ʾֵ������.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][2] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����޹��ƶ���ʾֵ������).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����޹��ƶ���ʾֵ������.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][3] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����޹��ƶ���ʾֵ������).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����޹��ƶ���ʾֵ������.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                        }
                    }
                }
                else if (intPrjID == (int)Cus_DgnItem.����ʱ��ʾֵ���)
                {
                    string[] _PrjParm = _Item.PrjParm.Split('|');

                    if (_PrjParm != null && _PrjParm.Length > 1)
                    {
                        int _PrjParmLength = _PrjParm.Length;
                        if (_PrjParm[_PrjParmLength - 1].Length == 4)
                        {
                            if (_PrjParm[_PrjParmLength - 1][1] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����й�����ʱ��ʾֵ���).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����й�����ʱ��ʾֵ���.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][2] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����޹�����ʱ��ʾֵ���).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����޹�����ʱ��ʾֵ���.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][3] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.�����޹�����ʱ��ʾֵ���).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.�����޹�����ʱ��ʾֵ���.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                        }
                    }
                }
                #endregion
            }
            return;
        }
        /// <summary>
        /// �洢�๦�ܷ�����XML�ĵ�
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            if (_LstDgn.Count == 0)
                return;
            _XmlNode.appendchild("", "DgnSy", "Name", Name);
            for (int _i = 0; _i < _LstDgn.Count; _i++)
            { 
                _XmlNode.appendchild(""
                                    ,"R"
                                    ,"PrjID"
                                    ,_LstDgn[_i].DgnPrjID
                                    ,"PrjName"
                                    ,_LstDgn[_i].DgnPrjName
                                    ,"PrjOutPut"
                                    ,_LstDgn[_i].OutPramerter.Jion()
                                    ,"PrjParameter"
                                    ,_LstDgn[_i].PrjParm);
            }
            _XmlNode.SaveXml(_FAPath);        
        }
        /// <summary>
        /// ����һ���µĶ๦�ܷ�����Ŀ
        /// </summary>
        /// <param name="PrjID">��ĿID��</param>
        /// <param name="PrjName">��Ŀ����</param>
        /// <param name="PrjOutPut">Դ�������(����|Ԫ��|��ѹ|����|��������)</param>
        /// <param name="PrjParm">�춨����</param>
        /// <returns></returns>
        public bool Add(string PrjID, string PrjName, string PrjOutPut, string PrjParm)
        {
            StPlan_Dgn _Item = new StPlan_Dgn();
            _Item.DgnPrjID = PrjID;
            _Item.DgnPrjName = PrjName;
            _Item.OutPramerter = new StPowerPramerter();
            _Item.OutPramerter.Split(PrjOutPut);
            _Item.PrjParm = PrjParm;
            if (_LstDgn.Contains(_Item))
                return false;
            _LstDgn.Add(_Item);
            return true;
        }

        /// <summary>
        /// ɾ������������Ŀ����
        /// </summary>
        public void RemoveAll()
        {
            _LstDgn.Clear();
        }

        /// <summary>
        /// ���ص�ǰ������Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstDgn.Count;
            }
        }

        /// <summary>
        /// �����б�����ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_Dgn getDgnPrj(int i)
        {
            if (i >= _LstDgn.Count)
                return new StPlan_Dgn();
            return _LstDgn[i];
        }

        /// <summary>
        /// �ƶ��๦����Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, StPlan_Dgn Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstDgn.Count ? _LstDgn.Count - 1 : i;
            this.Remove(Item);
            _LstDgn.Insert(i, Item);
            return;
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstDgn.Count)
                return;
            _LstDgn.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_Dgn Item)
        {
            if (!_LstDgn.Contains(Item))
                return;
            _LstDgn.Remove(Item);
            return;
        }
    }
}
