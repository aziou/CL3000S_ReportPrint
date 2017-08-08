using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// г����������
    /// </summary>
    public class csXieBo
    {
        private Dictionary<string, List<CLDC_DataCore.Struct.StXieBo>> XieBoCol;

        private List<string> _FaName;

        /// <summary>
        /// ���캯��
        /// </summary>
        public csXieBo()
        {
            XieBoCol = new Dictionary<string, List<CLDC_DataCore.Struct.StXieBo>>();
            _FaName = new List<string>();

            this.Load();
        }

        /// <summary>
        /// г���������ݼ���
        /// </summary>
        public void Load()
        {
            string ErrorString = "";
            XmlNode _XmlNode = CLDC_DataCore.DataBase.clsXmlControl.LoadXml(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_XIEBO, out ErrorString);
            XieBoCol.Clear();
            _FaName.Clear();
            if (ErrorString != "")     //�д���һ����ڵĴ�������δ�ҵ���г�������ļ�,δ�ҵ����ʼ��һ���յ�г���ļ�
            {
                _XmlNode = CLDC_DataCore.DataBase.clsXmlControl.CreateXmlNode("XieBo");
                CLDC_DataCore.DataBase.clsXmlControl.SaveXml(_XmlNode, System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_XIEBO);
            }

            for (int i = 0; i < _XmlNode.ChildNodes.Count; i++)
            {
                _FaName.Add(_XmlNode.ChildNodes[i].Attributes[0].Value);        //��ȡ��������

                List<CLDC_DataCore.Struct.StXieBo> Items = new List<CLDC_DataCore.Struct.StXieBo>();      //������Ŀ����
                
                for (int j = 0; j < _XmlNode.ChildNodes[i].ChildNodes.Count; j++)        //����ڶ���XML�ڵ�
                {
                    CLDC_DataCore.Struct.StXieBo Item = new CLDC_DataCore.Struct.StXieBo();

                    XmlNode _ChildNode = _XmlNode.ChildNodes[i].ChildNodes[j];   //��һ���ڵ���ԭ��

                    Item.YuanJian = (CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(_ChildNode.Attributes[0].Value);        //ԭ��

                    for (int z = 0; z < _ChildNode.ChildNodes.Count; z++)       //���������XML�ڵ�
                    {
                        XmlNode _CChildNode = _ChildNode.ChildNodes[z];         //��һ���ڵ��־�ǵ�ѹ���ǵ���

                        Item.IsUb = _CChildNode.Attributes[0].Value.ToString().ToLower() == "u" ? true : false;

                        for (int w = 0; w < _CChildNode.ChildNodes.Count; w++)         //������Ĳ�XML�ڵ�
                        {
                            Item.Num = int.Parse(_CChildNode.ChildNodes[w].Attributes[0].Value);        //����
                            Item.Extent = float.Parse(_CChildNode.ChildNodes[w].Attributes[1].Value);   //����
                            Item.Xw = float.Parse(_CChildNode.ChildNodes[w].Attributes[2].Value);   //г����λ

                            Items.Add(Item.Clone());            //����һ��г����Ŀ
                        }
                    }
                }

                XieBoCol.Add(_FaName[_FaName.Count - 1], Items);        //����һ��г������
            }

        }

        /// <summary>
        /// ��ȡ���������б�
        /// </summary>
        public List<string> FaNameList
        {
            get
            {
                return _FaName;
            }
        }

        /// <summary>
        /// ��ȡ������Ŀ�б�
        /// </summary>
        /// <param name="FaName">г����������</param>
        /// <returns></returns>
        public List<CLDC_DataCore.Struct.StXieBo> getXieBoFa(string FaName)
        {
            if (!XieBoCol.ContainsKey(FaName))
            {
                return new List<CLDC_DataCore.Struct.StXieBo>();
            }
            return XieBoCol[FaName];
        }
        /// <summary>
        /// ��֤���������Ƿ����
        /// </summary>
        /// <param name="FaName">��������</param>
        /// <returns></returns>
        public bool IsFaName(string FaName)
        {
            if (_FaName.Contains(FaName))
                return true;
            else
                return false;
        }

        /// <summary>
        /// �����洢
        /// </summary>
        /// <param name="FaName">��������</param>
        /// <param name="Items">��Ŀ�б�</param>
        public void Save(string FaName, List<CLDC_DataCore.Struct.StXieBo> Items)
        {
            this.Remove(FaName);

            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl();

            _XmlNode.appendchild("", "R", "Name", FaName);

            for (int i = 0; i < Items.Count; i++)
            { 
                if(CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode(),
                                        CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}","C","Name",(int)Items[i].YuanJian)))==null)       //���Ԫ���ڵ㲻���ڣ���׷��һ��
                {
                    _XmlNode.appendchild("", "C", "Name", ((int)Items[i].YuanJian).ToString());
                }
                if (CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode(),
                    CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}|{3},{4},{5}", "C", "Name", (int)Items[i].YuanJian, "D", "Name", Items[i].IsUb ? "U" : "I"))) == null)   //���������ѹ�ڵ㲻���ڣ���׷��һ��
                {
                    _XmlNode.appendchild(CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}", "C", "Name", ((int)Items[i].YuanJian).ToString())), "D", "Name", Items[i].IsUb ? "U" : "I");
                }

                XmlNode _Node=CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode(),
                                        CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}|{3},{4},{5}", "C", "Name", ((int)Items[i].YuanJian).ToString(), "D", "Name", Items[i].IsUb ? "U" : "I")));     //��ȡ������ѹ�ڵ�

                _Node = CLDC_DataCore.DataBase.clsXmlControl.RemoveChildNode(_Node, CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}", "E", "Cs", Items[i].Num.ToString())));            //�ڵ������ѹ�ڵ����Ƴ�������ͬ��г�������Ľڵ�

                _Node.AppendChild(CLDC_DataCore.DataBase.clsXmlControl.CreateXmlNode("E", "Cs", Items[i].Num.ToString(), "Fd", Items[i].Extent.ToString(), "Xw", Items[i].Xw.ToString()));          //׷��г����Ŀ�ڵ�

            }

            CLDC_DataCore.DataBase.clsXmlControl _SaveXml = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_XIEBO);         //��ȡг������XML�ĵ�

            _SaveXml.appendchild(_XmlNode.toXmlNode());         //׷�ӵ�ǰ����

            _SaveXml.SaveXml();                 //�洢����

            _FaName.Add(FaName);

            XieBoCol.Add(FaName, Items);

        }
        /// <summary>
        /// �Ƴ�һ��г������
        /// </summary>
        /// <param name="FaName">��������</param>
        public void Remove(string FaName)
        {
            if (!IsFaName(FaName))
            {
                return;
            }
            CLDC_DataCore.DataBase.clsXmlControl _Xml=new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_XIEBO);

            _Xml.RemoveChild(CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}","R","Name",FaName)));

            _Xml.SaveXml();

            _FaName.Remove(FaName);

            XieBoCol.Remove(FaName);

            return;

        }

    }
}
