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
	public class frmPrintOldDebet : Form
	{
		private Label label1;

		private Button cmdOK;

		private int _per;

		private Button cmdClose;

		private Label label2;

		private TextBox txtCountMonth;

		private TextBox txtDolg;

		private System.ComponentModel.Container components = null;

		public frmPrintOldDebet(int per)
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
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					string str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repOldDebet.xls");
					string str1 = Depot.oSettings.ReportPath.Trim();
					int dUser = Depot.oSettings.IDUser;
					string str2 = string.Concat(str1, "Temp\\repOldDebet", dUser.ToString(), ".xls");
					if (File.Exists(str2))
					{
						File.Delete(str2);
						File.Copy(str, str2, true);
					}
					else
					{
						File.Copy(str, str2, true);
					}
					SqlParameter sqlParameter = new SqlParameter("@CountMonth", SqlDbType.Int)
					{
						Direction = ParameterDirection.Input,
						Value = Convert.ToInt32(this.txtCountMonth.Text)
					};
					SqlParameter sqlParameter1 = new SqlParameter("@Dolg", SqlDbType.Int)
					{
						Direction = ParameterDirection.Input,
						Value = Convert.ToDouble(this.txtDolg.Text)
					};
					SqlParameter sqlParameter2 = new SqlParameter("@Path", SqlDbType.VarChar)
					{
						Direction = ParameterDirection.Input,
						Value = str2.Trim()
					};
					SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2 };
					if (!Saver.ExecuteProcedure("repOldDebet", sqlParameterArray))
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

		private void frmPrintOldDebet_Load(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.cmdOK = new Button();
			this.cmdClose = new Button();
			this.label2 = new Label();
			this.txtCountMonth = new TextBox();
			this.txtDolg = new TextBox();
			base.SuspendLayout();
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Месяцев назад:";
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(36, 64);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 34;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(136, 64);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 35;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 37;
			this.label2.Text = "Сумма долга:";
			this.txtCountMonth.Location = new Point(91, 7);
			this.txtCountMonth.Name = "txtCountMonth";
			this.txtCountMonth.TabIndex = 38;
			this.txtCountMonth.Text = "2";
			this.txtDolg.Location = new Point(92, 32);
			this.txtDolg.Name = "txtDolg";
			this.txtDolg.TabIndex = 39;
			this.txtDolg.Text = "1000";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(234, 96);
			base.Controls.Add(this.txtDolg);
			base.Controls.Add(this.txtCountMonth);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(352, 120);
			base.MinimumSize = new System.Drawing.Size(240, 120);
			base.Name = "frmPrintOldDebet";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Отчёт просроченной задолженности";
			base.ResumeLayout(false);
		}
	}
}