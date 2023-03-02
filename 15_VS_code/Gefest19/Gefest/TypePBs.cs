using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypePBs : SimpleClasss
	{
		public TypePB this[int index]
		{
			get
			{
				return (TypePB)base.get_Item(index);
			}
		}

		public TypePBs()
		{
			this.NameTable = "TypePB";
		}

		public TypePB Add()
		{
			TypePB typePB = new TypePB();
			typePB.Init();
			typePB.set_Parent(this);
			return typePB;
		}

		public TypePB item(long ID)
		{
			TypePB elementByID;
			try
			{
				elementByID = (TypePB)base.GetElementByID(ID);
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
			TypePB typePB = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypePB");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typePB = new TypePB();
						typePB.SetState(row);
						base.Add(typePB);
					}
					typePB = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typePB = null;
				num = 1;
			}
			return num;
		}
	}
}