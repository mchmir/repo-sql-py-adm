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
	public class frmLegalDoc : Form
	{
		private C1DateEdit dtDate;

		private Label label6;

		private GroupBox groupBox2;

		private C1TextBox txtAccount;

		private Button cmdAccount;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private Label lblCountLives;

		private Label label13;

		private ToolTip toolTip1;

		private ImageList imageList1;

		private Button cmdCard;

		private GroupBox groupBox1;

		private Label lblBalance;

		private Label label9;

		private Label lblOU;

		private Label label2;

		private Label lblPU;

		private Label label4;

		private Label lblStatus;

		private Label label3;

		private GroupBox groupBox3;

		private NumericUpDown numAmount;

		private Label label1;

		private C1DateEdit dtDate2;

		private Label label5;

		private TextBox txtNote;

		private Label label17;

		private GroupBox groupBox4;

		private Label label7;

		private Label label8;

		private Label label14;

		private C1DateEdit dtDate3;

		private C1Combo cmbResult;

		private TextBox txtNote2;

		private Label label15;

		private Label lblTax;

		private GroupBox groupBox5;

		private C1Combo cmbStatus;

		private Label label16;

		private Label label18;

		private Label label19;

		private Label label20;

		private Button cmdClose;

		private Button cmdOK;

		private TextBox txtNote3;

		private IContainer components;

		private Document _doc;

		private PD _pd9;

		private PD _pd12;

		private PD _pd21;

		private PD _pd37;

		private PD _pd29;

		private PD _pd8;

		private PD _pd10;

		private PD _pd11;

		private PD _pd20;

		private PD _pd22;

		private double _AmDolg;

		private Accountings _Accountings;

		private Contract _contract;

		private Gobject _gobject;

		private Label lblTax2;

		private Label lblClaim;

		private Label label21;

		private Label label22;

		private TextBox txtKomSbor;

		private NumericUpDown numAmountSud;

		private Label label23;

		private Label lblTaxSud;

		private ComboBox cmbPercent;

		private Label label24;

		private Gmeter _gmeter;

		public frmLegalDoc(Document oDocument)
		{
			this.InitializeComponent();
			this._doc = oDocument;
		}

		private void cmbPercent_SelectedIndexChanged(object sender, EventArgs e)
		{
			double num = Convert.ToDouble(this.cmbPercent.SelectedItem) / 100;
			this.RecalcTax(num);
			this.lblTaxSud.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.numAmountSud.Value) * num, 2));
		}

		private void cmbResult_TextChanged(object sender, EventArgs e)
		{
			if (this.cmbResult.SelectedIndex <= 0)
			{
				this.numAmountSud.ReadOnly = false;
				return;
			}
			this.numAmountSud.Value = new decimal(0);
			this.numAmountSud.ReadOnly = true;
		}

		private void cmbStatus_TextChanged(object sender, EventArgs e)
		{
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
						this._AmDolg = this._contract.CurrentBalance(this._Accountings.item((long)1)).AmountBalance;
						this.lblFIO.Text = this._contract.oPerson.FullName;
						this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
						Label str = this.lblCountLives;
						int countLives = this._contract.oGobjects[0].CountLives;
						str.Text = countLives.ToString();
						this._gobject = this._contract.oGobjects[0];
						this._gmeter = this._gobject.GetActiveGMeter();
						this.lblBalance.Text = Convert.ToString(this._contract.CurrentBalance());
						if (this._contract.Status != 0)
						{
							this.lblStatus.Text = "Не активен";
						}
						else
						{
							this.lblStatus.Text = "Активен";
						}
						if (this._gobject.oStatusGObject.get_ID() != (long)1)
						{
							this.lblOU.Text = "Не подключен";
						}
						else
						{
							this.lblOU.Text = "Подключен";
						}
						if (this._gmeter != null)
						{
							this.lblPU.Text = "Подключен";
						}
						else
						{
							this.lblPU.Text = "Не подключен";
						}
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

		private void cmdCard_Click(object sender, EventArgs e)
		{
			if (this._contract == null)
			{
				return;
			}
			(new frmContract(this._contract)).ShowDialog(this);
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			PD shortDateString;
			PD str;
			PD text;
			DateTime value;
			int selectedIndex;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract != null)
					{
						bool flag = false;
						if (this._doc.get_ID() == (long)0)
						{
							flag = true;
							this._doc.oContract = this._contract;
							this._doc.oPeriod = Depot.CurrentPeriod;
							this._doc.oTypeDocument = Depot.oTypeDocuments.item((long)13);
						}
						if (!this.dtDate2.ValueIsDbNull)
						{
							this._doc.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
						}
						this._doc.DocumentDate = (DateTime)this.dtDate.Value;
						if (this._doc.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						else
						{
							if (!this.dtDate2.ValueIsDbNull)
							{
								if (this._pd9 == null)
								{
									shortDateString = this._doc.oPDs.Add();
									shortDateString.oTypePD = Depot.oTypePDs.item((long)9);
									shortDateString.oDocument = this._doc;
								}
								else
								{
									shortDateString = this._pd9;
								}
								value = (DateTime)this.dtDate2.Value;
								shortDateString.Value = value.ToShortDateString();
								if (shortDateString.Save() == 0)
								{
									if (this._pd12 == null)
									{
										shortDateString = this._doc.oPDs.Add();
										shortDateString.oTypePD = Depot.oTypePDs.item((long)12);
										shortDateString.oDocument = this._doc;
									}
									else
									{
										shortDateString = this._pd12;
									}
									shortDateString.Value = this.numAmountSud.Value.ToString();
									if (shortDateString.Save() == 0)
									{
										if (this._pd21 == null)
										{
											shortDateString = this._doc.oPDs.Add();
											shortDateString.oTypePD = Depot.oTypePDs.item((long)21);
											shortDateString.oDocument = this._doc;
										}
										else
										{
											shortDateString = this._pd21;
										}
										shortDateString.Value = this.txtNote.Text;
										if (shortDateString.Save() == 0)
										{
											if (this._pd37 == null)
											{
												shortDateString = this._doc.oPDs.Add();
												shortDateString.oTypePD = Depot.oTypePDs.item((long)37);
												shortDateString.oDocument = this._doc;
											}
											else
											{
												shortDateString = this._pd37;
											}
											shortDateString.Value = this.cmbPercent.Text.ToString();
											if (shortDateString.Save() == 0)
											{
												if (this._pd29 == null)
												{
													shortDateString = this._doc.oPDs.Add();
													shortDateString.oTypePD = Depot.oTypePDs.item((long)29);
													shortDateString.oDocument = this._doc;
												}
												else
												{
													shortDateString = this._pd29;
												}
												shortDateString.Value = this.txtKomSbor.Text;
												if (shortDateString.Save() != 0)
												{
													if (flag)
													{
														this._doc.Delete();
													}
													MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
													return;
												}
											}
											else
											{
												if (flag)
												{
													this._doc.Delete();
												}
												MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
											}
										}
										else
										{
											if (flag)
											{
												this._doc.Delete();
											}
											MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
									}
									else
									{
										if (flag)
										{
											this._doc.Delete();
										}
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
								}
								else
								{
									if (flag)
									{
										this._doc.Delete();
									}
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							if (!this.dtDate3.ValueIsDbNull)
							{
								if (this._pd10 == null)
								{
									str = this._doc.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)10);
									str.oDocument = this._doc;
								}
								else
								{
									str = this._pd10;
								}
								value = (DateTime)this.dtDate3.Value;
								str.Value = value.ToShortDateString();
								if (str.Save() == 0)
								{
									if (this._pd11 == null)
									{
										str = this._doc.oPDs.Add();
										str.oTypePD = Depot.oTypePDs.item((long)11);
										str.oDocument = this._doc;
									}
									else
									{
										str = this._pd11;
									}
									if (this.cmbResult.SelectedIndex != -1)
									{
										selectedIndex = this.cmbResult.SelectedIndex;
										str.Value = selectedIndex.ToString();
									}
									else
									{
										str.Value = "0";
									}
									if (str.Save() == 0)
									{
										if (this._pd8 == null)
										{
											str = this._doc.oPDs.Add();
											str.oTypePD = Depot.oTypePDs.item((long)8);
											str.oDocument = this._doc;
										}
										else
										{
											str = this._pd8;
										}
										str.Value = this.txtNote2.Text;
										if (str.Save() != 0)
										{
											if (flag)
											{
												this._doc.Delete();
											}
											MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
									}
									else
									{
										if (flag)
										{
											this._doc.Delete();
										}
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
								}
								else
								{
									if (flag)
									{
										this._doc.Delete();
									}
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							if (this.cmbStatus.SelectedIndex > -1)
							{
								if (this._pd22 == null)
								{
									text = this._doc.oPDs.Add();
									text.oTypePD = Depot.oTypePDs.item((long)22);
									text.oDocument = this._doc;
								}
								else
								{
									text = this._pd22;
								}
								if (this.cmbStatus.SelectedIndex != -1)
								{
									selectedIndex = this.cmbStatus.SelectedIndex;
									text.Value = selectedIndex.ToString();
								}
								else
								{
									text.Value = "0";
								}
								if (text.Save() == 0)
								{
									if (this._pd20 == null)
									{
										text = this._doc.oPDs.Add();
										text.oTypePD = Depot.oTypePDs.item((long)20);
										text.oDocument = this._doc;
									}
									else
									{
										text = this._pd20;
									}
									text.Value = this.txtNote3.Text;
									if (text.Save() != 0)
									{
										if (flag)
										{
											this._doc.Delete();
										}
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
								}
								else
								{
									if (flag)
									{
										this._doc.Delete();
									}
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							if (this._doc.DocumentAmount > 0)
							{
								bool flag1 = false;
								long d = this._doc.get_ID();
								double num = Convert.ToDouble(this.cmbPercent.SelectedItem) / 100;
								if (!this._doc.UndertakingJuridikal(Depot.CurrentPeriod.get_ID(), this._doc.oContract.get_ID(), ref d, this._doc.DocumentAmount, Math.Round(this._doc.DocumentAmount * num + Convert.ToDouble(this.txtKomSbor.Text), 2), ref flag1))
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									this._doc.set_ID(d);
									base.DialogResult = System.Windows.Forms.DialogResult.OK;
									base.Close();
									return;
								}
							}
							base.DialogResult = System.Windows.Forms.DialogResult.OK;
							base.Close();
						}
					}
					else
					{
						MessageBox.Show("Не указан контракт!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageBox.Show(exception.Message, "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

		private void dtDate2_ValueChanged(object sender, EventArgs e)
		{
			if (this.dtDate2.ValueIsDbNull)
			{
				this.dtDate3.ValueIsDbNull = true;
				this.cmbStatus.SelectedIndex = 0;
			}
		}

		private void dtDate3_ValueChanged(object sender, EventArgs e)
		{
			if (this.dtDate2.ValueIsDbNull)
			{
				this.dtDate3.ValueIsDbNull = true;
				this.cmbStatus.SelectedIndex = 0;
			}
		}

		private void frmLegalDoc_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmLegalDoc_Load(object sender, EventArgs e)
		{
			try
			{
				this._Accountings = Depot.oAccountings;
				this.dtDate.Value = DateTime.Today.Date;
				Tools.LoadWindows(this);
				this.cmbResult.AddItem("иск удовлетворен");
				this.cmbResult.AddItem("иск не удовлетворен");
				this.cmbResult.AddItem("иск без рассмотрения");
				this.cmbResult.ColumnWidth = this.cmbResult.Width - this.cmbResult.Height;
				this.cmbStatus.AddItem("Не определено");
				this.cmbStatus.AddItem("Активно");
				this.cmbStatus.AddItem("Закрыт");
				this.cmbStatus.ColumnWidth = this.cmbStatus.Width - this.cmbStatus.Height;
				this.cmbStatus.SelectedIndex = 0;
				if (this._doc.get_ID() != (long)0)
				{
					this.dtDate.Value = this._doc.DocumentDate;
					this._contract = this._doc.oContract;
					this._AmDolg = this._contract.CurrentBalance(this._Accountings.item((long)1)).AmountBalance;
					this.cmdAccount.Enabled = false;
					this.txtAccount.Text = this._contract.Account;
					this.txtAccount.Enabled = false;
					this.lblFIO.Text = this._contract.oPerson.FullName;
					this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
					Label str = this.lblCountLives;
					int countLives = this._contract.oGobjects[0].CountLives;
					str.Text = countLives.ToString();
					this._gobject = this._contract.oGobjects[0];
					this._gmeter = this._gobject.GetActiveGMeter();
					this.lblBalance.Text = Convert.ToString(Math.Round(this._contract.CurrentBalance(), 2));
					switch (this._contract.Status)
					{
						case 0:
						{
							this.lblStatus.Text = "Не определен";
							break;
						}
						case 1:
						{
							this.lblStatus.Text = "Активен";
							break;
						}
						case 2:
						{
							this.lblStatus.Text = "Закрыт";
							break;
						}
					}
					if (this._gobject.oStatusGObject.get_ID() != (long)1)
					{
						this.lblOU.Text = "Не подключен";
					}
					else
					{
						this.lblOU.Text = "Подключен";
					}
					if (this._gmeter != null)
					{
						this.lblPU.Text = "Подключен";
					}
					else
					{
						this.lblPU.Text = "Не подключен";
					}
					if (this._contract.CurrentBalance(Depot.oAccountings.item((long)3)) != null)
					{
						this.lblTax2.Text = Convert.ToString(Math.Round(this._contract.CurrentBalance(Depot.oAccountings.item((long)3)).AmountBalance, 2));
					}
					if (this._contract.CurrentBalance(Depot.oAccountings.item((long)2)) != null)
					{
						this.lblClaim.Text = Convert.ToString(Math.Round(this._contract.CurrentBalance(Depot.oAccountings.item((long)2)).AmountBalance, 2));
					}
					NumericUpDown numericUpDown = this.numAmount;
					double documentAmount = this._doc.DocumentAmount;
					numericUpDown.Value = decimal.Parse(documentAmount.ToString());
					if (this._doc.GetPD(9) != null)
					{
						this._pd9 = this._doc.GetPD(9);
						this.dtDate2.Value = Convert.ToDateTime(this._pd9.Value);
					}
					if (this._doc.GetPD(12) != null)
					{
						this._pd12 = this._doc.GetPD(12);
						this.numAmountSud.Value = Convert.ToDecimal(this._pd12.Value);
					}
					if (this._doc.GetPD(37) == null)
					{
						this.cmbPercent.SelectedIndex = 0;
					}
					else
					{
						this._pd37 = this._doc.GetPD(37);
						this.cmbPercent.SelectedText = this._pd37.Value;
						this.RecalcTax(Convert.ToDouble(this._pd37.Value) / 100);
					}
					if (this._doc.GetPD(29) != null)
					{
						this._pd29 = this._doc.GetPD(29);
						this.txtKomSbor.Text = this._pd29.Value;
						this.txtKomSbor.Enabled = false;
					}
					if (this._doc.GetPD(21) != null)
					{
						this._pd21 = this._doc.GetPD(21);
						this.txtNote.Text = this._pd21.Value;
					}
					if (this._doc.GetPD(8) != null)
					{
						this._pd8 = this._doc.GetPD(8);
						this.txtNote2.Text = this._pd8.Value;
					}
					if (this._doc.GetPD(10) != null)
					{
						this._pd10 = this._doc.GetPD(10);
						this.dtDate3.Value = Convert.ToDateTime(this._pd10.Value);
					}
					if (this._doc.GetPD(11) != null)
					{
						this._pd11 = this._doc.GetPD(11);
						this.cmbResult.SelectedIndex = Convert.ToInt32(this._pd11.Value);
					}
					if (this._doc.GetPD(20) != null)
					{
						this._pd20 = this._doc.GetPD(20);
						this.txtNote3.Text = this._pd20.Value;
					}
					if (this._doc.GetPD(22) != null)
					{
						this._pd22 = this._doc.GetPD(22);
						this.cmbStatus.SelectedIndex = Convert.ToInt32(this._pd22.Value);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(exception.Message, "Иск", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void groupBox4_Enter(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmLegalDoc));
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.groupBox2 = new GroupBox();
			this.cmdCard = new Button();
			this.lblCountLives = new Label();
			this.label13 = new Label();
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
			this.lblStatus = new Label();
			this.label3 = new Label();
			this.lblPU = new Label();
			this.label4 = new Label();
			this.lblOU = new Label();
			this.label2 = new Label();
			this.lblBalance = new Label();
			this.label9 = new Label();
			this.groupBox3 = new GroupBox();
			this.label24 = new Label();
			this.txtKomSbor = new TextBox();
			this.label22 = new Label();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.numAmount = new NumericUpDown();
			this.label1 = new Label();
			this.dtDate2 = new C1DateEdit();
			this.label5 = new Label();
			this.lblTax = new Label();
			this.label21 = new Label();
			this.cmbPercent = new ComboBox();
			this.groupBox4 = new GroupBox();
			this.lblTaxSud = new Label();
			this.label23 = new Label();
			this.label15 = new Label();
			this.cmbResult = new C1Combo();
			this.txtNote2 = new TextBox();
			this.label7 = new Label();
			this.numAmountSud = new NumericUpDown();
			this.label8 = new Label();
			this.dtDate3 = new C1DateEdit();
			this.label14 = new Label();
			this.groupBox5 = new GroupBox();
			this.lblClaim = new Label();
			this.lblTax2 = new Label();
			this.txtNote3 = new TextBox();
			this.label20 = new Label();
			this.label19 = new Label();
			this.label18 = new Label();
			this.cmbStatus = new C1Combo();
			this.label16 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			((ISupportInitialize)this.dtDate).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtAccount).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.numAmount).BeginInit();
			((ISupportInitialize)this.dtDate2).BeginInit();
			this.groupBox4.SuspendLayout();
			((ISupportInitialize)this.cmbResult).BeginInit();
			((ISupportInitialize)this.numAmountSud).BeginInit();
			((ISupportInitialize)this.dtDate3).BeginInit();
			this.groupBox5.SuspendLayout();
			((ISupportInitialize)this.cmbStatus).BeginInit();
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
			this.label6.TabIndex = 58;
			this.label6.Text = "Дата документа";
			this.groupBox2.Controls.Add(this.cmdCard);
			this.groupBox2.Controls.Add(this.lblCountLives);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.txtAccount);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(8, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 120);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.cmdCard.FlatStyle = FlatStyle.Flat;
			this.cmdCard.ForeColor = SystemColors.ControlText;
			this.cmdCard.Location = new Point(180, 16);
			this.cmdCard.Name = "cmdCard";
			this.cmdCard.Size = new System.Drawing.Size(92, 20);
			this.cmdCard.TabIndex = 3;
			this.cmdCard.Text = "Карточка л/сч";
			this.cmdCard.Click += new EventHandler(this.cmdCard_Click);
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(88, 88);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 13;
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 88);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 12;
			this.label13.Text = "Проживает";
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
			this.groupBox1.Controls.Add(this.lblStatus);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.lblPU);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lblOU);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.lblBalance);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(292, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(240, 120);
			this.groupBox1.TabIndex = 61;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Текущее состояние";
			this.lblStatus.BackColor = SystemColors.Info;
			this.lblStatus.BorderStyle = BorderStyle.FixedSingle;
			this.lblStatus.ForeColor = SystemColors.ControlText;
			this.lblStatus.Location = new Point(112, 88);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(120, 20);
			this.lblStatus.TabIndex = 23;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 22;
			this.label3.Text = "Договор";
			this.lblPU.BackColor = SystemColors.Info;
			this.lblPU.BorderStyle = BorderStyle.FixedSingle;
			this.lblPU.ForeColor = SystemColors.ControlText;
			this.lblPU.Location = new Point(112, 64);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(120, 20);
			this.lblPU.TabIndex = 21;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 20;
			this.label4.Text = "Статус ПУ";
			this.lblOU.BackColor = SystemColors.Info;
			this.lblOU.BorderStyle = BorderStyle.FixedSingle;
			this.lblOU.ForeColor = SystemColors.ControlText;
			this.lblOU.Location = new Point(112, 40);
			this.lblOU.Name = "lblOU";
			this.lblOU.Size = new System.Drawing.Size(120, 20);
			this.lblOU.TabIndex = 19;
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Статус ОУ";
			this.lblBalance.BackColor = SystemColors.Info;
			this.lblBalance.BorderStyle = BorderStyle.FixedSingle;
			this.lblBalance.ForeColor = SystemColors.ControlText;
			this.lblBalance.Location = new Point(112, 16);
			this.lblBalance.Name = "lblBalance";
			this.lblBalance.Size = new System.Drawing.Size(120, 20);
			this.lblBalance.TabIndex = 17;
			this.lblBalance.Click += new EventHandler(this.lblBalance_Click);
			this.label9.ForeColor = SystemColors.ControlText;
			this.label9.Location = new Point(8, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 16);
			this.label9.TabIndex = 16;
			this.label9.Text = "Сальдо";
			this.groupBox3.Controls.Add(this.label24);
			this.groupBox3.Controls.Add(this.txtKomSbor);
			this.groupBox3.Controls.Add(this.label22);
			this.groupBox3.Controls.Add(this.txtNote);
			this.groupBox3.Controls.Add(this.label17);
			this.groupBox3.Controls.Add(this.numAmount);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.dtDate2);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.lblTax);
			this.groupBox3.Controls.Add(this.label21);
			this.groupBox3.Controls.Add(this.cmbPercent);
			this.groupBox3.ForeColor = SystemColors.Desktop;
			this.groupBox3.Location = new Point(8, 152);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(524, 88);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Передача дела в суд";
			this.label24.ForeColor = SystemColors.ControlText;
			this.label24.Location = new Point(136, 68);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(16, 16);
			this.label24.TabIndex = 100;
			this.label24.Text = "%";
			this.txtKomSbor.BorderStyle = BorderStyle.FixedSingle;
			this.txtKomSbor.Location = new Point(328, 64);
			this.txtKomSbor.Name = "txtKomSbor";
			this.txtKomSbor.Size = new System.Drawing.Size(188, 20);
			this.txtKomSbor.TabIndex = 99;
			this.txtKomSbor.Text = "150";
			this.label22.ForeColor = SystemColors.ControlText;
			this.label22.Location = new Point(248, 68);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(76, 16);
			this.label22.TabIndex = 98;
			this.label22.Text = "Комисс. сбор";
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(248, 32);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(268, 28);
			this.txtNote.TabIndex = 3;
			this.txtNote.Text = "";
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(248, 16);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 94;
			this.label17.Text = "Примечание";
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(88, 40);
			NumericUpDown num = this.numAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.Size = new System.Drawing.Size(152, 20);
			this.numAmount.TabIndex = 2;
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.numAmount.ValueChanged += new EventHandler(this.numAmount_ValueChanged);
			this.numAmount.Leave += new EventHandler(this.numAmount_Leave);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 91;
			this.label1.Text = "Дата подачи";
			this.dtDate2.BorderStyle = 1;
			this.dtDate2.EmptyAsNull = true;
			this.dtDate2.FormatType = FormatTypeEnum.LongDate;
			this.dtDate2.Location = new Point(88, 16);
			this.dtDate2.Name = "dtDate2";
			this.dtDate2.Size = new System.Drawing.Size(152, 18);
			this.dtDate2.TabIndex = 1;
			this.dtDate2.Tag = null;
			this.dtDate2.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate2.ValueChanged += new EventHandler(this.dtDate2_ValueChanged);
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 89;
			this.label5.Text = "Сумма иска";
			this.lblTax.BackColor = SystemColors.Info;
			this.lblTax.BorderStyle = BorderStyle.FixedSingle;
			this.lblTax.ForeColor = SystemColors.ControlText;
			this.lblTax.Location = new Point(152, 64);
			this.lblTax.Name = "lblTax";
			this.lblTax.Size = new System.Drawing.Size(88, 20);
			this.lblTax.TabIndex = 97;
			this.label21.ForeColor = SystemColors.ControlText;
			this.label21.Location = new Point(8, 68);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(76, 16);
			this.label21.TabIndex = 97;
			this.label21.Text = "Гос.пошлина";
			this.label21.Click += new EventHandler(this.label21_Click);
			this.cmbPercent.Items.AddRange(new object[] { "1,5", "3" });
			this.cmbPercent.Location = new Point(88, 64);
			this.cmbPercent.Name = "cmbPercent";
			this.cmbPercent.Size = new System.Drawing.Size(48, 21);
			this.cmbPercent.TabIndex = 99;
			this.cmbPercent.SelectedIndexChanged += new EventHandler(this.cmbPercent_SelectedIndexChanged);
			this.groupBox4.Controls.Add(this.lblTaxSud);
			this.groupBox4.Controls.Add(this.label23);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.cmbResult);
			this.groupBox4.Controls.Add(this.txtNote2);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.numAmountSud);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.dtDate3);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.ForeColor = SystemColors.Desktop;
			this.groupBox4.Location = new Point(8, 240);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(524, 96);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Решение суда";
			this.groupBox4.Enter += new EventHandler(this.groupBox4_Enter);
			this.lblTaxSud.BorderStyle = BorderStyle.FixedSingle;
			this.lblTaxSud.ForeColor = SystemColors.ControlText;
			this.lblTaxSud.Location = new Point(332, 64);
			this.lblTaxSud.Name = "lblTaxSud";
			this.lblTaxSud.Size = new System.Drawing.Size(184, 20);
			this.lblTaxSud.TabIndex = 98;
			this.label23.ForeColor = SystemColors.ControlText;
			this.label23.Location = new Point(248, 66);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(80, 16);
			this.label23.TabIndex = 97;
			this.label23.Text = "Гос. пошлина";
			this.label15.ForeColor = SystemColors.ControlText;
			this.label15.Location = new Point(8, 66);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(80, 16);
			this.label15.TabIndex = 96;
			this.label15.Text = "Иск на сумму";
			this.cmbResult.AddItemSeparator = ';';
			this.cmbResult.BorderStyle = 1;
			this.cmbResult.Caption = "";
			this.cmbResult.CaptionHeight = 17;
			this.cmbResult.CharacterCasing = 0;
			this.cmbResult.ColumnCaptionHeight = 17;
			this.cmbResult.ColumnFooterHeight = 17;
			this.cmbResult.ColumnHeaders = false;
			this.cmbResult.ColumnWidth = 100;
			this.cmbResult.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbResult.ContentHeight = 15;
			this.cmbResult.DataMode = DataModeEnum.AddItem;
			this.cmbResult.DeadAreaBackColor = Color.Empty;
			this.cmbResult.EditorBackColor = SystemColors.Window;
			this.cmbResult.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbResult.EditorForeColor = SystemColors.WindowText;
			this.cmbResult.EditorHeight = 15;
			this.cmbResult.FlatStyle = FlatModeEnum.Flat;
			this.cmbResult.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbResult.ItemHeight = 15;
			this.cmbResult.Location = new Point(88, 40);
			this.cmbResult.MatchEntryTimeout = (long)2000;
			this.cmbResult.MaxDropDownItems = 5;
			this.cmbResult.MaxLength = 32767;
			this.cmbResult.MouseCursor = Cursors.Default;
			this.cmbResult.Name = "cmbResult";
			this.cmbResult.RowDivider.Color = Color.DarkGray;
			this.cmbResult.RowDivider.Style = LineStyleEnum.None;
			this.cmbResult.RowSubDividerColor = Color.DarkGray;
			this.cmbResult.Size = new System.Drawing.Size(152, 19);
			this.cmbResult.TabIndex = 2;
			this.cmbResult.TextChanged += new EventHandler(this.cmbResult_TextChanged);
			this.cmbResult.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.txtNote2.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote2.Location = new Point(248, 32);
			this.txtNote2.Multiline = true;
			this.txtNote2.Name = "txtNote2";
			this.txtNote2.Size = new System.Drawing.Size(268, 27);
			this.txtNote2.TabIndex = 4;
			this.txtNote2.Text = "";
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(248, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 16);
			this.label7.TabIndex = 94;
			this.label7.Text = "Примечание";
			this.numAmountSud.BorderStyle = BorderStyle.FixedSingle;
			this.numAmountSud.DecimalPlaces = 2;
			this.numAmountSud.Location = new Point(88, 64);
			NumericUpDown numericUpDown = this.numAmountSud;
			numArray = new int[] { 9999999, 0, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray);
			this.numAmountSud.Name = "numAmountSud";
			this.numAmountSud.Size = new System.Drawing.Size(152, 20);
			this.numAmountSud.TabIndex = 3;
			this.numAmountSud.KeyPress += new KeyPressEventHandler(this.numAmountSud_KeyPress);
			this.numAmountSud.Enter += new EventHandler(this.numAmountSud_Enter);
			this.numAmountSud.KeyUp += new KeyEventHandler(this.numAmountSud_KeyUp);
			this.numAmountSud.KeyDown += new KeyEventHandler(this.numAmountSud_KeyDown);
			this.numAmountSud.ValueChanged += new EventHandler(this.numAmountSud_ValueChanged);
			this.numAmountSud.Leave += new EventHandler(this.numAmountSud_Leave);
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(8, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 16);
			this.label8.TabIndex = 91;
			this.label8.Text = "Дата решения";
			this.dtDate3.BorderStyle = 1;
			this.dtDate3.EmptyAsNull = true;
			this.dtDate3.FormatType = FormatTypeEnum.LongDate;
			this.dtDate3.Location = new Point(88, 16);
			this.dtDate3.Name = "dtDate3";
			this.dtDate3.Size = new System.Drawing.Size(152, 18);
			this.dtDate3.TabIndex = 1;
			this.dtDate3.Tag = null;
			this.dtDate3.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate3.ValueChanged += new EventHandler(this.dtDate3_ValueChanged);
			this.label14.ForeColor = SystemColors.ControlText;
			this.label14.Location = new Point(8, 40);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(80, 16);
			this.label14.TabIndex = 89;
			this.label14.Text = "Решение";
			this.groupBox5.Controls.Add(this.lblClaim);
			this.groupBox5.Controls.Add(this.lblTax2);
			this.groupBox5.Controls.Add(this.txtNote3);
			this.groupBox5.Controls.Add(this.label20);
			this.groupBox5.Controls.Add(this.label19);
			this.groupBox5.Controls.Add(this.label18);
			this.groupBox5.Controls.Add(this.cmbStatus);
			this.groupBox5.Controls.Add(this.label16);
			this.groupBox5.ForeColor = SystemColors.Desktop;
			this.groupBox5.Location = new Point(8, 336);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(524, 96);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Состояние исполнительного производства";
			this.lblClaim.BackColor = SystemColors.Info;
			this.lblClaim.BorderStyle = BorderStyle.FixedSingle;
			this.lblClaim.ForeColor = SystemColors.ControlText;
			this.lblClaim.Location = new Point(152, 64);
			this.lblClaim.Name = "lblClaim";
			this.lblClaim.Size = new System.Drawing.Size(88, 20);
			this.lblClaim.TabIndex = 105;
			this.lblTax2.BackColor = SystemColors.Info;
			this.lblTax2.BorderStyle = BorderStyle.FixedSingle;
			this.lblTax2.ForeColor = SystemColors.ControlText;
			this.lblTax2.Location = new Point(152, 38);
			this.lblTax2.Name = "lblTax2";
			this.lblTax2.Size = new System.Drawing.Size(88, 20);
			this.lblTax2.TabIndex = 104;
			this.txtNote3.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote3.Location = new Point(248, 32);
			this.txtNote3.Multiline = true;
			this.txtNote3.Name = "txtNote3";
			this.txtNote3.Size = new System.Drawing.Size(268, 32);
			this.txtNote3.TabIndex = 2;
			this.txtNote3.Text = "";
			this.label20.ForeColor = SystemColors.ControlText;
			this.label20.Location = new Point(248, 16);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(96, 16);
			this.label20.TabIndex = 103;
			this.label20.Text = "Примечание";
			this.label19.ForeColor = SystemColors.ControlText;
			this.label19.Location = new Point(8, 64);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(136, 16);
			this.label19.TabIndex = 100;
			this.label19.Text = "Задолженность по иску";
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(8, 38);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(136, 16);
			this.label18.TabIndex = 98;
			this.label18.Text = "Задолженность по гос.п.";
			this.cmbStatus.AddItemSeparator = ';';
			this.cmbStatus.BorderStyle = 1;
			this.cmbStatus.Caption = "";
			this.cmbStatus.CaptionHeight = 17;
			this.cmbStatus.CharacterCasing = 0;
			this.cmbStatus.ColumnCaptionHeight = 17;
			this.cmbStatus.ColumnFooterHeight = 17;
			this.cmbStatus.ColumnHeaders = false;
			this.cmbStatus.ColumnWidth = 100;
			this.cmbStatus.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbStatus.ContentHeight = 15;
			this.cmbStatus.DataMode = DataModeEnum.AddItem;
			this.cmbStatus.DeadAreaBackColor = Color.Empty;
			this.cmbStatus.EditorBackColor = SystemColors.Window;
			this.cmbStatus.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbStatus.EditorForeColor = SystemColors.WindowText;
			this.cmbStatus.EditorHeight = 15;
			this.cmbStatus.FlatStyle = FlatModeEnum.Flat;
			this.cmbStatus.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbStatus.ItemHeight = 15;
			this.cmbStatus.Location = new Point(88, 16);
			this.cmbStatus.MatchEntryTimeout = (long)2000;
			this.cmbStatus.MaxDropDownItems = 5;
			this.cmbStatus.MaxLength = 32767;
			this.cmbStatus.MouseCursor = Cursors.Default;
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.RowDivider.Color = Color.DarkGray;
			this.cmbStatus.RowDivider.Style = LineStyleEnum.None;
			this.cmbStatus.RowSubDividerColor = Color.DarkGray;
			this.cmbStatus.Size = new System.Drawing.Size(152, 19);
			this.cmbStatus.TabIndex = 1;
			this.cmbStatus.TextChanged += new EventHandler(this.cmbStatus_TextChanged);
			this.cmbStatus.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label16.ForeColor = SystemColors.ControlText;
			this.label16.Location = new Point(8, 16);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(80, 16);
			this.label16.TabIndex = 96;
			this.label16.Text = "Состояние";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(440, 436);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 7;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(336, 436);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 6;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(538, 463);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox5);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label6);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmLegalDoc";
			this.Text = "Юридический документ";
			base.Closing += new CancelEventHandler(this.frmLegalDoc_Closing);
			base.Load += new EventHandler(this.frmLegalDoc_Load);
			((ISupportInitialize)this.dtDate).EndInit();
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.numAmount).EndInit();
			((ISupportInitialize)this.dtDate2).EndInit();
			this.groupBox4.ResumeLayout(false);
			((ISupportInitialize)this.cmbResult).EndInit();
			((ISupportInitialize)this.numAmountSud).EndInit();
			((ISupportInitialize)this.dtDate3).EndInit();
			this.groupBox5.ResumeLayout(false);
			((ISupportInitialize)this.cmbStatus).EndInit();
			base.ResumeLayout(false);
		}

		private void label21_Click(object sender, EventArgs e)
		{
		}

		private void lblBalance_Click(object sender, EventArgs e)
		{
			(new frmBalance(this._contract)).ShowDialog(this);
		}

		private void numAmount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmount);
		}

		private void numAmount_Leave(object sender, EventArgs e)
		{
			try
			{
				double num = Convert.ToDouble(this.cmbPercent.SelectedText) / 100;
				this.RecalcTax(num);
			}
			catch
			{
			}
		}

		private void numAmount_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				double num = Convert.ToDouble(this.cmbPercent.SelectedText) / 100;
				this.RecalcTax(num);
			}
			catch
			{
			}
		}

		private void numAmountSud_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmountSud);
		}

		private void numAmountSud_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void numAmountSud_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void numAmountSud_KeyUp(object sender, KeyEventArgs e)
		{
		}

		private void numAmountSud_Leave(object sender, EventArgs e)
		{
			this.numAmountSudWork();
		}

		private void numAmountSud_ValueChanged(object sender, EventArgs e)
		{
		}

		private void numAmountSudWork()
		{
			this.txtNote2.Text = "";
			double num = Convert.ToDouble(this.cmbPercent.SelectedItem) / 100;
			this.lblTaxSud.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.numAmountSud.Value) * num, 2));
			if (this._AmDolg * -1 < Convert.ToDouble(this.numAmountSud.Value))
			{
				if (this._doc.get_ID() != (long)0 && this._doc.GetPD(8) != null)
				{
					this.txtNote2.Text = string.Concat(this._doc.GetPD(8).Value, ";");
				}
				TextBox textBox = this.txtNote2;
				textBox.Text = string.Concat(textBox.Text, " Сумма иска > осн.долга на ", Convert.ToString(Math.Round(Convert.ToDouble(this.numAmountSud.Value) - this._AmDolg * -1, 2)));
			}
		}

		private void RecalcTax(double Percent)
		{
			this.lblTax.Text = Convert.ToString(Math.Round(Convert.ToDouble(this.numAmount.Value) * Percent, 2));
		}

		private void ResetFields1()
		{
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblCountLives.Text = "";
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
	}
}