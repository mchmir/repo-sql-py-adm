using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintReferenceOnSubscriptionServiceUR : Form
	{
		private Button cmdClose;

		private Button cmdOK;

		private GroupBox groupBox1;

		private Label label2;

		private DateTimePicker txtdEnd;

		private CheckBox checkBox1;

		private System.ComponentModel.Container components = null;

		public frmPrintReferenceOnSubscriptionServiceUR()
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
			period.Load(this.txtdEnd.Value);
			if (period.get_Count() <= 0)
			{
				MessageBox.Show("Невозможно сформировать отчет", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

		private void frmPrintReferenceOnSubscriptionServiceUR_Load(object sender, EventArgs e)
		{
			this.txtdEnd.Focus();
			this.checkBox1.Checked = true;
		}

		private void InitializeComponent()
		{
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.groupBox1 = new GroupBox();
			this.label2 = new Label();
			this.txtdEnd = new DateTimePicker();
			this.checkBox1 = new CheckBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(116, 72);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 51;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(12, 72);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 50;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.groupBox1.BackColor = SystemColors.Control;
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtdEnd);
			this.groupBox1.ForeColor = SystemColors.ControlText;
			this.groupBox1.Location = new Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(204, 64);
			this.groupBox1.TabIndex = 52;
			this.groupBox1.TabStop = false;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label2.Location = new Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 48;
			this.label2.Text = "Период";
			this.txtdEnd.Location = new Point(72, 12);
			this.txtdEnd.Name = "txtdEnd";
			this.txtdEnd.Size = new System.Drawing.Size(124, 20);
			this.txtdEnd.TabIndex = 1;
			this.checkBox1.Location = new Point(72, 40);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(80, 16);
			this.checkBox1.TabIndex = 49;
			this.checkBox1.Text = "В м3";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(214, 98);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintReferenceOnSubscriptionServiceUR";
			this.Text = "Отчёт о реализации СУВГ абонентами";
			base.Load += new EventHandler(this.frmPrintReferenceOnSubscriptionServiceUR_Load);
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void Rep(long IDPeriod)
		{
			string str;
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 1 };
					string[] strArrays = new string[] { "@idPeriod", "@Month" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { IDPeriod.ToString(), null };
					DateTime value = this.txtdEnd.Value;
					strArrays[1] = value.ToString("MMMM yyyy");
					string[] strArrays2 = strArrays;
					str = (this.checkBox1.Checked ? string.Concat(Depot.oSettings.ReportPath.Trim(), "repReferenceOnSubscriptionServiceURM3.rpt") : string.Concat(Depot.oSettings.ReportPath.Trim(), "repReferenceOnSubscriptionServiceUR.rpt"));
					Form frmReport = new frmReports(str, strArrays1, strArrays2, numArray)
					{
						Text = string.Concat("Отчёт о реализации СУВГ абонентами за ", this.txtdEnd.Text),
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