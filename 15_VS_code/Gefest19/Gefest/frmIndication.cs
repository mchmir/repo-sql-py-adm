using C1.Win.C1Input;
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
	public class frmIndication : Form
	{
		private Button cmdOk;

		private Button cmdCancel;

		private Label label1;

		private Label label2;

		private Indication _indication;

		private C1DateEdit dtDisplay;

		private NumericUpDown nDisplay;

		private C1Combo cmbType;

		private Label label3;

		private Label label4;

		private Agents _agents;

		private C1Combo cmbAgent;

		private System.ComponentModel.Container components = null;

		public frmIndication(Indication oIndication)
		{
			this.InitializeComponent();
			this._indication = oIndication;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			this._indication.Datedisplay = Tools.DateOnly((DateTime)this.dtDisplay.Value);
			this._indication.Display = Convert.ToDouble(this.nDisplay.Value);
			this._indication.oTypeIndication = Depot.oTypeIndications[this.cmbType.SelectedIndex];
			if (Depot.oTypeIndications[this.cmbType.SelectedIndex].get_ID() != (long)3 && this._indication.oGmeter.oTypeVerify.get_ID() == (long)2)
			{
				MessageBox.Show("ПУ забракован! Тип показаний может быть только 'по акту'!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.cmbType.Focus();
				return;
			}
			if (this.cmbAgent.SelectedIndex > 0)
			{
				this._indication.oAgent = this._agents[this.cmbAgent.SelectedIndex - 1];
			}
			string str = this._indication.CalcFactUse();
			try
			{
				double num = Convert.ToDouble(str);
				if (num <= 10 && num != 0 || MessageBox.Show(string.Concat("Потребление составляет ", num.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
				{
					if (this._indication.Save() <= 0)
					{
						base.DialogResult = System.Windows.Forms.DialogResult.OK;
						base.Close();
					}
					else
					{
						MessageBox.Show("Ошибка сохранения!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
			catch
			{
				MessageBox.Show(str, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

		private void frmIndication_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmIndication_Load(object sender, EventArgs e)
		{
			this.Text = "Редактирование показаний";
			long d = (long)0;
			long num = (long)0;
			if (this._indication == null)
			{
				base.Close();
			}
			if (!this._indication.get_isNew())
			{
				d = this._indication.oTypeIndication.get_ID();
				if (this._indication.oAgent != null)
				{
					num = this._indication.oAgent.get_ID();
				}
				this.dtDisplay.Value = this._indication.Datedisplay;
				this.dtDisplay.Enabled = false;
			}
			else
			{
				this._indication.Datedisplay = DateTime.Today;
				this.Text = "Добавление показаний";
			}
			double display = this._indication.Display;
			this.nDisplay.Value = Convert.ToDecimal(display);
			Tools.FillC1(Depot.oTypeIndications, this.cmbType, d);
			this._agents = new Agents();
			TypeAgent[] typeAgentArray = new TypeAgent[2];
			typeAgentArray.SetValue(Depot.oTypeAgents.item((long)1), 0);
			typeAgentArray.SetValue(Depot.oTypeAgents.item((long)5), 1);
			this._agents.Load(typeAgentArray);
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, num, "Без контролера");
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmIndication));
			this.cmdOk = new Button();
			this.cmdCancel = new Button();
			this.dtDisplay = new C1DateEdit();
			this.label1 = new Label();
			this.label2 = new Label();
			this.nDisplay = new NumericUpDown();
			this.cmbType = new C1Combo();
			this.label3 = new Label();
			this.label4 = new Label();
			this.cmbAgent = new C1Combo();
			((ISupportInitialize)this.dtDisplay).BeginInit();
			((ISupportInitialize)this.nDisplay).BeginInit();
			((ISupportInitialize)this.cmbType).BeginInit();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			base.SuspendLayout();
			this.cmdOk.FlatStyle = FlatStyle.Flat;
			this.cmdOk.Location = new Point(120, 104);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(88, 24);
			this.cmdOk.TabIndex = 13;
			this.cmdOk.Text = "Ok";
			this.cmdOk.Click += new EventHandler(this.cmdOk_Click);
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(216, 104);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(88, 24);
			this.cmdCancel.TabIndex = 12;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.dtDisplay.BorderStyle = 1;
			this.dtDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.dtDisplay.FormatType = FormatTypeEnum.LongDate;
			this.dtDisplay.Location = new Point(104, 32);
			this.dtDisplay.Name = "dtDisplay";
			this.dtDisplay.TabIndex = 14;
			this.dtDisplay.Tag = null;
			this.dtDisplay.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.Location = new Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Дата показаний:";
			this.label2.Location = new Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 17;
			this.label2.Text = "Показания:";
			this.nDisplay.BorderStyle = BorderStyle.FixedSingle;
			this.nDisplay.DecimalPlaces = 3;
			this.nDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.nDisplay.Location = new Point(104, 56);
			NumericUpDown num = this.nDisplay;
			int[] numArray = new int[] { 276447231, 23283, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.nDisplay.Name = "nDisplay";
			this.nDisplay.Size = new System.Drawing.Size(200, 20);
			this.nDisplay.TabIndex = 18;
			this.nDisplay.TextAlign = HorizontalAlignment.Right;
			this.cmbType.AddItemSeparator = ';';
			this.cmbType.BorderStyle = 1;
			this.cmbType.Caption = "";
			this.cmbType.CaptionHeight = 17;
			this.cmbType.CharacterCasing = 0;
			this.cmbType.ColumnCaptionHeight = 17;
			this.cmbType.ColumnFooterHeight = 17;
			this.cmbType.ColumnHeaders = false;
			this.cmbType.ContentHeight = 15;
			this.cmbType.DataMode = DataModeEnum.AddItem;
			this.cmbType.DeadAreaBackColor = Color.Empty;
			this.cmbType.EditorBackColor = SystemColors.Window;
			this.cmbType.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.cmbType.EditorForeColor = SystemColors.WindowText;
			this.cmbType.EditorHeight = 15;
			this.cmbType.FlatStyle = FlatModeEnum.Flat;
			this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.cmbType.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbType.ItemHeight = 15;
			this.cmbType.Location = new Point(104, 8);
			this.cmbType.MatchEntryTimeout = (long)2000;
			this.cmbType.MaxDropDownItems = 5;
			this.cmbType.MaxLength = 32767;
			this.cmbType.MouseCursor = Cursors.Default;
			this.cmbType.Name = "cmbType";
			this.cmbType.RowDivider.Color = Color.DarkGray;
			this.cmbType.RowDivider.Style = LineStyleEnum.None;
			this.cmbType.RowSubDividerColor = Color.DarkGray;
			this.cmbType.Size = new System.Drawing.Size(200, 19);
			this.cmbType.TabIndex = 19;
			this.cmbType.Text = "c1Combo1";
			this.cmbType.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft Sans Serif, 8.25pt, style=Bold;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label3.Location = new Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 20;
			this.label3.Text = "Тип показаний:";
			this.label4.Location = new Point(4, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 16);
			this.label4.TabIndex = 22;
			this.label4.Text = "Агент:";
			this.label4.Click += new EventHandler(this.label4_Click);
			this.cmbAgent.AddItemSeparator = ';';
			this.cmbAgent.BorderStyle = 1;
			this.cmbAgent.Caption = "";
			this.cmbAgent.CaptionHeight = 17;
			this.cmbAgent.CharacterCasing = 0;
			this.cmbAgent.ColumnCaptionHeight = 17;
			this.cmbAgent.ColumnFooterHeight = 17;
			this.cmbAgent.ColumnHeaders = false;
			this.cmbAgent.ContentHeight = 15;
			this.cmbAgent.DataMode = DataModeEnum.AddItem;
			this.cmbAgent.DeadAreaBackColor = Color.Empty;
			this.cmbAgent.EditorBackColor = SystemColors.Window;
			this.cmbAgent.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.cmbAgent.EditorForeColor = SystemColors.WindowText;
			this.cmbAgent.EditorHeight = 15;
			this.cmbAgent.FlatStyle = FlatModeEnum.Flat;
			this.cmbAgent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.cmbAgent.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbAgent.ItemHeight = 15;
			this.cmbAgent.Location = new Point(104, 80);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(200, 19);
			this.cmbAgent.TabIndex = 21;
			this.cmbAgent.Text = "c1Combo1";
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft Sans Serif, 8.25pt, style=Bold;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			base.AcceptButton = this.cmdOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new System.Drawing.Size(306, 130);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.cmbAgent);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.cmbType);
			base.Controls.Add(this.nDisplay);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dtDisplay);
			base.Controls.Add(this.cmdOk);
			base.Controls.Add(this.cmdCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmIndication";
			this.Text = "Показания";
			base.Closing += new CancelEventHandler(this.frmIndication_Closing);
			base.Load += new EventHandler(this.frmIndication_Load);
			((ISupportInitialize)this.dtDisplay).EndInit();
			((ISupportInitialize)this.nDisplay).EndInit();
			((ISupportInitialize)this.cmbType).EndInit();
			((ISupportInitialize)this.cmbAgent).EndInit();
			base.ResumeLayout(false);
		}

		private void label4_Click(object sender, EventArgs e)
		{
		}
	}
}