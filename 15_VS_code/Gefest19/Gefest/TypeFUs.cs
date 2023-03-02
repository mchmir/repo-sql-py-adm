using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeFUs : SimpleClasss
	{
		public TypeFU this[int index]
		{
			get
			{
				return (TypeFU)base.get_Item(index);
			}
		}

		public TypeFUs()
		{
			this.NameTable = "TypeFU";
		}

		public TypeFU Add()
		{
			TypeFU typeFU = new TypeFU();
			typeFU.Init();
			typeFU.set_Parent(this);
			return typeFU;
		}

		public TypeFU item(long ID)
		{
			TypeFU elementByID;
			try
			{
				elementByID = (TypeFU)base.GetElementByID(ID);
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
			TypeFU typeFU = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeFU DESC");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeFU = new TypeFU();
						typeFU.SetState(row);
						base.Add(typeFU);
					}
					typeFU = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeFU = null;
				num = 1;
			}
			return num;
		}
	}
}