using Emgu.CV;
using Emgu.CV.UI;
using FaceCompareBase;
using FaceCompareThread;
using FaceDetectiveCtl.UnitBase;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FaceDetectiveCtl
{
	public sealed class FaceDetectiveControl : ImageBox
	{
		private delegate void DrawFarmeEventHandler();
		private DirectShowImageBase _directShowImageBase;
		private readonly FaceCompreaCore _faceCompreaCore = new FaceCompreaCore();
		private readonly FaceCompreaSet _faceCompreaSet = new FaceCompreaSet();
		private ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private CameraType _CaptureType;
		private string _IP = "192.168.0.64";
		private ushort _Port = 554;
		private string _UserName = "admin";
		private string _PassWord = "admin123";
		private int _UsbIndex;
		private Size _CaptureSize = new Size(640, 480);
		private bool _drawLine = true;
		private Mat _frameImage;
		private bool _isDrawPicture = true;
		private CameraInfor _Camera = new CameraInfor();
		private IContainer components;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event CompareSuccessHandler CompareSuccessEventHander;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event ShowFaceDeteiveImageHandler ShowFaceDeteiveImageEventHandler;
		[Category("人脸识别控件设置"), Description("获取或设置人脸识别检测阈值。相似度阈值应大于0小于1。"), DisplayName("阈值")]
		public float Threshold
		{
			get
			{
				if (this._faceCompreaSet.Threshold < 0f || this._faceCompreaSet.Threshold > 1f)
				{
					this._faceCompreaSet.Threshold = 0.6f;
				}
				return this._faceCompreaSet.Threshold;
			}
			set
			{
				this._faceCompreaSet.Threshold = value;
			}
		}
		[Category("人脸识别控件设置"), Description("两眼之间距离大于设置值时进行人脸比对；有效设置区间大于0小于200"), DisplayName("两眼之间距离")]
		public int Between2Eyes
		{
			get
			{
				if (this._faceCompreaSet.Between2Eyes < 0 || this._faceCompreaSet.Between2Eyes > 200)
				{
					this._faceCompreaSet.Between2Eyes = 30;
				}
				return this._faceCompreaSet.Between2Eyes;
			}
			set
			{
				this._faceCompreaSet.Between2Eyes = value;
			}
		}
		[Category("人脸识别控件设置"), Description("大于等于设置的人脸质量才进行对比；有效设置区间大于0小于1"), DisplayName("人脸抓拍质量")]
		public float Conf
		{
			get
			{
				if (this._faceCompreaSet.FaceConf < 0f || this._faceCompreaSet.FaceConf > 1f)
				{
					this._faceCompreaSet.FaceConf = 0.6f;
				}
				return this._faceCompreaSet.FaceConf;
			}
			set
			{
				this._faceCompreaSet.FaceConf = value;
			}
		}
		[Category("人脸识别控件设置"), Description("设置返回结果最大数量；有效设置范围大于0且小于100。"), DisplayName("返回结果最大数量")]
		public int CompareSuccessCount
		{
			get
			{
				if (this._faceCompreaSet.CompareSuccessCount < 0 || this._faceCompreaSet.CompareSuccessCount > 100)
				{
					this._faceCompreaSet.CompareSuccessCount = 5;
				}
				return this._faceCompreaSet.CompareSuccessCount;
			}
			set
			{
				this._faceCompreaSet.CompareSuccessCount = value;
			}
		}
		[Category("人脸识别控件设置"), Description("设置启动最大人脸识别，true:启动;false:不启动"), DisplayName("是否启动最大人脸检测")]
		public bool IsMaxFace
		{
			get
			{
				return this._faceCompreaSet.IsMaxFace;
			}
			set
			{
				this._faceCompreaSet.IsMaxFace = value;
			}
		}
		[Category("人脸识别控件设置"), Description("设置显示人脸框，true:启动;false:不启动"), DisplayName("是否显示人脸框")]
		public bool IsShowFaceRectangle
		{
			get
			{
				return this._faceCompreaSet.IsShowFaceRectangle;
			}
			set
			{
				this._faceCompreaSet.IsShowFaceRectangle = value;
			}
		}
		[Category("人脸识别控件设置"), Description("摄像机类型，USBCamera表示摄像机 IPCamera表示IP网络摄像机"), DisplayName("摄像机类型")]
		public CameraType CaptureType
		{
			get
			{
				return this._CaptureType;
			}
			set
			{
				this._CaptureType = value;
			}
		}
		[Category("人脸识别控件设置"), Description("摄像机IP，只有CaptureType设置为IPCamera（即IP网络摄像机）有效"), DisplayName("摄像机IP")]
		public string IP
		{
			get
			{
				return this._IP;
			}
			set
			{
				this._IP = value;
			}
		}
		[Category("人脸识别控件设置"), Description("摄像机端口，只有CaptureType设置为IPCamera（即IP网络摄像机）有效"), DisplayName("摄像机Rtsp端口")]
		public ushort Port
		{
			get
			{
				return this._Port;
			}
			set
			{
				this._Port = value;
			}
		}
		[Category("人脸识别控件设置"), Description("摄像机登陆用户名，只有CaptureType设置为IPCamera（即IP网络摄像机）有效"), DisplayName("摄像机登陆用户名")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				this._UserName = value;
			}
		}
		[Category("人脸识别控件设置"), Description("摄像机登陆密码，只有CaptureType设置为IPCamera（即海康摄像机）有效"), DisplayName("摄像机登陆密码")]
		public string PassWord
		{
			get
			{
				return this._PassWord;
			}
			set
			{
				this._PassWord = value;
			}
		}
		[Category("人脸识别控件设置"), Description("USB摄像机序号，只有CaptureType设置为USBCamera（即UBS摄像机）有效"), DisplayName("USB摄像机序号")]
		public int UsbIndex
		{
			get
			{
				return this._UsbIndex;
			}
			set
			{
				this._UsbIndex = value;
			}
		}
		[Category("人脸识别控件设置"), Description("摄像机分辨率"), DisplayName("摄像机分辨率")]
		public Size CaptureSize
		{
			get
			{
				return this._CaptureSize;
			}
			set
			{
				this._CaptureSize = value;
			}
		}
		[Category("人脸识别控件设置"), Description("设置帧间隔，例如设置3即表示每间隔3帧抓一张图像进行对比"), DisplayName("帧率")]
		public int FrameRate
		{
			get
			{
				if (this._faceCompreaSet.FrameRate < 0 || this._faceCompreaSet.FrameRate > 25)
				{
					this._faceCompreaSet.FrameRate = 3;
				}
				return this._faceCompreaSet.FrameRate;
			}
			set
			{
				this._faceCompreaSet.FrameRate = value;
			}
		}
		[Category("人脸识别控件设置"), Description("算法类型，整个程序运行一定要保持仅一种算法在被调用"), DisplayName("算法类型")]
		public FaceCompareType SetFaceCompareType
		{
			get
			{
				return this._faceCompreaSet.FaceCompareType;
			}
			set
			{
				this._faceCompreaSet.FaceCompareType = value;
			}
		}
		public FaceDetectiveControl()
		{
			this.InitializeComponent();
			this.BackColor = Color.Black;
			base.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
			this.DoubleBuffered = true;
		}
		public void Start()
		{
			try
			{
				this._faceCompreaCore.ShowImageEventHandler += new ShowImageHandler(this.ShowImageEventHandler);
				this._faceCompreaCore.DrawLinsEventHandler += new DrawLinsHandler(this.FaceCompreaCoreDrawLinsEventHandler);
				this._faceCompreaCore.CompareSuccessEventHandler += new CompareSuccessHandler(this.FaceCompreaCoreCompareSuccessEventHandler);
				this._faceCompreaCore.ShowFaceDeteiveImageEventHandler += new ShowFaceDeteiveImageHandler(this.FaceCompreaCoreOnShowFaceDeteiveImageEventHandler);
				this._faceCompreaCore.Start();
				this._directShowImageBase = new DirectShowImageBase();
				this._directShowImageBase.CreateDirect(base.Handle);
			}
			catch (Exception arg_86_0)
			{
				Console.WriteLine(arg_86_0);
			}
		}
		private void FaceCompreaCoreOnShowFaceDeteiveImageEventHandler(byte[] image, FaceModel faceModel, string port)
		{
			if (this.ShowFaceDeteiveImageEventHandler != null)
			{
				this.ShowFaceDeteiveImageEventHandler.BeginInvoke(image, faceModel, base.Name, null, null);
			}
		}
		private void FaceCompreaCoreCompareSuccessEventHandler(List<ResultInfo> recognitions, string port)
		{
			if (this.CompareSuccessEventHander != null)
			{
				this.CompareSuccessEventHander.BeginInvoke(recognitions, base.Name, null, null);
			}
		}
		private void FaceCompreaCoreDrawLinsEventHandler(List<FaceModel> faceModels, Size size)
		{
			if (base.Width > 0 && base.Height > 0 && this._drawLine)
			{
				this._drawLine = false;
				new DrawLinsHandler(this._DrowLine2).BeginInvoke(faceModels, size, delegate(IAsyncResult ar)
				{
					this._drawLine = true;
				}, null);
			}
		}
		private void _DrowLine2(List<FaceModel> faceModels, Size size)
		{
			foreach (FaceModel current in faceModels)
			{
				this._DrowLine2(current.FaceRect, Color.Chartreuse, 2, size);
			}
		}
		public void Init(List<FaceTemplate> faceTemplates)
		{
			this._faceCompreaSet.FaceTemplates = faceTemplates;
			this._faceCompreaSet.FaceCompareType = this.SetFaceCompareType;
			this._faceCompreaSet.CameraInfor = this.LoadCapTure();
			this._faceCompreaCore.Init(this._faceCompreaSet);
		}
		public void UnInit()
		{
			this._faceCompreaCore.UnInit();
		}

        delegate void MyDel1();
        private void ShowImageEventHandler(Mat img)
		{
			if (base.Width > 0 && base.Height > 0 && this._isDrawPicture)
			{
				this._isDrawPicture = false;
				this._frameImage = img;
                //delegate
                //{
                //	DirectShowImageBase expr_06 = this._directShowImageBase;
                //	if (expr_06 == null)
                //	{
                //		return;
                //	}
                //	expr_06.Render(this._frameImage.GetData(new int[0]), this._frameImage.Cols, this._frameImage.Height, this._frameImage.Step);
                //}.BeginInvoke(delegate(IAsyncResult ar)

                MyDel1 t = delegate
                {
                    DirectShowImageBase expr_06 = this._directShowImageBase;
                    if (expr_06 == null)
                    {
                        return;
                    }
                    expr_06.Render(this._frameImage.GetData(new int[0]), this._frameImage.Cols, this._frameImage.Height, this._frameImage.Step);
                };

                t.BeginInvoke(delegate (IAsyncResult ar)
                {
                    this._isDrawPicture = true;
                }, null); 
			}
		}
		private CameraInfor LoadCapTure()
		{
			this._Camera.Index = this._UsbIndex;
			this._Camera.cameraType = this.CaptureType;
			this._Camera.IP = this._IP;
			this._Camera.DevicePort = this._Port;
			this._Camera.UserName = this._UserName;
			this._Camera.PassWord = this._PassWord;
			this._Camera.ShowHwnd = base.Handle;
			this._Camera.Height = this.CaptureSize.Height;
			this._Camera.Width = this.CaptureSize.Width;
			return this._Camera;
		}
		public void Exit()
		{
			try
			{
				DirectShowImageBase expr_06 = this._directShowImageBase;
				if (expr_06 != null)
				{
					expr_06.Dispose();
				}
				this._faceCompreaCore.ShowImageEventHandler -= new ShowImageHandler(this.ShowImageEventHandler);
				this._faceCompreaCore.DrawLinsEventHandler -= new DrawLinsHandler(this.FaceCompreaCoreDrawLinsEventHandler);
				this._faceCompreaCore.ShowFaceDeteiveImageEventHandler -= new ShowFaceDeteiveImageHandler(this.FaceCompreaCoreOnShowFaceDeteiveImageEventHandler);
				this._faceCompreaCore.CompareSuccessEventHandler -= new CompareSuccessHandler(this.FaceCompreaCoreCompareSuccessEventHandler);
				this._faceCompreaCore.Stop();
			}
			catch (Exception message)
			{
				this._log.Error(message);
			}
		}
		[Bindable(true), Category("加载控件"), Description("用于添加需要进行人脸识别的人像模板")]
		public void IndsertFaceTemplate(FaceTemplate faceTemplate)
		{
			if (this._faceCompreaCore != null)
			{
				this._faceCompreaCore.AddFaceTemplate(faceTemplate);
			}
		}
		public List<FaceTemplate> GetFaceTemplate()
		{
			return this._faceCompreaCore.GetFaceTemplate();
		}
		public void RemoveFaceTemplate(string PersonID)
		{
			this._faceCompreaCore.RemoveFaceTempate(PersonID);
		}
		private void _DrowLine(FaceModelRect faceRect, Color color, int thickness)
		{
			decimal d = base.Height / this.CaptureSize.Height;
			decimal d2 = base.Width / this.CaptureSize.Width;
			faceRect.Top = Convert.ToInt32(Math.Ceiling(faceRect.Top * d));
			faceRect.Bottom = Convert.ToInt32(Math.Ceiling(faceRect.Bottom * d));
			faceRect.Left = Convert.ToInt32(Math.Ceiling(faceRect.Left * d2));
			faceRect.Right = Convert.ToInt32(Math.Ceiling(faceRect.Right * d2));
			Rectangle rect = FaceHelper.MC_GetRectangleByRect(faceRect);
			Graphics expr_D9 = base.CreateGraphics();
			expr_D9.SmoothingMode = SmoothingMode.AntiAlias;
			expr_D9.DrawRectangle(new Pen(color, (float)thickness), rect);
			expr_D9.Dispose();
		}
		private void _DrowLine2(FaceModelRect faceRect, Color color, int thickness, Size size)
		{
			using (Graphics graphics = base.CreateGraphics())
			{
				Point[][] arg_1D_0 = FaceDrawLines.GetPoints(faceRect, 6, size, base.Size);
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				Point[][] array = arg_1D_0;
				for (int i = 0; i < array.Length; i++)
				{
					Point[] points = array[i];
					graphics.DrawLines(new Pen(color, (float)thickness), points);
				}
				graphics.Save();
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			((ISupportInitialize)this).BeginInit();
			base.SuspendLayout();
			this.BackgroundImageLayout = ImageLayout.Stretch;
			base.SizeMode = PictureBoxSizeMode.StretchImage;
			((ISupportInitialize)this).EndInit();
			base.ResumeLayout(false);
		}
	}
}
