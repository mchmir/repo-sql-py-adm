using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmCarryPayment : Form
	{
		private GroupBox groupBox2;

		private C1TextBox txtAccount;

		private Button cmdAccount;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private ToolTip toolTip1;

		private ImageList imageList1;

		private GroupBox groupBox1;

		private Label label3;

		private Label label4;

		private Label label5;

		private Button cmdClose;

		private Button cmdOK;

		private C1DateEdit dtDate;

		private Label label6;

		private Label label7;

		private TextBox txtNote;

		private Label label17;

		private Button cmdAccount2;

		private C1TextBox txtAccount2;

		private Label lblAddress2;

		private Label lblFIO2;

		private IContainer components;

		private NumericUpDown numAmount;

		private Contract _contract;

		private Contract _contract2;

		private Document _doc1;

		private Document _doc2;

		private C1Combo cmbAccounting1;

		private C1Combo cmbAccounting2;

		private Accountings _acc;

		private Accounting _acc1;

		private Accounting _acc2;

		public frmCarryPayment(Document oDocument1, Document oDocument2)
		{
			this.InitializeComponent();
			this._doc1 = oDocument1;
			this._doc2 = oDocument2;
		}

		private void cmbAccounting1_TextChanged(object sender, EventArgs e)
		{
			this._acc1 = new Accounting();
			this._acc1 = this._acc[this.cmbAccounting1.SelectedIndex];
		}

		private void cmbAccounting2_TextChanged(object sender, EventArgs e)
		{
			this._acc2 = new Accounting();
			this._acc2 = this._acc[this.cmbAccounting2.SelectedIndex];
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
						this.lblFIO.Text = this._contract.oPerson.FullName;
						this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
						if (this._contract2 == null)
						{
							this.cmbAccounting1.Enabled = false;
							this.cmbAccounting2.Enabled = false;
						}
						else if (this._contract.get_ID() != this._contract2.get_ID())
						{
							this.cmbAccounting1.Enabled = false;
							this.cmbAccounting2.Enabled = false;
						}
						else
						{
							this.cmbAccounting1.Enabled = true;
							this.cmbAccounting2.Enabled = true;
						}
						this.txtAccount2.Focus();
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

		private void cmdAccount2_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.txtAccount2.Text.Length != 0)
				{
					Contracts contract = new Contracts();
					if (contract.Load(this.txtAccount2.Text.Trim()) != 0)
					{
						this.ResetFields2();
					}
					else if (contract.get_Count() <= 0)
					{
						this.ResetFields2();
					}
					else
					{
						this._contract2 = contract[0];
						this.lblFIO2.Text = this._contract2.oPerson.FullName;
						this.lblAddress2.Text = this._contract2.oPerson.oAddress.get_ShortAddress();
						if (this._contract == null)
						{
							this.cmbAccounting1.Enabled = false;
							this.cmbAccounting2.Enabled = false;
						}
						else if (this._contract.get_ID() != this._contract2.get_ID())
						{
							this.cmbAccounting1.Enabled = false;
							this.cmbAccounting2.Enabled = false;
						}
						else
						{
							this.cmbAccounting1.Enabled = true;
							this.cmbAccounting2.Enabled = true;
						}
						this.numAmount.Focus();
					}
				}
				else
				{
					this.ResetFields2();
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
			long d;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract == null || this._contract2 == null)
					{
						return;
					}
					else
					{
						if (this._contract.get_ID() == this._contract2.get_ID())
						{
							if (this._acc1.get_ID() == this._acc2.get_ID())
							{
								MessageBox.Show("Типы сальдо должны быть разными!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
						else if (this._acc1.get_ID() > (long)1 || this._acc2.get_ID() > (long)1)
						{
							MessageBox.Show("Типы сальдо должны быть одинаковыми - Основной долг!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						if (this.numAmount.Value != new decimal(0))
						{
							if (this._contract.get_ID() != this._contract2.get_ID())
							{
								this._doc1.oBatch = null;
								this._doc1.oContract = this._contract;
								this._doc1.oPeriod = Depot.CurrentPeriod;
								this._doc1.oTypeDocument = Depot.oTypeDocuments.item((long)3);
								this._doc1.DocumentAmount = Math.Round(Convert.ToDouble(this.numAmount.Value) * -1, 6);
								this._doc1.DocumentDate = (DateTime)this.dtDate.Value;
								this._doc1.Note = this.txtNote.Text;
								if (this._doc1.Save() == 0)
								{
									this._doc2.oBatch = null;
									this._doc2.oContract = this._contract2;
									this._doc2.oPeriod = Depot.CurrentPeriod;
									this._doc2.oTypeDocument = Depot.oTypeDocuments.item((long)3);
									this._doc2.DocumentAmount = Math.Round(Convert.ToDouble(this.numAmount.Value), 6);
									this._doc2.DocumentDate = (DateTime)this.dtDate.Value;
									this._doc2.Note = this.txtNote.Text;
									if (this._doc2.Save() != 0)
									{
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
									else
									{
										PD str = this._doc2.oPDs.Add();
										str.oTypePD = Depot.oTypePDs.item((long)25);
										str.oDocument = this._doc2;
										d = this._doc1.get_ID();
										str.Value = d.ToString();
										if (str.Save() == 0)
										{
											str = this._doc1.oPDs.Add();
											str.oTypePD = Depot.oTypePDs.item((long)25);
											str.oDocument = this._doc1;
											d = this._doc2.get_ID();
											str.Value = d.ToString();
											if (str.Save() == 0)
											{
												Balance currentPeriod = this._contract.CurrentBalance(Depot.oAccountings.item((long)1));
												if (currentPeriod == null)
												{
													currentPeriod = this._contract.oBalances.Add();
													currentPeriod.oAccounting = Depot.oAccountings.item((long)1);
													currentPeriod.oPeriod = Depot.CurrentPeriod;
													currentPeriod.oContract = this._contract;
													currentPeriod.AmountBalance = 0;
													currentPeriod.AmountCharge = 0;
													currentPeriod.AmountPay = 0;
												}
												Balance amountBalance = currentPeriod;
												amountBalance.AmountBalance = amountBalance.AmountBalance + Math.Round(Convert.ToDouble(this.numAmount.Value) * -1, 6);
												if (currentPeriod.Save() == 0)
												{
													Operation operation = new Operation()
													{
														DateOperation = (DateTime)this.dtDate.Value,
														AmountOperation = Math.Round(Convert.ToDouble(this.numAmount.Value) * -1, 6),
														oBalance = currentPeriod,
														oDocument = this._doc1,
														oTypeOperation = Depot.oTypeOperations.item((long)3)
													};
													if (operation.Save() == 0)
													{
														bool flag = false;
														if (!this._doc2.DocumentCarryPay(Depot.CurrentPeriod.get_ID(), this._contract2.get_ID(), this._doc2.DocumentDate, Convert.ToDouble(this.numAmount.Value), this._doc2.get_ID(), ref flag))
														{
															this._doc1.Delete();
															this._doc2.Delete();
															MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
															return;
														}
													}
													else
													{
														this._doc1.Delete();
														this._doc2.Delete();
														MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
														return;
													}
												}
												else
												{
													this._doc1.Delete();
													this._doc2.Delete();
													MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
													return;
												}
											}
											else
											{
												this._doc1.Delete();
												MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
											}
										}
										else
										{
											this._doc2.Delete();
											MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
									}
								}
								else
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							else
							{
								this._doc1.oBatch = null;
								this._doc1.oContract = this._contract;
								this._doc1.oPeriod = Depot.CurrentPeriod;
								this._doc1.oTypeDocument = Depot.oTypeDocuments.item((long)3);
								this._doc1.DocumentAmount = Math.Round(Convert.ToDouble(this.numAmount.Value) * -1, 6);
								this._doc1.DocumentDate = (DateTime)this.dtDate.Value;
								this._doc1.Note = this.txtNote.Text;
								if (this._doc1.Save() == 0)
								{
									this._doc2.oBatch = null;
									this._doc2.oContract = this._contract2;
									this._doc2.oPeriod = Depot.CurrentPeriod;
									this._doc2.oTypeDocument = Depot.oTypeDocuments.item((long)3);
									this._doc2.DocumentAmount = Math.Round(Convert.ToDouble(this.numAmount.Value), 6);
									this._doc2.DocumentDate = (DateTime)this.dtDate.Value;
									this._doc2.Note = this.txtNote.Text;
									if (this._doc2.Save() != 0)
									{
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
									else
									{
										PD pD = this._doc2.oPDs.Add();
										pD.oTypePD = Depot.oTypePDs.item((long)25);
										pD.oDocument = this._doc2;
										d = this._doc1.get_ID();
										pD.Value = d.ToString();
										if (pD.Save() == 0)
										{
											pD = this._doc1.oPDs.Add();
											pD.oTypePD = Depot.oTypePDs.item((long)25);
											pD.oDocument = this._doc1;
											d = this._doc2.get_ID();
											pD.Value = d.ToString();
											if (pD.Save() == 0)
											{
												Balance balance = this._contract.CurrentBalance(Depot.oAccountings.item(this._acc1.get_ID()));
												if (balance == null)
												{
													balance = this._contract.oBalances.Add();
													balance.oAccounting = Depot.oAccountings.item(this._acc1.get_ID());
													balance.oPeriod = Depot.CurrentPeriod;
													balance.oContract = this._contract;
													balance.AmountBalance = 0;
													balance.AmountCharge = 0;
													balance.AmountPay = 0;
												}
												Balance amountBalance1 = balance;
												amountBalance1.AmountBalance = amountBalance1.AmountBalance + Math.Round(Convert.ToDouble(this.numAmount.Value) * -1, 6);
												if (balance.Save() == 0)
												{
													Operation operation1 = new Operation()
													{
														DateOperation = (DateTime)this.dtDate.Value,
														AmountOperation = Math.Round(Convert.ToDouble(this.numAmount.Value) * -1, 6),
														oBalance = balance,
														oDocument = this._doc1,
														oTypeOperation = Depot.oTypeOperations.item((long)3)
													};
													if (operation1.Save() == 0)
													{
														Balance currentPeriod1 = this._contract2.CurrentBalance(Depot.oAccountings.item(this._acc2.get_ID()));
														if (currentPeriod1 == null)
														{
															currentPeriod1 = this._contract2.oBalances.Add();
															currentPeriod1.oAccounting = Depot.oAccountings.item(this._acc2.get_ID());
															currentPeriod1.oPeriod = Depot.CurrentPeriod;
															currentPeriod1.oContract = this._contract2;
															currentPeriod1.AmountBalance = 0;
															currentPeriod1.AmountCharge = 0;
															currentPeriod1.AmountPay = 0;
														}
														Balance balance1 = currentPeriod1;
														balance1.AmountBalance = balance1.AmountBalance + Math.Round(Convert.ToDouble(this.numAmount.Value), 6);
														if (currentPeriod1.Save() == 0)
														{
															Operation operation2 = new Operation()
															{
																DateOperation = (DateTime)this.dtDate.Value,
																AmountOperation = Math.Round(Convert.ToDouble(this.numAmount.Value), 6),
																oBalance = currentPeriod1,
																oDocument = this._doc2,
																oTypeOperation = Depot.oTypeOperations.item((long)3)
															};
															if (operation2.Save() != 0)
															{
																this._doc1.Delete();
																this._doc2.Delete();
																MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
																return;
															}
														}
														else
														{
															this._doc1.Delete();
															this._doc2.Delete();
															MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
															return;
														}
													}
													else
													{
														this._doc1.Delete();
														this._doc2.Delete();
														MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
														return;
													}
												}
												else
												{
													this._doc1.Delete();
													this._doc2.Delete();
													MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
													return;
												}
											}
											else
											{
												this._doc1.Delete();
												MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
											}
										}
										else
										{
											this._doc2.Delete();
											MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
									}
								}
								else
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							base.DialogResult = System.Windows.Forms.DialogResult.OK;
							base.Close();
						}
						else
						{
							this.numAmount.Focus();
							return;
						}
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

		private void dtDate_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtAccount.Focus();
			}
		}

		private void frmCarryPayment_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmCarryPayment_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				this.dtDate.Value = DateTime.Today.Date;
				if (this._doc1.oContract != null)
				{
					this._contract = this._doc1.oContract;
					this.txtAccount.Text = this._doc1.oContract.Account;
					this.lblFIO.Text = this._contract.oPerson.FullName;
					this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
					this.txtAccount.Enabled = false;
					this.cmdAccount.Enabled = false;
				}
				this._acc = new Accountings();
				this._acc.Load();
				Tools.FillC1(this._acc, this.cmbAccounting1, (long)0);
				Tools.FillC1(this._acc, this.cmbAccounting2, (long)0);
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmCarryPayment));
			this.groupBox2 = new GroupBox();
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.groupBox1 = new GroupBox();
			this.txtAccount2 = new C1TextBox();
			this.cmdAccount2 = new Button();
			this.lblAddress2 = new Label();
			this.lblFIO2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.label7 = new Label();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.numAmount = new NumericUpDown();
			this.cmbAccounting1 = new C1Combo();
			this.cmbAccounting2 = new C1Combo();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtAccount).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.txtAccount2).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.numAmount).BeginInit();
			((ISupportInitialize)this.cmbAccounting1).BeginInit();
			((ISupportInitialize)this.cmbAccounting2).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.cmbAccounting1);
			this.groupBox2.Controls.Add(this.txtAccount);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(6, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 96);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "С лицевого счета";
			this.txtAccount.BorderStyle = 1;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(48, 16);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(96, 21);
			this.txtAccount.TabIndex = 1;
			this.txtAccount.Tag = null;
			this.txtAccount.KeyPress += new KeyPressEventHandler(this.txtAccount_KeyPress);
			this.txtAccount.Enter += new EventHandler(this.txtAccount_Enter);
			this.txtAccount.Leave += new EventHandler(this.txtAccount_Leave);
			this.cmdAccount.FlatStyle = FlatStyle.Flat;
			this.cmdAccount.ForeColor = SystemColors.ControlText;
			this.cmdAccount.ImageIndex = 0;
			this.cmdAccount.ImageList = this.imageList1;
			this.cmdAccount.Location = new Point(144, 16);
			this.cmdAccount.Name = "cmdAccount";
			this.cmdAccount.Size = new System.Drawing.Size(21, 21);
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
			this.lblAddress.Size = new System.Drawing.Size(224, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(48, 40);
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
			this.groupBox1.Controls.Add(this.cmbAccounting2);
			this.groupBox1.Controls.Add(this.txtAccount2);
			this.groupBox1.Controls.Add(this.cmdAccount2);
			this.groupBox1.Controls.Add(this.lblAddress2);
			this.groupBox1.Controls.Add(this.lblFIO2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(288, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(280, 96);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "На лицевой счет";
			this.txtAccount2.BorderStyle = 1;
			this.txtAccount2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount2.Location = new Point(48, 16);
			this.txtAccount2.Name = "txtAccount2";
			this.txtAccount2.Size = new System.Drawing.Size(96, 21);
			this.txtAccount2.TabIndex = 1;
			this.txtAccount2.Tag = null;
			this.txtAccount2.KeyPress += new KeyPressEventHandler(this.txtAccount2_KeyPress);
			this.txtAccount2.Enter += new EventHandler(this.txtAccount2_Enter);
			this.txtAccount2.Leave += new EventHandler(this.txtAccount2_Leave);
			this.cmdAccount2.FlatStyle = FlatStyle.Flat;
			this.cmdAccount2.ForeColor = SystemColors.ControlText;
			this.cmdAccount2.ImageIndex = 0;
			this.cmdAccount2.ImageList = this.imageList1;
			this.cmdAccount2.Location = new Point(144, 16);
			this.cmdAccount2.Name = "cmdAccount2";
			this.cmdAccount2.Size = new System.Drawing.Size(21, 21);
			this.cmdAccount2.TabIndex = 2;
			this.cmdAccount2.Click += new EventHandler(this.cmdAccount2_Click);
			this.lblAddress2.BackColor = SystemColors.Info;
			this.lblAddress2.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress2.ForeColor = SystemColors.ControlText;
			this.lblAddress2.Location = new Point(48, 64);
			this.lblAddress2.Name = "lblAddress2";
			this.lblAddress2.Size = new System.Drawing.Size(224, 20);
			this.lblAddress2.TabIndex = 5;
			this.lblFIO2.BackColor = SystemColors.Info;
			this.lblFIO2.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO2.ForeColor = SystemColors.ControlText;
			this.lblFIO2.Location = new Point(48, 40);
			this.lblFIO2.Name = "lblFIO2";
			this.lblFIO2.Size = new System.Drawing.Size(224, 20);
			this.lblFIO2.TabIndex = 4;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Адрес";
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 1;
			this.label4.Text = "ФИО";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Л/с";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(472, 224);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 7;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(368, 224);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 6;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(128, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 1;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.KeyPress += new KeyPressEventHandler(this.dtDate_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 16);
			this.label6.TabIndex = 66;
			this.label6.Text = "Дата документа";
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(288, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 16);
			this.label7.TabIndex = 64;
			this.label7.Text = "Сумма";
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 152);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(552, 64);
			this.txtNote.TabIndex = 5;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.txtNote.Enter += new EventHandler(this.txtNote_Enter);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 136);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 71;
			this.label17.Text = "Примечание";
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(336, 8);
			NumericUpDown num = this.numAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.Size = new System.Drawing.Size(128, 20);
			this.numAmount.TabIndex = 4;
			this.numAmount.KeyPress += new KeyPressEventHandler(this.numAmount_KeyPress);
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.cmbAccounting1.AddItemSeparator = ';';
			this.cmbAccounting1.BorderStyle = 1;
			this.cmbAccounting1.Caption = "";
			this.cmbAccounting1.CaptionHeight = 17;
			this.cmbAccounting1.CharacterCasing = 0;
			this.cmbAccounting1.ColumnCaptionHeight = 17;
			this.cmbAccounting1.ColumnFooterHeight = 17;
			this.cmbAccounting1.ColumnHeaders = false;
			this.cmbAccounting1.ColumnWidth = 100;
			this.cmbAccounting1.ContentHeight = 15;
			this.cmbAccounting1.DataMode = DataModeEnum.AddItem;
			this.cmbAccounting1.DeadAreaBackColor = Color.Empty;
			this.cmbAccounting1.EditorBackColor = SystemColors.Window;
			this.cmbAccounting1.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbAccounting1.EditorForeColor = SystemColors.WindowText;
			this.cmbAccounting1.EditorHeight = 15;
			this.cmbAccounting1.FlatStyle = FlatModeEnum.Popup;
			this.cmbAccounting1.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbAccounting1.ItemHeight = 15;
			this.cmbAccounting1.Location = new Point(168, 16);
			this.cmbAccounting1.MatchEntryTimeout = (long)2000;
			this.cmbAccounting1.MaxDropDownItems = 10;
			this.cmbAccounting1.MaxLength = 32767;
			this.cmbAccounting1.MouseCursor = Cursors.Default;
			this.cmbAccounting1.Name = "cmbAccounting1";
			this.cmbAccounting1.RowDivider.Color = Color.DarkGray;
			this.cmbAccounting1.RowDivider.Style = LineStyleEnum.None;
			this.cmbAccounting1.RowSubDividerColor = Color.DarkGray;
			this.cmbAccounting1.Size = new System.Drawing.Size(104, 19);
			this.cmbAccounting1.TabIndex = 47;
			this.cmbAccounting1.TextChanged += new EventHandler(this.cmbAccounting1_TextChanged);
			this.cmbAccounting1.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"C дома\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.cmbAccounting2.AddItemSeparator = ';';
			this.cmbAccounting2.BorderStyle = 1;
			this.cmbAccounting2.Caption = "";
			this.cmbAccounting2.CaptionHeight = 17;
			this.cmbAccounting2.CharacterCasing = 0;
			this.cmbAccounting2.ColumnCaptionHeight = 17;
			this.cmbAccounting2.ColumnFooterHeight = 17;
			this.cmbAccounting2.ColumnHeaders = false;
			this.cmbAccounting2.ColumnWidth = 100;
			this.cmbAccounting2.ContentHeight = 15;
			this.cmbAccounting2.DataMode = DataModeEnum.AddItem;
			this.cmbAccounting2.DeadAreaBackColor = Color.Empty;
			this.cmbAccounting2.EditorBackColor = SystemColors.Window;
			this.cmbAccounting2.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbAccounting2.EditorForeColor = SystemColors.WindowText;
			this.cmbAccounting2.EditorHeight = 15;
			this.cmbAccounting2.FlatStyle = FlatModeEnum.Popup;
			this.cmbAccounting2.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbAccounting2.ItemHeight = 15;
			this.cmbAccounting2.Location = new Point(168, 16);
			this.cmbAccounting2.MatchEntryTimeout = (long)2000;
			this.cmbAccounting2.MaxDropDownItems = 10;
			this.cmbAccounting2.MaxLength = 32767;
			this.cmbAccounting2.MouseCursor = Cursors.Default;
			this.cmbAccounting2.Name = "cmbAccounting2";
			this.cmbAccounting2.RowDivider.Color = Color.DarkGray;
			this.cmbAccounting2.RowDivider.Style = LineStyleEnum.None;
			this.cmbAccounting2.RowSubDividerColor = Color.DarkGray;
			this.cmbAccounting2.Size = new System.Drawing.Size(104, 19);
			this.cmbAccounting2.TabIndex = 48;
			this.cmbAccounting2.TextChanged += new EventHandler(this.cmbAccounting2_TextChanged);
			this.cmbAccounting2.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"C дома\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(570, 255);
			base.Controls.Add(this.numAmount);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximumSize = new System.Drawing.Size(576, 287);
			base.MinimumSize = new System.Drawing.Size(576, 287);
			base.Name = "frmCarryPayment";
			this.Text = "Перенос оплаты";
			base.Closing += new CancelEventHandler(this.frmCarryPayment_Closing);
			base.Load += new EventHandler(this.frmCarryPayment_Load);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount2).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.numAmount).EndInit();
			((ISupportInitialize)this.cmbAccounting1).EndInit();
			((ISupportInitialize)this.cmbAccounting2).EndInit();
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
				this.txtNote.Focus();
			}
		}

		private void ResetFields1()
		{
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this._contract = null;
		}

		private void ResetFields2()
		{
			this.txtAccount2.Text = "";
			this.lblFIO2.Text = "";
			this.lblAddress2.Text = "";
			this._contract2 = null;
		}

		private void txtAccount_Enter(object sender, EventArgs e)
		{
			this.txtAccount.SelectAll();
		}

		private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdAccount_Click(null, null);
			}
		}

		private void txtAccount_Leave(object sender, EventArgs e)
		{
			this.cmdAccount_Click(null, null);
		}

		private void txtAccount2_Enter(object sender, EventArgs e)
		{
			this.txtAccount2.SelectAll();
		}

		private void txtAccount2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdAccount2_Click(null, null);
			}
		}

		private void txtAccount2_Leave(object sender, EventArgs e)
		{
			this.cmdAccount2_Click(null, null);
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