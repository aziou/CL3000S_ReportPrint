using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CLDC_Comm.DrawXieBo
{
    /// <summary>
    /// ����
    /// </summary>
    public class CCLLine
    {
        
        private Graphics m_gph_Map ;              //����
        private Color m_clr_LineColor = Color.Red;     //��ɫ
        private Pen m_pen_DrawPen ;
        private int m_int_MaxPoint = 0;             //
        private List<PointF> m_lst_XYPoint = new List<PointF>();
        private int m_int_Margin = 0;

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="gph_Map">��ͼ��</param>
        public CCLLine(Graphics gph_Map)
        {
            this.m_gph_Map = gph_Map;
            this.m_pen_DrawPen = new Pen(this.m_clr_LineColor, 1);
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="gph_Map">��ͼ��</param>
        /// <param name="lst_XY">���㼯��</param>
        public CCLLine(Graphics gph_Map, List<PointF> lst_XY)
        {
            this.m_gph_Map = gph_Map;
            m_lst_XYPoint = lst_XY;
            this.m_pen_DrawPen = new Pen(this.m_clr_LineColor, 1);
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="gph_Map">��ͼ��</param>
        /// <param name="pit_XY">��������</param>
        public CCLLine(Graphics gph_Map, PointF[] pit_XY)
        {
            this.m_gph_Map = gph_Map;
            for (int int_Inc = 0; int_Inc < pit_XY.Length; int_Inc++)
                Add(pit_XY[int_Inc]);
            this.m_pen_DrawPen = new Pen(this.m_clr_LineColor, 1);
        }

        /// <summary>
        /// �߾�
        /// </summary>
        public int Margin
        {
            get { return this.m_int_Margin; }
            set { this.m_int_Margin = value; }
        }
        


        /// <summary>
        /// ����
        /// </summary>
        public Graphics  ChartMap
        {
            get { return this.m_gph_Map; }
            set { this.m_gph_Map = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Pen DrawPen
        {
            get { return this.m_pen_DrawPen; }
            set { this.m_pen_DrawPen = value; }
        }

        /// <summary>
        /// ��ɫ
        /// </summary>
        public Color LineColor
        {
            get { return this.m_clr_LineColor; }
            set { this.m_clr_LineColor = value; }

        }

        /// <summary>
        /// ��󻭵�
        /// </summary>
        public int MaxPoint
        {
            get { return this.m_int_MaxPoint; }
            set { this.m_int_MaxPoint = value; }
        }
        

        /// <summary>
        /// ���ӻ���
        /// </summary>
        /// <param name="pit_XY">����</param>
        public void Add(PointF pit_XY)
        {
            if (this.m_int_MaxPoint > 0)        //�����ָ�����
            {
                if (this.m_lst_XYPoint.Count+1 >= this.m_int_MaxPoint)
                {
                    this.m_lst_XYPoint.Remove(this.m_lst_XYPoint[0]);
                }
            }
            this.m_lst_XYPoint.Add(pit_XY);
        }

        /// <summary>
        /// ����
        /// </summary>
        public void DrawLine()
        {
            
            for (int int_Inc = 1; int_Inc < this.m_lst_XYPoint.Count; int_Inc++)
            {
                PointF pif_Start = this.m_lst_XYPoint[int_Inc - 1];
                PointF pif_End = this.m_lst_XYPoint[int_Inc];
                pif_Start.X += this.m_int_Margin;
                pif_End.X += this.m_int_Margin;
                this.m_gph_Map.DrawLine(this.m_pen_DrawPen, pif_Start, pif_End);
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pit_XY">��������</param>
        public void DrawLine(PointF[] pit_XY)
        {
            for (int int_Inc = 0; int_Inc < pit_XY.Length; int_Inc++)
            {
                Add(pit_XY[int_Inc]);
            }
            DrawLine();
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="lst_XY">��������</param>
        public void DrawLine(List<PointF> lst_XY)
        {
            this.m_lst_XYPoint = lst_XY;
            DrawLine();
        }

    }
}
