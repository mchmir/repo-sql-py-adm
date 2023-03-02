using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmFilterDocVerifys : Form
	{
		private Button cmdCancel;

		private Button cmdOK;

		private System.ComponentModel.Container components = null;

		private ComboBox cmbStatus;

		private ComboBox cmbAgent;

		private Label label1;

		private Label label6;

		private Agents _agents;

		public frmFilterDocVerifys()
		{
			this.InitializeComponent();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
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

		private void frmFilterDocVerifys_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmFilterDocVerifys_Load(object sender, EventArgs e)
		{
			Tools.LoadWindows(this);
			this.cmbStatus.Items.AddRange(new object[] { "любое состояние", "прошел поверку", "не прошел поверку" });
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)5));
			Tools.FillCMBWhithAll(this._agents, this.cmbAgent, (long)0, "Все");
		}

		public void GetData(ref int idstatus, ref int idagent, ref string filter)
		{
			filter = this.cmbStatus.Text;
			idstatus = this.cmbStatus.SelectedIndex;
			if (this.cmbAgent.SelectedIndex <= 0)
			{
				idagent = 0;
				return;
			}
			idagent = (int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID();
		}

		private void InitializeComponent()
		{
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.label6 = new Label();
			this.cmbStatus = new ComboBox();
			this.cmbAgent = new ComboBox();
			this.label1 = new Label();
			base.SuspendLayout();
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(216, 72);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(120, 72);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(88, 24);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label6.Location = new Point(3, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(149, 16);
			this.label6.TabIndex = 58;
			this.label6.Text = "Состояние прибора учета:";
			this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbStatus.Location = new Point(152, 8);
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.Size = new System.Drawing.Size(152, 21);
			this.cmbStatus.TabIndex = 1;
			this.cmbAgent.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbAgent.Location = new Point(112, 32);
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.Size = new System.Drawing.Size(192, 21);
			this.cmbAgent.TabIndex = 59;
			this.label1.Location = new Point(4, 32);
			this.label1.Name = "label1";
			this.label1.TabIndex = 60;
			this.label1.Text = "Поверитель:";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(306, 106);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmbAgent);
			base.Controls.Add(this.cmbStatus);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.MinimumSize = new System.Drawing.Size(312, 96);
			base.Name = "frmFilterDocVerifys";
			this.Text = "Фильтр";
			base.Closing += new CancelEventHandler(this.frmFilterDocVerifys_Closing);
			base.Load += new EventHandler(this.frmFilterDocVerifys_Load);
			base.ResumeLayout(false);
		}
	}
}