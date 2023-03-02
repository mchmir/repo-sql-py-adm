using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintSaldoPoKvart : Form
	{
		private C1Combo cmbAgent;

		private Label label6;

		private GroupBox groupBox1;

		private ImageList imageList1;

		private Label label3;

		private Label label5;

		private C1Combo cmdStatusGO;

		private Button cmdClose;

		private Button cmdOK;

		private C1DateEdit dtPeriod;

		private Label label7;

		private IContainer components;

		private GRU _gru;

		private Agents _agents;

		private TextBox txtSumma;

		private Label label8;

		private C1Combo cmdStatysPU;

		private StatusGObjects _StatusGObjects;

		private CheckBox chkJL;

		private StatusGMeters _StatusGMeters;

		public frmPrintSaldoPoKvart()
		{
			this.InitializeComponent();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			long d = (long)0;
			long num = (long)0;
			long d1 = (long)0;
			long num1 = (long)0;
			Periods period = new Periods();
			period.Load((DateTime)this.dtPeriod.Value);
			if (this.cmbAgent.Text != "Все агенты")
			{
				d = (long)((int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID());
			}
			if (this.cmdStatusGO.Text != "По всем")
			{
				num = (long)((int)this._StatusGObjects[this.cmdStatusGO.SelectedIndex - 1].get_ID());
			}
			if (this.cmdStatysPU.Text != "По всем")
			{
				d1 = (long)((int)this._StatusGMeters[this.cmdStatysPU.SelectedIndex - 1].get_ID());
			}
			if (this.chkJL.Checked)
			{
				num1 = (long)1;
			}
			long num2 = (long)Convert.ToInt32(this.txtSumma.Text);
			if (period.get_Count() <= 0)
			{
				MessageBox.Show("Не верный выбор периода!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			this.Rep(period[0].get_ID(), d, num, num2, d1, num1);
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

		private void frmPrintSaldoPoKvart_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmPrintSaldoPoKvart_Load(object sender, EventArgs e)
		{
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)1));
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
			this._StatusGObjects = new StatusGObjects();
			this._StatusGObjects.Load();
			Tools.FillC1WhithAll(this._StatusGObjects, this.cmdStatusGO, (long)0, "По всем");
			this._StatusGMeters = new StatusGMeters();
			this._StatusGMeters.Load();
			Tools.FillC1WhithAll(this._StatusGMeters, this.cmdStatysPU, (long)0, "По всем");
			this.dtPeriod.Value = DateTime.Today.Date;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintSaldoPoKvart));
			this.cmbAgent = new C1Combo();
			this.label6 = new Label();
			this.groupBox1 = new GroupBox();
			this.label8 = new Label();
			this.cmdStatysPU = new C1Combo();
			this.dtPeriod = new C1DateEdit();
			this.label7 = new Label();
			this.label3 = new Label();
			this.txtSumma = new TextBox();
			this.label5 = new Label();
			this.cmdStatusGO = new C1Combo();
			this.imageList1 = new ImageList(this.components);
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.chkJL = new CheckBox();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmdStatysPU).BeginInit();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			((ISupportInitialize)this.cmdStatusGO).BeginInit();
			base.SuspendLayout();
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
			this.cmbAgent.Location = new Point(52, 12);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(272, 19);
			this.cmbAgent.TabIndex = 1;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label6.Location = new Point(8, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 60;
			this.label6.Text = "Агент:";
			this.groupBox1.Controls.Add(this.chkJL);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.cmdStatysPU);
			this.groupBox1.Controls.Add(this.dtPeriod);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.cmbAgent);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtSumma);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.cmdStatusGO);
			this.groupBox1.Location = new Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(332, 148);
			this.groupBox1.TabIndex = 61;
			this.groupBox1.TabStop = false;
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(8, 72);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 16);
			this.label8.TabIndex = 69;
			this.label8.Text = "Статус ПУ:";
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
			this.cmdStatysPU.Location = new Point(72, 64);
			this.cmdStatysPU.MatchEntryTimeout = (long)2000;
			this.cmdStatysPU.MaxDropDownItems = 5;
			this.cmdStatysPU.MaxLength = 32767;
			this.cmdStatysPU.MouseCursor = Cursors.Default;
			this.cmdStatysPU.Name = "cmdStatysPU";
			this.cmdStatysPU.RowDivider.Color = Color.DarkGray;
			this.cmdStatysPU.RowDivider.Style = LineStyleEnum.None;
			this.cmdStatysPU.RowSubDividerColor = Color.DarkGray;
			this.cmdStatysPU.Size = new System.Drawing.Size(252, 19);
			this.cmdStatysPU.TabIndex = 68;
			this.cmdStatysPU.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMMyyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(72, 88);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(124, 18);
			this.dtPeriod.TabIndex = 65;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(8, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 16);
			this.label7.TabIndex = 66;
			this.label7.Text = "Период";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 16);
			this.label3.TabIndex = 63;
			this.label3.Text = "Сумма дол. или пер.:";
			this.txtSumma.BorderStyle = BorderStyle.FixedSingle;
			this.txtSumma.Location = new Point(124, 120);
			this.txtSumma.Name = "txtSumma";
			this.txtSumma.Size = new System.Drawing.Size(72, 20);
			this.txtSumma.TabIndex = 5;
			this.txtSumma.Text = "100";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 63;
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
			this.cmdStatusGO.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.cmdStatusGO.ItemHeight = 15;
			this.cmdStatusGO.Location = new Point(72, 40);
			this.cmdStatusGO.MatchEntryTimeout = (long)2000;
			this.cmdStatusGO.MaxDropDownItems = 5;
			this.cmdStatusGO.MaxLength = 32767;
			this.cmdStatusGO.MouseCursor = Cursors.Default;
			this.cmdStatusGO.Name = "cmdStatusGO";
			this.cmdStatusGO.RowDivider.Color = Color.DarkGray;
			this.cmdStatusGO.RowDivider.Style = LineStyleEnum.None;
			this.cmdStatusGO.RowSubDividerColor = Color.DarkGray;
			this.cmdStatusGO.Size = new System.Drawing.Size(252, 19);
			this.cmdStatusGO.TabIndex = 4;
			this.cmdStatusGO.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = Color.Transparent;
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(244, 160);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 9;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(140, 160);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 8;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.chkJL.Location = new Point(208, 119);
			this.chkJL.Name = "chkJL";
			this.chkJL.TabIndex = 70;
			this.chkJL.Text = "юр. лица";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(338, 191);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintSaldoPoKvart";
			this.Text = "Сальдо по квартирам";
			base.Closing += new CancelEventHandler(this.frmPrintSaldoPoKvart_Closing);
			base.Load += new EventHandler(this.frmPrintSaldoPoKvart_Load);
			((ISupportInitialize)this.cmbAgent).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmdStatysPU).EndInit();
			((ISupportInitialize)this.dtPeriod).EndInit();
			((ISupportInitialize)this.cmdStatusGO).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDPeriod, long Agent, long Statys, long Amount, long StatysPU, long isJur)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2, 1, 2, 2, 2, 1 };
					string[] str = new string[] { "@idperiod", "@idagent", "@amountbalance", "@idstatusgobject", "@idstatusgmeter", "@isjuridical", "Month" };
					string[] strArrays = str;
					str = new string[] { IDPeriod.ToString(), Agent.ToString(), Amount.ToString(), Statys.ToString(), StatysPU.ToString(), isJur.ToString(), this.dtPeriod.Text.ToString() };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSaldoPoKvart.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "Сальдо по квартирам",
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
	}
}