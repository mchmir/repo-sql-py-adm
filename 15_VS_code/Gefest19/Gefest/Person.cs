using System;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class Person : SimpleClass
	{
		private Ownership _Ownership;

		private Classifier _Classifier;

		private Address _address;

		private SYSUser _User;

		private Phones _phones;

		private Contracts _contracts;

		public double CostDog
		{
			get
			{
				return (double)base.GetValue("CostDog");
			}
			set
			{
				base.SetValue("CostDog", value);
			}
		}

		public DateTime DateDog
		{
			get
			{
				return (DateTime)base.GetValue("DateDog");
			}
			set
			{
				base.SetValue("DateDog", value);
			}
		}

		public string FIOMainBuch
		{
			get
			{
				return (string)base.GetValue("FIOMainBuch");
			}
			set
			{
				base.SetValue("FIOMainBuch", value);
			}
		}

		public string FullName
		{
			get
			{
				if (this.isJuridical == 1)
				{
					return this.Surname;
				}
				string[] surname = new string[] { this.Surname, " ", this.Name, " ", this.Patronic };
				return string.Concat(surname);
			}
		}

		public int IDUser
		{
			get
			{
				return (int)base.GetValue("IDUser");
			}
			set
			{
				base.SetValue("IDUser", value);
			}
		}

		public int isJuridical
		{
			get
			{
				return (int)base.GetValue("isJuridical");
			}
			set
			{
				base.SetValue("isJuridical", value);
			}
		}

		public string Memo
		{
			get
			{
				return (string)base.GetValue("Memo");
			}
			set
			{
				base.SetValue("Memo", value);
			}
		}

		public string Name
		{
			get
			{
				return (string)base.GetValue("Name");
			}
			set
			{
				base.SetValue("Name", value);
			}
		}

		public string NameKZ
		{
			get
			{
				return (string)base.GetValue("NName");
			}
		}

		public string NumberDog
		{
			get
			{
				return (string)base.GetValue("NumberDog");
			}
			set
			{
				base.SetValue("NumberDog", value);
			}
		}

		public string NumberUDL
		{
			get
			{
				return (string)base.GetValue("NumberUDL");
			}
			set
			{
				base.SetValue("NumberUDL", value);
			}
		}

		public Address oAddress
		{
			get
			{
				if (this._address == null)
				{
					int value = (int)base.GetValue("IDAddress");
					this._address = new Address();
					if (this._address.Load((long)value) != 0)
					{
						this._address = null;
					}
				}
				return this._address;
			}
			set
			{
				this._address = value;
				if (this._address != null)
				{
					base.SetValue("IDAddress", this._address.get_ID());
				}
			}
		}

		public Classifier oClassifier
		{
			get
			{
				if (this._Classifier == null)
				{
					int value = (int)base.GetValue("IDClassifier");
					this._Classifier = new Classifier();
					if (this._Classifier.Load((long)value) != 0)
					{
						this._Classifier = null;
					}
				}
				return this._Classifier;
			}
			set
			{
				this._Classifier = value;
				if (this._Classifier != null)
				{
					base.SetValue("IDClassifier", this._Classifier.get_ID());
				}
			}
		}

		public Contracts oContracts
		{
			get
			{
				if (this._contracts == null)
				{
					this._contracts = new Contracts();
					if (this._contracts.Load(this) != 0)
					{
						this._contracts = null;
					}
				}
				return this._contracts;
			}
		}

		public Ownership oOwnership
		{
			get
			{
				if (this._Ownership == null)
				{
					int value = (int)base.GetValue("IDOwnership");
					this._Ownership = new Ownership();
					if (this._Ownership.Load((long)value) != 0)
					{
						this._Ownership = null;
					}
				}
				return this._Ownership;
			}
			set
			{
				this._Ownership = value;
				if (this._Ownership != null)
				{
					base.SetValue("IDOwnership", this._Ownership.get_ID());
				}
			}
		}

		public Phones oPhones
		{
			get
			{
				if (this._phones == null)
				{
					this._phones = new Phones();
					if (this._phones.Load(this) != 0)
					{
						this._phones = null;
					}
				}
				return this._phones;
			}
		}

		public SYSUser oUser
		{
			get
			{
				if (this._User == null)
				{
					int value = (int)base.GetValue("IDUser");
					this._User = new SYSUser();
					if (this._User.Load((long)value) != 0)
					{
						this._User = null;
					}
				}
				return this._User;
			}
			set
			{
				this._User = value;
				if (this._User != null)
				{
					base.SetValue("IDUser", this._User.get_ID());
				}
			}
		}

		public string Patronic
		{
			get
			{
				return (string)base.GetValue("Patronic");
			}
			set
			{
				base.SetValue("Patronic", value);
			}
		}

		public string PatronicKZ
		{
			get
			{
				return (string)base.GetValue("NPatronic");
			}
		}

		public string RNN
		{
			get
			{
				return (string)base.GetValue("RNN");
			}
			set
			{
				base.SetValue("RNN", value);
			}
		}

		public string Surname
		{
			get
			{
				return (string)base.GetValue("Surname");
			}
			set
			{
				base.SetValue("Surname", value);
			}
		}

		public string SurnameKZ
		{
			get
			{
				return (string)base.GetValue("NSurname");
			}
		}

		public string WorkPlace
		{
			get
			{
				return (string)base.GetValue("WorkPlace");
			}
			set
			{
				base.SetValue("WorkPlace", value);
			}
		}

		public Person()
		{
			this.NameTable = "Person";
			base.Init();
		}

		public bool SaveKZPersonName(long IDPerson, string Surname, string Name, string Patronic)
		{
			bool flag;
			try
			{
				string[] str = new string[] { "declare @blerr bit exec spSaveKZPersonName ", IDPerson.ToString(), ",N'", Surname.ToString(), "',N'", Name.ToString(), "',N'", Patronic.ToString(), "',@blerr" };
				Saver.ExecuteQuery(string.Concat(str), 3600);
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}
	}
}