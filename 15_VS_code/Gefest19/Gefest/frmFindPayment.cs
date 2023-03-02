using C1.Win.C1Input;
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
	public class frmFindPayment : Form
	{
		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private Button cmdFind;

		private Label label6;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private C1DateEdit dtDate1;

		private C1DateEdit dtDate2;

		private Label label1;

		private NumericUpDown numAmount;

		private Label label2;

		private GroupBox groupBox1;

		private ToolBar tbData;

		private ToolBarButton toolBarButton16;

		private ToolBarButton toolBarButton25;

		private ToolBarButton toolBarButton26;

		private ToolBarButton toolBarButton27;

		private ToolBarButton toolBarButton3;

		private ImageList imageList1;

		private RadioButton rButton1;

		private RadioButton rButton2;

		private RadioButton rButton3;

		private IContainer components;

		private ListViewSortManager m_sortMgr1;

		private int choice;

		private ToolBarButton toolBarButton1;

		private Documents _docs;

		public frmFindPayment()
		{
			this.InitializeComponent();
		}

		private void cmdFind_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.Items.Clear();
				this._docs = new Documents();
				this._docs.Load(Depot.oTypeDocuments.item((long)1), (DateTime)this.dtDate1.Value, (DateTime)this.dtDate2.Value, Convert.ToDouble(this.numAmount.Value), this.choice);
				foreach (Document _doc in this._docs)
				{
					ListViewItem listViewItem = new ListViewItem(_doc.oContract.Account)
					{
						Tag = _doc.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_doc.oContract.oPerson.FullName);
					listViewItem.SubItems.Add(Convert.ToString(Math.Round(_doc.DocumentAmount, 2)));
					listViewItem.SubItems.Add(_doc.DocumentDate.ToShortDateString());
					listViewItem.SubItems.Add(_doc.oBatch.oTypeBatch.get_Name());
					listViewItem.SubItems.Add(_doc.oBatch.NumberBatch);
					if (_doc.oBatch.oCashier == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						listViewItem.SubItems.Add(_doc.oBatch.oCashier.get_Name());
					}
					if (_doc.oBatch.oDispatcher == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						listViewItem.SubItems.Add(_doc.oBatch.oDispatcher.get_Name());
					}
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void dtDate1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDate2.Focus();
			}
		}

		private void dtDate2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numAmount.Focus();
			}
		}

		private void EditBatch()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				Batch batch = null;
				batch = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).oBatch;
				if (batch.oTypeBatch.get_ID() == (long)2)
				{
					MessageBox.Show("В этом режиме нельзя открывать кассовую смену", "Открытие пачки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				frmBatch _frmBatch = new frmBatch(batch, false, new FprnM45Class());
				_frmBatch.ShowDialog(this);
				_frmBatch = null;
				batch = null;
				base.Focus();
			}
		}

		private void EditDoc()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				Document document = null;
				document = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
				if (document.oBatch.oStatusBatch.get_ID() == (long)2)
				{
					MessageBox.Show("Нельзя править платеж из ранее закрытой пачки!", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					document = null;
					return;
				}
				if (document.oBatch.oTypeBatch.get_ID() == (long)2)
				{
					MessageBox.Show("В этом режиме нельзя редактировать платеж кассовой смены", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if ((new frmPaymentDoc(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.FillOneItem(document, false);
				}
				document = null;
				base.Focus();
			}
		}

		private void FillOneItem(Document o, bool isAdd)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (!isAdd)
				{
					this.lv.SelectedItems[0].SubItems[0].Text = o.oContract.Account;
					this.lv.SelectedItems[0].SubItems[1].Text = o.oContract.oPerson.FullName;
					this.lv.SelectedItems[0].SubItems[2].Text = Convert.ToString(Math.Round(o.DocumentAmount, 2));
					this.lv.SelectedItems[0].SubItems[3].Text = o.DocumentDate.ToShortDateString();
					this.lv.SelectedItems[0].SubItems[4].Text = o.oBatch.oTypeBatch.get_Name();
					this.lv.SelectedItems[0].SubItems[5].Text = o.oBatch.NumberBatch;
					if (o.oBatch.oCashier == null)
					{
						this.lv.SelectedItems[0].SubItems[6].Text = "нет";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[6].Text = o.oBatch.oCashier.get_Name();
					}
					if (o.oBatch.oDispatcher == null)
					{
						this.lv.SelectedItems[0].SubItems[7].Text = "нет";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[7].Text = o.oBatch.oDispatcher.get_Name();
					}
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

		private void frmFindPayment_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmFindPayment_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				this.rButton3.Checked = true;
				C1DateEdit c1DateEdit = this.dtDate1;
				DateTime today = DateTime.Today;
				DateTime dateTime = DateTime.Today;
				c1DateEdit.Value = today.AddDays((double)(-dateTime.Day + 1));
				C1DateEdit c1DateEdit1 = this.dtDate2;
				today = DateTime.Today;
				int day = -DateTime.Today.Day;
				int year = DateTime.Today.Year;
				dateTime = DateTime.Today;
				c1DateEdit1.Value = today.AddDays((double)(day + DateTime.DaysInMonth(year, dateTime.Month)));
				ListView listView = this.lv;
				Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmFindPayment));
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.cmdFind = new Button();
			this.dtDate1 = new C1DateEdit();
			this.label6 = new Label();
			this.dtDate2 = new C1DateEdit();
			this.label1 = new Label();
			this.numAmount = new NumericUpDown();
			this.label2 = new Label();
			this.groupBox1 = new GroupBox();
			this.rButton3 = new RadioButton();
			this.rButton2 = new RadioButton();
			this.rButton1 = new RadioButton();
			this.tbData = new ToolBar();
			this.toolBarButton16 = new ToolBarButton();
			this.toolBarButton1 = new ToolBarButton();
			this.toolBarButton25 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.toolBarButton27 = new ToolBarButton();
			this.toolBarButton3 = new ToolBarButton();
			this.imageList1 = new ImageList(this.components);
			((ISupportInitialize)this.dtDate1).BeginInit();
			((ISupportInitialize)this.dtDate2).BeginInit();
			((ISupportInitialize)this.numAmount).BeginInit();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6, this.columnHeader7 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 120);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(792, 312);
			this.lv.TabIndex = 7;
			this.lv.View = View.Details;
			this.lv.KeyPress += new KeyPressEventHandler(this.lv_KeyPress);
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.columnHeader1.Text = "Л/счет";
			this.columnHeader1.Width = 75;
			this.columnHeader11.Text = "ФИО";
			this.columnHeader11.Width = 150;
			this.columnHeader2.Text = "Сумма";
			this.columnHeader2.Width = 75;
			this.columnHeader3.Text = "Дата";
			this.columnHeader3.Width = 75;
			this.columnHeader4.Text = "Тип пачки";
			this.columnHeader4.Width = 100;
			this.columnHeader5.Text = "Номер пачки";
			this.columnHeader5.Width = 100;
			this.columnHeader6.Text = "Кассир";
			this.columnHeader6.Width = 100;
			this.columnHeader7.Text = "Агент";
			this.columnHeader7.Width = 100;
			this.cmdFind.FlatStyle = FlatStyle.Flat;
			this.cmdFind.Location = new Point(504, 56);
			this.cmdFind.Name = "cmdFind";
			this.cmdFind.Size = new System.Drawing.Size(104, 24);
			this.cmdFind.TabIndex = 5;
			this.cmdFind.Text = "Найти";
			this.cmdFind.Click += new EventHandler(this.cmdFind_Click);
			this.dtDate1.BorderStyle = 1;
			this.dtDate1.FormatType = FormatTypeEnum.LongDate;
			this.dtDate1.Location = new Point(32, 8);
			this.dtDate1.Name = "dtDate1";
			this.dtDate1.Size = new System.Drawing.Size(152, 18);
			this.dtDate1.TabIndex = 1;
			this.dtDate1.Tag = null;
			this.dtDate1.Value = new DateTime(2006, 11, 15, 0, 0, 0, 0);
			this.dtDate1.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate1.KeyPress += new KeyPressEventHandler(this.dtDate1_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(24, 16);
			this.label6.TabIndex = 82;
			this.label6.Text = "С";
			this.dtDate2.BorderStyle = 1;
			this.dtDate2.FormatType = FormatTypeEnum.LongDate;
			this.dtDate2.Location = new Point(232, 8);
			this.dtDate2.Name = "dtDate2";
			this.dtDate2.Size = new System.Drawing.Size(152, 18);
			this.dtDate2.TabIndex = 2;
			this.dtDate2.Tag = null;
			this.dtDate2.Value = new DateTime(2006, 11, 15, 0, 0, 0, 0);
			this.dtDate2.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate2.KeyPress += new KeyPressEventHandler(this.dtDate2_KeyPress);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(200, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 16);
			this.label1.TabIndex = 86;
			this.label1.Text = "по";
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(64, 32);
			NumericUpDown num = this.numAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.TabIndex = 3;
			this.numAmount.KeyPress += new KeyPressEventHandler(this.numAmount_KeyPress);
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 88;
			this.label2.Text = "Сумма";
			this.groupBox1.Controls.Add(this.rButton3);
			this.groupBox1.Controls.Add(this.rButton2);
			this.groupBox1.Controls.Add(this.rButton1);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(392, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(104, 72);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Вид";
			this.rButton3.FlatStyle = FlatStyle.Flat;
			this.rButton3.ForeColor = SystemColors.ControlText;
			this.rButton3.Location = new Point(8, 48);
			this.rButton3.Name = "rButton3";
			this.rButton3.Size = new System.Drawing.Size(88, 16);
			this.rButton3.TabIndex = 2;
			this.rButton3.Text = "Все";
			this.rButton3.CheckedChanged += new EventHandler(this.rButton3_CheckedChanged);
			this.rButton2.FlatStyle = FlatStyle.Flat;
			this.rButton2.ForeColor = SystemColors.ControlText;
			this.rButton2.Location = new Point(8, 32);
			this.rButton2.Name = "rButton2";
			this.rButton2.Size = new System.Drawing.Size(88, 16);
			this.rButton2.TabIndex = 1;
			this.rButton2.Text = "Агент";
			this.rButton2.CheckedChanged += new EventHandler(this.rButton2_CheckedChanged);
			this.rButton1.FlatStyle = FlatStyle.Flat;
			this.rButton1.ForeColor = SystemColors.ControlText;
			this.rButton1.Location = new Point(8, 16);
			this.rButton1.Name = "rButton1";
			this.rButton1.Size = new System.Drawing.Size(88, 16);
			this.rButton1.TabIndex = 0;
			this.rButton1.Text = "Касса";
			this.rButton1.CheckedChanged += new EventHandler(this.rButton1_CheckedChanged);
			this.tbData.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tbData.Appearance = ToolBarAppearance.Flat;
			this.tbData.AutoSize = false;
			this.tbData.BorderStyle = BorderStyle.FixedSingle;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.toolBarButton16, this.toolBarButton1, this.toolBarButton25, this.toolBarButton26, this.toolBarButton27, this.toolBarButton3 };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.Divider = false;
			this.tbData.Dock = DockStyle.None;
			this.tbData.DropDownArrows = true;
			this.tbData.ImageList = this.imageList1;
			this.tbData.Location = new Point(0, 90);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(792, 28);
			this.tbData.TabIndex = 6;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.toolBarButton16.ImageIndex = 1;
			this.toolBarButton16.Tag = "Edit";
			this.toolBarButton16.ToolTipText = "Редактировать";
			this.toolBarButton1.ImageIndex = 13;
			this.toolBarButton1.Tag = "Batch";
			this.toolBarButton1.ToolTipText = "Открыть пачку платежа";
			this.toolBarButton25.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton26.ImageIndex = 3;
			this.toolBarButton26.Tag = "Excel";
			this.toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.toolBarButton27.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton3.ImageIndex = 10;
			this.toolBarButton3.Tag = "PrintNakl";
			this.toolBarButton3.ToolTipText = "Печать накладной";
			this.toolBarButton3.Visible = false;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(794, 435);
			base.Controls.Add(this.tbData);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.numAmount);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.dtDate2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.cmdFind);
			base.Controls.Add(this.dtDate1);
			base.Controls.Add(this.label6);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmFindPayment";
			this.Text = "Поиск платежей";
			base.Closing += new CancelEventHandler(this.frmFindPayment_Closing);
			base.Load += new EventHandler(this.frmFindPayment_Load);
			((ISupportInitialize)this.dtDate1).EndInit();
			((ISupportInitialize)this.dtDate2).EndInit();
			((ISupportInitialize)this.numAmount).EndInit();
			this.groupBox1.ResumeLayout(false);
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

		private void numAmount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmount);
		}

		private void numAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdFind.Focus();
			}
		}

		private void rButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.choice = 0;
		}

		private void rButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.choice = 1;
		}

		private void rButton3_CheckedChanged(object sender, EventArgs e)
		{
			this.choice = 2;
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			"PrintNakl";
			string str = e.Button.Tag.ToString();
			string str1 = str;
			if (str != null)
			{
				str1 = string.IsInterned(str1);
				if ((object)str1 == (object)"Edit")
				{
					this.EditDoc();
					return;
				}
				if ((object)str1 == (object)"Batch")
				{
					this.EditBatch();
					return;
				}
				if ((object)str1 == (object)"Excel")
				{
					Tools.ConvertToExcel(this.lv);
				}
				else if ((object)str1 != (object)"PrintNakl")
				{
					return;
				}
			}
		}
	}
}