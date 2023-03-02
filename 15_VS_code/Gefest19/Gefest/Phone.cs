using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Phone : SimpleClass
	{
		private Person _Person;

		public override string Name
		{
			get
			{
				return (string)base.GetValue("NumberPhone");
			}
			set
			{
				base.SetValue("NumberPhone", value);
			}
		}

		public string NumberPhone
		{
			get
			{
				return (string)base.GetValue("NumberPhone");
			}
			set
			{
				base.SetValue("NumberPhone", value);
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

		public Phone()
		{
			this.NameTable = "Phone";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}