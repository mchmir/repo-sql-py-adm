using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintAccount : Form
	{
		private C1TextBox txtAccount;

		private Button cmdAccount;

		private Label label12;

		private ImageList imageList1;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private DateTimePicker txtdBegin;

		private Label label1;

		private DateTimePicker txtdEnd;

		private Label label2;

		private Button cmdClose;

		private Button cmdOK;

		private IContainer components;

		private CheckBox checkBox1;

		private CheckBox chkUslugi;

		public Contract _contract;

		public frmPrintAccount()
		{
			this.InitializeComponent();
		}

		private void cmdAccount_Click(object sender, EventArgs e)
		{
			this.CreateContracts();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.Q(this.txtdBegin.Value, this.txtdEnd.Value);
			base.Close();
		}

		private void CreateContract()
		{
			this.txtAccount.Text = this._contract.Account;
			this.lblFIO.Text = this._contract.oPerson.FullName;
			this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
			this.cmdOK.Focus();
		}

		private void CreateContracts()
		{
			if (this.txtAccount.Text.Length > 0)
			{
				Contracts contract = new Contracts();
				if (contract.Load(this.txtAccount.Text.Trim()) == 0)
				{
					if (contract.get_Count() > 0)
					{
						this._contract = contract[0];
						this.CreateContract();
						return;
					}
					this.DefaultForm();
				}
			}
		}

		private void DefaultForm()
		{
			this._contract = null;
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPrintAccount_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmPrintAccount_Load(object sender, EventArgs e)
		{
			this.txtdBegin.Value = DateTime.Today.AddMonths(-3);
			this.txtdEnd.Value = DateTime.Today;
			if (this._contract == null)
			{
				this.DefaultForm();
				return;
			}
			this.CreateContract();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintAccount));
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.imageList1 = new ImageList(this.components);
			this.label12 = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.txtdBegin = new DateTimePicker();
			this.label1 = new Label();
			this.txtdEnd = new DateTimePicker();
			this.label2 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.checkBox1 = new CheckBox();
			this.chkUslugi = new CheckBox();
			((ISupportInitialize)this.txtAccount).BeginInit();
			base.SuspendLayout();
			this.txtAccount.BorderStyle = 1;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(44, 4);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(112, 21);
			this.txtAccount.TabIndex = 4;
			this.txtAccount.Tag = null;
			this.txtAccount.KeyPress += new KeyPressEventHandler(this.txtAccount_KeyPress);
			this.txtAccount.LostFocus += new EventHandler(this.txtAccount_LostFocus);
			this.txtAccount.GotFocus += new EventHandler(this.txtAccount_GotFocus);
			this.cmdAccount.FlatStyle = FlatStyle.Flat;
			this.cmdAccount.ForeColor = SystemColors.ControlText;
			this.cmdAccount.ImageIndex = 0;
			this.cmdAccount.ImageList = this.imageList1;
			this.cmdAccount.Location = new Point(160, 4);
			this.cmdAccount.Name = "cmdAccount";
			this.cmdAccount.Size = new System.Drawing.Size(20, 20);
			this.cmdAccount.TabIndex = 5;
			this.cmdAccount.Click += new EventHandler(this.cmdAccount_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.label12.ForeColor = SystemColors.ControlText;
			this.label12.Location = new Point(4, 8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 16);
			this.label12.TabIndex = 3;
			this.label12.Text = "Л/с";
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(44, 76);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(288, 20);
			this.lblAddress.TabIndex = 7;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(44, 52);
			this.lblFIO.Name = "lblFIO";
			this.lblFIO.Size = new System.Drawing.Size(288, 20);
			this.lblFIO.TabIndex = 6;
			this.label10.ForeColor = SystemColors.ControlText;
			this.label10.Location = new Point(4, 76);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 16);
			this.label10.TabIndex = 9;
			this.label10.Text = "Адрес";
			this.label11.ForeColor = SystemColors.ControlText;
			this.label11.Location = new Point(4, 52);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 16);
			this.label11.TabIndex = 8;
			this.label11.Text = "ФИО";
			this.txtdBegin.CustomFormat = "MMMMyyyy";
			this.txtdBegin.Format = DateTimePickerFormat.Custom;
			this.txtdBegin.Location = new Point(44, 28);
			this.txtdBegin.Name = "txtdBegin";
			this.txtdBegin.Size = new System.Drawing.Size(124, 20);
			this.txtdBegin.TabIndex = 10;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "С";
			this.txtdEnd.CustomFormat = "MMMMyyyy";
			this.txtdEnd.Format = DateTimePickerFormat.Custom;
			this.txtdEnd.Location = new Point(204, 28);
			this.txtdEnd.Name = "txtdEnd";
			this.txtdEnd.Size = new System.Drawing.Size(124, 20);
			this.txtdEnd.TabIndex = 10;
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(176, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "по";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(240, 100);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 12;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(136, 100);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 11;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.checkBox1.Location = new Point(4, 104);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(128, 20);
			this.checkBox1.TabIndex = 35;
			this.checkBox1.Text = "Текстовый режим";
			this.chkUslugi.Location = new Point(204, 0);
			this.chkUslugi.Name = "chkUslugi";
			this.chkUslugi.Size = new System.Drawing.Size(124, 24);
			this.chkUslugi.TabIndex = 36;
			this.chkUslugi.Text = "только услуги";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(338, 128);
			base.Controls.Add(this.chkUslugi);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.txtdBegin);
			base.Controls.Add(this.label10);
			base.Controls.Add(this.label11);
			base.Controls.Add(this.lblAddress);
			base.Controls.Add(this.lblFIO);
			base.Controls.Add(this.txtAccount);
			base.Controls.Add(this.cmdAccount);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtdEnd);
			base.Controls.Add(this.label2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(344, 152);
			base.MinimumSize = new System.Drawing.Size(344, 152);
			base.Name = "frmPrintAccount";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Карточка абонента";
			base.Closing += new CancelEventHandler(this.frmPrintAccount_Closing);
			base.Load += new EventHandler(this.frmPrintAccount_Load);
			((ISupportInitialize)this.txtAccount).EndInit();
			base.ResumeLayout(false);
		}

		private void Q(DateTime dBegin, DateTime dEnd)
		{
			Form frmReport;
			try
			{
				try
				{
					int num = 0;
					if (this.chkUslugi.Checked)
					{
						num = 1;
					}
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 3, 3, 2 };
					string[] str = new string[] { "@IdContract", "@dBegin", "@dEnd", "@IDAcc" };
					string[] strArrays = str;
					str = new string[4];
					long d = this._contract.get_ID();
					str[0] = d.ToString();
					DateTime date = dBegin.Date;
					str[1] = date.ToString("u");
					date = dEnd.Date;
					str[2] = date.ToString("u");
					str[3] = num.ToString();
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repAccount.rpt");
					if (!this.checkBox1.Checked)
					{
						frmReport = new frmReports(str1, strArrays, strArrays1, numArray);
					}
					else
					{
						frmReport = new frmReportText(str1, strArrays, strArrays1, numArray);
					}
					frmReport.Text = string.Concat("Карточка Абонента ", this.txtAccount.Text);
					frmReport.MdiParent = Depot._main;
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

		private void txtAccount_GotFocus(object sender, EventArgs e)
		{
			base.AcceptButton = this.cmdAccount;
		}

		private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdAccount_Click(null, null);
			}
		}

		private void txtAccount_LostFocus(object sender, EventArgs e)
		{
			base.AcceptButton = this.cmdOK;
		}
	}
}