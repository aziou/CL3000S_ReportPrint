using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CLDC_MeterUI.UI_Detection_New.ModuleTestControl
{
    public partial class Ctr_ErrPlate : UserControl
    {
        private delegate void RunMothed();
        private static string[][] strStep = { 
          new string[]  {"1","读限位","","","自动","开始", "0","start"},
          new string[]  {"2","发指令控制电机","","","自动","", "0",""},
          new string[]  {"3","读限位","","","自动","", "0",""},
          new string[]  {"4","发指令控制电机","","","自动","","0",""},
          new string[]  {"5","读限位","","","自动","","0",""},
          new string[]  {"6","读温度","","","自动","", "0",""},
          new string[]  {"7","发指令控制电机到上位","","","自动","","0",""}, 
          new string[]  {"8","按钮电机到下位","","","手动","","0",""},
          new string[]  {"9","读下限位","","","手动","读限位","0",""},
          new string[]  {"10","解除电机限制","","","手动","解除", "0",""},
          new string[]  {"11","按钮电机到上位","","","手动","", "0",""},
          new string[]  {"12","读上限位","","","手动","读限位","0",""},
          new string[]  {"13","发隔离指令","","","手动","隔离","0",""},
          new string[]  {"14","发恢复指令","","","手动","恢复", "0",""},
          new string[]  {"15","发一回路指令","","","手动","一回路", "1",""},
          new string[]  {"16","发二回路指令","","","手动","二回路", "1",""}
          
                                            };
        public Ctr_ErrPlate()
        {
            InitializeComponent();

            int length = strStep.Length;
            for (int i = 0; i < length; i++)
            {
                dataGridViewX1.Rows.Add(strStep[i]);
                
            }
            dataGridViewX1.CellClick += new DataGridViewCellEventHandler(dataGridViewX1_CellClick);

        }

        int sleepTime
        {
            get
            {

                return int.Parse(textBoxX1.Text) * 1000;
            }
        }
        bool IsDan
        {
            get
            {
                return checkBoxX1.Checked;
            }
        }

        void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewX1.Columns.Count - 1)         //最后一列
            {
                if (string.IsNullOrEmpty(dataGridViewX1[e.ColumnIndex, e.RowIndex].Value.ToString()) == false)
                {

                    string switch_on = strStep[e.RowIndex][5];
                    switch (switch_on)
                    {
                        case "开始":
                            start();
                            break;
                        case "读限位":
                            string tmp = "";
                            ReadBwStates(e.RowIndex, ref tmp);
                            break;
                        case "解除":
                            break;
                        case "隔离":

                            break;
                        case "恢复":
                            break;
                        case "一回路":
                            break;
                        case "二回路":
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void start()
        {
            for (int i = 0; i < strStep.Length; i++)
            {
                dataGridViewX1[2, i].Value = "";
                dataGridViewX1[3, i].Value = "";
            }
            Refresh();
            //1
            int Rowid = 0;
            string tmp = "---";
            bool rst1 = ReadBwStates(Rowid, ref tmp);
            if (rst1 == false)
            {
                return;
            }
            System.Threading.Thread.Sleep(1000);
            //2
            bool[] bln_ep2r = null;
            string[] str_ds = null;
            if (tmp.IndexOf("上") != -1)
            {

                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(true, out bln_ep2r, out str_ds);
            }
            else
            {
                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(false, out bln_ep2r, out str_ds);
            }
            System.Threading.Thread.Sleep(sleepTime);
            dataGridViewX1[3, 1].Value = "√";
            Refresh();
            //3
            string tmp3 = "---";
            bool rst3 = ReadBwStates(2, ref tmp3);
            if (rst3 == false)
            {
                return;
            }
            System.Threading.Thread.Sleep(1000);
            //4
            if (tmp.IndexOf("上") != -1)
            {

                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(false, out bln_ep2r, out str_ds);
            }
            else
            {
                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(true, out bln_ep2r, out str_ds);
            }
            System.Threading.Thread.Sleep(sleepTime);
            dataGridViewX1[3, 3].Value = "√";
            Refresh();
            //5
            bool rst5 = ReadBwStates(4, ref tmp);
            if (rst5 == false)
            {
                return;
            }
            System.Threading.Thread.Sleep(1000);
            //6
            string[][] str_WDA = null;
            string[][] str_WDB = null;
            string[][] str_WDC = null;
            string str_wd = "";
            bool[] YJMeter = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadErrPltTemperature(YJMeter, out str_WDA, out str_WDB, out str_WDC);
            if (IsDan == true)
            {
                str_wd = "A进" + str_WDA[0][0] + ",A出" + str_WDA[0][1];
            }
            else
            {
                str_wd = "A进" + str_WDA[0][0] + ",A出" + str_WDA[0][1];
                str_wd += "B进" + str_WDB[0][0] + ",B出" + str_WDB[0][1];
                str_wd += "C进" + str_WDC[0][0] + ",C出" + str_WDC[0][1];
            }
            dataGridViewX1[2, 5].Value = str_wd;
            dataGridViewX1[3, 5].Value = "√";
            //7
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(false, out bln_ep2r, out str_ds);
            System.Threading.Thread.Sleep(sleepTime);
            dataGridViewX1[3, 6].Value = "√";
            //8
            MessageBoxEx.Show(this, "请手动按按钮，使电机到下位。");
            //9
        }

        private bool ReadBwStates(int Rowid,ref string tmp)
        {
            string[] bwState = null;
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.GetBWStatus(out bwState);
            bool rst1 = true;
            if (bwState != null)
            {


                if (bwState[0] == "101")
                {
                    tmp = "下限位、挂表";
                }
                if (bwState[0] == "100")
                {
                    tmp = "下限位、未挂表";
                }
                if (bwState[0] == "011")
                {
                    tmp = "上限位、挂表";
                }
                if (bwState[0] == "010")
                {
                    tmp = "上限位、未挂表";
                }
                if (bwState[0] == "000")
                {
                    tmp = "未到位、未挂表";
                }
                if (bwState[0] == "001")
                {
                    tmp = "未到位、挂表";
                }
                if (bwState[0] == "110" || bwState[0] == "111")
                {
                    tmp = "限位错误";
                    rst1 = false;
                }

                dataGridViewX1[2, Rowid].Value = tmp;
                string rightwg = "√";
                if (Rowid == 8)
                {
                    if (tmp.IndexOf("上") != -1)
                    {
                        rightwg = "×";
                        rst1 = false;
                    }
                }
                else if (Rowid == 11)
                {
                    if (tmp.IndexOf("下") != -1)
                    {
                        rightwg = "×";
                        rst1 = false;
                    }
                }
                else
                {
                    rightwg = "√";

                }
                dataGridViewX1[3, Rowid].Value = rightwg;
                if (rst1 == false)
                {
                    (dataGridViewX1[3, Rowid]).Style.BackColor = Color.Red;

                }
            }
            else
            {
                rst1 = false;
            }
            Refresh();
            return rst1;
        }

        private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX2.Checked == true)
            {
                dataGridViewX1.Rows[14].Visible = false;
                dataGridViewX1.Rows[15].Visible = false;
            }
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked == true)
            {
                dataGridViewX1.Rows[14].Visible = true;
                dataGridViewX1.Rows[15].Visible = true;
            }
        }

    }
}
