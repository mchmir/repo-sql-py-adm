using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintReferenceOnSubscriptionService : Form
	{
		private Label label2;

		private DateTimePicker txtdEnd;

		private GroupBox groupBox1;

		private Button cmdClose;

		private Button cmdOK;

		private System.ComponentModel.Container components = null;

		public frmPrintReferenceOnSubscriptionService()
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
				this.Rep(period[0].get_ID(), this.txtdEnd.Value);
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

		private void frmPrintReferenceOnSubscriptionService_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmPrintReferenceOnSubscriptionService_Load(object sender, EventArgs e)
		{
			this.txtdEnd.Focus();
		}

		private void InitializeComponent()
		{
			this.label2 = new Label();
			this.txtdEnd = new DateTimePicker();
			this.groupBox1 = new GroupBox();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label2.Location = new Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 48;
			this.label2.Text = "Дата";
			this.txtdEnd.Location = new Point(72, 12);
			this.txtdEnd.Name = "txtdEnd";
			this.txtdEnd.Size = new System.Drawing.Size(124, 20);
			this.txtdEnd.TabIndex = 1;
			this.groupBox1.BackColor = SystemColors.Control;
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtdEnd);
			this.groupBox1.ForeColor = SystemColors.ControlText;
			this.groupBox1.Location = new Point(4, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(204, 40);
			this.groupBox1.TabIndex = 49;
			this.groupBox1.TabStop = false;
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(116, 44);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 4;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(12, 44);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = SystemColors.ControlLight;
			base.ClientSize = new System.Drawing.Size(214, 74);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(220, 99);
			base.MinimumSize = new System.Drawing.Size(220, 99);
			base.Name = "frmPrintReferenceOnSubscriptionService";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Справка по абонентской службе";
			base.Closing += new CancelEventHandler(this.frmPrintReferenceOnSubscriptionService_Closing);
			base.Load += new EventHandler(this.frmPrintReferenceOnSubscriptionService_Load);
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void Rep(long IDPeriod, DateTime dBegin)
		{
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 3, 2, 1, 1 };
					string[] str = new string[] { "@Date", "@idPeriod", "Month", "Day" };
					string[] strArrays = str;
					str = new string[4];
					DateTime date = dBegin.Date;
					str[0] = date.ToString("u");
					str[1] = IDPeriod.ToString();
					date = this.txtdEnd.Value;
					str[2] = date.ToString("MMMM yyyy");
					str[3] = this.txtdEnd.Text;
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repReferenceOnSubscriptionService.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Справка по абонентской службе за ", this.txtdEnd.Text),
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