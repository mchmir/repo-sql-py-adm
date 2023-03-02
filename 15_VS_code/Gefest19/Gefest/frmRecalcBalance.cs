using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmRecalcBalance : Form
	{
		private Button cmdCancel;

		private C1DateEdit dtDate;

		private Label label1;

		private Button cmdApply;

		private System.ComponentModel.Container components = null;

		public frmRecalcBalance()
		{
			this.InitializeComponent();
		}

		private void cmdApply_Click(object sender, EventArgs e)
		{
			if (DateTime.Today.Subtract((DateTime)this.dtDate.Value).Days >= 60)
			{
				MessageBox.Show("Невозможно пересчитать баланс", "Пересчет баланса", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			frmLoad _frmLoad = new frmLoad("Пересчет остатков по абонентам...");
			_frmLoad.Show();
			_frmLoad.Refresh();
			Periods period = new Periods();
			period.Load((DateTime)this.dtDate.Value);
			_frmLoad.Refresh();
			this.spRecalcBalance(period[0].get_ID());
			this.spRecalcBalanceReal(period[0].get_ID());
			_frmLoad.Close();
			_frmLoad = null;
			MessageBox.Show("Пересчет остатков закончен", "Пересчет баланса", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			base.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
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

		private void frmRecalcBalance_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmRecalcBalance_Load(object sender, EventArgs e)
		{
			this.dtDate.Value = DateTime.Today;
		}

		private void InitializeComponent()
		{
			this.cmdCancel = new Button();
			this.dtDate = new C1DateEdit();
			this.label1 = new Label();
			this.cmdApply = new Button();
			((ISupportInitialize)this.dtDate).BeginInit();
			base.SuspendLayout();
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.ForeColor = SystemColors.ControlText;
			this.cmdCancel.Location = new Point(168, 32);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(120, 24);
			this.cmdCancel.TabIndex = 68;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.YearAndMonth;
			this.dtDate.Location = new Point(104, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(184, 18);
			this.dtDate.TabIndex = 65;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 67;
			this.label1.Text = "Период";
			this.cmdApply.FlatStyle = FlatStyle.Flat;
			this.cmdApply.ForeColor = SystemColors.ControlText;
			this.cmdApply.Location = new Point(40, 32);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(120, 24);
			this.cmdApply.TabIndex = 66;
			this.cmdApply.Text = "Пересчет";
			this.cmdApply.Click += new EventHandler(this.cmdApply_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(288, 61);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmdApply);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmRecalcBalance";
			this.Text = "Пересчет остатков";
			base.Closing += new CancelEventHandler(this.frmRecalcBalance_Closing);
			base.Load += new EventHandler(this.frmRecalcBalance_Load);
			((ISupportInitialize)this.dtDate).EndInit();
			base.ResumeLayout(false);
		}

		private void spRecalcBalance(long IDPeriod)
		{
			try
			{
				SqlParameter sqlParameter = new SqlParameter("@idPeriod", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDPeriod
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter };
				Saver.ExecuteProcedure("spRecalcBalancesOnePeriod", sqlParameterArray);
			}
			catch (Exception exception)
			{
			}
		}

		private void spRecalcBalanceReal(long IDPeriod)
		{
			try
			{
				SqlParameter sqlParameter = new SqlParameter("@idPeriod", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = IDPeriod
				};
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter };
				Saver.ExecuteProcedure("spRecalcBalancesRealOnePeriod", sqlParameterArray);
			}
			catch (Exception exception)
			{
			}
		}
	}
}