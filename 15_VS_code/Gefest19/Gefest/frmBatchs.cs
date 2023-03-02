using FprnM1C;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmBatchs : Form
	{
		private ImageList imageList1;

		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

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

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private Batchs _batchs;

		private Batchs _batchs1;

		private int idagent;

		private int idtypepay;

		private int idstatusbatch;

		private int idtypebatch;

		private double summa_filter;

		private string filter;

		private MenuItem menuAdd;

		private MenuItem menuEdit;

		private MenuItem menuDel;

		private MenuItem menuPrint;

		private ColumnHeader columnHeader6;

		private System.Windows.Forms.ContextMenu contextMenu2;

		private MenuItem menuItem1;

		private MenuItem menuItem2;

		private double summa;

		public frmBatchs()
		{
			this.InitializeComponent();
		}

		private void AddBatch()
		{
			(new frmOpenCashChange(4)).ShowDialog(this);
		}

		private void DeleteBatch()
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

		private void EditBatch()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				Batch batch = null;
				batch = this._batchs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
				if ((new frmBatch(batch, false, new FprnM45Class())).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.FillOneItem(batch, false);
				}
				batch = null;
				base.Focus();
			}
		}

		private void FillList()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.Items.Clear();
				this._batchs = new Batchs();
				this.summa = 0;
				this.RefreshCaption();
				this._batchs.Load(this.DateBegin, this.DateEnd, this.idagent, this.idtypepay, this.idstatusbatch, this.idtypebatch, this.summa_filter);
				foreach (Batch _batch in this._batchs)
				{
					DateTime batchDate = _batch.BatchDate;
					ListViewItem listViewItem = new ListViewItem(batchDate.ToShortDateString())
					{
						Tag = _batch.get_ID().ToString()
					};
					if (_batch.oTypeBatch.get_ID() == (long)2 && _batch.oStatusBatch.get_ID() == (long)1)
					{
						listViewItem.ForeColor = Color.Green;
					}
					if (_batch.oTypeBatch.get_ID() == (long)1 && _batch.oStatusBatch.get_ID() == (long)1)
					{
						listViewItem.ForeColor = Color.Red;
					}
					if (_batch.oTypeBatch.get_ID() == (long)1 && _batch.oStatusBatch.get_ID() == (long)2)
					{
						listViewItem.ForeColor = Color.Black;
					}
					if (_batch.oDispatcher == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						listViewItem.SubItems.Add(_batch.oDispatcher.get_Name());
					}
					if (_batch.oCashier == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						listViewItem.SubItems.Add(_batch.oCashier.get_Name());
					}
					listViewItem.SubItems.Add(_batch.oStatusBatch.get_Name());
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(_batch.BatchAmount, 2)));
					listViewItem.SubItems.Add(_batch.oTypeBatch.get_Name());
					listViewItem.SubItems.Add(_batch.NumberBatch);
					this.lv.Items.Add(listViewItem);
					this.summa += _batch.BatchAmount;
				}
				string[] strArrays = new string[2];
				int count = this.lv.Items.Count;
				strArrays[0] = string.Concat("Загружено: ", count.ToString());
				double num = Math.Round(this.summa, 3);
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

		private void FillOneItem(Batch o, bool isAdd)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.RefreshCaption();
				if (!isAdd)
				{
					if (o.oTypeBatch.get_ID() == (long)2 && o.oStatusBatch.get_ID() == (long)1)
					{
						this.lv.SelectedItems[0].ForeColor = Color.Green;
					}
					if (o.oTypeBatch.get_ID() == (long)1 && o.oStatusBatch.get_ID() == (long)1)
					{
						this.lv.SelectedItems[0].ForeColor = Color.Red;
					}
					if (o.oTypeBatch.get_ID() == (long)1 && o.oStatusBatch.get_ID() == (long)2)
					{
						this.lv.SelectedItems[0].ForeColor = Color.Black;
					}
					this.lv.SelectedItems[0].SubItems[0].Text = o.BatchDate.ToShortDateString();
					if (o.oDispatcher == null)
					{
						this.lv.SelectedItems[0].SubItems[1].Text = "нет";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[1].Text = o.oDispatcher.get_Name();
					}
					if (o.oCashier == null)
					{
						this.lv.SelectedItems[0].SubItems[2].Text = "нет";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[2].Text = o.oCashier.get_Name();
					}
					this.lv.SelectedItems[0].SubItems[3].Text = o.oStatusBatch.get_Name();
					this.lv.SelectedItems[0].SubItems[4].Text = Convert.ToString(Math.Round(o.BatchAmount, 2));
					this.lv.SelectedItems[0].SubItems[5].Text = o.oTypeBatch.get_Name();
					this.lv.SelectedItems[0].SubItems[6].Text = o.NumberBatch;
				}
				else
				{
					DateTime batchDate = o.BatchDate;
					ListViewItem listViewItem = new ListViewItem(batchDate.ToShortDateString())
					{
						Tag = o.get_ID().ToString()
					};
					if (o.oTypeBatch.get_ID() == (long)2 && o.oStatusBatch.get_ID() == (long)1)
					{
						listViewItem.ForeColor = Color.Green;
					}
					if (o.oTypeBatch.get_ID() == (long)1 && o.oStatusBatch.get_ID() == (long)1)
					{
						listViewItem.ForeColor = Color.Red;
					}
					if (o.oTypeBatch.get_ID() == (long)1 && o.oStatusBatch.get_ID() == (long)2)
					{
						listViewItem.ForeColor = Color.Black;
					}
					if (o.oDispatcher == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						listViewItem.SubItems.Add(o.oDispatcher.get_Name());
					}
					if (o.oCashier == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						listViewItem.SubItems.Add(o.oCashier.get_Name());
					}
					listViewItem.SubItems.Add(o.oStatusBatch.get_Name());
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.BatchAmount, 2)));
					listViewItem.SubItems.Add(o.oTypeBatch.get_Name());
					listViewItem.SubItems.Add(o.NumberBatch);
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

		private void frmBatchs_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
			Depot.status = new string[] { "", "" };
		}

		private void frmBatchs_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Insert:
				{
					this.AddBatch();
					return;
				}
				case Keys.Delete:
				{
					this.DeleteBatch();
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void frmBatchs_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				this.filter = "не установлен";
				this.idagent = 0;
				this.idtypepay = 0;
				this.idstatusbatch = 0;
				this.idtypebatch = 0;
				this.summa_filter = 0;
				DateTime today = DateTime.Today;
				DateTime dateTime = DateTime.Today;
				this.DateBegin = today.AddDays((double)(-dateTime.Day + 1));
				today = DateTime.Today;
				int day = -DateTime.Today.Day;
				int year = DateTime.Today.Year;
				dateTime = DateTime.Today;
				this.DateEnd = today.AddDays((double)(day + DateTime.DaysInMonth(year, dateTime.Month)));
				ListView listView = this.lv;
				Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this.Refresh();
				this.FillList();
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmBatchs));
			this.imageList1 = new ImageList(this.components);
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuAdd = new MenuItem();
			this.menuEdit = new MenuItem();
			this.menuDel = new MenuItem();
			this.menuItem4 = new MenuItem();
			this.menuPrint = new MenuItem();
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
			this.contextMenu2 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new MenuItem();
			this.menuItem2 = new MenuItem();
			base.SuspendLayout();
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader6, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5 };
			columns.AddRange(columnHeaderArray);
			this.lv.ContextMenu = this.contextMenu1;
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 28);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(792, 404);
			this.lv.TabIndex = 2;
			this.lv.View = View.Details;
			this.lv.KeyPress += new KeyPressEventHandler(this.lv_KeyPress);
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.lv.SelectedIndexChanged += new EventHandler(this.lv_SelectedIndexChanged);
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "Агент";
			this.columnHeader11.Width = 110;
			this.columnHeader6.Text = "Кассир";
			this.columnHeader6.Width = 110;
			this.columnHeader2.Text = "Статус";
			this.columnHeader2.Width = 110;
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 75;
			this.columnHeader4.Text = "Тип пачки";
			this.columnHeader4.Width = 110;
			this.columnHeader5.Text = "Номер";
			this.columnHeader5.Width = 75;
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
			this.menuDel.Visible = false;
			this.menuDel.Click += new EventHandler(this.menuDel_Click);
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "-";
			this.menuPrint.Index = 4;
			this.menuPrint.Text = "Печать";
			this.menuPrint.Click += new EventHandler(this.menuPrint_Click);
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
			this.toolBarButton23.Visible = false;
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
			this.toolBarButton3.DropDownMenu = this.contextMenu2;
			this.toolBarButton3.ImageIndex = 11;
			this.toolBarButton3.Style = ToolBarButtonStyle.DropDownButton;
			this.toolBarButton3.Tag = "PrintNakl";
			this.toolBarButton3.ToolTipText = "Печать";
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections = this.contextMenu2.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem1, this.menuItem2 };
			menuItemCollections.AddRange(menuItemArray);
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Печать ПКО/РКО";
			this.menuItem1.Click += new EventHandler(this.menuItem1_Click);
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Печать реестра";
			this.menuItem2.Click += new EventHandler(this.menuItem2_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(792, 433);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmBatchs";
			this.Text = "Журнал пачек";
			base.Closing += new CancelEventHandler(this.frmBatchs_Closing);
			base.Load += new EventHandler(this.frmBatchs_Load);
			base.KeyUp += new KeyEventHandler(this.frmBatchs_KeyUp);
			base.ResumeLayout(false);
		}

		private void lv_DoubleClick(object sender, EventArgs e)
		{
			this.EditBatch();
		}

		private void lv_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.EditBatch();
			}
		}

		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void menuAdd_Click(object sender, EventArgs e)
		{
			this.AddBatch();
		}

		private void menuDel_Click(object sender, EventArgs e)
		{
			this.DeleteBatch();
		}

		private void menuEdit_Click(object sender, EventArgs e)
		{
			this.EditBatch();
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			this.Print();
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			this.PrintReestr();
		}

		private void menuPrint_Click(object sender, EventArgs e)
		{
			this.Print();
		}

		private void Print()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				Batch batch = null;
				batch = this._batchs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
				if (batch.oTypeBatch.get_ID() == (long)3 || batch.oTypeBatch.get_ID() == (long)6)
				{
					if (batch.oTypeBatch.get_ID() == (long)3)
					{
						this.PrintRKO(batch, 1);
					}
					if (batch.oTypeBatch.get_ID() == (long)6)
					{
						this.PrintRKO(batch, 2);
						return;
					}
				}
				else
				{
					this.PrintPKO(batch);
				}
			}
		}

		public bool PrintPKO(Batch oBatch)
		{
			bool flag = false;
			try
			{
				double batchAmount = 0;
				this._batchs1 = new Batchs();
				this._batchs1.Load(oBatch.BatchDate, oBatch.oCashier);
				foreach (Batch batch in this._batchs1)
				{
					batchAmount += batch.BatchAmount;
				}
				string str = "";
				string str1 = "";
				if (oBatch.oDispatcher != null)
				{
					str1 = oBatch.oDispatcher.get_Name().ToString();
				}
				char[] chr = new char[] { Convert.ToChar("@") };
				string[] strArrays = oBatch.get_Name().Split(chr);
				if ((int)strArrays.Length == 2)
				{
					str = strArrays[0];
					if (str1 == "")
					{
						str1 = strArrays[1];
					}
				}
				string str2 = Tools.ConvertCurencyInString(batchAmount);
				string[] name = new string[] { "zAccount", "zDay", "zMonth", "zYear", "zMonth2", "zBatchAmount", "zNameCashier", "zBatchAmountStr", "zNameDispetcher", "zNumber" };
				string[] strArrays1 = name;
				name = new string[] { str, null, null, null, null, null, null, null, null, null };
				int day = DateTime.Today.Day;
				name[1] = day.ToString();
				day = DateTime.Today.Month;
				name[2] = day.ToString();
				day = DateTime.Today.Year;
				name[3] = day.ToString();
				DateTime today = DateTime.Today;
				name[4] = Tools.NameMonth(today.Month);
				name[5] = batchAmount.ToString();
				name[6] = oBatch.oCashier.get_Name();
				name[7] = str2;
				name[8] = str1;
				name[9] = oBatch.NumberBatch.ToString();
				string[] strArrays2 = name;
				Tools.ShowWordDocument(string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\ПКО.dot"), strArrays1, strArrays2);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - РКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		private void PrintReestr()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				Batch batch = null;
				batch = this._batchs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
				if (batch.oTypeBatch.get_ID() == (long)1 || batch.oTypeBatch.get_ID() == (long)2)
				{
					string str = string.Concat(Depot.oSettings.ReportPath, "repBatch.rpt");
					string[] strArrays = new string[] { "@idBatch" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { this.lv.SelectedItems[0].Tag.ToString() };
					int[] numArray = new int[] { 2 };
					frmReports frmReport = new frmReports(str, strArrays1, strArrays, numArray)
					{
						MdiParent = Depot._main
					};
					frmReport.Show();
					frmReport = null;
				}
			}
		}

		public bool PrintRKO(Batch oBatch, int type)
		{
			bool flag = false;
			try
			{
				string str = "";
				if (type == 1)
				{
					str = string.Concat("Сдано в центральную кассу через ", oBatch.oCashier.get_Name().ToString());
				}
				string str1 = "";
				string str2 = Tools.ConvertCurencyInString(oBatch.BatchAmount);
				char[] chr = new char[] { Convert.ToChar("@") };
				string[] strArrays = oBatch.get_Name().Split(chr);
				if ((int)strArrays.Length == 2)
				{
					str1 = strArrays[0];
				}
				string str3 = "";
				if (type == 1)
				{
					str3 = "Внутреннее перемещение";
				}
				string[] name = new string[] { "zBase", "zName", "zAccount", "zDay", "zMonth", "zYear", "zBatchAmount", "zNameCashier", "zBatchAmountStr" };
				string[] strArrays1 = name;
				name = new string[] { str3, str1, str, null, null, null, null, null, null };
				int day = oBatch.BatchDate.Day;
				name[3] = day.ToString();
				day = oBatch.BatchDate.Month;
				name[4] = day.ToString();
				day = oBatch.BatchDate.Year;
				name[5] = day.ToString();
				name[6] = oBatch.BatchAmount.ToString();
				name[7] = oBatch.oCashier.get_Name();
				name[8] = str2;
				string[] strArrays2 = name;
				Tools.ShowWordDocument(string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\РКО.dot"), strArrays1, strArrays2);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - PКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		private void RefreshCaption()
		{
			string[] shortDateString = new string[] { "Журнал пачек (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ") фильтр: ", this.filter };
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
					this.AddBatch();
					return;
				}
				if ((object)str1 == (object)"Edit")
				{
					this.EditBatch();
					return;
				}
				if ((object)str1 == (object)"Del")
				{
					this.DeleteBatch();
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
						this._batchs = null;
						this.FillList();
					}
					_frmDepthView = null;
					return;
				}
				if ((object)str1 == (object)"Filter")
				{
					frmFilterBatchs frmFilterBatch = new frmFilterBatchs();
					if (frmFilterBatch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						frmFilterBatch.GetData(ref this.idagent, ref this.idtypepay, ref this.idstatusbatch, ref this.idtypebatch, ref this.summa_filter, ref this.filter);
						this.FillList();
					}
					frmFilterBatch = null;
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