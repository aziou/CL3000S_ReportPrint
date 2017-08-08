using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI
{
    /// <summary>
    /// 电机延迟参数设置界面
    /// </summary>
    public partial class MotorDelayTimeControl : UserControl
    {
        public MotorDelayTimeControl()
        {
            InitializeComponent();
            InitUI();
        }
        /// <summary>
        /// 压接电机的textbox 和 checkbox
        /// </summary>
        private List<TextBox> TxtUpDownList = new List<TextBox>();
   //     private List<string> UpDownStringList = new List<string>();
        private List<CheckBox> ChbUpDownList = new List<CheckBox>();
    //    private List<bool> ChbUpDownBoolList = new List<bool>();
        /// <summary>
        /// 倾斜电机的textbox 和 checkbox
        /// </summary>
        private List<TextBox> TxtLeanList = new List<TextBox>();
     //   private List<string> LeanStringList = new List<string>();
        private List<CheckBox> ChbLeanList = new List<CheckBox>();
       // private List<bool> ChbLeanBoolList = new List<bool>();
        /// <summary>
        /// 初始化控件UI
        /// </summary>
        private void InitUI()
        {
            if (TxtUpDownList.Count > 0)
            {
                return;
            }
            for (int i = 0; i < 120; i++)
            {
                TextBox textBox = new TextBox();
                textBox.TextChanged += new EventHandler(textBox_Changed);
                TxtUpDownList.Add(textBox);
              //  TxtUpDownList.Add(new TextBox() );
            }
            for (int i = 0; i < 60; i++)
            {
                ChbUpDownList.Add(new CheckBox());
            }
            TxtLeanList.Clear();
            ChbLeanList.Clear();
            //for (int i = 0; i < 8; i++)  add zjl  20150115
            for (int i = 0; i < 16; i++)
            {
                TextBox textBox = new TextBox();
                textBox.TextChanged += new EventHandler(textBox_Changed);
                TxtLeanList.Add(textBox);
            }
            for (int i = 0; i < 8; i++)
            {
                ChbLeanList.Add(new CheckBox());
            }

            int textIndex = 0;
            int chbIndex = 0;
            int textLIndex = 0;
            int chbLIndex = 0;
            for (int j = 0; j < 17; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (j % 4 < 2 && (i > 0 && i < 16) && j < 15)
                    {
                        TxtUpDownList[textIndex].Anchor = AnchorStyles.None;
                        container.Controls.Add(TxtUpDownList[textIndex], i, j);
                        textIndex++;
                    }
                    if (j % 4 == 2 && (i > 0 && i < 16) && j < 15)
                    {
                        ChbUpDownList[chbIndex].Anchor = AnchorStyles.None;
                        ChbUpDownList[chbIndex].Text = (chbIndex + 1).ToString();
                        container.Controls.Add(ChbUpDownList[chbIndex], i, j);
                        chbIndex++;
                    }
                    if (i == 17)
                    {
                        if (j % 4 < 2 && j < 15)
                        {
                            TxtLeanList[textLIndex].Anchor = AnchorStyles.None;
                            container.Controls.Add(TxtLeanList[textLIndex], i, j);
                            textLIndex++;
                        }
                        if (j % 4 == 2 && j < 16)
                        {
                            ChbLeanList[chbLIndex].Anchor = AnchorStyles.None;
                            ChbLeanList[chbLIndex].Text = (chbLIndex + 1).ToString();
                            container.Controls.Add(ChbLeanList[chbLIndex], i, j);
                            chbLIndex++;
                        }
                    }

                    if (i == 19)
                    {
                        if (j % 4 < 2 && j < 15)
                        {
                            TxtLeanList[textLIndex].Anchor = AnchorStyles.None;
                            container.Controls.Add(TxtLeanList[textLIndex], i, j);
                            textLIndex++;
                        }
                        if (j % 4 == 2 && j < 16)
                        {
                            ChbLeanList[chbLIndex].Anchor = AnchorStyles.None;
                            ChbLeanList[chbLIndex].Text = (chbLIndex + 1).ToString();
                            container.Controls.Add(ChbLeanList[chbLIndex], i, j);
                            chbLIndex++;
                        }
                    }
                }
            }
        }
        #region ---显示数据到UI---
        /// <summary>
        ///将数组里的数据显示到UI
        ///（up数组为上延时数组 down下延时数组）
        /// </summary>
        /// <param name="up"></param>
        /// <param name="down"></param>
        public void ShowUpDown(string[] up, string[] down)
        {
            showUpDown(up, false);
            showUpDown(down, true);
        }
        /// <summary>
        ///将数组里的数据显示到UI
        ///（up数组为上延时数组 down下延时数组）
        /// </summary>
        /// <param name="up"></param>
        /// <param name="down"></param>
        public void ShowUpDown(int[] up, int[] down)
        {
            string[] upstring;
            string[] downstring;
            if (up != null)
            {
                upstring = new string[up.Length];
                for (int i = 0; i < upstring.Length; i++)
                {
                    upstring[i] = up[i].ToString();
                }
            }
            else
                upstring = new string[0];
            if (down != null)
            {
                downstring = new string[down.Length];
                for (int i = 0; i < downstring.Length; i++)
                {
                    downstring[i] = down[i].ToString();
                }
            }
            else
                downstring = new string[0];

            ShowUpDown(upstring, downstring);
        }
        /// <summary>
        /// 显示压接电机数据 updownFlog=true 显示下延时电机数据 FALSE 显示上延时 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="updownFlog"></param>
        private void showUpDown(string[] num, bool updownFlog)
        {
            int diff = 0;
            if (updownFlog)
                diff = 15;
            if (num != null)
            {
                int temp;
                for (int i = 0; i < num.Length; i++)
                {
                    if (i < (TxtUpDownList.Count / 2))
                    {
                        if (int.TryParse(num[i], out temp))
                        {
                            TxtUpDownList[((i + diff) / 15) * 15 + i].Text = num[i];
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        ///将数组里的数据显示到UI
        ///（standup数组为竖直延时数组 lean倾斜延时数组）
        /// </summary>
        /// <param name="standup"></param>
        /// <param name="lean"></param>
        public void ShowTurn(string[] standup, string[] lean)
        {
            showTurn(standup, false);
            showTurn(lean, true);
        }
        /// <summary>
        ///将数组里的数据显示到UI
        ///（standup数组为竖直延时数组 lean倾斜延时数组）
        /// </summary>
        /// <param name="standup"></param>
        /// <param name="lean"></param>
        public void ShowTurn(int[] standup, int[] lean)
        {
            if (standup != null)
            {
                string[] Getstring = new string[standup.Length];
                for (int i = 0; i < Getstring.Length; i++)
                {
                    Getstring[i] = standup[i].ToString();
                }
                showTurn(Getstring, false);
            }
            if (lean != null)
            {
                string[] Getstring = new string[lean.Length];
                for (int i = 0; i < Getstring.Length; i++)
                {
                    Getstring[i] = lean[i].ToString();
                }
                showTurn(Getstring, true);
            }
        }
        /// <summary>
        /// Flog=false 为竖直延时显示 true为倾斜显示
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Flog"></param>
        private void showTurn(string[] value, bool Flog)
        {
            int diff =0;
            if (Flog)
                diff = 2;
            if (value != null)
            {
                int temp;
                for (int i = 0; i < value.Length; i++)
                {
                    if (i < TxtLeanList.Count / 2)
                    {
                        if (Flog)
                        {
                            if (int.TryParse(value[i], out temp))
                            {
                                TxtLeanList[(i/2*4 + i % 2 +2)].Text = value[i];
                            }
                        }
                        else
                        {
                            if (int.TryParse(value[i], out temp))
                            {
                                TxtLeanList[(i / 2 * 4 + i % 2)].Text = value[i];
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 显示压接电机 Flog数组里的数据到UI
        /// </summary>
        /// <param name="Flog"></param>
        public void ShowUpDownEnable(bool[] Flog)
        {
            if (Flog != null)
            {
                for (int i = 0; i < Flog.Length; i++)
                {
                    if (i < ChbUpDownList.Count)
                    {
                        ChbUpDownList[i].Checked = Flog[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 显示翻转电机状态 Flog数组里的数据到UI
        /// </summary>
        /// <param name="Flog"></param>
        public void ShowTurnEnable(bool[] Flog)
        {
            if (Flog != null)
            {
                for (int i = 0; i < Flog.Length; i++)
                {
                    if (i < ChbLeanList.Count)
                    {
                        ChbLeanList[i].Checked = Flog[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        #endregion ---显示数据到UI---

        /// <summary>
        /// 设置对应 index 压接电机的参数
        /// </summary>
        /// <param name="index"></param>
        /// <param name="stringValue"></param>
        public void SetUpDownValue(int index, string stringValue)
        {
            if (index < TxtUpDownList.Count && stringValue != null)
            {
                TxtUpDownList[index].Text = stringValue;
            }
        }
        /// <summary>
        /// 设置对应 index  翻转电机的参数使能
        /// index<120
        /// </summary>
        /// <param name="index"></param>
        /// <param name="Value"></param>
        public void SetUpDownEnable(int index, bool Value)
        {
            if (index < ChbUpDownList.Count)
            {
                ChbUpDownList[index].Checked = Value;
            }
        }
        /// <summary>
        /// 设置对应 index 翻转电机的参数
        ///  index<8
        /// </summary>
        /// <param name="index"></param>
        /// <param name="stringValue"></param>
        public void SetTurnValue(int index, string stringValue)
        {
            if (index < TxtLeanList.Count && stringValue != null)
            {
                TxtLeanList[index].Text = stringValue;
            }
        }
        /// <summary>
        /// 设置对应 index  翻转电机的参数使能
        /// index<8
        /// </summary>
        /// <param name="index"></param>
        /// <param name="Value"></param>
        public void SetTurnEnable(int index, bool Value)
        {
            if (index < ChbLeanList.Count)
            {
                ChbLeanList[index].Checked = Value;
            }
        }

        /// <summary>
        /// 获取上延迟信息
        /// </summary>
        /// <returns></returns>
        public int[] GetUpMeterDelay()
        {
            return  GetUpDownMeterDelay(0);
        }
        /// <summary>
        /// 获取下延迟信息
        /// </summary>
        /// <returns></returns>
        public int[] GetDownMeterDelay()
        {
            return GetUpDownMeterDelay(1);
        }
        /// <summary>
        /// 获取上下延时状态
        /// </summary>
        /// <returns></returns>
        public bool[] GetUpDownCheck()
        {
            bool[] status=new bool[ChbUpDownList .Count ];
            for (int i = 0; i < ChbUpDownList.Count; i++)
            {
                status[i] = ChbUpDownList[i].Checked;
            }
            return status;
        }
        /// <summary>
        /// 获取倾斜电机延迟
        /// </summary>
        /// <returns></returns>
        public int[] GetLeanMeterDelay()
        {           
            return GetTurnMeterDelay(1);
        }
        /// <summary>
        /// 获取竖直电机延迟
        /// </summary>
        /// <returns></returns>
        public int[] GetStandupMeterDelay()
        {
            return GetTurnMeterDelay(0);
        }
        /// <summary>
        /// 获取翻转电机的状态
        /// </summary>
        /// <returns></returns>
        public bool[] GetTurnCheck()
        {
            bool [] status=new bool[ChbLeanList .Count ];
            for (int i = 0; i < ChbLeanList.Count; i++)
            {
                status[i] = ChbLeanList[i].Checked;
            }
            return status;
        }
        /// <summary>
        /// 获取翻转电机延迟信息 0 为竖直延时 1 为倾斜延时
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int[] GetTurnMeterDelay(int flog)
        {
              
            int[] Getstring = new int[TxtLeanList.Count / 2];
            int index=0;
            for (int i = 0; i < TxtLeanList.Count; i++)
            {
                if (flog == 1)
                {
                    
                        if (i % 4 >= 2)
                        {
                            int.TryParse(TxtLeanList[i].Text, out Getstring[index++]);
                        }
                     
                }
                else
                {
                    if (  i % 4 < 2)
                    {
                        int.TryParse(TxtLeanList[i].Text, out Getstring[index++]);
                    }
                }
                
                //if (i % 2 == flog)
                //    int.TryParse(TxtLeanList[i].Text, out Getstring[index++]);
                   // Getstring[index++] = TxtLeanList[i].Text;
            }
            return Getstring;
        }
        /// <summary>
        /// 获取压接电机延迟信息 0 为上延时 1 为下延时
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int[] GetUpDownMeterDelay(int flog)
        {
            int[] Getstring = new int[TxtUpDownList.Count/2 ];
            int index = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == flog)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        int.TryParse(TxtUpDownList[i * 15 + j].Text, out Getstring[index++]);
                    //    Getstring[index++] = TxtUpDownList[i * 15 + j].Text;
                    }
                }
            }
            return Getstring;
        }
        /// <summary>
        /// 获取正 负号状态 返回值 1为- ，0为+,-1为异常
        /// </summary>
        /// <returns></returns>
        public int GetOperation()
        {
            if (radioButton1.Checked)
            {
                return 0;
            }
            if (radioButton2.Checked)
            {
                return 1;
            }
            return -1;
        }

        #region---定义控件按钮事件---
        /// <summary>
        /// 上延时按钮事件
        /// </summary>
        public event EventHandler UpDelayClick;
        /// <summary>
        /// 下延时按钮事件
        /// </summary>
       public event EventHandler DownDelayClick;
        /// <summary>
        /// 竖直延时按钮事件
        /// </summary>
       public event EventHandler StandupDelayClick;
        /// <summary>
        /// 倾斜延时按钮事件
        /// </summary>
       public event EventHandler LeanDelayClick;
        /// <summary>
        /// 读压接电机延时事件
        /// </summary>
       public event EventHandler ReadUpDownClick;
        /// <summary>
        /// 读翻转电机延时事件
        /// </summary>
       public event EventHandler ReadTurnClick;
       private void button1_Click(object sender, EventArgs e)
       {//上延时
           if (UpDelayClick != null)
               UpDelayClick(sender, e);
       }

       private void button2_Click(object sender, EventArgs e)
       {//下延时
           if (DownDelayClick != null)
               DownDelayClick(sender, e);
       }

       private void button4_Click(object sender, EventArgs e)
       {//竖直延时
           if (StandupDelayClick != null)
               StandupDelayClick(sender, e);
       }

       private void button5_Click(object sender, EventArgs e)
       {
           if (LeanDelayClick != null)
               LeanDelayClick(sender, e);
       }


       private void button6_Click(object sender, EventArgs e)
       {//读压接电机
           if (ReadUpDownClick != null)
               ReadUpDownClick(sender, e);
       }

       private void button3_Click(object sender, EventArgs e)
       {//读翻转电机延时
           if (ReadTurnClick != null)
               ReadTurnClick(sender, e);
       }
        #endregion

       private void btnUp_Click(object sender, EventArgs e)
       {

           if (TxtUp.Text != "")
           {
               for (int i = 0; i < TxtUpDownList.Count / 2; i++)
               {
                   TxtUpDownList[(i/ 15) * 15 + i].Text = TxtUp.Text;             
               }
           }
           //int[] Getstring = new int[TxtUpDownList.Count / 2];
           //int index = 0;
           //for (int i = 0; i < 8; i++)
           //{
           //    if (i % 2 == flog)
           //    {
           //        for (int j = 0; j < 15; j++)
           //        {
           //            int.TryParse(TxtUpDownList[i * 15 + j].Text, out Getstring[index++]);
           //            //    Getstring[index++] = TxtUpDownList[i * 15 + j].Text;
           //        }
           //    }
           //}
       }

       private void btnDown_Click(object sender, EventArgs e)
       {
           if (TxtDown.Text != "")
           {
               for (int i = 0; i < TxtUpDownList.Count / 2; i++)
               {
                   TxtUpDownList[((i + 15) / 15) * 15 + i].Text = TxtDown.Text;
               }
           }
       }

       private void btnDeleteAll_Click(object sender, EventArgs e)
       {
           for (int i = 0; i < TxtUpDownList.Count; i++)
           {
               TxtUpDownList[i].Clear();
           }
       }

       /// <summary>
       /// 快速改变UI中checkbox状态
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
       {
           Label l = (Label)sender;
           int Index = int.Parse(l.Name.Replace("label", ""));
           for (int i = 0; i < 15; i++)
           {
               ChbUpDownList[((Index - 1) / 2) * 15 + i].Checked = !ChbUpDownList[((Index - 1) / 2) * 15 + i].Checked;
           }
       }
        /// <summary>
        /// 文本框输入改变事件 只允许数字输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void  textBox_Changed(object sender, EventArgs e)
       {
           TextBox t = (TextBox)sender;
           if (t.Text != "")
           {
               char[] chr = t.Text.ToCharArray();
               char c = chr[chr.Length - 1];
               if (c < '0' || c > '9')
               {
                   t.Text = t.Text.Remove(t.Text.Length - 1);
                   t.SelectionStart = t.Text.Length;
               }
           }
       }
    }
}
