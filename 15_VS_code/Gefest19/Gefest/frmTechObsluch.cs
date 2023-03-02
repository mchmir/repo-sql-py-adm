using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmTechObsluch : Form
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

		private Label lblBalance;

		private Label label9;

		private GroupBox groupBox1;

		private C1DateEdit dtDate;

		private Label label6;

		private Label label7;

		private TextBox txtNote;

		private Label label17;

		private Button cmdClose;

		private Button cmdOK;

		private IContainer components;

		private Contract _contract;

		private Gobject _gobject;

		private Accountings _Accountings;

		private Agents _agents;

		private Address _address;

		private ImageList imageList1;

		private Button cmdAddress;

		private NumericUpDown numNewIndication;

		private Label label18;

		private Label lblCurrentIndication;

		private Label label19;

		private Gmeter _gmeter;

		private NumericUpDown numCub;

		private Label label2;

		private Label label1;

		private C1Combo CmbUslugiVDGO;

		private UslugiVDGOs _UslugiVDGOs;

		private Label label3;

		private Label label4;

		private C1Combo cmbMechanic;

		private Label label5;

		private TextBox txtUslugiAmount;

		private Label lblTel;

		private Label label13;

		private Document _doc;

		private UslugiVDGO _usl;

		private Agent _ag;

		public frmTechObsluch(Document oDocument, UslugiVDGO UV, Agent AG)
		{
			this.InitializeComponent();
			this._doc = oDocument;
			this._usl = UV;
			this._ag = AG;
			this._contract = this._doc.oContract;
		}

		private void CmbAccounting_Change(object sender, EventArgs e)
		{
		}

		private void cmbMechanic_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numNewIndication.Focus();
			}
		}

		private void cmbTariff_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numNewIndication.Focus();
			}
		}

		private void CmbUslugiVDGO_Change(object sender, EventArgs e)
		{
		}

		private void CmbUslugiVDGO_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbMechanic.Focus();
			}
		}

		private void CmbUslugiVDGO_TextChanged(object sender, EventArgs e)
		{
			this.CreateUslugi();
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
						this.lblTel.Text = "";
						foreach (Phone oPhone in this._contract.oPerson.oPhones)
						{
							Label label = this.lblTel;
							label.Text = string.Concat(label.Text, oPhone.get_Name(), "; ");
						}
						this.lblBalance.Text = Convert.ToString(Math.Round(this._contract.CurrentBalance(), 2));
						this._gobject = this._contract.oGobjects[0];
						this._gmeter = this._gobject.GetActiveGMeter();
						if (this._gmeter == null)
						{
							this.lblCurrentIndication.Text = "";
						}
						else
						{
							Label label1 = this.lblCurrentIndication;
							string str = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
							DateTime datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
							label1.Text = string.Concat(str, " от ", datedisplay.ToShortDateString());
							if (!Tools.IsFirstWorkDay((DateTime)this.dtDate.Value, true, Depot.CurrentPeriod.DateBegin))
							{
								this.numNewIndication.Enabled = true;
							}
							else
							{
								this.numNewIndication.Enabled = false;
							}
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

		private void cmdAddress_Click(object sender, EventArgs e)
		{
			frmAddress _frmAddress = new frmAddress()
			{
				oAddress = this._address
			};
			this.ResetFields1();
			_frmAddress.ShowDialog(this);
			if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				Address address = _frmAddress.oAddress;
				Gobjects gobject = new Gobjects();
				if (gobject.Load(address) != 0)
				{
					return;
				}
				if (gobject.get_Count() <= 0)
				{
					return;
				}
				this._gobject = gobject[0];
				this._contract = this._gobject.oContract;
				this.txtAccount.Text = this._contract.Account;
				this._address = address;
				address = null;
				this.cmdAccount_Click(null, null);
			}
			_frmAddress = null;
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
						this._doc.oBatch = null;
						this._doc.oContract = this._contract;
						this._doc.oPeriod = Depot.CurrentPeriod;
						this._doc.oTypeDocument = Depot.oTypeDocuments.item((long)24);
						Document documentAmount = this._doc;
						documentAmount.DocumentAmount = documentAmount.DocumentAmount + Convert.ToDouble(this.txtUslugiAmount.Text);
						this._doc.DocumentDate = (DateTime)this.dtDate.Value;
						this._doc.Note = this.txtNote.Text;
						if (this._doc.Save() == 0)
						{
							PD str = this._doc.oPDs.Add();
							str.oTypePD = Depot.oTypePDs.item((long)16);
							str.oDocument = this._doc;
							long d = this._agents[this.cmbMechanic.SelectedIndex].get_ID();
							str.Value = d.ToString();
							if (str.Save() == 0)
							{
								str = this._doc.oPDs.Add();
								str.oTypePD = Depot.oTypePDs.item((long)35);
								str.oDocument = this._doc;
								str.Value = Convert.ToString(this._UslugiVDGOs[this.CmbUslugiVDGO.SelectedIndex].get_ID());
								if (str.Save() == 0)
								{
									Indication indication = null;
									if (this.numNewIndication.Value > new decimal(0))
									{
										indication = new Indication()
										{
											Display = Convert.ToDouble(this.numNewIndication.Value),
											Datedisplay = (DateTime)this.dtDate.Value,
											oGmeter = this._gmeter,
											oAgent = this._agents[this.cmbMechanic.SelectedIndex],
											oTypeIndication = Depot.oTypeIndications.item((long)3)
										};
										if (indication.Save(false) == 0)
										{
											str = this._doc.oPDs.Add();
											str.oTypePD = Depot.oTypePDs.item((long)1);
											str.oDocument = this._doc;
											str.Value = indication.get_ID().ToString();
											if (str.Save() != 0)
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
									this._Accountings = new Accountings();
									this._Accountings.Load();
									Balance item = this._contract.CurrentBalance(this._Accountings[5]);
									if (item == null)
									{
										item = this._contract.oBalances.Add();
										item.oAccounting = this._Accountings[5];
										item.oPeriod = Depot.CurrentPeriod;
										item.oContract = this._contract;
										item.AmountBalance = 0;
										item.AmountCharge = 0;
										item.AmountPay = 0;
									}
									Balance amountBalance = item;
									amountBalance.AmountBalance = amountBalance.AmountBalance - Convert.ToDouble(this.txtUslugiAmount.Text);
									if (item.Save() == 0)
									{
										Operation operation = new Operation()
										{
											DateOperation = (DateTime)this.dtDate.Value
										};
										Operation amountOperation = operation;
										amountOperation.AmountOperation = amountOperation.AmountOperation - Convert.ToDouble(this.txtUslugiAmount.Text);
										operation.oBalance = item;
										operation.oDocument = this._doc;
										operation.oTypeOperation = Depot.oTypeOperations.item((long)2);
										if (operation.Save() != 0)
										{
											this._doc.Delete();
											MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
										else if (this.numNewIndication.Value > new decimal(0))
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
											if (factUse.Save() != 0)
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
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
					}
					else
					{
						MessageBox.Show("Необходимо указать лицевой счет и тариф!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				catch (Exception exception)
				{
				}
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
				return;
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void CreateUslugi()
		{
			this.txtUslugiAmount.Text = Convert.ToString(this._UslugiVDGOs[this.CmbUslugiVDGO.SelectedIndex].Value);
			if (this._contract != null)
			{
				if (this._contract.oPerson.isJuridical == 1 && this._UslugiVDGOs[this.CmbUslugiVDGO.SelectedIndex].get_ID() == (long)11)
				{
					this.txtUslugiAmount.Text = Convert.ToString(this._contract.oPerson.CostDog);
				}
				this.txtNote.Text = this._UslugiVDGOs[this.CmbUslugiVDGO.SelectedIndex].get_Name();
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
				this.CmbUslugiVDGO.Focus();
			}
		}

		private void dtDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (!Tools.IsFirstWorkDay((DateTime)this.dtDate.Value, true, Depot.CurrentPeriod.DateBegin))
				{
					this.numNewIndication.Enabled = true;
				}
				else
				{
					this.numNewIndication.Enabled = false;
				}
			}
			catch
			{
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
				this._agents = new Agents();
				this._agents.Load(Depot.oTypeAgents.item((long)5));
				Tools.FillC1(this._agents, this.cmbMechanic, this._ag.get_ID());
				this._UslugiVDGOs = new UslugiVDGOs();
				this._UslugiVDGOs.Load();
				Tools.FillC1(this._UslugiVDGOs, this.CmbUslugiVDGO, this._usl.get_ID());
				this.txtNote.Text = this._usl.get_Name();
				if (this._doc == null)
				{
					this.dtDate.Value = DateTime.Today.Date;
				}
				else
				{
					this.dtDate.Value = this._doc.DocumentDate;
					if (this._doc.oContract != null)
					{
						this.txtAccount.Text = this._doc.oContract.Account;
						this.lblFIO.Text = this._doc.oContract.oPerson.FullName;
						this.lblAddress.Text = this._doc.oContract.oPerson.oAddress.get_ShortAddress();
						this.lblBalance.Text = Convert.ToString(Math.Round(this._doc.oContract.CurrentBalance(), 2));
						this._gobject = this._doc.oContract.oGobjects[0];
						this.txtAccount.ReadOnly = true;
						this.cmdAccount.Enabled = false;
					}
				}
			}
			catch
			{
			}
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{
		}

		private void groupBox2_Enter(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmTechObsluch));
			this.groupBox2 = new GroupBox();
			this.label13 = new Label();
			this.lblTel = new Label();
			this.lblCurrentIndication = new Label();
			this.label19 = new Label();
			this.cmdAddress = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblBalance = new Label();
			this.label9 = new Label();
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.groupBox1 = new GroupBox();
			this.txtUslugiAmount = new TextBox();
			this.cmbMechanic = new C1Combo();
			this.label5 = new Label();
			this.CmbUslugiVDGO = new C1Combo();
			this.label2 = new Label();
			this.numCub = new NumericUpDown();
			this.numNewIndication = new NumericUpDown();
			this.label18 = new Label();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.label7 = new Label();
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.label1 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtAccount).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmbMechanic).BeginInit();
			((ISupportInitialize)this.CmbUslugiVDGO).BeginInit();
			((ISupportInitialize)this.numCub).BeginInit();
			((ISupportInitialize)this.numNewIndication).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.lblTel);
			this.groupBox2.Controls.Add(this.lblCurrentIndication);
			this.groupBox2.Controls.Add(this.label19);
			this.groupBox2.Controls.Add(this.cmdAddress);
			this.groupBox2.Controls.Add(this.lblBalance);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.txtAccount);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(6, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(386, 140);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.groupBox2.Enter += new EventHandler(this.groupBox2_Enter);
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(240, 91);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(32, 23);
			this.label13.TabIndex = 20;
			this.label13.Text = "Тел.";
			this.lblTel.BackColor = SystemColors.Info;
			this.lblTel.BorderStyle = BorderStyle.FixedSingle;
			this.lblTel.ForeColor = SystemColors.ControlText;
			this.lblTel.Location = new Point(276, 88);
			this.lblTel.Name = "lblTel";
			this.lblTel.TabIndex = 19;
			this.lblCurrentIndication.BackColor = SystemColors.Info;
			this.lblCurrentIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblCurrentIndication.ForeColor = SystemColors.ControlText;
			this.lblCurrentIndication.Location = new Point(88, 112);
			this.lblCurrentIndication.Name = "lblCurrentIndication";
			this.lblCurrentIndication.Size = new System.Drawing.Size(136, 20);
			this.lblCurrentIndication.TabIndex = 18;
			this.label19.ForeColor = SystemColors.ControlText;
			this.label19.Location = new Point(8, 112);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(80, 16);
			this.label19.TabIndex = 17;
			this.label19.Text = "Тек-ие пок-ия";
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.ImageIndex = 1;
			this.cmdAddress.ImageList = this.imageList1;
			this.cmdAddress.Location = new Point(176, 16);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(20, 20);
			this.cmdAddress.TabIndex = 16;
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lblBalance.BackColor = SystemColors.Info;
			this.lblBalance.BorderStyle = BorderStyle.FixedSingle;
			this.lblBalance.ForeColor = SystemColors.ControlText;
			this.lblBalance.Location = new Point(88, 88);
			this.lblBalance.Name = "lblBalance";
			this.lblBalance.Size = new System.Drawing.Size(136, 20);
			this.lblBalance.TabIndex = 15;
			this.label9.ForeColor = SystemColors.ControlText;
			this.label9.Location = new Point(8, 88);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 16);
			this.label9.TabIndex = 14;
			this.label9.Text = "Тек-ее сальдо";
			this.txtAccount.BorderStyle = 1;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(48, 16);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(96, 21);
			this.txtAccount.TabIndex = 1;
			this.txtAccount.Tag = null;
			this.txtAccount.KeyUp += new KeyEventHandler(this.txtAccount_KeyUp);
			this.txtAccount.TextChanged += new EventHandler(this.txtAccount_TextChanged);
			this.txtAccount.KeyPress += new KeyPressEventHandler(this.txtAccount_KeyPress);
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
			this.groupBox1.Controls.Add(this.txtUslugiAmount);
			this.groupBox1.Controls.Add(this.cmbMechanic);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.CmbUslugiVDGO);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.numCub);
			this.groupBox1.Controls.Add(this.numNewIndication);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.txtNote);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.dtDate);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(4, 140);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(386, 244);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Документ";
			this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
			this.txtUslugiAmount.Location = new Point(103, 88);
			this.txtUslugiAmount.Name = "txtUslugiAmount";
			this.txtUslugiAmount.Size = new System.Drawing.Size(129, 20);
			this.txtUslugiAmount.TabIndex = 93;
			this.txtUslugiAmount.Text = "";
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
			this.cmbMechanic.Location = new Point(104, 64);
			this.cmbMechanic.MatchEntryTimeout = (long)2000;
			this.cmbMechanic.MaxDropDownItems = 5;
			this.cmbMechanic.MaxLength = 32767;
			this.cmbMechanic.MouseCursor = Cursors.Default;
			this.cmbMechanic.Name = "cmbMechanic";
			this.cmbMechanic.RowDivider.Color = Color.DarkGray;
			this.cmbMechanic.RowDivider.Style = LineStyleEnum.None;
			this.cmbMechanic.RowSubDividerColor = Color.DarkGray;
			this.cmbMechanic.Size = new System.Drawing.Size(200, 19);
			this.cmbMechanic.TabIndex = 92;
			this.cmbMechanic.KeyPress += new KeyPressEventHandler(this.cmbMechanic_KeyPress);
			this.cmbMechanic.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 68);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 91;
			this.label5.Text = "Выполнил";
			this.CmbUslugiVDGO.AddItemSeparator = ';';
			this.CmbUslugiVDGO.BorderStyle = 1;
			this.CmbUslugiVDGO.Caption = "";
			this.CmbUslugiVDGO.CaptionHeight = 17;
			this.CmbUslugiVDGO.CharacterCasing = 0;
			this.CmbUslugiVDGO.ColumnCaptionHeight = 17;
			this.CmbUslugiVDGO.ColumnFooterHeight = 17;
			this.CmbUslugiVDGO.ColumnHeaders = false;
			this.CmbUslugiVDGO.ColumnWidth = 100;
			this.CmbUslugiVDGO.ComboStyle = ComboStyleEnum.DropdownList;
			this.CmbUslugiVDGO.ContentHeight = 15;
			this.CmbUslugiVDGO.DataMode = DataModeEnum.AddItem;
			this.CmbUslugiVDGO.DeadAreaBackColor = Color.Empty;
			this.CmbUslugiVDGO.EditorBackColor = SystemColors.Window;
			this.CmbUslugiVDGO.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.CmbUslugiVDGO.EditorForeColor = SystemColors.WindowText;
			this.CmbUslugiVDGO.EditorHeight = 15;
			this.CmbUslugiVDGO.FlatStyle = FlatModeEnum.Flat;
			this.CmbUslugiVDGO.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.CmbUslugiVDGO.ItemHeight = 15;
			this.CmbUslugiVDGO.Location = new Point(104, 40);
			this.CmbUslugiVDGO.MatchEntryTimeout = (long)2000;
			this.CmbUslugiVDGO.MaxDropDownItems = 5;
			this.CmbUslugiVDGO.MaxLength = 32767;
			this.CmbUslugiVDGO.MouseCursor = Cursors.Default;
			this.CmbUslugiVDGO.Name = "CmbUslugiVDGO";
			this.CmbUslugiVDGO.RowDivider.Color = Color.DarkGray;
			this.CmbUslugiVDGO.RowDivider.Style = LineStyleEnum.None;
			this.CmbUslugiVDGO.RowSubDividerColor = Color.DarkGray;
			this.CmbUslugiVDGO.Size = new System.Drawing.Size(248, 19);
			this.CmbUslugiVDGO.TabIndex = 90;
			this.CmbUslugiVDGO.TextChanged += new EventHandler(this.CmbUslugiVDGO_TextChanged);
			this.CmbUslugiVDGO.KeyPress += new KeyPressEventHandler(this.CmbUslugiVDGO_KeyPress);
			this.CmbUslugiVDGO.Change += new ChangeEventHandler(this.CmbUslugiVDGO_Change);
			this.CmbUslugiVDGO.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 89;
			this.label2.Text = "Потребление";
			this.numCub.BackColor = SystemColors.Info;
			this.numCub.BorderStyle = BorderStyle.FixedSingle;
			this.numCub.DecimalPlaces = 3;
			this.numCub.Location = new Point(248, 140);
			NumericUpDown num = this.numCub;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown = this.numCub;
			numArray = new int[] { 9999999, 0, 0, -2147483648 };
			numericUpDown.Minimum = new decimal(numArray);
			this.numCub.Name = "numCub";
			this.numCub.Size = new System.Drawing.Size(128, 20);
			this.numCub.TabIndex = 87;
			this.numCub.TabStop = false;
			this.numCub.Visible = false;
			this.numNewIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numNewIndication.DecimalPlaces = 3;
			this.numNewIndication.Enabled = false;
			this.numNewIndication.Location = new Point(104, 112);
			NumericUpDown num1 = this.numNewIndication;
			numArray = new int[] { 9999, 0, 0, 0 };
			num1.Maximum = new decimal(numArray);
			this.numNewIndication.Name = "numNewIndication";
			this.numNewIndication.Size = new System.Drawing.Size(128, 20);
			this.numNewIndication.TabIndex = 5;
			this.numNewIndication.KeyPress += new KeyPressEventHandler(this.numNewIndication_KeyPress);
			this.numNewIndication.Enter += new EventHandler(this.numNewIndication_Enter);
			this.numNewIndication.Leave += new EventHandler(this.numNewIndication_Leave_1);
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(7, 116);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(97, 16);
			this.label18.TabIndex = 88;
			this.label18.Text = "Новые показания";
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 188);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(368, 48);
			this.txtNote.TabIndex = 8;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.txtNote.Enter += new EventHandler(this.txtNote_Enter);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 168);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 84;
			this.label17.Text = "Примечание";
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(8, 92);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 16);
			this.label7.TabIndex = 72;
			this.label7.Text = "Сумма";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(104, 16);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(192, 18);
			this.dtDate.TabIndex = 1;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.KeyPress += new KeyPressEventHandler(this.dtDate_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(4, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(92, 16);
			this.label6.TabIndex = 68;
			this.label6.Text = "Дата документа";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 16);
			this.label1.TabIndex = 72;
			this.label1.Text = "Вид услуги";
			this.label3.BackColor = SystemColors.Info;
			this.label3.BorderStyle = BorderStyle.FixedSingle;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(248, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 20);
			this.label3.TabIndex = 18;
			this.label3.Visible = false;
			this.label4.BackColor = SystemColors.Info;
			this.label4.BorderStyle = BorderStyle.FixedSingle;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(104, 140);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(136, 20);
			this.label4.TabIndex = 18;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(296, 388);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 4;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(192, 388);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(394, 431);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximumSize = new System.Drawing.Size(400, 463);
			base.Name = "frmTechObsluch";
			this.Text = "Услуги";
			base.Closing += new CancelEventHandler(this.frmChangeCharge_Closing);
			base.Load += new EventHandler(this.frmChangeCharge_Load);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmbMechanic).EndInit();
			((ISupportInitialize)this.CmbUslugiVDGO).EndInit();
			((ISupportInitialize)this.numCub).EndInit();
			((ISupportInitialize)this.numNewIndication).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			base.ResumeLayout(false);
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
				if (this.txtNote.Text.Length > 0)
				{
					this.cmdOK.Focus();
					return;
				}
				this.txtNote.Focus();
			}
		}

		private void numNewIndication_Leave_1(object sender, EventArgs e)
		{
			if (this.numNewIndication.Value == new decimal(0))
			{
				this.numCub.Value = new decimal(0);
				this.label4.Text = "0";
			}
			else
			{
				this.numCub.Value = this.numNewIndication.Value - Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display);
				this.label4.Text = Convert.ToString(Math.Round(this.numNewIndication.Value - Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display), 2));
				if ((this.numNewIndication.Value - Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display)) < new decimal(0))
				{
					MessageBox.Show("Потребление отрицательное!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.numNewIndication.Focus();
				}
				if ((this.numNewIndication.Value - Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display)) > new decimal(10))
				{
					MessageBox.Show("Потребление больше 10 м3!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				if ((DateTime)this.dtDate.Value <= this._gmeter.GetCurrentIndication().Datedisplay)
				{
					MessageBox.Show("Дата новых показаний должна быть больше предыдущих!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
		}

		private void ResetFields1()
		{
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblBalance.Text = "";
			this.lblTel.Text = "";
			this._contract = null;
			this._gobject = null;
			this.lblCurrentIndication.Text = "";
			this.numNewIndication.Value = new decimal(0);
			this.numNewIndication.Enabled = false;
			this._gmeter = null;
		}

		private void txtAccount_Enter(object sender, EventArgs e)
		{
			this.txtAccount.SelectAll();
		}

		private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
		{
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

		private void txtAccount_TextChanged(object sender, EventArgs e)
		{
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