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
	public class frmActJob : Form
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

		private Label lblPU;

		private Label label2;

		private C1DateEdit dtDate;

		private Label label6;

		private Label label1;

		private Label label3;

		private Label label4;

		private Label label7;

		private Button cmdClose;

		private Button cmdOK;

		private CheckBox checkBox1;

		private IContainer components;

		private GroupBox groupBox1;

		private TextBox txtNote;

		private ToolTip toolTip1;

		private NumericUpDown numBeginIndication;

		private NumericUpDown numLastIndication;

		private Label lblDifference;

		private C1Combo cmbMechanic;

		private C1TextBox txtNumber;

		private Label label5;

		private double FactAmount = 0;

		private Agents _mechanics;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private Indication _indication;

		private Contract _contract;

		private Label lblDisplay;

		private Document _doc;

		public frmActJob(Document oDocument)
		{
			this.InitializeComponent();
			this._doc = oDocument;
		}

		private void CalcDifference(bool need)
		{
			if (this.numBeginIndication.Value <= this.numLastIndication.Value)
			{
				this.lblDifference.Text = Convert.ToString(new decimal(1000) * (this.numLastIndication.Value - this.numBeginIndication.Value));
			}
			if (need && (this.numBeginIndication.Value - Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display)) >= new decimal(10))
			{
				MessageBox.Show("Потребление больше 10 м3!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void checkBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdOK.Focus();
			}
		}

		private void cmbMechanic_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numBeginIndication.Focus();
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
						this.lblFIO.Text = this._contract.oPerson.FullName;
						this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
						this._gobject = this._contract.oGobjects[0];
						this._gmeter = this._gobject.GetActiveGMeter();
						if (this._gmeter == null)
						{
							this._indication = null;
							this.lblPU.Text = "";
							this.lblDisplay.Text = "";
							this.groupBox1.Enabled = false;
							this.cmdOK.Enabled = false;
						}
						else
						{
							this._indication = this._gmeter.oIndications.Add();
							if (this._gmeter.oStatusGMeter.get_ID() != (long)1)
							{
								this.groupBox1.Enabled = false;
								this.cmdOK.Enabled = false;
							}
							else
							{
								this.groupBox1.Enabled = true;
								this.cmdOK.Enabled = true;
								Label label = this.lblPU;
								string[] serialNumber = new string[] { "№ ", this._gmeter.SerialNumber, ", ", this._gmeter.oTypeGMeter.Fullname, ", ", this._gmeter.oStatusGMeter.get_Name() };
								label.Text = string.Concat(serialNumber);
								Label label1 = this.lblDisplay;
								serialNumber = new string[] { Convert.ToString(this._gmeter.GetCurrentIndication().Display), " от ", null, null, null };
								DateTime datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
								serialNumber[2] = datedisplay.ToString("dd.MM.yyyy");
								serialNumber[3] = ", ";
								serialNumber[4] = this._gmeter.GetCurrentIndication().oTypeIndication.get_Name();
								label1.Text = string.Concat(serialNumber);
								this.numBeginIndication.Value = Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display);
								this.numLastIndication.Value = Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display);
							}
						}
						this.txtNumber.Focus();
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
			PD str;
			long d;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract == null)
					{
						this.txtAccount.Focus();
						return;
					}
					else if (this.cmbMechanic.SelectedIndex == -1)
					{
						MessageBox.Show("Не выбран слесарь/контролер!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					else if (this.numBeginIndication.Value <= this.numLastIndication.Value)
					{
						this.numLastIndication_Leave(null, null);
						this._doc.oBatch = null;
						this._doc.oContract = this._contract;
						this._doc.oPeriod = Depot.CurrentPeriod;
						this._doc.oTypeDocument = Depot.oTypeDocuments.item((long)20);
						this._doc.DocumentDate = (DateTime)this.dtDate.Value;
						this._doc.DocumentNumber = this.txtNumber.Text;
						this._doc.DocumentAmount = Math.Round(Convert.ToDouble(new decimal(1000) * (this.numLastIndication.Value - this.numBeginIndication.Value)), 6);
						this._doc.Note = this.txtNote.Text;
						if (this._doc.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							if (this.FactAmount >= 0)
							{
								this._indication.oAgent = this._mechanics[this.cmbMechanic.SelectedIndex];
								if (this._indication.Save(true) == 0)
								{
									str = this._doc.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)1);
									str.oDocument = this._doc;
									d = this._indication.get_ID();
									str.Value = d.ToString();
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
							str = this._doc.oPDs.Add();
							str.oTypePD = Depot.oTypePDs.item((long)7);
							str.oDocument = this._doc;
							d = this._gmeter.get_ID();
							str.Value = d.ToString();
							if (str.Save() == 0)
							{
								str = this._doc.oPDs.Add();
								str.oDocument = this._doc;
								d = this._mechanics[this.cmbMechanic.SelectedIndex].get_ID();
								str.Value = d.ToString();
								if (this._mechanics[this.cmbMechanic.SelectedIndex].oTypeAgent.get_ID() != (long)5)
								{
									str.oTypePD = Depot.oTypePDs.item((long)26);
								}
								else
								{
									str.oTypePD = Depot.oTypePDs.item((long)16);
								}
								if (str.Save() == 0)
								{
									str = this._doc.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)27);
									str.oDocument = this._doc;
									decimal value = this.numBeginIndication.Value;
									str.Value = value.ToString();
									if (str.Save() == 0)
									{
										str = this._doc.oPDs.Add();
										str.oTypePD = Depot.oTypePDs.item((long)29);
										str.oDocument = this._doc;
										value = this.numLastIndication.Value;
										str.Value = value.ToString();
										if (str.Save() == 0)
										{
											str = this._doc.oPDs.Add();
											str.oTypePD = Depot.oTypePDs.item((long)28);
											str.oDocument = this._doc;
											if (!this.checkBox1.Checked)
											{
												str.Value = "0";
											}
											else
											{
												this._gmeter.oStatusGMeter = Depot.oStatusGMeters.item((long)2);
												str.Value = "1";
											}
											if (str.Save() != 0)
											{
												this._doc.Delete();
												MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
											}
											else if (!this.checkBox1.Checked || this._gmeter.Save() == 0)
											{
												base.DialogResult = System.Windows.Forms.DialogResult.OK;
												base.Close();
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
						this.numLastIndication.Focus();
						return;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageBox.Show(string.Concat("Ошибка сохранения данных! ", exception.Message), "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
				this.cmbMechanic.Focus();
			}
		}

		private void dtDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (!Tools.IsFirstWorkDay((DateTime)this.dtDate.Value, true, Depot.CurrentPeriod.DateBegin))
				{
					this.numLastIndication.Enabled = true;
				}
				else
				{
					this.numLastIndication.Enabled = false;
				}
			}
			catch
			{
			}
		}

		private void frmActJob_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				long d = this._mechanics[this.cmbMechanic.SelectedIndex].get_ID();
				Tools.SaveParameter("mechanics", d.ToString());
			}
			catch
			{
			}
			this._mechanics = null;
			this._gobject = null;
			this._gmeter = null;
			this._indication = null;
			this._contract = null;
		}

		private void frmActJob_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				bool flag = false;
				string str = Tools.LoadParameter("mechanics", ref flag);
				if (str == "")
				{
					str = "0";
				}
				this.dtDate.Value = DateTime.Today.Date;
				this._mechanics = new Agents();
				Agents agent = this._mechanics;
				TypeAgent[] typeAgentArray = new TypeAgent[] { Depot.oTypeAgents.item((long)5), Depot.oTypeAgents.item((long)1) };
				agent.Load(typeAgentArray);
				Tools.FillC1(this._mechanics, this.cmbMechanic, Convert.ToInt64(str));
				if (this._doc.oContract != null)
				{
					this._contract = this._doc.oContract;
					this.txtAccount.Text = this._doc.oContract.Account;
					this.cmdAccount_Click(null, null);
					this.txtAccount.Enabled = false;
					this.cmdAccount.Enabled = false;
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmActJob));
			this.groupBox2 = new GroupBox();
			this.lblDisplay = new Label();
			this.lblPU = new Label();
			this.label2 = new Label();
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.label1 = new Label();
			this.numBeginIndication = new NumericUpDown();
			this.numLastIndication = new NumericUpDown();
			this.label3 = new Label();
			this.label4 = new Label();
			this.lblDifference = new Label();
			this.label7 = new Label();
			this.cmbMechanic = new C1Combo();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.checkBox1 = new CheckBox();
			this.txtNote = new TextBox();
			this.groupBox1 = new GroupBox();
			this.txtNumber = new C1TextBox();
			this.label5 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtAccount).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.numBeginIndication).BeginInit();
			((ISupportInitialize)this.numLastIndication).BeginInit();
			((ISupportInitialize)this.cmbMechanic).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.txtNumber).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.lblDisplay);
			this.groupBox2.Controls.Add(this.lblPU);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.txtAccount);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(332, 136);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Лицевой счет";
			this.lblDisplay.BackColor = SystemColors.Info;
			this.lblDisplay.BorderStyle = BorderStyle.FixedSingle;
			this.lblDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblDisplay.ForeColor = SystemColors.ControlText;
			this.lblDisplay.Location = new Point(48, 112);
			this.lblDisplay.Name = "lblDisplay";
			this.lblDisplay.Size = new System.Drawing.Size(280, 20);
			this.lblDisplay.TabIndex = 8;
			this.lblPU.BackColor = SystemColors.Info;
			this.lblPU.BorderStyle = BorderStyle.FixedSingle;
			this.lblPU.ForeColor = SystemColors.ControlText;
			this.lblPU.Location = new Point(48, 88);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(280, 20);
			this.lblPU.TabIndex = 7;
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "ПУ";
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
			this.lblAddress.Size = new System.Drawing.Size(280, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(48, 40);
			this.lblFIO.Name = "lblFIO";
			this.lblFIO.Size = new System.Drawing.Size(280, 20);
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
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(152, 40);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(176, 18);
			this.dtDate.TabIndex = 2;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.ValueChanged += new EventHandler(this.dtDate_ValueChanged);
			this.dtDate.KeyPress += new KeyPressEventHandler(this.dtDate_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 16);
			this.label6.TabIndex = 68;
			this.label6.Text = "Дата проверки:";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 16);
			this.label1.TabIndex = 69;
			this.label1.Text = "Начальные показания:";
			this.numBeginIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numBeginIndication.DecimalPlaces = 3;
			this.numBeginIndication.Location = new Point(152, 88);
			NumericUpDown num = this.numBeginIndication;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numBeginIndication.Name = "numBeginIndication";
			this.numBeginIndication.Size = new System.Drawing.Size(152, 20);
			this.numBeginIndication.TabIndex = 4;
			this.numBeginIndication.KeyPress += new KeyPressEventHandler(this.numBeginIndication_KeyPress);
			this.numBeginIndication.Enter += new EventHandler(this.numBeginIndication_Enter);
			this.numBeginIndication.Leave += new EventHandler(this.numBeginIndication_Leave);
			this.numLastIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numLastIndication.DecimalPlaces = 3;
			this.numLastIndication.Location = new Point(152, 112);
			NumericUpDown numericUpDown = this.numLastIndication;
			numArray = new int[] { 9999999, 0, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray);
			this.numLastIndication.Name = "numLastIndication";
			this.numLastIndication.Size = new System.Drawing.Size(152, 20);
			this.numLastIndication.TabIndex = 5;
			this.numLastIndication.KeyPress += new KeyPressEventHandler(this.numLastIndication_KeyPress);
			this.numLastIndication.Enter += new EventHandler(this.numLastIndication_Enter);
			this.numLastIndication.Leave += new EventHandler(this.numLastIndication_Leave);
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 16);
			this.label3.TabIndex = 71;
			this.label3.Text = "Конечные показания:";
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 16);
			this.label4.TabIndex = 73;
			this.label4.Text = "Разность, литр:";
			this.lblDifference.BackColor = SystemColors.Info;
			this.lblDifference.BorderStyle = BorderStyle.FixedSingle;
			this.lblDifference.ForeColor = SystemColors.ControlText;
			this.lblDifference.Location = new Point(152, 136);
			this.lblDifference.Name = "lblDifference";
			this.lblDifference.Size = new System.Drawing.Size(152, 20);
			this.lblDifference.TabIndex = 74;
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(8, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 32);
			this.label7.TabIndex = 75;
			this.label7.Text = "Выполнил (слесарь или контролер):";
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
			this.cmbMechanic.Location = new Point(152, 64);
			this.cmbMechanic.MatchEntryTimeout = (long)2000;
			this.cmbMechanic.MaxDropDownItems = 5;
			this.cmbMechanic.MaxLength = 32767;
			this.cmbMechanic.MouseCursor = Cursors.Default;
			this.cmbMechanic.Name = "cmbMechanic";
			this.cmbMechanic.RowDivider.Color = Color.DarkGray;
			this.cmbMechanic.RowDivider.Style = LineStyleEnum.None;
			this.cmbMechanic.RowSubDividerColor = Color.DarkGray;
			this.cmbMechanic.Size = new System.Drawing.Size(176, 19);
			this.cmbMechanic.TabIndex = 3;
			this.cmbMechanic.KeyPress += new KeyPressEventHandler(this.cmbMechanic_KeyPress);
			this.cmbMechanic.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(244, 384);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 4;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(144, 384);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.checkBox1.FlatStyle = FlatStyle.Flat;
			this.checkBox1.ForeColor = SystemColors.ControlText;
			this.checkBox1.Location = new Point(8, 160);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBox1.Size = new System.Drawing.Size(160, 16);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "Отключить ПУ";
			this.checkBox1.TextAlign = ContentAlignment.MiddleRight;
			this.checkBox1.KeyPress += new KeyPressEventHandler(this.checkBox1_KeyPress);
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 184);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(320, 32);
			this.txtNote.TabIndex = 7;
			this.txtNote.Text = "";
			this.toolTip1.SetToolTip(this.txtNote, "Примечание");
			this.txtNote.Enter += new EventHandler(this.txtNote_Enter);
			this.groupBox1.Controls.Add(this.txtNumber);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.numLastIndication);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lblDifference);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numBeginIndication);
			this.groupBox1.Controls.Add(this.cmbMechanic);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.dtDate);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.txtNote);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(4, 152);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(332, 224);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Выполненная работа";
			this.txtNumber.BorderStyle = 1;
			this.txtNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.txtNumber.Location = new Point(152, 16);
			this.txtNumber.Name = "txtNumber";
			this.txtNumber.Size = new System.Drawing.Size(176, 18);
			this.txtNumber.TabIndex = 1;
			this.txtNumber.Tag = null;
			this.txtNumber.KeyPress += new KeyPressEventHandler(this.txtNumber_KeyPress);
			this.txtNumber.Enter += new EventHandler(this.txtNumber_Enter);
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 16);
			this.label5.TabIndex = 83;
			this.label5.Text = "Номер акта:";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(338, 415);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmActJob";
			this.Text = "Акт проверки работы ПУ";
			base.Closing += new CancelEventHandler(this.frmActJob_Closing);
			base.Load += new EventHandler(this.frmActJob_Load);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.numBeginIndication).EndInit();
			((ISupportInitialize)this.numLastIndication).EndInit();
			((ISupportInitialize)this.cmbMechanic).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.txtNumber).EndInit();
			base.ResumeLayout(false);
		}

		private void numBeginIndication_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numBeginIndication);
		}

		private void numBeginIndication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numLastIndication.Focus();
			}
		}

		private void numBeginIndication_Leave(object sender, EventArgs e)
		{
			this.CalcDifference(true);
		}

		private void numLastIndication_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numLastIndication);
		}

		private void numLastIndication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.checkBox1.Focus();
			}
		}

		private void numLastIndication_Leave(object sender, EventArgs e)
		{
			this.CalcDifference(false);
			this.FactAmount = -1;
			if (this.numLastIndication.Value > new decimal(0) && this._indication != null)
			{
				string str = "";
				try
				{
					this._indication.oGmeter = this._gmeter;
					this._indication.oTypeIndication = Depot.oTypeIndications.item((long)3);
					this._indication.Datedisplay = (DateTime)this.dtDate.Value;
					this._indication.Display = Convert.ToDouble(this.numLastIndication.Value);
					str = this._indication.CalcFactUse();
					this.FactAmount = Convert.ToDouble(str);
				}
				catch
				{
					MessageBox.Show(string.Concat("Показанаия не будут сохранены в БД \n", str), "Показания прибора учета", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.FactAmount = -1;
				}
			}
			if (this.numLastIndication.Value == new decimal(0))
			{
				this.FactAmount = 0;
			}
		}

		private void ResetFields1()
		{
			this.groupBox1.Enabled = false;
			this.cmdOK.Enabled = false;
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblPU.Text = "";
			this.lblDisplay.Text = "";
			this._gobject = null;
			this._gmeter = null;
			this._contract = null;
			this._indication = null;
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
		}

		private void txtNote_Enter(object sender, EventArgs e)
		{
			this.txtNote.SelectAll();
		}

		private void txtNumber_Enter(object sender, EventArgs e)
		{
			this.txtNumber.SelectAll();
		}

		private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDate.Focus();
			}
		}
	}
}