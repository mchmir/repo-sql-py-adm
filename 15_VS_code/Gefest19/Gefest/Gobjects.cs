using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class Gobjects : SimpleClasss
	{
		private Contract _contract;

		public Gobject this[int index]
		{
			get
			{
				return (Gobject)base.get_Item(index);
			}
		}

		public Gobjects()
		{
			this.NameTable = "Gobject";
		}

		public Gobject Add()
		{
			Gobject gobject = new Gobject();
			gobject.Init();
			gobject.set_Parent(this);
			gobject.oContract = this._contract;
			gobject.oAddress = this._contract.oPerson.oAddress;
			return gobject;
		}

		public Gobject item(long ID)
		{
			Gobject elementByID;
			try
			{
				elementByID = (Gobject)base.GetElementByID(ID);
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

		public int Load(Contract oContract)
		{
			int num;
			int num1 = 0;
			this._contract = oContract;
			DataTable dataTable = null;
			Gobject gobject = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idContract" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oContract.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idstatusgobject desc, idgobject desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gobject = new Gobject();
						gobject.SetState(row);
						gobject.oContract = this._contract;
						base.Add(gobject);
					}
					gobject = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gobject = null;
				num = 1;
			}
			return num;
		}

		public int Load(Address oAddress)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Gobject gobject = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idaddress" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oAddress.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idgobject desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gobject = new Gobject();
						gobject.SetState(row);
						base.Add(gobject);
					}
					gobject = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gobject = null;
				num = 1;
			}
			return num;
		}

		public int Load(Contract oContract, StatusGObject oStatusGObject)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Gobject gobject = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idcontract", "idStatusGObject" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oContract.get_ID()), string.Concat("=", oStatusGObject.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "idstatusgobject, idgobject");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						gobject = new Gobject();
						gobject.SetState(row);
						base.Add(gobject);
					}
					gobject = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				gobject = null;
				num = 1;
			}
			return num;
		}
	}
}