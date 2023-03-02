using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPersonalCabinetRequests : Form
	{
		private ToolBar tbData;

		private ToolBarButton toolBarButton26;

		private ToolBarButton toolBarButton27;

		private ToolBarButton toolBarButton31;

		private ToolBarButton toolBarButton2;

		private ImageList imageList1;

		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private IContainer components;

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private PersonalCabinetRequests _requests;

		public frmPersonalCabinetRequests()
		{
			this.InitializeComponent();
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
				PersonalCabinetRequest personalCabinetRequest = null;
				personalCabinetRequest = this._requests.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
				if (personalCabinetRequest.State == 4)
				{
					MessageBox.Show("Заявка уже завершена!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if ((new frmPersonalCabinetRequest(personalCabinetRequest)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.FillOneItem(personalCabinetRequest);
				}
				personalCabinetRequest = null;
				base.Focus();
			}
		}

		private void FillList()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.Items.Clear();
				this._requests = new PersonalCabinetRequests();
				this.RefreshCaption();
				this._requests.Load(this.DateBegin, this.DateEnd);
				foreach (PersonalCabinetRequest _request in this._requests)
				{
					DateTime dateRequest = _request.DateRequest;
					ListViewItem listViewItem = new ListViewItem(dateRequest.ToShortDateString())
					{
						Tag = _request.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_request.Account);
					listViewItem.SubItems.Add(_request.FIO);
					listViewItem.SubItems.Add(_request.AdresHome);
					listViewItem.SubItems.Add(_request.Phone);
					listViewItem.SubItems.Add(_request.Email);
					listViewItem.SubItems.Add(this.GetStatus(_request));
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

		private void FillOneItem(PersonalCabinetRequest o)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.RefreshCaption();
				this.lv.SelectedItems[0].SubItems[6].Text = this.GetStatus(o);
				this._requests.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).State = o.State;
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void frmPersonalCabinetRequests_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmPersonalCabinetRequests_Load(object sender, EventArgs e)
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
				Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this.FillList();
			}
			catch
			{
			}
		}

		private string GetStatus(PersonalCabinetRequest o)
		{
			switch (o.State)
			{
				case 1:
				{
					return "Оформлена";
				}
				case 2:
				{
					return "Принята (у/л)";
				}
				case 3:
				{
					return "Принята (все документы)";
				}
				case 4:
				{
					return "Завершена";
				}
			}
			return "Оформлена";
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmPersonalCabinetRequests));
			this.tbData = new ToolBar();
			this.toolBarButton31 = new ToolBarButton();
			this.toolBarButton2 = new ToolBarButton();
			this.toolBarButton27 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.imageList1 = new ImageList(this.components);
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			base.SuspendLayout();
			this.tbData.Appearance = ToolBarAppearance.Flat;
			this.tbData.AutoSize = false;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.toolBarButton31, this.toolBarButton2, this.toolBarButton27, this.toolBarButton26 };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.DropDownArrows = true;
			this.tbData.ImageList = this.imageList1;
			this.tbData.Location = new Point(0, 0);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(920, 28);
			this.tbData.TabIndex = 2;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.toolBarButton31.ImageIndex = 4;
			this.toolBarButton31.Tag = "Depth";
			this.toolBarButton31.ToolTipText = "Интервал журнала";
			this.toolBarButton2.ImageIndex = 10;
			this.toolBarButton2.Tag = "Refresh";
			this.toolBarButton2.ToolTipText = "Обновить";
			this.toolBarButton27.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton26.ImageIndex = 3;
			this.toolBarButton26.Tag = "Excel";
			this.toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader6, this.columnHeader3, this.columnHeader5, this.columnHeader4 };
			columns.AddRange(columnHeaderArray);
			this.lv.Dock = DockStyle.Fill;
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 28);
			this.lv.MultiSelect = false;
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(920, 402);
			this.lv.TabIndex = 3;
			this.lv.View = View.Details;
			this.lv.KeyPress += new KeyPressEventHandler(this.lv_KeyPress);
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "Л/счет";
			this.columnHeader11.Width = 110;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 110;
			this.columnHeader6.Text = "Адрес";
			this.columnHeader6.Width = 80;
			this.columnHeader3.Text = "Телефон";
			this.columnHeader3.Width = 75;
			this.columnHeader5.Text = "E-mail";
			this.columnHeader5.Width = 80;
			this.columnHeader4.Text = "Состояние";
			this.columnHeader4.Width = 110;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(920, 430);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.Name = "frmPersonalCabinetRequests";
			this.Text = "Заявки на доступ в Личный Кабинет Абонента";
			base.Closing += new CancelEventHandler(this.frmPersonalCabinetRequests_Closing);
			base.Load += new EventHandler(this.frmPersonalCabinetRequests_Load);
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

		private void RefreshCaption()
		{
			string[] shortDateString = new string[] { "Заявки на доступ в Личный Кабинет Абонента (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ")" };
			this.Text = string.Concat(shortDateString);
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			"Refresh";
			string str = e.Button.Tag.ToString();
			string str1 = str;
			if (str != null)
			{
				str1 = string.IsInterned(str1);
				if ((object)str1 == (object)"Depth")
				{
					frmDepthView _frmDepthView = new frmDepthView();
					_frmDepthView.SetDate(this.DateBegin, this.DateEnd);
					if (_frmDepthView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						_frmDepthView.GetDate(ref this.DateBegin, ref this.DateEnd);
						this._requests = null;
						this.FillList();
					}
					_frmDepthView = null;
					return;
				}
				if ((object)str1 == (object)"Excel")
				{
					Tools.ConvertToExcel(this.lv);
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