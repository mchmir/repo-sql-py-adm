using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmUnloadingInTextFile : Form
	{
		private Label label5;

		private C1Combo cmbAgent;

		private Label label4;

		private Button cmdOk;

		private Button cmdCancel;

		private System.ComponentModel.Container components = null;

		private Correspondents _Correspondents;

		private C1Combo cmbSendig;

		private Sendings _Sendings;

		public frmUnloadingInTextFile()
		{
			this.InitializeComponent();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			try
			{
				frmLoad _frmLoad = new frmLoad("Формирование файла...");
				_frmLoad.Show();
				_frmLoad.Refresh();
				int d = 0;
				if (this.cmbSendig.SelectedIndex != 0)
				{
					d = (int)this._Sendings[this.cmbSendig.SelectedIndex - 1].get_ID();
				}
				SqlParameter sqlParameter = new SqlParameter("@IDSending", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = d
				};
				SqlParameter sqlParameter1 = new SqlParameter("@idcorrespondent", SqlDbType.Int)
				{
					Direction = ParameterDirection.Input,
					Value = (int)this._Correspondents[this.cmbAgent.SelectedIndex].get_ID()
				};
				_frmLoad.Refresh();
				SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1 };
				Saver.ExecuteProcedure("spUnloadingInTextFile", sqlParameterArray);
				_frmLoad.Close();
				_frmLoad = null;
				Depot.oSending = null;
				MessageBox.Show("Формирование файла закончено", "Формирование файла", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				base.Close();
			}
			catch (Exception exception)
			{
				MessageBox.Show("Ошибка формирование файла...", "Формирование файла", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void cmdView_Click(object sender, EventArgs e)
		{
			if ((new FolderBrowserDialog()
			{
				Description = "Выбор пути к отчетам"
			}).ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				base.Invalidate();
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

		private void frmUnloadingInTextFile_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmUnloadingInTextFile_Load(object sender, EventArgs e)
		{
			Tools.LoadWindows(this);
			this._Correspondents = new Correspondents();
			this._Correspondents.Load();
			Tools.FillC1(this._Correspondents, this.cmbAgent, (long)1);
			this.loadSending();
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmUnloadingInTextFile));
			this.label5 = new Label();
			this.cmbAgent = new C1Combo();
			this.label4 = new Label();
			this.cmdOk = new Button();
			this.cmdCancel = new Button();
			this.cmbSendig = new C1Combo();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.cmbSendig).BeginInit();
			base.SuspendLayout();
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(4, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 16);
			this.label5.TabIndex = 20;
			this.label5.Text = "Корреспондент:";
			this.cmbAgent.AddItemSeparator = ';';
			this.cmbAgent.BorderStyle = 1;
			this.cmbAgent.Caption = "";
			this.cmbAgent.CaptionHeight = 17;
			this.cmbAgent.CharacterCasing = 0;
			this.cmbAgent.ColumnCaptionHeight = 17;
			this.cmbAgent.ColumnFooterHeight = 17;
			this.cmbAgent.ColumnHeaders = false;
			this.cmbAgent.ColumnWidth = 100;
			this.cmbAgent.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbAgent.ContentHeight = 15;
			this.cmbAgent.DataMode = DataModeEnum.AddItem;
			this.cmbAgent.DeadAreaBackColor = Color.Empty;
			this.cmbAgent.EditorBackColor = SystemColors.Window;
			this.cmbAgent.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbAgent.EditorForeColor = SystemColors.WindowText;
			this.cmbAgent.EditorHeight = 15;
			this.cmbAgent.FlatStyle = FlatModeEnum.Flat;
			this.cmbAgent.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbAgent.ItemHeight = 15;
			this.cmbAgent.Location = new Point(104, 24);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(176, 19);
			this.cmbAgent.TabIndex = 19;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label4.Location = new Point(4, 4);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(92, 16);
			this.label4.TabIndex = 18;
			this.label4.Text = "Дата посылки:";
			this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOk.FlatStyle = FlatStyle.Flat;
			this.cmdOk.Location = new Point(76, 52);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(100, 24);
			this.cmdOk.TabIndex = 21;
			this.cmdOk.Text = "Сформировать";
			this.cmdOk.Click += new EventHandler(this.cmdOk_Click);
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(184, 52);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(96, 24);
			this.cmdCancel.TabIndex = 22;
			this.cmdCancel.Text = "Отмена";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmbSendig.AddItemSeparator = ';';
			this.cmbSendig.BorderStyle = 1;
			this.cmbSendig.Caption = "";
			this.cmbSendig.CaptionHeight = 17;
			this.cmbSendig.CharacterCasing = 0;
			this.cmbSendig.ColumnCaptionHeight = 17;
			this.cmbSendig.ColumnFooterHeight = 17;
			this.cmbSendig.ColumnHeaders = false;
			this.cmbSendig.ColumnWidth = 100;
			this.cmbSendig.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbSendig.ContentHeight = 15;
			this.cmbSendig.DataMode = DataModeEnum.AddItem;
			this.cmbSendig.DeadAreaBackColor = Color.Empty;
			this.cmbSendig.EditorBackColor = SystemColors.Window;
			this.cmbSendig.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbSendig.EditorForeColor = SystemColors.WindowText;
			this.cmbSendig.EditorHeight = 15;
			this.cmbSendig.FlatStyle = FlatModeEnum.Flat;
			this.cmbSendig.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbSendig.ItemHeight = 15;
			this.cmbSendig.Location = new Point(104, 0);
			this.cmbSendig.MatchEntryTimeout = (long)2000;
			this.cmbSendig.MaxDropDownItems = 5;
			this.cmbSendig.MaxLength = 32767;
			this.cmbSendig.MouseCursor = Cursors.Default;
			this.cmbSendig.Name = "cmbSendig";
			this.cmbSendig.RowDivider.Color = Color.DarkGray;
			this.cmbSendig.RowDivider.Style = LineStyleEnum.None;
			this.cmbSendig.RowSubDividerColor = Color.DarkGray;
			this.cmbSendig.Size = new System.Drawing.Size(176, 19);
			this.cmbSendig.TabIndex = 19;
			this.cmbSendig.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(282, 82);
			base.Controls.Add(this.cmbSendig);
			base.Controls.Add(this.cmdOk);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.cmbAgent);
			base.Controls.Add(this.label4);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmUnloadingInTextFile";
			this.Text = "Формирование транспортного файла для Поверителя";
			base.Closing += new CancelEventHandler(this.frmUnloadingInTextFile_Closing);
			base.Load += new EventHandler(this.frmUnloadingInTextFile_Load);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.cmbSendig).EndInit();
			base.ResumeLayout(false);
		}

		private void loadSending()
		{
			this._Sendings = new Sendings();
			this._Sendings.Load(3);
			this.cmbSendig.ClearItems();
			this.cmbSendig.AddItem("Новая посылка");
			for (int i = 0; i < this._Sendings.get_Count(); i++)
			{
				C1Combo c1Combo = this.cmbSendig;
				DateTime dateSending = this._Sendings[i].DateSending;
				c1Combo.AddItem(string.Concat(dateSending.ToString(), " №", this._Sendings[i].NumberSending));
			}
			this.cmbSendig.SelectedIndex = 0;
			this.cmbSendig.ColumnWidth = this.cmbSendig.Width - this.cmbSendig.Height;
		}
	}
}