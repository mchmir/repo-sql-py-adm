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
	public class frmPrintBigConsumption : Form
	{
		private Button button1;

		private Button button2;

		private C1DateEdit dtBegin;

		private Label label1;

		private C1DateEdit dtEnd;

		private Label label2;

		private Label label3;

		private NumericUpDown numericUpDown1;

		private GroupBox groupBox1;

		private Label label6;

		private C1Combo cmbAgent;

		private Label label4;

		private Label lblNameGRU;

		private Button cmdGRU;

		private TextBox txtInvNumber;

		private ImageList imageList1;

		private IContainer components;

		private GRU _gru;

		private NumericUpDown numericUpDown2;

		private Label label5;

		private C1Combo cmbTypePU;

		private Label label7;

		private Agents _agents;

		private TypeGMeters _typegmeters;

		public frmPrintBigConsumption()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			long d = (long)0;
			long num = (long)0;
			long d1 = (long)0;
			if (this.cmbAgent.Text != "Все агенты")
			{
				num = (long)((int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID());
			}
			if (this.cmbTypePU.Text != "Все типы ПУ")
			{
				d1 = (long)((int)this._typegmeters[this.cmbTypePU.SelectedIndex - 1].get_ID());
			}
			if (this._gru != null)
			{
				d = this._gru.get_ID();
			}
			this.Rep(d, num, d1);
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPrintBigConsumption_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmPrintBigConsumption_Load(object sender, EventArgs e)
		{
			C1DateEdit c1DateEdit = this.dtBegin;
			DateTime today = DateTime.Today;
			DateTime dateTime = DateTime.Today;
			c1DateEdit.Value = today.AddDays((double)(-dateTime.Day + 1));
			C1DateEdit date = this.dtEnd;
			today = DateTime.Today;
			date.Value = today.Date;
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)1));
			this._typegmeters = new TypeGMeters();
			this._typegmeters.Load();
			Tools.FillC1WhithAll(this._typegmeters, this.cmbTypePU, (long)0, "Все типы ПУ");
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintBigConsumption));
			this.button1 = new Button();
			this.button2 = new Button();
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			this.dtEnd = new C1DateEdit();
			this.label2 = new Label();
			this.label3 = new Label();
			this.numericUpDown1 = new NumericUpDown();
			this.groupBox1 = new GroupBox();
			this.label6 = new Label();
			this.cmbAgent = new C1Combo();
			this.label4 = new Label();
			this.lblNameGRU = new Label();
			this.cmdGRU = new Button();
			this.imageList1 = new ImageList(this.components);
			this.txtInvNumber = new TextBox();
			this.numericUpDown2 = new NumericUpDown();
			this.label5 = new Label();
			this.cmbTypePU = new C1Combo();
			this.label7 = new Label();
			((ISupportInitialize)this.dtBegin).BeginInit();
			((ISupportInitialize)this.dtEnd).BeginInit();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.numericUpDown2).BeginInit();
			((ISupportInitialize)this.cmbTypePU).BeginInit();
			base.SuspendLayout();
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Location = new Point(216, 156);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 24);
			this.button1.TabIndex = 41;
			this.button1.Text = "Отмена";
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Location = new Point(112, 156);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 24);
			this.button2.TabIndex = 40;
			this.button2.Text = "Сформировать";
			this.button2.Click += new EventHandler(this.button2_Click);
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.FormatType = FormatTypeEnum.LongDate;
			this.dtBegin.Location = new Point(28, 12);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(120, 18);
			this.dtBegin.TabIndex = 36;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 16);
			this.label1.TabIndex = 39;
			this.label1.Text = "C";
			this.dtEnd.BorderStyle = 1;
			this.dtEnd.FormatType = FormatTypeEnum.LongDate;
			this.dtEnd.Location = new Point(180, 12);
			this.dtEnd.Name = "dtEnd";
			this.dtEnd.Size = new System.Drawing.Size(120, 18);
			this.dtEnd.TabIndex = 37;
			this.dtEnd.Tag = null;
			this.dtEnd.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtEnd.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(152, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 38;
			this.label2.Text = "по";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(4, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 39;
			this.label3.Text = "Потребление c";
			this.numericUpDown1.DecimalPlaces = 2;
			this.numericUpDown1.Location = new Point(232, 88);
			NumericUpDown num = this.numericUpDown1;
			int[] numArray = new int[] { 100000, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(68, 20);
			this.numericUpDown1.TabIndex = 42;
			NumericUpDown numericUpDown = this.numericUpDown1;
			numArray = new int[] { 10, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray);
			this.groupBox1.Controls.Add(this.cmbTypePU);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.cmbAgent);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lblNameGRU);
			this.groupBox1.Controls.Add(this.cmdGRU);
			this.groupBox1.Controls.Add(this.txtInvNumber);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.dtBegin);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtEnd);
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.numericUpDown2);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new Point(4, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(304, 152);
			this.groupBox1.TabIndex = 43;
			this.groupBox1.TabStop = false;
			this.label6.Location = new Point(4, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 68;
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
			this.cmbAgent.Location = new Point(48, 38);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(252, 19);
			this.cmbAgent.TabIndex = 67;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(4, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 65;
			this.label4.Text = "РУ";
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(100, 62);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(180, 20);
			this.lblNameGRU.TabIndex = 66;
			this.cmdGRU.ForeColor = SystemColors.ControlText;
			this.cmdGRU.ImageIndex = 0;
			this.cmdGRU.ImageList = this.imageList1;
			this.cmdGRU.Location = new Point(280, 62);
			this.cmdGRU.Name = "cmdGRU";
			this.cmdGRU.Size = new System.Drawing.Size(20, 20);
			this.cmdGRU.TabIndex = 64;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.txtInvNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtInvNumber.Location = new Point(48, 62);
			this.txtInvNumber.Name = "txtInvNumber";
			this.txtInvNumber.Size = new System.Drawing.Size(48, 20);
			this.txtInvNumber.TabIndex = 63;
			this.txtInvNumber.Text = "";
			this.txtInvNumber.Leave += new EventHandler(this.txtInvNumber_Leave);
			this.txtInvNumber.KeyUp += new KeyEventHandler(this.txtInvNumber_KeyUp);
			this.txtInvNumber.Enter += new EventHandler(this.txtInvNumber_Enter);
			this.numericUpDown2.DecimalPlaces = 2;
			this.numericUpDown2.Location = new Point(92, 88);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(64, 20);
			this.numericUpDown2.TabIndex = 42;
			NumericUpDown num1 = this.numericUpDown2;
			numArray = new int[] { 5, 0, 0, 0 };
			num1.Value = new decimal(numArray);
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(168, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 16);
			this.label5.TabIndex = 39;
			this.label5.Text = "по";
			this.cmbTypePU.AddItemSeparator = ';';
			this.cmbTypePU.BorderStyle = 1;
			this.cmbTypePU.Caption = "";
			this.cmbTypePU.CaptionHeight = 17;
			this.cmbTypePU.CharacterCasing = 0;
			this.cmbTypePU.ColumnCaptionHeight = 17;
			this.cmbTypePU.ColumnFooterHeight = 17;
			this.cmbTypePU.ColumnHeaders = false;
			this.cmbTypePU.ColumnWidth = 100;
			this.cmbTypePU.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypePU.ContentHeight = 15;
			this.cmbTypePU.DataMode = DataModeEnum.AddItem;
			this.cmbTypePU.DeadAreaBackColor = Color.Empty;
			this.cmbTypePU.EditorBackColor = SystemColors.Window;
			this.cmbTypePU.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypePU.EditorForeColor = SystemColors.WindowText;
			this.cmbTypePU.EditorHeight = 15;
			this.cmbTypePU.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypePU.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbTypePU.ItemHeight = 15;
			this.cmbTypePU.Location = new Point(48, 116);
			this.cmbTypePU.MatchEntryTimeout = (long)2000;
			this.cmbTypePU.MaxDropDownItems = 5;
			this.cmbTypePU.MaxLength = 32767;
			this.cmbTypePU.MouseCursor = Cursors.Default;
			this.cmbTypePU.Name = "cmbTypePU";
			this.cmbTypePU.RowDivider.Color = Color.DarkGray;
			this.cmbTypePU.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypePU.RowSubDividerColor = Color.DarkGray;
			this.cmbTypePU.Size = new System.Drawing.Size(252, 19);
			this.cmbTypePU.TabIndex = 69;
			this.cmbTypePU.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label7.Location = new Point(4, 117);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 70;
			this.label7.Text = "Тип ПУ:";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(310, 196);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(316, 220);
			base.MinimumSize = new System.Drawing.Size(316, 220);
			base.Name = "frmPrintBigConsumption";
			this.Text = "Большое потребление";
			base.Closing += new CancelEventHandler(this.frmPrintBigConsumption_Closing);
			base.Load += new EventHandler(this.frmPrintBigConsumption_Load);
			((ISupportInitialize)this.dtBegin).EndInit();
			((ISupportInitialize)this.dtEnd).EndInit();
			((ISupportInitialize)this.numericUpDown1).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.numericUpDown2).EndInit();
			((ISupportInitialize)this.cmbTypePU).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDGRU, long Agent, long TypePU)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				int[] numArray = new int[] { 3, 3, 1, 1, 2, 2, 2 };
				string[] str = new string[] { "@DateB", "@DateE", "@factuseS", "@factuse", "@idagent", "@idgru", "@idtypepu" };
				string[] strArrays = str;
				str = new string[] { this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString(), null, null, null, null, null };
				decimal value = this.numericUpDown2.Value;
				str[2] = value.ToString();
				value = this.numericUpDown1.Value;
				str[3] = value.ToString();
				str[4] = Agent.ToString();
				str[5] = IDGRU.ToString();
				str[6] = TypePU.ToString();
				string[] strArrays1 = str;
				string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repBigConsumption.rpt");
				Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
				{
					Text = string.Concat("Большое потребление с ", this.dtBegin.Text.ToString(), " по ", this.dtEnd.Text.ToString()),
					MdiParent = base.MdiParent
				};
				frmReport.Show();
				frmReport = null;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
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