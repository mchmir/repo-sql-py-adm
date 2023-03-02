using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gefest
{
	public class frmComparisonContract : Form
	{
		private Button cmdCancel;

		private Button cmdOK;

		private CheckBox chPatronic;

		private CheckBox chName;

		private CheckBox chSurname;

		private Label lblPatronic;

		private Label lblName;

		private Label lblSurname;

		private System.ComponentModel.Container components = null;

		private Contract _contract;

		private int[] arr;

		public frmComparisonContract(Contract oContract, int[] oarr)
		{
			this.InitializeComponent();
			this._contract = oContract;
			this.arr = oarr;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (!this.chSurname.Checked)
			{
				this.arr[0] = 0;
			}
			else
			{
				this.arr[0] = 1;
			}
			if (!this.chName.Checked)
			{
				this.arr[1] = 0;
			}
			else
			{
				this.arr[1] = 1;
			}
			if (this.chPatronic.Checked)
			{
				this.arr[2] = 1;
				return;
			}
			this.arr[2] = 0;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmComparisonContract_Load(object sender, EventArgs e)
		{
			this.lblSurname.Text = this._contract.oPerson.Surname;
			this.lblName.Text = this._contract.oPerson.Name;
			this.lblPatronic.Text = this._contract.oPerson.Patronic;
			if (this.arr[0] == 1)
			{
				this.chSurname.Checked = true;
			}
			if (this.arr[1] == 1)
			{
				this.chName.Checked = true;
			}
			if (this.arr[2] == 1)
			{
				this.chPatronic.Checked = true;
			}
		}

		private void InitializeComponent()
		{
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.chPatronic = new CheckBox();
			this.chName = new CheckBox();
			this.chSurname = new CheckBox();
			this.lblPatronic = new Label();
			this.lblName = new Label();
			this.lblSurname = new Label();
			base.SuspendLayout();
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(395, 68);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(112, 24);
			this.cmdCancel.TabIndex = 24;
			this.cmdCancel.Text = "Отмена";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(275, 68);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(112, 24);
			this.cmdOK.TabIndex = 23;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.chPatronic.FlatStyle = FlatStyle.Flat;
			this.chPatronic.Location = new Point(3, 44);
			this.chPatronic.Name = "chPatronic";
			this.chPatronic.Size = new System.Drawing.Size(144, 16);
			this.chPatronic.TabIndex = 22;
			this.chPatronic.Text = "Отчество:";
			this.chName.FlatStyle = FlatStyle.Flat;
			this.chName.Location = new Point(3, 24);
			this.chName.Name = "chName";
			this.chName.Size = new System.Drawing.Size(144, 16);
			this.chName.TabIndex = 21;
			this.chName.Text = "Имя:";
			this.chSurname.FlatStyle = FlatStyle.Flat;
			this.chSurname.Location = new Point(3, 4);
			this.chSurname.Name = "chSurname";
			this.chSurname.Size = new System.Drawing.Size(144, 16);
			this.chSurname.TabIndex = 20;
			this.chSurname.Text = "Фамилия:";
			this.lblPatronic.BackColor = SystemColors.Info;
			this.lblPatronic.Location = new Point(155, 44);
			this.lblPatronic.Name = "lblPatronic";
			this.lblPatronic.Size = new System.Drawing.Size(352, 16);
			this.lblPatronic.TabIndex = 19;
			this.lblName.BackColor = SystemColors.Info;
			this.lblName.Location = new Point(155, 24);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(352, 16);
			this.lblName.TabIndex = 18;
			this.lblSurname.BackColor = SystemColors.Info;
			this.lblSurname.Location = new Point(155, 4);
			this.lblSurname.Name = "lblSurname";
			this.lblSurname.Size = new System.Drawing.Size(352, 16);
			this.lblSurname.TabIndex = 17;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(510, 93);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.chPatronic);
			base.Controls.Add(this.chName);
			base.Controls.Add(this.chSurname);
			base.Controls.Add(this.lblPatronic);
			base.Controls.Add(this.lblName);
			base.Controls.Add(this.lblSurname);
			base.Name = "frmComparisonContract";
			this.Text = "Выберите поля для изменения в БД";
			base.Load += new EventHandler(this.frmComparisonContract_Load);
			base.ResumeLayout(false);
		}
	}
}