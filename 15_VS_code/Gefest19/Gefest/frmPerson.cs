using C1.Win.C1Command;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmPerson : Form
	{
		private TabControl tabControl1;

		private TabPage tabPage1;

		private TextBox txtSurname;

		private Label label1;

		private Label label2;

		private TextBox txtName;

		private Label label3;

		private TextBox txtPatron;

		private Label label4;

		private TextBox txtRNN;

		private Label label5;

		private C1Combo cmbSocial;

		private C1Combo cmbPayment;

		private Label label6;

		private GroupBox groupBox1;

		private TextBox txtAddress;

		private GroupBox groupBox2;

		private GroupBox groupBox3;

		private Button cmdOk;

		private Button cmdCancel;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader6;

		private ImageList imageList1;

		private C1ToolBar tbDoc;

		private C1CommandLink c1CommandLink2;

		private C1CommandLink c1CommandLink1;

		private C1CommandLink c1CommandLink3;

		private C1CommandLink c1CommandLink5;

		private C1CommandHolder c1CommandHolder1;

		private C1Command c1Command1;

		private C1Command c1Command2;

		private C1Command c1Command3;

		private C1Command c1Command4;

		private ListView lvPhone;

		private ListView lvContract;

		private C1ToolBar c1ToolBar1;

		private C1CommandLink c1CommandLink4;

		private C1CommandLink c1CommandLink6;

		private C1CommandLink c1CommandLink7;

		private C1CommandLink c1CommandLink8;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private IContainer components;

		private Person _person;

		private Ownerships _ownerships;

		private Button cmdApply;

		private Classifiers _classifiers;

		private Button cmdAddress;

		private Button cmdPayment;

		private Button cmdSocial;

		private C1Command c1Command5;

		private C1Command c1Command6;

		private C1Command c1Command7;

		private C1Command c1Command8;

		private Label label7;

		private TextBox txtWork;

		private TabPage tabPage2;

		private ListView listView1;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private ColumnHeader columnHeader10;

		private TextBox txtMemo;

		private Label label17;

		private TabPage tabPage3;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader12;

		private ColumnHeader columnHeader13;

		private ColumnHeader columnHeader14;

		private ColumnHeader columnHeader15;

		private ColumnHeader columnHeader16;

		private ColumnHeader columnHeader19;

		private ColumnHeader columnHeader20;

		private ListView list2;

		private TextBox txtNUmberUDl;

		private Label label8;

		private ColumnHeader columnHeader17;

		private ColumnHeader columnHeader18;

		private TabPage tabPage4;

		private Label lblPassword;

		private Label label11;

		private Button bGeneratePassword;

		private Button bSavePassword;

		private Address _address;

		private Label label9;

		private TextBox txtEMail;

		private Contract _contract;

		private TextBox txtSurnameKZ;

		private TextBox txtNameKZ;

		private TextBox txtPatronKZ;

		private Button bCopyFIO;

		private Label label10;

		private Label lblState;

		private Button bBlockCabinet;

		private Button bDelPC;

		private PersonalCabinet _personalcabinet;

		public frmPerson(Person oPerson)
		{
			this._person = oPerson;
			this.InitializeComponent();
		}

		private void bBlockCabinet_Click(object sender, EventArgs e)
		{
			string str = "";
			if (this._personalcabinet.State != 1)
			{
				this._personalcabinet.State = 1;
				str = "Кабинет разблокирован!";
				this.bBlockCabinet.Text = "Заблокировать";
			}
			else
			{
				this._personalcabinet.State = 2;
				str = "Кабинет заблокирован!";
				this.bBlockCabinet.Text = "Разблокировать";
			}
			this._personalcabinet.Save();
			this._contract.oPersonalCabinet = this._personalcabinet;
			this.SetStrStateCabinet(this._personalcabinet.State);
			MessageBox.Show(str, "ОК", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void bCopyFIO_Click(object sender, EventArgs e)
		{
			this.txtSurnameKZ.Text = this.txtSurname.Text;
			this.txtNameKZ.Text = this.txtName.Text;
			this.txtPatronKZ.Text = this.txtPatron.Text;
		}

		private void bDelPC_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Аннулировать личный кабинет?", "Личный кабинет", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				this._contract.PCEmail = "";
				this._contract.PCPass = "";
				this._contract.PCPassword = "";
				this._contract.Save();
			}
		}

		private void bGeneratePassword_Click(object sender, EventArgs e)
		{
			int[] numArray = new int[8];
			Random random = new Random();
			string str = "";
			for (int i = 0; i < (int)numArray.Length; i++)
			{
				if (i % 2 != 0)
				{
					numArray[i] = random.Next(48, 57);
				}
				else
				{
					numArray[i] = random.Next(97, 122);
				}
				str = string.Concat(str, (char)numArray[i]);
			}
			this._personalcabinet.PCPass = str;
			this._personalcabinet.PCPassword = this.GetHashString(str);
			this.lblPassword.Text = "Пароль успешно сгенерирован";
			this.bSavePassword.Enabled = true;
		}

		private void bSavePassword_Click(object sender, EventArgs e)
		{
			if (this.txtEMail.Text.Length <= 0)
			{
				MessageBox.Show("Не указан адрес электронной почты", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				this._personalcabinet.Account = this._contract.Account;
				this._personalcabinet.State = 1;
				this._personalcabinet.PCEmail = this.txtEMail.Text;
				this._personalcabinet.Save();
				this._contract.oPersonalCabinet = this._personalcabinet;
				this._contract.Save();
				MessageBox.Show("Сохранено", "ОК", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			this.bSavePassword.Enabled = false;
		}

		private void c1Command1_Click(object sender, ClickEventArgs e)
		{
			if (this._person.get_isNew())
			{
				MessageBox.Show("Сохраните изменения!");
				return;
			}
			string[] strArrays = new string[] { "NumberPhone" };
			string[] strArrays1 = strArrays;
			strArrays = new string[] { "Номер телефона:" };
			string[] strArrays2 = strArrays;
			string[] strArrays3 = null;
			frmSimpleObj _frmSimpleObj = new frmSimpleObj("Добавление номера телефона", this._person.oPhones.Add(), strArrays1, strArrays2, strArrays3);
			_frmSimpleObj.ShowDialog(this);
			_frmSimpleObj = null;
			this.CreatePhones();
		}

		private void c1Command2_Click(object sender, ClickEventArgs e)
		{
			if (this.lvPhone.SelectedItems.Count > 0)
			{
				string[] strArrays = new string[] { "NumberPhone" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { "Номер телефона:" };
				string[] strArrays2 = strArrays;
				string[] strArrays3 = null;
				frmSimpleObj _frmSimpleObj = new frmSimpleObj("Добавление номера телефона", this._person.oPhones.item((long)this.lvPhone.SelectedItems[0].Tag), strArrays1, strArrays2, strArrays3);
				_frmSimpleObj.ShowDialog(this);
				_frmSimpleObj = null;
				this.CreatePhones();
			}
		}

		private void c1Command3_Click(object sender, ClickEventArgs e)
		{
			if (this.lvPhone.SelectedItems.Count > 0)
			{
				Phone phone = this._person.oPhones.item((long)this.lvPhone.SelectedItems[0].Tag);
				if (MessageBox.Show(string.Concat("Удалить номер тефона ", phone.get_Name(), "?"), "Номер телефона", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					this._person.oPhones.Remove(phone.get_ID());
					this.CreatePhones();
				}
				phone = null;
			}
		}

		private void c1Command4_Click(object sender, ClickEventArgs e)
		{
			Tools.ConvertToExcel(this.lvPhone);
		}

		private void c1Command5_Click(object sender, ClickEventArgs e)
		{
			if (this._person.get_isNew())
			{
				MessageBox.Show("Сохраните изменения!");
				return;
			}
			Contract contract = this._person.oContracts.Add();
			(new frmContract(contract)).ShowDialog(this);
			this.CreateContract();
		}

		private void c1Command6_Click(object sender, ClickEventArgs e)
		{
			if (this.lvContract.SelectedItems.Count > 0)
			{
				Contract contract = this._person.oContracts.item((long)this.lvContract.SelectedItems[0].Tag);
				(new frmContract(contract)).ShowDialog(this);
				this.CreateContract();
			}
		}

		private void c1Command7_Click(object sender, ClickEventArgs e)
		{
			if (this.lvContract.SelectedItems.Count > 0)
			{
				Contract contract = this._person.oContracts.item((long)this.lvContract.SelectedItems[0].Tag);
				if (MessageBox.Show(string.Concat("Удалить договор ", contract.Account, "?"), "Договор", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					this._person.oContracts.Remove(contract.get_ID());
					this.CreateContract();
				}
				contract = null;
			}
		}

		private void c1Command8_Click(object sender, ClickEventArgs e)
		{
			Tools.ConvertToExcel(this.lvContract);
		}

		private void cmbSocial_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbPayment.Focus();
			}
		}

		private void cmdAddress_Click(object sender, EventArgs e)
		{
			frmAddress _frmAddress = new frmAddress()
			{
				oAddress = this._address
			};
			_frmAddress.ShowDialog(this);
			if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				this._address = _frmAddress.oAddress;
				this.txtAddress.Text = this._address.get_ShortAddress();
			}
			_frmAddress = null;
		}

		private void cmdApply_Click(object sender, EventArgs e)
		{
			if (this.Save())
			{
				MessageBox.Show("Сохранено!", "Потребитель", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			MessageBox.Show("Ошибка при сохранении", "Потребитель", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			if (this.Save())
			{
				base.Close();
				return;
			}
			MessageBox.Show("Ошибка при сохранении", "Потребитель", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		private void cmdPayment_Click(object sender, EventArgs e)
		{
			Classifiers classifier = this._classifiers;
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Платежеспособность", classifier, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			this.CreateClassifier();
		}

		private void cmdSocial_Click(object sender, EventArgs e)
		{
			Ownerships ownership = this._ownerships;
			string[] strArrays = new string[] { "Название" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Социальное положение", ownership, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			this.CreateOwnerShip();
		}

		private void CreateClassifier()
		{
			if (this._classifiers == null)
			{
				this._classifiers = new Classifiers();
				this._classifiers.Load(this._person.isJuridical);
			}
			long d = (long)0;
			if (this._person.oClassifier != null)
			{
				d = this._person.oClassifier.get_ID();
			}
			Tools.FillC1(this._classifiers, this.cmbPayment, d);
		}

		private void CreateContract()
		{
			this.lvContract.Items.Clear();
			this._contract = new Contract();
			foreach (Contract oContract in this._person.oContracts)
			{
				this._contract = oContract;
				this._personalcabinet = oContract.oPersonalCabinet;
				ListViewItem d = this.lvContract.Items.Add(oContract.Account);
				d.Tag = oContract.get_ID();
				d.SubItems.Add(oContract.StastusName);
				if (oContract.oGobjects.get_Count() <= 0)
				{
					d.SubItems.Add("");
				}
				else
				{
					d.SubItems.Add(oContract.oGobjects[0].oAddress.get_ShortAddress());
				}
				ListViewItem.ListViewSubItemCollection subItems = d.SubItems;
				double num = oContract.CurrentBalance();
				subItems.Add(num.ToString("#.##"));
			}
			if (this._personalcabinet == null)
			{
				this.txtEMail.Text = this._contract.PCEmail;
			}
			else
			{
				this.SetStrStateCabinet(this._personalcabinet.State);
				this.txtEMail.Text = this._personalcabinet.PCEmail;
			}
			if (this.txtEMail.Text.Length == 0)
			{
				this.txtEMail.Text = this._contract.PCEmail;
			}
		}

		private void CreateHistory()
		{
			ListViewItem listViewItem;
			List list = new List();
			long d = this._person.get_ID();
			string str = string.Concat("select distinct datevalues,dbo.fGetFIOPerson (datevalues,idobject, 'Surname'),dbo.fGetFIOPerson (datevalues,idobject, 'name'),dbo.fGetFIOPerson (datevalues,idobject, 'Patronic') ,dbo.fGetFIOPerson (datevalues,idobject, 'rnn'),dbo.fGetFIOPerson (datevalues,idobject, 'NumberUDL') ,dbo.fgetuser(convert(int,dbo.fGetFIOPerson (datevalues,idobject, 'Iduser')))from oldvalues where idobject=", d.ToString(), " and nametable='Person' and namecolumn<>'IsJuridical' and namecolumn<>'WorkPlace'and datevalues>='1900-01-01'");
			list.set_nametable_pr("OldValues");
			list.set_select_pr(str);
			list.Load();
			this.listView1.Items.Clear();
			foreach (string[] mylistPr in list.get_mylist_pr())
			{
				listViewItem = this.listView1.Items.Add(mylistPr[1]);
				listViewItem.SubItems.Add(mylistPr[2]);
				listViewItem.SubItems.Add(mylistPr[3]);
				listViewItem.SubItems.Add(mylistPr[4]);
				listViewItem.SubItems.Add(mylistPr[5]);
				listViewItem.SubItems.Add(mylistPr[0]);
				listViewItem.SubItems.Add(mylistPr[6]);
			}
			listViewItem = this.listView1.Items.Add(this._person.Surname);
			listViewItem.SubItems.Add(this._person.Name);
			listViewItem.SubItems.Add(this._person.Patronic);
			listViewItem.SubItems.Add(this._person.RNN);
			listViewItem.SubItems.Add(this._person.NumberUDL);
			listViewItem.SubItems.Add("текущие");
			if (this._person.oUser == null)
			{
				listViewItem.SubItems.Add("");
				return;
			}
			listViewItem.SubItems.Add(this._person.oUser.get_Name());
		}

		private void CreateHistoryRab()
		{
			ListViewItem listViewItem;
			List list = new List();
			long d = this._person.get_ID();
			string str = string.Concat("select distinct datevalues,dbo.fGetWork (datevalues,idobject, 'WorkPlace') , dbo.fgetuser(convert(int,dbo.fGetWork (datevalues,idobject, 'Iduser')))from oldvalues where idobject=", d.ToString(), " and nametable='Person' and namecolumn<>'IsJuridical' and namecolumn='WorkPlace' and datevalues>='1900-01-01'");
			list.set_nametable_pr("OldValues");
			list.set_select_pr(str);
			list.Load();
			this.list2.Items.Clear();
			foreach (string[] mylistPr in list.get_mylist_pr())
			{
				listViewItem = this.list2.Items.Add(mylistPr[1]);
				listViewItem.SubItems.Add(mylistPr[0]);
				listViewItem.SubItems.Add(mylistPr[2]);
			}
			listViewItem = this.list2.Items.Add(this._person.WorkPlace);
			listViewItem.SubItems.Add("текущие");
			listViewItem.SubItems.Add(this._person.oUser.get_Name());
		}

		private void CreateOwnerShip()
		{
			if (this._ownerships == null)
			{
				this._ownerships = new Ownerships();
				this._ownerships.Load(this._person.isJuridical);
			}
			long d = (long)0;
			if (this._person.oOwnership != null)
			{
				d = this._person.oOwnership.get_ID();
			}
			Tools.FillC1(this._ownerships, this.cmbSocial, d);
		}

		private void CreatePhones()
		{
			this.lvPhone.Items.Clear();
			foreach (Phone oPhone in this._person.oPhones)
			{
				ListViewItem d = this.lvPhone.Items.Add(oPhone.get_Name());
				d.Tag = oPhone.get_ID();
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

		private void frmPerson_Closing(object sender, CancelEventArgs e)
		{
		}

		private void frmPerson_Load(object sender, EventArgs e)
		{
			if (this._person == null)
			{
				base.Close();
			}
			this.txtSurname.Text = this._person.Surname;
			this.txtName.Text = this._person.Name;
			this.txtPatron.Text = this._person.Patronic;
			this.txtSurnameKZ.Text = this._person.SurnameKZ;
			this.txtNameKZ.Text = this._person.NameKZ;
			this.txtPatronKZ.Text = this._person.PatronicKZ;
			this.txtRNN.Text = this._person.RNN;
			this.txtNUmberUDl.Text = this._person.NumberUDL;
			this.txtWork.Text = this._person.WorkPlace;
			this.txtMemo.Text = this._person.Memo;
			if (this._person.oAddress != null)
			{
				this._address = this._person.oAddress;
				this.txtAddress.Text = this._person.oAddress.get_ShortAddress();
			}
			this.CreateOwnerShip();
			this.CreateClassifier();
			this.CreatePhones();
			this.CreateContract();
			this.CreateHistory();
			this.CreateHistoryRab();
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmPerson));
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.bCopyFIO = new Button();
			this.txtPatronKZ = new TextBox();
			this.txtNameKZ = new TextBox();
			this.txtSurnameKZ = new TextBox();
			this.txtMemo = new TextBox();
			this.label17 = new Label();
			this.label7 = new Label();
			this.txtWork = new TextBox();
			this.cmdPayment = new Button();
			this.imageList1 = new ImageList(this.components);
			this.cmdSocial = new Button();
			this.groupBox3 = new GroupBox();
			this.tbDoc = new C1ToolBar();
			this.c1CommandLink2 = new C1CommandLink();
			this.c1Command1 = new C1Command();
			this.c1CommandLink1 = new C1CommandLink();
			this.c1Command2 = new C1Command();
			this.c1CommandLink3 = new C1CommandLink();
			this.c1Command3 = new C1Command();
			this.c1CommandLink5 = new C1CommandLink();
			this.c1Command4 = new C1Command();
			this.lvPhone = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.groupBox2 = new GroupBox();
			this.c1ToolBar1 = new C1ToolBar();
			this.c1CommandLink4 = new C1CommandLink();
			this.c1Command5 = new C1Command();
			this.c1CommandLink6 = new C1CommandLink();
			this.c1Command6 = new C1Command();
			this.c1CommandLink7 = new C1CommandLink();
			this.c1Command7 = new C1Command();
			this.c1CommandLink8 = new C1CommandLink();
			this.c1Command8 = new C1Command();
			this.lvContract = new ListView();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.groupBox1 = new GroupBox();
			this.cmdAddress = new Button();
			this.txtAddress = new TextBox();
			this.cmbPayment = new C1Combo();
			this.label6 = new Label();
			this.cmbSocial = new C1Combo();
			this.label5 = new Label();
			this.label4 = new Label();
			this.txtRNN = new TextBox();
			this.label3 = new Label();
			this.txtPatron = new TextBox();
			this.label2 = new Label();
			this.txtName = new TextBox();
			this.label1 = new Label();
			this.txtSurname = new TextBox();
			this.txtNUmberUDl = new TextBox();
			this.label8 = new Label();
			this.tabPage3 = new TabPage();
			this.list2 = new ListView();
			this.columnHeader16 = new ColumnHeader();
			this.columnHeader19 = new ColumnHeader();
			this.columnHeader20 = new ColumnHeader();
			this.tabPage2 = new TabPage();
			this.listView1 = new ListView();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader17 = new ColumnHeader();
			this.columnHeader18 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.tabPage4 = new TabPage();
			this.bBlockCabinet = new Button();
			this.lblState = new Label();
			this.label10 = new Label();
			this.txtEMail = new TextBox();
			this.label9 = new Label();
			this.bSavePassword = new Button();
			this.lblPassword = new Label();
			this.label11 = new Label();
			this.bGeneratePassword = new Button();
			this.cmdOk = new Button();
			this.cmdCancel = new Button();
			this.c1CommandHolder1 = new C1CommandHolder();
			this.cmdApply = new Button();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader12 = new ColumnHeader();
			this.columnHeader13 = new ColumnHeader();
			this.columnHeader14 = new ColumnHeader();
			this.columnHeader15 = new ColumnHeader();
			this.bDelPC = new Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmbPayment).BeginInit();
			((ISupportInitialize)this.cmbSocial).BeginInit();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((ISupportInitialize)this.c1CommandHolder1).BeginInit();
			base.SuspendLayout();
			this.tabControl1.Appearance = TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(656, 392);
			this.tabControl1.TabIndex = 0;
			this.tabPage1.Controls.Add(this.bCopyFIO);
			this.tabPage1.Controls.Add(this.txtPatronKZ);
			this.tabPage1.Controls.Add(this.txtNameKZ);
			this.tabPage1.Controls.Add(this.txtSurnameKZ);
			this.tabPage1.Controls.Add(this.txtMemo);
			this.tabPage1.Controls.Add(this.label17);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.txtWork);
			this.tabPage1.Controls.Add(this.cmdPayment);
			this.tabPage1.Controls.Add(this.cmdSocial);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.cmbPayment);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.cmbSocial);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.txtRNN);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.txtPatron);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.txtName);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.txtSurname);
			this.tabPage1.Controls.Add(this.txtNUmberUDl);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Location = new Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(648, 363);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Общие данные";
			this.bCopyFIO.Location = new Point(200, 9);
			this.bCopyFIO.Name = "bCopyFIO";
			this.bCopyFIO.Size = new System.Drawing.Size(16, 66);
			this.bCopyFIO.TabIndex = 48;
			this.bCopyFIO.Text = ">";
			this.bCopyFIO.Click += new EventHandler(this.bCopyFIO_Click);
			this.txtPatronKZ.BorderStyle = BorderStyle.FixedSingle;
			this.txtPatronKZ.Location = new Point(220, 56);
			this.txtPatronKZ.Name = "txtPatronKZ";
			this.txtPatronKZ.Size = new System.Drawing.Size(132, 20);
			this.txtPatronKZ.TabIndex = 47;
			this.txtPatronKZ.Text = "";
			this.txtNameKZ.BorderStyle = BorderStyle.FixedSingle;
			this.txtNameKZ.Location = new Point(220, 32);
			this.txtNameKZ.Name = "txtNameKZ";
			this.txtNameKZ.Size = new System.Drawing.Size(132, 20);
			this.txtNameKZ.TabIndex = 46;
			this.txtNameKZ.Text = "";
			this.txtSurnameKZ.BorderStyle = BorderStyle.FixedSingle;
			this.txtSurnameKZ.Location = new Point(220, 8);
			this.txtSurnameKZ.Name = "txtSurnameKZ";
			this.txtSurnameKZ.Size = new System.Drawing.Size(132, 20);
			this.txtSurnameKZ.TabIndex = 45;
			this.txtSurnameKZ.Text = "";
			this.txtMemo.BorderStyle = BorderStyle.FixedSingle;
			this.txtMemo.Location = new Point(128, 200);
			this.txtMemo.Multiline = true;
			this.txtMemo.Name = "txtMemo";
			this.txtMemo.Size = new System.Drawing.Size(520, 48);
			this.txtMemo.TabIndex = 43;
			this.txtMemo.Text = "";
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(24, 208);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 44;
			this.label17.Text = "Примечание";
			this.label7.Location = new Point(8, 128);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 16);
			this.label7.TabIndex = 12;
			this.label7.Text = "Место работы:";
			this.txtWork.BorderStyle = BorderStyle.FixedSingle;
			this.txtWork.Location = new Point(128, 128);
			this.txtWork.Name = "txtWork";
			this.txtWork.Size = new System.Drawing.Size(224, 20);
			this.txtWork.TabIndex = 11;
			this.txtWork.Text = "";
			this.cmdPayment.FlatStyle = FlatStyle.Flat;
			this.cmdPayment.ForeColor = SystemColors.ControlText;
			this.cmdPayment.ImageIndex = 4;
			this.cmdPayment.ImageList = this.imageList1;
			this.cmdPayment.Location = new Point(328, 176);
			this.cmdPayment.Name = "cmdPayment";
			this.cmdPayment.Size = new System.Drawing.Size(20, 20);
			this.cmdPayment.TabIndex = 7;
			this.cmdPayment.Click += new EventHandler(this.cmdPayment_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.cmdSocial.FlatStyle = FlatStyle.Flat;
			this.cmdSocial.ForeColor = SystemColors.ControlText;
			this.cmdSocial.ImageIndex = 4;
			this.cmdSocial.ImageList = this.imageList1;
			this.cmdSocial.Location = new Point(328, 152);
			this.cmdSocial.Name = "cmdSocial";
			this.cmdSocial.Size = new System.Drawing.Size(20, 20);
			this.cmdSocial.TabIndex = 5;
			this.cmdSocial.Click += new EventHandler(this.cmdSocial_Click);
			this.groupBox3.Controls.Add(this.tbDoc);
			this.groupBox3.Controls.Add(this.lvPhone);
			this.groupBox3.Location = new Point(360, 64);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(288, 128);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Телефоны";
			this.tbDoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tbDoc.Border.Width = 1;
			this.tbDoc.CommandHolder = null;
			C1CommandLinks commandLinks = this.tbDoc.CommandLinks;
			C1CommandLink[] c1CommandLinkArray = new C1CommandLink[] { this.c1CommandLink2, this.c1CommandLink1, this.c1CommandLink3, this.c1CommandLink5 };
			commandLinks.AddRange(c1CommandLinkArray);
			this.tbDoc.Location = new Point(8, 16);
			this.tbDoc.Movable = false;
			this.tbDoc.Name = "tbDoc";
			this.tbDoc.Size = new System.Drawing.Size(99, 24);
			this.tbDoc.Text = "c1ToolBar1";
			this.c1CommandLink2.Command = this.c1Command1;
			this.c1CommandLink2.Text = "Добавить";
			this.c1Command1.ImageIndex = 0;
			this.c1Command1.Name = "c1Command1";
			this.c1Command1.Text = "Добавить";
			this.c1Command1.Click += new ClickEventHandler(this.c1Command1_Click);
			this.c1CommandLink1.Command = this.c1Command2;
			this.c1CommandLink1.SortOrder = 1;
			this.c1CommandLink1.Text = "Изменить";
			this.c1Command2.ImageIndex = 1;
			this.c1Command2.Name = "c1Command2";
			this.c1Command2.Text = "Изменить";
			this.c1Command2.Click += new ClickEventHandler(this.c1Command2_Click);
			this.c1CommandLink3.Command = this.c1Command3;
			this.c1CommandLink3.SortOrder = 2;
			this.c1CommandLink3.Text = "Удалить";
			this.c1Command3.ImageIndex = 2;
			this.c1Command3.Name = "c1Command3";
			this.c1Command3.Text = "Удалить";
			this.c1Command3.Click += new ClickEventHandler(this.c1Command3_Click);
			this.c1CommandLink5.Command = this.c1Command4;
			this.c1CommandLink5.Delimiter = true;
			this.c1CommandLink5.SortOrder = 3;
			this.c1CommandLink5.Text = "Конвертировать в Excel";
			this.c1Command4.ImageIndex = 3;
			this.c1Command4.Name = "c1Command4";
			this.c1Command4.Text = "Конвертировать в Excel";
			this.c1Command4.Click += new ClickEventHandler(this.c1Command4_Click);
			this.lvPhone.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lvPhone.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1 };
			columns.AddRange(columnHeaderArray);
			this.lvPhone.FullRowSelect = true;
			this.lvPhone.GridLines = true;
			this.lvPhone.HeaderStyle = ColumnHeaderStyle.None;
			this.lvPhone.Location = new Point(8, 40);
			this.lvPhone.MultiSelect = false;
			this.lvPhone.Name = "lvPhone";
			this.lvPhone.Size = new System.Drawing.Size(272, 80);
			this.lvPhone.TabIndex = 1;
			this.lvPhone.View = View.Details;
			this.columnHeader1.Text = "Номер";
			this.columnHeader1.Width = 246;
			this.groupBox2.Controls.Add(this.c1ToolBar1);
			this.groupBox2.Controls.Add(this.lvContract);
			this.groupBox2.Location = new Point(0, 248);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(648, 112);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Договора";
			this.c1ToolBar1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.c1ToolBar1.Border.Width = 1;
			this.c1ToolBar1.CommandHolder = null;
			C1CommandLinks c1CommandLink = this.c1ToolBar1.CommandLinks;
			c1CommandLinkArray = new C1CommandLink[] { this.c1CommandLink4, this.c1CommandLink6, this.c1CommandLink7, this.c1CommandLink8 };
			c1CommandLink.AddRange(c1CommandLinkArray);
			this.c1ToolBar1.Location = new Point(8, 16);
			this.c1ToolBar1.Movable = false;
			this.c1ToolBar1.Name = "c1ToolBar1";
			this.c1ToolBar1.Size = new System.Drawing.Size(99, 24);
			this.c1ToolBar1.Text = "c1ToolBar1";
			this.c1CommandLink4.Command = this.c1Command5;
			this.c1Command5.ImageIndex = 0;
			this.c1Command5.Name = "c1Command5";
			this.c1Command5.Text = "Добавить";
			this.c1Command5.Click += new ClickEventHandler(this.c1Command5_Click);
			this.c1CommandLink6.Command = this.c1Command6;
			this.c1CommandLink6.SortOrder = 1;
			this.c1CommandLink6.Text = "Изменить";
			this.c1Command6.ImageIndex = 1;
			this.c1Command6.Name = "c1Command6";
			this.c1Command6.Text = "Изменить";
			this.c1Command6.Click += new ClickEventHandler(this.c1Command6_Click);
			this.c1CommandLink7.Command = this.c1Command7;
			this.c1CommandLink7.SortOrder = 2;
			this.c1CommandLink7.Text = "Удалить";
			this.c1Command7.ImageIndex = 2;
			this.c1Command7.Name = "c1Command7";
			this.c1Command7.Text = "Удалить";
			this.c1Command7.Click += new ClickEventHandler(this.c1Command7_Click);
			this.c1CommandLink8.Command = this.c1Command8;
			this.c1CommandLink8.Delimiter = true;
			this.c1CommandLink8.SortOrder = 3;
			this.c1CommandLink8.Text = "Конвертировать в Excel";
			this.c1Command8.ImageIndex = 3;
			this.c1Command8.Name = "c1Command8";
			this.c1Command8.Text = "Конвертировать в Excel";
			this.c1Command8.Click += new ClickEventHandler(this.c1Command8_Click);
			this.lvContract.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columnHeaderCollections = this.lvContract.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader6, this.columnHeader2, this.columnHeader3, this.columnHeader4 };
			columnHeaderCollections.AddRange(columnHeaderArray);
			this.lvContract.FullRowSelect = true;
			this.lvContract.GridLines = true;
			this.lvContract.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.lvContract.Location = new Point(8, 44);
			this.lvContract.MultiSelect = false;
			this.lvContract.Name = "lvContract";
			this.lvContract.Size = new System.Drawing.Size(632, 64);
			this.lvContract.TabIndex = 2;
			this.lvContract.View = View.Details;
			this.columnHeader6.Text = "Л/с";
			this.columnHeader6.Width = 84;
			this.columnHeader2.Text = "Статус";
			this.columnHeader2.Width = 103;
			this.columnHeader3.Text = "Адрес объекта";
			this.columnHeader3.Width = 243;
			this.columnHeader4.Text = "Сальдо";
			this.columnHeader4.Width = 176;
			this.groupBox1.Controls.Add(this.cmdAddress);
			this.groupBox1.Controls.Add(this.txtAddress);
			this.groupBox1.Location = new Point(360, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(288, 64);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Адрес";
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.ImageIndex = 4;
			this.cmdAddress.ImageList = this.imageList1;
			this.cmdAddress.Location = new Point(264, 16);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(20, 40);
			this.cmdAddress.TabIndex = 1;
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.txtAddress.BackColor = SystemColors.Info;
			this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
			this.txtAddress.Location = new Point(8, 16);
			this.txtAddress.Multiline = true;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(256, 40);
			this.txtAddress.TabIndex = 0;
			this.txtAddress.Text = "";
			this.cmbPayment.AddItemSeparator = ';';
			this.cmbPayment.BorderStyle = 1;
			this.cmbPayment.Caption = "";
			this.cmbPayment.CaptionHeight = 17;
			this.cmbPayment.CharacterCasing = 0;
			this.cmbPayment.ColumnCaptionHeight = 17;
			this.cmbPayment.ColumnFooterHeight = 17;
			this.cmbPayment.ColumnHeaders = false;
			this.cmbPayment.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbPayment.ContentHeight = 15;
			this.cmbPayment.DataMode = DataModeEnum.AddItem;
			this.cmbPayment.DeadAreaBackColor = Color.Empty;
			this.cmbPayment.EditorBackColor = SystemColors.Window;
			this.cmbPayment.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbPayment.EditorForeColor = SystemColors.WindowText;
			this.cmbPayment.EditorHeight = 15;
			this.cmbPayment.FlatStyle = FlatModeEnum.Flat;
			this.cmbPayment.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbPayment.ItemHeight = 15;
			this.cmbPayment.Location = new Point(128, 176);
			this.cmbPayment.MatchEntryTimeout = (long)2000;
			this.cmbPayment.MaxDropDownItems = 5;
			this.cmbPayment.MaxLength = 32767;
			this.cmbPayment.MouseCursor = Cursors.Default;
			this.cmbPayment.Name = "cmbPayment";
			this.cmbPayment.RowDivider.Color = Color.DarkGray;
			this.cmbPayment.RowDivider.Style = LineStyleEnum.None;
			this.cmbPayment.RowSubDividerColor = Color.DarkGray;
			this.cmbPayment.Size = new System.Drawing.Size(200, 19);
			this.cmbPayment.TabIndex = 6;
			this.cmbPayment.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label6.Location = new Point(8, 176);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 16);
			this.label6.TabIndex = 10;
			this.label6.Text = "Платежеспособность:";
			this.cmbSocial.AddItemSeparator = ';';
			this.cmbSocial.BorderStyle = 1;
			this.cmbSocial.Caption = "";
			this.cmbSocial.CaptionHeight = 17;
			this.cmbSocial.CharacterCasing = 0;
			this.cmbSocial.ColumnCaptionHeight = 17;
			this.cmbSocial.ColumnFooterHeight = 17;
			this.cmbSocial.ColumnHeaders = false;
			this.cmbSocial.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbSocial.ContentHeight = 15;
			this.cmbSocial.DataMode = DataModeEnum.AddItem;
			this.cmbSocial.DeadAreaBackColor = Color.Empty;
			this.cmbSocial.EditorBackColor = SystemColors.Window;
			this.cmbSocial.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbSocial.EditorForeColor = SystemColors.WindowText;
			this.cmbSocial.EditorHeight = 15;
			this.cmbSocial.FlatStyle = FlatModeEnum.Flat;
			this.cmbSocial.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbSocial.ItemHeight = 15;
			this.cmbSocial.Location = new Point(128, 152);
			this.cmbSocial.MatchEntryTimeout = (long)2000;
			this.cmbSocial.MaxDropDownItems = 5;
			this.cmbSocial.MaxLength = 32767;
			this.cmbSocial.MouseCursor = Cursors.Default;
			this.cmbSocial.Name = "cmbSocial";
			this.cmbSocial.RowDivider.Color = Color.DarkGray;
			this.cmbSocial.RowDivider.Style = LineStyleEnum.None;
			this.cmbSocial.RowSubDividerColor = Color.DarkGray;
			this.cmbSocial.Size = new System.Drawing.Size(200, 19);
			this.cmbSocial.TabIndex = 4;
			this.cmbSocial.KeyPress += new KeyPressEventHandler(this.cmbSocial_KeyPress);
			this.cmbSocial.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label5.Location = new Point(8, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "Соц. положение:";
			this.label4.Location = new Point(8, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "ИИН:";
			this.txtRNN.BorderStyle = BorderStyle.FixedSingle;
			this.txtRNN.Location = new Point(128, 80);
			this.txtRNN.Name = "txtRNN";
			this.txtRNN.Size = new System.Drawing.Size(224, 20);
			this.txtRNN.TabIndex = 3;
			this.txtRNN.Text = "";
			this.txtRNN.KeyPress += new KeyPressEventHandler(this.txtRNN_KeyPress);
			this.label3.Location = new Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Отчество:";
			this.txtPatron.BorderStyle = BorderStyle.FixedSingle;
			this.txtPatron.Location = new Point(64, 56);
			this.txtPatron.Name = "txtPatron";
			this.txtPatron.Size = new System.Drawing.Size(132, 20);
			this.txtPatron.TabIndex = 2;
			this.txtPatron.Text = "";
			this.txtPatron.KeyPress += new KeyPressEventHandler(this.txtPatron_KeyPress);
			this.label2.Location = new Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Имя:";
			this.txtName.BorderStyle = BorderStyle.FixedSingle;
			this.txtName.Location = new Point(64, 32);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(132, 20);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			this.txtName.KeyPress += new KeyPressEventHandler(this.txtName_KeyPress);
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Фамилия:";
			this.txtSurname.BorderStyle = BorderStyle.FixedSingle;
			this.txtSurname.Location = new Point(64, 8);
			this.txtSurname.Name = "txtSurname";
			this.txtSurname.Size = new System.Drawing.Size(132, 20);
			this.txtSurname.TabIndex = 0;
			this.txtSurname.Text = "";
			this.txtSurname.KeyPress += new KeyPressEventHandler(this.txtSurname_KeyPress);
			this.txtNUmberUDl.BorderStyle = BorderStyle.FixedSingle;
			this.txtNUmberUDl.Location = new Point(128, 104);
			this.txtNUmberUDl.Name = "txtNUmberUDl";
			this.txtNUmberUDl.Size = new System.Drawing.Size(224, 20);
			this.txtNUmberUDl.TabIndex = 3;
			this.txtNUmberUDl.Text = "";
			this.label8.Location = new Point(8, 104);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88, 16);
			this.label8.TabIndex = 7;
			this.label8.Text = "№ уд. лич.:";
			this.tabPage3.Controls.Add(this.list2);
			this.tabPage3.Location = new Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(648, 363);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "История мест работы";
			ListView.ColumnHeaderCollection columns1 = this.list2.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader16, this.columnHeader19, this.columnHeader20 };
			columns1.AddRange(columnHeaderArray);
			this.list2.GridLines = true;
			this.list2.Location = new Point(0, 0);
			this.list2.Name = "list2";
			this.list2.Size = new System.Drawing.Size(644, 352);
			this.list2.TabIndex = 1;
			this.list2.View = View.Details;
			this.columnHeader16.Text = "Место работы";
			this.columnHeader16.Width = 120;
			this.columnHeader19.Text = "по дату";
			this.columnHeader19.Width = 120;
			this.columnHeader20.Text = "Кто вводил";
			this.columnHeader20.Width = 120;
			this.tabPage2.Controls.Add(this.listView1);
			this.tabPage2.Location = new Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(648, 363);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "История ФИО";
			ListView.ColumnHeaderCollection columnHeaderCollections1 = this.listView1.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader5, this.columnHeader7, this.columnHeader8, this.columnHeader17, this.columnHeader18, this.columnHeader9, this.columnHeader10 };
			columnHeaderCollections1.AddRange(columnHeaderArray);
			this.listView1.GridLines = true;
			this.listView1.Location = new Point(4, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(644, 284);
			this.listView1.TabIndex = 0;
			this.listView1.View = View.Details;
			this.columnHeader5.Text = "Фамилия";
			this.columnHeader5.Width = 120;
			this.columnHeader7.Text = "Имя";
			this.columnHeader7.Width = 120;
			this.columnHeader8.Text = "Отчество";
			this.columnHeader8.Width = 120;
			this.columnHeader17.Text = "РНН";
			this.columnHeader18.Text = "Уд. лич.";
			this.columnHeader9.Text = "по дату";
			this.columnHeader9.Width = 120;
			this.columnHeader10.Text = "Кто вводил";
			this.columnHeader10.Width = 120;
			this.tabPage4.Controls.Add(this.bDelPC);
			this.tabPage4.Controls.Add(this.bBlockCabinet);
			this.tabPage4.Controls.Add(this.lblState);
			this.tabPage4.Controls.Add(this.label10);
			this.tabPage4.Controls.Add(this.txtEMail);
			this.tabPage4.Controls.Add(this.label9);
			this.tabPage4.Controls.Add(this.bSavePassword);
			this.tabPage4.Controls.Add(this.lblPassword);
			this.tabPage4.Controls.Add(this.label11);
			this.tabPage4.Controls.Add(this.bGeneratePassword);
			this.tabPage4.Location = new Point(4, 25);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(648, 363);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Личный кабинет";
			this.bBlockCabinet.ForeColor = Color.Red;
			this.bBlockCabinet.Location = new Point(456, 40);
			this.bBlockCabinet.Name = "bBlockCabinet";
			this.bBlockCabinet.Size = new System.Drawing.Size(184, 23);
			this.bBlockCabinet.TabIndex = 12;
			this.bBlockCabinet.Text = "Заблокировать";
			this.bBlockCabinet.Click += new EventHandler(this.bBlockCabinet_Click);
			this.lblState.BorderStyle = BorderStyle.FixedSingle;
			this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblState.Location = new Point(120, 8);
			this.lblState.Name = "lblState";
			this.lblState.Size = new System.Drawing.Size(320, 23);
			this.lblState.TabIndex = 11;
			this.lblState.TextAlign = ContentAlignment.MiddleCenter;
			this.label10.Location = new Point(8, 11);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(112, 16);
			this.label10.TabIndex = 10;
			this.label10.Text = "Текущее состояние:";
			this.txtEMail.Location = new Point(120, 72);
			this.txtEMail.Name = "txtEMail";
			this.txtEMail.Size = new System.Drawing.Size(320, 20);
			this.txtEMail.TabIndex = 9;
			this.txtEMail.Text = "";
			this.label9.Location = new Point(8, 72);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 16);
			this.label9.TabIndex = 8;
			this.label9.Text = "E-mail:";
			this.bSavePassword.Enabled = false;
			this.bSavePassword.Location = new Point(456, 72);
			this.bSavePassword.Name = "bSavePassword";
			this.bSavePassword.Size = new System.Drawing.Size(184, 23);
			this.bSavePassword.TabIndex = 7;
			this.bSavePassword.Text = "Сохранить";
			this.bSavePassword.Click += new EventHandler(this.bSavePassword_Click);
			this.lblPassword.BorderStyle = BorderStyle.FixedSingle;
			this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblPassword.Location = new Point(120, 40);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(320, 23);
			this.lblPassword.TabIndex = 6;
			this.lblPassword.TextAlign = ContentAlignment.MiddleCenter;
			this.label11.Location = new Point(8, 40);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 16);
			this.label11.TabIndex = 5;
			this.label11.Text = "Пароль:";
			this.bGeneratePassword.Location = new Point(456, 8);
			this.bGeneratePassword.Name = "bGeneratePassword";
			this.bGeneratePassword.Size = new System.Drawing.Size(184, 23);
			this.bGeneratePassword.TabIndex = 4;
			this.bGeneratePassword.Text = "Сгенерировать пароль";
			this.bGeneratePassword.Click += new EventHandler(this.bGeneratePassword_Click);
			this.cmdOk.FlatStyle = FlatStyle.Flat;
			this.cmdOk.Location = new Point(296, 392);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(116, 24);
			this.cmdOk.TabIndex = 11;
			this.cmdOk.Text = "Ok";
			this.cmdOk.Click += new EventHandler(this.cmdOk_Click);
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(536, 392);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(116, 24);
			this.cmdCancel.TabIndex = 13;
			this.cmdCancel.Text = "Закрыть";
			this.c1CommandHolder1.Commands.Add(this.c1Command1);
			this.c1CommandHolder1.Commands.Add(this.c1Command2);
			this.c1CommandHolder1.Commands.Add(this.c1Command3);
			this.c1CommandHolder1.Commands.Add(this.c1Command4);
			this.c1CommandHolder1.Commands.Add(this.c1Command5);
			this.c1CommandHolder1.Commands.Add(this.c1Command6);
			this.c1CommandHolder1.Commands.Add(this.c1Command7);
			this.c1CommandHolder1.Commands.Add(this.c1Command8);
			this.c1CommandHolder1.ImageList = this.imageList1;
			this.c1CommandHolder1.Owner = this;
			this.cmdApply.FlatStyle = FlatStyle.Flat;
			this.cmdApply.Location = new Point(416, 392);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(116, 24);
			this.cmdApply.TabIndex = 12;
			this.cmdApply.Text = "Применить";
			this.cmdApply.Click += new EventHandler(this.cmdApply_Click);
			this.columnHeader11.Text = "Фамилия";
			this.columnHeader11.Width = 120;
			this.columnHeader12.Text = "Имя";
			this.columnHeader12.Width = 120;
			this.columnHeader13.Text = "Отчество";
			this.columnHeader13.Width = 120;
			this.columnHeader14.Text = "по дату";
			this.columnHeader14.Width = 120;
			this.columnHeader15.Text = "Кто вводил";
			this.columnHeader15.Width = 120;
			this.bDelPC.ForeColor = Color.Red;
			this.bDelPC.Location = new Point(456, 224);
			this.bDelPC.Name = "bDelPC";
			this.bDelPC.Size = new System.Drawing.Size(184, 23);
			this.bDelPC.TabIndex = 13;
			this.bDelPC.Text = "Аннулировать";
			this.bDelPC.Visible = false;
			this.bDelPC.Click += new EventHandler(this.bDelPC_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new System.Drawing.Size(658, 418);
			base.Controls.Add(this.cmdApply);
			base.Controls.Add(this.cmdOk);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmPerson";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Потребитель физическое лицо";
			base.Closing += new CancelEventHandler(this.frmPerson_Closing);
			base.Load += new EventHandler(this.frmPerson_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmbPayment).EndInit();
			((ISupportInitialize)this.cmbSocial).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			((ISupportInitialize)this.c1CommandHolder1).EndInit();
			base.ResumeLayout(false);
		}

		private bool Save()
		{
			bool flag = false;
			try
			{
				long d = (long)-1;
				long num = (long)-1;
				if (this._person.oOwnership != null)
				{
					d = this._person.oOwnership.get_ID();
				}
				if (this._person.oOwnership != null)
				{
					num = this._person.oOwnership.get_ID() - (long)1;
				}
				if (this._person.WorkPlace != this.txtWork.Text || this._person.Memo != this.txtMemo.Text || this._person.Surname != this.txtSurname.Text || this._person.Name != this.txtName.Text || this._person.Patronic != this.txtPatron.Text || this._person.RNN != this.txtRNN.Text || this._person.WorkPlace != this.txtWork.Text || this._person.isJuridical != 0 || d != this._ownerships[this.cmbSocial.SelectedIndex].get_ID() || num != this._classifiers[this.cmbPayment.SelectedIndex].get_ID() || this._person.NumberUDL != this.txtNUmberUDl.Text)
				{
					this._person.Surname = this.txtSurname.Text;
					this._person.Name = this.txtName.Text;
					this._person.Patronic = this.txtPatron.Text;
					this._person.RNN = this.txtRNN.Text;
					this._person.NumberUDL = this.txtNUmberUDl.Text;
					this._person.Memo = this.txtMemo.Text;
					this._person.WorkPlace = this.txtWork.Text;
					this._person.oOwnership = this._ownerships[this.cmbSocial.SelectedIndex];
					this._person.oClassifier = this._classifiers[this.cmbPayment.SelectedIndex];
					this._person.oAddress = this._address;
					this._person.isJuridical = 0;
					flag = this._person.Save() <= 0;
				}
				else
				{
					flag = true;
				}
				flag = (this._person.SurnameKZ != this.txtSurnameKZ.Text || this._person.NameKZ != this.txtNameKZ.Text || this._person.PatronicKZ != this.txtPatronKZ.Text ? this._person.SaveKZPersonName(this._person.get_ID(), this.txtSurnameKZ.Text, this.txtNameKZ.Text, this.txtPatronKZ.Text) : true);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private void SetStrStateCabinet(int state)
		{
			if (state == 1)
			{
				this.lblState.Text = "Активен";
			}
			if (state == 2)
			{
				this.lblState.Text = "Заблокирован";
			}
		}

		private void txtName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtPatron.Focus();
			}
		}

		private void txtPatron_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtRNN.Focus();
			}
		}

		private void txtRNN_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbSocial.Focus();
			}
		}

		private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtName.Focus();
			}
		}
	}
}