using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmComparisonGMeter : Form
	{
		private Button cmdCancel;

		private Button cmdOK;

		private System.ComponentModel.Container components = null;

		private CheckBox chDateVerify;

		private CheckBox chDateInstall;

		private CheckBox chBeginValue;

		private CheckBox chSerialNumber;

		private CheckBox chTypeGMeter;

		private Label lblDateVerify;

		private Label lblDateInstall;

		private Label lblBeginValue;

		private Label lblSerialNumber;

		private Label lblTypeGMeter;

		private CheckBox chStatus;

		private Label lblStatus;

		private CheckBox chMemo;

		private Label lblMemo;

		private CheckBox chDateFabr;

		private Label lblDateFabr;

		private Gmeter _gmeter;

		private CheckBox chTypeVerify;

		private Label lblTypeVerify;

		private int[] arr;

		public frmComparisonGMeter(Gmeter oGmeter, int[] oarr)
		{
			this.InitializeComponent();
			this._gmeter = oGmeter;
			this.arr = oarr;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (!this.chTypeGMeter.Checked)
			{
				this.arr[0] = 0;
			}
			else
			{
				this.arr[0] = 1;
			}
			if (!this.chSerialNumber.Checked)
			{
				this.arr[1] = 0;
			}
			else
			{
				this.arr[1] = 1;
			}
			if (!this.chBeginValue.Checked)
			{
				this.arr[2] = 0;
			}
			else
			{
				this.arr[2] = 1;
			}
			if (!this.chDateInstall.Checked)
			{
				this.arr[3] = 0;
			}
			else
			{
				this.arr[3] = 1;
			}
			if (!this.chDateVerify.Checked)
			{
				this.arr[4] = 0;
			}
			else
			{
				this.arr[4] = 1;
			}
			if (!this.chTypeVerify.Checked)
			{
				this.arr[5] = 0;
			}
			else
			{
				this.arr[5] = 1;
			}
			if (!this.chStatus.Checked)
			{
				this.arr[6] = 0;
			}
			else
			{
				this.arr[6] = 1;
			}
			if (!this.chMemo.Checked)
			{
				this.arr[7] = 0;
			}
			else
			{
				this.arr[7] = 1;
			}
			if (this.chDateFabr.Checked)
			{
				this.arr[8] = 1;
				return;
			}
			this.arr[8] = 0;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmComparisonGMeter_Load(object sender, EventArgs e)
		{
			this.lblTypeGMeter.Text = this._gmeter.oTypeGMeter.Fullname;
			this.lblSerialNumber.Text = this._gmeter.SerialNumber;
			this.lblBeginValue.Text = this._gmeter.BeginValue.ToString();
			Label shortDateString = this.lblDateInstall;
			DateTime dateInstall = this._gmeter.DateInstall;
			shortDateString.Text = dateInstall.ToShortDateString();
			Label label = this.lblDateVerify;
			dateInstall = this._gmeter.DateVerify;
			label.Text = dateInstall.ToShortDateString();
			if (this._gmeter.oTypeVerify == null)
			{
				this.lblTypeVerify.Text = "не известно";
			}
			else
			{
				this.lblTypeVerify.Text = this._gmeter.oTypeVerify.get_Name();
			}
			this.lblStatus.Text = this._gmeter.oStatusGMeter.get_Name();
			this.lblMemo.Text = this._gmeter.Memo;
			Label shortDateString1 = this.lblDateFabr;
			dateInstall = this._gmeter.DateFabrication;
			shortDateString1.Text = dateInstall.ToShortDateString();
			if (this.arr[0] == 1)
			{
				this.chTypeGMeter.Checked = true;
			}
			if (this.arr[1] == 1)
			{
				this.chSerialNumber.Checked = true;
			}
			if (this.arr[2] == 1)
			{
				this.chBeginValue.Checked = true;
			}
			if (this.arr[3] == 1)
			{
				this.chDateInstall.Checked = true;
			}
			if (this.arr[4] == 1)
			{
				this.chDateVerify.Checked = true;
			}
			if (this.arr[5] == 1)
			{
				this.chTypeVerify.Checked = true;
			}
			if (this.arr[6] == 1)
			{
				this.chStatus.Checked = true;
			}
			if (this.arr[7] == 1)
			{
				this.chMemo.Checked = true;
			}
			if (this.arr[8] == 1)
			{
				this.chDateFabr.Checked = true;
			}
		}

		private void InitializeComponent()
		{
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.chDateVerify = new CheckBox();
			this.chDateInstall = new CheckBox();
			this.chBeginValue = new CheckBox();
			this.chSerialNumber = new CheckBox();
			this.chTypeGMeter = new CheckBox();
			this.lblDateVerify = new Label();
			this.lblDateInstall = new Label();
			this.lblBeginValue = new Label();
			this.lblSerialNumber = new Label();
			this.lblTypeGMeter = new Label();
			this.chStatus = new CheckBox();
			this.lblStatus = new Label();
			this.chMemo = new CheckBox();
			this.lblMemo = new Label();
			this.chDateFabr = new CheckBox();
			this.lblDateFabr = new Label();
			this.chTypeVerify = new CheckBox();
			this.lblTypeVerify = new Label();
			base.SuspendLayout();
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(395, 200);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(112, 24);
			this.cmdCancel.TabIndex = 28;
			this.cmdCancel.Text = "Отмена";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(275, 200);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(112, 24);
			this.cmdOK.TabIndex = 27;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.chDateVerify.FlatStyle = FlatStyle.Flat;
			this.chDateVerify.Location = new Point(3, 83);
			this.chDateVerify.Name = "chDateVerify";
			this.chDateVerify.Size = new System.Drawing.Size(144, 16);
			this.chDateVerify.TabIndex = 26;
			this.chDateVerify.Text = "Дата поверки:";
			this.chDateInstall.FlatStyle = FlatStyle.Flat;
			this.chDateInstall.Location = new Point(3, 63);
			this.chDateInstall.Name = "chDateInstall";
			this.chDateInstall.Size = new System.Drawing.Size(144, 16);
			this.chDateInstall.TabIndex = 25;
			this.chDateInstall.Text = "Дата установки:";
			this.chBeginValue.FlatStyle = FlatStyle.Flat;
			this.chBeginValue.Location = new Point(3, 43);
			this.chBeginValue.Name = "chBeginValue";
			this.chBeginValue.Size = new System.Drawing.Size(144, 16);
			this.chBeginValue.TabIndex = 24;
			this.chBeginValue.Text = "Нач. показания:";
			this.chSerialNumber.FlatStyle = FlatStyle.Flat;
			this.chSerialNumber.Location = new Point(3, 23);
			this.chSerialNumber.Name = "chSerialNumber";
			this.chSerialNumber.Size = new System.Drawing.Size(144, 16);
			this.chSerialNumber.TabIndex = 23;
			this.chSerialNumber.Text = "№ ПУ:";
			this.chTypeGMeter.FlatStyle = FlatStyle.Flat;
			this.chTypeGMeter.Location = new Point(3, 3);
			this.chTypeGMeter.Name = "chTypeGMeter";
			this.chTypeGMeter.Size = new System.Drawing.Size(144, 16);
			this.chTypeGMeter.TabIndex = 22;
			this.chTypeGMeter.Text = "Тип ПУ:";
			this.lblDateVerify.BackColor = SystemColors.Info;
			this.lblDateVerify.Location = new Point(155, 83);
			this.lblDateVerify.Name = "lblDateVerify";
			this.lblDateVerify.Size = new System.Drawing.Size(352, 16);
			this.lblDateVerify.TabIndex = 21;
			this.lblDateInstall.BackColor = SystemColors.Info;
			this.lblDateInstall.Location = new Point(155, 63);
			this.lblDateInstall.Name = "lblDateInstall";
			this.lblDateInstall.Size = new System.Drawing.Size(352, 16);
			this.lblDateInstall.TabIndex = 20;
			this.lblBeginValue.BackColor = SystemColors.Info;
			this.lblBeginValue.Location = new Point(155, 43);
			this.lblBeginValue.Name = "lblBeginValue";
			this.lblBeginValue.Size = new System.Drawing.Size(352, 16);
			this.lblBeginValue.TabIndex = 19;
			this.lblSerialNumber.BackColor = SystemColors.Info;
			this.lblSerialNumber.Location = new Point(155, 23);
			this.lblSerialNumber.Name = "lblSerialNumber";
			this.lblSerialNumber.Size = new System.Drawing.Size(352, 16);
			this.lblSerialNumber.TabIndex = 18;
			this.lblTypeGMeter.BackColor = SystemColors.Info;
			this.lblTypeGMeter.Location = new Point(155, 3);
			this.lblTypeGMeter.Name = "lblTypeGMeter";
			this.lblTypeGMeter.Size = new System.Drawing.Size(352, 16);
			this.lblTypeGMeter.TabIndex = 17;
			this.chStatus.Enabled = false;
			this.chStatus.FlatStyle = FlatStyle.Flat;
			this.chStatus.Location = new Point(3, 124);
			this.chStatus.Name = "chStatus";
			this.chStatus.Size = new System.Drawing.Size(144, 16);
			this.chStatus.TabIndex = 30;
			this.chStatus.Text = "Статус:";
			this.lblStatus.BackColor = SystemColors.Info;
			this.lblStatus.Location = new Point(155, 124);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(352, 16);
			this.lblStatus.TabIndex = 29;
			this.chMemo.FlatStyle = FlatStyle.Flat;
			this.chMemo.Location = new Point(3, 144);
			this.chMemo.Name = "chMemo";
			this.chMemo.Size = new System.Drawing.Size(144, 16);
			this.chMemo.TabIndex = 32;
			this.chMemo.Text = "Примечание:";
			this.lblMemo.BackColor = SystemColors.Info;
			this.lblMemo.Location = new Point(155, 144);
			this.lblMemo.Name = "lblMemo";
			this.lblMemo.Size = new System.Drawing.Size(352, 28);
			this.lblMemo.TabIndex = 31;
			this.chDateFabr.FlatStyle = FlatStyle.Flat;
			this.chDateFabr.Location = new Point(3, 176);
			this.chDateFabr.Name = "chDateFabr";
			this.chDateFabr.Size = new System.Drawing.Size(144, 16);
			this.chDateFabr.TabIndex = 34;
			this.chDateFabr.Text = "Дата изготовления:";
			this.lblDateFabr.BackColor = SystemColors.Info;
			this.lblDateFabr.Location = new Point(155, 176);
			this.lblDateFabr.Name = "lblDateFabr";
			this.lblDateFabr.Size = new System.Drawing.Size(352, 16);
			this.lblDateFabr.TabIndex = 33;
			this.chTypeVerify.FlatStyle = FlatStyle.Flat;
			this.chTypeVerify.Location = new Point(3, 104);
			this.chTypeVerify.Name = "chTypeVerify";
			this.chTypeVerify.Size = new System.Drawing.Size(144, 16);
			this.chTypeVerify.TabIndex = 36;
			this.chTypeVerify.Text = "Результат поверки:";
			this.lblTypeVerify.BackColor = SystemColors.Info;
			this.lblTypeVerify.Location = new Point(155, 104);
			this.lblTypeVerify.Name = "lblTypeVerify";
			this.lblTypeVerify.Size = new System.Drawing.Size(352, 16);
			this.lblTypeVerify.TabIndex = 35;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(510, 227);
			base.Controls.Add(this.chTypeVerify);
			base.Controls.Add(this.lblTypeVerify);
			base.Controls.Add(this.chDateFabr);
			base.Controls.Add(this.lblDateFabr);
			base.Controls.Add(this.chMemo);
			base.Controls.Add(this.lblMemo);
			base.Controls.Add(this.chStatus);
			base.Controls.Add(this.lblStatus);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.chDateVerify);
			base.Controls.Add(this.chDateInstall);
			base.Controls.Add(this.chBeginValue);
			base.Controls.Add(this.chSerialNumber);
			base.Controls.Add(this.chTypeGMeter);
			base.Controls.Add(this.lblDateVerify);
			base.Controls.Add(this.lblDateInstall);
			base.Controls.Add(this.lblBeginValue);
			base.Controls.Add(this.lblSerialNumber);
			base.Controls.Add(this.lblTypeGMeter);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "frmComparisonGMeter";
			this.Text = "Выберите поля для изменения в БД";
			base.Load += new EventHandler(this.frmComparisonGMeter_Load);
			base.ResumeLayout(false);
		}
	}
}