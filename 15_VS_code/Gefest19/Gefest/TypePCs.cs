using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypePCs : SimpleClasss
	{
		public TypePC this[int index]
		{
			get
			{
				return (TypePC)base.get_Item(index);
			}
		}

		public TypePCs()
		{
			this.NameTable = "TypePC";
		}

		public TypePC Add()
		{
			TypePC typePC = new TypePC();
			typePC.Init();
			typePC.set_Parent(this);
			return typePC;
		}

		public TypePC item(long ID)
		{
			TypePC elementByID;
			try
			{
				elementByID = (TypePC)base.GetElementByID(ID);
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
			TypePC typePC = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypePC");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typePC = new TypePC();
						typePC.SetState(row);
						base.Add(typePC);
					}
					typePC = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typePC = null;
				num = 1;
			}
			return num;
		}
	}
}