namespace facenet_0613
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.faceDetectiveControl1 = new FaceDetectiveCtl.FaceDetectiveControl();
            ((System.ComponentModel.ISupportInitialize)(this.faceDetectiveControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // faceDetectiveControl1
            // 
            this.faceDetectiveControl1.BackColor = System.Drawing.Color.Black;
            this.faceDetectiveControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.faceDetectiveControl1.Between2Eyes = 35;
            this.faceDetectiveControl1.CaptureSize = new System.Drawing.Size(640, 480);
            this.faceDetectiveControl1.CaptureType = FaceCompareThread.CameraType.IPCamera;
            this.faceDetectiveControl1.CompareSuccessCount = 1;
            this.faceDetectiveControl1.Conf = 0.7F;
            this.faceDetectiveControl1.FrameRate = 1;
            this.faceDetectiveControl1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.faceDetectiveControl1.IP = "192.168.0.69";
            this.faceDetectiveControl1.IsMaxFace = false;
            this.faceDetectiveControl1.IsShowFaceRectangle = true;
            this.faceDetectiveControl1.Location = new System.Drawing.Point(46, 62);
            this.faceDetectiveControl1.Name = "faceDetectiveControl1";
            this.faceDetectiveControl1.PassWord = "a83123008";
            this.faceDetectiveControl1.Port = ((ushort)(554));
            this.faceDetectiveControl1.SetFaceCompareType = FaceCompareBase.FaceCompareType.FaceComparePro;
            this.faceDetectiveControl1.Size = new System.Drawing.Size(170, 144);
            this.faceDetectiveControl1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.faceDetectiveControl1.TabIndex = 2;
            this.faceDetectiveControl1.TabStop = false;
            this.faceDetectiveControl1.Threshold = 0.7F;
            this.faceDetectiveControl1.UsbIndex = 0;
            this.faceDetectiveControl1.UserName = "admin";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.faceDetectiveControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.faceDetectiveControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FaceDetectiveCtl.FaceDetectiveControl faceDetectiveControl1;
    }
}

