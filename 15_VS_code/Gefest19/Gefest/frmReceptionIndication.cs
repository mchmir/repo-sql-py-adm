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
	public class frmReceptionIndication : Form
	{
		private Label lblAddress;

		private TabPage tabPage2;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private GroupBox groupBox2;

		private C1TextBox txtAccount;

		private Button cmdAccount;

		private Label lblCountLives;

		private Label lblPU;

		private Label label13;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private ToolTip toolTip1;

		private ImageList imageList1;

		private GroupBox groupBox1;

		private Label label3;

		private Label label6;

		private Label label7;

		private Label label8;

		private IContainer components;

		private Label lblAccount;

		private Label lblCountLives2;

		private Label lblFIO2;

		private Label lblPU2;

		private Button cmdAddress;

		private Label lblAddress2;

		private C1DateEdit dtDate;

		private Label label1;

		private GroupBox groupBox3;

		private NumericUpDown numNewIndication;

		private Label lblCurrentIndication;

		private Label label18;

		private Label label19;

		private Label lblDateIndication;

		private Label label4;

		private Button cmdApply1;

		private Label label2;

		private C1Combo cmbTypeIndication;

		private GroupBox groupBox4;

		private Panel panel1;

		private C1CommandHolder CommandHolder;

		private C1Command cmd_toolBarButton16;

		private C1Command cmd_toolBarButton23;

		private C1Command cmd_toolBarButton25;

		private C1Command cmd_toolBarButton26;

		private ImageList imageList2;

		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private StatusBar statusBar1;

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel2;

		private Address _address;

		private Contract _contract;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private Indications _indications;

		private Indication tmpIndication;

		private Agents _agents;

		private double FactAmount = 0;

		private double OldFactAmount = 0;

		private double FactsAmount = 0;

		private int indicationsCount = 0;

		private Label label5;

		private C1Combo cmbAgent;

		private Label label9;

		private Label lblFactUse;

		private Button cmdPrint;

		private Button cmdPrint2;

		private ColumnHeader columnHeader3;

		private ToolBar tbData;

		private ToolBarButton toolBarButton16;

		private ToolBarButton toolBarButton23;

		private ToolBarButton toolBarButton25;

		private ToolBarButton toolBarButton26;

		private int ind = 0;

		public frmReceptionIndication()
		{
			this.InitializeComponent();
		}

		private void cmbAgent_TextChanged(object sender, EventArgs e)
		{
			this.FillListView();
			this.FillStatusBar();
		}

		private void cmbTypeIndication_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.cmdApply1.Focus();
			}
		}

		private void cmbTypeIndication_TextChanged(object sender, EventArgs e)
		{
			if (Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex].get_ID() == (long)1 || Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex].get_ID() == (long)4)
			{
				this.cmbAgent.Enabled = false;
			}
			else
			{
				this.cmbAgent.Enabled = true;
			}
			this.FillListView();
			this.FillStatusBar();
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
						this.ShowContractInfo();
						contract = null;
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
			_frmAddress.ShowDialog(this);
			if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				this._address = _frmAddress.oAddress;
				if (this._address == null)
				{
					return;
				}
				Label label = this.lblAddress2;
				Label label1 = this.lblAddress;
				string shortAddress = this._address.get_ShortAddress();
				string str = shortAddress;
				label1.Text = shortAddress;
				label.Text = str;
				Gobjects gobject = new Gobjects();
				if (gobject.Load(this._address) != 0)
				{
					return;
				}
				if (gobject.get_Count() <= 0)
				{
					return;
				}
				this._gobject = gobject[0];
				this._contract = this._gobject.oContract;
				this.lblAccount.Text = this._contract.Account;
				this.ShowContractInfo();
			}
			_frmAddress = null;
		}

		private void cmdApply1_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract == null)
					{
						return;
					}
					else if (this.numNewIndication.Value == new decimal(0))
					{
						this.numNewIndication.Focus();
						return;
					}
					else if (this.cmbTypeIndication.SelectedIndex == -1)
					{
						MessageBox.Show("Необходимо указать тип показаний!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbTypeIndication.Focus();
						return;
					}
					else if (Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex].get_ID() == (long)3 || this._gmeter.oTypeVerify.get_ID() != (long)2)
					{
						this.numNewIndication_Leave(null, null);
						if (this.FactAmount < 0)
						{
							MessageBox.Show("Не верно указаны показания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							this.numNewIndication.Focus();
							return;
						}
						else if (this.FactAmount > 10 && MessageBox.Show(string.Concat("Потребление составляет ", this.FactAmount.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
						{
							this.numNewIndication.Focus();
							return;
						}
						else if (!((DateTime)this.dtDate.Value > DateTime.Today.Date) || MessageBox.Show("Дата показаний больше текущей. Продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
						{
							this.tmpIndication.oTypeIndication = Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex];
							if (this.cmbAgent.Enabled)
							{
								this.tmpIndication.oAgent = this._agents[this.cmbAgent.SelectedIndex];
							}
							bool d = this.tmpIndication.get_ID() == (long)0;
							if (this.tmpIndication.Save() != 0)
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								this.FactsAmount -= this.OldFactAmount;
								this.FactsAmount += this.FactAmount;
								if (this.numNewIndication.Value > new decimal(0) && this.tmpIndication.oFactUses.get_Count() == 0)
								{
									this.tmpIndication.oFactUses = null;
								}
								this.FillOneItem(this.tmpIndication, d);
								this.FillStatusBar();
								this.lv.Items[this.ind].BackColor = SystemColors.Window;
								this.OldFactAmount = 0;
								this.tmpIndication = null;
								this._contract = null;
								this._address = null;
								this._gmeter = null;
								this._gobject = null;
								this.txtAccount.Focus();
							}
						}
						else
						{
							this.dtDate.Focus();
							return;
						}
					}
					else
					{
						MessageBox.Show("ПУ забракован! Тип показаний может быть только 'по акту'!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbTypeIndication.Focus();
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
			this.txtAccount.Focus();
			this.cmdAccount_Click(null, null);
		}

		private void cmdPrint2_Click(object sender, EventArgs e)
		{
			this.printCard();
		}

		private void DeleteIndication()
		{
			if (this.lv.SelectedItems.Count > 0 && MessageBox.Show(string.Concat("Вы действительно хотите удалить показание для л/счета ", this.lv.SelectedItems[0].Text, "?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				double factAmount = 0;
				if (this._indications.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).oFactUses.get_Count() > 0)
				{
					factAmount = this._indications.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).oFactUses[0].FactAmount;
				}
				if (this._indications.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) == 0)
				{
					this.lv.Items.Remove(this.lv.SelectedItems[0]);
					this.FactsAmount -= factAmount;
					this.indicationsCount--;
					this.FillStatusBar();
					return;
				}
				MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

		private void EditIndication()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				try
				{
					C1TextBox c1TextBox = this.txtAccount;
					Label label = this.lblAccount;
					string text = this.lv.SelectedItems[0].SubItems[0].Text;
					string str = text;
					label.Text = text;
					((Control)c1TextBox).Text = str;
					this.cmdAccount_Click(null, null);
					this.tmpIndication = this._indications.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
					this.lv.Items[this.ind].BackColor = SystemColors.Window;
					this.ind = this.lv.SelectedItems[0].Index;
					this.lv.SelectedItems[0].BackColor = SystemColors.Info;
					this.numNewIndication.Value = Convert.ToDecimal(this.tmpIndication.Display);
					this.numNewIndication.Focus();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		private void FillListView()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this._indications = new Indications();
				Agent item = null;
				if (this.cmbAgent.Enabled && this.cmbAgent.SelectedIndex >= 0)
				{
					item = this._agents[this.cmbAgent.SelectedIndex];
				}
				this._indications.Load(DateTime.Today, Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex], item);
				this.lv.Items.Clear();
				this.FactsAmount = 0;
				this.indicationsCount = 0;
				foreach (Indication _indication in this._indications)
				{
					ListViewItem listViewItem = new ListViewItem(_indication.oGmeter.oGobject.oContract.Account)
					{
						Tag = _indication.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_indication.oGmeter.oGobject.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(_indication.Display));
					if (_indication.oFactUses.get_Count() <= 0)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						if (_indication.oFactUses[0].FactAmount == 0)
						{
							listViewItem.SubItems.Add("0");
						}
						else
						{
							ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
							double factAmount = _indication.oFactUses[0].FactAmount;
							subItems.Add(factAmount.ToString("#.##"));
						}
						this.FactsAmount += _indication.oFactUses[0].FactAmount;
					}
					listViewItem.SubItems.Add(_indication.Datedisplay.ToShortDateString());
					this.indicationsCount++;
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

		private void FillOneItem(Indication o, bool isAdd)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (!isAdd)
				{
					this.lv.SelectedItems[0].SubItems[0].Text = o.oGmeter.oGobject.oContract.Account;
					this.lv.SelectedItems[0].SubItems[1].Text = o.oGmeter.oGobject.oContract.oPerson.FullName;
					this.lv.SelectedItems[0].SubItems[2].Text = Convert.ToString(o.Display);
					if (o.oFactUses.get_Count() <= 0)
					{
						this.lv.SelectedItems[0].SubItems[3].Text = "0";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[3].Text = Convert.ToString(o.oFactUses[0].FactAmount);
					}
					this.lv.SelectedItems[0].SubItems[4].Text = o.Datedisplay.ToShortDateString();
				}
				else
				{
					this.indicationsCount++;
					ListViewItem listViewItem = new ListViewItem(o.oGmeter.oGobject.oContract.Account)
					{
						Tag = o.get_ID().ToString()
					};
					listViewItem.SubItems.Add(o.oGmeter.oGobject.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(o.Display));
					if (o.oFactUses.get_Count() <= 0)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
						double factAmount = o.oFactUses[0].FactAmount;
						subItems.Add(factAmount.ToString());
					}
					listViewItem.SubItems.Add(o.Datedisplay.ToShortDateString());
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
				this.statusBar1.Panels[0].Text = string.Concat("Принято показаний: ", Convert.ToString(this.indicationsCount));
				this.statusBar1.Panels[1].Text = string.Concat("Потребление в куб.: ", this.FactsAmount.ToString("#.##"));
			}
			catch
			{
			}
		}

		private void frmReceptionIndication_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmReceptionIndication_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
				{
					this.tabControl1.SelectedIndex = 0;
					this.txtAccount.Focus();
					return;
				}
				case Keys.F2:
				{
					this.tabControl1.SelectedIndex = 1;
					this.cmdAddress.Focus();
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void frmReceptionIndication_Load(object sender, EventArgs e)
		{
			try
			{
				this.dtDate.Value = DateTime.Today.Date;
				this._agents = new Agents();
				Agents agent = this._agents;
				TypeAgent[] typeAgentArray = new TypeAgent[] { Depot.oTypeAgents.item((long)1), Depot.oTypeAgents.item((long)5) };
				agent.Load(typeAgentArray);
				Tools.FillC1(this._agents, this.cmbAgent, (long)0);
				Tools.FillC1(Depot.oTypeIndications, this.cmbTypeIndication, (long)1);
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmReceptionIndication));
			this.lblAddress = new Label();
			this.tabPage2 = new TabPage();
			this.groupBox1 = new GroupBox();
			this.cmdPrint2 = new Button();
			this.imageList2 = new ImageList(this.components);
			this.cmdAddress = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblAddress2 = new Label();
			this.lblAccount = new Label();
			this.lblCountLives2 = new Label();
			this.lblPU2 = new Label();
			this.label3 = new Label();
			this.lblFIO2 = new Label();
			this.label6 = new Label();
			this.label7 = new Label();
			this.label8 = new Label();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.groupBox2 = new GroupBox();
			this.cmdPrint = new Button();
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.lblCountLives = new Label();
			this.lblPU = new Label();
			this.label13 = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.dtDate = new C1DateEdit();
			this.label1 = new Label();
			this.groupBox3 = new GroupBox();
			this.label9 = new Label();
			this.lblFactUse = new Label();
			this.label5 = new Label();
			this.cmbAgent = new C1Combo();
			this.label2 = new Label();
			this.cmbTypeIndication = new C1Combo();
			this.cmdApply1 = new Button();
			this.lblDateIndication = new Label();
			this.label4 = new Label();
			this.numNewIndication = new NumericUpDown();
			this.lblCurrentIndication = new Label();
			this.label18 = new Label();
			this.label19 = new Label();
			this.groupBox4 = new GroupBox();
			this.panel1 = new Panel();
			this.tbData = new ToolBar();
			this.toolBarButton16 = new ToolBarButton();
			this.toolBarButton23 = new ToolBarButton();
			this.toolBarButton25 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.CommandHolder = new C1CommandHolder();
			this.cmd_toolBarButton25 = new C1Command();
			this.cmd_toolBarButton26 = new C1Command();
			this.cmd_toolBarButton16 = new C1Command();
			this.cmd_toolBarButton23 = new C1Command();
			this.statusBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel2 = new StatusBarPanel();
			this.tabPage2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtAccount).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.cmbTypeIndication).BeginInit();
			((ISupportInitialize)this.numNewIndication).BeginInit();
			this.groupBox4.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.CommandHolder).BeginInit();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel2).BeginInit();
			base.SuspendLayout();
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(48, 64);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(224, 20);
			this.lblAddress.TabIndex = 5;
			this.tabPage2.Controls.Add(this.groupBox1);
			this.tabPage2.Location = new Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(284, 131);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "По адресу F2";
			this.tabPage2.Visible = false;
			this.groupBox1.Controls.Add(this.cmdPrint2);
			this.groupBox1.Controls.Add(this.cmdAddress);
			this.groupBox1.Controls.Add(this.lblAddress2);
			this.groupBox1.Controls.Add(this.lblAccount);
			this.groupBox1.Controls.Add(this.lblCountLives2);
			this.groupBox1.Controls.Add(this.lblPU2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.lblFIO2);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(4, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(280, 120);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Абонент";
			this.cmdPrint2.FlatStyle = FlatStyle.Flat;
			this.cmdPrint2.ForeColor = SystemColors.ControlText;
			this.cmdPrint2.ImageIndex = 11;
			this.cmdPrint2.ImageList = this.imageList2;
			this.cmdPrint2.Location = new Point(152, 64);
			this.cmdPrint2.Name = "cmdPrint2";
			this.cmdPrint2.Size = new System.Drawing.Size(20, 20);
			this.cmdPrint2.TabIndex = 2;
			this.toolTip1.SetToolTip(this.cmdPrint2, "Карточка абонента");
			this.cmdPrint2.Click += new EventHandler(this.cmdPrint2_Click);
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = Color.Transparent;
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.ImageIndex = 0;
			this.cmdAddress.ImageList = this.imageList1;
			this.cmdAddress.Location = new Point(252, 16);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(20, 20);
			this.cmdAddress.TabIndex = 1;
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lblAddress2.BackColor = SystemColors.Info;
			this.lblAddress2.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress2.ForeColor = SystemColors.ControlText;
			this.lblAddress2.Location = new Point(48, 16);
			this.lblAddress2.Name = "lblAddress2";
			this.lblAddress2.Size = new System.Drawing.Size(204, 20);
			this.lblAddress2.TabIndex = 26;
			this.lblAccount.BackColor = SystemColors.Info;
			this.lblAccount.BorderStyle = BorderStyle.FixedSingle;
			this.lblAccount.ForeColor = SystemColors.ControlText;
			this.lblAccount.Location = new Point(48, 64);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(96, 20);
			this.lblAccount.TabIndex = 11;
			this.lblCountLives2.BackColor = SystemColors.Info;
			this.lblCountLives2.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives2.ForeColor = SystemColors.ControlText;
			this.lblCountLives2.Location = new Point(72, 88);
			this.lblCountLives2.Name = "lblCountLives2";
			this.lblCountLives2.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives2.TabIndex = 10;
			this.lblPU2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblPU2.ForeColor = SystemColors.ControlText;
			this.lblPU2.Location = new Point(152, 88);
			this.lblPU2.Name = "lblPU2";
			this.lblPU2.Size = new System.Drawing.Size(120, 20);
			this.lblPU2.TabIndex = 7;
			this.toolTip1.SetToolTip(this.lblPU2, "Статус ПУ");
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Проживает";
			this.lblFIO2.BackColor = SystemColors.Info;
			this.lblFIO2.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO2.ForeColor = SystemColors.ControlText;
			this.lblFIO2.Location = new Point(48, 40);
			this.lblFIO2.Name = "lblFIO2";
			this.lblFIO2.Size = new System.Drawing.Size(224, 20);
			this.lblFIO2.TabIndex = 4;
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 2;
			this.label6.Text = "Адрес";
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(8, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 16);
			this.label7.TabIndex = 1;
			this.label7.Text = "ФИО";
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(8, 64);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "Л/с";
			this.tabControl1.Appearance = TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.ItemSize = new System.Drawing.Size(96, 21);
			this.tabControl1.Location = new Point(2, 32);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(292, 160);
			this.tabControl1.TabIndex = 2;
			this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Location = new Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(284, 131);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "По лиц. счету F1";
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
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 120);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
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
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(72, 88);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 10;
			this.lblPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblPU.ForeColor = SystemColors.ControlText;
			this.lblPU.Location = new Point(152, 88);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(120, 20);
			this.lblPU.TabIndex = 7;
			this.toolTip1.SetToolTip(this.lblPU, "Статус ПУ");
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 88);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Проживает";
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
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(128, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 1;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.ValueChanged += new EventHandler(this.dtDate_ValueChanged);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 58;
			this.label1.Text = "Дата разноски";
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.lblFactUse);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.cmbAgent);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.cmbTypeIndication);
			this.groupBox3.Controls.Add(this.cmdApply1);
			this.groupBox3.Controls.Add(this.lblDateIndication);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.numNewIndication);
			this.groupBox3.Controls.Add(this.lblCurrentIndication);
			this.groupBox3.Controls.Add(this.label18);
			this.groupBox3.Controls.Add(this.label19);
			this.groupBox3.ForeColor = SystemColors.Desktop;
			this.groupBox3.Location = new Point(10, 192);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(280, 200);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Прием показаний";
			this.label9.ForeColor = SystemColors.ControlText;
			this.label9.Location = new Point(4, 136);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(128, 16);
			this.label9.TabIndex = 15;
			this.label9.Text = "Потребление";
			this.lblFactUse.BackColor = SystemColors.Info;
			this.lblFactUse.BorderStyle = BorderStyle.FixedSingle;
			this.lblFactUse.ForeColor = SystemColors.ControlText;
			this.lblFactUse.Location = new Point(144, 136);
			this.lblFactUse.Name = "lblFactUse";
			this.lblFactUse.Size = new System.Drawing.Size(128, 20);
			this.lblFactUse.TabIndex = 14;
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(4, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 16);
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
			this.cmbAgent.Location = new Point(100, 40);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(176, 19);
			this.cmbAgent.TabIndex = 5;
			this.cmbAgent.TextChanged += new EventHandler(this.cmbAgent_TextChanged);
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(4, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 16);
			this.label2.TabIndex = 11;
			this.label2.Text = "Тип показаний";
			this.cmbTypeIndication.AddItemSeparator = ';';
			this.cmbTypeIndication.BorderStyle = 1;
			this.cmbTypeIndication.Caption = "";
			this.cmbTypeIndication.CaptionHeight = 17;
			this.cmbTypeIndication.CharacterCasing = 0;
			this.cmbTypeIndication.ColumnCaptionHeight = 17;
			this.cmbTypeIndication.ColumnFooterHeight = 17;
			this.cmbTypeIndication.ColumnHeaders = false;
			this.cmbTypeIndication.ColumnWidth = 100;
			this.cmbTypeIndication.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeIndication.ContentHeight = 15;
			this.cmbTypeIndication.DataMode = DataModeEnum.AddItem;
			this.cmbTypeIndication.DeadAreaBackColor = Color.Empty;
			this.cmbTypeIndication.EditorBackColor = SystemColors.Window;
			this.cmbTypeIndication.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeIndication.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeIndication.EditorHeight = 15;
			this.cmbTypeIndication.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeIndication.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbTypeIndication.ItemHeight = 15;
			this.cmbTypeIndication.Location = new Point(100, 16);
			this.cmbTypeIndication.MatchEntryTimeout = (long)2000;
			this.cmbTypeIndication.MaxDropDownItems = 5;
			this.cmbTypeIndication.MaxLength = 32767;
			this.cmbTypeIndication.MouseCursor = Cursors.Default;
			this.cmbTypeIndication.Name = "cmbTypeIndication";
			this.cmbTypeIndication.RowDivider.Color = Color.DarkGray;
			this.cmbTypeIndication.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeIndication.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeIndication.Size = new System.Drawing.Size(176, 19);
			this.cmbTypeIndication.TabIndex = 4;
			this.cmbTypeIndication.TextChanged += new EventHandler(this.cmbTypeIndication_TextChanged);
			this.cmbTypeIndication.KeyUp += new KeyEventHandler(this.cmbTypeIndication_KeyUp);
			this.cmbTypeIndication.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.cmdApply1.Enabled = false;
			this.cmdApply1.FlatStyle = FlatStyle.Flat;
			this.cmdApply1.ForeColor = SystemColors.ControlText;
			this.cmdApply1.Location = new Point(156, 168);
			this.cmdApply1.Name = "cmdApply1";
			this.cmdApply1.Size = new System.Drawing.Size(120, 24);
			this.cmdApply1.TabIndex = 7;
			this.cmdApply1.Text = "Принять";
			this.cmdApply1.Click += new EventHandler(this.cmdApply1_Click);
			this.lblDateIndication.BackColor = SystemColors.Info;
			this.lblDateIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblDateIndication.ForeColor = SystemColors.ControlText;
			this.lblDateIndication.Location = new Point(144, 88);
			this.lblDateIndication.Name = "lblDateIndication";
			this.lblDateIndication.Size = new System.Drawing.Size(128, 20);
			this.lblDateIndication.TabIndex = 8;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(4, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Текущие показания";
			this.numNewIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numNewIndication.DecimalPlaces = 2;
			this.numNewIndication.Enabled = false;
			this.numNewIndication.Location = new Point(144, 112);
			NumericUpDown num = this.numNewIndication;
			int[] numArray = new int[] { 99999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numNewIndication.Name = "numNewIndication";
			this.numNewIndication.Size = new System.Drawing.Size(128, 20);
			this.numNewIndication.TabIndex = 6;
			this.numNewIndication.TextAlign = HorizontalAlignment.Right;
			this.numNewIndication.KeyPress += new KeyPressEventHandler(this.numNewIndication_KeyPress);
			this.numNewIndication.Enter += new EventHandler(this.numNewIndication_Enter);
			this.numNewIndication.Leave += new EventHandler(this.numNewIndication_Leave);
			this.lblCurrentIndication.BackColor = SystemColors.Info;
			this.lblCurrentIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblCurrentIndication.ForeColor = SystemColors.ControlText;
			this.lblCurrentIndication.Location = new Point(144, 64);
			this.lblCurrentIndication.Name = "lblCurrentIndication";
			this.lblCurrentIndication.Size = new System.Drawing.Size(128, 20);
			this.lblCurrentIndication.TabIndex = 6;
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(4, 112);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(120, 16);
			this.label18.TabIndex = 4;
			this.label18.Text = "Новые показания";
			this.label19.ForeColor = SystemColors.ControlText;
			this.label19.Location = new Point(4, 88);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(148, 16);
			this.label19.TabIndex = 3;
			this.label19.Text = "Дата текущих показаний";
			this.groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.groupBox4.Controls.Add(this.panel1);
			this.groupBox4.ForeColor = SystemColors.Desktop;
			this.groupBox4.Location = new Point(300, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(424, 385);
			this.groupBox4.TabIndex = 59;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Принятые показания";
			this.panel1.Controls.Add(this.tbData);
			this.panel1.Controls.Add(this.lv);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(418, 366);
			this.panel1.TabIndex = 9;
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
			this.tbData.Size = new System.Drawing.Size(418, 24);
			this.tbData.TabIndex = 4;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.toolBarButton16.ImageIndex = 1;
			this.toolBarButton16.Tag = "Edit";
			this.toolBarButton16.ToolTipText = "Редактировать";
			this.toolBarButton23.ImageIndex = 2;
			this.toolBarButton23.Tag = "Del";
			this.toolBarButton23.ToolTipText = "Удалить";
			this.toolBarButton25.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton26.ImageIndex = 3;
			this.toolBarButton26.Tag = "Excel";
			this.toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader4, this.columnHeader5, this.columnHeader3 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(1, 24);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(416, 337);
			this.lv.TabIndex = 3;
			this.lv.View = View.Details;
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.lv.KeyUp += new KeyEventHandler(this.lv_KeyUp);
			this.columnHeader1.Text = "Л/с";
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 108;
			this.columnHeader4.Text = "Показание";
			this.columnHeader4.Width = 66;
			this.columnHeader5.Text = "Потребление";
			this.columnHeader5.Width = 62;
			this.columnHeader3.Text = "Дата";
			this.columnHeader3.Width = 95;
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
			this.cmd_toolBarButton23.ImageIndex = 2;
			this.cmd_toolBarButton23.Name = "cmd_toolBarButton23";
			this.cmd_toolBarButton23.ToolTipText = "Удалить";
			this.statusBar1.Location = new Point(0, 399);
			this.statusBar1.Name = "statusBar1";
			StatusBar.StatusBarPanelCollection panels = this.statusBar1.Panels;
			StatusBarPanel[] statusBarPanelArray = new StatusBarPanel[] { this.statusBarPanel1, this.statusBarPanel2 };
			panels.AddRange(statusBarPanelArray);
			this.statusBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(728, 24);
			this.statusBar1.TabIndex = 60;
			this.statusBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Text = "Принято показаний:";
			this.statusBarPanel1.Width = 121;
			this.statusBarPanel2.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel2.Text = "Потребление в куб.:";
			this.statusBarPanel2.Width = 591;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(728, 423);
			base.Controls.Add(this.statusBar1);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.Name = "frmReceptionIndication";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Прием показаний от потребителей";
			base.Closing += new CancelEventHandler(this.frmReceptionIndication_Closing);
			base.Load += new EventHandler(this.frmReceptionIndication_Load);
			base.KeyUp += new KeyEventHandler(this.frmReceptionIndication_KeyUp);
			this.tabPage2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.cmbTypeIndication).EndInit();
			((ISupportInitialize)this.numNewIndication).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.CommandHolder).EndInit();
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel2).EndInit();
			base.ResumeLayout(false);
		}

		private void lv_DoubleClick(object sender, EventArgs e)
		{
			this.EditIndication();
		}

		private void lv_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.EditIndication();
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
				this.cmdApply1.Focus();
			}
		}

		private void numNewIndication_Leave(object sender, EventArgs e)
		{
			try
			{
				if (this.tmpIndication.oFactUses.get_Count() > 0)
				{
					this.OldFactAmount = this.tmpIndication.oFactUses[0].FactAmount;
				}
				this.FactAmount = -1;
				if (this.numNewIndication.Value > new decimal(0))
				{
					string str = "";
					try
					{
						this.tmpIndication.Datedisplay = (DateTime)this.dtDate.Value;
						this.tmpIndication.Display = Convert.ToDouble(this.numNewIndication.Value);
						str = this.tmpIndication.CalcFactUse();
						this.FactAmount = Convert.ToDouble(str);
					}
					catch
					{
						MessageBox.Show(str, "Показания прибора учета", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						this.numNewIndication.Value = new decimal(0);
						this.numNewIndication.Focus();
					}
				}
				this.lblFactUse.Text = Convert.ToString(Math.Round(this.FactAmount, 2));
			}
			catch
			{
				MessageBox.Show("Ошибка расчета потребления!", "Ввод показаний", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
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
			this.lblCurrentIndication.Text = "";
			this.lblDateIndication.Text = "";
			this.numNewIndication.Value = new decimal(0);
			this.numNewIndication.Enabled = false;
			this._contract = null;
			this._gobject = null;
			this._gmeter = null;
			this.lv.Items[this.ind].BackColor = SystemColors.Window;
		}

		private void ShowContractInfo()
		{
			this.lblFactUse.Text = "";
			Label label = this.lblFIO;
			Label label1 = this.lblFIO2;
			string fullName = this._contract.oPerson.FullName;
			string str = fullName;
			label1.Text = fullName;
			label.Text = str;
			Label label2 = this.lblAddress;
			Label label3 = this.lblAddress2;
			string shortAddress = this._contract.oPerson.oAddress.get_ShortAddress();
			str = shortAddress;
			label3.Text = shortAddress;
			label2.Text = str;
			Label label4 = this.lblCountLives;
			Label label5 = this.lblCountLives2;
			int countLives = this._contract.oGobjects[0].CountLives;
			string str1 = countLives.ToString();
			str = str1;
			label5.Text = str1;
			label4.Text = str;
			this._address = this._contract.oPerson.oAddress;
			this._gobject = this._contract.oGobjects[0];
			this._gmeter = this._gobject.GetActiveGMeter();
			if (this._gmeter != null)
			{
				Label label6 = this.lblPU;
				string str2 = "Подключен";
				str = str2;
				this.lblPU2.Text = str2;
				label6.Text = str;
				if (this._gmeter.GetCurrentIndication().oTypeIndication == null)
				{
					this.lblCurrentIndication.Text = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
				}
				else
				{
					this.lblCurrentIndication.Text = string.Concat(Convert.ToString(this._gmeter.GetCurrentIndication().Display), ", ", this._gmeter.GetCurrentIndication().oTypeIndication.get_Name());
				}
				Label str3 = this.lblDateIndication;
				DateTime datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
				str3.Text = Convert.ToString(datedisplay.ToShortDateString());
				this.numNewIndication.Value = new decimal(0);
				if (!Tools.IsFirstWorkDay((DateTime)this.dtDate.Value, true, Depot.CurrentPeriod.DateBegin))
				{
					this.numNewIndication.Enabled = true;
				}
				else
				{
					this.numNewIndication.Enabled = false;
				}
				this.cmdApply1.Enabled = true;
				this.numNewIndication.Focus();
				this.tmpIndication = this._indications.Add();
				this.tmpIndication.oGmeter = this._gmeter;
			}
			else
			{
				Label label7 = this.lblPU;
				string str4 = "Не подключен";
				str = str4;
				this.lblPU2.Text = str4;
				label7.Text = str;
				this.numNewIndication.Value = new decimal(0);
				this.numNewIndication.Enabled = false;
				this.cmdApply1.Enabled = false;
			}
			this._address = null;
			this._gobject = null;
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedIndex == 0 && this._contract != null)
			{
				this.txtAccount.Text = this._contract.Account;
			}
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
						this.EditIndication();
					}
					else if ((object)str1 == (object)"Del")
					{
						this.DeleteIndication();
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

		private void txtAccount_Leave(object sender, EventArgs e)
		{
			this.cmdAccount_Click(null, null);
		}
	}
}