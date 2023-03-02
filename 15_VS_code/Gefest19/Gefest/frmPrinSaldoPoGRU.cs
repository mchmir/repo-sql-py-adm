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
	public class frmPrinSaldoPoGRU : Form
	{
		private GroupBox groupBox1;

		private Label label3;

		private C1Combo cmdStatysPU;

		private Label label5;

		private C1Combo cmdStatusGO;

		private C1DateEdit dtPeriod;

		private Label label1;

		private Button cmdClose;

		private Button cmdOK;

		private StatusGObjects _StatusGObjects;

		private StatusGMeters _StatusGMeters;

		private System.ComponentModel.Container components = null;

		public frmPrinSaldoPoGRU()
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
			Periods period = new Periods();
			period.Load((DateTime)this.dtPeriod.Value);
			if (this.cmdStatusGO.Text != "По всем")
			{
				d = (long)((int)this._StatusGObjects[this.cmdStatusGO.SelectedIndex - 1].get_ID());
			}
			if (this.cmdStatysPU.Text != "По всем")
			{
				num = (long)((int)this._StatusGMeters[this.cmdStatysPU.SelectedIndex - 1].get_ID());
			}
			if (period.get_Count() <= 0)
			{
				MessageBox.Show("Не верный выбор периода!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			this.Rep(period[0].get_ID(), d, num);
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

		private void frmPrinSaldoPoGRU_Load(object sender, EventArgs e)
		{
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrinSaldoPoGRU));
			this.groupBox1 = new GroupBox();
			this.label3 = new Label();
			this.cmdStatysPU = new C1Combo();
			this.label5 = new Label();
			this.cmdStatusGO = new C1Combo();
			this.dtPeriod = new C1DateEdit();
			this.label1 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmdStatysPU).BeginInit();
			((ISupportInitialize)this.cmdStatusGO).BeginInit();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cmdStatysPU);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.cmdStatusGO);
			this.groupBox1.Controls.Add(this.dtPeriod);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(356, 88);
			this.groupBox1.TabIndex = 35;
			this.groupBox1.TabStop = false;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(4, 40);
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
			this.cmdStatysPU.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmdStatysPU.ItemHeight = 15;
			this.cmdStatysPU.Location = new Point(68, 40);
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
			this.cmdStatysPU.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(4, 16);
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
			this.cmdStatusGO.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmdStatusGO.ItemHeight = 15;
			this.cmdStatusGO.Location = new Point(68, 16);
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
			this.cmdStatusGO.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMMyyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(68, 64);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(112, 18);
			this.dtPeriod.TabIndex = 3;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 30;
			this.label1.Text = "Период";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(264, 96);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 37;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(160, 96);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 36;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(360, 124);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrinSaldoPoGRU";
			this.Text = "Отчет сальдо по ГРУ";
			base.Load += new EventHandler(this.frmPrinSaldoPoGRU_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmdStatysPU).EndInit();
			((ISupportInitialize)this.cmdStatusGO).EndInit();
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDPeriod, long StatysGO, long StatysPU)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2, 2, 1 };
					string[] str = new string[] { "@idperiod", "@idstatusgobject", "@idstatusgmeter", "Month" };
					string[] strArrays = str;
					str = new string[] { IDPeriod.ToString(), StatysGO.ToString(), StatysPU.ToString(), this.dtPeriod.Text.ToString() };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSaldoPoGru.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "Cальдо по ГРУ ",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
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
	}
}