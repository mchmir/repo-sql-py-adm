using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypePGs : SimpleClasss
	{
		public TypePG this[int index]
		{
			get
			{
				return (TypePG)base.get_Item(index);
			}
		}

		public TypePGs()
		{
			this.NameTable = "TypePG";
		}

		public TypePG Add()
		{
			TypePG typePG = new TypePG();
			typePG.Init();
			typePG.set_Parent(this);
			return typePG;
		}

		public TypePG item(long ID)
		{
			TypePG elementByID;
			try
			{
				elementByID = (TypePG)base.GetElementByID(ID);
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
			TypePG typePG = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypePG");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typePG = new TypePG();
						typePG.SetState(row);
						base.Add(typePG);
					}
					typePG = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typePG = null;
				num = 1;
			}
			return num;
		}
	}
}