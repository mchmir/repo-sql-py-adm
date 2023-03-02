using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmTehObsluchs : Form
	{
		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ToolBar tbData;

		private ToolBarButton tbAdd;

		private ToolBarButton toolBarButton16;

		private ToolBarButton toolBarButton23;

		private ToolBarButton toolBarButton25;

		private ToolBarButton toolBarButton26;

		private ToolBarButton toolBarButton27;

		private ToolBarButton toolBarButton31;

		private ToolBarButton toolBarButton1;

		private ToolBarButton toolBarButton2;

		private ToolBarButton toolBarButton3;

		private IContainer components;

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private ColumnHeader columnHeader15;

		private Documents _docs;

		private Agents _agents;

		private double summa = 0;

		private ImageList imageList1;

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuAdd;

		private MenuItem menuEdit;

		private MenuItem menuDel;

		private MenuItem menuItem4;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private GroupBox groupBox1;

		private Label label1;

		private C1DateEdit dtDate;

		private Label label2;

		private C1Combo CmbUslugiVDGO;

		private MenuItem menuPrint;

		private Label label3;

		private C1Combo cmbMechanic;

		private UslugiVDGOs _UslugiVDGOs;

		private Agents _agents5;

		public frmTehObsluchs()
		{
			this.InitializeComponent();
			this.dtDate.Value = DateTime.Today.Date;
			this._UslugiVDGOs = new UslugiVDGOs();
			this._UslugiVDGOs.Load();
			Tools.FillC1(this._UslugiVDGOs, this.CmbUslugiVDGO, (long)5);
			this._agents5 = new Agents();
			this._agents5.Load(Depot.oTypeAgents.item((long)5));
			Tools.FillC1(this._agents5, this.cmbMechanic, (long)90);
		}

		private void AddDoc()
		{
			Document value = null;
			value = this._docs.Add();
			value.DocumentDate = (DateTime)this.dtDate.Value;
			if ((new frmTechObsluch(value, this._UslugiVDGOs[this.CmbUslugiVDGO.SelectedIndex], this._agents5[this.cmbMechanic.SelectedIndex])).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.FillOneItem(value, true);
			}
			value = null;
			base.Focus();
		}

		private void DeleteDoc()
		{
			if (this.lv.SelectedItems.Count > 0 && MessageBox.Show("Удалить текущий документ?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				if (this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).oPeriod.get_ID() != Depot.CurrentPeriod.get_ID())
				{
					MessageBox.Show("Ошибка удаления объекта! Документ за прошлый период!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				double documentAmount = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).DocumentAmount;
				if (this._docs.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) == 0)
				{
					this.lv.Items.Remove(this.lv.SelectedItems[0]);
					this.summa -= documentAmount;
					this.FillStatusBar();
					return;
				}
				MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

		private void EditDoc()
		{
		}

		private void FillList()
		{
			try
			{
				this.summa = 0;
				this.Cursor = Cursors.WaitCursor;
				this.lv.Items.Clear();
				this._docs = new Documents();
				this.RefreshCaption();
				this._docs.Load(Depot.oTypeDocuments.item((long)24), this.DateBegin, this.DateEnd);
				foreach (Document _doc in this._docs)
				{
					DateTime documentDate = _doc.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = _doc.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_doc.oContract.Account);
					listViewItem.SubItems.Add(_doc.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(_doc.oContract.oPerson.oAddress.get_ShortAddress());
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.DocumentAmount, 2)));
					this.summa += _doc.DocumentAmount;
					listViewItem.SubItems.Add(this._agents.item(Convert.ToInt64(_doc.GetPD(16).Value)).get_Name());
					listViewItem.SubItems.Add(_doc.Note);
					try
					{
						if (_doc.oContract.oActiveGobject.GetActiveGMeter() == null)
						{
							listViewItem.SubItems.Add("Отключен");
						}
						else
						{
							listViewItem.SubItems.Add("Подключен");
						}
					}
					catch
					{
						listViewItem.SubItems.Add("Отключен");
					}
					this.lv.Items.Add(listViewItem);
				}
				this.FillStatusBar();
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillOneItem(Document o, bool isAdd)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.RefreshCaption();
				if (!isAdd)
				{
					this.lv.SelectedItems[0].SubItems[0].Text = o.DocumentDate.ToShortDateString();
					this.lv.SelectedItems[0].SubItems[1].Text = o.oContract.Account;
					this.lv.SelectedItems[0].SubItems[2].Text = o.oContract.oPerson.FullName;
					this.lv.SelectedItems[0].SubItems[3].Text = o.oContract.oPerson.oAddress.get_ShortAddress();
					this.lv.SelectedItems[0].SubItems[4].Text = Convert.ToString(Math.Round(o.DocumentAmount, 2));
					this.lv.SelectedItems[0].SubItems[5].Text = this._agents.item(Convert.ToInt64(o.GetPD(16).Value)).get_Name();
					this.lv.SelectedItems[0].SubItems[6].Text = o.Note;
				}
				else
				{
					DateTime documentDate = o.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = o.get_ID().ToString()
					};
					listViewItem.SubItems.Add(o.oContract.Account);
					listViewItem.SubItems.Add(o.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(o.oContract.oPerson.oAddress.get_ShortAddress());
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.DocumentAmount, 2)));
					this.summa += o.DocumentAmount;
					listViewItem.SubItems.Add(this._agents.item(Convert.ToInt64(o.GetPD(16).Value)).get_Name());
					listViewItem.SubItems.Add(o.Note);
					this.lv.Items.Add(listViewItem);
				}
				this.FillStatusBar();
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillStatusBar()
		{
			try
			{
				string[] strArrays = new string[2];
				int count = this.lv.Items.Count;
				strArrays[0] = string.Concat("Загружено: ", count.ToString());
				strArrays[1] = string.Concat("На сумму: ", this.summa.ToString("#.##"));
				Depot.status = strArrays;
			}
			catch
			{
			}
		}

		private void frmTehObsluchs_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
			Depot.status = new string[] { "", "" };
		}

		private void frmTehObsluchs_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Insert:
				{
					this.AddDoc();
					return;
				}
				case Keys.Delete:
				{
					this.DeleteDoc();
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void frmTehObsluchs_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				DateTime today = DateTime.Today;
				DateTime dateTime = DateTime.Today;
				this.DateBegin = today.AddDays((double)(-dateTime.Day + 1));
				today = DateTime.Today;
				int day = -DateTime.Today.Day;
				int year = DateTime.Today.Year;
				dateTime = DateTime.Today;
				this.DateEnd = today.AddDays((double)(day + DateTime.DaysInMonth(year, dateTime.Month)));
				ListView listView = this.lv;
				Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this.Refresh();
				this._agents = new Agents();
				this._agents.Load();
				this.FillList();
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmTehObsluchs));
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader15 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.tbData = new ToolBar();
			this.tbAdd = new ToolBarButton();
			this.toolBarButton16 = new ToolBarButton();
			this.toolBarButton23 = new ToolBarButton();
			this.toolBarButton25 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.toolBarButton27 = new ToolBarButton();
			this.toolBarButton31 = new ToolBarButton();
			this.toolBarButton1 = new ToolBarButton();
			this.toolBarButton2 = new ToolBarButton();
			this.toolBarButton3 = new ToolBarButton();
			this.imageList1 = new ImageList(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuAdd = new MenuItem();
			this.menuEdit = new MenuItem();
			this.menuDel = new MenuItem();
			this.menuItem4 = new MenuItem();
			this.menuPrint = new MenuItem();
			this.groupBox1 = new GroupBox();
			this.dtDate = new C1DateEdit();
			this.label1 = new Label();
			this.label2 = new Label();
			this.CmbUslugiVDGO = new C1Combo();
			this.label3 = new Label();
			this.cmbMechanic = new C1Combo();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.CmbUslugiVDGO).BeginInit();
			((ISupportInitialize)this.cmbMechanic).BeginInit();
			base.SuspendLayout();
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader4, this.columnHeader3, this.columnHeader5, this.columnHeader15, this.columnHeader6 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 88);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(1016, 344);
			this.lv.TabIndex = 4;
			this.lv.View = View.Details;
			this.lv.SelectedIndexChanged += new EventHandler(this.lv_SelectedIndexChanged);
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "Л/счет";
			this.columnHeader11.Width = 75;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 166;
			this.columnHeader4.Text = "Адрес";
			this.columnHeader4.Width = 153;
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 82;
			this.columnHeader5.Text = "Выполнил";
			this.columnHeader5.Width = 134;
			this.columnHeader15.Text = "Примечание";
			this.columnHeader15.Width = 186;
			this.columnHeader6.Text = "Статус ПУ";
			this.columnHeader6.Width = 69;
			this.tbData.Appearance = ToolBarAppearance.Flat;
			this.tbData.AutoSize = false;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.tbAdd, this.toolBarButton16, this.toolBarButton23, this.toolBarButton25, this.toolBarButton26, this.toolBarButton27, this.toolBarButton31, this.toolBarButton1, this.toolBarButton2, this.toolBarButton3 };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.DropDownArrows = true;
			this.tbData.ImageList = this.imageList1;
			this.tbData.Location = new Point(0, 0);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(1016, 28);
			this.tbData.TabIndex = 3;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.tbAdd.ImageIndex = 0;
			this.tbAdd.Tag = "Add";
			this.tbAdd.ToolTipText = "Добавить";
			this.toolBarButton16.ImageIndex = 1;
			this.toolBarButton16.Tag = "Edit";
			this.toolBarButton16.ToolTipText = "Редактировать";
			this.toolBarButton16.Visible = false;
			this.toolBarButton23.ImageIndex = 2;
			this.toolBarButton23.Tag = "Del";
			this.toolBarButton23.ToolTipText = "Удалить";
			this.toolBarButton25.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton26.ImageIndex = 3;
			this.toolBarButton26.Tag = "Excel";
			this.toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.toolBarButton27.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton31.ImageIndex = 4;
			this.toolBarButton31.Tag = "Depth";
			this.toolBarButton31.ToolTipText = "Интервал журнала";
			this.toolBarButton1.ImageIndex = 8;
			this.toolBarButton1.Tag = "Filter";
			this.toolBarButton1.ToolTipText = "Фильтр";
			this.toolBarButton1.Visible = false;
			this.toolBarButton2.ImageIndex = 10;
			this.toolBarButton2.Tag = "Refresh";
			this.toolBarButton2.ToolTipText = "Обновить";
			this.toolBarButton3.ImageIndex = 11;
			this.toolBarButton3.Tag = "PrintNakl";
			this.toolBarButton3.ToolTipText = "Печать";
			this.toolBarButton3.Visible = false;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.contextMenu1.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuAdd, this.menuEdit, this.menuDel, this.menuItem4, this.menuPrint };
			menuItems.AddRange(menuItemArray);
			this.menuAdd.Index = 0;
			this.menuAdd.Text = "Добавить";
			this.menuEdit.Index = 1;
			this.menuEdit.Text = "Изменить";
			this.menuDel.Index = 2;
			this.menuDel.Text = "Удалить";
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "-";
			this.menuPrint.Index = 4;
			this.menuPrint.Text = "Печать";
			this.groupBox1.Controls.Add(this.cmbMechanic);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.CmbUslugiVDGO);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.dtDate);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new Point(0, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1016, 40);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Параметры создаваемого документа";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(40, 16);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(120, 18);
			this.dtDate.TabIndex = 2;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Дата:";
			this.label2.Location = new Point(176, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Вид услуги:";
			this.CmbUslugiVDGO.AddItemSeparator = ';';
			this.CmbUslugiVDGO.BorderStyle = 1;
			this.CmbUslugiVDGO.Caption = "";
			this.CmbUslugiVDGO.CaptionHeight = 17;
			this.CmbUslugiVDGO.CharacterCasing = 0;
			this.CmbUslugiVDGO.ColumnCaptionHeight = 17;
			this.CmbUslugiVDGO.ColumnFooterHeight = 17;
			this.CmbUslugiVDGO.ColumnHeaders = false;
			this.CmbUslugiVDGO.ColumnWidth = 100;
			this.CmbUslugiVDGO.ComboStyle = ComboStyleEnum.DropdownList;
			this.CmbUslugiVDGO.ContentHeight = 15;
			this.CmbUslugiVDGO.DataMode = DataModeEnum.AddItem;
			this.CmbUslugiVDGO.DeadAreaBackColor = Color.Empty;
			this.CmbUslugiVDGO.EditorBackColor = SystemColors.Window;
			this.CmbUslugiVDGO.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.CmbUslugiVDGO.EditorForeColor = SystemColors.WindowText;
			this.CmbUslugiVDGO.EditorHeight = 15;
			this.CmbUslugiVDGO.FlatStyle = FlatModeEnum.Flat;
			this.CmbUslugiVDGO.Images.Add((Image)resourceManager.GetObject("resource"));
			this.CmbUslugiVDGO.ItemHeight = 15;
			this.CmbUslugiVDGO.Location = new Point(240, 16);
			this.CmbUslugiVDGO.MatchEntryTimeout = (long)2000;
			this.CmbUslugiVDGO.MaxDropDownItems = 5;
			this.CmbUslugiVDGO.MaxLength = 32767;
			this.CmbUslugiVDGO.MouseCursor = Cursors.Default;
			this.CmbUslugiVDGO.Name = "CmbUslugiVDGO";
			this.CmbUslugiVDGO.RowDivider.Color = Color.DarkGray;
			this.CmbUslugiVDGO.RowDivider.Style = LineStyleEnum.None;
			this.CmbUslugiVDGO.RowSubDividerColor = Color.DarkGray;
			this.CmbUslugiVDGO.Size = new System.Drawing.Size(248, 19);
			this.CmbUslugiVDGO.TabIndex = 91;
			this.CmbUslugiVDGO.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label3.Location = new Point(496, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 16);
			this.label3.TabIndex = 92;
			this.label3.Text = "Выполнил:";
			this.cmbMechanic.AddItemSeparator = ';';
			this.cmbMechanic.BorderStyle = 1;
			this.cmbMechanic.Caption = "";
			this.cmbMechanic.CaptionHeight = 17;
			this.cmbMechanic.CharacterCasing = 0;
			this.cmbMechanic.ColumnCaptionHeight = 17;
			this.cmbMechanic.ColumnFooterHeight = 17;
			this.cmbMechanic.ColumnHeaders = false;
			this.cmbMechanic.ColumnWidth = 100;
			this.cmbMechanic.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbMechanic.ContentHeight = 15;
			this.cmbMechanic.DataMode = DataModeEnum.AddItem;
			this.cmbMechanic.DeadAreaBackColor = Color.Empty;
			this.cmbMechanic.EditorBackColor = SystemColors.Window;
			this.cmbMechanic.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbMechanic.EditorForeColor = SystemColors.WindowText;
			this.cmbMechanic.EditorHeight = 15;
			this.cmbMechanic.FlatStyle = FlatModeEnum.Flat;
			this.cmbMechanic.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbMechanic.ItemHeight = 15;
			this.cmbMechanic.Location = new Point(560, 16);
			this.cmbMechanic.MatchEntryTimeout = (long)2000;
			this.cmbMechanic.MaxDropDownItems = 5;
			this.cmbMechanic.MaxLength = 32767;
			this.cmbMechanic.MouseCursor = Cursors.Default;
			this.cmbMechanic.Name = "cmbMechanic";
			this.cmbMechanic.RowDivider.Color = Color.DarkGray;
			this.cmbMechanic.RowDivider.Style = LineStyleEnum.None;
			this.cmbMechanic.RowSubDividerColor = Color.DarkGray;
			this.cmbMechanic.Size = new System.Drawing.Size(200, 19);
			this.cmbMechanic.TabIndex = 93;
			this.cmbMechanic.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(1016, 433);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.KeyPreview = true;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmTehObsluchs";
			this.Text = "Журнал услуг";
			base.Closing += new CancelEventHandler(this.frmTehObsluchs_Closing);
			base.Load += new EventHandler(this.frmTehObsluchs_Load);
			base.KeyUp += new KeyEventHandler(this.frmTehObsluchs_KeyUp);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.CmbUslugiVDGO).EndInit();
			((ISupportInitialize)this.cmbMechanic).EndInit();
			base.ResumeLayout(false);
		}

		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void menuAdd_Click(object sender, EventArgs e)
		{
			this.AddDoc();
		}

		private void menuDel_Click(object sender, EventArgs e)
		{
			this.DeleteDoc();
		}

		private void menuEdit_Click(object sender, EventArgs e)
		{
			this.EditDoc();
		}

		private void menuPrint_Click(object sender, EventArgs e)
		{
		}

		private void RefreshCaption()
		{
			string[] shortDateString = new string[] { "Журнал технического обслуживание (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ")" };
			this.Text = string.Concat(shortDateString);
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			"PrintNakl";
			string str = e.Button.Tag.ToString();
			string str1 = str;
			if (str != null)
			{
				str1 = string.IsInterned(str1);
				if ((object)str1 == (object)"Add")
				{
					this.AddDoc();
					return;
				}
				if ((object)str1 == (object)"Edit")
				{
					this.EditDoc();
					return;
				}
				if ((object)str1 == (object)"Del")
				{
					this.DeleteDoc();
					return;
				}
				if ((object)str1 == (object)"Excel")
				{
					Tools.ConvertToExcel(this.lv);
					return;
				}
				if ((object)str1 == (object)"Depth")
				{
					frmDepthView _frmDepthView = new frmDepthView();
					_frmDepthView.SetDate(this.DateBegin, this.DateEnd);
					if (_frmDepthView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						_frmDepthView.GetDate(ref this.DateBegin, ref this.DateEnd);
						this._docs = null;
						this.FillList();
					}
					_frmDepthView = null;
					return;
				}
				if ((object)str1 != (object)"Filter")
				{
					if ((object)str1 == (object)"Refresh")
					{
						this.FillList();
					}
					else if ((object)str1 != (object)"PrintNakl")
					{
						return;
					}
				}
			}
		}
	}
}