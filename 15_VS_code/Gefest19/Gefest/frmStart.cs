using AxSHDocVw;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Gefest
{
	public class frmStart : Form
	{
		private Splitter splitter1;

		private TreeView tvStart;

		private AxWebBrowser WebBrowser;

		private System.ComponentModel.Container components = null;

		public frmStart()
		{
			this.InitializeComponent();
			foreach (MenuItem menuItem in Depot._main.Menu.MenuItems)
			{
				if (!menuItem.Visible)
				{
					continue;
				}
				TreeNode handle = this.tvStart.Nodes.Add(menuItem.Text);
				handle.Tag = menuItem.Handle;
				foreach (MenuItem menuItem1 in menuItem.MenuItems)
				{
					if (!menuItem1.Visible || !(menuItem1.Text != "-"))
					{
						continue;
					}
					TreeNode treeNode = handle.Nodes.Add(menuItem1.Text);
					treeNode.Tag = menuItem1.Handle;
					foreach (MenuItem menuItem2 in menuItem1.MenuItems)
					{
						if (!menuItem2.Visible || !(menuItem1.Text != "-"))
						{
							continue;
						}
						TreeNode handle1 = treeNode.Nodes.Add(menuItem2.Text);
						handle1.Tag = menuItem2.Handle;
					}
				}
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

		private void frmStart_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmStart_Load(object sender, EventArgs e)
		{
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmStart));
			this.tvStart = new TreeView();
			this.splitter1 = new Splitter();
			this.WebBrowser = new AxWebBrowser();
			((ISupportInitialize)this.WebBrowser).BeginInit();
			base.SuspendLayout();
			this.tvStart.BorderStyle = BorderStyle.FixedSingle;
			this.tvStart.Dock = DockStyle.Left;
			this.tvStart.ForeColor = SystemColors.HotTrack;
			this.tvStart.ImageIndex = -1;
			this.tvStart.Location = new Point(0, 0);
			this.tvStart.Name = "tvStart";
			this.tvStart.SelectedImageIndex = -1;
			this.tvStart.Size = new System.Drawing.Size(280, 429);
			this.tvStart.TabIndex = 0;
			this.tvStart.DoubleClick += new EventHandler(this.tvStart_DoubleClick);
			this.tvStart.AfterSelect += new TreeViewEventHandler(this.tvStart_AfterSelect);
			this.splitter1.Location = new Point(280, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 429);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			this.WebBrowser.Dock = DockStyle.Fill;
			this.WebBrowser.Enabled = true;
			this.WebBrowser.Location = new Point(283, 0);
			this.WebBrowser.OcxState = (AxHost.State)resourceManager.GetObject("WebBrowser.OcxState");
			this.WebBrowser.Size = new System.Drawing.Size(453, 429);
			this.WebBrowser.TabIndex = 2;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(736, 429);
			base.Controls.Add(this.WebBrowser);
			base.Controls.Add(this.splitter1);
			base.Controls.Add(this.tvStart);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Name = "frmStart";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Стартовый навигатор";
			base.Closing += new CancelEventHandler(this.frmStart_Closing);
			base.Load += new EventHandler(this.frmStart_Load);
			((ISupportInitialize)this.WebBrowser).EndInit();
			base.ResumeLayout(false);
		}

		private void tvStart_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				if (this.tvStart.SelectedNode != null)
				{
					object missing = Type.Missing;
					if (!File.Exists(string.Concat(Depot.oSettings.ReportPath, "help\\", this.tvStart.SelectedNode.Text, ".htm")) && File.Exists(string.Concat(Depot.oSettings.ReportPath, "help\\blank.htm")))
					{
						File.Copy(string.Concat(Depot.oSettings.ReportPath, "help\\blank.htm"), string.Concat(Depot.oSettings.ReportPath, "help\\", this.tvStart.SelectedNode.Text, ".htm"));
					}
					this.WebBrowser.Navigate(string.Concat(Depot.oSettings.ReportPath, "help\\", this.tvStart.SelectedNode.Text, ".htm"), ref missing, ref missing, ref missing, ref missing);
				}
			}
			catch
			{
			}
		}

		private void tvStart_DoubleClick(object sender, EventArgs e)
		{
			if (this.tvStart.SelectedNode != null)
			{
				MenuItem menuItem = Depot._main.Menu.FindMenuItem(0, (IntPtr)this.tvStart.SelectedNode.Tag);
				menuItem.PerformClick();
			}
		}
	}
}