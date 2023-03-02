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
	public class frmSettings : Form
	{
		private Button cmdOk;

		private Button cmdCancel;

		private ToolTip toolTip1;

		private IContainer components;

		private ImageList imageList1;

		private TabPage tabPage1;

		private Button cmdView;

		private TextBox txtReportPath;

		private Label label4;

		private Button cmdPassword;

		private TabControl tabControl1;

		private Label label5;

		private C1Combo cmbAgent;

		private CheckBox chStartup;

		private Agents _agents;

		public frmSettings()
		{
			this.InitializeComponent();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					Depot.oSettings.ReportPath = this.txtReportPath.Text;
					Depot.oSettings.Startup = this.chStartup.Checked;
					if (this.cmbAgent.SelectedIndex >= 0)
					{
						Depot.oSettings.oAgent = this._agents[this.cmbAgent.SelectedIndex];
					}
					Depot.oSettings.Save();
					base.Close();
				}
				catch
				{
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void cmdPassword_Click(object sender, EventArgs e)
		{
			(new frmLoginPassword()).ShowDialog();
		}

		private void cmdView_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
			{
				Description = "Выбор пути к отчетам",
				SelectedPath = this.txtReportPath.Text
			};
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.txtReportPath.Text = string.Concat(folderBrowserDialog.SelectedPath, "\\");
			}
			base.Invalidate();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmSettings_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmSettings_Load(object sender, EventArgs e)
		{
			if (Depot.oSettings == null)
			{
				base.Close();
				return;
			}
			Tools.LoadWindows(this);
			frmSettings frmSetting = this;
			string text = frmSetting.Text;
			string[] name = new string[] { text, " для ", Depot.oSettings.oUser.get_Name(), " (", null, null };
			name[4] = (Depot.oSettings.oAgent == null ? "н/у" : Depot.oSettings.oAgent.get_Name());
			name[5] = ")";
			frmSetting.Text = string.Concat(name);
			this.txtReportPath.Text = Depot.oSettings.ReportPath;
			this.chStartup.Checked = Depot.oSettings.Startup;
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
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmSettings));
			this.imageList1 = new ImageList(this.components);
			this.cmdOk = new Button();
			this.cmdCancel = new Button();
			this.toolTip1 = new ToolTip(this.components);
			this.cmdView = new Button();
			this.tabPage1 = new TabPage();
			this.chStartup = new CheckBox();
			this.label5 = new Label();
			this.cmbAgent = new C1Combo();
			this.txtReportPath = new TextBox();
			this.label4 = new Label();
			this.cmdPassword = new Button();
			this.tabControl1 = new TabControl();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			this.tabControl1.SuspendLayout();
			base.SuspendLayout();
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOk.FlatStyle = FlatStyle.Flat;
			this.cmdOk.Location = new Point(252, 140);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(88, 24);
			this.cmdOk.TabIndex = 3;
			this.cmdOk.Text = "Сохранить";
			this.cmdOk.Click += new EventHandler(this.cmdOk_Click);
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(348, 140);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdView.FlatStyle = FlatStyle.Flat;
			this.cmdView.Location = new Point(400, 8);
			this.cmdView.Name = "cmdView";
			this.cmdView.Size = new System.Drawing.Size(24, 20);
			this.cmdView.TabIndex = 4;
			this.cmdView.Text = "...";
			this.toolTip1.SetToolTip(this.cmdView, "Обзор папок");
			this.cmdView.Click += new EventHandler(this.cmdView_Click);
			this.tabPage1.Controls.Add(this.chStartup);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.cmbAgent);
			this.tabPage1.Controls.Add(this.cmdView);
			this.tabPage1.Controls.Add(this.txtReportPath);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.cmdPassword);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(432, 110);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Общие";
			this.chStartup.FlatStyle = FlatStyle.Flat;
			this.chStartup.Location = new Point(8, 56);
			this.chStartup.Name = "chStartup";
			this.chStartup.Size = new System.Drawing.Size(352, 24);
			this.chStartup.TabIndex = 16;
			this.chStartup.Text = "Открывать стартовый навигатор при запуске программы";
			this.chStartup.Visible = false;
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 16);
			this.label5.TabIndex = 15;
			this.label5.Text = "Агент";
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
			this.cmbAgent.Location = new Point(104, 32);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(288, 19);
			this.cmbAgent.TabIndex = 14;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.txtReportPath.BorderStyle = BorderStyle.FixedSingle;
			this.txtReportPath.Location = new Point(104, 8);
			this.txtReportPath.Name = "txtReportPath";
			this.txtReportPath.Size = new System.Drawing.Size(288, 20);
			this.txtReportPath.TabIndex = 3;
			this.txtReportPath.Text = "";
			this.label4.Location = new Point(8, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Путь к отчетам";
			this.cmdPassword.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdPassword.FlatStyle = FlatStyle.Flat;
			this.cmdPassword.ImageAlign = ContentAlignment.MiddleRight;
			this.cmdPassword.ImageIndex = 13;
			this.cmdPassword.ImageList = this.imageList1;
			this.cmdPassword.Location = new Point(8, 80);
			this.cmdPassword.Name = "cmdPassword";
			this.cmdPassword.Size = new System.Drawing.Size(104, 24);
			this.cmdPassword.TabIndex = 3;
			this.cmdPassword.Text = "Смена пароля";
			this.cmdPassword.TextAlign = ContentAlignment.MiddleLeft;
			this.cmdPassword.Click += new EventHandler(this.cmdPassword_Click);
			this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tabControl1.Appearance = TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.ItemSize = new System.Drawing.Size(47, 18);
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(440, 136);
			this.tabControl1.TabIndex = 1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(440, 167);
			base.Controls.Add(this.cmdOk);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.Name = "frmSettings";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Настройки";
			base.Closing += new CancelEventHandler(this.frmSettings_Closing);
			base.Load += new EventHandler(this.frmSettings_Load);
			this.tabPage1.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgent).EndInit();
			this.tabControl1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}