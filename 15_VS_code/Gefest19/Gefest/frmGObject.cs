using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmGObject : Form
	{
		private GroupBox groupBox2;

		private Label label13;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private ImageList imageList1;

		private Label label2;

		private Button cmdTypeGObject;

		private C1Combo cmbTypeGObject;

		private Label lblAddress2;

		private Label label3;

		private Button cmdAddress;

		private Button cmdGRU;

		private Label label4;

		private TextBox txtInvNumber;

		private Button cmdClose;

		private Label label17;

		private Button cmdOK;

		private ToolTip toolTip1;

		private IContainer components;

		private Label lblOU;

		private Label lblNameGRU;

		private TextBox txtNote;

		private Label lblAccount;

		private Label lblCountLives;

		private Button cmdCountLives;

		private TypeGobjects _typegobjects;

		private Gobject _gobject;

		public frmGObject(Gobject oGobject)
		{
			this.InitializeComponent();
			this._gobject = oGobject;
		}

		private void cmdAddress_Click(object sender, EventArgs e)
		{
			frmAddress _frmAddress = new frmAddress()
			{
				oAddress = this._gobject.oAddress
			};
			_frmAddress.ShowDialog(this);
			if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				this._gobject.oAddress = _frmAddress.oAddress;
				this.lblAddress2.Text = this._gobject.oAddress.get_ShortAddress();
			}
			_frmAddress = null;
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdCountLives_Click(object sender, EventArgs e)
		{
			(new frmChangeCountLives(this._gobject)).ShowDialog(this);
		}

		private void cmdGRU_Click(object sender, EventArgs e)
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
				this._gobject.oGRU = gRU.item(Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
				this.txtInvNumber.Text = this._gobject.oGRU.InvNumber;
				this.txtInvNumber.ForeColor = SystemColors.WindowText;
				this.lblNameGRU.Text = this._gobject.oGRU.get_Name();
			}
			frmSimpleObj = null;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._gobject.oGRU == null || this._gobject.oAddress == null || this.cmbTypeGObject.SelectedIndex == 1)
					{
						MessageBox.Show("Необходимо указать РУ, адрес, тип ОУ!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					else
					{
						this._gobject.oTypeGobject = this._typegobjects[this.cmbTypeGObject.SelectedIndex];
						this._gobject.Name = "Квартира";
						this._gobject.Memo = this.txtNote.Text;
						if (this._gobject.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							base.Close();
						}
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

		private void cmdTypeGObject_Click(object sender, EventArgs e)
		{
			TypeGobjects typeGobject = this._typegobjects;
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник объектов учета", typeGobject, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			this._typegobjects = new TypeGobjects();
			this._typegobjects.Load();
			if (frmSimpleObj.lvData.SelectedItems.Count <= 0)
			{
				Tools.FillC1(this._typegobjects, this.cmbTypeGObject, (long)0);
			}
			else
			{
				Tools.FillC1(this._typegobjects, this.cmbTypeGObject, Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
			}
			frmSimpleObj = null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmGObject_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmGObject_Load(object sender, EventArgs e)
		{
			try
			{
				if (this._gobject == null)
				{
					base.Close();
				}
				else if (this._gobject.get_ID() != (long)0)
				{
					Tools.LoadWindows(this);
					this._typegobjects = new TypeGobjects();
					this._typegobjects.Load();
					this.lblAccount.Text = this._gobject.oContract.Account;
					this.lblFIO.Text = this._gobject.oContract.oPerson.FullName;
					this.lblAddress.Text = this._gobject.oContract.oPerson.oAddress.get_ShortAddress();
					Tools.FillC1(this._typegobjects, this.cmbTypeGObject, this._gobject.oTypeGobject.get_ID());
					this.lblAddress2.Text = this._gobject.oAddress.get_ShortAddress();
					this.lblNameGRU.Text = this._gobject.oGRU.get_Name();
					this.txtInvNumber.Text = this._gobject.oGRU.InvNumber;
					this.lblCountLives.Text = this._gobject.CountLives.ToString();
					this.lblOU.Text = this._gobject.oStatusGObject.get_Name();
					this.txtNote.Text = this._gobject.Memo;
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

		private void GetGRU()
		{
			GRUs gRU = new GRUs();
			gRU.Load();
			this._gobject.oGRU = null;
			foreach (GRU gRU1 in gRU)
			{
				if (gRU1.InvNumber != this.txtInvNumber.Text.Trim())
				{
					continue;
				}
				this._gobject.oGRU = gRU1;
				this.txtInvNumber.Text = this._gobject.oGRU.InvNumber;
				this.txtInvNumber.ForeColor = SystemColors.WindowText;
				this.lblNameGRU.Text = this._gobject.oGRU.get_Name();
				return;
			}
			this.lblNameGRU.Text = "Укажите номер РУ";
			this.txtInvNumber.ForeColor = Color.Red;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmGObject));
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.imageList1 = new ImageList(this.components);
			this.lblOU = new Label();
			this.label13 = new Label();
			this.cmbTypeGObject = new C1Combo();
			this.label2 = new Label();
			this.cmdTypeGObject = new Button();
			this.lblAddress2 = new Label();
			this.label3 = new Label();
			this.cmdAddress = new Button();
			this.cmdGRU = new Button();
			this.lblNameGRU = new Label();
			this.label4 = new Label();
			this.txtInvNumber = new TextBox();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.lblCountLives = new Label();
			this.cmdCountLives = new Button();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.cmbTypeGObject).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.lblAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 96);
			this.groupBox2.TabIndex = 1;
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
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lblOU.Cursor = Cursors.Hand;
			this.lblOU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Underline, GraphicsUnit.Point, 204);
			this.lblOU.ForeColor = SystemColors.Desktop;
			this.lblOU.Location = new Point(160, 192);
			this.lblOU.Name = "lblOU";
			this.lblOU.Size = new System.Drawing.Size(120, 20);
			this.lblOU.TabIndex = 6;
			this.toolTip1.SetToolTip(this.lblOU, "Статус ОУ");
			this.lblOU.Click += new EventHandler(this.lblOU_Click);
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 192);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Проживает";
			this.cmbTypeGObject.AddItemSeparator = ';';
			this.cmbTypeGObject.BorderStyle = 1;
			this.cmbTypeGObject.Caption = "";
			this.cmbTypeGObject.CaptionHeight = 17;
			this.cmbTypeGObject.CharacterCasing = 0;
			this.cmbTypeGObject.ColumnCaptionHeight = 17;
			this.cmbTypeGObject.ColumnFooterHeight = 17;
			this.cmbTypeGObject.ColumnHeaders = false;
			this.cmbTypeGObject.ColumnWidth = 100;
			this.cmbTypeGObject.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeGObject.ContentHeight = 15;
			this.cmbTypeGObject.DataMode = DataModeEnum.AddItem;
			this.cmbTypeGObject.DeadAreaBackColor = Color.Empty;
			this.cmbTypeGObject.EditorBackColor = SystemColors.Window;
			this.cmbTypeGObject.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeGObject.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeGObject.EditorHeight = 15;
			this.cmbTypeGObject.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeGObject.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbTypeGObject.ItemHeight = 15;
			this.cmbTypeGObject.Location = new Point(52, 112);
			this.cmbTypeGObject.MatchEntryTimeout = (long)2000;
			this.cmbTypeGObject.MaxDropDownItems = 5;
			this.cmbTypeGObject.MaxLength = 32767;
			this.cmbTypeGObject.MouseCursor = Cursors.Default;
			this.cmbTypeGObject.Name = "cmbTypeGObject";
			this.cmbTypeGObject.RowDivider.Color = Color.DarkGray;
			this.cmbTypeGObject.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeGObject.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeGObject.Size = new System.Drawing.Size(204, 19);
			this.cmbTypeGObject.TabIndex = 1;
			this.cmbTypeGObject.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label2.Location = new Point(8, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 20;
			this.label2.Text = "Тип";
			this.cmdTypeGObject.FlatStyle = FlatStyle.Flat;
			this.cmdTypeGObject.ForeColor = SystemColors.ControlText;
			this.cmdTypeGObject.ImageIndex = 0;
			this.cmdTypeGObject.ImageList = this.imageList1;
			this.cmdTypeGObject.Location = new Point(264, 112);
			this.cmdTypeGObject.Name = "cmdTypeGObject";
			this.cmdTypeGObject.Size = new System.Drawing.Size(20, 20);
			this.cmdTypeGObject.TabIndex = 2;
			this.cmdTypeGObject.Click += new EventHandler(this.cmdTypeGObject_Click);
			this.lblAddress2.BackColor = SystemColors.Info;
			this.lblAddress2.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress2.ForeColor = SystemColors.ControlText;
			this.lblAddress2.Location = new Point(52, 136);
			this.lblAddress2.Name = "lblAddress2";
			this.lblAddress2.Size = new System.Drawing.Size(204, 20);
			this.lblAddress2.TabIndex = 24;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 23;
			this.label3.Text = "Адрес";
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.ImageIndex = 0;
			this.cmdAddress.ImageList = this.imageList1;
			this.cmdAddress.Location = new Point(264, 136);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(20, 20);
			this.cmdAddress.TabIndex = 3;
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.cmdGRU.FlatStyle = FlatStyle.Flat;
			this.cmdGRU.ForeColor = SystemColors.ControlText;
			this.cmdGRU.ImageIndex = 0;
			this.cmdGRU.ImageList = this.imageList1;
			this.cmdGRU.Location = new Point(264, 160);
			this.cmdGRU.Name = "cmdGRU";
			this.cmdGRU.Size = new System.Drawing.Size(20, 20);
			this.cmdGRU.TabIndex = 5;
			this.cmdGRU.Click += new EventHandler(this.cmdGRU_Click);
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(96, 160);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(160, 20);
			this.lblNameGRU.TabIndex = 27;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 26;
			this.label4.Text = "РУ";
			this.txtInvNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtInvNumber.Location = new Point(52, 160);
			this.txtInvNumber.Name = "txtInvNumber";
			this.txtInvNumber.Size = new System.Drawing.Size(36, 20);
			this.txtInvNumber.TabIndex = 4;
			this.txtInvNumber.Text = "";
			this.txtInvNumber.Leave += new EventHandler(this.txtInvNumber_Leave);
			this.txtInvNumber.KeyUp += new KeyEventHandler(this.txtInvNumber_KeyUp);
			this.txtInvNumber.Enter += new EventHandler(this.txtInvNumber_Enter);
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(192, 272);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 9;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(88, 272);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 8;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 232);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(276, 32);
			this.txtNote.TabIndex = 7;
			this.txtNote.Text = "";
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 216);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 32;
			this.label17.Text = "Примечание";
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(80, 192);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 33;
			this.cmdCountLives.FlatStyle = FlatStyle.Flat;
			this.cmdCountLives.ForeColor = SystemColors.ControlText;
			this.cmdCountLives.ImageIndex = 0;
			this.cmdCountLives.ImageList = this.imageList1;
			this.cmdCountLives.Location = new Point(128, 192);
			this.cmdCountLives.Name = "cmdCountLives";
			this.cmdCountLives.Size = new System.Drawing.Size(20, 20);
			this.cmdCountLives.TabIndex = 34;
			this.cmdCountLives.Click += new EventHandler(this.cmdCountLives_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(288, 301);
			base.Controls.Add(this.cmdCountLives);
			base.Controls.Add(this.lblCountLives);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.txtInvNumber);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.cmdGRU);
			base.Controls.Add(this.lblNameGRU);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.cmdAddress);
			base.Controls.Add(this.lblAddress2);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.cmdTypeGObject);
			base.Controls.Add(this.cmbTypeGObject);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.label13);
			base.Controls.Add(this.lblOU);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmGObject";
			this.Text = "Объект учета";
			base.Closing += new CancelEventHandler(this.frmGObject_Closing);
			base.Load += new EventHandler(this.frmGObject_Load);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.cmbTypeGObject).EndInit();
			base.ResumeLayout(false);
		}

		private void lblOU_Click(object sender, EventArgs e)
		{
			(new frmActionGObject(this._gobject)).ShowDialog(this);
			this.lblOU.Text = this._gobject.oStatusGObject.get_Name();
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