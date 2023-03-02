using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmSetConformityTypeGMeter : Form
	{
		private Label label1;

		private Button cmdCancel;

		private Button cmdOK;

		private System.ComponentModel.Container components = null;

		private ComboBox cmbTypeGMeter;

		private TypeGMeters _typegmeters;

		public frmSetConformityTypeGMeter()
		{
			this.InitializeComponent();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmSetConformityTypeGMeter_Load(object sender, EventArgs e)
		{
			this._typegmeters = new TypeGMeters();
			this._typegmeters.Load();
			Tools.FillCMBWhithAll(this._typegmeters, this.cmbTypeGMeter, (long)0, "нет соответствия");
		}

		public void GetData(ref int idconformity)
		{
			if (this.cmbTypeGMeter.SelectedIndex <= 0)
			{
				idconformity = 0;
				return;
			}
			idconformity = this.cmbTypeGMeter.SelectedIndex;
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.cmbTypeGMeter = new ComboBox();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			base.SuspendLayout();
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Выберите тип ПУ:";
			this.cmbTypeGMeter.Location = new Point(112, 8);
			this.cmbTypeGMeter.Name = "cmbTypeGMeter";
			this.cmbTypeGMeter.Size = new System.Drawing.Size(176, 21);
			this.cmbTypeGMeter.TabIndex = 1;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(176, 64);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(112, 24);
			this.cmdCancel.TabIndex = 30;
			this.cmdCancel.Text = "Отмена";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(56, 64);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(112, 24);
			this.cmdOK.TabIndex = 29;
			this.cmdOK.Text = "ОК";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(292, 95);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmbTypeGMeter);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmSetConformityTypeGMeter";
			this.Text = "Установите соответствие типов ПУ";
			base.Load += new EventHandler(this.frmSetConformityTypeGMeter_Load);
			base.ResumeLayout(false);
		}
	}
}