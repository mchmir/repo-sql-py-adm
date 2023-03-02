using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmActJobs : Form
	{
		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader13;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader10;

		private ToolBarButton tbAdd;

		private ToolBarButton toolBarButton16;

		private MenuItem menuAdd;

		private ImageList imageList1;

		private ToolBarButton toolBarButton3;

		private ToolBar tbData;

		private ToolBarButton toolBarButton23;

		private ToolBarButton toolBarButton25;

		private ToolBarButton toolBarButton26;

		private ToolBarButton toolBarButton27;

		private ToolBarButton toolBarButton31;

		private ToolBarButton toolBarButton1;

		private ToolBarButton toolBarButton2;

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuEdit;

		private MenuItem menuDel;

		private MenuItem menuItem4;

		private MenuItem menuPrint;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private IContainer components;

		private double summa = 0;

		private Documents _documents;

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ColumnHeader columnHeader8;

		private ListViewSortManager m_sortMgr1;

		public frmActJobs()
		{
			this.InitializeComponent();
		}

		private void AddDoc()
		{
			Document document = null;
			document = this._documents.Add();
			if ((new frmActJob(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.FillOneItem(document, true);
			}
			document = null;
			base.Focus();
		}

		private void DeleteDoc()
		{
			try
			{
				if (this.lv.SelectedItems.Count > 0)
				{
					long num = (long)0;
					if (MessageBox.Show("Удалить текущий документ?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						if (this._documents.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).GetPD(28) != null)
						{
							Convert.ToInt64(this._documents.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).GetPD(28).Value);
							num = Convert.ToInt64(this._documents.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).GetPD(7).Value);
						}
						if (this._documents.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).oPeriod.get_ID() == Depot.CurrentPeriod.get_ID())
						{
							double documentAmount = this._documents.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).DocumentAmount;
							if (this._documents.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) != 0)
							{
								MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								this.lv.Items.Remove(this.lv.SelectedItems[0]);
								this.summa -= documentAmount;
								string[] strArrays = new string[2];
								int count = this.lv.Items.Count;
								strArrays[0] = string.Concat("Загружено: ", count.ToString());
								double num1 = Math.Round(this.summa, 3);
								strArrays[1] = string.Concat("литры всего: ", num1.ToString());
								Depot.status = strArrays;
								Gmeter gmeter = new Gmeter();
								gmeter.Load(num);
								gmeter.oStatusGMeter = Depot.oStatusGMeters.item((long)1);
								gmeter.Save();
								gmeter = null;
							}
						}
						else
						{
							MessageBox.Show("Ошибка удаления объекта! Документ за прошлый период!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
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
			Agent agent;
			try
			{
				this.Cursor = Cursors.WaitCursor;
				double documentAmount = 0;
				this.lv.Items.Clear();
				this._documents = new Documents();
				this.RefreshCaption();
				this._documents.Load(Depot.oTypeDocuments.item((long)20), this.DateBegin, this.DateEnd);
				foreach (Document _document in this._documents)
				{
					DateTime documentDate = _document.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = _document.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_document.oContract.Account);
					listViewItem.SubItems.Add(_document.oContract.oPerson.FullName);
					if (_document.GetPD(7) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						Gmeter gmeter = new Gmeter();
						gmeter.Load(Convert.ToInt64(_document.GetPD(7).Value));
						listViewItem.SubItems.Add(string.Concat(gmeter.oTypeGMeter.get_Name(), ", ном.:", gmeter.SerialNumber));
					}
					if (_document.GetPD(16) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						agent = new Agent();
						agent.Load(Convert.ToInt64(_document.GetPD(16).Value));
						listViewItem.SubItems.Add(agent.get_Name());
					}
					if (_document.GetPD(26) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						agent = new Agent();
						agent.Load(Convert.ToInt64(_document.GetPD(26).Value));
						listViewItem.SubItems.Add(agent.get_Name());
					}
					if (_document.GetPD(27) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(_document.GetPD(27).Value);
					}
					if (_document.GetPD(29) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(_document.GetPD(29).Value);
					}
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(_document.DocumentAmount, 2)));
					if (_document.GetPD(28) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else if (Convert.ToInt64(_document.GetPD(28).Value) != (long)1)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						listViewItem.SubItems.Add("да");
					}
					listViewItem.SubItems.Add(_document.Note);
					this.lv.Items.Add(listViewItem);
					documentAmount += _document.DocumentAmount;
				}
				string[] strArrays = new string[2];
				int count = this.lv.Items.Count;
				strArrays[0] = string.Concat("Загружено: ", count.ToString());
				double num = Math.Round(documentAmount, 3);
				strArrays[1] = string.Concat("литры всего: ", num.ToString());
				Depot.status = strArrays;
				this.Cursor = Cursors.Default;
			}
			catch (Exception exception)
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
				if (isAdd)
				{
					DateTime documentDate = o.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = o.get_ID().ToString()
					};
					listViewItem.SubItems.Add(o.oContract.Account);
					listViewItem.SubItems.Add(o.oContract.oPerson.FullName);
					if (o.GetPD(7) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						Gmeter gmeter = new Gmeter();
						gmeter.Load(Convert.ToInt64(o.GetPD(7).Value));
						listViewItem.SubItems.Add(string.Concat(gmeter.oTypeGMeter.get_Name(), ", ном.:", gmeter.SerialNumber));
					}
					if (o.GetPD(16) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						Agent agent = new Agent();
						agent.Load(Convert.ToInt64(o.GetPD(16).Value));
						listViewItem.SubItems.Add(agent.get_Name());
					}
					if (o.GetPD(26) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						Agent agent1 = new Agent();
						agent1.Load(Convert.ToInt64(o.GetPD(26).Value));
						listViewItem.SubItems.Add(agent1.get_Name());
					}
					if (o.GetPD(27) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(o.GetPD(27).Value);
					}
					if (o.GetPD(29) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(o.GetPD(29).Value);
					}
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.DocumentAmount, 2)));
					if (o.GetPD(28) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else if (Convert.ToInt64(o.GetPD(28).Value) != (long)1)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						listViewItem.SubItems.Add("да");
					}
					listViewItem.SubItems.Add(o.Note);
					this.lv.Items.Add(listViewItem);
					this.summa += o.DocumentAmount;
					string[] strArrays = new string[2];
					int count = this.lv.Items.Count;
					strArrays[0] = string.Concat("Загружено: ", count.ToString());
					double num = Math.Round(this.summa, 3);
					strArrays[1] = string.Concat("литры всего: ", num.ToString());
					Depot.status = strArrays;
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

		private void frmActJobs_Closing(object sender, CancelEventArgs e)
		{
			Depot.status = new string[] { "", "" };
			this._documents = null;
			Tools.SaveWindows(this);
		}

		private void frmActJobs_KeyUp(object sender, KeyEventArgs e)
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

		private void frmActJobs_Load(object sender, EventArgs e)
		{
			Tools.LoadWindows(this);
			ListView listView = this.lv;
			Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
			this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
			DateTime today = DateTime.Today;
			DateTime dateTime = DateTime.Today;
			this.DateBegin = today.AddDays((double)(-dateTime.Day + 1));
			today = DateTime.Today;
			int day = -DateTime.Today.Day;
			int year = DateTime.Today.Year;
			dateTime = DateTime.Today;
			this.DateEnd = today.AddDays((double)(day + DateTime.DaysInMonth(year, dateTime.Month)));
			this.FillList();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmActJobs));
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader13 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.tbAdd = new ToolBarButton();
			this.toolBarButton16 = new ToolBarButton();
			this.menuAdd = new MenuItem();
			this.imageList1 = new ImageList(this.components);
			this.toolBarButton3 = new ToolBarButton();
			this.tbData = new ToolBar();
			this.toolBarButton23 = new ToolBarButton();
			this.toolBarButton25 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.toolBarButton27 = new ToolBarButton();
			this.toolBarButton31 = new ToolBarButton();
			this.toolBarButton1 = new ToolBarButton();
			this.toolBarButton2 = new ToolBarButton();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuEdit = new MenuItem();
			this.menuDel = new MenuItem();
			this.menuItem4 = new MenuItem();
			this.menuPrint = new MenuItem();
			base.SuspendLayout();
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader3, this.columnHeader7, this.columnHeader8, this.columnHeader13, this.columnHeader4, this.columnHeader10, this.columnHeader5, this.columnHeader6 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 28);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(832, 404);
			this.lv.TabIndex = 2;
			this.lv.View = View.Details;
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "Л/счет";
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 110;
			this.columnHeader3.Text = "ПУ";
			this.columnHeader3.Width = 75;
			this.columnHeader7.Text = "Слесарь";
			this.columnHeader7.Width = 75;
			this.columnHeader8.Text = "Контролер";
			this.columnHeader8.Width = 75;
			this.columnHeader13.Text = "Нач. пок.";
			this.columnHeader4.Text = "Кон. пок.";
			this.columnHeader10.Text = "Литры";
			this.columnHeader5.Text = "Отключили ПУ";
			this.columnHeader5.Width = 90;
			this.columnHeader6.Text = "Примечание";
			this.columnHeader6.Width = 80;
			this.tbAdd.ImageIndex = 0;
			this.tbAdd.Tag = "Add";
			this.tbAdd.ToolTipText = "Добавить";
			this.toolBarButton16.ImageIndex = 1;
			this.toolBarButton16.Tag = "Edit";
			this.toolBarButton16.ToolTipText = "Редактировать";
			this.toolBarButton16.Visible = false;
			this.menuAdd.Index = 0;
			this.menuAdd.Text = "Добавить";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.toolBarButton3.ImageIndex = 11;
			this.toolBarButton3.Tag = "PrintNakl";
			this.toolBarButton3.ToolTipText = "Печать";
			this.toolBarButton3.Visible = false;
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
			this.tbData.Size = new System.Drawing.Size(832, 28);
			this.tbData.TabIndex = 1;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
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
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.contextMenu1.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuAdd, this.menuEdit, this.menuDel, this.menuItem4, this.menuPrint };
			menuItems.AddRange(menuItemArray);
			this.menuEdit.Index = 1;
			this.menuEdit.Text = "Изменить";
			this.menuDel.Index = 2;
			this.menuDel.Text = "Удалить";
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "-";
			this.menuPrint.Index = 4;
			this.menuPrint.Text = "Печать";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(832, 433);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmActJobs";
			this.Text = "Журнал Актов проверки работы ПУ";
			base.Closing += new CancelEventHandler(this.frmActJobs_Closing);
			base.Load += new EventHandler(this.frmActJobs_Load);
			base.KeyUp += new KeyEventHandler(this.frmActJobs_KeyUp);
			base.ResumeLayout(false);
		}

		private void RefreshCaption()
		{
			string[] shortDateString = new string[] { "Журнал Актов проверки работы ПУ (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ")" };
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
				if ((object)str1 != (object)"Edit")
				{
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
							this._documents = null;
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
}