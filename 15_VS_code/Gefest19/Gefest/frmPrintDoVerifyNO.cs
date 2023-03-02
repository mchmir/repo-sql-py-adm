using C1.Win.C1Input;
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
	public class frmPrintDoVerifyNO : Form
	{
		private Button cmdClose;

		private Button cmdOK;

		private GroupBox groupBox1;

		private Label label3;

		private C1Combo cmdStatysPU;

		private Label label6;

		private C1Combo cmbAgent;

		private Label label4;

		private Label lblNameGRU;

		private Button cmdGRU;

		private TextBox txtInvNumber;

		private ImageList imageList1;

		private IContainer components;

		private GRU _gru;

		private Agents _agents;

		private C1DateEdit dtPeriod;

		private Label label1;

		private CheckBox checkBox1;

		private Label label5;

		private C1Combo cmdTypePU;

		private StatusGMeters _StatusGMeters;

		private CheckBox checkBox2;

		private TextBox txtCountMonth;

		private Label label2;

		private TypeGMeters _TypeGMeters;

		public frmPrintDoVerifyNO()
		{
			this.InitializeComponent();
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
				this._gru = gRU.item(Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
				this.txtInvNumber.Text = this._gru.InvNumber;
				this.txtInvNumber.ForeColor = SystemColors.WindowText;
				this.lblNameGRU.Text = this._gru.get_Name();
			}
			frmSimpleObj = null;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			long d = (long)0;
			long num = (long)0;
			long d1 = (long)0;
			long num1 = (long)0;
			if (this.cmdTypePU.SelectedIndex > 0)
			{
				d = this._TypeGMeters[this.cmdTypePU.SelectedIndex - 1].get_ID();
			}
			if (this.cmbAgent.Text != "Все агенты")
			{
				d1 = (long)((int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID());
			}
			if (this._gru != null)
			{
				num = this._gru.get_ID();
			}
			if (this.cmdStatysPU.Text != "По всем")
			{
				num1 = (long)((int)this._StatusGMeters[this.cmdStatysPU.SelectedIndex - 1].get_ID());
			}
			this.Rep(num, d1, num1, d);
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPrintDoVerifyNO_Load(object sender, EventArgs e)
		{
			this._TypeGMeters = new TypeGMeters();
			this._TypeGMeters.Load();
			Tools.FillC1WhithAll(this._TypeGMeters, this.cmdTypePU, (long)0, "По всем");
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)1));
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
			this._StatusGMeters = new StatusGMeters();
			this._StatusGMeters.Load();
			Tools.FillC1WhithAll(this._StatusGMeters, this.cmdStatysPU, (long)0, "По всем");
			this.cmdStatysPU.SelectedIndex = 2;
			this.dtPeriod.Value = DateTime.Today.Date;
			this.txtInvNumber.Focus();
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintDoVerifyNO));
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.groupBox1 = new GroupBox();
			this.checkBox2 = new CheckBox();
			this.label5 = new Label();
			this.cmdTypePU = new C1Combo();
			this.checkBox1 = new CheckBox();
			this.dtPeriod = new C1DateEdit();
			this.label1 = new Label();
			this.label3 = new Label();
			this.cmdStatysPU = new C1Combo();
			this.label6 = new Label();
			this.cmbAgent = new C1Combo();
			this.label4 = new Label();
			this.lblNameGRU = new Label();
			this.cmdGRU = new Button();
			this.imageList1 = new ImageList(this.components);
			this.txtInvNumber = new TextBox();
			this.txtCountMonth = new TextBox();
			this.label2 = new Label();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmdTypePU).BeginInit();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			((ISupportInitialize)this.cmdStatysPU).BeginInit();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			base.SuspendLayout();
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(268, 144);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 36;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(164, 144);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 35;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtCountMonth);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.cmdTypePU);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.dtPeriod);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cmdStatysPU);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.cmbAgent);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lblNameGRU);
			this.groupBox1.Controls.Add(this.cmdGRU);
			this.groupBox1.Controls.Add(this.txtInvNumber);
			this.groupBox1.Location = new Point(4, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(356, 140);
			this.groupBox1.TabIndex = 37;
			this.groupBox1.TabStop = false;
			this.checkBox2.Location = new Point(212, 116);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(56, 16);
			this.checkBox2.TabIndex = 77;
			this.checkBox2.Text = "+ тел.";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(20, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 76;
			this.label5.Text = "Тип ПУ:";
			this.cmdTypePU.AddItemSeparator = ';';
			this.cmdTypePU.BorderStyle = 1;
			this.cmdTypePU.Caption = "";
			this.cmdTypePU.CaptionHeight = 17;
			this.cmdTypePU.CharacterCasing = 0;
			this.cmdTypePU.ColumnCaptionHeight = 17;
			this.cmdTypePU.ColumnFooterHeight = 17;
			this.cmdTypePU.ColumnHeaders = false;
			this.cmdTypePU.ColumnWidth = 100;
			this.cmdTypePU.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmdTypePU.ContentHeight = 15;
			this.cmdTypePU.DataMode = DataModeEnum.AddItem;
			this.cmdTypePU.DeadAreaBackColor = Color.Empty;
			this.cmdTypePU.EditorBackColor = SystemColors.Window;
			this.cmdTypePU.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmdTypePU.EditorForeColor = SystemColors.WindowText;
			this.cmdTypePU.EditorHeight = 15;
			this.cmdTypePU.FlatStyle = FlatModeEnum.Flat;
			this.cmdTypePU.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmdTypePU.ItemHeight = 15;
			this.cmdTypePU.Location = new Point(68, 84);
			this.cmdTypePU.MatchEntryTimeout = (long)2000;
			this.cmdTypePU.MaxDropDownItems = 5;
			this.cmdTypePU.MaxLength = 32767;
			this.cmdTypePU.MouseCursor = Cursors.Default;
			this.cmdTypePU.Name = "cmdTypePU";
			this.cmdTypePU.RowDivider.Color = Color.DarkGray;
			this.cmdTypePU.RowDivider.Style = LineStyleEnum.None;
			this.cmdTypePU.RowSubDividerColor = Color.DarkGray;
			this.cmdTypePU.Size = new System.Drawing.Size(284, 19);
			this.cmdTypePU.TabIndex = 75;
			this.cmdTypePU.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.checkBox1.Location = new Point(172, 116);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(44, 16);
			this.checkBox1.TabIndex = 70;
			this.checkBox1.Text = "Все";
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMMyyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(68, 116);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(100, 18);
			this.dtPeriod.TabIndex = 68;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 116);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 69;
			this.label1.Text = "Период";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(4, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 67;
			this.label3.Text = "Статус ПУ:";
			this.cmdStatysPU.AddItemSeparator = ';';
			this.cmdStatysPU.BorderStyle = 1;
			this.cmdStatysPU.Caption = "";
			this.cmdStatysPU.CaptionHeight = 17;
			this.cmdStatysPU.CharacterCasing = 0;
			this.cmdStatysPU.ColumnCaptionHeight = 17;
			this.cmdStatysPU.ColumnFooterHeight = 17;
			this.cmdStatysPU.ColumnHeaders = false;
			this.cmdStatysPU.ColumnWidth = 100;
			this.cmdStatysPU.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmdStatysPU.ContentHeight = 15;
			this.cmdStatysPU.DataMode = DataModeEnum.AddItem;
			this.cmdStatysPU.DeadAreaBackColor = Color.Empty;
			this.cmdStatysPU.EditorBackColor = SystemColors.Window;
			this.cmdStatysPU.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmdStatysPU.EditorForeColor = SystemColors.WindowText;
			this.cmdStatysPU.EditorHeight = 15;
			this.cmdStatysPU.FlatStyle = FlatModeEnum.Flat;
			this.cmdStatysPU.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmdStatysPU.ItemHeight = 15;
			this.cmdStatysPU.Location = new Point(68, 64);
			this.cmdStatysPU.MatchEntryTimeout = (long)2000;
			this.cmdStatysPU.MaxDropDownItems = 5;
			this.cmdStatysPU.MaxLength = 32767;
			this.cmdStatysPU.MouseCursor = Cursors.Default;
			this.cmdStatysPU.Name = "cmdStatysPU";
			this.cmdStatysPU.RowDivider.Color = Color.DarkGray;
			this.cmdStatysPU.RowDivider.Style = LineStyleEnum.None;
			this.cmdStatysPU.RowSubDividerColor = Color.DarkGray;
			this.cmdStatysPU.Size = new System.Drawing.Size(284, 19);
			this.cmdStatysPU.TabIndex = 66;
			this.cmdStatysPU.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label6.Location = new Point(8, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 62;
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
			this.cmbAgent.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.cmbAgent.ItemHeight = 15;
			this.cmbAgent.Location = new Point(52, 16);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(300, 19);
			this.cmbAgent.TabIndex = 61;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 30;
			this.label4.Text = "РУ";
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(128, 40);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(200, 20);
			this.lblNameGRU.TabIndex = 31;
			this.cmdGRU.ForeColor = SystemColors.ControlText;
			this.cmdGRU.ImageIndex = 0;
			this.cmdGRU.ImageList = this.imageList1;
			this.cmdGRU.Location = new Point(332, 40);
			this.cmdGRU.Name = "cmdGRU";
			this.cmdGRU.Size = new System.Drawing.Size(20, 20);
			this.cmdGRU.TabIndex = 2;
			this.cmdGRU.Click += new EventHandler(this.cmdGRU_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.txtInvNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtInvNumber.Location = new Point(52, 40);
			this.txtInvNumber.Name = "txtInvNumber";
			this.txtInvNumber.Size = new System.Drawing.Size(72, 20);
			this.txtInvNumber.TabIndex = 1;
			this.txtInvNumber.Text = "";
			this.txtInvNumber.Leave += new EventHandler(this.txtInvNumber_Leave);
			this.txtInvNumber.KeyUp += new KeyEventHandler(this.txtInvNumber_KeyUp);
			this.txtInvNumber.Enter += new EventHandler(this.txtInvNumber_Enter);
			this.txtCountMonth.Location = new Point(332, 112);
			this.txtCountMonth.Name = "txtCountMonth";
			this.txtCountMonth.Size = new System.Drawing.Size(20, 20);
			this.txtCountMonth.TabIndex = 78;
			this.txtCountMonth.Text = "1";
			this.label2.Location = new Point(280, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 16);
			this.label2.TabIndex = 79;
			this.label2.Text = "кол. мес.";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(364, 174);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintDoVerifyNO";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Отчёт по ПУ без поверки";
			base.Load += new EventHandler(this.frmPrintDoVerifyNO_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmdTypePU).EndInit();
			((ISupportInitialize)this.dtPeriod).EndInit();
			((ISupportInitialize)this.cmdStatysPU).EndInit();
			((ISupportInitialize)this.cmbAgent).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDGRU, long Agent, long StatysPU, long TypePU)
		{
			int[] numArray;
			string[] strArrays;
			string[] strArrays1;
			string str;
			Form frmReport;
			string[] str1;
			this.Cursor = Cursors.WaitCursor;
			int num = 0;
			if (this.checkBox2.Checked)
			{
				num = 1;
			}
			if (!this.checkBox1.Checked)
			{
				numArray = new int[] { 2, 2, 2, 3, 2, 2 };
				str1 = new string[] { "@idgru", "@idstatusgmeter", "@idagent", "@Date", "@idtypegmeter", "@tel" };
				strArrays = str1;
				str1 = new string[] { IDGRU.ToString(), StatysPU.ToString(), Agent.ToString(), this.dtPeriod.Value.ToString(), TypePU.ToString(), num.ToString() };
				strArrays1 = str1;
				str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repDoVerifyNo.rpt");
				frmReport = new frmReports(str, strArrays, strArrays1, numArray)
				{
					Text = "Отчёт по ПУ без поверки ",
					MdiParent = base.MdiParent
				};
				frmReport.Show();
				frmReport = null;
			}
			if (this.checkBox1.Checked)
			{
				numArray = new int[] { 2, 2, 2, 2, 2 };
				str1 = new string[] { "@idgru", "@idstatusgmeter", "@idagent", "@tel", "@CM" };
				strArrays = str1;
				str1 = new string[] { IDGRU.ToString(), StatysPU.ToString(), Agent.ToString(), num.ToString(), this.txtCountMonth.Text.ToString() };
				strArrays1 = str1;
				str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repDoVerifyNoVse.rpt");
				frmReport = new frmReports(str, strArrays, strArrays1, numArray)
				{
					Text = "Отчёт по ПУ без поверки ",
					MdiParent = base.MdiParent
				};
				frmReport.Show();
				frmReport = null;
			}
			System.Windows.Forms.Cursor.Current = Cursors.Default;
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