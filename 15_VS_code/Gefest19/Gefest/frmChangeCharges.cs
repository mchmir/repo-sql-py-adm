using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmChangeCharges : Form
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

		private ImageList imageList1;

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuAdd;

		private MenuItem menuEdit;

		private MenuItem menuDel;

		private MenuItem menuItem4;

		private MenuItem menuPrint;

		private IContainer components;

		private ColumnHeader columnHeader10;

		private ColumnHeader columnHeader12;

		private ColumnHeader columnHeader13;

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private ColumnHeader columnHeader14;

		private ColumnHeader columnHeader15;

		private Documents _docs;

		private double summa = 0;

		private double cub = 0;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private double kg = 0;

		public frmChangeCharges()
		{
			this.InitializeComponent();
		}

		private void AddDoc()
		{
			Document document = null;
			document = this._docs.Add();
			if ((new frmChangeCharge(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
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
				double documentAmount = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).DocumentAmount;
				double factAmount = 0;
				double num = 0;
				if (this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).GetFactUseByType(Depot.oTypeFUs.item((long)1)) != null)
				{
					factAmount = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).GetFactUseByType(Depot.oTypeFUs.item((long)1)).FactAmount;
				}
				if (this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).GetFactUseByType(Depot.oTypeFUs.item((long)2)) != null)
				{
					num = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).GetFactUseByType(Depot.oTypeFUs.item((long)2)).FactAmount;
				}
				if (this._docs.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) == 0)
				{
					this.lv.Items.Remove(this.lv.SelectedItems[0]);
					this.summa -= documentAmount;
					this.cub -= factAmount;
					this.kg -= num;
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
			if (this.lv.SelectedItems.Count > 0)
			{
				Document document = null;
				document = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
				if (document.oPeriod.get_ID() != Depot.CurrentPeriod.get_ID())
				{
					MessageBox.Show("Ошибка редактирования объекта! Документ за прошлый период!", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if ((new frmChangeCharge(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.FillOneItem(document, false);
				}
				document = null;
				base.Focus();
			}
		}

		private void FillList()
		{
			frmLoad _frmLoad = new frmLoad("Загрузка списка ...");
			this.Refresh();
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.Items.Clear();
				this._docs = new Documents();
				this.summa = 0;
				this.cub = 0;
				this.kg = 0;
				this.RefreshCaption();
				this._docs.Load(Depot.oTypeDocuments.item((long)7), this.DateBegin, this.DateEnd);
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
					this.summa += _doc.DocumentAmount;
					if (_doc.GetPD(13) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(_doc.GetPD(13).Value);
					}
					if (_doc.GetPD(1) == null)
					{
						listViewItem.SubItems.Add("0");
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						Indication indication = new Indication();
						indication.Load(Convert.ToInt64(_doc.GetPD(1).Value));
						listViewItem.SubItems.Add(Convert.ToString(indication.Display));
						listViewItem.SubItems.Add(indication.oTypeIndication.get_Name());
					}
					if (_doc.GetFactUseByType(Depot.oTypeFUs.item((long)1)) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.GetFactUseByType(Depot.oTypeFUs.item((long)1)).FactAmount, 3)));
						this.cub += _doc.GetFactUseByType(Depot.oTypeFUs.item((long)1)).FactAmount;
					}
					if (_doc.GetFactUseByType(Depot.oTypeFUs.item((long)2)) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.GetFactUseByType(Depot.oTypeFUs.item((long)2)).FactAmount, 4)));
						this.kg += _doc.GetFactUseByType(Depot.oTypeFUs.item((long)2)).FactAmount;
					}
					if (_doc.GetPD(34) == null)
					{
						listViewItem.SubItems.Add("");
					}
					else
					{
						TypeCharge typeCharge = new TypeCharge();
						typeCharge.Load(Convert.ToInt64(_doc.GetPD(34).Value));
						listViewItem.SubItems.Add(typeCharge.get_Name());
					}
					listViewItem.SubItems.Add(_doc.oOperations[0].oBalance.oAccounting.get_Name());
					listViewItem.SubItems.Add(_doc.Note);
					if (_doc.oContract.oPerson.isJuridical != 1)
					{
						listViewItem.SubItems.Add("Физическое лицо");
					}
					else
					{
						listViewItem.SubItems.Add("Юридическое лицо");
					}
					this.lv.Items.Add(listViewItem);
				}
				this.FillStatusBar();
				this.Cursor = Cursors.Default;
				_frmLoad.Close();
				_frmLoad = null;
				this.Refresh();
			}
			catch
			{
				if (_frmLoad != null)
				{
					_frmLoad.Close();
				}
				_frmLoad = null;
				this.Refresh();
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
					if (o.GetPD(13) == null)
					{
						this.lv.SelectedItems[0].SubItems[4].Text = "0";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[4].Text = o.GetPD(13).Value;
					}
					if (o.GetPD(1) == null)
					{
						this.lv.SelectedItems[0].SubItems[5].Text = "0";
						this.lv.SelectedItems[0].SubItems[6].Text = "нет";
					}
					else
					{
						Indication indication = new Indication();
						indication.Load(Convert.ToInt64(o.GetPD(1).Value));
						this.lv.SelectedItems[0].SubItems[5].Text = Convert.ToString(indication.Display);
						this.lv.SelectedItems[0].SubItems[6].Text = indication.oTypeIndication.get_Name();
					}
					if (o.GetFactUseByType(Depot.oTypeFUs.item((long)1)) == null)
					{
						this.lv.SelectedItems[0].SubItems[7].Text = "0";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[7].Text = Convert.ToString(Math.Round(o.GetFactUseByType(Depot.oTypeFUs.item((long)1)).FactAmount, 2));
					}
					if (o.GetPD(34) == null)
					{
						this.lv.SelectedItems[0].SubItems[8].Text = "";
					}
					else
					{
						TypeCharge typeCharge = new TypeCharge();
						typeCharge.Load(Convert.ToInt64(o.GetPD(34).Value));
						this.lv.SelectedItems[0].SubItems[8].Text = typeCharge.get_Name();
					}
					if (o.GetFactUseByType(Depot.oTypeFUs.item((long)2)) == null)
					{
						this.lv.SelectedItems[0].SubItems[9].Text = "0";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[9].Text = Convert.ToString(Math.Round(o.GetFactUseByType(Depot.oTypeFUs.item((long)2)).FactAmount, 3));
					}
					this.lv.SelectedItems[0].SubItems[10].Text = o.oOperations[0].oBalance.oAccounting.get_Name();
					this.lv.SelectedItems[0].SubItems[11].Text = o.Note;
					if (o.oContract.oPerson.isJuridical != 1)
					{
						this.lv.SelectedItems[0].SubItems[12].Text = "Физическое лицо";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[12].Text = "Юридическое лицо";
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
					listViewItem.SubItems.Add(o.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.DocumentAmount, 2)));
					this.summa += o.DocumentAmount;
					if (o.GetPD(13) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(o.GetPD(13).Value);
					}
					if (o.GetPD(1) == null)
					{
						listViewItem.SubItems.Add("0");
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						Indication indication1 = new Indication();
						indication1.Load(Convert.ToInt64(o.GetPD(1).Value));
						listViewItem.SubItems.Add(Convert.ToString(indication1.Display));
						listViewItem.SubItems.Add(indication1.oTypeIndication.get_Name());
					}
					if (o.GetFactUseByType(Depot.oTypeFUs.item((long)1)) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.GetFactUseByType(Depot.oTypeFUs.item((long)1)).FactAmount, 2)));
						this.cub += o.GetFactUseByType(Depot.oTypeFUs.item((long)1)).FactAmount;
					}
					if (o.GetFactUseByType(Depot.oTypeFUs.item((long)2)) == null)
					{
						listViewItem.SubItems.Add("0");
					}
					else
					{
						listViewItem.SubItems.Add(Convert.ToString(Math.Round(o.GetFactUseByType(Depot.oTypeFUs.item((long)2)).FactAmount, 4)));
						this.kg += o.GetFactUseByType(Depot.oTypeFUs.item((long)2)).FactAmount;
					}
					if (o.GetPD(34) == null)
					{
						listViewItem.SubItems.Add("");
					}
					else
					{
						TypeCharge typeCharge1 = new TypeCharge();
						typeCharge1.Load(Convert.ToInt64(o.GetPD(34).Value));
						listViewItem.SubItems.Add(typeCharge1.get_Name());
					}
					listViewItem.SubItems.Add(o.oOperations[0].oBalance.oAccounting.get_Name());
					listViewItem.SubItems.Add(o.Note);
					if (o.oContract.oPerson.isJuridical != 1)
					{
						listViewItem.SubItems.Add("Физическое лицо");
					}
					else
					{
						listViewItem.SubItems.Add("Юридическое лицо");
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

		private void FillStatusBar()
		{
			try
			{
				string[] strArrays = new string[2];
				int count = this.lv.Items.Count;
				strArrays[0] = string.Concat("Загружено: ", count.ToString());
				string[] str = new string[] { "На сумму: ", this.summa.ToString("#.##"), ", Сумма кубов: ", this.cub.ToString("#.###"), ", Сумма кг.: ", this.kg.ToString("#.####") };
				strArrays[1] = string.Concat(str);
				Depot.status = strArrays;
			}
			catch
			{
			}
		}

		private void frmChangeCharges_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
			Depot.status = new string[] { "", "" };
		}

		private void frmChangeCharges_KeyUp(object sender, KeyEventArgs e)
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

		private void frmChangeCharges_Load(object sender, EventArgs e)
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
				Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmChangeCharges));
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader13 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader14 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader12 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader15 = new ColumnHeader();
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
			base.SuspendLayout();
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader3, this.columnHeader13, this.columnHeader4, this.columnHeader14, this.columnHeader10, this.columnHeader12, this.columnHeader7, this.columnHeader6, this.columnHeader15, this.columnHeader5 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 28);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(792, 404);
			this.lv.TabIndex = 4;
			this.lv.View = View.Details;
			this.lv.KeyPress += new KeyPressEventHandler(this.lv_KeyPress);
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "Л/счет";
			this.columnHeader11.Width = 75;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 110;
			this.columnHeader3.Text = "Сумма";
			this.columnHeader3.Width = 75;
			this.columnHeader13.Text = "Тариф";
			this.columnHeader13.Width = 75;
			this.columnHeader4.Text = "нов. пок.";
			this.columnHeader4.Width = 75;
			this.columnHeader14.Text = "Тип пок.";
			this.columnHeader14.Width = 75;
			this.columnHeader10.Text = "кубы";
			this.columnHeader10.Width = 75;
			this.columnHeader12.Text = "кг./м3";
			this.columnHeader12.Width = 75;
			this.columnHeader7.Text = "Тип корректировки";
			this.columnHeader6.Text = "Вид сальдо";
			this.columnHeader6.Width = 80;
			this.columnHeader15.Text = "Примечание";
			this.columnHeader15.Width = 100;
			this.columnHeader5.Text = "Статус контракта";
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
			this.tbData.TabIndex = 3;
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
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(792, 433);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmChangeCharges";
			this.Text = "Журнал корректировок";
			base.Closing += new CancelEventHandler(this.frmChangeCharges_Closing);
			base.Load += new EventHandler(this.frmChangeCharges_Load);
			base.KeyUp += new KeyEventHandler(this.frmChangeCharges_KeyUp);
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

		private void RefreshCaption()
		{
			string[] shortDateString = new string[] { "Журнал корректировок (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ")" };
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