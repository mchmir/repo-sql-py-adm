using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintPenyPoContractu : Form
	{
		private Button cmdClose;

		private C1DateEdit dtPeriod;

		private Button cmdOK;

		private Label label1;

		private C1TextBox txtAccount;

		private Label label12;

		public Contract _contract;

		private System.ComponentModel.Container components = null;

		public frmPrintPenyPoContractu()
		{
			this.InitializeComponent();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Periods period = new Periods();
			period.Load((DateTime)this.dtPeriod.Value);
			if (this.txtAccount.Text.Length > 0)
			{
				this.CreateContracts();
			}
			long d = (long)0;
			if (this._contract != null)
			{
				d = this._contract.get_ID();
			}
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2 };
					string[] str = new string[] { "@idperiod", "@idcontract" };
					string[] strArrays = str;
					str = new string[2];
					long num = period[0].get_ID();
					str[0] = num.ToString();
					str[1] = d.ToString();
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repPenyPoContractu.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "Отчёт по пени по контрактам",
						MdiParent = Depot._main
					};
					frmReport.Show();
					frmReport = null;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			base.Close();
		}

		private void CreateContracts()
		{
			if (this.txtAccount.Text.Length > 0)
			{
				Contracts contract = new Contracts();
				if (contract.Load(this.txtAccount.Text.Trim()) == 0)
				{
					this._contract = contract[0];
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPrintPenyPoContractu_Load(object sender, EventArgs e)
		{
			this.dtPeriod.Value = DateTime.Today;
		}

		private void InitializeComponent()
		{
			this.cmdClose = new Button();
			this.dtPeriod = new C1DateEdit();
			this.cmdOK = new Button();
			this.label1 = new Label();
			this.txtAccount = new C1TextBox();
			this.label12 = new Label();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			((ISupportInitialize)this.txtAccount).BeginInit();
			base.SuspendLayout();
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(112, 64);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 37;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMM yyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(56, 8);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(144, 18);
			this.dtPeriod.TabIndex = 35;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(8, 64);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 36;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 38;
			this.label1.Text = "Период:";
			this.txtAccount.BorderStyle = 1;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(56, 32);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(144, 21);
			this.txtAccount.TabIndex = 40;
			this.txtAccount.Tag = null;
			this.txtAccount.KeyPress += new KeyPressEventHandler(this.txtAccount_KeyPress);
			this.txtAccount.Click += new EventHandler(this.txtAccount_Click);
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label12.ForeColor = SystemColors.ControlText;
			this.label12.Location = new Point(8, 32);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 16);
			this.label12.TabIndex = 39;
			this.label12.Text = "Л/с";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(210, 95);
			base.Controls.Add(this.txtAccount);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.dtPeriod);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintPenyPoContractu";
			this.Text = "Отчёт по пени по контрактам";
			base.Load += new EventHandler(this.frmPrintPenyPoContractu_Load);
			((ISupportInitialize)this.dtPeriod).EndInit();
			((ISupportInitialize)this.txtAccount).EndInit();
			base.ResumeLayout(false);
		}

		private void txtAccount_Click(object sender, EventArgs e)
		{
			this.CreateContracts();
		}

		private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtAccount_Click(null, null);
			}
		}
	}
}