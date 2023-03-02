using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Operations : SimpleClasss
	{
		public Operation this[int index]
		{
			get
			{
				return (Operation)base.get_Item(index);
			}
		}

		public Operations()
		{
			this.NameTable = "Operation";
		}

		public Operation Add()
		{
			Operation operation = new Operation();
			operation.set_Parent(this);
			return operation;
		}

		public Operation item(long ID)
		{
			Operation elementByID;
			try
			{
				elementByID = (Operation)base.GetElementByID(ID);
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
			Operation operation = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDOperation");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						operation = new Operation();
						operation.SetState(row);
						base.Add(operation);
					}
					operation = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				operation = null;
				num = 1;
			}
			return num;
		}

		public int Load(Document oDocument)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Operation operation = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "iddocument" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oDocument.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDOperation");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						operation = new Operation();
						operation.SetState(row);
						base.Add(operation);
					}
					operation = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				operation = null;
				num = 1;
			}
			return num;
		}
	}
}