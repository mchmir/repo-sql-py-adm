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
	public class frmCreationClaimDocs : Form
	{
		private TextBox txtInvNumber;

		private Button cmdGRU;

		private Label lblNameGRU;

		private Label label4;

		private ImageList imageList1;

		private NumericUpDown numAmount;

		private Label label1;

		private C1DateEdit dtDate;

		private Label label6;

		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private Button cmdOK;

		private Button cmdFind;

		private IContainer components;

		private ListViewSortManager m_sortMgr1;

		private GRU _gru;

		private Agents _agents;

		private Button bCheckAll;

		private Button bUnCheckAll;

		private TabControl tcTabControl;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private C1Combo cmbAgent;

		private Contracts _contracts;

		public frmCreationClaimDocs()
		{
			this.InitializeComponent();
		}

		private void bCheckAll_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.lv.Items)
			{
				item.Checked = true;
			}
		}

		private void bUnCheckAll_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.lv.Items)
			{
				item.Checked = false;
			}
		}

		private void cmdFind_Click(object sender, EventArgs e)
		{
			string[] str;
			decimal value;
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.Enabled = true;
				List list = new List();
				list.set_nametable_pr("contract");
				long selectedIndex = (long)this.tcTabControl.SelectedIndex;
				if (selectedIndex <= (long)1 && selectedIndex >= (long)0)
				{
					switch ((int)selectedIndex)
					{
						case 0:
						{
							if (this._gru != null)
							{
								str = new string[] { "select c.idcontract,c.account,case when p.isJuridical=0 then isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') else p.Name end FIO,isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address,round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance from contract c  with (nolock) inner join person p  with (nolock) on c.idperson=p.idperson inner join gobject g  with (nolock) on g.idcontract=c.idcontract inner join address a  with (nolock) on a.idaddress=g.idaddress inner join house h  with (nolock) on h.idhouse=a.idhouse inner join street s  with (nolock) on s.idstreet=h.idstreet where g.idgru=", null, null, null, null, null };
								selectedIndex = this._gru.get_ID();
								str[1] = selectedIndex.ToString();
								str[2] = " and dbo.fGetLastPayment(c.IdContract)<";
								str[3] = Tools.ConvertDateFORSQL((DateTime)this.dtDate.Value);
								str[4] = " and round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2)<-";
								value = this.numAmount.Value;
								str[5] = value.ToString();
								list.set_select_pr(string.Concat(str));
								break;
							}
							else
							{
								this.txtInvNumber.Focus();
								return;
							}
						}
						case 1:
						{
							long d = (long)0;
							d = (long)((int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID());
							str = new string[] { "select c.idcontract,c.account,case when p.isJuridical=0 then isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') else p.Name end FIO,isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address,round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance from contract c  with (nolock) inner join person p  with (nolock) on c.idperson=p.idperson inner join gobject g  with (nolock) on g.idcontract=c.idcontract inner join address a  with (nolock) on a.idaddress=g.idaddress inner join house h  with (nolock) on h.idhouse=a.idhouse inner join street s  with (nolock) on s.idstreet=h.idstreet inner join gru gr with (nolock) on g.idgru=gr.idgru where gr.idagent=", d.ToString(), " and dbo.fGetLastPayment(c.IdContract)<", Tools.ConvertDateFORSQL((DateTime)this.dtDate.Value), " and round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2)<-", null };
							value = this.numAmount.Value;
							str[5] = value.ToString();
							list.set_select_pr(string.Concat(str));
							break;
						}
					}
				}
				list.Load();
				this.lv.Items.Clear();
				foreach (string[] mylistPr in list.get_mylist_pr())
				{
					ListViewItem listViewItem = new ListViewItem(mylistPr[1]);
					listViewItem.SubItems.Add(mylistPr[2]);
					listViewItem.SubItems.Add(mylistPr[3]);
					listViewItem.SubItems.Add(mylistPr[4]);
					listViewItem.Tag = mylistPr[0];
					listViewItem.Checked = false;
					this.lv.Items.Add(listViewItem);
				}
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
			}
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
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					string str = "";
					foreach (ListViewItem checkedItem in this.lv.CheckedItems)
					{
						Document document = new Document()
						{
							oBatch = null
						};
						document.SetValue("IDContract", Convert.ToInt32(checkedItem.Tag));
						document.oPeriod = Depot.CurrentPeriod;
						document.oTypeDocument = Depot.oTypeDocuments.item((long)10);
						document.DocumentAmount = Convert.ToDouble(checkedItem.SubItems[3].Text.Replace(".", ","));
						document.DocumentDate = DateTime.Today;
						if (document.Save() != 0)
						{
							checkedItem.Selected = true;
							checkedItem.EnsureVisible();
							this.lv.Enabled = false;
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						else
						{
							checkedItem.Checked = false;
							long d = document.get_ID();
							str = string.Concat(str, d.ToString(), ",");
							base.DialogResult = System.Windows.Forms.DialogResult.OK;
						}
					}
					str = str.Substring(0, str.Length - 1);
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repClaimDoc.rpt");
					string[] strArrays = new string[] { "@strIdDoc" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { str };
					int[] numArray = new int[] { 1 };
					frmReports frmReport = new frmReports(str1, strArrays1, strArrays, numArray)
					{
						MdiParent = Depot._main
					};
					frmReport.Show();
					frmReport = null;
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void dtDate_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdFind.Focus();
			}
		}

		private void frmCreationClaimDocs_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmCreationClaimDocs_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				ListView listView = this.lv;
				Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this.dtDate.Value = DateTime.Today.Date;
				this._agents = new Agents();
				this._agents.Load(Depot.oTypeAgents.item((long)1));
				Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "-Выберите контролера-");
			}
			catch
			{
			}
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
				this.numAmount.Focus();
				return;
			}
			this.lblNameGRU.Text = "Укажите номер РУ";
			this.txtInvNumber.ForeColor = Color.Red;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmCreationClaimDocs));
			this.txtInvNumber = new TextBox();
			this.cmdGRU = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblNameGRU = new Label();
			this.label4 = new Label();
			this.numAmount = new NumericUpDown();
			this.label1 = new Label();
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.cmdOK = new Button();
			this.cmdFind = new Button();
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.bCheckAll = new Button();
			this.bUnCheckAll = new Button();
			this.tcTabControl = new TabControl();
			this.tabPage1 = new TabPage();
			this.tabPage2 = new TabPage();
			this.cmbAgent = new C1Combo();
			((ISupportInitialize)this.numAmount).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			this.tcTabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			base.SuspendLayout();
			this.txtInvNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtInvNumber.Location = new Point(40, 8);
			this.txtInvNumber.Name = "txtInvNumber";
			this.txtInvNumber.Size = new System.Drawing.Size(72, 20);
			this.txtInvNumber.TabIndex = 1;
			this.txtInvNumber.Text = "";
			this.txtInvNumber.Leave += new EventHandler(this.txtInvNumber_Leave);
			this.txtInvNumber.KeyUp += new KeyEventHandler(this.txtInvNumber_KeyUp);
			this.txtInvNumber.Enter += new EventHandler(this.txtInvNumber_Enter);
			this.cmdGRU.FlatStyle = FlatStyle.Flat;
			this.cmdGRU.ForeColor = SystemColors.ControlText;
			this.cmdGRU.ImageIndex = 0;
			this.cmdGRU.ImageList = this.imageList1;
			this.cmdGRU.Location = new Point(288, 8);
			this.cmdGRU.Name = "cmdGRU";
			this.cmdGRU.Size = new System.Drawing.Size(20, 20);
			this.cmdGRU.TabIndex = 2;
			this.cmdGRU.Click += new EventHandler(this.cmdGRU_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(120, 8);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(160, 20);
			this.lblNameGRU.TabIndex = 58;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 16);
			this.label4.TabIndex = 57;
			this.label4.Text = "РУ";
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(152, 88);
			NumericUpDown num = this.numAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.Size = new System.Drawing.Size(152, 20);
			this.numAmount.TabIndex = 3;
			this.numAmount.KeyPress += new KeyPressEventHandler(this.numAmount_KeyPress);
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 71;
			this.label1.Text = "Сумма долга более";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(152, 112);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 4;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 14, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.KeyPress += new KeyPressEventHandler(this.dtDate_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(136, 16);
			this.label6.TabIndex = 73;
			this.label6.Text = "Дата оплаты не позднее";
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(440, 104);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(104, 24);
			this.cmdOK.TabIndex = 6;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmdFind.FlatStyle = FlatStyle.Flat;
			this.cmdFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.cmdFind.Location = new Point(440, 72);
			this.cmdFind.Name = "cmdFind";
			this.cmdFind.Size = new System.Drawing.Size(104, 24);
			this.cmdFind.TabIndex = 5;
			this.cmdFind.Text = "Найти";
			this.cmdFind.Click += new EventHandler(this.cmdFind_Click);
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			this.lv.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader3 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 136);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(600, 352);
			this.lv.TabIndex = 7;
			this.lv.View = View.Details;
			this.columnHeader1.Text = "Л/счет";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "ФИО";
			this.columnHeader11.Width = 150;
			this.columnHeader2.Text = "Адрес";
			this.columnHeader2.Width = 150;
			this.columnHeader3.Text = "Задолженность";
			this.columnHeader3.Width = 100;
			this.bCheckAll.FlatStyle = FlatStyle.Flat;
			this.bCheckAll.Location = new Point(440, 8);
			this.bCheckAll.Name = "bCheckAll";
			this.bCheckAll.Size = new System.Drawing.Size(104, 24);
			this.bCheckAll.TabIndex = 74;
			this.bCheckAll.Text = "Отметить все";
			this.bCheckAll.Click += new EventHandler(this.bCheckAll_Click);
			this.bUnCheckAll.FlatStyle = FlatStyle.Flat;
			this.bUnCheckAll.Location = new Point(440, 40);
			this.bUnCheckAll.Name = "bUnCheckAll";
			this.bUnCheckAll.Size = new System.Drawing.Size(104, 24);
			this.bUnCheckAll.TabIndex = 75;
			this.bUnCheckAll.Text = "Снять отметку";
			this.bUnCheckAll.Click += new EventHandler(this.bUnCheckAll_Click);
			this.tcTabControl.Controls.Add(this.tabPage1);
			this.tcTabControl.Controls.Add(this.tabPage2);
			this.tcTabControl.Location = new Point(8, 8);
			this.tcTabControl.Name = "tcTabControl";
			this.tcTabControl.SelectedIndex = 0;
			this.tcTabControl.Size = new System.Drawing.Size(328, 64);
			this.tcTabControl.TabIndex = 76;
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.lblNameGRU);
			this.tabPage1.Controls.Add(this.txtInvNumber);
			this.tabPage1.Controls.Add(this.cmdGRU);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(320, 38);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "РУ";
			this.tabPage2.Controls.Add(this.cmbAgent);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(320, 38);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Контролеры";
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
			this.cmbAgent.Location = new Point(8, 10);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(300, 19);
			this.cmbAgent.TabIndex = 62;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(554, 496);
			base.Controls.Add(this.tcTabControl);
			base.Controls.Add(this.bUnCheckAll);
			base.Controls.Add(this.bCheckAll);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdFind);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.numAmount);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MinimumSize = new System.Drawing.Size(560, 528);
			base.Name = "frmCreationClaimDocs";
			this.Text = "Формирование претензий";
			base.Closing += new CancelEventHandler(this.frmCreationClaimDocs_Closing);
			base.Load += new EventHandler(this.frmCreationClaimDocs_Load);
			((ISupportInitialize)this.numAmount).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			this.tcTabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgent).EndInit();
			base.ResumeLayout(false);
		}

		private void numAmount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmount);
		}

		private void numAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDate.Focus();
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