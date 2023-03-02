using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintReestrPenyGosPO : Form
	{
		private GroupBox groupBox1;

		private C1DateEdit dtPeriod;

		private Label label1;

		private Button cmdClose;

		private Button cmdOK;

		private CheckBox checkBox1;

		private System.ComponentModel.Container components = null;

		public frmPrintReestrPenyGosPO()
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
			if (period.get_Count() <= 0)
			{
				MessageBox.Show("Не верный выбор периода!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			this.Rep(period[0].get_ID());
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

		private void frmPrintReestrPenyGosPO_Load(object sender, EventArgs e)
		{
			this.dtPeriod.Value = DateTime.Today.Date;
			this.dtPeriod.Focus();
		}

		private void InitializeComponent()
		{
			this.groupBox1 = new GroupBox();
			this.dtPeriod = new C1DateEdit();
			this.label1 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.checkBox1 = new CheckBox();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.dtPeriod);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(240, 40);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMMyyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(64, 16);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(168, 18);
			this.dtPeriod.TabIndex = 1;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Период";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(152, 72);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 3;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(48, 72);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.checkBox1.Location = new Point(8, 48);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(136, 16);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.Text = " без.нал";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(250, 98);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintReestrPenyGosPO";
			this.Text = "Реестр платежей пени и гос.пош.";
			base.Load += new EventHandler(this.frmPrintReestrPenyGosPO_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDPeriod)
		{
			string str;
			int[] numArray;
			string[] strArrays;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (!this.checkBox1.Checked)
					{
						numArray = new int[] { 2 };
						int[] numArray1 = numArray;
						strArrays = new string[] { "@idperiod" };
						string[] strArrays1 = strArrays;
						strArrays = new string[] { IDPeriod.ToString() };
						string[] strArrays2 = strArrays;
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repReestrOplPoPeniOfKass.rpt");
						Form frmReport = new frmReports(str, strArrays1, strArrays2, numArray1)
						{
							Text = "Реестр платежей пени и гос. пош.",
							MdiParent = base.MdiParent
						};
						frmReport.Show();
						frmReport = null;
						System.Windows.Forms.Cursor.Current = Cursors.Default;
					}
					else
					{
						numArray = new int[] { 2, 1 };
						int[] numArray2 = numArray;
						strArrays = new string[] { "@idperiod", "Month" };
						string[] strArrays3 = strArrays;
						strArrays = new string[] { IDPeriod.ToString(), this.dtPeriod.Text.ToString() };
						string[] strArrays4 = strArrays;
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repReestrOplPoPeniOfBank.rpt");
						Form form = new frmReports(str, strArrays3, strArrays4, numArray2)
						{
							Text = "Реестр платежей пени и гос. пош.",
							MdiParent = base.MdiParent
						};
						form.Show();
						form = null;
						System.Windows.Forms.Cursor.Current = Cursors.Default;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					System.Windows.Forms.Cursor.Current = Cursors.Default;
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}
	}
}