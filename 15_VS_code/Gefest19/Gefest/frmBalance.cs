using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmBalance : Form
	{
		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private Contract _contract;

		private ListView lvBalance;

		private System.ComponentModel.Container components = null;

		public frmBalance(Contract oContract)
		{
			this.InitializeComponent();
			this._contract = oContract;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
			this._contract = null;
		}

		private void frmBalance_Closing(object sender, CancelEventArgs e)
		{
			this._contract = null;
		}

		private void frmBalance_Load(object sender, EventArgs e)
		{
			double amountBalance;
			if (this._contract == null)
			{
				base.Close();
			}
			foreach (Accounting oAccounting in Depot.oAccountings)
			{
				ListViewItem listViewItem = this.lvBalance.Items.Add(oAccounting.get_Name());
				if (this._contract.CurrentBalance(oAccounting) != null)
				{
					ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
					amountBalance = this._contract.CurrentBalance(oAccounting).AmountBalance;
					subItems.Add(amountBalance.ToString());
				}
				else
				{
					listViewItem.SubItems.Add("0");
				}
				if (this._contract.CurrentBalanceReal(oAccounting) != null)
				{
					ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
					amountBalance = this._contract.CurrentBalanceReal(oAccounting).AmountBalance;
					listViewSubItemCollections.Add(amountBalance.ToString());
				}
				else
				{
					listViewItem.SubItems.Add("0");
				}
			}
		}

		private void InitializeComponent()
		{
            this.lvBalance = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvBalance
            // 
            this.lvBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvBalance.Location = new System.Drawing.Point(0, 0);
            this.lvBalance.Name = "lvBalance";
            this.lvBalance.Size = new System.Drawing.Size(336, 112);
            this.lvBalance.TabIndex = 0;
            this.lvBalance.UseCompatibleStateImageBehavior = false;
            this.lvBalance.View = System.Windows.Forms.View.Details;
            this.lvBalance.SelectedIndexChanged += new System.EventHandler(this.lvBalance_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Вид учета";
            this.columnHeader1.Width = 170;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Сумма";
            this.columnHeader2.Width = 81;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Без среднего";
            this.columnHeader3.Width = 85;
            // 
            // frmBalance
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(336, 109);
            this.Controls.Add(this.lvBalance);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmBalance";
            this.Text = "Сальдо";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmBalance_Closing);
            this.Load += new System.EventHandler(this.frmBalance_Load);
            this.ResumeLayout(false);

		}

        private void lvBalance_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}