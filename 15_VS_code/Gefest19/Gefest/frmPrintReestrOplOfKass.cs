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
	public class frmPrintReestrOplOfKass : Form
	{
		private Label label6;

		private C1Combo cmbAgent;

		private GroupBox groupBox1;

		private Button cmdClose;

		private Button cmdOK;

		private Agents _agents;

		private Label label2;

		private C1DateEdit dtBegin;

		private Label label1;

		private C1DateEdit dtEnd;

		private System.ComponentModel.Container components = null;

		public frmPrintReestrOplOfKass()
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
			if (this.cmbAgent.Text != "Все")
			{
				d = (long)((int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID());
			}
			this.Rep(d);
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

		private void frmPrintReestrOplOfKass_Load(object sender, EventArgs e)
		{
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)2));
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все");
			C1DateEdit c1DateEdit = this.dtBegin;
			DateTime today = DateTime.Today;
			DateTime dateTime = DateTime.Today;
			c1DateEdit.Value = today.AddDays((double)(-dateTime.Day + 1));
			C1DateEdit date = this.dtEnd;
			today = DateTime.Today;
			date.Value = today.Date;
			this.dtBegin.Focus();
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintReestrOplOfKass));
			this.label6 = new Label();
			this.cmbAgent = new C1Combo();
			this.groupBox1 = new GroupBox();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.label2 = new Label();
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			this.dtEnd = new C1DateEdit();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dtBegin).BeginInit();
			((ISupportInitialize)this.dtEnd).BeginInit();
			base.SuspendLayout();
			this.label6.Location = new Point(8, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 64;
			this.label6.Text = "Кассир:";
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
			this.cmbAgent.Location = new Point(60, 36);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(160, 19);
			this.cmbAgent.TabIndex = 63;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.dtBegin);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtEnd);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.cmbAgent);
			this.groupBox1.Location = new Point(4, -4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(304, 64);
			this.groupBox1.TabIndex = 65;
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(216, 64);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 67;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(112, 64);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 66;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(152, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 67;
			this.label2.Text = "по";
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.FormatType = FormatTypeEnum.LongDate;
			this.dtBegin.Location = new Point(28, 8);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(120, 18);
			this.dtBegin.TabIndex = 65;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 16);
			this.label1.TabIndex = 68;
			this.label1.Text = "C";
			this.dtEnd.BorderStyle = 1;
			this.dtEnd.FormatType = FormatTypeEnum.LongDate;
			this.dtEnd.Location = new Point(180, 8);
			this.dtEnd.Name = "dtEnd";
			this.dtEnd.Size = new System.Drawing.Size(120, 18);
			this.dtEnd.TabIndex = 66;
			this.dtEnd.Tag = null;
			this.dtEnd.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtEnd.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(310, 90);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintReestrOplOfKass";
			this.Text = "Реестр оплыты в Кассу";
			base.Load += new EventHandler(this.frmPrintReestrOplOfKass_Load);
			((ISupportInitialize)this.cmbAgent).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtBegin).EndInit();
			((ISupportInitialize)this.dtEnd).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long Agent)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 3, 3, 2 };
					string[] str = new string[] { "@Date", "@dateE", "@idagent" };
					string[] strArrays = str;
					str = new string[] { this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString(), Agent.ToString() };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repReestrOplOfKass.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "",
						MdiParent = base.MdiParent
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
	}
}