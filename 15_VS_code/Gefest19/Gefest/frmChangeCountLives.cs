using C1.Win.C1Input;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmChangeCountLives : Form
	{
		private GroupBox groupBox2;

		private Label lblCountLives;

		private Label label13;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private Label lblAccount;

		private IContainer components;

		private C1TextBox txtCountLives;

		private Label label1;

		private C1DateEdit dtDate;

		private Label label6;

		private ImageList imageList1;

		private ToolTip toolTip1;

		private Button cmdHistory;

		private TextBox txtNote;

		private Label label17;

		private Button cmdClose;

		private Button cmdOK;

		private C1DateEdit dtDate2;

		private CheckBox checkBox;

		private C1TextBox txtNumber;

		private Label label2;

		private Gobject _gobject;

		public frmChangeCountLives(Gobject oGobject)
		{
			this.InitializeComponent();
			this._gobject = oGobject;
		}

		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBox.Checked)
			{
				this.dtDate2.Enabled = true;
				return;
			}
			this.dtDate2.Enabled = false;
		}

		private void checkBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDate2.Focus();
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdHistory_Click(object sender, EventArgs e)
		{
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this.txtCountLives.Text.Length == 0)
					{
						this.txtCountLives.Focus();
						return;
					}
					else if (!((DateTime)this.dtDate.Value > Depot.CurrentPeriod.DateEnd) || MessageBox.Show("Дата документа больше даты окончания периода. Продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						Document document = new Document()
						{
							oBatch = null,
							oContract = this._gobject.oContract,
							oPeriod = Depot.CurrentPeriod,
							oTypeDocument = Depot.oTypeDocuments.item((long)2),
							DocumentAmount = Convert.ToDouble(this.txtCountLives.Text),
							DocumentDate = (DateTime)this.dtDate.Value,
							DocumentNumber = this.txtNumber.Text,
							Note = this.txtNote.Text
						};
						if (document.Save() != 0)
						{
							document.Delete();
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						else
						{
							PD str = document.oPDs.Add();
							str.oTypePD = Depot.oTypePDs.item((long)5);
							str.oDocument = document;
							str.Value = this._gobject.CountLives.ToString();
							if (str.Save() == 0)
							{
								this._gobject.CountLives = Convert.ToInt32(this.txtCountLives.Text);
								if (this._gobject.Save() == 0)
								{
									if (this.checkBox.Checked)
									{
										str = document.oPDs.Add();
										str.oTypePD = Depot.oTypePDs.item((long)6);
										str.oDocument = document;
										str.Value = ((DateTime)this.dtDate2.Value).ToShortDateString();
										if (str.Save() != 0)
										{
											document.Delete();
											MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
									}
									base.Close();
									return;
								}
								else
								{
									document.Delete();
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							else
							{
								document.Delete();
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
					}
					else
					{
						return;
					}
				}
				catch
				{
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
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

		private void dtDate_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNumber.Focus();
			}
		}

		private void dtDate2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNote.Focus();
			}
		}

		private void frmChangeCountLives_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
			this._gobject = null;
		}

		private void frmChangeCountLives_Load(object sender, EventArgs e)
		{
			try
			{
				if (this._gobject == null)
				{
					base.Close();
				}
				else if (this._gobject.oContract != null)
				{
					C1DateEdit date = this.dtDate;
					DateTime today = DateTime.Today;
					date.Value = today.Date;
					C1DateEdit c1DateEdit = this.dtDate2;
					today = DateTime.Today;
					c1DateEdit.Value = today.Date;
					this.lblAccount.Text = this._gobject.oContract.Account;
					this.lblFIO.Text = this._gobject.oContract.oPerson.FullName;
					this.lblAddress.Text = this._gobject.oContract.oPerson.oAddress.get_ShortAddress();
					this.lblCountLives.Text = this._gobject.CountLives.ToString();
					this.checkBox_CheckedChanged(null, null);
				}
				else
				{
					base.Close();
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmChangeCountLives));
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblCountLives = new Label();
			this.label13 = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.txtCountLives = new C1TextBox();
			this.label1 = new Label();
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.imageList1 = new ImageList(this.components);
			this.toolTip1 = new ToolTip(this.components);
			this.cmdHistory = new Button();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.checkBox = new CheckBox();
			this.dtDate2 = new C1DateEdit();
			this.txtNumber = new C1TextBox();
			this.label2 = new Label();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtCountLives).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.dtDate2).BeginInit();
			((ISupportInitialize)this.txtNumber).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.lblAccount);
			this.groupBox2.Controls.Add(this.lblCountLives);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(2, 56);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(288, 120);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.lblAccount.BackColor = SystemColors.Info;
			this.lblAccount.BorderStyle = BorderStyle.FixedSingle;
			this.lblAccount.ForeColor = SystemColors.ControlText;
			this.lblAccount.Location = new Point(56, 16);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(112, 20);
			this.lblAccount.TabIndex = 11;
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(80, 88);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 10;
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 88);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Проживает";
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(56, 64);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(224, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(56, 40);
			this.lblFIO.Name = "lblFIO";
			this.lblFIO.Size = new System.Drawing.Size(224, 20);
			this.lblFIO.TabIndex = 4;
			this.label10.ForeColor = SystemColors.ControlText;
			this.label10.Location = new Point(8, 64);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 16);
			this.label10.TabIndex = 2;
			this.label10.Text = "Адрес";
			this.label11.ForeColor = SystemColors.ControlText;
			this.label11.Location = new Point(8, 40);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 16);
			this.label11.TabIndex = 1;
			this.label11.Text = "ФИО";
			this.label12.ForeColor = SystemColors.ControlText;
			this.label12.Location = new Point(8, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 16);
			this.label12.TabIndex = 0;
			this.label12.Text = "Л/с";
			this.txtCountLives.BorderStyle = 1;
			this.txtCountLives.EditMask = "###";
			this.txtCountLives.Location = new Point(128, 184);
			this.txtCountLives.Multiline = true;
			this.txtCountLives.Name = "txtCountLives";
			this.txtCountLives.Size = new System.Drawing.Size(44, 20);
			this.txtCountLives.TabIndex = 3;
			this.txtCountLives.Tag = null;
			this.txtCountLives.KeyPress += new KeyPressEventHandler(this.txtCountLives_KeyPress);
			this.txtCountLives.Enter += new EventHandler(this.txtCountLives_Enter);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 184);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 51;
			this.label1.Text = "Новая численность";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(128, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 0;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.KeyPress += new KeyPressEventHandler(this.dtDate_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 16);
			this.label6.TabIndex = 53;
			this.label6.Text = "Дата документа";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.cmdHistory.FlatStyle = FlatStyle.Flat;
			this.cmdHistory.ForeColor = SystemColors.ControlText;
			this.cmdHistory.ImageIndex = 0;
			this.cmdHistory.ImageList = this.imageList1;
			this.cmdHistory.Location = new Point(128, 144);
			this.cmdHistory.Name = "cmdHistory";
			this.cmdHistory.Size = new System.Drawing.Size(20, 20);
			this.cmdHistory.TabIndex = 1;
			this.toolTip1.SetToolTip(this.cmdHistory, "Просмотр истории");
			this.cmdHistory.Visible = false;
			this.cmdHistory.Click += new EventHandler(this.cmdHistory_Click);
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 256);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(276, 32);
			this.txtNote.TabIndex = 6;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 240);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 64;
			this.label17.Text = "Примечание";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(192, 296);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 8;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(88, 296);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.checkBox.FlatStyle = FlatStyle.Flat;
			this.checkBox.ForeColor = SystemColors.ControlText;
			this.checkBox.Location = new Point(8, 208);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new System.Drawing.Size(104, 16);
			this.checkBox.TabIndex = 4;
			this.checkBox.Text = "По период";
			this.checkBox.KeyPress += new KeyPressEventHandler(this.checkBox_KeyPress);
			this.checkBox.CheckedChanged += new EventHandler(this.checkBox_CheckedChanged);
			this.dtDate2.BorderStyle = 1;
			this.dtDate2.FormatType = FormatTypeEnum.YearAndMonth;
			this.dtDate2.Location = new Point(128, 208);
			this.dtDate2.Name = "dtDate2";
			this.dtDate2.Size = new System.Drawing.Size(152, 18);
			this.dtDate2.TabIndex = 5;
			this.dtDate2.Tag = null;
			this.dtDate2.Value = new DateTime(2006, 11, 15, 0, 0, 0, 0);
			this.dtDate2.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate2.KeyPress += new KeyPressEventHandler(this.dtDate2_KeyPress);
			this.txtNumber.BorderStyle = 1;
			this.txtNumber.Location = new Point(128, 32);
			this.txtNumber.MaxLength = 20;
			this.txtNumber.Multiline = true;
			this.txtNumber.Name = "txtNumber";
			this.txtNumber.Size = new System.Drawing.Size(152, 20);
			this.txtNumber.TabIndex = 1;
			this.txtNumber.Tag = null;
			this.txtNumber.KeyPress += new KeyPressEventHandler(this.txtNumber_KeyPress);
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 66;
			this.label2.Text = "Номер документа:";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(292, 327);
			base.Controls.Add(this.txtNumber);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.dtDate2);
			base.Controls.Add(this.checkBox);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdHistory);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.txtCountLives);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmChangeCountLives";
			this.Text = "Изменение численности";
			base.Closing += new CancelEventHandler(this.frmChangeCountLives_Closing);
			base.Load += new EventHandler(this.frmChangeCountLives_Load);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtCountLives).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.dtDate2).EndInit();
			((ISupportInitialize)this.txtNumber).EndInit();
			base.ResumeLayout(false);
		}

		private void txtCountLives_Enter(object sender, EventArgs e)
		{
			this.txtCountLives.SelectAll();
		}

		private void txtCountLives_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.checkBox.Focus();
			}
		}

		private void txtNote_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdOK.Focus();
			}
		}

		private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtCountLives.Focus();
			}
		}
	}
}