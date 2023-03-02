using C1.Win.C1List;
using C1.Win.C1List.Util;
using Microsoft.VisualBasic;
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
	public class ucAddress : UserControl
	{
		private ComboBox cmbCountry;

		private ComboBox cmbProvince;

		private ComboBox cmbPlace;

		private ComboBox cmbStreet;

		private ComboBox cmbHouse;

		private Button cmdAddCountry;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Button cmdCountrys;

		private Countrys _countrys;

		private Provinces _provinces;

		private Places _places;

		private Streets _streets;

		private Houses _houses;

		private Addresss _addresss;

		private TypeAddresss _typeadresss;

		private Button cmdProvinces;

		private Button cmdAddProvince;

		private ComboBox cmbAddress;

		private ToolTip toolTip1;

		private Button cmdPlaces;

		private Button cmdAddPlace;

		private Button cmdStreets;

		private Button cmdAddStreet;

		private Button cmdHouses;

		private Button cmdAddHouse;

		private Button cmdAddresss;

		private Button cmdAddAddress;

		private IContainer components;

		private C1Combo cmbStreet2;

		private Address _address;

		public Address oAddress
		{
			get
			{
				return this._addresss.get_Item(this.cmbAddress.SelectedIndex);
			}
			set
			{
				this._address = value;
			}
		}

		public Addresss oAddresss
		{
			get
			{
				return this._addresss;
			}
			set
			{
				this._addresss = value;
			}
		}

		public ucAddress()
		{
			this.InitializeComponent();
		}

		private void cmbAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				((frmAddress)base.ParentForm).cmdOk.Focus();
			}
		}

		private void cmbAddress_Leave(object sender, EventArgs e)
		{
			this.FindAddress();
		}

		private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tools.ClearCMB(this.cmbProvince);
			Tools.ClearCMB(this.cmbPlace);
			Tools.ClearCMB(this.cmbStreet);
			Tools.ClearCMB(this.cmbHouse);
			Tools.ClearCMB(this.cmbAddress);
			if (this.cmbCountry.SelectedIndex >= 0)
			{
				this._provinces = this._countrys.get_Item(this.cmbCountry.SelectedIndex).get_oProvinces();
				if (this._provinces != null)
				{
					if (this._address.get_ID() != (long)0)
					{
						Tools.FillCMB(this._provinces, this.cmbProvince, this._address.get_oPlace().get_oProvince().get_ID());
						return;
					}
					Tools.FillCMB(this._provinces, this.cmbProvince, (long)0);
				}
			}
		}

		private void cmbHouse_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbAddress.Focus();
			}
		}

		private void cmbHouse_Leave(object sender, EventArgs e)
		{
			this.FindHouse();
		}

		private void cmbHouse_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tools.ClearCMB(this.cmbAddress);
			if (this.cmbHouse.SelectedIndex >= 0)
			{
				this._addresss = this._houses.get_Item(this.cmbHouse.SelectedIndex).get_oAddresss();
				if (this._addresss != null)
				{
					if (this._address.get_ID() != (long)0)
					{
						Tools.FillCMB(this._addresss, this.cmbAddress, this._address.get_ID());
						return;
					}
					Tools.FillCMB(this._addresss, this.cmbAddress, (long)0);
				}
			}
		}

		private void cmbPlace_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tools.ClearCMB(this.cmbStreet);
			Tools.ClearCMB(this.cmbHouse);
			Tools.ClearCMB(this.cmbAddress);
			if (this.cmbPlace.SelectedIndex >= 0)
			{
				this._streets = this._places.get_Item(this.cmbPlace.SelectedIndex).get_oStreets();
				if (this._streets != null)
				{
					if (this._address.get_ID() != (long)0)
					{
						Tools.FillC1(this._streets, this.cmbStreet2, this._address.get_oHouse().get_oStreet().get_ID());
						return;
					}
					Tools.FillC1(this._streets, this.cmbStreet2, (long)0);
				}
			}
		}

		private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tools.ClearCMB(this.cmbPlace);
			Tools.ClearCMB(this.cmbStreet);
			Tools.ClearCMB(this.cmbHouse);
			Tools.ClearCMB(this.cmbAddress);
			if (this.cmbProvince.SelectedIndex >= 0)
			{
				this._places = this._provinces.get_Item(this.cmbProvince.SelectedIndex).get_oPlaces();
				if (this._places != null)
				{
					if (this._address.get_ID() != (long)0)
					{
						Tools.FillCMB(this._places, this.cmbPlace, this._address.get_oPlace().get_ID());
						return;
					}
					Tools.FillCMB(this._places, this.cmbPlace, (long)0);
				}
			}
		}

		private void cmbStreet_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbHouse.Focus();
			}
		}

		private void cmbStreet_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tools.ClearCMB(this.cmbHouse);
			Tools.ClearCMB(this.cmbAddress);
			if (this.cmbStreet.SelectedIndex >= 0)
			{
				this._houses = this._streets.get_Item(this.cmbStreet.SelectedIndex).get_oHouses();
				if (this._houses != null)
				{
					if (this._address.get_ID() != (long)0)
					{
						Tools.FillCMB(this._houses, this.cmbHouse, this._address.get_oHouse().get_ID());
						return;
					}
					Tools.FillCMB(this._houses, this.cmbHouse, (long)0);
				}
			}
		}

		private void cmbStreet2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbHouse.Focus();
			}
		}

		private void cmbStreet2_TextChanged(object sender, EventArgs e)
		{
			this.CreateHouse();
		}

		private void cmdAddAddress_Click(object sender, EventArgs e)
		{
			Address address = this._addresss.Add();
			string[] strArrays = new string[] { "Flat" };
			string[] strArrays1 = strArrays;
			strArrays = new string[] { "Номер:", "Тип:" };
			string[] strArrays2 = strArrays;
			strArrays = new string[] { "TypeAddress" };
			frmSimpleObj _frmSimpleObj = new frmSimpleObj("Добавление квартиры", address, strArrays1, strArrays2, strArrays);
			_frmSimpleObj.ShowDialog(this);
			_frmSimpleObj = null;
			Tools.FillCMB(this._addresss, this.cmbAddress, (long)0);
		}

		private void cmdAddAddresses_Click(object sender, EventArgs e)
		{
			if (this._typeadresss == null)
			{
				this._typeadresss = new TypeAddresss();
				this._typeadresss.Load();
			}
			string str = Interaction.InputBox("Введите количество квартир", "Ввод", "0", 100, 100);
			if (str.Length > 0)
			{
				try
				{
					int num = Convert.ToInt32(str);
					for (int i = 1; i <= num; i++)
					{
						Address address = this._addresss.Add();
						address.set_Flat(Convert.ToString(i));
						address.set_oTypeAddress(this._typeadresss.item((long)1));
					}
				}
				catch
				{
				}
				Tools.FillCMB(this._addresss, this.cmbAddress, (long)0);
			}
		}

		private void cmdAddCountry_Click(object sender, EventArgs e)
		{
			Country country = this._countrys.Add();
			string[] strArrays = new string[] { "Name" };
			string[] strArrays1 = strArrays;
			strArrays = new string[] { "Название:" };
			frmSimpleObj _frmSimpleObj = new frmSimpleObj("Добавление страны", country, strArrays1, strArrays, null);
			_frmSimpleObj.ShowDialog(this);
			_frmSimpleObj = null;
			Tools.FillCMB(this._countrys, this.cmbCountry, (long)0);
		}

		private void cmdAddHouse_Click(object sender, EventArgs e)
		{
			House house = this._houses.Add();
			string[] strArrays = new string[] { "HouseNumber", "HouseNumberChar", "IsEvenSide", "IsComfortable" };
			string[] strArrays1 = strArrays;
			strArrays = new string[] { "Номер:", "Буква:", "Четная сторона:", "Комфортабельный:", "Тип:" };
			string[] strArrays2 = strArrays;
			strArrays = new string[] { "TypeHouse" };
			frmSimpleObj _frmSimpleObj = new frmSimpleObj("Добавление дома", house, strArrays1, strArrays2, strArrays);
			_frmSimpleObj.ShowDialog(this);
			_frmSimpleObj = null;
			Tools.FillCMB(this._houses, this.cmbHouse, (long)0);
		}

		private void cmdAddPlace_Click(object sender, EventArgs e)
		{
			Place place = this._places.Add();
			string[] strArrays = new string[] { "Name" };
			string[] strArrays1 = strArrays;
			strArrays = new string[] { "Название:", "Тип нас.пункта:" };
			string[] strArrays2 = strArrays;
			strArrays = new string[] { "TypePlace" };
			frmSimpleObj _frmSimpleObj = new frmSimpleObj("Добавление города", place, strArrays1, strArrays2, strArrays);
			_frmSimpleObj.ShowDialog(this);
			_frmSimpleObj = null;
			Tools.FillCMB(this._places, this.cmbPlace, (long)0);
		}

		private void cmdAddProvince_Click(object sender, EventArgs e)
		{
			Province province = this._provinces.Add();
			string[] strArrays = new string[] { "Name" };
			string[] strArrays1 = strArrays;
			strArrays = new string[] { "Название:" };
			frmSimpleObj _frmSimpleObj = new frmSimpleObj("Добавление области", province, strArrays1, strArrays, null);
			_frmSimpleObj.ShowDialog(this);
			_frmSimpleObj = null;
			Tools.FillCMB(this._provinces, this.cmbProvince, (long)0);
		}

		private void cmdAddresss_Click(object sender, EventArgs e)
		{
			Addresss addresss = this._addresss;
			string[] strArrays = new string[] { "Квартира" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 150 };
			strArrays = new string[] { "Flat" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник квартир", addresss, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			Tools.FillCMB(this._addresss, this.cmbAddress, (long)0);
		}

		private void cmdAddStreet_Click(object sender, EventArgs e)
		{
			Street street = this._streets.Add();
			string[] strArrays = new string[] { "Name" };
			string[] strArrays1 = strArrays;
			strArrays = new string[] { "Название:", "Тип улицы:" };
			string[] strArrays2 = strArrays;
			strArrays = new string[] { "TypeStreet" };
			frmSimpleObj _frmSimpleObj = new frmSimpleObj("Добавление улицы", street, strArrays1, strArrays2, strArrays);
			_frmSimpleObj.ShowDialog(this);
			_frmSimpleObj = null;
			Tools.FillC1(this._streets, this.cmbStreet2, (long)0);
		}

		private void cmdCountrys_Click(object sender, EventArgs e)
		{
			Countrys country = this._countrys;
			string[] strArrays = new string[] { "Страна" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник стран", country, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			Tools.FillCMB(this._countrys, this.cmbCountry, (long)0);
		}

		private void cmdHouses_Click(object sender, EventArgs e)
		{
			Houses house = this._houses;
			string[] strArrays = new string[] { "Номер", "Буква" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 70, 70 };
			strArrays = new string[] { "HouseNumber", "HouseNumberChar" };
			Type[] typeArray = new Type[] { typeof(ListViewInt64Sort), typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник домов", house, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			Tools.FillCMB(this._houses, this.cmbHouse, (long)0);
		}

		private void cmdPlaces_Click(object sender, EventArgs e)
		{
			Places place = this._places;
			string[] strArrays = new string[] { "Город" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник городов", place, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			Tools.FillCMB(this._places, this.cmbPlace, (long)0);
		}

		private void cmdProvinces_Click(object sender, EventArgs e)
		{
			Provinces province = this._provinces;
			string[] strArrays = new string[] { "Область" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник областей", province, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			Tools.FillCMB(this._provinces, this.cmbProvince, (long)0);
		}

		private void cmdStreets_Click(object sender, EventArgs e)
		{
			Streets street = this._streets;
			string[] strArrays = new string[] { "Улица" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200 };
			strArrays = new string[] { "Name" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник улиц", street, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			Tools.FillC1(this._streets, this.cmbStreet2, (long)0);
		}

		private void CreateHouse()
		{
			try
			{
				Tools.ClearCMB(this.cmbHouse);
				Tools.ClearCMB(this.cmbAddress);
				if (this.cmbStreet2.SelectedIndex >= 0)
				{
					this._houses = this._streets.get_Item(this.cmbStreet2.SelectedIndex).get_oHouses();
					if (this._houses != null)
					{
						if (this._address.get_ID() == (long)0)
						{
							Tools.FillCMB(this._houses, this.cmbHouse, (long)0);
						}
						else
						{
							Tools.FillCMB(this._houses, this.cmbHouse, this._address.get_oHouse().get_ID());
						}
					}
				}
			}
			catch
			{
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

		public void FindAddress()
		{
			int num = 0;
			foreach (object item in this.cmbAddress.Items)
			{
				if (item.ToString() != this.cmbAddress.Text)
				{
					num++;
				}
				else
				{
					this.cmbAddress.SelectedIndex = num;
					this.cmbAddress.SelectionStart = this.cmbAddress.Text.Length;
					num = -1;
					break;
				}
			}
			if (num >= 0)
			{
				this.cmbAddress.Focus();
			}
		}

		public void FindHouse()
		{
			int num = 0;
			foreach (object item in this.cmbHouse.Items)
			{
				if (item.ToString() != this.cmbHouse.Text)
				{
					num++;
				}
				else
				{
					this.cmbHouse.SelectedIndex = num;
					this.cmbHouse.SelectionStart = this.cmbHouse.Text.Length;
					break;
				}
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(ucAddress));
			this.cmbCountry = new ComboBox();
			this.cmbProvince = new ComboBox();
			this.cmbPlace = new ComboBox();
			this.cmbStreet = new ComboBox();
			this.cmbHouse = new ComboBox();
			this.cmbAddress = new ComboBox();
			this.cmdAddCountry = new Button();
			this.cmdCountrys = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.cmdProvinces = new Button();
			this.cmdAddProvince = new Button();
			this.cmdPlaces = new Button();
			this.cmdAddPlace = new Button();
			this.cmdStreets = new Button();
			this.cmdAddStreet = new Button();
			this.cmdHouses = new Button();
			this.cmdAddHouse = new Button();
			this.cmdAddresss = new Button();
			this.cmdAddAddress = new Button();
			this.toolTip1 = new ToolTip(this.components);
			this.cmbStreet2 = new C1Combo();
			((ISupportInitialize)this.cmbStreet2).BeginInit();
			base.SuspendLayout();
			this.cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbCountry.Location = new Point(88, 8);
			this.cmbCountry.Name = "cmbCountry";
			this.cmbCountry.Size = new System.Drawing.Size(256, 24);
			this.cmbCountry.TabIndex = 0;
			this.cmbCountry.SelectedIndexChanged += new EventHandler(this.cmbCountry_SelectedIndexChanged);
			this.cmbProvince.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbProvince.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbProvince.Location = new Point(88, 40);
			this.cmbProvince.Name = "cmbProvince";
			this.cmbProvince.Size = new System.Drawing.Size(256, 24);
			this.cmbProvince.TabIndex = 1;
			this.cmbProvince.SelectedIndexChanged += new EventHandler(this.cmbProvince_SelectedIndexChanged);
			this.cmbPlace.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbPlace.Location = new Point(88, 72);
			this.cmbPlace.Name = "cmbPlace";
			this.cmbPlace.Size = new System.Drawing.Size(256, 24);
			this.cmbPlace.TabIndex = 2;
			this.cmbPlace.SelectedIndexChanged += new EventHandler(this.cmbPlace_SelectedIndexChanged);
			this.cmbStreet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbStreet.Location = new Point(48, 104);
			this.cmbStreet.Name = "cmbStreet";
			this.cmbStreet.Size = new System.Drawing.Size(32, 24);
			this.cmbStreet.TabIndex = 3;
			this.cmbStreet.Visible = false;
			this.cmbStreet.KeyPress += new KeyPressEventHandler(this.cmbStreet_KeyPress);
			this.cmbStreet.SelectedIndexChanged += new EventHandler(this.cmbStreet_SelectedIndexChanged);
			this.cmbHouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbHouse.Location = new Point(88, 136);
			this.cmbHouse.Name = "cmbHouse";
			this.cmbHouse.Size = new System.Drawing.Size(256, 24);
			this.cmbHouse.TabIndex = 4;
			this.cmbHouse.KeyPress += new KeyPressEventHandler(this.cmbHouse_KeyPress);
			this.cmbHouse.SelectedIndexChanged += new EventHandler(this.cmbHouse_SelectedIndexChanged);
			this.cmbHouse.Leave += new EventHandler(this.cmbHouse_Leave);
			this.cmbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbAddress.Location = new Point(88, 168);
			this.cmbAddress.Name = "cmbAddress";
			this.cmbAddress.Size = new System.Drawing.Size(256, 24);
			this.cmbAddress.TabIndex = 5;
			this.cmbAddress.KeyPress += new KeyPressEventHandler(this.cmbAddress_KeyPress);
			this.cmbAddress.Leave += new EventHandler(this.cmbAddress_Leave);
			this.cmdAddCountry.FlatStyle = FlatStyle.Flat;
			this.cmdAddCountry.Image = (Image)resourceManager.GetObject("cmdAddCountry.Image");
			this.cmdAddCountry.Location = new Point(344, 8);
			this.cmdAddCountry.Name = "cmdAddCountry";
			this.cmdAddCountry.Size = new System.Drawing.Size(20, 20);
			this.cmdAddCountry.TabIndex = 6;
			this.cmdAddCountry.Text = "Добавить";
			this.toolTip1.SetToolTip(this.cmdAddCountry, "Добавить");
			this.cmdAddCountry.Click += new EventHandler(this.cmdAddCountry_Click);
			this.cmdCountrys.FlatStyle = FlatStyle.Flat;
			this.cmdCountrys.Image = (Image)resourceManager.GetObject("cmdCountrys.Image");
			this.cmdCountrys.Location = new Point(368, 8);
			this.cmdCountrys.Name = "cmdCountrys";
			this.cmdCountrys.Size = new System.Drawing.Size(20, 20);
			this.cmdCountrys.TabIndex = 7;
			this.toolTip1.SetToolTip(this.cmdCountrys, "Справочник");
			this.cmdCountrys.Click += new EventHandler(this.cmdCountrys_Click);
			this.label1.Location = new Point(0, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Страна:";
			this.label2.Location = new Point(0, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "Область:";
			this.label3.Location = new Point(0, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 10;
			this.label3.Text = "Город:";
			this.label4.Location = new Point(0, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Улица:";
			this.label5.Location = new Point(0, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Дом:";
			this.label6.Location = new Point(0, 168);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 16);
			this.label6.TabIndex = 13;
			this.label6.Text = "Квартира:";
			this.cmdProvinces.FlatStyle = FlatStyle.Flat;
			this.cmdProvinces.Image = (Image)resourceManager.GetObject("cmdProvinces.Image");
			this.cmdProvinces.Location = new Point(368, 40);
			this.cmdProvinces.Name = "cmdProvinces";
			this.cmdProvinces.Size = new System.Drawing.Size(20, 20);
			this.cmdProvinces.TabIndex = 9;
			this.toolTip1.SetToolTip(this.cmdProvinces, "Справочник");
			this.cmdProvinces.Click += new EventHandler(this.cmdProvinces_Click);
			this.cmdAddProvince.FlatStyle = FlatStyle.Flat;
			this.cmdAddProvince.Image = (Image)resourceManager.GetObject("cmdAddProvince.Image");
			this.cmdAddProvince.Location = new Point(344, 40);
			this.cmdAddProvince.Name = "cmdAddProvince";
			this.cmdAddProvince.Size = new System.Drawing.Size(20, 20);
			this.cmdAddProvince.TabIndex = 8;
			this.cmdAddProvince.Text = "Добавить";
			this.toolTip1.SetToolTip(this.cmdAddProvince, "Добавить");
			this.cmdAddProvince.Click += new EventHandler(this.cmdAddProvince_Click);
			this.cmdPlaces.FlatStyle = FlatStyle.Flat;
			this.cmdPlaces.Image = (Image)resourceManager.GetObject("cmdPlaces.Image");
			this.cmdPlaces.Location = new Point(368, 72);
			this.cmdPlaces.Name = "cmdPlaces";
			this.cmdPlaces.Size = new System.Drawing.Size(20, 20);
			this.cmdPlaces.TabIndex = 11;
			this.toolTip1.SetToolTip(this.cmdPlaces, "Справочник");
			this.cmdPlaces.Click += new EventHandler(this.cmdPlaces_Click);
			this.cmdAddPlace.FlatStyle = FlatStyle.Flat;
			this.cmdAddPlace.Image = (Image)resourceManager.GetObject("cmdAddPlace.Image");
			this.cmdAddPlace.Location = new Point(344, 72);
			this.cmdAddPlace.Name = "cmdAddPlace";
			this.cmdAddPlace.Size = new System.Drawing.Size(20, 20);
			this.cmdAddPlace.TabIndex = 10;
			this.cmdAddPlace.Text = "Добавить";
			this.toolTip1.SetToolTip(this.cmdAddPlace, "Добавить");
			this.cmdAddPlace.Click += new EventHandler(this.cmdAddPlace_Click);
			this.cmdStreets.FlatStyle = FlatStyle.Flat;
			this.cmdStreets.Image = (Image)resourceManager.GetObject("cmdStreets.Image");
			this.cmdStreets.Location = new Point(368, 104);
			this.cmdStreets.Name = "cmdStreets";
			this.cmdStreets.Size = new System.Drawing.Size(20, 20);
			this.cmdStreets.TabIndex = 13;
			this.toolTip1.SetToolTip(this.cmdStreets, "Справочник");
			this.cmdStreets.Click += new EventHandler(this.cmdStreets_Click);
			this.cmdAddStreet.FlatStyle = FlatStyle.Flat;
			this.cmdAddStreet.Image = (Image)resourceManager.GetObject("cmdAddStreet.Image");
			this.cmdAddStreet.Location = new Point(344, 104);
			this.cmdAddStreet.Name = "cmdAddStreet";
			this.cmdAddStreet.Size = new System.Drawing.Size(20, 20);
			this.cmdAddStreet.TabIndex = 12;
			this.cmdAddStreet.Text = "Добавить";
			this.toolTip1.SetToolTip(this.cmdAddStreet, "Добавить");
			this.cmdAddStreet.Click += new EventHandler(this.cmdAddStreet_Click);
			this.cmdHouses.FlatStyle = FlatStyle.Flat;
			this.cmdHouses.Image = (Image)resourceManager.GetObject("cmdHouses.Image");
			this.cmdHouses.Location = new Point(368, 136);
			this.cmdHouses.Name = "cmdHouses";
			this.cmdHouses.Size = new System.Drawing.Size(20, 20);
			this.cmdHouses.TabIndex = 15;
			this.toolTip1.SetToolTip(this.cmdHouses, "Справочник");
			this.cmdHouses.Click += new EventHandler(this.cmdHouses_Click);
			this.cmdAddHouse.FlatStyle = FlatStyle.Flat;
			this.cmdAddHouse.Image = (Image)resourceManager.GetObject("cmdAddHouse.Image");
			this.cmdAddHouse.Location = new Point(344, 136);
			this.cmdAddHouse.Name = "cmdAddHouse";
			this.cmdAddHouse.Size = new System.Drawing.Size(20, 20);
			this.cmdAddHouse.TabIndex = 14;
			this.cmdAddHouse.Text = "Добавить";
			this.toolTip1.SetToolTip(this.cmdAddHouse, "Добавить");
			this.cmdAddHouse.Click += new EventHandler(this.cmdAddHouse_Click);
			this.cmdAddresss.FlatStyle = FlatStyle.Flat;
			this.cmdAddresss.Image = (Image)resourceManager.GetObject("cmdAddresss.Image");
			this.cmdAddresss.Location = new Point(368, 168);
			this.cmdAddresss.Name = "cmdAddresss";
			this.cmdAddresss.Size = new System.Drawing.Size(20, 20);
			this.cmdAddresss.TabIndex = 17;
			this.toolTip1.SetToolTip(this.cmdAddresss, "Справочник");
			this.cmdAddresss.Click += new EventHandler(this.cmdAddresss_Click);
			this.cmdAddAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddAddress.Image = (Image)resourceManager.GetObject("cmdAddAddress.Image");
			this.cmdAddAddress.Location = new Point(344, 168);
			this.cmdAddAddress.Name = "cmdAddAddress";
			this.cmdAddAddress.Size = new System.Drawing.Size(20, 20);
			this.cmdAddAddress.TabIndex = 16;
			this.cmdAddAddress.Text = "Добавить";
			this.toolTip1.SetToolTip(this.cmdAddAddress, "Добавить");
			this.cmdAddAddress.Click += new EventHandler(this.cmdAddAddress_Click);
			this.cmbStreet2.AddItemSeparator = ';';
			this.cmbStreet2.AutoCompletion = true;
			this.cmbStreet2.AutoDropDown = true;
			this.cmbStreet2.BorderStyle = 1;
			this.cmbStreet2.Caption = "";
			this.cmbStreet2.CaptionHeight = 17;
			this.cmbStreet2.CharacterCasing = 0;
			this.cmbStreet2.ColumnCaptionHeight = 17;
			this.cmbStreet2.ColumnFooterHeight = 17;
			this.cmbStreet2.ColumnHeaders = false;
			this.cmbStreet2.ColumnWidth = 185;
			this.cmbStreet2.ContentHeight = 17;
			this.cmbStreet2.DataMode = DataModeEnum.AddItem;
			this.cmbStreet2.DeadAreaBackColor = Color.Empty;
			this.cmbStreet2.DropMode = DropModeEnum.Manual;
			this.cmbStreet2.EditorBackColor = SystemColors.Window;
			this.cmbStreet2.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbStreet2.EditorForeColor = SystemColors.WindowText;
			this.cmbStreet2.EditorHeight = 17;
			this.cmbStreet2.FlatStyle = FlatModeEnum.Popup;
			this.cmbStreet2.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbStreet2.ItemHeight = 15;
			this.cmbStreet2.Location = new Point(88, 104);
			this.cmbStreet2.MatchEntryTimeout = (long)2000;
			this.cmbStreet2.MaxDropDownItems = 10;
			this.cmbStreet2.MaxLength = 32767;
			this.cmbStreet2.MouseCursor = Cursors.Default;
			this.cmbStreet2.Name = "cmbStreet2";
			this.cmbStreet2.RowDivider.Color = Color.DarkGray;
			this.cmbStreet2.RowDivider.Style = LineStyleEnum.None;
			this.cmbStreet2.RowSubDividerColor = Color.DarkGray;
			this.cmbStreet2.Size = new System.Drawing.Size(254, 21);
			this.cmbStreet2.TabIndex = 46;
			this.cmbStreet2.TextChanged += new EventHandler(this.cmbStreet2_TextChanged);
			this.cmbStreet2.KeyPress += new KeyPressEventHandler(this.cmbStreet2_KeyPress);
			this.cmbStreet2.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Улица\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>185</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			base.Controls.Add(this.cmbStreet2);
			base.Controls.Add(this.cmdAddresss);
			base.Controls.Add(this.cmdAddAddress);
			base.Controls.Add(this.cmdHouses);
			base.Controls.Add(this.cmdAddHouse);
			base.Controls.Add(this.cmdStreets);
			base.Controls.Add(this.cmdAddStreet);
			base.Controls.Add(this.cmdPlaces);
			base.Controls.Add(this.cmdAddPlace);
			base.Controls.Add(this.cmdProvinces);
			base.Controls.Add(this.cmdAddProvince);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmdCountrys);
			base.Controls.Add(this.cmdAddCountry);
			base.Controls.Add(this.cmbAddress);
			base.Controls.Add(this.cmbHouse);
			base.Controls.Add(this.cmbStreet);
			base.Controls.Add(this.cmbPlace);
			base.Controls.Add(this.cmbProvince);
			base.Controls.Add(this.cmbCountry);
			base.Name = "ucAddress";
			base.Size = new System.Drawing.Size(392, 200);
			base.Disposed += new EventHandler(this.ucAddress_Disposed);
			base.Load += new EventHandler(this.ucAddress_Load);
			((ISupportInitialize)this.cmbStreet2).EndInit();
			base.ResumeLayout(false);
		}

		private void ucAddress_Disposed(object sender, EventArgs e)
		{
			this._address = null;
			this._addresss = null;
			this._countrys = null;
			this._houses = null;
			this._places = null;
			this._provinces = null;
			this._streets = null;
			base.Dispose();
		}

		private void ucAddress_Load(object sender, EventArgs e)
		{
			this._countrys = new Countrys();
			this._countrys.Load();
			if (this._address == null)
			{
				this._address = new Address();
			}
			if (this._address.get_ID() == (long)0)
			{
				Tools.FillCMB(this._countrys, this.cmbCountry, (long)0);
			}
			else
			{
				Tools.FillCMB(this._countrys, this.cmbCountry, this._address.get_oPlace().get_oProvince().get_oCountry().get_ID());
			}
			this.cmbStreet2.Focus();
			this.cmbStreet2.SelectAll();
		}
	}
}