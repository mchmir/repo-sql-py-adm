using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmRepaymentDebt : Form
	{
		private C1DateEdit dtDate;

		private Label label6;

		private GroupBox groupBox2;

		private Label lblAccount;

		private Label lblCountLives;

		private Label label13;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private TextBox txtNote;

		private Label label17;

		private Button cmdClose;

		private Button cmdOK;

		private C1DateEdit dtDate2;

		private Label label1;

		private Label label2;

		private System.ComponentModel.Container components = null;

		private NumericUpDown numAmount;

		private Gobject _gobject;

		public frmRepaymentDebt(Gobject oGobject)
		{
			this.InitializeComponent();
			this._gobject = oGobject;
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
					if (this.numAmount.Value != new decimal(0))
					{
						Document document = new Document()
						{
							oBatch = null,
							oContract = this._gobject.oContract,
							oPeriod = Depot.CurrentPeriod,
							oTypeDocument = Depot.oTypeDocuments.item((long)19),
							DocumentAmount = Convert.ToDouble(this.numAmount.Value),
							DocumentDate = (DateTime)this.dtDate.Value,
							Note = this.txtNote.Text
						};
						if (document.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						else
						{
							PD shortDateString = document.oPDs.Add();
							shortDateString.oTypePD = Depot.oTypePDs.item((long)18);
							shortDateString.oDocument = document;
							shortDateString.Value = ((DateTime)this.dtDate2.Value).ToShortDateString();
							if (shortDateString.Save() == 0)
							{
								base.Close();
								return;
							}
							else
							{
								document.Delete();
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
					}
					else
					{
						this.numAmount.Focus();
						return;
					}
				}
				catch
				{
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
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

		private void frmRepaymentDebt_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmRepaymentDebt_Load(object sender, EventArgs e)
		{
			try
			{
				if (this._gobject == null)
				{
					base.Close();
				}
				else if (this._gobject.oContract != null)
				{
					C1DateEdit date = this.dtDate;
					DateTime today = DateTime.Today;
					date.Value = today.Date;
					C1DateEdit c1DateEdit = this.dtDate2;
					today = DateTime.Today;
					c1DateEdit.Value = today.Date;
					Tools.LoadWindows(this);
					this.lblAccount.Text = this._gobject.oContract.Account;
					this.lblFIO.Text = this._gobject.oContract.oPerson.FullName;
					this.lblAddress.Text = this._gobject.oContract.oPerson.oAddress.get_ShortAddress();
					this.lblCountLives.Text = this._gobject.CountLives.ToString();
					this.numAmount.Value = Convert.ToDecimal(this._gobject.oContract.CurrentBalance());
				}
				else
				{
					base.Close();
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblCountLives = new Label();
			this.label13 = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.dtDate2 = new C1DateEdit();
			this.label1 = new Label();
			this.label2 = new Label();
			this.numAmount = new NumericUpDown();
			((ISupportInitialize)this.dtDate).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.dtDate2).BeginInit();
			((ISupportInitialize)this.numAmount).BeginInit();
			base.SuspendLayout();
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(128, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 1;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 16);
			this.label6.TabIndex = 56;
			this.label6.Text = "Дата документа";
			this.groupBox2.Controls.Add(this.lblAccount);
			this.groupBox2.Controls.Add(this.lblCountLives);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(2, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(288, 120);
			this.groupBox2.TabIndex = 55;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.lblAccount.BackColor = SystemColors.Info;
			this.lblAccount.BorderStyle = BorderStyle.FixedSingle;
			this.lblAccount.ForeColor = SystemColors.ControlText;
			this.lblAccount.Location = new Point(56, 16);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(112, 20);
			this.lblAccount.TabIndex = 11;
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(80, 88);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 10;
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 88);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Проживает";
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(56, 64);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(224, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(56, 40);
			this.lblFIO.Name = "lblFIO";
			this.lblFIO.Size = new System.Drawing.Size(224, 20);
			this.lblFIO.TabIndex = 4;
			this.label10.ForeColor = SystemColors.ControlText;
			this.label10.Location = new Point(8, 64);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 16);
			this.label10.TabIndex = 2;
			this.label10.Text = "Адрес";
			this.label11.ForeColor = SystemColors.ControlText;
			this.label11.Location = new Point(8, 40);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 16);
			this.label11.TabIndex = 1;
			this.label11.Text = "ФИО";
			this.label12.ForeColor = SystemColors.ControlText;
			this.label12.Location = new Point(8, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 16);
			this.label12.TabIndex = 0;
			this.label12.Text = "Л/с";
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 232);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(276, 32);
			this.txtNote.TabIndex = 4;
			this.txtNote.Text = "";
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 216);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 68;
			this.label17.Text = "Примечание";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(192, 272);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 6;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(88, 272);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.dtDate2.BorderStyle = 1;
			this.dtDate2.FormatType = FormatTypeEnum.LongDate;
			this.dtDate2.Location = new Point(130, 184);
			this.dtDate2.Name = "dtDate2";
			this.dtDate2.Size = new System.Drawing.Size(152, 18);
			this.dtDate2.TabIndex = 3;
			this.dtDate2.Tag = null;
			this.dtDate2.Value = new DateTime(2006, 9, 26, 0, 0, 0, 0);
			this.dtDate2.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 160);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 69;
			this.label1.Text = "Задолженность";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 184);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 72;
			this.label2.Text = "Срок погашения";
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(130, 160);
			NumericUpDown num = this.numAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown = this.numAmount;
			numArray = new int[] { 9999999, 0, 0, -2147483648 };
			numericUpDown.Minimum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.Size = new System.Drawing.Size(152, 20);
			this.numAmount.TabIndex = 2;
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(292, 303);
			base.Controls.Add(this.numAmount);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.dtDate2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmRepaymentDebt";
			this.Text = "График погашения задолженности";
			base.Closing += new CancelEventHandler(this.frmRepaymentDebt_Closing);
			base.Load += new EventHandler(this.frmRepaymentDebt_Load);
			((ISupportInitialize)this.dtDate).EndInit();
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.dtDate2).EndInit();
			((ISupportInitialize)this.numAmount).EndInit();
			base.ResumeLayout(false);
		}

		private void numAmount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmount);
		}
	}
}