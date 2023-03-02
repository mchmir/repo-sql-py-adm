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
	public class frmFilterLegalDocs : Form
	{
		private Button cmdCancel;

		private Button cmdOK;

		private System.ComponentModel.Container components = null;

		private Label label6;

		private C1Combo cmbStatus;

		public frmFilterLegalDocs()
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

		private void frmFilterLegalDocs_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmFilterLegalDocs_Load(object sender, EventArgs e)
		{
			Tools.LoadWindows(this);
			this.cmbStatus.AddItem("Любое состояние");
			this.cmbStatus.AddItem("Начало работы");
			this.cmbStatus.AddItem("Передача дела в суд");
			this.cmbStatus.AddItem("Решение суда");
			this.cmbStatus.ColumnWidth = this.cmbStatus.Width - this.cmbStatus.Height;
		}

		public void GetData(ref int idstatus, ref string filter)
		{
			if (this.cmbStatus.SelectedIndex != 0)
			{
				filter = string.Concat("состояние ", this.cmbStatus.Text);
			}
			else
			{
				filter = "любое состояние";
			}
			idstatus = this.cmbStatus.SelectedIndex;
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmFilterLegalDocs));
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.label6 = new Label();
			this.cmbStatus = new C1Combo();
			((ISupportInitialize)this.cmbStatus).BeginInit();
			base.SuspendLayout();
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(216, 48);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(120, 48);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 24);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label6.Location = new Point(3, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(93, 16);
			this.label6.TabIndex = 58;
			this.label6.Text = "Состояние:";
			this.cmbStatus.AddItemSeparator = ';';
			this.cmbStatus.BorderStyle = 1;
			this.cmbStatus.Caption = "";
			this.cmbStatus.CaptionHeight = 17;
			this.cmbStatus.CharacterCasing = 0;
			this.cmbStatus.ColumnCaptionHeight = 17;
			this.cmbStatus.ColumnFooterHeight = 17;
			this.cmbStatus.ColumnHeaders = false;
			this.cmbStatus.ColumnWidth = 100;
			this.cmbStatus.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbStatus.ContentHeight = 15;
			this.cmbStatus.DataMode = DataModeEnum.AddItem;
			this.cmbStatus.DeadAreaBackColor = Color.Empty;
			this.cmbStatus.EditorBackColor = SystemColors.Window;
			this.cmbStatus.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbStatus.EditorForeColor = SystemColors.WindowText;
			this.cmbStatus.EditorHeight = 15;
			this.cmbStatus.FlatStyle = FlatModeEnum.Flat;
			this.cmbStatus.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbStatus.ItemHeight = 15;
			this.cmbStatus.Location = new Point(96, 8);
			this.cmbStatus.MatchEntryTimeout = (long)2000;
			this.cmbStatus.MaxDropDownItems = 5;
			this.cmbStatus.MaxLength = 32767;
			this.cmbStatus.MouseCursor = Cursors.Default;
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.RowDivider.Color = Color.DarkGray;
			this.cmbStatus.RowDivider.Style = LineStyleEnum.None;
			this.cmbStatus.RowSubDividerColor = Color.DarkGray;
			this.cmbStatus.Size = new System.Drawing.Size(204, 19);
			this.cmbStatus.TabIndex = 1;
			this.cmbStatus.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(306, 74);
			base.Controls.Add(this.cmbStatus);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.MinimumSize = new System.Drawing.Size(312, 96);
			base.Name = "frmFilterLegalDocs";
			this.Text = "Фильтр";
			base.Closing += new CancelEventHandler(this.frmFilterLegalDocs_Closing);
			base.Load += new EventHandler(this.frmFilterLegalDocs_Load);
			((ISupportInitialize)this.cmbStatus).EndInit();
			base.ResumeLayout(false);
		}
	}
}