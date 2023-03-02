using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintAnalysisDebtsDuty : Form
	{
		private GroupBox groupBox1;

		private C1DateEdit dtPeriod;

		private Label label7;

		private Label label3;

		private TextBox txtSumma;

		private Button cmdClose;

		private Button cmdOK;

		private CheckBox chkOnlyOn;

		private System.ComponentModel.Container components = null;

		public frmPrintAnalysisDebtsDuty()
		{
			this.InitializeComponent();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					int num = 0;
					if (this.chkOnlyOn.Checked)
					{
						num = 1;
					}
					int[] numArray = new int[] { 3, 1, 2 };
					string[] str = new string[] { "@dateB", "@amount", "@OnlyOn" };
					string[] strArrays = str;
					str = new string[] { this.dtPeriod.Value.ToString(), this.txtSumma.Text.ToString(), num.ToString() };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repAnalysisDebtsDuty.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "Анализ дебиторской задолженности",
						MdiParent = base.MdiParent
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
				base.Close();
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

		private void frmPrintAnalysisDebtsDuty_Load(object sender, EventArgs e)
		{
			this.dtPeriod.Value = DateTime.Today.Date;
		}

		private void InitializeComponent()
		{
			this.groupBox1 = new GroupBox();
			this.dtPeriod = new C1DateEdit();
			this.label7 = new Label();
			this.label3 = new Label();
			this.txtSumma = new TextBox();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.chkOnlyOn = new CheckBox();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.chkOnlyOn);
			this.groupBox1.Controls.Add(this.dtPeriod);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtSumma);
			this.groupBox1.Location = new Point(4, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(212, 92);
			this.groupBox1.TabIndex = 62;
			this.groupBox1.TabStop = false;
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMMyyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(72, 20);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(124, 18);
			this.dtPeriod.TabIndex = 65;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(8, 20);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 16);
			this.label7.TabIndex = 66;
			this.label7.Text = "Период";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 63;
			this.label3.Text = "Сумма долга:";
			this.txtSumma.BorderStyle = BorderStyle.FixedSingle;
			this.txtSumma.Location = new Point(104, 40);
			this.txtSumma.Name = "txtSumma";
			this.txtSumma.Size = new System.Drawing.Size(92, 20);
			this.txtSumma.TabIndex = 5;
			this.txtSumma.Text = "-2000";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(124, 96);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 64;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(24, 96);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 63;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.chkOnlyOn.Location = new Point(8, 64);
			this.chkOnlyOn.Name = "chkOnlyOn";
			this.chkOnlyOn.Size = new System.Drawing.Size(196, 24);
			this.chkOnlyOn.TabIndex = 67;
			this.chkOnlyOn.Text = "только подключенные объекты";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(218, 124);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintAnalysisDebtsDuty";
			this.Text = "Анализ дебиторской задолженности";
			base.Load += new EventHandler(this.frmPrintAnalysisDebtsDuty_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}
	}
}