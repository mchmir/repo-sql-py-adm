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
	public class frmPrintConsumptionGasUL : Form
	{
		private Label label1;

		private C1DateEdit dtBegin;

		private Button cmdClose;

		private Button cmdOK;

		private int _per;

		private C1Combo cmbStatus;

		private Label label2;

		private RadioButton rbAll;

		private RadioButton rbGaz;

		private RadioButton rbUslugi;

		private System.ComponentModel.Container components = null;

		public frmPrintConsumptionGasUL(int per)
		{
			this.InitializeComponent();
			this._per = per;
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			long num;
			int dUser;
			SqlParameter[] sqlParameterArray;
			int selectedIndex = this.cmbStatus.SelectedIndex;
			Periods period = new Periods();
			period.Load((DateTime)this.dtBegin.Value);
			num = (period.get_Count() <= 0 ? (long)0 : period[0].get_ID());
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					string str = "";
					string str1 = "";
					if (this.rbAll.Checked)
					{
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSalesGasVDGOUL.xls");
						string str2 = Depot.oSettings.ReportPath.Trim();
						dUser = Depot.oSettings.IDUser;
						str1 = string.Concat(str2, "Temp\\repSalesGasVDGOUL", dUser.ToString(), ".xls");
					}
					if (this.rbGaz.Checked)
					{
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSalesGasUL.xls");
						string str3 = Depot.oSettings.ReportPath.Trim();
						dUser = Depot.oSettings.IDUser;
						str1 = string.Concat(str3, "Temp\\repSalesGasUL", dUser.ToString(), ".xls");
					}
					if (this.rbUslugi.Checked)
					{
						str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repSalesUslUL.xls");
						string str4 = Depot.oSettings.ReportPath.Trim();
						dUser = Depot.oSettings.IDUser;
						str1 = string.Concat(str4, "Temp\\repSalesUslUL", dUser.ToString(), ".xls");
					}
					if (File.Exists(str1))
					{
						File.Delete(str1);
						File.Copy(str, str1, true);
					}
					else
					{
						File.Copy(str, str1, true);
					}
					SqlParameter sqlParameter = new SqlParameter("@idPeriod", SqlDbType.Int)
					{
						Direction = ParameterDirection.Input,
						Value = num
					};
					SqlParameter sqlParameter1 = new SqlParameter("@Status", SqlDbType.Int)
					{
						Direction = ParameterDirection.Input,
						Value = selectedIndex
					};
					SqlParameter sqlParameter2 = new SqlParameter("@Path", SqlDbType.VarChar)
					{
						Direction = ParameterDirection.Input,
						Value = str1.Trim()
					};
					SqlParameter sqlParameter3 = new SqlParameter("@MonthName", SqlDbType.VarChar)
					{
						Direction = ParameterDirection.Input
					};
					DateTime value = (DateTime)this.dtBegin.Value;
					sqlParameter3.Value = string.Concat(Tools.NameMonthIP(value.Month), " ", Convert.ToString(value.Year));
					if (this.rbAll.Checked)
					{
						sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3 };
						if (!Saver.ExecuteProcedure("repSalesGasVDGOUL", sqlParameterArray))
						{
							MessageBox.Show("Ошибка формирования отчета, возможно данный файл уже открыт", "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							Process.Start("Excel", str1);
						}
						this.Cursor = Cursors.Default;
					}
					if (this.rbGaz.Checked)
					{
						sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3 };
						if (!Saver.ExecuteProcedure("repSalesGasUL", sqlParameterArray))
						{
							MessageBox.Show("Ошибка формирования отчета, возможно данный файл уже открыт", "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							Process.Start("Excel", str1);
						}
						this.Cursor = Cursors.Default;
					}
					if (this.rbUslugi.Checked)
					{
						sqlParameterArray = new SqlParameter[] { sqlParameter, sqlParameter1, sqlParameter2, sqlParameter3 };
						if (!Saver.ExecuteProcedure("repSalesUslUL", sqlParameterArray))
						{
							MessageBox.Show("Ошибка формирования отчета, возможно данный файл уже открыт", "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							Process.Start("Excel", str1);
						}
						this.Cursor = Cursors.Default;
					}
				}
				catch (Exception exception)
				{
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

		private void frmPrintConsumptionGasUL_Load(object sender, EventArgs e)
		{
			this.cmbStatus.ClearItems();
			this.cmbStatus.AddItem("Не определен");
			this.cmbStatus.AddItem("Активен");
			this.cmbStatus.AddItem("Закрыт");
			this.cmbStatus.AddItem("Все");
			this.cmbStatus.SelectedIndex = 3;
			this.dtBegin.Value = DateTime.Now;
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmPrintConsumptionGasUL));
			this.dtBegin = new C1DateEdit();
			this.label1 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.cmbStatus = new C1Combo();
			this.label2 = new Label();
			this.rbAll = new RadioButton();
			this.rbGaz = new RadioButton();
			this.rbUslugi = new RadioButton();
			((ISupportInitialize)this.dtBegin).BeginInit();
			((ISupportInitialize)this.cmbStatus).BeginInit();
			base.SuspendLayout();
			this.dtBegin.BorderStyle = 1;
			this.dtBegin.CustomFormat = "MMMM yyyy";
			this.dtBegin.FormatType = FormatTypeEnum.CustomFormat;
			this.dtBegin.Location = new Point(48, 4);
			this.dtBegin.Name = "dtBegin";
			this.dtBegin.Size = new System.Drawing.Size(120, 18);
			this.dtBegin.TabIndex = 31;
			this.dtBegin.Tag = null;
			this.dtBegin.Value = new DateTime(2012, 10, 11, 0, 0, 0, 0);
			this.dtBegin.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Период";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(148, 64);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 35;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(48, 64);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(96, 24);
			this.cmdOK.TabIndex = 34;
			this.cmdOK.Text = "Сформировать";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
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
			this.cmbStatus.Location = new Point(48, 28);
			this.cmbStatus.MatchEntryTimeout = (long)2000;
			this.cmbStatus.MaxDropDownItems = 5;
			this.cmbStatus.MaxLength = 32767;
			this.cmbStatus.MouseCursor = Cursors.Default;
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.RowDivider.Color = Color.DarkGray;
			this.cmbStatus.RowDivider.Style = LineStyleEnum.None;
			this.cmbStatus.RowSubDividerColor = Color.DarkGray;
			this.cmbStatus.Size = new System.Drawing.Size(120, 19);
			this.cmbStatus.TabIndex = 36;
			this.cmbStatus.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>149</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(4, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 16);
			this.label2.TabIndex = 37;
			this.label2.Text = "Статус";
			this.rbAll.Checked = true;
			this.rbAll.Location = new Point(176, 4);
			this.rbAll.Name = "rbAll";
			this.rbAll.Size = new System.Drawing.Size(64, 16);
			this.rbAll.TabIndex = 38;
			this.rbAll.TabStop = true;
			this.rbAll.Text = "Общий";
			this.rbGaz.Location = new Point(176, 20);
			this.rbGaz.Name = "rbGaz";
			this.rbGaz.Size = new System.Drawing.Size(64, 16);
			this.rbGaz.TabIndex = 39;
			this.rbGaz.Text = "Газ";
			this.rbGaz.CheckedChanged += new EventHandler(this.rbGaz_CheckedChanged);
			this.rbUslugi.Location = new Point(176, 36);
			this.rbUslugi.Name = "rbUslugi";
			this.rbUslugi.Size = new System.Drawing.Size(64, 16);
			this.rbUslugi.TabIndex = 40;
			this.rbUslugi.Text = "Услуги";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(246, 92);
			base.Controls.Add(this.rbUslugi);
			base.Controls.Add(this.rbGaz);
			base.Controls.Add(this.rbAll);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.cmbStatus);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.dtBegin);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximumSize = new System.Drawing.Size(252, 116);
			base.MinimumSize = new System.Drawing.Size(252, 116);
			base.Name = "frmPrintConsumptionGasUL";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Отчёт о потреблении газа и услуг ВДГО";
			base.Load += new EventHandler(this.frmPrintConsumptionGasUL_Load);
			((ISupportInitialize)this.dtBegin).EndInit();
			((ISupportInitialize)this.cmbStatus).EndInit();
			base.ResumeLayout(false);
		}

		private void rbGaz_CheckedChanged(object sender, EventArgs e)
		{
		}
	}
}