using Emgu.CV;
using Emgu.CV.Structure;
using log4net;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
namespace FaceCompareThread
{
	public class CaptureHelper
	{
		private readonly FaceDeteiveThread _captureThread;
		private volatile Capture _capture;
		private volatile FaceCompreaSet _faceCompreaSet;
		private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private bool IsShow = true;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event ShowImageHandler ShowImageEventHandler;
		public CaptureHelper(FaceDeteiveThread captureThread, FaceCompreaSet faceCompreaSet)
		{
			this._captureThread = captureThread;
			this._faceCompreaSet = faceCompreaSet;
		}
		//protected override void Finalize()
		//{
		//	try
		//	{
		//		Capture expr_08 = this._capture;
		//		if (expr_08 != null)
		//		{
		//			expr_08.Dispose();
		//		}
		//	}
		//	finally
		//	{
		//		base.Finalize();
		//	}
		//}
		public void CaptureStart()
		{
			new Thread(new ThreadStart(this.StartCapture)).Start();
		}
		private void StartCapture()
		{
			if (this._faceCompreaSet != null)
			{
				Console.WriteLine(this._faceCompreaSet.CameraInfor.cameraType);
				if (this._faceCompreaSet.CameraInfor.cameraType == CameraType.IPCamera || this._faceCompreaSet.CameraInfor.cameraType == CameraType.YuShiCamera)
				{
					this._capture = new Capture(string.Format("rtsp://{0}:{1}@{2}:{3}/h264/ch1/main/av_stream", new object[]
					{
						this._faceCompreaSet.CameraInfor.UserName,
						this._faceCompreaSet.CameraInfor.PassWord,
						this._faceCompreaSet.CameraInfor.IP,
						this._faceCompreaSet.CameraInfor.DevicePort
					}));
				}
				else
				{
					if (this._faceCompreaSet.CameraInfor.cameraType == CameraType.DaHuaCamera)
					{
						string fileName = string.Format("rtsp://{0}:{1}@{2}:{3}/cam/realmonitor?channel=1&subtype=0", new object[]
						{
							this._faceCompreaSet.CameraInfor.UserName,
							this._faceCompreaSet.CameraInfor.PassWord,
							this._faceCompreaSet.CameraInfor.IP,
							this._faceCompreaSet.CameraInfor.DevicePort
						});
						this._capture = new Capture(fileName);
					}
					else
					{
						if (this._faceCompreaSet.CameraInfor.cameraType == CameraType.ZhongXingCamera)
						{
							string fileName2 = string.Format("rtsp://{0}:{1}@{2}:{3}/000100", new object[]
							{
								this._faceCompreaSet.CameraInfor.UserName,
								this._faceCompreaSet.CameraInfor.PassWord,
								this._faceCompreaSet.CameraInfor.IP,
								this._faceCompreaSet.CameraInfor.DevicePort
							});
							this._capture = new Capture(fileName2);
						}
						else
						{
							if (this._faceCompreaSet.CameraInfor.cameraType == CameraType.RTSP)
							{
								this._capture = new Capture(this._faceCompreaSet.CameraInfor.IP);
							}
							else
							{
								this._capture = new Capture(this._faceCompreaSet.CameraInfor.Index);
							}
						}
					}
				}
				this._capture.ImageGrabbed += new EventHandler(this.CaptureOnImageGrabbed);
				this._capture.Start();
			}
		}
        delegate void MyDel(Mat value);

        private void CaptureOnImageGrabbed(object sender, EventArgs eventArgs)
		{
			Mat mat = new Mat();
			try
			{
				if (this._capture != null)
				{
					if (!this._capture.Retrieve(mat, 0))
					{
						mat.Dispose();
					}
					else
					{
						if (mat.IsEmpty)
						{
							mat.Dispose();
						}
						else
						{
							Image<Bgr, byte> image = mat.ToImage<Bgr, byte>(false);
							if (this.IsShow)
							{
								this.IsShow = false;
                                MyDel t = delegate(Mat mat1)
								{
									this.OnShowImageEventHandler(mat1.Clone());
								};
                                t.BeginInvoke(mat.Clone(), delegate (IAsyncResult ar)
                                {
                                    this.IsShow = true;
                                }, null);

        //                        delegate (Mat mat1)
								//{
								//	this.OnShowImageEventHandler(mat1.Clone());
								//}.BeginInvoke(mat.Clone(), delegate(IAsyncResult ar)
								//{
								//	this.IsShow = true;
								//}, null);
							}
							this._captureThread.Start(image.Copy());
							image.Dispose();
						}
					}
				}
			}
			catch (Exception message)
			{
				this._log.Error(message);
			}
			finally
			{
				mat.Dispose();
			}
		}
		public void CaptureClose()
		{
			try
			{
				if (this._capture != null)
				{
					this._capture.ImageGrabbed -= new EventHandler(this.CaptureOnImageGrabbed);
					this._capture.Stop();
					if (this._faceCompreaSet.CameraInfor.cameraType == CameraType.USBCamera)
					{
						this._capture.Dispose();
					}
				}
			}
			catch (Exception message)
			{
				this._log.Error(message);
			}
		}
		protected virtual void OnShowImageEventHandler(Mat mat)
		{
			ShowImageHandler expr_06 = this.ShowImageEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06.BeginInvoke(mat, null, null);
		}
	}
}
