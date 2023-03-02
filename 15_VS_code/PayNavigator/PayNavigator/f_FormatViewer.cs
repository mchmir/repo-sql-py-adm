using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PayNavigator
{
	public class f_FormatViewer : Form
	{
		private IContainer components = null;

		private Panel panel1;

		private Panel panel2;

		private TextBox textBox1;

		public f_FormatViewer()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.panel1 = new Panel();
			this.panel2 = new Panel();
			this.textBox1 = new TextBox();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Dock = DockStyle.Top;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(522, 50);
			this.panel1.TabIndex = 0;
			this.panel2.Controls.Add(this.textBox1);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(0, 50);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(522, 249);
			this.panel2.TabIndex = 0;
			this.textBox1.Dock = DockStyle.Fill;
			this.textBox1.Location = new Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(522, 249);
			this.textBox1.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(522, 299);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Name = "f_FormatViewer";
			this.Text = "f_FormatViewer";
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}