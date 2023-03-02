using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmGMeterPlombHistory : Form
	{
		private ListView lvHistory;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private Button button1;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private System.ComponentModel.Container components = null;

		public frmGMeterPlombHistory(long IDGmeter)
		{
			this.InitializeComponent();
			this.ShowHistory(IDGmeter);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.lvHistory = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.button1 = new Button();
			this.columnHeader6 = new ColumnHeader();
			base.SuspendLayout();
			ListView.ColumnHeaderCollection columns = this.lvHistory.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6 };
			columns.AddRange(columnHeaderArray);
			this.lvHistory.GridLines = true;
			this.lvHistory.Location = new Point(8, 8);
			this.lvHistory.Name = "lvHistory";
			this.lvHistory.Size = new System.Drawing.Size(560, 272);
			this.lvHistory.TabIndex = 0;
			this.lvHistory.View = View.Details;
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 77;
			this.columnHeader2.Text = "Пломба №1";
			this.columnHeader2.Width = 75;
			this.columnHeader3.Text = "Пломба №2";
			this.columnHeader3.Width = 78;
			this.columnHeader4.Text = "Показания";
			this.columnHeader4.Width = 74;
			this.columnHeader5.Text = "Исполнитель";
			this.columnHeader5.Width = 150;
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new Point(496, 288);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "ОК";
			this.columnHeader6.Text = "Примечание";
			this.columnHeader6.Width = 200;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(576, 318);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.lvHistory);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "frmGMeterPlombHistory";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "История опломбировки ПУ";
			base.ResumeLayout(false);
		}

		protected void ShowHistory(long idgmeter)
		{
			Agents agent = new Agents();
			agent.Load(Depot.oTypeAgents.item((long)5));
			GmeterPlombHistorys gmeterPlombHistory = new GmeterPlombHistorys();
			gmeterPlombHistory.Load(idgmeter);
			ListViewItem listViewItem = new ListViewItem();
			this.lvHistory.Items.Clear();
			foreach (GmeterPlombHistory gmeterPlombHistory1 in gmeterPlombHistory)
			{
				listViewItem = new ListViewItem(gmeterPlombHistory1.DatePlomb.ToShortDateString());
				listViewItem.SubItems.Add(gmeterPlombHistory1.PlombNumber1.ToString());
				listViewItem.SubItems.Add(gmeterPlombHistory1.PlombNumber2.ToString());
				listViewItem.SubItems.Add(gmeterPlombHistory1.IndicationPlomb.ToString());
				listViewItem.SubItems.Add(agent.item(Convert.ToInt64(gmeterPlombHistory1.IDAgentPlomb)).get_Name());
				listViewItem.SubItems.Add(gmeterPlombHistory1.PlombMemo.ToString());
				this.lvHistory.Items.Add(listViewItem);
			}
		}
	}
}