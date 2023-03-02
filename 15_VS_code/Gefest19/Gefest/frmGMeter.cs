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
	public class frmGMeter : Form
	{
		private C1Combo cmbTypeGMeter;

		private Label label2;

		private Label label4;

		private Label label6;

		private Label label1;

		private Label label3;

		private C1DateEdit dtDateInstall;

		private C1DateEdit dtDateVerify;

		private C1DateEdit dtDateFabrication;

		private ToolTip toolTip1;

		private Label lblPU;

		private TextBox txtNote;

		private Label label17;

		private Button cmdClose;

		private Button cmdOK;

		private Label label18;

		private TextBox txtSerialNumber;

		private IContainer components;

		private GroupBox groupBox2;

		private Label lblAccount;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private GroupBox groupBox1;

		private Label label5;

		private Label lblNameGRU;

		private Label label7;

		private Label lblOU;

		private Gmeter _gmeter;

		private ImageList imageList1;

		private Button cmdTypeGMeter;

		private Label lblBeginIndication;

		private Label label8;

		private Label lblTypeVerify;

		private Label label13;

		private Label lblAgent;

		private TypeGMeters _typegmeters;

		private Documents _docs;

		private GroupBox groupBox3;

		private TextBox txtPlomb1;

		private Label label9;

		private Label label14;

		private TextBox txtPlomb2;

		private Label label15;

		private C1DateEdit dtPlombDate;

		private Label label16;

		private NumericUpDown nDisplayPlomb;

		private Button bShowHistory;

		private Label label19;

		private C1Combo cmbMechanic;

		private Label label20;

		private TextBox txtPlombMemo;

		private GroupBox groupBox4;

		private C1DateEdit dtDate;

		private Label label21;

		private Button bShowIndication;

		private GroupBox groupBox5;

		private Label label22;

		private Label lblFactUse;

		private Label label23;

		private C1Combo cmbAgent;

		private Label label24;

		private C1Combo cmbTypeIndication;

		private Button cmdApply1;

		private Label lblDateIndication;

		private Label label25;

		private NumericUpDown numNewIndication;

		private Label lblCurrentIndication;

		private Label label26;

		private Label label27;

		private Agents _agents;

		private Agents _iagents;

		private Indication tmpIndication;

		private CheckBox chkIDTypePlombWork;

		private double FactAmount = 0;

		public frmGMeter(Gmeter oGmeter)
		{
			this.InitializeComponent();
			this._gmeter = oGmeter;
		}

		private void bShowHistory_Click(object sender, EventArgs e)
		{
			frmGMeterPlombHistory _frmGMeterPlombHistory = new frmGMeterPlombHistory(this._gmeter.get_ID());
			_frmGMeterPlombHistory.ShowDialog(this);
		}

		private void bShowIndication_Click(object sender, EventArgs e)
		{
			if (base.Size.Width > 800)
			{
				base.Width = 552;
				this.bShowIndication.Text = ">>>";
				return;
			}
			base.Width = 868;
			this.bShowIndication.Text = "<<<";
		}

		private void cmbTypeGMeter_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDateFabrication.Focus();
			}
		}

		private void cmdApply1_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this.numNewIndication.Value == new decimal(0))
					{
						this.numNewIndication.Focus();
						return;
					}
					else if (this.cmbTypeIndication.SelectedIndex == -1)
					{
						MessageBox.Show("Необходимо указать тип показаний!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbTypeIndication.Focus();
						return;
					}
					else if (Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex].get_ID() == (long)3 || this._gmeter.oTypeVerify.get_ID() != (long)2)
					{
						this.numNewIndication_Leave(null, null);
						if (this.FactAmount < 0)
						{
							MessageBox.Show("Не верно указаны показания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							this.numNewIndication.Focus();
							return;
						}
						else if (this.FactAmount <= 10 || MessageBox.Show(string.Concat("Потребление составляет ", this.FactAmount.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
						{
							this.tmpIndication.oTypeIndication = Depot.oTypeIndications[this.cmbTypeIndication.SelectedIndex];
							if (this.cmbAgent.Enabled)
							{
								this.tmpIndication.oAgent = this._iagents[this.cmbAgent.SelectedIndex];
							}
							if (this.tmpIndication.Save() != 0)
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								this.tmpIndication = null;
								MessageBox.Show("Сохранено успешно!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.None);
							}
						}
						else
						{
							this.numNewIndication.Focus();
							return;
						}
					}
					else
					{
						MessageBox.Show("ПУ забракован! Тип показаний может быть только 'по акту'!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbTypeIndication.Focus();
						return;
					}
				}
				catch
				{
					MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this.cmbTypeGMeter.SelectedIndex != -1)
					{
						this._gmeter.oTypeGMeter = this._typegmeters[this.cmbTypeGMeter.SelectedIndex];
						this._gmeter.SerialNumber = this.txtSerialNumber.Text;
						this._gmeter.PlombNumber1 = this.txtPlomb1.Text;
						this._gmeter.PlombNumber2 = this.txtPlomb2.Text;
						this._gmeter.DatePlomb = (DateTime)this.dtPlombDate.Value;
						this._gmeter.IndicationPlomb = Convert.ToDouble(this.nDisplayPlomb.Value);
						this._gmeter.IDAgentPlomb = Convert.ToInt32(this._agents[this.cmbMechanic.SelectedIndex].get_ID());
						this._gmeter.PlombMemo = this.txtPlombMemo.Text;
						if (!this.chkIDTypePlombWork.Checked)
						{
							this._gmeter.IDTypePlombWork = 2;
						}
						else
						{
							this._gmeter.IDTypePlombWork = 1;
						}
						this._gmeter.DateInstall = (DateTime)this.dtDateInstall.Value;
						this._gmeter.DateVerify = (DateTime)this.dtDateVerify.Value;
						this._gmeter.DateFabrication = (DateTime)this.dtDateFabrication.Value;
						this._gmeter.Memo = this.txtNote.Text;
						if (this._gmeter.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							base.Close();
						}
					}
					else
					{
						MessageBox.Show("Не выбран тип ПУ!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.cmbTypeGMeter.Focus();
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

		private void cmdTypeGMeter_Click(object sender, EventArgs e)
		{
			TypeGMeters typeGMeter = this._typegmeters;
			string[] strArrays = new string[] { "Название", "Класс точности", "Меж провер. интер." };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200, 150, 150 };
			strArrays = new string[] { "Name", "ClassAccuracy", "ServiceLife" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник приборов учета", typeGMeter, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			this._typegmeters = new TypeGMeters();
			this._typegmeters.Load();
			if (frmSimpleObj.lvData.SelectedItems.Count <= 0)
			{
				Tools.FillC1(this._typegmeters, this.cmbTypeGMeter, (long)0);
			}
			else
			{
				Tools.FillC1(this._typegmeters, this.cmbTypeGMeter, Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
			}
			frmSimpleObj = null;
		}

		private void CreateTypeGMeter(long IdSelected)
		{
			try
			{
				this.cmbTypeGMeter.ClearItems();
				int num = 0;
				for (int i = 0; i < this._typegmeters.get_Count(); i++)
				{
					this.cmbTypeGMeter.AddItem(this._typegmeters[i].Fullname);
					if (this._typegmeters[i].get_ID() == IdSelected)
					{
						num = i;
					}
				}
				if (this.cmbTypeGMeter.ListCount > num)
				{
					this.cmbTypeGMeter.SelectedIndex = num;
				}
				this.cmbTypeGMeter.ColumnWidth = this.cmbTypeGMeter.Width - this.cmbTypeGMeter.Height;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.cmbTypeGMeter.Text = string.Concat("Ошибка загрузки справочника ", exception.Message);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
			this._gmeter = null;
			this._typegmeters = null;
		}

		private void dtDateFabrication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDateInstall.Focus();
			}
		}

		private void dtDateInstall_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDateVerify.Focus();
			}
		}

		private void dtDateVerify_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNote.Focus();
			}
		}

		private void frmGMeter_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
			this._gmeter = null;
			this._typegmeters = null;
		}

		private void frmGMeter_Load(object sender, EventArgs e)
		{
			DateTime datedisplay;
			try
			{
				if (Tools.CurrentUser() != "nkomarova")
				{
					base.Width = 552;
				}
				else
				{
					base.Width = 650;
				}
				this.bShowIndication.Text = ">>>";
				this._iagents = new Agents();
				Agents agent = this._iagents;
				TypeAgent[] typeAgentArray = new TypeAgent[] { Depot.oTypeAgents.item((long)1), Depot.oTypeAgents.item((long)5) };
				agent.Load(typeAgentArray);
				Tools.FillC1(this._iagents, this.cmbAgent, (long)137);
				Tools.FillC1(Depot.oTypeIndications, this.cmbTypeIndication, (long)3);
				try
				{
					if (this._gmeter.GetCurrentIndication().oTypeIndication == null)
					{
						this.lblCurrentIndication.Text = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
					}
					else
					{
						this.lblCurrentIndication.Text = string.Concat(Convert.ToString(this._gmeter.GetCurrentIndication().Display), ", ", this._gmeter.GetCurrentIndication().oTypeIndication.get_Name());
					}
					Label str = this.lblDateIndication;
					datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
					str.Text = Convert.ToString(datedisplay.ToShortDateString());
				}
				catch
				{
				}
				this.tmpIndication = new Indication()
				{
					oGmeter = this._gmeter
				};
				C1DateEdit date = this.dtDate;
				datedisplay = DateTime.Today;
				date.Value = datedisplay.Date;
				this.cmdApply1.Enabled = false;
				if (this._gmeter == null)
				{
					base.Close();
				}
				else if (this._gmeter.get_ID() != (long)0)
				{
					this._agents = new Agents();
					this._agents.Load(Depot.oTypeAgents.item((long)5));
					C1DateEdit c1DateEdit = this.dtDateFabrication;
					datedisplay = DateTime.Today;
					c1DateEdit.Value = datedisplay.Date;
					C1DateEdit date1 = this.dtDateVerify;
					datedisplay = DateTime.Today;
					date1.Value = datedisplay.Date;
					C1DateEdit c1DateEdit1 = this.dtDateInstall;
					datedisplay = DateTime.Today;
					c1DateEdit1.Value = datedisplay.Date;
					this._typegmeters = new TypeGMeters();
					this._typegmeters.Load();
					this.lblAccount.Text = this._gmeter.oGobject.oContract.Account;
					this.lblFIO.Text = this._gmeter.oGobject.oContract.oPerson.FullName;
					this.lblAddress.Text = this._gmeter.oGobject.oContract.oPerson.oAddress.get_ShortAddress();
					if (this._gmeter.oGobject.oStatusGObject.get_ID() != (long)1)
					{
						this.lblOU.Text = string.Concat(this._gmeter.oGobject.oTypeGobject.Name, ", отключен, ", this._gmeter.oGobject.oAddress.get_ShortAddress());
					}
					else
					{
						this.lblOU.Text = string.Concat(this._gmeter.oGobject.oTypeGobject.Name, ", подключен, ", this._gmeter.oGobject.oAddress.get_ShortAddress());
					}
					this.lblNameGRU.Text = this._gmeter.oGobject.oGRU.get_Name();
					this.CreateTypeGMeter(this._gmeter.oTypeGMeter.get_ID());
					this.dtDateInstall.Value = this._gmeter.DateInstall;
					this.dtDateVerify.Value = this._gmeter.DateVerify;
					this.dtDateFabrication.Value = this._gmeter.DateFabrication;
					this.txtSerialNumber.Text = this._gmeter.SerialNumber;
					this.txtPlomb1.Text = this._gmeter.PlombNumber1;
					this.txtPlomb2.Text = this._gmeter.PlombNumber2;
					this.txtPlombMemo.Text = this._gmeter.PlombMemo;
					if (this._gmeter.DatePlomb >= Convert.ToDateTime("01-01-1800"))
					{
						this.dtPlombDate.Value = this._gmeter.DatePlomb;
					}
					else
					{
						C1DateEdit date2 = this.dtPlombDate;
						datedisplay = DateTime.Today;
						date2.Value = datedisplay.Date;
					}
					Tools.FillC1(this._agents, this.cmbMechanic, (long)this._gmeter.IDAgentPlomb);
					this.nDisplayPlomb.Value = Convert.ToDecimal(this._gmeter.IndicationPlomb);
					this.txtNote.Text = this._gmeter.Memo;
					this.lblBeginIndication.Text = Convert.ToString(this._gmeter.BeginValue);
					if (this._gmeter.oStatusGMeter.get_ID() != (long)1)
					{
						this.lblPU.Text = "Отключен";
					}
					else
					{
						this.lblPU.Text = "Подключен";
					}
					this.lblTypeVerify.Text = Depot.oTypeVerifys.item(this._gmeter.oTypeVerify.get_ID()).get_Name();
					if (this._gmeter.oTypeVerify.get_ID() == (long)2)
					{
						this.lblTypeVerify.BackColor = Color.Red;
					}
					this._docs = new Documents();
					if (this._docs.Load(Depot.oTypeDocuments.item((long)22), this._gmeter.DateVerify, this._gmeter.get_ID()) == 0 && this._docs.get_Count() > 0 && this._docs[0].GetNamePD(16) != "")
					{
						this.lblAgent.Text = this._agents.item(Convert.ToInt64(this._docs[0].GetNamePD(16))).get_Name();
					}
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmGMeter));
			this.cmbTypeGMeter = new C1Combo();
			this.label2 = new Label();
			this.txtSerialNumber = new TextBox();
			this.label4 = new Label();
			this.dtDateInstall = new C1DateEdit();
			this.label6 = new Label();
			this.dtDateVerify = new C1DateEdit();
			this.label1 = new Label();
			this.dtDateFabrication = new C1DateEdit();
			this.label3 = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.lblPU = new Label();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.label18 = new Label();
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.groupBox1 = new GroupBox();
			this.lblNameGRU = new Label();
			this.lblOU = new Label();
			this.label5 = new Label();
			this.label7 = new Label();
			this.cmdTypeGMeter = new Button();
			this.imageList1 = new ImageList(this.components);
			this.lblBeginIndication = new Label();
			this.label8 = new Label();
			this.lblTypeVerify = new Label();
			this.lblAgent = new Label();
			this.label13 = new Label();
			this.groupBox3 = new GroupBox();
			this.txtPlombMemo = new TextBox();
			this.label20 = new Label();
			this.cmbMechanic = new C1Combo();
			this.label19 = new Label();
			this.bShowHistory = new Button();
			this.nDisplayPlomb = new NumericUpDown();
			this.label16 = new Label();
			this.label14 = new Label();
			this.txtPlomb2 = new TextBox();
			this.label9 = new Label();
			this.txtPlomb1 = new TextBox();
			this.dtPlombDate = new C1DateEdit();
			this.label15 = new Label();
			this.bShowIndication = new Button();
			this.groupBox4 = new GroupBox();
			this.label21 = new Label();
			this.dtDate = new C1DateEdit();
			this.groupBox5 = new GroupBox();
			this.label22 = new Label();
			this.lblFactUse = new Label();
			this.label23 = new Label();
			this.cmbAgent = new C1Combo();
			this.label24 = new Label();
			this.cmbTypeIndication = new C1Combo();
			this.cmdApply1 = new Button();
			this.lblDateIndication = new Label();
			this.label25 = new Label();
			this.numNewIndication = new NumericUpDown();
			this.lblCurrentIndication = new Label();
			this.label26 = new Label();
			this.label27 = new Label();
			this.chkIDTypePlombWork = new CheckBox();
			((ISupportInitialize)this.cmbTypeGMeter).BeginInit();
			((ISupportInitialize)this.dtDateInstall).BeginInit();
			((ISupportInitialize)this.dtDateVerify).BeginInit();
			((ISupportInitialize)this.dtDateFabrication).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.cmbMechanic).BeginInit();
			((ISupportInitialize)this.nDisplayPlomb).BeginInit();
			((ISupportInitialize)this.dtPlombDate).BeginInit();
			this.groupBox4.SuspendLayout();
			((ISupportInitialize)this.dtDate).BeginInit();
			this.groupBox5.SuspendLayout();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			((ISupportInitialize)this.cmbTypeIndication).BeginInit();
			((ISupportInitialize)this.numNewIndication).BeginInit();
			base.SuspendLayout();
			this.cmbTypeGMeter.AddItemSeparator = ';';
			this.cmbTypeGMeter.BorderStyle = 1;
			this.cmbTypeGMeter.Caption = "";
			this.cmbTypeGMeter.CaptionHeight = 17;
			this.cmbTypeGMeter.CharacterCasing = 0;
			this.cmbTypeGMeter.ColumnCaptionHeight = 17;
			this.cmbTypeGMeter.ColumnFooterHeight = 17;
			this.cmbTypeGMeter.ColumnHeaders = false;
			this.cmbTypeGMeter.ColumnWidth = 100;
			this.cmbTypeGMeter.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeGMeter.ContentHeight = 15;
			this.cmbTypeGMeter.DataMode = DataModeEnum.AddItem;
			this.cmbTypeGMeter.DeadAreaBackColor = Color.Empty;
			this.cmbTypeGMeter.EditorBackColor = SystemColors.Window;
			this.cmbTypeGMeter.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeGMeter.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeGMeter.EditorHeight = 15;
			this.cmbTypeGMeter.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeGMeter.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbTypeGMeter.ItemHeight = 15;
			this.cmbTypeGMeter.Location = new Point(332, 100);
			this.cmbTypeGMeter.MatchEntryTimeout = (long)2000;
			this.cmbTypeGMeter.MaxDropDownItems = 5;
			this.cmbTypeGMeter.MaxLength = 32767;
			this.cmbTypeGMeter.MouseCursor = Cursors.Default;
			this.cmbTypeGMeter.Name = "cmbTypeGMeter";
			this.cmbTypeGMeter.RowDivider.Color = Color.DarkGray;
			this.cmbTypeGMeter.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeGMeter.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeGMeter.Size = new System.Drawing.Size(184, 19);
			this.cmbTypeGMeter.TabIndex = 2;
			this.cmbTypeGMeter.KeyPress += new KeyPressEventHandler(this.cmbTypeGMeter_KeyPress);
			this.cmbTypeGMeter.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label2.Location = new Point(292, 100);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 22;
			this.label2.Text = "Тип";
			this.txtSerialNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtSerialNumber.Location = new Point(76, 100);
			this.txtSerialNumber.Name = "txtSerialNumber";
			this.txtSerialNumber.Size = new System.Drawing.Size(168, 20);
			this.txtSerialNumber.TabIndex = 1;
			this.txtSerialNumber.Text = "";
			this.txtSerialNumber.KeyPress += new KeyPressEventHandler(this.txtSerialNumber_KeyPress);
			this.txtSerialNumber.Enter += new EventHandler(this.txtSerialNumber_Enter);
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(4, 100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 28;
			this.label4.Text = "Номер";
			this.dtDateInstall.BorderStyle = 1;
			this.dtDateInstall.FormatType = FormatTypeEnum.LongDate;
			this.dtDateInstall.Location = new Point(108, 148);
			this.dtDateInstall.Name = "dtDateInstall";
			this.dtDateInstall.Size = new System.Drawing.Size(136, 18);
			this.dtDateInstall.TabIndex = 5;
			this.dtDateInstall.Tag = null;
			this.dtDateInstall.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDateInstall.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDateInstall.KeyPress += new KeyPressEventHandler(this.dtDateInstall_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(4, 124);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 16);
			this.label6.TabIndex = 29;
			this.label6.Text = "Дата выпуска";
			this.dtDateVerify.BorderStyle = 1;
			this.dtDateVerify.FormatType = FormatTypeEnum.LongDate;
			this.dtDateVerify.Location = new Point(404, 124);
			this.dtDateVerify.Name = "dtDateVerify";
			this.dtDateVerify.Size = new System.Drawing.Size(136, 18);
			this.dtDateVerify.TabIndex = 6;
			this.dtDateVerify.Tag = null;
			this.dtDateVerify.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDateVerify.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDateVerify.KeyPress += new KeyPressEventHandler(this.dtDateVerify_KeyPress);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(292, 124);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 31;
			this.label1.Text = "Дата проверки";
			this.dtDateFabrication.BorderStyle = 1;
			this.dtDateFabrication.FormatType = FormatTypeEnum.LongDate;
			this.dtDateFabrication.Location = new Point(108, 124);
			this.dtDateFabrication.Name = "dtDateFabrication";
			this.dtDateFabrication.Size = new System.Drawing.Size(136, 18);
			this.dtDateFabrication.TabIndex = 4;
			this.dtDateFabrication.Tag = null;
			this.dtDateFabrication.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDateFabrication.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDateFabrication.KeyPress += new KeyPressEventHandler(this.dtDateFabrication_KeyPress);
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(4, 148);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 33;
			this.label3.Text = "Дата установки";
			this.lblPU.Cursor = Cursors.Hand;
			this.lblPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Underline, GraphicsUnit.Point, 204);
			this.lblPU.ForeColor = SystemColors.Desktop;
			this.lblPU.Location = new Point(292, 192);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(120, 20);
			this.lblPU.TabIndex = 7;
			this.toolTip1.SetToolTip(this.lblPU, "Статус ПУ");
			this.lblPU.Click += new EventHandler(this.lblPU_Click);
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(108, 288);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(432, 32);
			this.txtNote.TabIndex = 8;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(4, 288);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 40;
			this.label17.Text = "Примечание";
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(448, 324);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 10;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(344, 324);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 9;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(4, 172);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(128, 16);
			this.label18.TabIndex = 42;
			this.label18.Text = "Начальные показания";
			this.groupBox2.Controls.Add(this.lblAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 96);
			this.groupBox2.TabIndex = 43;
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
			this.groupBox1.Controls.Add(this.lblNameGRU);
			this.groupBox1.Controls.Add(this.lblOU);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(288, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 96);
			this.groupBox1.TabIndex = 44;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Объект учета";
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(40, 56);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(208, 32);
			this.lblNameGRU.TabIndex = 29;
			this.lblOU.BackColor = SystemColors.Info;
			this.lblOU.BorderStyle = BorderStyle.FixedSingle;
			this.lblOU.ForeColor = SystemColors.ControlText;
			this.lblOU.Location = new Point(40, 16);
			this.lblOU.Name = "lblOU";
			this.lblOU.Size = new System.Drawing.Size(208, 32);
			this.lblOU.TabIndex = 30;
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 16);
			this.label5.TabIndex = 28;
			this.label5.Text = "РУ";
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(8, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 16);
			this.label7.TabIndex = 7;
			this.label7.Text = "ОУ";
			this.cmdTypeGMeter.FlatStyle = FlatStyle.Flat;
			this.cmdTypeGMeter.ForeColor = SystemColors.ControlText;
			this.cmdTypeGMeter.ImageIndex = 0;
			this.cmdTypeGMeter.ImageList = this.imageList1;
			this.cmdTypeGMeter.Location = new Point(520, 100);
			this.cmdTypeGMeter.Name = "cmdTypeGMeter";
			this.cmdTypeGMeter.Size = new System.Drawing.Size(20, 20);
			this.cmdTypeGMeter.TabIndex = 3;
			this.cmdTypeGMeter.Click += new EventHandler(this.cmdTypeGMeter_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lblBeginIndication.BackColor = SystemColors.Info;
			this.lblBeginIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblBeginIndication.ForeColor = SystemColors.ControlText;
			this.lblBeginIndication.Location = new Point(132, 172);
			this.lblBeginIndication.Name = "lblBeginIndication";
			this.lblBeginIndication.Size = new System.Drawing.Size(112, 20);
			this.lblBeginIndication.TabIndex = 46;
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(292, 144);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(108, 16);
			this.label8.TabIndex = 47;
			this.label8.Text = "Результат поверки";
			this.lblTypeVerify.BackColor = SystemColors.Info;
			this.lblTypeVerify.BorderStyle = BorderStyle.FixedSingle;
			this.lblTypeVerify.ForeColor = SystemColors.ControlText;
			this.lblTypeVerify.Location = new Point(404, 144);
			this.lblTypeVerify.Name = "lblTypeVerify";
			this.lblTypeVerify.Size = new System.Drawing.Size(136, 20);
			this.lblTypeVerify.TabIndex = 48;
			this.lblAgent.BackColor = SystemColors.Info;
			this.lblAgent.BorderStyle = BorderStyle.FixedSingle;
			this.lblAgent.ForeColor = SystemColors.ControlText;
			this.lblAgent.Location = new Point(404, 168);
			this.lblAgent.Name = "lblAgent";
			this.lblAgent.Size = new System.Drawing.Size(136, 20);
			this.lblAgent.TabIndex = 50;
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(292, 168);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(108, 16);
			this.label13.TabIndex = 49;
			this.label13.Text = "Поверял";
			this.groupBox3.Controls.Add(this.chkIDTypePlombWork);
			this.groupBox3.Controls.Add(this.txtPlombMemo);
			this.groupBox3.Controls.Add(this.label20);
			this.groupBox3.Controls.Add(this.cmbMechanic);
			this.groupBox3.Controls.Add(this.label19);
			this.groupBox3.Controls.Add(this.bShowHistory);
			this.groupBox3.Controls.Add(this.nDisplayPlomb);
			this.groupBox3.Controls.Add(this.label16);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.txtPlomb2);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.txtPlomb1);
			this.groupBox3.Controls.Add(this.dtPlombDate);
			this.groupBox3.Controls.Add(this.label15);
			this.groupBox3.Location = new Point(9, 215);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(531, 69);
			this.groupBox3.TabIndex = 51;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Пломбы";
			this.txtPlombMemo.Location = new Point(296, 40);
			this.txtPlombMemo.Name = "txtPlombMemo";
			this.txtPlombMemo.Size = new System.Drawing.Size(184, 20);
			this.txtPlombMemo.TabIndex = 95;
			this.txtPlombMemo.Text = "";
			this.label20.Location = new Point(257, 44);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(48, 16);
			this.label20.TabIndex = 94;
			this.label20.Text = "Прим.:";
			this.cmbMechanic.AddItemSeparator = ';';
			this.cmbMechanic.BorderStyle = 1;
			this.cmbMechanic.Caption = "";
			this.cmbMechanic.CaptionHeight = 17;
			this.cmbMechanic.CharacterCasing = 0;
			this.cmbMechanic.ColumnCaptionHeight = 17;
			this.cmbMechanic.ColumnFooterHeight = 17;
			this.cmbMechanic.ColumnHeaders = false;
			this.cmbMechanic.ColumnWidth = 100;
			this.cmbMechanic.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbMechanic.ContentHeight = 15;
			this.cmbMechanic.DataMode = DataModeEnum.AddItem;
			this.cmbMechanic.DeadAreaBackColor = Color.Empty;
			this.cmbMechanic.EditorBackColor = SystemColors.Window;
			this.cmbMechanic.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbMechanic.EditorForeColor = SystemColors.WindowText;
			this.cmbMechanic.EditorHeight = 15;
			this.cmbMechanic.FlatStyle = FlatModeEnum.Flat;
			this.cmbMechanic.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbMechanic.ItemHeight = 15;
			this.cmbMechanic.Location = new Point(344, 17);
			this.cmbMechanic.MatchEntryTimeout = (long)2000;
			this.cmbMechanic.MaxDropDownItems = 5;
			this.cmbMechanic.MaxLength = 32767;
			this.cmbMechanic.MouseCursor = Cursors.Default;
			this.cmbMechanic.Name = "cmbMechanic";
			this.cmbMechanic.RowDivider.Color = Color.DarkGray;
			this.cmbMechanic.RowDivider.Style = LineStyleEnum.None;
			this.cmbMechanic.RowSubDividerColor = Color.DarkGray;
			this.cmbMechanic.Size = new System.Drawing.Size(147, 19);
			this.cmbMechanic.TabIndex = 93;
			this.cmbMechanic.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label19.Location = new Point(257, 20);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(76, 16);
			this.label19.TabIndex = 21;
			this.label19.Text = "Исполнитель";
			this.bShowHistory.FlatStyle = FlatStyle.Flat;
			this.bShowHistory.ImageIndex = 2;
			this.bShowHistory.ImageList = this.imageList1;
			this.bShowHistory.Location = new Point(500, 16);
			this.bShowHistory.Name = "bShowHistory";
			this.bShowHistory.Size = new System.Drawing.Size(20, 20);
			this.bShowHistory.TabIndex = 20;
			this.bShowHistory.Click += new EventHandler(this.bShowHistory_Click);
			this.nDisplayPlomb.BorderStyle = BorderStyle.FixedSingle;
			this.nDisplayPlomb.DecimalPlaces = 3;
			this.nDisplayPlomb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.nDisplayPlomb.Location = new Point(40, 40);
			NumericUpDown num = this.nDisplayPlomb;
			int[] numArray = new int[] { 276447231, 23283, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.nDisplayPlomb.Name = "nDisplayPlomb";
			this.nDisplayPlomb.Size = new System.Drawing.Size(84, 20);
			this.nDisplayPlomb.TabIndex = 19;
			this.nDisplayPlomb.TextAlign = HorizontalAlignment.Right;
			this.label16.Location = new Point(4, 44);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(40, 16);
			this.label16.TabIndex = 8;
			this.label16.Text = "Пок-я:";
			this.label14.Location = new Point(140, 44);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(24, 16);
			this.label14.TabIndex = 3;
			this.label14.Text = "№2";
			this.txtPlomb2.Location = new Point(164, 41);
			this.txtPlomb2.Name = "txtPlomb2";
			this.txtPlomb2.Size = new System.Drawing.Size(92, 20);
			this.txtPlomb2.TabIndex = 2;
			this.txtPlomb2.Text = "";
			this.label9.Location = new Point(140, 20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(24, 16);
			this.label9.TabIndex = 1;
			this.label9.Text = "№1";
			this.txtPlomb1.Location = new Point(164, 16);
			this.txtPlomb1.Name = "txtPlomb1";
			this.txtPlomb1.Size = new System.Drawing.Size(92, 20);
			this.txtPlomb1.TabIndex = 0;
			this.txtPlomb1.Text = "";
			this.dtPlombDate.BorderStyle = 1;
			this.dtPlombDate.FormatType = FormatTypeEnum.ShortDate;
			this.dtPlombDate.Location = new Point(40, 17);
			this.dtPlombDate.Name = "dtPlombDate";
			this.dtPlombDate.Size = new System.Drawing.Size(84, 18);
			this.dtPlombDate.TabIndex = 7;
			this.dtPlombDate.Tag = null;
			this.dtPlombDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtPlombDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label15.Location = new Point(4, 18);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(36, 16);
			this.label15.TabIndex = 4;
			this.label15.Text = "Дата:";
			this.bShowIndication.FlatStyle = FlatStyle.Flat;
			this.bShowIndication.Location = new Point(504, 192);
			this.bShowIndication.Name = "bShowIndication";
			this.bShowIndication.Size = new System.Drawing.Size(36, 23);
			this.bShowIndication.TabIndex = 52;
			this.bShowIndication.Click += new EventHandler(this.bShowIndication_Click);
			this.groupBox4.Controls.Add(this.label21);
			this.groupBox4.Controls.Add(this.dtDate);
			this.groupBox4.Controls.Add(this.groupBox5);
			this.groupBox4.Location = new Point(556, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(296, 348);
			this.groupBox4.TabIndex = 53;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Разноска показаний";
			this.label21.ForeColor = SystemColors.ControlText;
			this.label21.Location = new Point(4, 16);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(88, 16);
			this.label21.TabIndex = 59;
			this.label21.Text = "Дата разноски";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(92, 16);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(152, 18);
			this.dtDate.TabIndex = 2;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.groupBox5.Controls.Add(this.label22);
			this.groupBox5.Controls.Add(this.lblFactUse);
			this.groupBox5.Controls.Add(this.label23);
			this.groupBox5.Controls.Add(this.cmbAgent);
			this.groupBox5.Controls.Add(this.label24);
			this.groupBox5.Controls.Add(this.cmbTypeIndication);
			this.groupBox5.Controls.Add(this.cmdApply1);
			this.groupBox5.Controls.Add(this.lblDateIndication);
			this.groupBox5.Controls.Add(this.label25);
			this.groupBox5.Controls.Add(this.numNewIndication);
			this.groupBox5.Controls.Add(this.lblCurrentIndication);
			this.groupBox5.Controls.Add(this.label26);
			this.groupBox5.Controls.Add(this.label27);
			this.groupBox5.ForeColor = SystemColors.Desktop;
			this.groupBox5.Location = new Point(8, 44);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(280, 200);
			this.groupBox5.TabIndex = 60;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Прием показаний";
			this.label22.ForeColor = SystemColors.ControlText;
			this.label22.Location = new Point(4, 136);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(128, 16);
			this.label22.TabIndex = 15;
			this.label22.Text = "Потребление";
			this.lblFactUse.BackColor = SystemColors.Info;
			this.lblFactUse.BorderStyle = BorderStyle.FixedSingle;
			this.lblFactUse.ForeColor = SystemColors.ControlText;
			this.lblFactUse.Location = new Point(144, 136);
			this.lblFactUse.Name = "lblFactUse";
			this.lblFactUse.Size = new System.Drawing.Size(128, 20);
			this.lblFactUse.TabIndex = 14;
			this.label23.ForeColor = SystemColors.ControlText;
			this.label23.Location = new Point(4, 40);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(92, 16);
			this.label23.TabIndex = 13;
			this.label23.Text = "Агент";
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
			this.cmbAgent.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.cmbAgent.ItemHeight = 15;
			this.cmbAgent.Location = new Point(100, 40);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(176, 19);
			this.cmbAgent.TabIndex = 5;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label24.ForeColor = SystemColors.ControlText;
			this.label24.Location = new Point(4, 16);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(92, 16);
			this.label24.TabIndex = 11;
			this.label24.Text = "Тип показаний";
			this.cmbTypeIndication.AddItemSeparator = ';';
			this.cmbTypeIndication.BorderStyle = 1;
			this.cmbTypeIndication.Caption = "";
			this.cmbTypeIndication.CaptionHeight = 17;
			this.cmbTypeIndication.CharacterCasing = 0;
			this.cmbTypeIndication.ColumnCaptionHeight = 17;
			this.cmbTypeIndication.ColumnFooterHeight = 17;
			this.cmbTypeIndication.ColumnHeaders = false;
			this.cmbTypeIndication.ColumnWidth = 100;
			this.cmbTypeIndication.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeIndication.ContentHeight = 15;
			this.cmbTypeIndication.DataMode = DataModeEnum.AddItem;
			this.cmbTypeIndication.DeadAreaBackColor = Color.Empty;
			this.cmbTypeIndication.EditorBackColor = SystemColors.Window;
			this.cmbTypeIndication.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeIndication.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeIndication.EditorHeight = 15;
			this.cmbTypeIndication.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeIndication.Images.Add((Image)resourceManager.GetObject("resource3"));
			this.cmbTypeIndication.ItemHeight = 15;
			this.cmbTypeIndication.Location = new Point(100, 16);
			this.cmbTypeIndication.MatchEntryTimeout = (long)2000;
			this.cmbTypeIndication.MaxDropDownItems = 5;
			this.cmbTypeIndication.MaxLength = 32767;
			this.cmbTypeIndication.MouseCursor = Cursors.Default;
			this.cmbTypeIndication.Name = "cmbTypeIndication";
			this.cmbTypeIndication.RowDivider.Color = Color.DarkGray;
			this.cmbTypeIndication.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeIndication.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeIndication.Size = new System.Drawing.Size(176, 19);
			this.cmbTypeIndication.TabIndex = 4;
			this.cmbTypeIndication.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.cmdApply1.FlatStyle = FlatStyle.Flat;
			this.cmdApply1.ForeColor = SystemColors.ControlText;
			this.cmdApply1.Location = new Point(156, 168);
			this.cmdApply1.Name = "cmdApply1";
			this.cmdApply1.Size = new System.Drawing.Size(120, 24);
			this.cmdApply1.TabIndex = 7;
			this.cmdApply1.Text = "Принять";
			this.cmdApply1.Click += new EventHandler(this.cmdApply1_Click);
			this.lblDateIndication.BackColor = SystemColors.Info;
			this.lblDateIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblDateIndication.ForeColor = SystemColors.ControlText;
			this.lblDateIndication.Location = new Point(144, 88);
			this.lblDateIndication.Name = "lblDateIndication";
			this.lblDateIndication.Size = new System.Drawing.Size(128, 20);
			this.lblDateIndication.TabIndex = 8;
			this.label25.ForeColor = SystemColors.ControlText;
			this.label25.Location = new Point(4, 64);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(128, 16);
			this.label25.TabIndex = 7;
			this.label25.Text = "Текущие показания";
			this.numNewIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numNewIndication.DecimalPlaces = 2;
			this.numNewIndication.Location = new Point(144, 112);
			NumericUpDown numericUpDown = this.numNewIndication;
			numArray = new int[] { 99999, 0, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray);
			this.numNewIndication.Name = "numNewIndication";
			this.numNewIndication.Size = new System.Drawing.Size(128, 20);
			this.numNewIndication.TabIndex = 6;
			this.numNewIndication.TextAlign = HorizontalAlignment.Right;
			this.numNewIndication.KeyPress += new KeyPressEventHandler(this.numNewIndication_KeyPress);
			this.numNewIndication.Enter += new EventHandler(this.numNewIndication_Enter);
			this.numNewIndication.Leave += new EventHandler(this.numNewIndication_Leave);
			this.lblCurrentIndication.BackColor = SystemColors.Info;
			this.lblCurrentIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblCurrentIndication.ForeColor = SystemColors.ControlText;
			this.lblCurrentIndication.Location = new Point(144, 64);
			this.lblCurrentIndication.Name = "lblCurrentIndication";
			this.lblCurrentIndication.Size = new System.Drawing.Size(128, 20);
			this.lblCurrentIndication.TabIndex = 6;
			this.label26.ForeColor = SystemColors.ControlText;
			this.label26.Location = new Point(4, 112);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(120, 16);
			this.label26.TabIndex = 4;
			this.label26.Text = "Новые показания";
			this.label27.ForeColor = SystemColors.ControlText;
			this.label27.Location = new Point(4, 88);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(148, 16);
			this.label27.TabIndex = 3;
			this.label27.Text = "Дата текущих показаний";
			this.chkIDTypePlombWork.Location = new Point(488, 40);
			this.chkIDTypePlombWork.Name = "chkIDTypePlombWork";
			this.chkIDTypePlombWork.Size = new System.Drawing.Size(48, 24);
			this.chkIDTypePlombWork.TabIndex = 96;
			this.chkIDTypePlombWork.Text = "уст.";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(862, 356);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.bShowIndication);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.lblAgent);
			base.Controls.Add(this.label13);
			base.Controls.Add(this.lblTypeVerify);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.lblBeginIndication);
			base.Controls.Add(this.cmdTypeGMeter);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.label18);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.txtSerialNumber);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.dtDateFabrication);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.dtDateVerify);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dtDateInstall);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.cmbTypeGMeter);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.lblPU);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MinimumSize = new System.Drawing.Size(552, 388);
			base.Name = "frmGMeter";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Прибор учета";
			base.Closing += new CancelEventHandler(this.frmGMeter_Closing);
			base.Load += new EventHandler(this.frmGMeter_Load);
			((ISupportInitialize)this.cmbTypeGMeter).EndInit();
			((ISupportInitialize)this.dtDateInstall).EndInit();
			((ISupportInitialize)this.dtDateVerify).EndInit();
			((ISupportInitialize)this.dtDateFabrication).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.cmbMechanic).EndInit();
			((ISupportInitialize)this.nDisplayPlomb).EndInit();
			((ISupportInitialize)this.dtPlombDate).EndInit();
			this.groupBox4.ResumeLayout(false);
			((ISupportInitialize)this.dtDate).EndInit();
			this.groupBox5.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgent).EndInit();
			((ISupportInitialize)this.cmbTypeIndication).EndInit();
			((ISupportInitialize)this.numNewIndication).EndInit();
			base.ResumeLayout(false);
		}

		private void lblPU_Click(object sender, EventArgs e)
		{
			if (this._gmeter.oTypeVerify.get_ID() == (long)2 && this._gmeter.oStatusGMeter.get_ID() == (long)2)
			{
				MessageBox.Show("Данный ПУ не может быть подключен!", "ПУ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			(new frmActionGMeter(this._gmeter)).ShowDialog(this);
			if (this._gmeter.oStatusGMeter.get_ID() == (long)1)
			{
				this.lblPU.Text = "Подключен";
				return;
			}
			this.lblPU.Text = "Отключен";
		}

		private void numNewIndication_Enter(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = this.numNewIndication;
			decimal value = this.numNewIndication.Value;
			numericUpDown.Select(0, value.ToString().Length);
		}

		private void numNewIndication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numNewIndication_Leave(null, null);
			}
			this.cmdApply1.Focus();
		}

		private void numNewIndication_Leave(object sender, EventArgs e)
		{
			try
			{
				this.FactAmount = -1;
				if (this.numNewIndication.Value <= new decimal(0))
				{
					this.cmdApply1.Enabled = false;
				}
				else
				{
					string str = "";
					try
					{
						this.tmpIndication.Datedisplay = (DateTime)this.dtDate.Value;
						this.tmpIndication.Display = Convert.ToDouble(this.numNewIndication.Value);
						str = this.tmpIndication.CalcFactUse();
						this.FactAmount = Convert.ToDouble(str);
						this.cmdApply1.Enabled = true;
					}
					catch
					{
						MessageBox.Show(str, "Показания прибора учета", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						this.numNewIndication.Value = new decimal(0);
						this.numNewIndication.Focus();
					}
				}
				this.lblFactUse.Text = Convert.ToString(Math.Round(this.FactAmount, 2));
			}
			catch
			{
				MessageBox.Show("Ошибка расчета потребления!", "Ввод показаний", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void txtNote_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdOK.Focus();
			}
		}

		private void txtSerialNumber_Enter(object sender, EventArgs e)
		{
			this.txtSerialNumber.SelectAll();
		}

		private void txtSerialNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbTypeGMeter.Focus();
			}
		}
	}
}