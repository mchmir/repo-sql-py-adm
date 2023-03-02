using C1.Win.C1Command;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmCarryPayments : Form
	{
		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private C1CommandHolder CommandHolder;

		private C1Command cmd_tbAdd;

		private C1Command cmd_toolBarButton16;

		private C1Command cmd_toolBarButton23;

		private C1Command cmd_toolBarButton25;

		private C1Command cmd_toolBarButton26;

		private C1Command cmd_toolBarButton27;

		private C1Command cmd_toolBarButton31;

		private C1Command cmd_toolBarButton1;

		private C1Command cmd_toolBarButton2;

		private C1Command cmd_toolBarButton3;

		private C1ContextMenu contextMenu1;

		private C1CommandLink menuAdd;

		private C1Command cmd_menuAdd;

		private C1CommandLink menuEdit;

		private C1Command cmd_menuEdit;

		private C1CommandLink menuDel;

		private C1Command cmd_menuDel;

		private C1CommandLink menuItem4;

		private C1CommandLink menuPrint;

		private C1Command cmd_menuPrint;

		private IContainer components;

		private ColumnHeader columnHeader13;

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private ImageList imageList1;

		private ColumnHeader columnHeader10;

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

		private Documents _docs;

		public frmCarryPayments()
		{
			this.InitializeComponent();
		}

		private void AddDoc()
		{
			Document document = null;
			Document document1 = null;
			document = this._docs.Add();
			document1 = this._docs.Add();
			if ((new frmCarryPayment(document, document1)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.FillOneItem(document, document1, true);
			}
			document = null;
			document1 = null;
			base.Focus();
		}

		private void DeleteDoc()
		{
			try
			{
				if (this.lv.SelectedItems.Count > 0 && MessageBox.Show("Удалить текущий документ?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					if (this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).oPeriod.get_ID() == Depot.CurrentPeriod.get_ID())
					{
						Document document = this._docs.item(Convert.ToInt64(this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).GetPD(25).Value));
						long d = document.get_ID();
						if (this._docs.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) != 0)
						{
							MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							this.lv.Items.Remove(this.lv.SelectedItems[0]);
							int indexItem = this.GetIndexItem(d);
							this.lv.Items.RemoveAt(indexItem);
						}
					}
					else
					{
						MessageBox.Show("Ошибка удаления объекта! Документ за прошлый период!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
			}
			catch
			{
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
				this.Cursor = Cursors.WaitCursor;
				double documentAmount = 0;
				this.lv.Items.Clear();
				this._docs = new Documents();
				this.RefreshCaption();
				this._docs.Load(Depot.oTypeDocuments.item((long)3), this.DateBegin, this.DateEnd);
				foreach (Document _doc in this._docs)
				{
					DateTime documentDate = _doc.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = _doc.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_doc.oContract.Account);
					listViewItem.SubItems.Add(_doc.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.DocumentAmount, 2)));
					Document document = new Document();
					document.Load(Convert.ToInt64(_doc.GetPD(25).Value));
					listViewItem.SubItems.Add(document.oContract.Account);
					listViewItem.SubItems.Add(document.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(_doc.Note);
					this.lv.Items.Add(listViewItem);
					documentAmount += _doc.DocumentAmount;
				}
				string[] strArrays = new string[2];
				int count = this.lv.Items.Count;
				strArrays[0] = string.Concat("Загружено: ", count.ToString());
				double num = Math.Round(documentAmount, 3);
				strArrays[1] = string.Concat("На Сумму: ", num.ToString());
				Depot.status = strArrays;
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillOneItem(Document o1, Document o2, bool isAdd)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.RefreshCaption();
				if (isAdd)
				{
					DateTime documentDate = o1.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = o1.get_ID().ToString()
					};
					listViewItem.SubItems.Add(o1.oContract.Account);
					listViewItem.SubItems.Add(o1.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o1.DocumentAmount, 2)));
					Document document = new Document();
					document.Load(Convert.ToInt64(o1.GetPD(25).Value));
					listViewItem.SubItems.Add(document.oContract.Account);
					listViewItem.SubItems.Add(document.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(o1.Note);
					this.lv.Items.Add(listViewItem);
					documentDate = o2.DocumentDate;
					listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = o2.get_ID().ToString()
					};
					listViewItem.SubItems.Add(o2.oContract.Account);
					listViewItem.SubItems.Add(o2.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o2.DocumentAmount, 2)));
					document = new Document();
					document.Load(Convert.ToInt64(o2.GetPD(25).Value));
					listViewItem.SubItems.Add(document.oContract.Account);
					listViewItem.SubItems.Add(document.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(o2.Note);
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

		private void frmCarryPayments_Closing(object sender, CancelEventArgs e)
		{
			Depot.status = new string[] { "", "" };
			Tools.SaveWindows(this);
		}

		private void frmCarryPayments_KeyUp(object sender, KeyEventArgs e)
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

		private void frmCarryPayments_Load(object sender, EventArgs e)
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

		private int GetIndexItem(long Tag)
		{
			int index;
			IEnumerator enumerator = this.lv.Items.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ListViewItem current = (ListViewItem)enumerator.Current;
					if (Convert.ToInt64(current.Tag) != Tag)
					{
						continue;
					}
					index = current.Index;
					return index;
				}
				return -1;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return index;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmCarryPayments));
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader13 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.CommandHolder = new C1CommandHolder();
			this.cmd_menuAdd = new C1Command();
			this.cmd_menuEdit = new C1Command();
			this.cmd_menuDel = new C1Command();
			this.cmd_menuPrint = new C1Command();
			this.contextMenu1 = new C1ContextMenu();
			this.menuAdd = new C1CommandLink();
			this.menuEdit = new C1CommandLink();
			this.menuDel = new C1CommandLink();
			this.menuItem4 = new C1CommandLink();
			this.menuPrint = new C1CommandLink();
			this.cmd_toolBarButton2 = new C1Command();
			this.cmd_toolBarButton3 = new C1Command();
			this.cmd_toolBarButton23 = new C1Command();
			this.cmd_toolBarButton25 = new C1Command();
			this.cmd_tbAdd = new C1Command();
			this.cmd_toolBarButton16 = new C1Command();
			this.cmd_toolBarButton31 = new C1Command();
			this.cmd_toolBarButton1 = new C1Command();
			this.cmd_toolBarButton26 = new C1Command();
			this.cmd_toolBarButton27 = new C1Command();
			this.imageList1 = new ImageList(this.components);
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
			((ISupportInitialize)this.CommandHolder).BeginInit();
			base.SuspendLayout();
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader3, this.columnHeader13, this.columnHeader4, this.columnHeader10 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 28);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(792, 404);
			this.lv.TabIndex = 4;
			this.lv.View = View.Details;
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "С л/счета";
			this.columnHeader11.Width = 75;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 110;
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 75;
			this.columnHeader13.Text = "На л/счет";
			this.columnHeader13.Width = 75;
			this.columnHeader4.Text = "ФИО";
			this.columnHeader4.Width = 110;
			this.columnHeader10.Text = "Примечание";
			this.columnHeader10.Width = 100;
			this.CommandHolder.Commands.Add(this.cmd_menuAdd);
			this.CommandHolder.Commands.Add(this.cmd_menuEdit);
			this.CommandHolder.Commands.Add(this.cmd_menuDel);
			this.CommandHolder.Commands.Add(this.cmd_menuPrint);
			this.CommandHolder.Commands.Add(this.contextMenu1);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton2);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton3);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton23);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton25);
			this.CommandHolder.Commands.Add(this.cmd_tbAdd);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton16);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton31);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton1);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton26);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton27);
			this.CommandHolder.Owner = this;
			this.cmd_menuAdd.Name = "cmd_menuAdd";
			this.cmd_menuAdd.Text = "Добавить";
			this.cmd_menuAdd.Click += new ClickEventHandler(this.menuAdd_Click);
			this.cmd_menuEdit.Name = "cmd_menuEdit";
			this.cmd_menuEdit.Text = "Изменить";
			this.cmd_menuEdit.Click += new ClickEventHandler(this.menuEdit_Click);
			this.cmd_menuDel.Name = "cmd_menuDel";
			this.cmd_menuDel.Text = "Удалить";
			this.cmd_menuDel.Click += new ClickEventHandler(this.menuDel_Click);
			this.cmd_menuPrint.Name = "cmd_menuPrint";
			this.cmd_menuPrint.Text = "Печать";
			this.cmd_menuPrint.Click += new ClickEventHandler(this.menuPrint_Click);
			C1CommandLinks commandLinks = this.contextMenu1.CommandLinks;
			C1CommandLink[] c1CommandLinkArray = new C1CommandLink[] { this.menuAdd, this.menuEdit, this.menuDel, this.menuItem4, this.menuPrint };
			commandLinks.AddRange(c1CommandLinkArray);
			this.contextMenu1.Name = "contextMenu1";
			this.menuAdd.Command = this.cmd_menuAdd;
			this.menuEdit.Command = this.cmd_menuEdit;
			this.menuDel.Command = this.cmd_menuDel;
			this.menuItem4.Text = "-";
			this.menuPrint.Command = this.cmd_menuPrint;
			this.cmd_toolBarButton2.ImageIndex = 10;
			this.cmd_toolBarButton2.Name = "cmd_toolBarButton2";
			this.cmd_toolBarButton2.ToolTipText = "Обновить";
			this.cmd_toolBarButton3.ImageIndex = 11;
			this.cmd_toolBarButton3.Name = "cmd_toolBarButton3";
			this.cmd_toolBarButton3.ToolTipText = "Печать";
			this.cmd_toolBarButton3.Visible = false;
			this.cmd_toolBarButton23.ImageIndex = 2;
			this.cmd_toolBarButton23.Name = "cmd_toolBarButton23";
			this.cmd_toolBarButton23.ToolTipText = "Удалить";
			this.cmd_toolBarButton25.Name = "cmd_toolBarButton25";
			this.cmd_tbAdd.ImageIndex = 0;
			this.cmd_tbAdd.Name = "cmd_tbAdd";
			this.cmd_tbAdd.ToolTipText = "Добавить";
			this.cmd_toolBarButton16.ImageIndex = 1;
			this.cmd_toolBarButton16.Name = "cmd_toolBarButton16";
			this.cmd_toolBarButton16.ToolTipText = "Редактировать";
			this.cmd_toolBarButton31.ImageIndex = 4;
			this.cmd_toolBarButton31.Name = "cmd_toolBarButton31";
			this.cmd_toolBarButton31.ToolTipText = "Интервал журнала";
			this.cmd_toolBarButton1.ImageIndex = 8;
			this.cmd_toolBarButton1.Name = "cmd_toolBarButton1";
			this.cmd_toolBarButton1.ToolTipText = "Фильтр";
			this.cmd_toolBarButton1.Visible = false;
			this.cmd_toolBarButton26.ImageIndex = 3;
			this.cmd_toolBarButton26.Name = "cmd_toolBarButton26";
			this.cmd_toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.cmd_toolBarButton27.Name = "cmd_toolBarButton27";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
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
			this.tbData.TabIndex = 5;
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
			this.toolBarButton1.Visible = false;
			this.toolBarButton2.ImageIndex = 10;
			this.toolBarButton2.Tag = "Refresh";
			this.toolBarButton2.ToolTipText = "Обновить";
			this.toolBarButton3.ImageIndex = 11;
			this.toolBarButton3.Tag = "PrintNakl";
			this.toolBarButton3.ToolTipText = "Печать";
			this.toolBarButton3.Visible = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(792, 433);
			base.Controls.Add(this.tbData);
			base.Controls.Add(this.lv);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmCarryPayments";
			this.Text = "Журнал переноса оплат";
			base.Closing += new CancelEventHandler(this.frmCarryPayments_Closing);
			base.Load += new EventHandler(this.frmCarryPayments_Load);
			base.KeyUp += new KeyEventHandler(this.frmCarryPayments_KeyUp);
			((ISupportInitialize)this.CommandHolder).EndInit();
			base.ResumeLayout(false);
		}

		private void menuAdd_Click(object sender, ClickEventArgs e)
		{
			this.AddDoc();
		}

		private void menuDel_Click(object sender, ClickEventArgs e)
		{
			this.DeleteDoc();
		}

		private void menuEdit_Click(object sender, ClickEventArgs e)
		{
			this.EditDoc();
		}

		private void menuPrint_Click(object sender, ClickEventArgs e)
		{
		}

		private void RefreshCaption()
		{
			string[] shortDateString = new string[] { "Журнал переноса оплат (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ")" };
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