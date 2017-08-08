using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 谐波参数设置
    /// </summary>
    public class csXieBo
    {
        private Dictionary<string, List<CLDC_DataCore.Struct.StXieBo>> XieBoCol;

        private List<string> _FaName;

        /// <summary>
        /// 构造函数
        /// </summary>
        public csXieBo()
        {
            XieBoCol = new Dictionary<string, List<CLDC_DataCore.Struct.StXieBo>>();
            _FaName = new List<string>();

            this.Load();
        }

        /// <summary>
        /// 谐波方案数据加载
        /// </summary>
        public void Load()
        {
            string ErrorString = "";
            XmlNode _XmlNode = CLDC_DataCore.DataBase.clsXmlControl.LoadXml(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_XIEBO, out ErrorString);
            XieBoCol.Clear();
            _FaName.Clear();
            if (ErrorString != "")     //有错误，一般存在的错误在于未找到该谐波配置文件,未找到则初始化一个空的谐波文件
            {
                _XmlNode = CLDC_DataCore.DataBase.clsXmlControl.CreateXmlNode("XieBo");
                CLDC_DataCore.DataBase.clsXmlControl.SaveXml(_XmlNode, System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_XIEBO);
            }

            for (int i = 0; i < _XmlNode.ChildNodes.Count; i++)
            {
                _FaName.Add(_XmlNode.ChildNodes[i].Attributes[0].Value);        //获取方案名称

                List<CLDC_DataCore.Struct.StXieBo> Items = new List<CLDC_DataCore.Struct.StXieBo>();      //方案项目内容
                
                for (int j = 0; j < _XmlNode.ChildNodes[i].ChildNodes.Count; j++)        //进入第二层XML节点
                {
                    CLDC_DataCore.Struct.StXieBo Item = new CLDC_DataCore.Struct.StXieBo();

                    XmlNode _ChildNode = _XmlNode.ChildNodes[i].ChildNodes[j];   //这一个节点是原件

                    Item.YuanJian = (CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(_ChildNode.Attributes[0].Value);        //原件

                    for (int z = 0; z < _ChildNode.ChildNodes.Count; z++)       //进入第三层XML节点
                    {
                        XmlNode _CChildNode = _ChildNode.ChildNodes[z];         //这一个节点标志是电压还是电流

                        Item.IsUb = _CChildNode.Attributes[0].Value.ToString().ToLower() == "u" ? true : false;

                        for (int w = 0; w < _CChildNode.ChildNodes.Count; w++)         //进入第四层XML节点
                        {
                            Item.Num = int.Parse(_CChildNode.ChildNodes[w].Attributes[0].Value);        //次数
                            Item.Extent = float.Parse(_CChildNode.ChildNodes[w].Attributes[1].Value);   //幅度
                            Item.Xw = float.Parse(_CChildNode.ChildNodes[w].Attributes[2].Value);   //谐波相位

                            Items.Add(Item.Clone());            //加入一个谐波项目
                        }
                    }
                }

                XieBoCol.Add(_FaName[_FaName.Count - 1], Items);        //加入一个谐波方案
            }

        }

        /// <summary>
        /// 获取方案名称列表
        /// </summary>
        public List<string> FaNameList
        {
            get
            {
                return _FaName;
            }
        }

        /// <summary>
        /// 获取方案项目列表
        /// </summary>
        /// <param name="FaName">谐波方案名称</param>
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
        /// 验证方案名称是否存在
        /// </summary>
        /// <param name="FaName">方案名称</param>
        /// <returns></returns>
        public bool IsFaName(string FaName)
        {
            if (_FaName.Contains(FaName))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 方案存储
        /// </summary>
        /// <param name="FaName">方案名称</param>
        /// <param name="Items">项目列表</param>
        public void Save(string FaName, List<CLDC_DataCore.Struct.StXieBo> Items)
        {
            this.Remove(FaName);

            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl();

            _XmlNode.appendchild("", "R", "Name", FaName);

            for (int i = 0; i < Items.Count; i++)
            { 
                if(CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode(),
                                        CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}","C","Name",(int)Items[i].YuanJian)))==null)       //如果元件节点不存在，则追加一个
                {
                    _XmlNode.appendchild("", "C", "Name", ((int)Items[i].YuanJian).ToString());
                }
                if (CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode(),
                    CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}|{3},{4},{5}", "C", "Name", (int)Items[i].YuanJian, "D", "Name", Items[i].IsUb ? "U" : "I"))) == null)   //如果电流电压节点不存在，则追加一个
                {
                    _XmlNode.appendchild(CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}", "C", "Name", ((int)Items[i].YuanJian).ToString())), "D", "Name", Items[i].IsUb ? "U" : "I");
                }

                XmlNode _Node=CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode(),
                                        CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}|{3},{4},{5}", "C", "Name", ((int)Items[i].YuanJian).ToString(), "D", "Name", Items[i].IsUb ? "U" : "I")));     //获取电流电压节点

                _Node = CLDC_DataCore.DataBase.clsXmlControl.RemoveChildNode(_Node, CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("{0},{1},{2}", "E", "Cs", Items[i].Num.ToString())));            //在电流电电压节点下移除存在相同的谐波次数的节点

                _Node.AppendChild(CLDC_DataCore.DataBase.clsXmlControl.CreateXmlNode("E", "Cs", Items[i].Num.ToString(), "Fd", Items[i].Extent.ToString(), "Xw", Items[i].Xw.ToString()));          //追加谐波项目节点

            }

            CLDC_DataCore.DataBase.clsXmlControl _SaveXml = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_XIEBO);         //获取谐波方案XML文档

            _SaveXml.appendchild(_XmlNode.toXmlNode());         //追加当前方案

            _SaveXml.SaveXml();                 //存储方案

            _FaName.Add(FaName);

            XieBoCol.Add(FaName, Items);

        }
        /// <summary>
        /// 移除一个谐波方案
        /// </summary>
        /// <param name="FaName">方案名称</param>
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
