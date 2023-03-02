using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintSvedenWorkAbonent : Form
	{
		private GroupBox groupBox1;

		private Button cmdClose;

		private Button cmdOK;

		private Label label2;

		private C1DateEdit dtBegin;

		private Label label1;

		private C1DateEdit dtEnd;

		private System.ComponentModel.Container components = null;

		public frmPrintSvedenWorkAbonent()
		{
			this.InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					SQLConnect.GetSQLConnect().ChangeDatabase("TechService");
					int[] numArray = new int[] { 3, 3 };
					string[] str = new string[] { "@dateB", "@dateE" };
					string[] strArrays = str;
					str = new string[] { this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString() };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSvedenWorkAbonent.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Откл. подк. ПУ и ОУ Аварийной службой с ", this.dtBegin.Text.ToString(), " по ", this.dtEnd.Text.ToString()),
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					SQLConnect.GetSQLConnect().ChangeDatabase("Gefest");
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					SQLConnect.GetSQLConnect().ChangeDatabase("Gefest");
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				SQLConnect.GetSQLConnect().ChangeDatabase("Gefest");
			}
			System.Windows.Forms.Cursor.Current = Cursors.Default;
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

		private void frmPrintSvedenWorkAbonent_Load(object sender, EventArgs e)
		{
			C1DateEdit c1DateEdit = this.dtBegin;
			DateTime today = DateTime.Today;
			DateTime dateTime = DateTime.Today;
			c1DateEdit.Value = today.AddDays((double)(-dateTime.Day + 1));
			C1DateEdit date = this.dtEnd;
			today = DateTime.Today;
			date.Value = today.Date;
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.groupBox1 = new GroupBox();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.label2 = new Label();
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			this.dtEnd = new C1DateEdit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dtBegin).BeginInit();
			((ISupportInitialize)this.dtEnd).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.dtBegin);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtEnd);
			this.groupBox1.Location = new Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(312, 48);
			this.groupBox1.TabIndex = 38;
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(220, 52);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 71;
			this.cmdClose.Text = "Отмена";
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(112, 52);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 70;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(154, 18);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 42;
			this.label2.Text = "по";
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.FormatType = FormatTypeEnum.LongDate;
			this.dtBegin.Location = new Point(30, 14);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(120, 18);
			this.dtBegin.TabIndex = 40;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(10, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 16);
			this.label1.TabIndex = 43;
			this.label1.Text = "C";
			this.dtEnd.BorderStyle = 1;
			this.dtEnd.FormatType = FormatTypeEnum.LongDate;
			this.dtEnd.Location = new Point(182, 14);
			this.dtEnd.Name = "dtEnd";
			this.dtEnd.Size = new System.Drawing.Size(120, 18);
			this.dtEnd.TabIndex = 41;
			this.dtEnd.Tag = null;
			this.dtEnd.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtEnd.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(314, 78);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdClose);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintSvedenWorkAbonent";
			this.Text = "Откл. подк. ПУ и ОУ Аварийной службой";
			base.Load += new EventHandler(this.frmPrintSvedenWorkAbonent_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtBegin).EndInit();
			((ISupportInitialize)this.dtEnd).EndInit();
			base.ResumeLayout(false);
		}
	}
}