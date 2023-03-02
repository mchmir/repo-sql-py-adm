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
	public class frmChangeCharge : Form
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

		private Label lblBalance;

		private Label label9;

		private Label lblCurrentIndication;

		private Label label19;

		private GroupBox groupBox1;

		private C1DateEdit dtDate;

		private Label label6;

		private C1Combo cmbTariff;

		private Label label1;

		private Label label7;

		private Label lblNewBalance;

		private NumericUpDown numNewIndication;

		private Label label18;

		private CheckBox checkBox1;

		private Label label2;

		private Label label3;

		private Label label4;

		private TextBox txtNote;

		private Label label17;

		private Button cmdClose;

		private Button cmdOK;

		private IContainer components;

		private NumericUpDown numCub;

		private NumericUpDown numKg;

		private NumericUpDown numAmount;

		private Contract _contract;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private Tariffs _tariffs;

		private TypeCharges _TypeCharges;

		private Accountings _Accountings;

		private UslugiVDGOs _UslugiVDGOs;

		private Agents _agents5;

		private C1Combo CmbAccounting;

		private Label label5;

		private C1Combo CmbTypeCharge;

		private Label label8;

		private Label lblIskSaldo;

		private C1Combo cmbDopUslugi;

		private GroupBox groupBox3;

		private C1Combo cmbMechanic;

		private Button bCalcSumm;

		private Document _doc;

		private PD _pd13;

		private FactUse _fu;

		private Operation _operation;

		private Balance _balance;

		private bool isEdit;

		public frmChangeCharge(Document oDocument)
		{
			this.InitializeComponent();
			this._doc = oDocument;
			this._contract = this._doc.oContract;
		}

		private void bCalcSumm_Click(object sender, EventArgs e)
		{
			this.numAmount.Value = Math.Abs(Convert.ToDecimal(this.numCub.Value)) * Convert.ToDecimal(this.cmbTariff.Text);
		}

		private void checkBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numCub.Focus();
			}
		}

		private void CmbAccounting_Change(object sender, EventArgs e)
		{
		}

		private void CmbAccounting_TextChanged(object sender, EventArgs e)
		{
			if (this._contract != null)
			{
				if (this._contract.CurrentBalance(this._Accountings[this.CmbAccounting.SelectedIndex]) != null)
				{
					this.lblBalance.Text = Convert.ToString(Math.Round(this._contract.CurrentBalance(this._Accountings[this.CmbAccounting.SelectedIndex]).AmountBalance, 2));
					return;
				}
				this.lblBalance.Text = "0";
			}
		}

		private void cmbTariff_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numAmount.Focus();
			}
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
						this.lblBalance.Text = Convert.ToString(Math.Round(this._contract.CurrentBalance(), 2));
						try
						{
							if (this._contract.CurrentBalance(this._Accountings.item((long)2)).AmountBalance * -1 <= 0)
							{
								this.lblIskSaldo.Text = "";
							}
							else
							{
								this.lblIskSaldo.Text = string.Concat("Исковое сальдо - ", Convert.ToString(Math.Round(this._contract.CurrentBalance(this._Accountings.item((long)2)).AmountBalance * -1, 2)));
							}
						}
						catch
						{
						}
						this._gobject = this._contract.oGobjects[0];
						this._gmeter = this._gobject.GetActiveGMeter();
						if (this._gmeter == null)
						{
							this.lblCurrentIndication.Text = "";
						}
						else
						{
							Label label = this.lblCurrentIndication;
							string str = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
							DateTime datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
							label.Text = string.Concat(str, " от ", datedisplay.ToShortDateString());
							this.numNewIndication.Enabled = true;
						}
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
			long d;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract == null || this.cmbTariff.SelectedIndex == -1)
					{
						MessageBox.Show("Необходимо указать лицевой счет и тариф!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					else
					{
						if (this._doc.get_ID() == (long)0)
						{
							this._doc.oBatch = null;
							this._doc.oContract = this._contract;
							this._doc.oPeriod = Depot.CurrentPeriod;
							this._doc.oTypeDocument = Depot.oTypeDocuments.item((long)7);
							this._doc.DocumentAmount = Math.Round(Convert.ToDouble(this.numAmount.Value), 6);
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
								str.oTypePD = Depot.oTypePDs.item((long)35);
								str.oDocument = this._doc;
								if (this._Accountings[this.CmbAccounting.SelectedIndex].get_ID() != (long)6)
								{
									str.Value = "0";
								}
								else
								{
									str.Value = Convert.ToString(this._UslugiVDGOs[this.cmbDopUslugi.SelectedIndex].get_ID());
								}
								if (str.Save() == 0)
								{
									str = this._doc.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)16);
									str.oDocument = this._doc;
									if (this._Accountings[this.CmbAccounting.SelectedIndex].get_ID() != (long)6)
									{
										str.Value = "0";
									}
									else
									{
										d = this._agents5[this.cmbMechanic.SelectedIndex].get_ID();
										str.Value = d.ToString();
									}
									if (str.Save() == 0)
									{
										str = this._doc.oPDs.Add();
										str.oTypePD = Depot.oTypePDs.item((long)13);
										str.oDocument = this._doc;
										str.Value = this.cmbTariff.Text;
										if (str.Save() == 0)
										{
											PD pD = this._doc.oPDs.Add();
											pD.oTypePD = Depot.oTypePDs.item((long)34);
											pD.oDocument = this._doc;
											d = this._TypeCharges[this.CmbTypeCharge.SelectedIndex].get_ID();
											pD.Value = d.ToString();
											if (pD.Save() == 0)
											{
												Indication indication = null;
												if (this.numNewIndication.Value > new decimal(0))
												{
													indication = new Indication()
													{
														Display = Convert.ToDouble(this.numNewIndication.Value),
														Datedisplay = (DateTime)this.dtDate.Value,
														oGmeter = this._gmeter
													};
													if (!this.checkBox1.Checked)
													{
														indication.oTypeIndication = Depot.oTypeIndications.item((long)2);
													}
													else
													{
														indication.oTypeIndication = Depot.oTypeIndications.item((long)3);
													}
													if (indication.Save(false) == 0)
													{
														PD str1 = this._doc.oPDs.Add();
														str1.oTypePD = Depot.oTypePDs.item((long)1);
														str1.oDocument = this._doc;
														str1.Value = indication.get_ID().ToString();
														if (str1.Save() != 0)
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
												Balance item = this._contract.CurrentBalance(this._Accountings[this.CmbAccounting.SelectedIndex]);
												if (item == null)
												{
													item = this._contract.oBalances.Add();
													item.oAccounting = this._Accountings[this.CmbAccounting.SelectedIndex];
													item.oPeriod = Depot.CurrentPeriod;
													item.oContract = this._contract;
													item.AmountBalance = 0;
													item.AmountCharge = 0;
													item.AmountPay = 0;
												}
												Balance amountBalance = item;
												amountBalance.AmountBalance = amountBalance.AmountBalance + Math.Round(Convert.ToDouble(this.numAmount.Value), 6);
												if (item.Save() == 0)
												{
													Operation operation = new Operation()
													{
														DateOperation = (DateTime)this.dtDate.Value,
														AmountOperation = Math.Round(Convert.ToDouble(this.numAmount.Value), 6),
														oBalance = item,
														oDocument = this._doc,
														oTypeOperation = Depot.oTypeOperations.item((long)3)
													};
													if (this._TypeCharges[this.CmbTypeCharge.SelectedIndex].get_ID() == (long)2)
													{
														operation.NumberOperation = 99999;
													}
													if (operation.Save() == 0)
													{
														if (this.numCub.Value != new decimal(0))
														{
															FactUse factUse = new FactUse()
															{
																oPeriod = Depot.CurrentPeriod,
																FactAmount = Convert.ToDouble(this.numCub.Value)
															};
															if (indication != null)
															{
																factUse.oIndication = indication;
															}
															factUse.oGobject = this._gobject;
															factUse.oDocument = this._doc;
															factUse.oTypeFU = Depot.oTypeFUs.item((long)1);
															factUse.oOperation = operation;
															if (factUse.Save() != 0)
															{
																this._doc.Delete();
																MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
																return;
															}
														}
														if (this.numKg.Value != new decimal(0))
														{
															FactUse factUse1 = new FactUse()
															{
																oPeriod = Depot.CurrentPeriod,
																FactAmount = Convert.ToDouble(this.numKg.Value)
															};
															if (indication != null)
															{
																factUse1.oIndication = indication;
															}
															factUse1.oGobject = this._gobject;
															factUse1.oDocument = this._doc;
															factUse1.oTypeFU = Depot.oTypeFUs.item((long)2);
															factUse1.oOperation = operation;
															if (factUse1.Save() != 0)
															{
																this._doc.Delete();
																MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
																return;
															}
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
						else
						{
							this._pd13.Value = this.cmbTariff.Text;
							if (this._pd13.Save() == 0)
							{
								this._operation.AmountOperation = Math.Round(Convert.ToDouble(this.numAmount.Value), 6);
								if (this._operation.Save() == 0)
								{
									if (this._fu.oTypeFU.get_ID() != (long)1)
									{
										this._fu.FactAmount = Convert.ToDouble(this.numKg.Value);
									}
									else
									{
										this._fu.FactAmount = Convert.ToDouble(this.numCub.Value);
									}
									if (this._fu.Save() == 0)
									{
										Balance balance = this._balance;
										balance.AmountBalance = balance.AmountBalance - Math.Round(this._doc.DocumentAmount, 6);
										Balance amountBalance1 = this._balance;
										amountBalance1.AmountBalance = amountBalance1.AmountBalance + Math.Round(Convert.ToDouble(this.numAmount.Value), 6);
										if (this._balance.Save() == 0)
										{
											this._doc.DocumentAmount = Math.Round(Convert.ToDouble(this.numAmount.Value), 6);
											if (this._doc.Save() == 0)
											{
												base.DialogResult = System.Windows.Forms.DialogResult.OK;
												base.Close();
											}
											else
											{
												MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
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
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
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
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
						base.DialogResult = System.Windows.Forms.DialogResult.OK;
						base.Close();
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
				this.cmbTariff.Focus();
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
				this.isEdit = false;
				Tools.LoadWindows(this);
				this._tariffs = new Tariffs();
				this._tariffs.Load();
				Tools.FillC1(this._tariffs, this.cmbTariff, (long)0);
				this.cmbTariff.AddItem("0");
				this._TypeCharges = new TypeCharges();
				this._TypeCharges.Load();
				Tools.FillC1(this._TypeCharges, this.CmbTypeCharge, (long)1);
				this._Accountings = new Accountings();
				this._Accountings.Load();
				Tools.FillC1(this._Accountings, this.CmbAccounting, (long)0);
				this._UslugiVDGOs = new UslugiVDGOs();
				this._UslugiVDGOs.Load();
				Tools.FillC1(this._UslugiVDGOs, this.cmbDopUslugi, (long)0);
				this._agents5 = new Agents();
				this._agents5.Load(Depot.oTypeAgents.item((long)5));
				Tools.FillC1(this._agents5, this.cmbMechanic, (long)0);
				Tariffs tariff = new Tariffs();
				if (tariff.Load(Depot.CurrentPeriod) == 0 && tariff.get_Count() > 0)
				{
					this.cmbTariff.Text = Convert.ToString(tariff[0].Value);
				}
				C1DateEdit date = this.dtDate;
				DateTime today = DateTime.Today;
				date.Value = today.Date;
				if (this._doc.get_ID() > (long)0)
				{
					this.isEdit = true;
					this.dtDate.Value = this._doc.DocumentDate;
					this.numAmount.Value = Convert.ToDecimal(this._doc.DocumentAmount);
					this.txtNote.Text = this._doc.Note;
					if (this._doc.GetPD(13) == null)
					{
						this.cmbTariff.Text = "0";
					}
					else
					{
						this._pd13 = this._doc.GetPD(13);
						this.cmbTariff.Text = this._doc.GetPD(13).Value;
					}
					if (this._doc.GetPD(1) != null)
					{
						Indication indication = new Indication();
						indication.Load(Convert.ToInt64(this._doc.GetPD(1).Value));
						this.numNewIndication.Value = Convert.ToDecimal(indication.Display);
						if (indication.oTypeIndication.get_ID() != (long)3)
						{
							this.checkBox1.Checked = false;
						}
						else
						{
							this.checkBox1.Checked = true;
						}
					}
					if (this._doc.GetFactUseByType(Depot.oTypeFUs.item((long)1)) != null)
					{
						this.numCub.Value = Convert.ToDecimal(this._doc.GetFactUseByType(Depot.oTypeFUs.item((long)1)).FactAmount);
						this._fu = this._doc.GetFactUseByType(Depot.oTypeFUs.item((long)1));
						this.numKg.Enabled = false;
					}
					if (this._doc.GetFactUseByType(Depot.oTypeFUs.item((long)2)) != null)
					{
						this.numKg.Value = Convert.ToDecimal(this._doc.GetFactUseByType(Depot.oTypeFUs.item((long)2)).FactAmount);
						this._fu = this._doc.GetFactUseByType(Depot.oTypeFUs.item((long)2));
						this.numCub.Enabled = false;
					}
					if (this._doc.GetPD(34) != null)
					{
						TypeCharge typeCharge = new TypeCharge();
						typeCharge.Load(Convert.ToInt64(this._doc.GetPD(34).Value));
						this.CmbTypeCharge.Text = typeCharge.get_Name();
					}
					this.CmbAccounting.Text = this._doc.oOperations[0].oBalance.oAccounting.get_Name();
					this._operation = this._doc.oOperations[0];
					this._balance = this._doc.oOperations[0].oBalance;
					this.CmbAccounting.Enabled = false;
					this.CmbTypeCharge.Enabled = false;
					this.numNewIndication.Enabled = false;
					this.dtDate.Enabled = false;
					this.checkBox1.Enabled = false;
					this.numNewIndication.Enabled = false;
					this.cmbDopUslugi.Enabled = false;
					this.cmbMechanic.Enabled = false;
					if (this._doc.oContract != null)
					{
						this.txtAccount.Text = this._doc.oContract.Account;
						this.lblFIO.Text = this._doc.oContract.oPerson.FullName;
						this.lblAddress.Text = this._doc.oContract.oPerson.oAddress.get_ShortAddress();
						this.lblBalance.Text = Convert.ToString(Math.Round(this._doc.oContract.CurrentBalance(), 2));
						this._gobject = this._doc.oContract.oGobjects[0];
						this._gmeter = this._gobject.GetActiveGMeter();
						if (this._gmeter == null)
						{
							this.lblCurrentIndication.Text = "";
						}
						else
						{
							Label label = this.lblCurrentIndication;
							string str = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
							today = this._gmeter.GetCurrentIndication().Datedisplay;
							label.Text = string.Concat(str, " от ", today.ToShortDateString());
						}
						this.txtAccount.ReadOnly = true;
						this.cmdAccount.Enabled = false;
					}
				}
			}
			catch
			{
				base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
				base.Close();
			}
		}

		private void groupBox2_Enter(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmChangeCharge));
			this.groupBox2 = new GroupBox();
			this.groupBox3 = new GroupBox();
			this.cmbMechanic = new C1Combo();
			this.cmbDopUslugi = new C1Combo();
			this.lblIskSaldo = new Label();
			this.CmbAccounting = new C1Combo();
			this.lblBalance = new Label();
			this.label9 = new Label();
			this.lblCurrentIndication = new Label();
			this.label19 = new Label();
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.label5 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.groupBox1 = new GroupBox();
			this.numAmount = new NumericUpDown();
			this.numKg = new NumericUpDown();
			this.numCub = new NumericUpDown();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.checkBox1 = new CheckBox();
			this.numNewIndication = new NumericUpDown();
			this.label18 = new Label();
			this.lblNewBalance = new Label();
			this.label7 = new Label();
			this.cmbTariff = new C1Combo();
			this.label1 = new Label();
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.CmbTypeCharge = new C1Combo();
			this.label8 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.bCalcSumm = new Button();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.cmbMechanic).BeginInit();
			((ISupportInitialize)this.cmbDopUslugi).BeginInit();
			((ISupportInitialize)this.CmbAccounting).BeginInit();
			((ISupportInitialize)this.txtAccount).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.numAmount).BeginInit();
			((ISupportInitialize)this.numKg).BeginInit();
			((ISupportInitialize)this.numCub).BeginInit();
			((ISupportInitialize)this.numNewIndication).BeginInit();
			((ISupportInitialize)this.cmbTariff).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.CmbTypeCharge).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.lblIskSaldo);
			this.groupBox2.Controls.Add(this.CmbAccounting);
			this.groupBox2.Controls.Add(this.lblBalance);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.lblCurrentIndication);
			this.groupBox2.Controls.Add(this.label19);
			this.groupBox2.Controls.Add(this.txtAccount);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(6, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(386, 192);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.groupBox2.Enter += new EventHandler(this.groupBox2_Enter);
			this.groupBox3.Controls.Add(this.cmbMechanic);
			this.groupBox3.Controls.Add(this.cmbDopUslugi);
			this.groupBox3.Location = new Point(8, 136);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(368, 48);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			this.cmbMechanic.AddItemSeparator = ';';
			this.cmbMechanic.BorderStyle = 1;
			this.cmbMechanic.Caption = "";
			this.cmbMechanic.CaptionHeight = 17;
			this.cmbMechanic.CharacterCasing = 0;
			this.cmbMechanic.ColumnCaptionHeight = 17;
			this.cmbMechanic.ColumnFooterHeight = 17;
			this.cmbMechanic.ColumnHeaders = false;
			this.cmbMechanic.ColumnWidth = 100;
			this.cmbMechanic.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbMechanic.ContentHeight = 15;
			this.cmbMechanic.DataMode = DataModeEnum.AddItem;
			this.cmbMechanic.DeadAreaBackColor = Color.Empty;
			this.cmbMechanic.EditorBackColor = SystemColors.Window;
			this.cmbMechanic.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbMechanic.EditorForeColor = SystemColors.WindowText;
			this.cmbMechanic.EditorHeight = 15;
			this.cmbMechanic.FlatStyle = FlatModeEnum.Flat;
			this.cmbMechanic.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbMechanic.ItemHeight = 15;
			this.cmbMechanic.Location = new Point(240, 16);
			this.cmbMechanic.MatchEntryTimeout = (long)2000;
			this.cmbMechanic.MaxDropDownItems = 5;
			this.cmbMechanic.MaxLength = 32767;
			this.cmbMechanic.MouseCursor = Cursors.Default;
			this.cmbMechanic.Name = "cmbMechanic";
			this.cmbMechanic.RowDivider.Color = Color.DarkGray;
			this.cmbMechanic.RowDivider.Style = LineStyleEnum.None;
			this.cmbMechanic.RowSubDividerColor = Color.DarkGray;
			this.cmbMechanic.Size = new System.Drawing.Size(120, 19);
			this.cmbMechanic.TabIndex = 93;
			this.cmbMechanic.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.cmbDopUslugi.AddItemSeparator = ';';
			this.cmbDopUslugi.BorderStyle = 1;
			this.cmbDopUslugi.Caption = "";
			this.cmbDopUslugi.CaptionHeight = 17;
			this.cmbDopUslugi.CharacterCasing = 0;
			this.cmbDopUslugi.ColumnCaptionHeight = 17;
			this.cmbDopUslugi.ColumnFooterHeight = 17;
			this.cmbDopUslugi.ColumnHeaders = false;
			this.cmbDopUslugi.ColumnWidth = 100;
			this.cmbDopUslugi.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbDopUslugi.ContentHeight = 15;
			this.cmbDopUslugi.DataMode = DataModeEnum.AddItem;
			this.cmbDopUslugi.DeadAreaBackColor = Color.Empty;
			this.cmbDopUslugi.EditorBackColor = SystemColors.Window;
			this.cmbDopUslugi.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbDopUslugi.EditorForeColor = SystemColors.WindowText;
			this.cmbDopUslugi.EditorHeight = 15;
			this.cmbDopUslugi.FlatStyle = FlatModeEnum.Flat;
			this.cmbDopUslugi.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbDopUslugi.ItemHeight = 15;
			this.cmbDopUslugi.Location = new Point(8, 16);
			this.cmbDopUslugi.MatchEntryTimeout = (long)2000;
			this.cmbDopUslugi.MaxDropDownItems = 5;
			this.cmbDopUslugi.MaxLength = 32767;
			this.cmbDopUslugi.MouseCursor = Cursors.Default;
			this.cmbDopUslugi.Name = "cmbDopUslugi";
			this.cmbDopUslugi.RowDivider.Color = Color.DarkGray;
			this.cmbDopUslugi.RowDivider.Style = LineStyleEnum.None;
			this.cmbDopUslugi.RowSubDividerColor = Color.DarkGray;
			this.cmbDopUslugi.Size = new System.Drawing.Size(224, 19);
			this.cmbDopUslugi.TabIndex = 18;
			this.cmbDopUslugi.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.lblIskSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblIskSaldo.ForeColor = Color.Red;
			this.lblIskSaldo.Location = new Point(184, 16);
			this.lblIskSaldo.Name = "lblIskSaldo";
			this.lblIskSaldo.Size = new System.Drawing.Size(192, 20);
			this.lblIskSaldo.TabIndex = 17;
			this.CmbAccounting.AddItemSeparator = ';';
			this.CmbAccounting.BorderStyle = 1;
			this.CmbAccounting.Caption = "";
			this.CmbAccounting.CaptionHeight = 17;
			this.CmbAccounting.CharacterCasing = 0;
			this.CmbAccounting.ColumnCaptionHeight = 17;
			this.CmbAccounting.ColumnFooterHeight = 17;
			this.CmbAccounting.ColumnHeaders = false;
			this.CmbAccounting.ColumnWidth = 100;
			this.CmbAccounting.ComboStyle = ComboStyleEnum.DropdownList;
			this.CmbAccounting.ContentHeight = 15;
			this.CmbAccounting.DataMode = DataModeEnum.AddItem;
			this.CmbAccounting.DeadAreaBackColor = Color.Empty;
			this.CmbAccounting.EditorBackColor = SystemColors.Window;
			this.CmbAccounting.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.CmbAccounting.EditorForeColor = SystemColors.WindowText;
			this.CmbAccounting.EditorHeight = 15;
			this.CmbAccounting.FlatStyle = FlatModeEnum.Flat;
			this.CmbAccounting.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.CmbAccounting.ItemHeight = 15;
			this.CmbAccounting.Location = new Point(248, 88);
			this.CmbAccounting.MatchEntryTimeout = (long)2000;
			this.CmbAccounting.MaxDropDownItems = 5;
			this.CmbAccounting.MaxLength = 32767;
			this.CmbAccounting.MouseCursor = Cursors.Default;
			this.CmbAccounting.Name = "CmbAccounting";
			this.CmbAccounting.RowDivider.Color = Color.DarkGray;
			this.CmbAccounting.RowDivider.Style = LineStyleEnum.None;
			this.CmbAccounting.RowSubDividerColor = Color.DarkGray;
			this.CmbAccounting.Size = new System.Drawing.Size(128, 19);
			this.CmbAccounting.TabIndex = 16;
			this.CmbAccounting.TextChanged += new EventHandler(this.CmbAccounting_TextChanged);
			this.CmbAccounting.Change += new ChangeEventHandler(this.CmbAccounting_Change);
			this.CmbAccounting.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.lblBalance.BackColor = SystemColors.Info;
			this.lblBalance.BorderStyle = BorderStyle.FixedSingle;
			this.lblBalance.ForeColor = SystemColors.ControlText;
			this.lblBalance.Location = new Point(88, 88);
			this.lblBalance.Name = "lblBalance";
			this.lblBalance.Size = new System.Drawing.Size(120, 20);
			this.lblBalance.TabIndex = 15;
			this.label9.ForeColor = SystemColors.ControlText;
			this.label9.Location = new Point(8, 88);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 16);
			this.label9.TabIndex = 14;
			this.label9.Text = "Тек-ее сальдо";
			this.lblCurrentIndication.BackColor = SystemColors.Info;
			this.lblCurrentIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblCurrentIndication.ForeColor = SystemColors.ControlText;
			this.lblCurrentIndication.Location = new Point(88, 112);
			this.lblCurrentIndication.Name = "lblCurrentIndication";
			this.lblCurrentIndication.Size = new System.Drawing.Size(120, 20);
			this.lblCurrentIndication.TabIndex = 13;
			this.label19.ForeColor = SystemColors.ControlText;
			this.label19.Location = new Point(8, 112);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(80, 16);
			this.label19.TabIndex = 12;
			this.label19.Text = "Тек-ие пок-ия";
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
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(216, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 16);
			this.label5.TabIndex = 14;
			this.label5.Text = "Вид сальдо";
			this.groupBox1.Controls.Add(this.bCalcSumm);
			this.groupBox1.Controls.Add(this.numAmount);
			this.groupBox1.Controls.Add(this.numKg);
			this.groupBox1.Controls.Add(this.numCub);
			this.groupBox1.Controls.Add(this.txtNote);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.numNewIndication);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.lblNewBalance);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.cmbTariff);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtDate);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.CmbTypeCharge);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(6, 208);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(386, 216);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Документ";
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(120, 88);
			NumericUpDown num = this.numAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown = this.numAmount;
			numArray = new int[] { 9999999, 0, 0, -2147483648 };
			numericUpDown.Minimum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.Size = new System.Drawing.Size(109, 20);
			this.numAmount.TabIndex = 3;
			this.numAmount.KeyPress += new KeyPressEventHandler(this.numAmount_KeyPress);
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.numKg.BorderStyle = BorderStyle.FixedSingle;
			this.numKg.DecimalPlaces = 4;
			this.numKg.Location = new Point(256, 136);
			NumericUpDown num1 = this.numKg;
			numArray = new int[] { 9999999, 0, 0, 0 };
			num1.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown1 = this.numKg;
			numArray = new int[] { 9999999, 0, 0, -2147483648 };
			numericUpDown1.Minimum = new decimal(numArray);
			this.numKg.Name = "numKg";
			this.numKg.Size = new System.Drawing.Size(80, 20);
			this.numKg.TabIndex = 7;
			this.numKg.KeyPress += new KeyPressEventHandler(this.numKg_KeyPress);
			this.numKg.Enter += new EventHandler(this.numKg_Enter);
			this.numCub.BorderStyle = BorderStyle.FixedSingle;
			this.numCub.DecimalPlaces = 3;
			this.numCub.Location = new Point(120, 136);
			NumericUpDown num2 = this.numCub;
			numArray = new int[] { 9999999, 0, 0, 0 };
			num2.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown2 = this.numCub;
			numArray = new int[] { 9999999, 0, 0, -2147483648 };
			numericUpDown2.Minimum = new decimal(numArray);
			this.numCub.Name = "numCub";
			this.numCub.Size = new System.Drawing.Size(104, 20);
			this.numCub.TabIndex = 6;
			this.numCub.KeyPress += new KeyPressEventHandler(this.numCub_KeyPress);
			this.numCub.Enter += new EventHandler(this.numCub_Enter);
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 176);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(368, 32);
			this.txtNote.TabIndex = 8;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.txtNote.Enter += new EventHandler(this.txtNote_Enter);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(4, 160);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 84;
			this.label17.Text = "Примечание";
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(344, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 82;
			this.label4.Text = "кг/м3";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(232, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 16);
			this.label3.TabIndex = 81;
			this.label3.Text = "м3";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(4, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 78;
			this.label2.Text = "Потребление";
			this.checkBox1.FlatStyle = FlatStyle.Flat;
			this.checkBox1.ForeColor = SystemColors.ControlText;
			this.checkBox1.Location = new Point(256, 112);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(120, 24);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "На основании акта";
			this.checkBox1.KeyPress += new KeyPressEventHandler(this.checkBox1_KeyPress);
			this.numNewIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numNewIndication.DecimalPlaces = 3;
			this.numNewIndication.Enabled = false;
			this.numNewIndication.Location = new Point(120, 112);
			NumericUpDown num3 = this.numNewIndication;
			numArray = new int[] { 9999, 0, 0, 0 };
			num3.Maximum = new decimal(numArray);
			this.numNewIndication.Name = "numNewIndication";
			this.numNewIndication.Size = new System.Drawing.Size(128, 20);
			this.numNewIndication.TabIndex = 4;
			this.numNewIndication.KeyPress += new KeyPressEventHandler(this.numNewIndication_KeyPress);
			this.numNewIndication.Enter += new EventHandler(this.numNewIndication_Enter);
			this.numNewIndication.Leave += new EventHandler(this.numNewIndication_Leave);
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(4, 112);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(108, 16);
			this.label18.TabIndex = 76;
			this.label18.Text = "Новые показания";
			this.lblNewBalance.BackColor = SystemColors.Info;
			this.lblNewBalance.BorderStyle = BorderStyle.FixedSingle;
			this.lblNewBalance.ForeColor = SystemColors.ControlText;
			this.lblNewBalance.Location = new Point(256, 88);
			this.lblNewBalance.Name = "lblNewBalance";
			this.lblNewBalance.Size = new System.Drawing.Size(120, 20);
			this.lblNewBalance.TabIndex = 74;
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(4, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 16);
			this.label7.TabIndex = 72;
			this.label7.Text = "Сумма";
			this.cmbTariff.AddItemSeparator = ';';
			this.cmbTariff.BorderStyle = 1;
			this.cmbTariff.Caption = "";
			this.cmbTariff.CaptionHeight = 17;
			this.cmbTariff.CharacterCasing = 0;
			this.cmbTariff.ColumnCaptionHeight = 17;
			this.cmbTariff.ColumnFooterHeight = 17;
			this.cmbTariff.ColumnHeaders = false;
			this.cmbTariff.ColumnWidth = 100;
			this.cmbTariff.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTariff.ContentHeight = 15;
			this.cmbTariff.DataMode = DataModeEnum.AddItem;
			this.cmbTariff.DeadAreaBackColor = Color.Empty;
			this.cmbTariff.EditorBackColor = SystemColors.Window;
			this.cmbTariff.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTariff.EditorForeColor = SystemColors.WindowText;
			this.cmbTariff.EditorHeight = 15;
			this.cmbTariff.FlatStyle = FlatModeEnum.Flat;
			this.cmbTariff.Images.Add((Image)resourceManager.GetObject("resource3"));
			this.cmbTariff.ItemHeight = 15;
			this.cmbTariff.Location = new Point(120, 40);
			this.cmbTariff.MatchEntryTimeout = (long)2000;
			this.cmbTariff.MaxDropDownItems = 5;
			this.cmbTariff.MaxLength = 32767;
			this.cmbTariff.MouseCursor = Cursors.Default;
			this.cmbTariff.Name = "cmbTariff";
			this.cmbTariff.RowDivider.Color = Color.DarkGray;
			this.cmbTariff.RowDivider.Style = LineStyleEnum.None;
			this.cmbTariff.RowSubDividerColor = Color.DarkGray;
			this.cmbTariff.Size = new System.Drawing.Size(256, 19);
			this.cmbTariff.TabIndex = 2;
			this.cmbTariff.KeyPress += new KeyPressEventHandler(this.cmbTariff_KeyPress);
			this.cmbTariff.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 71;
			this.label1.Text = "Тариф";
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
			this.CmbTypeCharge.AddItemSeparator = ';';
			this.CmbTypeCharge.BorderStyle = 1;
			this.CmbTypeCharge.Caption = "";
			this.CmbTypeCharge.CaptionHeight = 17;
			this.CmbTypeCharge.CharacterCasing = 0;
			this.CmbTypeCharge.ColumnCaptionHeight = 17;
			this.CmbTypeCharge.ColumnFooterHeight = 17;
			this.CmbTypeCharge.ColumnHeaders = false;
			this.CmbTypeCharge.ColumnWidth = 100;
			this.CmbTypeCharge.ComboStyle = ComboStyleEnum.DropdownList;
			this.CmbTypeCharge.ContentHeight = 15;
			this.CmbTypeCharge.DataMode = DataModeEnum.AddItem;
			this.CmbTypeCharge.DeadAreaBackColor = Color.Empty;
			this.CmbTypeCharge.EditorBackColor = SystemColors.Window;
			this.CmbTypeCharge.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.CmbTypeCharge.EditorForeColor = SystemColors.WindowText;
			this.CmbTypeCharge.EditorHeight = 15;
			this.CmbTypeCharge.FlatStyle = FlatModeEnum.Flat;
			this.CmbTypeCharge.Images.Add((Image)resourceManager.GetObject("resource4"));
			this.CmbTypeCharge.ItemHeight = 15;
			this.CmbTypeCharge.Location = new Point(120, 64);
			this.CmbTypeCharge.MatchEntryTimeout = (long)2000;
			this.CmbTypeCharge.MaxDropDownItems = 5;
			this.CmbTypeCharge.MaxLength = 32767;
			this.CmbTypeCharge.MouseCursor = Cursors.Default;
			this.CmbTypeCharge.Name = "CmbTypeCharge";
			this.CmbTypeCharge.RowDivider.Color = Color.DarkGray;
			this.CmbTypeCharge.RowDivider.Style = LineStyleEnum.None;
			this.CmbTypeCharge.RowSubDividerColor = Color.DarkGray;
			this.CmbTypeCharge.Size = new System.Drawing.Size(256, 19);
			this.CmbTypeCharge.TabIndex = 2;
			this.CmbTypeCharge.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(8, 64);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104, 16);
			this.label8.TabIndex = 71;
			this.label8.Text = "Тип коректировки";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(296, 432);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 4;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(192, 432);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.bCalcSumm.ImageList = this.imageList1;
			this.bCalcSumm.Location = new Point(229, 88);
			this.bCalcSumm.Name = "bCalcSumm";
			this.bCalcSumm.Size = new System.Drawing.Size(20, 20);
			this.bCalcSumm.TabIndex = 85;
			this.bCalcSumm.Text = "∑";
			this.bCalcSumm.Click += new EventHandler(this.bCalcSumm_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(394, 464);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximumSize = new System.Drawing.Size(400, 496);
			base.MinimumSize = new System.Drawing.Size(400, 496);
			base.Name = "frmChangeCharge";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Корректировка начисления";
			base.Closing += new CancelEventHandler(this.frmChangeCharge_Closing);
			base.Load += new EventHandler(this.frmChangeCharge_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.cmbMechanic).EndInit();
			((ISupportInitialize)this.cmbDopUslugi).EndInit();
			((ISupportInitialize)this.CmbAccounting).EndInit();
			((ISupportInitialize)this.txtAccount).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.numAmount).EndInit();
			((ISupportInitialize)this.numKg).EndInit();
			((ISupportInitialize)this.numCub).EndInit();
			((ISupportInitialize)this.numNewIndication).EndInit();
			((ISupportInitialize)this.cmbTariff).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.CmbTypeCharge).EndInit();
			base.ResumeLayout(false);
		}

		private void numAmount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmount);
		}

		private void numAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!this.isEdit && e.KeyChar == '\r')
			{
				if (this.numNewIndication.Enabled)
				{
					this.numNewIndication.Focus();
					return;
				}
				this.checkBox1.Focus();
			}
		}

		private void numCub_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numCub);
		}

		private void numCub_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numKg.Focus();
			}
		}

		private void numKg_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numKg);
		}

		private void numKg_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNote.Focus();
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
				this.checkBox1.Focus();
			}
		}

		private void numNewIndication_Leave(object sender, EventArgs e)
		{
			if (!this.isEdit && this.numNewIndication.Value != new decimal(0))
			{
				this.numCub.Value = this.numNewIndication.Value - Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display);
			}
		}

		private void ResetFields1()
		{
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblBalance.Text = "";
			this.lblCurrentIndication.Text = "";
			this.lblIskSaldo.Text = "";
			this.numNewIndication.Value = new decimal(0);
			this.numNewIndication.Enabled = false;
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

		private void txtAmount_Leave(object sender, EventArgs e)
		{
			this.lblNewBalance.Text = Convert.ToString(this._contract.CurrentBalance() + Convert.ToDouble(this.numAmount.Value));
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