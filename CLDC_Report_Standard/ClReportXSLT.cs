using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Xml.Xsl;

namespace CLReport_Standard
{


    /// <summary>
    /// 采用XSLT生成报表
    /// </summary>
    public class ClReportXSLT //: CLDC_DataCore.Interfaces.IReportInterface
    {
        private UI.UI_ReportInfo uiReportInfo = null;

        private UI.UI_ReportSet uiReportSet = null;

        private string iniPath = clsMain.getFilePath(@"Res\Templet.ini");

        #region IReportInterface 成员

        public void PrintRpt(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, int ReportTao, int ReportType, string Jdyj, string zzbz)
        {
            //创建数据的XML文档
            XmlDocument dom = CreateXmlDom(Items);
            //
            string rootPath = getFilePath("Res");
            string xmlDataFile = rootPath + @"\Data.xml";
            string xsltFile = rootPath + @"\template.xslt";
            string outputDocument = rootPath + @"\report.docx";
            //保存为XML文件
            dom.Save(xmlDataFile);

            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(xsltFile);

            transform.Transform(xmlDataFile, xmlWriter);

            XmlDocument newWordContent = new XmlDocument();
            newWordContent.LoadXml(stringWriter.ToString());

            WordTemplateParser.WordProcess process = new WordTemplateParser.WordProcess();
            process.Create(outputDocument);

            if (process.UpdateBody(newWordContent))
            {
                System.Diagnostics.Process.Start(outputDocument);
            }



        }

        public IEnumerable<string> ReportTaoXing()
        {
            int intSum = int.Parse(clsMain.getIniString("TaoName", "NameSum", "0", iniPath));

            for (int i = 0; i < intSum; i++)
            {
                yield return clsMain.getIniString("TaoName", string.Format("Name_{0}", i + 1), "", iniPath);
            }
        }

        public string[] ReportType(int taoxing)
        {
            string typestring = clsMain.getIniString(string.Format("Type_{0}", taoxing), "TypeName", "", iniPath);

            if (typestring == "") return new string[0];

            return typestring.Split(',');
        }

        public void ShowPanel(CLDC_DataCore.Interfaces.IControlPanel panel)
        {
            panel.Save += new EventHandler(panel_Save);

            Dictionary<string, string> Items = new Dictionary<string, string>();

            Items.Add("Report_Info", "报表信息设置");

            Items.Add("Report_Set", "报表模板配置");

            System.Windows.Forms.TabPage[] tabs = panel.tabPages(Items);

            tabs[0].Controls.Clear();

            uiReportInfo = new CLReport_Standard.UI.UI_ReportInfo();

            tabs[0].Controls.Add(uiReportInfo);

            uiReportInfo.Dock = System.Windows.Forms.DockStyle.Fill;

            uiReportInfo.Margin = new System.Windows.Forms.Padding();

            tabs[1].Controls.Clear();

            uiReportSet = new CLReport_Standard.UI.UI_ReportSet();

            tabs[1].Controls.Add(uiReportSet);

            uiReportSet.Dock = System.Windows.Forms.DockStyle.Fill;

            uiReportSet.Margin = new System.Windows.Forms.Padding(); ;
        }
        private void panel_Save(object sender, EventArgs e)
        {
            uiReportInfo.Save();
        }
        #endregion

        #region XML序列化
        private XmlDocument CreateXmlDom(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items)
        {
            XmlDocument dom = new XmlDocument();
            dom.AppendChild(dom.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlNode rootNode = dom.CreateNode(XmlNodeType.Element, "Data", null);
            dom.AppendChild(rootNode);
            if (Items == null) return dom;
            foreach (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meter in Items)
            {
                XmlNode meterNode = CreateObjectNode(meter, dom);
                if (meterNode != null)
                    rootNode.AppendChild(meterNode);
            }
            return dom;
        }

        private XmlNode CreateObjectNode(object oObject, XmlDocument dom)
        {
            if (oObject == null) return null;
            XmlNode objectNode = dom.CreateNode(XmlNodeType.Element, oObject.GetType().Name, null);
            //一级对象
            Type vType = oObject.GetType();
            //属性
            PropertyInfo[] Propertys = vType.GetProperties();
            foreach (PropertyInfo vProperty in Propertys)
            {
                XmlNode propertyNode = dom.CreateNode(XmlNodeType.Element, vProperty.Name, null);
                object objValue = GetValueXml(vProperty.GetValue(oObject, null), dom);
                SetNodeValue(propertyNode, objValue);
                objectNode.AppendChild(propertyNode);
            }
            //字段
            FieldInfo[] Fields = vType.GetFields();
            foreach (FieldInfo vFild in Fields)
            {
                XmlNode fieldNode = dom.CreateNode(XmlNodeType.Element, vFild.Name, null);
                //fieldNode.InnerText =;
                SetNodeValue(fieldNode, GetValueXml(vFild.GetValue(oObject), dom));
                objectNode.AppendChild(fieldNode);
            }
            return objectNode;
        }

        private object GetValueXml(object oValue, XmlDocument dom)
        {
            if (oValue == null) return string.Empty;
            Console.WriteLine(oValue.GetType().ToString());
            if (oValue is ValueType || oValue is System.String || oValue is string)
            {
                return oValue.ToString();
            }
            else
            {
                XmlNode rootNode = dom.CreateNode(XmlNodeType.Element, "Values", null);
                Type objType = oValue.GetType();
                if (objType.IsGenericType)
                {
                    if (oValue is IList)
                    {
                        IList olist = objType as IList;
                        for (int i = 0; i < olist.Count; i++)
                        {
                            XmlNode listNode = dom.CreateNode(XmlNodeType.Element, "List", null);
                            object objValue = GetValueXml(olist[i], dom);
                            SetNodeValue(listNode, objValue);
                            rootNode.AppendChild(listNode);
                        }
                    }
                    else if (oValue is IDictionary)
                    {
                        IDictionary o = oValue as IDictionary;// (IDictionary)Activator.CreateInstance(constructed);
                        foreach (object okey in o.Keys)
                        {
                            XmlNode element = dom.CreateNode(XmlNodeType.Element, "Dictionary", null);
                            SetNodeValue(element, GetValueXml(o[okey], dom));
                            //element.InnerXml = ;
                            rootNode.AppendChild(element);
                        }
                    }
                    return rootNode;
                }
                else if (objType.IsClass)
                {
                    rootNode = CreateObjectNode(oValue, dom);
                    return rootNode;
                }
            }
            return oValue.ToString();
        }

        private void SetNodeValue(XmlNode node, object objValue)
        {
            if (objValue is XmlNode)
            {
               // foreach (XmlNode c in ((XmlNode)objValue).ChildNodes)
               //     node.AppendChild(c);
                node.AppendChild((XmlNode)objValue);
            }
            else
                node.InnerXml = objValue.ToString();
        }
        #endregion

        /// <summary>
        /// 根据相对路径获取绝对路径
        /// </summary>
        /// <param name="filepath">相对路径</param>
        /// <returns></returns>
        public static string getFilePath(string filepath)
        {
            string tmp = System.Reflection.Assembly.GetExecutingAssembly().Location;

            tmp = tmp.Substring(0, tmp.LastIndexOf('\\'));
            if (filepath == "") return tmp;

            return string.Format(@"{0}\{1}", tmp, filepath);

        }
    }
}
