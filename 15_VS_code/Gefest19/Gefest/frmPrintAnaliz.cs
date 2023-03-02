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
	public class frmPrintAnaliz : Form
	{
		private Button cmdClose;

		private Button cmdOK;

		private C1DateEdit dtBegin;

		private Label label1;

		private System.ComponentModel.Container components = null;

		public frmPrintAnaliz()
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
					string str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repAnaliz.xls");
					string str1 = Depot.oSettings.ReportPath.Trim();
					int dUser = Depot.oSettings.IDUser;
					string str2 = string.Concat(str1, "Temp\\repAnaliz", dUser.ToString(), ".xls");
					if (File.Exists(str2))
					{
						File.Delete(str2);
						File.Copy(str, str2, true);
					}
					else
					{
						File.Copy(str, str2, true);
					}
					SqlParameter sqlParameter = new SqlParameter("@year", SqlDbType.DateTime)
					{
						Direction = ParameterDirection.Input,
						Value = this.dtBegin.Value
					};
					SqlParameter sqlParameter1 = new SqlParameter("@path", SqlDbType.VarChar)
					{
						Direction = ParameterDirection.Input,
						Value = str2.Trim()
					};
					SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1 };
					if (!Saver.ExecuteProcedure("repAnaliz", sqlParameterArray))
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

		private void frmPrintAnaliz_Load(object sender, EventArgs e)
		{
			this.dtBegin.CustomFormat = "yyyy";
			this.dtBegin.FormatType = FormatTypeEnum.CustomFormat;
			this.dtBegin.Value = DateTime.Today.Date;
		}

		private void InitializeComponent()
		{
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			((ISupportInitialize)this.dtBegin).BeginInit();
			base.SuspendLayout();
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(108, 28);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 39;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(8, 28);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 38;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.FormatType = FormatTypeEnum.LongDate;
			this.dtBegin.Location = new Point(48, 4);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(152, 18);
			this.dtBegin.TabIndex = 36;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 37;
			this.label1.Text = "Год";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(206, 58);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.dtBegin);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintAnaliz";
			this.Text = "Отчет для АМК";
			base.Load += new EventHandler(this.frmPrintAnaliz_Load);
			((ISupportInitialize)this.dtBegin).EndInit();
			base.ResumeLayout(false);
		}
	}
}