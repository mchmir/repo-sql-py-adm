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
	public class frmPrintSpravkaPoOtklKV : Form
	{
		private Label label4;

		private Label lblNameGRU;

		private Button cmdGRU;

		private TextBox txtInvNumber;

		private ImageList imageList1;

		private Label label3;

		private Button cmdOK;

		private Label label5;

		private Button cmdClose;

		private ImageList imageList2;

		private IContainer components;

		private ComboBox cmbStatusLS;

		private ComboBox cmbStatusLiz;

		private Label label6;

		private C1Combo cmbAgent;

		private GRU _gru;

		private C1DateEdit dtPeriod;

		private Label label1;

		private RadioButton rbAbo;

		private RadioButton rbVDGO;

		private Label label2;

		private ComboBox cmbIndHouse;

		private Agents _agents;

		public frmPrintSpravkaPoOtklKV()
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
			Periods period = new Periods();
			period.Load((DateTime)this.dtPeriod.Value);
			long d = (long)0;
			long num = (long)0;
			long selectedIndex = (long)0;
			if (this.cmbAgent.Text != "Все агенты")
			{
				num = (long)((int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID());
			}
			if (this.cmbIndHouse.SelectedIndex > 0)
			{
				selectedIndex = (long)this.cmbIndHouse.SelectedIndex;
			}
			if (this._gru != null)
			{
				d = this._gru.get_ID();
			}
			if (period.get_Count() <= 0)
			{
				MessageBox.Show("Не верный выбор периода!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				this.Rep(period[0].get_ID(), d, (long)this.cmbStatusLS.SelectedIndex, (long)(this.cmbStatusLiz.SelectedIndex - 1), num, selectedIndex);
			}
			base.Close();
		}

		private void cmdStatusGO_TextChanged(object sender, EventArgs e)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPrintSpravkaPoOtklKV_Load(object sender, EventArgs e)
		{
			this.cmbAgent.Focus();
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)1));
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintSpravkaPoOtklKV));
			this.label4 = new Label();
			this.lblNameGRU = new Label();
			this.cmdGRU = new Button();
			this.imageList1 = new ImageList(this.components);
			this.txtInvNumber = new TextBox();
			this.label3 = new Label();
			this.cmdOK = new Button();
			this.label5 = new Label();
			this.cmdClose = new Button();
			this.imageList2 = new ImageList(this.components);
			this.cmbStatusLS = new ComboBox();
			this.cmbStatusLiz = new ComboBox();
			this.label6 = new Label();
			this.cmbAgent = new C1Combo();
			this.dtPeriod = new C1DateEdit();
			this.label1 = new Label();
			this.rbAbo = new RadioButton();
			this.rbVDGO = new RadioButton();
			this.label2 = new Label();
			this.cmbIndHouse = new ComboBox();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			base.SuspendLayout();
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(4, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 34;
			this.label4.Text = "РУ";
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(104, 28);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(200, 20);
			this.lblNameGRU.TabIndex = 35;
			this.cmdGRU.ForeColor = SystemColors.ControlText;
			this.cmdGRU.ImageIndex = 0;
			this.cmdGRU.ImageList = this.imageList1;
			this.cmdGRU.Location = new Point(308, 28);
			this.cmdGRU.Name = "cmdGRU";
			this.cmdGRU.Size = new System.Drawing.Size(20, 20);
			this.cmdGRU.TabIndex = 33;
			this.cmdGRU.Click += new EventHandler(this.cmdGRU_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.txtInvNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtInvNumber.Location = new Point(44, 28);
			this.txtInvNumber.Name = "txtInvNumber";
			this.txtInvNumber.Size = new System.Drawing.Size(56, 20);
			this.txtInvNumber.TabIndex = 32;
			this.txtInvNumber.Text = "";
			this.txtInvNumber.Leave += new EventHandler(this.txtInvNumber_Leave);
			this.txtInvNumber.KeyUp += new KeyEventHandler(this.txtInvNumber_KeyUp);
			this.txtInvNumber.Enter += new EventHandler(this.txtInvNumber_Enter);
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(4, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 73;
			this.label3.Text = "Статус лица:";
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(136, 168);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 68;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(4, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 16);
			this.label5.TabIndex = 71;
			this.label5.Text = "Статус договора:";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(236, 168);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 69;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = Color.Transparent;
			ComboBox.ObjectCollection items = this.cmbStatusLS.Items;
			object[] objArray = new object[] { "По всем", "Активен", "Закрыт" };
			items.AddRange(objArray);
			this.cmbStatusLS.Location = new Point(104, 52);
			this.cmbStatusLS.Name = "cmbStatusLS";
			this.cmbStatusLS.Size = new System.Drawing.Size(224, 21);
			this.cmbStatusLS.TabIndex = 74;
			this.cmbStatusLS.Text = "По всем ";
			ComboBox.ObjectCollection objectCollections = this.cmbStatusLiz.Items;
			objArray = new object[] { "По всем", "Физическое лицо", "Юридическое лицо" };
			objectCollections.AddRange(objArray);
			this.cmbStatusLiz.Location = new Point(104, 76);
			this.cmbStatusLiz.Name = "cmbStatusLiz";
			this.cmbStatusLiz.Size = new System.Drawing.Size(224, 21);
			this.cmbStatusLiz.TabIndex = 74;
			this.cmbStatusLiz.Text = "По всем ";
			this.label6.Location = new Point(4, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 76;
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
			this.cmbAgent.Location = new Point(44, 4);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(284, 19);
			this.cmbAgent.TabIndex = 75;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMM yyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(103, 104);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(105, 18);
			this.dtPeriod.TabIndex = 77;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 16);
			this.label1.TabIndex = 78;
			this.label1.Text = "Период";
			this.rbAbo.Checked = true;
			this.rbAbo.Location = new Point(212, 102);
			this.rbAbo.Name = "rbAbo";
			this.rbAbo.Size = new System.Drawing.Size(48, 24);
			this.rbAbo.TabIndex = 79;
			this.rbAbo.TabStop = true;
			this.rbAbo.Text = "АбО";
			this.rbVDGO.Location = new Point(260, 103);
			this.rbVDGO.Name = "rbVDGO";
			this.rbVDGO.Size = new System.Drawing.Size(64, 24);
			this.rbVDGO.TabIndex = 80;
			this.rbVDGO.Text = "ВДГО";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(4, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 16);
			this.label2.TabIndex = 81;
			this.label2.Text = "Номер дома";
			ComboBox.ObjectCollection items1 = this.cmbIndHouse.Items;
			objArray = new object[] { "Все", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
			items1.AddRange(objArray);
			this.cmbIndHouse.Location = new Point(104, 128);
			this.cmbIndHouse.Name = "cmbIndHouse";
			this.cmbIndHouse.Size = new System.Drawing.Size(104, 21);
			this.cmbIndHouse.TabIndex = 82;
			this.cmbIndHouse.Text = "Все";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(330, 196);
			base.Controls.Add(this.cmbIndHouse);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.rbVDGO);
			base.Controls.Add(this.rbAbo);
			base.Controls.Add(this.dtPeriod);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.cmbAgent);
			base.Controls.Add(this.cmbStatusLS);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.lblNameGRU);
			base.Controls.Add(this.cmdGRU);
			base.Controls.Add(this.txtInvNumber);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.cmbStatusLiz);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(336, 220);
			base.MinimumSize = new System.Drawing.Size(336, 220);
			base.Name = "frmPrintSpravkaPoOtklKV";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Cправка по отключенным квартирам";
			base.Load += new EventHandler(this.frmPrintSpravkaPoOtklKV_Load);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDPeriod, long IDGRU, long StatysLS, long StatysLiz, long Agent, long IndHouse)
		{
			string str;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2, 2, 2, 2, 2 };
					string[] strArrays = new string[] { "@idperiod", "@idgru", "@Status", "@isJuridical", "@idagent", "@IndHouse" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { IDPeriod.ToString(), IDGRU.ToString(), StatysLS.ToString(), StatysLiz.ToString(), Agent.ToString(), IndHouse.ToString() };
					string[] strArrays2 = strArrays;
					str = (!this.rbAbo.Checked ? string.Concat(Depot.oSettings.ReportPath.Trim(), "repSpravkaPoOtklKVVDGO.rpt") : string.Concat(Depot.oSettings.ReportPath.Trim(), "repSpravkaPoOtklKV.rpt"));
					Form frmReport = new frmReports(str, strArrays1, strArrays2, numArray)
					{
						Text = "Cправка по отключенным квартирам",
						MdiParent = Depot._main
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