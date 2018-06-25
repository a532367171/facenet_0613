using Emgu.CV;
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
	public class FaceCompreaCore : IDisposable
	{
		private Thread _captureCaller;
		private FaceDeteiveThread _faceDeteiveThread;
		private Thread _faceCaller;
		private FaceCompreaThread _faceCompreaThread;
		private CaptureHelper _captureHelper;
		private volatile FaceCompreaSet _faceCompreaSet;
		private volatile IFaceCompare _faceCompare1;
		private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event CompareSuccessHandler CompareSuccessEventHandler;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event DrawLinsHandler DrawLinsEventHandler;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event ShowImageHandler ShowImageEventHandler;
		[method: CompilerGenerated]
		[CompilerGenerated]
		public event ShowFaceDeteiveImageHandler ShowFaceDeteiveImageEventHandler;
		protected virtual void OnDrawLinsEventHandler(List<FaceModel> facemodels, Size size)
		{
			DrawLinsHandler expr_06 = this.DrawLinsEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06.BeginInvoke(facemodels, size, null, null);
		}
		protected virtual void OnCompareSuccessEventHandler(List<ResultInfo> resultInfos, string port)
		{
			CompareSuccessHandler expr_06 = this.CompareSuccessEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06.BeginInvoke(resultInfos, string.Empty, null, null);
		}
		protected virtual void OnShowImageEventHandler(Mat img)
		{
			ShowImageHandler expr_06 = this.ShowImageEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06.BeginInvoke(img, null, null);
		}
		protected virtual void OnShowFaceDeteiveImageEventHandler(byte[] imBytes, FaceModel faceModel, string port)
		{
			ShowFaceDeteiveImageHandler expr_06 = this.ShowFaceDeteiveImageEventHandler;
			if (expr_06 == null)
			{
				return;
			}
			expr_06.BeginInvoke(imBytes, faceModel, port, null, null);
		}
		public void Start()
		{
			try
			{
				if (this._faceDeteiveThread != null)
				{
					this._faceDeteiveThread.DrawLinsEventHandler += new DrawLinsHandler(this.OnDrawLinsEventHandler);
				}
				if (this._faceCompreaThread != null)
				{
					this._faceCompreaThread.ShowFaceDeteiveImageEventHandler += new ShowFaceDeteiveImageHandler(this.OnShowFaceDeteiveImageEventHandler);
				}
				if (this._faceCompreaThread != null)
				{
					this._faceCompreaThread.CompareSuccessEventHandler += new CompareSuccessHandler(this.OnCompareSuccessEventHandler);
				}
				if (this._captureHelper != null)
				{
					this._captureHelper.ShowImageEventHandler += new ShowImageHandler(this.OnShowImageEventHandler);
				}
				if (this._faceCaller != null)
				{
					this._faceCaller.Start();
				}
				if (this._captureCaller != null)
				{
					this._captureCaller.Start();
				}
				if (this._captureHelper != null)
				{
					this._captureHelper.CaptureStart();
				}
			}
			catch (Exception message)
			{
				this._log.Error(message);
			}
		}
		public void Init(FaceCompreaSet faceCompreaSet)
		{
			this._faceCompreaSet = faceCompreaSet;
			this._faceCompreaSet.FaceTemplates = faceCompreaSet.FaceTemplates;
			this._faceCompare1 = FaceFactory.Create(this._faceCompreaSet.FaceCompareType);
			this._faceCompare1.CreateFaceEngne();
			this._faceCompreaThread = new FaceCompreaThread(this._faceCompreaSet, this._faceCompare1);
			this._faceDeteiveThread = new FaceDeteiveThread(this._faceCompreaThread, this._faceCompreaSet);
			this._faceCaller = new Thread(new ThreadStart(this._faceCompreaThread.Execute));
			this._captureCaller = new Thread(new ThreadStart(this._faceDeteiveThread.Execute));
			this._captureHelper = new CaptureHelper(this._faceDeteiveThread, this._faceCompreaSet);
		}
		public void Stop()
		{
			try
			{
				if (this._captureHelper != null)
				{
					this._captureHelper.ShowImageEventHandler -= new ShowImageHandler(this.OnShowImageEventHandler);
				}
				if (this._faceDeteiveThread != null)
				{
					this._faceDeteiveThread.DrawLinsEventHandler -= new DrawLinsHandler(this.OnDrawLinsEventHandler);
				}
				if (this._faceCompreaThread != null)
				{
					this._faceCompreaThread.ShowFaceDeteiveImageEventHandler -= new ShowFaceDeteiveImageHandler(this.OnShowFaceDeteiveImageEventHandler);
				}
				if (this._captureHelper != null)
				{
					this._captureHelper.CaptureClose();
				}
				if (this._faceCompreaThread != null)
				{
					this._faceCompreaThread.Close();
					this._faceCaller.Join();
				}
				if (this._faceDeteiveThread != null)
				{
					this._faceDeteiveThread.Close();
					this._captureCaller.Join();
				}
			}
			catch (Exception message)
			{
				this._log.Error(message);
			}
		}
		public void AddFaceTemplate(FaceTemplate template)
		{
			List<FaceTemplate> faceTemplates = this._faceCompreaSet.FaceTemplates;
			lock (faceTemplates)
			{
				this._faceCompreaSet.FaceTemplates.Add(template);
			}
		}
		public void RemoveFaceTempate(string PersonId)
		{
			List<FaceTemplate> faceTemplates = this._faceCompreaSet.FaceTemplates;
			lock (faceTemplates)
			{
				FaceTemplate faceTemplate = this._faceCompreaSet.FaceTemplates.FirstOrDefault((FaceTemplate p) => p.PersonId.Equals(PersonId));
				if (faceTemplate != null)
				{
					this._faceCompreaSet.FaceTemplates.Remove(faceTemplate);
				}
			}
		}
		public List<FaceTemplate> GetFaceTemplate()
		{
			return this._faceCompreaSet.FaceTemplates.ToList<FaceTemplate>();
		}
		public void UnInit()
		{
			this._faceCompare1.Dispose();
		}
		public void Dispose()
		{
			this._faceCompare1.Dispose();
		}
	}
}
