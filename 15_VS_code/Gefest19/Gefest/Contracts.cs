using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Contracts : SimpleClasss
	{
		private Person _person;

		public Contract this[int index]
		{
			get
			{
				return (Contract)base.get_Item(index);
			}
		}

		public Contracts()
		{
			this.NameTable = "Contract";
		}

		public Contract Add()
		{
			Contract contract = new Contract();
			contract.set_Parent(this);
			if (this._person != null)
			{
				contract.oPerson = this._person;
			}
			return contract;
		}

		public Contract item(long ID)
		{
			Contract elementByID;
			try
			{
				elementByID = (Contract)base.GetElementByID(ID);
			}
			catch
			{
				elementByID = null;
			}
			return elementByID;
		}

		public override int Load()
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Contract contract = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDContract");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						contract = new Contract();
						contract.SetState(row);
						base.Add(contract);
					}
					contract = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				contract = null;
				num = 1;
			}
			return num;
		}

		public int Load(string Account)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Contract contract = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "Account" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("='", Account, "'") };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "Account desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						contract = new Contract();
						contract.SetState(row);
						if (this._person != null)
						{
							contract.oPerson = this._person;
						}
						base.Add(contract);
					}
					contract = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				contract = null;
				num = 1;
			}
			return num;
		}

		public int Load(Person oPerson)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			this._person = oPerson;
			Contract contract = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "IdPerson" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", this._person.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "Account desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						contract = new Contract();
						contract.SetState(row);
						base.Add(contract);
					}
					contract = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				contract = null;
				num = 1;
			}
			return num;
		}
	}
}