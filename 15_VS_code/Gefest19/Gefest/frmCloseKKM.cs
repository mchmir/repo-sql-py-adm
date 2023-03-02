using FprnM1C;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gefest
{
	public class frmCloseKKM : Form
	{
		private Button cmdClose;

		private Button cmdNext;

		private CheckBox chkZRep;

		private CheckBox chkXRep;

		private IFprnM45 ECR;

		private System.ComponentModel.Container components = null;

		public frmCloseKKM()
		{
			this.InitializeComponent();
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdNext_Click(object sender, EventArgs e)
		{
			if (this.chkZRep.Checked)
			{
				this.ECR.DeviceEnabled = true;
				if (this.ECR.ResultCode != 0)
				{
					return;
				}
				if (this.ECR.GetStatus() != 0)
				{
					return;
				}
				this.ECR.Password = "30";
				this.ECR.Mode = 3;
				if (this.ECR.SetMode() != 0)
				{
					return;
				}
				this.ECR.ReportType = 1;
				if (this.ECR.Report() != 0)
				{
					return;
				}
			}
			if (this.chkXRep.Checked)
			{
				this.ECR.DeviceEnabled = true;
				if (this.ECR.ResultCode != 0)
				{
					return;
				}
				if (this.ECR.GetStatus() != 0)
				{
					return;
				}
				this.ECR.Password = "30";
				this.ECR.Mode = 2;
				if (this.ECR.SetMode() != 0)
				{
					return;
				}
				this.ECR.ReportType = 2;
				if (this.ECR.Report() != 0)
				{
					return;
				}
			}
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmCloseKKM_Load(object sender, EventArgs e)
		{
			this.ECR = new FprnM45Class();
		}

		private void InitializeComponent()
		{
			this.chkZRep = new CheckBox();
			this.chkXRep = new CheckBox();
			this.cmdClose = new Button();
			this.cmdNext = new Button();
			base.SuspendLayout();
			this.chkZRep.Location = new Point(4, 8);
			this.chkZRep.Name = "chkZRep";
			this.chkZRep.Size = new System.Drawing.Size(204, 24);
			this.chkZRep.TabIndex = 0;
			this.chkZRep.Text = "Z-отчет";
			this.chkXRep.Location = new Point(4, 44);
			this.chkXRep.Name = "chkXRep";
			this.chkXRep.Size = new System.Drawing.Size(204, 24);
			this.chkXRep.TabIndex = 0;
			this.chkXRep.Text = "X - отчет";
			this.chkXRep.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(116, 76);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 8;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdNext.FlatStyle = FlatStyle.Flat;
			this.cmdNext.Location = new Point(12, 76);
			this.cmdNext.Name = "cmdNext";
			this.cmdNext.Size = new System.Drawing.Size(92, 24);
			this.cmdNext.TabIndex = 7;
			this.cmdNext.Text = "Далее >";
			this.cmdNext.Click += new EventHandler(this.cmdNext_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(218, 106);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdNext);
			base.Controls.Add(this.chkZRep);
			base.Controls.Add(this.chkXRep);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmCloseKKM";
			this.Text = "Отчеты по смене";
			base.Load += new EventHandler(this.frmCloseKKM_Load);
			base.ResumeLayout(false);
		}
	}
}