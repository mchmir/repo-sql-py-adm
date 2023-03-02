using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintAccountList : Form
	{
		private ImageList imageList1;

		private Button button1;

		private Button button2;

		private IContainer components;

		private TypeGMeters _TypeGMeters;

		private GRU _gru;

		private GroupBox groupBox1;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private Agents _agents;

		public frmPrintAccountList()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					long num = (long)0;
					string str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repAccountList.rpt");
					num = (!this.radioButton1.Checked ? (long)2 : (long)1);
					this.Cursor = Cursors.WaitCursor;
					int[] numArray = new int[] { 2 };
					string[] strArrays = new string[] { "@Type" };
					string[] strArrays1 = strArrays;
					strArrays = new string[] { num.ToString() };
					Form frmReport = new frmReports(str, strArrays1, strArrays, numArray)
					{
						Text = "Список абонентов",
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

		private void frmPrintAccountList_Load(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintDoVerifyGTM));
			this.imageList1 = new ImageList(this.components);
			this.button1 = new Button();
			this.button2 = new Button();
			this.groupBox1 = new GroupBox();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Location = new Point(124, 88);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 24);
			this.button1.TabIndex = 71;
			this.button1.Text = "Отмена";
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Location = new Point(24, 88);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 24);
			this.button2.TabIndex = 70;
			this.button2.Text = "Сформировать";
			this.button2.Click += new EventHandler(this.button2_Click);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(212, 80);
			this.groupBox1.TabIndex = 69;
			this.groupBox1.TabStop = false;
			this.radioButton2.Location = new Point(12, 44);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(196, 24);
			this.radioButton2.TabIndex = 35;
			this.radioButton2.Text = "не начислять пеню";
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new Point(12, 16);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(192, 24);
			this.radioButton1.TabIndex = 34;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "не печатать счет-извещение";
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(218, 116);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintDoVerifyGTM";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Абоненты, которым...";
			base.Load += new EventHandler(this.frmPrintAccountList_Load);
			this.groupBox1.ResumeLayout(false);
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