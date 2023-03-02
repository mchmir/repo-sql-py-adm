using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintDubleNumberPU : Form
	{
		private Button cmdClose;

		private Button cmdOK;

		private System.ComponentModel.Container components = null;

		public frmPrintDubleNumberPU()
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
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 1, 2 };
					string[] strArrays = new string[] { "@serialnumber", "@idtypegmeter" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { "0", "0" };
					string[] strArrays2 = strArrays;
					string str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repDubleNumberPU.rpt");
					Form frmReport = new frmReports(str, strArrays1, strArrays2, numArray)
					{
						Text = "Справка по ПУ с одинаковыми номерами",
						MdiParent = Depot._main
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

		private void InitializeComponent()
		{
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			base.SuspendLayout();
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(156, 8);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 14;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(52, 8);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 13;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			base.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.cmdClose;
			base.ClientSize = new System.Drawing.Size(252, 38);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintDubleNumberPU";
			this.Text = "Справка по ПУ с одинаковыми номерами ";
			base.ResumeLayout(false);
		}
	}
}