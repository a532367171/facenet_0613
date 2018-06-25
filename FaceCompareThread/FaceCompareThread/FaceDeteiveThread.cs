using Emgu.CV;
using Emgu.CV.Structure;
using FaceCompareBase;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using WisFaceBase;
namespace FaceCompareThread
{
	public class FaceDeteiveThread
	{
		private volatile bool _stop;
		private readonly FaceCompreaThread _faceCompreaThread;
		private ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private readonly AutoResetEvent _haveVido = new AutoResetEvent(false);
		private volatile FaceCompreaSet _faceCompreaSet;
		private readonly WinFaceCore _winFaceCore;
		private readonly FaceDetectV4 _faceDetectV4;
		private readonly IFaceCompare iCompare;
		private Stopwatch _stopwatch = new Stopwatch();
		private int _a = 1;
		private volatile Image<Bgr, byte> _image;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event DrawLinsHandler DrawLinsEventHandler;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event ShowImageHandler ShowImageEventHandler;
		public FaceDeteiveThread(FaceCompreaThread faceCompreaThread, FaceCompreaSet faceCompreaSet)
		{
			this._faceCompreaThread = faceCompreaThread;
			this._faceCompreaSet = faceCompreaSet;
			this._stop = false;
			if (faceCompreaSet.FaceCompareType == FaceCompareType.FaceCompareV4)
			{
				this._faceDetectV4 = new FaceDetectV4();
				this._faceDetectV4.CreateDetectFaceEngine();
				return;
			}
			this._winFaceCore = new WinFaceCore();
			this._winFaceCore.CreateFaceEngine();
		}
		public void Execute()
		{
			while (!this._stop)
			{
				this._haveVido.WaitOne();
				if (!this._stop)
				{
					try
					{
						if (this._image != null)
						{
							if (this._a >= this._faceCompreaSet.FrameRate)
							{
								Image<Bgr, byte> image = this._image.Copy();
								byte[] data = image.Mat.GetData(new int[0]);
								int cols = image.Mat.Cols;
								int rows = image.Mat.Rows;
								int widthstep = image.Mat.Cols * 3;
								FaceModel[] array = null;
								int num;
								if (this._faceCompreaSet.FaceCompareType != FaceCompareType.FaceCompareV4)
								{
									WisFace[] array2;
									num = this._winFaceCore.DetectFaces(data, cols, rows, widthstep, out array2, 20);
									if (num > 0)
									{
										array = new FaceModel[num];
										for (int i = 0; i < num; i++)
										{
											array[i] = FaceUnit.WisFaceToFaceModel(array2[i]);
										}
										if (this.DrawLinsEventHandler != null)
										{
											this.OnDrawLinsEventHandler(array.ToList<FaceModel>(), image.Size);
										}
									}
								}
								else
								{
									num = this._faceDetectV4.DetectFaces4Image_only(data, cols, rows, widthstep, out array, 20);
									if (num > 0)
									{
										array = array.Take(num).ToArray<FaceModel>();
										if (this.DrawLinsEventHandler != null)
										{
											this.OnDrawLinsEventHandler(array.ToList<FaceModel>(), image.Size);
										}
									}
								}
								if (num > 0)
								{
									this._faceCompreaThread.Start(image.Clone(), array.ToArray<FaceModel>());
								}
								image.Dispose();
								this._a = 1;
							}
							else
							{
								this._a++;
							}
						}
					}
					catch (Exception arg)
					{
						this._log.Error("fd" + arg);
					}
				}
			}
		}
		public void Close()
		{
			if (!this._stop)
			{
				this._stop = true;
				this._haveVido.Set();
			}
		}
		public void Start(IImage mat)
		{
			if (!this._stop)
			{
				this._image = (Image<Bgr, byte>)mat;
				this._haveVido.Set();
			}
		}
		protected virtual void OnDrawLinsEventHandler(List<FaceModel> facemodels, Size size)
		{
			DrawLinsHandler expr_06 = this.DrawLinsEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06.BeginInvoke(facemodels, size, null, null);
		}
		protected virtual void OnShowImageEventHandler(Mat mat)
		{
			ShowImageHandler expr_06 = this.ShowImageEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06(mat);
		}
	}
}
