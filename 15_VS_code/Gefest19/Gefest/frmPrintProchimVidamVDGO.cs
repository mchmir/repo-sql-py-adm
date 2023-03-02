using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintProchimVidamVDGO : Form
	{
		private GroupBox groupBox1;

		private C1DateEdit dtPeriod;

		private Label label1;

		private Button cmdClose;

		private Button cmdOK;

		private int type;

		private System.ComponentModel.Container components = null;

		public frmPrintProchimVidamVDGO(int Type)
		{
			this.InitializeComponent();
			this.type = Type;
			switch (this.type)
			{
				case 0:
				{
					this.Text = "Отчёт по прочим видам деятельности";
					return;
				}
				case 1:
				{
					this.Text = "Справка по кредитовой задолженности";
					return;
				}
				case 2:
				{
					this.Text = "Оплата за услуги ВДГО";
					return;
				}
				default:
				{
					return;
				}
			}
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
				MessageBox.Show("Неверный выбор периода!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

		private void frmPrintProchimVidamVDGO_Load(object sender, EventArgs e)
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
			this.dtPeriod.CustomFormat = "MMMM yyyy";
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
			this.cmdClose.Location = new Point(152, 48);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 3;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(48, 48);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(250, 74);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintProchimVidamVDGO";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Отчёт по прочим видам деятельности";
			base.Load += new EventHandler(this.frmPrintProchimVidamVDGO_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDPeriod)
		{
			int[] numArray;
			string[] strArrays;
			string[] strArrays1;
			string str;
			Form frmReport;
			int[] numArray1;
			string[] str1;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					switch (this.type)
					{
						case 0:
						{
							numArray1 = new int[] { 2, 1 };
							numArray = numArray1;
							str1 = new string[] { "@idperiod", "@Month" };
							strArrays = str1;
							str1 = new string[] { IDPeriod.ToString(), this.dtPeriod.Text.ToString() };
							strArrays1 = str1;
							str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repProchimVidamVDGO.rpt");
							frmReport = new frmReports(str, strArrays, strArrays1, numArray)
							{
								Text = "Отёт по прочим видам деятельности",
								MdiParent = base.MdiParent
							};
							frmReport.Show();
							frmReport = null;
							break;
						}
						case 1:
						{
							numArray1 = new int[] { 2, 1 };
							numArray = numArray1;
							str1 = new string[] { "@idPeriod", "@Month" };
							strArrays = str1;
							str1 = new string[] { IDPeriod.ToString(), this.dtPeriod.Text.ToString() };
							strArrays1 = str1;
							str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSpravkaProchimVidamVDGO.rpt");
							frmReport = new frmReports(str, strArrays, strArrays1, numArray)
							{
								Text = "Справка по кредитовой задолженности",
								MdiParent = base.MdiParent
							};
							frmReport.Show();
							frmReport = null;
							break;
						}
						case 2:
						{
							numArray = new int[] { 2, 2, 1, 1 };
							str1 = new string[] { "@idPeriod", "@Status", "@Path", "@MonthName" };
							strArrays = str1;
							str1 = new string[] { IDPeriod.ToString(), "0", "", this.dtPeriod.Text.ToString() };
							strArrays1 = str1;
							str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSalesUslULOplata.rpt");
							frmReport = new frmReports(str, strArrays, strArrays1, numArray)
							{
								Text = "Отчёт по опалте за услуги ВДГО",
								MdiParent = base.MdiParent
							};
							frmReport.Show();
							frmReport = null;
							break;
						}
					}
					System.Windows.Forms.Cursor.Current = Cursors.Default;
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