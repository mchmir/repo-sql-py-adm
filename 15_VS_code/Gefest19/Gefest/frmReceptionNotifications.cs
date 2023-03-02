using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmReceptionNotifications : Form
	{
		private ImageList imageList1;

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuAdd;

		private MenuItem menuDel;

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

		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private IContainer components;

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private Documents _docs;

		private Gmeter _gmeter;

		private Agent _Agent;

		private StatusBar statusBar1;

		private StatusBarPanel statusBarPanel1;

		private ColumnHeader columnHeader5;

		public frmReceptionNotifications()
		{
			this.InitializeComponent();
		}

		private void AddDoc()
		{
			if ((new frmReceptionNotification()).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
			}
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
				if (this._docs.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) == 0)
				{
					this.lv.Items.Remove(this.lv.SelectedItems[0]);
					this.statusBar1.Panels[0].Text = string.Concat("Количество уведомлений:", this._docs.get_Count());
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

		private void FillList()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.Items.Clear();
				this._docs = new Documents();
				this._gmeter = new Gmeter();
				this._Agent = new Agent();
				this.RefreshCaption();
				this._docs.Load(Depot.oTypeDocuments.item((long)21), this.DateBegin, this.DateEnd);
				foreach (Document _doc in this._docs)
				{
					DateTime documentDate = _doc.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = _doc.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_doc.oContract.Account);
					listViewItem.SubItems.Add(_doc.oContract.oPerson.FullName);
					if (_doc.GetPD(7).get_Name() == null)
					{
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
					}
					else
					{
						this._gmeter.Load((long)Convert.ToInt32(_doc.GetPD(7).get_Name()));
						listViewItem.SubItems.Add(this._gmeter.oTypeGMeter.Fullname);
						listViewItem.SubItems.Add(this._gmeter.SerialNumber.ToString());
					}
					if (_doc.GetPD(26).get_Name() == null)
					{
						listViewItem.SubItems.Add("");
					}
					else
					{
						this._Agent.Load((long)Convert.ToInt32(_doc.GetPD(26).get_Name()));
						listViewItem.SubItems.Add(this._Agent.get_Name().ToString());
					}
					this.lv.Items.Add(listViewItem);
				}
				this.statusBar1.Panels[0].Text = string.Concat("Количество уведомлений:", this._docs.get_Count());
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.statusBar1.Panels[0].Text = "Количество уведомлений: 0";
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void frmReceptionNotifications_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmReceptionNotifications_KeyUp(object sender, KeyEventArgs e)
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

		private void frmReceptionNotifications_Load(object sender, EventArgs e)
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
				Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this.FillList();
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmReceptionNotifications));
			this.imageList1 = new ImageList(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuAdd = new MenuItem();
			this.menuDel = new MenuItem();
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
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.statusBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			base.SuspendLayout();
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.contextMenu1.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuAdd, this.menuDel };
			menuItems.AddRange(menuItemArray);
			this.menuAdd.Index = 0;
			this.menuAdd.Text = "Добавить";
			this.menuAdd.Click += new EventHandler(this.menuAdd_Click);
			this.menuDel.Index = 1;
			this.menuDel.Text = "Удалить";
			this.menuDel.Click += new EventHandler(this.menuDel_Click);
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
			this.tbData.Size = new System.Drawing.Size(792, 28);
			this.tbData.TabIndex = 2;
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
			this.toolBarButton3.ToolTipText = "Печать накладной";
			this.toolBarButton3.Visible = false;
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 32);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(792, 360);
			this.lv.TabIndex = 3;
			this.lv.View = View.Details;
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "Л/счет";
			this.columnHeader11.Width = 110;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 110;
			this.columnHeader3.Text = "Тип ПУ";
			this.columnHeader3.Width = 90;
			this.columnHeader4.Text = "№ПУ";
			this.columnHeader4.Width = 110;
			this.columnHeader5.Text = "Вручил";
			this.columnHeader5.Width = 97;
			this.statusBar1.Location = new Point(0, 397);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new StatusBarPanel[] { this.statusBarPanel1 });
			this.statusBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(792, 24);
			this.statusBar1.TabIndex = 4;
			this.statusBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel1.Text = "Количество уведомлений:";
			this.statusBarPanel1.Width = 776;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(792, 421);
			base.Controls.Add(this.statusBar1);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Name = "frmReceptionNotifications";
			this.Text = "Журнал уведомлений";
			base.Closing += new CancelEventHandler(this.frmReceptionNotifications_Closing);
			base.Load += new EventHandler(this.frmReceptionNotifications_Load);
			base.KeyUp += new KeyEventHandler(this.frmReceptionNotifications_KeyUp);
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			base.ResumeLayout(false);
		}

		private void menuAdd_Click(object sender, EventArgs e)
		{
			this.AddDoc();
		}

		private void menuDel_Click(object sender, EventArgs e)
		{
			this.DeleteDoc();
		}

		private void RefreshCaption()
		{
			this.Text = string.Concat("Журнал уведомлений (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString());
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			"Refresh";
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
				if ((object)str1 != (object)"Refresh")
				{
					return;
				}
				this.FillList();
			}
		}
	}
}