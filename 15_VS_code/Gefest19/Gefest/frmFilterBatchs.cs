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
	public class frmFilterBatchs : Form
	{
		private Button cmdCancel;

		private Button cmdOK;

		private System.ComponentModel.Container components = null;

		private Label label6;

		private C1Combo cmbAgent;

		private Label label1;

		private Label label2;

		private C1Combo cmbTypePay;

		private C1Combo cmbStatusBatch;

		private Label label3;

		private NumericUpDown numBatchAmount;

		private C1Combo cmbTypeBatch;

		private Label label4;

		private Agents _agents;

		public frmFilterBatchs()
		{
			this.InitializeComponent();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
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

		private void frmFilterBatchs_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmFilterBatchs_Load(object sender, EventArgs e)
		{
			Tools.LoadWindows(this);
			this._agents = new Agents();
			this._agents.Load();
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
			Tools.FillC1WhithAll(Depot.oTypePays, this.cmbTypePay, (long)0, "Все");
			Tools.FillC1WhithAll(Depot.oStatusBatchs, this.cmbStatusBatch, (long)0, "Все");
			Tools.FillC1WhithAll(Depot.oTypeBatchs, this.cmbTypeBatch, (long)0, "Все");
		}

		public void GetData(ref int idagent, ref int idtypepay, ref int idstatusbatch, ref int idtypebatch, ref double summa, ref string filter)
		{
			if (this.cmbAgent.SelectedIndex == 0 || this.cmbAgent.SelectedIndex == -1)
			{
				idagent = 0;
				filter = "по всем агентам";
			}
			else
			{
				idagent = (int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID();
				filter = string.Concat("агент ", this._agents[this.cmbAgent.SelectedIndex - 1].get_Name());
			}
			if (this.cmbTypePay.SelectedIndex == 0 || this.cmbTypePay.SelectedIndex == -1)
			{
				idtypepay = 0;
				filter = string.Concat(filter, ", любая оплата");
			}
			else
			{
				idtypepay = (int)Depot.oTypePays[this.cmbTypePay.SelectedIndex - 1].get_ID();
				filter = string.Concat(filter, ", оплата ", Depot.oTypePays[this.cmbTypePay.SelectedIndex - 1].get_Name());
			}
			if (this.cmbStatusBatch.SelectedIndex == 0 || this.cmbStatusBatch.SelectedIndex == -1)
			{
				idstatusbatch = 0;
				filter = string.Concat(filter, ", все пачки");
			}
			else
			{
				idstatusbatch = (int)Depot.oStatusBatchs[this.cmbStatusBatch.SelectedIndex - 1].get_ID();
				filter = string.Concat(filter, ", пачка ", Depot.oStatusBatchs[this.cmbStatusBatch.SelectedIndex - 1].get_Name());
			}
			if (this.cmbTypeBatch.SelectedIndex == 0 || this.cmbTypeBatch.SelectedIndex == -1)
			{
				idtypebatch = 0;
				filter = string.Concat(filter, ", все типы пачек");
			}
			else
			{
				idtypebatch = (int)Depot.oTypeBatchs[this.cmbTypeBatch.SelectedIndex - 1].get_ID();
				filter = string.Concat(filter, ", тип пачки ", Depot.oTypeBatchs[this.cmbTypeBatch.SelectedIndex - 1].get_Name());
			}
			summa = Convert.ToDouble(this.numBatchAmount.Value);
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmFilterBatchs));
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.label6 = new Label();
			this.cmbAgent = new C1Combo();
			this.cmbTypePay = new C1Combo();
			this.label1 = new Label();
			this.cmbStatusBatch = new C1Combo();
			this.label2 = new Label();
			this.label3 = new Label();
			this.numBatchAmount = new NumericUpDown();
			this.cmbTypeBatch = new C1Combo();
			this.label4 = new Label();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.cmbTypePay).BeginInit();
			((ISupportInitialize)this.cmbStatusBatch).BeginInit();
			((ISupportInitialize)this.numBatchAmount).BeginInit();
			((ISupportInitialize)this.cmbTypeBatch).BeginInit();
			base.SuspendLayout();
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(216, 144);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(120, 144);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 24);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label6.Location = new Point(3, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(93, 16);
			this.label6.TabIndex = 58;
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
			this.cmbAgent.Location = new Point(96, 8);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(204, 19);
			this.cmbAgent.TabIndex = 1;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.cmbTypePay.AddItemSeparator = ';';
			this.cmbTypePay.BorderStyle = 1;
			this.cmbTypePay.Caption = "";
			this.cmbTypePay.CaptionHeight = 17;
			this.cmbTypePay.CharacterCasing = 0;
			this.cmbTypePay.ColumnCaptionHeight = 17;
			this.cmbTypePay.ColumnFooterHeight = 17;
			this.cmbTypePay.ColumnHeaders = false;
			this.cmbTypePay.ColumnWidth = 100;
			this.cmbTypePay.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypePay.ContentHeight = 15;
			this.cmbTypePay.DataMode = DataModeEnum.AddItem;
			this.cmbTypePay.DeadAreaBackColor = Color.Empty;
			this.cmbTypePay.EditorBackColor = SystemColors.Window;
			this.cmbTypePay.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypePay.EditorForeColor = SystemColors.WindowText;
			this.cmbTypePay.EditorHeight = 15;
			this.cmbTypePay.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypePay.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbTypePay.ItemHeight = 15;
			this.cmbTypePay.Location = new Point(96, 32);
			this.cmbTypePay.MatchEntryTimeout = (long)2000;
			this.cmbTypePay.MaxDropDownItems = 5;
			this.cmbTypePay.MaxLength = 32767;
			this.cmbTypePay.MouseCursor = Cursors.Default;
			this.cmbTypePay.Name = "cmbTypePay";
			this.cmbTypePay.RowDivider.Color = Color.DarkGray;
			this.cmbTypePay.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypePay.RowSubDividerColor = Color.DarkGray;
			this.cmbTypePay.Size = new System.Drawing.Size(204, 19);
			this.cmbTypePay.TabIndex = 2;
			this.cmbTypePay.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label1.Location = new Point(3, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 16);
			this.label1.TabIndex = 60;
			this.label1.Text = "Тип оплаты:";
			this.cmbStatusBatch.AddItemSeparator = ';';
			this.cmbStatusBatch.BorderStyle = 1;
			this.cmbStatusBatch.Caption = "";
			this.cmbStatusBatch.CaptionHeight = 17;
			this.cmbStatusBatch.CharacterCasing = 0;
			this.cmbStatusBatch.ColumnCaptionHeight = 17;
			this.cmbStatusBatch.ColumnFooterHeight = 17;
			this.cmbStatusBatch.ColumnHeaders = false;
			this.cmbStatusBatch.ColumnWidth = 100;
			this.cmbStatusBatch.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbStatusBatch.ContentHeight = 15;
			this.cmbStatusBatch.DataMode = DataModeEnum.AddItem;
			this.cmbStatusBatch.DeadAreaBackColor = Color.Empty;
			this.cmbStatusBatch.EditorBackColor = SystemColors.Window;
			this.cmbStatusBatch.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbStatusBatch.EditorForeColor = SystemColors.WindowText;
			this.cmbStatusBatch.EditorHeight = 15;
			this.cmbStatusBatch.FlatStyle = FlatModeEnum.Flat;
			this.cmbStatusBatch.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.cmbStatusBatch.ItemHeight = 15;
			this.cmbStatusBatch.Location = new Point(96, 56);
			this.cmbStatusBatch.MatchEntryTimeout = (long)2000;
			this.cmbStatusBatch.MaxDropDownItems = 5;
			this.cmbStatusBatch.MaxLength = 32767;
			this.cmbStatusBatch.MouseCursor = Cursors.Default;
			this.cmbStatusBatch.Name = "cmbStatusBatch";
			this.cmbStatusBatch.RowDivider.Color = Color.DarkGray;
			this.cmbStatusBatch.RowDivider.Style = LineStyleEnum.None;
			this.cmbStatusBatch.RowSubDividerColor = Color.DarkGray;
			this.cmbStatusBatch.Size = new System.Drawing.Size(204, 19);
			this.cmbStatusBatch.TabIndex = 3;
			this.cmbStatusBatch.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label2.Location = new Point(3, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 16);
			this.label2.TabIndex = 62;
			this.label2.Text = "Статус пачки:";
			this.label3.Location = new Point(3, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 16);
			this.label3.TabIndex = 64;
			this.label3.Text = "Сумма пачки:";
			this.numBatchAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numBatchAmount.DecimalPlaces = 2;
			this.numBatchAmount.Location = new Point(97, 104);
			NumericUpDown num = this.numBatchAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numBatchAmount.Name = "numBatchAmount";
			this.numBatchAmount.TabIndex = 4;
			this.cmbTypeBatch.AddItemSeparator = ';';
			this.cmbTypeBatch.BorderStyle = 1;
			this.cmbTypeBatch.Caption = "";
			this.cmbTypeBatch.CaptionHeight = 17;
			this.cmbTypeBatch.CharacterCasing = 0;
			this.cmbTypeBatch.ColumnCaptionHeight = 17;
			this.cmbTypeBatch.ColumnFooterHeight = 17;
			this.cmbTypeBatch.ColumnHeaders = false;
			this.cmbTypeBatch.ColumnWidth = 100;
			this.cmbTypeBatch.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeBatch.ContentHeight = 15;
			this.cmbTypeBatch.DataMode = DataModeEnum.AddItem;
			this.cmbTypeBatch.DeadAreaBackColor = Color.Empty;
			this.cmbTypeBatch.EditorBackColor = SystemColors.Window;
			this.cmbTypeBatch.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeBatch.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeBatch.EditorHeight = 15;
			this.cmbTypeBatch.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeBatch.Images.Add((Image)resourceManager.GetObject("resource3"));
			this.cmbTypeBatch.ItemHeight = 15;
			this.cmbTypeBatch.Location = new Point(96, 80);
			this.cmbTypeBatch.MatchEntryTimeout = (long)2000;
			this.cmbTypeBatch.MaxDropDownItems = 5;
			this.cmbTypeBatch.MaxLength = 32767;
			this.cmbTypeBatch.MouseCursor = Cursors.Default;
			this.cmbTypeBatch.Name = "cmbTypeBatch";
			this.cmbTypeBatch.RowDivider.Color = Color.DarkGray;
			this.cmbTypeBatch.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeBatch.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeBatch.Size = new System.Drawing.Size(204, 19);
			this.cmbTypeBatch.TabIndex = 65;
			this.cmbTypeBatch.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label4.Location = new Point(3, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 16);
			this.label4.TabIndex = 66;
			this.label4.Text = "Тип пачки:";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(306, 170);
			base.Controls.Add(this.cmbTypeBatch);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.numBatchAmount);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.cmbStatusBatch);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cmbTypePay);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmbAgent);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.MinimumSize = new System.Drawing.Size(249, 126);
			base.Name = "frmFilterBatchs";
			this.Text = "Фильтр";
			base.Closing += new CancelEventHandler(this.frmFilterBatchs_Closing);
			base.Load += new EventHandler(this.frmFilterBatchs_Load);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.cmbTypePay).EndInit();
			((ISupportInitialize)this.cmbStatusBatch).EndInit();
			((ISupportInitialize)this.numBatchAmount).EndInit();
			((ISupportInitialize)this.cmbTypeBatch).EndInit();
			base.ResumeLayout(false);
		}
	}
}