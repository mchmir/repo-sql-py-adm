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
	public class frmPrintGarbTask : Form
	{
		private GroupBox groupBox1;

		private Label label4;

		private Label lblNameGRU;

		private Button cmdGRU;

		private TextBox txtInvNumber;

		private Label label1;

		private ImageList imageList1;

		private ComboBox cmbDom;

		private Label label2;

		private CheckBox chkPoc;

		private Button cmdClose;

		private Button cmdOK;

		private IContainer components;

		private C1DateEdit dtPeriod;

		private Label label6;

		private C1Combo cmbAgent;

		private Label label5;

		private C1Combo cmdStatusGO;

		private Label label3;

		private C1Combo cmdStatysPU;

		private GRU _gru;

		private Agents _agents;

		private StatusGObjects _StatusGObjects;

		private TextBox txtCLS;

		private Label label7;

		private TextBox txtCountInd;

		private Label label8;

		private TextBox txtCountIndicmax;

		private Label label9;

		private CheckBox chkTel;

		private Label label10;

		private StatusGMeters _StatusGMeters;

		private C1Combo CmbPrichina;

		private ComboBox comboBox1;

		private Label label11;

		private CheckBox checkBox1;

		private CheckBox chkPoc6M;

		private NumericUpDown numNoIndMonth;

		private TypeReasonDisconnects _TypeReasonDisconnects;

		public frmPrintGarbTask()
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
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = -1;
			int num6 = 0;
			int num7 = 0;
			int d2 = 0;
			int num8 = 0;
			Periods period = new Periods();
			period.Load((DateTime)this.dtPeriod.Value);
			if (this.cmbAgent.Text != "Все агенты")
			{
				num = (long)((int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID());
			}
			if (this._gru != null)
			{
				d = this._gru.get_ID();
			}
			if (this.cmdStatusGO.Text != "По всем")
			{
				d1 = (long)((int)this._StatusGObjects[this.cmdStatusGO.SelectedIndex - 1].get_ID());
			}
			if (this.CmbPrichina.Enabled && this.CmbPrichina.Text != "По всем")
			{
				d2 = (int)this._TypeReasonDisconnects[this.CmbPrichina.SelectedIndex - 1].get_ID();
			}
			if (this.cmdStatysPU.Text != "По всем")
			{
				num1 = (long)((int)this._StatusGMeters[this.cmdStatysPU.SelectedIndex - 1].get_ID());
			}
			if (this.chkPoc.Checked)
			{
				num2 = 1;
			}
			if (this.chkPoc6M.Checked)
			{
				num3 = Convert.ToInt32(this.numNoIndMonth.Value);
			}
			if (this.chkTel.Checked)
			{
				num4 = 0;
			}
			if (this.checkBox1.Checked)
			{
				num8 = 1;
			}
			if (this.txtCLS.Text != "Все")
			{
				num5 = Convert.ToInt32(this.txtCLS.Text);
			}
			if (this.txtCountInd.Text != "Все")
			{
				num6 = Convert.ToInt32(this.txtCountInd.Text);
			}
			if (this.txtCountIndicmax.Text != "Все")
			{
				num7 = Convert.ToInt32(this.txtCountIndicmax.Text);
			}
			if (period.get_Count() <= 0)
			{
				MessageBox.Show("Не верный выбор периода!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			this.Rep(d, period[0].get_ID(), num, d1, num1, (long)this.cmbDom.SelectedIndex, (long)num2, (long)num3, (long)num5, (long)num6, (long)num7, (long)num4, (long)d2, (long)this.comboBox1.SelectedIndex, (long)num8);
		}

		private void cmdStatysPU_TextChanged(object sender, EventArgs e)
		{
			if (this.cmdStatysPU.SelectedIndex != 1)
			{
				this.CmbPrichina.Enabled = false;
				return;
			}
			this.CmbPrichina.Enabled = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPrintGarbTask_Load(object sender, EventArgs e)
		{
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)1));
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
			this._StatusGObjects = new StatusGObjects();
			this._StatusGObjects.Load();
			Tools.FillC1WhithAll(this._StatusGObjects, this.cmdStatusGO, (long)0, "По всем");
			this.cmdStatusGO.SelectedIndex = 2;
			this._StatusGMeters = new StatusGMeters();
			this._StatusGMeters.Load();
			Tools.FillC1WhithAll(this._StatusGMeters, this.cmdStatysPU, (long)0, "По всем");
			this.cmdStatysPU.SelectedIndex = 2;
			this._TypeReasonDisconnects = new TypeReasonDisconnects();
			this._TypeReasonDisconnects.Load();
			Tools.FillC1WhithAll(this._TypeReasonDisconnects, this.CmbPrichina, (long)0, "По всем");
			this.txtInvNumber.Focus();
			this.dtPeriod.Value = DateTime.Today.Date;
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintGarbTask));
			this.groupBox1 = new GroupBox();
			this.numNoIndMonth = new NumericUpDown();
			this.chkPoc6M = new CheckBox();
			this.comboBox1 = new ComboBox();
			this.label11 = new Label();
			this.label10 = new Label();
			this.CmbPrichina = new C1Combo();
			this.chkTel = new CheckBox();
			this.txtCountIndicmax = new TextBox();
			this.label9 = new Label();
			this.txtCountInd = new TextBox();
			this.label8 = new Label();
			this.txtCLS = new TextBox();
			this.label7 = new Label();
			this.label3 = new Label();
			this.cmdStatysPU = new C1Combo();
			this.label5 = new Label();
			this.cmdStatusGO = new C1Combo();
			this.label6 = new Label();
			this.cmbAgent = new C1Combo();
			this.dtPeriod = new C1DateEdit();
			this.cmbDom = new ComboBox();
			this.label2 = new Label();
			this.label4 = new Label();
			this.lblNameGRU = new Label();
			this.cmdGRU = new Button();
			this.imageList1 = new ImageList(this.components);
			this.txtInvNumber = new TextBox();
			this.label1 = new Label();
			this.chkPoc = new CheckBox();
			this.checkBox1 = new CheckBox();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.numNoIndMonth).BeginInit();
			((ISupportInitialize)this.CmbPrichina).BeginInit();
			((ISupportInitialize)this.cmdStatysPU).BeginInit();
			((ISupportInitialize)this.cmdStatusGO).BeginInit();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.numNoIndMonth);
			this.groupBox1.Controls.Add(this.chkPoc6M);
			this.groupBox1.Controls.Add(this.comboBox1);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.CmbPrichina);
			this.groupBox1.Controls.Add(this.chkTel);
			this.groupBox1.Controls.Add(this.txtCountIndicmax);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.txtCountInd);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.txtCLS);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cmdStatysPU);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.cmdStatusGO);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.cmbAgent);
			this.groupBox1.Controls.Add(this.dtPeriod);
			this.groupBox1.Controls.Add(this.cmbDom);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lblNameGRU);
			this.groupBox1.Controls.Add(this.cmdGRU);
			this.groupBox1.Controls.Add(this.txtInvNumber);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.chkPoc);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Location = new Point(4, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(356, 252);
			this.groupBox1.TabIndex = 34;
			this.groupBox1.TabStop = false;
			this.numNoIndMonth.Location = new Point(316, 232);
			this.numNoIndMonth.Name = "numNoIndMonth";
			this.numNoIndMonth.Size = new System.Drawing.Size(36, 20);
			this.numNoIndMonth.TabIndex = 80;
			this.chkPoc6M.Location = new Point(180, 232);
			this.chkPoc6M.Name = "chkPoc6M";
			this.chkPoc6M.Size = new System.Drawing.Size(144, 20);
			this.chkPoc6M.TabIndex = 79;
			this.chkPoc6M.Text = "Без показ. более, мес. ";
			ComboBox.ObjectCollection items = this.comboBox1.Items;
			object[] objArray = new object[] { "-нет-", "тек.мес.", "1", "2", "3", "4", "5", "6" };
			items.AddRange(objArray);
			this.comboBox1.Location = new Point(280, 208);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(72, 21);
			this.comboBox1.TabIndex = 77;
			this.comboBox1.Text = "-нет-";
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.label11.Location = new Point(184, 212);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(64, 16);
			this.label11.TabIndex = 78;
			this.label11.Text = "Акт пер.";
			this.label10.ForeColor = SystemColors.ControlText;
			this.label10.Location = new Point(4, 116);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(104, 16);
			this.label10.TabIndex = 76;
			this.label10.Text = "Причина откл. ПУ:";
			this.CmbPrichina.AddItemSeparator = ';';
			this.CmbPrichina.BorderStyle = 1;
			this.CmbPrichina.Caption = "";
			this.CmbPrichina.CaptionHeight = 17;
			this.CmbPrichina.CharacterCasing = 0;
			this.CmbPrichina.ColumnCaptionHeight = 17;
			this.CmbPrichina.ColumnFooterHeight = 17;
			this.CmbPrichina.ColumnHeaders = false;
			this.CmbPrichina.ColumnWidth = 100;
			this.CmbPrichina.ComboStyle = ComboStyleEnum.DropdownList;
			this.CmbPrichina.ContentHeight = 15;
			this.CmbPrichina.DataMode = DataModeEnum.AddItem;
			this.CmbPrichina.DeadAreaBackColor = Color.Empty;
			this.CmbPrichina.EditorBackColor = SystemColors.Window;
			this.CmbPrichina.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.CmbPrichina.EditorForeColor = SystemColors.WindowText;
			this.CmbPrichina.EditorHeight = 15;
			this.CmbPrichina.FlatStyle = FlatModeEnum.Flat;
			this.CmbPrichina.Images.Add((Image)resourceManager.GetObject("resource"));
			this.CmbPrichina.ItemHeight = 15;
			this.CmbPrichina.Location = new Point(108, 112);
			this.CmbPrichina.MatchEntryTimeout = (long)2000;
			this.CmbPrichina.MaxDropDownItems = 5;
			this.CmbPrichina.MaxLength = 32767;
			this.CmbPrichina.MouseCursor = Cursors.Default;
			this.CmbPrichina.Name = "CmbPrichina";
			this.CmbPrichina.RowDivider.Color = Color.DarkGray;
			this.CmbPrichina.RowDivider.Style = LineStyleEnum.None;
			this.CmbPrichina.RowSubDividerColor = Color.DarkGray;
			this.CmbPrichina.Size = new System.Drawing.Size(244, 19);
			this.CmbPrichina.TabIndex = 75;
			this.CmbPrichina.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.chkTel.Location = new Point(4, 208);
			this.chkTel.Name = "chkTel";
			this.chkTel.Size = new System.Drawing.Size(168, 20);
			this.chkTel.TabIndex = 74;
			this.chkTel.Text = "По акту";
			this.txtCountIndicmax.BorderStyle = BorderStyle.FixedSingle;
			this.txtCountIndicmax.Location = new Point(280, 184);
			this.txtCountIndicmax.Name = "txtCountIndicmax";
			this.txtCountIndicmax.Size = new System.Drawing.Size(72, 20);
			this.txtCountIndicmax.TabIndex = 72;
			this.txtCountIndicmax.Text = "Все";
			this.label9.ForeColor = SystemColors.ControlText;
			this.label9.Location = new Point(184, 188);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(88, 16);
			this.label9.TabIndex = 73;
			this.label9.Text = "Кол. повт. мен.:";
			this.txtCountInd.BorderStyle = BorderStyle.FixedSingle;
			this.txtCountInd.Location = new Point(280, 160);
			this.txtCountInd.Name = "txtCountInd";
			this.txtCountInd.Size = new System.Drawing.Size(72, 20);
			this.txtCountInd.TabIndex = 70;
			this.txtCountInd.Text = "Все";
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(184, 164);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88, 16);
			this.label8.TabIndex = 71;
			this.label8.Text = "Кол. повт. пок.:";
			this.txtCLS.BorderStyle = BorderStyle.FixedSingle;
			this.txtCLS.Location = new Point(120, 156);
			this.txtCLS.Name = "txtCLS";
			this.txtCLS.Size = new System.Drawing.Size(60, 20);
			this.txtCLS.TabIndex = 68;
			this.txtCLS.Text = "Все";
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(4, 160);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(112, 16);
			this.label7.TabIndex = 69;
			this.label7.Text = "Кол. проживающих:";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(4, 92);
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
			this.cmdStatysPU.Location = new Point(68, 88);
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
			this.cmdStatysPU.TextChanged += new EventHandler(this.cmdStatysPU_TextChanged);
			this.cmdStatysPU.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(4, 68);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 65;
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
			this.cmdStatusGO.Location = new Point(68, 64);
			this.cmdStatusGO.MatchEntryTimeout = (long)2000;
			this.cmdStatusGO.MaxDropDownItems = 5;
			this.cmdStatusGO.MaxLength = 32767;
			this.cmdStatusGO.MouseCursor = Cursors.Default;
			this.cmdStatusGO.Name = "cmdStatusGO";
			this.cmdStatusGO.RowDivider.Color = Color.DarkGray;
			this.cmdStatusGO.RowDivider.Style = LineStyleEnum.None;
			this.cmdStatusGO.RowSubDividerColor = Color.DarkGray;
			this.cmdStatusGO.Size = new System.Drawing.Size(284, 19);
			this.cmdStatusGO.TabIndex = 64;
			this.cmdStatusGO.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
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
			this.cmbAgent.Images.Add((Image)resourceManager.GetObject("resource3"));
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
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMM yyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(68, 136);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(112, 18);
			this.dtPeriod.TabIndex = 3;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			ComboBox.ObjectCollection objectCollections = this.cmbDom.Items;
			objArray = new object[] { "По всем", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
			objectCollections.AddRange(objArray);
			this.cmbDom.Location = new Point(228, 136);
			this.cmbDom.Name = "cmbDom";
			this.cmbDom.Size = new System.Drawing.Size(124, 21);
			this.cmbDom.TabIndex = 4;
			this.cmbDom.Text = "По всем ";
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.label2.Location = new Point(184, 140);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 45;
			this.label2.Text = "Дом:";
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
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 136);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 30;
			this.label1.Text = "Период";
			this.chkPoc.Checked = true;
			this.chkPoc.CheckState = CheckState.Checked;
			this.chkPoc.Location = new Point(4, 184);
			this.chkPoc.Name = "chkPoc";
			this.chkPoc.Size = new System.Drawing.Size(168, 20);
			this.chkPoc.TabIndex = 5;
			this.chkPoc.Text = "Без показ. в тек. периоде";
			this.checkBox1.Location = new Point(4, 232);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(168, 20);
			this.checkBox1.TabIndex = 74;
			this.checkBox1.Text = "Нарушения";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(268, 260);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 8;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(164, 260);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(362, 288);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintGarbTask";
			this.Text = "Наряд-задание";
			base.Load += new EventHandler(this.frmPrintGarbTask_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.numNoIndMonth).EndInit();
			((ISupportInitialize)this.CmbPrichina).EndInit();
			((ISupportInitialize)this.cmdStatysPU).EndInit();
			((ISupportInitialize)this.cmdStatusGO).EndInit();
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDGRU, long IDPeriod, long Agent, long StatysGO, long StatysPU, long house, long indication, long indNo6Month, long Cls, long Ind, long IndMax, long Tele, long prichina, long PerAkt, long nar)
		{
			string str;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1 };
					string[] strArrays = new string[] { "Month", "@idperiod", "@idgru", "@idagent", "@idhouse", "@Indication", "@IndNo6Month", "@idstatusgobject", "@idstatusgmeter", "@countlives", "@countind", "@countindmax", "@Prichina", "@akt", "@PeriodAkt", "@TypeInfringements" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { this.dtPeriod.Text.ToString(), IDPeriod.ToString(), IDGRU.ToString(), Agent.ToString(), house.ToString(), indication.ToString(), indNo6Month.ToString(), StatysGO.ToString(), StatysPU.ToString(), Cls.ToString(), Ind.ToString(), IndMax.ToString(), prichina.ToString(), Tele.ToString(), PerAkt.ToString(), nar.ToString() };
					string[] strArrays2 = strArrays;
					str = (!this.chkTel.Checked ? string.Concat(Depot.oSettings.ReportPath.Trim(), "repGarbTask1.rpt") : string.Concat(Depot.oSettings.ReportPath.Trim(), "repGarbTask.rpt"));
					Form frmReport = new frmReports(str, strArrays1, strArrays2, numArray)
					{
						Text = "Наряд-задание по РУ№ ",
						MdiParent = base.Owner
					};
					frmReport.Show();
					frmReport = null;
					System.Windows.Forms.Cursor.Current = Cursors.Default;
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					System.Windows.Forms.Cursor.Current = Cursors.Default;
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
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