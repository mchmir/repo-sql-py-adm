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
	public class frmJPerson : Form
	{
		private TabControl tabControl1;

		private TabPage tabPage1;

		private TextBox txtSurname;

		private Label label1;

		private Label label2;

		private TextBox txtName;

		private TextBox txtPatron;

		private Label label4;

		private TextBox txtRNN;

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

		private C1Command c1Command5;

		private C1Command c1Command6;

		private C1Command c1Command7;

		private C1Command c1Command8;

		private C1Combo cmbOwnership;

		private Label label3;

		private Label label5;

		private TextBox textMainBuch;

		private C1Combo cmbClassifier;

		private TabPage tabPage2;

		private ListView listView1;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private TextBox txtMemo;

		private Label label17;

		private TabPage tabPage3;

		private GroupBox groupBox4;

		private TextBox txtCost;

		private Label label9;

		private DateTimePicker dtDateDog;

		private Label label8;

		private Label label7;

		private TextBox txtNumberDog;

		private GroupBox groupBox5;

		private Label label10;

		private ComboBox cmbYearTO;

		private GroupBox groupBox6;

		private CheckBox chkJan;

		private TextBox txtJanAmount;

		private TextBox txtFebAmount;

		private TextBox txtMarAmount;

		private TextBox txtJunAmount;

		private TextBox txtMayAmount;

		private TextBox txtAprAmount;

		private TextBox txtSepAmount;

		private TextBox txtAugAmount;

		private TextBox txtJulAmount;

		private TextBox txtOctAmount;

		private TextBox txtNovAmount;

		private TextBox txtDecAmount;

		private CheckBox chkFeb;

		private CheckBox chkMar;

		private CheckBox chkJun;

		private CheckBox chkMay;

		private CheckBox chkApr;

		private CheckBox chkSep;

		private CheckBox chkAug;

		private CheckBox chkJul;

		private CheckBox chkOct;

		private CheckBox chkNov;

		private CheckBox chkDec;

		private Button bSaveGrafikTO;

		private Address _address;

		private Button bClearGrafikTO;

		private ColumnHeader columnHeader10;

		private TabPage tabPage4;

		private Button bGeneratePassword;

		private Label label11;

		private Label lblPassword;

		private Button bSavePassword;

		private GrafikTOs _grafiktos;

		private TextBox txtEMail;

		private Label label12;

		private Contract _contract;

		private Button bBlockCabinet;

		private Label lblState;

		private Label label13;

		private PersonalCabinet _personalcabinet;

		public frmJPerson(Person oPerson)
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

		private void bClearGrafikTO_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вы действительно хотите удалить график ТО?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				try
				{
					this.ClearGrafikTO();
					foreach (GrafikTO _grafikto in this._grafiktos)
					{
						_grafikto.Delete();
					}
					this._grafiktos = new GrafikTOs();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
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

		private void bSaveGrafikTO_Click(object sender, EventArgs e)
		{
			try
			{
				GrafikTO grafikTO = new GrafikTO();
				if (this.chkJan.Checked && this.txtJanAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(1);
					grafikTO.Amount = Convert.ToDouble(this.txtJanAmount.Text);
					grafikTO.Save();
				}
				if (this.chkFeb.Checked && this.txtFebAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(2);
					grafikTO.Amount = Convert.ToDouble(this.txtFebAmount.Text);
					grafikTO.Save();
				}
				if (this.chkMar.Checked && this.txtMarAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(3);
					grafikTO.Amount = Convert.ToDouble(this.txtMarAmount.Text);
					grafikTO.Save();
				}
				if (this.chkApr.Checked && this.txtAprAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(4);
					grafikTO.Amount = Convert.ToDouble(this.txtAprAmount.Text);
					grafikTO.Save();
				}
				if (this.chkMay.Checked && this.txtMayAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(5);
					grafikTO.Amount = Convert.ToDouble(this.txtMayAmount.Text);
					grafikTO.Save();
				}
				if (this.chkJun.Checked && this.txtJunAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(6);
					grafikTO.Amount = Convert.ToDouble(this.txtJunAmount.Text);
					grafikTO.Save();
				}
				if (this.chkJul.Checked && this.txtJulAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(7);
					grafikTO.Amount = Convert.ToDouble(this.txtJulAmount.Text);
					grafikTO.Save();
				}
				if (this.chkAug.Checked && this.txtAugAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(8);
					grafikTO.Amount = Convert.ToDouble(this.txtAugAmount.Text);
					grafikTO.Save();
				}
				if (this.chkSep.Checked && this.txtSepAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(9);
					grafikTO.Amount = Convert.ToDouble(this.txtSepAmount.Text);
					grafikTO.Save();
				}
				if (this.chkOct.Checked && this.txtOctAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(10);
					grafikTO.Amount = Convert.ToDouble(this.txtOctAmount.Text);
					grafikTO.Save();
				}
				if (this.chkNov.Checked && this.txtNovAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(11);
					grafikTO.Amount = Convert.ToDouble(this.txtNovAmount.Text);
					grafikTO.Save();
				}
				if (this.chkDec.Checked && this.txtDecAmount.Text.Length > 0)
				{
					grafikTO = new GrafikTO();
					grafikTO = this.ExistGTO(12);
					grafikTO.Amount = Convert.ToDouble(this.txtDecAmount.Text);
					grafikTO.Save();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
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

		private void chkApr_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkApr.Checked)
			{
				this.txtAprAmount.Text = "";
				return;
			}
			this.txtAprAmount.Text = this.txtCost.Text;
		}

		private void chkAug_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkAug.Checked)
			{
				this.txtAugAmount.Text = "";
				return;
			}
			this.txtAugAmount.Text = this.txtCost.Text;
		}

		private void chkDec_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkDec.Checked)
			{
				this.txtDecAmount.Text = "";
				return;
			}
			this.txtDecAmount.Text = this.txtCost.Text;
		}

		private void chkFeb_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkFeb.Checked)
			{
				this.txtFebAmount.Text = "";
				return;
			}
			this.txtFebAmount.Text = this.txtCost.Text;
		}

		private void chkJan_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkJan.Checked)
			{
				this.txtJanAmount.Text = "";
				return;
			}
			this.txtJanAmount.Text = this.txtCost.Text;
		}

		private void chkJul_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkJul.Checked)
			{
				this.txtJulAmount.Text = "";
				return;
			}
			this.txtJulAmount.Text = this.txtCost.Text;
		}

		private void chkJun_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkJun.Checked)
			{
				this.txtJunAmount.Text = "";
				return;
			}
			this.txtJunAmount.Text = this.txtCost.Text;
		}

		private void chkMar_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkMar.Checked)
			{
				this.txtMarAmount.Text = "";
				return;
			}
			this.txtMarAmount.Text = this.txtCost.Text;
		}

		private void chkMay_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkMay.Checked)
			{
				this.txtMayAmount.Text = "";
				return;
			}
			this.txtMayAmount.Text = this.txtCost.Text;
		}

		private void chkNov_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkNov.Checked)
			{
				this.txtNovAmount.Text = "";
				return;
			}
			this.txtNovAmount.Text = this.txtCost.Text;
		}

		private void chkOct_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkOct.Checked)
			{
				this.txtOctAmount.Text = "";
				return;
			}
			this.txtOctAmount.Text = this.txtCost.Text;
		}

		private void chkSep_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkSep.Checked)
			{
				this.txtSepAmount.Text = "";
				return;
			}
			this.txtSepAmount.Text = this.txtCost.Text;
		}

		private void ClearGrafikTO()
		{
			this.chkJan.Checked = false;
			this.chkJan.Enabled = true;
			this.txtJanAmount.Text = "";
			this.chkFeb.Checked = false;
			this.chkFeb.Enabled = true;
			this.txtFebAmount.Text = "";
			this.chkMar.Checked = false;
			this.chkMar.Enabled = true;
			this.txtMarAmount.Text = "";
			this.chkApr.Checked = false;
			this.chkApr.Enabled = true;
			this.txtAprAmount.Text = "";
			this.chkMay.Checked = false;
			this.chkMay.Enabled = true;
			this.txtMayAmount.Text = "";
			this.chkJun.Checked = false;
			this.chkJun.Enabled = true;
			this.txtJunAmount.Text = "";
			this.chkJul.Checked = false;
			this.chkJul.Enabled = true;
			this.txtJulAmount.Text = "";
			this.chkAug.Checked = false;
			this.chkAug.Enabled = true;
			this.txtAugAmount.Text = "";
			this.chkSep.Checked = false;
			this.chkSep.Enabled = true;
			this.txtSepAmount.Text = "";
			this.chkOct.Checked = false;
			this.chkOct.Enabled = true;
			this.txtOctAmount.Text = "";
			this.chkNov.Checked = false;
			this.chkNov.Enabled = true;
			this.txtNovAmount.Text = "";
			this.chkDec.Checked = false;
			this.chkDec.Enabled = true;
			this.txtDecAmount.Text = "";
		}

		private void cmbYearTO_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CreateGrafikTO();
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
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Классификатор", classifier, strArrays1, numArray, strArrays, typeArray);
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
			Tools.FillC1(this._classifiers, this.cmbClassifier, d);
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
				this.txtEMail.Text = this._personalcabinet.PCEmail;
			}
			if (this.txtEMail.Text.Length == 0)
			{
				this.txtEMail.Text = this._contract.PCEmail;
			}
		}

		private void CreateGrafikTO()
		{
			try
			{
				this.ClearGrafikTO();
				this._grafiktos = new GrafikTOs();
				this._grafiktos.Load(this._person.oContracts[0], Convert.ToInt32(this.cmbYearTO.SelectedIndex + 2013));
				foreach (GrafikTO _grafikto in this._grafiktos)
				{
					switch (Convert.ToInt32(_grafikto.Month))
					{
						case 1:
						{
							this.chkJan.Checked = true;
							this.chkJan.Enabled = false;
							this.txtJanAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 2:
						{
							this.chkFeb.Checked = true;
							this.chkFeb.Enabled = false;
							this.txtFebAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 3:
						{
							this.chkMar.Checked = true;
							this.chkMar.Enabled = false;
							this.txtMarAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 4:
						{
							this.chkApr.Checked = true;
							this.chkApr.Enabled = false;
							this.txtAprAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 5:
						{
							this.chkMay.Checked = true;
							this.chkMay.Enabled = false;
							this.txtMayAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 6:
						{
							this.chkJun.Checked = true;
							this.chkJun.Enabled = false;
							this.txtJunAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 7:
						{
							this.chkJul.Checked = true;
							this.chkJul.Enabled = false;
							this.txtJulAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 8:
						{
							this.chkAug.Checked = true;
							this.chkAug.Enabled = false;
							this.txtAugAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 9:
						{
							this.chkSep.Checked = true;
							this.chkSep.Enabled = false;
							this.txtSepAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 10:
						{
							this.chkOct.Checked = true;
							this.chkOct.Enabled = false;
							this.txtOctAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 11:
						{
							this.chkNov.Checked = true;
							this.chkNov.Enabled = false;
							this.txtNovAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						case 12:
						{
							this.chkDec.Checked = true;
							this.chkDec.Enabled = false;
							this.txtDecAmount.Text = _grafikto.Amount.ToString();
							continue;
						}
						default:
						{
							continue;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void CreateHistory()
		{
			ListViewItem listViewItem;
			List list = new List();
			long d = this._person.get_ID();
			string str = string.Concat("select distinct datevalues,dbo.fGetFIOPerson (datevalues,idobject, 'Surname'),dbo.fGetFIOPerson (datevalues,idobject, 'name'),dbo.fGetFIOPerson (datevalues,idobject, 'Patronic'),dbo.fGetFIOPerson (datevalues,idobject, 'RNN') from oldvalues where idobject=", d.ToString(), " and nametable='Person' and namecolumn<>'IsJuridical' and datevalues>='1900-01-01'");
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
				listViewItem.SubItems.Add(mylistPr[0]);
			}
			listViewItem = this.listView1.Items.Add(this._person.Surname);
			listViewItem.SubItems.Add(this._person.Name);
			listViewItem.SubItems.Add(this._person.Patronic);
			listViewItem.SubItems.Add(this._person.RNN);
			listViewItem.SubItems.Add("текущие");
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
			Tools.FillC1(this._ownerships, this.cmbOwnership, d);
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

		private GrafikTO ExistGTO(int month)
		{
			GrafikTO grafikTO;
			GrafikTO num = new GrafikTO();
			IEnumerator enumerator = this._grafiktos.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					GrafikTO current = (GrafikTO)enumerator.Current;
					if (current.Month != month || current.Year != this.cmbYearTO.SelectedIndex + 2013)
					{
						continue;
					}
					num = current;
					grafikTO = num;
					return grafikTO;
				}
				Contract item = this._person.oContracts[0];
				num.IDContract = Convert.ToInt32(this._person.oContracts[0].get_ID());
				num.Month = month;
				num.Year = this.cmbYearTO.SelectedIndex + 2013;
				return num;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return grafikTO;
		}

		private void frmJPerson_Closing(object sender, CancelEventArgs e)
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
			this.txtRNN.Text = this._person.RNN;
			this.txtMemo.Text = this._person.Memo;
			if (this._person.NumberDog.Length <= 0)
			{
				this.dtDateDog.Value = DateTime.Today;
				this.txtCost.Text = "0";
			}
			else
			{
				this.txtNumberDog.Text = this._person.NumberDog;
				this.dtDateDog.Value = this._person.DateDog;
				this.txtCost.Text = Convert.ToString(this._person.CostDog);
			}
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
			int count = 0;
			for (int i = 2013; i <= DateTime.Now.Year + 1; i++)
			{
				this.cmbYearTO.Items.Add(i.ToString());
				if (i == DateTime.Now.Year)
				{
					count = this.cmbYearTO.Items.Count - 1;
				}
			}
			this.cmbYearTO.SelectedIndex = count;
			this.CreateGrafikTO();
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
			ResourceManager resourceManager = new ResourceManager(typeof(frmJPerson));
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.txtMemo = new TextBox();
			this.label17 = new Label();
			this.textMainBuch = new TextBox();
			this.label5 = new Label();
			this.label3 = new Label();
			this.cmdPayment = new Button();
			this.imageList1 = new ImageList(this.components);
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
			this.cmbClassifier = new C1Combo();
			this.label6 = new Label();
			this.cmbOwnership = new C1Combo();
			this.label4 = new Label();
			this.txtRNN = new TextBox();
			this.txtPatron = new TextBox();
			this.label2 = new Label();
			this.txtName = new TextBox();
			this.label1 = new Label();
			this.txtSurname = new TextBox();
			this.tabPage3 = new TabPage();
			this.groupBox5 = new GroupBox();
			this.bClearGrafikTO = new Button();
			this.groupBox6 = new GroupBox();
			this.txtDecAmount = new TextBox();
			this.chkDec = new CheckBox();
			this.txtNovAmount = new TextBox();
			this.chkNov = new CheckBox();
			this.txtOctAmount = new TextBox();
			this.chkOct = new CheckBox();
			this.txtSepAmount = new TextBox();
			this.chkSep = new CheckBox();
			this.txtAugAmount = new TextBox();
			this.chkAug = new CheckBox();
			this.txtJulAmount = new TextBox();
			this.chkJul = new CheckBox();
			this.txtJunAmount = new TextBox();
			this.chkJun = new CheckBox();
			this.txtMayAmount = new TextBox();
			this.chkMay = new CheckBox();
			this.txtAprAmount = new TextBox();
			this.chkApr = new CheckBox();
			this.txtMarAmount = new TextBox();
			this.chkMar = new CheckBox();
			this.txtFebAmount = new TextBox();
			this.chkFeb = new CheckBox();
			this.txtJanAmount = new TextBox();
			this.chkJan = new CheckBox();
			this.cmbYearTO = new ComboBox();
			this.label10 = new Label();
			this.bSaveGrafikTO = new Button();
			this.groupBox4 = new GroupBox();
			this.txtCost = new TextBox();
			this.label9 = new Label();
			this.dtDateDog = new DateTimePicker();
			this.label8 = new Label();
			this.label7 = new Label();
			this.txtNumberDog = new TextBox();
			this.tabPage2 = new TabPage();
			this.listView1 = new ListView();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.tabPage4 = new TabPage();
			this.label12 = new Label();
			this.txtEMail = new TextBox();
			this.bSavePassword = new Button();
			this.lblPassword = new Label();
			this.label11 = new Label();
			this.bGeneratePassword = new Button();
			this.cmdOk = new Button();
			this.cmdCancel = new Button();
			this.c1CommandHolder1 = new C1CommandHolder();
			this.cmdApply = new Button();
			this.bBlockCabinet = new Button();
			this.lblState = new Label();
			this.label13 = new Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmbClassifier).BeginInit();
			((ISupportInitialize)this.cmbOwnership).BeginInit();
			this.tabPage3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox4.SuspendLayout();
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
			this.tabControl1.Size = new System.Drawing.Size(656, 390);
			this.tabControl1.TabIndex = 0;
			this.tabPage1.Controls.Add(this.txtMemo);
			this.tabPage1.Controls.Add(this.label17);
			this.tabPage1.Controls.Add(this.textMainBuch);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.cmdPayment);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.cmbClassifier);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.cmbOwnership);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.txtRNN);
			this.tabPage1.Controls.Add(this.txtPatron);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.txtName);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.txtSurname);
			this.tabPage1.Location = new Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(648, 361);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Общие данные";
			this.txtMemo.BorderStyle = BorderStyle.FixedSingle;
			this.txtMemo.Location = new Point(128, 160);
			this.txtMemo.Multiline = true;
			this.txtMemo.Name = "txtMemo";
			this.txtMemo.Size = new System.Drawing.Size(520, 64);
			this.txtMemo.TabIndex = 41;
			this.txtMemo.Text = "";
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 152);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 42;
			this.label17.Text = "Примечание";
			this.textMainBuch.BorderStyle = BorderStyle.FixedSingle;
			this.textMainBuch.Location = new Point(128, 104);
			this.textMainBuch.Name = "textMainBuch";
			this.textMainBuch.Size = new System.Drawing.Size(224, 20);
			this.textMainBuch.TabIndex = 19;
			this.textMainBuch.Text = "";
			this.label5.Location = new Point(8, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 16);
			this.label5.TabIndex = 18;
			this.label5.Text = "ФИО Гл.бухгалтера:";
			this.label3.Location = new Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 16);
			this.label3.TabIndex = 17;
			this.label3.Text = "ФИО Руководителя:";
			this.cmdPayment.FlatStyle = FlatStyle.Flat;
			this.cmdPayment.ForeColor = SystemColors.ControlText;
			this.cmdPayment.ImageIndex = 4;
			this.cmdPayment.ImageList = this.imageList1;
			this.cmdPayment.Location = new Point(328, 128);
			this.cmdPayment.Name = "cmdPayment";
			this.cmdPayment.Size = new System.Drawing.Size(20, 20);
			this.cmdPayment.TabIndex = 16;
			this.cmdPayment.Click += new EventHandler(this.cmdPayment_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.groupBox3.Controls.Add(this.tbDoc);
			this.groupBox3.Controls.Add(this.lvPhone);
			this.groupBox3.Location = new Point(360, 64);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(288, 88);
			this.groupBox3.TabIndex = 14;
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
			this.lvPhone.Size = new System.Drawing.Size(272, 40);
			this.lvPhone.TabIndex = 1;
			this.lvPhone.View = View.Details;
			this.columnHeader1.Text = "Номер";
			this.columnHeader1.Width = 246;
			this.groupBox2.Controls.Add(this.c1ToolBar1);
			this.groupBox2.Controls.Add(this.lvContract);
			this.groupBox2.Location = new Point(0, 232);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(648, 128);
			this.groupBox2.TabIndex = 13;
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
			this.lvContract.Location = new Point(8, 40);
			this.lvContract.MultiSelect = false;
			this.lvContract.Name = "lvContract";
			this.lvContract.Size = new System.Drawing.Size(632, 80);
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
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Адрес";
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.ImageIndex = 4;
			this.cmdAddress.ImageList = this.imageList1;
			this.cmdAddress.Location = new Point(264, 16);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(20, 40);
			this.cmdAddress.TabIndex = 11;
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.txtAddress.BackColor = SystemColors.Info;
			this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
			this.txtAddress.Location = new Point(8, 16);
			this.txtAddress.Multiline = true;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(256, 40);
			this.txtAddress.TabIndex = 0;
			this.txtAddress.Text = "";
			this.cmbClassifier.AddItemSeparator = ';';
			this.cmbClassifier.BorderStyle = 1;
			this.cmbClassifier.Caption = "";
			this.cmbClassifier.CaptionHeight = 17;
			this.cmbClassifier.CharacterCasing = 0;
			this.cmbClassifier.ColumnCaptionHeight = 17;
			this.cmbClassifier.ColumnFooterHeight = 17;
			this.cmbClassifier.ColumnHeaders = false;
			this.cmbClassifier.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbClassifier.ContentHeight = 15;
			this.cmbClassifier.DataMode = DataModeEnum.AddItem;
			this.cmbClassifier.DeadAreaBackColor = Color.Empty;
			this.cmbClassifier.EditorBackColor = SystemColors.Window;
			this.cmbClassifier.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbClassifier.EditorForeColor = SystemColors.WindowText;
			this.cmbClassifier.EditorHeight = 15;
			this.cmbClassifier.FlatStyle = FlatModeEnum.Flat;
			this.cmbClassifier.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbClassifier.ItemHeight = 15;
			this.cmbClassifier.Location = new Point(128, 128);
			this.cmbClassifier.MatchEntryTimeout = (long)2000;
			this.cmbClassifier.MaxDropDownItems = 5;
			this.cmbClassifier.MaxLength = 32767;
			this.cmbClassifier.MouseCursor = Cursors.Default;
			this.cmbClassifier.Name = "cmbClassifier";
			this.cmbClassifier.RowDivider.Color = Color.DarkGray;
			this.cmbClassifier.RowDivider.Style = LineStyleEnum.None;
			this.cmbClassifier.RowSubDividerColor = Color.DarkGray;
			this.cmbClassifier.Size = new System.Drawing.Size(200, 19);
			this.cmbClassifier.TabIndex = 11;
			this.cmbClassifier.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label6.Location = new Point(8, 128);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 16);
			this.label6.TabIndex = 10;
			this.label6.Text = "Классификатор:";
			this.cmbOwnership.AddItemSeparator = ';';
			this.cmbOwnership.BorderStyle = 1;
			this.cmbOwnership.Caption = "";
			this.cmbOwnership.CaptionHeight = 17;
			this.cmbOwnership.CharacterCasing = 0;
			this.cmbOwnership.ColumnCaptionHeight = 17;
			this.cmbOwnership.ColumnFooterHeight = 17;
			this.cmbOwnership.ColumnHeaders = false;
			this.cmbOwnership.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbOwnership.ContentHeight = 15;
			this.cmbOwnership.DataMode = DataModeEnum.AddItem;
			this.cmbOwnership.DeadAreaBackColor = Color.Empty;
			this.cmbOwnership.EditorBackColor = SystemColors.Window;
			this.cmbOwnership.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbOwnership.EditorForeColor = SystemColors.WindowText;
			this.cmbOwnership.EditorHeight = 15;
			this.cmbOwnership.FlatStyle = FlatModeEnum.Flat;
			this.cmbOwnership.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbOwnership.ItemHeight = 15;
			this.cmbOwnership.Location = new Point(128, 32);
			this.cmbOwnership.MatchEntryTimeout = (long)2000;
			this.cmbOwnership.MaxDropDownItems = 5;
			this.cmbOwnership.MaxLength = 32767;
			this.cmbOwnership.MouseCursor = Cursors.Default;
			this.cmbOwnership.Name = "cmbOwnership";
			this.cmbOwnership.RowDivider.Color = Color.DarkGray;
			this.cmbOwnership.RowDivider.Style = LineStyleEnum.None;
			this.cmbOwnership.RowSubDividerColor = Color.DarkGray;
			this.cmbOwnership.Size = new System.Drawing.Size(64, 19);
			this.cmbOwnership.TabIndex = 9;
			this.cmbOwnership.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label4.Location = new Point(8, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "РНН:";
			this.txtRNN.BorderStyle = BorderStyle.FixedSingle;
			this.txtRNN.Location = new Point(128, 56);
			this.txtRNN.Name = "txtRNN";
			this.txtRNN.Size = new System.Drawing.Size(224, 20);
			this.txtRNN.TabIndex = 6;
			this.txtRNN.Text = "";
			this.txtPatron.BorderStyle = BorderStyle.FixedSingle;
			this.txtPatron.Location = new Point(128, 80);
			this.txtPatron.Name = "txtPatron";
			this.txtPatron.Size = new System.Drawing.Size(224, 20);
			this.txtPatron.TabIndex = 4;
			this.txtPatron.Text = "";
			this.label2.Location = new Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Полное:";
			this.txtName.BorderStyle = BorderStyle.FixedSingle;
			this.txtName.Location = new Point(200, 32);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(152, 20);
			this.txtName.TabIndex = 2;
			this.txtName.Text = "";
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Наименование:";
			this.txtSurname.BorderStyle = BorderStyle.FixedSingle;
			this.txtSurname.Location = new Point(128, 8);
			this.txtSurname.Name = "txtSurname";
			this.txtSurname.Size = new System.Drawing.Size(224, 20);
			this.txtSurname.TabIndex = 0;
			this.txtSurname.Text = "";
			this.tabPage3.Controls.Add(this.groupBox5);
			this.tabPage3.Controls.Add(this.groupBox4);
			this.tabPage3.Location = new Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(648, 361);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Договор на ТО";
			this.groupBox5.Controls.Add(this.bClearGrafikTO);
			this.groupBox5.Controls.Add(this.groupBox6);
			this.groupBox5.Controls.Add(this.cmbYearTO);
			this.groupBox5.Controls.Add(this.label10);
			this.groupBox5.Controls.Add(this.bSaveGrafikTO);
			this.groupBox5.Location = new Point(8, 64);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(632, 288);
			this.groupBox5.TabIndex = 45;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "График техобслуживания";
			this.bClearGrafikTO.Location = new Point(272, 20);
			this.bClearGrafikTO.Name = "bClearGrafikTO";
			this.bClearGrafikTO.Size = new System.Drawing.Size(128, 23);
			this.bClearGrafikTO.TabIndex = 4;
			this.bClearGrafikTO.Text = "Очистить график";
			this.bClearGrafikTO.Click += new EventHandler(this.bClearGrafikTO_Click);
			this.groupBox6.Controls.Add(this.txtDecAmount);
			this.groupBox6.Controls.Add(this.chkDec);
			this.groupBox6.Controls.Add(this.txtNovAmount);
			this.groupBox6.Controls.Add(this.chkNov);
			this.groupBox6.Controls.Add(this.txtOctAmount);
			this.groupBox6.Controls.Add(this.chkOct);
			this.groupBox6.Controls.Add(this.txtSepAmount);
			this.groupBox6.Controls.Add(this.chkSep);
			this.groupBox6.Controls.Add(this.txtAugAmount);
			this.groupBox6.Controls.Add(this.chkAug);
			this.groupBox6.Controls.Add(this.txtJulAmount);
			this.groupBox6.Controls.Add(this.chkJul);
			this.groupBox6.Controls.Add(this.txtJunAmount);
			this.groupBox6.Controls.Add(this.chkJun);
			this.groupBox6.Controls.Add(this.txtMayAmount);
			this.groupBox6.Controls.Add(this.chkMay);
			this.groupBox6.Controls.Add(this.txtAprAmount);
			this.groupBox6.Controls.Add(this.chkApr);
			this.groupBox6.Controls.Add(this.txtMarAmount);
			this.groupBox6.Controls.Add(this.chkMar);
			this.groupBox6.Controls.Add(this.txtFebAmount);
			this.groupBox6.Controls.Add(this.chkFeb);
			this.groupBox6.Controls.Add(this.txtJanAmount);
			this.groupBox6.Controls.Add(this.chkJan);
			this.groupBox6.Location = new Point(8, 56);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(616, 152);
			this.groupBox6.TabIndex = 2;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "по месяцам";
			this.txtDecAmount.Location = new Point(456, 120);
			this.txtDecAmount.Name = "txtDecAmount";
			this.txtDecAmount.Size = new System.Drawing.Size(88, 20);
			this.txtDecAmount.TabIndex = 23;
			this.txtDecAmount.Text = "";
			this.chkDec.Location = new Point(384, 120);
			this.chkDec.Name = "chkDec";
			this.chkDec.Size = new System.Drawing.Size(80, 24);
			this.chkDec.TabIndex = 22;
			this.chkDec.Text = "Декабрь";
			this.chkDec.CheckedChanged += new EventHandler(this.chkDec_CheckedChanged);
			this.txtNovAmount.Location = new Point(456, 88);
			this.txtNovAmount.Name = "txtNovAmount";
			this.txtNovAmount.Size = new System.Drawing.Size(88, 20);
			this.txtNovAmount.TabIndex = 21;
			this.txtNovAmount.Text = "";
			this.chkNov.Location = new Point(384, 88);
			this.chkNov.Name = "chkNov";
			this.chkNov.Size = new System.Drawing.Size(80, 24);
			this.chkNov.TabIndex = 20;
			this.chkNov.Text = "Ноябрь";
			this.chkNov.CheckedChanged += new EventHandler(this.chkNov_CheckedChanged);
			this.txtOctAmount.Location = new Point(456, 56);
			this.txtOctAmount.Name = "txtOctAmount";
			this.txtOctAmount.Size = new System.Drawing.Size(88, 20);
			this.txtOctAmount.TabIndex = 19;
			this.txtOctAmount.Text = "";
			this.chkOct.Location = new Point(384, 56);
			this.chkOct.Name = "chkOct";
			this.chkOct.Size = new System.Drawing.Size(80, 24);
			this.chkOct.TabIndex = 18;
			this.chkOct.Text = "Октябрь";
			this.chkOct.CheckedChanged += new EventHandler(this.chkOct_CheckedChanged);
			this.txtSepAmount.Location = new Point(456, 24);
			this.txtSepAmount.Name = "txtSepAmount";
			this.txtSepAmount.Size = new System.Drawing.Size(88, 20);
			this.txtSepAmount.TabIndex = 17;
			this.txtSepAmount.Text = "";
			this.chkSep.Location = new Point(384, 24);
			this.chkSep.Name = "chkSep";
			this.chkSep.Size = new System.Drawing.Size(80, 24);
			this.chkSep.TabIndex = 16;
			this.chkSep.Text = "Сентябрь";
			this.chkSep.CheckedChanged += new EventHandler(this.chkSep_CheckedChanged);
			this.txtAugAmount.Location = new Point(272, 120);
			this.txtAugAmount.Name = "txtAugAmount";
			this.txtAugAmount.Size = new System.Drawing.Size(88, 20);
			this.txtAugAmount.TabIndex = 15;
			this.txtAugAmount.Text = "";
			this.chkAug.Location = new Point(200, 120);
			this.chkAug.Name = "chkAug";
			this.chkAug.Size = new System.Drawing.Size(72, 24);
			this.chkAug.TabIndex = 14;
			this.chkAug.Text = "Август";
			this.chkAug.CheckedChanged += new EventHandler(this.chkAug_CheckedChanged);
			this.txtJulAmount.Location = new Point(272, 88);
			this.txtJulAmount.Name = "txtJulAmount";
			this.txtJulAmount.Size = new System.Drawing.Size(88, 20);
			this.txtJulAmount.TabIndex = 13;
			this.txtJulAmount.Text = "";
			this.chkJul.Location = new Point(200, 88);
			this.chkJul.Name = "chkJul";
			this.chkJul.Size = new System.Drawing.Size(72, 24);
			this.chkJul.TabIndex = 12;
			this.chkJul.Text = "Июль";
			this.chkJul.CheckedChanged += new EventHandler(this.chkJul_CheckedChanged);
			this.txtJunAmount.Location = new Point(272, 56);
			this.txtJunAmount.Name = "txtJunAmount";
			this.txtJunAmount.Size = new System.Drawing.Size(88, 20);
			this.txtJunAmount.TabIndex = 11;
			this.txtJunAmount.Text = "";
			this.chkJun.Location = new Point(200, 56);
			this.chkJun.Name = "chkJun";
			this.chkJun.Size = new System.Drawing.Size(72, 24);
			this.chkJun.TabIndex = 10;
			this.chkJun.Text = "Июнь";
			this.chkJun.CheckedChanged += new EventHandler(this.chkJun_CheckedChanged);
			this.txtMayAmount.Location = new Point(272, 24);
			this.txtMayAmount.Name = "txtMayAmount";
			this.txtMayAmount.Size = new System.Drawing.Size(88, 20);
			this.txtMayAmount.TabIndex = 9;
			this.txtMayAmount.Text = "";
			this.chkMay.Location = new Point(200, 24);
			this.chkMay.Name = "chkMay";
			this.chkMay.Size = new System.Drawing.Size(72, 24);
			this.chkMay.TabIndex = 8;
			this.chkMay.Text = "Май";
			this.chkMay.CheckedChanged += new EventHandler(this.chkMay_CheckedChanged);
			this.txtAprAmount.Location = new Point(88, 120);
			this.txtAprAmount.Name = "txtAprAmount";
			this.txtAprAmount.Size = new System.Drawing.Size(88, 20);
			this.txtAprAmount.TabIndex = 7;
			this.txtAprAmount.Text = "";
			this.chkApr.Location = new Point(16, 120);
			this.chkApr.Name = "chkApr";
			this.chkApr.Size = new System.Drawing.Size(72, 24);
			this.chkApr.TabIndex = 6;
			this.chkApr.Text = "Апрель";
			this.chkApr.CheckedChanged += new EventHandler(this.chkApr_CheckedChanged);
			this.txtMarAmount.Location = new Point(88, 88);
			this.txtMarAmount.Name = "txtMarAmount";
			this.txtMarAmount.Size = new System.Drawing.Size(88, 20);
			this.txtMarAmount.TabIndex = 5;
			this.txtMarAmount.Text = "";
			this.chkMar.Location = new Point(16, 88);
			this.chkMar.Name = "chkMar";
			this.chkMar.Size = new System.Drawing.Size(72, 24);
			this.chkMar.TabIndex = 4;
			this.chkMar.Text = "Март";
			this.chkMar.CheckedChanged += new EventHandler(this.chkMar_CheckedChanged);
			this.txtFebAmount.Location = new Point(88, 56);
			this.txtFebAmount.Name = "txtFebAmount";
			this.txtFebAmount.Size = new System.Drawing.Size(88, 20);
			this.txtFebAmount.TabIndex = 3;
			this.txtFebAmount.Text = "";
			this.chkFeb.Location = new Point(16, 56);
			this.chkFeb.Name = "chkFeb";
			this.chkFeb.Size = new System.Drawing.Size(72, 24);
			this.chkFeb.TabIndex = 2;
			this.chkFeb.Text = "Февраль";
			this.chkFeb.CheckedChanged += new EventHandler(this.chkFeb_CheckedChanged);
			this.txtJanAmount.Location = new Point(88, 24);
			this.txtJanAmount.Name = "txtJanAmount";
			this.txtJanAmount.Size = new System.Drawing.Size(88, 20);
			this.txtJanAmount.TabIndex = 1;
			this.txtJanAmount.Text = "";
			this.chkJan.Location = new Point(16, 24);
			this.chkJan.Name = "chkJan";
			this.chkJan.Size = new System.Drawing.Size(72, 24);
			this.chkJan.TabIndex = 0;
			this.chkJan.Text = "Январь";
			this.chkJan.CheckedChanged += new EventHandler(this.chkJan_CheckedChanged);
			this.cmbYearTO.Location = new Point(40, 22);
			this.cmbYearTO.Name = "cmbYearTO";
			this.cmbYearTO.Size = new System.Drawing.Size(80, 21);
			this.cmbYearTO.TabIndex = 1;
			this.cmbYearTO.SelectedIndexChanged += new EventHandler(this.cmbYearTO_SelectedIndexChanged);
			this.label10.Location = new Point(8, 24);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(24, 23);
			this.label10.TabIndex = 0;
			this.label10.Text = "Год";
			this.bSaveGrafikTO.Location = new Point(128, 20);
			this.bSaveGrafikTO.Name = "bSaveGrafikTO";
			this.bSaveGrafikTO.Size = new System.Drawing.Size(128, 23);
			this.bSaveGrafikTO.TabIndex = 3;
			this.bSaveGrafikTO.Text = "Сохранить график";
			this.bSaveGrafikTO.Click += new EventHandler(this.bSaveGrafikTO_Click);
			this.groupBox4.Controls.Add(this.txtCost);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.dtDateDog);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.txtNumberDog);
			this.groupBox4.Location = new Point(8, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(632, 48);
			this.groupBox4.TabIndex = 44;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Договор на техобслуживание газового оборудования";
			this.txtCost.Location = new Point(456, 18);
			this.txtCost.Name = "txtCost";
			this.txtCost.TabIndex = 5;
			this.txtCost.Text = "";
			this.label9.Location = new Point(384, 21);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 16);
			this.label9.TabIndex = 4;
			this.label9.Text = "Стоимость:";
			this.dtDateDog.Location = new Point(244, 18);
			this.dtDateDog.Name = "dtDateDog";
			this.dtDateDog.Size = new System.Drawing.Size(128, 20);
			this.dtDateDog.TabIndex = 3;
			this.label8.Location = new Point(203, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 16);
			this.label8.TabIndex = 2;
			this.label8.Text = "Дата:";
			this.label7.Location = new Point(10, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 1;
			this.label7.Text = "Номер:";
			this.txtNumberDog.Location = new Point(64, 18);
			this.txtNumberDog.Name = "txtNumberDog";
			this.txtNumberDog.Size = new System.Drawing.Size(128, 20);
			this.txtNumberDog.TabIndex = 0;
			this.txtNumberDog.Text = "";
			this.tabPage2.Controls.Add(this.listView1);
			this.tabPage2.Location = new Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(648, 361);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "История ФИО";
			ListView.ColumnHeaderCollection columns1 = this.listView1.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader5, this.columnHeader7, this.columnHeader8, this.columnHeader10, this.columnHeader9 };
			columns1.AddRange(columnHeaderArray);
			this.listView1.GridLines = true;
			this.listView1.Location = new Point(2, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(644, 284);
			this.listView1.TabIndex = 1;
			this.listView1.View = View.Details;
			this.columnHeader5.Text = "Фамилия";
			this.columnHeader5.Width = 154;
			this.columnHeader7.Text = "Имя";
			this.columnHeader7.Width = 152;
			this.columnHeader8.Text = "Отчество";
			this.columnHeader8.Width = 159;
			this.columnHeader10.Text = "РНН/ИИН";
			this.columnHeader10.Width = 116;
			this.columnHeader9.Text = "по дату";
			this.columnHeader9.Width = 166;
			this.tabPage4.Controls.Add(this.bBlockCabinet);
			this.tabPage4.Controls.Add(this.lblState);
			this.tabPage4.Controls.Add(this.label13);
			this.tabPage4.Controls.Add(this.label12);
			this.tabPage4.Controls.Add(this.txtEMail);
			this.tabPage4.Controls.Add(this.bSavePassword);
			this.tabPage4.Controls.Add(this.lblPassword);
			this.tabPage4.Controls.Add(this.label11);
			this.tabPage4.Controls.Add(this.bGeneratePassword);
			this.tabPage4.Location = new Point(4, 25);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(648, 361);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Личный кабинет";
			this.label12.Location = new Point(10, 72);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(80, 16);
			this.label12.TabIndex = 11;
			this.label12.Text = "E-mail:";
			this.txtEMail.Location = new Point(120, 72);
			this.txtEMail.Name = "txtEMail";
			this.txtEMail.Size = new System.Drawing.Size(320, 20);
			this.txtEMail.TabIndex = 10;
			this.txtEMail.Text = "";
			this.bSavePassword.Enabled = false;
			this.bSavePassword.Location = new Point(456, 72);
			this.bSavePassword.Name = "bSavePassword";
			this.bSavePassword.Size = new System.Drawing.Size(184, 23);
			this.bSavePassword.TabIndex = 3;
			this.bSavePassword.Text = "Записать пароль";
			this.bSavePassword.Click += new EventHandler(this.bSavePassword_Click);
			this.lblPassword.BorderStyle = BorderStyle.FixedSingle;
			this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblPassword.Location = new Point(120, 40);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(320, 23);
			this.lblPassword.TabIndex = 2;
			this.lblPassword.TextAlign = ContentAlignment.MiddleCenter;
			this.label11.Location = new Point(10, 43);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(96, 16);
			this.label11.TabIndex = 1;
			this.label11.Text = "Пароль:";
			this.bGeneratePassword.Location = new Point(456, 40);
			this.bGeneratePassword.Name = "bGeneratePassword";
			this.bGeneratePassword.Size = new System.Drawing.Size(184, 23);
			this.bGeneratePassword.TabIndex = 0;
			this.bGeneratePassword.Text = "Сгенерировать пароль";
			this.bGeneratePassword.Click += new EventHandler(this.bGeneratePassword_Click);
			this.cmdOk.FlatStyle = FlatStyle.Flat;
			this.cmdOk.Location = new Point(296, 400);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(116, 24);
			this.cmdOk.TabIndex = 13;
			this.cmdOk.Text = "Ok";
			this.cmdOk.Click += new EventHandler(this.cmdOk_Click);
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(536, 400);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(116, 24);
			this.cmdCancel.TabIndex = 12;
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
			this.cmdApply.Location = new Point(416, 400);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(116, 24);
			this.cmdApply.TabIndex = 14;
			this.cmdApply.Text = "Применить";
			this.cmdApply.Click += new EventHandler(this.cmdApply_Click);
			this.bBlockCabinet.Location = new Point(458, 8);
			this.bBlockCabinet.Name = "bBlockCabinet";
			this.bBlockCabinet.Size = new System.Drawing.Size(184, 23);
			this.bBlockCabinet.TabIndex = 15;
			this.bBlockCabinet.Text = "Заблокировать";
			this.bBlockCabinet.Click += new EventHandler(this.bBlockCabinet_Click);
			this.lblState.BorderStyle = BorderStyle.FixedSingle;
			this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblState.Location = new Point(122, 8);
			this.lblState.Name = "lblState";
			this.lblState.Size = new System.Drawing.Size(320, 23);
			this.lblState.TabIndex = 14;
			this.lblState.TextAlign = ContentAlignment.MiddleCenter;
			this.label13.Location = new Point(10, 16);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(112, 16);
			this.label13.TabIndex = 13;
			this.label13.Text = "Текущее состояние:";
			base.AcceptButton = this.cmdOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new System.Drawing.Size(658, 432);
			base.Controls.Add(this.cmdApply);
			base.Controls.Add(this.cmdOk);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmJPerson";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Потребитель юридическое лицо";
			base.Closing += new CancelEventHandler(this.frmJPerson_Closing);
			base.Load += new EventHandler(this.frmPerson_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmbClassifier).EndInit();
			((ISupportInitialize)this.cmbOwnership).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
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
				this._person.Surname = this.txtSurname.Text;
				this._person.Name = this.txtName.Text;
				this._person.Patronic = this.txtPatron.Text;
				this._person.RNN = this.txtRNN.Text;
				this._person.Memo = this.txtMemo.Text;
				this._person.oOwnership = this._ownerships[this.cmbOwnership.SelectedIndex];
				this._person.oClassifier = this._classifiers[this.cmbClassifier.SelectedIndex];
				this._person.oAddress = this._address;
				this._person.isJuridical = 1;
				if (this.txtNumberDog.Text.Length <= 0)
				{
					this._person.DateDog = DateTime.Today;
					this._person.NumberDog = this.txtNumberDog.Text.Trim();
					this._person.CostDog = Convert.ToDouble(this.txtCost.Text.Replace(".", ","));
				}
				else
				{
					this._person.DateDog = this.dtDateDog.Value;
					this._person.NumberDog = this.txtNumberDog.Text.Trim();
					this._person.CostDog = Convert.ToDouble(this.txtCost.Text.Replace(".", ","));
				}
				flag = this._person.Save() <= 0;
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
	}
}