using C1.Win.C1Input;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmCreateTransport : Form
	{
		private C1DateEdit dtBegin;

		private Label label1;

		private Button button1;

		private Button button2;

		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private StatusBar statusBar1;

		private Period _per;

		private StatusBarPanel statusBarPanel1;

		private System.ComponentModel.Container components = null;

		public frmCreateTransport()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this._per != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Filter = "Текстовый файл (*.txt)|*.txt",
					InitialDirectory = "C:\\"
				};
				DateTime dateEnd = this._per.DateEnd;
				saveFileDialog.FileName = string.Concat(dateEnd.ToString("yyyyMMdd"), "Real.txt");
				if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					string fileName = saveFileDialog.FileName;
					saveFileDialog = null;
					StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.GetEncoding(1251));
					foreach (ListViewItem item in this.lv.Items)
					{
						string[] text = new string[] { item.Text, ";", item.SubItems[1].Text, ";", item.SubItems[2].Text, ";", item.SubItems[3].Text };
						streamWriter.WriteLine(string.Concat(text));
					}
					streamWriter.Close();
					MessageBox.Show("Экспорт успешен");
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				this._per = null;
				double num = 0;
				double num1 = 0;
				int num2 = 0;
				DataTable dataTable = new DataTable();
				Periods period = new Periods();
				this.lv.Items.Clear();
				period.Load((DateTime)this.dtBegin.Value);
				if (period.get_Count() > 0)
				{
					this._per = period[0];
					long d = this._per.get_ID();
					Loader.LoadCollection(string.Concat("exec spCreateTransport ", d.ToString()), ref dataTable, "");
				}
				foreach (DataRow row in dataTable.Rows)
				{
					ListViewItem itemArray = this.lv.Items.Add(row.ItemArray[1].ToString());
					itemArray.Tag = row.ItemArray[0];
					itemArray.SubItems.Add(row.ItemArray[2].ToString());
					itemArray.SubItems.Add(row.ItemArray[3].ToString());
					ListViewItem.ListViewSubItemCollection subItems = itemArray.SubItems;
					DateTime dateTime = (DateTime)row.ItemArray[4];
					subItems.Add(dateTime.ToString("yyyy-MM-dd"));
					num2++;
					num += Convert.ToDouble(row.ItemArray[2]);
					num1 += Convert.ToDouble(row.ItemArray[3]);
				}
				StatusBarPanel item = this.statusBar1.Panels[0];
				string[] str = new string[] { "Всего: ", num2.ToString(), " шт., ", num.ToString(), " кг, ", num1.ToString(), " м3." };
				item.Text = string.Concat(str);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка формирования /n", exception.Message));
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

		private void frmCreateTransport_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmCreateTransport_Load(object sender, EventArgs e)
		{
			C1DateEdit c1DateEdit = this.dtBegin;
			DateTime dateBegin = Depot.CurrentPeriod.DateBegin;
			c1DateEdit.Value = dateBegin.AddDays(-1);
		}

		private void InitializeComponent()
		{
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			this.button1 = new Button();
			this.button2 = new Button();
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.statusBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			((ISupportInitialize)this.dtBegin).BeginInit();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			base.SuspendLayout();
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.FormatType = FormatTypeEnum.YearAndMonth;
			this.dtBegin.Location = new Point(64, 8);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(192, 18);
			this.dtBegin.TabIndex = 40;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 41;
			this.label1.Text = "Период:";
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Location = new Point(368, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 24);
			this.button1.TabIndex = 43;
			this.button1.Text = "Экспорт";
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Location = new Point(264, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 24);
			this.button2.TabIndex = 42;
			this.button2.Text = "Сформировать";
			this.button2.Click += new EventHandler(this.button2_Click);
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader11, this.columnHeader2, this.columnHeader3 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 40);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(480, 344);
			this.lv.TabIndex = 44;
			this.lv.View = View.Details;
			this.columnHeader1.Text = "Инв. номер";
			this.columnHeader1.Width = 93;
			this.columnHeader11.Text = "Кг";
			this.columnHeader11.Width = 110;
			this.columnHeader2.Text = "м3";
			this.columnHeader2.Width = 110;
			this.columnHeader3.Text = "Дата документа";
			this.columnHeader3.Width = 130;
			this.statusBar1.Location = new Point(0, 383);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new StatusBarPanel[] { this.statusBarPanel1 });
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(480, 22);
			this.statusBar1.TabIndex = 45;
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel1.Width = 464;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(480, 405);
			base.Controls.Add(this.statusBar1);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.dtBegin);
			base.Controls.Add(this.label1);
			base.Name = "frmCreateTransport";
			this.Text = "Экспорт потребления в транспортный файл для сбыта";
			base.Closing += new CancelEventHandler(this.frmCreateTransport_Closing);
			base.Load += new EventHandler(this.frmCreateTransport_Load);
			((ISupportInitialize)this.dtBegin).EndInit();
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			base.ResumeLayout(false);
		}
	}
}