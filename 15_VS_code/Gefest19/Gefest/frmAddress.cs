using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmAddress : Form
	{
		private System.ComponentModel.Container components = null;

		private Button cmdCancel;

		public Button cmdOk;

		private ucAddress ucAdd;

		private Address _address;

		private Addresss _addresss;

		public Address oAddress
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		public Addresss oAddresss
		{
			get
			{
				return this._addresss;
			}
			set
			{
				this._addresss = value;
			}
		}

		public frmAddress()
		{
			this.InitializeComponent();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			this.ucAdd.FindHouse();
			this.ucAdd.FindAddress();
			this.oAddress = this.ucAdd.oAddress;
			this.oAddresss = this.ucAdd.oAddresss;
			if (this.oAddress != null)
			{
				base.Close();
				return;
			}
			Tools.SaveWindows(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			this._address = null;
			base.Dispose(disposing);
		}

		private void frmAddress_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
			this.ucAdd.Dispose();
		}

		private void frmAddress_Load(object sender, EventArgs e)
		{
			Tools.LoadWindows(this);
			this.ucAdd = new ucAddress()
			{
				oAddress = this.oAddress
			};
			base.Controls.Add(this.ucAdd);
		}

		private void InitializeComponent()
		{
			this.cmdCancel = new Button();
			this.cmdOk = new Button();
			base.SuspendLayout();
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(276, 204);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(116, 24);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOk.FlatStyle = FlatStyle.Flat;
			this.cmdOk.Location = new Point(156, 204);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(116, 24);
			this.cmdOk.TabIndex = 8;
			this.cmdOk.Text = "Ok";
			this.cmdOk.Click += new EventHandler(this.cmdOk_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(394, 231);
			base.Controls.Add(this.cmdOk);
			base.Controls.Add(this.cmdCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmAddress";
			base.ShowInTaskbar = false;
			this.Text = "Справочник адресов";
			base.Closing += new CancelEventHandler(this.frmAddress_Closing);
			base.Load += new EventHandler(this.frmAddress_Load);
			base.ResumeLayout(false);
		}
	}
}