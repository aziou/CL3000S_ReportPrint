
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CLDC_DataCore.Function
{
    /// 
    /// Summary description for BitmapRegion. 
    /// 
    public class BitmapRegion
    {
        /// <summary>
        /// 
        /// </summary>
        public BitmapRegion()
        { }


        /// 
        /// Create and apply the region on the supplied control
        /// ����֧��λͼ����Ŀؼ���Ŀǰ��button��form��
        /// 
        /// The Control object to apply the region to�ؼ� 
        /// The Bitmap object to create the region fromλͼ 
        public static void CreateControlRegion(Control control, Bitmap bitmap)
        {
            // Return if control and bitmap are null
            //�ж��Ƿ���ڿؼ���λͼ
            if (control == null || bitmap == null)
                return;

            // Set our control''s size to be the same as the bitmap
            //���ÿؼ���СΪλͼ��С
            control.Width = bitmap.Width;
            control.Height = bitmap.Height;
            // Check if we are dealing with Form here 
            //���ؼ���formʱ
            if (control is System.Windows.Forms.Form)
            {
                // Cast to a Form object
                //ǿ��ת��ΪFORM
                Form form = (Form)control;
                // Set our form''s size to be a little larger that the bitmap just 
                // in case the form''s border style is not set to none in the first place 
                //��FORM�ı߽�FormBorderStyle��ΪNONEʱ��Ӧ��FORM�Ĵ�С���óɱ�λͼ��С�Դ�һ��
                form.Width = control.Width;
                form.Height = control.Height;
                // No border 
                //û�б߽�
                form.FormBorderStyle = FormBorderStyle.None;
                // Set bitmap as the background image 
                //��λͼ���óɴ��屳��ͼƬ
                form.BackgroundImage = bitmap;
                // Calculate the graphics path based on the bitmap supplied 
                //����λͼ�в�͸�����ֵı߽�
                GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);
                // Apply new region 
                //Ӧ���µ�����
                form.Region = new Region(graphicsPath);
            }
            // Check if we are dealing with Button here 
            //���ؼ���buttonʱ
            else if (control is System.Windows.Forms.Button)
            {
                // Cast to a button object 
                //ǿ��ת��Ϊ button
                Button button = (Button)control;
                // Do not show button text 
                //����ʾbutton text
                button.Text = "";

                // Change cursor to hand when over button 
                //�ı� cursor��style
                button.Cursor = Cursors.Hand;
                // Set background image of button 
                //����button�ı���ͼƬ
                button.BackgroundImage = bitmap;

                // Calculate the graphics path based on the bitmap supplied 
                //����λͼ�в�͸�����ֵı߽�
                GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap);
                // Apply new region 
                //Ӧ���µ�����
                button.Region = new Region(graphicsPath);
            }
        }

        /// 
        /// Calculate the graphics path that representing the figure in the bitmap 
        /// excluding the transparent color which is the top left pixel. 
        /// //����λͼ�в�͸�����ֵı߽�
        /// 
        /// The Bitmap object to calculate our graphics path from 
        /// Calculated graphics path 
        private static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap)
        {
            // Create GraphicsPath for our bitmap calculation 
            //���� GraphicsPath
            GraphicsPath graphicsPath = new GraphicsPath();
            // Use the top left pixel as our transparent color 
            //ʹ�����Ͻǵ�һ�����ɫ��Ϊ����͸��ɫ
            Color colorTransparent = bitmap.GetPixel(0, 0);
            // This is to store the column value where an opaque pixel is first found. 
            // This value will determine where we start scanning for trailing opaque pixels.
            //��һ���ҵ����X
            int colOpaquePixel = 0;
            // Go through all rows (Y axis) 
            // ƫ�������У�Y����
            for (int row = 0; row < bitmap.Height; row++)
            {
                // Reset value 
                //����
                colOpaquePixel = 0;
                // Go through all columns (X axis) 
                //ƫ�������У�X����
                for (int col = 0; col < bitmap.Width; col++)
                {
                    // If this is an opaque pixel, mark it and search for anymore trailing behind 
                    //����ǲ���Ҫ͸������ĵ����ǣ�Ȼ�����ƫ��
                    if (bitmap.GetPixel(col, row) != colorTransparent)
                    {
                        // Opaque pixel found, mark current position
                        //��¼��ǰ
                        colOpaquePixel = col;
                        // Create another variable to set the current pixel position 
                        //�����±�������¼��ǰ��
                        int colNext = col;
                        // Starting from current found opaque pixel, search for anymore opaque pixels 
                        // trailing behind, until a transparent   pixel is found or minimum width is reached 
                        //���ҵ��Ĳ�͸���㿪ʼ������Ѱ�Ҳ�͸����,һֱ���ҵ�����ﵽͼƬ��� 
                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row) == colorTransparent)
                                break;
                        // Form a rectangle for line of opaque   pixels found and add it to our graphics path 
                        //����͸����ӵ�graphics path
                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                        // No need to scan the line of opaque pixels just found 
                        col = colNext;
                    }
                }
            }
            // Return calculated graphics path 
            return graphicsPath;
        }
    }
}
