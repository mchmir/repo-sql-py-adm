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
	public class frmDocCreditUsl : Form
	{
		private GroupBox groupBox2;

		private C1TextBox txtAccount;

		private Button cmdAccount;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private ImageList imageList1;

		private ToolTip toolTip1;

		private Button cmdOK;

		private IContainer components;

		private Contract _contract;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private Tariffs _tariffs;

		private TypeCharges _TypeCharges;

		private Accountings _Accountings;

		private UslugiVDGOs _UslugiVDGOs;

		private Agents _agents5;

		private Label lblIskSaldo;

		private Document _doc;

		private PD _pd13;

		private FactUse _fu;

		private Operation _operation;

		private Balance _balance;

		private Button cmdClose;

		private NumericUpDown numAmount;

		private TextBox txtNote;

		private Label label17;

		private NumericUpDown numCountMonth;

		private Label label18;

		private Label label7;

		private C1DateEdit dtDate;

		private Label label6;

		private GroupBox groupBox1;

		private Label lblAmountByMonth;

		private bool isEdit;

		public frmDocCreditUsl(Document oDocument)
		{
			this.InitializeComponent();
			this._doc = oDocument;
			this._contract = this._doc.oContract;
		}

		private void cmdAccount_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.txtAccount.Text.Length != 0)
				{
					Contracts contract = new Contracts();
					if (contract.Load(this.txtAccount.Text.Trim()) != 0)
					{
						this.ResetFields1();
					}
					else if (contract.get_Count() <= 0)
					{
						this.ResetFields1();
					}
					else
					{
						this._contract = contract[0];
						this.lblIskSaldo.Text = "";
						this.lblFIO.Text = this._contract.oPerson.FullName;
						this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
						this.dtDate.Focus();
					}
				}
				else
				{
					this.ResetFields1();
				}
			}
			catch
			{
			}
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
					if (this._contract != null)
					{
						if (this._doc.get_ID() == (long)0)
						{
							this._doc.oBatch = null;
							this._doc.oContract = this._contract;
							this._doc.oPeriod = Depot.CurrentPeriod;
							this._doc.oTypeDocument = Depot.oTypeDocuments.item((long)25);
							this._doc.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
							this._doc.DocumentDate = (DateTime)this.dtDate.Value;
							this._doc.Note = this.txtNote.Text;
							if (this._doc.Save() != 0)
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
							else
							{
								PD str = this._doc.oPDs.Add();
								str.oTypePD = Depot.oTypePDs.item((long)16);
								str.oDocument = this._doc;
								str.Value = "137";
								if (str.Save() == 0)
								{
									str = this._doc.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)35);
									str.oDocument = this._doc;
									str.Value = "21";
									if (str.Save() == 0)
									{
										str = this._doc.oPDs.Add();
										str.oTypePD = Depot.oTypePDs.item((long)1);
										str.oDocument = this._doc;
										str.Value = Convert.ToString(this.numCountMonth.Value);
										if (str.Save() == 0)
										{
											bool flag = false;
											long d = this._doc.get_ID();
											if (!this._doc.SaveGrafikForCreditUsl(d, ref flag))
											{
												this._doc.Delete();
												MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
											}
										}
										else
										{
											this._doc.Delete();
											MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
									}
									else
									{
										this._doc.Delete();
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
								}
								else
								{
									this._doc.Delete();
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
						}
						base.DialogResult = System.Windows.Forms.DialogResult.OK;
						base.Close();
					}
					else
					{
						MessageBox.Show("Необходимо указать лицевой счет!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				catch (Exception exception)
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

		private void dtDate_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numAmount.Focus();
			}
		}

		private void frmChangeCharge_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmChangeCharge_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				this.dtDate.Value = DateTime.Today.Date;
			}
			catch
			{
				base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
				base.Close();
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmDocCreditUsl));
			this.groupBox2 = new GroupBox();
			this.lblIskSaldo = new Label();
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.cmdOK = new Button();
			this.cmdClose = new Button();
			this.numAmount = new NumericUpDown();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.numCountMonth = new NumericUpDown();
			this.label18 = new Label();
			this.lblAmountByMonth = new Label();
			this.label7 = new Label();
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.groupBox1 = new GroupBox();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtAccount).BeginInit();
			((ISupportInitialize)this.numAmount).BeginInit();
			((ISupportInitialize)this.numCountMonth).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.lblIskSaldo);
			this.groupBox2.Controls.Add(this.txtAccount);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(6, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(386, 96);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.lblIskSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblIskSaldo.ForeColor = Color.Red;
			this.lblIskSaldo.Location = new Point(184, 16);
			this.lblIskSaldo.Name = "lblIskSaldo";
			this.lblIskSaldo.Size = new System.Drawing.Size(192, 20);
			this.lblIskSaldo.TabIndex = 17;
			this.txtAccount.BorderStyle = 1;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(48, 16);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(96, 21);
			this.txtAccount.TabIndex = 1;
			this.txtAccount.Tag = null;
			this.txtAccount.KeyUp += new KeyEventHandler(this.txtAccount_KeyUp);
			this.txtAccount.Enter += new EventHandler(this.txtAccount_Enter);
			this.txtAccount.Leave += new EventHandler(this.txtAccount_Leave);
			this.cmdAccount.FlatStyle = FlatStyle.Flat;
			this.cmdAccount.ForeColor = SystemColors.ControlText;
			this.cmdAccount.ImageIndex = 0;
			this.cmdAccount.ImageList = this.imageList1;
			this.cmdAccount.Location = new Point(152, 16);
			this.cmdAccount.Name = "cmdAccount";
			this.cmdAccount.Size = new System.Drawing.Size(20, 20);
			this.cmdAccount.TabIndex = 2;
			this.cmdAccount.Click += new EventHandler(this.cmdAccount_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(48, 64);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(328, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(48, 40);
			this.lblFIO.Name = "lblFIO";
			this.lblFIO.Size = new System.Drawing.Size(328, 20);
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
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(192, 264);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(296, 264);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 4;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(120, 40);
			NumericUpDown num = this.numAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown = this.numAmount;
			numArray = new int[] { 9999999, 0, 0, -2147483648 };
			numericUpDown.Minimum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.Size = new System.Drawing.Size(112, 20);
			this.numAmount.TabIndex = 3;
			this.numAmount.KeyPress += new KeyPressEventHandler(this.numAmount_KeyPress);
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.numAmount.Leave += new EventHandler(this.numAmount_Leave);
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 104);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(368, 32);
			this.txtNote.TabIndex = 8;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.txtNote.Enter += new EventHandler(this.txtNote_Enter);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 88);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 84;
			this.label17.Text = "Примечание";
			this.numCountMonth.BorderStyle = BorderStyle.FixedSingle;
			this.numCountMonth.Location = new Point(120, 64);
			NumericUpDown num1 = this.numCountMonth;
			numArray = new int[] { 9999, 0, 0, 0 };
			num1.Maximum = new decimal(numArray);
			this.numCountMonth.Name = "numCountMonth";
			this.numCountMonth.Size = new System.Drawing.Size(112, 20);
			this.numCountMonth.TabIndex = 4;
			this.numCountMonth.KeyPress += new KeyPressEventHandler(this.numCountMonth_KeyPress);
			this.numCountMonth.Enter += new EventHandler(this.numCountMonth_Enter);
			this.numCountMonth.Leave += new EventHandler(this.numCountMonth_Leave);
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(4, 64);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(108, 16);
			this.label18.TabIndex = 76;
			this.label18.Text = "Срок:";
			this.lblAmountByMonth.BackColor = SystemColors.Info;
			this.lblAmountByMonth.BorderStyle = BorderStyle.FixedSingle;
			this.lblAmountByMonth.ForeColor = SystemColors.ControlText;
			this.lblAmountByMonth.Location = new Point(240, 40);
			this.lblAmountByMonth.Name = "lblAmountByMonth";
			this.lblAmountByMonth.Size = new System.Drawing.Size(136, 20);
			this.lblAmountByMonth.TabIndex = 74;
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(4, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 16);
			this.label7.TabIndex = 72;
			this.label7.Text = "Сумма";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(120, 16);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(256, 18);
			this.dtDate.TabIndex = 1;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.KeyPress += new KeyPressEventHandler(this.dtDate_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(4, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 16);
			this.label6.TabIndex = 68;
			this.label6.Text = "Дата документа";
			this.groupBox1.Controls.Add(this.numAmount);
			this.groupBox1.Controls.Add(this.txtNote);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.numCountMonth);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.lblAmountByMonth);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.dtDate);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(6, 112);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(386, 144);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Документ";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(394, 296);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximumSize = new System.Drawing.Size(400, 328);
			base.MinimumSize = new System.Drawing.Size(400, 328);
			base.Name = "frmDocCreditUsl";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Услуги в кредит";
			base.Closing += new CancelEventHandler(this.frmChangeCharge_Closing);
			base.Load += new EventHandler(this.frmChangeCharge_Load);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount).EndInit();
			((ISupportInitialize)this.numAmount).EndInit();
			((ISupportInitialize)this.numCountMonth).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void numAmount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmount);
		}

		private void numAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numCountMonth.Focus();
			}
		}

		private void numAmount_Leave(object sender, EventArgs e)
		{
			if (this.numAmount.Value > new decimal(0) && this.numCountMonth.Value > new decimal(0))
			{
				this.lblAmountByMonth.Text = Convert.ToString(Math.Round(this.numAmount.Value / this.numCountMonth.Value, 2));
			}
		}

		private void numCountMonth_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numCountMonth);
		}

		private void numCountMonth_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNote.Focus();
			}
		}

		private void numCountMonth_Leave(object sender, EventArgs e)
		{
			if (this.numAmount.Value > new decimal(0) && this.numCountMonth.Value > new decimal(0))
			{
				this.lblAmountByMonth.Text = Convert.ToString(Math.Round(this.numAmount.Value / this.numCountMonth.Value, 2));
			}
		}

		private void ResetFields1()
		{
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblAmountByMonth.Text = "";
			this.numCountMonth.Value = new decimal(0);
			this._contract = null;
			this._gobject = null;
			this._gmeter = null;
		}

		private void txtAccount_Enter(object sender, EventArgs e)
		{
			this.txtAccount.SelectAll();
		}

		private void txtAccount_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.cmdAccount_Click(null, null);
			}
		}

		private void txtAccount_Leave(object sender, EventArgs e)
		{
			this.cmdAccount_Click(null, null);
		}

		private void txtNote_Enter(object sender, EventArgs e)
		{
			this.txtNote.SelectAll();
		}

		private void txtNote_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdOK.Focus();
			}
		}
	}
}