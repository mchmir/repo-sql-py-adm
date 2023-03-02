using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPaymentDoc : Form
	{
		private IContainer components;

		private GroupBox groupBox3;

		private NumericUpDown numNewIndication;

		private NumericUpDown numAmount;

		private Label lblCurrentIndication;

		private Label label18;

		private Label label19;

		private Label label20;

		private GroupBox groupBox2;

		private Label lblCountLives;

		private Label lblPU;

		private Label label13;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private GroupBox groupBox1;

		private Label lblAgent;

		private Label label3;

		private Label label2;

		private Label label1;

		private ToolTip toolTip1;

		private Label lblAccount;

		private Label lblBatchCount;

		private Label lblBatchAmount;

		private Button cmdClose;

		private Button cmdOK;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private Contract _contract;

		private Indication _indication;

		private double FactAmount = 0;

		private PD _pd1;

		private Label lblBatchDate;

		private Label label5;

		private Document _doc;

		public frmPaymentDoc(Document oDocument)
		{
			this.InitializeComponent();
			this._doc = oDocument;
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
						this.numNewIndication_Leave(null, null);
						if (this.FactAmount < 0)
						{
							MessageBox.Show("Не верно указаны показания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							this.numNewIndication.Focus();
							return;
						}
						else if ((this.FactAmount > 10 || this.FactAmount == 0) && this.numNewIndication.Value > new decimal(0) && MessageBox.Show(string.Concat("Потребление составляет ", this.FactAmount.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
						{
							this.numNewIndication.Focus();
							return;
						}
						else
						{
							long d = this._doc.get_ID();
							long num = (long)0;
							if (this._indication != null)
							{
								num = this._indication.get_ID();
							}
							string documentNumber = this._doc.DocumentNumber;
							string note = this._doc.Note;
							bool flag = false;
							bool flag1 = false;
							this._doc.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
							if (this._doc.oBatch.oTypeBatch.get_ID() == (long)2)
							{
								flag1 = true;
							}
							double num1 = 0;
							if (!this._doc.DocumentPay(this._doc.oContract.oPerson.isJuridical, Depot.CurrentPeriod.get_ID(), this._doc.oBatch.get_ID(), this._doc.oContract.get_ID(), (long)1, this._doc.DocumentDate, Convert.ToDouble(this.numAmount.Value), Convert.ToDouble(this.numNewIndication.Value), flag1, SQLConnect.CurrentUser.get_ID(), ref d, ref num, ref this.FactAmount, ref documentNumber, ref note, ref flag, ref num1))
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								this._indication = null;
								this._doc.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
								base.DialogResult = System.Windows.Forms.DialogResult.OK;
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

		private void frmPaymentDoc_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmPaymentDoc_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				if (this._doc.oBatch.oTypeBatch.get_ID() != (long)1)
				{
					base.Close();
				}
				else if (this._doc.get_ID() != (long)0)
				{
					this.lblBatchCount.Text = Convert.ToString(this._doc.oBatch.BatchCount);
					this.lblBatchAmount.Text = Convert.ToString(this._doc.oBatch.BatchAmount);
					if (this._doc.oBatch.oDispatcher == null)
					{
						this.lblAgent.Text = "нет";
					}
					else
					{
						this.lblAgent.Text = this._doc.oBatch.oDispatcher.get_Name();
					}
					Label shortDateString = this.lblBatchDate;
					DateTime batchDate = this._doc.oBatch.BatchDate;
					shortDateString.Text = batchDate.ToShortDateString();
					this._contract = this._doc.oContract;
					this.lblAccount.Text = this._contract.Account;
					this.lblFIO.Text = this._contract.oPerson.FullName;
					this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
					Label str = this.lblCountLives;
					int countLives = this._contract.oGobjects[0].CountLives;
					str.Text = countLives.ToString();
					this._gobject = this._contract.oGobjects[0];
					this._gmeter = this._gobject.GetActiveGMeter();
					if (this._gmeter != null)
					{
						this.lblPU.Text = "Подключен";
						Label label = this.lblCurrentIndication;
						string str1 = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
						batchDate = this._gmeter.GetCurrentIndication().Datedisplay;
						label.Text = string.Concat(str1, " от ", batchDate.ToShortDateString());
						if (this._doc.GetPD(1) != null)
						{
							this._pd1 = this._doc.GetPD(1);
							this._indication = this._gmeter.oIndications.Add();
							this._indication.Load(Convert.ToInt64(this._pd1.Value));
							this.numNewIndication.Value = Convert.ToDecimal(this._indication.Display);
						}
						this.numNewIndication.Enabled = true;
					}
					else
					{
						this.lblPU.Text = "Не подключен";
						this._indication = null;
					}
					this.numAmount.Value = Convert.ToDecimal(this._doc.DocumentAmount);
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBox3 = new GroupBox();
			this.numNewIndication = new NumericUpDown();
			this.numAmount = new NumericUpDown();
			this.lblCurrentIndication = new Label();
			this.label18 = new Label();
			this.label19 = new Label();
			this.label20 = new Label();
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblCountLives = new Label();
			this.lblPU = new Label();
			this.label13 = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.groupBox1 = new GroupBox();
			this.lblBatchDate = new Label();
			this.label5 = new Label();
			this.lblBatchAmount = new Label();
			this.lblBatchCount = new Label();
			this.lblAgent = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.numNewIndication).BeginInit();
			((ISupportInitialize)this.numAmount).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox3.Controls.Add(this.numNewIndication);
			this.groupBox3.Controls.Add(this.numAmount);
			this.groupBox3.Controls.Add(this.lblCurrentIndication);
			this.groupBox3.Controls.Add(this.label18);
			this.groupBox3.Controls.Add(this.label19);
			this.groupBox3.Controls.Add(this.label20);
			this.groupBox3.ForeColor = SystemColors.Desktop;
			this.groupBox3.Location = new Point(4, 248);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(288, 96);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Платеж";
			this.numNewIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numNewIndication.DecimalPlaces = 2;
			this.numNewIndication.Enabled = false;
			this.numNewIndication.Location = new Point(160, 64);
			NumericUpDown num = this.numNewIndication;
			int[] numArray = new int[] { 9999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numNewIndication.Name = "numNewIndication";
			this.numNewIndication.TabIndex = 2;
			this.numNewIndication.KeyPress += new KeyPressEventHandler(this.numNewIndication_KeyPress);
			this.numNewIndication.Enter += new EventHandler(this.numNewIndication_Enter);
			this.numNewIndication.Leave += new EventHandler(this.numNewIndication_Leave);
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(160, 16);
			NumericUpDown numericUpDown = this.numAmount;
			numArray = new int[] { 999999, 0, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.TabIndex = 1;
			this.numAmount.KeyPress += new KeyPressEventHandler(this.numAmount_KeyPress);
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.lblCurrentIndication.BackColor = SystemColors.Info;
			this.lblCurrentIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblCurrentIndication.ForeColor = SystemColors.ControlText;
			this.lblCurrentIndication.Location = new Point(128, 40);
			this.lblCurrentIndication.Name = "lblCurrentIndication";
			this.lblCurrentIndication.Size = new System.Drawing.Size(152, 20);
			this.lblCurrentIndication.TabIndex = 2;
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(8, 64);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(120, 16);
			this.label18.TabIndex = 2;
			this.label18.Text = "Новые показания";
			this.label19.ForeColor = SystemColors.ControlText;
			this.label19.Location = new Point(8, 40);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(128, 16);
			this.label19.TabIndex = 1;
			this.label19.Text = "Текущие показания";
			this.label20.ForeColor = SystemColors.ControlText;
			this.label20.Location = new Point(8, 16);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(96, 16);
			this.label20.TabIndex = 0;
			this.label20.Text = "Сумма, тенге";
			this.groupBox2.Controls.Add(this.lblAccount);
			this.groupBox2.Controls.Add(this.lblCountLives);
			this.groupBox2.Controls.Add(this.lblPU);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 128);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(288, 120);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.lblAccount.BackColor = SystemColors.Info;
			this.lblAccount.BorderStyle = BorderStyle.FixedSingle;
			this.lblAccount.ForeColor = SystemColors.ControlText;
			this.lblAccount.Location = new Point(56, 16);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(96, 20);
			this.lblAccount.TabIndex = 11;
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(80, 88);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 10;
			this.lblPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblPU.ForeColor = SystemColors.ControlText;
			this.lblPU.Location = new Point(160, 88);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(120, 20);
			this.lblPU.TabIndex = 3;
			this.toolTip1.SetToolTip(this.lblPU, "Статус ПУ");
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
			this.groupBox1.Controls.Add(this.lblBatchDate);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.lblBatchAmount);
			this.groupBox1.Controls.Add(this.lblBatchCount);
			this.groupBox1.Controls.Add(this.lblAgent);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(4, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(288, 120);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Информация по ведомости";
			this.lblBatchDate.BackColor = SystemColors.Info;
			this.lblBatchDate.BorderStyle = BorderStyle.FixedSingle;
			this.lblBatchDate.ForeColor = SystemColors.ControlText;
			this.lblBatchDate.Location = new Point(160, 88);
			this.lblBatchDate.Name = "lblBatchDate";
			this.lblBatchDate.Size = new System.Drawing.Size(120, 20);
			this.lblBatchDate.TabIndex = 10;
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 16);
			this.label5.TabIndex = 9;
			this.label5.Text = "Дата пачки";
			this.lblBatchAmount.BackColor = SystemColors.Info;
			this.lblBatchAmount.BorderStyle = BorderStyle.FixedSingle;
			this.lblBatchAmount.ForeColor = SystemColors.ControlText;
			this.lblBatchAmount.Location = new Point(160, 16);
			this.lblBatchAmount.Name = "lblBatchAmount";
			this.lblBatchAmount.Size = new System.Drawing.Size(120, 20);
			this.lblBatchAmount.TabIndex = 8;
			this.lblBatchCount.BackColor = SystemColors.Info;
			this.lblBatchCount.BorderStyle = BorderStyle.FixedSingle;
			this.lblBatchCount.ForeColor = SystemColors.ControlText;
			this.lblBatchCount.Location = new Point(160, 40);
			this.lblBatchCount.Name = "lblBatchCount";
			this.lblBatchCount.Size = new System.Drawing.Size(120, 20);
			this.lblBatchCount.TabIndex = 7;
			this.lblAgent.BackColor = SystemColors.Info;
			this.lblAgent.BorderStyle = BorderStyle.FixedSingle;
			this.lblAgent.ForeColor = SystemColors.ControlText;
			this.lblAgent.Location = new Point(96, 64);
			this.lblAgent.Name = "lblAgent";
			this.lblAgent.Size = new System.Drawing.Size(184, 20);
			this.lblAgent.TabIndex = 5;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Агент";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Количество платежей";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Сумма платежей";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(200, 352);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 3;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(96, 352);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(298, 383);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmPaymentDoc";
			this.Text = "Платеж (редактирование)";
			base.Closing += new CancelEventHandler(this.frmPaymentDoc_Closing);
			base.Load += new EventHandler(this.frmPaymentDoc_Load);
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.numNewIndication).EndInit();
			((ISupportInitialize)this.numAmount).EndInit();
			this.groupBox2.ResumeLayout(false);
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
				if (this.numNewIndication.Enabled)
				{
					this.numNewIndication.Focus();
					return;
				}
				this.cmdOK.Focus();
			}
		}

		private void numNewIndication_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numNewIndication);
		}

		private void numNewIndication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdOK.Focus();
			}
		}

		private void numNewIndication_Leave(object sender, EventArgs e)
		{
			this.FactAmount = -1;
			if (this.numNewIndication.Value > new decimal(0) && this._indication != null)
			{
				string str = "";
				try
				{
					this._indication.oGmeter = this._gmeter;
					this._indication.Datedisplay = this._doc.oBatch.BatchDate;
					this._indication.Display = Convert.ToDouble(this.numNewIndication.Value);
					str = this._indication.CalcFactUse();
					this.FactAmount = Convert.ToDouble(str);
				}
				catch
				{
					MessageBox.Show(str, "Показания прибора учета", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.numNewIndication.Value = new decimal(0);
					this.numNewIndication.Focus();
				}
			}
			if (this.numNewIndication.Value == new decimal(0))
			{
				this.FactAmount = 0;
			}
		}
	}
}