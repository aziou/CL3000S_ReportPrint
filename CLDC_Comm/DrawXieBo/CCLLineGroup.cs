using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace CLDC_Comm.DrawXieBo
{
    /// <summary>
    /// 
    /// </summary>
    public class CCLLineGroup : CollectionBase
    {
        private Graphics m_gph_Map;
        private int m_int_Margin = 0;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gph_Map"></param>
        public CCLLineGroup(Graphics gph_Map)
        {
            this.m_gph_Map = gph_Map;
            
        }


        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="Index">序号</param>
        /// <returns></returns>
        public CCLLine this[int Index]
        {
            get { return (CCLLine)this.List[Index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="int_Index"></param>
        /// <param name="lst_XY"></param>
        /// <param name="pen_DrawPen"></param>
        public void SetLine(int int_Index, List<PointF> lst_XY, Pen pen_DrawPen)
        {
            if (this.Count > int_Index && int_Index >= 0)
            {
                CCLLine cle_Tmp = (CCLLine)(this.List[int_Index]);
                cle_Tmp.DrawPen = pen_DrawPen;
                cle_Tmp.DrawLine(lst_XY);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public int Margin
        {
            get { return this.m_int_Margin; }
            set
            {
                this.m_int_Margin = value;
                for (int int_Inc = 0; int_Inc < this.List.Count; int_Inc++)
                {
                    CCLLine cle_Tmp = (CCLLine)(this.List[int_Inc]);
                    cle_Tmp.Margin = this.m_int_Margin;
                }
            }
        }


        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="int_Count">线条数</param>
        public void Add(int int_Count)
        {
            for (int int_Inc = 0; int_Inc < int_Count; int_Inc++)
            {
                CCLLine cle_Line = new CCLLine(m_gph_Map);
                this.List.Add(cle_Line);
            }
        }


        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="cle_Line">新增的表格行</param>
        public void Add(CCLLine cle_Line)
        {
            cle_Line.ChartMap = m_gph_Map;
            this.List.Add(cle_Line);

        }

        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="lst_XY"></param>
        public void Add(List<PointF> lst_XY)
        {
            CCLLine cle_Line = new CCLLine(this.m_gph_Map, lst_XY);
            this.List.Add(cle_Line);
        }

        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="flt_X1">起点X位置</param>
        /// <param name="flt_Y1">起点Y位置</param>
        /// <param name="flt_X2">结束X位置</param>
        /// <param name="flt_Y2">结束Y位置</param>
        public void Add(float flt_X1, float flt_Y1, float flt_X2, float flt_Y2)
        {
            PointF[] pif_Point = new PointF[2];
            pif_Point[0] = new PointF(flt_X1, flt_Y1);
            pif_Point[1] = new PointF(flt_X2, flt_Y2);
            Add(pif_Point);
        }

        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="flt_X1">起点X位置</param>
        /// <param name="flt_Y1">起点Y位置</param>
        /// <param name="flt_X2">结束X位置</param>
        /// <param name="flt_Y2">结束Y位置</param>
        /// <param name="pen_DrawPen">画笔</param>
        public void Add(float flt_X1, float flt_Y1, float flt_X2, float flt_Y2, Pen pen_DrawPen)
        {
            PointF[] pif_Point = new PointF[2];
            pif_Point[0] = new PointF(flt_X1, flt_Y1);
            pif_Point[1] = new PointF(flt_X2, flt_Y2);
            Add(pif_Point, pen_DrawPen);
        }


        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="pit_XY"></param>
        public void Add(PointF[] pit_XY)
        {
            CCLLine cle_Line = new CCLLine(this.m_gph_Map, pit_XY);
            cle_Line.Margin = this.m_int_Margin;
            this.List.Add(cle_Line);
        }

        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="lst_XY"></param>
        /// <param name="pen_DrawPen"></param>
        public void Add(List<PointF> lst_XY, Pen pen_DrawPen)
        {
            CCLLine cle_Line = new CCLLine(this.m_gph_Map, lst_XY);
            cle_Line.Margin = this.m_int_Margin;
            cle_Line.DrawPen = pen_DrawPen;
            this.List.Add(cle_Line);
        }

        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="pit_XY"></param>
        /// <param name="pen_DrawPen"></param>
        public void Add(PointF[] pit_XY, Pen pen_DrawPen)
        {
            CCLLine cle_Line = new CCLLine(this.m_gph_Map, pit_XY);
            cle_Line.DrawPen = pen_DrawPen;
            cle_Line.Margin = this.m_int_Margin;
            this.List.Add(cle_Line);
        }




        /// <summary>
        /// 移除线
        /// </summary>
        /// <param name="int_Index"></param>
        public void Remove(int int_Index)
        {
            this.List.Remove(this.List[int_Index]);
        }

        /// <summary>
        /// 画线
        /// </summary>
        public void DrawLines()
        {
            for (int int_Inc = 0; int_Inc < this.List.Count; int_Inc++)
            {
                CCLLine cle_Tmp = (CCLLine)(this.List[int_Inc]);
                cle_Tmp.Margin = this.m_int_Margin;
                cle_Tmp.DrawLine();
            }
        }



    }
}
