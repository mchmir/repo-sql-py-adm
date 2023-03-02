using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPrintReestrVerifyGM : Form
	{
		private ImageList imageList1;

		private GroupBox groupBox1;

		private Button button1;

		private Button button2;

		private IContainer components;

		private Label label6;

		private C1Combo cmbAgent;

		private C1DateEdit dtBegin;

		private Label label1;

		private C1DateEdit dtEnd;

		private Label label2;

		private Agents _agents;

		public frmPrintReestrVerifyGM()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			long d = (long)0;
			if (this.cmbAgent.Text != "Все агенты")
			{
				d = (long)((int)this._agents[this.cmbAgent.SelectedIndex - 1].get_ID());
			}
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					string str = "";
					string str1 = "";
					str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repReestrVerifyGM.xls");
					string str2 = Depot.oSettings.ReportPath.Trim();
					int dUser = Depot.oSettings.IDUser;
					str1 = string.Concat(str2, "Temp\\repReestrVerifyGM", dUser.ToString(), ".xls");
					if (File.Exists(str1))
					{
						File.Delete(str1);
						File.Copy(str, str1, true);
					}
					else
					{
						File.Copy(str, str1, true);
					}
					SqlParameter sqlParameter = new SqlParameter("@dBegin", SqlDbType.DateTime)
					{
						Direction = ParameterDirection.Input,
						Value = this.dtBegin.Value
					};
					SqlParameter sqlParameter1 = new SqlParameter("@dEnd", SqlDbType.DateTime)
					{
						Direction = ParameterDirection.Input,
						Value = this.dtEnd.Value
					};
					SqlParameter sqlParameter2 = new SqlParameter("@IDAgent", SqlDbType.Int)
					{
						Direction = ParameterDirection.Input,
						Value = d
					};
					SqlParameter sqlParameter3 = new SqlParameter("@Path", SqlDbType.VarChar)
					{
						Direction = ParameterDirection.Input,
						Value = str1.Trim()
					};
					SqlParameter[] sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3 };
					if (!Saver.ExecuteProcedure("repReestrVerifysGM", sqlParameterArray))
					{
						MessageBox.Show("Ошибка формирования отчета, возможно данный файл уже открыт", "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						Process.Start("Excel", str1);
					}
					this.Cursor = Cursors.Default;
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageBox.Show(exception.Message.ToString(), "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.Cursor = Cursors.Default;
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
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

		private void frmPrintReestrVerifyGM_Load(object sender, EventArgs e)
		{
			this._agents = new Agents();
			this._agents.Load(Depot.oTypeAgents.item((long)5));
			Tools.FillC1WhithAll(this._agents, this.cmbAgent, (long)0, "Все агенты");
			C1DateEdit c1DateEdit = this.dtBegin;
			DateTime today = DateTime.Today;
			DateTime dateTime = DateTime.Today;
			c1DateEdit.Value = today.AddDays((double)(-dateTime.Day + 1));
			C1DateEdit date = this.dtEnd;
			today = DateTime.Today;
			date.Value = today.Date;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintReestrVerifyGM));
			this.imageList1 = new ImageList(this.components);
			this.groupBox1 = new GroupBox();
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			this.dtEnd = new C1DateEdit();
			this.label2 = new Label();
			this.label6 = new Label();
			this.cmbAgent = new C1Combo();
			this.button1 = new Button();
			this.button2 = new Button();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.dtBegin).BeginInit();
			((ISupportInitialize)this.dtEnd).BeginInit();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			base.SuspendLayout();
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.dtBegin);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtEnd);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.cmbAgent);
			this.groupBox1.Location = new Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(332, 68);
			this.groupBox1.TabIndex = 69;
			this.groupBox1.TabStop = false;
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.FormatType = FormatTypeEnum.LongDate;
			this.dtBegin.Location = new Point(49, 40);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(120, 18);
			this.dtBegin.TabIndex = 80;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 82;
			this.label1.Text = "Период";
			this.dtEnd.BorderStyle = 1;
			this.dtEnd.FormatType = FormatTypeEnum.LongDate;
			this.dtEnd.Location = new Point(206, 40);
			this.dtEnd.Name = "dtEnd";
			this.dtEnd.Size = new System.Drawing.Size(120, 18);
			this.dtEnd.TabIndex = 79;
			this.dtEnd.Tag = null;
			this.dtEnd.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtEnd.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(178, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 81;
			this.label2.Text = "по";
			this.label6.Location = new Point(4, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 78;
			this.label6.Text = "Агент:";
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
			this.cmbAgent.Location = new Point(48, 16);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(280, 19);
			this.cmbAgent.TabIndex = 77;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Location = new Point(244, 76);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 24);
			this.button1.TabIndex = 71;
			this.button1.Text = "Отмена";
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.Location = new Point(140, 76);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 24);
			this.button2.TabIndex = 70;
			this.button2.Text = "Сформировать";
			this.button2.Click += new EventHandler(this.button2_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.button1;
			base.ClientSize = new System.Drawing.Size(338, 104);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPrintReestrVerifyGM";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Реестр поверенных ПУ";
			base.Load += new EventHandler(this.frmPrintReestrVerifyGM_Load);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.dtBegin).EndInit();
			((ISupportInitialize)this.dtEnd).EndInit();
			((ISupportInitialize)this.cmbAgent).EndInit();
			base.ResumeLayout(false);
		}
	}
}