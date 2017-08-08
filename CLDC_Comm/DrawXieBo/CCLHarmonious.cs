using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace CLDC_Comm.DrawXieBo
{
    /// <summary>
    /// 类功能：谐波含量示意图
    /// 编写者：Kobe.Zhang
    /// 日  期：2009.01.19
    /// </summary>
    public class CCLHarmonious
    {

        private int m_int_MaxTimes = 21;            //谐波最大次数


        private bool[] m_bln_UaTimesSelected;      //A相电压各次谐波开关，即0=当次不加谐波，1=当次加谐波
        private float[] m_flt_UaValue;              //A相电压各次谐波含量
        private float[] m_flt_UaPhase;              //A相电压各次谐波相位

        private bool[] m_bln_UbTimesSelected;       //B相电压各次谐波开关，即0=当次不加谐波，1=当次加谐波
        private float[] m_flt_UbValue;              //B相电压各次谐波含量
        private float[] m_flt_UbPhase;              //B相电压各次谐波相位

        private bool[] m_bln_UcTimesSelected;       //C相电压各次谐波开关，即0=当次不加谐波，1=当次加谐波
        private float[] m_flt_UcValue;              //C相电压各次谐波含量
        private float[] m_flt_UcPhase;              //C相电压各次谐波相位

        private bool[] m_bln_IaTimesSelected;      //A相电流各次谐波开关，即0=当次不加谐波，1=当次加谐波
        private float[] m_flt_IaValue;              //A相电流各次谐波含量
        private float[] m_flt_IaPhase;              //A相电流各次谐波相位

        private bool[] m_bln_IbTimesSelected;       //B相电流各次谐波开关，即0=当次不加谐波，1=当次加谐波
        private float[] m_flt_IbValue;              //B相电流各次谐波含量
        private float[] m_flt_IbPhase;              //B相电流各次谐波相位

        private bool[] m_bln_IcTimesSelected;       //C相电流各次谐波开关，即0=当次不加谐波，1=当次加谐波
        private float[] m_flt_IcValue;              //C相电流各次谐波含量
        private float[] m_flt_IcPhase;              //C相电流各次谐波相位

        private float m_flt_Scope = 0.5f;           //显示波形的比例（为迭加含量留空白）
        private int m_int_ScaleWidth;               //有效宽度
        private int m_int_ScaleHeight;              //有效高度
        private int m_int_Margin = 10;              //边距
        private CCLCurve m_cce_Curve;               //曲线图

        private Color m_clr_UaLineColor = Color.Yellow;
        private Color m_clr_IaLineColor = Color.Yellow;
        private Color m_clr_UbLineColor = Color.Green;
        private Color m_clr_IbLineColor = Color.Green;
        private Color m_clr_UcLineColor = Color.Red;
        private Color m_clr_IcLineColor = Color.Red;

        private int m_int_LineWidth = 2;
        private bool[] m_bln_XXSelected ;
     

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pic_PicMap">图</param>
        public CCLHarmonious(System.Windows.Forms.PictureBox pic_PicMap)
        {
            this.m_cce_Curve = new CCLCurve(pic_PicMap);
            this.m_bln_XXSelected = new bool[] { false, false, false, false, false, false };



            pic_PicMap.SizeChanged += new EventHandler(pic_PicMap_SizeChanged);

            

            this.m_cce_Curve.ShowPI = true;
            this.m_cce_Curve.AxisType = AxisType.LeftMiddle;

            this.m_int_ScaleWidth = this.m_cce_Curve.ScaleWidth;
            this.m_int_ScaleHeight = this.m_cce_Curve.ScaleHeight;
            this.m_cce_Curve.Margin = 10;
            this.m_cce_Curve.Lines.Add(6);       //Ua/Ub/Uc/Ia/Ib/Ic

            this.m_bln_UaTimesSelected = new bool[m_int_MaxTimes];
            this.m_flt_UaPhase = new float[m_int_MaxTimes];
            this.m_flt_UaValue = new float[m_int_MaxTimes];
            this.m_bln_UbTimesSelected = new bool[m_int_MaxTimes];
            this.m_flt_UbPhase = new float[m_int_MaxTimes];
            this.m_flt_UbValue = new float[m_int_MaxTimes];
            this.m_bln_UcTimesSelected = new bool[m_int_MaxTimes];
            this.m_flt_UcPhase = new float[m_int_MaxTimes];
            this.m_flt_UcValue = new float[m_int_MaxTimes];

            this.m_bln_IaTimesSelected = new bool[m_int_MaxTimes];
            this.m_flt_IaPhase = new float[m_int_MaxTimes];
            this.m_flt_IaValue = new float[m_int_MaxTimes];
            this.m_bln_IbTimesSelected = new bool[m_int_MaxTimes];
            this.m_flt_IbPhase = new float[m_int_MaxTimes];
            this.m_flt_IbValue = new float[m_int_MaxTimes];
            this.m_bln_IcTimesSelected = new bool[m_int_MaxTimes];
            this.m_flt_IcPhase = new float[m_int_MaxTimes];
            this.m_flt_IcValue = new float[m_int_MaxTimes];
        }

        private delegate void DgtSizeChanged(object sender, EventArgs e);


        private void pic_PicMap_SizeChanged(object sender, EventArgs e)
        {

            ((System.Windows.Forms.PictureBox)sender).BeginInvoke(new DgtSizeChanged(PicSizeChanged),sender,e);
        }

        private void PicSizeChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1);
            this.m_cce_Curve = new CCLCurve((System.Windows.Forms.PictureBox)sender);
            this.m_int_ScaleWidth = this.m_cce_Curve.ScaleWidth;
            this.m_int_ScaleHeight = this.m_cce_Curve.ScaleHeight;
            this.Draw();
        }


        /// <summary>
        /// 
        /// </summary>
        public void ClearXieBo()
        {
            for (int i = 1; i < m_int_MaxTimes; i++)
            {
                this.m_flt_UaPhase[i] = 0;
                this.m_flt_UaValue[i] = 0;
                this.m_bln_UaTimesSelected[i] = false;
                this.m_flt_UbPhase[i] = 0;
                this.m_flt_UbValue[i] = 0;
                this.m_bln_UbTimesSelected[i] = false;
                this.m_flt_UcPhase[i] = 0;
                this.m_flt_UcValue[i] = 0;
                this.m_bln_UcTimesSelected[i] = false;
                this.m_flt_IaPhase[i] = 0;
                this.m_flt_IaValue[i] = 0;
                this.m_bln_IaTimesSelected[i] = false;
                this.m_flt_IbPhase[i] = 0;
                this.m_flt_IbValue[i] = 0;
                this.m_bln_IbTimesSelected[i] = false;
                this.m_flt_IcPhase[i] = 0;
                this.m_flt_IcValue[i] = 0;
                this.m_bln_IcTimesSelected[i] = false; 
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pic_PicMap">图</param>
        /// <param name="int_MaxTimes">最大次数</param>
        public CCLHarmonious(System.Windows.Forms.PictureBox pic_PicMap, int int_MaxTimes)
        {
            this.m_cce_Curve = new CCLCurve(pic_PicMap);
            this.m_cce_Curve.AxisType = AxisType.LeftMiddle;
            this.m_int_ScaleWidth = this.m_cce_Curve.ScaleWidth;
            this.m_int_ScaleHeight = this.m_cce_Curve.ScaleHeight;
            this.m_cce_Curve.Margin = 10;

            this.m_int_MaxTimes = int_MaxTimes;
            this.m_bln_UaTimesSelected = new bool[int_MaxTimes];
            this.m_flt_UaPhase = new float[int_MaxTimes];
            this.m_flt_UaValue = new float[int_MaxTimes];
            this.m_bln_UbTimesSelected = new bool[int_MaxTimes];
            this.m_flt_UbPhase = new float[int_MaxTimes];
            this.m_flt_UbValue = new float[int_MaxTimes];
            this.m_bln_UcTimesSelected = new bool[int_MaxTimes];
            this.m_flt_UcPhase = new float[int_MaxTimes];
            this.m_flt_UcValue = new float[int_MaxTimes];
            this.m_bln_IaTimesSelected = new bool[int_MaxTimes];
            this.m_flt_IaPhase = new float[int_MaxTimes];
            this.m_flt_IaValue = new float[int_MaxTimes];
            this.m_bln_IbTimesSelected = new bool[int_MaxTimes];
            this.m_flt_IbPhase = new float[int_MaxTimes];
            this.m_flt_IbValue = new float[int_MaxTimes];
            this.m_bln_IcTimesSelected = new bool[int_MaxTimes];
            this.m_flt_IcPhase = new float[int_MaxTimes];
            this.m_flt_IcValue = new float[int_MaxTimes];
        }


        //void CCLHarmonious_CheckedChanged(object sender, EventArgs e)
        //{
        //    for (int intInc = 0; intInc < 6; intInc++)
        //        this.m_bln_XXSelected[intInc] = this.m_chk_Selected[intInc].Checked;
        //    this.Draw();
        //}


        #region ------------------------属性---------------------------
        /// <summary>
        /// 谐波最大次数
        /// </summary>
        public int MaxTimes
        {
            get { return this.m_int_MaxTimes; }
            set
            {
                this.m_int_MaxTimes = value;
                Array.Resize(ref this.m_bln_UaTimesSelected, this.m_int_MaxTimes);
                Array.Resize(ref this.m_flt_UaPhase, this.m_int_MaxTimes);
                Array.Resize(ref this.m_flt_UaValue, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_bln_UbTimesSelected, this.m_int_MaxTimes);
                Array.Resize(ref this.m_flt_UbPhase, this.m_int_MaxTimes);
                Array.Resize(ref this.m_flt_UbValue, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_bln_UcTimesSelected, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_flt_UcPhase, this.m_int_MaxTimes);
                Array.Resize(ref this.m_flt_UcValue, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_bln_IaTimesSelected, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_flt_IaPhase, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_flt_IaValue, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_bln_IbTimesSelected, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_flt_IbPhase, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_flt_IbValue, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_bln_IcTimesSelected, this.m_int_MaxTimes);
                Array.Resize(ref this.m_flt_IcPhase, this.m_int_MaxTimes);
                Array.Resize(ref  this.m_flt_IcValue, this.m_int_MaxTimes);
            }
        }

        /// <summary>
        /// 坐标类型
        /// </summary>
        public AxisType AxisType
        {
            get { return this.m_cce_Curve.AxisType; }
            set { this.m_cce_Curve.AxisType = value; }
        }

        /// <summary>
        /// 背景格的宽度
        /// </summary>
        public int CellWidth
        {
            get { return this.m_cce_Curve.CellWidth; }
            set { this.m_cce_Curve.CellWidth = value; }
        }

        /// <summary>
        /// 背景着色
        /// </summary>
        public Color BackColor
        {
            get { return this.m_cce_Curve.BackColor; }
            set { this.m_cce_Curve.BackColor = value; }
        }

        /// <summary>
        /// 背景线着色
        /// </summary>
        public Color BackLineColor
        {
            get { return this.m_cce_Curve.BackLineColor; }
            set { this.m_cce_Curve.BackLineColor = value; }
        }

        /// <summary>
        /// 各相电压电流是否显示
        /// </summary>
        public bool[] XXSelected
        {
            get { return this.m_bln_XXSelected; }
            set { this.m_bln_XXSelected = value; }
        }


        /// <summary>
        /// 边距
        /// </summary>
        public int Margin
        {
            get { return this.m_int_Margin; }
            set
            {
                this.m_int_Margin = value;
                this.m_cce_Curve.Margin = this.m_int_Margin;
            }
        }

        /// <summary>
        /// A相电压各次谐波是否设置
        /// </summary>
        public bool[] UaSelected
        {
            get { return this.m_bln_UaTimesSelected; }
            set { this.m_bln_UaTimesSelected = value; }
        }

        /// <summary>
        /// B相电压各次谐波是否设置
        /// </summary>
        public bool[] UbSelected
        {
            get { return this.m_bln_UbTimesSelected; }
            set { this.m_bln_UbTimesSelected = value; }
        }

        /// <summary>
        /// C相电压各次谐波是否设置
        /// </summary>
        public bool[] UcSelected
        {
            get { return this.m_bln_UcTimesSelected; }
            set { this.m_bln_UcTimesSelected = value; }
        }

        /// <summary>
        /// A相电流各次谐波是否设置
        /// </summary>
        public bool[] IaSelected
        {
            get { return this.m_bln_IaTimesSelected; }
            set { this.m_bln_IaTimesSelected = value; }
        }

        /// <summary>
        /// B相电流各次谐波是否设置
        /// </summary>
        public bool[] IbSelected
        {
            get { return this.m_bln_IbTimesSelected; }
            set { this.m_bln_IbTimesSelected = value; }
        }

        /// <summary>
        /// C相电流各次谐波是否设置
        /// </summary>
        public bool[] IcSelected
        {
            get { return this.m_bln_IcTimesSelected; }
            set { this.m_bln_IcTimesSelected = value; }
        }

        /// <summary>
        /// A相电压各次谐波含量
        /// </summary>
        public float[] UaValue
        {
            get { return this.m_flt_UaValue; }
            set { this.m_flt_UaValue = value; }
        }

        /// <summary>
        /// B相电压各次谐波含量
        /// </summary>
        public float[] UbValue
        {
            get { return this.m_flt_UbValue; }
            set { this.m_flt_UbValue = value; }
        }

        /// <summary>
        /// C相电压各次谐波含量
        /// </summary>
        public float[] UcValue
        {
            get { return this.m_flt_UcValue; }
            set { this.m_flt_UcValue = value; }
        }

        /// <summary>
        /// A相电流各次谐波含量
        /// </summary>
        public float[] IaValue
        {
            get { return this.m_flt_IaValue; }
            set { this.m_flt_IaValue = value; }
        }

        /// <summary>
        /// B相电流各次谐波含量
        /// </summary>
        public float[] IbValue
        {
            get { return this.m_flt_IbValue; }
            set { this.m_flt_IbValue = value; }
        }

        /// <summary>
        /// C相电流各次谐波含量
        /// </summary>
        public float[] IcValue
        {
            get { return this.m_flt_IcValue; }
            set { this.m_flt_IcValue = value; }
        }

        /// <summary>
        /// A相电压各次谐波相位
        /// </summary>
        public float[] UaPhase
        {
            get { return this.m_flt_UaPhase; }
            set { this.m_flt_UaPhase = value; }
        }

        /// <summary>
        /// B相电压各次谐波相位
        /// </summary>
        public float[] UbPhase
        {
            get { return this.m_flt_UbPhase; }
            set { this.m_flt_UbPhase = value; }
        }

        /// <summary>
        /// C相电压各次谐波相位
        /// </summary>
        public float[] UcPhase
        {
            get { return this.m_flt_UcPhase; }
            set { this.m_flt_UcPhase = value; }
        }

        /// <summary>
        /// A相电流各次谐波相位
        /// </summary>
        public float[] IaPhase
        {
            get { return this.m_flt_IaPhase; }
            set { this.m_flt_IaPhase = value; }
        }

        /// <summary>
        /// B相电流各次谐波相位
        /// </summary>
        public float[] IbPhase
        {
            get { return this.m_flt_IbPhase; }
            set { this.m_flt_IbPhase = value; }
        }

        /// <summary>
        /// C相电流各次谐波相位
        /// </summary>
        public float[] IcPhase
        {
            get { return this.m_flt_IcPhase; }
            set { this.m_flt_IcPhase = value; }
        }

        #endregion


        /// <summary>
        /// 在基波上迭加谐波含量
        /// </summary>
        /// <param name="int_X">X轴</param>
        /// <param name="dbl_YY">基波数据</param>
        /// <param name="int_Times">谐波次数</param>
        /// <param name="flt_Content">谐波含量</param>
        /// <param name="flt_Phase">谐波相位</param>
        /// <returns></returns>
        private Double SpliceHarmonious(int int_X, Double dbl_YY, int int_Times, float flt_Content, float flt_Phase)
        {
            Double dbl_TmpPhase;
            Double dbl_TmpYY;
            dbl_TmpPhase = 2 * int_Times * Math.PI * int_X / this.m_int_ScaleWidth;
            dbl_TmpYY = Math.Sin(dbl_TmpPhase + flt_Phase);
            dbl_TmpYY = (1 - dbl_TmpYY * this.m_flt_Scope ) * this.m_int_ScaleHeight * flt_Content;
            return dbl_TmpYY + dbl_YY;
        }

        /// <summary>
        /// 计算谐波迭加谐波含量后的线点
        /// </summary>
        /// <param name="flt_XPhase">相移角度</param>
        /// <param name="bln_Selected">各次谐波是否迭加</param>
        /// <param name="flt_HValue">各次谐波含量</param>
        /// <param name="flt_HPhase">各次谐波相位</param>
        /// <returns></returns>
        private List<PointF> GetHarmonious(float flt_XPhase,   bool[] bln_Selected, float[] flt_HValue, float[] flt_HPhase)
        {
            List<PointF> lst_PointF = new List<PointF>();
            double dbl_StartX = 0;
            for (int int_Inc = 1; int_Inc < this.m_int_ScaleWidth; int_Inc++)    //按宽度来画
            {
                //----------------2π----
                double dbl_Phase = 2 * Math.PI * int_Inc / this.m_int_ScaleWidth;
                double dbl_Value = Math.Sin(dbl_Phase + flt_XPhase);    //当前角度+相位角度(区分相)
                dbl_Value = (1 - dbl_Value * this.m_flt_Scope) * (this.m_int_ScaleHeight / 2);
                if (int_Inc == 1) dbl_StartX = dbl_Value;                  //保存起点
                for (int int_Inb = 1; int_Inb < this.m_int_MaxTimes; int_Inb++)         //谐波次数
                {
                    if (bln_Selected[int_Inb])      //是否叠加
                        dbl_Value = SpliceHarmonious(int_Inc, dbl_Value, int_Inb + 1, flt_HValue[int_Inb], flt_HPhase[int_Inb]);
                }
                if (int_Inc == 1) dbl_StartX = dbl_Value - dbl_StartX;
                lst_PointF.Add(new PointF(int_Inc, (float)dbl_Value + m_int_Margin - (float)dbl_StartX));
            }
            return lst_PointF;
        }

        /// <summary>
        /// 画加谐波
        /// </summary>
        public void Draw()
        {

            this.m_cce_Curve.ShowPI = true;
            this.m_cce_Curve.AxisType = AxisType.LeftMiddle;

            this.m_int_ScaleWidth = this.m_cce_Curve.ScaleWidth;
            this.m_int_ScaleHeight = this.m_cce_Curve.ScaleHeight;
            this.m_cce_Curve.Margin = 10;

            //if (this.m_cce_Curve.Lines.Count != 6)
            //{
                this.m_cce_Curve.Lines.Clear();
                this.m_cce_Curve.Lines.Add(6);
            //}


            this.m_flt_Scope = 0.5f;
            List<PointF> lst_Point;
            Pen pen_DrawPen ;
            if (this.m_bln_XXSelected[0])           //-----Ua
            {
                pen_DrawPen = new Pen(this.m_clr_UaLineColor, this.m_int_LineWidth);
                lst_Point = GetHarmonious(0, this.m_bln_UaTimesSelected, this.m_flt_UaValue, this.m_flt_UaPhase);//谐波叠加计算
                this.m_cce_Curve.Lines.SetLine(0, lst_Point, pen_DrawPen);
            }
            if (this.m_bln_XXSelected[1])           //-----Ub
            {
                pen_DrawPen = new Pen(this.m_clr_UbLineColor, this.m_int_LineWidth);
                lst_Point = GetHarmonious(180, this.m_bln_UbTimesSelected, this.m_flt_UbValue, this.m_flt_UbPhase);  //谐波叠加计算
                this.m_cce_Curve.Lines.SetLine(1, lst_Point, pen_DrawPen);
            }
            if (this.m_bln_XXSelected[2])           //-----Uc
            {
                pen_DrawPen = new Pen(this.m_clr_UcLineColor, this.m_int_LineWidth);
                lst_Point = GetHarmonious(90, this.m_bln_UcTimesSelected, this.m_flt_UcValue, this.m_flt_UcPhase); //谐波叠加计算
                this.m_cce_Curve.Lines.SetLine(2, lst_Point, pen_DrawPen);
            }

            this.m_flt_Scope = 0.4f;
            if (this.m_bln_XXSelected[3])           //-----Ia
            {
                pen_DrawPen = new Pen(this.m_clr_IaLineColor, this.m_int_LineWidth);
                lst_Point = GetHarmonious(0, this.m_bln_IaTimesSelected, this.m_flt_IaValue, this.m_flt_IaPhase);//谐波叠加计算
                this.m_cce_Curve.Lines.SetLine(3, lst_Point, pen_DrawPen);
            }
            if (this.m_bln_XXSelected[4])           //-----Ib
            {
            pen_DrawPen = new Pen(this.m_clr_IbLineColor, this.m_int_LineWidth);
            lst_Point = GetHarmonious(180, this.m_bln_IbTimesSelected, this.m_flt_IbValue, this.m_flt_IbPhase);//谐波叠加计算
            this.m_cce_Curve.Lines.SetLine(4, lst_Point, pen_DrawPen);
            }
            if (this.m_bln_XXSelected[5])       //-----Ic
            {
                pen_DrawPen = new Pen(this.m_clr_IcLineColor, this.m_int_LineWidth);
                lst_Point = GetHarmonious(90, this.m_bln_IcTimesSelected, this.m_flt_IcValue, this.m_flt_IcPhase);//谐波叠加计算
                this.m_cce_Curve.Lines.SetLine(5, lst_Point, pen_DrawPen);
            }
            this.m_cce_Curve.Draw();

        }


    }


}
