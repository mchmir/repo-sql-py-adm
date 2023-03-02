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
	public class frmPrintSpravkaPoRU : Form
	{
		private GroupBox groupBox1;

		private Label label6;

		private C1Combo cmbAgent;

		private C1DateEdit dtPeriod;

		private ComboBox cmbDom;

		private Label label2;

		private Label label4;

		private Label lblNameGRU;

		private Button cmdGRU;

		private TextBox txtInvNumber;

		private Label label1;

		private ImageList imageList1;

		private Button cmdClose;

		private Button cmdOK;

		private ImageList imageList2;

		private TextBox txtCLS;

		private Label label3;

		private CheckBox chkPoc;

		private IContainer components;

		private GRU _gru;

		private Agents _agents;

		public frmPrintSpravkaPoRU()
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
			long num1 = (long)1;
			int num2 = -1;
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
			if (this.chkPoc.Checked)
			{
				num1 = (long)0;
			}
			if (this.txtCLS.Text != "Все")
			{
				num2 = Convert.ToInt32(this.txtCLS.Text);
			}
			if (period.get_Count() <= 0)
			{
				MessageBox.Show("Не верный выбор периода!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			this.Rep(d, period[0].get_ID(), num, (long)this.cmbDom.SelectedIndex, num1, (long)num2);
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

		private void frmPrintSpravkaPoRU_Load(object sender, EventArgs e)
		{
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)1));
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintSpravkaPoRU));
			this.groupBox1 = new GroupBox();
			this.chkPoc = new CheckBox();
			this.label3 = new Label();
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
			this.txtCLS = new TextBox();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.imageList2 = new ImageList(this.components);
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.chkPoc);
			this.groupBox1.Controls.Add(this.label3);
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
			this.groupBox1.Controls.Add(this.txtCLS);
			this.groupBox1.Location = new Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(356, 132);
			this.groupBox1.TabIndex = 35;
			this.groupBox1.TabStop = false;
			this.chkPoc.Location = new Point(8, 108);
			this.chkPoc.Name = "chkPoc";
			this.chkPoc.Size = new System.Drawing.Size(216, 20);
			this.chkPoc.TabIndex = 68;
			this.chkPoc.Text = "Квартир с нулевым потреблением";
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 16);
			this.label3.TabIndex = 67;
			this.label3.Text = "Кол. проживающих:";
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
			this.cmbAgent.Images.Add((Image)resourceManager.GetObject("resource"));
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
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMMyyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(52, 64);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(112, 18);
			this.dtPeriod.TabIndex = 3;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			ComboBox.ObjectCollection items = this.cmbDom.Items;
			object[] objArray = new object[] { "По всем", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
			items.AddRange(objArray);
			this.cmbDom.Location = new Point(220, 64);
			this.cmbDom.Name = "cmbDom";
			this.cmbDom.Size = new System.Drawing.Size(112, 21);
			this.cmbDom.TabIndex = 4;
			this.cmbDom.Text = "По всем ";
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.label2.Location = new Point(172, 64);
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
			this.label1.Location = new Point(4, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 30;
			this.label1.Text = "Период";
			this.txtCLS.BorderStyle = BorderStyle.FixedSingle;
			this.txtCLS.Location = new Point(128, 84);
			this.txtCLS.Name = "txtCLS";
			this.txtCLS.Size = new System.Drawing.Size(72, 20);
			this.txtCLS.TabIndex = 65;
			this.txtCLS.Text = "Все";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(260, 136);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 37;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(156, 136);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 36;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = Color.Transparent;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(358, 163);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdClose);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(364, 188);
			base.MinimumSize = new System.Drawing.Size(364, 188);
			base.Name = "frmPrintSpravkaPoRU";
			this.Text = "Справка по РУ";
			base.Load += new EventHandler(this.frmPrintSpravkaPoRU_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDGRU, long IDPeriod, long Agent, long house, long fu, long cls)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2, 2, 2, 2, 2, 1 };
					string[] str = new string[] { "@idperiod", "@idgru", "@idagent", "@idhouse", "@count", "@fact", "Month" };
					string[] strArrays = str;
					str = new string[] { IDPeriod.ToString(), IDGRU.ToString(), Agent.ToString(), house.ToString(), cls.ToString(), fu.ToString(), this.dtPeriod.Text.ToString() };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSpravkaPoRU.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "Справка по РУ ",
						MdiParent = base.MdiParent
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