#region Using directives
using System;
using System.Drawing;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using System.Drawing.Drawing2D;
using System.ComponentModel;
#endregion

namespace CLDC_Comm.ExtendedPanel
{
    /// <summary>
    /// Delegate for the dragging event.By dragging the caption you tell the panel to reposition itself
    /// </summary>
    /// <param name="sender">the object delegating the process, an instance of a CaptionCtrl</param>
    /// <param name="e">instance of an object holding the event data</param>
    public delegate void CaptionDraggingEvent(object sender, CaptionDraggingEventArgs e);

    public delegate void EventCheckedChanged(object sender, EventArgs e);

    public delegate void EventMoveOver(object sender,EventArgs e);

    public delegate void EventMouseMove(object sender,MouseEventArgs e);

    public delegate void EventPanelClose(object sender,EventArgs e);

    /// <summary>
    /// This defines the caption bar used for the ExtendedPanel
    /// </summary>
    internal class CaptionCtrl : CornerCtrl
    {
        #region Members

        public event EventMoveOver MoveOver = null;

        public event EventPanelClose RaiseClose = null;

        private CheckBox CheckItem = null;

        /// <summary>
        /// 是否显示排序序号
        /// </summary>
        private bool _ShowSortID = false;

        /// <summary>
        /// 排序ID
        /// </summary>
        private int _ID = 1;
        /// <summary>
        ///  是否显示关闭按钮
        /// </summary>
        private bool _ShowClose = false;

        /// <summary>
        /// 是否显示CheckBox
        /// </summary>
        private bool _ShowCheckBox = true;
        /// <summary>
        /// Boolean value specifing if the mouse button is down
        /// </summary>
        private bool mouseDown = false;

        /// <summary>
        /// 
        /// </summary>
        private int mouseX = 0;
        private int mouseY = 0;

        /// <summary>
        /// Instance of the string being drawn 
        /// </summary>
        private string text = "Caption";

        /// <summary>
        /// 
        /// </summary>
        private BrushType brushType = BrushType.Gradient;

        /// <summary>
        /// 
        /// </summary>
        private Color colorOne = Color.White;

        /// <summary>
        /// 
        /// </summary>
        private Color colorTwo = Color.FromArgb(155, Color.Orange);

        /// <summary>
        /// 
        /// </summary>
        private Color textColor = Color.Black;
        /// <summary>
        /// Instance of the image to be displayed near the text of the text
        /// </summary>
        private Icon captionIcon = null;

        /// <summary>
        /// 
        /// </summary>
        private DirectionCtrl directionCtrl = null;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 
        /// </summary>
        private Brush brush = null;

        /// <summary>
        /// 
        /// </summary>
        public event CaptionDraggingEvent Dragging = null;

        public event EventCheckedChanged CheckedChanged = null;

        #endregion

        #region ctor

        public CaptionCtrl()
        {
            InitializeComponent();

            //set up the mouse move event in case the style is to move the parent control
            this.MouseMove += new MouseEventHandler(OnMouseMoveEvent);

            //set up the mouse down event
            this.MouseDown += new MouseEventHandler(OnMouseDownEvent);

            //set up the mouse up event
            this.MouseUp += new MouseEventHandler(OnMouseUpEvent);

            //set up the brush
            InitializeBrush();

        }


        #endregion

        #region Properties

        [Browsable(false)]
        public Color DirectionCtrlColor
        {
            get
            {
                return directionCtrl.Color;
            }
            set
            {
                directionCtrl.Color = value;
            }
        }


        [Browsable(false)]
        public Color DirectionCtrlHoverColor
        {
            get
            {
                return directionCtrl.HoverColor;
            }
            set
            {
                directionCtrl.HoverColor = value;
            }
        }


        [Browsable(false)]
        [Category("外观")]
        [Description("设置或获取是否显示排序序号")]
        public bool ShowSortID
        {
            get { return _ShowSortID; }
            set { _ShowSortID = value; Update();}
        }


        [Category("外观")]
        [Description("设置或获取排序ID号")]
        public int ID
        {
            get { return _ID; }
            set 
            { 
                _ID = value;
                if (_ShowSortID)
                {
                    this.Refresh();
                }
                Update(); 
            }
        }

        [Category("外观")]
        [Description("Get or set the image to be displayed in the caption")]
        public Icon CaptionIcon
        {
            get
            {
                return captionIcon;
            }

            set
            {
                captionIcon = value;
                Update();
            }
        }

        [Category("外观")]
        [Description("获取或设置是否显示关闭按钮")]
        public bool ShowClose
        {
            get
            {
                return _ShowClose;
            }
            set
            {
                _ShowClose = value;
                if (_ShowClose)
                {
                    if (!this.Controls.Contains(directionCtrl))
                        this.Controls.Add(directionCtrl);
                }
                else
                {
                    if(this.Controls.Contains(directionCtrl))
                        this.Controls.Remove(directionCtrl);
                }
                Update();
            }
        }

        [Category("外观")]
        [Description("获取或设置是否显示选项框")]
        public bool ShowCheckBox
        {
            get
            {
                return _ShowCheckBox;
            }
            set
            {
                _ShowCheckBox = value;
                if (_ShowCheckBox)
                {
                    if (!this.Controls.Contains(this.CheckItem))
                    {
                        this.Controls.Add(this.CheckItem);
                    }
                }
                else
                {
                    if(this.Controls.Contains(this.CheckItem))
                        this.Controls.Remove(this.CheckItem);
                }
                Update();
            }
        }


        [Category("外观")]
        [Description("获取或设置是否选中")]
        public bool Checked
        {
            get
            {
                return CheckItem.CheckState == CheckState.Checked ? true : false;
            }
            set
            {
                if (value)
                {
                    CheckItem.CheckState = CheckState.Checked;
                }
                else
                {
                    CheckItem.CheckState = CheckState.Unchecked;
                }
                Update();
            }
        }


        [Category("外观")]
        [Description("设置或获取标题文本")]
        public string CaptionText
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                Update();
            }
        }

        [Category("外观")]
        [Description("设置或获取标题文本字体")]
        public Font CaptionFont
        {
            get
            {
                return this.Font;
            }
            set
            {
                this.Font = value;
                Refresh();
            }
        }

        [Category("外观")]
        [Description("前景色")]
        public Color ColorOne
        {
            get
            {
                return colorOne;
            }
            set
            {
                colorOne = value;
                InitializeBrush();
                Refresh();
            }
        }

        [Category("外观")]
        [Description("渐变色 ")]
        public Color ColorTwo
        {
            get
            {
                return colorTwo;
            }
            set
            {
                colorTwo = value;
                InitializeBrush();
                Refresh();
            }
        }

        [Category("外观")]
        [Description("标题文本颜色")]
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                Refresh();
            }
        }

        [Category("外观")]
        [Description("标题栏背景颜色刷子类型")]
        public BrushType BrushType
        {
            get
            {
                return this.brushType;
            }
            set
            {
                if (value != this.brushType)
                {
                    this.brushType = value;
                    InitializeBrush();
                    Refresh();
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Set up the graphic path used for drawing the borders
        /// </summary>
        protected override void InitializeGraphicPath()
        {
            cornerSquare = (int)(Height > Width ? Height * 0.05f : Width * 0.05f);// Width * 0.25f;
            base.InitializeGraphicPath();
        }
        #endregion

        #region Private

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponent()
        {
            this.directionCtrl = new DirectionCtrl();
            CheckItem = new CheckBox();
            this.SuspendLayout();

            this.Name = "CaptionCtrl";


            this.directionCtrl.Width = (int)(this.Height);
            this.directionCtrl.Height = this.Height;
            //System.Windows.Forms.MessageBoxEx.Show((this.Width-this.Height).ToString());
            this.directionCtrl.Location = new Point((int)(this.Width - this.Height), 0);
            this.directionCtrl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.directionCtrl.Name = "directionCtrl";
            this.directionCtrl.PanelClose += new DirectionCtrl.EventCloseClick(directionCtrl_PanelClose);
            //this.Controls.Add(directionCtrl);

            this.CheckItem.Name = "CheckItem";
            this.CheckItem.Text = "";
            this.CheckItem.Width = 30;
            this.CheckItem.Left = 3;
            this.CheckItem.CheckedChanged += new EventHandler(CheckItem_CheckedChanged);
            if (this._ShowCheckBox)
                this.Controls.Add(CheckItem);

            this.ResumeLayout(false);



        }


        /// <summary>
        /// Set up the brush used in filling this control
        /// </summary>
        private void InitializeBrush()
        {
            if (null != brush)
            {
                brush.Dispose();
            }
            if (brushType == BrushType.Solid)
            {
                brush = new SolidBrush(colorOne);
            }
            else
            {
                int width = this.Width;
                int height = this.Height;
                if (width == 0)
                {
                    width = 1;
                }
                if (height == 0)
                {
                    height = 1;
                }
                if (this.directionCtrl.DirectionStyle == DirectionStyle.Up || this.directionCtrl.DirectionStyle == DirectionStyle.Down)
                {
                    brush = new LinearGradientBrush(new Rectangle(0, 0, width, height), colorOne, colorTwo, LinearGradientMode.Vertical);
                }
                else
                {
                    brush = new LinearGradientBrush(new Rectangle(0, 0, width, height), colorOne, colorTwo, LinearGradientMode.Horizontal);
                }
            }
        }
        #endregion

        #region Public

        /// <summary>
        /// Set the handler for the event raised by the "click" control once it is pressed
        /// </summary>
        /// <param name="handler">instance of an handler for the event </param>
        public void SetStyleChangedHandler(DirectionCtrlStyleChangedEvent handler)
        {
            directionCtrl.handlerStyleChange += handler;
        }

        /// <summary>
        /// Checks to see if dragging is enabled.Used by ExtendedPanel to add/remove the handler
        /// </summary>
        public bool IsDraggingEnabled()
        {
            return (Dragging != null);
        }

        /// <summary>
        /// Set the direction style for the "click" control
        /// </summary>
        /// <param name="style">the new direction style</param>
        public void SetDirectionStyle(DirectionStyle style)
        {
            this.directionCtrl.DirectionStyle = style;

        }
        #endregion

        #region WM_PAINT

        /// <summary>
        /// Method for handling the WM_PAINT message
        /// </summary>
        /// <param name="e">instance of the object holding the event data</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //set up some flags
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //check to see if the path has been initialized
            if (graphicPath == null)
            {
                InitializeGraphicPath();
            }
            //draw the border
            e.Graphics.DrawPath(new Pen(borderColor), graphicPath);

            //paint the background
            e.Graphics.FillPath(brush, graphicPath);

            //measure text length
            StringFormat stringFormat = null;
            float xAxis = 0;
            float yAxis = 0;
            if (Width >= Height)
            {
                stringFormat = new StringFormat();
                SizeF size = e.Graphics.MeasureString(text, this.Font, new PointF(0, 0), stringFormat);
                yAxis = (Height - size.Height) * 0.5f;
            }
            else
            {
                stringFormat = new StringFormat(StringFormatFlags.DirectionVertical);
                SizeF size = e.Graphics.MeasureString(text, this.Font, new PointF(0, 0), stringFormat);
                xAxis = (Width - size.Width) * 0.5f;
            }

            //draw the image if it has been set up

            string TmpText = text;
            if (_ShowSortID)
            {
                TmpText=string.Format("【{0}】  {1}", _ID.ToString(),text );
            }

            if (captionIcon != null)
            {
                if (Width >= Height)
                {
                    e.Graphics.DrawIcon(captionIcon, (int)cornerSquare / 6, (int)cornerSquare / 6);//, cornerSquare, cornerSquare);
                    //draw the text
                    e.Graphics.DrawString(TmpText, this.Font, new SolidBrush(textColor), new PointF(xAxis + (int)cornerSquare / 6 + captionIcon.Width, yAxis + (int)cornerSquare / 6), stringFormat);
                }
                else
                {
                    e.Graphics.DrawIcon(captionIcon, (int)cornerSquare / 6, (int)cornerSquare / 6);
                    //draw the text
                    e.Graphics.DrawString(TmpText, this.Font, new SolidBrush(textColor), new PointF(xAxis + (int)cornerSquare / 6, yAxis + (int)cornerSquare / 6 + +captionIcon.Height), stringFormat);
                }
            }
            else
            {
                //draw the text
                e.Graphics.DrawString(TmpText, this.Font, new SolidBrush(textColor), new PointF(16 + xAxis, yAxis), stringFormat);
            }
        }

        #endregion

        #region Events

        private void CheckItem_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(sender, e);
            }
        }
        /// <summary>
        /// 关闭按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void directionCtrl_PanelClose(object sender, EventArgs e)
        {
            if (RaiseClose != null)
                this.RaiseClose(sender, e);
        }

        /// <summary>
        /// Event raised when the mouse is moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            //is the mouse down
            if (mouseDown && Dragging != null)
            {
                if (mouseX != e.X || mouseY != e.Y)
                {
                    int width = mouseX - e.X;
                    int height = mouseY - e.Y;
                    //raise the event
                    Dragging(this, new CaptionDraggingEventArgs(width, height));
                }
            }
        }

        /// <summary>
        /// Event raised when one of the mouse buttons is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                //save mouse coordinates
                mouseX = e.X;
                mouseY = e.Y;
            }

        }

        /// <summary>
        /// Event raised when the mouse button being pressed is released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseUpEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
                //reset mouse X,Y coordinates
                mouseX = 0;
                mouseY = 0;
                if (MoveOver != null /*&& Moved*/)
                {
                    this.MoveOver(sender, new EventArgs());
                }
            }
        }

        #endregion

        #region Override

        /// <summary>
        /// Override the OnResize because it needs to set up the location of the "direction" control and the size of that control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //need to recreate the graphicpath for the border
            InitializeGraphicPath();

            if (this.Width >= Height)
            {
                this.directionCtrl.Location = new Point((int)(this.Width - this.Height), 0);
                this.directionCtrl.Width = (int)(this.Height);
                this.directionCtrl.Height = this.Height;
            }
            else
            {
                this.directionCtrl.Location = new Point(0, (int)(this.Height - this.Width));
                this.directionCtrl.Width = this.Width;
                this.directionCtrl.Height = this.Width;
            }
            this.CheckItem.Top = (this.Height - this.CheckItem.Height) / 2;
            //set the brush
            InitializeBrush();
            this.Region = new Region(regionPath);
        }

        #endregion

    }
}
