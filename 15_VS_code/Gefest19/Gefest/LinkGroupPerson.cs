using System;
using WebSecurityDLL;

namespace Gefest
{
	public class LinkGroupPerson : SimpleClass
	{
		private GroupPerson _GroupPerson;

		private Person _Person;

		public GroupPerson oGroupPerson
		{
			get
			{
				if (this._GroupPerson == null)
				{
					int value = (int)base.GetValue("IDGroupPerson");
					this._GroupPerson = new GroupPerson();
					if (this._GroupPerson.Load((long)value) != 0)
					{
						this._GroupPerson = null;
					}
				}
				return this._GroupPerson;
			}
			set
			{
				this._GroupPerson = value;
				if (this._GroupPerson != null)
				{
					base.SetValue("IDGroupPerson", this._GroupPerson.get_ID());
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

		public LinkGroupPerson()
		{
			this.NameTable = "LinkGroupPerson";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}