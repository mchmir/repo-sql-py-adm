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
	public class frmReceptionIndicationTemps : Form
	{
		private ToolTip toolTip1;

		private ImageList imageList1;

		private IContainer components;

		private C1DateEdit dtDate;

		private Label label1;

		private GroupBox groupBox3;

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

		private StatusBar statusBar1;

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel2;

		private Address _address;

		private Contract _contract;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private IndicationsTemp _indications;

		private IndicationTemp tmpIndicationTemp;

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

		private ColumnHeader columnHeader3;

		private GroupBox groupBox2;

		private Label lblCountLives;

		private Label lblPU;

		private Label label13;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private ColumnHeader columnHeader6;

		private Label lblAccount;

		private Label lblNewIndication;

		private ColumnHeader columnHeader5;

		private int ind = 0;

		public frmReceptionIndicationTemps()
		{
			this.InitializeComponent();
		}

		private void cmbTypeIndication_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.cmdApply1.Focus();
			}
		}

		private void cmdAccountClick()
		{
			try
			{
				if (this.lblAccount.Text.Length != 0)
				{
					Contracts contract = new Contracts();
					if (contract.Load(this.lblAccount.Text.Trim()) != 0)
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
						this.lblFactUse.Text = "";
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
							if (this._gmeter.GetCurrentIndication().oTypeIndication == null)
							{
								this.lblCurrentIndication.Text = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
							}
							else
							{
								this.lblCurrentIndication.Text = string.Concat(Convert.ToString(this._gmeter.GetCurrentIndication().Display), ", ", this._gmeter.GetCurrentIndication().oTypeIndication.get_Name());
							}
							Label label = this.lblDateIndication;
							DateTime datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
							label.Text = Convert.ToString(datedisplay.ToShortDateString());
							this.cmdApply1.Enabled = true;
						}
						else
						{
							this.lblPU.Text = "Не подключен";
							this.cmdApply1.Enabled = false;
						}
						if (this.tmpIndicationTemp.State != 0)
						{
							this.cmdApply1.Enabled = false;
						}
						else
						{
							this.cmdApply1.Enabled = true;
						}
						this.numNewIndicationLeave();
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
					if (this._contract == null)
					{
						return;
					}
					else if (this.lblNewIndication.Text == "0")
					{
						return;
					}
					else if (this.cmbTypeIndication.SelectedIndex == -1)
					{
						MessageBox.Show("Необходимо указать тип показаний!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbTypeIndication.Focus();
						return;
					}
					else if (Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex].get_ID() != (long)3 && this._gmeter.oTypeVerify.get_ID() == (long)2)
					{
						MessageBox.Show("ПУ забракован! Тип показаний может быть только 'по акту'!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbTypeIndication.Focus();
						return;
					}
					else if (this.FactAmount < 0)
					{
						MessageBox.Show("Не верно указаны показания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					else if (this.FactAmount <= 10 || MessageBox.Show(string.Concat("Потребление составляет ", this.FactAmount.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						this.tmpIndication.oTypeIndication = Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex];
						if (this.cmbAgent.Enabled)
						{
							this.tmpIndication.oAgent = this._agents[this.cmbAgent.SelectedIndex];
						}
						this.tmpIndication.get_ID();
						if (this.tmpIndication.Save() == 0)
						{
							this.lv.Items[this.ind].BackColor = SystemColors.Window;
							this.tmpIndication = null;
							this._contract = null;
							this._address = null;
							this._gmeter = null;
							this._gobject = null;
						}
						this.tmpIndicationTemp.State = 1;
						if (this.tmpIndicationTemp.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							this.FillOneItem(this.tmpIndicationTemp);
							this.tmpIndicationTemp = null;
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

		private void EditIndication()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				try
				{
					this.lblAccount.Text = this.lv.SelectedItems[0].SubItems[0].Text;
					this.tmpIndicationTemp = this._indications.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
					this.tmpIndication = new Indication()
					{
						Datedisplay = this.tmpIndicationTemp.Datedisplay,
						Display = this.tmpIndicationTemp.Display,
						oAgent = this.tmpIndicationTemp.oAgent,
						oGmeter = this.tmpIndicationTemp.oGmeter,
						oTypeIndication = this.tmpIndicationTemp.oTypeIndication
					};
					Tools.FillC1(Depot.oTypeIndications, this.cmbTypeIndication, this.tmpIndication.oTypeIndication.get_ID());
					if (this.tmpIndication.oAgent == null)
					{
						this.cmbAgent.Enabled = false;
					}
					else
					{
						this.cmbAgent.Enabled = true;
						Tools.FillC1(this._agents, this.cmbAgent, this.tmpIndication.oAgent.get_ID());
					}
					this.lv.Items[this.ind].BackColor = SystemColors.Window;
					this.ind = this.lv.SelectedItems[0].Index;
					this.lv.SelectedItems[0].BackColor = SystemColors.Info;
					this.lblNewIndication.Text = Convert.ToString(this.tmpIndicationTemp.Display);
					this.cmdApply1.Enabled = true;
					if (this.tmpIndicationTemp.State != 0)
					{
						this.cmdApply1.Enabled = false;
					}
					else
					{
						this.cmdApply1.Enabled = true;
					}
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
				this._indications = new IndicationsTemp();
				if (this.cmbAgent.Enabled && this.cmbAgent.SelectedIndex >= 0)
				{
					Agent item = this._agents[this.cmbAgent.SelectedIndex];
				}
				this._indications.Load((DateTime)this.dtDate.Value);
				this.lv.Items.Clear();
				this.FactsAmount = 0;
				this.indicationsCount = 0;
				foreach (IndicationTemp _indication in this._indications)
				{
					ListViewItem listViewItem = new ListViewItem(_indication.oGmeter.oGobject.oContract.Account)
					{
						Tag = _indication.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_indication.oGmeter.oGobject.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(_indication.Display));
					ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
					DateTime datedisplay = _indication.Datedisplay;
					subItems.Add(datedisplay.ToShortDateString());
					if (_indication.State != 0)
					{
						listViewItem.SubItems.Add("Проведено");
					}
					else
					{
						listViewItem.SubItems.Add("Не проведено");
					}
					if (_indication.oUserAdd == null)
					{
						ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
						datedisplay = _indication.DateAdd;
						listViewSubItemCollections.Add(string.Concat("Неизвестный, ", datedisplay.ToShortDateString()));
					}
					else
					{
						ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
						string name = _indication.oUserAdd.get_Name();
						datedisplay = _indication.DateAdd;
						subItems1.Add(string.Concat(name, " ", datedisplay.ToShortDateString()));
					}
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

		private void FillOneItem(IndicationTemp o)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.SelectedItems[0].SubItems[4].Text = "Проведено";
				this._indications.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).State = 1;
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillStatusBar()
		{
			try
			{
				this.statusBar1.Panels[0].Text = string.Concat("Принято показаний: ", Convert.ToString(this.indicationsCount));
			}
			catch
			{
			}
		}

		private void frmReceptionIndicationTemps_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmReceptionIndicationTemps_Load(object sender, EventArgs e)
		{
			try
			{
				this.dtDate.Value = DateTime.Today.Date;
				this._agents = new Agents();
				Agents agent = this._agents;
				TypeAgent[] typeAgentArray = new TypeAgent[] { Depot.oTypeAgents.item((long)1), Depot.oTypeAgents.item((long)5) };
				agent.Load(typeAgentArray);
				Tools.FillC1(this._agents, this.cmbAgent, (long)0);
				Tools.FillC1(Depot.oTypeIndications, this.cmbTypeIndication, (long)0);
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmReceptionIndicationTemps));
			this.imageList2 = new ImageList(this.components);
			this.imageList1 = new ImageList(this.components);
			this.toolTip1 = new ToolTip(this.components);
			this.lblPU = new Label();
			this.dtDate = new C1DateEdit();
			this.label1 = new Label();
			this.groupBox3 = new GroupBox();
			this.lblNewIndication = new Label();
			this.label9 = new Label();
			this.lblFactUse = new Label();
			this.label5 = new Label();
			this.cmbAgent = new C1Combo();
			this.label2 = new Label();
			this.cmbTypeIndication = new C1Combo();
			this.cmdApply1 = new Button();
			this.lblDateIndication = new Label();
			this.label4 = new Label();
			this.lblCurrentIndication = new Label();
			this.label18 = new Label();
			this.label19 = new Label();
			this.groupBox4 = new GroupBox();
			this.panel1 = new Panel();
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.CommandHolder = new C1CommandHolder();
			this.cmd_toolBarButton25 = new C1Command();
			this.cmd_toolBarButton26 = new C1Command();
			this.cmd_toolBarButton16 = new C1Command();
			this.cmd_toolBarButton23 = new C1Command();
			this.statusBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel2 = new StatusBarPanel();
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblCountLives = new Label();
			this.label13 = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.columnHeader5 = new ColumnHeader();
			((ISupportInitialize)this.dtDate).BeginInit();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.cmbTypeIndication).BeginInit();
			this.groupBox4.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.CommandHolder).BeginInit();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel2).BeginInit();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = Color.Transparent;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lblPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblPU.ForeColor = SystemColors.ControlText;
			this.lblPU.Location = new Point(152, 88);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(120, 20);
			this.lblPU.TabIndex = 7;
			this.toolTip1.SetToolTip(this.lblPU, "Статус ПУ");
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
			this.groupBox3.Controls.Add(this.lblNewIndication);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.lblFactUse);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.cmbAgent);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.cmbTypeIndication);
			this.groupBox3.Controls.Add(this.cmdApply1);
			this.groupBox3.Controls.Add(this.lblDateIndication);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.lblCurrentIndication);
			this.groupBox3.Controls.Add(this.label18);
			this.groupBox3.Controls.Add(this.label19);
			this.groupBox3.ForeColor = SystemColors.Desktop;
			this.groupBox3.Location = new Point(16, 160);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(280, 200);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Прием показаний";
			this.lblNewIndication.BackColor = SystemColors.Info;
			this.lblNewIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblNewIndication.ForeColor = SystemColors.ControlText;
			this.lblNewIndication.Location = new Point(144, 112);
			this.lblNewIndication.Name = "lblNewIndication";
			this.lblNewIndication.Size = new System.Drawing.Size(128, 20);
			this.lblNewIndication.TabIndex = 16;
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
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
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
			this.cmbTypeIndication.KeyUp += new KeyEventHandler(this.cmbTypeIndication_KeyUp);
			this.cmbTypeIndication.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
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
			this.groupBox4.Size = new System.Drawing.Size(594, 385);
			this.groupBox4.TabIndex = 59;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Принятые показания";
			this.panel1.Controls.Add(this.lv);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(588, 366);
			this.panel1.TabIndex = 9;
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader4, this.columnHeader3, this.columnHeader6, this.columnHeader5 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(1, 8);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(586, 352);
			this.lv.TabIndex = 3;
			this.lv.View = View.Details;
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.lv.KeyUp += new KeyEventHandler(this.lv_KeyUp);
			this.columnHeader1.Text = "Л/с";
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 108;
			this.columnHeader4.Text = "Показание";
			this.columnHeader4.Width = 66;
			this.columnHeader3.Text = "Дата";
			this.columnHeader3.Width = 95;
			this.columnHeader6.Text = "Статус";
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
			this.statusBar1.Size = new System.Drawing.Size(898, 24);
			this.statusBar1.TabIndex = 60;
			this.statusBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Text = "Принято показаний:";
			this.statusBarPanel1.Width = 121;
			this.statusBarPanel2.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel2.Text = "Потребление в куб.:";
			this.statusBarPanel2.Width = 761;
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
			this.groupBox2.Location = new Point(16, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 120);
			this.groupBox2.TabIndex = 61;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.lblAccount.BackColor = SystemColors.Info;
			this.lblAccount.BorderStyle = BorderStyle.FixedSingle;
			this.lblAccount.ForeColor = SystemColors.ControlText;
			this.lblAccount.Location = new Point(48, 16);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(224, 20);
			this.lblAccount.TabIndex = 11;
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(72, 88);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 10;
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 88);
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
			this.columnHeader5.Text = "Кто принял";
			this.columnHeader5.Width = 91;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(898, 423);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.statusBar1);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.Name = "frmReceptionIndicationTemps";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Журнал приема показаний от потребителей до закрытия периода";
			base.Closing += new CancelEventHandler(this.frmReceptionIndicationTemps_Closing);
			base.Load += new EventHandler(this.frmReceptionIndicationTemps_Load);
			((ISupportInitialize)this.dtDate).EndInit();
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.cmbTypeIndication).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.CommandHolder).EndInit();
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel2).EndInit();
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void lv_DoubleClick(object sender, EventArgs e)
		{
			this.EditIndication();
			this.cmdAccountClick();
		}

		private void lv_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.EditIndication();
				this.cmdAccountClick();
			}
		}

		private void numNewIndicationLeave()
		{
			try
			{
				this.FactAmount = -1;
				if (Convert.ToDouble(this.lblNewIndication.Text) > 0)
				{
					string str = "";
					try
					{
						this.tmpIndication.Datedisplay = (DateTime)this.dtDate.Value;
						this.tmpIndication.Display = Convert.ToDouble(Convert.ToDouble(this.lblNewIndication.Text));
						str = this.tmpIndication.CalcFactUse();
						this.FactAmount = Convert.ToDouble(str);
					}
					catch
					{
						MessageBox.Show(str, "Показания прибора учета", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				this.lblFactUse.Text = Convert.ToString(Math.Round(this.FactAmount, 2));
			}
			catch
			{
				MessageBox.Show("Ошибка расчета потребления!", "Ввод показаний", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void ResetFields1()
		{
			this.lblAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblCountLives.Text = "";
			this.lblPU.Text = "";
			this.lblCurrentIndication.Text = "";
			this.lblDateIndication.Text = "";
			this.lblNewIndication.Text = "";
			this._contract = null;
			this._gobject = null;
			this._gmeter = null;
			this.lv.Items[this.ind].BackColor = SystemColors.Window;
		}
	}
}