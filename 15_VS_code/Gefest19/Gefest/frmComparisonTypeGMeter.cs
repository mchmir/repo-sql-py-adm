using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmComparisonTypeGMeter : Form
	{
		private Label lblName;

		private Label lblClassAccuracy;

		private Label lblCountDigital;

		private Label lblServiceLife;

		private Label lblMemo;

		private CheckBox chName;

		private CheckBox chClassAccuracy;

		private CheckBox chCountDigital;

		private CheckBox chServiceLife;

		private CheckBox chMemo;

		private Button cmdOK;

		private Button cmdCancel;

		private System.ComponentModel.Container components = null;

		private TypeGMeter _typegmeter;

		private int[] arr;

		public frmComparisonTypeGMeter(TypeGMeter oTypeGMeter, int[] oarr)
		{
			this.InitializeComponent();
			this._typegmeter = oTypeGMeter;
			this.arr = oarr;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (!this.chName.Checked)
			{
				this.arr[0] = 0;
			}
			else
			{
				this.arr[0] = 1;
			}
			if (!this.chClassAccuracy.Checked)
			{
				this.arr[1] = 0;
			}
			else
			{
				this.arr[1] = 1;
			}
			if (!this.chCountDigital.Checked)
			{
				this.arr[2] = 0;
			}
			else
			{
				this.arr[2] = 1;
			}
			if (!this.chServiceLife.Checked)
			{
				this.arr[3] = 0;
			}
			else
			{
				this.arr[3] = 1;
			}
			if (this.chMemo.Checked)
			{
				this.arr[4] = 1;
				return;
			}
			this.arr[4] = 0;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmComparisonTypeGMeter_Load(object sender, EventArgs e)
		{
			this.lblName.Text = this._typegmeter.get_Name();
			this.lblClassAccuracy.Text = this._typegmeter.ClassAccuracy.ToString();
			Label str = this.lblCountDigital;
			int countDigital = this._typegmeter.CountDigital;
			str.Text = countDigital.ToString();
			Label label = this.lblServiceLife;
			countDigital = this._typegmeter.ServiceLife;
			label.Text = countDigital.ToString();
			this.lblMemo.Text = this._typegmeter.Memo;
			if (this.arr[0] == 1)
			{
				this.chName.Checked = true;
			}
			if (this.arr[1] == 1)
			{
				this.chClassAccuracy.Checked = true;
			}
			if (this.arr[2] == 1)
			{
				this.chCountDigital.Checked = true;
			}
			if (this.arr[3] == 1)
			{
				this.chServiceLife.Checked = true;
			}
			if (this.arr[4] == 1)
			{
				this.chMemo.Checked = true;
			}
		}

		private void InitializeComponent()
		{
			this.lblName = new Label();
			this.lblClassAccuracy = new Label();
			this.lblCountDigital = new Label();
			this.lblServiceLife = new Label();
			this.lblMemo = new Label();
			this.chName = new CheckBox();
			this.chClassAccuracy = new CheckBox();
			this.chCountDigital = new CheckBox();
			this.chServiceLife = new CheckBox();
			this.chMemo = new CheckBox();
			this.cmdOK = new Button();
			this.cmdCancel = new Button();
			base.SuspendLayout();
			this.lblName.BackColor = SystemColors.Info;
			this.lblName.Location = new Point(156, 4);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(352, 16);
			this.lblName.TabIndex = 5;
			this.lblClassAccuracy.BackColor = SystemColors.Info;
			this.lblClassAccuracy.Location = new Point(156, 24);
			this.lblClassAccuracy.Name = "lblClassAccuracy";
			this.lblClassAccuracy.Size = new System.Drawing.Size(352, 16);
			this.lblClassAccuracy.TabIndex = 6;
			this.lblCountDigital.BackColor = SystemColors.Info;
			this.lblCountDigital.Location = new Point(156, 44);
			this.lblCountDigital.Name = "lblCountDigital";
			this.lblCountDigital.Size = new System.Drawing.Size(352, 16);
			this.lblCountDigital.TabIndex = 7;
			this.lblServiceLife.BackColor = SystemColors.Info;
			this.lblServiceLife.Location = new Point(156, 64);
			this.lblServiceLife.Name = "lblServiceLife";
			this.lblServiceLife.Size = new System.Drawing.Size(352, 16);
			this.lblServiceLife.TabIndex = 8;
			this.lblMemo.BackColor = SystemColors.Info;
			this.lblMemo.Location = new Point(156, 84);
			this.lblMemo.Name = "lblMemo";
			this.lblMemo.Size = new System.Drawing.Size(352, 16);
			this.lblMemo.TabIndex = 9;
			this.chName.FlatStyle = FlatStyle.Flat;
			this.chName.Location = new Point(4, 4);
			this.chName.Name = "chName";
			this.chName.Size = new System.Drawing.Size(144, 16);
			this.chName.TabIndex = 10;
			this.chName.Text = "Название:";
			this.chClassAccuracy.FlatStyle = FlatStyle.Flat;
			this.chClassAccuracy.Location = new Point(4, 24);
			this.chClassAccuracy.Name = "chClassAccuracy";
			this.chClassAccuracy.Size = new System.Drawing.Size(144, 16);
			this.chClassAccuracy.TabIndex = 11;
			this.chClassAccuracy.Text = "Класс точности:";
			this.chCountDigital.FlatStyle = FlatStyle.Flat;
			this.chCountDigital.Location = new Point(4, 44);
			this.chCountDigital.Name = "chCountDigital";
			this.chCountDigital.Size = new System.Drawing.Size(144, 16);
			this.chCountDigital.TabIndex = 12;
			this.chCountDigital.Text = "Разрядность шкалы:";
			this.chServiceLife.FlatStyle = FlatStyle.Flat;
			this.chServiceLife.Location = new Point(4, 64);
			this.chServiceLife.Name = "chServiceLife";
			this.chServiceLife.Size = new System.Drawing.Size(144, 16);
			this.chServiceLife.TabIndex = 13;
			this.chServiceLife.Text = "Период поверки:";
			this.chMemo.FlatStyle = FlatStyle.Flat;
			this.chMemo.Location = new Point(4, 84);
			this.chMemo.Name = "chMemo";
			this.chMemo.Size = new System.Drawing.Size(144, 16);
			this.chMemo.TabIndex = 14;
			this.chMemo.Text = "Примечание:";
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(276, 108);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(112, 24);
			this.cmdOK.TabIndex = 15;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(396, 108);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(112, 24);
			this.cmdCancel.TabIndex = 16;
			this.cmdCancel.Text = "Отмена";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(512, 137);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.chMemo);
			base.Controls.Add(this.chServiceLife);
			base.Controls.Add(this.chCountDigital);
			base.Controls.Add(this.chClassAccuracy);
			base.Controls.Add(this.chName);
			base.Controls.Add(this.lblMemo);
			base.Controls.Add(this.lblServiceLife);
			base.Controls.Add(this.lblCountDigital);
			base.Controls.Add(this.lblClassAccuracy);
			base.Controls.Add(this.lblName);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "frmComparisonTypeGMeter";
			this.Text = "Выберите поля для изменения в БД";
			base.Load += new EventHandler(this.frmComparisonTypeGMeter_Load);
			base.ResumeLayout(false);
		}
	}
}