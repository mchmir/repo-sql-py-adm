using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintDoVerifyGTM : Form
	{
		private ImageList imageList1;

		private GroupBox groupBox1;

		private Button button1;

		private Button button2;

		private IContainer components;

		private TypeGMeters _TypeGMeters;

		private GRU _gru;

		private C1DateEdit dtBegin;

		private Label label1;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private CheckBox chkNeed;

		private Agents _agents;

		public frmPrintDoVerifyGTM()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string str;
			try
			{
				try
				{
					long num = (long)0;
					int num1 = 0;
					if (this.chkNeed.Checked)
					{
						num1 = 1;
					}
					if (!this.radioButton1.Checked)
					{
						num = (long)2;
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repDoVerifyGGS.rpt");
					}
					else
					{
						num = (long)1;
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repDoVerifyGTM.rpt");
					}
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 3, 2 };
					string[] strArrays = new string[] { "@idtypegmeter", "@date", "@NeedCheck" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { num.ToString(), this.dtBegin.Value.ToString(), num1.ToString() };
					Form frmReport = new frmReports(str, strArrays1, strArrays, numArray)
					{
						Text = "Уведомление на проверку ПУ",
						MdiParent = Depot._main
					};
					frmReport.Show();
					frmReport = null;
					base.Close();
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPrintDoVerify_Load(object sender, EventArgs e)
		{
			this.dtBegin.Value = DateTime.Today;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintDoVerifyGTM));
			this.imageList1 = new ImageList(this.components);
			this.groupBox1 = new GroupBox();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.label1 = new Label();
			this.dtBegin = new C1DateEdit();
			this.button1 = new Button();
			this.button2 = new Button();
			this.chkNeed = new CheckBox();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dtBegin).BeginInit();
			base.SuspendLayout();
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.chkNeed);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtBegin);
			this.groupBox1.Location = new Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(212, 120);
			this.groupBox1.TabIndex = 69;
			this.groupBox1.TabStop = false;
			this.radioButton2.Location = new Point(12, 72);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.TabIndex = 35;
			this.radioButton2.Text = "Горгаз-сервис";
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new Point(12, 44);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.TabIndex = 34;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "ГазТехМонтаж";
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 33;
			this.label1.Text = "Период";
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.FormatType = FormatTypeEnum.LongDate;
			this.dtBegin.Location = new Point(52, 16);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(120, 18);
			this.dtBegin.TabIndex = 32;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Location = new Point(124, 128);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 24);
			this.button1.TabIndex = 71;
			this.button1.Text = "Отмена";
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Location = new Point(24, 128);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 24);
			this.button2.TabIndex = 70;
			this.button2.Text = "Сформировать";
			this.button2.Click += new EventHandler(this.button2_Click);
			this.chkNeed.Location = new Point(116, 44);
			this.chkNeed.Name = "chkNeed";
			this.chkNeed.Size = new System.Drawing.Size(88, 52);
			this.chkNeed.TabIndex = 36;
			this.chkNeed.Text = "нужна отметка в договоре";
			this.chkNeed.TextAlign = ContentAlignment.MiddleCenter;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.button1;
			base.ClientSize = new System.Drawing.Size(218, 158);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintDoVerifyGTM";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Уведомление на проверку ПУ";
			base.Load += new EventHandler(this.frmPrintDoVerify_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtBegin).EndInit();
			base.ResumeLayout(false);
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButton1.Checked)
			{
				this.radioButton2.Checked = false;
			}
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButton2.Checked)
			{
				this.radioButton1.Checked = false;
			}
		}
	}
}