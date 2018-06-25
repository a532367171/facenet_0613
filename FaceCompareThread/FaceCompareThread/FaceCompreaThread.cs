using Emgu.CV;
using Emgu.CV.Structure;
using FaceCompareBase;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
namespace FaceCompareThread
{
	public class FaceCompreaThread
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class class_c
		{
			public static readonly FaceCompreaThread.class_c class_c_9 = new FaceCompreaThread.class_c();
			public static Func<ResultInfo, float> class_c_9__13_0;
			internal float _Execute_b__13_0(ResultInfo a)
			{
				return a.Score;
			}
		}
		private readonly AutoResetEvent _haveImage = new AutoResetEvent(false);
		private ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private volatile bool _stop;
		private readonly IFaceCompare _ifaceCompareBase;
		private readonly FaceCompreaSet _faceCompreaSet;
		private volatile Image<Bgr, byte> _image;
		private volatile FaceModel[] _faceModels;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event ShowFaceDeteiveImageHandler ShowFaceDeteiveImageEventHandler;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event CompareSuccessHandler CompareSuccessEventHandler;
		public FaceCompreaThread(FaceCompreaSet faceCompreaSet, IFaceCompare faceCompare)
		{
			this._stop = false;
			this._faceCompreaSet = faceCompreaSet;
			this._ifaceCompareBase = faceCompare;
		}
		public void Close()
		{
			if (!this._stop)
			{
				this._stop = true;
				this._haveImage.Set();
			}
		}
		public void Execute()
		{
			while (!this._stop)
			{
				this._haveImage.WaitOne();
				if (!this._stop)
				{
					try
					{
						if (this._image != null && this._faceModels != null)
						{
							Image<Bgr, byte> image = this._image.Copy();
							FaceModel[] array = this._faceModels.ToArray<FaceModel>();
							byte[] data = image.Mat.GetData(new int[0]);
							int cols = image.Mat.Cols;
							int rows = image.Mat.Rows;
							int widthstep = image.Mat.Cols * 3;
							FaceModel[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								FaceModel faceModel = array2[i];
								if (faceModel.FaceRect.fConf >= this._faceCompreaSet.FaceConf)
								{
									Rectangle rect = FaceHelper.MC_GetRectangleByRect(faceModel.FaceRect, image.Size);
									Image<Bgr, byte> image2 = image.GetSubRect(rect).Copy();
									this.OnShowFaceDeteiveImageEventHandler(image2.ToJpegData(95), faceModel, "");
									FaceModel faceModel2 = faceModel.Clone();
									if (this._ifaceCompareBase.ExtractFeature(data, cols, rows, widthstep, ref faceModel2) >= 0)
									{
										List<ResultInfo> list = new List<ResultInfo>();
										foreach (FaceTemplate current in this._faceCompreaSet.FaceTemplates.ToList<FaceTemplate>())
										{
											float num = this._ifaceCompareBase.Compare2Feature(faceModel2.Feature, current.FaceFeature);
											if (num >= this._faceCompreaSet.Threshold)
											{
												list.Add(new ResultInfo
												{
													FaceImage = image2.ToJpegData(95),
													FaceTemplate = current,
													FaceModel = faceModel2,
													Score = num
												});
											}
										}
										image2.Dispose();
										if (list.Count > 0)
										{
											IEnumerable<ResultInfo> arg_1E8_0 = list;
											Func<ResultInfo, float> arg_1E8_1;
											if ((arg_1E8_1 = FaceCompreaThread.class_c.class_c_9__13_0) == null)
											{
												arg_1E8_1 = (FaceCompreaThread.class_c.class_c_9__13_0 = new Func<ResultInfo, float>(FaceCompreaThread.class_c.class_c_9._Execute_b__13_0));
											}
											list = arg_1E8_0.OrderByDescending(arg_1E8_1).Take(this._faceCompreaSet.CompareSuccessCount).ToList<ResultInfo>();
											this.OnCompareSuccessEventHandler(list);
										}
									}
									if (this._faceCompreaSet.IsMaxFace)
									{
										break;
									}
								}
							}
							image.Dispose();
						}
					}
					catch (Exception message)
					{
						this._log.Error(message);
					}
				}
			}
		}
		public void Start(Image<Bgr, byte> image, FaceModel[] faceModels)
		{
			if (!this._stop)
			{
				this._image = image;
				this._haveImage.Set();
				this._faceModels = faceModels;
			}
		}
		protected virtual void OnShowFaceDeteiveImageEventHandler(byte[] imbytes, FaceModel facemodel, string port)
		{
			ShowFaceDeteiveImageHandler expr_06 = this.ShowFaceDeteiveImageEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06.BeginInvoke(imbytes, facemodel, port, null, null);
		}
		protected virtual void OnCompareSuccessEventHandler(List<ResultInfo> recognitions)
		{
			CompareSuccessHandler expr_06 = this.CompareSuccessEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06.BeginInvoke(recognitions, string.Empty, null, null);
		}
	}
}
