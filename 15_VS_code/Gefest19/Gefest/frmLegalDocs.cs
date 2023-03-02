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
	public class frmLegalDocs : Form
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

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private Documents _docs;

		private int idstatus;

		private string filter;

		private ImageList imageList1;

		private MenuItem menuAdd;

		private MenuItem menuEdit;

		private MenuItem menuDel;

		private MenuItem menuPrint;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private ColumnHeader columnHeader10;

		private StatusBar statusBar1;

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel2;

		private StatusBarPanel statusBarPanel3;

		private ColumnHeader columnHeader12;

		private StatusBarPanel statusBarPanel4;

		private StatusBarPanel statusBarPanel5;

		private double summa;

		public frmLegalDocs()
		{
			this.InitializeComponent();
		}

		private void AddDoc()
		{
			Document document = null;
			document = this._docs.Add();
			if ((new frmLegalDoc(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.FillOneItem(document, true);
			}
			document = null;
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
				if ((new frmLegalDoc(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
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
				int num = 0;
				double num1 = 0;
				double num2 = 0;
				double num3 = 0;
				double amountBalance = 0;
				double amountBalance1 = 0;
				this.RefreshCaption();
				this._docs.Load(Depot.oTypeDocuments.item((long)13), this.DateBegin, this.DateEnd, this.idstatus);
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				double num7 = 0;
				double num8 = 0;
				double num9 = 0;
				foreach (Document _doc in this._docs)
				{
					try
					{
						DateTime documentDate = _doc.DocumentDate;
						ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
						{
							Tag = _doc.get_ID().ToString()
						};
						listViewItem.SubItems.Add(_doc.oContract.Account);
						listViewItem.SubItems.Add(_doc.oContract.oPerson.oAddress.get_ShortAddress());
						listViewItem.SubItems.Add(_doc.oContract.oPerson.FullName);
						if (Convert.ToString(_doc.DocumentAmount) == "NULL")
						{
							listViewItem.SubItems.Add("0");
						}
						else
						{
							listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.DocumentAmount, 2)));
						}
						listViewItem.SubItems.Add(this.GetStatusLegalDoc(_doc));
						if (_doc.oContract.CurrentBalance(Depot.oAccountings.item((long)2)) == null)
						{
							listViewItem.SubItems.Add("0");
						}
						else
						{
							listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.oContract.CurrentBalance(Depot.oAccountings.item((long)2)).AmountBalance, 2)));
						}
						listViewItem.SubItems.Add(Convert.ToString(_doc.GetPD(12).get_Name()));
						listViewItem.SubItems.Add(_doc.oContract.oGobjects[0].oGRU.oAgent.get_Name());
						listViewItem.SubItems.Add(_doc.oContract.oGobjects[0].oStatusGObject.get_Name());
						listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.oContract.CurrentBalance(), 2)));
						if (_doc.oContract.CurrentBalance(Depot.oAccountings.item((long)3)) == null)
						{
							listViewItem.SubItems.Add("0");
						}
						else
						{
							listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.oContract.CurrentBalance(Depot.oAccountings.item((long)3)).AmountBalance, 2)));
						}
						this.lv.Items.Add(listViewItem);
						num1 += _doc.oContract.CurrentBalance();
						num3 += Math.Round(_doc.DocumentAmount, 2);
						num2 += Convert.ToDouble(_doc.GetPD(12).get_Name());
						if (_doc.oContract.CurrentBalance(Depot.oAccountings.item((long)2)) != null)
						{
							amountBalance += _doc.oContract.CurrentBalance(Depot.oAccountings.item((long)2)).AmountBalance;
						}
						if (_doc.oContract.CurrentBalance(Depot.oAccountings.item((long)3)) != null)
						{
							amountBalance1 += _doc.oContract.CurrentBalance(Depot.oAccountings.item((long)3)).AmountBalance;
						}
						if (this.GetStatusLegalDoc(_doc) == "Не определено")
						{
							num4++;
							num7 += Math.Round(_doc.DocumentAmount, 2);
						}
						if (this.GetStatusLegalDoc(_doc) == "Активно")
						{
							num6++;
							num8 += Math.Round(_doc.DocumentAmount, 2);
						}
						if (this.GetStatusLegalDoc(_doc) == "Закрыт")
						{
							num5++;
							num9 += Math.Round(_doc.DocumentAmount, 2);
						}
					}
					catch
					{
						num++;
					}
				}
				this.statusBar1.Panels[0].Text = string.Concat("Сальдо: ", Convert.ToString(Math.Round(num1, 2)));
				this.statusBar1.Panels[1].Text = string.Concat("Сумма : ", Convert.ToString(Math.Round(num3, 2)));
				this.statusBar1.Panels[2].Text = string.Concat("Долг по иску: ", Convert.ToString(Math.Round(amountBalance, 2)));
				this.statusBar1.Panels[3].Text = string.Concat("Долг по ГП: ", Convert.ToString(Math.Round(amountBalance1, 2)));
				string str = "";
				string str1 = "";
				string str2 = "";
				string str3 = "";
				str = string.Concat(Convert.ToString(num4 + num6 + num5), "(", Convert.ToString(num7 + num8 + num9), ")");
				str1 = string.Concat(Convert.ToString(num4), "(", Convert.ToString(num7), ")");
				str2 = string.Concat(Convert.ToString(num6), "(", Convert.ToString(num8), ")");
				str3 = string.Concat(Convert.ToString(num5), "(", Convert.ToString(num9), ")");
				StatusBarPanel item = this.statusBar1.Panels[4];
				string[] strArrays = new string[] { "Исков: ", str, ", НО - ", str1, ", А - ", str2, ", З - ", str3 };
				item.Text = string.Concat(strArrays);
				this.Cursor = Cursors.Default;
				if (num > 0)
				{
					MessageBox.Show(string.Concat("Не загружено ", Convert.ToInt32(num), " иск(-ов) по неизвестной причине. Обратитесь к разработчику."), "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.Cursor = Cursors.Default;
				MessageBox.Show(string.Concat("Ошибка загрузки данных!", exception.Message), "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
					this.lv.SelectedItems[0].SubItems[2].Text = o.oContract.oPerson.oAddress.get_ShortAddress();
					this.lv.SelectedItems[0].SubItems[3].Text = o.oContract.oPerson.FullName;
					this.lv.SelectedItems[0].SubItems[4].Text = Convert.ToString(Math.Round(o.DocumentAmount, 2));
					this.lv.SelectedItems[0].SubItems[5].Text = this.GetStatusLegalDoc(o);
					if (o.oContract.CurrentBalance(Depot.oAccountings.item((long)2)) == null)
					{
						this.lv.SelectedItems[0].SubItems[6].Text = "0";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[6].Text = Convert.ToString(Math.Round(o.oContract.CurrentBalance(Depot.oAccountings.item((long)2)).AmountBalance, 2));
					}
					this.lv.SelectedItems[0].SubItems[7].Text = Convert.ToString(o.GetPD(12).get_Name());
					this.lv.SelectedItems[0].SubItems[8].Text = o.oContract.oGobjects[0].oGRU.oAgent.get_Name();
					this.lv.SelectedItems[0].SubItems[9].Text = o.oContract.oGobjects[0].oStatusGObject.get_Name();
					this.lv.SelectedItems[0].SubItems[10].Text = Convert.ToString(Math.Round(o.oContract.CurrentBalance(), 2));
					if (o.oContract.CurrentBalance(Depot.oAccountings.item((long)3)) == null)
					{
						this.lv.SelectedItems[0].SubItems[11].Text = "0";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[11].Text = Convert.ToString(Math.Round(o.oContract.CurrentBalance(Depot.oAccountings.item((long)3)).AmountBalance, 2));
					}
				}
				else
				{
					DateTime documentDate = o.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = o.get_ID().ToString()
					};
					listViewItem.SubItems.Add(o.oContract.Account);
					listViewItem.SubItems.Add(o.oContract.oPerson.oAddress.get_ShortAddress());
					listViewItem.SubItems.Add(o.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.DocumentAmount, 2)));
					listViewItem.SubItems.Add(this.GetStatusLegalDoc(o));
					if (o.oContract.CurrentBalance(Depot.oAccountings.item((long)2)) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.oContract.CurrentBalance(Depot.oAccountings.item((long)2)).AmountBalance, 2)));
					}
					listViewItem.SubItems.Add(Convert.ToString(o.GetPD(12).get_Name()));
					listViewItem.SubItems.Add(o.oContract.oGobjects[0].oGRU.oAgent.get_Name());
					listViewItem.SubItems.Add(o.oContract.oGobjects[0].oStatusGObject.get_Name());
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.oContract.CurrentBalance(), 2)));
					if (o.oContract.CurrentBalance(Depot.oAccountings.item((long)3)) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.oContract.CurrentBalance(Depot.oAccountings.item((long)3)).AmountBalance, 2)));
					}
					this.lv.Items.Add(listViewItem);
				}
				this.Cursor = Cursors.Default;
			}
			catch (Exception exception)
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void frmLegalDocs_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmLegalDocs_KeyUp(object sender, KeyEventArgs e)
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

		private void frmLegalDocs_Load(object sender, EventArgs e)
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
				Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this.FillList();
			}
			catch
			{
			}
		}

		private string GetStatusLegalDoc(Document oDocument)
		{
			if (oDocument.GetPD(22) == null)
			{
				return "Не определено";
			}
			if (Convert.ToInt64(oDocument.GetPD(22).Value) == (long)0)
			{
				return "Не определено";
			}
			if (Convert.ToInt64(oDocument.GetPD(22).Value) == (long)1)
			{
				return "Активно";
			}
			return "Закрыт";
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmLegalDocs));
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader12 = new ColumnHeader();
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
			this.statusBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel2 = new StatusBarPanel();
			this.statusBarPanel3 = new StatusBarPanel();
			this.statusBarPanel4 = new StatusBarPanel();
			this.statusBarPanel5 = new StatusBarPanel();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel2).BeginInit();
			((ISupportInitialize)this.statusBarPanel3).BeginInit();
			((ISupportInitialize)this.statusBarPanel4).BeginInit();
			((ISupportInitialize)this.statusBarPanel5).BeginInit();
			base.SuspendLayout();
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader6, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader10, this.columnHeader9, this.columnHeader7, this.columnHeader8, this.columnHeader5, this.columnHeader12 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 28);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(1032, 372);
			this.lv.TabIndex = 2;
			this.lv.View = View.Details;
			this.lv.KeyPress += new KeyPressEventHandler(this.lv_KeyPress);
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.lv.SelectedIndexChanged += new EventHandler(this.lv_SelectedIndexChanged);
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "Л/счет";
			this.columnHeader11.Width = 110;
			this.columnHeader6.Text = "Адрес";
			this.columnHeader6.Width = 100;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 110;
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 75;
			this.columnHeader4.Text = "Состояние";
			this.columnHeader4.Width = 110;
			this.columnHeader10.Text = "Долг по иску";
			this.columnHeader10.Width = 80;
			this.columnHeader9.Text = "Сумма иска";
			this.columnHeader9.Width = 100;
			this.columnHeader7.Text = "Контролер";
			this.columnHeader7.Width = 90;
			this.columnHeader8.Text = "Статус ОУ";
			this.columnHeader8.Width = 80;
			this.columnHeader5.Text = "Тек. сальдо";
			this.columnHeader5.Width = 100;
			this.columnHeader12.Text = "Долг по гос.пош.";
			this.columnHeader12.Width = 100;
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
			this.tbData.Size = new System.Drawing.Size(1032, 28);
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
			this.toolBarButton3.ToolTipText = "Печать накладной";
			this.toolBarButton3.Visible = false;
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
			this.statusBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.statusBar1.Location = new Point(0, 409);
			this.statusBar1.Name = "statusBar1";
			StatusBar.StatusBarPanelCollection panels = this.statusBar1.Panels;
			StatusBarPanel[] statusBarPanelArray = new StatusBarPanel[] { this.statusBarPanel1, this.statusBarPanel2, this.statusBarPanel3, this.statusBarPanel4, this.statusBarPanel5 };
			panels.AddRange(statusBarPanelArray);
			this.statusBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(1032, 24);
			this.statusBar1.TabIndex = 4;
			this.statusBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Text = "Итого в кассе:";
			this.statusBarPanel1.Width = 104;
			this.statusBarPanel2.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel2.Text = "По абонентам:";
			this.statusBarPanel2.Width = 107;
			this.statusBarPanel3.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel3.Text = "По агентам:";
			this.statusBarPanel3.Width = 90;
			this.statusBarPanel4.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel4.Text = "statusBarPanel4";
			this.statusBarPanel4.Width = 113;
			this.statusBarPanel5.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel5.Text = "statusBarPanel5";
			this.statusBarPanel5.Width = 113;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(1032, 433);
			base.Controls.Add(this.statusBar1);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmLegalDocs";
			this.Text = "Журнал юридических документов";
			base.Closing += new CancelEventHandler(this.frmLegalDocs_Closing);
			base.Load += new EventHandler(this.frmLegalDocs_Load);
			base.KeyUp += new KeyEventHandler(this.frmLegalDocs_KeyUp);
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel2).EndInit();
			((ISupportInitialize)this.statusBarPanel3).EndInit();
			((ISupportInitialize)this.statusBarPanel4).EndInit();
			((ISupportInitialize)this.statusBarPanel5).EndInit();
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
			string[] shortDateString = new string[] { "Журнал юридических документов (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ") фильтр: ", this.filter };
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
					frmFilterLegalDocs frmFilterLegalDoc = new frmFilterLegalDocs();
					if (frmFilterLegalDoc.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						frmFilterLegalDoc.GetData(ref this.idstatus, ref this.filter);
						this.FillList();
					}
					frmFilterLegalDoc = null;
					return;
				}
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