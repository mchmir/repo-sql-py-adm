using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmAddContracts : Form
	{
		private GroupBox groupBox1;

		private Button cmdAddress;

		private TextBox txtAddress;

		private ImageList imageList1;

		private IContainer components;

		private TextBox txtAccBegins;

		private Label label1;

		private Label label2;

		private Button bOk;

		private Button bCancel;

		private Address _address;

		private Addresss _addresss;

		private NumericUpDown numCountFlats;

		private TypeAddresss _typeadresss;

		private Ownerships _ownerships;

		private Classifiers _classifiers;

		private TypeContracts _typecontracts;

		private TypeInfringementss _typeinfringementss;

		private TypeTariffs _typetariffs;

		public frmAddContracts()
		{
			this.InitializeComponent();
		}

		private void AddNewAccounts()
		{
			if (this._typeadresss == null)
			{
				this._typeadresss = new TypeAddresss();
				this._typeadresss.Load();
			}
			if (this._ownerships == null)
			{
				this._ownerships = new Ownerships();
				this._ownerships.Load(0);
			}
			if (this._classifiers == null)
			{
				this._classifiers = new Classifiers();
				this._classifiers.Load(0);
			}
			if (this._typecontracts == null)
			{
				this._typecontracts = new TypeContracts();
				this._typecontracts.Load();
			}
			if (this._typeinfringementss == null)
			{
				this._typeinfringementss = new TypeInfringementss();
				this._typeinfringementss.Load();
			}
			if (this._typetariffs == null)
			{
				this._typetariffs = new TypeTariffs();
				this._typetariffs.Load();
			}
			int num = Convert.ToInt32(this.numCountFlats.Value);
			if (num == 0)
			{
				MessageBox.Show("Количество квартир должно быть больше 0.", "Внимание");
				return;
			}
			if (this.txtAccBegins.Text.Length != 4)
			{
				MessageBox.Show("Лицевой счет должен начинаться с 4 символов.", "Внимание");
				return;
			}
			try
			{
				int num1 = 1;
				while (num1 <= num)
				{
					Address address = this._addresss.Add();
					address.set_Flat(Convert.ToString(num1));
					address.set_oTypeAddress(this._typeadresss.item((long)1));
					if (address.Save() == 0)
					{
						Person person = new Person()
						{
							Surname = "",
							Name = "",
							Patronic = "",
							RNN = "",
							NumberUDL = "",
							Memo = "",
							WorkPlace = "",
							oOwnership = this._ownerships.item((long)4),
							oClassifier = this._classifiers.item((long)3),
							oAddress = address,
							isJuridical = 0
						};
						if (person.Save() == 0)
						{
							Contract contract = new Contract();
							if (num1 < 10)
							{
								contract.Account = string.Concat(this.txtAccBegins.Text, "00", Convert.ToString(num1));
							}
							if (num1 > 9)
							{
								contract.Account = string.Concat(this.txtAccBegins.Text, "0", Convert.ToString(num1));
							}
							if (num1 > 99)
							{
								contract.Account = string.Concat(this.txtAccBegins.Text, Convert.ToString(num1));
							}
							contract.Status = 0;
							contract.oTypeContract = this._typecontracts.item((long)1);
							contract.oTypeInfringements = this._typeinfringementss.item((long)1);
							contract.oTypeTariff = this._typetariffs.item((long)1);
							int year = DateTime.Today.Year;
							DateTime today = DateTime.Today;
							contract.DateBegin = new DateTime(year, today.Month, 1);
							contract.DateEnd = new DateTime(3000, 1, 1);
							contract.Memo = "";
							contract.PrintChetIzvehen = 0;
							contract.ChargePeny = 1;
							contract.oPerson = person;
							if (contract.Save() == 0)
							{
								num1++;
							}
							else
							{
								MessageBox.Show("При сохранении договора возникла ошибка. Сообщите администратору.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
						else
						{
							MessageBox.Show("При сохранении потребителя возникла ошибка. Сообщите администратору.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
					}
					else
					{
						MessageBox.Show("При сохранении адреса возникла ошибка. Сообщите администратору.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
			}
			catch
			{
			}
			MessageBox.Show("Завершено.", "Внимание");
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void bOk_Click(object sender, EventArgs e)
		{
			string[] str = new string[] { "Вы хотите добавить ", null, null, null, null };
			str[1] = this.numCountFlats.Value.ToString();
			str[2] = " кв. в доме по адресу ";
			str[3] = this.txtAddress.Text.ToString();
			str[4] = ". Продолжить?";
			if (MessageBox.Show(string.Concat(str), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				this.AddNewAccounts();
			}
		}

		private void cmdAddress_Click(object sender, EventArgs e)
		{
			frmAddress _frmAddress = new frmAddress();
			_frmAddress.ShowDialog(this);
			if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				this._addresss = _frmAddress.oAddresss;
				this.txtAddress.Text = string.Concat(this._addresss.get_oHouse().get_oStreet().get_Name(), ", ", this._addresss.get_oHouse().get_Name());
			}
			_frmAddress = null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmAddContracts));
			this.groupBox1 = new GroupBox();
			this.cmdAddress = new Button();
			this.imageList1 = new ImageList(this.components);
			this.txtAddress = new TextBox();
			this.txtAccBegins = new TextBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.bOk = new Button();
			this.bCancel = new Button();
			this.numCountFlats = new NumericUpDown();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.numCountFlats).BeginInit();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.cmdAddress);
			this.groupBox1.Controls.Add(this.txtAddress);
			this.groupBox1.Location = new Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(304, 56);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Адрес";
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.ImageIndex = 4;
			this.cmdAddress.ImageList = this.imageList1;
			this.cmdAddress.Location = new Point(266, 19);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(24, 24);
			this.cmdAddress.TabIndex = 1;
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.txtAddress.BackColor = SystemColors.Info;
			this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
			this.txtAddress.Location = new Point(10, 19);
			this.txtAddress.Multiline = true;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(256, 24);
			this.txtAddress.TabIndex = 0;
			this.txtAddress.Text = "";
			this.txtAccBegins.Location = new Point(176, 72);
			this.txtAccBegins.Name = "txtAccBegins";
			this.txtAccBegins.Size = new System.Drawing.Size(136, 20);
			this.txtAccBegins.TabIndex = 10;
			this.txtAccBegins.Text = "";
			this.label1.Location = new Point(8, 74);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 14);
			this.label1.TabIndex = 11;
			this.label1.Text = "Лицевые счета начинаются с:";
			this.label2.Location = new Point(8, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(168, 14);
			this.label2.TabIndex = 13;
			this.label2.Text = "Количество квартир в доме:";
			this.bOk.Location = new Point(239, 136);
			this.bOk.Name = "bOk";
			this.bOk.TabIndex = 14;
			this.bOk.Text = "Добавить";
			this.bOk.Click += new EventHandler(this.bOk_Click);
			this.bCancel.Location = new Point(160, 136);
			this.bCancel.Name = "bCancel";
			this.bCancel.TabIndex = 15;
			this.bCancel.Text = "Закрыть";
			this.bCancel.Click += new EventHandler(this.bCancel_Click);
			this.numCountFlats.Location = new Point(176, 104);
			NumericUpDown num = this.numCountFlats;
			int[] numArray = new int[] { 999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numCountFlats.Name = "numCountFlats";
			this.numCountFlats.Size = new System.Drawing.Size(136, 20);
			this.numCountFlats.TabIndex = 16;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(320, 166);
			base.Controls.Add(this.numCountFlats);
			base.Controls.Add(this.bCancel);
			base.Controls.Add(this.bOk);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtAccBegins);
			base.Controls.Add(this.groupBox1);
			base.MaximumSize = new System.Drawing.Size(328, 200);
			base.MinimumSize = new System.Drawing.Size(328, 200);
			base.Name = "frmAddContracts";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Адреса, потребители и договора";
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.numCountFlats).EndInit();
			base.ResumeLayout(false);
		}
	}
}