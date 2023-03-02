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
	public class frmFindContract : Form
	{
		private List lvContent;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private TabPage tabPage4;

		private Label label2;

		private TextBox txtAccount;

		private Label label1;

		private Label label4;

		private Label label5;

		private Label label3;

		private Label label6;

		private C1Combo cmbStreet;

		private C1Combo cmbHouse2;

		private ImageList imageList1;

		private ToolBarButton toolBarButton2;

		private ToolBarButton toolBarButton3;

		private ToolBarButton toolBarButton4;

		private ToolBarButton toolBarButton5;

		private ToolBarButton toolBarButton6;

		private ToolBarButton toolBarButton7;

		private ToolBarButton toolBarButton8;

		private ToolBarButton toolBarButton9;

		private ToolBarButton toolBarButton10;

		private ToolBarButton toolBarButton11;

		private ToolBarButton toolBarButton12;

		private ToolBar tbMain;

		private ListView lvContract;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private TabControl tabFind;

		private TextBox txtFIO;

		private TextBox txtRNN;

		private IContainer components;

		private Place _place;

		private C1Combo cmbHouse1;

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuItem1;

		private MenuItem menuItem2;

		private ColumnHeader columnHeader9;

		private Streets _streets;

		private ToolBarButton toolBarButton1;

		private TabPage tabPage5;

		private Label label7;

		private TextBox txtNumberPU;

		private Button cmdSearch;

		private ColumnHeader columnHeader10;

		private CheckBox chkFIO;

		private TabPage tabPage6;

		private Label label8;

		private TextBox txtFIOS;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader12;

		private TabPage tabPage7;

		private Label label9;

		private TextBox txtPhoneNumber;

		private ListViewSortManager m_sortMgr1;

		public frmFindContract()
		{
			this.InitializeComponent();
			ListView listView = this.lvContract;
			Type[] typeArray = new Type[] { typeof(ListViewInt64Sort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewInt64Sort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
			this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
		}

		private void cmbHouse1_TextChanged(object sender, EventArgs e)
		{
			this.cmbHouse2.SelectedIndex = this.cmbHouse1.SelectedIndex;
		}

		private void cmbStreet_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbHouse1.Focus();
			}
		}

		private void cmbStreet_Leave(object sender, EventArgs e)
		{
			base.AcceptButton = this.cmdSearch;
		}

		private void cmbStreet_TextChanged(object sender, EventArgs e)
		{
			base.AcceptButton = null;
			this.CreateHouse();
		}

		private void cmdSearch_Click(object sender, EventArgs e)
		{
			this.SearchConsumer(this.tabFind.SelectedIndex);
		}

		private void CreateHouse()
		{
			// 
			// Current member / type: System.Void Gefest.frmFindContract::CreateHouse()
			// File path: C:\Program Files (x86)\gorgaz\Gefest\Gefest.exe
			// 
			// Product version: 2019.1.118.0
			// Exception in: System.Void CreateHouse()
			// 
			// Not supported type System.EventHandler.
			//    at Telerik.JustDecompiler.Ast.Expressions.BinaryExpression.(TypeDefinition ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\Expressions\BinaryExpression.cs:line 684
			//    at Telerik.JustDecompiler.Ast.Expressions.BinaryExpression.(TypeDefinition , TypeDefinition ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\Expressions\BinaryExpression.cs:line 597
			//    at Telerik.JustDecompiler.Ast.Expressions.BinaryExpression.() in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\Expressions\BinaryExpression.cs:line 490
			//    at Telerik.JustDecompiler.Ast.Expressions.BinaryExpression.() in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\Expressions\BinaryExpression.cs:line 228
			//    at ..( , Expression , String , IEnumerable`1 ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\RebuildEventsStep.cs:line 95
			//    at ..( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\RebuildEventsStep.cs:line 77
			//    at ..( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\CombinedTransformerStep.cs:line 86
			//    at ..(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 87
			//    at ..Visit(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 276
			//    at ..( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 383
			//    at ..(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 59
			//    at ..Visit(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 276
			//    at ..Visit[,]( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 286
			//    at ..Visit( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 317
			//    at ..( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 337
			//    at ..(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 49
			//    at ..Visit(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 276
			//    at ..( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 483
			//    at ..(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 83
			//    at ..Visit(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 276
			//    at ..Visit[,]( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 286
			//    at ..Visit( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 317
			//    at ..( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 337
			//    at ..(DecompilationContext ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\CombinedTransformerStep.cs:line 44
			//    at ..(MethodBody ,  , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 88
			//    at ..(MethodBody , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 70
			//    at Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:line 95
			//    at Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:line 58
			//    at ..(ILanguage , MethodDefinition ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 117
			// 
			// mailto: JustDecompilePublicFeedback@telerik.com

		}

		private void CreateStreet()
		{
			if (this._place == null)
			{
				this._place = new Place();
				bool flag = false;
				string str = Tools.LoadParameter("IDPlace", ref flag);
				if (str.Length > 0)
				{
					this._place.Load((long)Tools.ConvertToLong(str));
				}
			}
			if (this._streets == null)
			{
				this._streets = this._place.get_oStreets();
			}
			try
			{
				this.cmbStreet.Enabled = true;
				Tools.FillC1(this._streets, this.cmbStreet, (long)0);
				this.CreateHouse();
			}
			catch
			{
				this.cmbStreet.Enabled = false;
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

		private void EditContract()
		{
			if (this.lvContract.SelectedItems.Count > 0)
			{
				string str = this.lvContract.SelectedItems[0].Tag.ToString();
				char[] chr = new char[] { Convert.ToChar(";") };
				string[] strArrays = str.Split(chr);
				if (Convert.ToInt64(strArrays[0]) > (long)0)
				{
					Contract contract = new Contract();
					contract.Load(Convert.ToInt64(strArrays[0]));
					(new frmContract(contract)).ShowDialog(this);
					contract = null;
				}
				this.lvContract.Select();
			}
		}

		private void EditPerson()
		{
			Form _frmPerson;
			if (this.lvContract.SelectedItems.Count > 0)
			{
				string str = this.lvContract.SelectedItems[0].Tag.ToString();
				char[] chr = new char[] { Convert.ToChar(";") };
				string[] strArrays = str.Split(chr);
				if (Convert.ToInt64(strArrays[1]) > (long)0)
				{
					Person person = new Person();
					person.Load(Convert.ToInt64(strArrays[1]));
					if (person.isJuridical != 1)
					{
						_frmPerson = new frmPerson(person);
					}
					else
					{
						_frmPerson = new frmJPerson(person);
					}
					_frmPerson.ShowDialog(this);
					_frmPerson = null;
					this.lvContract.SelectedItems[0].SubItems[1].Text = person.FullName;
				}
			}
		}

		private void frmFindContract_Closing(object sender, CancelEventArgs e)
		{
			Depot.status = new string[] { "", "" };
			this.lvContent = null;
			this._place = null;
			this._streets = null;
		}

		private void frmFindContract_Load(object sender, EventArgs e)
		{
			base.AcceptButton = this.cmdSearch;
			this.tabFind_SelectedIndexChanged(null, null);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmFindContract));
			this.tabFind = new TabControl();
			this.tabPage1 = new TabPage();
			this.chkFIO = new CheckBox();
			this.txtAccount = new TextBox();
			this.label2 = new Label();
			this.tabPage2 = new TabPage();
			this.cmbHouse1 = new C1Combo();
			this.cmbHouse2 = new C1Combo();
			this.cmbStreet = new C1Combo();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label3 = new Label();
			this.tabPage3 = new TabPage();
			this.txtFIO = new TextBox();
			this.label1 = new Label();
			this.tabPage4 = new TabPage();
			this.txtRNN = new TextBox();
			this.label6 = new Label();
			this.tabPage5 = new TabPage();
			this.txtNumberPU = new TextBox();
			this.label7 = new Label();
			this.tabPage6 = new TabPage();
			this.txtFIOS = new TextBox();
			this.label8 = new Label();
			this.cmdSearch = new Button();
			this.tbMain = new ToolBar();
			this.toolBarButton2 = new ToolBarButton();
			this.toolBarButton3 = new ToolBarButton();
			this.toolBarButton4 = new ToolBarButton();
			this.toolBarButton10 = new ToolBarButton();
			this.toolBarButton5 = new ToolBarButton();
			this.toolBarButton6 = new ToolBarButton();
			this.toolBarButton7 = new ToolBarButton();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new MenuItem();
			this.menuItem2 = new MenuItem();
			this.toolBarButton8 = new ToolBarButton();
			this.toolBarButton1 = new ToolBarButton();
			this.toolBarButton9 = new ToolBarButton();
			this.toolBarButton11 = new ToolBarButton();
			this.toolBarButton12 = new ToolBarButton();
			this.imageList1 = new ImageList(this.components);
			this.lvContract = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader12 = new ColumnHeader();
			this.tabPage7 = new TabPage();
			this.label9 = new Label();
			this.txtPhoneNumber = new TextBox();
			this.tabFind.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.cmbHouse1).BeginInit();
			((ISupportInitialize)this.cmbHouse2).BeginInit();
			((ISupportInitialize)this.cmbStreet).BeginInit();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.tabPage7.SuspendLayout();
			base.SuspendLayout();
			this.tabFind.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tabFind.Appearance = TabAppearance.FlatButtons;
			this.tabFind.Controls.Add(this.tabPage1);
			this.tabFind.Controls.Add(this.tabPage2);
			this.tabFind.Controls.Add(this.tabPage3);
			this.tabFind.Controls.Add(this.tabPage4);
			this.tabFind.Controls.Add(this.tabPage5);
			this.tabFind.Controls.Add(this.tabPage6);
			this.tabFind.Controls.Add(this.tabPage7);
			this.tabFind.Location = new Point(0, 0);
			this.tabFind.Name = "tabFind";
			this.tabFind.SelectedIndex = 0;
			this.tabFind.Size = new System.Drawing.Size(864, 72);
			this.tabFind.TabIndex = 1;
			this.tabFind.SelectedIndexChanged += new EventHandler(this.tabFind_SelectedIndexChanged);
			this.tabPage1.Controls.Add(this.chkFIO);
			this.tabPage1.Controls.Add(this.txtAccount);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Location = new Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(856, 43);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "По лицевому счету";
			this.chkFIO.Location = new Point(10, 24);
			this.chkFIO.Name = "chkFIO";
			this.chkFIO.Size = new System.Drawing.Size(190, 16);
			this.chkFIO.TabIndex = 25;
			this.chkFIO.Text = "дополнительно по ФИО/Наим.";
			this.txtAccount.BorderStyle = BorderStyle.FixedSingle;
			this.txtAccount.Location = new Point(200, 8);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(368, 20);
			this.txtAccount.TabIndex = 0;
			this.txtAccount.Text = "";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(196, 16);
			this.label2.TabIndex = 24;
			this.label2.Text = "Лицевой счет (минимум 4 символа):";
			this.tabPage2.Controls.Add(this.cmbHouse1);
			this.tabPage2.Controls.Add(this.cmbHouse2);
			this.tabPage2.Controls.Add(this.cmbStreet);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Location = new Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(856, 43);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "По адресу";
			this.cmbHouse1.AddItemSeparator = ';';
			this.cmbHouse1.BorderStyle = 1;
			this.cmbHouse1.Caption = "";
			this.cmbHouse1.CaptionHeight = 17;
			this.cmbHouse1.CharacterCasing = 0;
			this.cmbHouse1.ColumnCaptionHeight = 17;
			this.cmbHouse1.ColumnFooterHeight = 17;
			this.cmbHouse1.ColumnHeaders = false;
			this.cmbHouse1.ColumnWidth = 100;
			this.cmbHouse1.ContentHeight = 15;
			this.cmbHouse1.DataMode = DataModeEnum.AddItem;
			this.cmbHouse1.DeadAreaBackColor = Color.Empty;
			this.cmbHouse1.EditorBackColor = SystemColors.Window;
			this.cmbHouse1.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbHouse1.EditorForeColor = SystemColors.WindowText;
			this.cmbHouse1.EditorHeight = 15;
			this.cmbHouse1.FlatStyle = FlatModeEnum.Popup;
			this.cmbHouse1.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbHouse1.ItemHeight = 15;
			this.cmbHouse1.Location = new Point(64, 24);
			this.cmbHouse1.MatchEntryTimeout = (long)2000;
			this.cmbHouse1.MaxDropDownItems = 10;
			this.cmbHouse1.MaxLength = 32767;
			this.cmbHouse1.MouseCursor = Cursors.Default;
			this.cmbHouse1.Name = "cmbHouse1";
			this.cmbHouse1.RowDivider.Color = Color.DarkGray;
			this.cmbHouse1.RowDivider.Style = LineStyleEnum.None;
			this.cmbHouse1.RowSubDividerColor = Color.DarkGray;
			this.cmbHouse1.Size = new System.Drawing.Size(120, 19);
			this.cmbHouse1.TabIndex = 46;
			this.cmbHouse1.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"C дома\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.cmbHouse2.AddItemSeparator = ';';
			this.cmbHouse2.BorderStyle = 1;
			this.cmbHouse2.Caption = "";
			this.cmbHouse2.CaptionHeight = 17;
			this.cmbHouse2.CharacterCasing = 0;
			this.cmbHouse2.ColumnCaptionHeight = 17;
			this.cmbHouse2.ColumnFooterHeight = 17;
			this.cmbHouse2.ColumnHeaders = false;
			this.cmbHouse2.ColumnWidth = 100;
			this.cmbHouse2.ContentHeight = 15;
			this.cmbHouse2.DataMode = DataModeEnum.AddItem;
			this.cmbHouse2.DeadAreaBackColor = Color.Empty;
			this.cmbHouse2.EditorBackColor = SystemColors.Window;
			this.cmbHouse2.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbHouse2.EditorForeColor = SystemColors.WindowText;
			this.cmbHouse2.EditorHeight = 15;
			this.cmbHouse2.FlatStyle = FlatModeEnum.Popup;
			this.cmbHouse2.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbHouse2.ItemHeight = 15;
			this.cmbHouse2.Location = new Point(216, 24);
			this.cmbHouse2.MatchEntryTimeout = (long)2000;
			this.cmbHouse2.MaxDropDownItems = 10;
			this.cmbHouse2.MaxLength = 32767;
			this.cmbHouse2.MouseCursor = Cursors.Default;
			this.cmbHouse2.Name = "cmbHouse2";
			this.cmbHouse2.RowDivider.Color = Color.DarkGray;
			this.cmbHouse2.RowDivider.Style = LineStyleEnum.None;
			this.cmbHouse2.RowSubDividerColor = Color.DarkGray;
			this.cmbHouse2.Size = new System.Drawing.Size(120, 19);
			this.cmbHouse2.TabIndex = 47;
			this.cmbHouse2.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"По дом\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.cmbStreet.AddItemSeparator = ';';
			this.cmbStreet.AutoCompletion = true;
			this.cmbStreet.AutoDropDown = true;
			this.cmbStreet.BorderStyle = 1;
			this.cmbStreet.Caption = "";
			this.cmbStreet.CaptionHeight = 17;
			this.cmbStreet.CharacterCasing = 0;
			this.cmbStreet.ColumnCaptionHeight = 17;
			this.cmbStreet.ColumnFooterHeight = 17;
			this.cmbStreet.ColumnHeaders = false;
			this.cmbStreet.ColumnWidth = 185;
			this.cmbStreet.ContentHeight = 15;
			this.cmbStreet.DataMode = DataModeEnum.AddItem;
			this.cmbStreet.DeadAreaBackColor = Color.Empty;
			this.cmbStreet.EditorBackColor = SystemColors.Window;
			this.cmbStreet.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbStreet.EditorForeColor = SystemColors.WindowText;
			this.cmbStreet.EditorHeight = 15;
			this.cmbStreet.FlatStyle = FlatModeEnum.Popup;
			this.cmbStreet.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.cmbStreet.ItemHeight = 15;
			this.cmbStreet.Location = new Point(64, 0);
			this.cmbStreet.MatchEntryTimeout = (long)2000;
			this.cmbStreet.MaxDropDownItems = 10;
			this.cmbStreet.MaxLength = 32767;
			this.cmbStreet.MouseCursor = Cursors.Default;
			this.cmbStreet.Name = "cmbStreet";
			this.cmbStreet.RowDivider.Color = Color.DarkGray;
			this.cmbStreet.RowDivider.Style = LineStyleEnum.None;
			this.cmbStreet.RowSubDividerColor = Color.DarkGray;
			this.cmbStreet.Size = new System.Drawing.Size(272, 19);
			this.cmbStreet.TabIndex = 45;
			this.cmbStreet.TextChanged += new EventHandler(this.cmbStreet_TextChanged);
			this.cmbStreet.KeyPress += new KeyPressEventHandler(this.cmbStreet_KeyPress);
			this.cmbStreet.Leave += new EventHandler(this.cmbStreet_Leave);
			this.cmbStreet.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Улица\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>185</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label4.Location = new Point(8, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 38;
			this.label4.Text = "Улица";
			this.label5.Location = new Point(8, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 16);
			this.label5.TabIndex = 37;
			this.label5.Text = "Дом      с";
			this.label3.Location = new Point(192, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 16);
			this.label3.TabIndex = 42;
			this.label3.Text = "по";
			this.tabPage3.Controls.Add(this.txtFIO);
			this.tabPage3.Controls.Add(this.label1);
			this.tabPage3.Location = new Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(856, 43);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "По ФИО/Наим.";
			this.txtFIO.BorderStyle = BorderStyle.FixedSingle;
			this.txtFIO.Location = new Point(200, 8);
			this.txtFIO.Name = "txtFIO";
			this.txtFIO.Size = new System.Drawing.Size(368, 20);
			this.txtFIO.TabIndex = 25;
			this.txtFIO.Text = "";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(174, 16);
			this.label1.TabIndex = 26;
			this.label1.Text = "Фамилия (минимум 3 символа):";
			this.tabPage4.Controls.Add(this.txtRNN);
			this.tabPage4.Controls.Add(this.label6);
			this.tabPage4.Location = new Point(4, 25);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(856, 43);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "По ИИН";
			this.txtRNN.BorderStyle = BorderStyle.FixedSingle;
			this.txtRNN.Location = new Point(200, 8);
			this.txtRNN.Name = "txtRNN";
			this.txtRNN.Size = new System.Drawing.Size(368, 20);
			this.txtRNN.TabIndex = 27;
			this.txtRNN.Text = "";
			this.label6.AutoSize = true;
			this.label6.Location = new Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(155, 16);
			this.label6.TabIndex = 28;
			this.label6.Text = "ИИН (минимум 6 символов):";
			this.tabPage5.Controls.Add(this.txtNumberPU);
			this.tabPage5.Controls.Add(this.label7);
			this.tabPage5.Location = new Point(4, 25);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(856, 43);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "По номеру ПУ";
			this.txtNumberPU.BorderStyle = BorderStyle.FixedSingle;
			this.txtNumberPU.Location = new Point(200, 8);
			this.txtNumberPU.Name = "txtNumberPU";
			this.txtNumberPU.Size = new System.Drawing.Size(368, 20);
			this.txtNumberPU.TabIndex = 29;
			this.txtNumberPU.Text = "";
			this.label7.AutoSize = true;
			this.label7.Location = new Point(8, 11);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(160, 16);
			this.label7.TabIndex = 30;
			this.label7.Text = "№ ПУ (минимум 4 символов):";
			this.tabPage6.Controls.Add(this.txtFIOS);
			this.tabPage6.Controls.Add(this.label8);
			this.tabPage6.Location = new Point(4, 25);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(856, 43);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "По ФИО";
			this.txtFIOS.BorderStyle = BorderStyle.FixedSingle;
			this.txtFIOS.Location = new Point(192, 8);
			this.txtFIOS.Name = "txtFIOS";
			this.txtFIOS.Size = new System.Drawing.Size(376, 20);
			this.txtFIOS.TabIndex = 28;
			this.txtFIOS.Text = "";
			this.label8.AutoSize = true;
			this.label8.Location = new Point(8, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(174, 16);
			this.label8.TabIndex = 27;
			this.label8.Text = "Фамилия (минимум 3 символа):";
			this.cmdSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.cmdSearch.FlatStyle = FlatStyle.Flat;
			this.cmdSearch.Location = new Point(864, 40);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(160, 32);
			this.cmdSearch.TabIndex = 2;
			this.cmdSearch.Text = "Поиск";
			this.cmdSearch.Click += new EventHandler(this.cmdSearch_Click);
			this.tbMain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.tbMain.Appearance = ToolBarAppearance.Flat;
			this.tbMain.BorderStyle = BorderStyle.FixedSingle;
			ToolBar.ToolBarButtonCollection buttons = this.tbMain.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.toolBarButton2, this.toolBarButton3, this.toolBarButton4, this.toolBarButton10, this.toolBarButton5, this.toolBarButton6, this.toolBarButton7, this.toolBarButton8, this.toolBarButton1, this.toolBarButton9, this.toolBarButton11, this.toolBarButton12 };
			buttons.AddRange(toolBarButtonArray);
			this.tbMain.Dock = DockStyle.None;
			this.tbMain.DropDownArrows = true;
			this.tbMain.ImageList = this.imageList1;
			this.tbMain.Location = new Point(0, 72);
			this.tbMain.Name = "tbMain";
			this.tbMain.ShowToolTips = true;
			this.tbMain.Size = new System.Drawing.Size(1024, 29);
			this.tbMain.TabIndex = 3;
			this.tbMain.TabStop = true;
			this.tbMain.TextAlign = ToolBarTextAlign.Right;
			this.tbMain.ButtonClick += new ToolBarButtonClickEventHandler(this.tbMain_ButtonClick);
			this.toolBarButton2.Enabled = false;
			this.toolBarButton2.Text = "Договор";
			this.toolBarButton3.ImageIndex = 1;
			this.toolBarButton3.Tag = "EditC";
			this.toolBarButton3.ToolTipText = "Изменить договор";
			this.toolBarButton4.ImageIndex = 2;
			this.toolBarButton4.Tag = "DelC";
			this.toolBarButton4.ToolTipText = "Удалить договор";
			this.toolBarButton10.ImageIndex = 11;
			this.toolBarButton10.Tag = "PrintC";
			this.toolBarButton10.ToolTipText = "Карточка договора";
			this.toolBarButton5.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton6.Enabled = false;
			this.toolBarButton6.Text = "Потребитель";
			this.toolBarButton7.DropDownMenu = this.contextMenu1;
			this.toolBarButton7.ImageIndex = 0;
			this.toolBarButton7.Style = ToolBarButtonStyle.DropDownButton;
			this.toolBarButton7.Tag = "AddP";
			this.toolBarButton7.ToolTipText = "Добавить потребителя";
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.contextMenu1.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuItem1, this.menuItem2 };
			menuItems.AddRange(menuItemArray);
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Физическое лицо";
			this.menuItem1.Click += new EventHandler(this.menuItem1_Click);
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Юридическое лицо";
			this.menuItem2.Click += new EventHandler(this.menuItem2_Click);
			this.toolBarButton8.ImageIndex = 1;
			this.toolBarButton8.Tag = "EditP";
			this.toolBarButton8.ToolTipText = "Изменить потребителя";
			this.toolBarButton1.ImageIndex = 14;
			this.toolBarButton1.Tag = "Juridical";
			this.toolBarButton1.ToolTipText = "Изменение типа потребителя";
			this.toolBarButton9.ImageIndex = 2;
			this.toolBarButton9.Tag = "DelP";
			this.toolBarButton9.ToolTipText = "Удалить потребителя";
			this.toolBarButton11.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton12.ImageIndex = 3;
			this.toolBarButton12.Tag = "Excel";
			this.toolBarButton12.ToolTipText = "Экспорт в Excel";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lvContract.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lvContract.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lvContract.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6, this.columnHeader7, this.columnHeader8, this.columnHeader10, this.columnHeader11, this.columnHeader9, this.columnHeader12 };
			columns.AddRange(columnHeaderArray);
			this.lvContract.FullRowSelect = true;
			this.lvContract.GridLines = true;
			this.lvContract.Location = new Point(0, 104);
			this.lvContract.MultiSelect = false;
			this.lvContract.Name = "lvContract";
			this.lvContract.Size = new System.Drawing.Size(1026, 320);
			this.lvContract.TabIndex = 4;
			this.lvContract.View = View.Details;
			this.lvContract.KeyPress += new KeyPressEventHandler(this.lvContract_KeyPress);
			this.lvContract.DoubleClick += new EventHandler(this.lvContract_DoubleClick);
			this.lvContract.KeyUp += new KeyEventHandler(this.lvContract_KeyUp);
			this.lvContract.Leave += new EventHandler(this.lvContract_Leave);
			this.lvContract.Enter += new EventHandler(this.lvContract_Enter);
			this.lvContract.SelectedIndexChanged += new EventHandler(this.lvContract_SelectedIndexChanged);
			this.columnHeader1.Text = "Л/с";
			this.columnHeader1.Width = 76;
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 143;
			this.columnHeader3.Text = "Адрес";
			this.columnHeader3.Width = 157;
			this.columnHeader4.Text = "Прож.";
			this.columnHeader4.TextAlign = HorizontalAlignment.Right;
			this.columnHeader5.Text = "Сальдо";
			this.columnHeader5.TextAlign = HorizontalAlignment.Right;
			this.columnHeader5.Width = 76;
			this.columnHeader6.Text = "ПУ";
			this.columnHeader6.Width = 79;
			this.columnHeader7.Text = "ОУ";
			this.columnHeader7.Width = 76;
			this.columnHeader8.Text = "Договор";
			this.columnHeader8.Width = 85;
			this.columnHeader10.Text = "Дата изготовления ПУ";
			this.columnHeader10.Width = 70;
			this.columnHeader11.Text = "Дата поверки ПУ";
			this.columnHeader9.Text = "ИИН";
			this.columnHeader9.Width = 88;
			this.columnHeader12.Text = "Соц. положение";
			this.tabPage7.Controls.Add(this.txtPhoneNumber);
			this.tabPage7.Controls.Add(this.label9);
			this.tabPage7.Location = new Point(4, 25);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Size = new System.Drawing.Size(856, 43);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.Text = "По номеру телефона";
			this.label9.AutoSize = true;
			this.label9.Location = new Point(8, 11);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(232, 16);
			this.label9.TabIndex = 25;
			this.label9.Text = "Номер телефона (ХХ-ХХ-ХХ или ХХХХХХ):";
			this.label9.Click += new EventHandler(this.label9_Click);
			this.txtPhoneNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtPhoneNumber.Location = new Point(240, 8);
			this.txtPhoneNumber.Name = "txtPhoneNumber";
			this.txtPhoneNumber.Size = new System.Drawing.Size(376, 20);
			this.txtPhoneNumber.TabIndex = 29;
			this.txtPhoneNumber.Text = "";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(1024, 421);
			base.Controls.Add(this.lvContract);
			base.Controls.Add(this.tbMain);
			base.Controls.Add(this.cmdSearch);
			base.Controls.Add(this.tabFind);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.MinimumSize = new System.Drawing.Size(750, 230);
			base.Name = "frmFindContract";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Поиск договора/потребителя";
			base.Closing += new CancelEventHandler(this.frmFindContract_Closing);
			base.Load += new EventHandler(this.frmFindContract_Load);
			this.tabFind.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((ISupportInitialize)this.cmbHouse1).EndInit();
			((ISupportInitialize)this.cmbHouse2).EndInit();
			((ISupportInitialize)this.cmbStreet).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.tabPage6.ResumeLayout(false);
			this.tabPage7.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void label9_Click(object sender, EventArgs e)
		{
		}

		private void lvContract_DoubleClick(object sender, EventArgs e)
		{
			this.EditContract();
		}

		private void lvContract_Enter(object sender, EventArgs e)
		{
			base.AcceptButton = null;
		}

		private void lvContract_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.EditContract();
			}
		}

		private void lvContract_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
			{
				base.AcceptButton = null;
			}
		}

		private void lvContract_Leave(object sender, EventArgs e)
		{
			base.AcceptButton = this.cmdSearch;
		}

		private void lvContract_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			Person person = new Person()
			{
				isJuridical = 0
			};
			(new frmPerson(person)).ShowDialog(this);
			person = null;
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			Person person = new Person()
			{
				isJuridical = 1
			};
			(new frmJPerson(person)).ShowDialog(this);
			person = null;
		}

		private void PrintAccount()
		{
			if (this.lvContract.SelectedItems.Count > 0)
			{
				string str = this.lvContract.SelectedItems[0].Tag.ToString();
				char[] chr = new char[] { Convert.ToChar(";") };
				string[] strArrays = str.Split(chr);
				if (Convert.ToInt64(strArrays[0]) > (long)0)
				{
					Contract contract = new Contract();
					contract.Load(Convert.ToInt64(strArrays[0]));
					frmPrintAccount _frmPrintAccount = new frmPrintAccount()
					{
						_contract = contract,
						MdiParent = Depot._main
					};
					_frmPrintAccount.Show();
					_frmPrintAccount.WindowState = FormWindowState.Normal;
					base.Invalidate(true);
					_frmPrintAccount = null;
					contract = null;
				}
				this.lvContract.Select();
			}
		}

		private void SearchConsumer(int TypeSearch)
		{
			int houseNumber;
			IEnumerator enumerator;
			if (this.lvContent == null)
			{
				this.lvContent = new List();
			}
			string str = "";
			switch (TypeSearch)
			{
				case 0:
				{
					if (this.txtAccount.Text.Length <= 3)
					{
						break;
					}
					str = string.Concat("select c.idcontract, p.idperson, c.account, case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO, isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 104), 10) else ' ' end date , p.RNN, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateVerify, 104), 10) else ' ' end DateVerify, os.Name from person p inner join contract c on c.idperson=p.idperson  and c.account like '", this.txtAccount.Text, "%' left join gobject g on g.idcontract=c.idcontract left join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject left join address a on a.idaddress=g.idaddress left join house h on h.idhouse=a.idhouse left join street s on s.idstreet=h.idstreet left join gmeter gm on g.idgobject=gm.idgobject and gm.idstatusgmeter=1 left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter left join OwnerShip os on p.IDOwnership=os.IDOwnership order by account");
					break;
				}
				case 1:
				{
					long d = this._streets.get_Item(this.cmbStreet.SelectedIndex).get_ID();
					str = string.Concat("select c.idcontract, p.idperson, c.account, case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO, isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract,  case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 104), 10) else ' ' end date, p.RNN, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateVerify, 104), 10) else ' ' end DateVerify, os.Name from person p inner join contract c on c.idperson=p.idperson inner join gobject g on g.idcontract=c.idcontract inner join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject inner join address a on a.idaddress=g.idaddress inner join house h on h.idhouse=a.idhouse inner join street s on s.idstreet=h.idstreet left join gmeter gm on g.idgobject=gm.idgobject and gm.idstatusgmeter=1  left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter left join OwnerShip os on p.IDOwnership=os.IDOwnership where s.IdStreet= ", d.ToString(), " ");
					if (this.cmbHouse1.SelectedIndex > 0)
					{
						if (this._streets.get_Item(this.cmbStreet.SelectedIndex).get_oHouses().get_Item(this.cmbHouse1.SelectedIndex - 1).get_ID() != this._streets.get_Item(this.cmbStreet.SelectedIndex).get_oHouses().get_Item(this.cmbHouse2.SelectedIndex - 1).get_ID())
						{
							houseNumber = this._streets.get_Item(this.cmbStreet.SelectedIndex).get_oHouses().get_Item(this.cmbHouse1.SelectedIndex - 1).get_HouseNumber();
							str = string.Concat(str, " and h.housenumber>=", houseNumber.ToString());
							houseNumber = this._streets.get_Item(this.cmbStreet.SelectedIndex).get_oHouses().get_Item(this.cmbHouse2.SelectedIndex - 1).get_HouseNumber();
							str = string.Concat(str, " and h.housenumber<=", houseNumber.ToString());
						}
						else
						{
							d = this._streets.get_Item(this.cmbStreet.SelectedIndex).get_oHouses().get_Item(this.cmbHouse2.SelectedIndex - 1).get_ID();
							str = string.Concat(str, " and h.idhouse=", d.ToString());
						}
					}
					str = string.Concat(str, " order by account ");
					break;
				}
				case 2:
				{
					if (this.txtFIO.Text.Length <= 2)
					{
						break;
					}
					str = string.Concat("select c.idcontract, p.idperson, c.account,  case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO,  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 104), 10) else ' ' end date, p.RNN, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateVerify, 104), 10) else ' ' end DateVerify, os.Name from person p  inner join address a on a.idaddress=p.idaddress  and p.Surname like '", this.txtFIO.Text, "%' inner join house h on h.idhouse=a.idhouse  inner join street s on s.idstreet=h.idstreet  left join contract c on c.idperson=p.idperson  left join gobject g on g.idcontract=c.idcontract  left join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject  left join gmeter gm on g.idgobject=gm.idgobject  and gm.idstatusgmeter=1  left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter left join OwnerShip os on p.IDOwnership=os.IDOwnership order by case when p.isJuridical=1 then p.Name else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end");
					break;
				}
				case 3:
				{
					if (this.txtRNN.Text.Length <= 5)
					{
						break;
					}
					str = string.Concat("select c.idcontract, p.idperson, c.account,  case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO,  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract,  case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 104), 10) else ' ' end date, p.RNN, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateVerify, 104), 10) else ' ' end DateVerify, os.Name from person p  inner join address a on a.idaddress=p.idaddress  and p.RNN like '", this.txtRNN.Text, "%' inner join house h on h.idhouse=a.idhouse  inner join street s on s.idstreet=h.idstreet  left join contract c on c.idperson=p.idperson  left join gobject g on g.idcontract=c.idcontract  left join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject  left join gmeter gm on g.idgobject=gm.idgobject  and gm.idstatusgmeter=1  left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter left join OwnerShip os on p.IDOwnership=os.IDOwnership order by c.Account");
					break;
				}
				case 4:
				{
					if (this.txtNumberPU.Text.Length <= 3)
					{
						break;
					}
					str = string.Concat("select c.idcontract, p.idperson, c.account,  case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO,  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 104), 10) else ' ' end date, p.RNN, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateVerify, 104), 10) else ' ' end DateVerify, os.Name from person p  inner join address a on a.idaddress=p.idaddress  inner join house h on h.idhouse=a.idhouse  inner join street s on s.idstreet=h.idstreet  inner join contract c on c.idperson=p.idperson  inner join gobject g on g.idcontract=c.idcontract  inner join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject  inner join gmeter gm on g.idgobject=gm.idgobject  and serialnumber like '", this.txtNumberPU.Text, "%' inner join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter left join OwnerShip os on p.IDOwnership=os.IDOwnership order by c.Account");
					break;
				}
				case 5:
				{
					if (this.txtFIOS.Text.Length <= 3)
					{
						break;
					}
					str = string.Concat("select c.idcontract, p.idperson, c.account,  case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO,  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 104), 10) else ' ' end date, p.RNN, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateVerify, 104), 10) else ' ' end DateVerify, os.Name from person p  inner join address a on a.idaddress=p.idaddress  inner join house h on h.idhouse=a.idhouse  inner join street s on s.idstreet=h.idstreet  inner join contract c on c.idperson=p.idperson  inner join gobject g on g.idcontract=c.idcontract  inner join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject  inner join gmeter gm on g.idgobject=gm.idgobject  inner join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter and p.name like '", this.txtFIOS.Text, "%' left join OwnerShip os on p.IDOwnership=os.IDOwnership order by c.Account");
					break;
				}
				case 6:
				{
					if (this.txtPhoneNumber.Text.Length <= 3)
					{
						break;
					}
					str = string.Concat("select c.idcontract, p.idperson, c.account,  case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO,  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(convert(nchar (5),tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract,  case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 104), 10) else ' ' end date, p.RNN, case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateVerify, 104), 10) else ' ' end DateVerify, os.Name from person p  inner join address a on a.idaddress=p.idaddress  inner join house h on h.idhouse=a.idhouse  inner join street s on s.idstreet=h.idstreet  inner join phone pp on pp.idperson=p.idperson and pp.numberphone like '", this.txtPhoneNumber.Text, "%' left join contract c on c.idperson=p.idperson  left join gobject g on g.idcontract=c.idcontract  left join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject  left join gmeter gm on g.idgobject=gm.idgobject  and gm.idstatusgmeter=1  left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter left join OwnerShip os on p.IDOwnership=os.IDOwnership order by c.Account");
					break;
				}
			}
			this.lvContent.set_nametable_pr("contract");
			this.lvContent.set_select_pr(str);
			this.lvContent.Load();
			this.lvContract.Items.Clear();
			if (this.chkFIO.Checked && TypeSearch == 0 && this.lvContent.get_mylist_pr().Count == 1)
			{
				enumerator = this.lvContent.get_mylist_pr().GetEnumerator();
				try
				{
					if (enumerator.MoveNext())
					{
						string[] current = (string[])enumerator.Current;
						string[] strArrays = current[3].Split(new char[] { ' ' });
						this.txtFIO.Text = strArrays[0];
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this.SearchConsumer(2);
			}
			foreach (string[] mylistPr in this.lvContent.get_mylist_pr())
			{
				ListViewItem listViewItem = this.lvContract.Items.Add(mylistPr[2]);
				listViewItem.SubItems.Add(mylistPr[3]);
				listViewItem.SubItems.Add(mylistPr[4]);
				listViewItem.SubItems.Add(mylistPr[5]);
				listViewItem.SubItems.Add(mylistPr[6]);
				listViewItem.SubItems.Add(mylistPr[7]);
				listViewItem.SubItems.Add(mylistPr[8]);
				listViewItem.SubItems.Add(mylistPr[9]);
				listViewItem.SubItems.Add(mylistPr[10]);
				listViewItem.SubItems.Add(mylistPr[12]);
				listViewItem.SubItems.Add(mylistPr[11]);
				listViewItem.SubItems.Add(mylistPr[13]);
				listViewItem.Tag = string.Concat(mylistPr[0], ";", mylistPr[1]);
			}
			if (this.lvContract.Items.Count > 0)
			{
				this.lvContent.get_mylist_pr().Clear();
				this.lvContract.Focus();
				this.lvContract.Items[0].Selected = true;
			}
			string[] strArrays1 = new string[2];
			houseNumber = this.lvContract.Items.Count;
			strArrays1[0] = string.Concat("Загружено: ", houseNumber.ToString());
			strArrays1[1] = "";
			Depot.status = strArrays1;
		}

		private void tabFind_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (this.tabFind.SelectedIndex)
			{
				case 0:
				{
					this.txtAccount.SelectAll();
					this.txtAccount.Focus();
					break;
				}
				case 1:
				{
					this.CreateStreet();
					this.cmbStreet.Focus();
					break;
				}
				case 2:
				{
					this.txtFIO.SelectAll();
					this.txtFIO.Focus();
					break;
				}
				case 3:
				{
					this.txtRNN.SelectAll();
					this.txtRNN.Focus();
					break;
				}
				case 4:
				{
					this.txtNumberPU.SelectAll();
					this.txtNumberPU.Focus();
					break;
				}
			}
			base.AcceptButton = this.cmdSearch;
		}

		private void tbMain_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			string str;
			"Excel";
			string str1 = e.Button.Tag.ToString();
			string str2 = str1;
			if (str1 != null)
			{
				str2 = string.IsInterned(str2);
				if ((object)str2 == (object)"EditC")
				{
					this.EditContract();
					return;
				}
				if ((object)str2 != (object)"DelC")
				{
					if ((object)str2 == (object)"AddP")
					{
						this.menuItem1_Click(null, null);
						return;
					}
					if ((object)str2 == (object)"EditP")
					{
						this.EditPerson();
						return;
					}
					if ((object)str2 != (object)"DelP")
					{
						if ((object)str2 == (object)"PrintC")
						{
							this.PrintAccount();
							return;
						}
						if ((object)str2 != (object)"Juridical")
						{
							if ((object)str2 != (object)"Excel")
							{
								return;
							}
							Tools.ConvertToExcel(this.lvContract);
						}
						else if (this.lvContract.SelectedItems.Count > 0)
						{
							string str3 = this.lvContract.SelectedItems[0].Tag.ToString();
							char[] chr = new char[] { Convert.ToChar(";") };
							string[] strArrays = str3.Split(chr);
							if (Convert.ToInt64(strArrays[1]) > (long)0)
							{
								Person person = new Person();
								person.Load(Convert.ToInt64(strArrays[1]));
								str = (person.isJuridical != 1 ? "c 'Физическое лицо' на 'Юридическое лицо'?" : "c 'Юридическое лицо' на 'Физическое лицо'?");
								if (MessageBox.Show(string.Concat("Вы действительно хотите изменить тип потребителя ", str), "Потребитель", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
								{
									if (person.isJuridical != 1)
									{
										person.isJuridical = 1;
									}
									else
									{
										person.isJuridical = 0;
									}
									person.Save();
									return;
								}
							}
						}
					}
				}
			}
		}
	}
}