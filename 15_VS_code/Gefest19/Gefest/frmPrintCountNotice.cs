using C1.Win.C1Input;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintCountNotice : Form
	{
		private TextBox txtInvNumber;

		private Button cmdGRU;

		private Label lblNameGRU;

		private Label label4;

		private ImageList imageList1;

		private Label label1;

		private GroupBox groupBox1;

		private Button cmdClose;

		private Button cmdOK;

		private IContainer components;

		private C1DateEdit dtPeriod;

		private CheckBox chkAll;

		private GRU _gru;

		public frmPrintCountNotice()
		{
			this.InitializeComponent();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdGRU_Click_1(object sender, EventArgs e)
		{
			GRUs gRU = new GRUs();
			gRU.Load();
			string[] strArrays = new string[] { "Номер", "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 100, 300 };
			strArrays = new string[] { "InvNumber", "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник РУ", gRU, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			if (frmSimpleObj.lvData.SelectedItems.Count > 0)
			{
				this._gru = gRU.item(Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
				this.txtInvNumber.Text = this._gru.InvNumber;
				this.txtInvNumber.ForeColor = SystemColors.WindowText;
				this.lblNameGRU.Text = this._gru.get_Name();
			}
			frmSimpleObj = null;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (!this.chkAll.Checked)
			{
				if (this._gru == null)
				{
					MessageBox.Show("Введены не все данные!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				Periods period = new Periods();
				period.Load((DateTime)this.dtPeriod.Value);
				this.Rep(this._gru.get_ID(), period[0].get_ID(), this.dtPeriod.Text.ToString());
				return;
			}
			GRUs gRU = new GRUs();
			gRU.Load();
			Periods period1 = new Periods();
			period1.Load((DateTime)this.dtPeriod.Value);
			DateTime now = DateTime.Now;
			string str = string.Concat("gru_reports_", now.ToString("yyyyMMdd"), ".txt");
			using (StreamWriter streamWriter = new StreamWriter(string.Concat("D:\\_gru_reports_\\", str), true))
			{
				now = DateTime.Now;
				streamWriter.WriteLine(string.Concat(now.ToString(), ": начали формирование "));
			}
			int num = 0;
			int count = gRU.get_Count();
			MessageBox.Show(count.ToString());
			foreach (GRU gRU1 in gRU)
			{
				if (Convert.ToUInt32(gRU1.InvNumber) < Convert.ToUInt32(this.txtInvNumber.Text))
				{
					continue;
				}
				try
				{
					this.RepExport(gRU1.get_ID(), period1[0].get_ID(), this.dtPeriod.Text.ToString(), string.Concat(this.dtPeriod.Text.ToString(), "_", gRU1.oAgent.get_Name()), string.Concat(gRU1.oAgent.get_Name(), "_", gRU1.InvNumber));
					num++;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			using (StreamWriter streamWriter1 = new StreamWriter(string.Concat("D:\\_gru_reports_\\", str), true))
			{
				now = DateTime.Now;
				streamWriter1.WriteLine(string.Concat(now.ToString(), ": закончили формирование : ", Convert.ToString(num)));
			}
			MessageBox.Show("done");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void dtPeriod_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.cmdOK.Focus();
		}

		private void frmPrintCountNotice_Load(object sender, EventArgs e)
		{
			this.txtInvNumber.Focus();
			this.dtPeriod.Value = DateTime.Today.AddMonths(-1);
		}

		private void GetGRU()
		{
			GRUs gRU = new GRUs();
			gRU.Load();
			this._gru = null;
			foreach (GRU gRU1 in gRU)
			{
				if (gRU1.InvNumber != this.txtInvNumber.Text.Trim())
				{
					continue;
				}
				this._gru = gRU1;
				this.txtInvNumber.Text = this._gru.InvNumber;
				this.txtInvNumber.ForeColor = SystemColors.WindowText;
				this.lblNameGRU.Text = this._gru.get_Name();
				this.dtPeriod.Focus();
				return;
			}
			this.lblNameGRU.Text = "Укажите номер РУ";
			this.txtInvNumber.ForeColor = Color.Red;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintCountNotice));
			this.txtInvNumber = new TextBox();
			this.cmdGRU = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblNameGRU = new Label();
			this.label4 = new Label();
			this.label1 = new Label();
			this.groupBox1 = new GroupBox();
			this.dtPeriod = new C1DateEdit();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.chkAll = new CheckBox();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dtPeriod).BeginInit();
			base.SuspendLayout();
			this.txtInvNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtInvNumber.Location = new Point(52, 12);
			this.txtInvNumber.Name = "txtInvNumber";
			this.txtInvNumber.Size = new System.Drawing.Size(72, 20);
			this.txtInvNumber.TabIndex = 1;
			this.txtInvNumber.Text = "";
			this.txtInvNumber.Leave += new EventHandler(this.txtInvNumber_Leave);
			this.txtInvNumber.KeyUp += new KeyEventHandler(this.txtInvNumber_KeyUp);
			this.txtInvNumber.Enter += new EventHandler(this.txtInvNumber_Enter);
			this.cmdGRU.FlatStyle = FlatStyle.Flat;
			this.cmdGRU.ForeColor = SystemColors.ControlText;
			this.cmdGRU.ImageIndex = 0;
			this.cmdGRU.ImageList = this.imageList1;
			this.cmdGRU.Location = new Point(300, 12);
			this.cmdGRU.Name = "cmdGRU";
			this.cmdGRU.Size = new System.Drawing.Size(20, 20);
			this.cmdGRU.TabIndex = 2;
			this.cmdGRU.Click += new EventHandler(this.cmdGRU_Click_1);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(128, 12);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(168, 20);
			this.lblNameGRU.TabIndex = 31;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 30;
			this.label4.Text = "РУ";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 30;
			this.label1.Text = "Период";
			this.groupBox1.Controls.Add(this.chkAll);
			this.groupBox1.Controls.Add(this.dtPeriod);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lblNameGRU);
			this.groupBox1.Controls.Add(this.cmdGRU);
			this.groupBox1.Controls.Add(this.txtInvNumber);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(328, 60);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.dtPeriod.BorderStyle = 1;
			this.dtPeriod.CustomFormat = "MMMM yyyy";
			this.dtPeriod.FormatType = FormatTypeEnum.CustomFormat;
			this.dtPeriod.Location = new Point(52, 36);
			this.dtPeriod.Name = "dtPeriod";
			this.dtPeriod.Size = new System.Drawing.Size(116, 18);
			this.dtPeriod.TabIndex = 3;
			this.dtPeriod.Tag = null;
			this.dtPeriod.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPeriod.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtPeriod.KeyPress += new KeyPressEventHandler(this.dtPeriod_KeyPress);
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(232, 64);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 5;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(128, 64);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.chkAll.Location = new Point(211, 38);
			this.chkAll.Name = "chkAll";
			this.chkAll.Size = new System.Drawing.Size(104, 16);
			this.chkAll.TabIndex = 32;
			this.chkAll.Text = "экспорт всех";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.cmdClose;
			base.ClientSize = new System.Drawing.Size(330, 92);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(336, 116);
			base.MinimizeBox = false;
			base.MinimumSize = new System.Drawing.Size(336, 116);
			base.Name = "frmPrintCountNotice";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Счёт-извещение";
			base.Load += new EventHandler(this.frmPrintCountNotice_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtPeriod).EndInit();
			base.ResumeLayout(false);
		}

		private void Rep(long IDGRU, long IDPeriod, string vMonth)
		{
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2, 1 };
					string[] str = new string[] { "@IdGRU", "@IdPeriod", "@Month" };
					string[] strArrays = str;
					str = new string[] { IDGRU.ToString(), IDPeriod.ToString(), vMonth };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repCountNoticeGM.rpt");
					Form frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Счёт-извещение по РУ №", this._gru.InvNumber.ToString(), " за ", vMonth.ToString())
					};
					frmReport.ShowDialog(this);
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

		private void RepExport(long IDGRU, long IDPeriod, string vMonth, string Path, string FileName)
		{
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 2, 1 };
					string[] str = new string[] { "@IdGRU", "@IdPeriod", "@Month" };
					string[] strArrays = str;
					str = new string[] { IDGRU.ToString(), IDPeriod.ToString(), vMonth };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repCountNoticeGMOneByPage.rpt");
					DateTime now = DateTime.Now;
					string str2 = string.Concat("gru_reports_", now.ToString("yyyyMMdd"), ".txt");
					using (StreamWriter streamWriter = new StreamWriter(string.Concat("D:\\_gru_reports_\\", str2), true))
					{
						now = DateTime.Now;
						streamWriter.WriteLine(string.Concat(now.ToString(), ": начали формировать файл ", FileName));
					}
					Tools.RptToPDF(str1, strArrays, strArrays1, numArray, string.Concat("D:\\_gru_reports_\\", Path), FileName);
					using (StreamWriter streamWriter1 = new StreamWriter(string.Concat("D:\\_gru_reports_\\", str2), true))
					{
						now = DateTime.Now;
						streamWriter1.WriteLine(string.Concat(now.ToString(), ": сформировали файл ", FileName));
					}
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

		private void txtInvNumber_Enter(object sender, EventArgs e)
		{
			this.txtInvNumber.SelectAll();
		}

		private void txtInvNumber_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			this.GetGRU();
		}

		private void txtInvNumber_Leave(object sender, EventArgs e)
		{
			this.GetGRU();
		}
	}
}