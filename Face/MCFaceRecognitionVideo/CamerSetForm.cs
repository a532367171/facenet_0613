using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MCFaceRecognitionVideo.resources;
using MC_DAL;
using MC_DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace MCFaceRecognitionVideo
{
	public class CamerSetForm : XtraForm
	{
		private List<CamerInfo> _camerInfos = new List<CamerInfo>();
		private readonly CamerInfoService _camerInfoService = new CamerInfoService();
		private CamerInfo camerInfo = new CamerInfo();
		private bool isAdd;
		private IContainer components;
		private ImageList imageList1;
		private Panel panel2;
		private TreeView treeView1;
		private GroupBox groupBox1;
		private Label label12;
		private Label lbl_Recommended;
		private Label lbl_FacialMarker;
		private TextEdit txt_between2Eyes;
		private Label lbl_TwoEyesDistance;
		private SimpleButton button2;
		private SimpleButton btn_save;
		private TextEdit txt_threshold;
		private TextEdit txt_password;
		private TextEdit txt_username;
		private TextEdit txt_Port;
		private TextEdit txt_IP;
		private Label lbl_Resolution;
		private Label lbl_Threshold;
		private Label lbl_UserName;
		private Label lbl_Password;
		private Label lbl_IP;
		private Label lbl_Port;
		private Label lbl_CameraType;
		private Label lbl_Stat2;
		private Label lbl_State;
		private ComboBoxEdit cbx_camerType;
		private ComboBoxEdit cbx_PX;
		private ComboBoxEdit cbx_IsShowFaceRectangle;
		public CamerSetForm()
		{
			this.InitializeComponent();
			if (LanguageSet.Resource != null)
			{
				this.UpDataMainFormMenuLanguage(LanguageSet.Resource);
			}
			this.treeView1.ExpandAll();
			this.LoadTree();
		}
		private void LoadTree()
		{
			this._camerInfos = this._camerInfoService.GetList();
			for (int i = 0; i < this._camerInfos.Count; i++)
			{
				this.treeView1.Nodes[0].Nodes[i].Tag = this._camerInfos[i];
			}
		}
		private void LoadPage()
		{
			if (this.camerInfo.IsTure == 1L)
			{
				this.lbl_Stat2.Text = UnitField.Fortified;
				this.lbl_Stat2.ForeColor = Color.Lime;
				this.button2.Text = UnitField.Removal;
			}
			else
			{
				this.lbl_Stat2.Text = UnitField.Removal;
				this.lbl_Stat2.ForeColor = Color.Red;
				this.button2.Text = UnitField.Fortified;
			}
			if (this.camerInfo.CamerType.Equals(UnitField.USBCamera))
			{
				this.cbx_camerType.Text = UnitField.USBCamera;
				this.lbl_IP.Text = UnitField.USBSubscript;
			}
			else
			{
				if (this.camerInfo.CamerType.Equals(UnitField.Dahuatech))
				{
					this.cbx_camerType.Text = UnitField.Dahuatech;
					this.lbl_IP.Text = UnitField.CameraIP;
				}
				else
				{
					if (this.camerInfo.CamerType.Equals(UnitField.ZTE))
					{
						this.cbx_camerType.Text = UnitField.ZTE;
						this.lbl_IP.Text = UnitField.CameraIP;
					}
					else
					{
						if (this.camerInfo.CamerType.Equals(UnitField.Uniview))
						{
							this.cbx_camerType.Text = UnitField.Uniview;
							this.lbl_IP.Text = UnitField.CameraIP;
						}
						else
						{
							if (this.camerInfo.CamerType.Equals(UnitField.RTSPAddress))
							{
								this.cbx_camerType.Text = UnitField.RTSPAddress;
								this.lbl_IP.Text = UnitField.RTSPAddress;
							}
							else
							{
								this.cbx_camerType.Text = UnitField.Hikvision;
								this.lbl_IP.Text = UnitField.CameraIP;
							}
						}
					}
				}
			}
			this.txt_Port.Text = this.camerInfo.CamerPort;
			this.txt_IP.Text = this.camerInfo.CamerAddress;
			long camerHeight = this.camerInfo.CamerHeight;
			if (camerHeight != 720L)
			{
				if (camerHeight != 1080L)
				{
					this.cbx_PX.Text = "480";
				}
				else
				{
					this.cbx_PX.Text = "1080P";
				}
			}
			else
			{
				this.cbx_PX.Text = "720P";
			}
			this.txt_username.Text = this.camerInfo.CamerUser;
			this.txt_password.Text = this.camerInfo.CamerPassword;
			this.txt_threshold.Text = this.camerInfo.tmp1;
			this.txt_between2Eyes.Text = this.camerInfo.tmp2;
			this.cbx_IsShowFaceRectangle.Text = (this.camerInfo.tmp3.Equals(string.Empty) ? UnitField.OnShow : this.camerInfo.tmp3);
		}
		private void tsb_close_Click(object sender, EventArgs e)
		{
			if (this.isAdd)
			{
				base.DialogResult = DialogResult.OK;
			}
			base.Close();
		}
		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (!(e.Node.Text != UnitField.MonitoringChannel))
			{
				this.groupBox1.Enabled = false;
				return;
			}
			this.groupBox1.Enabled = true;
			if (e.Node.Tag != null)
			{
				this.camerInfo = (CamerInfo)e.Node.Tag;
				this.groupBox1.Text = e.Node.Text;
				this.LoadPage();
				return;
			}
			this.camerInfo = new CamerInfo();
		}
		private void btn_save_Click(object sender, EventArgs e)
		{
			if (!this.VerificationForm())
			{
				return;
			}
			this.camerInfo.CamerAddress = this.txt_IP.Text;
			string text = this.cbx_PX.Text;
			if (!(text == "720P"))
			{
				if (!(text == "1080P"))
				{
					this.camerInfo.CamerHeight = 480L;
					this.camerInfo.CamerWeight = 640L;
				}
				else
				{
					this.camerInfo.CamerHeight = 1080L;
					this.camerInfo.CamerWeight = 1920L;
				}
			}
			else
			{
				this.camerInfo.CamerHeight = 720L;
				this.camerInfo.CamerWeight = 1280L;
			}
			this.camerInfo.CamerType = this.cbx_camerType.Text;
			this.camerInfo.CamerPort = this.txt_Port.Text;
			this.camerInfo.CamerUser = this.txt_username.Text;
			this.camerInfo.CamerPassword = this.txt_password.Text;
			this.camerInfo.tmp1 = this.txt_threshold.Text;
			this.camerInfo.tmp2 = this.txt_between2Eyes.Text;
			this.camerInfo.tmp3 = this.cbx_IsShowFaceRectangle.Text;
			if (LanguageSet.Resource != null)
			{
				this.camerInfo.Channel = this.treeView1.SelectedNode.Text;
			}
			else
			{
				this.camerInfo.Channel = this.treeView1.SelectedNode.Text.Substring(3, 3);
			}
			long num;
			if (this.camerInfo.ID > 0L)
			{
				num = this._camerInfoService.Update(this.camerInfo);
			}
			else
			{
				num = this._camerInfoService.add(this.camerInfo);
			}
			if (num > 0L)
			{
				XtraMessageBox.Show(UnitField.SaveSuccess, UnitField.SystemMessage, MessageBoxButtons.OK);
				this.LoadTree();
				this.LoadPage();
				this.isAdd = true;
				return;
			}
			XtraMessageBox.Show(UnitField.SaveFiled, UnitField.SystemMessage, MessageBoxButtons.OK);
		}
		private bool VerificationForm()
		{
			if (this.cbx_camerType.Text.Equals(string.Empty))
			{
				XtraMessageBox.Show(UnitField.SelectedCameraType, UnitField.SystemMessage, MessageBoxButtons.OK);
				this.cbx_camerType.Focus();
				return false;
			}
			if (this.cbx_camerType.Text.Equals(UnitField.USBCamera))
			{
				if (this.txt_IP.Text.Length <= 0)
				{
					XtraMessageBox.Show(UnitField.USBSubscriptNull, UnitField.SystemMessage, MessageBoxButtons.OK);
					this.txt_IP.Focus();
					return false;
				}
				string pattern = "^-?\\d+\\.?\\d*$";
				if (!Regex.IsMatch(this.txt_IP.Text, pattern))
				{
					XtraMessageBox.Show(UnitField.USBSubscriptFiled, UnitField.SystemMessage, MessageBoxButtons.OK);
					this.txt_IP.Focus();
					return false;
				}
			}
			else
			{
				if (this.cbx_camerType.Text.Equals(UnitField.RTSPAddress))
				{
					if (this.txt_IP.Text.Length <= 0)
					{
						XtraMessageBox.Show(UnitField.RTSPAddressNull, UnitField.SystemMessage, MessageBoxButtons.OK);
						this.txt_IP.Focus();
						return false;
					}
				}
				else
				{
					if (this.txt_IP.Text.Length <= 0)
					{
						XtraMessageBox.Show(UnitField.CameraIPNull, UnitField.SystemMessage, MessageBoxButtons.OK);
						this.txt_IP.Focus();
						return false;
					}
					IPAddress iPAddress;
					if (!IPAddress.TryParse(this.txt_IP.Text, out iPAddress))
					{
						XtraMessageBox.Show(UnitField.CameraIPFiled, UnitField.SystemMessage, MessageBoxButtons.OK);
						this.txt_IP.Focus();
						return false;
					}
					if (this.txt_Port.Text.Length <= 0)
					{
						XtraMessageBox.Show(UnitField.PortNull, UnitField.SystemMessage, MessageBoxButtons.OK);
						this.txt_Port.Focus();
						return false;
					}
					string pattern2 = "^-?\\d+\\.?\\d*$";
					if (!Regex.IsMatch(this.txt_Port.Text, pattern2))
					{
						XtraMessageBox.Show(UnitField.PortFiled, UnitField.SystemMessage, MessageBoxButtons.OK);
						this.txt_Port.Focus();
						return false;
					}
					if (this.txt_username.Text.Equals(string.Empty))
					{
						XtraMessageBox.Show(UnitField.UserNameNull, UnitField.SystemMessage, MessageBoxButtons.OK);
						this.txt_username.Focus();
						return false;
					}
					if (this.txt_password.Text.Equals(string.Empty))
					{
						XtraMessageBox.Show(UnitField.PassWordNull, UnitField.SystemMessage, MessageBoxButtons.OK);
						this.txt_password.Focus();
						return false;
					}
				}
			}
			if (this.txt_threshold.Text.Equals(string.Empty))
			{
				XtraMessageBox.Show(UnitField.ThresholdNull, UnitField.SystemMessage, MessageBoxButtons.OK);
				this.txt_threshold.Focus();
				return false;
			}
			string pattern3 = "^-?\\d+\\.?\\d*$";
			if (!Regex.IsMatch(this.txt_threshold.Text, pattern3))
			{
				XtraMessageBox.Show(UnitField.ThresholdFiled, UnitField.SystemMessage, MessageBoxButtons.OK);
				this.txt_threshold.Focus();
				return false;
			}
			if (float.Parse(this.txt_threshold.Text) < 0f || float.Parse(this.txt_threshold.Text) >= 1f)
			{
				XtraMessageBox.Show(UnitField.ThresholdBetween, UnitField.SystemMessage, MessageBoxButtons.OK);
				this.txt_threshold.Focus();
				return false;
			}
			if (this.txt_between2Eyes.Text.Length > 0)
			{
				string pattern4 = "^-?\\d+\\.?\\d*$";
				if (!Regex.IsMatch(this.txt_between2Eyes.Text, pattern4))
				{
					XtraMessageBox.Show(UnitField.BetweenEyeFiled, UnitField.SystemMessage, DefaultBoolean.True);
					this.txt_between2Eyes.Focus();
					return false;
				}
			}
			return true;
		}
		private void button2_Click(object sender, EventArgs e)
		{
			if (this.button2.Text.Equals(UnitField.Fortified))
			{
				this.camerInfo.IsTure = 1L;
			}
			else
			{
				this.camerInfo.IsTure = 0L;
			}
			this.btn_save_Click(sender, e);
		}
		private void cbx_camerType_TextChanged(object sender, EventArgs e)
		{
			if (UnitField.USBCamera.Equals(this.cbx_camerType.Text))
			{
				this.lbl_IP.Text = UnitField.USBSubscript;
				return;
			}
			if (this.cbx_camerType.Text.Equals(UnitField.RTSPAddress))
			{
				this.lbl_IP.Text = UnitField.RTSPAddress;
				return;
			}
			this.lbl_IP.Text = UnitField.CameraIP;
		}
		private void CamerSetForm_Load(object sender, EventArgs e)
		{
			if (this._camerInfos.Count == 0)
			{
				for (int i = 1; i < 5; i++)
				{
					this.camerInfo.CamerType = UnitField.USBCamera;
					this.camerInfo.CamerPort = "554";
					this.camerInfo.CamerUser = "admin";
					this.camerInfo.CamerPassword = "admin123";
					this.camerInfo.tmp1 = "0.8";
					this.camerInfo.tmp2 = "30";
					this.camerInfo.tmp3 = "OnShow";
					this.camerInfo.CamerAddress = "192.168.1.64";
					this.camerInfo.CamerWeight = 1280L;
					this.camerInfo.CamerHeight = 720L;
					this.camerInfo.IsTure = 0L;
					this.camerInfo.Channel = string.Format("Channel{0}", i);
					this._camerInfoService.add(this.camerInfo);
				}
				this.LoadTree();
			}
		}
		private void UpDataMainFormMenuLanguage(ResourceManager rm)
		{
			this.cbx_camerType.Properties.Items[0] = UnitField.USBCamera;
			this.cbx_camerType.Properties.Items[1] = UnitField.RTSPAddress;
			this.cbx_camerType.Properties.Items[2] = UnitField.Dahuatech;
			this.cbx_camerType.Properties.Items[3] = UnitField.Hikvision;
			this.cbx_camerType.Properties.Items[4] = UnitField.ZTE;
			this.cbx_camerType.Properties.Items[5] = UnitField.Uniview;
			this.cbx_IsShowFaceRectangle.Properties.Items[0] = UnitField.OnShow;
			this.cbx_IsShowFaceRectangle.Properties.Items[1] = UnitField.DonotShow;
			this.Text = UnitField.ProtectionSettings;
			this.lbl_State.Text = UnitField.State;
			this.lbl_CameraType.Text = UnitField.CameraType;
			this.lbl_Resolution.Text = UnitField.Resolution;
			this.lbl_Port.Text = UnitField.Port;
			this.lbl_UserName.Text = UnitField.UserName;
			this.lbl_Password.Text = UnitField.Password;
			this.lbl_Threshold.Text = UnitField.Threshold;
			this.lbl_TwoEyesDistance.Text = UnitField.TwoEyesDistance;
			this.btn_save.Text = UnitField.Save;
			this.lbl_FacialMarker.Text = UnitField.FacialMarker;
			this.groupBox1.Text = (this.treeView1.Nodes[0].Text = UnitField.MonitoringChannel);
			this.treeView1.Nodes[0].Nodes[0].Text = UnitField.Channel1;
			this.treeView1.Nodes[0].Nodes[1].Text = UnitField.Channel2;
			this.treeView1.Nodes[0].Nodes[2].Text = UnitField.Channel3;
			this.treeView1.Nodes[0].Nodes[3].Text = UnitField.Channel4;
			this.lbl_Recommended.Text = UnitField.Recommended;
			this.lbl_Stat2.Text = UnitField.Fortified;
			this.lbl_IP.Text = UnitField.CameraIP;
			this.button2.Text = UnitField.Fortified;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamerSetForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("左上（通道1）");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("右上（通道2）");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("左下（通道3）");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("右下（通道4）");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("监控通道", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbx_IsShowFaceRectangle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbx_PX = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbx_camerType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_Recommended = new System.Windows.Forms.Label();
            this.lbl_FacialMarker = new System.Windows.Forms.Label();
            this.txt_between2Eyes = new DevExpress.XtraEditors.TextEdit();
            this.lbl_TwoEyesDistance = new System.Windows.Forms.Label();
            this.button2 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.txt_threshold = new DevExpress.XtraEditors.TextEdit();
            this.txt_password = new DevExpress.XtraEditors.TextEdit();
            this.txt_username = new DevExpress.XtraEditors.TextEdit();
            this.txt_Port = new DevExpress.XtraEditors.TextEdit();
            this.txt_IP = new DevExpress.XtraEditors.TextEdit();
            this.lbl_Resolution = new System.Windows.Forms.Label();
            this.lbl_Threshold = new System.Windows.Forms.Label();
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.lbl_IP = new System.Windows.Forms.Label();
            this.lbl_Port = new System.Windows.Forms.Label();
            this.lbl_CameraType = new System.Windows.Forms.Label();
            this.lbl_Stat2 = new System.Windows.Forms.Label();
            this.lbl_State = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_IsShowFaceRectangle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_PX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_camerType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_between2Eyes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_threshold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Port.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_IP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "video.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 598);
            this.panel2.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 35;
            this.treeView1.Location = new System.Drawing.Point(25, 35);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "video1";
            treeNode1.Text = "左上（通道1）";
            treeNode2.Name = "节点4";
            treeNode2.Text = "右上（通道2）";
            treeNode3.Name = "video3";
            treeNode3.Text = "左下（通道3）";
            treeNode4.Name = "video4";
            treeNode4.Text = "右下（通道4）";
            treeNode5.ImageKey = "(默认值)";
            treeNode5.Name = "head";
            treeNode5.NodeFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode5.SelectedImageIndex = -2;
            treeNode5.Text = "监控通道";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(267, 546);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbx_IsShowFaceRectangle);
            this.groupBox1.Controls.Add(this.cbx_PX);
            this.groupBox1.Controls.Add(this.cbx_camerType);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lbl_Recommended);
            this.groupBox1.Controls.Add(this.lbl_FacialMarker);
            this.groupBox1.Controls.Add(this.txt_between2Eyes);
            this.groupBox1.Controls.Add(this.lbl_TwoEyesDistance);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.txt_threshold);
            this.groupBox1.Controls.Add(this.txt_password);
            this.groupBox1.Controls.Add(this.txt_username);
            this.groupBox1.Controls.Add(this.txt_Port);
            this.groupBox1.Controls.Add(this.txt_IP);
            this.groupBox1.Controls.Add(this.lbl_Resolution);
            this.groupBox1.Controls.Add(this.lbl_Threshold);
            this.groupBox1.Controls.Add(this.lbl_UserName);
            this.groupBox1.Controls.Add(this.lbl_Password);
            this.groupBox1.Controls.Add(this.lbl_IP);
            this.groupBox1.Controls.Add(this.lbl_Port);
            this.groupBox1.Controls.Add(this.lbl_CameraType);
            this.groupBox1.Controls.Add(this.lbl_Stat2);
            this.groupBox1.Controls.Add(this.lbl_State);
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(320, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 545);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "监控通道";
            // 
            // cbx_IsShowFaceRectangle
            // 
            this.cbx_IsShowFaceRectangle.Location = new System.Drawing.Point(139, 416);
            this.cbx_IsShowFaceRectangle.Name = "cbx_IsShowFaceRectangle";
            // 
            // 
            // 
            this.cbx_IsShowFaceRectangle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbx_IsShowFaceRectangle.Properties.Items.AddRange(new object[] {
            "显示",
            "不显示"});
            this.cbx_IsShowFaceRectangle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbx_IsShowFaceRectangle.Size = new System.Drawing.Size(270, 20);
            this.cbx_IsShowFaceRectangle.TabIndex = 15;
            // 
            // cbx_PX
            // 
            this.cbx_PX.Location = new System.Drawing.Point(139, 160);
            this.cbx_PX.Name = "cbx_PX";
            // 
            // 
            // 
            this.cbx_PX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbx_PX.Properties.Items.AddRange(new object[] {
            "480",
            "720P",
            "960",
            "1080P"});
            this.cbx_PX.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbx_PX.Size = new System.Drawing.Size(270, 20);
            this.cbx_PX.TabIndex = 14;
            // 
            // cbx_camerType
            // 
            this.cbx_camerType.Location = new System.Drawing.Point(139, 72);
            this.cbx_camerType.Name = "cbx_camerType";
            // 
            // 
            // 
            this.cbx_camerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbx_camerType.Properties.Items.AddRange(new object[] {
            "USB摄像机",
            "RTSP地址",
            "海康IP摄像机",
            "大华IP摄像机",
            "中兴IP摄像机",
            "宇视IP摄像机"});
            this.cbx_camerType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbx_camerType.Size = new System.Drawing.Size(270, 20);
            this.cbx_camerType.TabIndex = 13;
            this.cbx_camerType.TextChanged += new System.EventHandler(this.cbx_camerType_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(270, 376);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 20);
            this.label12.TabIndex = 11;
            this.label12.Text = "px";
            // 
            // lbl_Recommended
            // 
            this.lbl_Recommended.AutoSize = true;
            this.lbl_Recommended.ForeColor = System.Drawing.Color.OrangeRed;
            this.lbl_Recommended.Location = new System.Drawing.Point(294, 377);
            this.lbl_Recommended.Name = "lbl_Recommended";
            this.lbl_Recommended.Size = new System.Drawing.Size(128, 19);
            this.lbl_Recommended.TabIndex = 10;
            this.lbl_Recommended.Text = "（建议设置60px）";
            // 
            // lbl_FacialMarker
            // 
            this.lbl_FacialMarker.AutoSize = true;
            this.lbl_FacialMarker.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_FacialMarker.Location = new System.Drawing.Point(23, 416);
            this.lbl_FacialMarker.Name = "lbl_FacialMarker";
            this.lbl_FacialMarker.Size = new System.Drawing.Size(65, 20);
            this.lbl_FacialMarker.TabIndex = 8;
            this.lbl_FacialMarker.Text = "人脸框：";
            // 
            // txt_between2Eyes
            // 
            this.txt_between2Eyes.EditValue = "30";
            this.txt_between2Eyes.Location = new System.Drawing.Point(139, 373);
            this.txt_between2Eyes.Name = "txt_between2Eyes";
            // 
            // 
            // 
            this.txt_between2Eyes.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_between2Eyes.Properties.Appearance.Options.UseFont = true;
            this.txt_between2Eyes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txt_between2Eyes.Size = new System.Drawing.Size(128, 26);
            this.txt_between2Eyes.TabIndex = 7;
            // 
            // lbl_TwoEyesDistance
            // 
            this.lbl_TwoEyesDistance.AutoSize = true;
            this.lbl_TwoEyesDistance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_TwoEyesDistance.Location = new System.Drawing.Point(23, 376);
            this.lbl_TwoEyesDistance.Name = "lbl_TwoEyesDistance";
            this.lbl_TwoEyesDistance.Size = new System.Drawing.Size(107, 20);
            this.lbl_TwoEyesDistance.TabIndex = 6;
            this.lbl_TwoEyesDistance.Text = "两眼之间距离：";
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.button2.Location = new System.Drawing.Point(340, 489);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 41);
            this.button2.TabIndex = 5;
            this.button2.Text = "撤 防";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_save
            // 
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_save.Location = new System.Drawing.Point(230, 489);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(95, 41);
            this.btn_save.TabIndex = 5;
            this.btn_save.Text = "保 存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_threshold
            // 
            this.txt_threshold.EditValue = "0.8";
            this.txt_threshold.Location = new System.Drawing.Point(139, 331);
            this.txt_threshold.Name = "txt_threshold";
            // 
            // 
            // 
            this.txt_threshold.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_threshold.Properties.Appearance.Options.UseFont = true;
            this.txt_threshold.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txt_threshold.Size = new System.Drawing.Size(270, 26);
            this.txt_threshold.TabIndex = 2;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(139, 285);
            this.txt_password.Name = "txt_password";
            // 
            // 
            // 
            this.txt_password.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_password.Properties.Appearance.Options.UseFont = true;
            this.txt_password.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txt_password.Size = new System.Drawing.Size(270, 26);
            this.txt_password.TabIndex = 2;
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(139, 242);
            this.txt_username.Name = "txt_username";
            // 
            // 
            // 
            this.txt_username.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_username.Properties.Appearance.Options.UseFont = true;
            this.txt_username.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txt_username.Size = new System.Drawing.Size(270, 26);
            this.txt_username.TabIndex = 2;
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(139, 199);
            this.txt_Port.Name = "txt_Port";
            // 
            // 
            // 
            this.txt_Port.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Port.Properties.Appearance.Options.UseFont = true;
            this.txt_Port.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txt_Port.Size = new System.Drawing.Size(270, 26);
            this.txt_Port.TabIndex = 2;
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(139, 113);
            this.txt_IP.Name = "txt_IP";
            // 
            // 
            // 
            this.txt_IP.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_IP.Properties.Appearance.Options.UseFont = true;
            this.txt_IP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txt_IP.Size = new System.Drawing.Size(270, 26);
            this.txt_IP.TabIndex = 2;
            // 
            // lbl_Resolution
            // 
            this.lbl_Resolution.AutoSize = true;
            this.lbl_Resolution.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Resolution.Location = new System.Drawing.Point(23, 159);
            this.lbl_Resolution.Name = "lbl_Resolution";
            this.lbl_Resolution.Size = new System.Drawing.Size(65, 20);
            this.lbl_Resolution.TabIndex = 1;
            this.lbl_Resolution.Text = "分辨率：";
            // 
            // lbl_Threshold
            // 
            this.lbl_Threshold.AutoSize = true;
            this.lbl_Threshold.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Threshold.Location = new System.Drawing.Point(23, 334);
            this.lbl_Threshold.Name = "lbl_Threshold";
            this.lbl_Threshold.Size = new System.Drawing.Size(79, 20);
            this.lbl_Threshold.TabIndex = 1;
            this.lbl_Threshold.Text = "对比阈值：";
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_UserName.Location = new System.Drawing.Point(23, 245);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(93, 20);
            this.lbl_UserName.TabIndex = 1;
            this.lbl_UserName.Text = "登陆用户名：";
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Password.Location = new System.Drawing.Point(23, 288);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(79, 20);
            this.lbl_Password.TabIndex = 1;
            this.lbl_Password.Text = "登陆密码：";
            // 
            // lbl_IP
            // 
            this.lbl_IP.AutoSize = true;
            this.lbl_IP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_IP.Location = new System.Drawing.Point(23, 116);
            this.lbl_IP.Name = "lbl_IP";
            this.lbl_IP.Size = new System.Drawing.Size(78, 20);
            this.lbl_IP.TabIndex = 1;
            this.lbl_IP.Text = "摄像机IP：";
            // 
            // lbl_Port
            // 
            this.lbl_Port.AutoSize = true;
            this.lbl_Port.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Port.Location = new System.Drawing.Point(23, 202);
            this.lbl_Port.Name = "lbl_Port";
            this.lbl_Port.Size = new System.Drawing.Size(107, 20);
            this.lbl_Port.TabIndex = 1;
            this.lbl_Port.Text = "摄像机端口号：";
            // 
            // lbl_CameraType
            // 
            this.lbl_CameraType.AutoSize = true;
            this.lbl_CameraType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_CameraType.Location = new System.Drawing.Point(23, 73);
            this.lbl_CameraType.Name = "lbl_CameraType";
            this.lbl_CameraType.Size = new System.Drawing.Size(93, 20);
            this.lbl_CameraType.TabIndex = 1;
            this.lbl_CameraType.Text = "摄像机类型：";
            // 
            // lbl_Stat2
            // 
            this.lbl_Stat2.AutoSize = true;
            this.lbl_Stat2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Stat2.ForeColor = System.Drawing.Color.Lime;
            this.lbl_Stat2.Location = new System.Drawing.Point(135, 30);
            this.lbl_Stat2.Name = "lbl_Stat2";
            this.lbl_Stat2.Size = new System.Drawing.Size(37, 19);
            this.lbl_Stat2.TabIndex = 0;
            this.lbl_Stat2.Text = "设防";
            // 
            // lbl_State
            // 
            this.lbl_State.AutoSize = true;
            this.lbl_State.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_State.Location = new System.Drawing.Point(23, 30);
            this.lbl_State.Name = "lbl_State";
            this.lbl_State.Size = new System.Drawing.Size(79, 20);
            this.lbl_State.TabIndex = 0;
            this.lbl_State.Text = "当前状态：";
            // 
            // CamerSetForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 639);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(816, 639);
            this.Name = "CamerSetForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "布防设置";
            this.Load += new System.EventHandler(this.CamerSetForm_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_IsShowFaceRectangle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_PX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbx_camerType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_between2Eyes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_threshold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Port.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_IP.Properties)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
