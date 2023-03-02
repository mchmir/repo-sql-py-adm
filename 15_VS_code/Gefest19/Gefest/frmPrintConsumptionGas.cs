using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintConsumptionGas : Form
	{
		private Label label1;

		private C1DateEdit dtBegin;

		private Label label2;

		private C1DateEdit dtEnd;

		private Button cmdClose;

		private Button cmdOK;

		private CheckBox checkBox1;

		private int _per;

		private System.ComponentModel.Container components = null;

		public frmPrintConsumptionGas(int per)
		{
			this.InitializeComponent();
			this._per = per;
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Periods period = new Periods();
			period.Load((DateTime)this.dtBegin.Value);
			if (period.get_Count() <= 0)
			{
				this.Rep((long)0);
			}
			else
			{
				this.Rep(period[0].get_ID());
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

		private void frmPrintConsumptionGas_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmPrintConsumptionGas_Load(object sender, EventArgs e)
		{
			DateTime today;
			DateTime dateTime;
			switch (this._per)
			{
				case 1:
				{
					this.dtEnd.Visible = false;
					this.label2.Visible = false;
					this.dtBegin.CustomFormat = "MMMM yyyy";
					this.dtBegin.FormatType = FormatTypeEnum.CustomFormat;
					C1DateEdit date = this.dtBegin;
					today = DateTime.Today;
					date.Value = today.Date;
					this.Text = "Отчёт о потребление газа";
					return;
				}
				case 2:
				{
					C1DateEdit c1DateEdit = this.dtBegin;
					today = DateTime.Today;
					dateTime = DateTime.Today;
					c1DateEdit.Value = today.AddDays((double)(-dateTime.Day + 1));
					C1DateEdit date1 = this.dtEnd;
					today = DateTime.Today;
					date1.Value = today.Date;
					this.checkBox1.Visible = false;
					this.label1.Text = "C";
					this.Text = "Отчёт потребление по РУ";
					return;
				}
				case 3:
				{
					this.dtEnd.Visible = false;
					this.label2.Visible = false;
					this.dtBegin.CustomFormat = "MMMM yyyy";
					this.dtBegin.FormatType = FormatTypeEnum.CustomFormat;
					C1DateEdit c1DateEdit1 = this.dtBegin;
					today = DateTime.Today;
					c1DateEdit1.Value = today.Date;
					this.checkBox1.Text = "С начислением по среднему";
					this.Text = "Справка по начислению за газ";
					return;
				}
				case 4:
				{
					this.dtEnd.Visible = false;
					this.label2.Visible = false;
					this.dtBegin.CustomFormat = "MMMM yyyy";
					this.dtBegin.FormatType = FormatTypeEnum.CustomFormat;
					C1DateEdit date2 = this.dtBegin;
					today = DateTime.Today;
					date2.Value = today.Date;
					this.checkBox1.Visible = false;
					this.Text = "Оперативные данные";
					return;
				}
				case 5:
				{
					C1DateEdit c1DateEdit2 = this.dtBegin;
					today = DateTime.Today;
					dateTime = DateTime.Today;
					c1DateEdit2.Value = today.AddDays((double)(-dateTime.Day + 1));
					C1DateEdit date3 = this.dtEnd;
					today = DateTime.Today;
					date3.Value = today.Date;
					this.checkBox1.Visible = false;
					this.label1.Text = "C";
					this.Text = "Отчёт по работе операторов";
					return;
				}
				case 6:
				{
					this.dtEnd.Visible = false;
					this.label2.Visible = false;
					this.dtBegin.CustomFormat = "MMMM yyyy";
					this.dtBegin.FormatType = FormatTypeEnum.CustomFormat;
					C1DateEdit c1DateEdit3 = this.dtBegin;
					today = DateTime.Today;
					c1DateEdit3.Value = today.Date;
					this.checkBox1.Visible = false;
					this.Text = "Отчёт по внесению показаний операторов";
					return;
				}
				case 7:
				{
					this.dtEnd.Visible = false;
					this.label2.Visible = false;
					this.dtBegin.CustomFormat = "MMMM yyyy";
					this.dtBegin.FormatType = FormatTypeEnum.CustomFormat;
					C1DateEdit date4 = this.dtBegin;
					today = DateTime.Today;
					date4.Value = today.Date;
					this.checkBox1.Visible = false;
					this.Text = "Отчёт по внесению показаний от контролёров";
					return;
				}
				case 8:
				{
					this.dtEnd.Visible = false;
					this.label2.Visible = false;
					this.label1.Visible = false;
					this.dtBegin.Visible = false;
					this.checkBox1.Visible = false;
					this.Text = "Справка по абон. , отказ. от дост. сч-ов";
					return;
				}
				case 9:
				{
					C1DateEdit c1DateEdit4 = this.dtBegin;
					today = DateTime.Today;
					c1DateEdit4.Value = today.Date;
					C1DateEdit date5 = this.dtEnd;
					today = DateTime.Today;
					date5.Value = today.Date;
					this.checkBox1.Visible = false;
					this.label1.Text = "C";
					this.Text = "Справка по неопломбированным ПУ";
					return;
				}
				case 10:
				{
					C1DateEdit c1DateEdit5 = this.dtBegin;
					today = DateTime.Today;
					dateTime = DateTime.Today;
					c1DateEdit5.Value = today.AddDays((double)(-dateTime.Day + 1));
					C1DateEdit date6 = this.dtEnd;
					today = DateTime.Today;
					date6.Value = today.Date;
					this.checkBox1.Visible = false;
					this.label1.Text = "C";
					this.Text = "Реестр снятых пломб";
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void InitializeComponent()
		{
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			this.dtEnd = new C1DateEdit();
			this.label2 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.checkBox1 = new CheckBox();
			((ISupportInitialize)this.dtBegin).BeginInit();
			((ISupportInitialize)this.dtEnd).BeginInit();
			base.SuspendLayout();
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.FormatType = FormatTypeEnum.LongDate;
			this.dtBegin.Location = new Point(48, 4);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(120, 18);
			this.dtBegin.TabIndex = 31;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Период";
			this.dtEnd.BorderStyle = 1;
			this.dtEnd.FormatType = FormatTypeEnum.LongDate;
			this.dtEnd.Location = new Point(204, 4);
			this.dtEnd.Name = "dtEnd";
			this.dtEnd.Size = new System.Drawing.Size(120, 18);
			this.dtEnd.TabIndex = 31;
			this.dtEnd.Tag = null;
			this.dtEnd.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtEnd.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(176, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 32;
			this.label2.Text = "по";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(232, 28);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 35;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(132, 28);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 34;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.checkBox1.Location = new Point(4, 24);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(124, 32);
			this.checkBox1.TabIndex = 33;
			this.checkBox1.Text = "Текстовый режим";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(330, 55);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.dtBegin);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dtEnd);
			base.Controls.Add(this.label2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(336, 80);
			base.MinimumSize = new System.Drawing.Size(336, 80);
			base.Name = "frmPrintConsumptionGas";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Отчёт о потребление газа";
			base.Closing += new CancelEventHandler(this.frmPrintConsumptionGas_Closing);
			base.Load += new EventHandler(this.frmPrintConsumptionGas_Load);
			((ISupportInitialize)this.dtBegin).EndInit();
			((ISupportInitialize)this.dtEnd).EndInit();
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
			this.Cursor = Cursors.WaitCursor;
			switch (this._per)
			{
				case 1:
				{
					numArray1 = new int[] { 2, 1 };
					numArray = numArray1;
					str1 = new string[] { "@IdPeriod", "Month" };
					strArrays = str1;
					str1 = new string[] { IDPeriod.ToString(), this.dtBegin.Text.ToString() };
					strArrays1 = str1;
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repConsumptionGas.rpt");
					if (!this.checkBox1.Checked)
					{
						frmReport = new frmReports(str, strArrays, strArrays1, numArray);
					}
					else
					{
						frmReport = new frmReportText(str, strArrays, strArrays1, numArray);
					}
					frmReport.Text = string.Concat("Отчёт о потребление газа за ", this.dtBegin.Text.ToString());
					frmReport.MdiParent = base.MdiParent;
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 2:
				{
					numArray1 = new int[] { 3, 3 };
					numArray = numArray1;
					str1 = new string[] { "@dBegin", "@dEnd" };
					strArrays = str1;
					str1 = new string[] { this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString() };
					strArrays1 = str1;
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repConsumptionPoRU.rpt");
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Отчёт потребление по РУ с ", this.dtBegin.Text.ToString(), " по ", this.dtEnd.Text.ToString()),
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 3:
				{
					numArray1 = new int[] { 2, 1 };
					numArray = numArray1;
					str1 = new string[] { "@idperiod", "Month" };
					strArrays = str1;
					str1 = new string[] { IDPeriod.ToString(), this.dtBegin.Text.ToString() };
					strArrays1 = str1;
					str = (!this.checkBox1.Checked ? string.Concat(Depot.oSettings.ReportPath.Trim(), "repReferenceAddingNotAverage.rpt") : string.Concat(Depot.oSettings.ReportPath.Trim(), "repReferenceAddingAverage.rpt"));
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Справка по начислению за газ за ", this.dtBegin.Text.ToString()),
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 4:
				{
					numArray = new int[] { 2, 2, 1 };
					str1 = new string[] { "@month", "@year", "MonthName" };
					strArrays = str1;
					str1 = new string[3];
					DateTime value = (DateTime)this.dtBegin.Value;
					int month = value.Month;
					str1[0] = month.ToString();
					value = (DateTime)this.dtBegin.Value;
					month = value.Year;
					str1[1] = month.ToString();
					str1[2] = this.dtBegin.Text.ToString();
					strArrays1 = str1;
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repOperData.rpt");
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Оперативные данные, ", this.dtBegin.Text.ToString()),
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 5:
				{
					numArray1 = new int[] { 3, 3 };
					numArray = numArray1;
					str1 = new string[] { "@dBegin", "@dEnd" };
					strArrays = str1;
					str1 = new string[] { this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString() };
					strArrays1 = str1;
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repRaznoska.rpt");
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Отчёт по работе операторов с ", this.dtBegin.Text.ToString(), " по ", this.dtEnd.Text.ToString()),
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 6:
				{
					numArray1 = new int[] { 2 };
					numArray = numArray1;
					str1 = new string[] { "@idperiod" };
					strArrays = str1;
					str1 = new string[] { IDPeriod.ToString() };
					strArrays1 = str1;
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repRaznoskaIndication.rpt");
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = "Отчёт по внесению показаний операторов",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 7:
				{
					numArray1 = new int[] { 2, 1 };
					numArray = numArray1;
					str1 = new string[] { "@idperiod", "@Period" };
					strArrays = str1;
					str1 = new string[] { IDPeriod.ToString(), this.dtBegin.Text.ToString() };
					strArrays1 = str1;
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repRaznoskaAgent.rpt");
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = "Отчёт по внесению показаний от контролёров",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 8:
				{
					numArray1 = new int[0];
					numArray = numArray1;
					str1 = new string[0];
					strArrays = str1;
					str1 = new string[0];
					strArrays1 = str1;
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSpravkaCountNoticeNO.rpt");
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = "Справка по абон. , отказ. от дост. сч-ов",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 9:
				{
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repNoPlombGMeter.rpt");
					numArray = new int[] { 2, 3, 3 };
					str1 = new string[] { "@idperiod", "@dBegin", "@dEnd" };
					strArrays = str1;
					str1 = new string[] { Convert.ToString(0), this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString() };
					strArrays1 = str1;
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = "Справка по неопломбированным ПУ",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
				case 10:
				{
					numArray1 = new int[] { 3, 3 };
					numArray = numArray1;
					str1 = new string[] { "@DB", "@DE" };
					strArrays = str1;
					str1 = new string[] { this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString() };
					strArrays1 = str1;
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repReestrUnplomb.rpt");
					frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Реестр снятых пломб с ", this.dtBegin.Text.ToString(), " по ", this.dtEnd.Text.ToString()),
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					break;
				}
			}
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}
	}
}