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
	public class frmRecalcCashBalance : Form
	{
		private C1DateEdit dtDate;

		private Label label1;

		private Label label5;

		private C1Combo cmbAgent;

		private Button cmdApply;

		private Button cmdCancel;

		private System.ComponentModel.Container components = null;

		private Agents _agents;

		public frmRecalcCashBalance()
		{
			this.InitializeComponent();
		}

		private void cmdApply_Click(object sender, EventArgs e)
		{
			Depot.spRecalcCash(this._agents[this.cmbAgent.SelectedIndex].get_ID(), (DateTime)this.dtDate.Value);
			MessageBox.Show("Пересчет закончен!", "Пересчет сальдо", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
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

		private void frmRecalcCashBalance_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmRecalcCashBalance_Load(object sender, EventArgs e)
		{
			Tools.LoadWindows(this);
			this.dtDate.Value = DateTime.Today.Date;
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)2));
			if (Depot.oSettings.oAgent == null)
			{
				Tools.FillC1(this._agents, this.cmbAgent, (long)0);
				return;
			}
			Tools.FillC1(this._agents, this.cmbAgent, Depot.oSettings.oAgent.get_ID());
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmRecalcCashBalance));
			this.dtDate = new C1DateEdit();
			this.label1 = new Label();
			this.cmdApply = new Button();
			this.label5 = new Label();
			this.cmbAgent = new C1Combo();
			this.cmdCancel = new Button();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			base.SuspendLayout();
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(136, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 59;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 61;
			this.label1.Text = "Дата";
			this.cmdApply.FlatStyle = FlatStyle.Flat;
			this.cmdApply.ForeColor = SystemColors.ControlText;
			this.cmdApply.Location = new Point(40, 80);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(120, 24);
			this.cmdApply.TabIndex = 60;
			this.cmdApply.Text = "Пересчет";
			this.cmdApply.Click += new EventHandler(this.cmdApply_Click);
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 63;
			this.label5.Text = "Кассир";
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
			this.cmbAgent.Location = new Point(72, 32);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(216, 19);
			this.cmbAgent.TabIndex = 62;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.ForeColor = SystemColors.ControlText;
			this.cmdCancel.Location = new Point(168, 80);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(120, 24);
			this.cmdCancel.TabIndex = 64;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(292, 111);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.cmbAgent);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmdApply);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmRecalcCashBalance";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Пересчет сальдо по кассе";
			base.Closing += new CancelEventHandler(this.frmRecalcCashBalance_Closing);
			base.Load += new EventHandler(this.frmRecalcCashBalance_Load);
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.cmbAgent).EndInit();
			base.ResumeLayout(false);
		}
	}
}