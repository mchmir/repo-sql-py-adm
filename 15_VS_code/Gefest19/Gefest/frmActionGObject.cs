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
	public class frmActionGObject : Form
	{
		private GroupBox groupBox2;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private ImageList imageList1;

		private IContainer components;

		private GroupBox groupBox1;

		private Label label5;

		private Label label6;

		private C1DateEdit dtDate;

		private TextBox txtNote;

		private Label label17;

		private Button cmdClose;

		private Button cmdOK;

		private C1Combo cmbMechanic;

		private C1Combo cmbTypeEnd;

		private Label lblTypeEnd;

		private Label lblAccount;

		private C1TextBox txtCountLives;

		private Label label13;

		private TextBox txtInvNumber;

		private Button cmdGRU;

		private Label lblNameGRU;

		private Label label4;

		private Button cmdAddress;

		private Label lblAddress2;

		private Label label3;

		private Button cmdTypeGObject;

		private C1Combo cmbTypeGObject;

		private Label label1;

		private Label label7;

		private C1Combo cmbOU;

		private GroupBox grNewOU;

		private Gobject _gobject;

		private Agents _agents;

		private TariffConnectionGobject _TariffConnectionGobject;

		private TariffDisconnectionGobject _TariffDisconnectionGobject;

		private TariffDisconnectionGobjectStakan _TariffDisconnectionGobjectStakan;

		private TypeGobjects _typegobjects;

		private CheckBox chkBox;

		private Gobjects _gobjects;

		private Accountings _Accountings;

		public frmActionGObject(Gobject oGobject)
		{
			this.InitializeComponent();
			this._gobject = oGobject;
		}

		private void chkBox_CheckedChanged(object sender, EventArgs e)
		{
			double value;
			if (!this.chkBox.Checked)
			{
				this.chkBox.ForeColor = Color.Blue;
				return;
			}
			this.chkBox.ForeColor = Color.Red;
			if (this._gobject.oStatusGObject.get_ID() == (long)2)
			{
				CheckBox checkBox = this.chkBox;
				value = this._TariffConnectionGobject.Value;
				checkBox.Text = string.Concat("Начислить за подк. ", value.ToString(), " тг.");
				return;
			}
			if (Depot.oTypeEnds[this.cmbTypeEnd.SelectedIndex].get_ID() == (long)3)
			{
				CheckBox checkBox1 = this.chkBox;
				value = this._TariffDisconnectionGobjectStakan.Value;
				checkBox1.Text = string.Concat("Начислить за отк. ", value.ToString(), " тг.");
				return;
			}
			CheckBox checkBox2 = this.chkBox;
			value = this._TariffDisconnectionGobject.Value;
			checkBox2.Text = string.Concat("Начислить за отк. ", value.ToString(), " тг.");
		}

		private void cmbOU_TextChanged(object sender, EventArgs e)
		{
			if (this.cmbOU.SelectedIndex != 0)
			{
				if (this.cmbOU.SelectedIndex > 0)
				{
					this._gobject = this._gobjects[this.cmbOU.SelectedIndex];
				}
				this.grNewOU.Enabled = false;
				return;
			}
			this.grNewOU.Enabled = true;
			if (this._typegobjects == null)
			{
				this._typegobjects = new TypeGobjects();
				this._typegobjects.Load();
			}
			Tools.FillC1(this._typegobjects, this.cmbTypeGObject, (long)1);
			this.lblAddress2.Text = this._gobject.oAddress.get_ShortAddress();
		}

		private void cmbTypeEnd_TextChanged(object sender, EventArgs e)
		{
			double value;
			if (this.chkBox.Checked)
			{
				if (this._gobject.oStatusGObject.get_ID() == (long)2)
				{
					CheckBox checkBox = this.chkBox;
					value = this._TariffConnectionGobject.Value;
					checkBox.Text = string.Concat("Начислить за подк. ", value.ToString(), " тг.");
					return;
				}
				if (Depot.oTypeEnds[this.cmbTypeEnd.SelectedIndex].get_ID() == (long)3)
				{
					CheckBox checkBox1 = this.chkBox;
					value = this._TariffDisconnectionGobjectStakan.Value;
					checkBox1.Text = string.Concat("Начислить за отк. ", value.ToString(), " тг.");
					return;
				}
				CheckBox checkBox2 = this.chkBox;
				value = this._TariffDisconnectionGobject.Value;
				checkBox2.Text = string.Concat("Начислить за отк. ", value.ToString(), " тг.");
			}
		}

		private void cmdAddress_Click(object sender, EventArgs e)
		{
			frmAddress _frmAddress = new frmAddress()
			{
				oAddress = this._gobject.oAddress
			};
			_frmAddress.ShowDialog(this);
			if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				this._gobject.oAddress = _frmAddress.oAddress;
				this.lblAddress2.Text = this._gobject.oAddress.get_ShortAddress();
			}
			_frmAddress = null;
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdGRU_Click(object sender, EventArgs e)
		{
			GRUs gRU = new GRUs();
			gRU.Load();
			string[] strArrays = new string[] { "Номер", "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 100, 300 };
			strArrays = new string[] { "InvNumber", "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник РУ", gRU, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			if (frmSimpleObj.lvData.SelectedItems.Count > 0)
			{
				this._gobject.oGRU = gRU.item(Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
				this.txtInvNumber.Text = this._gobject.oGRU.InvNumber;
				this.txtInvNumber.ForeColor = SystemColors.WindowText;
				this.lblNameGRU.Text = this._gobject.oGRU.get_Name();
			}
			frmSimpleObj = null;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			long d;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this.cmbMechanic.SelectedIndex == -1)
					{
						MessageBox.Show("Не выбран слесарь!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					else if (this._gobject.oStatusGObject.get_ID() != (long)1 || this.cmbTypeEnd.SelectedIndex != -1)
					{
						if (this.grNewOU.Enabled)
						{
							if (this.cmbTypeGObject.SelectedIndex == -1)
							{
								MessageBox.Show("Не выбран тип ОУ!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
							else if (this._gobject.oGRU == null || this._gobject.oAddress == null)
							{
								MessageBox.Show("Необходимо указать РУ, адрес!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
							else
							{
								this._gobject.oTypeGobject = this._typegobjects[this.cmbTypeGObject.SelectedIndex];
								Gobject gobject = this._gobject;
								int count = this._gobject.oContract.oGobjects.get_Count();
								gobject.Name = string.Concat("Квартира", count.ToString());
								this._gobject.Memo = this.txtNote.Text;
								this._gobject.CountLives = Convert.ToInt32(this.txtCountLives.Text);
								if (this._gobject.Save() != 0)
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
						}
						Document document = new Document()
						{
							oBatch = null,
							oContract = this._gobject.oContract,
							oPeriod = Depot.CurrentPeriod
						};
						if (this._gobject.oStatusGObject.get_ID() != (long)2)
						{
							document.oTypeDocument = Depot.oTypeDocuments.item((long)17);
						}
						else
						{
							document.oTypeDocument = Depot.oTypeDocuments.item((long)6);
						}
						document.DocumentDate = (DateTime)this.dtDate.Value;
						document.Note = this.txtNote.Text;
						if (document.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							if (this._gobject.oStatusGObject.get_ID() != (long)2)
							{
								this._gobject.oStatusGObject = Depot.oStatusGObjects.item((long)2);
							}
							else
							{
								this._gobject.oStatusGObject = Depot.oStatusGObjects.item((long)1);
							}
							if (this._gobject.Save() == 0)
							{
								PD str = document.oPDs.Add();
								if (this._gobject.oStatusGObject.get_ID() == (long)2)
								{
									str.oTypePD = Depot.oTypePDs.item((long)14);
									str.oDocument = document;
									d = Depot.oTypeEnds[this.cmbTypeEnd.SelectedIndex].get_ID();
									str.Value = d.ToString();
									if (str.Save() != 0)
									{
										document.Delete();
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
								}
								str = document.oPDs.Add();
								str.oTypePD = Depot.oTypePDs.item((long)15);
								str.oDocument = document;
								d = this._gobject.get_ID();
								str.Value = d.ToString();
								if (str.Save() == 0)
								{
									str = document.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)16);
									str.oDocument = document;
									d = this._agents[this.cmbMechanic.SelectedIndex].get_ID();
									str.Value = d.ToString();
									if (str.Save() == 0)
									{
										base.Close();
									}
									else
									{
										document.Delete();
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
								}
								else
								{
									document.Delete();
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							else
							{
								document.Delete();
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
						if (this.grNewOU.Enabled)
						{
							Document document1 = new Document()
							{
								oBatch = null,
								oContract = this._gobject.oContract,
								oPeriod = Depot.CurrentPeriod,
								oTypeDocument = Depot.oTypeDocuments.item((long)2),
								DocumentAmount = Convert.ToDouble(this.txtCountLives.Text),
								DocumentDate = (DateTime)this.dtDate.Value,
								Note = this.txtNote.Text
							};
							if (document1.Save() != 0)
							{
								document.Delete();
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
							else
							{
								PD pD = document1.oPDs.Add();
								pD.oTypePD = Depot.oTypePDs.item((long)5);
								pD.oDocument = document1;
								pD.Value = "0";
								if (pD.Save() == 0)
								{
									base.Close();
									return;
								}
								else
								{
									document.Delete();
									document1.Delete();
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
						}
					}
					else
					{
						MessageBox.Show("Не выбран тип окончания!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

		private void cmdTypeGObject_Click(object sender, EventArgs e)
		{
			TypeGobjects typeGobject = this._typegobjects;
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник объектов учета", typeGobject, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			this._typegobjects = new TypeGobjects();
			this._typegobjects.Load();
			if (frmSimpleObj.lvData.SelectedItems.Count <= 0)
			{
				Tools.FillC1(this._typegobjects, this.cmbTypeGObject, (long)1);
			}
			else
			{
				Tools.FillC1(this._typegobjects, this.cmbTypeGObject, Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
			}
			frmSimpleObj = null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void FillC1WhithAll(Gobjects oObjects, C1Combo cmb, long IdSelected, string strAll)
		{
			try
			{
				cmb.ClearItems();
				cmb.AddItem(strAll);
				for (int i = 0; i < oObjects.get_Count(); i++)
				{
					if (oObjects[i].oStatusGObject.get_ID() != (long)1)
					{
						cmb.AddItem(string.Concat(oObjects[i].oTypeGobject.Name, ", отключен, ", oObjects[i].oAddress.get_ShortAddress()));
					}
					else
					{
						cmb.AddItem(string.Concat(oObjects[i].oTypeGobject.Name, ", подключен, ", oObjects[i].oAddress.get_ShortAddress()));
					}
					oObjects[i].get_ID();
				}
				cmb.SelectedIndex = 0;
				cmb.ColumnWidth = cmb.Width - cmb.VScrollBar.Width;
			}
			catch (Exception exception)
			{
				cmb.Text = string.Concat("Ошибка загрузки справочника ", exception.Message);
			}
		}

		private void frmActionGObject_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
			this._agents = null;
			this._gobject = null;
			this._gobjects = null;
			this._typegobjects = null;
		}

		private void frmActionGObject_Load(object sender, EventArgs e)
		{
			double value;
			try
			{
				if (this._gobject == null)
				{
					base.Close();
				}
				else if (this._gobject.oContract != null)
				{
					this._TariffConnectionGobject = new TariffConnectionGobject();
					this._TariffConnectionGobject.LoadLast();
					this._TariffDisconnectionGobject = new TariffDisconnectionGobject();
					this._TariffDisconnectionGobject.LoadLast();
					this._TariffDisconnectionGobjectStakan = new TariffDisconnectionGobjectStakan();
					this._TariffDisconnectionGobjectStakan.LoadLast();
					this.dtDate.Value = DateTime.Today.Date;
					this.lblAccount.Text = this._gobject.oContract.Account;
					this.lblFIO.Text = this._gobject.oContract.oPerson.FullName;
					this.lblAddress.Text = this._gobject.oContract.oPerson.oAddress.get_ShortAddress();
					this.dtDate.Value = DateTime.Today;
					this._agents = new Agents();
					this._agents.Load(Depot.oTypeAgents.item((long)5));
					Tools.FillC1(this._agents, this.cmbMechanic, (long)0);
					if (!this._gobject.get_isNew())
					{
						if (this._gobject.oStatusGObject.get_ID() != (long)1)
						{
							this.cmbOU.Text = string.Concat(this._gobject.oTypeGobject.Name, ", отключен, ", this._gobject.oAddress.get_ShortAddress());
							this.Text = "Подключение объекта учета";
							CheckBox checkBox = this.chkBox;
							value = this._TariffConnectionGobject.Value;
							checkBox.Text = string.Concat("Начислить за подк. ", value.ToString(), " тг.");
							this.lblTypeEnd.Visible = false;
							this.cmbTypeEnd.Visible = false;
						}
						else
						{
							this.cmbOU.Text = string.Concat(this._gobject.oTypeGobject.Name, ", подключен, ", this._gobject.oAddress.get_ShortAddress());
							CheckBox checkBox1 = this.chkBox;
							value = this._TariffDisconnectionGobject.Value;
							checkBox1.Text = string.Concat("Начислить за отк. ", value.ToString(), " тг.");
						}
						this.cmbOU.Enabled = false;
					}
					else
					{
						this._gobject.oStatusGObject = Depot.oStatusGObjects.item((long)2);
						this._gobjects = new Gobjects();
						this._gobjects.Load(this._gobject.oContract, Depot.oStatusGObjects.item((long)2));
						this.FillC1WhithAll(this._gobjects, this.cmbOU, (long)0, "Новый ОУ");
						this.Text = "Подключение объекта учета";
						this.lblTypeEnd.Visible = false;
						this.cmbTypeEnd.Visible = false;
					}
					if (this._gobject.oStatusGObject.get_ID() == (long)1)
					{
						Tools.FillC1(Depot.oTypeEnds, this.cmbTypeEnd, (long)0);
					}
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

		private void GetGRU()
		{
			GRUs gRU = new GRUs();
			gRU.Load();
			this._gobject.oGRU = null;
			foreach (GRU gRU1 in gRU)
			{
				if (gRU1.InvNumber != this.txtInvNumber.Text.Trim())
				{
					continue;
				}
				this._gobject.oGRU = gRU1;
				this.txtInvNumber.Text = this._gobject.oGRU.InvNumber;
				this.txtInvNumber.ForeColor = SystemColors.WindowText;
				this.lblNameGRU.Text = this._gobject.oGRU.get_Name();
				return;
			}
			this.lblNameGRU.Text = "Укажите номер РУ";
			this.txtInvNumber.ForeColor = Color.Red;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmActionGObject));
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.imageList1 = new ImageList(this.components);
			this.groupBox1 = new GroupBox();
			this.chkBox = new CheckBox();
			this.cmbTypeEnd = new C1Combo();
			this.dtDate = new C1DateEdit();
			this.cmbMechanic = new C1Combo();
			this.lblTypeEnd = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.grNewOU = new GroupBox();
			this.cmdTypeGObject = new Button();
			this.cmbTypeGObject = new C1Combo();
			this.label1 = new Label();
			this.cmdAddress = new Button();
			this.lblAddress2 = new Label();
			this.label3 = new Label();
			this.txtInvNumber = new TextBox();
			this.cmdGRU = new Button();
			this.lblNameGRU = new Label();
			this.label4 = new Label();
			this.txtCountLives = new C1TextBox();
			this.label13 = new Label();
			this.label7 = new Label();
			this.cmbOU = new C1Combo();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmbTypeEnd).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.cmbMechanic).BeginInit();
			this.grNewOU.SuspendLayout();
			((ISupportInitialize)this.cmbTypeGObject).BeginInit();
			((ISupportInitialize)this.txtCountLives).BeginInit();
			((ISupportInitialize)this.cmbOU).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.lblAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(564, 64);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.lblAccount.BackColor = SystemColors.Info;
			this.lblAccount.BorderStyle = BorderStyle.FixedSingle;
			this.lblAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblAccount.ForeColor = SystemColors.ControlText;
			this.lblAccount.Location = new Point(48, 16);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(112, 20);
			this.lblAccount.TabIndex = 7;
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(320, 40);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(240, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(48, 40);
			this.lblFIO.Name = "lblFIO";
			this.lblFIO.Size = new System.Drawing.Size(224, 20);
			this.lblFIO.TabIndex = 4;
			this.label10.ForeColor = SystemColors.ControlText;
			this.label10.Location = new Point(280, 40);
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
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.chkBox);
			this.groupBox1.Controls.Add(this.cmbTypeEnd);
			this.groupBox1.Controls.Add(this.dtDate);
			this.groupBox1.Controls.Add(this.cmbMechanic);
			this.groupBox1.Controls.Add(this.lblTypeEnd);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(288, 96);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(280, 112);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Выполненная работа";
			this.chkBox.Location = new Point(8, 88);
			this.chkBox.Name = "chkBox";
			this.chkBox.Size = new System.Drawing.Size(256, 16);
			this.chkBox.TabIndex = 4;
			this.chkBox.Text = "Начислить за под/отк";
			this.chkBox.Visible = false;
			this.chkBox.CheckedChanged += new EventHandler(this.chkBox_CheckedChanged);
			this.cmbTypeEnd.AddItemSeparator = ';';
			this.cmbTypeEnd.BorderStyle = 1;
			this.cmbTypeEnd.Caption = "";
			this.cmbTypeEnd.CaptionHeight = 17;
			this.cmbTypeEnd.CharacterCasing = 0;
			this.cmbTypeEnd.ColumnCaptionHeight = 17;
			this.cmbTypeEnd.ColumnFooterHeight = 17;
			this.cmbTypeEnd.ColumnHeaders = false;
			this.cmbTypeEnd.ColumnWidth = 100;
			this.cmbTypeEnd.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeEnd.ContentHeight = 15;
			this.cmbTypeEnd.DataMode = DataModeEnum.AddItem;
			this.cmbTypeEnd.DeadAreaBackColor = Color.Empty;
			this.cmbTypeEnd.EditorBackColor = SystemColors.Window;
			this.cmbTypeEnd.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeEnd.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeEnd.EditorHeight = 15;
			this.cmbTypeEnd.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeEnd.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbTypeEnd.ItemHeight = 15;
			this.cmbTypeEnd.Location = new Point(72, 64);
			this.cmbTypeEnd.MatchEntryTimeout = (long)2000;
			this.cmbTypeEnd.MaxDropDownItems = 5;
			this.cmbTypeEnd.MaxLength = 32767;
			this.cmbTypeEnd.MouseCursor = Cursors.Default;
			this.cmbTypeEnd.Name = "cmbTypeEnd";
			this.cmbTypeEnd.RowDivider.Color = Color.DarkGray;
			this.cmbTypeEnd.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeEnd.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeEnd.Size = new System.Drawing.Size(200, 19);
			this.cmbTypeEnd.TabIndex = 3;
			this.cmbTypeEnd.TextChanged += new EventHandler(this.cmbTypeEnd_TextChanged);
			this.cmbTypeEnd.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(120, 16);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 1;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 21, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
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
			this.cmbMechanic.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbMechanic.ItemHeight = 15;
			this.cmbMechanic.Location = new Point(72, 40);
			this.cmbMechanic.MatchEntryTimeout = (long)2000;
			this.cmbMechanic.MaxDropDownItems = 5;
			this.cmbMechanic.MaxLength = 32767;
			this.cmbMechanic.MouseCursor = Cursors.Default;
			this.cmbMechanic.Name = "cmbMechanic";
			this.cmbMechanic.RowDivider.Color = Color.DarkGray;
			this.cmbMechanic.RowDivider.Style = LineStyleEnum.None;
			this.cmbMechanic.RowSubDividerColor = Color.DarkGray;
			this.cmbMechanic.Size = new System.Drawing.Size(200, 19);
			this.cmbMechanic.TabIndex = 2;
			this.cmbMechanic.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.lblTypeEnd.ForeColor = SystemColors.ControlText;
			this.lblTypeEnd.Location = new Point(8, 64);
			this.lblTypeEnd.Name = "lblTypeEnd";
			this.lblTypeEnd.Size = new System.Drawing.Size(56, 16);
			this.lblTypeEnd.TabIndex = 2;
			this.lblTypeEnd.Text = "Тип";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 1;
			this.label5.Text = "Выполнил";
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 16);
			this.label6.TabIndex = 0;
			this.label6.Text = "Дата выполнения";
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(96, 216);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(472, 32);
			this.txtNote.TabIndex = 4;
			this.txtNote.Text = "";
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 216);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 36;
			this.label17.Text = "Примечание";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(472, 256);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 6;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(368, 256);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.grNewOU.Controls.Add(this.cmdTypeGObject);
			this.grNewOU.Controls.Add(this.cmbTypeGObject);
			this.grNewOU.Controls.Add(this.label1);
			this.grNewOU.Controls.Add(this.cmdAddress);
			this.grNewOU.Controls.Add(this.lblAddress2);
			this.grNewOU.Controls.Add(this.label3);
			this.grNewOU.Controls.Add(this.txtInvNumber);
			this.grNewOU.Controls.Add(this.cmdGRU);
			this.grNewOU.Controls.Add(this.lblNameGRU);
			this.grNewOU.Controls.Add(this.label4);
			this.grNewOU.Controls.Add(this.txtCountLives);
			this.grNewOU.Controls.Add(this.label13);
			this.grNewOU.Enabled = false;
			this.grNewOU.ForeColor = SystemColors.Desktop;
			this.grNewOU.Location = new Point(4, 96);
			this.grNewOU.Name = "grNewOU";
			this.grNewOU.Size = new System.Drawing.Size(280, 112);
			this.grNewOU.TabIndex = 2;
			this.grNewOU.TabStop = false;
			this.grNewOU.Text = "Новый ОУ";
			this.cmdTypeGObject.FlatStyle = FlatStyle.Flat;
			this.cmdTypeGObject.ForeColor = SystemColors.ControlText;
			this.cmdTypeGObject.ImageIndex = 0;
			this.cmdTypeGObject.ImageList = this.imageList1;
			this.cmdTypeGObject.Location = new Point(256, 16);
			this.cmdTypeGObject.Name = "cmdTypeGObject";
			this.cmdTypeGObject.Size = new System.Drawing.Size(20, 20);
			this.cmdTypeGObject.TabIndex = 2;
			this.cmdTypeGObject.Click += new EventHandler(this.cmdTypeGObject_Click);
			this.cmbTypeGObject.AddItemSeparator = ';';
			this.cmbTypeGObject.BorderStyle = 1;
			this.cmbTypeGObject.Caption = "";
			this.cmbTypeGObject.CaptionHeight = 17;
			this.cmbTypeGObject.CharacterCasing = 0;
			this.cmbTypeGObject.ColumnCaptionHeight = 17;
			this.cmbTypeGObject.ColumnFooterHeight = 17;
			this.cmbTypeGObject.ColumnHeaders = false;
			this.cmbTypeGObject.ColumnWidth = 100;
			this.cmbTypeGObject.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeGObject.ContentHeight = 15;
			this.cmbTypeGObject.DataMode = DataModeEnum.AddItem;
			this.cmbTypeGObject.DeadAreaBackColor = Color.Empty;
			this.cmbTypeGObject.EditorBackColor = SystemColors.Window;
			this.cmbTypeGObject.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeGObject.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeGObject.EditorHeight = 15;
			this.cmbTypeGObject.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeGObject.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.cmbTypeGObject.ItemHeight = 15;
			this.cmbTypeGObject.Location = new Point(48, 16);
			this.cmbTypeGObject.MatchEntryTimeout = (long)2000;
			this.cmbTypeGObject.MaxDropDownItems = 5;
			this.cmbTypeGObject.MaxLength = 32767;
			this.cmbTypeGObject.MouseCursor = Cursors.Default;
			this.cmbTypeGObject.Name = "cmbTypeGObject";
			this.cmbTypeGObject.RowDivider.Color = Color.DarkGray;
			this.cmbTypeGObject.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeGObject.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeGObject.Size = new System.Drawing.Size(208, 19);
			this.cmbTypeGObject.TabIndex = 1;
			this.cmbTypeGObject.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 60;
			this.label1.Text = "Тип";
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.ImageIndex = 0;
			this.cmdAddress.ImageList = this.imageList1;
			this.cmdAddress.Location = new Point(256, 40);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(20, 20);
			this.cmdAddress.TabIndex = 3;
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.lblAddress2.BackColor = SystemColors.Info;
			this.lblAddress2.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress2.ForeColor = SystemColors.ControlText;
			this.lblAddress2.Location = new Point(48, 40);
			this.lblAddress2.Name = "lblAddress2";
			this.lblAddress2.Size = new System.Drawing.Size(208, 20);
			this.lblAddress2.TabIndex = 57;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 56;
			this.label3.Text = "Адрес";
			this.txtInvNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtInvNumber.Location = new Point(48, 64);
			this.txtInvNumber.Name = "txtInvNumber";
			this.txtInvNumber.Size = new System.Drawing.Size(48, 20);
			this.txtInvNumber.TabIndex = 4;
			this.txtInvNumber.Text = "";
			this.txtInvNumber.Leave += new EventHandler(this.txtInvNumber_Leave);
			this.txtInvNumber.KeyUp += new KeyEventHandler(this.txtInvNumber_KeyUp);
			this.txtInvNumber.Enter += new EventHandler(this.txtInvNumber_Enter);
			this.cmdGRU.FlatStyle = FlatStyle.Flat;
			this.cmdGRU.ForeColor = SystemColors.ControlText;
			this.cmdGRU.ImageIndex = 0;
			this.cmdGRU.ImageList = this.imageList1;
			this.cmdGRU.Location = new Point(256, 64);
			this.cmdGRU.Name = "cmdGRU";
			this.cmdGRU.Size = new System.Drawing.Size(20, 20);
			this.cmdGRU.TabIndex = 5;
			this.cmdGRU.Click += new EventHandler(this.cmdGRU_Click);
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(96, 64);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(160, 20);
			this.lblNameGRU.TabIndex = 54;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 53;
			this.label4.Text = "РУ";
			this.txtCountLives.BorderStyle = 1;
			this.txtCountLives.EditMask = "###";
			this.txtCountLives.Location = new Point(48, 88);
			this.txtCountLives.Multiline = true;
			this.txtCountLives.Name = "txtCountLives";
			this.txtCountLives.Size = new System.Drawing.Size(40, 20);
			this.txtCountLives.TabIndex = 6;
			this.txtCountLives.Tag = null;
			this.txtCountLives.Enter += new EventHandler(this.txtCountLives_Enter);
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 88);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(40, 16);
			this.label13.TabIndex = 49;
			this.label13.Text = "Прож.";
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(8, 72);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 16);
			this.label7.TabIndex = 62;
			this.label7.Text = "ОУ";
			this.cmbOU.AddItemSeparator = ';';
			this.cmbOU.BorderStyle = 1;
			this.cmbOU.Caption = "";
			this.cmbOU.CaptionHeight = 17;
			this.cmbOU.CharacterCasing = 0;
			this.cmbOU.ColumnCaptionHeight = 17;
			this.cmbOU.ColumnFooterHeight = 17;
			this.cmbOU.ColumnHeaders = false;
			this.cmbOU.ColumnWidth = 100;
			this.cmbOU.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbOU.ContentHeight = 15;
			this.cmbOU.DataMode = DataModeEnum.AddItem;
			this.cmbOU.DeadAreaBackColor = Color.Empty;
			this.cmbOU.EditorBackColor = SystemColors.Window;
			this.cmbOU.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbOU.EditorForeColor = SystemColors.WindowText;
			this.cmbOU.EditorHeight = 15;
			this.cmbOU.FlatStyle = FlatModeEnum.Flat;
			this.cmbOU.Images.Add((Image)resourceManager.GetObject("resource3"));
			this.cmbOU.ItemHeight = 15;
			this.cmbOU.Location = new Point(52, 72);
			this.cmbOU.MatchEntryTimeout = (long)2000;
			this.cmbOU.MaxDropDownItems = 5;
			this.cmbOU.MaxLength = 32767;
			this.cmbOU.MouseCursor = Cursors.Default;
			this.cmbOU.Name = "cmbOU";
			this.cmbOU.RowDivider.Color = Color.DarkGray;
			this.cmbOU.RowDivider.Style = LineStyleEnum.None;
			this.cmbOU.RowSubDividerColor = Color.DarkGray;
			this.cmbOU.Size = new System.Drawing.Size(516, 19);
			this.cmbOU.TabIndex = 1;
			this.cmbOU.TextChanged += new EventHandler(this.cmbOU_TextChanged);
			this.cmbOU.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(570, 287);
			base.Controls.Add(this.cmbOU);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.grNewOU);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmActionGObject";
			this.Text = "Отключение объекта учета";
			base.Closing += new CancelEventHandler(this.frmActionGObject_Closing);
			base.Load += new EventHandler(this.frmActionGObject_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmbTypeEnd).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.cmbMechanic).EndInit();
			this.grNewOU.ResumeLayout(false);
			((ISupportInitialize)this.cmbTypeGObject).EndInit();
			((ISupportInitialize)this.txtCountLives).EndInit();
			((ISupportInitialize)this.cmbOU).EndInit();
			base.ResumeLayout(false);
		}

		private void txtCountLives_Enter(object sender, EventArgs e)
		{
			this.txtCountLives.SelectAll();
		}

		private void txtInvNumber_Enter(object sender, EventArgs e)
		{
			this.txtInvNumber.SelectAll();
		}

		private void txtInvNumber_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			this.GetGRU();
		}

		private void txtInvNumber_Leave(object sender, EventArgs e)
		{
			this.GetGRU();
		}
	}
}