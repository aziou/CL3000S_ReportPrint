using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CLDC_Comm.DrawXieBo
{
    /// <summary>
    /// �๦�ܣ�����ͼ
    /// ��д�ߣ�Kobe.Zhang
    /// ��  �ڣ�2009.01.19
    /// </summary>
    public class CCLCurve
    {

        private System.Windows.Forms.PictureBox m_pic_PicMap;
        private Bitmap m_btp_VectorMap;
        private Graphics m_gph_Map;

        private CCLBackground m_cld_Background;

        private Color m_clr_BackColor = Color.White;
        private Color m_clr_BackLineColor = Color.Silver;
        private int m_int_Margin=10;
        private int m_int_Height;       //��
        private int m_int_Width;        //��

        private AxisType m_enm_AxisType = AxisType.LeftMiddle;
        private int m_int_CellWidth = 5;
        private bool m_bln_ShowPI = false;


        private CCLLineGroup m_clg_Lines;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pic_PicMap"></param>
        public CCLCurve(System.Windows.Forms.PictureBox pic_PicMap)
        {
            this.m_pic_PicMap = pic_PicMap;
            this.m_int_Height = this.m_pic_PicMap.Height;
            this.m_int_Width = this.m_pic_PicMap.Width;

            this.m_btp_VectorMap = new Bitmap(this.m_int_Width, this.m_int_Height);
            this.m_gph_Map = Graphics.FromImage(this.m_btp_VectorMap);
            this.m_cld_Background = new CCLBackground(this.m_gph_Map, this.m_int_Width, this.m_int_Height);
            this.m_clg_Lines = new CCLLineGroup(this.m_gph_Map);
            this.m_clg_Lines.Margin = this.m_int_Margin;
            this.m_cld_Background.Margin = this.m_int_Margin;
            
        }


        /// <summary>
        /// ��������
        /// </summary>
        public AxisType AxisType
        {
            get { return this.m_enm_AxisType; }
            set
            {
                this.m_enm_AxisType = value;
                this.m_cld_Background.AxisType = value;
            }
        }

        /// <summary>
        /// ������Ŀ��
        /// </summary>
        public int CellWidth
        {
            get { return this.m_int_CellWidth ; }
            set
            {
                this.m_int_CellWidth = value;
                this.m_cld_Background.CellWidth = value;
            }
        }

        /// <summary>
        /// ������ɫ
        /// </summary>
        public Color BackColor
        {
            get { return this.m_cld_Background.BackColor; }
            set { this.m_cld_Background.BackColor = value; }
        }

        /// <summary>
        /// ��ͼ��Ч���
        /// </summary>
        public int ScaleWidth
        {
            get { return this.m_int_Width - this.m_int_Margin * 2; }
        }

        /// <summary>
        /// ��ͼ��Ч�߶�
        /// </summary>
        public int ScaleHeight
        {
            get { return (this.m_int_Height - this.m_int_Margin * 2); }
        }

        /// <summary>
        /// ���
        /// </summary>
        public int Width
        {
            get { return this.m_int_Width; }
            set
            {
                if (this.m_int_Width != value)
                {
                    this.m_int_Width = value;
                    ChangeSize(this.m_int_Height, this.m_int_Width);
                }
            }
        }

        /// <summary>
        /// �߶�
        /// </summary>
        public int Height
        {
            get { return this.m_int_Height ; }
            set
            {
                if (this.m_int_Height != value)
                {
                    this.m_int_Height = value;
                    ChangeSize(this.m_int_Height, this.m_int_Width);
                }

            }
        }

        /// <summary>
        /// ��ʾ��
        /// </summary>
        public bool ShowPI
        {
            get { return this.m_bln_ShowPI ; }
            set
            {
                this.m_bln_ShowPI = value;
                this.m_cld_Background.ShowPI = value;
            }
        }


        /// <summary>
        /// ��������ɫ
        /// </summary>
        public Color BackLineColor
        {
            get { return this.m_cld_Background.LineColor; }
            set { this.m_cld_Background.LineColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public CCLLineGroup Lines
        {
            get { return this.m_clg_Lines; }
            set { this.m_clg_Lines = value; }
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
                this.m_cld_Background.Margin = this.m_int_Margin;
            }
        }

        /// <summary>
        /// ������߶�
        /// </summary>
        /// <param name="int_Height"></param>
        /// <param name="int_Width"></param>
        private void ChangeSize(int int_Height, int int_Width)
        {
            this.m_int_Height = int_Height;
            this.m_int_Width = int_Width;
            this.m_btp_VectorMap = new Bitmap(int_Width, int_Height);
            this.m_gph_Map = Graphics.FromImage(this.m_btp_VectorMap);
            this.m_cld_Background = new CCLBackground(this.m_gph_Map, this.m_int_Width, this.m_int_Height);
            this.m_clg_Lines = new CCLLineGroup(this.m_gph_Map);
            //this.Draw();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Draw()
        {
            this.m_cld_Background.Draw();
            this.m_clg_Lines.Margin = this.m_int_Margin;
            this.m_clg_Lines.DrawLines();
            this.m_pic_PicMap.Image = this.m_btp_VectorMap;
            this.m_pic_PicMap.Refresh();
        }

    }
}
