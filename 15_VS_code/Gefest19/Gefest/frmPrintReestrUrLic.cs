using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintReestrUrLic : Form
	{
		private C1DateEdit dtPeriod;

		private ComboBox cmbDom;

		private Label label2;

		private Label label1;

		private Button cmdOK;

		private Button cmdClose;

		private ComboBox cmbDogTO;

		private CheckBox chkDogTO;

		private System.ComponentModel.Container components = null;

		public frmPrintReestrUrLic()
		{
			this.InitializeComponent();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Periods period = new Periods();
			period.Load((DateTime)this.dtPeriod.Value);
			if (period.get_Count() <= 0)
			{
				MessageBox.Show("Не верный выбор периода!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			this.Rep(period[0].get_ID(), (long)(this.cmbDom.SelectedIndex - 1), (long)this.cmbDogTO.SelectedIndex);
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

		private void frmPrintReestrUrLic_Load(object sender, EventArgs e)
		{
			this.dtPeriod.Value = DateTime.Today.Date;
			this.cmbDogTO.SelectedIndex = 0;
		}

		private void InitializeComponent()
		{
			this.dtPeriod = new C1DateEdit();
			this.cmbDom = new ComboBox();
			this.label2 = new Label();
			this.label1 = new Label();
			this.cmdOK = new Button();
			this.cmdClose = new Button();
			this.cmbDogTO = new ComboBox();
			this.chkDogTO = new CheckBox();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			base.SuspendLayout();
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMMyyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(136, 8);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(136, 18);
			this.dtPeriod.TabIndex = 46;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			ComboBox.ObjectCollection items = this.cmbDom.Items;
			object[] objArray = new object[] { "По всем", "Не определен", "Активен", "Закрыт" };
			items.AddRange(objArray);
			this.cmbDom.Location = new Point(135, 32);
			this.cmbDom.Name = "cmbDom";
			this.cmbDom.Size = new System.Drawing.Size(136, 21);
			this.cmbDom.TabIndex = 47;
			this.cmbDom.Text = "По всем ";
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label2.Location = new Point(16, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 49;
			this.label2.Text = "Статус договора:";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 48;
			this.label1.Text = "Период";
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(88, 96);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 50;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(192, 96);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 51;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			ComboBox.ObjectCollection objectCollections = this.cmbDogTO.Items;
			objArray = new object[] { "По всем", "Заключен", "Нет договора" };
			objectCollections.AddRange(objArray);
			this.cmbDogTO.Location = new Point(134, 59);
			this.cmbDogTO.Name = "cmbDogTO";
			this.cmbDogTO.Size = new System.Drawing.Size(136, 21);
			this.cmbDogTO.TabIndex = 53;
			this.cmbDogTO.Text = "По всем ";
			this.chkDogTO.Location = new Point(16, 58);
			this.chkDogTO.Name = "chkDogTO";
			this.chkDogTO.Size = new System.Drawing.Size(112, 24);
			this.chkDogTO.TabIndex = 54;
			this.chkDogTO.Text = "Договор на ТО:";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(288, 126);
			base.Controls.Add(this.chkDogTO);
			base.Controls.Add(this.cmbDogTO);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.dtPeriod);
			base.Controls.Add(this.cmbDom);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.Name = "frmPrintReestrUrLic";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Список юридических лиц";
			base.Load += new EventHandler(this.frmPrintReestrUrLic_Load);
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDPeriod, long house, long Dog)
		{
			int[] numArray;
			string[] strArrays;
			string[] strArrays1;
			string str;
			string[] str1;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this.chkDogTO.Checked)
					{
						numArray = new int[] { 2, 2, 2, 1 };
						str1 = new string[] { "@IdPeriod", "@per", "@Dog", "Month" };
						strArrays = str1;
						str1 = new string[] { IDPeriod.ToString(), house.ToString(), Dog.ToString(), this.dtPeriod.Text.ToString() };
						strArrays1 = str1;
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repReestrUrLicTO.rpt");
					}
					else
					{
						numArray = new int[] { 2, 2, 1 };
						str1 = new string[] { "@IdPeriod", "@per", "Month" };
						strArrays = str1;
						str1 = new string[] { IDPeriod.ToString(), house.ToString(), this.dtPeriod.Text.ToString() };
						strArrays1 = str1;
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repReestrUrLic.rpt");
					}
					Form frmReport = new frmReports(str, strArrays, strArrays1, numArray)
					{
						Text = "Список юридических лиц",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}
	}
}