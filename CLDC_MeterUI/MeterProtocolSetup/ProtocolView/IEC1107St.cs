using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.ProtocolView
{

    /// <summary>
    /// 在这个模块中应该还有很多测试中的返回事件，在返回事件中应该还有一个报文返回的事件，在模块中我会定义事件，然后在调用这个UI的窗体中实例这个事件
    /// </summary>
    public partial class IEC1107St : BaseControl
    {
        /// <summary>
        /// 返回报文事件
        /// </summary>
        /// <param name="MessageString">报文字符串</param>
        /// <param name="Tx">是否是发送</param>
        public delegate void EventReturnMessage(string MessageString, bool Tx);
        /// <summary>
        /// 声明一个报文事件，请在数据返回或发送时候引发该事件
        /// </summary>
        public event EventReturnMessage ReturnMessage;

        public IEC1107St()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 保存前检查内容是否有为空的
        /// </summary>
        /// <returns></returns>
        public bool CheckNull()
        {
            if (Cmb_Setting.Text == "")
            {
                MessageBox.Show("通信参数不能为空，参照格式(波特率,校验方式,数据位,停止位).....", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Txt_WritePwd.Text == "") Txt_WritePwd.Text = "000000";
            if (Txt_ClearXLPwd.Text == "") Txt_ClearXLPwd.Text = "000000";
            if (Txt_ClearDLPwd.Text == "") Txt_ClearDLPwd.Text = "000000";
            if (Cmb_Jian.Text == "") Cmb_Jian.SelectedIndex = 0;
            if (Cmb_Feng.Text == "") Cmb_Feng.SelectedIndex = 1;
            if (Cmb_Ping.Text == "") Cmb_Ping.SelectedIndex = 2;
            if (Cmb_Gu.Text == "") Cmb_Gu.SelectedIndex = 3;
            if (Cmb_DateTimeFormat.Text == "") Cmb_DateTimeFormat.SelectedIndex = 0;
            if (Cmb_SundayType.Text == "") Cmb_SundayType.SelectedIndex = 1;

            return true;
        }

        /// <summary>
        /// 根据协议模型将协议配置数据填充到窗体中
        /// </summary>
        /// <param name="ProtocolInfo"></param>
        /// <returns></returns>
        public bool setInfo(CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo ProtocolInfo)
        {
            if (ProtocolInfo == null || !ProtocolInfo.Loading)//如果协议模型为空，或者加载失败的话直接退出
            {
                return false;
            }

            this.TestProtocol = ProtocolInfo;

            Cmb_Setting.Text = ProtocolInfo.Setting;            //通信参数

            //Txt_UserID.Text = ProtocolInfo.UserID;                    //登陆用户

            Txt_WritePwd.Text = ProtocolInfo.WritePassword;       //写密码

            //Txt_WriteClass.Text = ProtocolInfo.WriteClass;          //写密码等级

            Txt_ClearXLPwd.Text = ProtocolInfo.ClearDemandPassword; //清需量密码

            //Txt_ClearXLClass.Text = ProtocolInfo.ClearDemandClass;      //清需量等级

            Txt_ClearDLPwd.Text = ProtocolInfo.ClearDLPassword;         //清电量密码

            //Txt_ClearDLClass.Text = ProtocolInfo.ClearDLClass;          //清电量等级
            //Cmb_VerifyType.SelectedIndex = ProtocolInfo.VerifyPasswordType;      //验证方式 
            //Txt_FECount.Text = ProtocolInfo.FECount.ToString();                 //下发FE个数

            Cmb_Jian.Text = (ProtocolInfo.TariffOrderType.IndexOf("1") + 1).ToString();       //尖费率号
            Cmb_Feng.Text = (ProtocolInfo.TariffOrderType.IndexOf("2") + 1).ToString();       //峰费率号
            Cmb_Ping.Text = (ProtocolInfo.TariffOrderType.IndexOf("3") + 1).ToString();       //平费率号
            Cmb_Gu.Text = (ProtocolInfo.TariffOrderType.IndexOf("4") + 1).ToString();         //谷费率号
            Cmb_DateTimeFormat.Text = ProtocolInfo.DateTimeFormat;              //日期格式化字符串
            Cmb_SundayType.Text = ProtocolInfo.SundayIndex.ToString();          //周末表示

            //Chk_DataFiledPwd.Checked = ProtocolInfo.DataFieldPassword;   //数据域是否包含密码
            //Chk_BlockAddAA.Checked = ProtocolInfo.BlockAddAA;             //写数据库块是否+AA

            ProtocolInfo.ConfigFile = Txt_ConfigFile.Text;              //配置文件，暂时无用

            Type_CommTest.SelectedIndex = -1;
            Type_WriteTime.SelectedIndex = -1;
            Type_ReadTime.SelectedIndex = -1;
            Type_ClearXL.SelectedIndex = -1;
            Type_ReadXL.SelectedIndex = -1;
            Type_ReadDL.SelectedIndex = -1;
            Type_ClearDL.SelectedIndex = -1;


            if (ProtocolInfo.DgnPras.Count > 0)
            {
                
                if (ProtocolInfo.DgnPras.ContainsKey("001"))
                    Type_CommTest.SelectedIndex = int.Parse(ProtocolInfo.DgnPras["001"].Split('|')[0]);           //通信测试
                if (ProtocolInfo.DgnPras.ContainsKey("002"))
                    Type_WriteTime.SelectedIndex = int.Parse(ProtocolInfo.DgnPras["002"].Split('|')[0]);            //写表时间
                if (ProtocolInfo.DgnPras.ContainsKey("003"))
                    Type_ReadTime.SelectedIndex = int.Parse(ProtocolInfo.DgnPras["003"].Split('|')[0]);             //读表时间
                if (ProtocolInfo.DgnPras.ContainsKey("004"))
                    Type_ClearXL.SelectedIndex = int.Parse(ProtocolInfo.DgnPras["004"].Split('|')[0]);              //清空需量
                if (ProtocolInfo.DgnPras.ContainsKey("005"))
                    Type_ReadXL.SelectedIndex = int.Parse(ProtocolInfo.DgnPras["005"].Split('|')[0]);               //读取需量
                if (ProtocolInfo.DgnPras.ContainsKey("006"))
                    Type_ReadDL.SelectedIndex = int.Parse(ProtocolInfo.DgnPras["006"].Split('|')[0]);               //读取电量
                //if (ProtocolInfo.DgnPras.ContainsKey("007"))
                //    Type_ReadSD.SelectedIndex = int.Parse((ProtocolInfo.DgnPras["007"].Split('|'))[0]);             //读取时段
                if (ProtocolInfo.DgnPras.ContainsKey("008"))
                    Type_ClearDL.SelectedIndex = int.Parse(ProtocolInfo.DgnPras["008"].Split('|')[0]);              //清空电量

                /*
                if (ProtocolInfo.DgnPras.ContainsKey("100"))                                //事件记录
                {
                    TmpArr = ProtocolInfo.DgnPras["100"].Split('|');
                    if (TmpArr.Length < 3) return false;
                    Title_EventLog.Text = TmpArr[0];
                    Len_EventLog.Text = TmpArr[1];
                    Dot_EventLog.Text = TmpArr[2];
                }
                if (ProtocolInfo.DgnPras.ContainsKey("101"))                                //瞬时存储器
                {
                    TmpArr = ProtocolInfo.DgnPras["101"].Split('|');
                    if (TmpArr.Length < 3) return false;
                    Title_NowRom.Text = TmpArr[0];
                    Len_NowRom.Text = TmpArr[1];
                    Dot_NowRom.Text = TmpArr[2];
                }
                if (ProtocolInfo.DgnPras.ContainsKey("102"))                                //状态寄存器
                {
                    TmpArr = ProtocolInfo.DgnPras["102"].Split('|');
                    if (TmpArr.Length < 3) return false;
                    Title_State.Text = TmpArr[0];
                    Len_State.Text = TmpArr[1];
                    Dot_State.Text = TmpArr[2];
                }
                if (ProtocolInfo.DgnPras.ContainsKey("103"))                                //失压寄存器
                {
                    TmpArr = ProtocolInfo.DgnPras["103"].Split('|');
                    if (TmpArr.Length < 3) return false;
                    Title_Lost.Text = TmpArr[0];
                    Len_Lost.Text = TmpArr[1];
                    Dot_Lost.Text = TmpArr[2];
                }
                if (ProtocolInfo.DgnPras.ContainsKey("104"))
                {
                    TmpArr = ProtocolInfo.DgnPras["104"].Split('|');             //运行状态
                    if (TmpArr.Length < 3) return false;
                    Title_Run.Text = TmpArr[0];
                    Len_Run.Text = TmpArr[1];
                    Dot_Run.Text = TmpArr[2];
                }
                if (ProtocolInfo.DgnPras.ContainsKey("105"))                                //预付费
                {
                    TmpArr = ProtocolInfo.DgnPras["105"].Split('|');
                    if (TmpArr.Length < 3) return false;
                    Title_Yff.Text = TmpArr[0];
                    Len_Yff.Text = TmpArr[1];
                    Dot_Yff.Text = TmpArr[2];
                }
                 */
            }

            return true;
        }


        public override bool getInfo(ref CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo ProtocolInfo)
        {
            if (ProtocolInfo == null) return false;

            ProtocolInfo.Setting = Cmb_Setting.Text;                                    //通信参数
            ProtocolInfo.WritePassword = Txt_WritePwd.Text;                             //写密码
            //ProtocolInfo.WriteClass = Txt_WriteClass.Text;                              //写等级
            ProtocolInfo.ClearDemandPassword = Txt_ClearXLPwd.Text;                     //清需量密码
            //ProtocolInfo.ClearDemandClass = Txt_ClearXLClass.Text;                      //清需量等级
            ProtocolInfo.ClearDLPassword = Txt_ClearDLPwd.Text;                         //清电量密码
            //ProtocolInfo.ClearDLClass = Txt_ClearDLClass.Text;                          //清电量等级
            //ProtocolInfo.VerifyPasswordType = Cmb_VerifyType.SelectedIndex;             //密码验证了姓    

            //try
            //{
            //    ProtocolInfo.FECount = int.Parse(Txt_FECount.Text);                     //下发FE个数
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            {
                string[] TmpFeiLvOrder = new string[4];

                TmpFeiLvOrder[int.Parse(Cmb_Jian.Text) - 1] = "1";

                TmpFeiLvOrder[int.Parse(Cmb_Feng.Text) - 1] = "2";

                TmpFeiLvOrder[int.Parse(Cmb_Ping.Text) - 1] = "3";

                TmpFeiLvOrder[int.Parse(Cmb_Gu.Text) - 1] = "4";

                ProtocolInfo.TariffOrderType = string.Join("", TmpFeiLvOrder);         //费率排序

                TmpFeiLvOrder = null;
            }

            ProtocolInfo.DateTimeFormat = Cmb_DateTimeFormat.Text;          //时间格式化字符串
            ProtocolInfo.SundayIndex = int.Parse(Cmb_SundayType.Text);                 //周末表示
            //ProtocolInfo.DataFieldPassword = Chk_DataFiledPwd.Checked;      //数据域是否包含密码
            //ProtocolInfo.BlockAddAA = Chk_BlockAddAA.Checked;               //写数据块是否+AA
            ProtocolInfo.ConfigFile = Txt_ConfigFile.Text;                  //配置文件,暂时无用

            ProtocolInfo.DgnPras = new Dictionary<string, string>();

            ProtocolInfo.DgnPras.Add("001", Type_CommTest.SelectedIndex.ToString() + "|");      //通信测试

            if (Type_WriteTime.SelectedIndex > -1)
            {
                ProtocolInfo.DgnPras.Add("002", Type_WriteTime.SelectedIndex.ToString() + "|");       //写表时间
            }
            if (Type_ReadTime.SelectedIndex > -1)
            {
                ProtocolInfo.DgnPras.Add("003", Type_ReadTime.SelectedIndex.ToString() + "|");        //读表时间
            }
            if (Type_ClearXL.SelectedIndex > -1)
            {
                ProtocolInfo.DgnPras.Add("004", Type_ClearXL.SelectedIndex.ToString() + "|");      //清空需量
            }
            if (Type_ReadXL.SelectedIndex > -1)
            {
                ProtocolInfo.DgnPras.Add("005", Type_ReadXL.SelectedIndex.ToString() + "|");       //读表需量
            }
            if (Type_ReadDL.SelectedIndex > -1)
            {
                ProtocolInfo.DgnPras.Add("006", Type_ReadDL.SelectedIndex.ToString() + "|");        //读表电量
            }
            //ProtocolInfo.DgnPras.Add("007", Type_ReadSD.SelectedIndex.ToString() + "|");        //读表时段
            if (Type_ClearDL.SelectedIndex > -1)
            {
                ProtocolInfo.DgnPras.Add("008", Type_ClearDL.SelectedIndex.ToString() + "|");        //清空电量
            }
            //ProtocolInfo.DgnPras.Add("100", string.Format("{0}|{1}|{2}", Title_EventLog.Text, Len_EventLog.Text, Dot_EventLog.Text));        //事件记录
            //ProtocolInfo.DgnPras.Add("101", string.Format("{0}|{1}|{2}", Title_NowRom.Text, Len_NowRom.Text, Dot_NowRom.Text));        //瞬时寄存器检查
            //ProtocolInfo.DgnPras.Add("102", string.Format("{0}|{1}|{2}", Title_State.Text, Len_State.Text, Dot_State.Text));        //状态寄存器检查
            //ProtocolInfo.DgnPras.Add("103", string.Format("{0}|{1}|{2}", Title_Lost.Text, Len_Lost.Text, Dot_Lost.Text));        //失压寄存器检查
            //ProtocolInfo.DgnPras.Add("104", string.Format("{0}|{1}|{2}", Title_Run.Text, Len_Run.Text, Dot_Run.Text));        //运行状态检查
            //ProtocolInfo.DgnPras.Add("105", string.Format("{0}|{1}|{2}", Title_Yff.Text, Len_Yff.Text, Dot_Yff.Text));        //预付费检查

            return true;
        }

        /// <summary>
        /// 开始测试方法
        /// </summary>
        public override void StartTest(string Adr, float U, int Index)
        {
            ParaPanel = panel1;
            base.StartTest(Adr, U, Index);

            //if (!this.getInfo(ref TestProtocol)) return;

            //this.MeterAdr = Adr;

            //foreach (Control _Item in panel1.Controls)
            //{
            //    if (_Item is CheckBox && ((CheckBox)_Item).Checked)
            //    {
            //        if (blnStop) return;
            //        this.Nexted = false;    //这个标志是在接收到返回信息后置为真！！切记
            //        switch (((CheckBox)_Item).Name.ToLower())
            //        {
            //            case "chk_commtest":            //通信测试
            //                //这个里面需要写具体命令发送之类的代码

            //                break;
            //            case "chk_readtime":            //读时间

            //                break;
            //            case "chk_writetime":           //写时间

            //                break;
            //            case "chk_clearxl":             //清空需量

            //                break;
            //            case "chk_readxl":              //读取需量

            //                break;
            //            case "chk_readdl":              //读取电量

            //                break;
            //            case "chk_readsd":              //读时段

            //                break;
            //            case "chk_cleardl":             //清空电量

            //                break;
            //            case "chk_eventlog":            //事件记录

            //                break;
            //            case "chk_nowrom":              //瞬时寄存器

            //                break;
            //            case "chk_state":               //状态寄存器

            //                break;
            //            case "chk_lost":                //失压寄存器

            //                break;
            //            case "chk_run":                 //运行状态

            //                break;
            //            case "chk_yff":                 //预付费

            //                break;
            //            default:
            //                this.Nexted = true;
            //                break;

            //        }


            //        this.WaitSleep(100);        //休眠，直到满足条件（条件为Nexted为真，或blnStop为真）  
            //    }
            //}

        }


        /// <summary>
        /// 发送命令后的线程等待
        /// </summary>
        /// <param name="Milliseconds">休眠间隔时间</param>
        private void WaitSleep(int Milliseconds)
        {
            while (!this.Nexted)
            {
                System.Threading.Thread.Sleep(Milliseconds);
                if (this.blnStop)
                    return;
            }
        }

       

    }
}
