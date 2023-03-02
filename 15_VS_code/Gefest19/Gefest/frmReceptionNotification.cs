using C1.Win.C1Command;
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
	public class frmReceptionNotification : Form
	{
		private ToolTip toolTip1;

		private ImageList imageList1;

		private ImageList imageList2;

		private GroupBox groupBox4;

		private Panel panel1;

		private C1CommandHolder CommandHolder;

		private C1Command cmd_toolBarButton16;

		private C1Command cmd_toolBarButton23;

		private C1Command cmd_toolBarButton25;

		private C1Command cmd_toolBarButton26;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader3;

		private ListView lv;

		private C1DateEdit dtDate;

		private Label label1;

		private GroupBox groupBox2;

		private Button cmdPrint;

		private C1TextBox txtAccount;

		private Button cmdAccount;

		private Label lblCountLives;

		private Label lblPU;

		private Label label13;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private GroupBox groupBox3;

		private Label label5;

		private C1Combo cmbAgent;

		private Button cmdApply1;

		private IContainer components;

		private Address _address;

		private Documents _documents;

		private Document tmpDocument;

		private Contract _contract;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private Agent _agent;

		private Agents _agents;

		private PD tmpPD;

		private int CountDocument = 0;

		private Label label2;

		private Label lblPU1;

		private StatusBar statusBar1;

		private StatusBarPanel statusBarPanel1;

		private Label label3;

		private Label lblPoverki;

		private ToolBar tbData;

		private ToolBarButton toolBarButton16;

		private ToolBarButton toolBarButton23;

		private ToolBarButton toolBarButton25;

		private ToolBarButton toolBarButton26;

		private ColumnHeader columnHeader5;

		public frmReceptionNotification()
		{
			this.InitializeComponent();
		}

		private void cmbAgent_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdApply1.Focus();
			}
		}

		private void cmbGmeter_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.cmbAgent.Focus();
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
						Label str = this.lblCountLives;
						int countLives = this._contract.oGobjects[0].CountLives;
						str.Text = countLives.ToString();
						this._address = this._contract.oPerson.oAddress;
						this._gobject = this._contract.oGobjects[0];
						this._gmeter = this._gobject.GetActiveGMeter();
						if (this._gmeter != null)
						{
							this.lblPU.Text = "Подключен";
							this.cmdApply1.Visible = true;
							this.lblPU1.Text = string.Concat(this._gmeter.oTypeGMeter.Fullname, " №", this._gmeter.SerialNumber.ToString());
							this.lblPoverki.Text = this._gmeter.DateVerify.ToLongDateString();
						}
						else
						{
							this.lblPU.Text = "Не подключен";
							this.cmdApply1.Visible = false;
						}
						this.cmbAgent.Focus();
						contract = null;
						this._address = null;
						this._gobject = null;
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

		private void cmdApply1_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract != null)
					{
						this.tmpDocument = new Document();
						bool d = this.tmpDocument.get_ID() == (long)0;
						this.tmpDocument.oTypeDocument = Depot.oTypeDocuments.item((long)21);
						this.tmpDocument.oContract = this._contract;
						this.tmpDocument.oPeriod = Depot.CurrentPeriod;
						this.tmpDocument.DocumentDate = (DateTime)this.dtDate.Value;
						if (this.tmpDocument.Save() == 0)
						{
							this.tmpPD = new PD()
							{
								oTypePD = Depot.oTypePDs.item((long)7)
							};
							PD pD = this.tmpPD;
							long num = this._gmeter.get_ID();
							((SimpleClass)pD).set_Name(num.ToString());
							this.tmpPD.oDocument = this.tmpDocument;
							if (this.tmpPD.Save() == 0)
							{
								this.tmpPD = new PD()
								{
									oTypePD = Depot.oTypePDs.item((long)26),
									oDocument = this.tmpDocument
								};
								PD pD1 = this.tmpPD;
								num = this._agents[this.cmbAgent.SelectedIndex].get_ID();
								((SimpleClass)pD1).set_Name(num.ToString());
								if (this.tmpPD.Save() == 0)
								{
									this.FillOneItem(this.tmpDocument, d);
									this.ResetFields1();
									this.txtAccount.Focus();
								}
								else
								{
									this.tmpDocument.Delete();
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							else
							{
								this.tmpDocument.Delete();
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
						return;
					}
				}
				catch
				{
					MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void cmdPrint_Click(object sender, EventArgs e)
		{
			this.printCard();
		}

		private void DeleteDocument()
		{
			if (this.lv.SelectedItems.Count > 0 && MessageBox.Show(string.Concat("Вы действительно хотите удалить документ для л/счета ", this.lv.SelectedItems[0].Text, "?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				if (this._documents.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) == 0)
				{
					this.lv.Items.Remove(this.lv.SelectedItems[0]);
					this.CountDocument--;
					this.FillStatusBar();
					return;
				}
				MessageBox.Show("Ошибка удаления документа!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

		private void dtDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.FillListView();
				this.FillStatusBar();
			}
			catch
			{
			}
		}

		private void EditDocument()
		{
		}

		private void FillListView()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this._documents = new Documents();
				this._gmeter = new Gmeter();
				this._agent = new Agent();
				this._documents.Load(Depot.oTypeDocuments.item((long)21), (DateTime)this.dtDate.Value, (DateTime)this.dtDate.Value);
				this.lv.Items.Clear();
				this.CountDocument = 0;
				foreach (Document _document in this._documents)
				{
					ListViewItem listViewItem = new ListViewItem(_document.oContract.Account)
					{
						Tag = _document.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_document.oContract.oPerson.FullName);
					this._gmeter.Load((long)Convert.ToInt32(_document.GetPD(7).get_Name()));
					this._agent.Load((long)Convert.ToInt32(_document.GetPD(26).get_Name()));
					listViewItem.SubItems.Add(this._gmeter.oTypeGMeter.Fullname);
					listViewItem.SubItems.Add(_document.DocumentDate.ToShortDateString());
					listViewItem.SubItems.Add(this._agent.get_Name().ToString());
					this.CountDocument++;
					this.lv.Items.Add(listViewItem);
				}
				this.Cursor = Cursors.Default;
			}
			catch (Exception exception)
			{
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillOneItem(Document o, bool isAdd)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (!isAdd)
				{
					this.lv.SelectedItems[0].SubItems[0].Text = o.oContract.Account;
					this.lv.SelectedItems[0].SubItems[1].Text = o.oContract.oPerson.FullName;
					this._gmeter.Load((long)Convert.ToInt32(o.GetPD(7).get_Name()));
					this._agent.Load((long)Convert.ToInt32(o.GetPD(26).get_Name()));
					this.lv.SelectedItems[0].SubItems[2].Text = Convert.ToString(this._gmeter.oTypeGMeter.Fullname);
					this.lv.SelectedItems[0].SubItems[3].Text = o.DocumentDate.ToShortDateString();
					this.lv.SelectedItems[0].SubItems[4].Text = this._agent.get_Name().ToString();
				}
				else
				{
					this.CountDocument++;
					ListViewItem listViewItem = new ListViewItem(o.oContract.Account)
					{
						Tag = o.get_ID().ToString()
					};
					listViewItem.SubItems.Add(o.oContract.oPerson.FullName);
					this._gmeter.Load((long)Convert.ToInt32(o.GetPD(7).get_Name()));
					this._agent.Load((long)Convert.ToInt32(o.GetPD(26).get_Name()));
					listViewItem.SubItems.Add(this._gmeter.oTypeGMeter.Fullname);
					listViewItem.SubItems.Add(o.DocumentDate.ToShortDateString());
					listViewItem.SubItems.Add(this._agent.get_Name().ToString());
					this.lv.Items.Insert(0, listViewItem);
				}
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillStatusBar()
		{
			try
			{
				this.statusBar1.Panels[0].Text = string.Concat("Разнесено документов: ", Convert.ToString(this.CountDocument));
			}
			catch
			{
			}
		}

		private void frmReceptionNotification_Load(object sender, EventArgs e)
		{
			try
			{
				this.dtDate.Value = DateTime.Today.Date;
				this._agents = new Agents();
				Agents agent = this._agents;
				TypeAgent[] typeAgentArray = new TypeAgent[] { Depot.oTypeAgents.item((long)1), Depot.oTypeAgents.item((long)1) };
				agent.Load(typeAgentArray);
				Tools.FillC1(this._agents, this.cmbAgent, (long)0);
				this.FillListView();
				this.FillStatusBar();
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmReceptionNotification));
			this.toolTip1 = new ToolTip(this.components);
			this.cmdPrint = new Button();
			this.imageList2 = new ImageList(this.components);
			this.lblPU = new Label();
			this.imageList1 = new ImageList(this.components);
			this.groupBox4 = new GroupBox();
			this.panel1 = new Panel();
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.CommandHolder = new C1CommandHolder();
			this.cmd_toolBarButton25 = new C1Command();
			this.cmd_toolBarButton26 = new C1Command();
			this.cmd_toolBarButton16 = new C1Command();
			this.cmd_toolBarButton23 = new C1Command();
			this.dtDate = new C1DateEdit();
			this.label1 = new Label();
			this.groupBox2 = new GroupBox();
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.lblCountLives = new Label();
			this.label13 = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.label2 = new Label();
			this.lblPU1 = new Label();
			this.label3 = new Label();
			this.lblPoverki = new Label();
			this.groupBox3 = new GroupBox();
			this.label5 = new Label();
			this.cmbAgent = new C1Combo();
			this.cmdApply1 = new Button();
			this.statusBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.tbData = new ToolBar();
			this.toolBarButton16 = new ToolBarButton();
			this.toolBarButton23 = new ToolBarButton();
			this.toolBarButton25 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.groupBox4.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.CommandHolder).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtAccount).BeginInit();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			base.SuspendLayout();
			this.cmdPrint.FlatStyle = FlatStyle.Flat;
			this.cmdPrint.ForeColor = SystemColors.ControlText;
			this.cmdPrint.ImageIndex = 11;
			this.cmdPrint.ImageList = this.imageList2;
			this.cmdPrint.Location = new Point(176, 16);
			this.cmdPrint.Name = "cmdPrint";
			this.cmdPrint.Size = new System.Drawing.Size(20, 20);
			this.cmdPrint.TabIndex = 3;
			this.toolTip1.SetToolTip(this.cmdPrint, "Карточка абонента");
			this.cmdPrint.Click += new EventHandler(this.cmdPrint_Click);
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = Color.Transparent;
			this.lblPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblPU.ForeColor = SystemColors.ControlText;
			this.lblPU.Location = new Point(128, 144);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(144, 20);
			this.lblPU.TabIndex = 7;
			this.toolTip1.SetToolTip(this.lblPU, "Статус ПУ");
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.groupBox4.Controls.Add(this.panel1);
			this.groupBox4.ForeColor = SystemColors.Desktop;
			this.groupBox4.Location = new Point(284, 4);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(440, 336);
			this.groupBox4.TabIndex = 60;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Разнесенные уведомления";
			this.panel1.Controls.Add(this.tbData);
			this.panel1.Controls.Add(this.lv);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(434, 317);
			this.panel1.TabIndex = 9;
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader4, this.columnHeader3, this.columnHeader5 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 24);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(431, 292);
			this.lv.TabIndex = 3;
			this.lv.View = View.Details;
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.lv.KeyUp += new KeyEventHandler(this.lv_KeyUp);
			this.columnHeader1.Text = "Л/с";
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 108;
			this.columnHeader4.Text = "Тип ПУ";
			this.columnHeader4.Width = 90;
			this.columnHeader3.Text = "Дата";
			this.columnHeader3.Width = 95;
			this.columnHeader5.Text = "Вручил";
			this.columnHeader5.Width = 75;
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton25);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton26);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton16);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton23);
			this.CommandHolder.Owner = this;
			this.cmd_toolBarButton25.Name = "cmd_toolBarButton25";
			this.cmd_toolBarButton26.ImageIndex = 3;
			this.cmd_toolBarButton26.Name = "cmd_toolBarButton26";
			this.cmd_toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.cmd_toolBarButton16.ImageIndex = 1;
			this.cmd_toolBarButton16.Name = "cmd_toolBarButton16";
			this.cmd_toolBarButton16.ToolTipText = "Редактировать";
			this.cmd_toolBarButton16.Visible = false;
			this.cmd_toolBarButton23.ImageIndex = 2;
			this.cmd_toolBarButton23.Name = "cmd_toolBarButton23";
			this.cmd_toolBarButton23.ToolTipText = "Удалить";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(120, 4);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 61;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.ValueChanged += new EventHandler(this.dtDate_ValueChanged);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(0, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 62;
			this.label1.Text = "Дата разноски";
			this.groupBox2.Controls.Add(this.cmdPrint);
			this.groupBox2.Controls.Add(this.txtAccount);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblCountLives);
			this.groupBox2.Controls.Add(this.lblPU);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.lblPU1);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.lblPoverki);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(0, 28);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 172);
			this.groupBox2.TabIndex = 63;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.txtAccount.BorderStyle = 1;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(48, 16);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(96, 21);
			this.txtAccount.TabIndex = 1;
			this.txtAccount.Tag = null;
			this.txtAccount.KeyPress += new KeyPressEventHandler(this.txtAccount_KeyPress);
			this.txtAccount.Enter += new EventHandler(this.txtAccount_Enter);
			this.cmdAccount.FlatStyle = FlatStyle.Flat;
			this.cmdAccount.ForeColor = SystemColors.ControlText;
			this.cmdAccount.ImageIndex = 0;
			this.cmdAccount.ImageList = this.imageList1;
			this.cmdAccount.Location = new Point(152, 16);
			this.cmdAccount.Name = "cmdAccount";
			this.cmdAccount.Size = new System.Drawing.Size(20, 20);
			this.cmdAccount.TabIndex = 2;
			this.cmdAccount.Click += new EventHandler(this.cmdAccount_Click);
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(72, 144);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 10;
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 144);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Проживает";
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
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "ПУ";
			this.lblPU1.BackColor = SystemColors.Info;
			this.lblPU1.BorderStyle = BorderStyle.FixedSingle;
			this.lblPU1.ForeColor = SystemColors.ControlText;
			this.lblPU1.Location = new Point(48, 88);
			this.lblPU1.Name = "lblPU1";
			this.lblPU1.Size = new System.Drawing.Size(224, 20);
			this.lblPU1.TabIndex = 5;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Дата посл. пов.:";
			this.lblPoverki.BackColor = SystemColors.Info;
			this.lblPoverki.BorderStyle = BorderStyle.FixedSingle;
			this.lblPoverki.ForeColor = SystemColors.ControlText;
			this.lblPoverki.Location = new Point(104, 112);
			this.lblPoverki.Name = "lblPoverki";
			this.lblPoverki.Size = new System.Drawing.Size(168, 20);
			this.lblPoverki.TabIndex = 5;
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.cmbAgent);
			this.groupBox3.Controls.Add(this.cmdApply1);
			this.groupBox3.ForeColor = SystemColors.Desktop;
			this.groupBox3.Location = new Point(0, 208);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(280, 104);
			this.groupBox3.TabIndex = 64;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Разноска врученных уведомлений";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 16);
			this.label5.TabIndex = 13;
			this.label5.Text = "Агент";
			this.cmbAgent.AddItemSeparator = ';';
			this.cmbAgent.BorderStyle = 1;
			this.cmbAgent.Caption = "";
			this.cmbAgent.CaptionHeight = 17;
			this.cmbAgent.CharacterCasing = 0;
			this.cmbAgent.ColumnCaptionHeight = 17;
			this.cmbAgent.ColumnFooterHeight = 17;
			this.cmbAgent.ColumnHeaders = false;
			this.cmbAgent.ColumnWidth = 100;
			this.cmbAgent.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbAgent.ContentHeight = 15;
			this.cmbAgent.DataMode = DataModeEnum.AddItem;
			this.cmbAgent.DeadAreaBackColor = Color.Empty;
			this.cmbAgent.EditorBackColor = SystemColors.Window;
			this.cmbAgent.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbAgent.EditorForeColor = SystemColors.WindowText;
			this.cmbAgent.EditorHeight = 15;
			this.cmbAgent.FlatStyle = FlatModeEnum.Flat;
			this.cmbAgent.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbAgent.ItemHeight = 15;
			this.cmbAgent.Location = new Point(80, 16);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(192, 19);
			this.cmbAgent.TabIndex = 12;
			this.cmbAgent.KeyPress += new KeyPressEventHandler(this.cmbAgent_KeyPress);
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.cmdApply1.FlatStyle = FlatStyle.Flat;
			this.cmdApply1.ForeColor = SystemColors.ControlText;
			this.cmdApply1.Location = new Point(152, 48);
			this.cmdApply1.Name = "cmdApply1";
			this.cmdApply1.Size = new System.Drawing.Size(120, 24);
			this.cmdApply1.TabIndex = 3;
			this.cmdApply1.Text = "Принять";
			this.cmdApply1.Click += new EventHandler(this.cmdApply1_Click);
			this.statusBar1.Location = new Point(0, 339);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new StatusBarPanel[] { this.statusBarPanel1 });
			this.statusBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(726, 24);
			this.statusBar1.TabIndex = 65;
			this.statusBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel1.Text = "Разнесено документов:";
			this.statusBarPanel1.Width = 710;
			this.tbData.Appearance = ToolBarAppearance.Flat;
			this.tbData.AutoSize = false;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.toolBarButton16, this.toolBarButton23, this.toolBarButton25, this.toolBarButton26 };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.Divider = false;
			this.tbData.DropDownArrows = true;
			this.tbData.ImageList = this.imageList2;
			this.tbData.Location = new Point(0, 0);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(434, 24);
			this.tbData.TabIndex = 4;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.toolBarButton16.ImageIndex = 1;
			this.toolBarButton16.Tag = "Edit";
			this.toolBarButton16.ToolTipText = "Редактировать";
			this.toolBarButton16.Visible = false;
			this.toolBarButton23.ImageIndex = 2;
			this.toolBarButton23.Tag = "Del";
			this.toolBarButton23.ToolTipText = "Удалить";
			this.toolBarButton25.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton26.ImageIndex = 3;
			this.toolBarButton26.Tag = "Excel";
			this.toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(726, 363);
			base.Controls.Add(this.statusBar1);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.groupBox4);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmReceptionNotification";
			this.Text = "Разноска уведомлений";
			base.Load += new EventHandler(this.frmReceptionNotification_Load);
			this.groupBox4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.CommandHolder).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount).EndInit();
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			base.ResumeLayout(false);
		}

		private void lv_DoubleClick(object sender, EventArgs e)
		{
		}

		private void lv_KeyUp(object sender, KeyEventArgs e)
		{
		}

		private void printCard()
		{
			if (this._contract != null)
			{
				frmPrintAccount _frmPrintAccount = new frmPrintAccount()
				{
					_contract = this._contract
				};
				_frmPrintAccount.ShowDialog(this);
				_frmPrintAccount = null;
			}
		}

		private void ResetFields1()
		{
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblCountLives.Text = "";
			this.lblPU.Text = "";
			this.lblPU1.Text = "";
			this.lblPoverki.Text = "";
			this._contract = null;
			this._gobject = null;
			this.tmpDocument = null;
			this.tmpPD = null;
			this._gmeter = null;
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			try
			{
				"Excel";
				string str = e.Button.Tag.ToString();
				string str1 = str;
				if (str != null)
				{
					str1 = string.IsInterned(str1);
					if ((object)str1 == (object)"Edit")
					{
						this.EditDocument();
					}
					else if ((object)str1 == (object)"Del")
					{
						this.DeleteDocument();
					}
					else if ((object)str1 == (object)"Excel")
					{
						Tools.ConvertToExcel(this.lv);
					}
				}
			}
			catch
			{
			}
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
	}
}