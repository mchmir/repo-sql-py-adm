using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class PCs : SimpleClasss
	{
		public PC this[int index]
		{
			get
			{
				return (PC)base.get_Item(index);
			}
		}

		public PCs()
		{
			this.NameTable = "PC";
		}

		public PC Add()
		{
			PC pC = new PC();
			pC.Init();
			pC.set_Parent(this);
			return pC;
		}

		public PC item(long ID)
		{
			PC elementByID;
			try
			{
				elementByID = (PC)base.GetElementByID(ID);
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
			DataTable dataTable = null;
			PC pC = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idContract" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oContract.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDTypePC, IdPeriod DESC");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						pC = new PC();
						pC.SetState(row);
						base.Add(pC);
					}
					pC = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				pC = null;
				num = 1;
			}
			return num;
		}
	}
}