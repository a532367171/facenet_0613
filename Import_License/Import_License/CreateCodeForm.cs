using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Import_License
{
	public class CreateCodeForm : Form
	{
		private WinAPI winApi;

		public Process process;

		public IntPtr appWin;

		public string exeName = "";

		private IContainer components;

		private Panel panel1;

		private Label label1;

		public CreateCodeForm()
		{
			this.InitializeComponent();
			this.winApi = new WinAPI();
			this.init();
		}

		public void init()
		{
			this.exeName = "GenCode.exe";
			try
			{
				this.process = Process.Start(this.exeName);
				this.process.Exited += new EventHandler(this.ProcessOnExited);
				this.process.WaitForInputIdle();
				this.appWin = this.process.MainWindowHandle;
				WinAPI.SetParent(this.appWin, this.panel1.Handle);
				WinAPI.SetWindowLong(this.appWin, -16, 268435456L);
				WinAPI.MoveWindow(this.appWin, 0, 0, this.panel1.Width, this.panel1.Height, true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error");
			}
		}

		private void ProcessOnExited(object sender, EventArgs eventArgs)
		{
			base.Close();
		}

		private void Form2_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				this.process.Kill();
			}
			catch
			{
			}
		}

		private void Form2_Resize(object sender, EventArgs e)
		{
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
			this.panel1 = new Panel();
			this.label1 = new Label();
			base.SuspendLayout();
			this.panel1.Location = new Point(10, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(370, 104);
			this.panel1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 139);
			this.label1.Name = "label1";
			this.label1.Size = new Size(197, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "说明：点击Generate按钮生成机器码";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(393, 162);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.panel1);
			base.MaximizeBox = false;
			this.MaximumSize = new Size(409, 200);
			this.MinimumSize = new Size(409, 200);
			base.Name = "CreateCodeForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "生成机器码";
			base.FormClosed += new FormClosedEventHandler(this.Form2_FormClosed);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
