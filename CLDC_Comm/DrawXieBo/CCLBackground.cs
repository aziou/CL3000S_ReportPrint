using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace CLDC_Comm.DrawXieBo
{
    /// <summary>
    /// ������
    /// </summary>
    class CCLBackground
    {
        private Graphics m_gph_Map;
        private AxisType m_enm_AxisType = AxisType.Middle;
        private Color m_BackColor = Color.White;
        private Color m_LineColor = Color.LightGray;
        private int m_int_CellWidth = 5;
        private int m_int_Width,  m_int_Height;
        private int m_int_Margin = 10;
        private CCLLineGroup m_clg_Lines;
        private bool m_bln_ShowPI = false;

        public CCLBackground(Graphics gph_Map, int int_Width, int int_Height)
        {
            this.m_gph_Map = gph_Map;
            this.m_int_Height = int_Height;
            this.m_int_Width = int_Width;
            this.m_clg_Lines = new CCLLineGroup(this.m_gph_Map);
            this.m_clg_Lines.Margin = this.m_int_Margin;
        }

        /// <summary>
        /// �������λ������
        /// </summary>
        public AxisType AxisType
        {
            get { return this.m_enm_AxisType; }
            set { this.m_enm_AxisType = value; }
        }


        /// <summary>
        /// ������Ŀ��
        /// </summary>
        public int CellWidth
        {
            get { return this.m_int_CellWidth; }
            set { this.m_int_CellWidth = value; }
        }

        /// <summary>
        /// ������ɫ
        /// </summary>
        public Color BackColor
        {
            get { return this.m_BackColor; }
            set { this.m_BackColor = value; }
        }

        
        /// <summary>
        /// ��������ɫ
        /// </summary>
        public Color LineColor
        {
            get { return m_LineColor; }
            set { m_LineColor = value; }
        }

        /// <summary>
        /// �߾� 
        /// </summary>
        public int Margin
        {
            get { return this.m_int_Margin; }
            set
            {
                this.m_int_Margin = value;
                this.m_clg_Lines.Margin = this.m_int_Margin;
            }
        }

        /// <summary>
        /// ��ʾ��
        /// </summary>
        public bool ShowPI
        {
            get { return this.m_bln_ShowPI; }
            set { this.m_bln_ShowPI = value; }
        }


        /// <summary>
        /// ������
        /// </summary>
        public void Draw()
        {
            this.m_gph_Map.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen pen_DrawPen = new Pen(this.m_LineColor, 1);
            pen_DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.m_gph_Map.Clear(this.m_BackColor);
            this.m_clg_Lines = new CCLLineGroup(this.m_gph_Map);
            this.m_clg_Lines.Margin = this.m_int_Margin;

            Rectangle rcl_MapArec = new Rectangle(0, this.m_int_Margin, this.m_int_Width - this.m_int_Margin * 2, this.m_int_Height - this.m_int_Margin * 2);
            //this.m_gph_Map.DrawRectangle(pen_DrawPen, rcl_MapArec);
            DrawXAxis(this.m_gph_Map, pen_DrawPen, rcl_MapArec, this.m_enm_AxisType);
            DrawYAxis(this.m_gph_Map, pen_DrawPen, rcl_MapArec, this.m_enm_AxisType);
            this.m_clg_Lines.DrawLines();
        }

        /// <summary>
        /// ��X��
        /// </summary>
        /// <param name="gph_Map">ͼ</param>
        /// <param name="pen_DrawPen">����</param>
        /// <param name="rcl_MapArec">����</param>
        /// <param name="enm_AxisType">��������</param>
        private void DrawXAxis(Graphics gph_Map, Pen pen_DrawPen, Rectangle rcl_MapArec, AxisType enm_AxisType)
        {
            float  flt_X1 = 0;
            float flt_X2 = 0;
            float flt_Y1 = 0;
            float flt_Y2 = 0;

            int int_Len = 0;
            CoordinateType enm_XCoordinate = GetCoordinate(true, enm_AxisType);
            if (enm_XCoordinate == CoordinateType.None) return;
            if (enm_XCoordinate == CoordinateType.Top)      //X���ڶ�
            {
                flt_X1 = rcl_MapArec.X;
                flt_Y1 = rcl_MapArec.X;
                flt_X2 = rcl_MapArec.Width + rcl_MapArec.X;
                flt_Y2 = flt_X1;
                //---------------������------------------
                int_Len = rcl_MapArec.Height / (this.m_int_CellWidth * 5);
                for (int int_Inc = 0; int_Inc < int_Len+1; int_Inc++)
                    this.m_clg_Lines.Add(flt_X1, flt_Y1 + int_Inc * this.m_int_CellWidth * 5, flt_X2, flt_Y2 + int_Inc * this.m_int_CellWidth * 5, pen_DrawPen);
                    
            
            }
            else if (enm_XCoordinate == CoordinateType.Middle)  //X�����м�
            {
                flt_X1 = rcl_MapArec.X;
                flt_Y1 = rcl_MapArec.Y + rcl_MapArec.Height / 2;
                flt_X2 = rcl_MapArec.Width + rcl_MapArec.X;
                flt_Y2 = flt_Y1;
                //---------------������------------------
                int_Len = ((rcl_MapArec.Height / 2) / this.m_int_CellWidth * 5);
                for (int int_Inc = 0; int_Inc < int_Len ; int_Inc++)     //���м������߻�
                {
                    this.m_clg_Lines.Add(flt_X1, flt_Y1 + int_Inc * this.m_int_CellWidth * 5, flt_X2, flt_Y2 + int_Inc * this.m_int_CellWidth * 5, pen_DrawPen);
                    this.m_clg_Lines.Add(flt_X1, flt_Y1 - int_Inc * this.m_int_CellWidth * 5, flt_X2, flt_Y2 - int_Inc * this.m_int_CellWidth * 5, pen_DrawPen);
                }
            }
            else if (enm_XCoordinate == CoordinateType.Bottom)  //X���ڵײ�
            {
                flt_X1 = rcl_MapArec.X;
                flt_Y1 = rcl_MapArec.Height + rcl_MapArec.Y;
                flt_X2 = rcl_MapArec.Width + rcl_MapArec.X;
                flt_Y2 = flt_Y1;
                //---------------������------------------
                int_Len = rcl_MapArec.Height / (this.m_int_CellWidth * 5);
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                    this.m_clg_Lines.Add(flt_X1, flt_Y1 - int_Inc * this.m_int_CellWidth * 5, flt_X2, flt_Y2 - int_Inc * this.m_int_CellWidth * 5, pen_DrawPen);
            }


            //--------------��������-----------------------
            this.m_clg_Lines.Add(flt_X1, flt_Y1, flt_X2, flt_Y2, pen_DrawPen);

            //-----------------����ͷ-------------------------------
            PointF[] pif_AngleLine = new PointF[3];
            pif_AngleLine[0] = new PointF(flt_X2 + 10, flt_Y2 - 5);
            pif_AngleLine[1] = new PointF(flt_X2 + 20, flt_Y2);
            pif_AngleLine[2] = new PointF(flt_X2 + 10, flt_Y2 + 5);
            this.m_gph_Map.FillPolygon(pen_DrawPen.Brush, pif_AngleLine);


            //----------------����̶�-------------------------------------

            CoordinateType enm_YCoordinate = GetCoordinate(false, enm_AxisType);
            if (enm_YCoordinate == CoordinateType.Middle)     //X���ڶ�
            {
                int_Len = ((rcl_MapArec.Width / 2) / this.m_int_CellWidth);
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                {
                    float  flt_LeftY = rcl_MapArec.X + rcl_MapArec.Width / 2 - int_Inc * this.m_int_CellWidth;
                    float  flt_RightY = rcl_MapArec.X + rcl_MapArec.Width / 2 + int_Inc * this.m_int_CellWidth;
                    if (enm_XCoordinate == CoordinateType.Bottom)
                    {
                        this.m_clg_Lines.Add(flt_LeftY, flt_Y1 - 2, flt_LeftY, flt_Y1, pen_DrawPen);    //���м�����������
                        this.m_clg_Lines.Add(flt_RightY, flt_Y1 - 2, flt_RightY, flt_Y1, pen_DrawPen);
                    }
                    else if (enm_XCoordinate == CoordinateType.Top)
                    {
                        this.m_clg_Lines.Add(flt_LeftY, flt_Y1, flt_LeftY, flt_Y1 + 2, pen_DrawPen);
                        this.m_clg_Lines.Add(flt_RightY, flt_Y1, flt_RightY, flt_Y1 + 2, pen_DrawPen);
                    }
                    else
                    {
                        this.m_clg_Lines.Add(flt_LeftY, flt_Y1 - 2, flt_LeftY, flt_Y1 + 2, pen_DrawPen);
                        this.m_clg_Lines.Add(flt_RightY, flt_Y1 - 2, flt_RightY, flt_Y1 + 2, pen_DrawPen);
                    }
                }
            }
            else if (enm_YCoordinate == CoordinateType.Right)     //X���ڶ�
            {
                int_Len = rcl_MapArec.Width / this.m_int_CellWidth;
                if (enm_XCoordinate == CoordinateType.Bottom)
                {
                    for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                    {
                        float flt_LeftY = rcl_MapArec.X + rcl_MapArec.Width - int_Inc * this.m_int_CellWidth;
                        this.m_clg_Lines.Add(flt_LeftY, flt_Y1 - 2, flt_LeftY, flt_Y1, pen_DrawPen);    //���м�����������
                    }
                }
                else if (enm_XCoordinate == CoordinateType.Top)
                {
                    for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                    {
                        float flt_LeftY = rcl_MapArec.X + rcl_MapArec.Width - int_Inc * this.m_int_CellWidth;
                        this.m_clg_Lines.Add(flt_LeftY, flt_Y1, flt_LeftY, flt_Y1 + 2, pen_DrawPen);   //���м�����������
                    }
                }
                else
                {
                    for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                    {
                        float flt_LeftY = rcl_MapArec.X + rcl_MapArec.Width - int_Inc * this.m_int_CellWidth;
                        this.m_clg_Lines.Add(flt_LeftY, flt_Y1 - 2, flt_LeftY, flt_Y1 + 2, pen_DrawPen);
                    }
                }
            }
            else
            {
                int_Len = rcl_MapArec.Width / this.m_int_CellWidth;
                int int_Count = int_Len / 4;
                string[] str_Name = new string[] { "1/2��", "��", "3/2��", "2��" };
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                {
                    float flt_LeftY =this.m_int_Margin + rcl_MapArec.X + int_Inc * this.m_int_CellWidth;
                    float flt_YY1 = flt_Y1 - 2;
                    float flt_YY2 = flt_Y1 + 2;
                    if (int_Inc % int_Count == int_Count - 1 && this.m_bln_ShowPI)
                    {
                        flt_YY1 = flt_Y1 - 6;
                        flt_YY2 = flt_Y1 + 6;
                        this.m_gph_Map.DrawString(str_Name[int_Inc / int_Count], new Font("����", 10), pen_DrawPen.Brush, flt_LeftY, flt_YY2 + 2);
                    }
                    if (enm_XCoordinate == CoordinateType.Bottom)
                        flt_YY2=flt_Y1;
                    else if (enm_XCoordinate == CoordinateType.Top)
                        flt_YY1=flt_Y1;
                    this.m_clg_Lines.Add(flt_LeftY, flt_YY1, flt_LeftY, flt_YY2, pen_DrawPen);
                }
               
            }
        }

        /// <summary>
        /// ��Y��
        /// </summary>
        /// <param name="gph_Map">ͼ</param>
        /// <param name="pen_DrawPen">����</param>
        /// <param name="rcl_MapArec">����</param>
        /// <param name="enm_AxisType">��������</param>
        private void DrawYAxis(Graphics gph_Map, Pen pen_DrawPen, Rectangle rcl_MapArec, AxisType enm_AxisType)
        {
            float flt_X1 = 0;
            float flt_X2 = 0;
            float flt_Y1 = 0;
            float flt_Y2 = 0;

            int int_Len = 0;
            CoordinateType enm_YCoordinate = GetCoordinate(false, enm_AxisType);
            if (enm_YCoordinate == CoordinateType.None) return;
            if (enm_YCoordinate == CoordinateType.Left)      //Y������
            {
                flt_X1 = rcl_MapArec.X;
                flt_Y1 = rcl_MapArec.Y;
                flt_X2 = rcl_MapArec.X;
                flt_Y2 = rcl_MapArec.Height + rcl_MapArec.Y;
                //----------------------������̶�-----------------------
                int_Len = rcl_MapArec.Width / (this.m_int_CellWidth * 5);
                for (int int_Inc = 0; int_Inc < int_Len + 1; int_Inc++)
                    this.m_clg_Lines.Add(flt_X1 + int_Inc * this.m_int_CellWidth * 5, flt_Y1, flt_X2 + int_Inc * this.m_int_CellWidth * 5, flt_Y2, pen_DrawPen);
            }
            else if (enm_YCoordinate == CoordinateType.Middle)  //Y�����м�
            {
                flt_X1 = rcl_MapArec.X + rcl_MapArec.Width / 2;
                flt_Y1 = rcl_MapArec.Y;
                flt_X2 = flt_X1;
                flt_Y2 = rcl_MapArec.Height + rcl_MapArec.Y;
                //----------------------������̶�-----------------------
                int_Len = ((rcl_MapArec.Width / 2) / this.m_int_CellWidth * 5);
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                {
                    this.m_clg_Lines.Add(flt_X1 + int_Inc * this.m_int_CellWidth * 5, flt_Y1, flt_X2 + int_Inc * this.m_int_CellWidth * 5, flt_Y2, pen_DrawPen);
                    this.m_clg_Lines.Add(flt_X1 - int_Inc * this.m_int_CellWidth * 5, flt_Y1, flt_X2 - int_Inc * this.m_int_CellWidth * 5, flt_Y2, pen_DrawPen);
                }
            }
            else if (enm_YCoordinate == CoordinateType.Right)  //Y������
            {
                flt_X1 = rcl_MapArec.X + rcl_MapArec.Width;
                flt_Y1 = rcl_MapArec.Y;
                flt_X2 = flt_X1;
                flt_Y2 = rcl_MapArec.Height + rcl_MapArec.Y;
                //----------------------������̶�-----------------------
                int_Len = rcl_MapArec.Width / (this.m_int_CellWidth * 5);
                for (int int_Inc = 0; int_Inc < int_Len + 1; int_Inc++)
                    this.m_clg_Lines.Add(flt_X1 - int_Inc * this.m_int_CellWidth * 5, flt_Y1, flt_X2 - int_Inc * this.m_int_CellWidth * 5, flt_Y2, pen_DrawPen);
            }

            //----------------------������-----------------------
            this.m_clg_Lines.Add(flt_X1, flt_Y1, flt_X2, flt_Y2, pen_DrawPen);
            //---------------------����ͷ-------------------------------
            PointF[] pif_AngleLine = new PointF[3];
            pif_AngleLine[0] = new PointF(flt_X1 + 10, flt_Y1 - 5);
            pif_AngleLine[1] = new PointF(flt_X1 + 15, flt_Y1 + 5);
            pif_AngleLine[2] = new PointF(flt_X1 + 5, flt_Y1 + 5);
            this.m_gph_Map.FillPolygon(pen_DrawPen.Brush, pif_AngleLine);

            //--------------------����������-------------------------
            CoordinateType enm_XCoordinate = GetCoordinate(true, enm_AxisType);
            if (enm_XCoordinate == CoordinateType.Middle)     
            {
                int_Len = ((rcl_MapArec.Height  / 2) / this.m_int_CellWidth);
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                {
                    float flt_TopX = rcl_MapArec.Y + rcl_MapArec.Height / 2 - int_Inc * this.m_int_CellWidth;
                    float flt_BottomX = rcl_MapArec.Y + rcl_MapArec.Height / 2 + int_Inc * this.m_int_CellWidth;
                    if (enm_YCoordinate == CoordinateType.Right)
                    {
                        this.m_clg_Lines.Add(flt_X1 - 2, flt_TopX, flt_X1, flt_TopX, pen_DrawPen);    //���м�����������
                        this.m_clg_Lines.Add(flt_X1 - 2, flt_BottomX, flt_X1, flt_BottomX, pen_DrawPen);
                    }
                    else if (enm_YCoordinate == CoordinateType.Left)
                    {
                        this.m_clg_Lines.Add(flt_X1, flt_TopX, flt_X1 + 2, flt_TopX, pen_DrawPen);    //���м�����������
                        this.m_clg_Lines.Add(flt_X1, flt_BottomX, flt_X1 + 2, flt_BottomX, pen_DrawPen);
                    }
                    else
                    {
                        this.m_clg_Lines.Add(flt_X1 - 2, flt_TopX, flt_X1 + 2, flt_TopX, pen_DrawPen);    //���м�����������
                        this.m_clg_Lines.Add(flt_X1 - 2, flt_BottomX, flt_X1 + 2, flt_BottomX, pen_DrawPen);
                    }
                }
            }
            else if (enm_XCoordinate == CoordinateType.Top)    
            {
                int_Len = rcl_MapArec.Height  / this.m_int_CellWidth;
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                {
                    float flt_TopX = rcl_MapArec.Y+ int_Inc * this.m_int_CellWidth;
                    if (enm_YCoordinate == CoordinateType.Right)
                        this.m_clg_Lines.Add(flt_X1 - 2, flt_TopX, flt_X1, flt_TopX, pen_DrawPen);    //���м�����������
                    else if (enm_YCoordinate == CoordinateType.Left)
                        this.m_clg_Lines.Add(flt_X1, flt_TopX, flt_X1 + 2, flt_TopX, pen_DrawPen); 
                    else
                        this.m_clg_Lines.Add(flt_X1-2, flt_TopX, flt_X1 + 2, flt_TopX, pen_DrawPen); 
                }
            }
            else
            {
                int_Len = rcl_MapArec.Height  / this.m_int_CellWidth;
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                {
                    float flt_TopX = rcl_MapArec.Y + int_Inc * this.m_int_CellWidth;
                    if (enm_YCoordinate == CoordinateType.Right)
                        this.m_clg_Lines.Add(flt_X1 - 2, flt_TopX, flt_X1, flt_TopX, pen_DrawPen);    //���м�����������
                    else if (enm_YCoordinate == CoordinateType.Left)
                        this.m_clg_Lines.Add(flt_X1, flt_TopX, flt_X1 + 2, flt_TopX, pen_DrawPen);
                    else
                        this.m_clg_Lines.Add(flt_X1 - 2, flt_TopX, flt_X1 + 2, flt_TopX, pen_DrawPen); 
                }
            }
        }

        /// <summary>
        /// ���귽��
        /// </summary>
        /// <param name="bln_IsXAxis">��X��</param>
        /// <param name="enm_AxisType">��������</param>
        /// <returns></returns>
        private CoordinateType GetCoordinate(bool bln_IsXAxis, AxisType enm_AxisType)
        {
            if (bln_IsXAxis)
            {
                if (enm_AxisType == AxisType.LeftTop                    //X���ڶ���
                || enm_AxisType == AxisType.MiddleTop
                || enm_AxisType == AxisType.RightTop)
                {
                    return CoordinateType.Top;
                }
                else if (enm_AxisType == AxisType.Middle                    //X�����м�
                || enm_AxisType == AxisType.LeftMiddle
                || enm_AxisType == AxisType.RightMiddle)
                {
                    return CoordinateType.Middle;
                }
                else if (enm_AxisType == AxisType.LeftBottom                      //X���ڲ�
               || enm_AxisType == AxisType.MiddleBottom
               || enm_AxisType == AxisType.RightBottom)
                {
                    return CoordinateType.Bottom;
                }
            }
            else
            {
                if (enm_AxisType == AxisType.LeftTop                    //X���ڶ���
                || enm_AxisType == AxisType.LeftMiddle
                || enm_AxisType == AxisType.LeftBottom)
                {
                    return CoordinateType.Left;
                }
                else if (enm_AxisType == AxisType.Middle                    //X�����м�
                || enm_AxisType == AxisType.MiddleBottom
                || enm_AxisType == AxisType.MiddleTop)
                {
                    return CoordinateType.Middle ;
                }
                else if (enm_AxisType == AxisType.RightTop                        //X���ڲ�
               || enm_AxisType == AxisType.RightMiddle
               || enm_AxisType == AxisType.RightBottom)
                {
                    return CoordinateType.Right;
                }
            }
            return CoordinateType.None;
        }
    }
}
