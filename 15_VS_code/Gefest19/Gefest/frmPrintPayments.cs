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
	public class frmPrintPayments : Form
	{
		private Button cmdClose;

		private C1DateEdit dtPeriod;

		private Button cmdOK;

		private Label label1;

		private Label label2;

		private C1Combo cmbTypePay;

		private C1Combo cmbTypeAgent;

		private Label label3;

		private System.ComponentModel.Container components = null;

		public frmPrintPayments()
		{
			this.InitializeComponent();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					long d = (long)0;
					long num = (long)0;
					long num1 = (long)0;
					if (this.cmbTypePay.SelectedIndex > 0)
					{
						d = Depot.oTypePays[this.cmbTypePay.SelectedIndex - 1].get_ID();
					}
					if (this.cmbTypeAgent.SelectedIndex > 0)
					{
						num = Depot.oTypeAgents[this.cmbTypeAgent.SelectedIndex - 1].get_ID();
					}
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2, 2, 2, 2, 1 };
					string[] str = new string[] { "@Month", "@Year", "@IdTypeAgent", "@IdtypePay", "@IdAgent", "MonthName" };
					string[] strArrays = str;
					str = new string[6];
					DateTime value = (DateTime)this.dtPeriod.Value;
					int month = value.Month;
					str[0] = month.ToString();
					value = (DateTime)this.dtPeriod.Value;
					month = value.Year;
					str[1] = month.ToString();
					str[2] = num.ToString();
					str[3] = d.ToString();
					str[4] = num1.ToString();
					str[5] = this.dtPeriod.Text;
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repPayments.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Поступление денежных средств ", this.dtPeriod.Text),
						MdiParent = Depot._main
					};
					frmReport.Show();
					frmReport = null;
					base.Close();
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPrintPayments_Load(object sender, EventArgs e)
		{
			this.dtPeriod.Value = DateTime.Today;
			Tools.FillC1WhithAll(Depot.oTypePays, this.cmbTypePay, (long)0, "Все");
			Tools.FillC1WhithAll(Depot.oTypeAgents, this.cmbTypeAgent, (long)0, "Все");
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintPayments));
			this.cmdClose = new Button();
			this.dtPeriod = new C1DateEdit();
			this.cmdOK = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			this.cmbTypePay = new C1Combo();
			this.cmbTypeAgent = new C1Combo();
			this.label3 = new Label();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			((ISupportInitialize)this.cmbTypePay).BeginInit();
			((ISupportInitialize)this.cmbTypeAgent).BeginInit();
			base.SuspendLayout();
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(224, 80);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 33;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMM yyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(104, 8);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(208, 18);
			this.dtPeriod.TabIndex = 31;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(120, 80);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 32;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 34;
			this.label1.Text = "Период:";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 35;
			this.label2.Text = "Вид оплаты:";
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
			this.cmbTypePay.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbTypePay.ItemHeight = 15;
			this.cmbTypePay.Location = new Point(104, 32);
			this.cmbTypePay.MatchEntryTimeout = (long)2000;
			this.cmbTypePay.MaxDropDownItems = 5;
			this.cmbTypePay.MaxLength = 32767;
			this.cmbTypePay.MouseCursor = Cursors.Default;
			this.cmbTypePay.Name = "cmbTypePay";
			this.cmbTypePay.RowDivider.Color = Color.DarkGray;
			this.cmbTypePay.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypePay.RowSubDividerColor = Color.DarkGray;
			this.cmbTypePay.Size = new System.Drawing.Size(208, 19);
			this.cmbTypePay.TabIndex = 36;
			this.cmbTypePay.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.cmbTypeAgent.AddItemSeparator = ';';
			this.cmbTypeAgent.BorderStyle = 1;
			this.cmbTypeAgent.Caption = "";
			this.cmbTypeAgent.CaptionHeight = 17;
			this.cmbTypeAgent.CharacterCasing = 0;
			this.cmbTypeAgent.ColumnCaptionHeight = 17;
			this.cmbTypeAgent.ColumnFooterHeight = 17;
			this.cmbTypeAgent.ColumnHeaders = false;
			this.cmbTypeAgent.ColumnWidth = 100;
			this.cmbTypeAgent.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeAgent.ContentHeight = 15;
			this.cmbTypeAgent.DataMode = DataModeEnum.AddItem;
			this.cmbTypeAgent.DeadAreaBackColor = Color.Empty;
			this.cmbTypeAgent.EditorBackColor = SystemColors.Window;
			this.cmbTypeAgent.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeAgent.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeAgent.EditorHeight = 15;
			this.cmbTypeAgent.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeAgent.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbTypeAgent.ItemHeight = 15;
			this.cmbTypeAgent.Location = new Point(104, 56);
			this.cmbTypeAgent.MatchEntryTimeout = (long)2000;
			this.cmbTypeAgent.MaxDropDownItems = 5;
			this.cmbTypeAgent.MaxLength = 32767;
			this.cmbTypeAgent.MouseCursor = Cursors.Default;
			this.cmbTypeAgent.Name = "cmbTypeAgent";
			this.cmbTypeAgent.RowDivider.Color = Color.DarkGray;
			this.cmbTypeAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeAgent.Size = new System.Drawing.Size(208, 19);
			this.cmbTypeAgent.TabIndex = 38;
			this.cmbTypeAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 16);
			this.label3.TabIndex = 37;
			this.label3.Text = "Тип агента:";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(320, 109);
			base.Controls.Add(this.cmbTypeAgent);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.cmbTypePay);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.dtPeriod);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintPayments";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Поступление денежных средств";
			base.Load += new EventHandler(this.frmPrintPayments_Load);
			((ISupportInitialize)this.dtPeriod).EndInit();
			((ISupportInitialize)this.cmbTypePay).EndInit();
			((ISupportInitialize)this.cmbTypeAgent).EndInit();
			base.ResumeLayout(false);
		}
	}
}