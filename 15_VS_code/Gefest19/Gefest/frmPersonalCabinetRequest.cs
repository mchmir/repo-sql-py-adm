using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPersonalCabinetRequest : Form
	{
		private Label label4;

		private TextBox txtAccount;

		private TextBox txtFIO;

		private TextBox txtAddress;

		private TextBox txtPhone;

		private TextBox txtEMail;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label5;

		private C1Combo cmbStatus;

		private Label label6;

		private Button bOK;

		private System.ComponentModel.Container components = null;

		private Button bCancel;

		private PersonalCabinetRequest _request;

		public frmPersonalCabinetRequest(PersonalCabinetRequest request)
		{
			this._request = request;
			this.InitializeComponent();
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			this._request.State = this.cmbStatus.SelectedIndex + 1;
			this._request.NeedSendMessage = 1;
			this._request.Save();
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
			base.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			base.Close();
		}

		private void CreateStatus(int _status)
		{
			this.cmbStatus.ClearItems();
			this.cmbStatus.AddItem("Оформлена");
			this.cmbStatus.AddItem("Принята (у/л)");
			this.cmbStatus.AddItem("Принята (все документы)");
			this.cmbStatus.AddItem("Завершена");
			this.cmbStatus.SelectedIndex = _status - 1;
			this.cmbStatus.ColumnWidth = this.cmbStatus.Width - this.cmbStatus.Height;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPersonalCabinetRequest_Load(object sender, EventArgs e)
		{
			this.txtAccount.Text = this._request.Account;
			this.txtAddress.Text = this._request.AdresHome;
			this.txtEMail.Text = this._request.Email;
			this.txtFIO.Text = this._request.FIO;
			this.txtPhone.Text = this._request.Phone;
			this.CreateStatus(this._request.State);
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmPersonalCabinetRequest));
			this.txtAccount = new TextBox();
			this.txtFIO = new TextBox();
			this.txtAddress = new TextBox();
			this.txtPhone = new TextBox();
			this.txtEMail = new TextBox();
			this.label4 = new Label();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label5 = new Label();
			this.cmbStatus = new C1Combo();
			this.label6 = new Label();
			this.bOK = new Button();
			this.bCancel = new Button();
			((ISupportInitialize)this.cmbStatus).BeginInit();
			base.SuspendLayout();
			this.txtAccount.BackColor = SystemColors.Info;
			this.txtAccount.BorderStyle = BorderStyle.FixedSingle;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(120, 8);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.ReadOnly = true;
			this.txtAccount.Size = new System.Drawing.Size(288, 20);
			this.txtAccount.TabIndex = 3;
			this.txtAccount.Text = "";
			this.txtFIO.BackColor = SystemColors.Info;
			this.txtFIO.BorderStyle = BorderStyle.FixedSingle;
			this.txtFIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtFIO.Location = new Point(120, 32);
			this.txtFIO.Name = "txtFIO";
			this.txtFIO.ReadOnly = true;
			this.txtFIO.Size = new System.Drawing.Size(288, 20);
			this.txtFIO.TabIndex = 4;
			this.txtFIO.Text = "";
			this.txtAddress.BackColor = SystemColors.Info;
			this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
			this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAddress.Location = new Point(120, 56);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.ReadOnly = true;
			this.txtAddress.Size = new System.Drawing.Size(288, 20);
			this.txtAddress.TabIndex = 5;
			this.txtAddress.Text = "";
			this.txtPhone.BackColor = SystemColors.Info;
			this.txtPhone.BorderStyle = BorderStyle.FixedSingle;
			this.txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtPhone.Location = new Point(120, 80);
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.ReadOnly = true;
			this.txtPhone.Size = new System.Drawing.Size(288, 20);
			this.txtPhone.TabIndex = 6;
			this.txtPhone.Text = "";
			this.txtEMail.BackColor = SystemColors.Info;
			this.txtEMail.BorderStyle = BorderStyle.FixedSingle;
			this.txtEMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtEMail.Location = new Point(120, 104);
			this.txtEMail.Name = "txtEMail";
			this.txtEMail.ReadOnly = true;
			this.txtEMail.Size = new System.Drawing.Size(288, 20);
			this.txtEMail.TabIndex = 7;
			this.txtEMail.Text = "";
			this.label4.Location = new Point(24, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "Лицевой счет:";
			this.label1.Location = new Point(24, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "ФИО:";
			this.label2.Location = new Point(24, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "Адрес:";
			this.label3.Location = new Point(24, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "Телефон:";
			this.label5.Location = new Point(24, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 12;
			this.label5.Text = "E-mail:";
			this.cmbStatus.AddItemSeparator = ';';
			this.cmbStatus.BorderStyle = 1;
			this.cmbStatus.Caption = "";
			this.cmbStatus.CaptionHeight = 17;
			this.cmbStatus.CharacterCasing = 0;
			this.cmbStatus.ColumnCaptionHeight = 17;
			this.cmbStatus.ColumnFooterHeight = 17;
			this.cmbStatus.ColumnHeaders = false;
			this.cmbStatus.ColumnWidth = 149;
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
			this.cmbStatus.Location = new Point(120, 128);
			this.cmbStatus.MatchEntryTimeout = (long)2000;
			this.cmbStatus.MaxDropDownItems = 5;
			this.cmbStatus.MaxLength = 32767;
			this.cmbStatus.MouseCursor = Cursors.Default;
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.RowDivider.Color = Color.DarkGray;
			this.cmbStatus.RowDivider.Style = LineStyleEnum.None;
			this.cmbStatus.RowSubDividerColor = Color.DarkGray;
			this.cmbStatus.Size = new System.Drawing.Size(288, 19);
			this.cmbStatus.TabIndex = 13;
			this.cmbStatus.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>149</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label6.Location = new Point(24, 128);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 16);
			this.label6.TabIndex = 14;
			this.label6.Text = "Статус:";
			this.bOK.Location = new Point(256, 160);
			this.bOK.Name = "bOK";
			this.bOK.TabIndex = 15;
			this.bOK.Text = "Сохранить";
			this.bOK.Click += new EventHandler(this.bOK_Click);
			this.bCancel.Location = new Point(336, 160);
			this.bCancel.Name = "bCancel";
			this.bCancel.TabIndex = 16;
			this.bCancel.Text = "Отмена";
			this.bCancel.Click += new EventHandler(this.button1_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(416, 190);
			base.Controls.Add(this.bCancel);
			base.Controls.Add(this.bOK);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.cmbStatus);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.txtEMail);
			base.Controls.Add(this.txtPhone);
			base.Controls.Add(this.txtAddress);
			base.Controls.Add(this.txtFIO);
			base.Controls.Add(this.txtAccount);
			base.Name = "frmPersonalCabinetRequest";
			this.Text = "Заявка на доступ в Личный Кабинет Абонента";
			base.Load += new EventHandler(this.frmPersonalCabinetRequest_Load);
			((ISupportInitialize)this.cmbStatus).EndInit();
			base.ResumeLayout(false);
		}
	}
}