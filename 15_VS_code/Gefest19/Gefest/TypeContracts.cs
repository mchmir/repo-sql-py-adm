using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeContracts : SimpleClasss
	{
		public TypeContract this[int index]
		{
			get
			{
				return (TypeContract)base.get_Item(index);
			}
		}

		public TypeContracts()
		{
			this.NameTable = "TypeContract";
		}

		public TypeContract Add()
		{
			TypeContract typeContract = new TypeContract();
			typeContract.Init();
			typeContract.set_Parent(this);
			return typeContract;
		}

		public TypeContract item(long ID)
		{
			TypeContract elementByID;
			try
			{
				elementByID = (TypeContract)base.GetElementByID(ID);
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
			TypeContract typeContract = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeContract");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeContract = new TypeContract();
						typeContract.SetState(row);
						base.Add(typeContract);
					}
					typeContract = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeContract = null;
				num = 1;
			}
			return num;
		}
	}
}