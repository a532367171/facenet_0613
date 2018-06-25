using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MCFaceRecognitionVideo.resources;
using MCFaceRecognitionVideo.UnitBase;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
namespace MCFaceRecognitionVideo
{
	public class SystemSetting : XtraForm
	{
		private Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		private IContainer components;
		private Panel panel1;
		private ImageList imageList1;
		private Label lbl_IsSaveImage;
		private Label lbl_PhotoSavePath;
		private ComboBoxEdit cbx_IsCapturePhotos;
		private SimpleButton btn_Path;
		private TextEdit txt_PhotSavePath;
		private Label lbl_HomePageStyle;
		private PictureEdit pictureEdit2;
		private PictureEdit pictureEdit1;
		private RadioButton radio_sytle2;
		private RadioButton radio_Style;
		private ComboBoxEdit cbx_IsPlaySound;
		private Label lbl_IsRunSound;
		private SimpleButton btn_save;
		private TextEdit txt_systemName;
		private Label lbl_SoftwareName;
		public SystemSetting()
		{
			this.InitializeComponent();
			if (LanguageSet.Resource != null)
			{
				this.UpDataMainFormMenuLanguage(LanguageSet.Resource);
			}
			this.cbx_IsCapturePhotos.Text = XMLHelper.getXmlValue("PhotoSetting", "IsCapturePhotos");
			this.txt_PhotSavePath.Text = XMLHelper.getXmlValue("PhotoSetting", "PhotoSavePath");
			this.txt_systemName.Text = ConfigurationManager.AppSettings["MainTitle"];
			this.cbx_IsPlaySound.Text = (ConfigurationManager.AppSettings["IsPlaySound"].Equals("true") ? UnitField.Yes : UnitField.No);
			if (ConfigurationManager.AppSettings["MainForm"].Equals("VideoForm"))
			{
				this.radio_Style.Checked = true;
				this.radio_sytle2.Checked = false;
				return;
			}
			this.radio_sytle2.Checked = true;
			this.radio_Style.Checked = false;
		}
		private void tsb_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}
		private void btn_Path_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowDialog();
			this.txt_PhotSavePath.Text = folderBrowserDialog.SelectedPath;
		}
		private void btn_save_Click(object sender, EventArgs e)
		{
			XMLHelper.setXmlValue("PhotoSetting", "IsCapturePhotos", this.cbx_IsCapturePhotos.Text);
			XMLHelper.setXmlValue("PhotoSetting", "PhotoSavePath", this.txt_PhotSavePath.Text);
			this.cfa.AppSettings.Settings["MainTitle"].Value = this.txt_systemName.Text;
			this.cfa.AppSettings.Settings["IsPlaySound"].Value = (this.cbx_IsPlaySound.Text.Equals(UnitField.Yes) ? "true" : "false");
			this.cfa.AppSettings.Settings["MainForm"].Value = (this.radio_Style.Checked ? "VideoForm" : "VideoForm2");
			this.cfa.Save();
			if (XtraMessageBox.Show(UnitField.SystemSave, UnitField.SystemMessage, MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Process.Start(Assembly.GetExecutingAssembly().Location);
				base.Close();
				Process.GetCurrentProcess().Kill();
			}
		}
		private void UpDataMainFormMenuLanguage(ResourceManager rm)
		{
			this.cbx_IsCapturePhotos.Properties.Items[0] = UnitField.Yes;
			this.cbx_IsCapturePhotos.Properties.Items[1] = UnitField.No;
			this.lbl_IsSaveImage.Text = UnitField.SaveImage;
			this.lbl_PhotoSavePath.Text = UnitField.PhotoSavePath;
			this.lbl_IsRunSound.Text = UnitField.IsRunSound;
			this.lbl_SoftwareName.Text = UnitField.SoftwareName;
			this.lbl_HomePageStyle.Text = UnitField.HomePageStyle;
			this.radio_Style.Text = UnitField.Style1;
			this.radio_sytle2.Text = UnitField.Style2;
			this.btn_save.Text = UnitField.Save;
			this.btn_Path.Text = UnitField.FolderPath;
			this.cbx_IsPlaySound.Properties.Items[0] = UnitField.Yes;
			this.cbx_IsPlaySound.Properties.Items[1] = UnitField.No;
			this.Text = UnitField.SystemSettings;
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
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_systemName = new DevExpress.XtraEditors.TextEdit();
            this.lbl_SoftwareName = new System.Windows.Forms.Label();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.cbx_IsPlaySound = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbl_IsRunSound = new System.Windows.Forms.Label();
            this.radio_sytle2 = new System.Windows.Forms.RadioButton();
            this.radio_Style = new System.Windows.Forms.RadioButton();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lbl_HomePageStyle = new System.Windows.Forms.Label();
            this.btn_Path = new DevExpress.XtraEditors.SimpleButton();
            this.txt_PhotSavePath = new DevExpress.XtraEditors.TextEdit();
            this.cbx_IsCapturePhotos = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbl_IsSaveImage = new System.Windows.Forms.Label();
            this.lbl_PhotoSavePath = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_systemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_IsPlaySound.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PhotSavePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_IsCapturePhotos.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_systemName);
            this.panel1.Controls.Add(this.lbl_SoftwareName);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.cbx_IsPlaySound);
            this.panel1.Controls.Add(this.lbl_IsRunSound);
            this.panel1.Controls.Add(this.radio_sytle2);
            this.panel1.Controls.Add(this.radio_Style);
            this.panel1.Controls.Add(this.pictureEdit2);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.lbl_HomePageStyle);
            this.panel1.Controls.Add(this.btn_Path);
            this.panel1.Controls.Add(this.txt_PhotSavePath);
            this.panel1.Controls.Add(this.cbx_IsCapturePhotos);
            this.panel1.Controls.Add(this.lbl_IsSaveImage);
            this.panel1.Controls.Add(this.lbl_PhotoSavePath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 598);
            this.panel1.TabIndex = 3;
            // 
            // txt_systemName
            // 
            this.txt_systemName.EditValue = "人脸识别监控预警系统 服务电话：400 825 3771";
            this.txt_systemName.Location = new System.Drawing.Point(164, 165);
            this.txt_systemName.Name = "txt_systemName";
            this.txt_systemName.Size = new System.Drawing.Size(438, 20);
            this.txt_systemName.TabIndex = 28;
            // 
            // lbl_SoftwareName
            // 
            this.lbl_SoftwareName.AutoSize = true;
            this.lbl_SoftwareName.ForeColor = System.Drawing.Color.White;
            this.lbl_SoftwareName.Location = new System.Drawing.Point(22, 164);
            this.lbl_SoftwareName.Name = "lbl_SoftwareName";
            this.lbl_SoftwareName.Size = new System.Drawing.Size(67, 14);
            this.lbl_SoftwareName.TabIndex = 27;
            this.lbl_SoftwareName.Text = "软件名称：";
            // 
            // btn_save
            // 
            this.btn_save.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_save.Location = new System.Drawing.Point(319, 528);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(127, 48);
            this.btn_save.TabIndex = 26;
            this.btn_save.Text = "保 存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // cbx_IsPlaySound
            // 
            this.cbx_IsPlaySound.EditValue = "是";
            this.cbx_IsPlaySound.Location = new System.Drawing.Point(164, 233);
            this.cbx_IsPlaySound.Name = "cbx_IsPlaySound";
            // 
            // 
            // 
            this.cbx_IsPlaySound.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbx_IsPlaySound.Properties.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cbx_IsPlaySound.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbx_IsPlaySound.Size = new System.Drawing.Size(149, 20);
            this.cbx_IsPlaySound.TabIndex = 25;
            // 
            // lbl_IsRunSound
            // 
            this.lbl_IsRunSound.AutoSize = true;
            this.lbl_IsRunSound.ForeColor = System.Drawing.Color.White;
            this.lbl_IsRunSound.Location = new System.Drawing.Point(22, 232);
            this.lbl_IsRunSound.Name = "lbl_IsRunSound";
            this.lbl_IsRunSound.Size = new System.Drawing.Size(91, 14);
            this.lbl_IsRunSound.TabIndex = 24;
            this.lbl_IsRunSound.Text = "开启报警声音：";
            // 
            // radio_sytle2
            // 
            this.radio_sytle2.AutoSize = true;
            this.radio_sytle2.Location = new System.Drawing.Point(433, 303);
            this.radio_sytle2.Name = "radio_sytle2";
            this.radio_sytle2.Size = new System.Drawing.Size(56, 18);
            this.radio_sytle2.TabIndex = 23;
            this.radio_sytle2.Text = "样式2";
            this.radio_sytle2.UseVisualStyleBackColor = true;
            // 
            // radio_Style
            // 
            this.radio_Style.AutoSize = true;
            this.radio_Style.Checked = true;
            this.radio_Style.Location = new System.Drawing.Point(167, 305);
            this.radio_Style.Name = "radio_Style";
            this.radio_Style.Size = new System.Drawing.Size(56, 18);
            this.radio_Style.TabIndex = 22;
            this.radio_Style.TabStop = true;
            this.radio_Style.Text = "样式1";
            this.radio_Style.UseVisualStyleBackColor = true;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Location = new System.Drawing.Point(433, 331);
            this.pictureEdit2.Name = "pictureEdit2";
            // 
            // 
            // 
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pictureEdit2.Properties.ReadOnly = true;
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Properties.ShowMenu = false;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit2.Size = new System.Drawing.Size(234, 160);
            this.pictureEdit2.TabIndex = 21;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(167, 331);
            this.pictureEdit1.Name = "pictureEdit1";
            // 
            // 
            // 
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pictureEdit1.Properties.InitialImage = null;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(234, 160);
            this.pictureEdit1.TabIndex = 20;
            // 
            // lbl_HomePageStyle
            // 
            this.lbl_HomePageStyle.AutoSize = true;
            this.lbl_HomePageStyle.ForeColor = System.Drawing.Color.White;
            this.lbl_HomePageStyle.Location = new System.Drawing.Point(22, 303);
            this.lbl_HomePageStyle.Name = "lbl_HomePageStyle";
            this.lbl_HomePageStyle.Size = new System.Drawing.Size(91, 14);
            this.lbl_HomePageStyle.TabIndex = 18;
            this.lbl_HomePageStyle.Text = "首页显示样式：";
            // 
            // btn_Path
            // 
            this.btn_Path.Location = new System.Drawing.Point(608, 94);
            this.btn_Path.Name = "btn_Path";
            this.btn_Path.Size = new System.Drawing.Size(85, 26);
            this.btn_Path.TabIndex = 17;
            this.btn_Path.Text = "选择路径";
            this.btn_Path.Click += new System.EventHandler(this.btn_Path_Click);
            // 
            // txt_PhotSavePath
            // 
            this.txt_PhotSavePath.EditValue = "D:\\CapturePhotos";
            this.txt_PhotSavePath.Location = new System.Drawing.Point(164, 96);
            this.txt_PhotSavePath.Name = "txt_PhotSavePath";
            this.txt_PhotSavePath.Size = new System.Drawing.Size(438, 20);
            this.txt_PhotSavePath.TabIndex = 16;
            // 
            // cbx_IsCapturePhotos
            // 
            this.cbx_IsCapturePhotos.EditValue = "是";
            this.cbx_IsCapturePhotos.Location = new System.Drawing.Point(164, 32);
            this.cbx_IsCapturePhotos.Name = "cbx_IsCapturePhotos";
            // 
            // 
            // 
            this.cbx_IsCapturePhotos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbx_IsCapturePhotos.Properties.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cbx_IsCapturePhotos.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbx_IsCapturePhotos.Size = new System.Drawing.Size(149, 20);
            this.cbx_IsCapturePhotos.TabIndex = 15;
            // 
            // lbl_IsSaveImage
            // 
            this.lbl_IsSaveImage.AutoSize = true;
            this.lbl_IsSaveImage.ForeColor = System.Drawing.Color.White;
            this.lbl_IsSaveImage.Location = new System.Drawing.Point(22, 31);
            this.lbl_IsSaveImage.Name = "lbl_IsSaveImage";
            this.lbl_IsSaveImage.Size = new System.Drawing.Size(115, 14);
            this.lbl_IsSaveImage.TabIndex = 11;
            this.lbl_IsSaveImage.Text = "是否保存抓拍照片：";
            // 
            // lbl_PhotoSavePath
            // 
            this.lbl_PhotoSavePath.AutoSize = true;
            this.lbl_PhotoSavePath.ForeColor = System.Drawing.Color.White;
            this.lbl_PhotoSavePath.Location = new System.Drawing.Point(22, 97);
            this.lbl_PhotoSavePath.Name = "lbl_PhotoSavePath";
            this.lbl_PhotoSavePath.Size = new System.Drawing.Size(115, 14);
            this.lbl_PhotoSavePath.TabIndex = 8;
            this.lbl_PhotoSavePath.Text = "抓拍照片存放路径：";
            // 
            // SystemSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 639);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(816, 639);
            this.Name = "SystemSetting";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.SystemSetting_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_systemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_IsPlaySound.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PhotSavePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_IsCapturePhotos.Properties)).EndInit();
            this.ResumeLayout(false);

		}

        private void SystemSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
