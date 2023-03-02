using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmClaimDoc : Form
	{
		private GroupBox groupBox2;

		private Label lblAccount;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private C1DateEdit dtDate;

		private Label label6;

		private NumericUpDown numAmount;

		private Label label2;

		private C1DateEdit dtDate2;

		private Label label1;

		private TextBox txtNote;

		private Label label17;

		private Button cmdClose;

		private Button cmdOK;

		private C1Combo cmbStatus;

		private Label label16;

		private System.ComponentModel.Container components = null;

		private Contract _contract;

		private Document _doc;

		private PD _pd23;

		private PD _pd24;

		public frmClaimDoc(Document oDocument)
		{
			this.InitializeComponent();
			this._doc = oDocument;
		}

		private void cmbStatus_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNote.Focus();
			}
		}

		private void cmbStatus_TextChanged(object sender, EventArgs e)
		{
			if (this.dtDate2.ValueIsDbNull)
			{
				this.cmbStatus.SelectedIndex = 0;
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			PD shortDateString;
			PD str;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract != null)
					{
						if (this._doc.get_ID() == (long)0)
						{
							this._doc.oBatch = null;
							this._doc.oContract = this._contract;
							this._doc.oPeriod = Depot.CurrentPeriod;
							this._doc.oTypeDocument = Depot.oTypeDocuments.item((long)10);
						}
						this._doc.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
						this._doc.Note = this.txtNote.Text;
						if (this._doc.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						else
						{
							if (!this.dtDate2.ValueIsDbNull)
							{
								if (this._pd23 == null)
								{
									shortDateString = this._doc.oPDs.Add();
									shortDateString.oTypePD = Depot.oTypePDs.item((long)23);
									shortDateString.oDocument = this._doc;
								}
								else
								{
									shortDateString = this._pd23;
								}
								shortDateString.Value = ((DateTime)this.dtDate2.Value).ToShortDateString();
								if (shortDateString.Save() != 0)
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							if (this.cmbStatus.SelectedIndex > 0)
							{
								if (this._pd24 == null)
								{
									str = this._doc.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)24);
									str.oDocument = this._doc;
								}
								else
								{
									str = this._pd24;
								}
								str.Value = this.cmbStatus.SelectedIndex.ToString();
								if (str.Save() != 0)
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							base.DialogResult = System.Windows.Forms.DialogResult.OK;
							base.Close();
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

		private void dtDate2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbStatus.Focus();
			}
		}

		private void dtDate2_TextChanged(object sender, EventArgs e)
		{
			if (this.dtDate2.ValueIsDbNull)
			{
				this.cmbStatus.SelectedIndex = 0;
			}
		}

		private void frmClaimDoc_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmClaimDoc_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				this.cmbStatus.AddItem("Активна");
				this.cmbStatus.AddItem("Закрыта");
				this.cmbStatus.ColumnWidth = this.cmbStatus.Width - this.cmbStatus.Height;
				if (this._doc.get_ID() != (long)0)
				{
					this.dtDate.Value = this._doc.DocumentDate;
					this._contract = this._doc.oContract;
					this.lblAccount.Text = this._contract.Account;
					this.lblFIO.Text = this._contract.oPerson.FullName;
					this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
					this.numAmount.Value = Convert.ToDecimal(this._doc.DocumentAmount);
					this.txtNote.Text = this._doc.Note;
					if (this._doc.GetPD(23) != null)
					{
						this._pd23 = this._doc.GetPD(23);
						this.dtDate2.Value = Convert.ToDateTime(this._pd23.Value);
					}
					if (this._doc.GetPD(24) == null)
					{
						this.cmbStatus.SelectedIndex = 0;
					}
					else
					{
						this._pd24 = this._doc.GetPD(24);
						this.cmbStatus.SelectedIndex = Convert.ToInt32(this._pd24.Value);
					}
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmClaimDoc));
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.dtDate = new C1DateEdit();
			this.label6 = new Label();
			this.numAmount = new NumericUpDown();
			this.label2 = new Label();
			this.dtDate2 = new C1DateEdit();
			this.label1 = new Label();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.cmbStatus = new C1Combo();
			this.label16 = new Label();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.numAmount).BeginInit();
			((ISupportInitialize)this.dtDate2).BeginInit();
			((ISupportInitialize)this.cmbStatus).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.lblAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 96);
			this.groupBox2.TabIndex = 44;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.lblAccount.BackColor = SystemColors.Info;
			this.lblAccount.BorderStyle = BorderStyle.FixedSingle;
			this.lblAccount.ForeColor = SystemColors.ControlText;
			this.lblAccount.Location = new Point(48, 16);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(112, 20);
			this.lblAccount.TabIndex = 6;
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(48, 64);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(224, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(48, 40);
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
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(129, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.ReadOnly = true;
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 59;
			this.dtDate.TabStop = false;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(9, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 16);
			this.label6.TabIndex = 60;
			this.label6.Text = "Дата документа";
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Enabled = false;
			this.numAmount.Location = new Point(124, 136);
			NumericUpDown num = this.numAmount;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown = this.numAmount;
			numArray = new int[] { 9999999, 0, 0, -2147483648 };
			numericUpDown.Minimum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.ReadOnly = true;
			this.numAmount.Size = new System.Drawing.Size(152, 20);
			this.numAmount.TabIndex = 73;
			this.numAmount.TabStop = false;
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(7, 160);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 16);
			this.label2.TabIndex = 80;
			this.label2.Text = "Вручена";
			this.dtDate2.BorderStyle = 1;
			this.dtDate2.EmptyAsNull = true;
			this.dtDate2.FormatType = FormatTypeEnum.LongDate;
			this.dtDate2.Location = new Point(124, 160);
			this.dtDate2.Name = "dtDate2";
			this.dtDate2.Size = new System.Drawing.Size(152, 18);
			this.dtDate2.TabIndex = 1;
			this.dtDate2.Tag = null;
			this.dtDate2.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate2.TextChanged += new EventHandler(this.dtDate2_TextChanged);
			this.dtDate2.KeyPress += new KeyPressEventHandler(this.dtDate2_KeyPress);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(7, 136);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 16);
			this.label1.TabIndex = 79;
			this.label1.Text = "Сумма";
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(7, 232);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(276, 32);
			this.txtNote.TabIndex = 3;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(7, 216);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 78;
			this.label17.Text = "Примечание";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(191, 272);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 5;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(87, 272);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.cmbStatus.AddItemSeparator = ';';
			this.cmbStatus.BorderStyle = 1;
			this.cmbStatus.Caption = "";
			this.cmbStatus.CaptionHeight = 17;
			this.cmbStatus.CharacterCasing = 0;
			this.cmbStatus.ColumnCaptionHeight = 17;
			this.cmbStatus.ColumnFooterHeight = 17;
			this.cmbStatus.ColumnHeaders = false;
			this.cmbStatus.ColumnWidth = 100;
			this.cmbStatus.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbStatus.ContentHeight = 15;
			this.cmbStatus.DataMode = DataModeEnum.AddItem;
			this.cmbStatus.DeadAreaBackColor = Color.Empty;
			this.cmbStatus.EditorBackColor = SystemColors.Window;
			this.cmbStatus.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbStatus.EditorForeColor = SystemColors.WindowText;
			this.cmbStatus.EditorHeight = 15;
			this.cmbStatus.FlatStyle = FlatModeEnum.Flat;
			this.cmbStatus.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbStatus.ItemHeight = 15;
			this.cmbStatus.Location = new Point(124, 184);
			this.cmbStatus.MatchEntryTimeout = (long)2000;
			this.cmbStatus.MaxDropDownItems = 5;
			this.cmbStatus.MaxLength = 32767;
			this.cmbStatus.MouseCursor = Cursors.Default;
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.RowDivider.Color = Color.DarkGray;
			this.cmbStatus.RowDivider.Style = LineStyleEnum.None;
			this.cmbStatus.RowSubDividerColor = Color.DarkGray;
			this.cmbStatus.Size = new System.Drawing.Size(152, 19);
			this.cmbStatus.TabIndex = 2;
			this.cmbStatus.TextChanged += new EventHandler(this.cmbStatus_TextChanged);
			this.cmbStatus.KeyPress += new KeyPressEventHandler(this.cmbStatus_KeyPress);
			this.cmbStatus.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label16.ForeColor = SystemColors.ControlText;
			this.label16.Location = new Point(8, 184);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(80, 16);
			this.label16.TabIndex = 98;
			this.label16.Text = "Статус";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(290, 303);
			base.Controls.Add(this.cmbStatus);
			base.Controls.Add(this.label16);
			base.Controls.Add(this.numAmount);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.dtDate2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.dtDate);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmClaimDoc";
			this.Text = "Претензия";
			base.Closing += new CancelEventHandler(this.frmClaimDoc_Closing);
			base.Load += new EventHandler(this.frmClaimDoc_Load);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.numAmount).EndInit();
			((ISupportInitialize)this.dtDate2).EndInit();
			((ISupportInitialize)this.cmbStatus).EndInit();
			base.ResumeLayout(false);
		}

		private void txtNote_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdOK.Focus();
			}
		}
	}
}