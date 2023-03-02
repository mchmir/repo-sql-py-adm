using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintPaymentsMonth : Form
	{
		private Label label1;

		private C1DateEdit dtBegin;

		private Button cmdClose;

		private Button cmdOK;

		private int _per;

		private Label label2;

		private C1DateEdit dtEnd;

		private System.ComponentModel.Container components = null;

		public frmPrintPaymentsMonth(int per)
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
			long num;
			long num1;
			Periods period = new Periods();
			period.Load((DateTime)this.dtBegin.Value);
			num = (period.get_Count() <= 0 ? (long)0 : period[0].get_ID());
			period.Load((DateTime)this.dtEnd.Value);
			num1 = (period.get_Count() <= 0 ? (long)0 : period[0].get_ID());
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					string str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repPaymentsMonth.xls");
					string str1 = Depot.oSettings.ReportPath.Trim();
					int dUser = Depot.oSettings.IDUser;
					string str2 = string.Concat(str1, "Temp\\repPaymentsMonth", dUser.ToString(), ".xls");
					if (File.Exists(str2))
					{
						File.Delete(str2);
						File.Copy(str, str2, true);
					}
					else
					{
						File.Copy(str, str2, true);
					}
					SqlParameter sqlParameter = new SqlParameter("@idPeriodB", SqlDbType.Int)
					{
						Direction = ParameterDirection.Input,
						Value = num
					};
					SqlParameter sqlParameter1 = new SqlParameter("@idPeriodE", SqlDbType.Int)
					{
						Direction = ParameterDirection.Input,
						Value = num1
					};
					SqlParameter sqlParameter2 = new SqlParameter("@Path", SqlDbType.VarChar)
					{
						Direction = ParameterDirection.Input,
						Value = str2.Trim()
					};
					SqlParameter sqlParameter3 = new SqlParameter("@DateName", SqlDbType.VarChar)
					{
						Direction = ParameterDirection.Input,
						Value = "за период с "
					};
					DateTime value = (DateTime)this.dtBegin.Value;
					SqlParameter sqlParameter4 = sqlParameter3;
					object obj = sqlParameter4.Value;
					object[] objArray = new object[] { obj, Tools.NameMonthIP(value.Month), " ", Convert.ToString(value.Year) };
					sqlParameter4.Value = string.Concat(objArray);
					value = (DateTime)this.dtEnd.Value;
					SqlParameter sqlParameter5 = sqlParameter3;
					obj = sqlParameter5.Value;
					objArray = new object[] { obj, " по ", Tools.NameMonthIP(value.Month), " ", Convert.ToString(value.Year) };
					sqlParameter5.Value = string.Concat(objArray);
					SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3 };
					if (!Saver.ExecuteProcedure("repPaymentsMonth", sqlParameterArray))
					{
						MessageBox.Show("Ошибка формирования отчета, возможно данный файл уже открыт", "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						Process.Start("Excel", str2);
					}
					this.Cursor = Cursors.Default;
				}
				catch (Exception exception)
				{
					this.Cursor = Cursors.Default;
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
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

		private void frmPrintPaymentsMonth_Load(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.label2 = new Label();
			this.dtEnd = new C1DateEdit();
			((ISupportInitialize)this.dtBegin).BeginInit();
			((ISupportInitialize)this.dtEnd).BeginInit();
			base.SuspendLayout();
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.CustomFormat = "MMMM yyyy";
			this.dtBegin.FormatType = FormatTypeEnum.CustomFormat;
			this.dtBegin.Location = new Point(64, 6);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(120, 18);
			this.dtBegin.TabIndex = 31;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2012, 10, 11, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Период с:";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(236, 52);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 35;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(136, 52);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 34;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(190, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 16);
			this.label2.TabIndex = 37;
			this.label2.Text = "по";
			this.dtEnd.BorderStyle = 1;
			this.dtEnd.CustomFormat = "MMMM yyyy";
			this.dtEnd.FormatType = FormatTypeEnum.CustomFormat;
			this.dtEnd.Location = new Point(210, 6);
			this.dtEnd.Name = "dtEnd";
			this.dtEnd.Size = new System.Drawing.Size(120, 18);
			this.dtEnd.TabIndex = 38;
			this.dtEnd.Tag = null;
			this.dtEnd.Value = new DateTime(2012, 10, 11, 0, 0, 0, 0);
			this.dtEnd.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(334, 80);
			base.Controls.Add(this.dtEnd);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.dtBegin);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(352, 104);
			base.MinimumSize = new System.Drawing.Size(352, 104);
			base.Name = "frmPrintPaymentsMonth";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Отчёт о поступлении денежных средств";
			((ISupportInitialize)this.dtBegin).EndInit();
			((ISupportInitialize)this.dtEnd).EndInit();
			base.ResumeLayout(false);
		}
	}
}