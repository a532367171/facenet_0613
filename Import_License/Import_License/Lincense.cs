using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Import_License
{
	public class Lincense : Form
	{
		private readonly OpenFileDialog _openFileDialog1 = new OpenFileDialog
		{
			Title = "选择授权文件",
			Filter = "Liceense(*.license)|*.license",
			FileName = string.Empty,
			FilterIndex = 1,
			RestoreDirectory = true,
			DefaultExt = "license"
		};

		private IContainer components;

		private Button button1;

		private TabControl tbg1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private Label label1;

		private Button btn_Online;

		private Label label2;

		private Button btn_createLicense;

		public Lincense()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._openFileDialog1.ShowDialog() == DialogResult.OK && this._openFileDialog1.FileName.Length > 0)
				{
					File.Copy(this._openFileDialog1.FileName, Directory.GetCurrentDirectory() + "\\" + this._openFileDialog1.SafeFileName, true);
					if (File.Exists(Directory.GetCurrentDirectory() + "\\" + this._openFileDialog1.SafeFileName))
					{
						MessageBox.Show("安装成功！", "系统提示", MessageBoxButtons.OK);
					}
					else
					{
						MessageBox.Show("安装失败！", "系统提示", MessageBoxButtons.OK);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("安装失败！{0}", ex.Message), "系统提示", MessageBoxButtons.OK);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void btn_Online_Click(object sender, EventArgs e)
		{
			string s = "config\naccesstoremote=1\nbroadcastsearch=1\naggressive=1\nserverlist\n123.56.91.186\n/serverlist\n/config";
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			string address = "http://localhost:1947/_int_/action.html";
			byte[] bytes2 = new WebClient
			{
				Headers = 
				{
					{
						"Content-Type",
						"application/x-www-form-urlencoded"
					}
				}
			}.UploadData(address, "POST", bytes);
			if (Encoding.UTF8.GetString(bytes2).Contains("OK"))
			{
				MessageBox.Show("在线授权认证请求已发送，请等待2分钟后在进行软件登录使用。");
				return;
			}
			MessageBox.Show("在线授权认证请求失败");
		}

		private void btn_createLicense_Click(object sender, EventArgs e)
		{
			new CreateCodeForm
			{
				ShowIcon = false
			}.ShowDialog();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Lincense));
			this.button1 = new Button();
			this.tbg1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.btn_createLicense = new Button();
			this.label2 = new Label();
			this.tabPage2 = new TabPage();
			this.label1 = new Label();
			this.btn_Online = new Button();
			this.tbg1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			base.SuspendLayout();
			this.button1.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
			this.button1.Location = new Point(174, 197);
			this.button1.Margin = new Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new Size(136, 45);
			this.button1.TabIndex = 0;
			this.button1.Text = "安装 License";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.tbg1.Controls.Add(this.tabPage1);
			this.tbg1.Controls.Add(this.tabPage2);
			this.tbg1.Location = new Point(12, 12);
			this.tbg1.Name = "tbg1";
			this.tbg1.SelectedIndex = 0;
			this.tbg1.Size = new Size(340, 299);
			this.tbg1.TabIndex = 1;
			this.tabPage1.Controls.Add(this.btn_createLicense);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Location = new Point(4, 26);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(332, 269);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "离线授权";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.btn_createLicense.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
			this.btn_createLicense.Location = new Point(22, 197);
			this.btn_createLicense.Margin = new Padding(4);
			this.btn_createLicense.Name = "btn_createLicense";
			this.btn_createLicense.Size = new Size(136, 45);
			this.btn_createLicense.TabIndex = 3;
			this.btn_createLicense.Text = "机器码生成工具";
			this.btn_createLicense.UseVisualStyleBackColor = true;
			this.btn_createLicense.Click += new EventHandler(this.btn_createLicense_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(4, 27);
			this.label2.Name = "label2";
			this.label2.Size = new Size(315, 119);
			this.label2.TabIndex = 2;
			this.label2.Text = "说明：\r\n\r\n（1）、点击机器码生成工具按钮，进行本机机器码生成。\r\n           复制机器码发送给软件供应商。\r\n\r\n（2）、软件供应商会发送一个.license结尾的授权文件\r\n         ， 点击安装License，选择授权文件进行安装。";
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.btn_Online);
			this.tabPage2.Location = new Point(4, 26);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(332, 269);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "在线授权";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 27);
			this.label1.Name = "label1";
			this.label1.Size = new Size(303, 102);
			this.label1.TabIndex = 1;
			this.label1.Text = "说明：\r\n\r\n（1）、在进行在线授权认证过程中必须连接互联网，\r\n           在软件试用运行期间不能长时间断开联网。\r\n\r\n（2）、点击在线授权认证按钮开始进行在线授权认证。";
			this.btn_Online.Location = new Point(95, 193);
			this.btn_Online.Name = "btn_Online";
			this.btn_Online.Size = new Size(122, 46);
			this.btn_Online.TabIndex = 0;
			this.btn_Online.Text = "在线授权认证";
			this.btn_Online.UseVisualStyleBackColor = true;
			this.btn_Online.Click += new EventHandler(this.btn_Online_Click);
			base.AutoScaleDimensions = new SizeF(8f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(364, 323);
			base.Controls.Add(this.tbg1);
			this.Font = new Font("微软雅黑", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
			base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.Margin = new Padding(4);
			this.MaximumSize = new Size(380, 359);
			this.MinimumSize = new Size(380, 170);
			base.Name = "Lincense";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "软件授权工具";
			base.Load += new EventHandler(this.Form1_Load);
			this.tbg1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
