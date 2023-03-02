using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Phones : SimpleClasss
	{
		private Person _person;

		public Phone this[int index]
		{
			get
			{
				return (Phone)base.get_Item(index);
			}
		}

		public Phones()
		{
			this.NameTable = "Phone";
		}

		public Phone Add()
		{
			Phone phone = new Phone();
			phone.Init();
			phone.set_Parent(this);
			if (this._person != null)
			{
				phone.oPerson = this._person;
			}
			return phone;
		}

		public Phone item(long ID)
		{
			Phone elementByID;
			try
			{
				elementByID = (Phone)base.GetElementByID(ID);
			}
			catch
			{
				elementByID = null;
			}
			return elementByID;
		}

		public override int Load()
		{
			return 1;
		}

		public int Load(Person oPerson)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			this._person = oPerson;
			Phone phone = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "IdPerson" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", this._person.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDPhone desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						phone = new Phone();
						phone.SetState(row);
						if (this._person != null)
						{
							phone.oPerson = this._person;
						}
						base.Add(phone);
					}
					phone = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				phone = null;
				num = 1;
			}
			return num;
		}
	}
}