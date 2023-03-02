using FprnM1C;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmMain : Form
	{
		private IContainer components;

		private MainMenu mnuMain;

		private MenuItem menuItem4;

		private MenuItem mnuBook;

		private MenuItem menuItem1;

		private MenuItem menuItem15;

		private MenuItem menuItem19;

		private MenuItem mnuCascade;

		private MenuItem mnuTopBottom;

		private MenuItem mnuLeftRight;

		private MenuItem mnuAgent;

		private MenuItem mnuTypeBatch;

		private MenuItem menuItem18;

		private MenuItem mnuCloseAllWindow;

		private MenuItem menuItem20;

		private ToolBar tbMain;

		private bool isLogin;

		private MenuItem menuItem53;

		private ImageList imageList1;

		private StatusBar sbMain;

		public StatusBarPanel sbpStatus;

		public StatusBarPanel sbpStatus2;

		private StatusBarPanel sbpUser;

		private StatusBarPanel sbpServer;

		private MenuItem menuItem22;

		private MenuItem mnuTypeGmeter;

		private MenuItem mnuTypeGobject;

		private MenuItem mnuTypeDocument;

		private MenuItem mnuTypeOperation;

		private MenuItem mnuTypePay;

		private MenuItem menuItem7;

		private MenuItem menuItem8;

		private MenuItem menuItem9;

		private MenuItem menuItem10;

		private MenuItem menuItem11;

		private MenuItem menuItem12;

		private MenuItem menuItem13;

		private MenuItem menuItem14;

		private MenuItem menuItem16;

		private MenuItem menuItem17;

		private MenuItem menuItem21;

		private ToolBarButton toolBarButton1;

		private ToolBarButton toolBarButton2;

		private ToolBarButton toolBarButton3;

		private ToolBarButton toolBarButton4;

		private ToolBarButton toolBarButton5;

		private ToolBarButton toolBarButton7;

		private MenuItem menuItem23;

		private MenuItem menuLegalDocs;

		private MenuItem menufrmPrintCountNotice;

		private MenuItem menurepGarbTask;

		private MenuItem menuClaimDocs;

		private MenuItem menuFindPayment;

		private MenuItem menuBatchs;

		private MenuItem menuCarryPayment;

		private MenuItem menuChangeCharge;

		private MenuItem menuReceptionIndication;

		private MenuItem menuItem2;

		private MenuItem menuItem3;

		private MenuItem menuItem5;

		private MenuItem menuItem6;

		private MenuItem menuItem24;

		private ToolBarButton toolBarButton8;

		private ToolBarButton toolBarButton9;

		private ToolBarButton toolBarButton10;

		private ToolBarButton toolBarButton11;

		private ToolBarButton toolBarButton12;

		private ToolBarButton toolBarButton13;

		private ToolBarButton toolBarButton14;

		private ToolBarButton toolBarButton15;

		private ToolBarButton toolBarButton16;

		private ToolBarButton toolBarButton17;

		private ToolBarButton toolBarButton18;

		private ToolBarButton toolBarButton6;

		private MenuItem menuItem25;

		private MenuItem menuRecalcCash;

		private MenuItem menuItem26;

		private MenuItem menuItem27;

		private MenuItem menuItem29;

		private MenuItem menuItem30;

		private MenuItem menuItem31;

		private MenuItem menuItem32;

		private MenuItem menuItem33;

		private MenuItem menuGRU;

		private MenuItem menuItem34;

		private MenuItem menuItem35;

		private MenuItem menuItem36;

		private MenuItem menuItem37;

		private MenuItem menuItem38;

		private MenuItem menuItem39;

		private MenuItem menuItem40;

		private MenuItem menuItem41;

		private MenuItem menuItem42;

		private MenuItem menuItem43;

		private MenuItem menuItem44;

		private MenuItem menuItem45;

		private MenuItem menuItem46;

		private MenuItem menuItem47;

		private MenuItem menuItem48;

		private MenuItem menuItem49;

		private MenuItem menuItem50;

		private MenuItem menuItem51;

		private MenuItem menuItem52;

		private MenuItem menuItem54;

		private MenuItem menuItem55;

		private MenuItem menuItem56;

		private MenuItem menuItem57;

		private MenuItem menuItem58;

		private MenuItem menuItem59;

		private MenuItem menuItem60;

		private MenuItem menuItem61;

		private MenuItem menuItem62;

		private MenuItem menuItem63;

		private MenuItem menuItem64;

		private MenuItem menuItem65;

		private MenuItem menuItem66;

		private MenuItem menuItem67;

		private MenuItem menuItem68;

		private MenuItem menuItem69;

		private MenuItem menuItem70;

		private MenuItem menuItem71;

		private MenuItem menuItem28;

		private MenuItem menuItem72;

		private MenuItem menuItem73;

		private MenuItem menuItem74;

		private MenuItem menuItem75;

		private MenuItem menuItem76;

		private MenuItem menuItem77;

		private MenuItem menuItem78;

		private MenuItem menuItem79;

		private MenuItem menuItem80;

		private MenuItem menuItem81;

		private MenuItem menuItem82;

		private MenuItem menuItem83;

		private MenuItem menuItem84;

		private MenuItem menuItem85;

		private MenuItem menuItem86;

		private MenuItem menuItem87;

		private MenuItem menuItem88;

		private Form frmNeedToActivate = null;

		private MenuItem menuItem89;

		private MenuItem menuItem90;

		private MenuItem menuItem91;

		private MenuItem menuItem92;

		private MenuItem menuItem93;

		private MenuItem menuItem94;

		private MenuItem menuItem95;

		private MenuItem menuItem96;

		private MenuItem menuItem97;

		private MenuItem menuItem98;

		private MenuItem menuItem99;

		private MenuItem menuItem100;

		private MenuItem menuItem101;

		private MenuItem menuItem102;

		private MenuItem menuItem103;

		private MenuItem menuItem104;

		private MenuItem menuItem105;

		private MenuItem menuItem106;

		private MenuItem menuItem107;

		private MenuItem menuReceptionIndicationTemp;

		private MenuItem menuItem108;

		private MenuItem menuItem109;

		private MenuItem menuItem110;

		private MenuItem menuItem111;

		private MenuItem menuItem112;

		private MenuItem menuItem113;

		private MenuItem menuItem114;

		private MenuItem menuItem115;

		private MenuItem menuItem116;

		private IFprnM45 ECR;

		public frmMain()
		{
			EventsLog.set_NameApplication("Gefest");
			frmLogin _frmLogin = new frmLogin();
			_frmLogin.ShowDialog(this);
			this.isLogin = _frmLogin.isLogin;
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmMain_Closing(object sender, CancelEventArgs e)
		{
			EventsLog.SaveLog(string.Concat("Завершение приложения\u00a0:", EventsLog.get_NameApplication()), 1);
			Tools.LogOff();
			Tools.SaveWindows(this);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			if (this.isLogin)
			{
				base.WindowState = FormWindowState.Maximized;
				Tools.ShowMenu(this.mnuMain);
				Tools.ShowToolBar(this.tbMain, this.mnuMain);
				this.sbpUser.Text = Tools.CurrentUser();
				this.sbpServer.Text = Tools.CurrentServer();
				Depot._main = this;
				if (Depot.oSettings.Startup)
				{
					frmStart _frmStart = new frmStart()
					{
						MdiParent = this
					};
					_frmStart.Show();
					_frmStart = null;
				}
				if ((Tools.CurrentUser() == "egluhih" ? true : Tools.CurrentUser() == "pmalkov") & DateTime.Today.Day == 25)
				{
					(new frmPrintDebitors()).PublicRep();
				}
			}
			else
			{
				Tools.LogOff();
				base.Close();
			}
			try
			{
				if (Depot.oSettings.oAgent != null)
				{
					this.ECR = new FprnM45Class();
				}
			}
			catch
			{
			}
		}

		private string GetHashString(string s)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(s);
			byte[] numArray = (new MD5CryptoServiceProvider()).ComputeHash(bytes);
			string empty = string.Empty;
			byte[] numArray1 = numArray;
			for (int i = 0; i < (int)numArray1.Length; i++)
			{
				byte num = numArray1[i];
				empty = string.Concat(empty, string.Format("{0:x2}", num));
			}
			return empty;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmMain));
			this.mnuMain = new MainMenu();
			this.menuItem20 = new MenuItem();
			this.menuItem7 = new MenuItem();
			this.menuItem8 = new MenuItem();
			this.menuItem9 = new MenuItem();
			this.menuItem10 = new MenuItem();
			this.menuItem11 = new MenuItem();
			this.menuBatchs = new MenuItem();
			this.menuItem21 = new MenuItem();
			this.menuFindPayment = new MenuItem();
			this.menuItem87 = new MenuItem();
			this.menuItem23 = new MenuItem();
			this.menuLegalDocs = new MenuItem();
			this.menuClaimDocs = new MenuItem();
			this.menuCarryPayment = new MenuItem();
			this.menuChangeCharge = new MenuItem();
			this.menuReceptionIndication = new MenuItem();
			this.menuItem32 = new MenuItem();
			this.menuItem34 = new MenuItem();
			this.menuItem42 = new MenuItem();
			this.menuItem51 = new MenuItem();
			this.menuItem79 = new MenuItem();
			this.menuItem101 = new MenuItem();
			this.menuItem100 = new MenuItem();
			this.menuItem106 = new MenuItem();
			this.menuItem108 = new MenuItem();
			this.menuReceptionIndicationTemp = new MenuItem();
			this.menuItem109 = new MenuItem();
			this.menuItem110 = new MenuItem();
			this.menuItem111 = new MenuItem();
			this.menuItem114 = new MenuItem();
			this.mnuBook = new MenuItem();
			this.menuItem4 = new MenuItem();
			this.menuItem15 = new MenuItem();
			this.mnuTypeBatch = new MenuItem();
			this.menuItem1 = new MenuItem();
			this.mnuAgent = new MenuItem();
			this.mnuTypeGmeter = new MenuItem();
			this.mnuTypeGobject = new MenuItem();
			this.mnuTypeDocument = new MenuItem();
			this.mnuTypeOperation = new MenuItem();
			this.mnuTypePay = new MenuItem();
			this.menuItem78 = new MenuItem();
			this.menuGRU = new MenuItem();
			this.menuItem69 = new MenuItem();
			this.menuItem71 = new MenuItem();
			this.menuItem22 = new MenuItem();
			this.menuItem37 = new MenuItem();
			this.menuItem36 = new MenuItem();
			this.menuItem91 = new MenuItem();
			this.menuItem92 = new MenuItem();
			this.menuItem54 = new MenuItem();
			this.menuItem97 = new MenuItem();
			this.menuItem93 = new MenuItem();
			this.menuItem35 = new MenuItem();
			this.menuItem74 = new MenuItem();
			this.menuItem70 = new MenuItem();
			this.menuItem65 = new MenuItem();
			this.menuItem27 = new MenuItem();
			this.menuItem3 = new MenuItem();
			this.menuItem84 = new MenuItem();
			this.menuItem24 = new MenuItem();
			this.menuItem60 = new MenuItem();
			this.menuItem90 = new MenuItem();
			this.menuItem95 = new MenuItem();
			this.menuItem6 = new MenuItem();
			this.menuItem61 = new MenuItem();
			this.menuItem83 = new MenuItem();
			this.menuItem94 = new MenuItem();
			this.menuItem62 = new MenuItem();
			this.menuItem26 = new MenuItem();
			this.menuItem2 = new MenuItem();
			this.menufrmPrintCountNotice = new MenuItem();
			this.menurepGarbTask = new MenuItem();
			this.menuItem45 = new MenuItem();
			this.menuItem5 = new MenuItem();
			this.menuItem29 = new MenuItem();
			this.menuItem43 = new MenuItem();
			this.menuItem44 = new MenuItem();
			this.menuItem46 = new MenuItem();
			this.menuItem102 = new MenuItem();
			this.menuItem103 = new MenuItem();
			this.menuItem28 = new MenuItem();
			this.menuItem39 = new MenuItem();
			this.menuItem96 = new MenuItem();
			this.menuItem47 = new MenuItem();
			this.menuItem112 = new MenuItem();
			this.menuItem113 = new MenuItem();
			this.menuItem38 = new MenuItem();
			this.menuItem68 = new MenuItem();
			this.menuItem72 = new MenuItem();
			this.menuItem40 = new MenuItem();
			this.menuItem66 = new MenuItem();
			this.menuItem58 = new MenuItem();
			this.menuItem41 = new MenuItem();
			this.menuItem52 = new MenuItem();
			this.menuItem59 = new MenuItem();
			this.menuItem56 = new MenuItem();
			this.menuItem73 = new MenuItem();
			this.menuItem75 = new MenuItem();
			this.menuItem76 = new MenuItem();
			this.menuItem104 = new MenuItem();
			this.menuItem77 = new MenuItem();
			this.menuItem80 = new MenuItem();
			this.menuItem81 = new MenuItem();
			this.menuItem82 = new MenuItem();
			this.menuItem85 = new MenuItem();
			this.menuItem88 = new MenuItem();
			this.menuItem107 = new MenuItem();
			this.menuItem98 = new MenuItem();
			this.menuItem99 = new MenuItem();
			this.menuItem12 = new MenuItem();
			this.menuRecalcCash = new MenuItem();
			this.menuItem30 = new MenuItem();
			this.menuItem31 = new MenuItem();
			this.menuItem25 = new MenuItem();
			this.menuItem16 = new MenuItem();
			this.menuItem13 = new MenuItem();
			this.menuItem33 = new MenuItem();
			this.menuItem49 = new MenuItem();
			this.menuItem50 = new MenuItem();
			this.menuItem48 = new MenuItem();
			this.menuItem17 = new MenuItem();
			this.menuItem55 = new MenuItem();
			this.menuItem105 = new MenuItem();
			this.menuItem57 = new MenuItem();
			this.menuItem14 = new MenuItem();
			this.menuItem86 = new MenuItem();
			this.menuItem63 = new MenuItem();
			this.menuItem64 = new MenuItem();
			this.menuItem89 = new MenuItem();
			this.menuItem19 = new MenuItem();
			this.mnuCascade = new MenuItem();
			this.mnuTopBottom = new MenuItem();
			this.mnuLeftRight = new MenuItem();
			this.menuItem18 = new MenuItem();
			this.mnuCloseAllWindow = new MenuItem();
			this.tbMain = new ToolBar();
			this.toolBarButton1 = new ToolBarButton();
			this.toolBarButton8 = new ToolBarButton();
			this.toolBarButton4 = new ToolBarButton();
			this.toolBarButton6 = new ToolBarButton();
			this.toolBarButton9 = new ToolBarButton();
			this.toolBarButton10 = new ToolBarButton();
			this.toolBarButton12 = new ToolBarButton();
			this.toolBarButton11 = new ToolBarButton();
			this.toolBarButton18 = new ToolBarButton();
			this.toolBarButton13 = new ToolBarButton();
			this.toolBarButton14 = new ToolBarButton();
			this.toolBarButton15 = new ToolBarButton();
			this.toolBarButton16 = new ToolBarButton();
			this.toolBarButton17 = new ToolBarButton();
			this.toolBarButton2 = new ToolBarButton();
			this.toolBarButton5 = new ToolBarButton();
			this.toolBarButton7 = new ToolBarButton();
			this.toolBarButton3 = new ToolBarButton();
			this.imageList1 = new ImageList(this.components);
			this.menuItem53 = new MenuItem();
			this.sbMain = new StatusBar();
			this.sbpStatus = new StatusBarPanel();
			this.sbpStatus2 = new StatusBarPanel();
			this.sbpUser = new StatusBarPanel();
			this.sbpServer = new StatusBarPanel();
			this.menuItem115 = new MenuItem();
			this.menuItem116 = new MenuItem();
			((ISupportInitialize)this.sbpStatus).BeginInit();
			((ISupportInitialize)this.sbpStatus2).BeginInit();
			((ISupportInitialize)this.sbpUser).BeginInit();
			((ISupportInitialize)this.sbpServer).BeginInit();
			base.SuspendLayout();
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.mnuMain.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuItem20, this.menuItem8, this.menuItem23, this.mnuBook, this.menuItem22, this.menuItem12, this.menuItem63, this.menuItem19 };
			menuItems.AddRange(menuItemArray);
			this.menuItem20.Index = 0;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections = this.menuItem20.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem7, this.menuItem115, this.menuItem116 };
			menuItemCollections.AddRange(menuItemArray);
			this.menuItem20.Text = "Договора и Потребители";
			this.menuItem7.Index = 0;
			this.menuItem7.Text = "Поиск договоров и потребителей";
			this.menuItem7.Click += new EventHandler(this.menuItem7_Click);
			this.menuItem8.Index = 1;
			System.Windows.Forms.Menu.MenuItemCollection menuItems1 = this.menuItem8.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem9, this.menuItem10, this.menuItem11, this.menuBatchs, this.menuItem21, this.menuFindPayment, this.menuItem87 };
			menuItems1.AddRange(menuItemArray);
			this.menuItem8.Text = "Платежи";
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "Начать прием оплат";
			this.menuItem9.Click += new EventHandler(this.menuItem9_Click);
			this.menuItem10.Index = 1;
			this.menuItem10.Text = "Завершить прием оплат";
			this.menuItem10.Click += new EventHandler(this.menuItem10_Click);
			this.menuItem11.Index = 2;
			this.menuItem11.Text = "Кассовый отчет";
			this.menuItem11.Click += new EventHandler(this.menuItem11_Click);
			this.menuBatchs.Index = 3;
			this.menuBatchs.Text = "Журнал пачек";
			this.menuBatchs.Click += new EventHandler(this.menuBatchs_Click);
			this.menuItem21.Index = 4;
			this.menuItem21.Text = "Прием транспортного файла";
			this.menuItem21.Click += new EventHandler(this.menuItem21_Click);
			this.menuFindPayment.Index = 5;
			this.menuFindPayment.Text = "Поиск платежей";
			this.menuFindPayment.Click += new EventHandler(this.menuFindPayment_Click);
			this.menuItem87.Index = 6;
			this.menuItem87.Text = "Отчеты по смене KKM";
			this.menuItem87.Click += new EventHandler(this.menuItem87_Click);
			this.menuItem23.Index = 2;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections1 = this.menuItem23.MenuItems;
			menuItemArray = new MenuItem[] { this.menuLegalDocs, this.menuClaimDocs, this.menuCarryPayment, this.menuChangeCharge, this.menuReceptionIndication, this.menuItem32, this.menuItem34, this.menuItem42, this.menuItem51, this.menuItem79, this.menuItem101, this.menuItem100, this.menuItem106, this.menuItem108, this.menuReceptionIndicationTemp, this.menuItem109, this.menuItem110, this.menuItem111, this.menuItem114 };
			menuItemCollections1.AddRange(menuItemArray);
			this.menuItem23.Text = "Документы";
			this.menuLegalDocs.Index = 0;
			this.menuLegalDocs.Text = "Юридические документы";
			this.menuLegalDocs.Click += new EventHandler(this.menuLegalDocs_Click);
			this.menuClaimDocs.Index = 1;
			this.menuClaimDocs.Text = "Претензии";
			this.menuClaimDocs.Click += new EventHandler(this.menuClaimDocs_Click);
			this.menuCarryPayment.Index = 2;
			this.menuCarryPayment.Text = "Перенос оплаты";
			this.menuCarryPayment.Click += new EventHandler(this.menuCarryPayment_Click);
			this.menuChangeCharge.Index = 3;
			this.menuChangeCharge.Text = "Корректировка начисления";
			this.menuChangeCharge.Click += new EventHandler(this.menuChangeCharge_Click);
			this.menuReceptionIndication.Index = 4;
			this.menuReceptionIndication.Text = "Разноска показаний";
			this.menuReceptionIndication.Click += new EventHandler(this.menuReceptionIndication_Click);
			this.menuItem32.Index = 5;
			this.menuItem32.Text = "Корректировка оплаты (старые)";
			this.menuItem32.Click += new EventHandler(this.menuItem32_Click);
			this.menuItem34.Index = 6;
			this.menuItem34.Text = "Акт проверки работы ПУ";
			this.menuItem34.Click += new EventHandler(this.menuItem34_Click);
			this.menuItem42.Index = 7;
			this.menuItem42.Text = "Журнал уведомлений";
			this.menuItem42.Click += new EventHandler(this.menuItem42_Click);
			this.menuItem51.Index = 8;
			this.menuItem51.Text = "Журнал поверки ПУ";
			this.menuItem51.Click += new EventHandler(this.menuItem51_Click);
			this.menuItem79.Index = 9;
			this.menuItem79.Text = "Журнал услуг";
			this.menuItem79.Click += new EventHandler(this.menuItem79_Click);
			this.menuItem101.Index = 10;
			this.menuItem101.Text = "-";
			this.menuItem100.Index = 11;
			this.menuItem100.Text = "Прием реестра по опломбировке";
			this.menuItem100.Click += new EventHandler(this.menuItem100_Click);
			this.menuItem106.Index = 12;
			this.menuItem106.Text = "Прием реестра по распломбировке";
			this.menuItem106.Click += new EventHandler(this.menuItem106_Click);
			this.menuItem108.Index = 13;
			this.menuItem108.Text = "-";
			this.menuReceptionIndicationTemp.Index = 14;
			this.menuReceptionIndicationTemp.Text = "Разноска показаний до закрытия периода";
			this.menuReceptionIndicationTemp.Click += new EventHandler(this.menuReceptionIndicationTemp_Click);
			this.menuItem109.Index = 15;
			this.menuItem109.Text = "Журнал показаний до закрытия периода";
			this.menuItem109.Click += new EventHandler(this.menuItem109_Click);
			this.menuItem110.Index = 16;
			this.menuItem110.Text = "-";
			this.menuItem111.Index = 17;
			this.menuItem111.Text = "Заявки на доступ в ЛКА";
			this.menuItem111.Click += new EventHandler(this.menuItem111_Click);
			this.menuItem114.Index = 18;
			this.menuItem114.Text = "Кредитные услуги";
			this.menuItem114.Click += new EventHandler(this.menuItem114_Click);
			this.mnuBook.Index = 3;
			System.Windows.Forms.Menu.MenuItemCollection menuItems2 = this.mnuBook.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem4, this.menuItem15, this.mnuTypeBatch, this.menuItem1, this.mnuAgent, this.mnuTypeGmeter, this.mnuTypeGobject, this.mnuTypeDocument, this.mnuTypeOperation, this.mnuTypePay, this.menuItem78, this.menuGRU, this.menuItem69, this.menuItem71 };
			menuItems2.AddRange(menuItemArray);
			this.mnuBook.Text = "Справочники";
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "Адрес";
			this.menuItem4.Click += new EventHandler(this.menuItem4_Click);
			this.menuItem15.Index = 1;
			this.menuItem15.Text = "Тип договоров";
			this.menuItem15.Click += new EventHandler(this.menuItem15_Click);
			this.mnuTypeBatch.Index = 2;
			this.mnuTypeBatch.Text = "Тип пачки";
			this.mnuTypeBatch.Click += new EventHandler(this.mnuTypeBatch_Click);
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "-";
			this.mnuAgent.Index = 4;
			this.mnuAgent.Text = "Агенты";
			this.mnuAgent.Click += new EventHandler(this.mnuAgent_Click);
			this.mnuTypeGmeter.Index = 5;
			this.mnuTypeGmeter.Text = "Типы приборов учета";
			this.mnuTypeGmeter.Click += new EventHandler(this.mnuTypeGMeter_Click);
			this.mnuTypeGobject.Index = 6;
			this.mnuTypeGobject.Text = "Типы объектов учёта";
			this.mnuTypeGobject.Click += new EventHandler(this.mnuTypeGobject_Click);
			this.mnuTypeDocument.Index = 7;
			this.mnuTypeDocument.Text = "Типы документов";
			this.mnuTypeDocument.Click += new EventHandler(this.mnuTypeDocument_Click);
			this.mnuTypeOperation.Index = 8;
			this.mnuTypeOperation.Text = "Типы операций";
			this.mnuTypeOperation.Click += new EventHandler(this.mnuTypeOperation_Click);
			this.mnuTypePay.Index = 9;
			this.mnuTypePay.Text = "Типы оплаты";
			this.mnuTypePay.Click += new EventHandler(this.mnuTypePay_Click);
			this.menuItem78.Index = 10;
			this.menuItem78.Text = "Типы нарушения";
			this.menuItem78.Click += new EventHandler(this.menuItem78_Click);
			this.menuGRU.Index = 11;
			this.menuGRU.Text = "Резервуарные установки";
			this.menuGRU.Click += new EventHandler(this.menuGRU_Click);
			this.menuItem69.Index = 12;
			this.menuItem69.Text = "Причины отключения ПУ";
			this.menuItem69.Click += new EventHandler(this.menuItem69_Click);
			this.menuItem71.Index = 13;
			this.menuItem71.Text = "Cтавка рефинансирования ";
			this.menuItem71.Click += new EventHandler(this.menuItem71_Click);
			this.menuItem22.Index = 4;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections2 = this.menuItem22.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem37, this.menuItem36, this.menuItem91, this.menuItem92, this.menuItem54, this.menuItem97, this.menuItem93, this.menuItem35, this.menuItem74, this.menuItem70, this.menuItem65, this.menuItem27, this.menuItem3, this.menuItem84, this.menuItem24, this.menuItem60, this.menuItem90, this.menuItem95, this.menuItem6, this.menuItem61, this.menuItem83, this.menuItem94, this.menuItem62, this.menuItem26, this.menuItem2, this.menufrmPrintCountNotice, this.menurepGarbTask, this.menuItem45, this.menuItem5, this.menuItem29, this.menuItem43, this.menuItem44, this.menuItem46, this.menuItem102, this.menuItem103, this.menuItem28, this.menuItem39, this.menuItem96, this.menuItem47, this.menuItem112, this.menuItem113, this.menuItem38, this.menuItem68, this.menuItem72, this.menuItem40, this.menuItem66, this.menuItem58, this.menuItem41, this.menuItem52, this.menuItem59, this.menuItem56, this.menuItem73, this.menuItem75, this.menuItem76, this.menuItem104, this.menuItem77, this.menuItem80, this.menuItem81, this.menuItem82, this.menuItem85, this.menuItem88, this.menuItem107, this.menuItem98, this.menuItem99 };
			menuItemCollections2.AddRange(menuItemArray);
			this.menuItem22.Text = "Отчёты";
			this.menuItem37.Index = 0;
			this.menuItem37.Text = "Оперативные данные";
			this.menuItem37.Click += new EventHandler(this.menuItem37_Click);
			this.menuItem36.Index = 1;
			this.menuItem36.Text = "Поступление денежных средств";
			this.menuItem36.Click += new EventHandler(this.menuItem36_Click_1);
			this.menuItem91.Index = 2;
			this.menuItem91.Text = "Поступление денежных средств по месяцам";
			this.menuItem91.Click += new EventHandler(this.menuItem91_Click);
			this.menuItem92.Index = 3;
			this.menuItem92.Text = "Просроченная задолженность";
			this.menuItem92.Click += new EventHandler(this.menuItem92_Click);
			this.menuItem54.Index = 4;
			this.menuItem54.Text = "Анализ дебиторской задолженности";
			this.menuItem54.Click += new EventHandler(this.menuItem54_Click);
			this.menuItem97.Index = 5;
			this.menuItem97.Text = "Анализ деб. задолженности по основному долгу";
			this.menuItem97.Click += new EventHandler(this.menuItem97_Click);
			this.menuItem93.Index = 6;
			this.menuItem93.Text = "Отчёт по абонентам-должникам";
			this.menuItem93.Click += new EventHandler(this.menuItem93_Click);
			this.menuItem35.Index = 7;
			this.menuItem35.Text = "Сальдо по квартирам";
			this.menuItem35.Click += new EventHandler(this.menuItem35_Click);
			this.menuItem74.Index = 8;
			this.menuItem74.Text = "Сальдо по квартирам для ЗП";
			this.menuItem74.Click += new EventHandler(this.menuItem74_Click);
			this.menuItem70.Index = 9;
			this.menuItem70.Text = "Сальдо с указанием места работы";
			this.menuItem70.Click += new EventHandler(this.menuItem70_Click);
			this.menuItem65.Index = 10;
			this.menuItem65.Text = "Cальдо по ГРУ";
			this.menuItem65.Click += new EventHandler(this.menuItem65_Click);
			this.menuItem27.Index = 11;
			this.menuItem27.Text = "-";
			this.menuItem3.Index = 12;
			this.menuItem3.Text = "Справка по абонентской службе";
			this.menuItem3.Click += new EventHandler(this.menuItem3_Click);
			this.menuItem84.Index = 13;
			this.menuItem84.Text = "Справка по абонентской службе для рук.";
			this.menuItem84.Click += new EventHandler(this.menuItem84_Click);
			this.menuItem24.Index = 14;
			this.menuItem24.Text = "Справка по начислению за газ";
			this.menuItem24.Click += new EventHandler(this.menuItem24_Click);
			this.menuItem60.Index = 15;
			this.menuItem60.Text = "Отчёт о реализации СУВГ абонентам";
			this.menuItem60.Click += new EventHandler(this.menuItem60_Click);
			this.menuItem90.Index = 16;
			this.menuItem90.Text = "Отчёт по реализации газа и услуг ю/л";
			this.menuItem90.Click += new EventHandler(this.menuItem90_Click);
			this.menuItem95.Index = 17;
			this.menuItem95.Text = "Отчёт по оплате за услуги ю/л";
			this.menuItem95.Click += new EventHandler(this.menuItem95_Click);
			this.menuItem6.Index = 18;
			this.menuItem6.Text = "Отчёт о потребление газа";
			this.menuItem6.Click += new EventHandler(this.menuItem6_Click);
			this.menuItem61.Index = 19;
			this.menuItem61.Text = "Отчёт для АМК";
			this.menuItem61.Click += new EventHandler(this.menuItem61_Click);
			this.menuItem83.Index = 20;
			this.menuItem83.Text = "Отчёт по прочим видам деятельности";
			this.menuItem83.Click += new EventHandler(this.menuItem83_Click);
			this.menuItem94.Index = 21;
			this.menuItem94.Text = "Справка по кредитовой задолженности по услугам";
			this.menuItem94.Click += new EventHandler(this.menuItem94_Click);
			this.menuItem62.Index = 22;
			this.menuItem62.Text = "Справка по вновь установленным ПУ";
			this.menuItem62.Click += new EventHandler(this.menuItem62_Click);
			this.menuItem26.Index = 23;
			this.menuItem26.Text = "-";
			this.menuItem2.Index = 24;
			this.menuItem2.Text = "Карточка абонента";
			this.menuItem2.Click += new EventHandler(this.menuItem2_Click);
			this.menufrmPrintCountNotice.Index = 25;
			this.menufrmPrintCountNotice.Text = "Счёт-извещение";
			this.menufrmPrintCountNotice.Click += new EventHandler(this.menufrmPrintCountNotice_Click);
			this.menurepGarbTask.Index = 26;
			this.menurepGarbTask.Text = "Наряд-задание";
			this.menurepGarbTask.Click += new EventHandler(this.menurepGarbTask_Click);
			this.menuItem45.Index = 27;
			this.menuItem45.Text = "Наряд на отключение";
			this.menuItem45.Click += new EventHandler(this.menuItem45_Click);
			this.menuItem5.Index = 28;
			this.menuItem5.Text = "Потребление по РУ";
			this.menuItem5.Click += new EventHandler(this.menuItem5_Click);
			this.menuItem29.Index = 29;
			this.menuItem29.Text = "Большое потребление";
			this.menuItem29.Click += new EventHandler(this.menuItem29_Click);
			this.menuItem43.Index = 30;
			this.menuItem43.Text = "Справка по РУ";
			this.menuItem43.Click += new EventHandler(this.menuItem43_Click);
			this.menuItem44.Index = 31;
			this.menuItem44.Text = "Cправка по отключенным квартирам";
			this.menuItem44.Click += new EventHandler(this.menuItem44_Click);
			this.menuItem46.Index = 32;
			this.menuItem46.Text = "Справка по ПУ с одинаковыми номерами ";
			this.menuItem46.Click += new EventHandler(this.menuItem46_Click);
			this.menuItem102.Index = 33;
			this.menuItem102.Text = "Справка по неопломбированным ПУ";
			this.menuItem102.Click += new EventHandler(this.menuItem102_Click);
			this.menuItem103.Index = 34;
			this.menuItem103.Text = "Справка по ПУ, отключенным до опломбировки";
			this.menuItem103.Click += new EventHandler(this.menuItem103_Click);
			this.menuItem28.Index = 35;
			this.menuItem28.Text = "-";
			this.menuItem39.Index = 36;
			this.menuItem39.Text = "Уведомление на проверку ПУ";
			this.menuItem39.Click += new EventHandler(this.menuItem39_Click);
			this.menuItem96.Index = 37;
			this.menuItem96.Text = "Уведомление на поверку ПУ (ГТМ)";
			this.menuItem96.Click += new EventHandler(this.menuItem96_Click);
			this.menuItem47.Index = 38;
			this.menuItem47.Text = "Отчёт по ПУ без поверки";
			this.menuItem47.Click += new EventHandler(this.menuItem47_Click);
			this.menuItem112.Index = 39;
			this.menuItem112.Text = "Отчёт по поверенным ПУ";
			this.menuItem112.Click += new EventHandler(this.menuItem112_Click);
			this.menuItem113.Index = 40;
			this.menuItem113.Text = "Реестр поверенных ПУ";
			this.menuItem113.Click += new EventHandler(this.menuItem113_Click);
			this.menuItem38.Index = 41;
			this.menuItem38.Text = "Справка по состоянию ПУ";
			this.menuItem38.Click += new EventHandler(this.menuItem38_Click);
			this.menuItem68.Index = 42;
			this.menuItem68.Text = "Отчёт по отключенным ПУ без поверки ";
			this.menuItem68.Click += new EventHandler(this.menuItem68_Click);
			this.menuItem72.Index = 43;
			this.menuItem72.Text = "-";
			this.menuItem40.Index = 44;
			this.menuItem40.Text = "Отчёт по работе операторов";
			this.menuItem40.Visible = false;
			this.menuItem40.Click += new EventHandler(this.menuItem40_Click);
			this.menuItem66.Index = 45;
			this.menuItem66.Text = "Список юридических лиц";
			this.menuItem66.Click += new EventHandler(this.menuItem66_Click);
			this.menuItem58.Index = 46;
			this.menuItem58.Text = "Список РУ";
			this.menuItem58.Click += new EventHandler(this.menuItem58_Click);
			this.menuItem41.Index = 47;
			this.menuItem41.Text = "Отчёт по внесению показаний операторов";
			this.menuItem41.Click += new EventHandler(this.menuItem41_Click);
			this.menuItem52.Index = 48;
			this.menuItem52.Text = "Отчёт по внесению показаний от контролеров";
			this.menuItem52.Click += new EventHandler(this.menuItem52_Click);
			this.menuItem59.Index = 49;
			this.menuItem59.Text = "Справка по повторам показаний";
			this.menuItem59.Visible = false;
			this.menuItem59.Click += new EventHandler(this.menuItem59_Click);
			this.menuItem56.Index = 50;
			this.menuItem56.Text = "Откл. подк. ПУ и ОУ Аварийной службой";
			this.menuItem56.Click += new EventHandler(this.menuItem56_Click);
			this.menuItem73.Index = 51;
			this.menuItem73.Text = "Отчёт по начислению пени";
			this.menuItem73.Click += new EventHandler(this.menuItem73_Click);
			this.menuItem75.Index = 52;
			this.menuItem75.Text = "Справка по абон. , отказ. от дост. сч-ов";
			this.menuItem75.Click += new EventHandler(this.menuItem75_Click);
			this.menuItem76.Index = 53;
			this.menuItem76.Text = "Список домов, подключенных к ЦГС";
			this.menuItem76.Click += new EventHandler(this.menuItem76_Click);
			this.menuItem104.Index = 54;
			this.menuItem104.Text = "Список абонентов";
			this.menuItem104.Click += new EventHandler(this.menuItem104_Click);
			this.menuItem77.Index = 55;
			this.menuItem77.Text = "Наряд по абонентам без актов";
			this.menuItem77.Click += new EventHandler(this.menuItem77_Click);
			this.menuItem80.Index = 56;
			this.menuItem80.Text = "Реестр по прочим видам деятельности";
			this.menuItem80.Click += new EventHandler(this.menuItem80_Click);
			this.menuItem81.Index = 57;
			this.menuItem81.Text = "Реестр опл. плат. услуг пост. в кассу";
			this.menuItem81.Click += new EventHandler(this.menuItem81_Click);
			this.menuItem82.Index = 58;
			this.menuItem82.Text = "Реестр опл. плат. услуг пост. из банка";
			this.menuItem82.Click += new EventHandler(this.menuItem82_Click);
			this.menuItem85.Index = 59;
			this.menuItem85.Text = "Карточка абонента Эксперимент ";
			this.menuItem85.Click += new EventHandler(this.menuItem85_Click);
			this.menuItem88.Index = 60;
			this.menuItem88.Text = "Реестр платежей пени и Гос. пошлины";
			this.menuItem88.Click += new EventHandler(this.menuItem88_Click);
			this.menuItem107.Index = 61;
			this.menuItem107.Text = "Реестр снятых пломб";
			this.menuItem107.Click += new EventHandler(this.menuItem107_Click);
			this.menuItem98.Index = 62;
			this.menuItem98.Text = "-";
			this.menuItem99.Index = 63;
			this.menuItem99.Text = "Кассовый отчет по терминалу";
			this.menuItem99.Click += new EventHandler(this.menuItem99_Click);
			this.menuItem12.Index = 5;
			System.Windows.Forms.Menu.MenuItemCollection menuItems3 = this.menuItem12.MenuItems;
			menuItemArray = new MenuItem[] { this.menuRecalcCash, this.menuItem30, this.menuItem31, this.menuItem25, this.menuItem16, this.menuItem13, this.menuItem33, this.menuItem49, this.menuItem50, this.menuItem48, this.menuItem17, this.menuItem55, this.menuItem105, this.menuItem57, this.menuItem14, this.menuItem86 };
			menuItems3.AddRange(menuItemArray);
			this.menuItem12.Text = "Сервис";
			this.menuRecalcCash.Index = 0;
			this.menuRecalcCash.Text = "Пересчет сальдо по кассе";
			this.menuRecalcCash.Click += new EventHandler(this.menuRecalcCash_Click);
			this.menuItem30.Index = 1;
			this.menuItem30.Text = "Пересчет остатков";
			this.menuItem30.Click += new EventHandler(this.menuItem30_Click);
			this.menuItem31.Index = 2;
			this.menuItem31.Text = "-";
			this.menuItem25.Index = 3;
			this.menuItem25.Text = "Интерфейс";
			this.menuItem25.Click += new EventHandler(this.menuItem25_Click);
			this.menuItem16.Index = 4;
			this.menuItem16.Text = "Настройки";
			this.menuItem16.Click += new EventHandler(this.menuItem16_Click);
			this.menuItem13.Index = 5;
			this.menuItem13.Text = "Стартовый навигатор";
			this.menuItem13.Visible = false;
			this.menuItem13.Click += new EventHandler(this.menuItem13_Click);
			this.menuItem33.Index = 6;
			this.menuItem33.Text = "Формирование транспортного файла для Сбыта";
			this.menuItem33.Click += new EventHandler(this.menuItem33_Click);
			this.menuItem49.Index = 7;
			this.menuItem49.Text = "-";
			this.menuItem50.Index = 8;
			this.menuItem50.Text = "Формирование транспортного файла для Поверителя";
			this.menuItem50.Click += new EventHandler(this.menuItem50_Click);
			this.menuItem48.Index = 9;
			this.menuItem48.Text = "Прием транспортного файла от Поверителя";
			this.menuItem48.Click += new EventHandler(this.menuItem48_Click);
			this.menuItem17.Index = 10;
			this.menuItem17.Text = "-";
			this.menuItem55.Index = 11;
			this.menuItem55.Text = "Закрытие периода, Restore Database";
			this.menuItem55.Click += new EventHandler(this.menuItem55_Click);
			this.menuItem105.Index = 12;
			this.menuItem105.Text = "Загрузка телефонов";
			this.menuItem105.Click += new EventHandler(this.menuItem105_Click);
			this.menuItem57.Index = 13;
			this.menuItem57.Text = "-";
			this.menuItem14.Index = 14;
			this.menuItem14.Text = "Выход";
			this.menuItem14.Click += new EventHandler(this.menuItem14_Click);
			this.menuItem86.Index = 15;
			this.menuItem86.Text = "Настройка ККМ";
			this.menuItem86.Click += new EventHandler(this.menuItem86_Click);
			this.menuItem63.Index = 6;
			System.Windows.Forms.Menu.MenuItemCollection menuItemCollections3 = this.menuItem63.MenuItems;
			menuItemArray = new MenuItem[] { this.menuItem64, this.menuItem89 };
			menuItemCollections3.AddRange(menuItemArray);
			this.menuItem63.Text = "Справка";
			this.menuItem64.Index = 0;
			this.menuItem64.Shortcut = Shortcut.F1;
			this.menuItem64.Text = "Вызов справки";
			this.menuItem64.Click += new EventHandler(this.menuItem64_Click);
			this.menuItem89.Index = 1;
			this.menuItem89.Text = "Версия";
			this.menuItem89.Click += new EventHandler(this.menuItem89_Click);
			this.menuItem19.Index = 7;
			this.menuItem19.MdiList = true;
			System.Windows.Forms.Menu.MenuItemCollection menuItems4 = this.menuItem19.MenuItems;
			menuItemArray = new MenuItem[] { this.mnuCascade, this.mnuTopBottom, this.mnuLeftRight, this.menuItem18, this.mnuCloseAllWindow };
			menuItems4.AddRange(menuItemArray);
			this.menuItem19.Text = "Окно";
			this.mnuCascade.Index = 0;
			this.mnuCascade.Text = "Каскадом";
			this.mnuCascade.Click += new EventHandler(this.mnuCascade_Click);
			this.mnuTopBottom.Index = 1;
			this.mnuTopBottom.Text = "По вертикали";
			this.mnuTopBottom.Click += new EventHandler(this.mnuTopBottom_Click);
			this.mnuLeftRight.Index = 2;
			this.mnuLeftRight.Text = "По горизонтали";
			this.mnuLeftRight.Click += new EventHandler(this.mnuLeftRight_Click);
			this.menuItem18.Index = 3;
			this.menuItem18.Text = "-";
			this.mnuCloseAllWindow.Index = 4;
			this.mnuCloseAllWindow.Text = "Закрыть все окна";
			this.mnuCloseAllWindow.Click += new EventHandler(this.mnuCloseAllWindow_Click);
			this.tbMain.Appearance = ToolBarAppearance.Flat;
			ToolBar.ToolBarButtonCollection buttons = this.tbMain.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.toolBarButton1, this.toolBarButton8, this.toolBarButton4, this.toolBarButton6, this.toolBarButton9, this.toolBarButton10, this.toolBarButton12, this.toolBarButton11, this.toolBarButton18, this.toolBarButton13, this.toolBarButton14, this.toolBarButton15, this.toolBarButton16, this.toolBarButton17, this.toolBarButton2, this.toolBarButton5, this.toolBarButton7, this.toolBarButton3 };
			buttons.AddRange(toolBarButtonArray);
			this.tbMain.Divider = false;
			this.tbMain.DropDownArrows = true;
			this.tbMain.ImageList = this.imageList1;
			this.tbMain.Location = new Point(0, 0);
			this.tbMain.Name = "tbMain";
			this.tbMain.ShowToolTips = true;
			this.tbMain.Size = new System.Drawing.Size(984, 26);
			this.tbMain.TabIndex = 1;
			this.tbMain.TabStop = true;
			this.tbMain.ButtonClick += new ToolBarButtonClickEventHandler(this.tbMain_ButtonClick);
			this.toolBarButton1.ImageIndex = 0;
			this.toolBarButton1.Tag = "menuItem7";
			this.toolBarButton1.ToolTipText = "Поиск договоров и потребителей";
			this.toolBarButton8.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton4.ImageIndex = 17;
			this.toolBarButton4.ToolTipText = "Журнал пачек";
			this.toolBarButton6.ImageIndex = 16;
			this.toolBarButton6.ToolTipText = "Поиск платежей";
			this.toolBarButton9.ImageIndex = 5;
			this.toolBarButton9.ToolTipText = "Начать прием оплат";
			this.toolBarButton10.ImageIndex = 6;
			this.toolBarButton10.ToolTipText = "Завершить прием оплат";
			this.toolBarButton12.ImageIndex = 8;
			this.toolBarButton12.ToolTipText = "Кассовый отчет";
			this.toolBarButton11.ImageIndex = 7;
			this.toolBarButton11.ToolTipText = "Прием транспотного файла";
			this.toolBarButton18.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton13.ImageIndex = 9;
			this.toolBarButton13.ToolTipText = "Юридические документы";
			this.toolBarButton14.ImageIndex = 10;
			this.toolBarButton14.ToolTipText = "Претензии";
			this.toolBarButton15.ImageIndex = 12;
			this.toolBarButton15.ToolTipText = "Перенос оплаты";
			this.toolBarButton16.ImageIndex = 11;
			this.toolBarButton16.ToolTipText = "Корректировка начисления";
			this.toolBarButton17.ImageIndex = 13;
			this.toolBarButton17.ToolTipText = "Разноска показаний";
			this.toolBarButton2.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton5.ImageIndex = 15;
			this.toolBarButton5.ToolTipText = "Стартовый навигатор";
			this.toolBarButton7.ImageIndex = 14;
			this.toolBarButton7.ToolTipText = "Настройки";
			this.toolBarButton3.ImageIndex = 4;
			this.toolBarButton3.ToolTipText = "Выход";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.menuItem53.Index = -1;
			this.menuItem53.Text = "";
			this.sbMain.Location = new Point(0, 459);
			this.sbMain.Name = "sbMain";
			StatusBar.StatusBarPanelCollection panels = this.sbMain.Panels;
			StatusBarPanel[] statusBarPanelArray = new StatusBarPanel[] { this.sbpStatus, this.sbpStatus2, this.sbpUser, this.sbpServer };
			panels.AddRange(statusBarPanelArray);
			this.sbMain.ShowPanels = true;
			this.sbMain.Size = new System.Drawing.Size(984, 22);
			this.sbMain.TabIndex = 3;
			this.sbpStatus.AutoSize = StatusBarPanelAutoSize.Contents;
			this.sbpStatus.Width = 10;
			this.sbpStatus2.AutoSize = StatusBarPanelAutoSize.Spring;
			this.sbpStatus2.Width = 938;
			this.sbpUser.AutoSize = StatusBarPanelAutoSize.Contents;
			this.sbpUser.Width = 10;
			this.sbpServer.AutoSize = StatusBarPanelAutoSize.Contents;
			this.sbpServer.Width = 10;
			this.menuItem115.Index = 1;
			this.menuItem115.Text = "-";
			this.menuItem116.Index = 2;
			this.menuItem116.Text = "Ввод абонентов по дому";
			this.menuItem116.Click += new EventHandler(this.menuItem116_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = SystemColors.Menu;
			base.ClientSize = new System.Drawing.Size(984, 481);
			base.Controls.Add(this.sbMain);
			base.Controls.Add(this.tbMain);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.IsMdiContainer = true;
			base.Menu = this.mnuMain;
			base.Name = "frmMain";
			this.Text = "Gefest";
			base.WindowState = FormWindowState.Maximized;
			base.Closing += new CancelEventHandler(this.frmMain_Closing);
			base.Load += new EventHandler(this.frmMain_Load);
			((ISupportInitialize)this.sbpStatus).EndInit();
			((ISupportInitialize)this.sbpStatus2).EndInit();
			((ISupportInitialize)this.sbpUser).EndInit();
			((ISupportInitialize)this.sbpServer).EndInit();
			base.ResumeLayout(false);
		}

		[STAThread]
		private static void Main()
		{
			Application.Run(new frmMain());
		}

		private void menuBatchs_Click(object sender, EventArgs e)
		{
			Form frmBatch = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmBatchs")
				{
					num++;
				}
				else
				{
					frmBatch = form;
					frmBatch.Activate();
					break;
				}
			}
			if (frmBatch == null)
			{
				frmBatch = new frmBatchs()
				{
					MdiParent = this
				};
			}
			frmBatch.Show();
			frmBatch = null;
		}

		private void menuCarryPayment_Click(object sender, EventArgs e)
		{
			Form frmCarryPayment = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmCarryPayments")
				{
					num++;
				}
				else
				{
					frmCarryPayment = form;
					frmCarryPayment.Activate();
					break;
				}
			}
			if (frmCarryPayment == null)
			{
				frmCarryPayment = new frmCarryPayments()
				{
					MdiParent = this
				};
			}
			frmCarryPayment.Show();
			frmCarryPayment = null;
		}

		private void menuChangeCharge_Click(object sender, EventArgs e)
		{
			Form frmChangeCharge = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmChangeCharges")
				{
					num++;
				}
				else
				{
					frmChangeCharge = form;
					frmChangeCharge.Activate();
					break;
				}
			}
			if (frmChangeCharge == null)
			{
				frmChangeCharge = new frmChangeCharges()
				{
					MdiParent = this
				};
			}
			frmChangeCharge.Show();
			frmChangeCharge = null;
		}

		private void menuClaimDocs_Click(object sender, EventArgs e)
		{
			Form frmClaimDoc = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmClaimDocs")
				{
					num++;
				}
				else
				{
					frmClaimDoc = form;
					frmClaimDoc.Activate();
					break;
				}
			}
			if (frmClaimDoc == null)
			{
				frmClaimDoc = new frmClaimDocs()
				{
					MdiParent = this
				};
			}
			frmClaimDoc.Show();
			frmClaimDoc = null;
		}

		private void menuFindPayment_Click(object sender, EventArgs e)
		{
			Form _frmFindPayment = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmFindPayment")
				{
					num++;
				}
				else
				{
					_frmFindPayment = form;
					_frmFindPayment.Activate();
					break;
				}
			}
			if (_frmFindPayment == null)
			{
				_frmFindPayment = new frmFindPayment()
				{
					MdiParent = this
				};
			}
			_frmFindPayment.Show();
			_frmFindPayment = null;
		}

		private void menufrmPrintCountNotice_Click(object sender, EventArgs e)
		{
			(new frmPrintCountNotice()).ShowDialog(this);
		}

		private void menuGObjects_Click(object sender, EventArgs e)
		{
		}

		private void menuGRU_Click(object sender, EventArgs e)
		{
			GRUs gRU = new GRUs();
			gRU.Load();
			string[] strArrays = new string[] { "Номер", "Название", "Старый номер", "Примечание" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 100, 300, 100, 200 };
			strArrays = new string[] { "InvNumber", "Name", "Memo", "Note" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник РУ", gRU, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void menuItem10_Click(object sender, EventArgs e)
		{
			(new frmOpenCashChange(2)).ShowDialog(this);
		}

		private void menuItem100_Click(object sender, EventArgs e)
		{
			(new frmGetPlombFile()).Show();
		}

		private void menuItem102_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(9)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem103_Click(object sender, EventArgs e)
		{
			string str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repOffGMeterToPlomb.rpt");
			this.Cursor = Cursors.WaitCursor;
			int[] numArray = new int[] { 2 };
			string[] strArrays = new string[] { "@idperiod" };
			string[] strArrays1 = strArrays;
			strArrays = new string[] { Convert.ToString(0) };
			Form frmReport = new frmReports(str, strArrays1, strArrays, numArray)
			{
				Text = "Справка по ПУ, отключенным до опломбировки",
				MdiParent = Depot._main
			};
			this.Cursor = Cursors.Default;
			frmReport.Show();
			frmReport = null;
		}

		private void menuItem104_Click(object sender, EventArgs e)
		{
			frmPrintAccountList _frmPrintAccountList = new frmPrintAccountList()
			{
				MdiParent = this
			};
			_frmPrintAccountList.Show();
			_frmPrintAccountList.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintAccountList = null;
		}

		private void menuItem105_Click(object sender, EventArgs e)
		{
			frmGetTFPhones frmGetTFPhone = new frmGetTFPhones()
			{
				MdiParent = this
			};
			frmGetTFPhone.Show();
			frmGetTFPhone.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmGetTFPhone = null;
		}

		private void menuItem106_Click(object sender, EventArgs e)
		{
			(new frmGetUnPlombFile()).Show();
		}

		private void menuItem107_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(10)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem109_Click(object sender, EventArgs e)
		{
			Form frmReceptionIndicationTemp = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmReceptionIndicationTemps")
				{
					num++;
				}
				else
				{
					frmReceptionIndicationTemp = form;
					frmReceptionIndicationTemp.Activate();
					break;
				}
			}
			if (frmReceptionIndicationTemp == null)
			{
				frmReceptionIndicationTemp = new frmReceptionIndicationTemps()
				{
					MdiParent = this
				};
			}
			frmReceptionIndicationTemp.Show();
			frmReceptionIndicationTemp = null;
		}

		private void menuItem11_Click(object sender, EventArgs e)
		{
			(new frmOpenCashChange(3)).ShowDialog(this);
		}

		private void menuItem111_Click(object sender, EventArgs e)
		{
			Form frmPersonalCabinetRequest = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmPersonalCabinetRequests")
				{
					num++;
				}
				else
				{
					frmPersonalCabinetRequest = form;
					frmPersonalCabinetRequest.Activate();
					break;
				}
			}
			if (frmPersonalCabinetRequest == null)
			{
				frmPersonalCabinetRequest = new frmPersonalCabinetRequests()
				{
					MdiParent = this
				};
			}
			frmPersonalCabinetRequest.Show();
			frmPersonalCabinetRequest = null;
		}

		private void menuItem112_Click(object sender, EventArgs e)
		{
			string str = Interaction.InputBox("Введите год:", "Параметры отчета", "", 100, 100);
			if (str.Length == 4)
			{
				string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repVerifysGM.rpt");
				this.Cursor = Cursors.WaitCursor;
				int[] numArray = new int[] { 1 };
				string[] strArrays = new string[] { "@Year" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { Convert.ToString(str) };
				Form frmReport = new frmReports(str1, strArrays1, strArrays, numArray)
				{
					Text = "Отчет по поверенным ПУ",
					MdiParent = Depot._main
				};
				this.Cursor = Cursors.Default;
				frmReport.Show();
				frmReport = null;
			}
		}

		private void menuItem113_Click(object sender, EventArgs e)
		{
			frmPrintReestrVerifyGM _frmPrintReestrVerifyGM = new frmPrintReestrVerifyGM()
			{
				MdiParent = this
			};
			_frmPrintReestrVerifyGM.Show();
			_frmPrintReestrVerifyGM.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintReestrVerifyGM = null;
		}

		private void menuItem114_Click(object sender, EventArgs e)
		{
			Form frmDocCreditUsl = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmDocCreditUsls")
				{
					num++;
				}
				else
				{
					frmDocCreditUsl = form;
					frmDocCreditUsl.Activate();
					break;
				}
			}
			if (frmDocCreditUsl == null)
			{
				frmDocCreditUsl = new frmDocCreditUsls()
				{
					MdiParent = this
				};
			}
			frmDocCreditUsl.Show();
			frmDocCreditUsl = null;
		}

		private void menuItem116_Click(object sender, EventArgs e)
		{
			Form frmAddContract = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmAddContracts")
				{
					num++;
				}
				else
				{
					frmAddContract = form;
					frmAddContract.Activate();
					break;
				}
			}
			if (frmAddContract == null)
			{
				frmAddContract = new frmAddContracts()
				{
					MdiParent = this
				};
			}
			frmAddContract.Show();
			frmAddContract = null;
		}

		private void menuItem13_Click(object sender, EventArgs e)
		{
			bool flag = false;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmStart")
				{
					num++;
				}
				else
				{
					form.Activate();
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				frmStart _frmStart = new frmStart()
				{
					MdiParent = this
				};
				_frmStart.Show();
				_frmStart = null;
			}
		}

		private void menuItem14_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void menuItem15_Click(object sender, EventArgs e)
		{
			TypeContracts typeContract = new TypeContracts();
			typeContract.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник типов договоров", typeContract, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void menuItem16_Click(object sender, EventArgs e)
		{
			Form frmSetting = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmSettings")
				{
					num++;
				}
				else
				{
					frmSetting = form;
					break;
				}
			}
			if (frmSetting == null)
			{
				frmSetting = new frmSettings()
				{
					MdiParent = this
				};
			}
			frmSetting.Show();
			frmSetting = null;
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			frmPrintAccount _frmPrintAccount = new frmPrintAccount()
			{
				MdiParent = this
			};
			_frmPrintAccount.Show();
			_frmPrintAccount.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintAccount = null;
		}

		private void menuItem21_Click(object sender, EventArgs e)
		{
			(new frmGetTransportFile(null, null)).ShowDialog(this);
		}

		private void menuItem24_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(3)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem25_Click(object sender, EventArgs e)
		{
			// 
			// Current member / type: System.Void Gefest.frmMain::menuItem25_Click(System.Object,System.EventArgs)
			// File path: C:\Program Files (x86)\gorgaz\Gefest\Gefest.exe
			// 
			// Product version: 2019.1.118.0
			// Exception in: System.Void menuItem25_Click(System.Object,System.EventArgs)
			// 
			// Object reference not set to an instance of an object.
			//    at ..( , Int32 , Statement& , Int32& ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:line 78
			//    at ..( , Int32& , Statement& , Int32& ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:line 33
			//    at ..( ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 57
			//    at ..(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 49
			//    at ..Visit(ICodeNode ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 276
			//    at ..(DecompilationContext ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 33
			//    at ..(MethodBody ,  , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 88
			//    at ..(MethodBody , ILanguage ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 70
			//    at Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:line 95
			//    at Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:line 58
			//    at ..(ILanguage , MethodDefinition ,  ) in C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 117
			// 
			// mailto: JustDecompilePublicFeedback@telerik.com

		}

		private void menuItem29_Click(object sender, EventArgs e)
		{
			frmPrintBigConsumption _frmPrintBigConsumption = new frmPrintBigConsumption()
			{
				MdiParent = this
			};
			_frmPrintBigConsumption.Show();
			_frmPrintBigConsumption.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintBigConsumption = null;
		}

		private void menuItem3_Click(object sender, EventArgs e)
		{
			frmPrintReferenceOnSubscriptionService _frmPrintReferenceOnSubscriptionService = new frmPrintReferenceOnSubscriptionService()
			{
				MdiParent = this
			};
			_frmPrintReferenceOnSubscriptionService.Show();
			_frmPrintReferenceOnSubscriptionService.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintReferenceOnSubscriptionService = null;
		}

		private void menuItem30_Click(object sender, EventArgs e)
		{
			(new frmRecalcBalance()).ShowDialog(this);
		}

		private void menuItem32_Click(object sender, EventArgs e)
		{
			Form frmOldDoc = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmOldDocs")
				{
					num++;
				}
				else
				{
					frmOldDoc = form;
					break;
				}
			}
			if (frmOldDoc == null)
			{
				frmOldDoc = new frmOldDocs()
				{
					MdiParent = this
				};
			}
			frmOldDoc.Show();
			frmOldDoc.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmOldDoc = null;
		}

		private void menuItem33_Click(object sender, EventArgs e)
		{
			(new frmCreateTransport()).ShowDialog(this);
		}

		private void menuItem34_Click(object sender, EventArgs e)
		{
			Form frmActJob = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmActJobs")
				{
					num++;
				}
				else
				{
					frmActJob = form;
					break;
				}
			}
			if (frmActJob == null)
			{
				frmActJob = new frmActJobs()
				{
					MdiParent = this
				};
			}
			frmActJob.Show();
			frmActJob.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmActJob = null;
		}

		private void menuItem35_Click(object sender, EventArgs e)
		{
			frmPrintSaldo _frmPrintSaldo = new frmPrintSaldo()
			{
				MdiParent = this
			};
			_frmPrintSaldo.Show();
			_frmPrintSaldo.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintSaldo = null;
		}

		private void menuItem36_Click_1(object sender, EventArgs e)
		{
			frmPrintPayments frmPrintPayment = new frmPrintPayments()
			{
				MdiParent = this
			};
			frmPrintPayment.Show();
			frmPrintPayment.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintPayment = null;
		}

		private void menuItem37_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(4)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem38_Click(object sender, EventArgs e)
		{
			frmPrintСonditionGobject _frmPrintСonditionGobject = new frmPrintСonditionGobject()
			{
				MdiParent = this
			};
			_frmPrintСonditionGobject.Show();
			_frmPrintСonditionGobject.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintСonditionGobject = null;
		}

		private void menuItem39_Click(object sender, EventArgs e)
		{
			frmPrintDoVerify _frmPrintDoVerify = new frmPrintDoVerify()
			{
				MdiParent = this
			};
			_frmPrintDoVerify.Show();
			_frmPrintDoVerify.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintDoVerify = null;
		}

		private void menuItem4_Click(object sender, EventArgs e)
		{
			(new frmAddress()).ShowDialog(this);
		}

		private void menuItem40_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(5)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem41_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(6)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem42_Click(object sender, EventArgs e)
		{
			Form frmReceptionNotification = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmReceptionNotifications")
				{
					num++;
				}
				else
				{
					frmReceptionNotification = form;
					frmReceptionNotification.Activate();
					break;
				}
			}
			if (frmReceptionNotification == null)
			{
				frmReceptionNotification = new frmReceptionNotifications()
				{
					MdiParent = this
				};
			}
			frmReceptionNotification.Show();
			frmReceptionNotification = null;
		}

		private void menuItem43_Click(object sender, EventArgs e)
		{
			frmPrintSpravkaPoRU _frmPrintSpravkaPoRU = new frmPrintSpravkaPoRU();
			_frmPrintSpravkaPoRU.Show();
			_frmPrintSpravkaPoRU.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintSpravkaPoRU = null;
		}

		private void menuItem44_Click(object sender, EventArgs e)
		{
			frmPrintSpravkaPoOtklKV _frmPrintSpravkaPoOtklKV = new frmPrintSpravkaPoOtklKV();
			_frmPrintSpravkaPoOtklKV.Show();
			_frmPrintSpravkaPoOtklKV.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintSpravkaPoOtklKV = null;
		}

		private void menuItem45_Click(object sender, EventArgs e)
		{
			Form _frmNariad = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmNariad")
				{
					num++;
				}
				else
				{
					_frmNariad = form;
					_frmNariad.Activate();
					break;
				}
			}
			if (_frmNariad == null)
			{
				_frmNariad = new frmNariad()
				{
					MdiParent = this
				};
			}
			_frmNariad.Show();
			_frmNariad.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmNariad = null;
		}

		private void menuItem46_Click(object sender, EventArgs e)
		{
			frmPrintDubleNumberPU _frmPrintDubleNumberPU = new frmPrintDubleNumberPU()
			{
				MdiParent = this
			};
			_frmPrintDubleNumberPU.Show();
			_frmPrintDubleNumberPU.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintDubleNumberPU = null;
		}

		private void menuItem47_Click(object sender, EventArgs e)
		{
			frmPrintDoVerifyNO _frmPrintDoVerifyNO = new frmPrintDoVerifyNO()
			{
				MdiParent = this
			};
			_frmPrintDoVerifyNO.Show();
			_frmPrintDoVerifyNO.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintDoVerifyNO = null;
		}

		private void menuItem48_Click(object sender, EventArgs e)
		{
			(new frmGetChanges()).Show();
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(2)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem50_Click(object sender, EventArgs e)
		{
			Form _frmUnloadingInTextFile = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmUnloadingInTextFile")
				{
					num++;
				}
				else
				{
					_frmUnloadingInTextFile = form;
					_frmUnloadingInTextFile.Activate();
					break;
				}
			}
			if (_frmUnloadingInTextFile == null)
			{
				_frmUnloadingInTextFile = new frmUnloadingInTextFile()
				{
					MdiParent = this
				};
			}
			_frmUnloadingInTextFile.Show();
			_frmUnloadingInTextFile.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmUnloadingInTextFile = null;
		}

		private void menuItem51_Click(object sender, EventArgs e)
		{
			Form frmDocVerify = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmDocVerifys")
				{
					num++;
				}
				else
				{
					frmDocVerify = form;
					frmDocVerify.Activate();
					break;
				}
			}
			if (frmDocVerify == null)
			{
				frmDocVerify = new frmDocVerifys()
				{
					MdiParent = this
				};
			}
			frmDocVerify.Show();
			frmDocVerify = null;
		}

		private void menuItem52_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(7)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem54_Click(object sender, EventArgs e)
		{
			frmPrintAnalysisDebtsDuty _frmPrintAnalysisDebtsDuty = new frmPrintAnalysisDebtsDuty()
			{
				MdiParent = this
			};
			_frmPrintAnalysisDebtsDuty.Show();
			_frmPrintAnalysisDebtsDuty.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintAnalysisDebtsDuty = null;
		}

		private void menuItem55_Click(object sender, EventArgs e)
		{
			(new frmActionForPeriod()).ShowDialog(this);
		}

		private void menuItem56_Click(object sender, EventArgs e)
		{
			frmPrintSvedenWorkAbonent _frmPrintSvedenWorkAbonent = new frmPrintSvedenWorkAbonent()
			{
				MdiParent = this
			};
			_frmPrintSvedenWorkAbonent.Show();
			_frmPrintSvedenWorkAbonent.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintSvedenWorkAbonent = null;
		}

		private void menuItem58_Click(object sender, EventArgs e)
		{
			FrmPrintSpisokGRU frmPrintSpisokGRU = new FrmPrintSpisokGRU()
			{
				MdiParent = this
			};
			frmPrintSpisokGRU.Show();
			frmPrintSpisokGRU.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintSpisokGRU = null;
		}

		private void menuItem59_Click(object sender, EventArgs e)
		{
			frmPrinDoubleIndication _frmPrinDoubleIndication = new frmPrinDoubleIndication()
			{
				MdiParent = this
			};
			_frmPrinDoubleIndication.Show();
			_frmPrinDoubleIndication.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrinDoubleIndication = null;
		}

		private void menuItem6_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(1)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem60_Click(object sender, EventArgs e)
		{
			frmPrintReferenceOnSubscriptionServiceUR _frmPrintReferenceOnSubscriptionServiceUR = new frmPrintReferenceOnSubscriptionServiceUR()
			{
				MdiParent = this
			};
			_frmPrintReferenceOnSubscriptionServiceUR.Show();
			_frmPrintReferenceOnSubscriptionServiceUR.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintReferenceOnSubscriptionServiceUR = null;
		}

		private void menuItem61_Click(object sender, EventArgs e)
		{
			frmPrintAnaliz _frmPrintAnaliz = new frmPrintAnaliz()
			{
				MdiParent = this
			};
			_frmPrintAnaliz.Show();
			_frmPrintAnaliz.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintAnaliz = null;
		}

		private void menuItem62_Click(object sender, EventArgs e)
		{
			frmPrintSpravkaVnovUstPU _frmPrintSpravkaVnovUstPU = new frmPrintSpravkaVnovUstPU()
			{
				MdiParent = this
			};
			_frmPrintSpravkaVnovUstPU.Show();
			_frmPrintSpravkaVnovUstPU.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintSpravkaVnovUstPU = null;
		}

		private void menuItem64_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, string.Concat(Depot.oSettings.ReportPath.Trim(), "HelpGefest.chm"));
		}

		private void menuItem65_Click(object sender, EventArgs e)
		{
			frmPrinSaldoPoGRU _frmPrinSaldoPoGRU = new frmPrinSaldoPoGRU()
			{
				MdiParent = this
			};
			_frmPrinSaldoPoGRU.Show();
			_frmPrinSaldoPoGRU.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrinSaldoPoGRU = null;
		}

		private void menuItem66_Click(object sender, EventArgs e)
		{
			frmPrintReestrUrLic _frmPrintReestrUrLic = new frmPrintReestrUrLic()
			{
				MdiParent = this
			};
			_frmPrintReestrUrLic.Show();
			_frmPrintReestrUrLic.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintReestrUrLic = null;
		}

		private void menuItem68_Click(object sender, EventArgs e)
		{
			frmPrintDoVerifyNOSpisok _frmPrintDoVerifyNOSpisok = new frmPrintDoVerifyNOSpisok()
			{
				MdiParent = this
			};
			_frmPrintDoVerifyNOSpisok.Show();
			_frmPrintDoVerifyNOSpisok.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintDoVerifyNOSpisok = null;
		}

		private void menuItem69_Click(object sender, EventArgs e)
		{
			TypeReasonDisconnects typeReasonDisconnect = new TypeReasonDisconnects();
			typeReasonDisconnect.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник причин отключения ПУ", typeReasonDisconnect, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void menuItem7_Click(object sender, EventArgs e)
		{
			bool flag = false;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmFindContract")
				{
					num++;
				}
				else
				{
					form.Activate();
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				frmFindContract _frmFindContract = new frmFindContract()
				{
					MdiParent = this
				};
				_frmFindContract.Show();
				_frmFindContract = null;
			}
		}

		private void menuItem70_Click(object sender, EventArgs e)
		{
			frmPrintSaldoWorkPlace _frmPrintSaldoWorkPlace = new frmPrintSaldoWorkPlace()
			{
				MdiParent = this
			};
			_frmPrintSaldoWorkPlace.Show();
			_frmPrintSaldoWorkPlace.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintSaldoWorkPlace = null;
		}

		private void menuItem71_Click(object sender, EventArgs e)
		{
			Stavkas stavka = new Stavkas();
			stavka.Load();
			string[] strArrays = new string[] { "Значение", "Дата" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200, 200 };
			strArrays = new string[] { "Name", "Date" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewDateSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник Cтавок рефинансирование ", stavka, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void menuItem73_Click(object sender, EventArgs e)
		{
			frmPrintPenyPoContractu _frmPrintPenyPoContractu = new frmPrintPenyPoContractu()
			{
				MdiParent = this
			};
			_frmPrintPenyPoContractu.Show();
			_frmPrintPenyPoContractu.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintPenyPoContractu = null;
		}

		private void menuItem74_Click(object sender, EventArgs e)
		{
			frmPrintSaldoPoKvart _frmPrintSaldoPoKvart = new frmPrintSaldoPoKvart()
			{
				MdiParent = this
			};
			_frmPrintSaldoPoKvart.Show();
			_frmPrintSaldoPoKvart.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintSaldoPoKvart = null;
		}

		private void menuItem75_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGas frmPrintConsumptionGa = new frmPrintConsumptionGas(8)
			{
				MdiParent = this
			};
			frmPrintConsumptionGa.Show();
			frmPrintConsumptionGa.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintConsumptionGa = null;
		}

		private void menuItem76_Click(object sender, EventArgs e)
		{
			frmPrintSpisokDomPodkKGS frmPrintSpisokDomPodkKG = new frmPrintSpisokDomPodkKGS()
			{
				MdiParent = this
			};
			frmPrintSpisokDomPodkKG.Show();
			frmPrintSpisokDomPodkKG.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintSpisokDomPodkKG = null;
		}

		private void menuItem77_Click(object sender, EventArgs e)
		{
			frmPrintNoAkt _frmPrintNoAkt = new frmPrintNoAkt()
			{
				MdiParent = this
			};
			_frmPrintNoAkt.Show();
			_frmPrintNoAkt.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintNoAkt = null;
		}

		private void menuItem78_Click(object sender, EventArgs e)
		{
			TypeInfringementss typeInfringementss = new TypeInfringementss();
			typeInfringementss.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник типов нарушений", typeInfringementss, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void menuItem79_Click(object sender, EventArgs e)
		{
			Form frmTehObsluch = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmTehObsluchs")
				{
					num++;
				}
				else
				{
					frmTehObsluch = form;
					frmTehObsluch.Activate();
					break;
				}
			}
			if (frmTehObsluch == null)
			{
				frmTehObsluch = new frmTehObsluchs()
				{
					MdiParent = this
				};
			}
			frmTehObsluch.Show();
			frmTehObsluch = null;
		}

		private void menuItem80_Click(object sender, EventArgs e)
		{
			frmPrintUslugiVDGO _frmPrintUslugiVDGO = new frmPrintUslugiVDGO()
			{
				MdiParent = this
			};
			_frmPrintUslugiVDGO.Show();
			_frmPrintUslugiVDGO.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintUslugiVDGO = null;
		}

		private void menuItem81_Click(object sender, EventArgs e)
		{
			frmPrintReestrOplOfKass _frmPrintReestrOplOfKass = new frmPrintReestrOplOfKass()
			{
				MdiParent = this
			};
			_frmPrintReestrOplOfKass.Show();
			_frmPrintReestrOplOfKass.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintReestrOplOfKass = null;
		}

		private void menuItem82_Click(object sender, EventArgs e)
		{
			frmPrintReestrOplOfBank _frmPrintReestrOplOfBank = new frmPrintReestrOplOfBank()
			{
				MdiParent = this
			};
			_frmPrintReestrOplOfBank.Show();
			_frmPrintReestrOplOfBank.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintReestrOplOfBank = null;
		}

		private void menuItem83_Click(object sender, EventArgs e)
		{
			frmPrintProchimVidamVDGO _frmPrintProchimVidamVDGO = new frmPrintProchimVidamVDGO(0)
			{
				MdiParent = this
			};
			_frmPrintProchimVidamVDGO.Show();
			_frmPrintProchimVidamVDGO.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintProchimVidamVDGO = null;
		}

		private void menuItem84_Click(object sender, EventArgs e)
		{
			frmPrintrepReferenceOnSubscriptionServiceRuk _frmPrintrepReferenceOnSubscriptionServiceRuk = new frmPrintrepReferenceOnSubscriptionServiceRuk()
			{
				MdiParent = this
			};
			_frmPrintrepReferenceOnSubscriptionServiceRuk.Show();
			_frmPrintrepReferenceOnSubscriptionServiceRuk.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintrepReferenceOnSubscriptionServiceRuk = null;
		}

		private void menuItem85_Click(object sender, EventArgs e)
		{
			frmPrintAccountOpt _frmPrintAccountOpt = new frmPrintAccountOpt()
			{
				MdiParent = this
			};
			_frmPrintAccountOpt.Show();
			_frmPrintAccountOpt.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintAccountOpt = null;
		}

		private void menuItem86_Click(object sender, EventArgs e)
		{
			this.ECR.DeviceEnabled = true;
			if (this.ECR.ResultCode != 0)
			{
				MessageBox.Show("Не\tвозможно подключить порт!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			this.ECR = new FprnM45Class();
			this.ECR.ShowProperties();
		}

		private void menuItem87_Click(object sender, EventArgs e)
		{
			frmCloseKKM _frmCloseKKM = new frmCloseKKM()
			{
				MdiParent = this
			};
			_frmCloseKKM.Show();
			_frmCloseKKM.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmCloseKKM = null;
		}

		private void menuItem88_Click(object sender, EventArgs e)
		{
			frmPrintReestrPenyGosPO _frmPrintReestrPenyGosPO = new frmPrintReestrPenyGosPO()
			{
				MdiParent = this
			};
			_frmPrintReestrPenyGosPO.Show();
			_frmPrintReestrPenyGosPO.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintReestrPenyGosPO = null;
		}

		private void menuItem89_Click(object sender, EventArgs e)
		{
			MessageBox.Show(string.Concat("Версия - 1153/", Application.ProductVersion.ToString()), "Gefest", MessageBoxButtons.OK);
		}

		private void menuItem9_Click(object sender, EventArgs e)
		{
			(new frmOpenCashChange(1)).ShowDialog(this);
		}

		private void menuItem90_Click(object sender, EventArgs e)
		{
			frmPrintConsumptionGasUL _frmPrintConsumptionGasUL = new frmPrintConsumptionGasUL(0)
			{
				MdiParent = this
			};
			_frmPrintConsumptionGasUL.Show();
			_frmPrintConsumptionGasUL.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintConsumptionGasUL = null;
		}

		private void menuItem91_Click(object sender, EventArgs e)
		{
			frmPrintPaymentsMonth _frmPrintPaymentsMonth = new frmPrintPaymentsMonth(0)
			{
				MdiParent = this
			};
			_frmPrintPaymentsMonth.Show();
			_frmPrintPaymentsMonth.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintPaymentsMonth = null;
		}

		private void menuItem92_Click(object sender, EventArgs e)
		{
			frmPrintOldDebet _frmPrintOldDebet = new frmPrintOldDebet(0)
			{
				MdiParent = this
			};
			_frmPrintOldDebet.Show();
			_frmPrintOldDebet.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintOldDebet = null;
		}

		private void menuItem93_Click(object sender, EventArgs e)
		{
			frmPrintDebitors frmPrintDebitor = new frmPrintDebitors()
			{
				MdiParent = this
			};
			frmPrintDebitor.Show();
			frmPrintDebitor.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			frmPrintDebitor = null;
		}

		private void menuItem94_Click(object sender, EventArgs e)
		{
			frmPrintProchimVidamVDGO _frmPrintProchimVidamVDGO = new frmPrintProchimVidamVDGO(1)
			{
				MdiParent = this
			};
			_frmPrintProchimVidamVDGO.Show();
			_frmPrintProchimVidamVDGO.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintProchimVidamVDGO = null;
		}

		private void menuItem95_Click(object sender, EventArgs e)
		{
			frmPrintProchimVidamVDGO _frmPrintProchimVidamVDGO = new frmPrintProchimVidamVDGO(2)
			{
				MdiParent = this
			};
			_frmPrintProchimVidamVDGO.Show();
			_frmPrintProchimVidamVDGO.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintProchimVidamVDGO = null;
		}

		private void menuItem96_Click(object sender, EventArgs e)
		{
			frmPrintDoVerifyGTM _frmPrintDoVerifyGTM = new frmPrintDoVerifyGTM()
			{
				MdiParent = this
			};
			_frmPrintDoVerifyGTM.Show();
			_frmPrintDoVerifyGTM.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintDoVerifyGTM = null;
		}

		private void menuItem97_Click(object sender, EventArgs e)
		{
			frmPrintAnalysisMainDebtsDuty _frmPrintAnalysisMainDebtsDuty = new frmPrintAnalysisMainDebtsDuty()
			{
				MdiParent = this
			};
			_frmPrintAnalysisMainDebtsDuty.Show();
			_frmPrintAnalysisMainDebtsDuty.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintAnalysisMainDebtsDuty = null;
		}

		private void menuItem99_Click(object sender, EventArgs e)
		{
			frmCashRepTerminal _frmCashRepTerminal = new frmCashRepTerminal()
			{
				MdiParent = this
			};
			_frmCashRepTerminal.Show();
			_frmCashRepTerminal.WindowState = FormWindowState.Normal;
			_frmCashRepTerminal.StartPosition = FormStartPosition.CenterScreen;
			base.Invalidate(true);
			_frmCashRepTerminal = null;
		}

		private void menuLegalDocs_Click(object sender, EventArgs e)
		{
			Form frmLegalDoc = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmLegalDocs")
				{
					num++;
				}
				else
				{
					frmLegalDoc = form;
					frmLegalDoc.Activate();
					break;
				}
			}
			if (frmLegalDoc == null)
			{
				frmLegalDoc = new frmLegalDocs()
				{
					MdiParent = this
				};
			}
			frmLegalDoc.Show();
			frmLegalDoc = null;
		}

		private void menuRecalcCash_Click(object sender, EventArgs e)
		{
			frmRecalcCashBalance _frmRecalcCashBalance = new frmRecalcCashBalance()
			{
				MdiParent = this
			};
			_frmRecalcCashBalance.Show();
			_frmRecalcCashBalance.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmRecalcCashBalance = null;
		}

		private void menuReceptionIndication_Click(object sender, EventArgs e)
		{
			Form _frmReceptionIndication = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmReceptionIndication")
				{
					num++;
				}
				else
				{
					_frmReceptionIndication = form;
					_frmReceptionIndication.Activate();
					break;
				}
			}
			if (_frmReceptionIndication == null)
			{
				_frmReceptionIndication = new frmReceptionIndication()
				{
					MdiParent = this
				};
			}
			_frmReceptionIndication.Show();
			_frmReceptionIndication = null;
		}

		private void menuReceptionIndicationTemp_Click(object sender, EventArgs e)
		{
			Form _frmReceptionIndicationTemp = null;
			Form[] mdiChildren = base.MdiChildren;
			int num = 0;
			while (num < (int)mdiChildren.Length)
			{
				Form form = mdiChildren[num];
				if (form.Name != "frmReceptionIndicationTemp")
				{
					num++;
				}
				else
				{
					_frmReceptionIndicationTemp = form;
					_frmReceptionIndicationTemp.Activate();
					break;
				}
			}
			if (_frmReceptionIndicationTemp == null)
			{
				_frmReceptionIndicationTemp = new frmReceptionIndicationTemp()
				{
					MdiParent = this
				};
			}
			_frmReceptionIndicationTemp.Show();
			_frmReceptionIndicationTemp = null;
		}

		private void menurepGarbTask_Click(object sender, EventArgs e)
		{
			frmPrintGarbTask _frmPrintGarbTask = new frmPrintGarbTask();
			_frmPrintGarbTask.Show();
			_frmPrintGarbTask.WindowState = FormWindowState.Normal;
			base.Invalidate(true);
			_frmPrintGarbTask = null;
		}

		private void mnuAgent_Click(object sender, EventArgs e)
		{
			Agents agent = new Agents();
			agent.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник агентов", agent, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void mnuCascade_Click(object sender, EventArgs e)
		{
			base.LayoutMdi(MdiLayout.Cascade);
		}

		private void mnuCloseAllWindow_Click(object sender, EventArgs e)
		{
			while ((int)base.MdiChildren.Length > 0)
			{
				base.MdiChildren[0].Close();
			}
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void mnuLeftRight_Click(object sender, EventArgs e)
		{
			base.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void mnuTopBottom_Click(object sender, EventArgs e)
		{
			base.LayoutMdi(MdiLayout.TileVertical);
		}

		private void mnuTypeBatch_Click(object sender, EventArgs e)
		{
			TypeBatchs typeBatch = new TypeBatchs();
			typeBatch.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник типов пачки", typeBatch, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void mnuTypeDocument_Click(object sender, EventArgs e)
		{
			TypeDocuments typeDocument = new TypeDocuments();
			typeDocument.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник типов документов", typeDocument, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void mnuTypeGMeter_Click(object sender, EventArgs e)
		{
			TypeGMeters typeGMeter = new TypeGMeters();
			typeGMeter.Load();
			string[] strArrays = new string[] { "Название", "Класс точности", "Меж провер. интер." };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200, 150, 150 };
			strArrays = new string[] { "Name", "ClassAccuracy", "ServiceLife" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник приборов учета", typeGMeter, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void mnuTypeGobject_Click(object sender, EventArgs e)
		{
			TypeGobjects typeGobject = new TypeGobjects();
			typeGobject.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник объектов учета", typeGobject, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void mnuTypeOperation_Click(object sender, EventArgs e)
		{
			TypeOperations typeOperation = new TypeOperations();
			typeOperation.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник типов операций", typeOperation, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		private void mnuTypePay_Click(object sender, EventArgs e)
		{
			TypePays typePay = new TypePays();
			typePay.Load();
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник типов оплаты", typePay, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
		}

		public void SetFrmNeedToActivate(Form frm)
		{
			this.frmNeedToActivate = frm;
		}

		private void tbMain_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button.Tag != null)
			{
				MenuItem menuItem = base.Menu.FindMenuItem(0, (IntPtr)e.Button.Tag);
				if (menuItem != null)
				{
					menuItem.PerformClick();
				}
			}
		}
	}
}