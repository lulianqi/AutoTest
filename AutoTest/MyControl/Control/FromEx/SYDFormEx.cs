using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace SYDControls
{

    /// <summary>
    /// 自定义扩展窗体FormEx
    /// </summary>
    public class SYDFormEx : FormBase
    {
        #region Field

        //窗体圆角的半径
        private int _radius = 5;

        //是否允许窗体改变大小
        private bool _canResize = true;  

        //绘制窗体标题的字体、标题的颜色
        private Font _textFont = new Font("微软雅黑", 10.0f, FontStyle.Bold);
        private Color _textForeColor = Color.FromArgb(250, Color.White);

        //是否绘制带有阴影的窗体标题
        private bool _isTextWithShadow = false;   
        private Color _textShadowColor = Color.FromArgb(2, Color.Black); //标题的阴影颜色
        private int _textShadowWidth = 4;  //标题阴影的宽度

        private Image _formFringe = RenderHelper.GetImageFormResourceStream("ControlExs.SYDFormEx.Res.fringe_bkg.png");
        private Image _formBkg;

        /// <summary>
        /// 边框图片  add by bluner
        /// </summary>
        private Image _borderImage = RenderHelper.GetImageFormResourceStream("ControlExs.SYDFormEx.Res.framemod.png");

        //系统按钮管理器
        private SystemButtonManager _systemButtonManager;  

        #endregion

        #region Constructor

        public SYDFormEx()
        {
            InitializeComponent();
            FormExIni();
            _systemButtonManager = new SystemButtonManager(this);
        }

        #endregion

        #region Properties

        [Description("窗体圆角的半径")]
        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    this.Invalidate();
                }
            }
        }

        [Description("是否允许窗体改变大小")]
        public bool CanResize
        {
            get
            {
                return _canResize;
            }
            set
            {
                if (_canResize != value)
                {
                    _canResize = value;
                }
            }
        }

        public override Image BackgroundImage
        {
            get
            {
                return _formBkg;
            }
            set
            {
                if (_formBkg != value)
                {
                    _formBkg = value;
                    Invalidate();
                }
            }
        }

        [Description("用于绘制窗体标题的字体")]
        public Font TextFont
        {
            get { return _textFont; }
            set
            {
                if (_textFont != value)
                {
                    _textFont = value;
                }
            }

        }

        [Description("用于绘制窗体标题的颜色")]
        public Color TextForeColor
        {
            get { return _textForeColor; }
            set
            {
                if (_textForeColor != value)
                { _textForeColor = value; }
            }
        }

        [Description("是否绘制带有阴影的窗体标题")]
        public bool TextWithShadow
        {
            get { return _isTextWithShadow; }
            set
            {
                if (_isTextWithShadow != value)
                {
                    _isTextWithShadow = value;
                }
            }
        }

        [Description("如果TextWithShadow属性为True,则使用该属性绘制阴影")]
        public Color TextShadowColor
        {
            get { return _textShadowColor; }
            set
            {
                if (_textShadowColor != value)
                {
                    _textShadowColor = value;
                }
            }
        }

        [Description("如果TextWithShadow属性为True,则使用该属性获取或色泽阴影的宽度")]
        public int TextShadowWidth
        {
            get { return _textShadowWidth; }
            set
            {
                if (_textShadowWidth != value)
                {
                    _textShadowWidth = value;
                }
            }
        }

        [Browsable(false)]
        [Description("返回窗体关闭系统按钮所在的坐标矩形")]
        public Rectangle CloseBoxRect
        {
            get { return SystemButtonManager.SystemButtonArray[0].LocationRect; }
        }

        [Browsable(false)]
        [Description("返回窗体最大化或者还原系统按钮所在的坐标矩形")]
        public Rectangle MaximiziBoxRect
        {
            get { return SystemButtonManager.SystemButtonArray[1].LocationRect; }
        }

        [Browsable(false)]
        [Description("返回窗体最小化系统按钮所在的坐标矩形")]
        public Rectangle MinimiziBoxRect 
        {
            get { return SystemButtonManager.SystemButtonArray[2].LocationRect; }
        }

        internal Rectangle IconRect
        {
            get
            {
                if (base.ShowIcon && base.Icon != null)
                {
                    return new Rectangle(8, 6, SystemInformation.SmallIconSize.Width, SystemInformation.SmallIconSize.Width);
                }
                return Rectangle.Empty;
            }
        }

        internal Rectangle TextRect
        {
            get
            {
                if (base.Text.Length != 0)
                {
                    return new Rectangle(IconRect.Right + 2, 4, Width - (8 + IconRect.Width + 2), TextFont.Height);
                }
                return Rectangle.Empty;
            }
        }

        internal SystemButtonManager SystemButtonManager
        {
            get
            {
                if (_systemButtonManager == null)
                {
                    _systemButtonManager = new SystemButtonManager(this);
                }
                return _systemButtonManager;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = value; }
        }

        #endregion

        #region Overrides

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    if (MaximizeBox) { cp.Style |= (int)WindowStyle.WS_MAXIMIZEBOX; }
                    if (MinimizeBox) { cp.Style |= (int)WindowStyle.WS_MINIMIZEBOX; }
                    //cp.ExStyle |= (int)WindowStyle.WS_CLIPCHILDREN;  //防止因窗体控件太多出现闪烁
                    cp.ClassStyle |= (int)ClassStyle.CS_DropSHADOW;  //实现窗体边框阴影效果
                }
                return cp;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            RenderHelper.SetFormRoundRectRgn(this, Radius);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateSystemButtonRect();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RenderHelper.SetFormRoundRectRgn(this, Radius);
            UpdateSystemButtonRect();
            UpdateMaxButton();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_ERASEBKGND:
                    m.Result = IntPtr.Zero;
                    break;
                case Win32.WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Move);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Down);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Up);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SystemButtonManager.ProcessMouseOperate(Point.Empty, MouseOperate.Leave);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //draw BackgroundImage
            if (BackgroundImage != null)
            {
                switch (BackgroundImageLayout)
                {
                    case ImageLayout.Stretch:
                    case ImageLayout.Zoom:
                        e.Graphics.DrawImage(
                            _formBkg,
                            ClientRectangle,
                            new Rectangle(0, 0, _formBkg.Width, _formBkg.Height),
                            GraphicsUnit.Pixel);
                        break;
                    case ImageLayout.Center:
                    case ImageLayout.None:
                    case ImageLayout.Tile:
                        e.Graphics.DrawImage(
                            _formBkg,
                            ClientRectangle,
                            ClientRectangle,
                            GraphicsUnit.Pixel);
                        break;
                }
            }

            //draw system buttons
            SystemButtonManager.DrawSystemButtons(e.Graphics);

            

            //draw icon
            if (Icon != null && ShowIcon)
            {
                e.Graphics.DrawIcon(Icon, IconRect);
            }

            //draw text
            if (Text.Length != 0)
            {
                if (TextWithShadow)
                {
                    using (Image textImg = RenderHelper.GetStringImgWithShadowEffect(Text, TextFont, TextForeColor, TextShadowColor, TextShadowWidth))
                    {
                        e.Graphics.DrawImage(textImg,TextRect.Location);
                    }
                }
                else
                {
                    TextRenderer.DrawText(
                    e.Graphics,
                    Text, TextFont,
                    TextRect,
                    TextForeColor,
                    TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
                }
            }

            //绘制窗体边框
            this.DrawFrameBorder(e.Graphics);

            //draw fringe
           // RenderHelper.DrawFormFringe(this, e.Graphics, _formFringe, Radius);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_systemButtonManager != null)
                {
                    _systemButtonManager.Dispose();
                    _systemButtonManager = null;

                    _formFringe.Dispose();
                    _formFringe = null;

                    _textFont.Dispose();
                    _textFont = null;

                    if (_formBkg != null)
                    {
                        _formBkg.Dispose();
                        _formBkg = null;
                    }
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Private Methods

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SYDFormEx
            // 
            this.ClientSize = new System.Drawing.Size(684, 402);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SYDFormEx";
            this.ResumeLayout(false);

        }

        private void FormExIni()
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            SetStyles();
        }

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private void WmNcHitTest(ref Message m)  //调整窗体大小
        {
            int wparam = m.LParam.ToInt32();
            Point mouseLocation = new Point(RenderHelper.LOWORD(wparam), RenderHelper.HIWORD(wparam));
            mouseLocation = PointToClient(mouseLocation);

            if (WindowState != FormWindowState.Maximized)
            {
                if (CanResize == true)
                {
                    if (mouseLocation.X < 5 && mouseLocation.Y < 5)
                    {
                        m.Result = new IntPtr(Win32.HTTOPLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 5 && mouseLocation.Y < 5)
                    {
                        m.Result = new IntPtr(Win32.HTTOPRIGHT);
                        return;
                    }

                    if (mouseLocation.X < 5 && mouseLocation.Y > Height - 5)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOMLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 5 && mouseLocation.Y > Height - 5)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOMRIGHT);
                        return;
                    }

                    if (mouseLocation.Y < 3)
                    {
                        m.Result = new IntPtr(Win32.HTTOP);
                        return;
                    }

                    if (mouseLocation.Y > Height - 3)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOM);
                        return;
                    }

                    if (mouseLocation.X < 3)
                    {
                        m.Result = new IntPtr(Win32.HTLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 3)
                    {
                        m.Result = new IntPtr(Win32.HTRIGHT);
                        return;
                    }
                }
            }
            m.Result = new IntPtr(Win32.HTCLIENT);
        }

        private void UpdateMaxButton()
        {
            bool isMax = WindowState == FormWindowState.Maximized;
            if (isMax)
            {
                SystemButtonManager.SystemButtonArray[1].NormalImg = RenderHelper.GetImageFormResourceStream("ControlExs.SYDFormEx.Res.SystemButtons.restore_normal.png");
                SystemButtonManager.SystemButtonArray[1].HighLightImg = RenderHelper.GetImageFormResourceStream("ControlExs.SYDFormEx.Res.SystemButtons.restore_highlight.png");
                SystemButtonManager.SystemButtonArray[1].DownImg = RenderHelper.GetImageFormResourceStream("ControlExs.SYDFormEx.Res.SystemButtons.restore_down.png");
                SystemButtonManager.SystemButtonArray[1].ToolTip = "还原";
            }
            else
            {
                SystemButtonManager.SystemButtonArray[1].NormalImg = RenderHelper.GetImageFormResourceStream("ControlExs.SYDFormEx.Res.SystemButtons.max_normal.png");
                SystemButtonManager.SystemButtonArray[1].HighLightImg = RenderHelper.GetImageFormResourceStream("ControlExs.SYDFormEx.Res.SystemButtons.max_highlight.png");
                SystemButtonManager.SystemButtonArray[1].DownImg = RenderHelper.GetImageFormResourceStream("ControlExs.SYDFormEx.Res.SystemButtons.max_down.png");
                SystemButtonManager.SystemButtonArray[1].ToolTip = "最大化";
            }
        }

        protected void UpdateSystemButtonRect()
        {
            bool isShowMaxButton = MaximizeBox;
            bool isShowMinButton = MinimizeBox;
            Rectangle closeRect = new Rectangle(
                    Width - SystemButtonManager.SystemButtonArray[0].NormalImg.Width,
                    -1,
                    SystemButtonManager.SystemButtonArray[0].NormalImg.Width,
                    SystemButtonManager.SystemButtonArray[0].NormalImg.Height);

            //update close button location rect.
            SystemButtonManager.SystemButtonArray[0].LocationRect = closeRect;

            //Max
            if (isShowMaxButton)
            {
                SystemButtonManager.SystemButtonArray[1].LocationRect = new Rectangle(
                    closeRect.X - SystemButtonManager.SystemButtonArray[1].NormalImg.Width,
                    -1,
                    SystemButtonManager.SystemButtonArray[1].NormalImg.Width,
                    SystemButtonManager.SystemButtonArray[1].NormalImg.Height);
            }
            else
            {
                SystemButtonManager.SystemButtonArray[1].LocationRect = Rectangle.Empty;
            }

            //Min
            if (!isShowMinButton)
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = Rectangle.Empty;
                return;
            }
            if (isShowMaxButton)
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = new Rectangle(
                    SystemButtonManager.SystemButtonArray[1].LocationRect.X - SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                    -1,
                    SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                    SystemButtonManager.SystemButtonArray[2].NormalImg.Height);
            }
            else
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = new Rectangle(
                   closeRect.X - SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                   -1,
                   SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                   SystemButtonManager.SystemButtonArray[2].NormalImg.Height);
            }
        }

        /// <summary>
        /// 绘制窗体边框
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrameBorder(Graphics g)
        {
            Rectangle rect = this.ClientRectangle;
            int cut1 = 1;
            int cut2 = 5;
            //左上角
            g.DrawImage(this._borderImage, new Rectangle(rect.X, rect.Y, cut2, cut2), 0, 0, cut2, cut2, GraphicsUnit.Pixel);
            //上边
            g.DrawImage(this._borderImage, new Rectangle(rect.X + cut2, rect.Y, rect.Width - cut2 * 2, cut1), cut2, 0, this._borderImage.Width - cut2 * 2, cut2, GraphicsUnit.Pixel);
            //右上角
            g.DrawImage(this._borderImage, new Rectangle(rect.X + rect.Width - cut2, rect.Y, cut2, cut2), this._borderImage.Width - cut2, 0, cut2, cut2, GraphicsUnit.Pixel);
            //左边
            g.DrawImage(this._borderImage, new Rectangle(rect.X, rect.Y + cut2, cut1, rect.Height - cut2 * 2), 0, cut2, cut1, this._borderImage.Height - cut2 * 2, GraphicsUnit.Pixel);
            //左下角
            g.DrawImage(this._borderImage, new Rectangle(rect.X, rect.Y + rect.Height - cut2, cut2, cut2), 0, this._borderImage.Height - cut2, cut2, cut2, GraphicsUnit.Pixel);
            //右边
            g.DrawImage(this._borderImage, new Rectangle(rect.X + rect.Width - cut1, rect.Y + cut2, cut1, rect.Height - cut2 * 2), this._borderImage.Width - cut1, cut2, cut1, this._borderImage.Height - cut2 * 2, GraphicsUnit.Pixel);
            //右下角
            g.DrawImage(this._borderImage, new Rectangle(rect.X + rect.Width - cut2, rect.Y + rect.Height - cut2, cut2, cut2), this._borderImage.Width - cut2, this._borderImage.Height - cut2, cut2, cut2, GraphicsUnit.Pixel);
            //下边
            g.DrawImage(this._borderImage, new Rectangle(rect.X + cut2, rect.Y + rect.Height - cut1, rect.Width - cut2 * 2, cut1), cut2, this._borderImage.Height - cut1, this._borderImage.Width - cut2 * 2, cut1, GraphicsUnit.Pixel);
        }
        #endregion


    }
}
