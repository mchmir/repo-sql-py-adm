using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Enterprise : SimpleClass
	{
		private Bank _Bank;

		private Person _Person;

		public string Account
		{
			get
			{
				return (string)base.GetValue("Account");
			}
			set
			{
				base.SetValue("Account", value);
			}
		}

		public Bank oBank
		{
			get
			{
				if (this._Bank == null)
				{
					int value = (int)base.GetValue("IDBank");
					this._Bank = new Bank();
					if (this._Bank.Load((long)value) != 0)
					{
						this._Bank = null;
					}
				}
				return this._Bank;
			}
			set
			{
				this._Bank = value;
				if (this._Bank != null)
				{
					base.SetValue("IDBank", this._Bank.get_ID());
				}
			}
		}

		public Person oPerson
		{
			get
			{
				if (this._Person == null)
				{
					int value = (int)base.GetValue("IDPerson");
					this._Person = new Person();
					if (this._Person.Load((long)value) != 0)
					{
						this._Person = null;
					}
				}
				return this._Person;
			}
			set
			{
				this._Person = value;
				if (this._Person != null)
				{
					base.SetValue("IDPerson", this._Person.get_ID());
				}
			}
		}

		public Enterprise()
		{
			this.NameTable = "Enterprise";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}