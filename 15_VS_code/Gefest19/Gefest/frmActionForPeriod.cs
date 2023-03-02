using C1.Win.C1Command;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmActionForPeriod : Form
	{
		private Label label1;

		private Label lblPeriod;

		private Button cmdClosePeriod;

		private Button cmdCancel;

		private Button cmdError;

		private ListView lv;

		private ColumnHeader columnHeader6;

		private C1ContextMenu contextMenu1;

		private C1CommandHolder CommandHolder;

		private C1CommandLink menuItem1;

		private C1Command cmd_menuItem1;

		private System.ComponentModel.Container components = null;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ListViewSortManager m_sortMgr1;

		private Button cmdRestore;

		private Button bUnlockSingleUser;

		private int err = 0;

		public frmActionForPeriod()
		{
			this.InitializeComponent();
		}

		private void bUnlockSingleUser_Click(object sender, EventArgs e)
		{
			Tools.LogOff();
			SYSUser sYSUser = new SYSUser();
			sYSUser.set_Name("unsingle");
			sYSUser.set_Password("123");
			if (!sYSUser.LogonUser())
			{
				MessageBox.Show("Ошибка подключения к базе!", "Закрытие периода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (!Saver.ExecuteQueryForSingleUser())
			{
				MessageBox.Show("Ошибка возврата базы в многопользовательский режим!", "Закрытие периода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			Depot.oPeriods = null;
			Label label = this.lblPeriod;
			int year = Depot.CurrentPeriod.Year;
			string str = year.ToString();
			year = Depot.CurrentPeriod.Month;
			label.Text = string.Concat("Год ", str, ", месяц ", year.ToString());
			MessageBox.Show("Закрытие периода прошло успешно!", "Закрытие периода", MessageBoxButtons.OK, MessageBoxIcon.None);
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdClosePeriod_Click(object sender, EventArgs e)
		{
			// 
			// Current member / type: System.Void Gefest.frmActionForPeriod::cmdClosePeriod_Click(System.Object,System.EventArgs)
			// File path: C:\Program Files (x86)\gorgaz\Gefest\Gefest.exe
			// 
			// Product version: 2019.1.118.0
			// Exception in: System.Void cmdClosePeriod_Click(System.Object,System.EventArgs)
			// 
			// Not supported type System.Windows.Forms.ItemCheckEventHandler.
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

		private void cmdError_Click(object sender, EventArgs e)
		{
			if (this.err == 0)
			{
				return;
			}
			if (this.err == 2)
			{
				this.err = 1;
				base.Height = 128;
				this.cmdError.Text = "Ошибки>>";
				return;
			}
			this.err = 2;
			base.Height = 284;
			this.cmdError.Text = "Ошибки<<";
		}

		private void cmdError_EnabledChanged(object sender, EventArgs e)
		{
			if (((Button)sender).Enabled)
			{
				this.err = 1;
				return;
			}
			this.err = 0;
			base.Height = 128;
			this.cmdError.Text = "Ошибки>>";
		}

		private void cmdRestore_Click(object sender, EventArgs e)
		{
			// 
			// Current member / type: System.Void Gefest.frmActionForPeriod::cmdRestore_Click(System.Object,System.EventArgs)
			// File path: C:\Program Files (x86)\gorgaz\Gefest\Gefest.exe
			// 
			// Product version: 2019.1.118.0
			// Exception in: System.Void cmdRestore_Click(System.Object,System.EventArgs)
			// 
			// Not supported type System.Windows.Forms.ItemCheckEventHandler.
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmActionForPeriod_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmActionForPeriod_Load(object sender, EventArgs e)
		{
			ListView listView = this.lv;
			Type[] typeArray = new Type[] { typeof(ListViewInt32Sort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
			this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
			base.Height = 128;
			Label label = this.lblPeriod;
			int year = Depot.CurrentPeriod.Year;
			string str = year.ToString();
			year = Depot.CurrentPeriod.Month;
			label.Text = string.Concat("Год ", str, ", месяц ", year.ToString());
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.lblPeriod = new Label();
			this.cmdClosePeriod = new Button();
			this.cmdCancel = new Button();
			this.cmdError = new Button();
			this.lv = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader1 = new ColumnHeader();
			this.contextMenu1 = new C1ContextMenu();
			this.menuItem1 = new C1CommandLink();
			this.cmd_menuItem1 = new C1Command();
			this.CommandHolder = new C1CommandHolder();
			this.cmdRestore = new Button();
			this.bUnlockSingleUser = new Button();
			((ISupportInitialize)this.CommandHolder).BeginInit();
			base.SuspendLayout();
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Текущий период:";
			this.lblPeriod.BackColor = SystemColors.Info;
			this.lblPeriod.Location = new Point(108, 8);
			this.lblPeriod.Name = "lblPeriod";
			this.lblPeriod.Size = new System.Drawing.Size(252, 23);
			this.lblPeriod.TabIndex = 1;
			this.cmdClosePeriod.FlatStyle = FlatStyle.Flat;
			this.cmdClosePeriod.Location = new Point(124, 40);
			this.cmdClosePeriod.Name = "cmdClosePeriod";
			this.cmdClosePeriod.Size = new System.Drawing.Size(116, 23);
			this.cmdClosePeriod.TabIndex = 1;
			this.cmdClosePeriod.Text = "Закрытие периода";
			this.cmdClosePeriod.Click += new EventHandler(this.cmdClosePeriod_Click);
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(364, 40);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(116, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Выход";
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdError.Enabled = false;
			this.cmdError.FlatStyle = FlatStyle.Flat;
			this.cmdError.Location = new Point(4, 40);
			this.cmdError.Name = "cmdError";
			this.cmdError.Size = new System.Drawing.Size(116, 23);
			this.cmdError.TabIndex = 3;
			this.cmdError.Text = "Ошибки>>";
			this.cmdError.Click += new EventHandler(this.cmdError_Click);
			this.cmdError.EnabledChanged += new EventHandler(this.cmdError_EnabledChanged);
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			this.CommandHolder.SetC1ContextMenu(this.lv, this.contextMenu1);
			this.lv.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader2, this.columnHeader6, this.columnHeader1 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(4, 96);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(478, 169);
			this.lv.TabIndex = 4;
			this.lv.TabStop = false;
			this.lv.View = View.Details;
			this.lv.ItemCheck += new ItemCheckEventHandler(this.lv_ItemCheck);
			this.columnHeader2.Text = "SPID";
			this.columnHeader2.Width = 61;
			this.columnHeader6.Text = "Пользователь";
			this.columnHeader6.Width = 134;
			this.columnHeader1.Text = "Имя компьютера";
			this.columnHeader1.Width = 137;
			this.contextMenu1.CommandLinks.AddRange(new C1CommandLink[] { this.menuItem1 });
			this.contextMenu1.Name = "contextMenu1";
			this.menuItem1.Command = this.cmd_menuItem1;
			this.cmd_menuItem1.Name = "cmd_menuItem1";
			this.cmd_menuItem1.Text = "Закрыть все выбранные процессы пользователей";
			this.cmd_menuItem1.Click += new ClickEventHandler(this.menuItem1_Click);
			this.CommandHolder.Commands.Add(this.cmd_menuItem1);
			this.CommandHolder.Commands.Add(this.contextMenu1);
			this.CommandHolder.Owner = this;
			this.cmdRestore.FlatStyle = FlatStyle.Flat;
			this.cmdRestore.Location = new Point(244, 40);
			this.cmdRestore.Name = "cmdRestore";
			this.cmdRestore.Size = new System.Drawing.Size(116, 23);
			this.cmdRestore.TabIndex = 5;
			this.cmdRestore.Text = "Restore";
			this.cmdRestore.Click += new EventHandler(this.cmdRestore_Click);
			this.bUnlockSingleUser.Enabled = false;
			this.bUnlockSingleUser.FlatStyle = FlatStyle.Flat;
			this.bUnlockSingleUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.bUnlockSingleUser.Location = new Point(4, 68);
			this.bUnlockSingleUser.Name = "bUnlockSingleUser";
			this.bUnlockSingleUser.Size = new System.Drawing.Size(476, 23);
			this.bUnlockSingleUser.TabIndex = 6;
			this.bUnlockSingleUser.Text = "Вернуть БД из монопольного режима";
			this.bUnlockSingleUser.Click += new EventHandler(this.bUnlockSingleUser_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(486, 268);
			base.Controls.Add(this.bUnlockSingleUser);
			base.Controls.Add(this.cmdRestore);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.cmdError);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdClosePeriod);
			base.Controls.Add(this.lblPeriod);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "frmActionForPeriod";
			this.Text = "Закрытие периода (+ Back UP), Restore";
			base.Closing += new CancelEventHandler(this.frmActionForPeriod_Closing);
			base.Load += new EventHandler(this.frmActionForPeriod_Load);
			((ISupportInitialize)this.CommandHolder).EndInit();
			base.ResumeLayout(false);
		}

		private void lv_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			try
			{
				if (this.lv.Items[e.Index].SubItems[1].Text.ToLower().Trim() == SQLConnect.CurrentUser.get_Name().ToLower().Trim())
				{
					e.NewValue = CheckState.Unchecked;
				}
			}
			catch
			{
			}
		}

		private void menuItem1_Click(object sender, ClickEventArgs e)
		{
			try
			{
				string str = "";
				foreach (ListViewItem checkedItem in this.lv.CheckedItems)
				{
					str = string.Concat(str, " KILL ", checkedItem.Tag.ToString());
				}
				if (!Saver.ExecuteQuery(str, 0))
				{
					MessageBox.Show("Не удалось закрыть все выбранные процессы!");
				}
				else
				{
					this.cmdError.Enabled = false;
					MessageBox.Show("Выбранные процессы закрыты!");
				}
			}
			catch
			{
			}
		}
	}
}