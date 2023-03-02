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
	public class frmGetGMeter : Form
	{
		private GroupBox groupBox1;

		private Label lblContractInfo;

		private GroupBox groupBox2;

		private C1Combo cmbGObject;

		private GroupBox groupBox3;

		private C1Combo cmbGMeter;

		private Button bSelect;

		private Button bCancel;

		private string _account = "";

		private Gmeter _gmeter;

		private Contract _contract;

		private System.ComponentModel.Container components = null;

		public frmGetGMeter(string Account)
		{
			this.InitializeComponent();
			this._account = Account;
		}

		private void bSelect_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
			base.Close();
		}

		private void cmbGObject_TextChanged(object sender, EventArgs e)
		{
			this.CreateGMeter();
		}

		private void CreateGMeter()
		{
			Gmeters item = this._contract.oGobjects[this.cmbGObject.SelectedIndex].oGmeters;
			this.cmbGMeter.ClearItems();
			foreach (Gmeter gmeter in item)
			{
				if (gmeter.oTypeGMeter != null)
				{
					C1Combo c1Combo = this.cmbGMeter;
					string[] serialNumber = new string[] { gmeter.SerialNumber, ", ", gmeter.oStatusGMeter.get_Name(), ", ", gmeter.oTypeGMeter.Fullname };
					c1Combo.AddItem(string.Concat(serialNumber));
				}
				else
				{
					this.cmbGMeter.AddItem(string.Concat(gmeter.SerialNumber, ", ", gmeter.oStatusGMeter.get_Name()));
				}
			}
			if (this.cmbGMeter.ListCount > 0)
			{
				this.cmbGMeter.SelectedIndex = 0;
			}
			this.cmbGMeter.ColumnWidth = this.cmbGMeter.Width - this.cmbGMeter.VScrollBar.Width;
		}

		private void CreateGObject()
		{
			this.cmbGObject.ClearItems();
			foreach (Gobject oGobject in this._contract.oGobjects)
			{
				C1Combo c1Combo = this.cmbGObject;
				string[] name = new string[] { oGobject.Name, ", ", oGobject.oAddress.get_ShortAddress(), ", ", oGobject.oStatusGObject.get_Name() };
				c1Combo.AddItem(string.Concat(name));
			}
			if (this.cmbGObject.ListCount > 0)
			{
				this.cmbGObject.SelectedIndex = 0;
			}
			this.cmbGObject.ColumnWidth = this.cmbGObject.Width - this.cmbGObject.Height;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmGetGMeter_Load(object sender, EventArgs e)
		{
			try
			{
				if (this._account.Length != 0)
				{
					Contracts contract = new Contracts();
					contract.Load(this._account);
					if (contract[0] != null)
					{
						this._contract = contract[0];
						this.lblContractInfo.Text = string.Concat(this._contract.Account, Environment.NewLine);
						Label label = this.lblContractInfo;
						label.Text = string.Concat(label.Text, this._contract.oPerson.FullName, Environment.NewLine);
						Label label1 = this.lblContractInfo;
						label1.Text = string.Concat(label1.Text, this._contract.oPerson.oAddress.get_ShortAddress());
						this.CreateGObject();
					}
				}
			}
			catch
			{
			}
		}

		public void GetGMeter(ref Gmeter gm)
		{
			Gobject item = this._contract.oGobjects[this.cmbGObject.SelectedIndex];
			if (item != null)
			{
				this._gmeter = item.oGmeters[this.cmbGMeter.SelectedIndex];
			}
			gm = this._gmeter;
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmGetGMeter));
			this.groupBox1 = new GroupBox();
			this.lblContractInfo = new Label();
			this.groupBox2 = new GroupBox();
			this.cmbGObject = new C1Combo();
			this.groupBox3 = new GroupBox();
			this.cmbGMeter = new C1Combo();
			this.bSelect = new Button();
			this.bCancel = new Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.cmbGObject).BeginInit();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.cmbGMeter).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.lblContractInfo);
			this.groupBox1.Location = new Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(488, 80);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Абонент";
			this.lblContractInfo.Location = new Point(8, 16);
			this.lblContractInfo.Name = "lblContractInfo";
			this.lblContractInfo.Size = new System.Drawing.Size(472, 56);
			this.lblContractInfo.TabIndex = 0;
			this.groupBox2.Controls.Add(this.cmbGObject);
			this.groupBox2.Location = new Point(8, 88);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(488, 56);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Объект учета";
			this.cmbGObject.AddItemSeparator = ';';
			this.cmbGObject.BorderStyle = 1;
			this.cmbGObject.Caption = "";
			this.cmbGObject.CaptionHeight = 17;
			this.cmbGObject.CharacterCasing = 0;
			this.cmbGObject.ColumnCaptionHeight = 17;
			this.cmbGObject.ColumnFooterHeight = 17;
			this.cmbGObject.ColumnHeaders = false;
			this.cmbGObject.ColumnWidth = 245;
			this.cmbGObject.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbGObject.ContentHeight = 15;
			this.cmbGObject.DataMode = DataModeEnum.AddItem;
			this.cmbGObject.DeadAreaBackColor = Color.Empty;
			this.cmbGObject.EditorBackColor = SystemColors.Window;
			this.cmbGObject.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbGObject.EditorForeColor = SystemColors.WindowText;
			this.cmbGObject.EditorHeight = 15;
			this.cmbGObject.FlatStyle = FlatModeEnum.Flat;
			this.cmbGObject.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbGObject.ItemHeight = 15;
			this.cmbGObject.Location = new Point(8, 24);
			this.cmbGObject.MatchEntryTimeout = (long)2000;
			this.cmbGObject.MaxDropDownItems = 5;
			this.cmbGObject.MaxLength = 32767;
			this.cmbGObject.MouseCursor = Cursors.Default;
			this.cmbGObject.Name = "cmbGObject";
			this.cmbGObject.RowDivider.Color = Color.DarkGray;
			this.cmbGObject.RowDivider.Style = LineStyleEnum.None;
			this.cmbGObject.RowSubDividerColor = Color.DarkGray;
			this.cmbGObject.Size = new System.Drawing.Size(472, 19);
			this.cmbGObject.TabIndex = 1;
			this.cmbGObject.TextChanged += new EventHandler(this.cmbGObject_TextChanged);
			this.cmbGObject.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>245</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.groupBox3.Controls.Add(this.cmbGMeter);
			this.groupBox3.Location = new Point(8, 144);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(488, 56);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Прибор учета";
			this.cmbGMeter.AddItemSeparator = ';';
			this.cmbGMeter.BorderStyle = 1;
			this.cmbGMeter.Caption = "";
			this.cmbGMeter.CaptionHeight = 17;
			this.cmbGMeter.CharacterCasing = 0;
			this.cmbGMeter.ColumnCaptionHeight = 17;
			this.cmbGMeter.ColumnFooterHeight = 17;
			this.cmbGMeter.ColumnHeaders = false;
			this.cmbGMeter.ColumnWidth = 245;
			this.cmbGMeter.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbGMeter.ContentHeight = 15;
			this.cmbGMeter.DataMode = DataModeEnum.AddItem;
			this.cmbGMeter.DeadAreaBackColor = Color.Empty;
			this.cmbGMeter.EditorBackColor = SystemColors.Window;
			this.cmbGMeter.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbGMeter.EditorForeColor = SystemColors.WindowText;
			this.cmbGMeter.EditorHeight = 15;
			this.cmbGMeter.FlatStyle = FlatModeEnum.Flat;
			this.cmbGMeter.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbGMeter.ItemHeight = 15;
			this.cmbGMeter.Location = new Point(8, 24);
			this.cmbGMeter.MatchEntryTimeout = (long)2000;
			this.cmbGMeter.MaxDropDownItems = 5;
			this.cmbGMeter.MaxLength = 32767;
			this.cmbGMeter.MouseCursor = Cursors.Default;
			this.cmbGMeter.Name = "cmbGMeter";
			this.cmbGMeter.RowDivider.Color = Color.DarkGray;
			this.cmbGMeter.RowDivider.Style = LineStyleEnum.None;
			this.cmbGMeter.RowSubDividerColor = Color.DarkGray;
			this.cmbGMeter.Size = new System.Drawing.Size(472, 19);
			this.cmbGMeter.TabIndex = 4;
			this.cmbGMeter.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>245</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.bSelect.Location = new Point(344, 208);
			this.bSelect.Name = "bSelect";
			this.bSelect.TabIndex = 3;
			this.bSelect.Text = "Выбрать";
			this.bSelect.Click += new EventHandler(this.bSelect_Click);
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new Point(422, 208);
			this.bCancel.Name = "bCancel";
			this.bCancel.TabIndex = 4;
			this.bCancel.Text = "Отмена";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(504, 238);
			base.Controls.Add(this.bCancel);
			base.Controls.Add(this.bSelect);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "frmGetGMeter";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Выбор ПУ";
			base.Load += new EventHandler(this.frmGetGMeter_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.cmbGObject).EndInit();
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.cmbGMeter).EndInit();
			base.ResumeLayout(false);
		}
	}
}