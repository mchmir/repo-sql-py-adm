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
	public class frmClaimDocs : Form
	{
		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

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

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuItem4;

		private IContainer components;

		private MenuItem menuAdd;

		private MenuItem menuEdit;

		private MenuItem menuDel;

		private MenuItem menuPrint;

		private ImageList imageList1;

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private Documents _docs;

		private int idstatus;

		private string filter;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private double summa;

		public frmClaimDocs()
		{
			this.InitializeComponent();
		}

		private void AddDoc()
		{
			if ((new frmCreationClaimDocs()).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.FillList();
			}
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
			if (this.lv.SelectedItems.Count > 0)
			{
				Document document = null;
				document = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
				if ((new frmClaimDoc(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.FillOneItem(document, false);
				}
				document = null;
				base.Focus();
			}
		}

		private void FillList()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.Items.Clear();
				this._docs = new Documents();
				this.summa = 0;
				this.RefreshCaption();
				this._docs.Load(Depot.oTypeDocuments.item((long)10), this.DateBegin, this.DateEnd, this.idstatus);
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
					listViewItem.SubItems.Add(this.GetStatusClaimDoc(_doc));
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.oContract.CurrentBalance(), 2)));
					listViewItem.SubItems.Add(_doc.GetNamePD(23).ToString());
					this.lv.Items.Add(listViewItem);
				}
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
					this.lv.SelectedItems[0].SubItems[3].Text = Convert.ToString(Math.Round(o.DocumentAmount, 2));
					this.lv.SelectedItems[0].SubItems[4].Text = this.GetStatusClaimDoc(o);
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
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.DocumentAmount, 2)));
					listViewItem.SubItems.Add(this.GetStatusClaimDoc(o));
					this.lv.Items.Add(listViewItem);
				}
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void frmClaimDocs_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmClaimDocs_KeyUp(object sender, KeyEventArgs e)
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

		private void frmClaimDocs_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				this.filter = "не установлен";
				this.idstatus = 0;
				DateTime today = DateTime.Today;
				DateTime dateTime = DateTime.Today;
				this.DateBegin = today.AddDays((double)(-dateTime.Day + 1));
				today = DateTime.Today;
				int day = -DateTime.Today.Day;
				int year = DateTime.Today.Year;
				dateTime = DateTime.Today;
				this.DateEnd = today.AddDays((double)(day + DateTime.DaysInMonth(year, dateTime.Month)));
				ListView listView = this.lv;
				Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDateSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this.FillList();
			}
			catch
			{
			}
		}

		private string GetStatusClaimDoc(Document oDocument)
		{
			if (oDocument.GetPD(23) == null)
			{
				return "Сформирована";
			}
			if (oDocument.GetPD(24) != null)
			{
				return "Закрыта";
			}
			return "Вручена";
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmClaimDocs));
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
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
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			base.SuspendLayout();
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader6, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader7 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 28);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(792, 404);
			this.lv.TabIndex = 2;
			this.lv.View = View.Details;
			this.lv.KeyPress += new KeyPressEventHandler(this.lv_KeyPress);
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "Л/счет";
			this.columnHeader11.Width = 110;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 110;
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 75;
			this.columnHeader4.Text = "Состояние";
			this.columnHeader4.Width = 110;
			this.columnHeader5.Text = "Текущий баланс";
			this.columnHeader5.Width = 80;
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
			this.tbData.TabIndex = 1;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.tbAdd.ImageIndex = 0;
			this.tbAdd.Tag = "Add";
			this.tbAdd.ToolTipText = "Добавить";
			this.toolBarButton16.ImageIndex = 1;
			this.toolBarButton16.Tag = "Edit";
			this.toolBarButton16.ToolTipText = "Редактировать";
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
			this.toolBarButton2.ImageIndex = 10;
			this.toolBarButton2.Tag = "Refresh";
			this.toolBarButton2.ToolTipText = "Обновить";
			this.toolBarButton3.ImageIndex = 11;
			this.toolBarButton3.Tag = "PrintNakl";
			this.toolBarButton3.ToolTipText = "Печать претензий";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.contextMenu1.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuAdd, this.menuEdit, this.menuDel, this.menuItem4, this.menuPrint };
			menuItems.AddRange(menuItemArray);
			this.menuAdd.Index = 0;
			this.menuAdd.Text = "Добавить";
			this.menuAdd.Click += new EventHandler(this.menuAdd_Click);
			this.menuEdit.Index = 1;
			this.menuEdit.Text = "Изменить";
			this.menuEdit.Click += new EventHandler(this.menuEdit_Click);
			this.menuDel.Index = 2;
			this.menuDel.Text = "Удалить";
			this.menuDel.Click += new EventHandler(this.menuDel_Click);
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "-";
			this.menuPrint.Index = 4;
			this.menuPrint.Text = "Печать";
			this.menuPrint.Click += new EventHandler(this.menuPrint_Click);
			this.columnHeader6.Text = "Адрес";
			this.columnHeader6.Width = 80;
			this.columnHeader7.Text = "Дата вручения";
			this.columnHeader7.Width = 80;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(792, 433);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmClaimDocs";
			this.Text = "Журнал претензий";
			base.Closing += new CancelEventHandler(this.frmClaimDocs_Closing);
			base.Load += new EventHandler(this.frmClaimDocs_Load);
			base.KeyUp += new KeyEventHandler(this.frmClaimDocs_KeyUp);
			base.ResumeLayout(false);
		}

		private void lv_DoubleClick(object sender, EventArgs e)
		{
			this.EditDoc();
		}

		private void lv_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.EditDoc();
			}
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

		private void Print()
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					string str = "";
					foreach (ListViewItem selectedItem in this.lv.SelectedItems)
					{
						str = string.Concat(str, selectedItem.Tag.ToString(), ",");
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

		private void RefreshCaption()
		{
			string[] shortDateString = new string[] { "Журнал претензий (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ") фильтр: ", this.filter };
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
				if ((object)str1 == (object)"Filter")
				{
					frmFilterClaimDocs frmFilterClaimDoc = new frmFilterClaimDocs();
					if (frmFilterClaimDoc.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						frmFilterClaimDoc.GetData(ref this.idstatus, ref this.filter);
						this.FillList();
					}
					frmFilterClaimDoc = null;
					return;
				}
				if ((object)str1 == (object)"Refresh")
				{
					this.FillList();
					return;
				}
				if ((object)str1 != (object)"PrintNakl")
				{
					return;
				}
				this.Print();
			}
		}
	}
}