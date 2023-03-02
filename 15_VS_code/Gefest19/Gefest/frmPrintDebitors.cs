using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintDebitors : Form
	{
		private Label label1;

		private TextBox txtSumDolg;

		private Label label2;

		private TextBox txtPeriodDolg;

		private Button bCancel;

		private Button bOK;

		private System.ComponentModel.Container components = null;

		public frmPrintDebitors()
		{
			this.InitializeComponent();
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			try
			{
				this.Rep(Convert.ToInt32(this.txtPeriodDolg.Text), Convert.ToInt32(this.txtSumDolg.Text));
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
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

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.txtSumDolg = new TextBox();
			this.label2 = new Label();
			this.txtPeriodDolg = new TextBox();
			this.bCancel = new Button();
			this.bOK = new Button();
			base.SuspendLayout();
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Сумма долга свыше, тг";
			this.txtSumDolg.Location = new Point(160, 12);
			this.txtSumDolg.Name = "txtSumDolg";
			this.txtSumDolg.Size = new System.Drawing.Size(128, 20);
			this.txtSumDolg.TabIndex = 1;
			this.txtSumDolg.Text = "5000";
			this.label2.Location = new Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Период долга свыше, мес";
			this.txtPeriodDolg.Location = new Point(160, 40);
			this.txtPeriodDolg.Name = "txtPeriodDolg";
			this.txtPeriodDolg.Size = new System.Drawing.Size(128, 20);
			this.txtPeriodDolg.TabIndex = 3;
			this.txtPeriodDolg.Text = "10";
			this.bCancel.Location = new Point(216, 72);
			this.bCancel.Name = "bCancel";
			this.bCancel.TabIndex = 4;
			this.bCancel.Text = "Отмена";
			this.bCancel.Click += new EventHandler(this.bCancel_Click);
			this.bOK.Location = new Point(136, 72);
			this.bOK.Name = "bOK";
			this.bOK.TabIndex = 5;
			this.bOK.Text = "ОК";
			this.bOK.Click += new EventHandler(this.bOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(296, 102);
			base.Controls.Add(this.bOK);
			base.Controls.Add(this.bCancel);
			base.Controls.Add(this.txtPeriodDolg);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtSumDolg);
			base.Controls.Add(this.label1);
			base.MaximizeBox = false;
			base.MaximumSize = new System.Drawing.Size(304, 136);
			base.MinimumSize = new System.Drawing.Size(304, 136);
			base.Name = "frmPrintDebitors";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Должники";
			base.ResumeLayout(false);
		}

		public void PublicRep()
		{
			this.Rep(18, 0);
		}

		private void Rep(int CountMonth, int SumDolg)
		{
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2 };
					string[] str = new string[] { "@CountMonth", "@SumDolg" };
					string[] strArrays = str;
					str = new string[] { CountMonth.ToString(), SumDolg.ToString() };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repDebitors.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "Должники",
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
			}
		}
	}
}