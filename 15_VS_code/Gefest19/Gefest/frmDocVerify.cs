using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmDocVerify : Form
	{
		private Label label6;

		private Label label1;

		private IContainer components;

		private Button cmdClose;

		private Button cmdOK;

		private Label label7;

		private Label label8;

		private ToolTip toolTip1;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private Button cmdAddress;

		private TextBox txtAccount;

		private Button cmdAgent;

		private ComboBox cmbAgent;

		private Label label14;

		private Label label2;

		private ComboBox cmbGMeter;

		private Label label3;

		private NumericUpDown numEndIndication;

		private NumericUpDown numBeginIndication;

		private GroupBox groupBox1;

		private TextBox txtNumber;

		private CheckBox checkGMeter;

		private GroupBox groupBox2;

		private DateTimePicker dtDate;

		private TextBox txtNote;

		private Document _doc;

		private Contract _contract;

		private Agents _agents;

		private PD _pd30;

		private PD _pd27;

		private PD _pd32;

		private PD _pd7;

		private PD _pd16;

		private long IDGMeter;

		public frmDocVerify(Document oDocument)
		{
			this.InitializeComponent();
			this._doc = oDocument;
		}

		private void AccountCheck(Contract oContract, long IDGMeter)
		{
			try
			{
				if (oContract != null)
				{
					this._contract = oContract;
				}
				else if (this.txtAccount.Text.Length != 0)
				{
					Contracts contract = new Contracts();
					if (contract.Load(this.txtAccount.Text.Trim()) != 0)
					{
						this.ResetFields();
						return;
					}
					else if (contract.get_Count() <= 0)
					{
						this.ResetFields();
						return;
					}
					else
					{
						this._contract = contract[0];
					}
				}
				else
				{
					this.ResetFields();
					return;
				}
				this.txtAccount.Text = this._contract.Account;
				this.lblFIO.Text = this._contract.oPerson.FullName;
				this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
				Tools.FillCMB(this._contract.oGobjects[0].oGmeters, this.cmbGMeter, IDGMeter);
			}
			catch
			{
			}
		}

		private void checkGMeter_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkGMeter.Checked)
			{
				this.label1.Text = "Номер сертификата:";
				return;
			}
			this.label1.Text = "Номер извещения о непригодности:";
		}

		private void cmdAddress_Click(object sender, EventArgs e)
		{
			try
			{
				frmAddress _frmAddress = new frmAddress();
				if (this._contract != null)
				{
					_frmAddress.oAddress = this._contract.oPerson.oAddress;
				}
				_frmAddress.ShowDialog(this);
				if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
				{
					Gobjects gobject = new Gobjects();
					if (gobject.Load(_frmAddress.oAddress) != 0)
					{
						this.ResetFields();
						return;
					}
					else if (gobject.get_Count() <= 0)
					{
						this.ResetFields();
						return;
					}
					else
					{
						this._contract = gobject[0].oContract;
						this.AccountCheck(this._contract, (long)0);
					}
				}
				_frmAddress = null;
			}
			catch
			{
			}
		}

		private void cmdAgent_Click(object sender, EventArgs e)
		{
			try
			{
				Agents agent = this._agents;
				string[] strArrays = new string[] { "ФИО/Название" };
				string[] strArrays1 = strArrays;
				int[] numArray = new int[] { 300 };
				strArrays = new string[] { "Name" };
				Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
				frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник поверителей", agent, strArrays1, numArray, strArrays, typeArray);
				frmSimpleObj.ShowDialog(this);
				this._agents = new Agents();
				this._agents.Load(Depot.oTypeAgents.item((long)1));
				if (frmSimpleObj.lvData.SelectedItems.Count <= 0)
				{
					Tools.FillCMB(this._agents, this.cmbAgent, (long)0);
				}
				else
				{
					Tools.FillCMB(this._agents, this.cmbAgent, Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
				}
				frmSimpleObj = null;
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
			PD pD;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract == null)
					{
						MessageBox.Show("Не указан лицевой счет!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.txtAccount.Focus();
						return;
					}
					else if (this.cmbGMeter.SelectedIndex == -1)
					{
						MessageBox.Show("Не указан прибор учета!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbGMeter.Focus();
						return;
					}
					else if (this._doc.get_ID() == (long)0 && this._contract.oGobjects[0].oGmeters[this.cmbGMeter.SelectedIndex].oTypeVerify.get_ID() == (long)2)
					{
						MessageBox.Show("Указан забракованный прибор учета!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbGMeter.Focus();
						return;
					}
					else if (this.cmbAgent.SelectedIndex == -1)
					{
						MessageBox.Show("Не указан исполнитель!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbAgent.Focus();
						return;
					}
					else if (this.numBeginIndication.Value > this.numEndIndication.Value)
					{
						MessageBox.Show("Не верно указаны показания прибора учета!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.numBeginIndication.Focus();
						return;
					}
					else if (this.txtNumber.Text.Trim().Length != 0)
					{
						bool flag = false;
						long d = this._doc.get_ID();
						if (this._doc.get_ID() == (long)0)
						{
							flag = true;
						}
						this._doc.oContract = this._contract;
						this._doc.oPeriod = Depot.CurrentPeriod;
						this._doc.oTypeDocument = Depot.oTypeDocuments.item((long)22);
						this._doc.DocumentDate = this.dtDate.Value;
						this._doc.DocumentNumber = this.txtNumber.Text;
						this._doc.DocumentAmount = Convert.ToDouble(this.numBeginIndication.Value);
						this._doc.Note = this.txtNote.Text;
						if (this._doc.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						else
						{
							if (this._pd30 == null)
							{
								pD = this._doc.oPDs.Add();
								pD.oTypePD = Depot.oTypePDs.item((long)30);
								pD.oDocument = this._doc;
							}
							else
							{
								pD = this._pd30;
							}
							if (!this.checkGMeter.Checked)
							{
								pD.set_Name("2");
							}
							else
							{
								pD.set_Name("1");
							}
							if (pD.Save() == 0)
							{
								if (this._pd27 == null)
								{
									pD = this._doc.oPDs.Add();
									pD.oTypePD = Depot.oTypePDs.item((long)27);
									pD.oDocument = this._doc;
								}
								else
								{
									pD = this._pd27;
								}
								pD.set_Name(this.numEndIndication.Value.ToString());
								if (pD.Save() == 0)
								{
									if (this._pd7 == null)
									{
										pD = this._doc.oPDs.Add();
										pD.oTypePD = Depot.oTypePDs.item((long)7);
										pD.oDocument = this._doc;
									}
									else
									{
										pD = this._pd7;
									}
									long num = this._contract.oGobjects[0].oGmeters[this.cmbGMeter.SelectedIndex].get_ID();
									pD.set_Name(num.ToString());
									if (pD.Save() == 0)
									{
										if (this._pd16 == null)
										{
											pD = this._doc.oPDs.Add();
											pD.oTypePD = Depot.oTypePDs.item((long)16);
											pD.oDocument = this._doc;
										}
										else
										{
											pD = this._pd16;
										}
										num = this._agents[this.cmbAgent.SelectedIndex].get_ID();
										pD.set_Name(num.ToString());
										if (pD.Save() == 0)
										{
											if (d == (long)0)
											{
												if (this._pd32 == null)
												{
													pD = this._doc.oPDs.Add();
													pD.oTypePD = Depot.oTypePDs.item((long)32);
													pD.oDocument = this._doc;
												}
												else
												{
													pD = this._pd32;
												}
												DateTime dateVerify = this._contract.oGobjects[0].oGmeters[this.cmbGMeter.SelectedIndex].DateVerify;
												pD.set_Name(dateVerify.ToShortDateString());
												if (pD.Save() != 0)
												{
													if (flag)
													{
														this._doc.Delete();
													}
													MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
													return;
												}
											}
											this._contract.oGobjects[0].oGmeters[this.cmbGMeter.SelectedIndex].DateVerify = this.dtDate.Value;
											if (this.checkGMeter.Checked)
											{
												this._contract.oGobjects[0].oGmeters[this.cmbGMeter.SelectedIndex].oTypeVerify = Depot.oTypeVerifys.item((long)1);
											}
											else
											{
												this._contract.oGobjects[0].oGmeters[this.cmbGMeter.SelectedIndex].oTypeVerify = Depot.oTypeVerifys.item((long)2);
											}
											if (this._contract.oGobjects[0].oGmeters[this.cmbGMeter.SelectedIndex].Save() == 0)
											{
												base.DialogResult = System.Windows.Forms.DialogResult.OK;
												base.Close();
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
					}
					else
					{
						string str = this.label1.Text.Remove(this.label1.Text.Length - 1, 1);
						MessageBox.Show(string.Concat("Не указан ", str, "!"), "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.txtNumber.Focus();
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

		private void frmDocVerify_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmDocVerify_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				this.IDGMeter = (long)0;
				this._agents = new Agents();
				this._agents.Load(Depot.oTypeAgents.item((long)5));
				if (this._doc.get_ID() == (long)0)
				{
					this.dtDate.Value = DateTime.Today.Date;
					Tools.FillCMB(this._agents, this.cmbAgent, (long)0);
				}
				else
				{
					this._pd7 = this._doc.GetPD(7);
					this.IDGMeter = Convert.ToInt64(this._pd7.Value);
					this.AccountCheck(this._doc.oContract, this.IDGMeter);
					this.txtAccount.Enabled = false;
					this.cmbGMeter.Enabled = false;
					this.cmdAddress.Enabled = false;
					this.dtDate.Value = this._doc.DocumentDate;
					this.numBeginIndication.Value = Convert.ToDecimal(this._doc.DocumentAmount);
					this._pd27 = this._doc.GetPD(27);
					this.numEndIndication.Value = Convert.ToDecimal(this._pd27.Value.Replace(".", ","));
					this._pd30 = this._doc.GetPD(30);
					if (Convert.ToInt16(this._pd30.get_Name()) != 1)
					{
						this.checkGMeter.Checked = false;
					}
					else
					{
						this.checkGMeter.Checked = true;
					}
					this.txtNumber.Text = this._doc.DocumentNumber;
					this.txtNote.Text = this._doc.Note;
					this._pd32 = this._doc.GetPD(32);
					this._pd16 = this._doc.GetPD(16);
					Tools.FillCMB(this._agents, this.cmbAgent, Convert.ToInt64(this._pd16.Value));
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmDocVerify));
			this.label6 = new Label();
			this.label1 = new Label();
			this.txtNumber = new TextBox();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.cmdAgent = new Button();
			this.label7 = new Label();
			this.txtNote = new TextBox();
			this.label8 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.cmdAddress = new Button();
			this.dtDate = new DateTimePicker();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.txtAccount = new TextBox();
			this.cmbAgent = new ComboBox();
			this.numEndIndication = new NumericUpDown();
			this.label14 = new Label();
			this.numBeginIndication = new NumericUpDown();
			this.label2 = new Label();
			this.cmbGMeter = new ComboBox();
			this.label3 = new Label();
			this.groupBox1 = new GroupBox();
			this.checkGMeter = new CheckBox();
			this.groupBox2 = new GroupBox();
			((ISupportInitialize)this.numEndIndication).BeginInit();
			((ISupportInitialize)this.numBeginIndication).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(176, 24);
			this.label6.TabIndex = 61;
			this.label6.Text = "Дата поверки прибора учета:";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 120);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(196, 16);
			this.label1.TabIndex = 62;
			this.label1.Text = "Номер извещения о непригодности:";
			this.txtNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtNumber.Location = new Point(208, 116);
			this.txtNumber.Name = "txtNumber";
			this.txtNumber.Size = new System.Drawing.Size(136, 20);
			this.txtNumber.TabIndex = 6;
			this.txtNumber.Text = "";
			this.txtNumber.Enter += new EventHandler(this.txtNumber_Enter);
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(268, 352);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 9;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(172, 352);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 8;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmdAgent.FlatStyle = FlatStyle.Flat;
			this.cmdAgent.ForeColor = SystemColors.ControlText;
			this.cmdAgent.Image = (Image)resourceManager.GetObject("cmdAgent.Image");
			this.cmdAgent.Location = new Point(324, 20);
			this.cmdAgent.Name = "cmdAgent";
			this.cmdAgent.Size = new System.Drawing.Size(20, 20);
			this.cmdAgent.TabIndex = 2;
			this.toolTip1.SetToolTip(this.cmdAgent, "Справочник исполнителей");
			this.cmdAgent.Click += new EventHandler(this.cmdAgent_Click);
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(8, 20);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 16);
			this.label7.TabIndex = 81;
			this.label7.Text = "Поверитель:";
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(100, 296);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.ScrollBars = ScrollBars.Vertical;
			this.txtNote.Size = new System.Drawing.Size(252, 32);
			this.txtNote.TabIndex = 7;
			this.txtNote.Text = "";
			this.txtNote.Enter += new EventHandler(this.txtNote_Enter);
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(16, 300);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(76, 16);
			this.label8.TabIndex = 91;
			this.label8.Text = "Примечание:";
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.Image = (Image)resourceManager.GetObject("cmdAddress.Image");
			this.cmdAddress.Location = new Point(324, 68);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(20, 20);
			this.cmdAddress.TabIndex = 3;
			this.toolTip1.SetToolTip(this.cmdAddress, "Справочник адресов");
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.dtDate.Location = new Point(208, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 20);
			this.dtDate.TabIndex = 1;
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(48, 68);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(272, 20);
			this.lblAddress.TabIndex = 100;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(48, 44);
			this.lblFIO.Name = "lblFIO";
			this.lblFIO.Size = new System.Drawing.Size(296, 20);
			this.lblFIO.TabIndex = 99;
			this.label10.ForeColor = SystemColors.ControlText;
			this.label10.Location = new Point(8, 68);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 16);
			this.label10.TabIndex = 96;
			this.label10.Text = "Адрес:";
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label11.ForeColor = SystemColors.ControlText;
			this.label11.Location = new Point(8, 44);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 16);
			this.label11.TabIndex = 95;
			this.label11.Text = "ФИО:";
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			this.label12.ForeColor = SystemColors.ControlText;
			this.label12.Location = new Point(8, 20);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(80, 16);
			this.label12.TabIndex = 94;
			this.label12.Text = "Лицевой счет:";
			this.txtAccount.BorderStyle = BorderStyle.FixedSingle;
			this.txtAccount.Location = new Point(88, 20);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(136, 20);
			this.txtAccount.TabIndex = 1;
			this.txtAccount.Text = "";
			this.txtAccount.KeyPress += new KeyPressEventHandler(this.txtAccount_KeyPress);
			this.txtAccount.Leave += new EventHandler(this.txtAccount_Leave);
			this.txtAccount.Enter += new EventHandler(this.txtAccount_Enter);
			this.cmbAgent.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbAgent.Location = new Point(88, 20);
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.Size = new System.Drawing.Size(232, 21);
			this.cmbAgent.TabIndex = 1;
			this.numEndIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numEndIndication.DecimalPlaces = 3;
			this.numEndIndication.Location = new Point(180, 72);
			NumericUpDown num = this.numEndIndication;
			int[] numArray = new int[] { 9999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numEndIndication.Name = "numEndIndication";
			this.numEndIndication.Size = new System.Drawing.Size(164, 20);
			this.numEndIndication.TabIndex = 4;
			this.numEndIndication.Enter += new EventHandler(this.numEndIndication_Enter);
			this.label14.ForeColor = SystemColors.ControlText;
			this.label14.Location = new Point(8, 72);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(160, 16);
			this.label14.TabIndex = 106;
			this.label14.Text = "Показания на конец поверки:";
			this.numBeginIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numBeginIndication.DecimalPlaces = 3;
			this.numBeginIndication.Location = new Point(180, 48);
			NumericUpDown numericUpDown = this.numBeginIndication;
			numArray = new int[] { 9999, 0, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray);
			this.numBeginIndication.Name = "numBeginIndication";
			this.numBeginIndication.Size = new System.Drawing.Size(164, 20);
			this.numBeginIndication.TabIndex = 3;
			this.numBeginIndication.Enter += new EventHandler(this.numBeginIndication_Enter);
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(168, 16);
			this.label2.TabIndex = 108;
			this.label2.Text = "Показания на начало поверки:";
			this.cmbGMeter.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbGMeter.Location = new Point(88, 92);
			this.cmbGMeter.Name = "cmbGMeter";
			this.cmbGMeter.Size = new System.Drawing.Size(256, 21);
			this.cmbGMeter.TabIndex = 4;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 110;
			this.label3.Text = "Прибор учета:";
			this.groupBox1.Controls.Add(this.cmdAddress);
			this.groupBox1.Controls.Add(this.txtAccount);
			this.groupBox1.Controls.Add(this.lblAddress);
			this.groupBox1.Controls.Add(this.lblFIO);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.cmbGMeter);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new Point(8, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 124);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Поверяется прибор учета ";
			this.checkGMeter.FlatStyle = FlatStyle.Flat;
			this.checkGMeter.Location = new Point(8, 96);
			this.checkGMeter.Name = "checkGMeter";
			this.checkGMeter.Size = new System.Drawing.Size(184, 16);
			this.checkGMeter.TabIndex = 5;
			this.checkGMeter.Text = "Прибор учета поверку прошел";
			this.checkGMeter.CheckedChanged += new EventHandler(this.checkGMeter_CheckedChanged);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.numBeginIndication);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.numEndIndication);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.cmbAgent);
			this.groupBox2.Controls.Add(this.cmdAgent);
			this.groupBox2.Controls.Add(this.checkGMeter);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.txtNumber);
			this.groupBox2.Location = new Point(8, 156);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(352, 180);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Выполненная работа";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(366, 383);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.label6);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmDocVerify";
			this.Text = "Поверка прибора учета";
			base.Closing += new CancelEventHandler(this.frmDocVerify_Closing);
			base.Load += new EventHandler(this.frmDocVerify_Load);
			((ISupportInitialize)this.numEndIndication).EndInit();
			((ISupportInitialize)this.numBeginIndication).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void numBeginIndication_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numBeginIndication);
		}

		private void numEndIndication_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numEndIndication);
		}

		private void ResetFields()
		{
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this._contract = null;
		}

		private void txtAccount_Enter(object sender, EventArgs e)
		{
			this.txtAccount.SelectAll();
		}

		private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.AccountCheck(null, (long)0);
			}
		}

		private void txtAccount_Leave(object sender, EventArgs e)
		{
			this.AccountCheck(null, (long)0);
		}

		private void txtNote_Enter(object sender, EventArgs e)
		{
			this.txtNote.SelectAll();
		}

		private void txtNumber_Enter(object sender, EventArgs e)
		{
			this.txtNumber.SelectAll();
		}
	}
}