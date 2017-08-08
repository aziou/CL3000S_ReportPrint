#region Using Directives
using System;
using System.Drawing;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#endregion

namespace CLDC_Comm.ExtendedPanel
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="size">�ؼ����µĴ�С������ȡ���ڶԶԽӿ�͸߶�</param>
    internal delegate void NotifyAnimationCallback(int size);

    /// <summary>
    ///  
    /// </summary>
    internal delegate void NotifyAnimationFinishedCallback();


    public partial class ExtendedPanel : CornerCtrl
    {

        #region Members

        public event EventPanelClose PanelClose = null;

        public event EventMouseMove EvtMouseMove = null;


        /// <summary>
        /// ����ƶ�����
        /// </summary>
        public event EventMoveOver EvtMoveOver = null;

        /// <summary>
        /// ����ؼ�
        /// </summary>
        private Control _CtrParent = null;


        //private bool _ShowSortID = false;

        //private int _ID = 1;

        /// <summary>
        /// 
        /// </summary>
        private bool firstTimeVisible = false;

        /// <summary>
        /// ���������ƿ���ͨ���϶�����������
        /// </summary>
        private bool moveable = false;

        /// <summary>
        /// When  
        /// </summary>
        private bool backupMoveable = false;

        /// <summary>
        /// ����ؼ���ɵĸ߶ȵ��ؼ�չ���������ʱ��
        /// </summary>
        private int backupHeight = 0;

        /// <summary>
        /// ����ؼ���ɵĿ�ȵ��ؼ�չ���������ʱ��
        /// </summary>
        private int backupWidth = 0;

        /// <summary>
        /// ��չ��ʱʹ�û��������ز���
        /// </summary>
        private int step = 20;

        /// <summary>
        /// �������߶�
        /// </summary>
        private int captionSize = 0;

        /// <summary>
        /// ������Anchor���ԣ���Ϊ������Ҫ�ģ�������ⱻ����Ϊ��/��
        /// </summary>
        private AnchorStyles backupAnchor = AnchorStyles.None;

        /// <summary>
        /// ��־�������Ƿ���һ���۵�����/����Ŀ���
        /// </summary>
        private Animation animation = Animation.Yes;

        /// <summary>
        ///  
        /// </summary>
        private ExtendedPanelState state = ExtendedPanelState.Expanded;

        /// <summary>
        /// ������뷽ʽ
        /// </summary>
        private DirectionStyle captionAlign = DirectionStyle.Up;

        /// <summary>
        /// �������ؼ�
        /// </summary>
        private CaptionCtrl captionCtrl = null;

        /// <summary>
        /// չ�����뷽ʽ
        /// </summary>
        private CollapseAnimation collapseAnimation = null;

        /// <summary>
        /// 
        /// </summary>
        private NotifyAnimationCallback callbackNotifyAnimation = null;

        /// <summary>
        /// 
        /// </summary>
        private NotifyAnimationFinishedCallback callbackNotifyAnimationFinished = null;

        /// <summary>
        /// 
        /// </summary>
        private List<Control> visibleControls = new List<Control>();

        public event EventCheckedChanged CheckedChanged = null;

        private object dummy = 1;
        #endregion


        #region ctor

        /// <summary>
        /// The constructor
        /// </summary>
        public ExtendedPanel()
        {

            InitializeComponent();

         
            captionCtrl.SetStyleChangedHandler(new DirectionCtrlStyleChangedEvent(CollapsingHandler));


            if (moveable == true)
            {
                captionCtrl.Dragging += new CaptionDraggingEvent(CaptionDraggingEvent);

            }

            this.captionCtrl.CheckedChanged += new EventCheckedChanged(captionCtrl_CheckedChanged);
            this.captionCtrl.RaiseClose += new EventPanelClose(captionCtrl_RaiseClose);
            callbackNotifyAnimation = new NotifyAnimationCallback(SetSizeCallback);
            callbackNotifyAnimationFinished = new NotifyAnimationFinishedCallback(AnimationFinished);

        }


 



        #endregion

        #region Public

        /// <summary>
        /// ���𷽷�
        /// </summary>
        public void Collapse()
        {
            if (this.state != ExtendedPanelState.Expanded)
            {
                throw new InvalidOperationException("���Ʊ�����������״̬����Ҫ������!");
            }

            DirectionStyle oldStyle = DirectionStyle.Up;
            DirectionStyle newStyle = DirectionStyle.Down;

            switch (captionAlign)
            {
                case DirectionStyle.Up:         //set above
                    break;

                case DirectionStyle.Left:
                    oldStyle = DirectionStyle.Left;
                    newStyle = DirectionStyle.Right;
                    break;

                case DirectionStyle.Right:
                    oldStyle = DirectionStyle.Right;
                    newStyle = DirectionStyle.Left;
                    break;

                case DirectionStyle.Down:
                    oldStyle = DirectionStyle.Down;
                    newStyle = DirectionStyle.Up;
                    break;
            }
            this.captionCtrl.SetDirectionStyle(newStyle);
            ChangeStyleEventArgs args = new ChangeStyleEventArgs(oldStyle, newStyle);
            CollapsingHandler(this, args);
        }

        /// <summary>
        /// չ��
        /// </summary>
        public void Expand()
        {
            if (this.state != ExtendedPanelState.Collapsed)
            {
                throw new InvalidOperationException("���Ʊ�����������״̬����Ҫ������!");
            }

            DirectionStyle oldStyle = DirectionStyle.Down;
            DirectionStyle newStyle = DirectionStyle.Up;

            switch (captionAlign)
            {
                case DirectionStyle.Up:         //set above
                    break;

                case DirectionStyle.Left:
                    oldStyle = DirectionStyle.Right;
                    newStyle = DirectionStyle.Left;
                    break;

                case DirectionStyle.Right:
                    oldStyle = DirectionStyle.Left;
                    newStyle = DirectionStyle.Right;
                    break;

                case DirectionStyle.Down:
                    oldStyle = DirectionStyle.Up;
                    newStyle = DirectionStyle.Down;
                    break;
            }
            this.captionCtrl.SetDirectionStyle(newStyle);
            ChangeStyleEventArgs args = new ChangeStyleEventArgs(oldStyle, newStyle);
            CollapsingHandler(this, args);
        }


        #endregion

        #region Private

        /// <summary>
        /// �����¶�λ������£��������ص��Ĺ���
        /// </summary>
        /// <param name="oldCaptionSize"></param>
        private void CheckDocking(int oldCaptionSize)
        {
            int offset = captionSize - oldCaptionSize;
            if (offset != 0)
            {
                switch (captionAlign)
                {
                    case DirectionStyle.Up:
                        this.SuspendLayout();
                        foreach (Control control in Controls)
                        {
                            if (control != captionCtrl)
                            {
                                control.Top += offset;
                            }
                        }
                        this.ResumeLayout(false);
                        break;

                    case DirectionStyle.Down:
                        this.SuspendLayout();
                        foreach (Control control in Controls)
                        {
                            if (control != captionCtrl)
                            {
                                control.Top -= offset;
                            }
                        }
                        this.ResumeLayout(false);

                        break;

                    case DirectionStyle.Left:
                        this.SuspendLayout();
                        foreach (Control control in Controls)
                        {
                            if (control != captionCtrl)
                            {
                                control.Left += offset;
                            }
                        }
                        this.ResumeLayout(false);
                        break;

                    case DirectionStyle.Right:
                        this.SuspendLayout();
                        foreach (Control control in Controls)
                        {
                            if (control != captionCtrl)
                            {
                                control.Left -= offset;
                            }
                        }
                        this.ResumeLayout(false);
                        break;
                }
            }
        }

        /// <summary>
        /// ���/���ؿ��Ʊ��������δ��ʾ�ڱ���������Ŀ���ģʽ
        /// </summary>
        private void ShowControls()
        {

            if (state == ExtendedPanelState.Collapsed)
            {

                lock (dummy)
                {
                    if (visibleControls.Count > 0)
                    {
                        while (visibleControls.Count > 0)
                        {
                            //visibleControls[visibleControls.Count - 1].Enabled = true;
                            visibleControls[visibleControls.Count - 1].Visible = true;
                            visibleControls.RemoveAt(visibleControls.Count - 1);
                        }

                    }
                    else
                    {
                        foreach (Control control in this.Controls)
                        {
                            if (control != this.captionCtrl)
                            {
                                if (control.Visible)
                                {
                                    visibleControls.Add(control);
                                    control.Visible = false;
                                    //  control.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��С��ԭ
        /// </summary>
        /// <param name="size"></param>
        private void SetSizeCallback(int size)
        {
            switch (this.captionAlign)
            {
                case DirectionStyle.Down:
                    int tempY = this.Height - size;
                    //set the new location of the panel
                    Win32Wrapper.SetWindowPos(this.Handle, IntPtr.Zero, this.Location.X, this.Location.Y + tempY, this.Width, size, Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER | Win32Wrapper.FlagsSetWindowPos.SWP_SHOWWINDOW);
                    break;

                case DirectionStyle.Up:
                    this.Height = size;
                    break;

                case DirectionStyle.Right:
                    int tempX = this.Width - size;
                    Win32Wrapper.SetWindowPos(this.Handle, IntPtr.Zero, this.Location.X + tempX, this.Location.Y, size, this.Height, Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER | Win32Wrapper.FlagsSetWindowPos.SWP_SHOWWINDOW);
                    break;

                case DirectionStyle.Left:
                    this.Width = size;
                    if (this.Width < this.captionCtrl.Width)
                    {
                        this.Width = this.captionCtrl.Width;
                    }
                    break;
            }
        }

        /// <summary>
        /// Method called in order to have this control accessed in a thread safe way
        /// </summary>
        private void AnimationFinished()
        {
            //check to see  if Anchoring needs special treatment
            if (this.captionAlign == DirectionStyle.Right || this.captionAlign == DirectionStyle.Down)
            {
                this.Anchor = backupAnchor;
            }
            if (captionAlign == DirectionStyle.Down)
            {
                //set caption location (no redrawing) and hiding
                Win32Wrapper.SetWindowPos(this.captionCtrl.Handle, IntPtr.Zero, 0, this.Height - this.captionCtrl.Height, 0, 0, Win32Wrapper.FlagsSetWindowPos.SWP_NOREDRAW | Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER | Win32Wrapper.FlagsSetWindowPos.SWP_NOSIZE | Win32Wrapper.FlagsSetWindowPos.SWP_HIDEWINDOW);
                //set back the parent
                this.captionCtrl.Parent = this;
                this.captionCtrl.Visible = true;

                //set back the moveable property; during collapsing the movement is not allowed
                moveable = backupMoveable;
            }
            else
            {
                if (captionAlign == DirectionStyle.Right)
                {
                    //set caption location (no redrawing) and hiding
                    Win32Wrapper.SetWindowPos(this.captionCtrl.Handle, IntPtr.Zero, this.Width - this.captionCtrl.Width, 0, 0, 0, Win32Wrapper.FlagsSetWindowPos.SWP_NOREDRAW | Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER | Win32Wrapper.FlagsSetWindowPos.SWP_NOSIZE | Win32Wrapper.FlagsSetWindowPos.SWP_HIDEWINDOW);
                    //set back the parent
                    this.captionCtrl.Parent = this;
                    this.captionCtrl.Visible = true;
                    //set back the moveable property; during collapsing the movement is not allowed
                    moveable = backupMoveable;
                }
            }
            //set the state of the object expanded/collapsed
            SetState();
            ShowControls();

        }

        /// <summary>
        /// Set the state for this panel 
        /// </summary>
        private void SetState()
        {
            if (this.captionCtrl.Width >= this.captionCtrl.Height)
            {
                if (this.captionCtrl.Height == this.Height)
                {
                    state = ExtendedPanelState.Collapsed;
                }
                else
                {
                    state = ExtendedPanelState.Expanded;
                }
            }
            else
            {
                if (this.captionCtrl.Width == this.Width)
                {
                    state = ExtendedPanelState.Collapsed;
                }
                else
                {
                    state = ExtendedPanelState.Expanded;
                }
            }
        }

        /// <summary>
        /// Set the caption properties for size and location
        /// </summary>
        /// <param name="flag">if true will set location and the widht/percentage of the parent based on alignment</param>
        private void SetCaptionControl(bool flag)
        {
            if (flag && state == ExtendedPanelState.Expanded)
            {
                this.captionCtrl.SetDirectionStyle(captionAlign);
            }
            switch (captionAlign)
            {

                case DirectionStyle.Up:
                    if (flag)
                    {
                        this.captionCtrl.Height = captionSize;//(int)(this.Height * captionSize / 100);
                        this.captionCtrl.Location = new Point(0, 0);
                    }
                    if (this.Width != this.captionCtrl.Width)
                    {
                        this.captionCtrl.Width = this.Width;
                    }
                    break;

                case DirectionStyle.Down:
                    if (flag)
                    {
                        this.captionCtrl.Height = captionSize; //(int)(this.Height * captionSize / 100);
                        this.captionCtrl.Location = new Point(0, this.Height - this.captionCtrl.Height);
                    }
                    if (this.Width != this.captionCtrl.Width)
                    {
                        this.captionCtrl.Width = this.Width;
                    }

                    break;

                case DirectionStyle.Left:
                    if (flag)
                    {
                        this.captionCtrl.Width = captionSize;// (int)(this.Width * captionSize / 100);
                        this.captionCtrl.Location = new Point(0, 0);
                    }
                    if (this.captionCtrl.Height != this.Height)
                    {
                        this.captionCtrl.Height = this.Height;
                    }
                    break;

                case DirectionStyle.Right:
                    if (flag)
                    {
                        this.captionCtrl.Width = captionSize;// (int)(this.Width * captionSize / 100);
                        this.captionCtrl.Location = new Point(this.Width - this.captionCtrl.Width, 0);
                    }
                    if (this.captionCtrl.Height != this.Height)
                    {
                        this.captionCtrl.Height = this.Height;
                    }

                    break;
            }
        }

        /// <summary>
        /// This method will take the caption control out of the controls of this panel
        /// </summary>
        private void ChangeCaptionParent()
        {
            //take the caption out of the panel beacause of the flickering
            this.captionCtrl.Parent = this.Parent;
            this.captionCtrl.Location = new Point(this.Location.X + this.Width - this.captionCtrl.Width, this.Location.Y + this.Height - this.captionCtrl.Height);
            Win32Wrapper.SetWindowPos(this.Handle, this.captionCtrl.Handle, 0, 0, 0, 0, Win32Wrapper.FlagsSetWindowPos.SWP_NOMOVE | Win32Wrapper.FlagsSetWindowPos.SWP_NOSIZE | Win32Wrapper.FlagsSetWindowPos.SWP_NOREDRAW);

            //disable moving 
            backupMoveable = moveable;
            moveable = false;
        }
        #endregion

        #region Protected

        /// <summary>
        /// 
        /// </summary>
        protected override void InitializeGraphicPath()
        {
            cornerSquare = (int)(captionCtrl.Height > captionCtrl.Width ? captionCtrl.Height * 0.05f : captionCtrl.Width * 0.05f);// Width * 0.25f;
            base.InitializeGraphicPath();
        }

        #endregion

        #region WM_PAINT

        /// <summary>
        /// The event raised for painting the control
        /// </summary>
        /// <param name="e">Instance of the object containing event data</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //set flags for the graphics object
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            //recreate the path if is ull
            if (null == graphicPath)
            {
                InitializeGraphicPath();
            }

            //draw the border
            e.Graphics.DrawPath(new Pen(borderColor), graphicPath);

        }

        #endregion

        #region Overrides

        /// <summary>
        /// This needs overriding in the case the control is being resized.
        /// </summary>
        /// <param name="e"></param>

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (captionSize == 0)
            {
                captionSize = (int)(this.Height * 20 / 100);
                CheckDocking(0);
            }

            //if the control is resized (other than collapsing and expanding) the caption needs to be updated
            SetCaptionControl(state != ExtendedPanelState.Collapsing && state != ExtendedPanelState.Expanding);
            this.Refresh();
        }


        protected override void WndProc(ref Message m)
        {

            if (!DesignMode && firstTimeVisible && m.Msg == 0x18)
            {
                firstTimeVisible = false;
                backupHeight = Height;
                backupWidth = Width;

                switch (this.captionAlign)
                {
                    case DirectionStyle.Down:
                        //set the new location of the panel
                        Win32Wrapper.SetWindowPos(this.Handle, IntPtr.Zero, this.Location.X, this.Location.Y + captionCtrl.Location.Y, this.Width, captionCtrl.Height, Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER);
                        this.captionCtrl.SetDirectionStyle(DirectionStyle.Up);
                        break;

                    case DirectionStyle.Up:
                        this.Height = captionCtrl.Height;
                        this.captionCtrl.SetDirectionStyle(DirectionStyle.Down);
                        break;

                    case DirectionStyle.Right:
                        //int tempX = this.Width - size;
                        Win32Wrapper.SetWindowPos(this.Handle, IntPtr.Zero, this.Location.X + Width - captionCtrl.Location.X, this.Location.Y, captionCtrl.Width, this.Height, Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER);
                        this.captionCtrl.SetDirectionStyle(DirectionStyle.Left);
                        break;

                    case DirectionStyle.Left:
                        this.Width = this.captionCtrl.Width;
                        this.captionCtrl.SetDirectionStyle(DirectionStyle.Right);
                        break;
                }
                this.captionCtrl.Location = new Point(0, 0);
                //set the state of the object expanded/collapsed
                ShowControls();

            }
            base.WndProc(ref m);
        }
        #endregion

        #region Properties



        [Browsable(false)]
        [Category("��Ϊ")]
        [DefaultValue(false)]
        [Description("���û��ȡ�Ƿ���ʾ����ID")]
        public bool ShowOrderID
        {
            get { return captionCtrl.ShowSortID; }
            set { captionCtrl.ShowSortID = value; Update(); }
        }

        [Category("��Ϊ")]
        [DefaultValue(1)]
        [Description("���û��ȡIDֵ")]
        public int ID
        {
            get { return captionCtrl.ID; }
            set { captionCtrl.ID = value; Update(); }
        }

        /// <summary>
        /// ���ø����ĸ���ؼ�
        /// </summary>
        public Control CtrParent
        {
            set
            {
                _CtrParent = value;
            }
        }


        [Browsable(true)]
        [Category("��Ϊ")]
        [DefaultValue(false)]
        [Description("���û��ȡ�ؼ��ڸ��������Ƿ�ɱ��ƶ�")]
        public bool Moveable
        {
            get
            {
                return moveable;
            }

            set
            {
                if (value != moveable)
                {
                    moveable = value;
                    if (moveable == true)
                    {
                        if (!captionCtrl.IsDraggingEnabled())
                        {
                            captionCtrl.Dragging += new CaptionDraggingEvent(CaptionDraggingEvent);
                            captionCtrl.MoveOver += new EventMoveOver(captionCtrl_MoveOver);
                        }
                    }
                    else
                    {
                        if (captionCtrl.IsDraggingEnabled())
                        {
                            captionCtrl.Dragging -= new CaptionDraggingEvent(CaptionDraggingEvent);
                            captionCtrl.MoveOver -= new EventMoveOver(captionCtrl_MoveOver);
                        }
                    }
                }
            }
        }

        public void captionCtrl_MoveOver(object sender, EventArgs e)
        {
            if (this.EvtMoveOver != null)
            {
                this.EvtMoveOver(sender, e);
            }
        }
        /// <summary>
        /// �Ƿ�ѡ��
        /// </summary>
        [Browsable(true)]
        [Category("��Ϊ")]
        [DefaultValue(false)]
        [Description("��ȡ�������Ƿ�ѡ��")]
        public bool Checked
        {
            get
            {
                return this.captionCtrl.Checked;
            }
            set
            {
                this.captionCtrl.Checked = value;
                Update();
            }
        }

        [Browsable(true)]
        [Category("��Ϊ")]
        [DefaultValue(false)]
        [Description("��ȡ�������Ƿ���ʾ�رհ�ť")]
        public bool ShowClose
        {
            get
            {
                return this.captionCtrl.ShowClose;
            }
            set
            {
                this.captionCtrl.ShowClose = value;
                Update();
            }
        }

        [Browsable(true)]
        [Category("��Ϊ")]
        [DefaultValue(false)]
        [Description("��ȡ�������Ƿ���ʾѡ���")]
        public bool ShowCheckBox
        {
            get
            {
                return this.captionCtrl.ShowCheckBox;
            }
            set
            {
                this.captionCtrl.ShowCheckBox = value;
                Update();
            }
        }


        [Browsable(true)]
        [Category("��Ϊ")]
        [DefaultValue(Animation.Yes)]
        [Description("����/��ȡ�Ƿ��������չ��ʽ���Ŷ���")]
        public Animation Animation
        {
            get
            {
                return animation;
            }
            set
            {
                animation = value;
            }
        }

        [Category("����")]
        [DefaultValue(DirectionStyle.Up)]
        [Description("���û��ȡ������뷽ʽ")]
        public DirectionStyle CaptionAlign
        {
            get
            {
                return this.captionAlign;
            }
            set
            {
                if (value != captionAlign)
                {
                    captionAlign = value;
                    SetCaptionControl(true);
                    this.captionCtrl.Refresh();

                }
            }
        }

        [Category("����")]
        [DefaultValue("Caption")]
        [Description("���û��ȡ��������")]
        public string CaptionText
        {
            get
            {
                return this.captionCtrl.CaptionText;
            }
            set
            {
                this.captionCtrl.CaptionText = value;
                this.captionCtrl.Refresh();
            }
        }

        [Category("����")]
        [DefaultValue(null)]
        [Description("���û��ȡ����ͼƬ���һ�û���꣩")]
        public Icon CaptionImage
        {
            get
            {
                return this.captionCtrl.CaptionIcon;
            }
            set
            {
                this.captionCtrl.CaptionIcon = value;
                this.captionCtrl.Refresh();
            }
        }

        [Category("����")]
        [DefaultValue(BrushType.Gradient)]
        [Description("���û��ȡ�������ʹ�õĻ�������")]
        public BrushType CaptionBrush
        {
            get
            {
                return captionCtrl.BrushType;
            }
            set
            {
                this.captionCtrl.BrushType = value;
            }
        }

        [Category("����")]
        [Description("���û��ȡ�����ı���ɫ")]
        public Color CaptionTextColor
        {
            get
            {
                return this.captionCtrl.TextColor;
            }
            set
            {
                this.captionCtrl.TextColor = value;
            }
        }

        [Category("����")]
        [Description("���û��ȡ����ǰ��ɫ")]
        public Color CaptionColorOne
        {
            get
            {
                return this.captionCtrl.ColorOne;
            }
            set
            {
                this.captionCtrl.ColorOne = value;
            }
        }

        [Category("����")]
        [Description("���û��ȡ���⽥��ɫ")]
        public Color CaptionColorTwo
        {
            get
            {
                return this.captionCtrl.ColorTwo;
            }
            set
            {
                this.captionCtrl.ColorTwo = value;
            }
        }

        [Category("����")]
        [Description("���û��ȡ��������")]
        public Font CaptionFont
        {
            get
            {
                return this.captionCtrl.CaptionFont;
            }
            set
            {
                this.captionCtrl.CaptionFont = value;
            }
        }

        [Category("���")]
        [DefaultValue(ExtendedPanelState.Expanded)]
        [Description("���û��ȡ��ʼ��ʱ��չ����������")]
        public ExtendedPanelState State
        {
            get
            {
                return this.state;
            }

            [DesignOnly(true)]
            set
            {
                if (value == ExtendedPanelState.Collapsing || value == ExtendedPanelState.Expanding)
                {
                    return;
                }
                this.state = value;
                if (value == ExtendedPanelState.Collapsed)
                {
                    firstTimeVisible = true;
                }
            }
        }


        [Category("����")]
        [DefaultValue(0)]
        [Description("���û��ȡ�������߶�")]
        public int CaptionSize
        {
            get
            {
                return this.captionSize;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("�������߶ȱ������=0 ");
                }
                if (state != ExtendedPanelState.Expanded)
                {
                    throw new InvalidOperationException("���������ñ���Ĵ�С����������ȫչ��");
                }
                if (value != this.captionSize)
                {
                    int backupCaptionSize = captionSize;
                    this.captionSize = value;
                    SetCaptionControl(true);
                    CheckDocking(backupCaptionSize);
                    this.captionCtrl.Refresh();
                    this.Refresh();
                }
            }
        }

        [Category("����")]
        [DefaultValue(20)]
        [Description("��ȡ/�����������չ��ʱ�Ƿ񲥷Ŷ���")]
        public int AnimationStep
        {
            get
            {
                return this.step;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("�������1");
                }
                step = value;
            }
        }

        [Browsable(false)]
        public Color DirectionCtrlColor
        {
            get
            {
                return captionCtrl.DirectionCtrlColor;
            }
            set
            {
                captionCtrl.DirectionCtrlColor = value;
            }
        }

        [Browsable(false)]
        public Color DirectionCtrlHoverColor
        {
            get
            {
                return captionCtrl.DirectionCtrlHoverColor;
            }
            set
            {
                captionCtrl.DirectionCtrlHoverColor = value;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// �ر��¼�(ֻ���ڴ��ڹرհ�ťʱ����Ч)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void captionCtrl_RaiseClose(object sender, EventArgs e)
        {
            if (PanelClose != null)
                this.PanelClose(this, new EventArgs());
        }


        private void CollapsingHandler(object sender, ChangeStyleEventArgs e)
        {
            //check to see  if Anchoring needs special treatment
            if (this.captionAlign == DirectionStyle.Right || this.captionAlign == DirectionStyle.Down)
            {
                backupAnchor = this.Anchor;
                this.Anchor |= AnchorStyles.Left;
                this.Anchor |= AnchorStyles.Top;
                this.Anchor &= ~AnchorStyles.Right;
                this.Anchor &= ~AnchorStyles.Bottom;
            }
            //create the thread for collasping/expanding the control 
            if (null == collapseAnimation)
            {
                collapseAnimation = new CollapseAnimation();
                //set the events to be raised by the animation worker thread
                collapseAnimation.NotifyAnimation += new NotifyAnimationEvent(OnNotifyAnimationEvent);
                collapseAnimation.NotifyAnimationFinished += new NotifyAnimationFinishedEvent(OnNotifyAnimationFinished);
                if (backupHeight == 0)
                {
                    backupHeight = this.Height;
                }
                if (backupWidth == 0)
                {
                    backupWidth = this.Width;
                }
            }

            switch (this.captionAlign)
            {
                case DirectionStyle.Up:
                    if (e.Old == DirectionStyle.Up)
                    {
                        backupHeight = this.Height;
                        backupWidth = this.Width;

                        collapseAnimation.Maximum = this.Height;
                        collapseAnimation.Minimum = captionCtrl.Height;
                        if (animation == Animation.Yes)
                        {
                            collapseAnimation.Step = step;
                        }
                        else
                        {
                            collapseAnimation.Step = this.Height - captionCtrl.Height;
                        }
                    }
                    else
                    {
                        collapseAnimation.Maximum = backupHeight;
                        collapseAnimation.Minimum = captionCtrl.Height;
                        if (animation == Animation.Yes)
                        {
                            collapseAnimation.Step = -step;
                        }
                        else
                        {
                            collapseAnimation.Step = -(backupHeight - captionCtrl.Height);
                        }
                    }
                    break;

                case DirectionStyle.Down:
                    if (e.Old == DirectionStyle.Down)
                    {
                        //have to extract caption ctrl because of the flickering involved
                        ChangeCaptionParent();

                        //save the size as will need them for expanding the control back
                        backupHeight = this.Height;
                        backupWidth = this.Width;

                        collapseAnimation.Maximum = this.Height;
                        collapseAnimation.Minimum = captionCtrl.Height;
                        if (animation == Animation.Yes)
                        {
                            collapseAnimation.Step = step;
                        }
                        else
                        {
                            collapseAnimation.Step = this.Height - captionCtrl.Height;
                        }
                    }
                    else
                    {
                        //have to extract caption ctrl because of the flickering involved
                        ChangeCaptionParent();

                        collapseAnimation.Maximum = backupHeight;
                        collapseAnimation.Minimum = captionCtrl.Height;
                        if (animation == Animation.Yes)
                        {
                            collapseAnimation.Step = -step;
                        }
                        else
                        {
                            collapseAnimation.Step = -(backupHeight - captionCtrl.Height);
                        }
                    }
                    break;


                case DirectionStyle.Left:
                    if (e.Old == DirectionStyle.Left)
                    {
                        //save the size as will need them for expanding the control back
                        backupHeight = this.Height;
                        backupWidth = this.Width;

                        collapseAnimation.Maximum = this.Width;
                        collapseAnimation.Minimum = captionCtrl.Width;
                        if (animation == Animation.Yes)
                        {
                            collapseAnimation.Step = step;
                        }
                        else
                        {
                            collapseAnimation.Step = this.Width - captionCtrl.Width;
                        }
                    }
                    else
                    {
                        collapseAnimation.Maximum = backupWidth;
                        collapseAnimation.Minimum = captionCtrl.Width;
                        if (animation == Animation.Yes)
                        {
                            collapseAnimation.Step = -step;
                        }
                        else
                        {
                            collapseAnimation.Step = -(backupWidth - captionCtrl.Width);
                        }
                    }
                    break;

                case DirectionStyle.Right:
                    if (e.Old == DirectionStyle.Right)
                    {
                        //have to extract caption ctrl because of the flickering involved
                        ChangeCaptionParent();

                        backupHeight = this.Height;
                        backupWidth = this.Width;

                        collapseAnimation.Maximum = this.Width;
                        collapseAnimation.Minimum = captionCtrl.Width;
                        if (animation == Animation.Yes)
                        {
                            collapseAnimation.Step = step;
                        }
                        else
                        {
                            collapseAnimation.Step = this.Width - captionCtrl.Width;
                        }
                    }
                    else
                    {
                        //have to extract caption ctrl because of the flickering involved
                        ChangeCaptionParent();

                        collapseAnimation.Maximum = backupWidth;
                        collapseAnimation.Minimum = captionCtrl.Width;
                        if (animation == Animation.Yes)
                        {
                            collapseAnimation.Step = -step;
                        }
                        else
                        {
                            collapseAnimation.Step = -(backupWidth - captionCtrl.Width);
                        }
                    }
                    break;
            }

            SetState();
            ShowControls();
            //start collapsing/expanding and set the new state
            if (state == ExtendedPanelState.Collapsed)
            {
                state = ExtendedPanelState.Expanding;
            }
            else
            {
                if (state == ExtendedPanelState.Expanded)
                {
                    state = ExtendedPanelState.Collapsing;
                }
            }
            collapseAnimation.Start();
        }


        private void captionCtrl_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!ShowCheckBox) return;
            if (CheckedChanged != null)
            {

                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (this.Controls[i] is CaptionCtrl)
                    {
                        continue;
                    }
                    this.Controls[i].Enabled = captionCtrl.Checked;
                }
                this.CheckedChanged(sender, e);
                
            }
        }


        private void OnNotifyAnimationEvent(object sender, int size)
        {
            this.Invoke(callbackNotifyAnimation, size);
        }


        private void OnNotifyAnimationFinished(object sender)
        {
            this.Invoke(callbackNotifyAnimationFinished);
        }


        private void CaptionDraggingEvent(object sender, CaptionDraggingEventArgs e)
        {
            if (moveable == true)
            {
                //set the new location
                if (this._CtrParent != null)
                {
                    this._CtrParent.Location = new Point(this._CtrParent.Location.X - e.Width, this._CtrParent.Location.Y - e.Height);
                    if(e.Height!=0)
                    {
                        if(this.EvtMouseMove!=null)
                        {
                            this.EvtMouseMove(sender,new MouseEventArgs( MouseButtons.Left,1,0,0,1));
                        }
                    }
                }
                else
                {
                    this.Location = new Point(this.Location.X - e.Width, this.Location.Y - e.Height);
                }
            }
        }
        #endregion
    }
}