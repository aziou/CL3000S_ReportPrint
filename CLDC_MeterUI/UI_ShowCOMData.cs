using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI
{
    public partial class UI_ShowCOMData : Office2007Form
    {
        private delegate void OnEeventShowText(byte[] bData);
        private delegate void OnEventShowText(string strText);
        public UI_ShowCOMData()
        {
            InitializeComponent();
        }

        public void ShowData(byte[] bData)
        {
            if (!this.IsHandleCreated) return;
            if (bData.Length < 4) return;
            this.BeginInvoke(new OnEeventShowText(dteShowData), new object[] { bData });
        }
        /// <summary>
        /// 显示485通讯数据
        /// </summary>
        /// <param name="strData"></param>
        public void ShowData(string strData)
        {
            if (!this.IsHandleCreated) return;
            this.BeginInvoke(new OnEventShowText(dteShowData2), new object[] { strData });
        }
        /// <summary>
        /// 显示2036通讯数据
        /// </summary>
        /// <param name="strData"></param>
        public void ShowData2(string strData)
        {
            if (!this.IsHandleCreated) return;
            this.BeginInvoke(new OnEventShowText(dteShowData3), new object[] { strData });            
        }


        private void dteShowData(byte[] bData)
        {
            string strData = string.Empty;

            string strCmdType ="";
            if (bData[1] == 0x80)
            {
                strData += "接收<==";
                strCmdType = getCmdType(bData);
            }
            else
            {
                strData += "发送==>";
                strCmdType = PC_UnPacket(bData);
            }
            for (int i = 0; i < bData.Length; i++)
                strData += Convert.ToString(bData[i], 16) + " ";
            strData += "\r\n[" + strCmdType + "] \r\n";
            textBox1.Text += strData + "\r\n";
            textBox1.ScrollToCaret();
        }
        private void dteShowData2(string strText)
        {
            textBox1.Text += strText + "\r\n";
            textBox1.ScrollToCaret();
        }
        private void dteShowData3(string strText)
        {
            textBox2.Text = strText + "\r\n" + textBox2.Text;
            textBox2.ScrollToCaret();
        }
        private string getCmdType(byte[] bData)
        {
            int Cmd = bData[4];
            switch (Cmd)
            {
                case 0x30:
                    {
                        return "操作成功";
                    }
                case 0x33:
                    {
                        return "操作失败";
                    }
                case 0x50:
                    {
                        return "传送有关数据或参数";
                    }
                case 0xa0:
                    {
                        return "询问有关数据或参数";
                    }
                case 0xA3:
                    {
                        return "改写有关数据或参数";
                    }
                case 0xc9:
                    {
                        return "询问设备型号、版本号、产品出厂串号";
                    }
                case 0xc0:
                    {
                        return "询问命令，询问对方是否已准备好接受新的命令";
                    }
            }


            return "不明操作";
        }


        #region----------发送数据解析----------
        private string PC_UnPacket(byte[] RxBuf)
        {
            switch (RxBuf[4])
            {
                //==========================================
                case 0xC9:
                    return "读取控制器版本号";
                //==========================================
                case 0xA0:						//0xA0
                    switch (RxBuf[5])			//读
                    {
                        //==========================================
                        case 0x00:						//0xA0 + 0x00
                            switch (RxBuf[6])
                            {
                                //==========================================
                                case 0x02:						//0xA0 + 0x00 + 0x02
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x8E:						//0xA0 + 0x00 + 0x02 + 0x8E
                                            //PC_ReadTime();				//读取时间
                                            return "读取时间";
                                    }
                                    break;
                                //==========================================
                                case 0x04:						//0xA0 + 0x00 + 0x04
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x02:						//0xA0 + 0x00 + 0x04 + 0x02
                                            //PC_ReadWorkState();			//读工作状态
                                            return "读工作状态";
                                        //==========================================
                                        case 0x30:						//0xA0 + 0x00 + 0x04 + 0x30
                                            //PC_ReadTemperatureAndHumidity();
                                            return "读取温度和温度";
                                    }
                                    break;
                                //==========================================
                                case 0x08:						//0xA0 + 0x00 + 0x08
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x40:						//0xA0 + 0x00 + 0x08 + 0x40
                                            //PC_ReadSMC();			//读标准表常数
                                            return "读标准表常数";
                                    }
                                    break;
                                //==========================================
                                case 0x10:						//0xA0 + 0x00 + 0x10
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x40:						//0xA0 + 0x00 + 0x10 + 0x40
                                            //PC_ReadTotalEnergy();
                                            return "读取走字累计电量";
                                    }
                                    break;
                            }
                            break;
                        //==========================================
                        case 0x02:						//0xA0 + 0x02
                            switch (RxBuf[6])
                            {
                                //==========================================
                                case 0x02:						//0xA0 + 0x02 + 0x02
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x3F:						//0xA0 + 0x02 + 0x02 + 0x3f +  ChkSum
                                            //PC_ReadRange();				//读取档位
                                            return "读取档位";
                                    }
                                    break;
                                //==========================================
                                case 0x3F:						//0xA0 + 0x02 + 0x3F
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x7F:						//0xA0 + 0x02 + 0x3f + 0x7f + 0x08 + 0x3f + 0xff + 0xff + 0x0f + ChkSum
                                            //PC_ReadBaseData();			//读取基本数据
                                            return "读取基本数据";
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                //==========================================
                case 0xA3:						//0xA3
                    switch (RxBuf[5])			//写
                    {
                        //==========================================
                        case 0x00:						//0xA3 + 0x00
                            switch (RxBuf[6])
                            {
                                //==========================================
                                case 0x04:						//0xA3 + 0x00 + 0x04
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x01:						//0xA3 + 0x00 + 0x04 + 0x01
                                            //PC_SetLink(RxBuf);
                                            return "联机";
                                    }
                                    break;
                                //==========================================
                                case 0x08:						//0xA3 + 0x00 + 0x08
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x04:						//0xA3 + 0x00 + 0x08 + 0x04
                                            //PC_SetUniformTestCircleTimes(RxBuf);
                                            return "设置检定圈数[统一]";
                                        //==========================================
                                        case 0x08:						//0xA3 + 0x00 + 0x08 + 0x08
                                            //PC_SetUniformMeterConst(RxBuf);
                                            return "设置被检表常数[统一]";
                                    }
                                    break;
                                //==========================================
                                case 0x10:						//0xA3 + 0x00 + 0x10
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x01:						//0xA3 + 0x00 + 0x10 + 0x01
                                            //PC_SetStartOrEndFunc(RxBuf);
                                            return "启动/停止当前操作";
                                    }
                                    break;
                                //==========================================
                                case 0x20:						//0xA3 + 0x00 + 0x20
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x18:						//0xA3 + 0x00 + 0x20 + 0x18
                                            //PC_SetUniformEDayBMFreqAndPluseNum(RxBuf);
                                            return "设置日计时误差脉冲数量和频率[统一]";
                                        //==========================================
                                        case 0x40:						//0xA3 + 0x00 + 0x20 + 0x40
                                            //PC_SetUniformTstChannel(RxBuf);
                                            return "设置脉冲通道[统一]";
                                    }
                                    break;
                                //==========================================
                                case 0x28:						//0xA3 + 0x00 + 0x28
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x20:						//0xA3 + 0x00 + 0x28 + 0x20
                                            //PC_SetUniformDemandECycPluseDistTimeAndNum(RxBuf);
                                            return "设置需量脉冲周期和数量";
                                    }
                                    break;
                                //==========================================
                                case 0x38:						//0xA3 + 0x00 + 0x38
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x11:						//0xA3 + 0x00 + 0x38 + 0x11
                                            //PC_SetFuncParam(RxBuf);
                                            return "设置检定参数";
                                    }
                                    break;
                                //==========================================
                                case 0x80:						//0xA3 + 0x00 + 0x80
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x40:						//0xA3 + 0x00 + 0x80 + 0x40
                                            //PC_SetMeterSwitch(RxBuf);
                                            return "设置被检表开关[无用]";
                                    }
                                    break;

                                //==========================================
                                case 0xC0:						//0xA3 + 0x00 + 0xC0
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0xFF:						//0xA3 + 0x00 + 0xC0 + 0xFF
                                            //PC_SetBaseWorkParam(RxBuf);
                                            return "设置基本工作参数";
                                    }
                                    break;
                            }
                            break;
                        //==========================================
                        case 0x01:						//0xA3 + 0x01
                            switch (RxBuf[6])
                            {
                                //==========================================
                                case 0x01:						//0xA3 + 0x01 + 0x01
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x03:						//0xA3 + 0x01 + 0x01 + 0x03
                                            //PC_SetMeterCommunicatePort(RxBuf);
                                            return "设置通讯端口";
                                        //==========================================
                                        case 0x0C:						//0xA3 + 0x01 + 0x01 + 0x0C
                                            //PC_SetMeterCommunicateMode(RxBuf);
                                            return "设置通讯模式";
                                        //==========================================
                                        case 0x20:						//0xA3 + 0x01 + 0x01 + 0x20
                                            //PC_SetTstStateLight(RxBuf);
                                            return "设置状态灯";
                                    }
                                    break;
                            }
                            break;
                        //==========================================
                        case 0x05:						//0xA3 + 0x05
                            switch (RxBuf[6])
                            {
                                //==========================================
                                case 0x01:						//0xA3 + 0x05 + 0x01
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x80:						//0xA3 + 0x05 + 0x01 + 0x80
                                            //PC_SetVoltageDownTst(RxBuf);
                                            return "电压跌落";
                                    }
                                    break;
                                //==========================================
                                case 0x02:						//0xA3 + 0x05 + 0x02
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x3F:						//0xA3 + 0x05 + 0x02 + 0x3F
                                            //PC_SetAcSrcPhi(RxBuf);
                                            return "设置交流源角度";
                                    }
                                    break;
                                //==========================================
                                case 0x20:						//0xA3 + 0x05 + 0x20
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x7F:						//0xA3 + 0x05 + 0x20 + 0x7F
                                            //PC_SetHarmonicSwitch(RxBuf);
                                            return "设置谐波开关";
                                    }
                                    break;
                                //==========================================
                                case 0x44:						//0xA3 + 0x05 + 0x44
                                    switch (RxBuf[7])
                                    {
                                        //==========================================
                                        case 0x80:						//0xA3 + 0x05 + 0x44 + 0x80
                                            //PC_SetSrcRenew(RxBuf);
                                            return "更新交流源";
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                //==========================================	
                case 0xA5:						//0xA5
                    switch (RxBuf[5])			//读数组
                    {
                        //==========================================
                        case 0x00:						//0xA5 + 0x00
                            switch (RxBuf[6])
                            {
                                //==========================================		 	
                                case 0x21:						//0xA5 + 0x00 + 0x21
                                    //PC_ReadFuncData1(RxBuf);
                                    return "读取检定数据1";
                                //==========================================		 	
                                case 0x22:						//0xA5 + 0x00 + 0x22
                                   // PC_ReadFuncData2(RxBuf);
                                    return "读取检定数据2";
                                //==========================================
                                case 0x23:						//0xA5 + 0x00 + 0x23
                                    //PC_ReadFuncData3(RxBuf);
                                    return "读取检定数据3";
                                //==========================================
                                case 0x24:						//0xA5 + 0x00 + 0x24
                                    //PC_ReadFuncData4(RxBuf);
                                    return "读取检定数据4";
                                //==========================================
                                case 0x25:						//0xA5 + 0x00 + 0x25
                                    //PC_ReadFuncData5(RxBuf);
                                    return "读取检定数据5";
                                //==========================================
                                case 0x27:						//0xA5 + 0x00 + 0x27
                                    //PC_ReadMalfunctionState(RxBuf);
                                    return "读取故障状态";
                            }
                            break;
                        //==========================================
                        case 0x01:						//0xA5 + 0x01
                            switch (RxBuf[6])
                            {
                                //==========================================		 	
                                case 0x04:						//0xA5 + 0x01 + 0x04
                                    //PC_ReadTemperatureState(RxBuf);
                                    return "读取温度故障";
                            }
                            break;
                    }
                    break;
                //==========================================	
                case 0xA6:						//0xA6
                    switch (RxBuf[5])			//写数组
                    {
                        //==========================================
                        case 0x00:						//0xA6 + 0x00
                            switch (RxBuf[6])
                            {
                                //==========================================
                                case 0x1A:						//0xA6 + 0x00 + 0x1A
                                    //PC_SetTestCircleTimes(RxBuf);
                                    return "设置误差检定每个点校验圈数";
                                //==========================================		 	
                                case 0x1B:						//0xA6 + 0x00 + 0x1B
                                    //PC_SetMeterConst(RxBuf);
                                    return "设置被检表常数";
                                //==========================================		 	
                                case 0x1F:						//0xA6 + 0x00 + 0x1F
                                    //PC_SetStopAtFlagPulseNum(RxBuf);
                                    return "设置停止脉冲个数";
                                //==========================================		 	
                                case 0x25:						//0xA6 + 0x00 + 0x25
                                    //PC_SetDemandECycPluseNum(RxBuf);
                                    return "设置需量周期脉冲个数";
                                //==========================================		 	
                                case 0x26:						//0xA6 + 0x00 + 0x26
                                    //PC_SetTstChannel(RxBuf);
                                    return "设置脉冲通道";
                                //==========================================		 	
                                case 0x29:						//0xA6 + 0x00 + 0x29
                                    //PC_SetDemandECycPluseDistTime(RxBuf);
                                    return "设置需量脉冲周期";
                                //==========================================
                                case 0x2B:						//0xA6 + 0x00 + 0x2B
                                    //PC_SetEDayBMFreq(RxBuf);
                                    return "设置日计时误差频率";
                                //==========================================	 	
                                case 0x2C:						//0xA6 + 0x00 + 0x2C
                                   // PC_SetEDayPluseNum(RxBuf);
                                    return "设置日计时误差脉冲个数";
                            }
                            break;
                        //==========================================
                        case 0x01:						//0xA6 + 0x01
                            switch (RxBuf[6])
                            {
                                //==========================================
                                case 0x00:						//0xA6 + 0x01 + 0x00
                                    //PC_SetIsEligible(RxBuf);
                                    return "设置表位灯是否合格";
                            }
                            break;
                        //==========================================
                        case 0x05:						//0xA6 + 0x05
                            switch (RxBuf[6])
                            {
                                //==========================================
                                case 0x00:						//0xA6 + 0x05 + 0x00
                                    //PC_SetUcHarmonicPercent(RxBuf);
                                    return "C相电压谐波含量";
                                //==========================================
                                case 0x01:						//0xA6 + 0x05 + 0x01
                                    //PC_SetUbHarmonicPercent(RxBuf);
                                    return "B相电压谐波含量";
                                //==========================================
                                case 0x02:						//0xA6 + 0x05 + 0x02
                                    //PC_SetUaHarmonicPercent(RxBuf);
                                    return "A相电压谐波含量";
                                //==========================================
                                case 0x03:						//0xA6 + 0x05 + 0x03
                                    //PC_SetIcHarmonicPercent(RxBuf);
                                    return "C相电流谐波含量";
                                //==========================================
                                case 0x04:						//0xA6 + 0x05 + 0x04
                                    //PC_SetIbHarmonicPercent(RxBuf);
                                    return "B相电流谐波含量";
                                //==========================================
                                case 0x05:						//0xA6 + 0x05 + 0x05
                                    //PC_SetIaHarmonicPercent(RxBuf);
                                    return "A相电流谐波含量";
                                //==========================================
                                case 0x08:						//0xA6 + 0x05 + 0x08
                                    //PC_SetUcHarmonicPhase(RxBuf);
                                    return "C相电压谐波相位";
                                //==========================================
                                case 0x09:						//0xA6 + 0x05 + 0x09
                                    //PC_SetUbHarmonicPhase(RxBuf);
                                    return "B相电压谐波相位";
                                //==========================================
                                case 0x0A:						//0xA6 + 0x05 + 0x0A
                                    //PC_SetUaHarmonicPhase(RxBuf);
                                    return "A相电压谐波相位";
                                //==========================================
                                case 0x0B:						//0xA6 + 0x05 + 0x0B
                                    //PC_SetIcHarmonicPhase(RxBuf);
                                    return "C相电流谐波相位";
                                //==========================================
                                case 0x0C:						//0xA6 + 0x05 + 0x0C
                                    //PC_SetIbHarmonicPhase(RxBuf);
                                    return "B相电流谐波相位";
                                //==========================================
                                case 0x0D:						//0xA6 + 0x05 + 0x0D
                                    //PC_SetIaHarmonicPhase(RxBuf);
                                    return "A相电流谐波相位";
                            }
                            break;
                    }
                    break;
            }

            return "错误的命令";
            //PC_RespondERR();
        }

        #endregion
    }
}