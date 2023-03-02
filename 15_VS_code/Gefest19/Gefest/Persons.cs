using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Persons : SimpleClasss
	{
		public Person this[int index]
		{
			get
			{
				return (Person)base.get_Item(index);
			}
		}

		public Persons()
		{
			this.NameTable = "Person";
		}

		public Person Add()
		{
			Person person = new Person();
			person.set_Parent(this);
			return person;
		}

		public Person item(long ID)
		{
			Person elementByID;
			try
			{
				elementByID = (Person)base.GetElementByID(ID);
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
			Person person = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idPerson" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oPerson.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idPerson");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						person = new Person();
						person.SetState(row);
						base.Add(person);
					}
					person = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				person = null;
				num = 1;
			}
			return num;
		}
	}
}