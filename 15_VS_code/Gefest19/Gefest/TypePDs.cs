using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypePDs : SimpleClasss
	{
		public TypePD this[int index]
		{
			get
			{
				return (TypePD)base.get_Item(index);
			}
		}

		public TypePDs()
		{
			this.NameTable = "TypePD";
		}

		public TypePD Add()
		{
			TypePD typePD = new TypePD();
			typePD.Init();
			typePD.set_Parent(this);
			return typePD;
		}

		public TypePD item(long ID)
		{
			TypePD elementByID;
			try
			{
				elementByID = (TypePD)base.GetElementByID(ID);
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
			TypePD typePD = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypePD");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typePD = new TypePD();
						typePD.SetState(row);
						base.Add(typePD);
					}
					typePD = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typePD = null;
				num = 1;
			}
			return num;
		}
	}
}