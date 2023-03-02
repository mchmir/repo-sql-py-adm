using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmNariad : Form
	{
		private List lvContent;

		private GRU _gru;

		private Agents _agents;

		private StatusGObjects _StatusGObjects;

		private ListView lvContract;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private ImageList imageList1;

		private Label label4;

		private Label lblNameGRU;

		private TextBox txtInvNumber;

		private Label label6;

		private C1Combo cmbAgent;

		private Label label3;

		private TextBox txtSumma;

		private Label label5;

		private C1Combo cmdStatusGO;

		private Button cmdSearch;

		private Button cmdPrint;

		private IContainer components;

		private ColumnHeader columnHeader10;

		private ComboBox comboBox1;

		private Label label1;

		private ListViewSortManager m_sortMgr1;

		public frmNariad()
		{
			this.InitializeComponent();
		}

		private void cmdPrint_Click(object sender, EventArgs e)
		{
			string str = "";
			foreach (ListViewItem item in this.lvContract.Items)
			{
				if (!item.Checked)
				{
					continue;
				}
				string str1 = item.Tag.ToString();
				char[] chr = new char[] { Convert.ToChar(";") };
				string[] strArrays = str1.Split(chr);
				str = string.Concat(str, strArrays[0], ",");
			}
			str = str.Substring(0, str.Length - 1);
			if (str.Length > 7000)
			{
				MessageBox.Show("Сократите колличество выбранных лицевых счетов.");
				return;
			}
			this.Rep(str);
		}

		private void cmdSearch_Click(object sender, EventArgs e)
		{
			long d;
			if (this.lvContent == null)
			{
				this.lvContent = new List();
			}
			string str = "";
			this.lvContract.Items.Clear();
			string[] strArrays = new string[] { "Ждите идет загрузка... ", "" };
			Depot.status = strArrays;
			try
			{
				str = " select * from ( select c.idcontract, p.idperson, c.account, case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO, isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(str(tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract, ag.Name, dbo.fLastIndicationDate(gm.idgmeter, GetDate()) dateind from person p inner join contract c on c.idperson=p.idperson inner join gobject g on g.idcontract=c.idcontract inner join gru on gru.idgru=g.idgru ";
				if (this._gru != null)
				{
					d = this._gru.get_ID();
					str = string.Concat(str, " and gru.idgru=", d.ToString());
				}
				str = string.Concat(str, " inner join agent ag on ag.IDAgent=gru.IDAgent ");
				if (this.comboBox1.SelectedIndex > 0)
				{
					if (this.comboBox1.Text == "Юридические лица")
					{
						str = string.Concat(str, " and dbo.fGetIsJuridical(c.idperson,dbo.fGetNowPeriod())=1");
					}
					if (this.comboBox1.Text == "Физические лица")
					{
						str = string.Concat(str, " and dbo.fGetIsJuridical(c.idperson,dbo.fGetNowPeriod())=0");
					}
				}
				if (this.cmbAgent.SelectedIndex > 0)
				{
					d = this._agents[this.cmbAgent.SelectedIndex - 1].get_ID();
					str = string.Concat(str, " and ag.idagent=", d.ToString());
				}
				str = string.Concat(str, " inner join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject ");
				if (this.cmdStatusGO.SelectedIndex > 0)
				{
					d = this._StatusGObjects[this.cmdStatusGO.SelectedIndex - 1].get_ID();
					str = string.Concat(str, " and g.IDStatusGObject=", d.ToString());
				}
				str = string.Concat(str, " left join address a on a.idaddress=g.idaddress left join house h on h.idhouse=a.idhouse left join street s on s.idstreet=h.idstreet left join gmeter gm on g.idgobject=gm.idgobject and gm.idstatusgmeter=1 left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter ) qq");
				if (Convert.ToDouble(this.txtSumma.Text) > 0)
				{
					str = string.Concat(str, " where balance>", Convert.ToDouble(this.txtSumma.Text));
				}
				if (Convert.ToDouble(this.txtSumma.Text) < 0)
				{
					str = string.Concat(str, " where balance<", Convert.ToDouble(this.txtSumma.Text));
				}
				str = string.Concat(str, " order by account");
				this.lvContent.set_nametable_pr("contract");
				this.lvContent.set_select_pr(str);
				this.lvContent.Load();
				foreach (string[] mylistPr in this.lvContent.get_mylist_pr())
				{
					ListViewItem listViewItem = this.lvContract.Items.Add(mylistPr[2]);
					listViewItem.SubItems.Add(mylistPr[3]);
					listViewItem.SubItems.Add(mylistPr[4]);
					listViewItem.SubItems.Add(mylistPr[5]);
					listViewItem.SubItems.Add(mylistPr[6]);
					listViewItem.SubItems.Add(mylistPr[7]);
					listViewItem.SubItems.Add(mylistPr[8]);
					listViewItem.SubItems.Add(mylistPr[9]);
					listViewItem.SubItems.Add(mylistPr[10]);
					if (mylistPr[11].ToLower() == "null")
					{
						listViewItem.SubItems.Add(DateTime.MinValue.ToShortDateString());
					}
					else
					{
						listViewItem.SubItems.Add(mylistPr[11]);
					}
					listViewItem.Checked = true;
					listViewItem.Tag = string.Concat(mylistPr[0], ";", mylistPr[1]);
				}
				if (this.lvContract.Items.Count > 0)
				{
					this.lvContract.Focus();
					this.lvContract.Items[0].Selected = true;
				}
			}
			catch
			{
			}
			strArrays = new string[2];
			int count = this.lvContract.Items.Count;
			strArrays[0] = string.Concat("Загружено: ", count.ToString());
			strArrays[1] = "";
			Depot.status = strArrays;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EditContract()
		{
			if (this.lvContract.SelectedItems.Count > 0)
			{
				string str = this.lvContract.SelectedItems[0].Tag.ToString();
				char[] chr = new char[] { Convert.ToChar(";") };
				string[] strArrays = str.Split(chr);
				if (Convert.ToInt64(strArrays[0]) > (long)0)
				{
					Contract contract = new Contract();
					contract.Load(Convert.ToInt64(strArrays[0]));
					(new frmContract(contract)).ShowDialog(this);
					contract = null;
				}
				this.lvContract.Select();
			}
		}

		private void EditPerson()
		{
			Form _frmPerson;
			if (this.lvContract.SelectedItems.Count > 0)
			{
				string str = this.lvContract.SelectedItems[0].Tag.ToString();
				char[] chr = new char[] { Convert.ToChar(";") };
				string[] strArrays = str.Split(chr);
				if (Convert.ToInt64(strArrays[1]) > (long)0)
				{
					Person person = new Person();
					person.Load(Convert.ToInt64(strArrays[1]));
					if (person.isJuridical != 1)
					{
						_frmPerson = new frmPerson(person);
					}
					else
					{
						_frmPerson = new frmJPerson(person);
					}
					_frmPerson.ShowDialog(this);
					_frmPerson = null;
					this.lvContract.SelectedItems[0].SubItems[1].Text = person.FullName;
				}
			}
		}

		private void frmNariad_Load(object sender, EventArgs e)
		{
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)1));
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
			this._StatusGObjects = new StatusGObjects();
			this._StatusGObjects.Load();
			Tools.FillC1WhithAll(this._StatusGObjects, this.cmdStatusGO, (long)1, "По всем");
			ListView listView = this.lvContract;
			Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDateSort) };
			this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
		}

		private void GetGRU()
		{
			GRUs gRU = new GRUs();
			gRU.Load();
			this._gru = null;
			foreach (GRU gRU1 in gRU)
			{
				if (gRU1.InvNumber != this.txtInvNumber.Text.Trim())
				{
					continue;
				}
				this._gru = gRU1;
				this.txtInvNumber.Text = this._gru.InvNumber;
				this.txtInvNumber.ForeColor = SystemColors.WindowText;
				this.lblNameGRU.Text = this._gru.get_Name();
				return;
			}
			this.lblNameGRU.Text = "Укажите номер РУ";
			this.txtInvNumber.ForeColor = Color.Red;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmNariad));
			this.lvContract = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.imageList1 = new ImageList(this.components);
			this.label4 = new Label();
			this.lblNameGRU = new Label();
			this.txtInvNumber = new TextBox();
			this.label6 = new Label();
			this.cmbAgent = new C1Combo();
			this.label3 = new Label();
			this.txtSumma = new TextBox();
			this.label5 = new Label();
			this.cmdStatusGO = new C1Combo();
			this.cmdSearch = new Button();
			this.cmdPrint = new Button();
			this.comboBox1 = new ComboBox();
			this.label1 = new Label();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.cmdStatusGO).BeginInit();
			base.SuspendLayout();
			this.lvContract.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lvContract.BorderStyle = BorderStyle.FixedSingle;
			this.lvContract.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns = this.lvContract.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6, this.columnHeader7, this.columnHeader8, this.columnHeader9, this.columnHeader10 };
			columns.AddRange(columnHeaderArray);
			this.lvContract.FullRowSelect = true;
			this.lvContract.GridLines = true;
			this.lvContract.Location = new Point(0, 104);
			this.lvContract.MultiSelect = false;
			this.lvContract.Name = "lvContract";
			this.lvContract.Size = new System.Drawing.Size(848, 344);
			this.lvContract.TabIndex = 6;
			this.lvContract.View = View.Details;
			this.columnHeader1.Text = "Л/с";
			this.columnHeader1.Width = 76;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 143;
			this.columnHeader3.Text = "Адрес";
			this.columnHeader3.Width = 157;
			this.columnHeader4.Text = "Прож.";
			this.columnHeader4.TextAlign = HorizontalAlignment.Right;
			this.columnHeader5.Text = "Сальдо";
			this.columnHeader5.TextAlign = HorizontalAlignment.Right;
			this.columnHeader5.Width = 76;
			this.columnHeader6.Text = "ПУ";
			this.columnHeader6.Width = 79;
			this.columnHeader7.Text = "ОУ";
			this.columnHeader7.Width = 76;
			this.columnHeader8.Text = "Договор";
			this.columnHeader8.Width = 85;
			this.columnHeader9.Text = "Контролер";
			this.columnHeader9.Width = 81;
			this.columnHeader10.Text = "Дата показ.";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 71;
			this.label4.Text = "РУ:";
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(104, 40);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(216, 20);
			this.lblNameGRU.TabIndex = 73;
			this.txtInvNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtInvNumber.Location = new Point(48, 40);
			this.txtInvNumber.Name = "txtInvNumber";
			this.txtInvNumber.Size = new System.Drawing.Size(52, 20);
			this.txtInvNumber.TabIndex = 66;
			this.txtInvNumber.Text = "";
			this.txtInvNumber.Leave += new EventHandler(this.txtInvNumber_Leave);
			this.txtInvNumber.KeyUp += new KeyEventHandler(this.txtInvNumber_KeyUp);
			this.txtInvNumber.Enter += new EventHandler(this.txtInvNumber_Enter);
			this.label6.Location = new Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 69;
			this.label6.Text = "Агент:";
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
			this.cmbAgent.Location = new Point(48, 8);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(272, 19);
			this.cmbAgent.TabIndex = 65;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(336, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 16);
			this.label3.TabIndex = 72;
			this.label3.Text = "Сумма дол. или пер.:";
			this.txtSumma.BorderStyle = BorderStyle.FixedSingle;
			this.txtSumma.Location = new Point(456, 40);
			this.txtSumma.Name = "txtSumma";
			this.txtSumma.Size = new System.Drawing.Size(112, 20);
			this.txtSumma.TabIndex = 68;
			this.txtSumma.Text = "-3000";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(336, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 70;
			this.label5.Text = "Статус ОУ:";
			this.cmdStatusGO.AddItemSeparator = ';';
			this.cmdStatusGO.BorderStyle = 1;
			this.cmdStatusGO.Caption = "";
			this.cmdStatusGO.CaptionHeight = 17;
			this.cmdStatusGO.CharacterCasing = 0;
			this.cmdStatusGO.ColumnCaptionHeight = 17;
			this.cmdStatusGO.ColumnFooterHeight = 17;
			this.cmdStatusGO.ColumnHeaders = false;
			this.cmdStatusGO.ColumnWidth = 100;
			this.cmdStatusGO.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmdStatusGO.ContentHeight = 15;
			this.cmdStatusGO.DataMode = DataModeEnum.AddItem;
			this.cmdStatusGO.DeadAreaBackColor = Color.Empty;
			this.cmdStatusGO.EditorBackColor = SystemColors.Window;
			this.cmdStatusGO.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmdStatusGO.EditorForeColor = SystemColors.WindowText;
			this.cmdStatusGO.EditorHeight = 15;
			this.cmdStatusGO.FlatStyle = FlatModeEnum.Flat;
			this.cmdStatusGO.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmdStatusGO.ItemHeight = 15;
			this.cmdStatusGO.Location = new Point(400, 8);
			this.cmdStatusGO.MatchEntryTimeout = (long)2000;
			this.cmdStatusGO.MaxDropDownItems = 5;
			this.cmdStatusGO.MaxLength = 32767;
			this.cmdStatusGO.MouseCursor = Cursors.Default;
			this.cmdStatusGO.Name = "cmdStatusGO";
			this.cmdStatusGO.RowDivider.Color = Color.DarkGray;
			this.cmdStatusGO.RowDivider.Style = LineStyleEnum.None;
			this.cmdStatusGO.RowSubDividerColor = Color.DarkGray;
			this.cmdStatusGO.Size = new System.Drawing.Size(168, 19);
			this.cmdStatusGO.TabIndex = 67;
			this.cmdStatusGO.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.cmdSearch.FlatStyle = FlatStyle.Flat;
			this.cmdSearch.ImageAlign = ContentAlignment.MiddleLeft;
			this.cmdSearch.ImageIndex = 6;
			this.cmdSearch.ImageList = this.imageList1;
			this.cmdSearch.Location = new Point(576, 8);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(160, 24);
			this.cmdSearch.TabIndex = 74;
			this.cmdSearch.Text = "Поиск";
			this.cmdSearch.Click += new EventHandler(this.cmdSearch_Click);
			this.cmdPrint.FlatStyle = FlatStyle.Flat;
			this.cmdPrint.ImageAlign = ContentAlignment.MiddleLeft;
			this.cmdPrint.ImageIndex = 11;
			this.cmdPrint.ImageList = this.imageList1;
			this.cmdPrint.Location = new Point(576, 40);
			this.cmdPrint.Name = "cmdPrint";
			this.cmdPrint.Size = new System.Drawing.Size(160, 24);
			this.cmdPrint.TabIndex = 75;
			this.cmdPrint.Text = "Печать";
			this.cmdPrint.Click += new EventHandler(this.cmdPrint_Click);
			this.comboBox1.Items.AddRange(new object[] { "По всем", "Юридические лица", "Физические лица" });
			this.comboBox1.Location = new Point(112, 72);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(208, 21);
			this.comboBox1.TabIndex = 76;
			this.comboBox1.Text = "По всем";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 77;
			this.label1.Text = "Тип потребителя:";
			base.AcceptButton = this.cmdSearch;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(848, 445);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.cmdPrint);
			base.Controls.Add(this.cmdSearch);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.lblNameGRU);
			base.Controls.Add(this.txtInvNumber);
			base.Controls.Add(this.txtSumma);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.cmbAgent);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.cmdStatusGO);
			base.Controls.Add(this.lvContract);
			base.Name = "frmNariad";
			this.Text = "Наряд на отключение";
			base.Load += new EventHandler(this.frmNariad_Load);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.cmdStatusGO).EndInit();
			base.ResumeLayout(false);
		}

		private void PrintAccount()
		{
			if (this.lvContract.SelectedItems.Count > 0)
			{
				string str = this.lvContract.SelectedItems[0].Tag.ToString();
				char[] chr = new char[] { Convert.ToChar(";") };
				string[] strArrays = str.Split(chr);
				if (Convert.ToInt64(strArrays[0]) > (long)0)
				{
					Contract contract = new Contract();
					contract.Load(Convert.ToInt64(strArrays[0]));
					frmPrintAccount _frmPrintAccount = new frmPrintAccount()
					{
						_contract = contract,
						MdiParent = Depot._main
					};
					_frmPrintAccount.Show();
					_frmPrintAccount = null;
					contract = null;
				}
				this.lvContract.Select();
			}
		}

		private void Rep(string strContract)
		{
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 1 };
					string[] strArrays = new string[] { "@strContract" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { strContract };
					string[] strArrays2 = strArrays;
					string str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repNariadForCut.rpt");
					Form frmReport = new frmReports(str, strArrays1, strArrays2, numArray)
					{
						Text = "Наряд на отключение",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}

		private void tbMain_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			"Excel";
			string str = e.Button.Tag.ToString();
			string str1 = str;
			if (str != null)
			{
				str1 = string.IsInterned(str1);
				if ((object)str1 == (object)"EditC")
				{
					this.EditContract();
					return;
				}
				if ((object)str1 == (object)"EditP")
				{
					this.EditPerson();
					return;
				}
				if ((object)str1 == (object)"PrintC")
				{
					this.PrintAccount();
					return;
				}
				if ((object)str1 != (object)"Excel")
				{
					return;
				}
				Tools.ConvertToExcel(this.lvContract);
			}
		}

		private void txtInvNumber_Enter(object sender, EventArgs e)
		{
			this.txtInvNumber.SelectAll();
			base.AcceptButton = null;
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
			base.AcceptButton = this.cmdSearch;
		}
	}
}